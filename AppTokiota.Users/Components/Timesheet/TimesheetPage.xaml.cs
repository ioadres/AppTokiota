using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Components.Core;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Users.Components.Timesheet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimesheetPage : ContentPage
	{
		public TimesheetPage ()
		{
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "TimesheetPage");

                Crashes.TrackError(e, dic);
            }
		}
  
	}
}