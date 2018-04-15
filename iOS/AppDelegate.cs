﻿using AppTokiota.iOS.Renderers;
using AppTokiota.Users;
using AppTokiota.Users.Controls;
using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;

namespace AppTokiota.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
        global::Xamarin.Forms.Forms.Init();
           
            LoadApplication(new App(new iOSInitializer()));

 

           /*  LoadApplication(UXDivers.Gorilla.iOS.Player.CreateApplication(
            new UXDivers.Gorilla.Config("Good Gorilla")
                .RegisterAssembliesFromTypes<
                                   Prism.IActiveAware,
                                   Prism.PrismApplicationBase,
                                   Prism.Unity.PrismApplication>()
                .RegisterAssemblyFromType<AwesomeLabelControl>()
                .RegisterAssemblyFromType<MenuItemCell>()
                .RegisterAssemblyFromType<ExtendedEntry>()
                .RegisterAssemblyFromType<ExtendedEntryRenderer>()
                .RegisterAssembly(typeof(AppTokiota.Users.Components.Core.ViewModelBase).Assembly)     
            // Register UXDivers Effects assembly
            ));*/




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
