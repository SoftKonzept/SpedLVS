using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFGrid : DataGridView
    {
        public AFGrid()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void AFGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (this.SelectedRows[0].Index == e.RowIndex)
                {
                    LinearGradientBrush oBrush = new LinearGradientBrush(e.CellBounds, Color.FromArgb(255, 128, 0), Color.FromArgb(255, 177, 99), LinearGradientMode.Vertical);
                    e.Graphics.FillRectangle(oBrush, e.CellBounds);
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Border);
                    e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground);
                    e.Handled = true;                    
                }
            }
            catch //(Exception ex) 
            {
              // MessageBox.Show(ex.ToString());
            } 
        }
    }
}
