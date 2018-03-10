using System;
using AppTokiota.Attributes;
using AppTokiota.Components.Core;
using Prism.Ioc;
using AppTokiota.Components.Core.Interfaces;

namespace AppTokiota.Components.Login
{
    public class LoginModule: BaseLoginModule
    {
        public LoginModule() {
            Tag = "LoginPage";
        }

        public override void Register(IContainerRegistry containerRegistry) {

            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        }

    }
}
