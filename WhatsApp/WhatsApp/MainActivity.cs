using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.ViewPager.Widget;
using Google.Android.Material.Tabs;
using System.Linq;
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
    }
}