using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Models
{
    [DataContract]
    public class TaskT
    {
        [DataMember(Name ="id")]
        public int Id { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "scheduled")]
        public float Scheduled { get; set; }

        [DataMember(Name = "consumed")]
        public float Consumed { get; set; }

        [DataMember(Name = "deviation")]
        public float Deviation { get; set; }

        [DataMember(Name = "projectId")]
        public int ProjectId { get; set; }

        [DataMember(Name = "assignementId")]
        public int AssignementId { get; set; }

        [DataMember(Name = "isClosed")]
        public bool IsClosed { get; set; }

        [DataMember(Name = "isChargeable")]
        public bool IsChargeable { get; set; }
    }
}
