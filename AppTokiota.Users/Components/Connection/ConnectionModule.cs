using AppTokiota.Users.Components.Connection;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Services;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Components
{
    public class ConnectionModule : IConnectionModule
    {
        public static string Tag => "/" + nameof(ConnectionPage);
        private readonly IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public ConnectionModule(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ConnectionPage, ConnectionPageViewModel>();
        }
    }
}
