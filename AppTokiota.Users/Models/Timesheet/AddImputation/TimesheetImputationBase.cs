using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTokiota.Users.Models
{
    public class TimesheetImutationBase
    {
        public List<ActivityDay> Activities { get; set; }
        public List<Project> Projects { get; set; }

        public static List<ActivityDay> Map(Timesheet currentTimesheet, DateTime dateTime)
        {
            var activities = new List<ActivityDay>();

            try
            {

                foreach (var item in currentTimesheet.Activities.Where(x => x.Value.Date.Equals(dateTime)))
                {
                    var activity = item.Value;
                    var project = currentTimesheet.Projects.FirstOrDefault(y => y.Key.Equals(activity.ProjectId));

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
            catch (Exception e)
            {

            }

            return activities;
        }

        public static ProjectActivity Map(Project Project)
        {
            try
            {
                return new ProjectActivity()
                {
                    Id = Project.Id,
                    DetailedDescription = Project.DetailedDescription,
                    DisplayName = Project.DisplayName,
                    HtmlColor = Project.HtmlColor,
                    IsClosed = Project.IsClosed,
                    IsHoliday = Project.IsHoliday
                };
            }
            catch (Exception e)
            {
                return new ProjectActivity();
            }
        }

        public static TaskActivity Map(Project Project, int taskId)
        {
            try
            {
                var task = Project.Tasks.FirstOrDefault(x => x.Key.Equals(taskId));

                return new TaskActivity()
                {
                    Id = task.Value.Id,
                    DisplayName = task.Value.DisplayName,
                    AssignementId = task.Value.AssignementId,
                    Consumed = task.Value.Consumed,
                    Deviation = task.Value.Deviation,
                    IsChargeable = task.Value.IsChargeable,
                    IsClosed = task.Value.IsClosed,
                    Scheduled = task.Value.Scheduled
                };

            }
            catch (Exception e)
            {
                return new TaskActivity();
            }

        }

    }
}
