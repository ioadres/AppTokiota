using AppTokiota.Users.Components.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Services;
using Prism.Ioc;
using AppTokiota.Users.Components.Core;

namespace AppTokiota.Users.Components.DashBoard
{
    public class DashBoardModule : IDashBoardModule
    {
		private readonly ITimesheetService _timesheetService;
		private readonly IChartService _chartService;

		public ITimesheetService TimesheetService => _timesheetService;

		public IChartService ChartService => _chartService;

		public DashBoardModule(ITimesheetService timesheetService, IChartService chartService)
        {
			_timesheetService = timesheetService;
			_chartService = chartService;
        }
    }
}
