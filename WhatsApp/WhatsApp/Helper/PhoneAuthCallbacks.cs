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

        private string mVerificationId;
        private PhoneAuthProvider.ForceResendingToken mResendToken;

        public override void OnVerificationCompleted(PhoneAuthCredential p0)
        {
            signInWithPhoneAuthCredential(p0);
        }

        public override void OnVerificationFailed(FirebaseException p0)
        {
            Toast.MakeText(new PhoneLoginActivity(), "Invalid Phone Number , Please enter correct phone number with yout country code", ToastLength.Short)
                 .Show();
        }
        public override void OnCodeSent(string p0, PhoneAuthProvider.ForceResendingToken p1)
        {
            mVerificationId = p0;
            mResendToken = p1;

            Toast.MakeText(new PhoneLoginActivity(), "Code has been sent", ToastLength.Short)
               .Show();

            base.OnCodeSent(p0, p1);
        }





     
    }
}