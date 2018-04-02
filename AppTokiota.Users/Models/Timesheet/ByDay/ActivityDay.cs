using System;
namespace AppTokiota.Users.Models
{
    public class ActivityDay
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Deviation { get; set; }
        public double Imputed { get; set; }
        public ProjectActivity Project { get; set; }
        public TaskActivity Task { get; set; }
        public string UserId { get; set; }
        public int AssignementId { get; set; }
    }
}
