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
using AppTokiota.Controls;
using AppTokiota.Droid.Renderers;
using Prism.Unity;

namespace AppTokiota.Droid
{
    [Activity(Label = "AppTokiota.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(
                this,
                new UXDivers.Gorilla.Config("Good Gorilla")
                             // Register Grial Shared assembly
                             .RegisterAssembliesFromTypes<Prism.IPlatformInitializer, Prism.PrismApplicationBase, Prism.Unity.PrismApplication>()
                            .RegisterAssemblyFromType<ExtendedEntry>()
                            .RegisterAssemblyFromType<ExtendedEntryRenderer>()
                            .RegisterAssemblyFromType<RoundButtonRenderer>()
                            .RegisterAssembly(typeof(AppTokiota.Components.Core.ViewModelBase).Assembly)
                ));        
                
          //LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
