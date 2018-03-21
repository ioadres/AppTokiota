using AppTokiota.Components.BaseNavigation;
using AppTokiota.Components.Core;
using AppTokiota.Components.Core.Module;
using AppTokiota.Components.Dashboard;
using AppTokiota.Components.Login;
using AppTokiota.Components.Master;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppTokiota.Components.Splash
{
    public class SplashPageViewModel : ViewModelBase
    {
        private ISplashModule _splashModule;

        public SplashPageViewModel(IViewModelBaseModule baseModule, ISplashModule splashModule) : base(baseModule)
        {
            Title = "Splash";
            _splashModule = splashModule;
            
            Device.StartTimer(new TimeSpan(0, 0, 3), () =>
            {
                BaseModule.AuthenticationService.InitializeAsync();
                AuthenticationRun();
                return false;
            });
        }


        private void AuthenticationRun()
        {
            if (BaseModule.AuthenticationService.IsAuthenticated)
            {
                NavigateCommand.Execute(MasterModule.Tag + BaseNavigationModule.Tag + DashBoardModule.Tag);
            }
            else
            {
                NavigateCommand.Execute(LoginModule.Tag);
            }           
        }
    }
}
