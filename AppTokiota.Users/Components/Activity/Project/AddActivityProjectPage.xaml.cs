using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Users.Components.Activity
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddActivityProjectPage : ContentPage
	{
		public AddActivityProjectPage ()
		{
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "AddActivityProjectPage");

                Crashes.TrackError(e, dic);
            }
        }
	}
}