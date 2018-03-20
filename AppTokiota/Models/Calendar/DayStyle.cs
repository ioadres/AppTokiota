using AppTokiota.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTokiota.Models.Calendar
{
    public class DayStyle
    {
        public Color? TextColor { get; set; }
        public Color? BackgroundColor { get; set; }
        public Color? BorderColor { get; set; }
        public FontAttributes? FontAttributes { get; set; }
        public string FontFamily { get; set; }
        public int? BorderWidth { get; set; }
        public double? FontSize { get; set; }
        public FileImageSource BackgroundImage { get; set; }
        public Pattern BackgroundPattern { get; set; }
    }
}
