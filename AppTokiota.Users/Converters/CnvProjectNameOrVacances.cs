using AppTokiota.Users.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class CnvProjectNameOrVacances : IValueConverter
    {
        private const string holiday = "\uf072";
        private const string weekend = "\uf118";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) 
            {
                var day = (Day)value;
                if (day.Holiday.IsHolyday) return holiday + "  Holydays!!";
                if (day.IsWeekend) return weekend + "  Weekend!!";
            }
            if (parameter != null)
            {
                var aux = parameter as Label;
                if (aux != null)
                {
                    var auxValue = aux.Text;
                    return auxValue; 
                }
               
            }
            return value;           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; 
        }

    }
}
