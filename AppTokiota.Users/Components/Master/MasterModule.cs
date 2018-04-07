using AppTokiota.Users.Components.Core.Module;
using Prism.Ioc;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.BaseNavigation;

namespace AppTokiota.Users.Components.Master
{
    public class MasterModule : IMasterModule
    {
        public MasterModule()
        {
        }

        public static void Register(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
        }

        public static string GetMasterNavigationPage(string page) {
            return $"{PageRoutes.GetKey<MasterPage>()}{PageRoutes.GetKey<BaseNavigationPage>()}{page}";
        }
    }
}
