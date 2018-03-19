using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Models
{
    [DataContract]
    public class Day
    {
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "isClosed")]
        public bool IsClosed { get; set; }

        [DataMember(Name = "holiday")]
        public bool Holiday { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "isWeekend")]
        public bool IsWeekend { get; set; }
    }
}
