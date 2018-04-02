using System;
namespace AppTokiota.Users.Components.Core.Module
{
    public interface IManageImputedDayModule: IBaseModule
    {
        Services.ITimesheetService TimesheetService { get; }
    }
}
