using LVS;
using LVS.ASN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrJobSelectToCopy : UserControl
    {
        internal const string NameColumnSelect = "Selected";
        internal ctrMenu ctrMenue;
        internal clsASNWizzard asnWizz;
        internal DataTable dtDgvSource;
        public frmTmp _frmTmp;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myMenu"></param>
        /// <param name="myAsnWizz"></param>
        public ctrJobSelectToCopy(ctrMenu myMenu, clsASNWizzard myAsnWizz)
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
        private void ctrJobSelectToCopy_Load(object sender, EventArgs e)
        {
            InitDgv();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDgv()
        {
            dtDgvSource = new DataTable();
            dtDgvSource = clsJobs.GetJobsByAdress(this.ctrMenue.GL_User, this.asnWizz.Jobs.AdrVerweisID);
            dtDgvSource.Columns.Add(NameColumnSelect, typeof(Boolean)).SetOrdinal(0);

            this.dgv.DataSource = dtDgvSource;
            this.dgv.BestFitColumns();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (!this.dgv.Rows[e.RowIndex].Cells[NameColumnSelect].ReadOnly)
                {
                    if (
                            (this.dgv.Rows[e.RowIndex].Cells[NameColumnSelect].Value == null) ||
                            (this.dgv.Rows[e.RowIndex].Cells[NameColumnSelect].Value.ToString() == string.Empty)
                       )
                    {
                        this.dgv.Rows[e.RowIndex].Cells[NameColumnSelect].Value = false;
                    }

                    bool CellValue = (bool)this.dgv.Rows[e.RowIndex].Cells[NameColumnSelect].Value;
                    if (CellValue == true)
                    {
                        this.dgv.Rows[e.RowIndex].Cells[NameColumnSelect].Value = false;
                    }
                    else
                    {
                        this.dgv.Rows[e.RowIndex].Cells[NameColumnSelect].Value = true;
                    }
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
        private void TakeOver()
        {
            //check Datensätze ausgeählt?
            this.dtDgvSource.DefaultView.RowFilter = NameColumnSelect + "=True";
            DataTable dtSelected = this.dtDgvSource.DefaultView.ToTable();
            if ((dtSelected.Rows.Count > 0) && (this.asnWizz.AuftragggeberAdr.ID > 0))
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
                    clsJobs.InsertShapeByRefAdr(ListSelectedID
                                                    , (int)this.asnWizz.AuftragggeberAdr.ID
                                                    , this.asnWizz.AuftragggeberAdr.BenutzerID
                                                    , this.ctrMenue._frmMain.system.AbBereich.ID
                                                    , this.ctrMenue._frmMain.system.Mandant.ID);
                }
            }
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
