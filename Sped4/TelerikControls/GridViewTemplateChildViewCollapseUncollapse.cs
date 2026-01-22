using Telerik.WinControls.UI;

namespace Sped4.TelerikControls
{
    public class GridViewTemplateChildViewCollapseUncollapse
    {

        public static void CollapseOrUncollapseTemplate(ref RadGridView gridView, bool collapse)
        {
            foreach (GridViewRowInfo row in gridView.Rows)
            {
                row.IsExpanded = collapse;
            }
        }

        public static void CollapseAllExceptCurrent(ref RadGridView gridView)
        {
            GridViewRowInfo currentRow = gridView.CurrentRow;

            foreach (GridViewRowInfo row in gridView.Rows)
            {
                // Nur die aktuelle Zeile offen lassen
                row.IsExpanded = (row == currentRow);
            }
        }

    }
}
