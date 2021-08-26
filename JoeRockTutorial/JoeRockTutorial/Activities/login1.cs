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

namespace JoeRockTutorial.Activities
{
    [Activity(Label = "login1", MainLauncher = true)]
    public class login1 : Activity
    {
        private Button btnSignUp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login_1);
            // Create your application here

            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUpEmail);
            btnSignUp.Click += (s, e) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_SignIn signUpDialog = new dialog_SignIn();
                signUpDialog.Show(transaction,"dialog_fragment");
            };
        }

    
    }
}