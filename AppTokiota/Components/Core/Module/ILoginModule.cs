using System;
using System.Threading.Tasks;
using AppTokiota.Models;
using AppTokiota.Services.Authentication;
using AppTokiota.Services.Dialog;

namespace AppTokiota.Components.Core.Module
{
    public interface ILoginModule : IBaseModule
    {
        IAuthenticationService AuthenticationService { get; }
        IDialogService DialogService { get; }
    }
}
