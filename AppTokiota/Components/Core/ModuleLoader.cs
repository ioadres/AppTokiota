using System;
using AppTokiota.Components.Login;
using Prism.Ioc;
using AppTokiota.Components.Core.Module;
using AppTokiota.Components.Dashboard;

namespace AppTokiota.Components.Core
{
    public static class ModuleLoader
    {
        public static void Load(IContainerRegistry containerRegistry) {
            containerRegistry.Register<ILoginModule, LoginModule>();
            LoginModule.Register(containerRegistry);

            containerRegistry.Register<IDashBoardModule, DashBoardModule>();
            DashBoardModule.Register(containerRegistry);
        }
    }
}
