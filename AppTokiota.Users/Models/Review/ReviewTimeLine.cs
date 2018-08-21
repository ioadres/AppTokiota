using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AppTokiota.Users.Models
{
    public class ReviewTimeLine
    {
        public Day Day { get; set; }
        public bool IsLast { get; set; } = false;

        public int ProjectsForDay { get; set; }
        public int TasksForDay { get; set; }
        public double ImputationTasksDay { get; set; }
        public double DesviationTasksDay { get; set; }

        public static ReviewTimeLine Map(ItemTimeLine x)
        {
            var currentTimeSheetDay = new ReviewTimeLine();
            currentTimeSheetDay.ProjectsForDay = x.Activities.Select(y => y.Project.Id).Distinct().Count();
            currentTimeSheetDay.TasksForDay = x.Activities.Select(y => y.Task.Id).Distinct().Count();
            currentTimeSheetDay.DesviationTasksDay = x.Activities.Sum(d => d.Deviation);
            currentTimeSheetDay.ImputationTasksDay = x.Activities.Sum(d => d.Imputed);
            currentTimeSheetDay.Day = x.Day;
            currentTimeSheetDay.IsLast = x.IsLast;
            return currentTimeSheetDay;
        }
    }
}
