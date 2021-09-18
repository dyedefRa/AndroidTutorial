package crc64ee1668752c35d02a;


public class GroupChatActivity
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		com.google.firebase.database.ValueEventListener,
		android.view.View.OnClickListener,
		com.google.firebase.database.ChildEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onStart:()V:GetOnStartHandler\n" +
			"n_onCancelled:(Lcom/google/firebase/database/DatabaseError;)V:GetOnCancelled_Lcom_google_firebase_database_DatabaseError_Handler:Firebase.Database.IValueEventListenerInvoker, Xamarin.Firebase.Database\n" +
			"n_onDataChange:(Lcom/google/firebase/database/DataSnapshot;)V:GetOnDataChange_Lcom_google_firebase_database_DataSnapshot_Handler:Firebase.Database.IValueEventListenerInvoker, Xamarin.Firebase.Database\n" +
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onChildAdded:(Lcom/google/firebase/database/DataSnapshot;Ljava/lang/String;)V:GetOnChildAdded_Lcom_google_firebase_database_DataSnapshot_Ljava_lang_String_Handler:Firebase.Database.IChildEventListenerInvoker, Xamarin.Firebase.Database\n" +
			"n_onChildChanged:(Lcom/google/firebase/database/DataSnapshot;Ljava/lang/String;)V:GetOnChildChanged_Lcom_google_firebase_database_DataSnapshot_Ljava_lang_String_Handler:Firebase.Database.IChildEventListenerInvoker, Xamarin.Firebase.Database\n" +
			"n_onChildMoved:(Lcom/google/firebase/database/DataSnapshot;Ljava/lang/String;)V:GetOnChildMoved_Lcom_google_firebase_database_DataSnapshot_Ljava_lang_String_Handler:Firebase.Database.IChildEventListenerInvoker, Xamarin.Firebase.Database\n" +
			"n_onChildRemoved:(Lcom/google/firebase/database/DataSnapshot;)V:GetOnChildRemoved_Lcom_google_firebase_database_DataSnapshot_Handler:Firebase.Database.IChildEventListenerInvoker, Xamarin.Firebase.Database\n" +
			"";
		mono.android.Runtime.register ("WhatsApp.Activities.GroupChatActivity, WhatsApp", GroupChatActivity.class, __md_methods);
	}


	public GroupChatActivity ()
	{
		super ();
		if (getClass () == GroupChatActivity.class)
			mono.android.TypeManager.Activate ("WhatsApp.Activities.GroupChatActivity, WhatsApp", "", this, new java.lang.Object[] {  });
	}


	public GroupChatActivity (int p0)
	{
		super (p0);
		if (getClass () == GroupChatActivity.class)
			mono.android.TypeManager.Activate ("WhatsApp.Activities.GroupChatActivity, WhatsApp", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onStart ()
	{
		n_onStart ();
	}

	private native void n_onStart ();


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


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);


	public void onChildAdded (com.google.firebase.database.DataSnapshot p0, java.lang.String p1)
	{
		n_onChildAdded (p0, p1);
	}

	private native void n_onChildAdded (com.google.firebase.database.DataSnapshot p0, java.lang.String p1);


	public void onChildChanged (com.google.firebase.database.DataSnapshot p0, java.lang.String p1)
	{
		n_onChildChanged (p0, p1);
	}

	private native void n_onChildChanged (com.google.firebase.database.DataSnapshot p0, java.lang.String p1);


	public void onChildMoved (com.google.firebase.database.DataSnapshot p0, java.lang.String p1)
	{
		n_onChildMoved (p0, p1);
	}

	private native void n_onChildMoved (com.google.firebase.database.DataSnapshot p0, java.lang.String p1);


	public void onChildRemoved (com.google.firebase.database.DataSnapshot p0)
	{
		n_onChildRemoved (p0);
	}

	private native void n_onChildRemoved (com.google.firebase.database.DataSnapshot p0);

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
