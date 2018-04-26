using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Models
{
    public class Imputed
    {
        public static string Tag = "Imputed";
        public TimesheetForDay CurrentTimesheet { get; set; }
        public float Consumed { get; set; }
        public float Desviation { get; set; }
    }
}
