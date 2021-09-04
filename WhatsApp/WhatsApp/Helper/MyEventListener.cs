using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsApp.Activities;

namespace WhatsApp.Helper
{
    public class MyEventListener : Java.Lang.Object, IValueEventListener
    {       
        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Child("name").Exists())
                Toast.MakeText(Application.Context, "Welcome", ToastLength.Short)
                    .Show();
            else
                SendUserToSettingsActivity();
        }

        private void SendUserToSettingsActivity()
        {
            Intent settingsIntent = new Intent(Application.Context, typeof(SettingsActivity));
            settingsIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            Application.Context.StartActivity(settingsIntent);
            //Activity.Finish();
        }

        public IntPtr Handle => throw new NotImplementedException();

        public int JniIdentityHashCode => throw new NotImplementedException();

        public Java.Interop.JniObjectReference PeerReference;

        public JniPeerMembers JniPeerMembers => throw new NotImplementedException();

        public JniManagedPeerStates JniManagedPeerState => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Disposed()
        {
            throw new NotImplementedException();
        }

        public void DisposeUnlessReferenced()
        {
            throw new NotImplementedException();
        }

        public void Finalized()
        {
            throw new NotImplementedException();
        }

        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }


        public void SetJniIdentityHashCode(int value)
        {
           
        }

        public void SetJniManagedPeerState(JniManagedPeerStates value)
        {
            throw new NotImplementedException();
        }

        public void SetPeerReference(JniObjectReference reference)
        {
            throw new NotImplementedException();
        }

        public void UnregisterFromRuntime()
        {
            throw new NotImplementedException();
        }
    }
}