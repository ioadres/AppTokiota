using System;
using AppTokiota.Attributes;
using AppTokiota.Components.Core;
using Prism.Ioc;
using AppTokiota.Services.Authentication;
using System.Threading.Tasks;
using AppTokiota.Models;
using AppTokiota.Components.Core.Module;

namespace AppTokiota.Components.Login
{
    public class LoginModule: BaseModule, ILoginModule
    {
        private readonly IAuthenticationService _authenticationService;
        public IAuthenticationService AuthenticationService => _authenticationService;

        public LoginModule(IAuthenticationService authenticationService) {
            Tag = nameof(LoginPage);
            _authenticationService = authenticationService;
        }
        
        public static void Register(IContainerRegistry containerRegistry) {

            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        }
    }
}
