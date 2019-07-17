using System;
using System.Collections.Generic;
using System.Linq;
using AVFoundation;
using Foundation;
using UIKit;
using UserNotifications;

namespace mobile_examples.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            // Registering notification action categories
            var categories = new UNNotificationCategory[] { RegisterTimeoutCategory()};
            UNUserNotificationCenter.Current.SetNotificationCategories(new NSSet<UNNotificationCategory>(categories));

            HandleLocalNotification(app, options);

            // Background Audio
            var currentSession = AVAudioSession.SharedInstance();
            currentSession.SetCategory(AVAudioSessionCategory.Playback, AVAudioSessionCategoryOptions.MixWithOthers);
            currentSession.SetActive(true);

            return base.FinishedLaunching(app, options);
        }

        private UNNotificationCategory RegisterTimeoutCategory()
        {
            // Create action
            var actionID = "resume";
            var title = "Resume";
            var action = UNNotificationAction.FromIdentifier(actionID, title, UNNotificationActionOptions.None);

            // Create category
            var categoryID = "timeout";
            var actions = new UNNotificationAction[] { action };
            var intentIDs = new string[] { };
            var categoryOptions = new UNNotificationCategoryOptions[] { };
            var category =
                UNNotificationCategory.FromIdentifier(categoryID, actions, intentIDs, UNNotificationCategoryOptions.None);

            return category;
        }

        private void HandleLocalNotification(UIApplication app, NSDictionary options)
        {
            // Use Notification Center after iOS 10
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0) || options == null) return;

            if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
            {
                var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                if (localNotification != null) ReceivedLocalNotification(app, localNotification);
            }
        }
    }
}
