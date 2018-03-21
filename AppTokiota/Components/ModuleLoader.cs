using System;
using AppTokiota.Components.Login;
using Prism.Ioc;
using AppTokiota.Components.Core.Module;

namespace AppTokiota.Components
{
    public static class ModuleLoader
    {
        public static void Load(IContainerRegistry containerRegistry, IContainerProvider container) {
            containerRegistry.Register<ILoginModule, LoginModule>();
            LoginModule.Register(containerRegistry);
        }
    }
}
