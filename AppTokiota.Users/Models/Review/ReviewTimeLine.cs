using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Models
{
    public class ReviewTimeLine
    {
        public Day Day { get; set; }
        public bool IsLast { get; set; } = false;
        public ActivityDay Activity { get; set; }
    }
}
