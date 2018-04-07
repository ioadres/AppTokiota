using AppTokiota.Users.Components.Connection;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Services;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Components.Connection
{
    public class ConnectionModule : IConnectionModule
    {
        private readonly IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public ConnectionModule(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
    }
}
