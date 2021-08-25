using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using JoeRockTutorial.Models;
using System;
using System.Collections.Generic;

namespace JoeRockTutorial.Activities
{
    [Activity(Label = "creating_listview", MainLauncher = true)]
    public class creating_listview : Activity
    {
        private List<Models.Person> items;
        ListView _listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.creating_listview);
            _listView = FindViewById<ListView>(Resource.Id.listView1);

            items = new List<Models.Person>();
            items.Add(new Models.Person() { FirstName = "Baki", LastName = "GG", Age = "28", Gender = "Male" });
            items.Add(new Models.Person() { FirstName = "Emre", LastName = "GGe", Age = "24", Gender = "Male" });
            items.Add(new Models.Person() { FirstName = "Anıl", LastName = "GGa", Age = "22", Gender = "Male" });
            items.Add(new Models.Person() { FirstName = "Ali", LastName = "GGal", Age = "25", Gender = "Male" });


            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);

            //_listView.Adapter = adapter;
            MyListViewAdapter adapter = new MyListViewAdapter(this, items);


            _listView.Adapter = adapter;
            _listView.ItemClick += _listView_ItemClick;
            //_listView.ItemLongClick += _listView_ItemLongClick;

        }

        //private void _listView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        //{
        //    var clickedItem = items[e.Position].FirstName;
        //    Toast.MakeText(this, clickedItem+ "Item long click", ToastLength.Short);
        //}

        private void _listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var clickedItem = items[e.Position].FirstName;
            Toast.MakeText(this, clickedItem + "Item long click", ToastLength.Short)
                .Show();
        }
    }
}