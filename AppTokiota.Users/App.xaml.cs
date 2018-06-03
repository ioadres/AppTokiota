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
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
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

            // Handle when your app starts
            AppCenter.Start("ios=0a951cf0-dd56-4345-9c3f-6d4b8b31704d;" + "android=40b5c075-e36b-4f98-8829-3c952b6f89bb", typeof(Analytics), typeof(Crashes));
        

            if (AppSettings.IsEnableNotification)
            {
                var dateNow = DateTime.Now;
                var limitDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 16, 0, 0);
                CrossLocalNotifications.Current.Show("Tokiota :: Timesheet", "Remember input your timesheet", 1, dateNow.AddSeconds(10));
            }
        }

        protected override void OnSleep()
        {
            CrossLocalNotifications.Current.Cancel(1);
        }

        protected override void OnResume()
        {
			if(_network.IsAvailable()) {
				AuthenticationRun();
			}
            if (AppSettings.IsEnableNotification)
            {
                var dateNow = DateTime.Now;
                var limitDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 16, 0, 0);
                CrossLocalNotifications.Current.Show("Tokiota :: Timesheet", "Remember input your timesheet", 1, dateNow.AddSeconds(10));
            }
        }      
        
        async void AuthenticationRun() 
        {
            try
            {    
				var result = await _authenticationService.UserIsAuthenticatedAndValidAsync();
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