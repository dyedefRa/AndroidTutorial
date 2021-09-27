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
using WhatsApp.Activities;

namespace WhatsApp.Helper
{
    public class PhoneAuthCallbacks : PhoneAuthProvider.OnVerificationStateChangedCallbacks
    {
        private PhoneLoginActivity activity;
        public PhoneAuthCallbacks(PhoneLoginActivity _activity)
        {
            activity = _activity;
        }
        public string mVerificationId;
        public PhoneAuthProvider.ForceResendingToken mResendToken;

        public override void OnVerificationCompleted(PhoneAuthCredential p0)
        {
            signInWithPhoneAuthCredential(p0);
        }

        public override void OnVerificationFailed(FirebaseException p0)
        {
            activity.loadingBar.Dismiss();

            Toast.MakeText(activity, "Invalid Phone Number , Please enter correct phone number with yout country code", ToastLength.Short)
                 .Show();


            activity.sendVerificiationCodeButton.Visibility = ViewStates.Visible;
            activity.inputPhoneNumber.Visibility = ViewStates.Visible;
            activity.verifyButton.Visibility = ViewStates.Invisible;
            activity.inputVerificationCode.Visibility = ViewStates.Invisible;
        }

        public override void OnCodeSent(string p0, PhoneAuthProvider.ForceResendingToken p1)
        {
            mVerificationId = p0;
            mResendToken = p1;

            activity.loadingBar.Dismiss();

            activity.sendVerificiationCodeButton.Visibility = ViewStates.Invisible;
            activity.inputPhoneNumber.Visibility = ViewStates.Invisible;
            activity.verifyButton.Visibility = ViewStates.Visible;
            activity.inputVerificationCode.Visibility = ViewStates.Visible;

            Toast.MakeText(activity, "Code has been sent", ToastLength.Short)
               .Show();

            base.OnCodeSent(p0, p1);
        }

        public void signInWithPhoneAuthCredential(PhoneAuthCredential credential)
        {
            TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
            taskCompletionListener.Failure += TaskCompletionListener_Failure;
            taskCompletionListener.Complete += TaskCompletionListener_Complete;

            FirebaseClient.GetFirebaseAuth().SignInWithCredential(credential)
                .AddOnCompleteListener(taskCompletionListener)
                .AddOnFailureListener(taskCompletionListener);

        }

        private void TaskCompletionListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(activity, "ERROR!", ToastLength.Short)
                .Show();
        }

        private void TaskCompletionListener_Complete(object sender, EventArgs e)
        {
            activity.loadingBar.Dismiss();
            Toast.MakeText(activity, "Congratulations , you are logged in successfully...", ToastLength.Short)
                .Show();

            activity.SendUserToMainActivity();
        }


    }
}
