using AppTokiota.Users.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class CellStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var day = (Day)value; 
            if (day.IsWeekend || day.Holiday.IsHolyday)return Color.White;
            else return Color.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
