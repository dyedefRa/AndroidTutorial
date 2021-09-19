using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void OnClick(View v)
        {
            sendVerificiationCodeButton.Visibility = ViewStates.Invisible;
            inputPhoneNumber.Visibility = ViewStates.Invisible;
            verifyButton.Visibility = ViewStates.Visible;
            inputVerificationCode.Visibility = ViewStates.Visible;
        }
    }
}