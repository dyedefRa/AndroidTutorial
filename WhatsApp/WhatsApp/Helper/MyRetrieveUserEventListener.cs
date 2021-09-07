using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DE.Hdodenhof.Circleimageview;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsApp.Activities;

namespace WhatsApp.Helper
{
    public class MyRetrieveUserEventListener : Java.Lang.Object, IValueEventListener
    {
        EditText userName;
        EditText userStatus;
        public CircleImageView userProfileImage;
        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Exists() && snapshot.HasChild(FirebaseClient.UserExistencePropertyStaticName) && snapshot.HasChild(FirebaseClient.UserExistencePropertyStaticImageName))
            {
                string retrieveUserName = snapshot
                    .Child(FirebaseClient.UserExistencePropertyStaticName)
                    .GetValue(true).ToString();
                string retrievesStatus = snapshot
                  .Child("status")
                  .GetValue(true).ToString();
                string retrieveProfileImage = snapshot
                .Child("image")
                .GetValue(true).ToString();

                SettingsActivity settingsActivity = new SettingsActivity();
                //settingsActivity.userName.Text = retrieveUserName;
                //settingsActivity.userStatus.Text = retrievesStatus;
            }
            else if (snapshot.Exists() && snapshot.HasChild(FirebaseClient.UserExistencePropertyStaticName))
            {
                string retrieveUserName = snapshot
                    .Child(FirebaseClient.UserExistencePropertyStaticName)
                    .GetValue(true).ToString();
                string retrievesStatus = snapshot
                  .Child("status")
                  .GetValue(true).ToString();
                string retrieveProfileImage = snapshot
                .Child("image")?
                .GetValue(true)?.ToString();

                //BURASI HATALI  
                SettingsActivity settingsActivity = new SettingsActivity();
                //settingsActivity.userName.Text = retrieveUserName;
                //settingsActivity.userStatus.Text = retrievesStatus;
                userName = settingsActivity.FindViewById<EditText>(Resource.Id.set_user_name);
                userStatus = settingsActivity.FindViewById<EditText>(Resource.Id.set_profile_status);
                userProfileImage = settingsActivity.FindViewById<CircleImageView>(Resource.Id.set_profile_image);
                userName.Text = retrieveUserName;
                userStatus.Text = retrievesStatus;
            }
            else
            {
                Toast.MakeText(Application.Context, "Please Set/Update Your Profile Information...", ToastLength.Short)
                    .Show();
            }
        }
    }
}