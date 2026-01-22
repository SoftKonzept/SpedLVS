using Telerik.WinControls.UI;


namespace Sped4.Classes.TelerikCls
{
    public class GridViewCustomerSort
    {
        /// <summary>
        ///             SortResult: 
        ///             returns negative value when Row1 is before Row2, 
        ///             positive value if Row1 is after Row2 
        ///             and zero if the rows are have equal values in a specified column
        /// </summary>
        /// <returns></returns>
        public static int GridViewCustomSorting(string myRowValue1, string myRowValue2, string myColNameToSort, RadSortOrder myColSort)
        {
            int retInt = 0;

            int iTmp1 = 0;
            int.TryParse(myRowValue1, out iTmp1);
            int iTmp2 = 0;
            int.TryParse(myRowValue2, out iTmp2);

            switch (myColSort)
            {
                case RadSortOrder.Ascending:
                    if (iTmp1 > iTmp2)
                    {
                        retInt = 1;
                    }
                    else if (iTmp1 < iTmp2)
                    {
                        retInt = -1;
                    }
                    else
                    {
                        retInt = 0;
                    }
                    break;
                case RadSortOrder.Descending:
                    if (iTmp1 > iTmp2)
                    {
                        retInt = -1;
                    }
                    else if (iTmp1 < iTmp2)
                    {
                        retInt = 1;
                    }
                    else
                    {
                        retInt = 0;
                    }
                    break;
                case RadSortOrder.None:

                    break;
            }

            return retInt;
        }
    }
}
