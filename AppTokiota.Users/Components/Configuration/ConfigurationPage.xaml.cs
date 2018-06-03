using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace AppTokiota.Users.Components.Configuration
{
    public partial class ConfigurationPage : ContentPage
    {

        public ConfigurationPage()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "ConfigurationPage");

                Crashes.TrackError(e, dic);
            }
        }
    }
}
