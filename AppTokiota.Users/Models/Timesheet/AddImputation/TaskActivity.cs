using System;
namespace AppTokiota.Users.Models
{
    public class TaskActivity
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public float Scheduled { get; set; }
        public float Consumed { get; set; }
        public float Deviation { get; set; }
        public int AssignementId { get; set; }
        public bool IsClosed { get; set; }
        public bool IsChargeable { get; set; }

		public static TaskActivity Map(TaskT taskT) {
			return new TaskActivity()
			{
				Id = taskT.Id,
				DisplayName = taskT.DisplayName,
				Scheduled = taskT.Scheduled,
				Consumed = taskT.Consumed,
				Deviation = taskT.Deviation,
				AssignementId = taskT.AssignementId,
				IsClosed = taskT.IsClosed,
				IsChargeable = taskT.IsChargeable
			};
		}
    }

}
