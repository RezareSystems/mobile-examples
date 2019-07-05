using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using mobile_examples.Droid.Services;
using mobileExamples.Services.NotificationService;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationServiceDroid))]
namespace mobile_examples.Droid.Services
{
    public class NotificationServiceDroid : INotificationService
    {
        /// <summary>
        ///     Sends a local notification
        /// </summary>
        /// <param name="content">
        ///     The content of the notification
        /// </param>
        public void Notify(NotificationContent content)
        {
            // Get the current content
            var context = Android.App.Application.Context;

            // If the context cannot be found, don't do anything
            if (context == null)
                return;

            // Get the resource ID for the icon
            var iconId = context.Resources.GetIdentifier(content.IconSource, "drawable", context.PackageName);

            // Get the defaults
            var defaults = GenerateDefaults(content);

            // Generate extras
            var extras = GenerateExtras(content);

            // Create the intent
            var intent = CreateIntent(context, extras);

            // Build the notification
            var builder = new NotificationCompat.Builder(context, "mobile-examples")
                .SetContentIntent(intent)
                .SetContentTitle(content.Title)
                .SetContentText(content.Body)
                .SetSmallIcon(iconId)
                .SetDefaults(defaults)
                .AddExtras(extras)
                .SetAutoCancel(true);

            // Add all the buttons we want for the notification
            //foreach (var button in content.NotificationButtons)
            //{
            //    AddButton(context, builder, iconId, button);
            //}

            var notification = builder.Build();

            // Get the notification manager
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            // Publish the notification
            notificationManager.Notify(content.Id, notification);
        }

        /// <summary>
        /// Adds the resume button onto the notification
        /// </summary>
        /// <param name="context"></param>
        /// <param name="builder"></param>
        /// <param name="iconId"></param>
        /// <param name="button"></param>
        ////private void AddButton(Context context, NotificationCompat.Builder builder, int iconId, BasicButton button)
        ////{
        ////    var buttonIntent = new Intent(button.IntentName);
        ////    var buttonPending = PendingIntent.GetBroadcast(context,
        ////        0, buttonIntent, PendingIntentFlags.UpdateCurrent);

        ////    builder.AddAction(iconId, button.ButtonText, buttonPending);
        ////}

        public void ClearNotification(int notificationID)
        {
            // Get the current content
            var context = Android.App.Application.Context;

            // If the context cannot be found, don't do anything
            if (context == null)
                return;

            // Get the notification manager
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            notificationManager.Cancel(notificationID);
        }

        /// <summary>
        ///     Generates the notification defaults based on the content
        /// </summary>
        /// <param name="content">
        ///     The content of the notification
        /// </param>
        /// <returns>
        ///     Returns the defaults corresponding to <paramref name="content"/>
        /// </returns>
        private int GenerateDefaults(NotificationContent content)
        {
            var defaults = (NotificationDefaults)0;

            if (content.PlaySound)
                defaults |= NotificationDefaults.Sound;

            if (content.Vibrate)
                defaults |= NotificationDefaults.Vibrate;

            return (int)defaults;
        }


        private PendingIntent CreateIntent(Context context, Bundle extras)
        {
            var intent = new Intent(context, typeof(MainActivity));
            intent.PutExtras(extras);

            // We are only using on pending intent, so the ID can be 0
            const int pendingIntentId = 0;
            var pendingIntent = PendingIntent.GetActivity(context,
                pendingIntentId, intent, PendingIntentFlags.OneShot);


            return pendingIntent;
        }

        private Bundle GenerateExtras(NotificationContent content)
        {
            if (!content.HasCustomData()) return Bundle.Empty;

            var extras = new Bundle();

            foreach (var data in content.CustomData)
            {
                extras.PutString(data.Key, data.Value);
            }

            return extras;
        }
    }
}