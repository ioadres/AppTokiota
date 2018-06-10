using System;
using AppTokiota.Users.Components.Core.Module;
using AppTokiota.Users.OS;

namespace AppTokiota.Users.Components.Configuration
{
	public class ConfigurationModule: IConfigurationModule
    {
        private readonly IRememberNotificationBase _rememberNotificationBase;

        public IRememberNotificationBase RememberNotificationBase => _rememberNotificationBase;

        public ConfigurationModule(IRememberNotificationBase rememberNotificationBase)
        {
            _rememberNotificationBase = rememberNotificationBase;
        }
    }
}
