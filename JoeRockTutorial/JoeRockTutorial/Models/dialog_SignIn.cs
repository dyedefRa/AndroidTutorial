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
        private EditText txtFirstName;
        private EditText txtEmail;
        private EditText txtPassword;
        private Button btnSignUp;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Bu classtan findViewById methodu cekecegız.

            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            txtFirstName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            txtEmail = view.FindViewById<EditText>(Resource.Id.txtEmailAddress);
            txtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            btnSignUp = view.FindViewById<Button>(Resource.Id.btnDialogEmail);

            btnSignUp.Click += BtnSignUp_Click;

            return view;
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(txtFirstName.Text, txtEmail.Text, txtPassword.Text));
            this.Dismiss();
        }

        public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;


        //public override void OnActivityCreated(Bundle savedInstanceState)
        //{
        //    Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
        //    base.OnActivityCreated(savedInstanceState);
        //    //Dialog.Window.Attributes.WindowAnimations=Resource.Style.dialog_ani
        //}
    }

    public class OnSignUpEventArgs : EventArgs
    {
        public OnSignUpEventArgs(string firstName, string email, string password) : base()
        {
            FirstName = firstName;
            Email = email;
            Password = password;
        }
        private string mFirstName;
        private string mEmail;
        private string mPassword;

        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }

        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
    }
}