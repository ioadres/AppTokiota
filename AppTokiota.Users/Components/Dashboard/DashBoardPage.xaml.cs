using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Users.Components.DashBoard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashBoardPage : ContentPage
    {
        public DashBoardPage()
        {
            try
            {
                InitializeComponent();

            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "DashBoardPage");

                Crashes.TrackError(e, dic);
            }
        }
    }
}