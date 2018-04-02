using System;
using AppTokiota.Users.Components.Core.Module;
using Prism.Ioc;
using AppTokiota.Users.Services;
using AppTokiota.Users.Components.ManageImputedDay;

namespace AppTokiota.Users.Components.ManageImputedDay
{
    public class ManageImputedDayModule : IManageImputedDayModule
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly ITimesheetService _timesheetService;

        public IAuthenticationService AuthenticationService => _authenticationService;
        public IDialogService DialogService => _dialogService;
        public ITimesheetService TimesheetService => _timesheetService;

        public ManageImputedDayModule(IAuthenticationService authenticationService, IDialogService dialogService, ITimesheetService timesheetService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _timesheetService = timesheetService;
        }

        public static string Tag => nameof(ManageImputedDayPage);
        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ManageImputedDayPage, ManageImputedDayPageViewModel>();
        }
    }
}