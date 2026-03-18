using LVS;
using LVS.ASN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Sped4.Controls.ASNCenter
{
    public partial class ctrASNActionSelectToCopy : UserControl
    {
        public int SearchButton = 0;
        internal ctrMenu _ctrMenue;
        internal clsASNWizzard asnWizz;
        internal DataTable dtDgvSource;
        public frmTmp _frmTmp;
        internal const string NameColumnSelect = "Selected";

        public ctrASNActionSelectToCopy(ctrMenu myMenu, clsASNWizzard myAsnWizz)
        {
            InitializeComponent();
            this._ctrMenue = myMenu;
            this.asnWizz = myAsnWizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrASNActionSelectToCopy_Load(object sender, EventArgs e)
        {
            SetADRAfterADRSearch(this.asnWizz.AsnAction.Empfaenger);
            InitDgv();
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
            if ((dtSelected.Rows.Count > 0) && (this.asnWizz.AsnAction.Empfaenger > 0))
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
                    clsASNAction.InsertShapeByRefAdr(ListSelectedID
                                                    , (int)this.asnWizz.AuftragggeberAdr.ID
                                                    , (int)this.asnWizz.AsnAction.Empfaenger
                                                    , this.asnWizz.AuftragggeberAdr.BenutzerID
                                                    , this.asnWizz.AsnAction.GL_System.sys_ArbeitsbereichID
                                                    , this.asnWizz.AsnAction.GL_System.sys_MandantenID);
                }
            }
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
        /// <summary>
        /// 
        /// </summary>
        private void InitDgv()
        {
            dtDgvSource = new DataTable();
            dtDgvSource = clsASNAction.GetASNActionByAuftraggeber(this._ctrMenue.GL_User, this._ctrMenue._frmMain.system, this.asnWizz.AsnAction.Empfaenger);
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
        private void btnSearchE_Click(object sender, EventArgs e)
        {
            SearchButton = 3;
            _ctrMenue.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void SetADRAfterADRSearch(decimal myDecADR_ID)
        {
            this.asnWizz.AsnAction.Empfaenger = myDecADR_ID;
            this.asnWizz.AsnAction.AdrEmpfaenger.ID = this.asnWizz.AsnAction.Empfaenger;
            this.asnWizz.AsnAction.AdrEmpfaenger.FillClassOnly();
            this.asnWizz.AsnAction.AdrAuftraggeber = this.asnWizz.AuftragggeberAdr;
            this.asnWizz.AsnAction.Auftraggeber = this.asnWizz.AsnAction.AdrAuftraggeber.ID;
            this.tbSearchE.Text = this.asnWizz.AsnAction.AdrEmpfaenger.ViewID;
            this.tbEmpfaenger.Text = this.asnWizz.AsnAction.AdrEmpfaenger.ADRStringShort;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudAdrIdDirect_Leave(object sender, EventArgs e)
        {
            if (nudAdrIdDirect.Value > 0)
            {
                SetADRAfterADRSearch(nudAdrIdDirect.Value);
            }
        }
    }
}
