using System;
using System.Collections.Generic;
using System.Linq;
using AppTokiota.Users.Models;

namespace AppTokiota.Users.Services
{
    public class TimesheetServiceBase
    {
        public TimesheetForDay GetTimesheetByDate(Timesheet currentTimesheet, DateTime dateTime)
        {
            var timesheetForDay = new TimesheetForDay();
            timesheetForDay.Day = currentTimesheet.Days.FirstOrDefault(x => x.Date.Equals(dateTime));
            timesheetForDay.Projects = currentTimesheet.Projects.Values.ToList();
            timesheetForDay.Activities = TimesheetForDay.Map(currentTimesheet, dateTime).OrderBy(x => x.Id).ToList();
            return timesheetForDay;
        }

        public TimesheetForMultipleDay GetTimesheetByDates(Timesheet currentTimesheet, List<DateTime> dateTimes)
        {
            var timesheetForDay = new TimesheetForMultipleDay();
            timesheetForDay.Days = currentTimesheet.Days.Where(x => x.IsClosed == false && dateTimes.Any(y => y.Date.Equals(x.Date))).ToList();
            timesheetForDay.Projects = currentTimesheet.Projects.Values.ToList();
            timesheetForDay.Activities = null;
            return timesheetForDay;
        }
    }
}
