using Telerik.WinControls.UI;

namespace Sped4.TelerikControls
{
    public class GridViewRowSetUnselected
    {
        /// <summary>
        ///           Setzt die Properties IsSelected und IsCurrent auf False
        /// </summary>
        /// <param name="myDgv"></param>
        public static void Rows_SetUnselected(ref RadGridView myDgv)
        {
            foreach (GridViewRowInfo rowInfo in myDgv.Rows)
            {
                rowInfo.IsSelected = false;
                rowInfo.IsCurrent = false;
            }
        }
    }
}
