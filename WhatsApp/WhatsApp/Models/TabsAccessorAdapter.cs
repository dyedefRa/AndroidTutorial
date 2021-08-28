using AndroidX.Fragment.App;
using Java.Lang;
using System;
using WhatsApp.Fragments;

namespace WhatsApp.Models
{
    public class TabsAccessorAdapter : FragmentPagerAdapter
    {
        public TabsAccessorAdapter(FragmentManager fragmentManager) : base(fragmentManager)
        {
            //Fragmentları tanıttık
        }
        public override int Count => 3;

        public override AndroidX.Fragment.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    ChatFragment chatFragment = new ChatFragment();
                    return chatFragment;
                case 1:
                    GroupFragment groupFragment = new GroupFragment();
                    return groupFragment;
                case 2:
                    ContactFragment contactFragment = new ContactFragment();
                    return contactFragment;
                default:
                    return null;
            }
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            switch (position)
            {
                case 0:
                    return new Java.Lang.String("CHATS");
                case 1:
                    return new Java.Lang.String("GROUPS");
                case 2:
                    return new Java.Lang.String("CONTACTS");
                default:
                    return null;
            }
        }
        //override pa
    }
}