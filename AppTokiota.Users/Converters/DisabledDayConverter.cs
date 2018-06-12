using System;
using System.Globalization;
using AppTokiota.Users.Helpers;
using AppTokiota.Users.Models;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class DisabledDayConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var day = (Day)value;
            var result = FreeDay.IsFreeOrWeekendDay(day);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
