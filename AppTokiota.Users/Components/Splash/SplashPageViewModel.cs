using AppTokiota.Users.Components.BaseNavigation;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components;
using AppTokiota.Users.Components.Login;
using AppTokiota.Users.Components.Master;
using Plugin.Connectivity;
using System;
using Xamarin.Forms;
using AppTokiota.Users.Components.DashBoard;
using AppTokiota.Users.Components.Connection;

namespace AppTokiota.Users.Components.Splash
{
    public class SplashPageViewModel : ViewModelBase
    {
        private ISplashModule _splashModule;

        public SplashPageViewModel(IViewModelBaseModule baseModule, ISplashModule splashModule) : base(baseModule)
        {
            Title = "Splash";
            _splashModule = splashModule;

            IsBusy = true;

            Device.StartTimer(new TimeSpan(0, 0, 3), () =>
            {
                AuthenticationRun();
                IsBusy = false;
                return false;
            });
        }
        

        private void AuthenticationRun()
        {
			if (AppSettings.AuthenticatedUserResponse != null)
            {
				NavigateCommand.Execute(MasterModule.GetMasterNavigationPage(PageRoutes.GetKey<DashBoardPage>()));
            } else {
			    NavigateCommand.Execute(PageRoutes.GetKey<LoginPage>());
            }     
        }
    }
}
