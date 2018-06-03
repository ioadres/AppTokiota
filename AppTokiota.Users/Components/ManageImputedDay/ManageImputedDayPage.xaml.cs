using System;
using System.Collections.Generic;
using AppTokiota.Users.Models;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace AppTokiota.Users.Components.ManageImputedDay
{
    public partial class ManageImputedDayPage : ContentPage
    {
		public ManageImputedDayPage()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "ManageImputedDayPage");

                Crashes.TrackError(e, dic);
            }
        }
    }
}
