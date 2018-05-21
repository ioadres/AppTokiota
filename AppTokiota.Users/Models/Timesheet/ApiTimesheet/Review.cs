using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppTokiota.Users.Models
{
    [DataContract]
    public class Review
    {
        public static string Tag = nameof(Review);

        [DataMember(Name = "activities")]
        public Dictionary<int, Activity> Activities { get; set; }
        [DataMember(Name = "projects")]
        public Dictionary<int, Project> Projects { get; set; }
        [DataMember(Name = "days")]
        public List<Day> Days { get; set; }
        [DataMember(Name = "isClosed")]
        public bool IsClosed { get; set; }
        [DataMember(Name = "isValidated")]
        public bool IsValidated { get; set; }
        [DataMember(Name = "monthWorkableHours")]
        public double MonthWorkableHours{ get; set; }
        [DataMember(Name = "pendingProjects")]
        public Dictionary<int, Project> PendingProjects { get; set; }
    }
}
