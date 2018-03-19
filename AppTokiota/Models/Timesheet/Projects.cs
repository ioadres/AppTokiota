using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Models
{
    [DataContract]
    public class Project
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "isClosed")]
        public bool IsClosed { get; set; }

        [DataMember(Name = "htmlColor")]
        public object HtmlColor { get; set; }

        [DataMember(Name = "isHoliday")]
        public bool IsHoliday { get; set; }

        [DataMember(Name = "detailedDescription")]
        public bool DetailedDescription { get; set; }

        [DataMember(Name = "tasks")]
        public List<TaskT> Tasks { get; set; }
    }
}
