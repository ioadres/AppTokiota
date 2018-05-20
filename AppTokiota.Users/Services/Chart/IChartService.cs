using System;
using System.Collections.Generic;
using Microcharts;

namespace AppTokiota.Users.Services
{
    public interface IChartService
    {
		List<Entry> GenerateChartActivitiesImputedGroupByTaskAndProject(Models.Timesheet timesheet);
		List<Entry> GenerateChartImputationMonthVsHourMonthExpected(Models.Timesheet timesheet);
		List<Entry> GenerateChartActivitiesImputationVsDeviation(Models.Timesheet timesheet);     
    }
}
