using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoeRockTutorial.Models
{
    public class MyListViewAdapter : BaseAdapter<Person>
    {
        public List<Person> items;
        private Context context;
        public MyListViewAdapter(Context _context, List<Person> _items)
        {
            items = _items;
            context = _context;
        }
      

        public override Person this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate
                    (Resource.Layout.listview_row, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            txtName.Text = items[position].FirstName;

            TextView txtLastName = row.FindViewById<TextView>(Resource.Id.txtlastName);
            txtLastName.Text = items[position].LastName;

            TextView txtAge = row.FindViewById<TextView>(Resource.Id.txtAge);
            txtAge.Text = items[position].Age;

            TextView txtGender = row.FindViewById<TextView>(Resource.Id.txtGender);
            txtGender.Text = items[position].Gender;

            return row;
        }
    }
}