using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Prism;
using Prism.Ioc;
using UXDivers.Gorilla.Droid;
using AppTokiota.Users.Controls;
using AppTokiota.Droid.Renderers;
using Prism.Unity;
using Acr.UserDialogs;
using Lottie.Forms.Droid;
using AppTokiota.Users.OS;
using AppTokiota.Droid.Helpers;

namespace AppTokiota.Droid
{
    [Activity(Label = "TimeSheet", Icon = "@drawable/logo", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = AppTokiota.Droid.Resource.Layout.Tabbar;
            ToolbarResource = AppTokiota.Droid.Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            UserDialogs.Init(this);
            AnimationViewRenderer.Init();
            LoadAlarm();

            LoadApplication(new AppTokiota.Users.App(new AndroidInitializer()));
        }

        private void LoadAlarm()
        {
            Android.Icu.Util.Calendar Calendar_Object = Android.Icu.Util.Calendar.GetInstance(Android.Icu.Util.TimeZone.Default);

            Calendar_Object.Set(Android.Icu.Util.CalendarField.Month, 6);
            Calendar_Object.Set(Android.Icu.Util.CalendarField.Year, 2018);
            Calendar_Object.Set(Android.Icu.Util.CalendarField.DayOfMonth, 15);

            Calendar_Object.Set(Android.Icu.Util.CalendarField.HourOfDay, 21);
            Calendar_Object.Set(Android.Icu.Util.CalendarField.Minute, 52);
            Calendar_Object.Set(Android.Icu.Util.CalendarField.Second, 0);

            // MyView is my current Activity, and AlarmReceiver is the
            // BoradCastReceiver
            Intent myIntent = new Intent(this, typeof(AlarmReceiver));

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, myIntent, 0);

            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context
                    .GetSystemService(AlarmService);

            /*
             * The following sets the Alarm in the specific time by getting the long
             * value of the alarm date time which is in calendar object by calling
             * the getTimeInMillis(). Since Alarm supports only long value , we're
             * using this method.
             */

            alarmManager.SetInexactRepeating(AlarmType.RtcWakeup, 600, 1200, pendingIntent);
            //alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);
            // alarmManager.Set(AlarmType.Rtc, Calendar_Object.TimeInMillis, pendingIntent);
            //alarmManager.SetExact(AlarmType.RtcWakeup, Calendar_Object.TimeInMillis, pendingIntent);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRememberNotificationBase, RememberNotification>();
        }
    }
}
