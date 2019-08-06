using System;
using System.Collections.Generic;
using System.Text;

namespace mobileExamples.Services.NotificationService
{
    /// <summary>
    /// Class to pass to native code to describe the notification
    /// </summary>
    public class NotificationContent
    {
        /// <summary>
        ///     Creates a new <see cref="NotificationContent"/>
        /// </summary>
        public NotificationContent()
        {
            CustomData = new Dictionary<string, string>();
        }

        /// <summary>
        ///     Gets or sets the ID of the notification
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the title of the notification
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the body of the notification
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the device is 
        ///     vibrated when the notification is delivered
        /// </summary>
        public bool Vibrate { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether a sound is 
        ///     played when the notification is delivered
        /// </summary>
        public bool PlaySound { get; set; }

        /// <summary>
        ///     Gets or sets the source icon shown in the notification
        /// </summary>
        /// <remarks>
        ///     Don't include the file extension for this, 
        ///     eg. icon_error_grey NOT icon_error_grey.png
        /// </remarks>
        public string IconSource { get; set; }

        /// <summary>
        ///     Gets or sets custom data to be added to the notification
        /// </summary>
        public IDictionary<string, string> CustomData { get; set; }

        /// <summary>
        ///     Indicates whether this notification has custom data
        /// </summary>
        /// <returns>
        ///     Returns true if this notification has custom data, false otherwise
        /// </returns>
        public bool HasCustomData()
        {
            return (CustomData != null && CustomData.Count > 0);
        }

        /// <summary>
        /// the name of the action category for use on iOS
        /// </summary>
        public string iOSCategory;
    }
}
