using AppTokiota.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.Core.Module
{
   public interface IViewModelBaseModule : IBaseModule
    {
        IAuthenticationService AuthenticationService { get; }
        IDialogService DialogService { get; }
        INavigationService NavigationService { get; }
    }
}
