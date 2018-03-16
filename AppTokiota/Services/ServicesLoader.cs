using System;
using AppTokiota.Components.Login;
using Prism.Ioc;
using AppTokiota.Services.Authentication;
using AppTokiota.Services.Request;
using AppTokiota.Services.Dialog;

namespace AppTokiota.Services
{
    public static class ServicesLoader
    {
        public static void Load(IContainerRegistry containerRegistry)
        {
         
            if (AppSettings.UseFakeServices)
            {
                containerRegistry.RegisterSingleton<IAuthenticationService, FakeAuthenticationService>();
            } else
            {
                containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            }

            containerRegistry.Register<IRequestService, RequestService>();
            containerRegistry.Register<IDialogService, DialogService>();
        }
    }
}
