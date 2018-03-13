using System;
using System.Threading.Tasks;
using AppTokiota.Models;

namespace AppTokiota.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
        TokenResponse AuthenticatedUser { get; }
        Task<bool> Login(string email, string password);
        Task Logout();
    }
}
