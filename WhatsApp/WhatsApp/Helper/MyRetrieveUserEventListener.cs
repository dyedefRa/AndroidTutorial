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
        //https://stackoverflow.com/questions/10996479/how-to-update-a-textview-of-an-activity-from-another-class

        Context context;
        EditText userName;
        EditText userStatus;
        public CircleImageView userProfileImage;
        public MyRetrieveUserEventListener(Context _context)
        {
            //BURASI   
            context = _context;
            //_context
            //userName = ((Activity)context).FindViewById<EditText>(Resource.Id.set_user_name);
            //userStatus = ((Activity)context).FindViewById<EditText>(Resource.Id.set_profile_status);
            //userProfileImage = ((Activity)context).FindViewById<CircleImageView>(Resource.Id.set_profile_image);
        }

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

                userName.Text = retrieveUserName;
                userStatus.Text= retrievesStatus;

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