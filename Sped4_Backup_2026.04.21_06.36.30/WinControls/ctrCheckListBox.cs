using System.Windows.Forms;

namespace Sped4.WinControls
{
    public class ctrCheckListBox
    {

        public static void SetValueToAllItems(ref CheckedListBox myClb, bool myVal)
        {
            for (int i = 0; i < myClb.Items.Count - 1; i++)
            {
                myClb.SetItemChecked(i, myVal);
            }
        }

        public static void SetValueToItemAtIndex(ref CheckedListBox myClb, int myIndex, bool myVal, bool mySingleSelection)
        {
            if (mySingleSelection)
            {
                ctrCheckListBox.SetValueToAllItems(ref myClb, false);
            }
            myClb.SetItemChecked(myIndex, myVal);
        }

        public static void ReSetValueToItemAtIndex(ref CheckedListBox myClb, int myIndex, bool myVal, bool mySingleSelection)
        {
            if (mySingleSelection)
            {
                ctrCheckListBox.SetValueToAllItems(ref myClb, false);
            }
            bool reSetItemValue = !(myVal);
            myClb.SetItemChecked(myIndex, reSetItemValue);
        }
    }
}
