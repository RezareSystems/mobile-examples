using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace mobile_examples.Droid.BroadcastReceivers.NotificationReceivers
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    [IntentFilter(new[] { "actionButton" })] // Should match the name of the intent from the button you wish to receive
    public class BasicBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent != null)
            {
                // Your handler goes here
                var app = (App)Xamarin.Forms.Application.Current;
            }
        }
    }
}