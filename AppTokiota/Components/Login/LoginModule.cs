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
        
        public LoginModule(IAuthenticationService authenticationService) {
            SetTag(nameof(LoginPage));

            _authenticationService = authenticationService;
        }

        public Task<TokenResponse> Login(string email, string password)
        {
           return _authenticationService.Login(email, password);
        }

        public String GetUrlCompamy()
        {
            return AppSettings.UrlCompany;
        }


        public override void Register(IContainerRegistry containerRegistry) {

            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        }

    }
}
