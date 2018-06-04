using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTokiota.Users.Components.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            try
            {
				IsPresented = false;
				MasterBehavior = MasterBehavior.Popover;
                Appearing += (sender, e) =>
                {
                    this.IsPresented = false;
					this.MasterBehavior = MasterBehavior.Popover;
                };

                InitializeComponent();
            } 
            catch(Exception e)
            {
                var dic = new Dictionary<string, string>();
                dic.Add("Page", "MasterPage");

                Crashes.TrackError(e, dic);
            }
        }
    }
}