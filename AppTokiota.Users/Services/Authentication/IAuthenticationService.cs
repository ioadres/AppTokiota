using System;
using System.Threading.Tasks;
using AppTokiota.Users.Models;
using System.Net.Http;

namespace AppTokiota.Users.Services
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
        AuthenticatedUserResponse AuthenticatedUser { get; }
        Task<StateRequest> Login(string email, string password);
		Task<bool> UserIsAuthenticatedAndValidAsync();
        Task Logout();
    }
}
