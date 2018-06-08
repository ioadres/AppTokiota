using System;
using AppTokiota.iOS.Helpers;
using AppTokiota.Users.OS;
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

            // Request notification permissions from the user
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (approved, err) => {
               
            });

            // Watch for notifications while the app is active
            UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

        }

        public void EmitCreateRememberNotification() {

            Console.WriteLine("Create Notification");
            // Rebuild notification
            var content = new UNMutableNotificationContent();
            content.Title = "Tokiota: Timesheet";
            content.Body = "Remember input your timesheet ;)";

            // New trigger time
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(60 * 60 * 8, true);

            // ID of Notification to be updated
            var requestID = "RememberNotification";
            var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

            // Add to system to modify existing Notification
            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                if (err != null)
                {
                    // Do something with error...
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
