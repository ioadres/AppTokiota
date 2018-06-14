using System;
using System.Globalization;
using AppTokiota.Users.Helpers;
using AppTokiota.Users.Models;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class GetIconDayConverter: IValueConverter
    {
        private const string holiday = "\uf072";
        private const string task = "\uf040";
        private const string weekend = "\uf0fc";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var day = (Day)value;

            if(FreeDay.IsFree(day)){
                return holiday;
            }
            if (FreeDay.IsWeekend(day))
            {
                return weekend;
            }

            return task;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }
    }
}
