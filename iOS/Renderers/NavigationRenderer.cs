using System;
using System.ComponentModel;
using AppTokiota.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRenderer))]

namespace AppTokiota.iOS.Renderers
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Element.PropertyChanged += HandlePropertyChanged;
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName || e.PropertyName == Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.StatusBarTextColorModeProperty.PropertyName)
                UpdateStatusBarStyle();
        }

        private async void UpdateStatusBarStyle()
        {
            // we want to change defaults XF status bar style calcs
            await System.Threading.Tasks.Task.Delay(1);
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }
    }
}
