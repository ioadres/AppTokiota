using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;

namespace AppTokiota.Users.Models
{
    public class TimesheetForDay :  TimesheetImutationBase
    {
        public static string Tag = nameof(TimesheetForDay);
        public Day Day { get; set; }
    }
}
