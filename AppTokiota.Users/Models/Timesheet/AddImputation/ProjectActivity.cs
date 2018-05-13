using System;
namespace AppTokiota.Users.Models
{
    public class ProjectActivity
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public bool IsClosed { get; set; }
        public string HtmlColor { get; set; }
        public bool DetailedDescription { get; set; }
        public bool IsHoliday{ get; set; }

		public static ProjectActivity Map(Project project) {
			return new ProjectActivity()
			{
				Id = project.Id,
				DisplayName = project.DisplayName,
				IsClosed = project.IsClosed,
				HtmlColor = project.HtmlColor,
				DetailedDescription = project.DetailedDescription,
				IsHoliday = project.IsHoliday
			};
		}
    }
}
