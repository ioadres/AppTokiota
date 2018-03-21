using AppTokiota.Users.Services;
using System;
namespace AppTokiota.Users.Components.Core.Module
{
    public interface ITimesheetModule : IBaseModule
    {
        ITimesheetService TimesheetService { get; }
        ICalendarService CalendarService { get; }
    }
}
