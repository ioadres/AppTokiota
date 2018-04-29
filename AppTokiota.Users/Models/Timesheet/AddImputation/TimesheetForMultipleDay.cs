using System;
using System.Collections.Generic;

namespace AppTokiota.Users.Models
{
    public class TimesheetForMultipleDay : TimesheetImutationBase
    {
        public static string Tag = nameof(TimesheetForMultipleDay);
        public List<Day> Days { get; set; }
    }
}
