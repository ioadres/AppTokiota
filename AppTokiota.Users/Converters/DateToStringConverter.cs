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
            // Get the en-US culture.
            CultureInfo ci = new CultureInfo(AppSettings.CultureInfoApp);
            // Get the DateTimeFormatInfo for the en-US culture.
            DateTimeFormatInfo dtfi = ci.DateTimeFormat;

            var date = DateTime.Parse(value.ToString());
            var dayOfWeekAux = (int)date.DayOfWeek;
            var dayOfWeek = dtfi.AbbreviatedDayNames.GetValue(dayOfWeekAux);
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
