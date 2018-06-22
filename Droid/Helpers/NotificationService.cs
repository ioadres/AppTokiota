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
    [Service]
    public class NotificationService : Service
    {

        protected NotificationManager mManager;


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
            //Getting Notification Service            
            mManager = (NotificationManager)this.BaseContext.ApplicationContext
                    .GetSystemService(NotificationService);
            /*
             * When the user taps the notification we have to show the Home Screen
             * of our App, this job can be done with the help of the following
             * Intent.
             */
            Intent intent1 = new Intent(this.BaseContext.ApplicationContext, typeof(MainActivity));            

            intent1.AddFlags(ActivityFlags.SingleTop
                    | ActivityFlags.ClearTop);

            PendingIntent pendingNotificationIntent = PendingIntent.GetActivity(
                    Android.App.Application.Context.ApplicationContext, 0, intent1,
                    PendingIntentFlags.UpdateCurrent);

            Notification.Builder builder = new Notification.Builder(this);

            builder.SetAutoCancel(true);
            builder.SetTicker("this is ticker text");
            builder.SetContentTitle("TimeSheet");
            builder.SetContentText("Remember input your timesheet ;)");
            builder.SetSmallIcon(Resource.Drawable.logo);
            builder.SetContentIntent(pendingNotificationIntent);
            builder.SetOngoing(true);            
            builder.SetSubText("This is subtext...");   //API level 16
            builder.SetNumber(100);
            var notification = builder.Build();

            mManager.Notify(0, notification);

            return base.OnStartCommand(intent, flags, startId);

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}