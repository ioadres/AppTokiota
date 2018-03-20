using AppTokiota.Services;
using System;
namespace AppTokiota.Components.Core.Module
{
    public interface ITimesheetModule : IBaseModule
    {
        IAuthenticationService AuthenticationService { get; }
        IDialogService DialogService { get; }
        ITimesheetService TimesheetService { get; }
        ICalendarService CalendarService { get; }
    }
}
