using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Models
{
    [DataContract]
    public class Activity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "deviation")]
        public double Deviation { get; set; }

        [DataMember(Name = "imputed")]
        public double Imputed { get; set; }

        [DataMember(Name = "projectId")]
        public int ProjectId { get; set; }

        [DataMember(Name = "taskId")]
        public int TaskId { get; set; }

        [DataMember(Name = "userId")]
        public string UserId { get; set; }

        [DataMember(Name = "assignementId")]
        public int AssignementId { get; set; }
    }
}
