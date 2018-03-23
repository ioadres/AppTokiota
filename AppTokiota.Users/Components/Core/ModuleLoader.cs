using AppTokiota.Users.Components.Login;
using Prism.Ioc;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components.Dashboard;
using AppTokiota.Users.Components.Splash;
using AppTokiota.Users.Components.Menu;
using AppTokiota.Users.Components.Master;
using AppTokiota.Users.Components.BaseNavigation;
using AppTokiota.Users.Components.Timesheet;
using AppTokiota.Users.Components.Connection;

namespace AppTokiota.Users.Components.Core
{
    public static class ModuleLoader
    {
        public static void Load(IContainerRegistry containerRegistry) {

            containerRegistry.Register<IViewModelBaseModule, ViewModelBaseModule>();

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


            containerRegistry.Register<IConnectionModule, ConnectionModule>();
            ConnectionModule.Register(containerRegistry);

            containerRegistry.Register<ITimesheetModule, TimesheetModule>();
            TimesheetModule.Register(containerRegistry);
        }
    }
}
