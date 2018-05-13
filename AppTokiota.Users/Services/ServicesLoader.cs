using System;
using Prism.Ioc;
using AppTokiota.Users.Services;
using AppTokiota.Users.Services.Cache;

namespace AppTokiota.Users.Services
{
    public static class ServicesLoader
    {
        public static void Load(IContainerRegistry containerRegistry)
        {
         
            if (AppSettings.UseFakeServices)
            {
                containerRegistry.RegisterSingleton<IAuthenticationService, FakeAuthenticationService>();
                containerRegistry.RegisterSingleton<ITimesheetService, FakeTimesheetService>();
            } else
            {
                containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
                containerRegistry.RegisterSingleton<ITimesheetService, TimesheetService>();
            }

            containerRegistry.RegisterSingleton<IDialogService, DialogService>();
            containerRegistry.RegisterSingleton<ICacheEntity, AkavacheEntity>();
            containerRegistry.RegisterSingleton<ICalendarService, CalendarService>();
			containerRegistry.RegisterSingleton<INetworkConnectionService, NetworkConnectionService>();

            containerRegistry.Register<IRequestService, RequestService>();
        }
    }
}
