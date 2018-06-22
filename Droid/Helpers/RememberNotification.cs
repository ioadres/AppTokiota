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
            //Android.Icu.Util.Calendar Calendar_Object = Android.Icu.Util.Calendar.GetInstance(Android.Icu.Util.TimeZone.Default);
            //Calendar_Object.Set(Android.Icu.Util.CalendarField.Millisecond, DateTime.UtcNow.Millisecond);

            // Set the alarm to start at approximately 2:00 p.m.
            Android.Icu.Util.Calendar calendar = Android.Icu.Util.Calendar.GetInstance(Android.Icu.Util.TimeZone.Default);
            calendar.Set(Android.Icu.Util.CalendarField.Millisecond, DateTime.UtcNow.Millisecond);
            calendar.Set(Android.Icu.Util.CalendarField.HourOfDay, 17);

            // MyView is my current Activity, and AlarmReceiver is the
            // BoradCastReceiver
            //Intent myIntent = new Intent(Activity.ApplicationContext, typeof(AlarmReceiver));

            //PendingIntent pendingIntent = PendingIntent.GetBroadcast(Activity, 0, myIntent, 0);

            //AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context
            //        .GetSystemService(Context.AlarmService);

            /*
             * The following sets the Alarm in the specific time by getting the long
             * value of the alarm date time which is in calendar object by calling
             * the getTimeInMillis(). Since Alarm supports only long value , we're
             * using this method.
             */

            // alarmManager.SetInexactRepeating(AlarmType.RtcWakeup, 600, 1200, pendingIntent);
            //alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);
            //alarmManager.Set(AlarmType.Rtc, Calendar_Object.TimeInMillis, pendingIntent);
            //alarmManager.SetExact(AlarmType.RtcWakeup, Calendar_Object.TimeInMillis, pendingIntent);
            var ms = calendar.TimeInMillis - SystemClock.ElapsedRealtime();
            SetAlarm (ms);
        }
        public void SetAlarm(long miliseconds)
        {
            AlarmManager alarmManager = (AlarmManager)Activity.GetSystemService(Context.AlarmService);
            Intent intent = new Intent(Activity, typeof(AlarmReceiver));

            intent.PutExtra("repeat", true);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Activity, /*id de la alarma que sea unico */0, intent, PendingIntentFlags.CancelCurrent);

            //alarmManager.Set(AlarmType.Rtc, miliseconds, pendingIntent);
            alarmManager.SetInexactRepeating(AlarmType.ElapsedRealtimeWakeup, miliseconds, AlarmManager.IntervalHour, pendingIntent);
            //API19 NOt work  https://stackoverflow.com/questions/27806336/alarmmanager-setinexactrepeating-not-working-in-android-4-1-2-works-on-android
            Toast toast = Toast.MakeText(Activity, "Set", ToastLength.Short);

            toast.Show();
        }

        public void CancelAlarm()
        {
            AlarmManager alarmManager = (AlarmManager)Activity.GetSystemService(Context.AlarmService);
            Intent intent = new Intent(Activity, typeof(AlarmReceiver));

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Activity, /*a traves del anterior id ahora podemos pedir que se actualice */0, intent, PendingIntentFlags.UpdateCurrent);

            //Con el pending intent actualizado podemos cancelarlo
            pendingIntent.Cancel();
            alarmManager.Cancel(pendingIntent);

            Toast toast = Toast.MakeText(Activity, "Remove", ToastLength.Short);

            toast.Show();
        }

        public static void Init(MainActivity mainActivity)
        {
            RememberNotification.Activity = mainActivity;
        }
    }
}
