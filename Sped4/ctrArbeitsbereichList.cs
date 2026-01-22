using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrArbeitsbereichList : UserControl
    {
        public Globals._GL_USER GL_User;
        internal frmArbeitsbereiche _frmArbeitsbereiche;
        internal Int32 iGrdWidth;

        public delegate void ArbeitsbereichTakeOverEventHandler(decimal TakeOverID);
        public event ArbeitsbereichTakeOverEventHandler GetArbeitsbereichTakeOver;

        ///<summary>ctrArbeitsbereichList / ctrArbeitsbereichList</summary>
        ///<remarks></remarks>
        public ctrArbeitsbereichList()
        {
            InitializeComponent();
        }
        ///<summary>ctrArbeitsbereichList / ctrArbeitsbereichList_Load</summary>
        ///<remarks></remarks>
        private void ctrArbeitsbereichList_Load(object sender, EventArgs e)
        {

        }
        ///<summary>ctrArbeitsbereichList / InitGrd</summary>
        ///<remarks></remarks>
        public void InitGrd()
        {
            DataTable dt = new DataTable();
            dt = clsArbeitsbereiche.GetArbeitsbereichList(GL_User.User_ID);
            dgv.DataSource = dt;
            for (Int32 i = 0; i <= dgv.Columns.Count - 1; i++)
            {
                string strColName = dgv.Columns[i].Name;
                switch (strColName)
                {
                    case "MandantenID":
                        dgv.Columns[i].IsVisible = false;
                        break;
                    default:
                        dgv.Columns[i].IsVisible = true;
                        break;
                }
            }
            this.dgv.BestFitColumns();
            //SetFrmWidth();
        }
        ///<summary>ctrArbeitsbereichList / SetFrmWidth</summary>
        ///<remarks>Passt die Ctr-Breite an</remarks>
        private void SetFrmWidth()
        {
            //iGrdWidth = Functions.dgv_GetWidthShownGrid(ref grd);
            //this.panABList.Width = iGrdWidth;
            //this.Width = iGrdWidth;
            //this.Refresh();
        }
        ///<summary>ctrArbeitsbereichList / dgv_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgv_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.dgv.Rows.Count > 0)
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgv.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                if (decTmp > 0)
                {
                    GetArbeitsbereichTakeOver(decTmp);
                }
            }
        }

    }
}
