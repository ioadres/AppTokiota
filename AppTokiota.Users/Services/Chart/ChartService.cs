using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTokiota.Users.Helpers;
using Microcharts;
using SkiaSharp;

namespace AppTokiota.Users.Services
{
	public class ChartService :IChartService
    {
		public List<Entry> GenerateChartActivitiesImputedGroupByTaskAndProject(Models.Timesheet timesheet) {

            var projectsGroup = timesheet.Activities.GroupBy(x => new { x.Value.ProjectId, x.Value.TaskId });
            var entries = new List<Microcharts.Entry>();
            var indexColor = 0;
            foreach (var item in projectsGroup)
            {

                var tempProject = timesheet.Projects.FirstOrDefault(x => x.Value.Id.Equals(item.Key.ProjectId));
                var tempTask = tempProject.Value?.Tasks.FirstOrDefault(x => x.Value.Id.Equals(item.Key.TaskId));

                var imputed = item.Sum(x => x.Value.Imputed);

                entries.Add(new Entry(float.Parse(imputed.ToString()))
                {
					Color = GenerateColors.GetColor(indexColor),
					TextColor = GenerateColors.GetColor(indexColor),
                    Label = TruncateLongString(tempTask.Value.Value.DisplayName, 20),
					ValueLabel = TimeFormat.Format(imputed)
                });
                indexColor++;
            }

			return entries;

        }

		public List<Entry> GenerateChartImputationMonthVsHourMonthExpected(Models.Timesheet timesheet)
        {
            var entries = new List<Microcharts.Entry>();
            var totalHoursMonth = (8 * timesheet.Days.Where(x => x.IsClosed == false && x.IsWeekend == false && (x.Holiday == null || x.Holiday?.IsHolyday == false)).Count()) * 60;

            var consumed = timesheet.Activities.Sum(x => x.Value.Imputed);
            var pending = totalHoursMonth - consumed;
            pending = pending < 0 ? pending = 0 : pending;

            entries.Add(new Entry(float.Parse(consumed.ToString()))
            {
				Color = GenerateColors.GetColor(2),
				TextColor = GenerateColors.GetColor(2),
                Label = "Consumed",
				ValueLabel = TimeFormat.Format(consumed)
            });


            entries.Add(new Entry(float.Parse(pending.ToString()))
            {
				Color = GenerateColors.GetColor(3),
				TextColor = GenerateColors.GetColor(3),
                Label = "Pending",
				ValueLabel = TimeFormat.Format(pending)
            });
			return entries;
        }

		public List<Entry> GenerateChartActivitiesImputationVsDeviation(Models.Timesheet timesheet)
        { 
			var entries = new List<Microcharts.Entry>();
            var imputed = timesheet.Activities.Sum(x => x.Value.Imputed);
            var desviation = timesheet.Activities.Sum(x => x.Value.Deviation);

            entries.Add(new Entry(float.Parse(imputed.ToString()))
            {
				Color = GenerateColors.GetColor(0),
                TextColor = GenerateColors.GetColor(0),
                Label = "Imputed",
				ValueLabel = TimeFormat.Format(imputed)
            });

			entries.Add(new Entry(float.Parse(desviation.ToString()))
			{
				Color = GenerateColors.GetColor(1),
                TextColor = GenerateColors.GetColor(1),
				Label = "Deviation",
				ValueLabel = TimeFormat.Format(desviation)
			});

			return entries;
             
        }

        private string TruncateLongString(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }       

		
    }
}
