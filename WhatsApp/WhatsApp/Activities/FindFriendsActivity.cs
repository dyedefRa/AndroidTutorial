using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;

namespace WhatsApp.Activities
{
    [Activity(Label = "FindFriendsActivity")]
    public class FindFriendsActivity : AppCompatActivity
    {
        private AndroidX.AppCompat.Widget.Toolbar mToolBar;
        private RecyclerView findFriendsRecyclerList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.find_friends_activity);
            findFriendsRecyclerList = FindViewById<RecyclerView>(Resource.Id.find_friends_recycler_list);
            findFriendsRecyclerList.SetLayoutManager(new LinearLayoutManager(this));

            mToolBar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.find_friends_toolbar);

            SetSupportActionBar(mToolBar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetTitle(Resource.String.findFriends);
        }

        protected override void OnStart()
        {
            base.OnStart();

            //FirebaseRecyclerAdapter
        }
    }
}