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
using Prism.Navigation;
using AppTokiota.Users.Components.Master;
using System.Runtime.InteropServices;
using Prism.Mvvm;
using System.Globalization;
using System.Reflection;
using Prism.Modularity;
using System.Threading.Tasks;
using AppTokiota.Users.Components.DashBoard;

namespace AppTokiota.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class App : PrismApplication
    {
        private IAuthenticationService _authenticationService;

        /* 
         * NOTE: 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            _authenticationService = Container.Resolve<IAuthenticationService>();

            BlobCache.ApplicationName = AppSettings.IdAppCache;
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
            CrossConnectivity.Current.ConnectivityChanged += TaskScheduler_OnConnectivityChanged;
        }

        protected override void OnSleep()
        {
            CrossConnectivity.Current.ConnectivityChanged -= TaskScheduler_OnConnectivityChanged;
        }

        protected override void OnResume()
        {
            CrossConnectivity.Current.ConnectivityChanged += TaskScheduler_OnConnectivityChanged;
            AuthenticationRun();           
        }

        async void TaskScheduler_OnConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected == true)
            {
                AuthenticationRun(true);
            }
            else
            {
                await NavigationService.NavigateAsync(PageRoutes.GetKey<ConnectionPage>());
            }
        }

        async void AuthenticationRun(bool isConnectivityChange = false) 
        {
            try
            {    
                var result = await _authenticationService.UserIsAuthenticatedAndValidAsync();
                if (!result)
                {
                    await NavigationService.NavigateAsync(PageRoutes.GetKey<LoginPage>());
                } else {
                    if(_authenticationService.IsAuthenticated && isConnectivityChange) {
                        await NavigationService.NavigateAsync(MasterModule.GetMasterNavigationPage(PageRoutes.GetKey<DashBoardPage>()));
                    } 
                }
            }
            catch (Exception ex)
            {
            }
        }



        void TaskScheduler_UnobservedTaskException(Object sender, UnobservedTaskExceptionEventArgs e)
        {
            if (!e.Observed)
            {
                // prevents the app domain from being torn down
                e.SetObserved();

                // show the crash page
                //ShowCrashPage(e.Exception.Flatten().GetBaseException());
            }
        }

    }
}