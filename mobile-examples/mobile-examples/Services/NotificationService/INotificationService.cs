using System;
using System.Collections.Generic;
using System.Text;

namespace mobileExamples.Services.NotificationService
{
    public interface INotificationService
    {
        /// <summary>
        ///     Sends a local notification
        /// </summary>
        /// <param name="content">
        ///     The content of the notification
        /// </param>
        void Notify(NotificationContent content);

        void ClearNotification(int notificationID);
    }
}
