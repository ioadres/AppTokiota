using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace AppTokiota.Users.Components.ManageImputedDay
{
    public partial class InfoActivityPopUpPage : ContentPage
    {
        public InfoActivityPopUpPage()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "InfoActivityPopUpPage");

                Crashes.TrackError(e, dic);
            }
        }
    }
}
