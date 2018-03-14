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

        public async Task<bool> Login(string email, string password)
        {
            var succeeded = false;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = AppSettings.MicrosoftAuthEndpoint;

                    var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type","password"},
                    {"client_id", AppSettings.MicrosoftApiClientId},
                    {"resource", AppSettings.MicrosoftResource},
                    {"username", email },
                    {"password", password }
                });
                    var response = await client.PostAsync(url, content);

                    var json = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json);
                    
                    succeeded = true; 
                }
            } catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error with MSAL authentication: {e}");
                //throw new ServiceAuthenticationException();
            }

            return succeeded;            
        }
     
        public Task Logout()
        {
            AppSettings.RemoveUserData();
            throw new NotImplementedException();
        }
        
    }
}
