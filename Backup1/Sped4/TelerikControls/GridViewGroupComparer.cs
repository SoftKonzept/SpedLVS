using System.Collections.Generic;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4.TelerikControls
{
    public class GridViewGroupComparer : IComparer<Group<GridViewRowInfo>>
    {
        public int Compare(Group<GridViewRowInfo> x, Group<GridViewRowInfo> y)
        {
            int parsedX, parsedY;
            if (int.TryParse(x.Key.ToString(), out parsedX) && int.TryParse(y.Key.ToString(), out parsedY))
            {
                return parsedX.CompareTo(parsedY);
            }
            return x.Key.ToString().CompareTo(y.Key.ToString());
        }
    }

}
