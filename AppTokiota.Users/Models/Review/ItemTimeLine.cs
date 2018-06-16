using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTokiota.Users.Models
{
    public class ItemTimeLine
    {
        public static string Tag = nameof(ItemTimeLine);
        public Day Day { get; set; }
        public bool IsLast { get; set; } = false;
        public List<ActivityDay> Activities { get; set; }

    }
}
