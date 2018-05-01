using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Models
{
    public class Imputed
    {
        public static string Tag = "Imputed";
        public TimesheetForDay CurrentTimesheet { get; set; }
        public TimesheetForMultipleDay CurrentTimesheetMultipleDay { get; set; }

        public Time Consumed { get; set; }
        public Time Deviation { get; set; }

        public Imputed() {
            Consumed = new Time();
            Deviation = new Time();
        }
    }

    public class Time {
        public float Minute
        {
            get;
            set;
        }
        public float Hour
        {
            get;
            set;
        }

		public override string ToString()
		{
            return Time.DateTimeFormatInfo(Hour, Minute);
		}

        public float GetMimutes()
        {
            return Hour * 60 + Minute;
        }

        public static string DateTimeFormatInfo(float Hour, float Minute) {
            return Hour.ToString() + "h " + Minute.ToString() + "m";
        }
	}
}
