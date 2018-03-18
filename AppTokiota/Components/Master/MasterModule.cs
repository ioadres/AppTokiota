using AppTokiota.Components.Core.Module;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.Master
{
    public class MasterModule : IMasterModule
    {

        public static string Tag => "/" + nameof(MasterPage);

        public MasterModule()
        {
        }

        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
        }

        public static string GetMasterNavigationPage(string page) {
            return MasterModule.Tag + BaseNavigation.BaseNavigationModule.Tag + page;
        }
    }
}
