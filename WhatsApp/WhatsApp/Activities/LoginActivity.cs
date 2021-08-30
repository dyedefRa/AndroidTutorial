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
        private Button btnLogin, btnPhoneLogin;
        private EditText txtUserEmail, txtUserPassword;
        private TextView txtNeedNewAccountLink, txtForgetPasswordLink;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity);
            // Create your application here
            InitializeFields();
            txtNeedNewAccountLink.Click += (s, e) =>
            {
                Intent registerIntent = new Intent(this, typeof(RegisterActivity));
                StartActivity(registerIntent);
            };
        }

        private void InitializeFields()
        {
            btnLogin = FindViewById<Button>(Resource.Id.login_button);
            btnPhoneLogin = FindViewById<Button>(Resource.Id.phone_login_button);
            txtUserEmail = FindViewById<EditText>(Resource.Id.login_email);
            txtUserPassword = FindViewById<EditText>(Resource.Id.login_password);
            txtNeedNewAccountLink = FindViewById<TextView>(Resource.Id.need_new_account_link);
            txtForgetPasswordLink = FindViewById<TextView>(Resource.Id.forget_password_link);
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