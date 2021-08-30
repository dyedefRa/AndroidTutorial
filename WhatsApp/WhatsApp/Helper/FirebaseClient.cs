using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatsApp.Helper
{
    public class FirebaseClient
    {


        //public static void InitalizeDatabase()
        //{
        //    FirebaseDatabase database;

        //    var app = FirebaseApp.InitializeApp(Application.Context);

        //    if (app == null)
        //    {
        //        var options = new FirebaseOptions.Builder()
        //            .SetApplicationId("whatsapp-81d75")
        //            .SetApiKey("AIzaSyDW2iP83_mGuJ1hFgOitXv1bONjCxvUoFE")
        //            .SetDatabaseUrl("https://whatsapp-81d75-default-rtdb.firebaseio.com")
        //            .SetStorageBucket("whatsapp-81d75.appspot.com")
        //            .Build();
        //    }
        //    database = FirebaseDatabase.GetInstance(app);

        //    DatabaseReference dbref = database.GetReference("UserSupport");
        //    dbref.SetValue("Ticket1");

        //    Toast.MakeText(Application.Context, "Completed", ToastLength.Short).Show();
        //}

        public static FirebaseApp GetApp()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetApplicationId("whatsapp-81d75")
                    .SetApiKey("AIzaSyDW2iP83_mGuJ1hFgOitXv1bONjCxvUoFE")
                    .SetDatabaseUrl("https://whatsapp-81d75-default-rtdb.firebaseio.com")
                    .SetStorageBucket("whatsapp-81d75.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(Application.Context, options);
            }

            return app;
        }
    }
}