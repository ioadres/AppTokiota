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
            await Task.Delay(500);
            var stateRequest = new StateRequest()
            {
                Success = true               
            };

            if (email.StartsWith("a"))
            {
                stateRequest.Success = false;
                stateRequest.Message = "Usuario y/o Contraseña incorrecta.";
            } else
            {
                AppSettings.AuthenticatedUserResponse = new AuthenticatedUserResponse();
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
