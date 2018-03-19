using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AppTokiota.Models;
using ModernHttpClient;
using Newtonsoft.Json;

namespace AppTokiota.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
        }

        public bool IsAuthenticated => AppSettings.AuthenticatedUserResponse != null;

        public Models.AuthenticatedUserResponse AuthenticatedUser => AppSettings.AuthenticatedUserResponse;

        public async Task<StateRequest> Login(string email, string password)
        {
            var state = new StateRequest();
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    var url = String.Format(AppSettings.MicrosoftAuthEndpoint, AppSettings.MicrosoftTenant);
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
                    
                    if(response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<AuthenticatedUserResponse>(json);
                        AppSettings.AuthenticatedUserResponse = tokenResponse;
                        AppSettings.User = new User(email,password);
                    }
                    else
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
            return Task.FromResult(true);
        }

    }
}
