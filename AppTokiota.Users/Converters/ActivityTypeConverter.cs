using System;
using System.Globalization;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class ActivityTypeConverter: IValueConverter
    {
        private const string holiday = "\uf072";
        private const string task = "\uf0ae";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ? holiday : task;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }
    }
}
