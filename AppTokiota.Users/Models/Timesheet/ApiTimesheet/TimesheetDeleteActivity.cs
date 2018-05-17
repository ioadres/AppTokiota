using System;
using System.Collections.Generic;

namespace AppTokiota.Users.Models
{
	public class TimesheetDeleteActivity
	{
		public string description { get; set; }
        public double hours { get; set; }
        public double deviation { get; set; }
        public double chargeableHours { get; set; }
        public int day { get; set; }
        public object managerComment { get; set; }
        public string lastModifiedDate { get; set; }
        public object deviationReason { get; set; }
        public object assignement { get; set; }
        public object monthSheet { get; set; }
        public int id { get; set; }
        public List<object> domainEvents { get; set; }
        public int entityState { get; set; }
	}
}