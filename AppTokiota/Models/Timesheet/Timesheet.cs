using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Models
{
    [DataContract]
    public class Timesheet
    {
        [DataMember(Name = "activities")]
        public List<Activity> Activities { get; set; }
        [DataMember(Name = "projects")]
        public List<Project> Projects { get; set; }
        [DataMember(Name = "days")]
        public List<Day> Days { get; set; }
    }
}
