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
                containerRegistry.RegisterSingleton<INetworkConnectionService, FakeNetworkConnectionService>();
                containerRegistry.RegisterSingleton<IAuthenticationService, FakeAuthenticationService>();
                containerRegistry.RegisterSingleton<ITimesheetService, FakeTimesheetService>();
                containerRegistry.RegisterSingleton<IReviewService, FakeReviewService>();
            } else
            {
                containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
                containerRegistry.RegisterSingleton<ITimesheetService, TimesheetService>();
                containerRegistry.RegisterSingleton<INetworkConnectionService, NetworkConnectionService>();
                containerRegistry.RegisterSingleton<IReviewService, ReviewService>();
            }

            containerRegistry.RegisterSingleton<IDialogService, DialogService>();
			containerRegistry.RegisterSingleton<IDialogErrorCustomService, DialogErrorCustomService>();
            containerRegistry.RegisterSingleton<ICacheEntity, AkavacheEntity>();
            containerRegistry.RegisterSingleton<ICalendarService, CalendarService>();

            containerRegistry.Register<IRequestService, RequestService>();
        }
    }
}
