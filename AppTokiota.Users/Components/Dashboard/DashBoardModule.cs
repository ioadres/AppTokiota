using AppTokiota.Users.Components.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Services;
using Prism.Ioc;
using AppTokiota.Users.Components.Core;

namespace AppTokiota.Users.Components.DashBoard
{
    public class DashBoardModule : IDashBoardModule
    {
        private readonly IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public DashBoardModule(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService; 
        }
    }
}
