using System;
using System.Threading.Tasks;
using AppTokiota.Models;

namespace AppTokiota.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
        AuthenticatedUserResponse AuthenticatedUser { get; }
        Task<StateRequest> Login(string email, string password);
        Task Logout();
    }
}
