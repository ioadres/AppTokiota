using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Users.Components.Login
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "LoginPage");

                Crashes.TrackError(e, dic);
            }
		}
	}
}