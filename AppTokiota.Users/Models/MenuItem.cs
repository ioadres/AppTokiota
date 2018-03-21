using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Models
{
    public class MenuItem : ObjectCommandArg
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }

        public MenuItem()
        {
        }
    }
}
