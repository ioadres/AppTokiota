using System;
using System.Globalization;
using AppTokiota.Users.Models;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class FormatTimeMinutesToStringEntry: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hour = int.Parse(value.ToString()) / 60;
            var minute = (int.Parse(value.ToString()) % 60);

            return Time.DateTimeFormatInfo(hour, minute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
