using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrExtraChargeAssignment : UserControl
    {
        public Globals._GL_USER GLUser;
        internal clsExtraCharge ExtraCharge;

        public clsExtraChargeAssignment ExtraChargeAssignment;
        internal ctrMenu ctrMenu;
        internal clsLager Lager;
        internal ctrEinlagerung ctrEinlagerung;
        internal ctrArbeitsliste ctrArbeitsliste = null;

        public clsExtraChargeADR ExtraChargeADR;

        public bool bExtraChargeAssignmentForArt;
        internal bool bUpdate;
        internal frmTmp _frmTmp;
        internal decimal ArtikelTableID;
        internal decimal LEingangTableID;

        public decimal AdrID { get; set; }

        ///<summary>ctrExtraChargeAssignment / ctrExtraChargeAssignmentl</summary>
        ///<remarks></remarks>
        public ctrExtraChargeAssignment()
        {
            InitializeComponent();
        }
        ///<summary>ctrExtraChargeAssignment / ctrExtraChargeAssignment_Load</summary>
        ///<remarks></remarks>
        private void ctrExtraChargeAssignment_Load(object sender, EventArgs e)
        {
            //Einheit
            cbEinheit.DataSource = clsEinheiten.GetEinheiten(this.GLUser);
            cbEinheit.DisplayMember = "Bezeichnung";
            cbEinheit.ValueMember = "Bezeichnung";

            //Eingang oder Artikel
            string strLabel = string.Empty;
            if (bExtraChargeAssignmentForArt)
            {
                strLabel = "Artikel ID / LVSNR:";
                if (ctrArbeitsliste != null)
                {
                    clsArtikel art = new clsArtikel();
                    art.ID = decimal.Parse(this.ctrArbeitsliste.dgvArbeitsliste.SelectedRows[0].Cells["ArtikelID"].Value.ToString());
                    art.GetArtikeldatenByTableID();
                    tbID.Text = art.ID.ToString() + " / " + art.LVS_ID.ToString();
                    this.ArtikelTableID = art.ID;
                    this.LEingangTableID = art.LEingangTableID;
                    ExtraCharge.AdrID = this.AdrID;

                }
                else
                {

                    tbID.Text = this.Lager.Artikel.ID.ToString() + " / " + this.Lager.Artikel.LVS_ID.ToString();
                    this.ArtikelTableID = this.Lager.Artikel.ID;
                    this.LEingangTableID = this.Lager.LEingangTableID;
                }
            }
            else
            {
                strLabel = "Eingang-ID / Eingang-Nr:";
                tbID.Text = this.Lager.Eingang.LEingangTableID.ToString() + " / " + this.Lager.Eingang.LEingangID.ToString();
                this.ArtikelTableID = 0;
                this.LEingangTableID = this.Lager.LEingangTableID;
            }

            lID.Text = strLabel;

            //ExtraCharge
            InitDGVExtraCharge(true);
            //ExtraCHargeAssignemtn
            InitDGVExtraChargeAssignment();
        }
        ///<summary>ctrExtraChargeAssignment / InitGlobals</summary>
        ///<remarks></remarks>
        public void InitGlobals(ctrMenu myCtrMenu)
        {
            this.GLUser = myCtrMenu.GL_User;
            this.ctrMenu = myCtrMenu;

            ExtraCharge = new clsExtraCharge();
            ExtraCharge.InitClass(this.GLUser);
            ExtraCharge.ArbeitsbereichID = this.ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID;

            ExtraChargeAssignment = new clsExtraChargeAssignment();
            ExtraChargeAssignment.InitClass(this.GLUser);
        }
        ///<summary>ctrExtraChargeAssignment / InitDGVExtraCharge</summary>
        ///<remarks></remarks>
        private void InitDGVExtraCharge(bool IsWorklist = false)
        {
            DataTable dt = ExtraCharge.GetExtraChargeList();
            this.dgvExtraCharge.DataSource = dt;
            //Spalten ausschalten
            if (this.dgvExtraCharge.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvExtraCharge.Columns.Count - 1; i++)
                {
                    string colName = this.dgvExtraCharge.Columns[i].Name.ToString();
                    switch (colName)
                    {
                        case "ID":
                        case "IsGlobal":
                        case "erstellt":
                        case "RGText":
                        case "ArbeitsbereichID":
                        case "Einheit":
                        case "Preis":
                        case "UserID":
                        case "AdrID":
                            this.dgvExtraCharge.Columns[i].IsVisible = false;
                            break;

                    }
                    this.dgvExtraCharge.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
                }
                this.dgvExtraCharge.BestFitColumns();

                //SetSelected and Current Row
                for (Int32 i = 0; i <= this.dgvExtraCharge.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(this.dgvExtraCharge.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp == ExtraCharge.ID)
                    {
                        this.dgvExtraCharge.Rows[i].IsSelected = true;
                        this.dgvExtraCharge.Rows[i].IsCurrent = true;
                    }
                }
            }
        }
        ///<summary>ctrExtraChargeAssignment / InitDGVExtraChargeAssignment</summary>
        ///<remarks></remarks>
        private void InitDGVExtraChargeAssignment()
        {
            DataTable dt = ExtraChargeAssignment.GetExtraChargeAssignmentList(this.bExtraChargeAssignmentForArt, this.ArtikelTableID, this.LEingangTableID);
            this.dgvExtraChargeAssignment.DataSource = dt;
            if (this.dgvExtraChargeAssignment.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvExtraChargeAssignment.Columns.Count - 1; i++)
                {
                    string colName = this.dgvExtraChargeAssignment.Columns[i].Name.ToString();
                    switch (colName)
                    {
                        case "ID":
                        case "ExtraChargeID":
                            this.dgvExtraChargeAssignment.Columns[i].IsVisible = false;
                            break;

                        case "LEingangID":
                            this.dgvExtraChargeAssignment.Columns[i].HeaderText = "Eingang";
                            this.dgvExtraChargeAssignment.Columns.Move(i, 0);
                            break;

                        case "Datum":
                            this.dgvExtraChargeAssignment.Columns[i].FormatString = "{0:d}";
                            break;
                    }
                    this.dgvExtraChargeAssignment.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
                }
                this.dgvExtraChargeAssignment.BestFitColumns();
            }
        }
        ///<summary>ctrExtraChargeAssignment / tsbnExtraChargeEditClose_Click</summary>
        ///<remarks></remarks>
        private void tsbnExtraChargeEditClose_Click(object sender, EventArgs e)
        {
            this.ctrEinlagerung.InitDGVExtraChargeAssignment();
            this.ctrEinlagerung.InitGrdArtVita();
            this._frmTmp.CloseFrmTmp();
        }
        ///<summary>ctrExtraChargeAssignment / ClearExtraChargeAssignmentInputFields</summary>
        ///<remarks></remarks>
        private void ClearExtraChargeAssignmentInputFields()
        {
            tbECAssSonderkostenbezeichnung.Text = string.Empty;
            nudECAssPreis.Value = 0;
            nudECAssMenge.Value = 1;
            dtpECAssDatum.Value = DateTime.Now;
            cbEinheit.SelectedIndex = 0;
            tbECAssRGText.Text = string.Empty;
        }
        ///<summary>ctrExtraChargeAssignment / SetExtraChargeAssignmentInputFieldsEnabled</summary>
        ///<remarks>Bezeichnung und ID sollen nicht veränderbar sein. Alle anderen Kosten schon.</remarks>
        private void SetExtraChargeAssignmentInputFieldsEnabled(bool bEnabled)
        {
            nudECAssPreis.Enabled = bEnabled;
            nudECAssMenge.Enabled = bEnabled;
            dtpECAssDatum.Enabled = bEnabled;
        }
        ///<summary>ctrExtraChargeAssignment / SetExtraChargeDataToCtr</summary>
        ///<remarks>Setz die Daten aus der ExtraCharge Daten aufs CTR</remarks>
        private void SetExtraChargeDataToCtr()
        {
            tbECAssSonderkostenbezeichnung.Text = ExtraCharge.Bezeichnung;
            Functions.SetComboToSelecetedValue(ref cbEinheit, ExtraCharge.Einheit);

            clsExtraChargeADR ExtraChargeADR = new clsExtraChargeADR();
            ExtraChargeADR.ExtraChargeID = ExtraCharge.ID;
            ExtraChargeADR.AdrID = Lager.Eingang.Auftraggeber;
            ExtraChargeADR.Fill();
            nudECAssPreis.Value = ExtraChargeADR.Preis;

            clsArtikel art = new clsArtikel();
            art.ID = ArtikelTableID;
            art.GetArtikeldatenByTableID();

            if (ExtraCharge.ID > 0)
            {
                if (ExtraCharge.Einheit.Equals("kg") || ExtraCharge.Einheit.Equals("to"))
                    nudECAssMenge.Value = art.Brutto;
                else
                    nudECAssMenge.Value = 1;
            }
            else
            {
                nudECAssMenge.Value = 1;
            }
            // Kundenbezogener Preis einbauen

            //tbECAssPreis.Text = Functions.FormatDecimal();
            tbECAssRGText.Text = ExtraCharge.RGText;
        }
        ///<summary>ctrExtraChargeAssignment / SetExtraChargeAssignmentDataToCtr</summary>
        ///<remarks></remarks>
        private void SetExtraChargeAssignmentDataToCtr()
        {
            tbECAssSonderkostenbezeichnung.Text = ExtraChargeAssignment.ExtraCharge.Bezeichnung;
            Functions.SetComboToSelecetedValue(ref cbEinheit, ExtraChargeAssignment.Einheit);
            nudECAssPreis.Value = ExtraChargeAssignment.Preis;
            tbECAssRGText.Text = ExtraChargeAssignment.RGText;
            dtpECAssDatum.Value = ExtraChargeAssignment.Datum;
            Decimal decTmp = 0;
            Decimal.TryParse(ExtraChargeAssignment.Menge.ToString(), out decTmp);
            nudECAssMenge.Value = decTmp;
        }
        ///<summary>ctrExtraChargeAssignment / ClearExtraChargeAssignmentInputFields</summary>
        ///<remarks></remarks>
        private void dgvExtraCharge_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bUpdate = false;
            SetExtraChargeDataToCtr();
            SetExtraChargeAssignmentInputFieldsEnabled(true);
        }
        ///<summary>ctrExtraChargeAssignment / dgvExtraCharge_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvExtraCharge_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvExtraCharge.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvExtraCharge.Rows[this.dgvExtraCharge.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    ExtraCharge.ID = decTmp;
                    ExtraCharge.Fill();
                    this.dgvExtraCharge.CurrentRow.IsSelected = true;
                }
            }
        }
        ///<summary>ctrExtraChargeAssignment / tsbtnECAssSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnECAssSave_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;
            if (tbECAssSonderkostenbezeichnung.Text == string.Empty)
            {
                strError += "Es wurden keine Nebenkosten ausgewählt!\n";
            }
            if (nudECAssMenge.Value <= 0)
            {
                strError += "Es wurde keine gültige Menge eingegeben!\n";
            }

            if (strError == string.Empty)
            {
                Decimal decTmpECAssID = ExtraChargeAssignment.ID;
                ExtraChargeAssignment = new clsExtraChargeAssignment();
                ExtraChargeAssignment.InitClass(this.GLUser);

                Int32 iTmp = 1;
                Int32.TryParse(nudECAssMenge.Value.ToString(), out iTmp);
                decimal decTmp1 = 1;
                Decimal.TryParse(nudECAssMenge.Value.ToString(), out decTmp1);
                if (bUpdate)
                {
                    ExtraChargeAssignment.ID = decTmpECAssID;
                    ExtraChargeAssignment.Einheit = cbEinheit.SelectedValue.ToString();
                    ExtraChargeAssignment.Preis = nudECAssPreis.Value;
                    ExtraChargeAssignment.Menge = iTmp;
                    ExtraChargeAssignment.RGText = tbECAssRGText.Text.ToString().Trim();
                    ExtraChargeAssignment.Datum = dtpECAssDatum.Value;

                    ExtraChargeAssignment.Update();
                }
                else
                {
                    ExtraChargeAssignment.Add(ExtraCharge, this.bExtraChargeAssignmentForArt, this.ArtikelTableID, this.LEingangTableID, Convert.ToInt32(decTmp1), this.dtpECAssDatum.Value);
                }
                ClearExtraChargeAssignmentInputFields();
                SetExtraChargeAssignmentInputFieldsEnabled(false);
                bUpdate = false;
                InitDGVExtraChargeAssignment();
                if (ctrEinlagerung != null)
                {
                    ctrEinlagerung.InitDGVExtraChargeAssignment();
                }
            }
            else
                clsMessages.Allgemein_ERRORTextShow(strError);
        }
        ///<summary>ctrExtraChargeAssignment / dgvExtraChargeAssignment_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvExtraChargeAssignment_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvExtraChargeAssignment.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvExtraChargeAssignment.Rows[this.dgvExtraChargeAssignment.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    ExtraChargeAssignment.ID = decTmp;
                    ExtraChargeAssignment.Fill();
                    this.dgvExtraChargeAssignment.CurrentRow.IsSelected = true;
                }
            }
        }
        ///<summary>ctrExtraChargeAssignment / dgvExtraChargeAssignment_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvExtraChargeAssignment_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bUpdate = true;
            SetExtraChargeAssignmentDataToCtr();
            SetExtraChargeAssignmentInputFieldsEnabled(true);
        }

        private void tsbtnECAssDelete_Click(object sender, EventArgs e)
        {
            if (this.ExtraChargeAssignment.ID > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    this.ExtraChargeAssignment.Delete();
                    InitDGVExtraChargeAssignment();
                    if (ctrEinlagerung != null)
                    {
                        ctrEinlagerung.InitDGVExtraChargeAssignment();
                    }
                }
            }
        }
    }
}
