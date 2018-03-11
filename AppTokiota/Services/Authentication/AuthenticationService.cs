using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AppTokiota.Models;
using Newtonsoft.Json;

namespace AppTokiota.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
        }

        public bool IsAuthenticated =>false ;

        public Models.TokenResponse AuthenticatedUser => null;

        public async Task<TokenResponse> Login(string email, string password)
        {
            using (var client = new HttpClient())
            {
                var url = AppSettings.MicrosoftAuthEndpoint;

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type","password"},
                    {"client_id", ""},
                    {"resource", ""},
                    {"username", email },
                    {"password", password }
                });
                var response = await client.PostAsync(url, content);

                var json = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json);

                return tokenResponse;
            }
        }
     
        public Task Logout()
        {
            AppSettings.RemoveUserData();
            throw new NotImplementedException();
        }

    }
}
