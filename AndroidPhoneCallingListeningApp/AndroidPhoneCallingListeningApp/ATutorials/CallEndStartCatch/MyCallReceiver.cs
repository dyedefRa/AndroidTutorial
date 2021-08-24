using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidPhoneCallingListeningApp.ATutorials.CallEndStartCatch
{
    public class MyCallReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.GetStringExtra(TelephonyManager.ExtraState).Equals(TelephonyManager.ExtraStateOffhook))
            {
                ShowToast(context, "Calling Started...");
            }
            else if (intent.GetStringExtra(TelephonyManager.ExtraState).Equals(TelephonyManager.ExtraState))
            {
                ShowToast(context, "Calling Ended...");

            }
            else if (intent.GetStringExtra(TelephonyManager.ExtraState).Equals(TelephonyManager.ExtraStateRinging))
            {
                ShowToast(context, "Ringing...");

            }
            else if (intent.GetStringExtra(TelephonyManager.ExtraState).Equals(TelephonyManager.ExtraStateIdle))
            {
                ShowToast(context, "ExtraStateIdle...");

            }

        }

        public void ShowToast(Context context, string message)
        {
            Toast toast = Toast.MakeText(context, message, ToastLength.Short);
            toast.SetGravity(GravityFlags.Center, 0, 0);
            toast.Show();
        }
    }
}