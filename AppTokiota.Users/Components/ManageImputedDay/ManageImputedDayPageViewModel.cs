 using System;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using Prism.Navigation;
using AppTokiota.Users.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Xamarin.Forms;
using AppTokiota.Users.Components.Activity;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;

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
            Title = "";
            IsBusy = true;
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

        private bool _isVisibleButtonAdd = false;
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
        /// Gets or sets the Total DeviationTotal 
        /// </summary>
        /// <value>The Total DeviationTotal</value>
        private bool _anyActivities = false;
        public bool AnyActivities
        {
            get { return _anyActivities; }
            set { 
                SetProperty(ref _anyActivities, value);            
            }
        }

        /// <summary>
        /// Gets or sets the Visibility Any Activity 
        /// </summary>
        /// <value>The Total DeviationTotal</value>
        private bool _anyActivitiesIcon = true;
        public bool AnyActivitiesIcon
        {
            get { return _anyActivitiesIcon; }
            set
            {
                SetProperty(ref _anyActivitiesIcon, value);
            }
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
        protected async void OnDeleteItem(ActivityDay activity)
        {
            if(!IsEnabled) {
                BaseModule.DialogService.ShowToast("This day is closed!");
            }


            if (IsEnabled && IsInternetAndCloseModal())
            {
                var remove = await BaseModule.DialogService.ShowConfirmAsync("Are your sure you want delete this activity?", "Delete Activity", "Delete", "Cancel");
                if (remove)
                {
                    try
                    {
                        BaseModule.AnalyticsService.TrackEvent("[DeleteActivity] :: Start");
                        var result = await _manageImputedDayModule.TimesheetService.DeleteActivityTimesheet(activity.Date, activity.Id);
                        if (result != null)
                        {
                            _currentTimesheetForDay.Activities.Remove(activity);
                            UpdateDayOfTimesheet(_currentTimesheetForDay);
                            IsBusy = false;
                            BaseModule.AnalyticsService.TrackEvent("[DeleteActivity] :: Success");
                        }
                        else
                        {
                            IsBusy = false;
                            BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                            BaseModule.AnalyticsService.TrackEvent("[DeleteActivity] :: Error");
                        }
                    }
                    catch (Exception)
                    {
                        IsBusy = false;
                        BaseModule.DialogErrorCustomService.DialogErrorCommonTryAgain();
                    }
                }
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
            BaseModule.AnalyticsService.TrackEvent("[Activity] :: Add :: Single :: ManageImputedDay");
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
            IsBusy = true;
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
            } else{
                IsVisibleButtonAdd = true;
            }

            IsBusy = false;
        }

        private void UpdateDayOfTimesheet(TimesheetForDay timesheet)
        {
            _currentTimesheetForDay = timesheet;

            Device.BeginInvokeOnMainThread(() =>
            {
                Activities = new ObservableCollection<ActivityDay>(timesheet.Activities);
                ImputedTotal = Activities.Sum(x => x.Imputed);
                DeviationTotal = Activities.Sum(x => x.Deviation);
                AnyActivities = timesheet.Activities.Any();
                AnyActivitiesIcon = AnyActivities;
                IsEnabled = !_currentTimesheetForDay.Day.IsClosed;
            });
        }

    }
}
