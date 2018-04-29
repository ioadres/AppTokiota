using System;
using System.Globalization;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class FormatTimeEntry: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
