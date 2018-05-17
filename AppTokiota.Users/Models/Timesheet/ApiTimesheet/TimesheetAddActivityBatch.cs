using System;
namespace AppTokiota.Users.Models
{
	public class TimesheetAddActivityBatch : TimesheetAddActivity
    {
        public int Day
        {
        	get;
        	set;
        }
		public int Month
        {
            get;
            set;
        }
		public int Year
        {
            get;
            set;
        }
    }
}
