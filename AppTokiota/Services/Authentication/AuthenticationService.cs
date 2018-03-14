using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AppTokiota.Models;
using ModernHttpClient;
using Newtonsoft.Json;

namespace AppTokiota.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
        }

        public bool IsAuthenticated => false;

        public Models.AuthenticatedUserResponse AuthenticatedUser => null;

        public async Task<StateRequest> Login(string email, string password)
        {
            var state = new StateRequest();
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
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
                    var tokenResponse = JsonConvert.DeserializeObject<AuthenticatedUserResponse>(json);
                   
                    if(!response.IsSuccessStatusCode)
                    {
                        state.Message = "Error : Usuario y/o contraseña incorrecta.";
                    }
                    state.Success = response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error with Authentication: {e}");
            }

            return state;
        }

        public Task Logout()
        {
            AppSettings.RemoveUserData();
            throw new NotImplementedException();
        }

    }
}
