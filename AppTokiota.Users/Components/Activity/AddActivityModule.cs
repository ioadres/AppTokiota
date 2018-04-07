using System;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Services;
using Prism.Ioc;

namespace AppTokiota.Users.Components.Activity
{
    public class AddActivityModule : IAddActivityModule
    {
        private readonly ITimesheetService _timesheetService;
        public ITimesheetService TimesheetService => _timesheetService;

        public AddActivityModule(ITimesheetService timesheetService)
        {         
            _timesheetService = timesheetService;
        }
    }
}