using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Models;

namespace AppTokiota.Services.Authentication
{
    public class FakeAuthenticationService : IAuthenticationService
    {

        public bool IsAuthenticated => AppSettings.AuthenticatedUserResponse != null;

        public Models.AuthenticatedUserResponse AuthenticatedUser => AppSettings.AuthenticatedUserResponse;

        public async Task<StateRequest> Login(string email, string password)
        {
            await Task.Delay(2000);
            var stateRequest = new StateRequest()
            {
                Success = true               
            };

            if (email.ToLower().StartsWith("a"))
            {
                stateRequest.Success = false;
                stateRequest.Message = "Usuario y/o Contraseña incorrecta.";
            } 
            else
            {
                AppSettings.AuthenticatedUserResponse = new AuthenticatedUserResponse();
                AppSettings.User = new User(email, password);
            }

            return stateRequest;
        }

        public Task Logout()
        {
            AppSettings.RemoveUserData();
            return Task.FromResult(true);
        }
    }
}
