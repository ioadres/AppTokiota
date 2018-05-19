using System;
using System.ComponentModel;
using AppTokiota.iOS.Renderers;
using AppTokiota.Users.Components.BaseNavigation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(BaseNavigationPage), typeof(BaseNavigationPageRenderer))]
namespace AppTokiota.iOS.Renderers
{

    public class BaseNavigationPageRenderer : NavigationRenderer
    {
		public BaseNavigationPageRenderer(): base()
        {
			NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationBar.ShadowImage = new UIImage();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Element.PropertyChanged += HandlePropertyChanged;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var navPage = (NavigationPage)Element;
                navPage.PropertyChanged -= HandlePropertyChanged;
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var textAttributes = new UITextAttributes();
                textAttributes.Font = UIFont.FromName("Poppins-Regular", 15);
                UINavigationBar.Appearance.SetTitleTextAttributes(textAttributes);
            }
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
