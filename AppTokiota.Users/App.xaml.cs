using Xamarin.Forms.Xaml;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using AppTokiota.Users.Services;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Splash;
using Akavache;
using System;
using Plugin.Connectivity;
using AppTokiota.Users.Components.Login;
using AppTokiota.Users.Components.Connection;
using AppTokiota.Users.Components;
using AppTokiota.Users.Components.Dashboard;

namespace AppTokiota.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class App : PrismApplication
    {
        private IAuthenticationService _authenticationService;

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            _authenticationService = Container.Resolve<IAuthenticationService>();

            InitializeComponent();

            BlobCache.ApplicationName = AppSettings.IdAppCache;
            BlobCache.LocalMachine.InsertObject(AppSettings.IdAppCache, true, DateTimeOffset.Now.AddSeconds(10));
            await NavigationService.NavigateAsync(SplashModule.Tag);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //# Container Services
            ServicesLoader.Load(containerRegistry);

            //# Container Components
            ModuleLoader.Load(containerRegistry);

        }

        private async void OnConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {

            try
            {
                if (e.IsConnected == true)
                {
                    var result = await _authenticationService.UserIsAuthenticatedAndValidAsync();
                    if (!result)
                    {
                        await NavigationService.NavigateAsync(LoginModule.Tag);
                    }
                    else
                    {
                        await NavigationService.NavigateAsync(DashBoardModule.Tag);
                    }
                }
                else
                {
                    await _authenticationService.Logout();
                    await NavigationService.NavigateAsync(ConnectionModule.Tag);
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected override void OnStart()
        {
            CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;
        }

        protected override void OnSleep()
        {
            CrossConnectivity.Current.ConnectivityChanged -= OnConnectivityChanged;
        }

        protected override void OnResume()
        {
            CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;
        }
    }
}