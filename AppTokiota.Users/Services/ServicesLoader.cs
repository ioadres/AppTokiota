using System;
using Prism.Ioc;
using AppTokiota.Users.Services;

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
                containerRegistry.Register<ITimesheetService, TimesheetService>();
            }

            containerRegistry.Register<ICalendarService, CalendarService>();
            containerRegistry.Register<IRequestService, RequestService>();
            containerRegistry.Register<IDialogService, DialogService>();
        }
    }
}
