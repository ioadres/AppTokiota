using AppTokiota.Components.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Services;
using Prism.Ioc;

namespace AppTokiota.Components.Dashboard
{
    public class DashBoardModule : BaseModule, IDashBoardModule
    {
        public static string Tag => nameof(DashBoardPage);
        private readonly IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public DashBoardModule(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DashBoardPage, DashBoardPageViewModel>();
        }
    }
}
