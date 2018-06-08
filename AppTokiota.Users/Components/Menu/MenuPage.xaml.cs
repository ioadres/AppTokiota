using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Users.Components.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "MenuPage");

                Crashes.TrackError(e, dic);
            }
        }
    }
}