using AppTokiota.Components.Login;
using Prism.Ioc;
using AppTokiota.Components.Core.Module;
using AppTokiota.Components.Dashboard;
using AppTokiota.Components.Splash;
using AppTokiota.Components.Menu;
using AppTokiota.Components.Master;
using AppTokiota.Components.BaseNavigation;

namespace AppTokiota.Components.Core
{
    public static class ModuleLoader
    {
        public static void Load(IContainerRegistry containerRegistry) {
            containerRegistry.Register<ILoginModule, LoginModule>();
            LoginModule.Register(containerRegistry);

            containerRegistry.Register<IDashBoardModule, DashBoardModule>();
            DashBoardModule.Register(containerRegistry);

            containerRegistry.Register<ISplashModule, SplashModule>();
            SplashModule.Register(containerRegistry);

            containerRegistry.Register<IMenuModule, MenuModule>();
            MenuModule.Register(containerRegistry);

            containerRegistry.Register<IMasterModule, MasterModule>();
            MasterModule.Register(containerRegistry);

            containerRegistry.Register<IBaseNavigationModule, BaseNavigationModule>();
            BaseNavigationModule.Register(containerRegistry);
        }
    }
}
