using System.Collections.Generic;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace Sped4.Classes.TelerikCls
{
    class TelerikFunktions
    {

        public static CompositeFilterDescriptor SetGridViewFilter(string myCol, string mySearchValue)
        {
            CompositeFilterDescriptor comFilter = new CompositeFilterDescriptor();
            comFilter.FilterDescriptors.Add(myCol, FilterOperator.StartsWith, mySearchValue);
            return comFilter;
        }

        public static List<int> GetColumnArtikelIdFrom(ref RadGridView myGrid)
        {
            List<int> list = new List<int>();
            foreach (GridViewRowInfo r in myGrid.Rows)
            {
                int iTmp = 0;
                int.TryParse(r.Cells["ArtikelId"].Value.ToString(), out iTmp);
                if (iTmp > 0)
                {
                    list.Add(iTmp);
                }
            }
            return list;
        }
    }
}
