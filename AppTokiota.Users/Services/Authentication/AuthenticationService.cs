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
using Akavache;

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

                        var user = new User(email, password);
                        await _cacheEntity.InsertObjectAsync(AppSettings.IdAppUserCache, user);
                        await _cacheEntity.InsertObjectAsync(AppSettings.IdAppCache, true, DateTimeOffset.Now.AddSeconds(Double.Parse(AppSettings.AuthenticatedUserResponse.ExpiresIn)));
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

        public async Task<bool> RefreshToken() 
        {
            try
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    var url = String.Format(AppSettings.MicrosoftAuthEndpoint, AppSettings.MicrosoftTenant);
                    var content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"grant_type","refresh_token"},
                        {"client_id", AppSettings.MicrosoftApiClientId},
                        {"resource", AppSettings.MicrosoftResource},
                        {"refresh_token", AppSettings.AuthenticatedUserResponse.RefreshToken }
                    });
                    var response = await client.PostAsync(url, content);
                    var json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<AuthenticatedRefreshTokenResponse>(json);
                        AppSettings.AuthenticatedUserResponse = AuthenticatedUserResponse.Mapper(AppSettings.AuthenticatedUserResponse, tokenResponse);
                        await _cacheEntity.InsertObjectAsync(AppSettings.IdAppCache, true, DateTimeOffset.Now.AddSeconds(Double.Parse(AppSettings.AuthenticatedUserResponse.ExpiresIn)));
                    }
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error with refresh Token authentication: {ex}");
                return false;
            }
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
                if(!exist) {
                    exist = await RefreshToken();
                }
                return exist;
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
            _cacheEntity.RemoveObjectAsync<User>(AppSettings.IdAppUserCache);
            _cacheEntity.RemoveObjectAsync<bool>(AppSettings.IdAppCache);
            return Task.FromResult(true);
        }
    }
}
