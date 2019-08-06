using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AudioToolbox;
using Foundation;
using mobile_examples.iOS.Services;
using mobileExamples.Services.NotificationService;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationServiceIOS))]
namespace mobile_examples.iOS.Services
{
    public class NotificationServiceIOS : INotificationService
    {
        /// <summary>
        ///     Sends a local notification
        /// </summary>
        /// <param name="content">
        ///     The content of the notification
        /// </param>
        public void Notify(NotificationContent content)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                NotifyVersion10(content);
            else
                NotifyVersion9(content);
        }

        public void ClearNotification(int notificationID)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                ClearNotification10(notificationID);
            else
                ClearNotification9(notificationID);
        }

        /// <summary>
        ///     Sends a local notification for iOS v9 and below
        /// </summary>
        /// <param name="content">
        ///     The content to place into the notification
        /// </param>
        private void NotifyVersion9(NotificationContent content)
        {
            UILocalNotification notification = new UILocalNotification()
            {
                FireDate = NSDate.FromTimeIntervalSinceNow(1),
                AlertTitle = content.Title,
                AlertAction = content.Title,
                AlertBody = content.Body,
                UserInfo = GenerateUserInfo(content),
                Category = content.iOSCategory,
            };

            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }

        /// <summary>
        ///     Sends a local notification for iOS v10 and above
        /// </summary>
        /// <param name="content">
        ///     The content to put into the notification
        /// </param>
        private void NotifyVersion10(NotificationContent content)
        {
            // Generate the iOS notification content based on the passed in content
            var notificationContent = new UNMutableNotificationContent
            {
                Title = content.Title,
                Body = content.Body,
                UserInfo = GenerateUserInfo(content),
                CategoryIdentifier = content.iOSCategory
            };

            notificationContent.Sound = UNNotificationSound.GetDefaultCriticalSound(10);

            // Create the trigger to send the notification
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(1, false);

            // Create the request
            var request = UNNotificationRequest.FromIdentifier(content.Id.ToString(), notificationContent, trigger);

            // Send the notification request
            UNUserNotificationCenter.Current.AddNotificationRequest(request, err =>
            {
                if (err != null)
                {
                    Debug.WriteLine("An error occurred while sending iOS notification");
                }
            });
        }

        private void ClearNotification10(int notificationId)
        {
            UNUserNotificationCenter.Current.RemoveDeliveredNotifications(new string[] { notificationId.ToString() });
            //UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { notificationId.ToString() });
        }

        private void ClearNotification9(int notificationId)
        {

        }

        private NSDictionary<NSString, NSString> GenerateUserInfo(NotificationContent content)
        {
            if (!content.HasCustomData()) new NSDictionary<NSString, NSString>();

            var keys = new NSString[content.CustomData.Count];
            var values = new NSString[content.CustomData.Count];
            var index = 0;

            foreach (var data in content.CustomData)
            {
                keys[index] = new NSString(data.Key);
                values[index] = new NSString(data.Value);

                index++;
            }

            return new NSDictionary<NSString, NSString>(keys, values);
        }
    }
}