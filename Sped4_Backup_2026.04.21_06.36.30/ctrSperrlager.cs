using LVS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;


namespace Sped4
{
    public partial class ctrSperrlager : UserControl
    {
        public Globals._GL_USER GL_User;
        internal ctrMenu _ctrMenu;
        internal clsLager Lager = new clsLager();
        internal DataTable dtSPL = new DataTable("SPL");
        internal Int32 SearchButton = 0;
        internal decimal BestandADRID = 0;
        internal clsASNTransfer AsnTransfer = new clsASNTransfer();



        /*************************************************************************
        *                           Methoden / Procedure
        **************************************************************************/
        ///<summary>ctrSperrlager / ctrSperrlager</summary>
        ///<remarks></remarks>
        public ctrSperrlager()
        {
            InitializeComponent();
        }
        ///<summary>ctrSperrlager / ctrBestand_Load</summary>
        ///<remarks></remarks>
        private void ctrSperrlager_Load(object sender, EventArgs e)
        {
            SetAuswahlBestandDaten();
            InitDGV();
            CustomizedCtr();
        }
        ///<summary>ctrSperrlager / CustomizedCtr</summary>
        ///<remarks></remarks>
        private void CustomizedCtr()
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Lager_SPL_RebookInAltEingang)
            {
                cbReebookInOldEingang.Checked = true;
            }
            this._ctrMenu._frmMain.system.Client.ctrSperrlager_CustomizeChangeReebookInOldEingangSetEnabled(ref cbReebookInOldEingang);
            this._ctrMenu._frmMain.system.Client.ctrSperrlager_CustomizeSETCheckOutButtonVisible(ref this.tsbtnCheckOut);
        }
        ///<summary>ctrSperrlager / SetAuswahlJournalDaten</summary>
        ///<remarks>Setzt die Standardwerte</remarks>
        private void SetAuswahlBestandDaten()
        {
            //Datum
            string strTmp = "01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
            dtpVon.Value = Convert.ToDateTime(strTmp);
            dtpBis.Value = DateTime.Now.Date.AddDays(1);
        }
        ///<summary>ctrSperrlager / InitDGV</summary>
        ///<remarks></remarks>
        public void InitDGV()
        {
            Lager = new clsLager();
            Lager.InitSubClasses();
            Lager._GL_User = this.GL_User;
            Lager.BestandVon = dtpVon.Value;
            Lager.BestandBis = dtpBis.Value;
            Lager.AbBereichID = this._ctrMenu._frmMain.system.AbBereich.ID;

            dtSPL.Clear();
            dtSPL = Lager.GetSperrlager();
            //neu grid
            this.dgvSPL.DataSource = dtSPL.DefaultView;
            if (this.dgvSPL.Rows.Count > 0)
            {
                FillComboAuftraggeber();
                //TestSearch
                Functions.InitComboSearch(ref tscbSearch, dtSPL, this._ctrMenu._frmMain.system);
                Functions.setView(ref dtSPL, ref dgvSPL, clsClient.const_ViewName_SPL, clsClient.const_ViewKategorie_SPL, this._ctrMenu._frmMain.GL_System, false);
                dtSPL.DefaultView.RowFilter = string.Empty;
                this.dgvSPL.BestFitColumns();
                //Berechnung Bestanddaten
                CalcSumGridDaten();
            }
        }
        ///<summary>ctrSperrlager / dgvSPL_CreateCell</summary>
        ///<remarks></remarks>
        private void dgvSPL_CreateCell(object sender, Telerik.WinControls.UI.GridViewCreateCellEventArgs e)
        {
            e.Column.ReadOnly = true;
        }
        ///<summary>ctrSperrlager / CalcSumGridDaten</summary>
        ///<remarks></remarks>
        private void CalcSumGridDaten()
        {
            Int32 iTmp = 0;
            decimal decTmpNetto = 0;
            decimal decTmpBrutto = 0;
            if (dtSPL.Rows.Count > 0)
            {
                DataTable dtTmp = dtSPL.DefaultView.ToTable(true, "ArtikelID", "Netto", "Brutto");
                if (dtTmp.Rows.Count > 0)
                {
                    object objAnzahl = dtTmp.Rows.Count;
                    object objNetto = dtTmp.Compute("SUM(Netto)", "");
                    object objBrutto = dtTmp.Compute("SUM(Brutto)", "");
                    Int32.TryParse(objAnzahl.ToString(), out iTmp);
                    decimal.TryParse(objNetto.ToString(), out decTmpNetto);
                    decimal.TryParse(objBrutto.ToString(), out decTmpBrutto);
                }
            }
            tbAnzahl.Text = iTmp.ToString();
            tbNetto.Text = Functions.FormatDecimal(decTmpNetto);
            tbBrutto.Text = Functions.FormatDecimal(decTmpBrutto);
        }
        ///<summary>ctrSperrlager / cbSchaden_CheckedChanged</summary>
        ///<remarks></remarks>
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            InitDGV();
        }
        ///<summary>ctrSperrlager / tsbtnClear_Click</summary>
        ///<remarks>löscht alle Vorgaben und setzt alle Ctr auf den ursprungszustand zurück</remarks>
        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            SetAuswahlBestandDaten();
            InitDGV();
        }
        ///<summary>ctrSperrlager / tsbtnClose_Click</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrSperrlager();
        }
        ///<summary>ctrSperrlager / FillComboAuftraggeber</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void FillComboAuftraggeber()
        {
            if (dtSPL.Rows.Count > 0)
            {
                DataTable dtTmp = dtSPL.DefaultView.ToTable(true, "Auftraggeber");
                dtTmp.DefaultView.Sort = "Auftraggeber";
                cbAuftraggeber.DataSource = null;
                cbAuftraggeber.DisplayMember = "Auftraggeber";
                cbAuftraggeber.ValueMember = "Auftraggeber";
                cbAuftraggeber.DataSource = dtTmp.DefaultView;
                cbAuftraggeber.SelectedIndex = -1;
            }
        }
        ///<summary>ctrSperrlager / FillComboAuftraggeber</summary>
        ///<remarks>Ctr schliessen</remarks>
        private void cbAuftraggeber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dgvSPL.Rows.Count > 0)
            {
                string strFilter = "Auftraggeber LIKE '" + cbAuftraggeber.Text + "'";
                dtSPL.DefaultView.RowFilter = string.Empty;
                dtSPL.DefaultView.RowFilter = strFilter;
                CalcSumGridDaten();
            }
        }
        ///<summary>ctrSperrlager / dgvSPL_CellClick</summary>
        ///<remarks>Erfassen der auszubuchenden Artikel</remarks>
        private void dgvSPL_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if ((this.dgvSPL.Rows.Count > 0) && (e.RowIndex >= 0))
            {
                decimal decTmp = 0;
                string strArtID = this.dgvSPL.Rows[e.RowIndex].Cells["ArtikelID"].Value.ToString();
                Decimal.TryParse(strArtID, out decTmp);
                if (decTmp > 0)
                {
                    this.Lager.Artikel.ID = decTmp;
                    this.Lager.Artikel.GetArtikeldatenByTableID();
                }

                if (this.dgvSPL.Columns[e.ColumnIndex].Name.Equals("ausbuchen"))
                {
                    if (cbAuftraggeber.SelectedIndex > -1)
                    {
                        if ((bool)this.dgvSPL.Rows[e.RowIndex].Cells["ausbuchen"].Value == true)
                        {
                            this.dgvSPL.Rows[e.RowIndex].Cells["ausbuchen"].Value = false;
                        }
                        else
                        {
                            this.dgvSPL.Rows[e.RowIndex].Cells["ausbuchen"].Value = true;
                        }
                    }
                    else
                    {
                        this.dgvSPL.Rows[e.RowIndex].Cells["ausbuchen"].Value = false;
                        clsMessages.Allgemein_ERRORTextShow("Es wurde kein Auftraggeber ausgewählt.");
                    }
                }
            }
        }
        ///<summary>ctrSperrlager / tsbtnCheckOut_Click</summary>
        ///<remarks>ausgewählte Artikel zurück in den Bestand buchen.
        ///         - SPL ausbuchen
        ///         - neuen/alten Eingang erstellen
        ///         - neue/alte LVSNr
        ///         </remarks>
        private void tsbtnCheckOut_Click(object sender, EventArgs e)
        {
            DataTable dtReBook = dtSPL.DefaultView.ToTable();
            if (dtReBook.Rows.Count > 0)
            {
                Lager.InitSubClasses();
                Lager.SPL._GL_User = this.GL_User;
                Lager.SPL.dtCheckOut = dtReBook;
                if (cbReebookInOldEingang.Checked)
                {
                    Lager.SPL.DoSPLCheckOutInOldEingang();
                }
                else
                {
                    Lager.SPL.DoSPLCheckOutInNEWEingnag();
                }
                InitASNTransfer(clsASNAction.const_ASNAction_SPLOut);
                SetAuswahlBestandDaten();
                InitDGV();
            }
        }
        ///<summary>ctrSperrlager / tstbSearchArtikel_TextChanged</summary>
        ///<remarks></remarks>
        private void tstbSearchArtikel_TextChanged(object sender, EventArgs e)
        {
        }
        ///<summary>ctrSperrlager / tsbtnStartSearch_Click</summary>
        ///<remarks></remarks>
        private void tsbtnStartSearch_Click(object sender, EventArgs e)
        {
            if (tscbSearch.SelectedIndex > -1)
            {
                tstbSearchArtikel.Text = tstbSearchArtikel.Text.Trim();
                string strFilter = "";
                strFilter = Functions.GetSearchFilterString(ref tscbSearch, tscbSearch.Text, tstbSearchArtikel.Text);
                dtSPL.DefaultView.RowFilter = strFilter;
            }
        }
        ///<summary>ctrSperrlager / tsbtnRLToSL_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRLToSL_Click(object sender, EventArgs e)
        {
            if (this.dgvSPL.Rows.Count > 0)
            {
                if (this.Lager.Artikel.ID > 0)
                {
                    if (clsMessages.RL_DoRL())
                    {
                        //SPL ausbuchen
                        //this.Lager.InitSubClasses();
                        this.Lager.sys = this._ctrMenu._frmMain.system;
                        this.Lager.SPL._GL_User = this.GL_User;
                        if (this.Lager.DoRL())
                        {
                            //Rücklieferung durchgeführt
                            // --- Lagermeldungen erstellen
                            InitASNTransfer(clsASNAction.const_ASNAction_RücklieferungSL);
                            this.InitDGV();
                        }
                    }
                }
            }
        }
        ///<summary>ctrEinlagerung / CheckDirectPrintLabel</summary>
        ///<remarks>prüft die Freigabe auf den Labeldruck</remarks> 
        private void InitASNTransfer(Int32 myASNAction)
        {
            AsnTransfer = new clsASNTransfer();
            if (AsnTransfer.DoASNTransfer(this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system.AbBereich.ID, this._ctrMenu._frmMain.system.AbBereich.MandantenID))
            {
                if (this._ctrMenu._frmMain.system.Client.Modul.ASN_UserOldASNFileCreation)
                {
                    switch (myASNAction)
                    {
                        case clsASNAction.const_ASNAction_RücklieferungSL:
                            decimal decTmp = 0;
                            Decimal.TryParse(this.dgvSPL.Rows[this.dgvSPL.CurrentRow.Index].Cells["ArtikelID"].Value.ToString(), out decTmp);
                            this.Lager.Artikel.ID = decTmp;
                            this.Lager.Artikel.GetArtikeldatenByTableID();
                            this.Lager.ASNAction.ASNActionProcessNr = myASNAction;
                            if (this.Lager.ASNAction.ASNActionProcessNr > 0)
                            {
                                AsnTransfer.CreateLM(ref this.Lager);
                            }
                            break;

                        case clsASNAction.const_ASNAction_SPLOut:
                            for (Int32 i = 0; i <= this.Lager.SPL.dtCheckOut.Rows.Count - 1; i++)
                            {
                                if ((bool)this.Lager.SPL.dtCheckOut.Rows[i]["ausbuchen"])
                                {
                                    this.Lager.Artikel = new clsArtikel();
                                    this.Lager.Artikel.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
                                    decTmp = 0;
                                    Decimal.TryParse(this.Lager.SPL.dtCheckOut.Rows[i]["ArtikelID"].ToString(), out decTmp);
                                    this.Lager.Artikel.ID = decTmp;
                                    this.Lager.Artikel.GetArtikeldatenByTableID();
                                    this.Lager.Artikel.listArt.Clear();
                                    this.Lager.Artikel.listArt.Add(this.Lager.Artikel.Copy());

                                    this.Lager.ASNAction.ASNActionProcessNr = myASNAction;
                                    if (this.Lager.ASNAction.ASNActionProcessNr > 0)
                                    {
                                        AsnTransfer.CreateLM(ref this.Lager);
                                    }
                                }
                            }
                            break;
                        case clsASNAction.const_ASNAction_Ausgang:
                            this.Lager.Ausgang.FillAusgang();
                            this.Lager.Artikel.GetArtikeldatenByTableID();
                            this.Lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Ausgang;
                            if (this.Lager.ASNAction.ASNActionProcessNr > 0)
                            {
                                AsnTransfer.CreateLM(ref this.Lager);
                            }
                            break;
                    }
                }
                else
                {
                    AsnTransfer.CreateLM_Eingang(ref this.Lager);
                }
            }
        }
        ///<summary>ctrEinlagerung / tsbtnAuslagerung_Click</summary>
        ///<remarks>Artikel auslagern</remarks> 
        private void tsbtnAuslagerung_Click(object sender, EventArgs e)
        {
            if (dtSPL.Rows.Count > 0)
            {
                if (clsMessages.Lager_SPL_StoreOut())
                {
                    Lager._GL_System = this._ctrMenu._frmMain.GL_System;
                    Lager._GL_User = this.GL_User;
                    Lager.sys = this._ctrMenu._frmMain.system;
                    Lager.InitSubClasses();
                    List<decimal> ListArtToSToreOut = new List<decimal>();
                    DataTable dtSPLToAusgang = dtSPL.DefaultView.ToTable();
                    foreach (DataRow row in dtSPLToAusgang.Rows)
                    {
                        if ((bool)row["ausbuchen"])
                        {
                            decimal decTmp = 0;
                            decimal.TryParse(row["ArtikelID"].ToString(), out decTmp);
                            if (decTmp > 0)
                            {
                                ListArtToSToreOut.Add(decTmp);
                            }
                        }
                    }
                    if (Lager.ProzessStoreOutWithSPLOut(ListArtToSToreOut))
                    {
                        InitASNTransfer(clsASNAction.const_ASNAction_Ausgang);
                    }
                    SetAuswahlBestandDaten();
                }
                InitDGV();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitDGV();
        }
    }
}
