using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatsApp.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        private FirebaseUser currentUser;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity);
            // Create your application here
        }

        protected override void OnStart()
        {
            base.OnStart();

            if (currentUser != null)
            {
                SendUserToMainActivity();
            }
        }

        private void SendUserToMainActivity()
        {
            Intent sendtoMainIntent = new Intent(this, typeof(MainActivity));
            StartActivity(sendtoMainIntent);
        }
    }
}