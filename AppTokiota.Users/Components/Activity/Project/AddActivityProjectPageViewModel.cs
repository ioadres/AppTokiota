using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Components.Activity
{
    public class AddActivityProjectPageViewModel : ViewModelBase
    {
        #region Services
        protected readonly IAddActivityModule _addActivityModule;
        #endregion

        private Models.TimesheetForDay _currentTimesheetForDay;
        public Models.TimesheetForDay CurrentTimesheetForDay
        {
            get { return _currentTimesheetForDay; }
        }

        private bool _confirmVisibility;
        public bool ConfirmVisibility
        {
            get { return _confirmVisibility; }
            set { SetProperty(ref _confirmVisibility, value); }
        }

        

        #region GoBack
        public DelegateCommand GoBackCommand => new DelegateCommand(GoBack);
        protected void GoBack()
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add(TimesheetForDay.Tag, _currentTimesheetForDay);
            BaseModule.NavigationService.NavigateAsync(PageRoutes.GetKey<AddActivityTimeDesviationPage>(), navigationParameters, false, false);
        }
        #endregion

        #region CloseAction
        public DelegateCommand CloseCommand => new DelegateCommand(Close);
        protected void Close()
        {
            BaseModule.NavigationService.GoBackAsync();
        }
        #endregion

        public AddActivityProjectPageViewModel(IViewModelBaseModule baseModule, IAddActivityModule addActivityModule) : base(baseModule)
        {
            _addActivityModule = addActivityModule;
            Title = "New Activity";
            ConfirmVisibility = true;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _currentTimesheetForDay = parameters.GetValue<TimesheetForDay>(TimesheetForDay.Tag);
            Title = _currentTimesheetForDay.Day.Date.ToString("dd-MM-yyyy");

        }
    }
}
