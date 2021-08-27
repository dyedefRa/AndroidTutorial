using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JoeRockTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JoeRockTutorial.Activities
{
    [Activity(Label = "login1", MainLauncher = true)]
    public class login1 : Activity
    {
        private Button btnSignUp;
        private ProgressBar progressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login_1);
            // Create your application here

            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUpEmail);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            btnSignUp.Click += (s, e) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_SignIn signUpDialog = new dialog_SignIn();
                signUpDialog.Show(transaction, "dialog_fragment");

                signUpDialog.mOnSignUpComplete += SignUpDialog_mOnSignUpComplete;
            };
        }

        private void SignUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();
           

        }
        private void ActLikeARequest()
        {
            Thread.Sleep(3000);
            RunOnUiThread(() => { progressBar.Visibility = ViewStates.Invisible; });
        }
    }
}