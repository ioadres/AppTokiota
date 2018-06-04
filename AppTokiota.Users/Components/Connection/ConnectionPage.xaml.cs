using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Users.Components.Connection
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnectionPage : ContentPage
	{
		public ConnectionPage ()
		{
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "ConnectionPage");

                Crashes.TrackError(e, dic);
            }
		}
	}
}