using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.ViewPager.Widget;
using Firebase.Auth;
using Firebase.Database;
using Google.Android.Material.Tabs;

using System;
using System.Linq;
using WhatsApp.Activities;
using WhatsApp.Helper;
using WhatsApp.Models;

namespace WhatsApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IValueEventListener
    {
        private AndroidX.AppCompat.Widget.Toolbar mToolBar;
        private ViewPager mViewPager;
        private TabLayout mTabLayout;
        private TabsAccessorAdapter mTabsAccessorAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //NOTE 1
            mToolBar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.main_page_toolbar);
            SetSupportActionBar(mToolBar);
            //ActionBar.SetTitle("");

            //NOTE 2
            mViewPager = FindViewById<ViewPager>(Resource.Id.main_tabs_pager);

            mTabsAccessorAdapter = new TabsAccessorAdapter(SupportFragmentManager);
            mViewPager.Adapter = mTabsAccessorAdapter;

            mTabLayout = FindViewById<TabLayout>(Resource.Id.main_tabs);
            mTabLayout.SetupWithViewPager(mViewPager);

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //Main activity başlamadan once eger current user yoksa logine at.
        //Varsa VerifyUserExistance() a bak. ;;
        //Eger name prop null degılse Welcome yaz degılse Setting sayfasına yonlendır.
        protected override void OnStart()
        {
            base.OnStart();
            if (FirebaseClient.GetCurrentUser() == null)
            {
                SendUserToLoginActivity();
            }
            else
            {
                VerifyUserExistance();
            }
        }

        private void VerifyUserExistance()
        {
            string currentUserId = FirebaseClient.GetCurrentUser().Uid;

            //Eğer user null degilse name propertisine bak. OnDataChange
            //Eger name prop null degılse Welcome yaz degılse Setting sayfasına yonlendır.

            FirebaseClient.GetDatabaseReference().Child("Users").Child(currentUserId).AddValueEventListener(this);
        }
        //AddValueEventListener
        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }
        //AddValueEventListener
        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Child("name").Exists())
                Toast.MakeText(Application.Context, "Welcome", ToastLength.Short)
                    .Show();
            else
                SendUserToSettingsActivity();
        }

        private void SendUserToLoginActivity()
        {
            Intent loginIntent = new Intent(this, typeof(LoginActivity));
            loginIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(loginIntent);
            Finish();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Drawable.options_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.main_logout_option)//4
            {
                FirebaseClient.GetFirebaseAuth().SignOut();
                SendUserToLoginActivity();
            }
            else if (item.ItemId == Resource.Id.main_settings_option)//3
            {
                SendUserToSettingsActivity();
            }
            else if (item.ItemId == Resource.Id.main_find_friends_option)//1
            {

            }
            else if (item.ItemId == Resource.Id.main_create_group_option)//2
            {
                //Burada dialog cıkardık.
                RequestNewGroup();
            }
            return true;
        }

        private void SendUserToSettingsActivity()
        {
            Intent settingsIntent = new Intent(this, typeof(SettingsActivity));
            settingsIntent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(settingsIntent);
            Finish();
        }

        EditText groupNameField;
        string groupName;
        private void RequestNewGroup()
        {
            //Dialog Alert
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Enter Group Name :");


            groupNameField = new EditText(this);
            groupNameField.SetHint(Resource.String.hintEx);
            builder.SetView(groupNameField);

            builder.SetPositiveButton("Create", (senderAlert, args) =>
            {
                groupName = groupNameField.Text;

                if (string.IsNullOrEmpty(groupName))
                    Toast.MakeText(this, "Please write Group Name", ToastLength.Short)
                        .Show();
                else
                {
                    CreateNewGroup(groupName);
                }
            });
            builder.SetNegativeButton("Cancel", (senderAlert, args) =>
            {

                //CLOSE AYARLA.
            });

            builder.Show();
        }

        //Burada yenı group chıledı olusturuk!!!
        private void CreateNewGroup(string groupName)
        {
            TaskCompletionListener taskCompletionListener = new TaskCompletionListener();
            taskCompletionListener.Complete += TaskCompletionListener_Complete;

            FirebaseClient.GetDatabaseReference()
               .Child(FirebaseClient.GroupStaticName) //Gruops
               .Child(groupName)
               .SetValue("")
               .AddOnCompleteListener(taskCompletionListener);

        }

        private void TaskCompletionListener_Complete(object sender, EventArgs e)
        {
            Toast.MakeText(this, groupName + " group is Created Successfully... ", ToastLength.Short)
                            .Show();
        }
    }
}