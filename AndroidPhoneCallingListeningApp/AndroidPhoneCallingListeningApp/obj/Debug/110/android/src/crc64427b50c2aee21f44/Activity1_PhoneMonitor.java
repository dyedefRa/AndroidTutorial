package crc64427b50c2aee21f44;


public class Activity1_PhoneMonitor
	extends android.telephony.PhoneStateListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCallStateChanged:(ILjava/lang/String;)V:GetOnCallStateChanged_ILjava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("AndroidPhoneCallingListeningApp.Extend.Activity1+PhoneMonitor, AndroidPhoneCallingListeningApp", Activity1_PhoneMonitor.class, __md_methods);
	}


	public Activity1_PhoneMonitor ()
	{
		super ();
		if (getClass () == Activity1_PhoneMonitor.class)
			mono.android.TypeManager.Activate ("AndroidPhoneCallingListeningApp.Extend.Activity1+PhoneMonitor, AndroidPhoneCallingListeningApp", "", this, new java.lang.Object[] {  });
	}


	public Activity1_PhoneMonitor (java.util.concurrent.Executor p0)
	{
		super (p0);
		if (getClass () == Activity1_PhoneMonitor.class)
			mono.android.TypeManager.Activate ("AndroidPhoneCallingListeningApp.Extend.Activity1+PhoneMonitor, AndroidPhoneCallingListeningApp", "Java.Util.Concurrent.IExecutor, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onCallStateChanged (int p0, java.lang.String p1)
	{
		n_onCallStateChanged (p0, p1);
	}

	private native void n_onCallStateChanged (int p0, java.lang.String p1);

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
