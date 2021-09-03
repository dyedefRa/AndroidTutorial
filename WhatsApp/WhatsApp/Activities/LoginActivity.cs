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
using WhatsApp.Helper;

namespace WhatsApp.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        //private FirebaseUser currentUser;
        private FirebaseAuth mAuth;

        private ProgressDialog loadingBar;

        private Button btnLogin, btnPhoneLogin;
        private EditText txtUserEmail, txtUserPassword;
        private TextView txtNeedNewAccountLink, txtForgetPasswordLink;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity);

            mAuth = FirebaseAuth.GetInstance(FirebaseClient.GetFirebaseApp());
            //currentUser = mAuth.CurrentUser;

            InitializeFields();

            txtNeedNewAccountLink.Click += TxtNeedNewAccountLink_Click;
            btnLogin.Click += BtnLogin_Click;
        }

        private void InitializeFields()
        {
            btnLogin = FindViewById<Button>(Resource.Id.login_button);
            btnPhoneLogin = FindViewById<Button>(Resource.Id.phone_login_button);
            txtUserEmail = FindViewById<EditText>(Resource.Id.login_email);
            txtUserPassword = FindViewById<EditText>(Resource.Id.login_password);
            txtNeedNewAccountLink = FindViewById<TextView>(Resource.Id.need_new_account_link);
            txtForgetPasswordLink = FindViewById<TextView>(Resource.Id.forget_password_link);
            loadingBar = new ProgressDialog(this);
        }

        private void ProgressBarInitialize()
        {
            loadingBar.SetTitle(new Java.Lang.String("Sign In"));
            loadingBar.SetMessage(new Java.Lang.String("Please wait..."));
            loadingBar.SetCanceledOnTouchOutside(true);
            loadingBar.Show();
        }

        protected override void OnStart()
        {
            base.OnStart();

            //if (currentUser != null)
            //    SendUserToMainActivity();
        }


        private void SendUserToMainActivity()
        {
            Intent mainIntent = new Intent(this, typeof(MainActivity));
            mainIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(mainIntent);
            Finish();
        }

        private void TxtNeedNewAccountLink_Click(object sender, EventArgs e)
        {
            SendUserToRegisterActivity();
        }

        private void SendUserToRegisterActivity()
        {
            Intent registerIntent = new Intent(this, typeof(RegisterActivity));
            StartActivity(registerIntent);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            AllowUserToLogin();
        }

        private void AllowUserToLogin()
        {
            ProgressBarInitialize();

            string userEmail = txtUserEmail.Text;
            string userPassword = txtUserPassword.Text;

            if (string.IsNullOrEmpty(userEmail))
                Toast.MakeText(this, "Please enter email.", ToastLength.Short)
                .Show();
            else if (string.IsNullOrEmpty(userEmail))
                Toast.MakeText(this, "Please enter password.", ToastLength.Short)
                .Show();
            else
            {
                //    ProgressBarInitialize();

                TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
                taskCompletionListener.Success += TaskCompletionListener_Success; taskCompletionListener.Failure += TaskCompletionListener_Failure;
                mAuth.SignInWithEmailAndPassword(userEmail, userPassword)
                   .AddOnSuccessListener(taskCompletionListener)
                .AddOnFailureListener(taskCompletionListener);

            }
        }

        private void TaskCompletionListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Logged in Successfull", ToastLength.Short)
               .Show();
            loadingBar.Dismiss();
            SendUserToMainActivity();
        }

        private void TaskCompletionListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(this, "ERROR, Logged in NOT Successfull", ToastLength.Short)
             .Show();
            loadingBar.Dismiss();
        }


    }
}