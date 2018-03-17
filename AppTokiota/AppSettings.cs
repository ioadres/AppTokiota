using System;
using AppTokiota.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using AppTokiota.Extensions;

namespace AppTokiota
{
    public static partial class AppSettings
    {       
        private static ISettings Settings => CrossSettings.Current;

        public static AuthenticatedUserResponse AuthenticatedUserResponse
        {
            get => Settings.GetValueOrDefault(nameof(AuthenticatedUserResponse), default(AuthenticatedUserResponse));

            set => Settings.AddOrUpdateValue(nameof(AuthenticatedUserResponse), value);
        }

        public static User User
        {
            get => Settings.GetValueOrDefault(nameof(User), default(User));

            set => Settings.AddOrUpdateValue(nameof(User), value);
        }

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

        public static bool UseFakeServices
        {
            get => Settings.GetValueOrDefault(nameof(UseFakeServices), DefaultUseFakeServices);

            set => Settings.AddOrUpdateValue(nameof(UseFakeServices), value);
        }



        // When logout
        public static void RemoveUserData()
        {
            Settings.Remove(nameof(AuthenticatedUserResponse));
            Settings.Remove(nameof(User));
        }
    }
}
