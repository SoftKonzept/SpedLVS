using LVS;
using LVS.ASN;
using Sped4.Controls.ASNCenter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrASNAction : UserControl
    {
        internal clsASNWizzard asnWizz;

        internal ctrMenu _ctrMenu;
        public Int32 SearchButton = 0;
        public clsADR AdrAuftraggeber;
        public clsADR AdrEmpf;
        internal ctrASNActionSelectToCopy _ctrASNActionSelectToCopy;


        ///<summary>ctrAC_ASNAction / ctrAC_ASNAction</summary>
        ///<remarks></remarks>
        public ctrASNAction(ctrMenu myMenu, clsASNWizzard myAsnWizz)
        {
            InitializeComponent();
            _ctrMenu = myMenu;
            asnWizz = myAsnWizz;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrASNAction_Load(object sender, EventArgs e)
        {
            if (
                (this._ctrMenu is ctrMenu) && (this.asnWizz is clsASNWizzard)
              )
            {
                //comboArbeitsbereich
                cbArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(_ctrMenu._frmMain.system._GL_User.User_ID);
                cbArbeitsbereich.DisplayMember = "Arbeitsbereich";
                cbArbeitsbereich.ValueMember = "ID";

                //comboMandanden
                cbMandant.DataSource = clsMandanten.GetMandatenList(_ctrMenu._frmMain.system._GL_User.User_ID);
                cbMandant.DisplayMember = "Matchcode";
                cbMandant.ValueMember = "Mandanten_ID";

                //comboASNAktion
                cbASNAction.DataSource = clsASNAction.GetASNAktionList();
                cbASNAction.DisplayMember = "Aktion";
                cbASNAction.ValueMember = "ID";

                //ComboASNTyp
                cbASNTyp.DataSource = clsASNTyp.GetASNTypList(_ctrMenu._frmMain.system._GL_User.User_ID);
                cbASNTyp.DisplayMember = "Typ";
                cbASNTyp.ValueMember = "TypID";

                ClearInputFields();
                InitDGVASNAktion();
            }
        }
        ///<summary>ctrAC_ASNAction / InitDGVASNAktion</summary>
        ///<remarks></remarks>
        private void InitDGVASNAktion()
        {
            this.asnWizz.AsnAction.AdrAuftraggeber = this.asnWizz.AuftragggeberAdr;
            this.dgvASNAction.DataSource = this.asnWizz.AsnAction.ListAsnActionByAdr;

            for (Int32 i = 0; i <= this.dgvASNAction.Columns.Count - 1; i++)
            {
                string colName = this.dgvASNAction.Columns[i].Name.ToString();
                switch (colName)
                {
                    case "ASNActionProcessNr":
                        this.dgvASNAction.Columns[i].HeaderText = "Pr-Nr";
                        this.dgvASNAction.Columns[i].IsVisible = true;
                        break;
                    case "ASNActionName":
                    case clsASNAction.const_DBColName_ActionName:
                        this.dgvASNAction.Columns[i].HeaderText = "Bezeichnung";
                        this.dgvASNAction.Columns[i].IsVisible = true;
                        break;

                    case clsASNAction.const_DBColName_ID:
                    case clsASNAction.const_DBColName_Auftraggeber:
                    case clsASNAction.const_DBColName_Empfaenger:
                    case clsASNAction.const_DBColName_ASNTypID:
                    case clsASNAction.const_DBColName_OrderID:
                    case "MandantenID":
                    case clsASNAction.const_DBColName_AbBereichID:
                    case clsASNAction.const_DBColName_Bemerkung:
                    case clsASNAction.const_DBColName_activ:
                    case clsASNAction.const_DBColName_IsVirtFile:
                    case clsASNAction.const_DBColName_UseOldPropertyValue:

                        this.dgvASNAction.Columns[i].IsVisible = true;
                        break;

                    default:
                        this.dgvASNAction.Columns[i].IsVisible = false;
                        break;
                }
            }
            this.dgvASNAction.BestFitColumns();
        }
        ///<summary>ctrAC_ASNAction / ClearInputFields</summary>
        ///<remarks></remarks>
        private void ClearInputFields()
        {
            tbSearchE.Text = string.Empty;
            tbEmpfaenger.Text = string.Empty;
            tbBemerkung.Text = string.Empty;
            cbArbeitsbereich.SelectedIndex = -1;
            cbASNAction.SelectedIndex = -1;
            cbASNTyp.SelectedIndex = -1;
            cbMandant.SelectedIndex = -1;
            cbAktiv.Checked = false;
            cbVirtFile.Checked = false;
            nudAdrIdDirect.Value = 0;
            nudOrderID.Value = 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchE_Click(object sender, EventArgs e)
        {
            SearchButton = 3;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void SetADRAfterADRSearch(decimal myDecADR_ID)
        {
            this.asnWizz.AsnAction.AdrEmpfaenger.ID = myDecADR_ID;
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
        private void dgvASNAction_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvASNAction.Rows.Count > 0)
            {
                this.dgvASNAction.CurrentRow.IsSelected = true;
            }
        }
        /// <summary>     
        ///             Setzt die ausgewählten Werte in die Eingabefelder 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvASNAction_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearInputFields();
            SetASNActionClassValueToCtr();
            SetAsnActionInputFieldEnabled(true);
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetASNActionClassValueToCtr()
        {
            if (dgvASNAction.RowCount > 0)
            {
                if (dgvASNAction.SelectedRows.Count > 0)
                {
                    decimal decTmp = 0;
                    decimal.TryParse(dgvASNAction.SelectedRows[0].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.asnWizz.AsnAction.ID = decTmp;
                        this.asnWizz.AsnAction.Fill();

                        this.tbSearchE.Text = this.asnWizz.AsnAction.AdrEmpfaenger.ViewID;
                        this.tbEmpfaenger.Text = this.asnWizz.AsnAction.AdrEmpfaenger.ADRStringShort;
                        this.cbAktiv.Checked = this.asnWizz.AsnAction.activ;
                        this.cbVirtFile.Checked = this.asnWizz.AsnAction.IsVirtFile;
                        Functions.SetComboToSelecetedValue(ref cbArbeitsbereich, this.asnWizz.AsnAction.AbBereichID.ToString());
                        Functions.SetComboToSelecetedValue(ref cbMandant, this.asnWizz.AsnAction.MandantenID.ToString());
                        Functions.SetComboToSelecetedValue(ref cbASNAction, this.asnWizz.AsnAction.ASNActionProcessNr.ToString());
                        Functions.SetComboToSelecetedValue(ref cbASNTyp, this.asnWizz.AsnAction.ASNTypID.ToString());
                        nudOrderID.Value = (decimal)this.asnWizz.AsnAction.OrderID;
                        tbBemerkung.Text = this.asnWizz.AsnAction.Bemerkung;
                        this.cbUseOldPropertyValue.Checked = this.asnWizz.AsnAction.UseOldPropertyValue;
                    }
                }
            }
        }
        /// <summary>
        ///             Füllt die Werte der Klasse anhand der Eingabefelder
        /// </summary>
        private void SetValueToASNActionCls(bool myIsGlobalAction)
        {
            try
            {
                if (myIsGlobalAction)
                {
                    this.asnWizz.AsnAction.Empfaenger = 0;
                }
                else
                {
                    this.asnWizz.AsnAction.Empfaenger = this.asnWizz.AsnAction.AdrEmpfaenger.ID;
                }

                this.asnWizz.AsnAction.activ = cbAktiv.Checked;
                this.asnWizz.AsnAction.IsVirtFile = cbVirtFile.Checked;
                this.asnWizz.AsnAction.AbBereichID = (decimal)cbArbeitsbereich.SelectedValue;
                this.asnWizz.AsnAction.MandantenID = (decimal)cbMandant.SelectedValue;
                decimal decTmp = 0;
                if (decimal.TryParse(cbASNTyp.SelectedValue.ToString(), out decTmp))
                {

                }
                this.asnWizz.AsnAction.ASNTypID = decTmp;
                this.asnWizz.AsnAction.ASNActionProcessNr = (int)cbASNAction.SelectedValue;
                this.asnWizz.AsnAction.ASNActionName = cbASNAction.Text.ToString();
                this.asnWizz.AsnAction.Bemerkung = tbBemerkung.Text;
                this.asnWizz.AsnAction.OrderID = (int)nudOrderID.Value;
                this.asnWizz.AsnAction.UseOldPropertyValue = cbUseOldPropertyValue.Checked;
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        ///             aktivieren / deaktivieren der Eingabefelder
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetAsnActionInputFieldEnabled(bool bEnabled)
        {
            btnSearchE.Enabled = bEnabled;
            tbSearchE.Enabled = bEnabled;
            tbEmpfaenger.Enabled = bEnabled;
            tbBemerkung.Enabled = bEnabled;
            cbArbeitsbereich.Enabled = bEnabled;
            cbASNAction.Enabled = bEnabled;
            cbASNTyp.Enabled = bEnabled;
            cbMandant.Enabled = bEnabled;
        }
        /// <summary>
        ///            Neuen Datensatz anlegen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAsnActionAdd_Click(object sender, EventArgs e)
        {
            this.asnWizz.AsnAction = new clsASNAction();
            this.asnWizz.AsnAction.InitClass(ref _ctrMenu._frmMain.GL_User);
            ClearInputFields();
            SetAsnActionInputFieldEnabled(true);
        }
        /// <summary>
        ///             Datensatz speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAsnActionSave_Click(object sender, EventArgs e)
        {
            SaveAction(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myIsGlobalAction"></param>
        private void SaveAction(bool myIsGlobalAction)
        {
            SetValueToASNActionCls(myIsGlobalAction);
            if (this.asnWizz.AsnAction.ID > 0)
            {
                this.asnWizz.AsnAction.Update();
            }
            else
            {
                this.asnWizz.AsnAction.Add();
            }
            InitDGVASNAktion();
            ClearInputFields();
            SetAsnActionInputFieldEnabled(false);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAsnActionDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                string strTxt = string.Empty;
                if (this.asnWizz.AsnAction.Delete())
                {
                    strTxt = "Der Datensatz wurde gelöscht!";
                }
                else
                {
                    strTxt = "Der Lösch-Vorgang konnte nicht durchgeführt werden!";
                }
                clsMessages.Allgemein_InfoTextShow(strTxt);
                InitDGVASNAktion();
            }
        }
        /// <summary>
        ///             Daten DGV neu laden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAsnActionRefresh_Click(object sender, EventArgs e)
        {
            InitDGVASNAktion();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnASNActionCopy_Click(object sender, EventArgs e)
        {
            if (asnWizz.AsnAction is clsASNAction)
            {
                this.asnWizz.AsnAction.ID = 0;
                if (this.asnWizz.AsnAction.ID == 0)
                {
                    this.asnWizz.AsnAction.Add();
                    InitDGVASNAktion();
                    ClearInputFields();
                    SetAsnActionInputFieldEnabled(false);
                }
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnASNActionDefaultAddRange_Click(object sender, EventArgs e)
        {
            if (clsMessages.DoProzess())
            {
                clsASNAction defAction = new clsASNAction();
                defAction.InitClass(ref this._ctrMenu.GL_User);
                defAction.CreateDefaultActionRange(this.asnWizz);
                InitDGVASNAktion();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddASNActionRange_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            //this.IsReceiverSearch = false;
            _ctrMenu.OpenADRSearch(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDecADR_ID"></param>
        public void TakeOverAdrID(decimal myDecADR_ID)
        {
            this.asnWizz.AsnAction.Empfaenger = myDecADR_ID;
            this._ctrASNActionSelectToCopy = new ctrASNActionSelectToCopy(this._ctrMenu, this.asnWizz);
            this._ctrMenu.OpenFrmTMP(this._ctrASNActionSelectToCopy);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySourceAdrId"></param>
        /// <param name="myDestAdrId"></param>
        /// <param name="myUserId"></param>
        /// <returns></returns>
        public static bool InsertShapeByRefAdr(List<int> myListID, int myDestAdrId, decimal myUserId, decimal myAbBereichID)
        {
            bool bReturn = false;
            //if (!clsVDAClientValue.ExistValue(myDestAdrId, myUserId))
            //{
            //Eintrag
            string strSql = string.Empty;
            strSql = " INSERT INTO [dbo].[ASNAction] ([ActionASN],[ActionName],[Auftraggeber],[Empfaenger],[OrderID],[MandantID] " +
                                                    ",[AbBereichID],[ASNTypID],[Bemerkung],[activ] ,[IsVirtFile]) " +

                     "SELECT " +
                            "a.ActionASN " +
                            ",a.ActionName " +
                            ", " + myDestAdrId +
                            ", a.Receiver " +
                            ", a.ASNField " +
                            ", a.ArtField" +
                            ", a.IsDefValue " +
                            ", a.DefValue " +
                            ", a.CopyToField " +
                            ", a.FormatFunction " +
                            // ", a.AbBereichID " +
                            ", " + (int)myAbBereichID +
                                " FROM ASNArtFieldAssignment a " +
                                    "WHERE " +
                                    " a.ID in (" + string.Join(",", myListID.ToArray()) + ");";
            //"a.Sender =" + mySourceAdrId + "; ";
            bReturn = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "InsertASNArtFieldAssignment", myUserId);
            //}
            return bReturn;
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
                TakeOverAdrID(nudAdrIdDirect.Value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetGlobalAction_Click(object sender, EventArgs e)
        {
            SaveAction(true);
        }
    }
}
