using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class DateToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = DateTime.Parse(value.ToString());
            var dayOfWeek = date.DayOfWeek;
            var dayOfWeekNumber = date.Day;

            var dayOfWeekString = dayOfWeekNumber + "-" + dayOfWeek; 
            return dayOfWeekString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
