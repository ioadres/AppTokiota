using System;
using System.Linq;
namespace AppTokiota.Users.Models
{
    public class ActivityDay
    {
        public static string Tag = nameof(ActivityDay);

		public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Deviation { get; set; }
        public double Imputed { get; set; }
        public ProjectActivity Project { get; set; }
        public TaskActivity Task { get; set; }
        public string UserId { get; set; }
        public int AssignementId { get; set; }

		public static ActivityDay Map(Activity activity, TimesheetForDay timesheetForDay) {
			var project = timesheetForDay.Projects.FirstOrDefault(x => x.Id.Equals(activity.ProjectId));
			var task = project?.Tasks.FirstOrDefault(x => x.Value.Id.Equals(activity.TaskId)).Value;
			return new ActivityDay()
			{
				AssignementId = activity.AssignementId,
				Description = activity.Description,
				Deviation = activity.Deviation,
				Imputed = activity.Imputed,
				Project = ProjectActivity.Map(project),
				Task = TaskActivity.Map(task)
			};
		}
    }
}
