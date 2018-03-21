using System;
using AppTokiota.Users.Models;

namespace AppTokiota
{
    public static partial class AppSettings
    {
        // Endpoints
        private const string DefaultMicrosoftTenant = "";
        private const string DefaultMicrosoftApiClientId = "";
        private const string DefaultMicrosoftGrantType = "password";
        private const string DefaultMicrosoftResource = "";
        private const string DefaultMicrosoftAuthEndpoint = "https://login.microsoft.com/{0}/oauth2/token";


        // Endpoint Timesheet
        private const string DefaultTimesheetUrlEndPoint = "?";
        



        // App Config
        private const string DefaultUrlCompany = "http://tokiota.com/";
        private const bool DefaultUseFakeServices = false;


    }
}
