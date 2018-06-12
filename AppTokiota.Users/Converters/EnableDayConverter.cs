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
                var result = int.Parse(value.ToString());
                return result > 0;
            }
            return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
