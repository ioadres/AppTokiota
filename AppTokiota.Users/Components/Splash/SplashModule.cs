using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Services;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Components.Splash
{
    public class SplashModule : ISplashModule
    {
        private readonly IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public SplashModule(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
    }
}
