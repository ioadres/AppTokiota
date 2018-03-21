using AppTokiota.Components.Core.Module;
using AppTokiota.Services;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.Menu
{
    public class MenuModule : IMenuModule
    {
        public static string Tag => nameof(MenuPage);
        private readonly IAuthenticationService _authenticationService;

        public IAuthenticationService AuthenticationService => _authenticationService;

        public MenuModule(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
        }
    }
}
