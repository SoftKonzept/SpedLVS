using LVS;
using LVS.ASN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrASNArtFieldAssignSelectToCopy : UserControl
    {
        internal ctrMenu ctrMenue;
        internal clsASNWizzard asnWizz;
        internal DataTable dtDgvSource;
        public frmTmp _frmTmp;
        internal const string NameColumnSelect = "Selected";
        /// <summary>
        /// 
        /// </summary>
        public ctrASNArtFieldAssignSelectToCopy(ctrMenu myMenu, clsASNWizzard myAsnWizz)
        {
            InitializeComponent();
            this.ctrMenue = myMenu;
            this.asnWizz = myAsnWizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrASNArtFieldAssignSelectToCopy_Load(object sender, EventArgs e)
        {
            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgv()
        {
            dtDgvSource = new DataTable();
            dtDgvSource = clsASNArtFieldAssignment.GetASNArtFieldAssignmentsBySender(this.ctrMenue.GL_User, this.asnWizz.ASNArtFieldAssign.Receiver);
            dtDgvSource.Columns.Add(NameColumnSelect, typeof(Boolean)).SetOrdinal(0);

            this.dgvASNArtFieldAssignmentSelect.DataSource = dtDgvSource;
            this.dgvASNArtFieldAssignmentSelect.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_DoubleClick(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvASNArtFieldAssignmentSelect_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (!this.dgvASNArtFieldAssignmentSelect.Rows[e.RowIndex].Cells[NameColumnSelect].ReadOnly)
                {
                    if (
                            (this.dgvASNArtFieldAssignmentSelect.Rows[e.RowIndex].Cells[NameColumnSelect].Value == null) ||
                            (this.dgvASNArtFieldAssignmentSelect.Rows[e.RowIndex].Cells[NameColumnSelect].Value.ToString() == string.Empty)
                       )
                    {
                        this.dgvASNArtFieldAssignmentSelect.Rows[e.RowIndex].Cells[NameColumnSelect].Value = false;
                    }

                    bool CellValue = (bool)this.dgvASNArtFieldAssignmentSelect.Rows[e.RowIndex].Cells[NameColumnSelect].Value;
                    if (CellValue == true)
                    {
                        this.dgvASNArtFieldAssignmentSelect.Rows[e.RowIndex].Cells[NameColumnSelect].Value = false;
                    }
                    else
                    {
                        this.dgvASNArtFieldAssignmentSelect.Rows[e.RowIndex].Cells[NameColumnSelect].Value = true;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_DoubleClick(object sender, EventArgs e)
        {
            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        private void TakeOver()
        {
            //check Datensätze ausgeählt?
            this.dtDgvSource.DefaultView.RowFilter = NameColumnSelect + "=True";
            DataTable dtSelected = this.dtDgvSource.DefaultView.ToTable();
            if (dtSelected.Rows.Count > 0)
            {
                //ID ermitteln und übernehmen
                List<int> ListSelectedID = new List<int>();
                foreach (DataRow row in dtSelected.Rows)
                {
                    string strID = row["ID"].ToString();
                    int iTmp = 0;
                    int.TryParse(strID, out iTmp);
                    if (iTmp > 0)
                    {
                        if (!ListSelectedID.Contains(iTmp))
                        {
                            ListSelectedID.Add(iTmp);
                        }
                    }
                }
                if (ListSelectedID.Count > 0)
                {
                    //clsASNArtFieldAssignment.InsertShapeByRefAdr((int)myDecADR_ID, (int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.AuftragggeberAdr.BenutzerID, this.asnWizz.ASNArtFieldAssign._GL_System.sys_ArbeitsbereichID);
                    clsASNArtFieldAssignment.InsertShapeByRefAdr(ListSelectedID, (int)this.asnWizz.AuftragggeberAdr.ID, this.asnWizz.AuftragggeberAdr.BenutzerID, this.asnWizz.ASNArtFieldAssign._GL_System.sys_ArbeitsbereichID);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTakeOver_Click(object sender, EventArgs e)
        {
            TakeOver();
            this._frmTmp.CloseFrmTmp();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this._frmTmp.CloseFrmTmp();
        }
    }
}
