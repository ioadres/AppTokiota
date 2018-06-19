using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using AppTokiota.Users.Services;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Splash;
using Akavache;
using System;
using AppTokiota.Users.Components.Login;
using System.Threading.Tasks;
using System.Diagnostics;
using Plugin.LocalNotifications;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Xamarin.Forms;
using Plugin.Connectivity;
using AppTokiota.Users.OS;

namespace AppTokiota.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class App : PrismApplication
    {
		private INetworkConnectionService _network;
		private IAuthenticationService _authenticationService;

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            BlobCache.ApplicationName = AppSettings.IdAppCache;

            InitializeComponent();

            await NavigationService.NavigateAsync(PageRoutes.GetKey<SplashPage>());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {           
            //# Container Services
            ServicesLoader.Load(containerRegistry);

            //# Container Components
            ModuleLoader.Load(containerRegistry);

        }

        protected override void OnStart()
        {
			_network = Container.Resolve<INetworkConnectionService>();
			_authenticationService = Container.Resolve<IAuthenticationService>();

            AppCenter.Start(AppSettings.AppCenter, typeof(Analytics), typeof(Crashes));

            MessagingCenter.Subscribe<ISubscribeMessagingCenter>(this, nameof(UnauthorizedAccessException), async (app) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    await AuthenticationRun();
                }
            });

            RememberNotificationBuild();

            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            RememberNotificationBuild();
            base.OnResume();
        }  

        void RememberNotificationBuild() {
            var rememberNotification = Container.Resolve<IRememberNotificationBase>();
            rememberNotification.RemoveBadgeRememberNotification();
            if (AppSettings.IsEnableNotification)
            {
                rememberNotification.EmitCreateRememberNotification();
            }
            else
            {
                rememberNotification.EmitRemoveRememberNotification();
            }
        }
        
        async Task AuthenticationRun(bool forceRefresh = false) 
        {
            try
            {    
                var result = await _authenticationService.UserIsAuthenticatedAndValidAsync(forceRefresh);
                if (!result)
                {
                    await NavigationService.NavigateAsync(PageRoutes.GetKey<LoginPage>());
                } 
            }
            catch (Exception ex)
            {
				Debug.WriteLine(ex);
            }
        }
    }
}