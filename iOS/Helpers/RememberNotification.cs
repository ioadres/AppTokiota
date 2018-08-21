using System;
using Acr.UserDialogs;
using AppTokiota.iOS.Helpers;
using AppTokiota.Users.OS;
using Foundation;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(RememberNotification))]
namespace AppTokiota.iOS.Helpers
{
    public class RememberNotification : IRememberNotificationBase
    {
        public static string CreateRememberNotification = "CreateRememberNotification";
        public static string DestroyRememberNotification = "DestroyRememberNotification";

        public static void Init() {
            (new RememberNotification()).CreateNotification();
        }

        public void CreateNotification() {
            this.EmitRemoveRememberNotification();
            // Watch for notifications while the app is active
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
        }

        public void RemoveBadgeRememberNotification() {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }

        public void EmitCreateRememberNotification() {
            
            // Request notification permissions from the user
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (approved, err) =>
            {
                if (approved)
                {
                    // Rebuild notification
                    var content = new UNMutableNotificationContent();
                    content.Title = "Tokiota: Timesheet";
                    content.Body = "Remember input your timesheet ;)";
                    content.Badge = 1;

                    var date = new NSDateComponents();
                    date.Hour = 11;
                    date.Minute = 05;
                    var trigger = UNCalendarNotificationTrigger.CreateTrigger(date, true);

                    // ID of Notification to be updated
                    var requestID = "RememberNotification";
                    var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                    // Add to system to modify existing Notification
                    UNUserNotificationCenter.Current.AddNotificationRequest(request, (error) => {
                        if (error != null)
                        {
                            // Do something with error...
                        }
                    });
                }  else if(AppSettings.IsEnableNotification) {
                    
                    UserDialogs.Instance.AlertAsync("Please enable the notifications in your settings", "iOS Settings - Notification - Tokiota App", "Ok");
                }
            });
        }

        public void EmitRemoveRememberNotification() {
            String[] request = new String[1];
            request[0] = "RememberNotification";
            UNUserNotificationCenter.Current.RemovePendingNotificationRequests(request);
            Console.WriteLine("Destroy Notification");
        }


    }
}
