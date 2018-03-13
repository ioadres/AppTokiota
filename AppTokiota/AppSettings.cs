using System;
using AppTokiota.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AppTokiota
{
    public static partial class AppSettings
    {       
        private static ISettings Settings => CrossSettings.Current;

        // API Endpoints
        public static string MicrosoftAuthEndpoint
        {
            get => Settings.GetValueOrDefault(nameof(MicrosoftAuthEndpoint), DefaultMicrosoftAuthEndpoint);

            set => Settings.AddOrUpdateValue(nameof(MicrosoftAuthEndpoint), value);
        }

        public static string MicrosoftResource
        {
            get => Settings.GetValueOrDefault(nameof(MicrosoftResource), DefaultMicrosoftResource);

            set => Settings.AddOrUpdateValue(nameof(MicrosoftResource), value);
        }

        public static string MicrosoftGrantType
        {
            get => Settings.GetValueOrDefault(nameof(MicrosoftGrantType), DefaultMicrosoftGrantType);

            set => Settings.AddOrUpdateValue(nameof(MicrosoftGrantType), value);
        }

        public static string MicrosoftTenant
        {
            get => Settings.GetValueOrDefault(nameof(MicrosoftTenant), DefaultMicrosoftTenant);

            set => Settings.AddOrUpdateValue(nameof(MicrosoftTenant), value);
        }

        public static string MicrosoftApiClientId
        {
            get => Settings.GetValueOrDefault(nameof(MicrosoftApiClientId), DefaultMicrosoftApiClientId);

            set => Settings.AddOrUpdateValue(nameof(MicrosoftApiClientId), value);
        }

        public static string UrlCompany
        {
            get => Settings.GetValueOrDefault(nameof(UrlCompany), DefaultUrlCompany);

            set => Settings.AddOrUpdateValue(nameof(UrlCompany), value);
        }

        

        // When logout
        public static void RemoveUserData()
        {
            Settings.Remove(nameof(TokenResponse));
        }
    }
}
