using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AppTokiota.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using AppTokiota.Services.Authentication;
using System.Diagnostics;
using AppTokiota.Utils;

namespace AppTokiota.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        Timer _accessTokenRenewer;

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

        public Task Logout()
        {
            AppSettings.RemoveUserData();
            return Task.FromResult(true);
        }

        public async Task<bool> UserIsAuthenticatedAndValidAsync()
        {
            if (!IsAuthenticated)
            {
                return false;
            }            
            else
            {
                bool refreshSucceded = false;

                try
                {
                   /* var tokenCache = App.AuthenticationClient.UserTokenCache;
                    AuthenticationResult ar = await App.AuthenticationClient.AcquireTokenSilentAsync(
                        new string[] { AppSettings.B2cClientId },
                        AuthenticatedUser.Id,
                        $"{AppSettings.B2cAuthority}{AppSettings.B2cTenant}",
                        AppSettings.B2cPolicy,
                        true);
                    SaveAuthenticationResult(ar);*/

                    refreshSucceded = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error with token refresh attempt: {ex}");
                }

                return refreshSucceded;
            }
        }

        public async Task InitializeAsync()
        {
            await UserIsAuthenticatedAndValidAsync();
            _accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback), this, TimeSpan.FromSeconds(int.Parse(AppSettings.AuthenticatedUserResponse.ExpiresIn)), TimeSpan.FromMilliseconds(-1));
        }

        async Task OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                await UserIsAuthenticatedAndValidAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Failed to renew access token. Details: {0}", ex.Message));
            }
            finally
            {
                try
                {
                    _accessTokenRenewer.Change(TimeSpan.FromSeconds(int.Parse(AppSettings.AuthenticatedUserResponse.ExpiresIn)), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
                }
            }
        } 
    }
}
