using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTokiota.Users.Components.Connection
{
    public class ConnectionPageViewModel : ViewModelBase
    {
        private readonly IConnectionModule _connectionModule;

        public ConnectionPageViewModel(IViewModelBaseModule baseModule, IConnectionModule connectionModule) : base(baseModule)
        {
            Title = "Connection";
            _connectionModule = connectionModule;
        }

		#region Close
		public DelegateCommand CloseCommand => new DelegateCommand(Close);
        protected async void Close()
        {
			await BaseModule.NavigationService.GoBackAsync();
        }
        #endregion
    }
}
