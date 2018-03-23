using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;
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
            Title = "Dashboard";
            _connectionModule = connectionModule;
        }
    }
}
