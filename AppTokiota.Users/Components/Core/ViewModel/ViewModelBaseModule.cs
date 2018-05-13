using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTokiota.Users.Services.Cache;

namespace AppTokiota.Users.Components.Core
{
    public class ViewModelBaseModule : IViewModelBaseModule
    {
        private readonly IAuthenticationService _authenticationService;
		private readonly INetworkConnectionService _networkConnectionService;
        private readonly ICacheEntity _cacheEntity;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public IAuthenticationService AuthenticationService => _authenticationService;
        public ICacheEntity CacheEntity => _cacheEntity;
        public IDialogService DialogService => _dialogService;
        public INavigationService NavigationService => _navigationService;
		public INetworkConnectionService NetworkConnectionService => _networkConnectionService;

		public ViewModelBaseModule(INavigationService navigationService, IAuthenticationService authenticationService, IDialogService dialogService, ICacheEntity cacheEntity,INetworkConnectionService networkConnectionService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _cacheEntity = cacheEntity;
			_networkConnectionService = networkConnectionService;
        }
    }
}
