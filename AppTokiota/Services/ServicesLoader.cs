using System;
using AppTokiota.Components.Login;
using Prism.Ioc;
using AppTokiota.Services.Authentication;
using AppTokiota.Services.Request;

namespace AppTokiota.Services
{
    public static class ServicesLoader
    {
        public static void Load(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.Register<IRequestService, RequestService>();
        }
    }
}
