using AppTokiota.Services;
using System;
namespace AppTokiota.Components.Core.Module
{
    public interface ITimesheetModule : IBaseModule
    {
        ITimesheetService TimesheetService { get; }
        ICalendarService CalendarService { get; }
    }
}
