using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4.TelerikControls
{
    public class RowSelectionGridBehavior : GridDataRowBehavior
    {
        protected override bool OnMouseDownLeft(MouseEventArgs e)
        {
            GridDataRowElement row = this.GetRowAtPoint(e.Location) as GridDataRowElement;
            if (row != null)
            {
                RadGridViewDragDropService svc = this.GridViewElement.GetService<RadGridViewDragDropService>();
                svc.Start(row);
            }
            return base.OnMouseDownLeft(e);
        }

    }
}
