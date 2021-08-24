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

namespace AndroidPhoneCallingListeningApp.Extend
{
    [Activity(Label = "MonitoringThePhone")]
    public class Activity1 : Activity
    {
        TextView txtOutput;
        TelephonyManager telephonyManager;
        PhoneMonitor listener;
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.layout1);
            txtOutput = FindViewById<TextView>(Resource.Id.txtOutput2);
            telephonyManager = (TelephonyManager)GetSystemService(Context.TelephonyService);
            listener = new PhoneMonitor();
            listener.CallStateChanged += Listener_CallStateChanged;
            telephonyManager.Listen(listener, PhoneStateListenerFlags.CallState);
        }

        void Listener_CallStateChanged(object sender, EventArgs e)
        {
            txtOutput.Text += System.Environment.NewLine + ((CallStateChangedArgs)e).Message + System.Environment.NewLine;
        }

        public class PhoneMonitor : PhoneStateListener
        {

            public override void OnCallStateChanged([GeneratedEnum] CallState state, string phoneNumber)
            {
                base.OnCallStateChanged(state, phoneNumber);

                switch (state)
                {
                    case CallState.Ringing:
                        RaiseCallStateChanged("Ringing");
                        break;
                    case CallState.Idle:
                        RaiseCallStateChanged("Idle");
                        break;
                    case CallState.Offhook:
                        RaiseCallStateChanged("Offhook");
                        break;
                }
            }
            public event System.EventHandler CallStateChanged = null;

            private void RaiseCallStateChanged(string message)
            {
                if (CallStateChanged != null)
                {
                    CallStateChangedArgs cse = new CallStateChangedArgs(message);
                    CallStateChanged(this, cse);
                }
            }
        }

        public class CallStateChangedArgs : EventArgs
        {
            public CallStateChangedArgs(string message)
            {
                this.Message = message;
            }
            public string Message { get; set; }

        }
    }
}