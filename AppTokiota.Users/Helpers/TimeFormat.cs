using System;
namespace AppTokiota.Users.Helpers
{
    public static class TimeFormat
    {
		public static string Format(double time)
        {
            var minutesObject = int.Parse(string.Format("{0}", time));
            var hours = (int)(minutesObject / 60);
            var minutes = (int)(minutesObject % 60);
            var hourString = string.Empty;
            var minuteString = string.Empty;
            if (hours < 10) hourString = "0";
            if (minutes < 10) minuteString = "0";

            return $"{hourString}{hours}h:{minuteString}{minutes}m";
        }
    }
}
