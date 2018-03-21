using AppTokiota.Users.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Components.Core.Module
{
   public interface IViewModelBaseModule : IBaseModule
    {
        IAuthenticationService AuthenticationService { get; }
        IDialogService DialogService { get; }
        INavigationService NavigationService { get; }
    }
}
