using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UserNotifications;

namespace mobile_examples.iOS
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var app = (App)Xamarin.Forms.Application.Current;

            switch (response.ActionIdentifier)
            {
                case "resume":
                    
                    break;
                default:
                    // Take action based on identifier
                    if (response.IsDefaultAction)
                    {
                        //provide a default action for notifications
                    }
                    else if (response.IsDismissAction)
                    {
                        // Handle dismiss action
                    }

                    break;
            }

            // Inform caller it has been handled
            completionHandler();
        }

        private IDictionary<string, string> GenerateUserInfoDictionary(NSDictionary userInfo)
        {
            var dict = new Dictionary<string, string>();

            if (userInfo == null) return dict;

            foreach (var kvp in userInfo)
            {
                if (kvp.Value == null) continue;

                var key = kvp.Key.ToString();
                var value = kvp.Value.ToString();

                dict[key] = value;
            }

            return dict;
        }
    }
}