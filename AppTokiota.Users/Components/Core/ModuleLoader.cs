using AppTokiota.Users.Components.Login;
using Prism.Ioc;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components;
using AppTokiota.Users.Components.Splash;
using AppTokiota.Users.Components.Menu;
using AppTokiota.Users.Components.Master;
using AppTokiota.Users.Components.BaseNavigation;
using AppTokiota.Users.Components.Timesheet;
using AppTokiota.Users.Components.Connection;
using Prism.Unity;
using Unity;
using AppTokiota.Users.Components.DashBoard;
using AppTokiota.Users.Components.Activity;
using AppTokiota.Users.Components.ManageImputedDay;
using System;

namespace AppTokiota.Users.Components.Core
{
    public static class ModuleLoader
    {
        public static void Load(IContainerRegistry containerRegistry)
        {

            containerRegistry.Register<IViewModelBaseModule, ViewModelBaseModule>();

            containerRegistry.Register<ILoginModule, LoginModule>();
            containerRegistry.RegisterForNavigation<LoginPage>();
            PageRoutes.AddKey<LoginPage>($"/{nameof(LoginPage)}");

            containerRegistry.Register<IDashBoardModule, DashBoardModule>();
            containerRegistry.RegisterForNavigation<DashBoardPage>();
            PageRoutes.AddKey<DashBoardPage>(nameof(DashBoardPage));

            containerRegistry.Register<ISplashModule, SplashModule>();
            containerRegistry.RegisterForNavigation<SplashPage>();
            PageRoutes.AddKey<SplashPage>(nameof(SplashPage));

            containerRegistry.Register<IMenuModule, MenuModule>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            PageRoutes.AddKey<MenuPage>(nameof(MenuPage));

            containerRegistry.Register<IMasterModule, MasterModule>();
            containerRegistry.RegisterForNavigation<MasterPage>();
            PageRoutes.AddKey<MasterPage>($"/{nameof(MasterPage)}");

            containerRegistry.Register<IBaseNavigationModule, BaseNavigationModule>();
            containerRegistry.RegisterForNavigation<BaseNavigationPage>();
            PageRoutes.AddKey<BaseNavigationPage>($"/{nameof(BaseNavigationPage)}/");

            containerRegistry.Register<IConnectionModule, ConnectionModule>();
            containerRegistry.RegisterForNavigation<ConnectionPage>();
            PageRoutes.AddKey<ConnectionPage>($"/{nameof(ConnectionPage)}");

            containerRegistry.Register<ITimesheetModule, TimesheetModule>();
            containerRegistry.RegisterForNavigation<TimesheetPage>();
            PageRoutes.AddKey<TimesheetPage>(nameof(TimesheetPage));

            containerRegistry.Register<IManageImputedDayModule, ManageImputedDayModule>();
            containerRegistry.RegisterForNavigation<ManageImputedDayPage>();
            PageRoutes.AddKey<ManageImputedDayPage>(nameof(ManageImputedDayPage));

            containerRegistry.Register<IAddActivityModule, AddActivityModule>();
            containerRegistry.RegisterForNavigation<AddActivityPage>();
            PageRoutes.AddKey<AddActivityPage>($"{nameof(AddActivityPage)}");

            containerRegistry.RegisterForNavigation<AddActivityTimeDesviationPage>();
            PageRoutes.AddKey<AddActivityTimeDesviationPage>($"{nameof(AddActivityTimeDesviationPage)}"); 


        }

    }
}
