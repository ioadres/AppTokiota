using System;
using AppTokiota.Users.Components.Core;
using AppTokiota.Users.Components.Core.Module;

namespace AppTokiota.Users.Components.Configuration
{
	public class ConfigurationPageViewModel: ViewModelBase
    {
        #region Services
		protected readonly IConfigurationModule _configurationModule;
        #endregion

		public ConfigurationPageViewModel(IViewModelBaseModule baseModule, IConfigurationModule configurationModule) : base(baseModule)
        {
			_configurationModule = configurationModule;

            Title = "Configuration";
        }
    }
}
