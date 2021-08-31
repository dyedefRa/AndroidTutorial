using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsApp.Helper;

namespace WhatsApp.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        private FirebaseAuth mAuth;

        private ProgressDialog loadingBar;

        private Button btnCreateAccount;
        private EditText txtUserEmail, txtUserPassword;
        private TextView txtAlreadyHaveAccountLink;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register_activity);

            mAuth = FirebaseAuth.GetInstance(FirebaseClient.GetApp());


            InitializeFields();
            btnCreateAccount.Click += (s, e) =>
            {
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
                    ProgressBarInitialize();

                    TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
                    taskCompletionListener.Success += TaskCompletionListener_Success;
                    taskCompletionListener.Failure += TaskCompletionListener_Failure;

                    mAuth.CreateUserWithEmailAndPassword(userEmail, userPassword)
                   .AddOnSuccessListener(taskCompletionListener)
                .AddOnFailureListener(taskCompletionListener);

                }
            };
            txtAlreadyHaveAccountLink.Click += TxtAlreadyHaveAccountLink_Click;
        }

        private void TxtAlreadyHaveAccountLink_Click(object sender, EventArgs e)
        {
            SendUserToLoginActivity();
        }

        private void InitializeFields()
        {
            btnCreateAccount = FindViewById<Button>(Resource.Id.register_button);
            txtUserEmail = FindViewById<EditText>(Resource.Id.register_email);
            txtUserPassword = FindViewById<EditText>(Resource.Id.register_password);
            txtAlreadyHaveAccountLink = FindViewById<TextView>(Resource.Id.already_have_account_link);
            loadingBar = new ProgressDialog(this);
        }

        private void ProgressBarInitialize()
        {
            loadingBar.SetTitle(new Java.Lang.String("Creating New Account"));
            loadingBar.SetMessage(new Java.Lang.String("Please wait, while we were creating new account for you..."));
            loadingBar.SetCanceledOnTouchOutside(true);
            loadingBar.Show();
        }

        private void TaskCompletionListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(this, "HATA !", ToastLength.Short)
                     .Show();
            loadingBar.Dismiss();
        }

        private void TaskCompletionListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Account Created Successfully", ToastLength.Short)
                     .Show();
            loadingBar.Dismiss();
            SendUserToLoginActivity();
        }

        private void SendUserToLoginActivity()
        {
            Intent loginIntent = new Intent(this, typeof(LoginActivity));
            StartActivity(loginIntent);

        }
    }
}