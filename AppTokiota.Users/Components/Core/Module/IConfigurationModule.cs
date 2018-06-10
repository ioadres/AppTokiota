using System;
using AppTokiota.Users.OS;

namespace AppTokiota.Users.Components.Core.Module
{
    public interface IConfigurationModule
    {
        IRememberNotificationBase RememberNotificationBase { get; }
    }
}
