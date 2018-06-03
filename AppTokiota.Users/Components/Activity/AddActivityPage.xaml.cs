using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;

namespace AppTokiota.Users.Components.Activity
{
    public partial class AddActivityPage : ContentPage
    {
        public AddActivityPage()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "AddActivityPage");

                Crashes.TrackError(e, dic);
            }
        }

     }
}
