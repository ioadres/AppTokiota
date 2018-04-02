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
    }
}
