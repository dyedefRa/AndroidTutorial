using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DE.Hdodenhof.Circleimageview;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatsApp.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {

        private Button updateAccountSettings;
        private EditText userName, userStatus;
        private CircleImageView userProfileImage;
        private string currentUserId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.settings_activity);

            InitializeFields();

            updateAccountSettings.Click += UpdateAccountSettings_Click;
        }

        private void InitializeFields()
        {
            updateAccountSettings = FindViewById<Button>(Resource.Id.update_settings_button);
            userName = FindViewById<EditText>(Resource.Id.set_user_name);
            userStatus = FindViewById<EditText>(Resource.Id.set_profile_status);
            userProfileImage = FindViewById<CircleImageView>(Resource.Id.set_profile_image);
        }

        private void UpdateAccountSettings_Click(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            string setUserName = userName.Text;
            string setUserStatus = userStatus.Text;

            if (string.IsNullOrEmpty(setUserName))
            {
                Toast.MakeText(this, "Please write your user name first...", ToastLength.Short);
            }
            else if (string.IsNullOrEmpty(setUserStatus))
            {
                Toast.MakeText(this, "Please write your user status...", ToastLength.Short);
            }
            else
            {
            //    HashMap gsag = new HashMap();
            //    gsag.Put()
            //    Java.Util.HashMap<string, string> profileMap = new Java.Util.HashMap<string, string>();
            }
        }


    }
}