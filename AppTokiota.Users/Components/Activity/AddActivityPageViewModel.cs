using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Prism.Navigation;
using Prism.Commands;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;

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

        private string _timeSelectedImputation;
        public string TimeSelectedImputation
        {
            get { return _timeSelectedImputation; }
            set { SetProperty(ref _timeSelectedImputation, value); }
        }

        private string _timeSelectedDesviation;
        public string TimeSelectedDesviation
        {
            get { return _timeSelectedDesviation; }
            set { SetProperty(ref _timeSelectedDesviation, value); }
        }

        #region TimeDesviationAction
        public DelegateCommand<Dictionary<string, string>> TimeDesviationCommand => new DelegateCommand<Dictionary<string, string>>(TimeDesviationAction);
        protected void TimeDesviationAction(Dictionary<string, string> response)
        {
            TimeSelectedDesviation = response["Format"];
        }
        #endregion

        #region TimeImputationAction
        public DelegateCommand<Dictionary<string, string>> TimeImputationCommand => new DelegateCommand<Dictionary<string, string>>(TimeImputationAction);
        protected void TimeImputationAction(Dictionary<string, string> response)
        {
            TimeSelectedImputation = response["Format"];
        }
        #endregion

        #region TimeImputedOpen
        public DelegateCommand TimeImputedOpenCommand => new DelegateCommand(TimeImputedOpen);
        protected void TimeImputedOpen()
        {
            TimeImputationEntryVisibility = !TimeImputationEntryVisibility;
        }
        #endregion

        #region TimeDesviationAction
        public DelegateCommand TimeDesviationOpenCommand => new DelegateCommand(TimeDesviationOpen);
        protected void TimeDesviationOpen()
        {
            TimeDesviationEntryVisibility = !TimeDesviationEntryVisibility;
        }
        #endregion

        #region TimeDesviationAction
        public DelegateCommand ClosePopupCommand => new DelegateCommand(ClosePopup);
        protected void ClosePopup()
        {
            PopupNavigation.PopAllAsync();
        }
        #endregion

        public AddActivityPageViewModel(IViewModelBaseModule baseModule, IAddActivityModule addActivityModule) : base(baseModule)
        {
           _addActivityModule = addActivityModule;

            Title = "New Activity";
        }    

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
            Title = _currentTimesheetForDay.Day.Date.ToString("dd-MM-yyyy");
            BaseModule.NavigationService.GoBackAsync();
        }

		public override void OnNavigatedFrom(NavigationParameters parameters)
		{
            //_currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
		}
	}
}

