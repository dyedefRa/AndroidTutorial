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
    public class dialog_SignIn : DialogFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            return view;
        }

        //public override void OnActivityCreated(Bundle savedInstanceState)
        //{
        //    Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
        //    base.OnActivityCreated(savedInstanceState);
        //    //Dialog.Window.Attributes.WindowAnimations=Resource.Style.dialog_ani
        //}
    }
}