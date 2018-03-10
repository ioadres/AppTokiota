using System;
using AppTokiota.Components.Core.Interfaces;
using AppTokiota.Components.Login;
using Prism.Ioc;

namespace AppTokiota.Components.Core
{
    public static class ModuleLoader
    {
        public static void Load(IContainerRegistry containerRegistry) {
            containerRegistry.Register<BaseLoginModule, LoginModule>();
        }
    }
}
