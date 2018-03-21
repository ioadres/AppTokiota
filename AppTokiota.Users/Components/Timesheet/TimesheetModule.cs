using System;
using AppTokiota.Users.Components.Core.Module;
using Prism.Ioc;
using AppTokiota.Users.Services;

namespace AppTokiota.Users.Components.Timesheet
{
    public class TimesheetModule : ITimesheetModule
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly ITimesheetService _timesheetService;
        private readonly ICalendarService _calendarService;

        public IAuthenticationService AuthenticationService => _authenticationService;
        public IDialogService DialogService => _dialogService;
        public ITimesheetService TimesheetService => _timesheetService;
        public ICalendarService CalendarService => _calendarService;

        public TimesheetModule(IAuthenticationService authenticationService, IDialogService dialogService, ITimesheetService timesheetService, ICalendarService calendarService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _timesheetService = timesheetService;
            _calendarService = calendarService;
        }
        public static string Tag => nameof(TimesheetPage);
        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TimesheetPage, TimesheetPageViewModel>();
        }
    }
}
