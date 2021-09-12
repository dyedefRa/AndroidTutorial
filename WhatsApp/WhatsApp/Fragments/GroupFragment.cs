using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatsApp.Helper;

namespace WhatsApp.Fragments
{
    public class GroupFragment : AndroidX.Fragment.App.Fragment, IValueEventListener
    {
        private View groupFragmentView;
        private ListView listView;
        private ArrayAdapter<string> arrayAdapter;
        private List<string> listOfGroup = new List<string>();
        private DatabaseReference groupRef;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            groupFragmentView = inflater.Inflate(Resource.Layout.fragment_group, container, false);
            groupRef = FirebaseClient.GetDatabaseReference().Child(FirebaseClient.GroupStaticName);

            InitializeFields();

            groupRef.AddValueEventListener(this);

            return groupFragmentView;
        }

        private void InitializeFields()
        {
            listView = groupFragmentView.FindViewById<ListView>(Resource.Id.listView1);
      
        
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            //Datayı aldık.
            HashSet<string> set = new HashSet<string>();
            var iterator = snapshot.Children.Iterator();

            while (iterator.HasNext)
            {
                set.Add(((DataSnapshot)iterator.Next()).Key);
            }

            listOfGroup.Clear();
            listOfGroup.AddRange(set);
            arrayAdapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1, listOfGroup);
            arrayAdapter.NotifyDataSetChanged();
           
            listView.SetAdapter(arrayAdapter);
        }

        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }
    }
}