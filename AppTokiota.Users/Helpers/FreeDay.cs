using System;
using System.Collections.Generic;
using System.Text;
using AppTokiota.Users.Models;

namespace AppTokiota.Users.Helpers
{
    public class FreeDay
    {
        public static bool IsFreeOrWeekendDay(Day day)
        {
            if (day.IsWeekend || day.Holiday.IsHolyday) return true;
            else return false;
        }

    }
}
