using AppTokiota.Components.Core.Module;
using AppTokiota.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Components.Core
{
    public class ViewModelBaseModule : IViewModelBaseModule
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;

        protected readonly INavigationService _navigationService;

        public IAuthenticationService AuthenticationService => _authenticationService;
        public IDialogService DialogService => _dialogService;

        public INavigationService NavigationService => _navigationService;

        public ViewModelBaseModule(INavigationService navigationService, IAuthenticationService authenticationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _dialogService = dialogService;
        }
    }
}
