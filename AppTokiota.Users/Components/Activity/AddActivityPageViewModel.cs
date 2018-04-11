using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Prism.Navigation;
using Prism.Commands;

namespace AppTokiota.Users.Components.Activity
{
    public class AddActivityPageViewModel : ViewModelBase
    {
        #region Services
        protected readonly IAddActivityModule _addActivityModule;
        #endregion

        private Models.TimesheetForDay _currentTimesheetForDay;
        public Models.TimesheetForDay CurrentTimesheetForDay
        {
            get { return _currentTimesheetForDay; }
        }

        private bool _timeImputationEntryVisibility;
        public bool TimeImputationEntryVisibility
        {
            get { return _timeImputationEntryVisibility; }
            set { SetProperty(ref _timeImputationEntryVisibility, value); }
        }

        private bool _timeDesviationEntryVisibility;
        public bool TimeDesviationEntryVisibility
        {
            get { return _timeDesviationEntryVisibility; }
            set { SetProperty(ref _timeDesviationEntryVisibility, value); }
        }

        #region TimeDesviationAction
        public DelegateCommand TimeDesviationCommand => new DelegateCommand(TimeDesviationAction);
        protected async void TimeDesviationAction()
        {
        }
        #endregion

        #region TimeImputationAction
        public DelegateCommand TimeImputationCommand => new DelegateCommand(TimeImputationAction);
        protected async void TimeImputationAction()
        {
        }
        #endregion

        #region TimeImputedOpen
        public DelegateCommand TimeImputedOpenCommand => new DelegateCommand(TimeImputedOpen);
        protected async void TimeImputedOpen()
        {
            TimeImputationEntryVisibility = true;
        }
        #endregion

        #region TimeDesviationAction
        public DelegateCommand TimeDesviationOpenCommand => new DelegateCommand(TimeDesviationOpen);
        protected async void TimeDesviationOpen()
        {
            TimeDesviationEntryVisibility = true;
        }
        #endregion

        public AddActivityPageViewModel(IViewModelBaseModule baseModule, IAddActivityModule addActivityModule) : base(baseModule)
        {
           _addActivityModule = addActivityModule;

            Title = "Add Activity";
        }    

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
        }

		public override void OnNavigatedFrom(NavigationParameters parameters)
		{
           
		}
	}
}

