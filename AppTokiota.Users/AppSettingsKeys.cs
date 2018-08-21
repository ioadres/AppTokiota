using System;
using AppTokiota.Users.Models;

namespace AppTokiota
{
    public static partial class AppSettings
    {
        private const string DefaultMicrosoftTenant = "";
        private const string DefaultMicrosoftApiClientId = "";
        private const string DefaultMicrosoftGrantType = "password";
        private const string DefaultMicrosoftResource = "";
        private const string DefaultMicrosoftAuthEndpoint = "";
        private const string DefaultAppCenter = "";

        // Endpoint Timesheet

        private const string DefaultTimesheetUrlEndPoint = "";
        private const string DefaultTimesheetDomain = "";


        // App Config
        private const string DefaultIdAppCache = "";
        private const string DefaultIdAppUserCache = "";

        private const int DefaultHoursDay = 8;
        private const string DefaultStartupView = "DashBoardPage";

        private const string DefaultUrlTwitterCompany = "https://twitter.com/tokiota_it";
        private const string DefaultUrlLinkedinCompany = "https://www.linkedin.com/company/tokiota/";
        private const string DefaultUrlCodeCompany = "https://github.com/ioadres/AppTokiota";
        private const string DefaultUrlCompany = "http://tokiota.com/";

        private const string DefaultCultureInfoApp = "EN";
        private const bool DefaultIsEnableNotification = true;
        private const bool DefaultIsEnableCache = true;
        private const bool DefaultSendReview = false;
        private const bool DefaultUseFakeServices = false;


    }
}