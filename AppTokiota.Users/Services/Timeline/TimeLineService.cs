using System;
using System.Collections.Generic;

using AppTokiota.Users.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AppTokiota.Users.Services
{
    public class TimeLineService : ITimeLineService
    {
        public async Task<IList<TimesheetForDay>> GetListTimesheetForDay(Review review)
        {
            var listTimesheetForDay = new List<TimesheetForDay>();

            foreach (var TsDay in review.Days)
            {
                var timesheetForDay = new TimesheetForDay();
                timesheetForDay.Day = new Day {
                    Date = TsDay.Date, 
                    IsClosed = TsDay.IsClosed,
                    Holiday = new Holiday { IsHolyday = TsDay.Holiday.IsHolyday, Name = TsDay.Holiday.Name },
                    name = TsDay.name,
                    IsWeekend = TsDay.IsWeekend
                };
                listTimesheetForDay.Add(timesheetForDay);
            }

            foreach (var ts in listTimesheetForDay) {
                ts.Activities = MapActivities(ts.Day.Date, review);
            }
            return await Task.FromResult(listTimesheetForDay);

        }

        private List<ActivityDay> MapActivities(DateTime dateTime, Review review)
        {
            var activities = new List<ActivityDay>();
            try
            {
                foreach (var item in review.Activities.Where(x => x.Value.Date.Equals(dateTime)))
                {
                    var activity = item.Value;
                    var project = review.Projects.FirstOrDefault(y => y.Key.Equals(activity.ProjectId));

                    activities.Add(new ActivityDay()
                    {
                        AssignementId = activity.AssignementId,
                        Project = TimesheetForDay.Map(project.Value),
                        Date = activity.Date,
                        Id = activity.Id,
                        Description = activity.Description,
                        Deviation = activity.Deviation,
                        UserId = activity.UserId,
                        Imputed = activity.Imputed,
                        Task = TimesheetForDay.Map(project.Value, activity.TaskId)
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return activities;
        }

      
    }
}
