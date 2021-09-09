package crc646d4d0d07ce40ce84;


public class MyRetrieveUserEventListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.firebase.database.ValueEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCancelled:(Lcom/google/firebase/database/DatabaseError;)V:GetOnCancelled_Lcom_google_firebase_database_DatabaseError_Handler:Firebase.Database.IValueEventListenerInvoker, Xamarin.Firebase.Database\n" +
			"n_onDataChange:(Lcom/google/firebase/database/DataSnapshot;)V:GetOnDataChange_Lcom_google_firebase_database_DataSnapshot_Handler:Firebase.Database.IValueEventListenerInvoker, Xamarin.Firebase.Database\n" +
			"";
		mono.android.Runtime.register ("WhatsApp.Helper.MyRetrieveUserEventListener, WhatsApp", MyRetrieveUserEventListener.class, __md_methods);
	}


	public MyRetrieveUserEventListener ()
	{
		super ();
		if (getClass () == MyRetrieveUserEventListener.class)
			mono.android.TypeManager.Activate ("WhatsApp.Helper.MyRetrieveUserEventListener, WhatsApp", "", this, new java.lang.Object[] {  });
	}

	public MyRetrieveUserEventListener (android.content.Context p0)
	{
		super ();
		if (getClass () == MyRetrieveUserEventListener.class)
			mono.android.TypeManager.Activate ("WhatsApp.Helper.MyRetrieveUserEventListener, WhatsApp", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onCancelled (com.google.firebase.database.DatabaseError p0)
	{
		n_onCancelled (p0);
	}

	private native void n_onCancelled (com.google.firebase.database.DatabaseError p0);


	public void onDataChange (com.google.firebase.database.DataSnapshot p0)
	{
		n_onDataChange (p0);
	}

	private native void n_onDataChange (com.google.firebase.database.DataSnapshot p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
