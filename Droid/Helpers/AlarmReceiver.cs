using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppTokiota.Droid.Helpers
{
    [BroadcastReceiver]
    [IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            // When our Alaram time is triggered , this method will be excuted (onReceive)
            // We're invoking a service in this method which shows Notification to the User
            Intent myIntent = new Intent(context, typeof(NotificationService));
            context.StartService(myIntent);
        }
}
}