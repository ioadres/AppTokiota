using System;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using Prism.Navigation;
using AppTokiota.Users.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppTokiota.Users.Components.Activity;
using Rg.Plugins.Popup.Services;

namespace AppTokiota.Users.Components.ManageImputedDay
{
    public class ManageImputedDayPageViewModel: ViewModelBase
    {
        #region Services
        protected readonly IManageImputedDayModule _manageImputedDayModule;
        #endregion

        public ManageImputedDayPageViewModel(IViewModelBaseModule baseModule, IManageImputedDayModule manageImputedDayModule) : base(baseModule)
        {
            _manageImputedDayModule = manageImputedDayModule;

            Title = "Imputed Day";
        }

        private Models.TimesheetForDay _currentTimesheetForDay;
        public Models.TimesheetForDay CurrentTimesheetForDay
        {
            get { return _currentTimesheetForDay; }
        }

        private bool _isEnable;
        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }

        /// <summary>
        /// Gets or sets the activities collection dates 
        /// </summary>
        /// <value>The activities</value>
        private ObservableCollection<ActivityDay> _activities;
        public ObservableCollection<ActivityDay> Activities
        {
            get { return _activities; }
            set { SetProperty(ref _activities, value); }
        }

        /// <summary>
        /// Gets or sets the Total Desviation 
        /// </summary>
        /// <value>The Total Desviation</value>
        private double _desviationTotal;
        public double DesviationTotal
        {
            get { return _desviationTotal; }
            set { SetProperty(ref _desviationTotal, value); }
        }

        /// <summary>
        /// Gets or sets the Total Imputed 
        /// </summary>
        /// <value>The Total Imputed</value>
        private double _imputedTotal;
        public double ImputedTotal
        {
            get { return _imputedTotal; }
            set { SetProperty(ref _imputedTotal, value); }
        }

        #region EventOnDeleteItem
        public DelegateCommand<object> OnDeleteItemCommand => new DelegateCommand<object>((obj) => { OnDeleteItem((ActivityDay)obj); });
        protected void OnDeleteItem(ActivityDay activity)
        {
            _currentTimesheetForDay.Activities.Remove(activity);
            UpdateDayOfTimesheet(_currentTimesheetForDay);
        }
        #endregion

        #region EventOnEditItem
        public DelegateCommand<object> OnEditItemCommand => new DelegateCommand<object>((obj) => { OnEditItem((TimesheetForDay)obj); });
        protected void OnEditItem(TimesheetForDay from)
        {

        }
        #endregion

        #region EventOnEditItem
        public DelegateCommand OnAddItemCommand => new DelegateCommand(() => OnAddItem());
        protected async void OnAddItem()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(TimesheetForDay.Tag, _currentTimesheetForDay);
            await BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<AddActivityPage>(), navigationParameters, true);
        }
        #endregion

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var keyContains = parameters.ContainsKey(TimesheetForDay.Tag);
            if(keyContains) {
                _currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
                UpdateDayOfTimesheet(_currentTimesheetForDay);
                Title = _currentTimesheetForDay.Day.Date.ToString("yyyy-MM-dd");
            }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        private async void UpdateDayOfTimesheet(TimesheetForDay timesheet) {
            await Task.Run(() =>
            {
                Activities = new ObservableCollection<ActivityDay>(timesheet.Activities);
                ImputedTotal = Activities.Sum(x => x.Imputed);
                DesviationTotal = Activities.Sum(x => x.Deviation);
                _currentTimesheetForDay = timesheet;
                IsEnable = !_currentTimesheetForDay.Day.IsClosed;
            });
        }


    }
}
