using System;
using AppTokiota.Components.Core.Module;
using Prism.Ioc;

namespace AppTokiota.Components.Timesheet
{
    public class TimesheetModule : ITimesheetModule
    {
        public static string Tag => nameof(TimesheetPage);

        public TimesheetModule()
        {
        }

        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TimesheetPage, TimesheetPageViewModel>();
        }
    }
}
