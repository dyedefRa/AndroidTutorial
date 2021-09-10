using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DE.Hdodenhof.Circleimageview;
using Firebase.Database;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsApp.Helper;

namespace WhatsApp.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity, IValueEventListener
    {
        private Button updateAccountSettings;
        private EditText userName, userStatus;
        private CircleImageView userProfileImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.settings_activity);

            InitializeFields();
            userName.Visibility = ViewStates.Invisible;
            updateAccountSettings.Click += UpdateAccountSettings_Click;

            RetrieveUserInformation();
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
                var currentUserId = FirebaseClient.GetCurrentUser().Uid;
                HashMap profileMap = new HashMap();
                profileMap.Put(FirebaseClient.UserStaticUID, currentUserId);
                profileMap.Put(FirebaseClient.UserStaticName, setUserName);
                profileMap.Put(FirebaseClient.UserStaticStatusName, setUserStatus);

                TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
                taskCompletionListener.Success += TaskCompletionListener_Success;
                taskCompletionListener.Failure += TaskCompletionListener_Failure;

                FirebaseClient.GetDatabaseReference()
                    .Child("Users")
                    .Child(currentUserId)
                    .SetValue(profileMap)
                    .AddOnSuccessListener(taskCompletionListener)
                    .AddOnFailureListener(taskCompletionListener);
            }
        }

        private void TaskCompletionListener_Success(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Profile Updated Successfully", ToastLength.Short);
            SendUserToMainActivity();
        }

        private void TaskCompletionListener_Failure(object sender, EventArgs e)
        {
            Toast.MakeText(this, "ERROR !", ToastLength.Short);
        }

        private void SendUserToMainActivity()
        {
            Intent mainIntent = new Intent(this, typeof(MainActivity));
            mainIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(mainIntent);
            Finish();
        }

        private void RetrieveUserInformation()
        {

            var currentUserId = FirebaseClient.GetCurrentUser().Uid;
            FirebaseClient.GetDatabaseReference()
                .Child("Users")
                .Child(currentUserId)
                .AddValueEventListener(this);
        }

        //AddValueEventListener ONCHANGE BURAYA DUSUYOR
        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Exists() && snapshot.HasChild(FirebaseClient.UserStaticName) && snapshot.HasChild(FirebaseClient.UserStaticImageName))
            {
                string retrieveUserName = snapshot
                    .Child(FirebaseClient.UserStaticName)
                    .GetValue(true).ToString();
                string retrievesStatus = snapshot
                  .Child(FirebaseClient.UserStaticStatusName)
                  .GetValue(true).ToString();
                string retrieveProfileImage = snapshot
                .Child(FirebaseClient.UserStaticImageName)
                .GetValue(true).ToString();

                userName.Text = retrieveUserName;
                userStatus.Text = retrievesStatus;

            }
            else if (snapshot.Exists() && snapshot.HasChild(FirebaseClient.UserStaticName))
            {
                string retrieveUserName = snapshot
                    .Child(FirebaseClient.UserStaticName)
                    .GetValue(true).ToString();
                string retrievesStatus = snapshot
                  .Child(FirebaseClient.UserStaticStatusName)
                  .GetValue(true).ToString();
                string retrieveProfileImage = snapshot
                .Child(FirebaseClient.UserStaticImageName)?
                .GetValue(true)?.ToString();

                userName.Text = retrieveUserName;
                userStatus.Text = retrievesStatus;
            }
            else
            {
                userName.Visibility = ViewStates.Visible;
                Toast.MakeText(Application.Context, "Please Set/Update Your Profile Information...", ToastLength.Short)
                    .Show();
            }
        }

        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

      
    }
}