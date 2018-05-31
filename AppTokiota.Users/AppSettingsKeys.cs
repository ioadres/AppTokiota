using System;
using AppTokiota.Users.Models;

namespace AppTokiota
{
    public static partial class AppSettings
    {
		// Endpoints

<<<<<<< Updated upstream
        private const string DefaultMicrosoftTenant = "";

        private const string DefaultMicrosoftApiClientId = "";

        private const string DefaultMicrosoftGrantType = "";

        private const string DefaultMicrosoftResource = "";

        private const string DefaultMicrosoftAuthEndpoint = "";
=======
        private const string DefaultMicrosoftTenant = "3c7b5863-ed0c-442a-a87b-48c140428bd7";
        private const string DefaultMicrosoftApiClientId = "d4e094ac-7dc8-4a76-b04f-5774d23b693b";
        private const string DefaultMicrosoftGrantType = "password";
        private const string DefaultMicrosoftResource = "https://tokiota.com/sso";
        private const string DefaultMicrosoftAuthEndpoint = "https://login.microsoft.com/{0}/oauth2/token";
>>>>>>> Stashed changes



        // Endpoint Timesheet
<<<<<<< Updated upstream

        private const string DefaultTimesheetUrlEndPoint = "";
=======
        private const string DefaultTimesheetUrlEndPoint = "https://timesheet.tokiota.com//api/timesheets";
        private const string DefaultTimesheetDomain = "https://timesheet.tokiota.com//api";
>>>>>>> Stashed changes

        private const string DefaultTimesheetDomain = "";



        // App Config
<<<<<<< Updated upstream

        private const string DefaultIdAppCache = "";

        private const string DefaultIdAppUserCache = "";

        private const string DefaultUrlCompany = "";
=======
        private const string DefaultIdAppCache = "TokiotaApp";
        private const string DefaultIdAppUserCache = "TokiotaAppUser";
        private const string DefaultUrlCompany = "http://tokiota.com/";
        private const bool DefaultUseFakeServices = false;
>>>>>>> Stashed changes

        private const bool DefaultUseFakeServices = ;

        private const string DefaultCultureInfoApp = "";

    }
}
