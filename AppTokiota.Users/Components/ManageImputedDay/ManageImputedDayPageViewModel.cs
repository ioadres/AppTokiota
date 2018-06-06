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
using System.Diagnostics;

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

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }

        private bool _isVisibleButtonAdd = true;
        public bool IsVisibleButtonAdd
        {
            get { return _isVisibleButtonAdd; }
            set { SetProperty(ref _isVisibleButtonAdd, value); }
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
        /// Gets or sets the Total DeviationTotal 
        /// </summary>
        /// <value>The Total DeviationTotal</value>
        private double _deviationTotal;
        public double DeviationTotal
        {
            get { return _deviationTotal; }
            set { SetProperty(ref _deviationTotal, value); }
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
			IsBusy = true;
			if (IsInternetAndCloseModal())
			{
				Device.BeginInvokeOnMainThread(async () =>
				{
					try
					{
						var result = await _manageImputedDayModule.TimesheetService.DeleteActivityTimesheet(activity.Date, activity.Id);
						if (result != null)
						{

							_currentTimesheetForDay.Activities.Remove(activity);
							UpdateDayOfTimesheet(_currentTimesheetForDay);
							IsBusy = false;
						}
						else
						{
							IsBusy = false;
							BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
						}
					} catch(Exception e) {
						IsBusy = false;
						Debug.WriteLine(e);
						BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
					}
				});
			}
        }
        #endregion

        #region EventOnAddItem
        public DelegateCommand OnAddItemCommand => new DelegateCommand(() => OnAddItem());
        protected async void OnAddItem()
        {
            var navigationParameters = new NavigationParameters();
            var imputedContext = new Imputed()
            {
                CurrentTimesheet = _currentTimesheetForDay
            };
            navigationParameters.Add(Imputed.Tag, imputedContext);
            await BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<AddActivityPage>(), navigationParameters, false, true);           
        }
        #endregion

        #region EventOnInfoActivityItemCommand
        public DelegateCommand<object> OnInfoActivityItemCommand => new DelegateCommand<object>((obj) => { OnInfoActivityItem((ActivityDay)obj); });
        protected void OnInfoActivityItem(ActivityDay from)
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(ActivityDay.Tag, from);
            BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<InfoActivityPopUpPage>(), navigationParameters, true, true);
        }
        #endregion

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var keyContains = parameters.ContainsKey(TimesheetForDay.Tag);
            if(keyContains) {
                var currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
                UpdateDayOfTimesheet(currentTimesheetForDay);
                Title = currentTimesheetForDay.Day.Date.ToString("yyyy-MM-dd");
            }

			keyContains = parameters.ContainsKey(Models.ActivityDay.Tag);
			if (keyContains)
            {
				var activity = parameters.GetValue<Models.ActivityDay>(Models.ActivityDay.Tag);
				_currentTimesheetForDay.Activities.Add(activity);
				UpdateDayOfTimesheet(_currentTimesheetForDay);
            }

            keyContains = parameters.ContainsKey("IsVisibleButtonAdd");
            if (keyContains)
            {
                IsVisibleButtonAdd = parameters.GetValue<bool>("IsVisibleButtonAdd");
            }
        }

        private void UpdateDayOfTimesheet(TimesheetForDay timesheet)
        {
            _currentTimesheetForDay = timesheet;

            Device.BeginInvokeOnMainThread(() =>
            {
                Activities = new ObservableCollection<ActivityDay>(timesheet.Activities);
                ImputedTotal = Activities.Sum(x => x.Imputed);
                DeviationTotal = Activities.Sum(x => x.Deviation);

                IsEnabled = !_currentTimesheetForDay.Day.IsClosed;
            });
        }

    }
}
