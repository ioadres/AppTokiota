using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.Components.DashBoard;
using AppTokiota.Users.Components.Master;
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
        protected void Close()
        {
			if(IsInternetAndCloseModal()) {
                NavigateCommand.Execute(MasterModule.GetMasterNavigationPage(PageRoutes.GetKey<DashBoardPage>()));
			}
        }
        #endregion
    }
}
