using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AppTokiota.Users.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using System.Diagnostics;
using AppTokiota.Users.Utils;
using AppTokiota.Users.Services.Cache;

namespace AppTokiota.Users.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private ICacheEntity _cacheEntity;

        public bool IsAuthenticated => AppSettings.AuthenticatedUserResponse != null;

        public Models.AuthenticatedUserResponse AuthenticatedUser => AppSettings.AuthenticatedUserResponse;

        public AuthenticationService(ICacheEntity cacheEntity)
        {
            _cacheEntity = cacheEntity;
        }

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

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<AuthenticatedUserResponse>(json);
                        AppSettings.AuthenticatedUserResponse = tokenResponse;
                        AppSettings.User = new User(email, password);
                    }
                    else
                    {
                        state.Message = "Error : Usuario y/o contraseña incorrecta.";
                    }

                    state.Success = response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error with Token authentication: {ex}");
                throw new ServiceAuthenticationException();
            }


            return state;
        }

        public async Task<bool> UserIsAuthenticatedAndValidAsync()
        {            
            if (!IsAuthenticated)
            {
                return false;
            }
            try
            {
                var keyToken = AppSettings.IdAppCache;
                var exist = await _cacheEntity.GetObjectAsync<bool>(keyToken);
                if (!exist)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error with token refresh attempt: {ex}");
            }

            return false;
        }

        public Task Logout()
        {
            AppSettings.RemoveUserData();
            return Task.FromResult(true);
        }
    }
}
