using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppTokiota.Users.Converters
{
    public class DescriptionToShortDescription : IValueConverter
    {
        private const int MAXSTRING = 100;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var endMessage = "...";
            var message = "No Description!";
            if (value.ToString().Length > 0)
            {
                if (value.ToString().Length < MAXSTRING)
                {
                    endMessage = string.Empty;
                    message = value.ToString();
                }
                else
                {
                    message = value.ToString().Substring(0, MAXSTRING);
                }
            }

            return $"{message} {endMessage}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }
    }
}
