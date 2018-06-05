using AppTokiota.Users.Models;
using System;
using System.Globalization;
using Xamarin.Forms;
using AppTokiota.Users.Helpers;

namespace AppTokiota.Users.Converters
{
    public class EnableDayConverter :  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var day = (Day)value;
                var result = FreeDay.IsFreeOrWeekendDay(day);
                return !result;
            }
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
