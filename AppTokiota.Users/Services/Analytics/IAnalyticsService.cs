using System;
namespace AppTokiota.Users.Services
{
    public interface IAnalyticsService
    {
        void TrackEvent(string message);
    }
}
