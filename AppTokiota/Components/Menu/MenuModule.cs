using AppTokiota.Components.Core.Module;
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

        public MenuModule()
        {

        }

        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
        }
    }
}
