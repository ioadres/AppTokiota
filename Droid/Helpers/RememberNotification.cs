using System;
using AppTokiota.Droid.Helpers;
using AppTokiota.Users.OS;
using Xamarin.Forms;
using Android.Content;
using Android.App;
using Android.Widget;
using Android.OS;

[assembly: Dependency(typeof(RememberNotification))]
namespace AppTokiota.Droid.Helpers
{
    public class RememberNotification : IRememberNotificationBase
    {
        public static MainActivity Activity { get; private set; }

        public RememberNotification()
        {
        }

        public void EmitCreateRememberNotification()
        {
            LoadAlarm();
        }

        public void EmitRemoveRememberNotification()
        {
            CancelAlarm();
        }

        public void RemoveBadgeRememberNotification()
        {
        }

        private void LoadAlarm()
        {
            Android.Icu.Util.Calendar calendar = Android.Icu.Util.Calendar.GetInstance(Android.Icu.Util.TimeZone.Default);
            //calendar.Set(Android.Icu.Util.CalendarField.Millisecond, DateTime.UtcNow.Millisecond);
            calendar.Set(Android.Icu.Util.CalendarField.HourOfDay, 15);
            calendar.Set(Android.Icu.Util.CalendarField.Minute, 00);

            var ms = calendar.TimeInMillis;
            SetAlarm (ms);
        }
        public void SetAlarm(long miliseconds)
        {
            AlarmManager alarmManager = (AlarmManager)Activity.GetSystemService(Context.AlarmService);
            Intent intent = new Intent(Activity, typeof(AlarmReceiver));

            intent.PutExtra("repeat", true);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Activity, /*id de la alarma que sea unico */0, intent, PendingIntentFlags.CancelCurrent);
            alarmManager.SetInexactRepeating(AlarmType.RtcWakeup, miliseconds, AlarmManager.IntervalDay, pendingIntent);
            //API19 NOt work  https://stackoverflow.com/questions/27806336/alarmmanager-setinexactrepeating-not-working-in-android-4-1-2-works-on-android
        }

        public void CancelAlarm()
        {
            AlarmManager alarmManager = (AlarmManager)Activity.GetSystemService(Context.AlarmService);
            Intent intent = new Intent(Activity, typeof(AlarmReceiver));

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Activity, /*a traves del anterior id ahora podemos pedir que se actualice */0, intent, PendingIntentFlags.UpdateCurrent);

            //Con el pending intent actualizado podemos cancelarlo
            pendingIntent.Cancel();
            alarmManager.Cancel(pendingIntent);
        }

        public static void Init(MainActivity mainActivity)
        {
            RememberNotification.Activity = mainActivity;
        }
    }
}
