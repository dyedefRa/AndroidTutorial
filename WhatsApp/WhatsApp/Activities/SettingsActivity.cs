using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Theartofdev.Edmodo.Cropper;
using DE.Hdodenhof.Circleimageview;
using Firebase.Database;
using Firebase.Storage;
using Java.Util;
using Square.Picasso;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WhatsApp.Helper;

namespace WhatsApp.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity, IValueEventListener, IOnSuccessListener, IOnCompleteListener
    {
        private Button updateAccountSettings;
        private EditText userName, userStatus;
        private CircleImageView userProfileImage;
        private ProgressDialog loadingBar;

        private const int GalleryPick = 1;

        string currentUserId;
        StorageReference userProfileImageRef;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.settings_activity);

            InitializeFields();
            currentUserId = FirebaseClient.GetCurrentUser().Uid;
            userProfileImageRef = FirebaseClient.GetFirebaseStorageReferenceWithChildName("Profile Images");

            userName.Visibility = ViewStates.Invisible;
            updateAccountSettings.Click += UpdateAccountSettings_Click;
            RetrieveUserInformation();
            userProfileImage.Click += UserProfileImage_Click;
        }

        private void InitializeFields()
        {
            updateAccountSettings = FindViewById<Button>(Resource.Id.update_settings_button);
            userName = FindViewById<EditText>(Resource.Id.set_user_name);
            userStatus = FindViewById<EditText>(Resource.Id.set_profile_status);
            userProfileImage = FindViewById<CircleImageView>(Resource.Id.set_profile_image);
            loadingBar = new ProgressDialog(this);
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
                HashMap profileMap = new HashMap();
                profileMap.Put(FirebaseClient.UserStaticUID, currentUserId);
                profileMap.Put(FirebaseClient.UserStaticName, setUserName);
                profileMap.Put(FirebaseClient.UserStaticStatusName, setUserStatus);

                TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
                taskCompletionListener.Success += TaskCompletionListener_Success;
                taskCompletionListener.Failure += TaskCompletionListener_Failure;

                FirebaseClient.GetDatabaseReference()
                    .Child(FirebaseClient.UsersChildStaticName)
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

        private void UserProfileImage_Click(object sender, EventArgs e)
        {
            //galeriyi aç.
            Intent galleryIntent = new Intent();
            galleryIntent.SetAction(Intent.ActionGetContent);
            galleryIntent.SetType("image/*");
            StartActivityForResult(galleryIntent, GalleryPick); //1     
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == GalleryPick && resultCode == Result.Ok && data != null)
            {
                var imagerUri = data.Data;
                //Kesme işlemi ve 3.part library.
                CropImage.Activity(imagerUri)
                    .SetGuidelines(CropImageView.Guidelines.On)
                    .SetAspectRatio(1, 1)
                    .Start(this);
            }
            if (requestCode == CropImage.CropImageActivityRequestCode)
            {
                CropImage.ActivityResult result = CropImage.GetActivityResult(data);

                //Burdan sonra kırpılan resim işlemleri. Firebase e kaydedelim.
                if (resultCode == Result.Ok)
                {
                    loadingBar.SetTitle("Set Profile Image");
                    loadingBar.SetMessage("Please wait, your profile image is updating...");
                    loadingBar.SetCanceledOnTouchOutside(false);
                    loadingBar.Show();

                    var resultUri = result.Uri;
                    var currentUserId = FirebaseClient.GetCurrentUser().Uid;
                    //Kaydetme işlemi.
                    StorageReference filePath = userProfileImageRef.Child(currentUserId + ".jpg");
                    filePath.PutFile(resultUri).AddOnSuccessListener(this); // => OnSuccess                
                }
            }
        }

        //Resmi kullanıya eşleştirme
        public void OnSuccess(Java.Lang.Object result)
        {
            Toast.MakeText(this, "Profile Image uploaded successfully...", ToastLength.Short)
               .Show();
            var downloadUrl = result.ToString();

            FirebaseClient.GetDatabaseReference()
                .Child("Users")
                .Child(currentUserId)
                .Child("image")
                .SetValue(downloadUrl)
                .AddOnCompleteListener(this); // OnComplete
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Image save in Database , successfully", ToastLength.Short)
                  .Show();
                loadingBar.Dismiss();
            }
            else
            {
                string errorMessage = task.Exception.ToString();
                Toast.MakeText(this, "ERROR! " + errorMessage, ToastLength.Short)
                 .Show();
                loadingBar.Dismiss();
            }
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
            FirebaseClient.GetDatabaseReference()
                .Child(FirebaseClient.UsersChildStaticName)
                .Child(currentUserId)
                .AddValueEventListener(this); //=> OnDataChange
        }

        //RetrieveUserInformation > AddValueEventListener ONCHANGE BURAYA DUSUYOR
        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Exists() && snapshot.HasChild(FirebaseClient.UserStaticName) && snapshot.HasChild(FirebaseClient.UserStaticImageName))
            {
                string retrieveUserName = snapshot
                    .Child(FirebaseClient.UserStaticName) //name
                    .GetValue(true).ToString();
                string retrievesStatus = snapshot
                  .Child(FirebaseClient.UserStaticStatusName) // status
                  .GetValue(true).ToString();
                string retrieveProfileImage = snapshot
                .Child(FirebaseClient.UserStaticImageName) //image
                .GetValue(true).ToString();

                userName.Text = retrieveUserName;
                userStatus.Text = retrievesStatus;

                //BURADA SORUN VAR !!
                var ss =  Android.Net.Uri.Parse(retrieveProfileImage);
                userProfileImage.SetImageURI(ss);

                //Picasso.Get().Load(retrieveProfileImage).Into(userProfileImage);
                //Picasso.Load(retrieveProfileImage).Into(userProfileImage);
                //Picasso.With(this).Load(retrieveProfileImage).Into(userProfileImage);


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