using System;
using AppTokiota.Users.Services;

namespace AppTokiota.Users.Components.Core.Module
{
    public interface IAddActivityModule
    {
        ITimesheetService TimesheetService { get; }
    }
}
