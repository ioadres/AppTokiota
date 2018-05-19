using AppTokiota.iOS.Renderers;
using AppTokiota.Users;
using AppTokiota.Users.Controls;
using Foundation;
using Prism;
using Prism.Ioc;
using Refractored.XamForms.PullToRefresh.iOS;
using UIKit;

namespace AppTokiota.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
           
			PullToRefreshLayoutRenderer.Init();

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
