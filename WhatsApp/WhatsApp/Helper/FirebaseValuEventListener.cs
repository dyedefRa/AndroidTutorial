using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WhatsApp.Helper
{
    public class FirebaseValuEventListener : Java.Lang.Object, IValueEventListener
    {
        public FirebaseValuEventListener()
        {
            //var ss = FirebaseClient.GetFirebaseApp();
            //FirebaseClient.GetDatabaseReference(
            //FirebaseDatabase.GetInstance(ss).
        }
        public event EventHandler Cancelled;
        public event EventHandler DataChanged;

        public void OnDataChange(DataSnapshot snapshot)
        {
            DataChanged?.Invoke(this, new EventArgs());
        }

        public void OnCancelled(DatabaseError error)
        {
            Cancelled?.Invoke(this, new EventArgs());
        }

    }
}