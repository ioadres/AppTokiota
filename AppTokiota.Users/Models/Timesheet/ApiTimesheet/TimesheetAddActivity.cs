using System;
namespace AppTokiota.Users.Models
{
    public class TimesheetAddActivity
    {
		public int AssignementId
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public int Deviation
		{
			get;
			set;
		}

		public int Imputed
        {
            get;
            set;
        }

		public int ProjectId
        {
            get;
            set;
        }

		public int TaskId
        {
            get;
            set;
        }
	}
}
