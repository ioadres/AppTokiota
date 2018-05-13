using AppTokiota.Users.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Services.Cache;

namespace AppTokiota.Users.Components.Core.Module
{
   public interface IViewModelBaseModule : IBaseModule
    {
		INetworkConnectionService NetworkConnectionService { get; }
        IAuthenticationService AuthenticationService { get; }
        IDialogService DialogService { get; }
        INavigationService NavigationService { get; }
        ICacheEntity CacheEntity { get; }
    }
}
