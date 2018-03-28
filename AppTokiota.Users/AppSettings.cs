using System;
using AppTokiota.Users.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using AppTokiota.Users.Extensions;

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

        public static string IdAppCache
        {
            get => Settings.GetValueOrDefault(nameof(IdAppCache), DefaultIdAppCache);

            set => Settings.AddOrUpdateValue(nameof(IdAppCache), value);
        }

        public static string IdAppUserCache
        {
            get => Settings.GetValueOrDefault(nameof(IdAppUserCache), DefaultIdAppUserCache);

            set => Settings.AddOrUpdateValue(nameof(IdAppUserCache), value);
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

        // Api Timesheet Endpoint

        
        public static string TimesheetUrlEndPoint
        {
            get => Settings.GetValueOrDefault(nameof(TimesheetUrlEndPoint), DefaultTimesheetUrlEndPoint);

            set => Settings.AddOrUpdateValue(nameof(TimesheetUrlEndPoint), value);
        }

        public static string TimesheetDomain
        {
            get => Settings.GetValueOrDefault(nameof(TimesheetDomain), DefaultTimesheetDomain);

            set => Settings.AddOrUpdateValue(nameof(TimesheetDomain), value);
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
        }
    }
}
