using System;
using AppTokiota.Users.Components.Core.Module;
using Prism.Ioc;
using AppTokiota.Users.Services;
namespace AppTokiota.Users.Components.ManageImputedDay
{
    public class ManageImputedDayModule : IManageImputedDayModule
    {
        private readonly ITimesheetService _timesheetService;        
        public ITimesheetService TimesheetService => _timesheetService;

        public ManageImputedDayModule(TimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }
    }
}