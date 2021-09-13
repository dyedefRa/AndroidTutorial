using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatsApp.Activities
{
    [Activity(Label = "GroupChatActivity")]
    public class GroupChatActivity : AppCompatActivity
    {
        private AndroidX.AppCompat.Widget.Toolbar mToolBar;
        private ImageButton sendMessageButton;
        private EditText userMessageInput;
        private ScrollView mScrollView;
        private TextView displayTextMessage;

        private string currentGroupName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.group_chat_activity);
            //GroupFragment > listview.   OnItemClick Den gelıyor.
            currentGroupName = Intent.GetStringExtra("groupName");
            Toast.MakeText(this, currentGroupName, ToastLength.Short)
                .Show();
            InitializeFields();
        }

        private void InitializeFields()
        {
            mToolBar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.group_chat_bar_layout);
            //Burası için AppCompatActivity tan mıras aldırdık.
            SetSupportActionBar(mToolBar);
            //BURASI GRUP ISMI OLACKA
            SupportActionBar.SetTitle(Resource.String.groupName);

            sendMessageButton = FindViewById<ImageButton>(Resource.Id.send_message_button);
            userMessageInput = FindViewById<EditText>(Resource.Id.input_group_message);
            displayTextMessage = FindViewById<TextView>(Resource.Id.group_chat_text_display);
            mScrollView = FindViewById<ScrollView>(Resource.Id.my_scroll_view);


        }
    }
}