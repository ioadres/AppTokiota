using System;
using Xamarin.Forms;

namespace AppTokiota.Users.OS
{
    public interface IRememberNotificationBase
    {

        void EmitCreateRememberNotification();
        void EmitRemoveRememberNotification();


    }
}
