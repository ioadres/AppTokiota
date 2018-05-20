using System;
using System.Collections.Generic;
using SkiaSharp;
using System.Linq;

namespace AppTokiota.Users.Helpers
{
    public static class GenerateColors
    {
		public static readonly List<string> Colors = new List<string>(){
			"#484F64", "#FFD45F", "#26C3AC", "#FA6978"
		};

		public static SKColor GetColor(int index) {

			var color = "";
			if (index < Colors.Count())
			{
				color = Colors.ElementAt(index);
			} else{
				color = GenerateColorHex();
			}

			return SKColor.Parse(color);
		}

		public static string GenerateColorHex() {
			var random = new Random();
           return String.Format("#{0:X6}", random.Next(0x1000000));
		}


    }
}
