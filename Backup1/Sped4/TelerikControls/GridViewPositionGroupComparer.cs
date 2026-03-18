using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4.TelerikControls
{
    public class GridViewPositionGroupComparer : IComparer<Group<GridViewRowInfo>>
    {
        public int Compare(Group<GridViewRowInfo> myGroupX, Group<GridViewRowInfo> myGroupY)
        {
            int minX = 0; // x.Rows.Min(r => Convert.ToInt32(r.Cells["Pos"].Value));
            int minY = 0; // y.Rows.Min(r => Convert.ToInt32(r.Cells["Pos"].Value));

            minX = myGroupX
                         .Select(row => Convert.ToInt32(row.Cells["Pos"].Value))
                         .Min();

            minY = myGroupY
                         .Select(row => Convert.ToInt32(row.Cells["Pos"].Value))
                         .Min();
            return minX.CompareTo(minY);
        }

        //public int Compare(Group<GridViewRowInfo> x, Group<GridViewRowInfo> y)
        //{
        //    int parsedX;
        //    int parsedY;
        //    if (int.TryParse(((object[])x.Key).First().ToString(), out parsedX) &&
        //        int.TryParse(((object[])y.Key).First().ToString(), out parsedY))
        //    {
        //        int result = parsedX.CompareTo(parsedY);
        //        DataGroup xGroup = x as DataGroup;
        //        if (xGroup != null && ((DataGroup)x).GroupDescriptor.GroupNames.First().Direction == ListSortDirection.Descending)
        //        {
        //            result *= -1;
        //        }
        //        return result;
        //    }
        //    return ((object[])x.Key)[0].ToString().CompareTo(((object[])y.Key)[0].ToString());
        //}
    }

}
