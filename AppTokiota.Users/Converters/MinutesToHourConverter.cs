using System;
using System.Globalization;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class MinutesToHourConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var minutesObject = int.Parse(string.Format("{0}", value));
            var hours = (int)(minutesObject / 60);
            var minutes = (int)(minutesObject % 60);
            var hourString = string.Empty;
            var minuteString = string.Empty;
            if (hours < 10) hourString = "0";
            if (minutes < 10) minuteString = "0";
            return (string)$"{hourString}{hours}h:{minuteString}{minutes}m";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }
    }
}
