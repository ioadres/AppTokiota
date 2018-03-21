using AppTokiota.Controls;
using AppTokiota.Models;
using AppTokiota.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTokiota.Helpers
{
    public static class CalendarColors
    {
        private static Color Red = (Color)(Application.Current.Resources["RedColor"]);
        private static Color RedDarkColor = (Color)(Application.Current.Resources["RedDarkColor"]);
        private static Color Yellow = (Color)(Application.Current.Resources["YellowColor"]);
        private static Color Blue = (Color)(Application.Current.Resources["BlueColor"]);
        private static Color Purple = (Color)(Application.Current.Resources["PurpleColor"]);
        private static Color Green = (Color)(Application.Current.Resources["GreenColor"]);
        private static Color Orange = (Color)(Application.Current.Resources["OrangeColor"]);
        private static Color Gray = (Color)(Application.Current.Resources["DarkGrayColor"]);
        private static Color Dark = (Color)(Application.Current.Resources["DarkColor"]);
        private static Color White = (Color)(Application.Current.Resources["WhiteColor"]);

        public static DayStyle GetWeekend()
        {
            return new DayStyle()
            {
                BackgroundColor = White,
                BorderColor = Gray,
                TextColor = Gray
            };
        }

        public static DayStyle GetHoliday()
        {
            return new DayStyle()
            {
                BackgroundColor = Orange,
                BorderColor = Orange,
                TextColor = White,
                BackgroundPattern = new Pattern { WidthPercent = 1f, HightPercent = 0.25f, Color = Orange }
            };
        }

        public static DayStyle GetInputed()
        {
            return new DayStyle()
            {
                BackgroundColor = Green,
                BorderColor = Green,
                TextColor = White,
                BackgroundPattern = new Pattern { WidthPercent = 1f, HightPercent = 0.25f, Color = Green }
            };
        }
        public static DayStyle SetCloseDay()
        {
            return new DayStyle()
            {
                BackgroundColor = Gray,
                BorderColor = Gray,
                TextColor = Dark,
                BackgroundPattern = new Pattern { WidthPercent = 1f, HightPercent = 0.25f, Color = Gray }
            };
        }

        public static SpecialDate GetSpecialDay(SpecialDate specialDate, DateTime date, List<DayStyle> dayStyle)
        {
            if (dayStyle.Any())
            {

                if (dayStyle.Count() > 1)
                {
                    specialDate.BackgroundPattern = new BackgroundPattern(1)
                    {
                        Pattern = dayStyle.Select(x => x.BackgroundPattern).ToList()
                    };
                }
                else
                {
                    var dayS = dayStyle.FirstOrDefault();
                    specialDate.BackgroundColor = dayS.BackgroundColor;
                    specialDate.TextColor = dayS.TextColor;
                    specialDate.FontFamily = dayS.FontFamily;
                    specialDate.FontAttributes = dayS.FontAttributes;
                    specialDate.BorderWidth = dayS.BorderWidth;
                    specialDate.BackgroundImage = dayS.BackgroundImage;
                    specialDate.FontSize = dayS.FontSize;
                }
            }

            return specialDate;
        }
    }
}
