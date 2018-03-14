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
        public static string Tag => nameof(LoginPage);

        private readonly IAuthenticationService _authenticationService;
        public IAuthenticationService AuthenticationService => _authenticationService;

        public LoginModule(IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        }
        
        public static void Register(IContainerRegistry containerRegistry) {

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
