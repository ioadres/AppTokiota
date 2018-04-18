using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace AppTokiota.Users.Components.Activity
{
    public partial class AddActivityPage : PopupPage
    {
        public AddActivityPage()
        {
            try {

            InitializeComponent();
            } catch(Exception e) {
                var t = e;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

    }
}
