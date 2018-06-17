using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.App;
using System.Threading;
using Java.Util;

namespace AppTokiota.Droid.Helpers
{
    public class NotificationService : Service
    {

        private NotificationManager mManager;

        //public class LocalBinder: Binder
        //{
        //    NotificationService getService() {
        //        return NotificationService.this;
        //    }
        //}

    public override IBinder OnBind(Intent intent)
    {
        return null;
    }

    public override void OnCreate()
    {
        base.OnCreate();
    }

    [return: GeneratedEnum]
    public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        return base.OnStartCommand(intent, flags, startId);

        //Getting Notification Service
        mManager = (NotificationManager)Android.App.Application.Context.ApplicationContext
                .GetSystemService("NOTIFICATION_SERVICE");
        /*
         * When the user taps the notification we have to show the Home Screen
         * of our App, this job can be done with the help of the following
         * Intent.
         */
        Intent intent1 = new Intent(Android.App.Application.Context.ApplicationContext, typeof(MainActivity));

        Notification notification = new Notification(Resource.Drawable.logo,
                "See My App something for you", Java.Lang.JavaSystem.CurrentTimeMillis());

        intent1.AddFlags(ActivityFlags.SingleTop
                | ActivityFlags.ClearTop);

        PendingIntent pendingNotificationIntent = PendingIntent.GetActivity(
                Android.App.Application.Context.ApplicationContext, 0, intent1,
                PendingIntentFlags.UpdateCurrent);

        notification.Flags |= NotificationFlags.AutoCancel;

        notification.SetLatestEventInfo(Application.Context.ApplicationContext,
                "SANBOOK", "See My App something for you",
                pendingNotificationIntent);

        mManager.Notify(0, notification);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
}