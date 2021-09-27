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
        public Button sendVerificiationCodeButton, verifyButton;
        public EditText inputPhoneNumber, inputVerificationCode;
        public ProgressDialog loadingBar;

        private string mVerificationId;
        private PhoneAuthProvider.ForceResendingToken mResendToken;


        //BURADA HATA VAR 

        //https://www.youtube.com/watch?v=o6rpBldK7vM&list=PLxefhmF0pcPmtdoud8f64EpgapkclCllj&index=26&ab_channel=CodingCafe

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.phone_login_activity);

            sendVerificiationCodeButton = FindViewById<Button>(Resource.Id.send_ver_code_button);
            verifyButton = FindViewById<Button>(Resource.Id.verify_button);
            inputPhoneNumber = FindViewById<EditText>(Resource.Id.phone_number_input);
            inputVerificationCode = FindViewById<EditText>(Resource.Id.verification_code_input);
            loadingBar = new ProgressDialog(this);
            sendVerificiationCodeButton.SetOnClickListener(this); // => OnClick

            verifyButton.Click += VerifyButton_Click;

        }

        private void VerifyButton_Click(object sender, EventArgs e)
        {
            sendVerificiationCodeButton.Visibility = ViewStates.Invisible;
            inputPhoneNumber.Visibility = ViewStates.Invisible;

            string verificationCode = inputVerificationCode.Text;

            if (string.IsNullOrEmpty(verificationCode))
            {
                Toast.MakeText(this, "Please write verification code first...", ToastLength.Short)
                    .Show();
            }
            else
            {
                loadingBar.SetTitle("Code Verification");
                loadingBar.SetMessage("Please wait , while we are verifying verification code...");
                loadingBar.SetCanceledOnTouchOutside(false);
                loadingBar.Show();
                PhoneAuthCredential credential = PhoneAuthProvider.GetCredential(mVerificationId, verificationCode);
                //PhoneAuthCallbackss sgag = new PhoneAuthCallbackss();
                PhoneAuthCallbacks sgag = new PhoneAuthCallbacks(this);

                sgag.signInWithPhoneAuthCredential(credential);
            }
        }


        public void OnClick(View v)
        {
            sendVerificiationCodeButton.Visibility = ViewStates.Invisible;
            inputPhoneNumber.Visibility = ViewStates.Invisible;
            verifyButton.Visibility = ViewStates.Visible;
            inputVerificationCode.Visibility = ViewStates.Visible;

            string phoneNumber = inputPhoneNumber.Text;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                Toast.MakeText(this, "Please enter your phone number first", ToastLength.Short)
                    .Show();
            }
            else
            {


                var timeout = new Java.Lang.Long(60L);
                PhoneAuthOptions options = PhoneAuthOptions.NewBuilder()
                    .SetPhoneNumber(phoneNumber)
                    .SetTimeout(timeout, Java.Util.Concurrent.TimeUnit.Seconds)
                    .SetActivity(this)
                    .SetCallbacks(new PhoneAuthCallbacks(this))
                    .Build();

                PhoneAuthProvider.VerifyPhoneNumber(options);

            }

        }

        public void SendUserToMainActivity()
        {
            Intent mainIntent = new Intent(this, typeof(MainActivity));
            StartActivity(mainIntent);
            Finish();
        }

        //public class PhoneAuthCallbackss : PhoneAuthProvider.OnVerificationStateChangedCallbacks
        //{
        //    private string mVerificationId;
        //    private PhoneAuthProvider.ForceResendingToken mResendToken;

        //    public override void OnVerificationCompleted(PhoneAuthCredential p0)
        //    {
        //        signInWithPhoneAuthCredential(p0);
        //    }

        //    public override void OnVerificationFailed(FirebaseException p0)
        //    {
        //        Toast.MakeText(new PhoneLoginActivity(), "Invalid Phone Number , Please enter correct phone number with yout country code", ToastLength.Short)
        //             .Show();
        //    }

        //    public override void OnCodeSent(string p0, PhoneAuthProvider.ForceResendingToken p1)
        //    {
        //        mVerificationId = p0;
        //        mResendToken = p1;
        //        PhoneLoginActivity phoneLoginActivity = new PhoneLoginActivity();
        //        phoneLoginActivity.sendVerificiationCodeButton.Visibility = ViewStates.Invisible;
        //        //sendVerificiationCodeButton.Visibility = ViewStates.Invisible;
        //        Toast.MakeText(new PhoneLoginActivity(), "Code has been sent", ToastLength.Short)
        //           .Show();

        //        base.OnCodeSent(p0, p1);
        //    }

        //    public void signInWithPhoneAuthCredential(PhoneAuthCredential credential)
        //    {
        //        TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
        //        taskCompletionListener.Complete += TaskCompletionListener_Complete;

        //        FirebaseClient.GetFirebaseAuth().SignInWithCredential(credential)
        //            .AddOnCompleteListener(taskCompletionListener);
        //    }

        //    private void TaskCompletionListener_Complete(object sender, EventArgs e)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}


