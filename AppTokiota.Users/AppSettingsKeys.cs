using System;
using AppTokiota.Users.Models;

namespace AppTokiota
{
    public static partial class AppSettings
    {
        private const string DefaultMicrosoftTenant = "3c7b5863-ed0c-442a-a87b-48c140428bd7";
        private const string DefaultMicrosoftApiClientId = "d4e094ac-7dc8-4a76-b04f-5774d23b693b";
        private const string DefaultMicrosoftGrantType = "password";
        private const string DefaultMicrosoftResource = "https://tokiota.com/sso";
        private const string DefaultMicrosoftAuthEndpoint = "https://login.microsoft.com/{0}/oauth2/token";
        private const string DefaultAppCenter = "ios=0a951cf0-dd56-4345-9c3f-6d4b8b31704d;" + "android=40b5c075-e36b-4f98-8829-3c952b6f89bb";

        // Endpoint Timesheet

        private const string DefaultTimesheetUrlEndPoint = "https://timesheet.tokiota.com/api/timesheets";
        private const string DefaultTimesheetDomain = "https://timesheet.tokiota.com/api";


        // App Config
        private const string DefaultIdAppCache = "TokiotaApp";
        private const string DefaultIdAppUserCache = "TokiotaAppUser";

        private const int DefaultHoursDay = 8;
        private const string DefaultStartupView = "DashBoardPage";

        private const string DefaultUrlTwitterCompany = "https://twitter.com/tokiota_it";
        private const string DefaultUrlLinkedinCompany = "https://www.linkedin.com/company/tokiota/";
        private const string DefaultUrlCodeCompany = "https://github.com/ioadres/AppTokiota";
        private const string DefaultUrlCompany = "http://tokiota.com/";

        private const string DefaultCultureInfoApp = "EN";
        private const bool DefaultIsEnableNotification = true;
        private const bool DefaultIsEnableCache = true;
        private const bool DefaultSendReview = true;


        private const bool DefaultUseFakeServices = false;
    }
}
