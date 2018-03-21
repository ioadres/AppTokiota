using System;
using AppTokiota.Users.Models;

namespace AppTokiota
{
    public static partial class AppSettings
    {
        // Endpoints
        private const string DefaultMicrosoftTenant = "3c7b5863-ed0c-442a-a87b-48c140428bd7";
        private const string DefaultMicrosoftApiClientId = "d4e094ac-7dc8-4a76-b04f-5774d23b693b";
        private const string DefaultMicrosoftGrantType = "password";
        private const string DefaultMicrosoftResource = "https://tokiota.com/sso";
        private const string DefaultMicrosoftAuthEndpoint = "https://login.microsoft.com/{0}/oauth2/token";


        // Endpoint Timesheet
        private const string DefaultTimesheetUrlEndPoint = "https://timesheet.tokiota.com//api/timesheets?";
        



        // App Config
        private const string DefaultUrlCompany = "http://tokiota.com/";
        private const bool DefaultUseFakeServices = false;


    }
}
