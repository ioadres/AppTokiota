using AppTokiota.Components.Core.Module;
using AppTokiota.Services;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.Splash
{
    public class SplashModule : BaseModule, ISplashModule
    {
        private readonly IAuthenticationService _authenticationService;

        public static string Tag => nameof(SplashPage);

        public IAuthenticationService AuthenticationService => _authenticationService;

        public SplashModule(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SplashPage, SplashPageViewModel>();
        }
    }
}
