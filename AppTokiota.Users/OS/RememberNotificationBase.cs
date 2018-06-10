using System;
using Xamarin.Forms;

namespace AppTokiota.Users.OS
{
    public interface IRememberNotificationBase
    {
        void RemoveBadgeRememberNotification();
        void EmitCreateRememberNotification();
        void EmitRemoveRememberNotification();
    }
}
