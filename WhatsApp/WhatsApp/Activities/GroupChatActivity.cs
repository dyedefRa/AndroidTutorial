using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsApp.Helper;
using static Android.Views.View;

namespace WhatsApp.Activities
{
    [Activity(Label = "GroupChatActivity")]
    public class GroupChatActivity : AppCompatActivity, IValueEventListener, IOnClickListener, IChildEventListener
    {
        private AndroidX.AppCompat.Widget.Toolbar mToolBar;
        private ImageButton sendMessageButton;
        private EditText userMessageInput;
        //mesaj geldikçe otomatik en alta scroll yapsın dıye eklemısız.
        private ScrollView mScrollView;
        private TextView displayTextMessage;
        private DatabaseReference userRef, groupNameRef, groupMessageKeyRef;

        private string currentUserName;
        private string currentUserUID;
        private string currentGroupName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

    

            SetContentView(Resource.Layout.group_chat_activity);
            //GroupFragment > listview.   OnItemClick Den gelıyor.
            currentGroupName = Intent.GetStringExtra("groupName");
            Toast.MakeText(this, currentGroupName, ToastLength.Short)
                .Show();
            currentUserUID = FirebaseClient.GetCurrentUser().Uid;
            userRef = FirebaseClient.GetDatabaseReference().Child(FirebaseClient.UsersChildStaticName)
                .Child(currentUserUID); //Users > currentUser

            groupNameRef = FirebaseClient.GetDatabaseReference().Child(FirebaseClient.GroupChildStaticName)
                .Child(currentGroupName); //Groups > currentGroup

        

            InitializeFields();

            GetUserInfo();

            sendMessageButton.SetOnClickListener(this);
        }

        protected override void OnStart()
        {
            base.OnStart();

            //AÇıldığında mesajlar otomatik gelsin.
            groupNameRef.AddChildEventListener(this);
            //IChildEventListener den miras aldık.
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

        private void GetUserInfo()
        {
            userRef.AddValueEventListener(this);
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Exists())
            {
                currentUserName = snapshot.Child(FirebaseClient.UserStaticName).Value.ToString(); // name
            }
        }

        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnClick(View v)
        {
            SaveMessageInfoToDatabase();

            userMessageInput.Text = "";

        }

        private void SaveMessageInfoToDatabase()
        {
            string messsage = userMessageInput.Text;
            //Yeni key elde etme
            string messageKey = groupNameRef.Push().Key;

            //MEssajın keyi!
            groupMessageKeyRef = groupNameRef.Child(messageKey);
            //Groups > currentGroup > messageKEy
            if (string.IsNullOrEmpty(messsage))
                Toast.MakeText(this, "Please write message first...", ToastLength.Short)
                    .Show();
            else
            {
                var currentDate = DateTime.Now.ToString("MMM dd, yyyy");
                var currentTime = DateTime.Now.ToString("hh:mm a");

                Dictionary<string, Java.Lang.Object> messageInfoMap = new Dictionary<string, Java.Lang.Object>();
                messageInfoMap.Add("name", currentUserName);
                messageInfoMap.Add("message", messsage);
                messageInfoMap.Add("date", currentDate);
                messageInfoMap.Add("time", currentTime);

                groupMessageKeyRef.UpdateChildren(messageInfoMap);
            }
        }

        public void OnChildAdded(DataSnapshot snapshot, string previousChildName)
        {
            //SAYFA AÇILDIĞINDA OTOMATIK OLARAK MESAJLARI GETIRIR!.
            if (snapshot.Exists())
            {
                DisplayMessages(snapshot);
            }
        }

        private void DisplayMessages(DataSnapshot snapshot)
        {
            //SAYFA AÇILDIĞINDA OTOMATIK OLARAK MESAJLARI GETIRIR!.

            var iterator = snapshot.Children.Iterator();

            while (iterator.HasNext)
            {
                string chatDate = ((DataSnapshot)iterator.Next()).GetValue(true).ToString();
                string chatMessage = ((DataSnapshot)iterator.Next()).GetValue(true).ToString();
                string chatName = ((DataSnapshot)iterator.Next()).GetValue(true).ToString();
                string chatTime = ((DataSnapshot)iterator.Next()).GetValue(true).ToString();

                displayTextMessage.Append(chatName + " :\n" + chatMessage + " \n" + chatTime + "  " + chatDate + " \n\n");

                mScrollView.FullScroll(FocusSearchDirection.Down);
            }
     
        }

        public void OnChildChanged(DataSnapshot snapshot, string previousChildName)
        {
            throw new NotImplementedException();
        }

        public void OnChildMoved(DataSnapshot snapshot, string previousChildName)
        {
            throw new NotImplementedException();
        }

        public void OnChildRemoved(DataSnapshot snapshot)
        {
            throw new NotImplementedException();
        }
    }
}