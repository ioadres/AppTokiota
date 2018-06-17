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
            if (day?.IsWeekend == true || day?.Holiday?.IsHolyday == true) return true;
            else return false;
        }

        public static bool IsFree(Day day) {
            return day?.Holiday?.IsHolyday == true;
        }

        public static bool IsWeekend(Day day)
        {
            return day?.IsWeekend == true;
        }

    }
}
