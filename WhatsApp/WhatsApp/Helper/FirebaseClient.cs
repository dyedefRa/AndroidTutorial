using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatsApp.Helper
{
    public class FirebaseClient
    {
        private static string ReferenceName = "WhatsAppDb";

        //bu name propertisinden user in usernamını olusturup oılsturmadıgını kontrol edıyoruz .ona göre main yada setting activitye gonderıyoruz
        public static string UserStaticUID = "uid";
        public static string UserStaticName = "name";
        public static string UserStaticImageName = "image";
        public static string UserStaticStatusName = "status";
        //Create User Login Logout methodları. Auth methodları yanı
        public static FirebaseAuth GetFirebaseAuth()
        {
            return FirebaseAuth.GetInstance(FirebaseClient.GetFirebaseApp());
        }

        //Bunu realtimedb de yer açmak yada kayıt almak için eklıyoruz
        public static DatabaseReference GetDatabaseReference()
        {
            DatabaseReference dbref = FirebaseDatabase.GetInstance(GetFirebaseApp()).GetReference(ReferenceName);
            return dbref;
        }

        public static FirebaseUser GetCurrentUser()
        {
            return GetFirebaseAuth().CurrentUser;                
        }

     

        private static FirebaseApp GetFirebaseApp()
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

    }
}