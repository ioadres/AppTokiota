using System;
using AppTokiota.Users.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using AppTokiota.Users.Extensions;
using System.Globalization;

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

        public static string AppCenter
        {
            get => Settings.GetValueOrDefault(nameof(AppCenter), DefaultAppCenter);

            set => Settings.AddOrUpdateValue(nameof(AppCenter), value);
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

        public static int HoursDay
        {
            get => Settings.GetValueOrDefault(nameof(HoursDay), DefaultHoursDay);

            set => Settings.AddOrUpdateValue(nameof(HoursDay), value);
        }

        public static string StartupView
        {
            get => Settings.GetValueOrDefault(nameof(StartupView), DefaultStartupView);

            set => Settings.AddOrUpdateValue(nameof(StartupView), value);
        }


        public static string UrlCompany
        {
            get => Settings.GetValueOrDefault(nameof(UrlCompany), DefaultUrlCompany);

            set => Settings.AddOrUpdateValue(nameof(UrlCompany), value);
        }

        public static string UrlTwitterCompany
        {
            get => Settings.GetValueOrDefault(nameof(UrlCompany), DefaultUrlTwitterCompany);

            set => Settings.AddOrUpdateValue(nameof(UrlCompany), value);
        }

        public static string UrlLinkedinCompany
        {
            get => Settings.GetValueOrDefault(nameof(UrlCompany), DefaultUrlLinkedinCompany);

            set => Settings.AddOrUpdateValue(nameof(UrlCompany), value);
        }

        public static string UrlCodeCompany
        {
            get => Settings.GetValueOrDefault(nameof(UrlCompany), DefaultUrlCodeCompany);

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

        //Culture Info
        public static String CultureInfoApp
        {
            get => Settings.GetValueOrDefault(nameof(CultureInfoApp), DefaultCultureInfoApp);

            set => Settings.AddOrUpdateValue(nameof(CultureInfoApp), value);
        }


        public static bool IsEnableNotification
        {
            get => Settings.GetValueOrDefault(nameof(IsEnableNotification), DefaultIsEnableNotification);

            set => Settings.AddOrUpdateValue(nameof(IsEnableNotification), value);
        }

        public static bool IsEnableCache
        {
            get => Settings.GetValueOrDefault(nameof(IsEnableCache), DefaultIsEnableCache);

            set => Settings.AddOrUpdateValue(nameof(IsEnableCache), value);
        }

        public static bool SendReview
        {
            get => Settings.GetValueOrDefault(nameof(SendReview), DefaultSendReview);

            set => Settings.AddOrUpdateValue(nameof(SendReview), value);
        }

    }
}
