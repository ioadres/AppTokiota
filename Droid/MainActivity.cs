using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism;
using Prism.Ioc;
using UXDivers.Gorilla.Droid;
using AppTokiota.Users.Controls;
using AppTokiota.Droid.Renderers;
using Prism.Unity;
using Acr.UserDialogs;
using Lottie.Forms.Droid;
using AppTokiota.Users.OS;
using AppTokiota.Droid.Helpers;

namespace AppTokiota.Droid
{
    [Activity(Label = "TimeSheet", Icon = "@drawable/MyIcon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = AppTokiota.Droid.Resource.Layout.Tabbar;
            ToolbarResource = AppTokiota.Droid.Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            AnimationViewRenderer.Init();

            LoadApplication(new AppTokiota.Users.App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRememberNotificationBase, RememberNotification>();
        }
    }
}
