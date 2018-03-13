using System;
using System.Threading.Tasks;
using AppTokiota.Models;
using AppTokiota.Services.Authentication;

namespace AppTokiota.Components.Core.Module
{
    public interface ILoginModule : IBaseModule
    {
        IAuthenticationService AuthenticationService { get; }
    }
}
