using Android.App;
using Android.Content;

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
using static Android.Views.View;

namespace WhatsApp.Activities
{
    [Activity(Label = "PhoneLoginActivity")]
    public class PhoneLoginActivity : Activity, IOnClickListener
    {
        private Button sendVerificiationCodeButton, verifyButton;
        private EditText inputPhoneNumber, inputVerificationCode;




        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.phone_login_activity);

            sendVerificiationCodeButton = FindViewById<Button>(Resource.Id.send_ver_code_button);
            verifyButton = FindViewById<Button>(Resource.Id.verify_button);
            inputPhoneNumber = FindViewById<EditText>(Resource.Id.phone_number_input);
            inputVerificationCode = FindViewById<EditText>(Resource.Id.verification_code_input);

            sendVerificiationCodeButton.SetOnClickListener(this); // => OnClick
        }

        private string mVerificationId;
        private PhoneAuthProvider.ForceResendingToken mResendToken;

        public void OnClick(View v)
        {
            //sendVerificiationCodeButton.Visibility = ViewStates.Invisible;
            //inputPhoneNumber.Visibility = ViewStates.Invisible;
            //verifyButton.Visibility = ViewStates.Visible;
            //inputVerificationCode.Visibility = ViewStates.Visible;

            string phoneNumber = inputPhoneNumber.Text;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                Toast.MakeText(this, "Phone number is required", ToastLength.Short)
                    .Show();
            }
            else
            {



                var timeout = new Java.Lang.Long(60L);
                PhoneAuthOptions options = PhoneAuthOptions.NewBuilder()
                    .SetPhoneNumber(phoneNumber)
                    .SetTimeout(timeout, Java.Util.Concurrent.TimeUnit.Seconds)
                    .SetActivity(this)
                    .SetCallbacks(new PhoneAuthCallbacks())
                    .Build();

                PhoneAuthProvider.VerifyPhoneNumber(options);

                //PhoneAuthProvider.GetInstance(FirebaseClient.GetFirebaseAuth())
                //    .VerifyPhoneNumber(
                //    phoneNumber,
                //    60,
                //    Java.Util.Concurrent.TimeUnit.Seconds,
                //    this,
                //    callbacks)

            }


        }



        private void signInWithPhoneAuthCredential(PhoneAuthCredential credential)
        {

            TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
            taskCompletionListener.Complete += TaskCompletionListener_Complete;

            FirebaseClient.GetFirebaseAuth().SignInWithCredential(credential)
                .AddOnCompleteListener(taskCompletionListener);


        }

        private void TaskCompletionListener_Complete(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        //public class PhoneAuthCallbacks : PhoneAuthProvider.OnVerificationStateChangedCallbacks, View.IOnClickListener
        //{

        //    public override void OnVerificationCompleted(PhoneAuthCredential credential)
        //    {
        //        signInWithPhoneAuthCredential(credential);
        //    }

        //    public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
        //    {
        //        // The SMS verification code has been sent to the provided phone number, we
        //        // now need to ask the user to enter the code and then construct a credential
        //        // by combining the code with a verification ID.
        //        //base.OnCodeSent(verificationId, forceResendingToken);

        //    }

        //    public override void OnVerificationFailed(FirebaseException p0)
        //    {
        //        Toast.MakeText(this, "Invalid Phone Number , Please enter correct phone number with yout country code", ToastLength.Short)
        //          .Show();
        //    }

        //    public void OnClick(View v)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}


