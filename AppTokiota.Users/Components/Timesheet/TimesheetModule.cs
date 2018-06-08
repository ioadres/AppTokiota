using System;
using AppTokiota.Users.Components.Core.Module;
using Prism.Ioc;
using AppTokiota.Users.Services;

namespace AppTokiota.Users.Components.Timesheet
{
    public class TimesheetModule : ITimesheetModule
    {
        private readonly ITimesheetService _timesheetService;
        private readonly ICalendarService _calendarService;

        public ITimesheetService TimesheetService => _timesheetService;
        public ICalendarService CalendarService => _calendarService;

        public TimesheetModule(ITimesheetService timesheetService, ICalendarService calendarService)
        {
            _timesheetService = timesheetService;
            _calendarService = calendarService;
        }
    }
}
