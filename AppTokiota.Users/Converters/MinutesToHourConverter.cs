using System;
using System.Globalization;
using AppTokiota.Users.Helpers;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class MinutesToHourConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			var result = TimeFormat.Format(int.Parse(string.Format("{0}", value)));
			return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }
    }
}
