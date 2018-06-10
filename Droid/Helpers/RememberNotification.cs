using System;
using AppTokiota.Droid.Helpers;
using AppTokiota.Users.OS;
using Xamarin.Forms;

[assembly: Dependency(typeof(RememberNotification))]
namespace AppTokiota.Droid.Helpers
{
    public class RememberNotification : IRememberNotificationBase
    {
        public RememberNotification()
        {
        }

        public void EmitCreateRememberNotification()
        {
        }

        public void EmitRemoveRememberNotification()
        {
        }

        public void RemoveBadgeRememberNotification()
        {
        }
    }
}
