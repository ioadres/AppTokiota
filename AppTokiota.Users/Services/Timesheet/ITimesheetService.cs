﻿using AppTokiota.Users.Controls;
using AppTokiota.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Services
{
    public interface ITimesheetService
    {
        Task<Timesheet> GetTimesheetBeetweenDates(DateTime from, DateTime To);
        TimesheetForDay GetTimesheetByDate(Timesheet currentTimesheet, DateTime dateTime);
        TimesheetForMultipleDay GetTimesheetByDates(Timesheet currentTimesheet, List<DateTime> dateTimes);
		Task<Activity> PostActivity(TimesheetAddActivity timesheetAddActivity, DateTime dateTime);
		Task<List<Activity>> BatchActivity(List<TimesheetAddActivityBatch> timesheetAddActivityBatch);
		Task<TimesheetDeleteActivity> DeleteActivityTimesheet(DateTime from, int idActivity);
	}
}
