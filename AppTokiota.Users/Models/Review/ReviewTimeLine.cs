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
    }
}
