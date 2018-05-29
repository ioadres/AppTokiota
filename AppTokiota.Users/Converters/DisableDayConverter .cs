using AppTokiota.Users.Models;
using System;
using System.Globalization;
using Xamarin.Forms;
using AppTokiota.Users.Helpers;

namespace AppTokiota.Users.Converters
{
    public class DisableDayConverter :  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var day = (Day)value;
            var result =  FreeDay.IsFreeOrWeekendDay(day);
            return result; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
