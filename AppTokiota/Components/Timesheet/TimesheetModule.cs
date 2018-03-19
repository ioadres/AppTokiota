using System;
using AppTokiota.Components.Core.Module;
using Prism.Ioc;
using AppTokiota.Services;

namespace AppTokiota.Components.Timesheet
{
    public class TimesheetModule : ITimesheetModule
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly ITimesheetService _timesheetService;

        public IAuthenticationService AuthenticationService => _authenticationService;
        public IDialogService DialogService => _dialogService;
        public ITimesheetService TimesheetService => _timesheetService;

        public TimesheetModule(IAuthenticationService authenticationService, IDialogService dialogService, ITimesheetService timesheetService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _timesheetService = timesheetService;
        }
        public static string Tag => nameof(TimesheetPage);
        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TimesheetPage, TimesheetPageViewModel>();
        }
    }
}
