using System;
using Prism.Ioc;
using System.Threading.Tasks;
using AppTokiota.Models;

namespace AppTokiota.Components.Core.Module
{
    public interface ILoginModule : IBaseModule
    {
        Task<TokenResponse> Login(string email, string password);
        String GetUrlCompamy();
    }
}
