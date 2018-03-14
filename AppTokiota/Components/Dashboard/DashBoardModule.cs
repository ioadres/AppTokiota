using AppTokiota.Components.Core.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Services.Authentication;
using Prism.Ioc;

namespace AppTokiota.Components.Dashboard
{
    public class DashBoardModule : BaseModule, IDashBoardModule
    {
        public static string Tag => nameof(DashBoardPage);

        public DashBoardModule()
        {
        }

        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DashBoardPage, DashBoardViewModel>();
        }
    }
}
