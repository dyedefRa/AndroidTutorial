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

namespace MyApp.Activities
{
    [Activity(Label = "open_file_get_file", MainLauncher = true)]
    public class open_file_get_file : Activity
    {
        TextView txtPathShow;
        Button btnOpenFile;
        Intent myFileIntent;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.open_file_get_file);

            txtPathShow = FindViewById<TextView>(Resource.Id.txtFileShow);
            btnOpenFile = FindViewById<Button>(Resource.Id.btnOpenFile);

            btnOpenFile.Click += (s, e) =>
             {
                 myFileIntent = new Intent(Intent.ActionGetContent);
                 myFileIntent.SetType("*/*");

                 StartActivityForResult(myFileIntent, 10);
             };
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {

            switch (requestCode)
            {
                case 10:
                    if (resultCode == Result.Ok)
                    {
                        string path = data.Data.Path;
                        txtPathShow.Text = path;
                    }
                    break;
            }
        }

        }
    }