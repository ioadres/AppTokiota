using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Models
{
    [DataContract]
    public class Day
    {
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "isClosed")]
        public bool IsClosed { get; set; }

        [DataMember(Name = "holiday")]
        public Holiday Holiday { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "isWeekend")]
        public bool IsWeekend { get; set; }
    }

    [DataContract]
    public class Holiday
    {
        [DataMember(Name = "isHolyday")]
        public bool IsHolyday { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
