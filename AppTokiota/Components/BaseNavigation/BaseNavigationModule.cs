using AppTokiota.Components.Core.Module;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.BaseNavigation
{
    public class BaseNavigationModule : IBaseNavigationModule
    {
        public static string Tag => "/" + nameof(BaseNavigationPage) + "/";

        public BaseNavigationModule()
        {

        }

        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<BaseNavigationPage, BaseNavigationPageViewModel>();
        }
    }
}
