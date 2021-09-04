using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
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
    public class MainActivity : AppCompatActivity
    { 
        private Toolbar mToolBar;
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
            mToolBar = FindViewById<Toolbar>(Resource.Id.main_page_toolbar);
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

        protected override void OnStart()
        {
            base.OnStart();
            if (FirebaseClient.GetCurrentUser() == null)
            {
                SendUserToLoginActivity();
            }
            else
            {
                //Eğer user null degilse
                VerifyUserExistance();
            }
        }

        private void VerifyUserExistance()
        {
            string currentUserId = FirebaseClient.GetCurrentUserId;

            //Eğer user null degilse name propertisine bak. (MyEventListener da var)
            //Eger name prop null degılse Welcome yaz degılse Setting sayfasına yonlendır.
            //BURASI1
            FirebaseClient.GetDatabaseReference().Child("Users").Child(currentUserId).AddValueEventListener(new MyEventListener());
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
            //return base.OnOptionsItemSelected(item);

            if (item.ItemId == Resource.Id.main_logout_option)
            {
                FirebaseClient.GetFirebaseAuth().SignOut();
                SendUserToLoginActivity();
            }
            else if (item.ItemId == Resource.Id.main_settings_option)
            {
                SendUserToSettingsActivity();
            }
            else if (item.ItemId == Resource.Id.main_find_friends_option)
            {

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
    }
}