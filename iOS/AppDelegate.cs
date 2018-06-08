using AppTokiota.iOS.Helpers;
using AppTokiota.iOS.Renderers;
using AppTokiota.Users;
using AppTokiota.Users.Controls;
using AppTokiota.Users.OS;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using Prism;
using Prism.Ioc;
using Refractored.XamForms.PullToRefresh.iOS;
using UIKit;
using UserNotifications;

namespace AppTokiota.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            RememberNotification.Init();
            global::Xamarin.Forms.Forms.Init();
            AnimationViewRenderer.Init();
            PullToRefreshLayoutRenderer.Init();

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRememberNotificationBase,RememberNotification>();
        }
    }
}
