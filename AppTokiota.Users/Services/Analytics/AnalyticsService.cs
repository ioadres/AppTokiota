using System;
using System.Collections.Generic;
using AppTokiota.Users.Models;
using Microsoft.AppCenter.Analytics;

namespace AppTokiota.Users.Services
{
    public class AnalyticsService: IAnalyticsService
    {
        private ICacheEntity _cacheService;

        public AnalyticsService(ICacheEntity cache) {
            _cacheService = cache;
        }

        public async void TrackEvent(string message) {

            var user = await _cacheService.GetObjectAsync<User>(AppSettings.IdAppUserCache);

            var dictionary = new Dictionary<string, string>();
            if (user != null) {
                dictionary.Add("User", user.Email);
            }

            Analytics.TrackEvent(message,dictionary);
        }
    }
}
