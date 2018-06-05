using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTokiota.Users.Components.Core
{
    public class ViewModelBaseModule : IViewModelBaseModule
    {
        private readonly IAuthenticationService _authenticationService;
		private readonly INetworkConnectionService _networkConnectionService;
        private readonly ICacheEntity _cacheEntity;
        private readonly IDialogService _dialogService;
		private readonly IDialogErrorCustomService _dialogErrorCustomService;
        private readonly INavigationService _navigationService;
        private readonly IAnalyticsService _analytics;

        public IAnalyticsService AnalyticsService => _analytics;
        public IAuthenticationService AuthenticationService => _authenticationService;
        public ICacheEntity CacheEntity => _cacheEntity;
        public IDialogService DialogService => _dialogService;
        public INavigationService NavigationService => _navigationService;
		public INetworkConnectionService NetworkConnectionService => _networkConnectionService;

		public IDialogErrorCustomService DialogErrorCustomService => _dialogErrorCustomService;

        public ViewModelBaseModule(IAnalyticsService analytics ,INavigationService navigationService, IAuthenticationService authenticationService, IDialogService dialogService, ICacheEntity cacheEntity,INetworkConnectionService networkConnectionService, IDialogErrorCustomService dialogErrorCustom)
        {
            _analytics = analytics;
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _cacheEntity = cacheEntity;
			_networkConnectionService = networkConnectionService;
			_dialogErrorCustomService = dialogErrorCustom;
        }
    }
}
