using Common.Enumerations;
using LVS;
using LVS.ViewData;
using Sped4.Classes;
using Sped4.Classes.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;


namespace Sped4
{
    public partial class ctrFaktLager : UserControl
    {
        internal const string const_LableInfo_Rechnung = "Rechnung";
        internal const string const_LableInfo_Gutschrift = "Gutschrift";
        internal const string const_LableInfo_Korrektur = "Korrektur";
        internal const string const_LableInfo_Vorschau = "Vorschau";
        internal const string const_LableInvoice_eInvoiceActiv = "e - Rechnung aktiviert";
        internal const string const_LableInvoice_eInvoiceNotActiv = "e - Rechnung deaktiviert - Rechnung kann nicht gespeichert werden";
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        internal DataTable dtAbrKunden = new DataTable();
        internal DataTable dtRGAuftraggeber = new DataTable();
        internal DataTable dtTarife = new DataTable();
        //internal DataTable dtMandanten = new DataTable();
        internal DateTime LastRGDate = DateTime.Now;

        internal clsTarif Tarif;
        internal clsFaktLager FaktLager;
        internal List<string> AttachmentList = new List<string>();
        //internal clsRechnung Rechnung;

        internal string strTmpAbrArt = string.Empty;
        internal decimal decNetto = 0;
        internal decimal Empfaenger = 0;
        internal decimal Auftraggeber = 0;
        internal clsADR cAuftraggeber;
        internal string DokumentenArt = string.Empty;
        internal decimal MandantenID = 0;
        internal string MandantenName = string.Empty;
        internal decimal MwStSatz = 0M;

        internal Int32 SearchButton = 0;
        internal bool bCanCalculate = false;
        MandantenViewData MandantVD = new MandantenViewData();
        AddressViewData adrVD = new AddressViewData();
        //internal ZUGFeRD_IsAvailable ZF = new ZUGFeRD_IsAvailable(new Common.Models.Mandanten(), new Common.Models.Addresses());
        public ctrFaktLager()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
        }
        ///<summary>ctrFaktLager / ctrFaktLager_Load</summary>
        ///<remarks></remarks>
        private void ctrFaktLager_Load(object sender, EventArgs e)
        {
            SetComTECSettings();
            this.panInvoiceSelection.Visible = this._ctrMenu._frmMain.system.Client.Modul.Fakt_eInvoiceIsAvailable;
            this.tsbtnMail.Visible = (!this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion);

            //--- Check e-Rechnung ob die beteiligten Addressen alle notwendigen auf Bestandteile


            //this._ctrMenu._frmWait.AddInfo("Fakturierung Lager wird geladen... ");
            RadGridLocalizationProvider.CurrentProvider = new clsGermanRadGridLocalizationProvider();
            SetAbrZeitraumDefault();

            FaktLager = new clsFaktLager();
            FaktLager.GL_System = this._ctrMenu._frmMain.GL_System;
            FaktLager.bUseBKZ = this._ctrMenu._frmMain.system.Client.Modul.Lager_USEBKZ;
            FaktLager.bCalcPreviewAdmin = this.tsbtnAdminCalc.Visible;
            FaktLager._GL_User = this.GL_User;
            FaktLager.Abrechnungsdatum = DateTime.Now;
            FaktLager.MandantenID = this._ctrMenu._frmMain.system.AbBereich.MandantenID;
            FaktLager.Sys = this._ctrMenu._frmMain.system;
            FaktLager.Auftraggeber = 0;

            FaktLager.RepDocSettings.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0, this._ctrMenu._frmMain.system.AbBereich.ID);

            Tarif = new clsTarif();
            Tarif._GL_User = this.GL_User;

            FaktLager.Rechnung = new clsRechnung();
            FaktLager.Rechnung._GL_User = this.GL_User;
            //--- Setzt Prüfergebnis aus Check e-Rechnung ob die beteiligten Addressen alle notwendigen auf Bestandteile
            rtsEInvoice.SetToggleState(FaktLager.ZUGFeRDAvailable.IsZUGFeRDAvailable);

            LastRGDate = clsRechnung.GetLastRGDate(this.GL_User, this._ctrMenu._frmMain.system, true);

            InitDGVAbrKunden();
            tscbDokument.ComboBox.SelectedIndex = 0;
            //Table für RG-Grd laden
            FaktLager.dtRechnung.Clear();
            //Rechnungsdaten werden aus der DB ermittelt und direkt die formatiert
            FaktLager.GetRGFromDB();
            InitGrdRG();
            InitRGSum();
            SetVorschauLabel(const_LableInfo_Vorschau);

            //RGANhang druck ausblenden
            vorschauAnhangToolStripMenuItem.Visible = this._ctrMenu._frmMain.system.Client.Modul.Print_Documents_UseRGAnhang;
            druckAnhangToolStripMenuItem.Visible = this._ctrMenu._frmMain.system.Client.Modul.Print_Documents_UseRGAnhang;
        }
        ///<summary>ctrFaktLager/ SetComTECSettings</summary>
        ///<remarks></remarks>
        private void SetComTECSettings()
        {
            string strAdmin = "Administrator";
            if (
                (this.GL_User.LoginName.ToString().ToUpper() == "ADMINISTRATOR")
                ||
                (this.GL_User.LoginName.ToString().ToUpper() == "ADMIN")
                )
            {
                this.tsbtnAdminCalc.Visible = true;
            }
            else
            {
                this.tsbtnAdminCalc.Visible = false;
            }
        }
        ///<summary>ctrFaktLager / SetAbrZeitraumDefault</summary>
        ///<remarks></remarks>
        private void SetAbrZeitraumDefault()
        {
            //Abrechnungszeitraum setzen
            DateTime dtAbrMonat = DateTime.Now.Date.AddMonths(-1);
            dtpAbrVon.Value = Functions.GetFirstDayOfMonth(dtAbrMonat);
            dtpAbrBis.Value = Functions.GetLastDayOfMonth(dtAbrMonat);
        }
        ///<summary>ctrFaktLager / InitRGSum</summary>
        ///<remarks></remarks>
        private void InitRGSum()
        {
            decNetto = 0;
            tbRGNetto.Text = Functions.FormatDecimal(0);
            tbRGBrutto.Text = Functions.FormatDecimal(0);
            tbMwStBetrag.Text = Functions.FormatDecimal(0);
        }
        ///<summary>ctrFaktLager / InitDGVAbrKunden</summary>
        ///<remarks>Füllt das Grid mit den abzurechnenden Kunden.</remarks>
        private void InitDGVAbrKunden()
        {
            bCanCalculate = false;
            this.tsbtnAdminCalc.Enabled = bCanCalculate;
            this.tsbtnRGErstellen.Enabled = bCanCalculate;

            //Grid abzurechnende Kunden lagen
            dtAbrKunden.Clear();
            if (FaktLager is clsFaktLager)
            {
                FaktLager.VonZeitraum = dtpAbrVon.Value;
                FaktLager.BisZeitraum = dtpAbrBis.Value;
                dtAbrKunden = FaktLager.dtAbrKunden;
            }

            this.dgvOPAuftraggeber.DataSource = dtAbrKunden;
            if (dgvOPAuftraggeber.Rows.Count > 0)
            {
                this.dgvOPAuftraggeber.Columns["AuftraggeberID"].IsVisible = false;
            }

            if (this.dgvOPAuftraggeber.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvOPAuftraggeber.Rows.Count - 1; i++)
                {
                    if (this.dgvOPAuftraggeber.Rows[i] != null)
                    {
                        decimal decTmp = 0;
                        string strTmp = this.dgvOPAuftraggeber.Rows[i].Cells["AuftraggeberID"].Value.ToString();
                        Decimal.TryParse(strTmp, out decTmp);
                        if (decTmp > 0)
                        {
                            InitLoadAfterAuftraggeberSelection(decTmp);
                            break;
                        }
                    }
                }
            }
        }
        ///<summary>ctrFaktLager / InitDGVAbrKunden</summary>
        ///<remarks></remarks>
        private void InitDGVTarif()
        {
            ClearFrm();
            dtTarife.Clear();
            Tarif.AdrID = Auftraggeber;
            dtTarife = clsTarif.GetTarifeByAdrID(this.GL_User, Tarif.AdrID, true, dtpAbrVon.Value, dtpAbrBis.Value, this._ctrMenu._frmMain.system.AbBereich.ID);
            this.dgvTarife.DataSource = dtTarife;
            //Columns visible setzen

            bCanCalculate = (this.dgvTarife.Rows.Count > 0);
            this.tsbtnAdminCalc.Enabled = bCanCalculate;
            this.tsbtnRGErstellen.Enabled = bCanCalculate;

            if (this.dgvTarife.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvTarife.Columns.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        if (this.dgvTarife.Rows[i] != null)
                        {
                            decimal decTmp = 0;
                            string strTarifID = this.dgvTarife.Rows[i].Cells["ID"].Value.ToString();
                            if (Decimal.TryParse(strTarifID, out decTmp))
                            {
                                SetAndFillTarifandTarifPos(decTmp);
                            }
                        }
                    }
                    string strTmp = this.dgvTarife.Columns[i].Name.ToString();
                    if (strTmp == "Tarifname")
                    {
                        this.dgvTarife.Columns[i].IsVisible = true;
                        this.dgvTarife.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                    }
                    else
                    {
                        this.dgvTarife.Columns[i].IsVisible = false;
                    }
                }
                this.dgvTarife.ClearSelection();
            }
        }
        ///<summary>ctrFaktLager / SetADRToFrm</summary>
        ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
        ///<param name="ADR_ID">ADR_ID</param>
        public void SetADRToFrm(decimal myDecADR_ID)
        {
            string strE = string.Empty;
            string strMC = string.Empty;
            //DataSet ds = clsADR.ReadADRbyID(myDecADR_ID);
            clsADR tmpAdr = new clsADR();
            tmpAdr.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, myDecADR_ID, true);
            strMC = tmpAdr.ViewID;
            strE = "[" + tmpAdr.ID + "] " + tmpAdr.ViewID + " - ";
            strE = strE + tmpAdr.KD_ID + " - ";
            strE = strE + tmpAdr.Name1 + " - ";
            strE = strE + tmpAdr.PLZ + " - ";
            strE = strE + tmpAdr.Ort;

            //SearchButton
            // 1 = KD /Auftraggeber
            // 2 = Versender
            // 3 = Empfänger
            // 4 = neutrale Versandadresse
            // 5 = neutrale Empfangsadresse
            // 6 = Mandanten
            // 7 = Spedition
            // 8 =
            // 9 =
            // 10= Rechnungsempfänger
            switch (SearchButton)
            {
                case 1:
                    Auftraggeber = myDecADR_ID;
                    Empfaenger = Auftraggeber;
                    tbSearchA.Text = strMC;
                    tbAuftraggeber.Text = strE;
                    if (tmpAdr.AdrID_RG > 0)
                    {
                        //tmpAdr.AdrID_RG = tmpAdr.ID;
                        this.SearchButton = 10;
                        SetADRToFrm(tmpAdr.AdrID_RG);
                        this.FaktLager.InvoiceReceiver = tmpAdr.AdrID_RG;
                    }
                    else
                    {
                        //Rechnungstext
                        //tbRGText.Text = tmpAdr.ADRTexte.Lagerrechnung_Text;
                    }
                    break;

                case 10:
                    Empfaenger = myDecADR_ID;
                    tbSearchRGE.Text = strMC;
                    tbRGEmpfaenger.Text = strE;
                    //Rechnungstext
                    //tbRGText.Text = tmpAdr.ADRTexte.Lagerrechnung_Text;
                    break;
            }
            this.FaktLager.InitZUGFeRD((int)this.Empfaenger);
            rtsEInvoice.SetToggleState(this.FaktLager.ZUGFeRDAvailable.IsZUGFeRDAvailable);
        }
        ///<summary>ctrFaktLager / ClearFrm</summary>
        ///<remarks>Form wird geleert</remarks>
        private void ClearFrm()
        {
            tbTarifSelected.Text = string.Empty;
            tbRGNr.Text = string.Empty;
            ClearAdrBoxen();

            //RGGrid leeren
            FaktLager.dtRechnung.Rows.Clear();
            //SetAbrZeitraumDefault();
        }
        ///<summary>ctrFaktLager / ClearAdrBoxen</summary>
        ///<remarks></remarks>
        private void ClearAdrBoxen()
        {
            tbSearchA.Text = string.Empty;
            tbSearchRGE.Text = string.Empty;
            tbAuftraggeber.Text = string.Empty;
            tbRGEmpfaenger.Text = string.Empty;
            tbRGText.Text = string.Empty;
            tbFibuInfo.Text = string.Empty;
        }
        ///<summary>ctrFaktLager / SetAndFillTarifandTarifPos</summary>
        ///<remarks></remarks>
        private void SetAndFillTarifandTarifPos(decimal myTarifID)
        {
            Tarif = new clsTarif();
            Tarif._GL_User = this.GL_User;
            Tarif.ID = myTarifID;
            Tarif.Fill();
            ClearFrm();
            //Adressen für Auftraggeber und Rechnungsempfänger setzen
            SearchButton = 1;
            SetADRToFrm(Auftraggeber);
            //SearchButton = 10;
            //SetADRToFrm(Empfaenger);
        }
        ///<summary>ctrFaktLager / InitDGVBestandsliste</summary>
        ///<remarks></remarks>
        private void InitDGVBestandsliste()
        {
            //Zuweisung Variablen
            FaktLager.VonZeitraum = dtpAbrVon.Value;
            FaktLager.BisZeitraum = dtpAbrBis.Value;
            FaktLager.LagerbestandInclSPL = Tarif.LagerBestandIncSPL;
            FaktLager.MandantenID = this._ctrMenu._frmMain.system.AbBereich.MandantenID;
            Tarif.AbBereich = new clsArbeitsbereiche();
            Tarif.AbBereich = this._ctrMenu._frmMain.system.AbBereich.Copy();
            FaktLager.GetBestandsauflistung(ref Tarif);

            this.dgvBestandsliste.DataSource = null;
            this.dgvBestandsliste.DataSource = FaktLager.dtBestandsauflistung;
            if (this.dgvBestandsliste.Columns["Bestandsart"] != null)
            {
                //this.dgvBestandsliste.Columns["Bestandsart"].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                this.dgvBestandsliste.Columns["Bestandsart"].TextAlignment = ContentAlignment.TopLeft;
            }
            if (this.dgvBestandsliste.Columns["Tarif"] != null)
            {
                //this.dgvBestandsliste.Columns["Tarif"].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                this.dgvBestandsliste.Columns["Tarif"].TextAlignment = ContentAlignment.TopLeft;
            }
            if (this.dgvBestandsliste.Columns["Bestand"] != null)
            {
                //this.dgvBestandsliste.Columns["Bestand"].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                this.dgvBestandsliste.Columns["Bestand"].FormatString = "{0:N3}";
                this.dgvBestandsliste.Columns["Bestand"].TextAlignment = ContentAlignment.TopRight;
            }
            if (this.dgvBestandsliste.Columns["Einheit"] != null)
            {
                //this.dgvBestandsliste.Columns["Einheit"].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                this.dgvBestandsliste.Columns["Einheit"].TextAlignment = ContentAlignment.TopCenter;
            }
            this.dgvBestandsliste.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            //this.dgvBestandsliste.BestFitColumns();
        }
        ///<summary>ctrFaktLager / tsbtnBestand_Click</summary>
        ///<remarks></remarks>
        private void tsbtnBestand_Click(object sender, EventArgs e)
        {
            InitDGVBestandsliste();
        }
        ///<summary>ctrFaktLager / tsbtnRGVorschau_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRGVorschau_Click(object sender, EventArgs e)
        {
            FaktLager.bCalcPreviewAdmin = false;
            InitCalculationPre();
        }
        ///<summary>ctrFaktLager / tsbtnRGVorschau_Click</summary>
        ///<remarks></remarks>
        private void InitCalculationPre()
        {
            FaktLager.InitZUGFeRD();
            //InitDGVBestandsliste();
            //ClearFrm();
            tbTarifSelected.Text = Tarif.Tarifname;
            SetEingabeFelderEnabled(true);
            if (this.FaktLager.Rechnung.RGRevision != null)
            {
                SetVorschauLabel(const_LableInfo_Korrektur);
                FaktLager.bCalcPreviewAdmin = true;
                InitDGVBestandsliste();
                FaktLager.bCalcPreviewAdmin = false;
            }
            else
            {
                SetVorschauLabel(const_LableInfo_Vorschau);
                InitDGVBestandsliste();
            }
            InitGrdRG();
            //Adressen für Auftraggeber und Rechnungsempfänger setzen
            SearchButton = 1;
            SetADRToFrm(Auftraggeber);
            //SearchButton = 10;
            //SetADRToFrm(Empfaenger);
            this.tabAuswahl.SelectTab(tpBestand);
        }
        ///<summary>ctrFaktLager / tsbtnAdminCalc_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAdminCalc_Click(object sender, EventArgs e)
        {
            FaktLager.bCalcPreviewAdmin = this.tsbtnAdminCalc.Visible;
            InitCalculationPre();
        }
        ///<summary>ctrFaktLager / InitGrdRG</summary>
        ///<remarks></remarks>
        private void InitGrdRG()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
            DataTable dtInvoiceSorted = new DataTable();
            FaktLager.dtRechnung.DefaultView.Sort = string.Empty;
            // Prüfen, ob die Spalte existiert
            if ((FaktLager.dtRechnung.Rows.Count > 0) && (FaktLager.dtRechnung.Columns.Contains("Pos")))
            {
                FaktLager.dtRechnung.DefaultView.Sort = "Pos";
            }
            dtInvoiceSorted = FaktLager.dtRechnung.DefaultView.ToTable();
            this.grdRG.DataSource = dtInvoiceSorted;
            this.dgvExistRGAuftraggeber.DataSource = FaktLager.dtRGAuftraggeber;
            for (Int32 i = 0; i <= this.grdRG.Columns.Count - 1; i++)
            {
                switch (this.grdRG.Columns[i].Name)
                {
                    case "Abrechnungsart":
                        this.grdRG.Columns.Move(this.grdRG.Columns[i].Index, 1);
                        break;

                    //case "Text":
                    case "RGPosText":
                        this.grdRG.Columns.Move(this.grdRG.Columns[i].Index, 2);
                        this.grdRG.Columns[i].IsVisible = true;
                        this.grdRG.Columns[i].MinWidth = 100;
                        break;

                    case "Menge":
                        this.grdRG.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                        this.grdRG.Columns[i].FormatString = "{0:N3}";
                        this.grdRG.Columns[i].TextAlignment = ContentAlignment.TopRight;
                        this.grdRG.Columns[i].MinWidth = 50;
                        this.grdRG.Columns.Move(this.grdRG.Columns[i].Index, 3);
                        break;

                    //case "CalcModValue":
                    //    this.grdRG.Columns[i].HeaderText = "Dauer";
                    //    this.grdRG.Columns[i].FormatString = "{0:N0}";
                    //    this.grdRG.Columns[i].TextAlignment = ContentAlignment.TopRight;
                    //    this.grdRG.Columns[i].MinWidth = 40;
                    //    this.grdRG.Columns.Move(this.grdRG.Columns[i].Index, 4);
                    //    break;

                    case "PricePerUnitFactor":
                        this.grdRG.Columns[i].HeaderText = "Faktor";
                        this.grdRG.Columns[i].FormatString = "{0:N3}";
                        this.grdRG.Columns[i].TextAlignment = ContentAlignment.TopRight;
                        this.grdRG.Columns[i].MinWidth = 40;
                        this.grdRG.Columns.Move(this.grdRG.Columns[i].Index, 4);
                        break;

                    //case "Marge €":
                    //case "Marge %":
                    //    this.grdRG.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                    //    this.grdRG.Columns[i].FormatString = "{0:N2}";
                    //    this.grdRG.Columns[i].TextAlignment = ContentAlignment.TopRight;
                    //    this.grdRG.Columns[i].MinWidth = 40;
                    //    break;

                    case "€/Einheit":
                        this.grdRG.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                        this.grdRG.Columns[i].FormatString = "{0:N2}";
                        this.grdRG.Columns[i].TextAlignment = ContentAlignment.TopRight;
                        this.grdRG.Columns[i].MinWidth = 50;
                        break;

                    case "Netto €":
                        this.grdRG.Columns[i].AutoSizeMode = BestFitColumnMode.AllCells;
                        this.grdRG.Columns[i].FormatString = "{0:N2}";
                        this.grdRG.Columns[i].TextAlignment = ContentAlignment.TopRight;
                        this.grdRG.Columns[i].MinWidth = 60;
                        break;

                    case "TarifPosID":
                    case "SumCalc":
                    case "Pos":
                    case "Anfangsbestand":
                    case "Zugang":
                    case "Endbestand":
                    case "Marge €":
                    case "Marge %":
                    case "CalcModValue":
                        this.grdRG.Columns[i].IsVisible = false;
                        break;

                    default:
                        this.grdRG.Columns[i].IsVisible = false;
                        break;
                }
            }
            //--- sorting
            Telerik.WinControls.Data.SortDescriptor sortDescriptor = new Telerik.WinControls.Data.SortDescriptor();
            sortDescriptor.Direction = ListSortDirection.Ascending;
            sortDescriptor.PropertyName = "Pos"; // Sort by Abrechnungsart

            this.grdRG.MasterTemplate.SortDescriptors.Clear();
            this.grdRG.MasterTemplate.SortDescriptors.Add(sortDescriptor);

            //--- grouping
            Telerik.WinControls.Data.GroupDescriptor groupDescriptor = new Telerik.WinControls.Data.GroupDescriptor();
            groupDescriptor.GroupNames.Clear();
            groupDescriptor.GroupNames.Add("Abrechnungsart", ListSortDirection.Ascending);

            this.grdRG.EnableGrouping = true;
            this.grdRG.GroupDescriptors.Clear();
            this.grdRG.GroupDescriptors.Add(groupDescriptor);

            //this.grdRG.MasterTemplate.GroupComparer = new TelerikControls.GridViewPositionGroupComparer();



            this.grdRG.MasterTemplate.ExpandAllGroups(); // This expands all groups

            this.grdRG.AutoScroll = true;
            //radGridView1.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
            this.grdRG.HorizontalScrollState = ScrollState.AutoHide;
            this.grdRG.VerticalScrollState = ScrollState.AutoHide;

            this.grdRG.BestFitColumns();
            if (this.FaktLager.Rechnung.ID > 0)
            {
                if (this.dgvExistRGAuftraggeber.Columns.Contains("ID"))
                {
                    for (Int32 i = 0; i <= this.dgvExistRGAuftraggeber.Rows.Count - 1; i++)
                    {
                        decimal decTmp = 0;
                        Decimal.TryParse(this.dgvExistRGAuftraggeber.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                        if (this.FaktLager.Rechnung.ID == decTmp)
                        {
                            this.dgvExistRGAuftraggeber.Rows[i].IsCurrent = true;
                            this.dgvExistRGAuftraggeber.Rows[i].IsSelected = true;
                        }
                    }
                }
            }
            CalcRGSum();
            //--2020_08_06 mr kann raus
            //MwStSatz des Kunden ermittlen
            //nudMwStSatz.Value = clsKunde.GetMwStSatz(Auftraggeber);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRG_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            //foreach (GridViewRowInfo row in grdRG.Rows)
            //{
            //    row.IsSelected = false;
            //}
        }
        ///<summary>ctrFaktLager / CalcRGSum</summary>
        ///<remarks></remarks>
        private void CalcRGSum()
        {
            if (FaktLager.dtRechnung.Rows.Count > 0)
            {
                decimal decMwStSatz = nudMwStSatz.Value;
                decimal decMwStBetrag = 0;
                decimal decBrutto = 0;
                if (decMwStSatz > 0)
                {
                    //decBrutto = FaktLager.Netto * ((100 + decMwStSatz) / 100);
                    FaktLager.Netto = Math.Round(FaktLager.Netto, 2, MidpointRounding.AwayFromZero);
                    decBrutto = FaktLager.Netto * ((100 + decMwStSatz) / 100);
                    decBrutto = Math.Round(decBrutto, 2, MidpointRounding.AwayFromZero);
                    decMwStBetrag = decBrutto - FaktLager.Netto;
                }
                else
                {
                    decMwStSatz = 0;
                    decMwStBetrag = 0;
                    decBrutto = FaktLager.Netto;
                }

                tbRGNetto.Text = Functions.FormatDecimal(FaktLager.Netto);
                tbMwStBetrag.Text = Functions.FormatDecimal(decMwStBetrag);
                tbRGBrutto.Text = Functions.FormatDecimal(decBrutto);
            }
            else
            {
                InitRGSum();
            }
        }
        ///<summary>ctrFaktLager / CalcRGSum</summary>
        ///<remarks></remarks>
        private void nudMwStSatz_ValueChanged(object sender, EventArgs e)
        {
            CalcRGSum();
        }
        ///<summary>ctrFaktLager / SetVorschau</summary>
        ///<remarks>Setzt die Vorschau Info.</remarks>
        private void SetVorschauLabel(string strTxt)
        {
            lInfo.Text = strTxt;
            //Test
            if (this.FaktLager.Rechnung.RGRevision != null)
            {
                lInfo.Text = const_LableInfo_Korrektur;
            }
        }
        ///<summary>ctrFaktLager / SetVorschau</summary>
        ///<remarks>Setzt die Vorschau Info.</remarks>
        private void tsbtnRGErstellen_Click(object sender, EventArgs e)
        {
            //false, da immer bei der Erstellung einer Rechnung die bereits Berechneten berücksichtigt werden müssen
            FaktLager.bCalcPreviewAdmin = false;
            //ClearFrm();
            tbTarifSelected.Text = Tarif.Tarifname;
            SetEingabeFelderEnabled(true);
            if (this.FaktLager.Rechnung.RGRevision != null)
            {
                this.FaktLager.Rechnung.RGRevision.Delete(true);
            }
            InitDGVBestandsliste();
            //Setzen der Rechnungsdaten
            //RGNr wird beim Speichern gesetzt
            dtpRGDatum.Value = dtpRGDatum.Value;
            InitGrdRG();
            //Rechnung speichern
            SaveRG();

            this.FaktLager.Rechnung.RGRevision = null;
            SetVorschauLabel(const_LableInfo_Rechnung);
            this.tabAuswahl.SelectTab(tpRechnungen);
            FaktLager.dtRechnung.Clear();
            ClearFrm();
        }
        ///<summary>ctrFaktLager / SaveRG</summary>
        ///<remarks></remarks>
        private void SaveRG()
        {
            if (!FaktLager.Rechnung.ExistRGId())
            {
                //clsRechnung tmpRGRevision = this.FaktLager.Rechnung.RGRevision.Copy();
                clsRechnung tmpRGRevision = this.FaktLager.Rechnung.Copy();
                tmpRGRevision.ID = 0;

                FaktLager.Rechnung = new clsRechnung();
                FaktLager.Rechnung.sys = this._ctrMenu._frmMain.system;
                FaktLager.Rechnung._GL_User = this.GL_User;
                FaktLager.Rechnung.RGRevision = tmpRGRevision.Copy();
                FaktLager.Rechnung.Empfaenger = Empfaenger;
                FaktLager.Rechnung.Auftraggeber = Auftraggeber;
                FaktLager.Rechnung.RGNr = 0;
                FaktLager.Rechnung.AbrechnungsTarifName = Tarif.Tarifname;
                FaktLager.Rechnung.Datum = dtpRGDatum.Value;
                //-- fällig
                int iZZ = 14;
                if (Tarif.Zahlungsziel > 0)
                {
                    iZZ = Tarif.Zahlungsziel;
                }
                FaktLager.Rechnung.faellig = dtpRGDatum.Value.AddDays(iZZ);
                FaktLager.Rechnung.AbrZeitraumVon = dtpAbrVon.Value;
                FaktLager.Rechnung.AbrZeitraumBis = dtpAbrBis.Value;
                FaktLager.Rechnung.MwStSatz = nudMwStSatz.Value;
                FaktLager.Rechnung.InfoText = tbRGText.Text;
                FaktLager.Rechnung.FibuInfo = tbFibuInfo.Text;
                decimal decTmp = 0;
                Decimal.TryParse(tbMwStBetrag.Text, out decTmp);
                FaktLager.Rechnung.MwStBetrag = decTmp;
                decTmp = 0;
                Decimal.TryParse(tbRGNetto.Text, out decTmp);
                FaktLager.Rechnung.NettoBetrag = decTmp;
                decTmp = 0;
                Decimal.TryParse(tbRGBrutto.Text, out decTmp);
                FaktLager.Rechnung.BruttoBetrag = decTmp;


                FaktLager.Rechnung.bezahlt = Globals.DefaultDateTimeMaxValue; //aktuelle nicht vorgesehen eventuell für Mahnungen
                FaktLager.Rechnung.Druck = false;
                FaktLager.Rechnung.Druckdatum = Globals.DefaultDateTimeMaxValue;
                FaktLager.Rechnung.RGArt = enumTarifArt.Lager.ToString();
                FaktLager.Rechnung.MandantenID = FaktLager.MandantenID;
                FaktLager.Rechnung.ArBereichID = this._ctrMenu._frmMain.system.AbBereich.ID;
                FaktLager.Rechnung.exFibu = false;
                FaktLager.Rechnung.Anfangsbestand = FaktLager.Anfangsbestand;

                //Rechnungspositionen
                FaktLager.Rechnung.dtRGPositionen = FaktLager.dtRechnung;
                //Artikel der einzelnen Positionen
                FaktLager.Rechnung.dtArtikelEinlagerung = FaktLager.dtArtikelEinlagerung;
                FaktLager.Rechnung.dtArtikelAnfangsbestand = FaktLager.dtArtikelAnfangsbestand;
                FaktLager.Rechnung.dtArtikelAuslagerung = FaktLager.dtArtikelAuslagerung;
                FaktLager.Rechnung.dtArtikelDirektanlieferung = FaktLager.dtArtikelDirektanlieferung;
                FaktLager.Rechnung.dtArtikelLagerTransporte = FaktLager.dtArtikelLagerTransporte;
                FaktLager.Rechnung.dtArtikelRuecklieferung = FaktLager.dtArtikelRuecklieferung;
                FaktLager.Rechnung.dtArtikelSperrlager = FaktLager.dtArtikelSperrlager;
                FaktLager.Rechnung.dtArtikelLagerbestand = FaktLager.dtArtikelLagerbestand;
                FaktLager.Rechnung.dtArtikelVorfracht = FaktLager.dtArtikelVorfracht;
                FaktLager.Rechnung.dtArtikelGleis = FaktLager.dtArtikelGleis;
                FaktLager.Rechnung.dtArtikelNebenkosten = FaktLager.dtArtikelNebenkosten;
                FaktLager.Rechnung.dtArtikelToll = FaktLager.dtArtikelToll;

                //Gutschrift
                string strSelDoc = tscbDokument.SelectedText.ToString();
                switch (strSelDoc)
                {
                    case "Rechnung":
                        FaktLager.Rechnung.GS = false;
                        FaktLager.Rechnung.Storno = false;
                        break;
                    case "Gutschrift":
                        FaktLager.Rechnung.GS = true;
                        FaktLager.Rechnung.Storno = false;
                        break;
                }
                FaktLager.Rechnung.Storno = false;

                FaktLager.Rechnung.Add();

                //wenn ID exist, dann Rechnungsdaten laden und auf in Form eintragen
                //sont Fehlermeldung
                if (FaktLager.Rechnung.RechnungExist)
                {
                    //Rechnungsdaten aus Datenbank laden
                    FaktLager.Rechnung.Fill();
                    SetRGDatenToFrm();
                    SetEingabeFelderEnabled(false);
                    //Table für RG-Grd laden
                    FaktLager.dtRechnung.Clear();
                    //Rechnungsdaten werden aus der DB ermittelt und direkt die formatiert
                    FaktLager.GetRGFromDB();
                    InitGrdRG();
                }
                else
                {
                    //Fehlermeldung
                    clsMessages.Fakturierung_LagerRGInsertFailed();
                }
                if (FaktLager.Rechnung.RGRevision.RGNr > 0)
                {
                    //Add Logbucheintrag Löschen
                    string Txt = "ID:[" + this.FaktLager.Rechnung.ID.ToString() + "]/ RG-/GS-Nr:[" + FaktLager.Rechnung.RGNr.ToString() + "] / Arbeitsbereicht:[" + this._ctrMenu._frmMain.system.AbBereich.ID.ToString() + "]";
                    string Beschreibung = "Rechnung korrigiert: " + Txt + "  ";
                    Functions.AddLogbuch(this.FaktLager._GL_User.User_ID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
                }
                FaktLager.Rechnung.RGRevision = new clsRechnung();
                InitDGVExistRGAuftraggber();
            }
        }
        ///<summary>ctrFaktLager / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tscbMandanten_SelectedIndexChanged(object sender, EventArgs e)
        {
            /****
            decimal decTmp = 0;
            string strName = string.Empty;
            Functions.SetMandantenDaten(ref tscbMandanten, ref decTmp, ref strName);
            FaktLager.MandantenID = decTmp;
            FaktLager.MandantenName = strName;
             * ****/
        }
        ///<summary>ctrFaktLager / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void SetRGDatenToFrm()
        {
            ClearFrm();
            //Tarifname
            tbTarifSelected.Text = FaktLager.Rechnung.AbrechnungsTarifName;
            tbRGNr.Text = FaktLager.Rechnung.RGNr.ToString();
            dtpRGDatum.Value = FaktLager.Rechnung.Datum;
            dtpAbrVon.Value = FaktLager.Rechnung.AbrZeitraumVon;
            dtpAbrBis.Value = FaktLager.Rechnung.AbrZeitraumBis;
            //Auftraggeber
            SearchButton = 1;
            SetADRToFrm(FaktLager.Rechnung.Auftraggeber);
            //Rechnungsempfänger
            SearchButton = 10;
            SetADRToFrm(FaktLager.Rechnung.Empfaenger);

            // e Rechnungsdaten
            SetXInvoiceValueToFrm();

            //Beträge
            tbRGNetto.Text = Functions.FormatDecimal(FaktLager.Rechnung.NettoBetrag);
            nudMwStSatz.Value = FaktLager.Rechnung.MwStSatz;
            tbMwStBetrag.Text = Functions.FormatDecimal(FaktLager.Rechnung.MwStBetrag);
            tbRGBrutto.Text = Functions.FormatDecimal(FaktLager.Rechnung.BruttoBetrag);

            //REchnungstext
            tbRGText.Text = FaktLager.Rechnung.InfoText;
            tbFibuInfo.Text = FaktLager.Rechnung.FibuInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetXInvoiceValueToFrm()
        {
            ClearXInvoiceValueToFrm();
            if (FaktLager.Rechnung.ID > 0)
            {
                // e Rechnungsdaten
                tbUSID.Text = FaktLager.Rechnung.Mandant.VatId;
                tbBank.Text = FaktLager.Rechnung.Mandant.Bank;
                tbBLZ.Text = FaktLager.Rechnung.Mandant.Blz;
                tbBic.Text = FaktLager.Rechnung.Mandant.Bic;
                tbKonto.Text = FaktLager.Rechnung.Mandant.Konto;
                tbIban.Text = FaktLager.Rechnung.Mandant.Iban;
                tbAP.Text = FaktLager.Rechnung.Mandant.Contact;
                tbPhone.Text = FaktLager.Rechnung.Mandant.Phone;
                tbMail.Text = FaktLager.Rechnung.Mandant.Mail;
                tbHomepage.Text = FaktLager.Rechnung.Mandant.Homepage;

                tbUstIdReceiver.Text = FaktLager.Rechnung.ADR_RGEmpfaenger.Kunde.USt_ID;
                tbReceiverMailAdress.Text = FaktLager.Rechnung.ADR_RGEmpfaenger.Kunde.Mailaddress;
                tbInvoiceReceiver.Text = "[" + FaktLager.Rechnung.ADR_RGEmpfaenger.ID + "] - " + FaktLager.Rechnung.ADR_RGEmpfaenger.ViewID;
            }
            else
            {
                tbUSID.Text = FaktLager.ZUGFeRDAvailable.Mandant.VatId;
                tbBank.Text = FaktLager.ZUGFeRDAvailable.Mandant.Bank;
                tbBLZ.Text = FaktLager.ZUGFeRDAvailable.Mandant.Blz;
                tbBic.Text = FaktLager.ZUGFeRDAvailable.Mandant.Bic;
                tbKonto.Text = FaktLager.ZUGFeRDAvailable.Mandant.Konto;
                tbIban.Text = FaktLager.ZUGFeRDAvailable.Mandant.Iban;
                tbAP.Text = FaktLager.ZUGFeRDAvailable.Mandant.Contact;
                tbPhone.Text = FaktLager.ZUGFeRDAvailable.Mandant.Phone;
                tbMail.Text = FaktLager.ZUGFeRDAvailable.Mandant.Mail;
                tbHomepage.Text = FaktLager.ZUGFeRDAvailable.Mandant.Homepage;

                tbUstIdReceiver.Text = FaktLager.ZUGFeRDAvailable.AdrInvoiceReceiver.CustomerData.UstId;
                tbReceiverMailAdress.Text = FaktLager.ZUGFeRDAvailable.AdrInvoiceReceiver.CustomerData.Mailaddress;
                tbInvoiceReceiver.Text = "[" + FaktLager.ZUGFeRDAvailable.AdrInvoiceReceiver.Id + "] - " + FaktLager.ZUGFeRDAvailable.AdrInvoiceReceiver.AddressStringShort;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearXInvoiceValueToFrm()
        {
            tbUSID.Text = string.Empty;
            tbBank.Text = string.Empty;
            tbBLZ.Text = string.Empty;
            tbBic.Text = string.Empty;
            tbKonto.Text = string.Empty;
            tbIban.Text = string.Empty;
            tbAP.Text = string.Empty;
            tbPhone.Text = string.Empty;
            tbMail.Text = string.Empty;
            tbHomepage.Text = string.Empty;

            tbUstIdReceiver.Text = string.Empty;
            tbReceiverMailAdress.Text = string.Empty;
            tbInvoiceReceiver.Text = string.Empty;
        }
        ///<summary>ctrFaktLager / tscbMandanten_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void InitDGVExistRGAuftraggber()
        {
            FaktLager.Rechnung.sys = this._ctrMenu._frmMain.system;
            FaktLager.Auftraggeber = Auftraggeber;
            FaktLager.dtRGAuftraggeber.Clear();
            FaktLager.GetExistRGAuftraggeber();
            this.dgvExistRGAuftraggeber.DataSource = FaktLager.dtRGAuftraggeber.DefaultView;

            for (Int32 i = 0; i <= this.dgvExistRGAuftraggeber.Columns.Count - 1; i++)
            {
                string ColName = this.dgvExistRGAuftraggeber.Columns[i].Name.ToString();
                switch (ColName)
                {
                    case "ID":
                        this.dgvExistRGAuftraggeber.Columns["ID"].IsVisible = false;
                        break;
                    case "Druck":
                        this.dgvExistRGAuftraggeber.Columns["Druck"].IsPinned = true;
                        this.dgvExistRGAuftraggeber.Columns["Druck"].AutoSizeMode = BestFitColumnMode.HeaderCells;
                        break;
                    case "RGNr":
                        this.dgvExistRGAuftraggeber.Columns["RGNr"].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        this.dgvExistRGAuftraggeber.Columns["RGNr"].TextAlignment = ContentAlignment.TopRight;
                        break;
                    case "Datum":
                        this.dgvExistRGAuftraggeber.Columns["Datum"].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        this.dgvExistRGAuftraggeber.Columns["Datum"].FormatString = "{0:dd.MM.yyyy}";
                        this.dgvExistRGAuftraggeber.Columns["Datum"].TextAlignment = ContentAlignment.TopCenter;
                        break;
                    case "Jahr":
                        this.dgvExistRGAuftraggeber.Columns["Jahr"].IsVisible = false;
                        break;
                }
            }
            this.dgvExistRGAuftraggeber.GroupDescriptors.Clear();
            Telerik.WinControls.Data.GroupDescriptor descriptor = new Telerik.WinControls.Data.GroupDescriptor();
            descriptor.GroupNames.Add("Jahr", System.ComponentModel.ListSortDirection.Descending);
            this.dgvExistRGAuftraggeber.GroupDescriptors.Add(descriptor);
        }
        ///<summary>ctrFaktLager / SetEingabeFelderEnabled</summary>
        ///<remarks></remarks>
        private void SetEingabeFelderEnabled(bool bEnabled)
        {
            tbTarifSelected.Enabled = bEnabled;
            tbRGNr.Enabled = bEnabled;
            tbSearchA.Enabled = bEnabled;
            tbSearchRGE.Enabled = bEnabled;

            dtpRGDatum.Enabled = bEnabled;
            dtpAbrVon.Enabled = bEnabled;
            dtpAbrBis.Enabled = bEnabled;

            tbRGNetto.Enabled = bEnabled;
            nudMwStSatz.Enabled = bEnabled;
            tbMwStBetrag.Enabled = bEnabled;
            tbMwStBetrag.Enabled = bEnabled;
            tbRGBrutto.Enabled = bEnabled;

            //Button RGEmpfänger
            btnRGEmpfaenger.Enabled = bEnabled;

        }
        ///<summary>ctrFaktLager / btnRGEmpfaenger_Click</summary>
        ///<remarks>Adressscuhe öffnen.</remarks>
        private void btnRGEmpfaenger_Click(object sender, EventArgs e)
        {
            SearchButton = 10;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrFaktLager / tsbtnStorno_Click</summary>
        ///<remarks>Storno für eine komplette Rechnung / Gutschrift wird erstellt.</remarks>
        private void tsbtnStorno_Click(object sender, EventArgs e)
        {
            //FaktLager.Rechnung.MandantenID= this.FaktLager.MandantenID;
            if (
                (this.FaktLager.Rechnung.ID > 0) &&
                (this.FaktLager.Rechnung.StornoID == 0) &&
                (this.FaktLager.Rechnung.ExistStornoZurRG == false)
                )
            {
                FaktLager.Rechnung.MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
                FaktLager.Rechnung.sys = this._ctrMenu._frmMain.system;
                FaktLager.Rechnung.CreateStorno();
            }
            InitDGVExistRGAuftraggber();
        }
        ///<summary>ctrFaktLager / tsbtnDruckVorschau_Click</summary>
        ///<remarks>Druckvorschau der gewählten Rechnung / GS erstellen</remarks>
        private void tsbtnDruckVorschau_Click(object sender, EventArgs e)
        {
            //if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            //{
            //    //this._ctrMenu._frmMain.system.CustomizeDocPath(ref this._ctrMenu._frmMain.GL_System, this.FaktLager.Rechnung.Empfaenger);
            //    //this.ListReportDoc = this._ctrMenu._frmMain.system.Client.ListPrintCenterReportEinlagerung.ToList();
            //}
            //else
            //{
            //    this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
            //    this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(Globals.enumDokumentenart.LagerRechnung.ToString());
            //}
            //this._ctrMenu.OpenFrmReporView(this, false);
        }
        ///<summary>ctrFaktLager / tscbDokument_SelectedIndexChanged</summary>
        ///<remarks>Zuweisung Mandanten Daten</remarks>
        private void tscbDokument_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Functions.SetMandantenDaten(ref tscbMandanten, ref MandantenID, ref MandantenName);
            //FaktLager.MandantenID = MandantenID;
            //FaktLager.MandantenName = MandantenName;
        }
        ///<summary>ctrFaktLager / tsbtnPrint_Click</summary>
        ///<remarks>Dokument direkt drucken</remarks>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            //if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            //{
            //    //this._ctrMenu._frmMain.system.CustomizeDocPath(ref this._ctrMenu._frmMain.GL_System, this.FaktLager.Rechnung.Empfaenger);
            //    //this.ListReportDoc = this._ctrMenu._frmMain.system.Client.ListPrintCenterReportEinlagerung.ToList();
            //}
            //else
            //{
            //    this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
            //    this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(Globals.enumDokumentenart.LagerRechnung.ToString());
            //}

            //if (!this.FaktLager.Rechnung.Druck)
            //{
            //    this._ctrMenu.OpenFrmReporView(this, true);
            //    this.FaktLager.Rechnung.Druck = true;
            //    this.FaktLager.Rechnung.Druckdatum = DateTime.Now;
            //    this.FaktLager.Rechnung.UpdateRechnungPrint();
            //    this.FaktLager.Rechnung.Fill();
            //}
            //else
            //{
            //    if (clsMessages.Fakturierung_RGGSPrintAgain())
            //    {
            //        this._ctrMenu.OpenFrmReporView(this, true);
            //    }
            //}
            //InitDGVExistRGAuftraggber();
        }
        ///<summary>ctrFaktLager / toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //PrintRGAnhangPreview(true);
            //if (this.FaktLager.Rechnung.ID > 0)
            //{
            //    if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            //    {
            //        List<decimal> AuftraggeberMat = new List<decimal>();
            //        bool bUseMatDoc = false;
            //        clsSystem sys = new clsSystem();
            //        sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
            //        if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
            //        {
            //            string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
            //            foreach (string auftraggeber in AuftraggeberSplit)
            //            {
            //                if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
            //                {
            //                    bUseMatDoc = true;
            //                }
            //            }
            //        }
            //        if (true) { }
            //        ctrPrintLager TmpPrint = new ctrPrintLager();
            //        TmpPrint.Hide();
            //        TmpPrint._ctrMenu = this._ctrMenu;


            //        if (!bUseMatDoc)
            //        {
            //            this.DokumentenArt = Globals.enumDokumentenart.RGAnhang.ToString();
            //        }
            //        else
            //        {
            //            this.DokumentenArt = Globals.enumDokumentenart.RGAnhangMat.ToString();
            //        }
            //    }
            //    else
            //    {
            //        this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
            //        this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(Globals.enumDokumentenart.RGAnhang.ToString());
            //    }

            //    this._ctrMenu.OpenFrmReporView(this, true);
            //    this.DokumentenArt = Globals.enumDokumentenart.LagerRechnung.ToString();
            //}
        }
        ///<summary>ctrFaktLager / tsbtnRGDelete_Click</summary>
        ///<remarks>Rechnung/ Gutschrift löschen</remarks>
        private void tsbtnRGDelete_Click(object sender, EventArgs e)
        {
            if (clsMessages.Fakturierung_Delete())
            {
                FaktLager.Rechnung.sys = this._ctrMenu._frmMain.system;
                FaktLager.Rechnung.Delete(false);
                ClearFrm();
                InitDGVExistRGAuftraggber();
            }
        }
        ///<summary>ctrFaktLager / tbRGNr_TextChanged</summary>
        ///<remarks></remarks>
        private void tbRGNr_TextChanged(object sender, EventArgs e)
        {
            string strTmp = tbRGNr.Text.Trim();
            decimal decTmp = 0;
            bool bRGOK = Decimal.TryParse(strTmp, out decTmp);
            SetMenueButtonEnabled();
        }
        ///<summary>ctrFaktLager / tbRGNr_TextChanged</summary>
        ///<remarks></remarks>
        private void tsbtnCloseFakt_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrFaktLager();
        }
        ///<summary>ctrFaktLager / dtpRGDatum_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpRGDatum_ValueChanged(object sender, EventArgs e)
        {
            if (!this._ctrMenu._frmMain.GL_System.VE_Fakturierung_InvoiceDateInPast)
            {
                if (dtpRGDatum.Value < LastRGDate)
                {
                    SetLastRechnungsDatum();
                }
            }

            if (FaktLager != null)
            {
                FaktLager.Abrechnungsdatum = dtpRGDatum.Value;
            }
        }
        ///<summary>ctrFaktLager / dgvExistRGAuftraggeber_CellClick</summary>
        ///<remarks></remarks>
        private void dgvExistRGAuftraggeber_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (this.FaktLager.Rechnung.RGRevision == null)
            {
                if (this.dgvOPAuftraggeber.Rows.Count > 0)
                {
                    if (e.RowIndex > -1)
                    {
                        string strTmp = this.dgvExistRGAuftraggeber.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        decimal decTmp = 0;
                        Decimal.TryParse(strTmp, out decTmp);

                        if (clsRechnung.ExistRechnung(this.GL_User, decTmp))
                        {
                            this.FaktLager.Rechnung = new clsRechnung();
                            this.FaktLager.Rechnung._GL_User = this.GL_User;
                            this.FaktLager.Rechnung.ID = decTmp;
                            this.FaktLager.Rechnung.sys = this.FaktLager.Sys;
                            this.FaktLager.Rechnung.Fill();

                            //Dokumentenart
                            strTmp = string.Empty;
                            strTmp = this.dgvExistRGAuftraggeber.Rows[e.RowIndex].Cells["Art"].Value.ToString();
                            if ((strTmp == "RG") || (strTmp == "Storno-RG"))
                            {
                                tscbDokument.ComboBox.SelectedIndex = 0;
                            }
                            else
                            {
                                //tscbDokument.ComboBox.SelectedIndex = 1;
                            }
                            DokumentenArt = tscbDokument.ComboBox.SelectedItem.ToString();

                            ClearFrm();
                            SetRGDatenToFrm();
                            SetEingabeFelderEnabled(false);
                            SetMenueButtonEnabled();
                            //Table für RG-Grd laden
                            FaktLager.dtRechnung.Clear();
                            //Rechnungsdaten werden aus der DB ermittelt und direkt die formatiert
                            FaktLager.GetRGFromDB();
                            InitGrdRG();
                            this.tsbtnRGKorrektur.Enabled = (!this.FaktLager.Rechnung.Druck);
                            if (this.FaktLager.Rechnung.GS)
                            {
                                SetVorschauLabel(const_LableInfo_Gutschrift);
                            }
                            else
                            {
                                SetVorschauLabel(const_LableInfo_Rechnung);
                            }
                        }
                    }
                }
                SetLastRechnungsDatum();
            }
            else
            {
                for (Int32 i = 0; i <= this.dgvExistRGAuftraggeber.Rows.Count - 1; i++)
                {
                    string strTmp = this.dgvExistRGAuftraggeber.Rows[i].Cells["ID"].Value.ToString();
                    decimal decTmp = 0;
                    Decimal.TryParse(strTmp, out decTmp);
                    if (decTmp == this.FaktLager.Rechnung.ID)
                    {
                        this.dgvExistRGAuftraggeber.Rows[i].IsSelected = true;
                        this.dgvExistRGAuftraggeber.Rows[i].IsCurrent = true;
                        i = this.dgvExistRGAuftraggeber.Rows.Count;
                    }
                }
            }
        }
        ///<summary>ctrFaktLager / SetLastRechnungsDatum</summary>
        ///<remarks>Set das mögliche RG-Datum</remarks>
        private void SetLastRechnungsDatum()
        {
            //Rechnungsdatum setzen
            if (LastRGDate.Date < dtpAbrBis.Value.Date)
            {
                dtpRGDatum.Value = dtpAbrBis.Value;
            }
            else
            {
                dtpRGDatum.Value = LastRGDate.Date;
            }
        }
        ///<summary>ctrFaktLager / dgvOPAuftraggeber_Click</summary>
        ///<remarks></remarks>
        private void dgvOPAuftraggeber_Click(object sender, EventArgs e)
        {
            if (this.dgvOPAuftraggeber.CurrentCell != null)
            {
                string strTmp = this.dgvOPAuftraggeber.Rows[this.dgvOPAuftraggeber.CurrentCell.RowIndex].Cells["AuftraggeberID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    if (clsADR.ExistAdrID(decTmp, this.GL_User.User_ID))
                    {
                        InitLoadAfterAuftraggeberSelection(decTmp);
                        tabAuswahl.SelectTab(tpTarife);
                        this.FaktLager.Rechnung.RGRevision = null;
                        this.FaktLager.Rechnung.ID = 0;
                        SetVorschauLabel(const_LableInfo_Vorschau);
                    }
                }
            }
        }
        ///<summary>ctrFaktLager / dgvOPAuftraggeber_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgvOPAuftraggeber_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvOPAuftraggeber.CurrentCell != null)
            {
                string strTmp = this.dgvOPAuftraggeber.Rows[this.dgvOPAuftraggeber.CurrentCell.RowIndex].Cells["AuftraggeberID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    if (clsADR.ExistAdrID(decTmp, this.GL_User.User_ID))
                    {
                        InitLoadAfterAuftraggeberSelection(decTmp);
                        tabAuswahl.SelectTab(tpTarife);
                        this.FaktLager.Rechnung.RGRevision = null;
                        this.FaktLager.Rechnung.ID = 0;
                        SetVorschauLabel(const_LableInfo_Vorschau);
                    }
                }
            }
        }
        ///<summary>ctrFaktLager / dgvOPAuftraggeber_CellClick</summary>
        ///<remarks></remarks>
        private void InitLoadAfterAuftraggeberSelection(decimal myAdrIDAuftraggeber)
        {
            MandantVD = new MandantenViewData((int)this._ctrMenu._frmMain.system.Mandant.ID);

            cAuftraggeber = new clsADR();
            cAuftraggeber.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, myAdrIDAuftraggeber, true);
            adrVD = new AddressViewData((int)cAuftraggeber.ID, (int)_ctrMenu._frmMain.GL_User.User_ID);

            Auftraggeber = cAuftraggeber.ID;
            //Empfaenger = myAdrIDAuftraggeber;
            //Adresse existiert, jetzt können die entsprechenden Tarife geladen werden
            Tarif._GL_User = this.GL_User;
            Tarif.AdrID = Auftraggeber;

            FaktLager.Auftraggeber = Auftraggeber;
            //rtsEInvoice.SetToggleState(FaktLager.ZUGFeRDAvailable.IsZUGFeRDAvailable);
            ClearAdrBoxen();
            InitDGVTarif();
            SearchButton = 1;
            SetADRToFrm(Auftraggeber);
            nudMwStSatz.Value = clsKunde.GetMwStSatz(Auftraggeber);
            //SearchButton = 10;
            //SetADRToFrm(Empfaenger);
            //Laden der bestehenden Rechnungen des Auftraggebers

            InitDGVExistRGAuftraggber();

            //this.FaktLager.InitZUGFeRD((int)FaktLager.Auftraggeber);
            //rtsEInvoice.SetToggleState(this.FaktLager.ZUGFeRDAvailable.IsZUGFeRDAvailable);
            SetXInvoiceValueToFrm();
        }
        ///<summary>ctrFaktLager / dgvTarife_SelectionChanged_1</summary>
        ///<remarks></remarks>
        private void dgvTarife_SelectionChanged_1(object sender, EventArgs e)
        {
            string strTarif = string.Empty;
            if (this.dgvTarife.CurrentCell != null)
            {
                strTarif = this.dgvTarife.Rows[this.dgvTarife.CurrentCell.RowIndex].Cells["Tarifname"].Value.ToString();
                decimal decTmp = 0;
                string strTarifID = this.dgvTarife.Rows[this.dgvTarife.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
                if (Decimal.TryParse(strTarifID, out decTmp))
                {
                    Tarif = new clsTarif();
                    Tarif._GL_User = this.GL_User;
                    Tarif.ID = decTmp;
                    Tarif.Fill();
                }
                ClearFrm();
                tbTarifSelected.Text = Tarif.Tarifname;
                //Adressen für Auftraggeber und Rechnungsempfänger setzen
                SearchButton = 1;
                SetADRToFrm(Auftraggeber);
                //SearchButton = 10;
                //SetADRToFrm(Empfaenger);

                this.FaktLager.Rechnung.RGRevision = null;
                this.FaktLager.Rechnung.ID = 0;
                SetVorschauLabel(const_LableInfo_Vorschau);
            }
            tbTarifSelected.Text = Tarif.Tarifname;
        }
        ///<summary>ctrFaktLager / dgvTarife_SelectionChanged</summary>
        ///<remarks>der gewählte Tarifname wird gesetzt</remarks>
        private void dgvTarife_SelectionChanged(object sender, EventArgs e)
        {
            //string strTarif = string.Empty;
            //if (this.dgvTarife.CurrentCell != null)
            //{
            //    strTarif = this.dgvTarife.Rows[this.dgvTarife.CurrentCell.RowIndex].Cells["Tarifname"].Value.ToString();
            //    decimal decTmp = 0;
            //    string strTarifID = this.dgvTarife.Rows[this.dgvTarife.CurrentCell.RowIndex].Cells["ID"].Value.ToString();
            //    if (Decimal.TryParse(strTarifID, out decTmp))
            //    {
            //        SetAndFillTarifandTarifPos(decTmp);
            //    }
            //}
            //tbTarifSelected.Text = strTarif;
        }
        ///<summary>ctrFaktLager / btnRefreshBestand_Click</summary>
        ///<remarks>Bestandsberechnung aktualisieren</remarks>
        private void btnRefreshBestand_Click(object sender, EventArgs e)
        {
            ClearFrm();
            SetEingabeFelderEnabled(true);
            InitDGVBestandsliste();
            SetVorschauLabel(const_LableInfo_Rechnung);
            InitGrdRG();
        }
        ///<summary>ctrFaktLager / tsbtnRefreshTarife_Click</summary>
        ///<remarks>Tarife aktualisieren</remarks>
        private void tsbtnRefreshTarife_Click(object sender, EventArgs e)
        {
            InitDGVTarif();
            //InitDGVBestandsliste();            
        }
        ///<summary>ctrFaktLager / tsbtnRefreshAuftraggeber_Click</summary>
        ///<remarks>Auftraggeber aktualisieren</remarks>
        private void tsbtnRefreshAuftraggeber_Click(object sender, EventArgs e)
        {
            InitDGVAbrKunden();
            InitDGVBestandsliste();
        }
        ///<summary>ctrFaktLager / tsbtnRefreshRG_Click</summary>
        ///<remarks>Rechnungen aktualisieren</remarks>
        private void tsbtnRefreshRG_Click(object sender, EventArgs e)
        {
            InitDGVExistRGAuftraggber();
            InitDGVBestandsliste();
        }
        ///<summary>ctrFaktLager / dgvTarife_SelectionChanged_1</summary>
        ///<remarks>Nach Anzeigen einer Rechnung wird die Form für das Berechnen neuer Tarife gesperrrt. Hiermit 
        ///         kann die Form wieder freigegben werden</remarks>
        private void btnFreigabeFrm_Click(object sender, EventArgs e)
        {
            ClearFrm();
            SetEingabeFelderEnabled(true);
            SetVorschauLabel(const_LableInfo_Vorschau);
            //Grid Rechnung leeren
            this.FaktLager.Rechnung = new clsRechnung();
            this.FaktLager.Rechnung._GL_User = this.GL_User;
            this.FaktLager.Rechnung.sys = this.FaktLager.Sys;
            this.FaktLager.Rechnung.RGRevision = null;

            InitGrdRG();
            //Selection exist RG aufheben
            this.dgvExistRGAuftraggeber.ClearSelection();
            this.dgvExistRGAuftraggeber.SelectionMode = GridViewSelectionMode.FullRowSelect;
            SetMenueButtonEnabled();
        }
        ///<summary>ctrFaktLager / SetMenueButtonEnabled</summary>
        ///<remarks></remarks>
        private void SetMenueButtonEnabled()
        {
            //Rechnung wird angezeigt
            if (FaktLager.Rechnung.RechnungExist)
            {
                tsbtnFreigabeFrm.Enabled = true;
                tsbtnPrintDocs.Enabled = true;
                tsbtnRGKorrektur.Enabled = true;
                tsbtnArtList.Enabled = true;
                //löschen nur wenn die Rechnung noch nicht gedruckt ist und die Rechnung die letzte erzeugt Rechnung ist,
                //damit durch eine Löschung keine Lücke im Nummernkreis der Rechnungen entsteht.
                tsbtnRGDelete.Enabled = ((!FaktLager.Rechnung.Druck) && (FaktLager.Rechnung.RGNr == FaktLager.Rechnung.RGNrMAX));
                //STorno der Rechnung nur wenn schon gedruckt, oder wenn die Rechnung nicht gelöscht werden kann
                tsbtnStorno.Enabled = (!FaktLager.Rechnung.ExistStornoZurRG); // && (FaktLager.Rechnung.Druck));// && (!FaktLager.Rechnung.Storno));

                tsbtnRGVorschau.Enabled = false;
                tsbtnRGErstellen.Enabled = false;
            }
            else
            {
                tsbtnFreigabeFrm.Enabled = false;
                tsbtnPrintDocs.Enabled = false;
                tsbtnArtList.Enabled = false;
                //löschen nur wenn die Rechnung noch nicht gedruckt ist
                tsbtnRGDelete.Enabled = false;
                tsbtnStorno.Enabled = false;

                tsbtnRGVorschau.Enabled = true;
                tsbtnRGErstellen.Enabled = true;
            }
        }
        ///<summary>ctrFaktLager / tsbtnArtList_Click</summary>
        ///<remarks>Artikelliste anzeigen</remarks>
        private void tsbtnArtList_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenCtrFaktArtikelList(this);
        }
        ///<summary>ctrFaktLager / tsbtnArtList_Click</summary>
        ///<remarks>Artikelliste anzeigen</remarks>
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string _MandantenName = string.Empty;
            //Functions.SetMandantenDaten(ref tscbMandanten, ref MandantenID, ref _MandantenName);
            //FaktLager.MandantenID = MandantenID;
        }
        ///<summary>ctrFaktLager / dtpAbrVon_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpAbrVon_ValueChanged(object sender, EventArgs e)
        {
            DateTime AbrMonat = ((DateTimePicker)sender).Value.Date;
            dtpAbrVon.Value = ((DateTimePicker)sender).Value;
            dtpAbrBis.Value = Functions.GetLastDayOfMonth(AbrMonat);

            //InitDGVAbrKunden();
            //this.tabAuswahl.SelectedTab = tpAuftraggeber;
        }
        ///<summary>ctrFaktLager / grdRG_RowFormatting</summary>
        ///<remarks></remarks>
        private void grdRG_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            Int32 iTmp = 0;
            string strValue = e.RowElement.RowInfo.Cells["Pos"].Value.ToString();
            if (Int32.TryParse(strValue, out iTmp))
            {
                e.RowElement.DrawFill = true;
                if (_ctrMenu._frmMain.system.Client.Modul.Fakt_eInvoiceIsAvailable)
                {

                    e.RowElement.BackColor = helper_TelerikGridViewRowInfo.GetBackroundColorForEInvoiceItem(e.RowElement.RowInfo);
                }
                else
                {
                    e.RowElement.BackColor = Color.White;
                    //e.RowElement.RowInfo.Cells["Text"].Value = string.Empty;
                }
            }
            else
            {
                e.RowElement.DrawFill = true;
                e.RowElement.BackColor = Color.Beige;
            }
            //e.RowElement.DrawFill = true;
            //e.RowElement.BackColor = Color.Beige;
        }
        private void grdRG_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.HeaderText.Equals("Text"))
            {
                Int32 iTmp = 0;
                string strValue = e.CellElement.RowInfo.Cells["Pos"].Value.ToString();
                if (Int32.TryParse(strValue, out iTmp))
                {
                    e.CellElement.DrawFill = true;
                    e.CellElement.ForeColor = Color.White;
                }
                else
                {
                    e.CellElement.DrawFill = true;
                    e.CellElement.ForeColor = Color.Black;
                }
            }
            else
            {
                e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
            }
        }

        ///<summary>ctrFaktLager / dgvExistRGAuftraggeber_ToolTipTextNeeded</summary>
        ///<remarks>Zeit den Cellinhalt alt Tooltip an.</remarks>
        private void dgvExistRGAuftraggeber_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrFaktLager / dgvOPAuftraggeber_ToolTipTextNeeded</summary>
        ///<remarks>Zeit den Cellinhalt alt Tooltip an.</remarks>
        private void dgvOPAuftraggeber_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrFaktLager / dgvTarife_ToolTipTextNeeded</summary>
        ///<remarks>Zeit den Cellinhalt alt Tooltip an.</remarks>
        private void dgvTarife_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrFaktLager / dgvBestandsliste_ToolTipTextNeeded</summary>
        ///<remarks>Zeit den Cellinhalt alt Tooltip an.</remarks>
        private void dgvBestandsliste_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrFaktLager / grdRG_ToolTipTextNeeded</summary>
        ///<remarks>Zeit den Cellinhalt alt Tooltip an.</remarks>
        private void grdRG_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrFaktLager / dtpAbrBis_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpAbrBis_ValueChanged(object sender, EventArgs e)
        {
            SetLastRechnungsDatum();
        }
        ///<summary>ctrFaktLager / pbClearRGText_Click</summary>
        ///<remarks></remarks>
        private void pbClearRGText_Click(object sender, EventArgs e)
        {
            this.tbRGText.Text = string.Empty;
        }
        ///<summary>ctrFaktLager / pictureBox1_Click</summary>
        ///<remarks></remarks>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.tbFibuInfo.Text = string.Empty;
        }
        ///<summary>ctrFaktLager / tsbtnRGKorrektur_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRGKorrektur_Click(object sender, EventArgs e)
        {
            if (clsMessages.Fakturierung_Korrektur())
            {
                if (
                        (FaktLager.Rechnung.ExistRGId()) &&
                        (!FaktLager.Rechnung.Druck)
                   )
                {
                    FaktLager.Rechnung.sys = this._ctrMenu._frmMain.system;
                    //FaktLager.Rechnung.Delete(true);
                    FaktLager.Rechnung.Revision();

                    this.Tarif.ID = FaktLager.Rechnung.RGPos.TarifID;
                    this.Tarif.Fill();

                    tbTarifSelected.Text = FaktLager.Rechnung.RGRevision.AbrechnungsTarifName;
                    SetEingabeFelderEnabled(true);
                    //this.FaktLager.dtRechnung.Clear();
                    //InitDGVBestandsliste();
                    //Setzen der Rechnungsdaten
                    //RGNr wird beim Speichern gesetzt
                    dtpRGDatum.Value = FaktLager.Rechnung.RGRevision.Datum;
                    dtpAbrVon.Value = FaktLager.Rechnung.AbrZeitraumVon;
                    dtpAbrBis.Value = FaktLager.Rechnung.AbrZeitraumBis;
                    dtpAbrVon.Enabled = true;
                    dtpAbrBis.Enabled = true;
                    dtpRGDatum.Enabled = true;
                    SearchButton = 1;
                    //Auftraggeber = this.FaktLager.Rechnung.Auftraggeber;
                    SetADRToFrm(this.FaktLager.Rechnung.Auftraggeber);
                    //Empfaenger = this.FaktLager.Rechnung.Empfaenger;
                    SearchButton = 10;
                    SetADRToFrm(this.FaktLager.Rechnung.Empfaenger);
                    tbRGNr.Text = FaktLager.Rechnung.RGNr.ToString();
                    tbRGText.Text = FaktLager.Rechnung.InfoText;
                    tbFibuInfo.Text = FaktLager.Rechnung.FibuInfo;
                    SetVorschauLabel(const_LableInfo_Korrektur);
                    InitGrdRG();
                    DataTable dt1 = FaktLager.dtRechnung;
                    this.tabAuswahl.SelectTab(tpRechnungen);
                    this.tsbtnRGVorschau.Enabled = true;
                    this.tsbtnRGErstellen.Enabled = true;
                    this.tsbtnRGKorrektur.Enabled = false;
                }
            }
            else
            {
                this.FaktLager.Rechnung.RGRevision = null;
            }
        }
        ///<summary>ctrFaktLager / tsbtnRefreshRGList_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshRGList_Click(object sender, EventArgs e)
        {
            InitDGVExistRGAuftraggber();
        }
        ///<summary>ctrFaktLager / PrintRG</summary>
        ///<remarks></remarks>
        private void PrintRG(bool bPrintDirect)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                //this._ctrMenu._frmMain.system.CustomizeDocPath(ref this._ctrMenu._frmMain.GL_System, this.FaktLager.Rechnung.Empfaenger);
                //this.ListReportDoc = this._ctrMenu._frmMain.system.Client.ListPrintCenterReportEinlagerung.ToList();
                this._ctrMenu.OpenFrmReporView(this, bPrintDirect);
            }
            else
            {
                this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
                this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.Lagerrechnung.ToString());
                if (this.FaktLager.RepDocSettings is clsReportDocSetting)
                {
                    if (bPrintDirect)
                    {
                        //CHeck ob die Rechnung schon gedruckt, wenn nicht, dann Update
                        if (!this.FaktLager.Rechnung.Druck)
                        {
                            this._ctrMenu.OpenFrmReporView(this, bPrintDirect);
                            this.FaktLager.Rechnung.Druck = true;
                            this.FaktLager.Rechnung.Druckdatum = DateTime.Now;
                            this.FaktLager.Rechnung.UpdateRechnungPrint();
                            this.FaktLager.Rechnung.Fill();
                        }
                        else
                        {
                            if (clsMessages.Fakturierung_RGGSPrintAgain())
                            {
                                this._ctrMenu.OpenFrmReporView(this, bPrintDirect);
                            }
                        }
                    }
                    else
                    {
                        this._ctrMenu.OpenFrmReporView(this, bPrintDirect);
                    }
                }
                else
                {
                    clsMessages.Print_Fail_ReportAssignment();
                }
            }
        }
        ///<summary>ctrFaktLager / PrintRGAnhangPreview</summary>
        ///<remarks></remarks>
        private void PrintRGAnhangPreview(bool bPrintDirect)
        {
            if (this.FaktLager.Rechnung.ID > 0)
            {
                if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                {
                    List<decimal> AuftraggeberMat = new List<decimal>();
                    bool bUseMatDoc = false;
                    clsSystem sys = new clsSystem(Application.StartupPath);
                    sys.GetAuftraggeberAnhangMat(ref _ctrMenu._frmMain.GL_System, _ctrMenu._frmMain.GL_System.sys_MandantenID);
                    if (_ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat != string.Empty)
                    {
                        string[] AuftraggeberSplit = _ctrMenu._frmMain.GL_System.AuftraggeberAnhangMat.Split(',');
                        foreach (string auftraggeber in AuftraggeberSplit)
                        {
                            if (this.FaktLager.Rechnung.Empfaenger == clsADR.GetIDByMatchcode(auftraggeber))
                            {
                                bUseMatDoc = true;
                            }
                        }
                    }
                    if (true) { }
                    ctrPrintLager TmpPrint = new ctrPrintLager();
                    TmpPrint.Hide();
                    TmpPrint._ctrMenu = this._ctrMenu;


                    if (!bUseMatDoc)
                    {
                        this.DokumentenArt = enumDokumentenArt.RGAnhang.ToString();
                    }
                    else
                    {
                        this.DokumentenArt = enumDokumentenArt.RGAnhangMat.ToString();
                    }
                }
                else
                {
                    this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
                    this.FaktLager.RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumDokumentenArt.RGAnhang.ToString());
                }

                this._ctrMenu.OpenFrmReporView(this, bPrintDirect);
                this.DokumentenArt = enumDokumentenArt.LagerRechnung.ToString();
            }
        }
        ///<summary>ctrFaktLager / tsbtnMail_Click</summary>
        ///<remarks></remarks>
        private void tsbtnMail_Click(object sender, EventArgs e)
        {
            if (!this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
            {
                if (this.FaktLager.Rechnung.ID > 0)
                {
                    this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this.FaktLager.Rechnung.Empfaenger, this._ctrMenu._frmMain.system.AbBereich.ID);
                    this.AttachmentList.Clear();
                    //PDF erstellen
                    if (this._ctrMenu._frmMain.system.ReportDocSetting.ExistReportSetting(enumIniDocKey.LagerrechnungMail))
                    {
                        AttachmentList = this.FaktLager.CreateRGAndRGAnhangToPDF(this._ctrMenu._frmMain.system);
                        if (this.AttachmentList.Count > 0)
                        {
                            this._ctrMenu.OpenCtrMailCockpitInFrm(this);
                        }
                    }
                    else
                    {
                        string str = "Für diese Dokumentenart sind keine Dokumente hinterlegt!";
                        clsMessages.Allgemein_InfoTextShow(str);
                    }
                }
            }
        }
        ///<summary>ctrFaktLager / vorschauRechnungToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void vorschauRechnungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintRG(false);
        }
        ///<summary>ctrFaktLager / vorschauAnhangToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void vorschauAnhangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintRGAnhangPreview(false);
        }
        ///<summary>ctrFaktLager / druckRechnungToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void druckRechnungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintRG(true);
            InitDGVExistRGAuftraggber();
        }
        ///<summary>ctrFaktLager / druckAnhangToolStripMenuItem_Click</summary>
        ///<remarks></remarks>
        private void druckAnhangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintRGAnhangPreview(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSourceRGTextADR_CheckedChanged(object sender, EventArgs e)
        {
            cbSourceRGTextTarif.Checked = !(cbSourceRGTextADR.Checked);
            if (cbSourceRGTextADR.Checked)
            {
                if (cAuftraggeber is clsADR)
                {
                    tbRGText.Text = cAuftraggeber.ADRTexte.Lagerrechnung_Text;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSourceRGTextTarif_CheckedChanged(object sender, EventArgs e)
        {
            cbSourceRGTextADR.Checked = !(cbSourceRGTextTarif.Checked);
            if (cbSourceRGTextTarif.Checked)
            {
                if ((this.Tarif is clsTarif) && (!this.Tarif.RGText.Equals(string.Empty)))
                {
                    tbRGText.Text = this.Tarif.RGText;
                }
            }
        }
        /// <summary>
        ///             Save manuel added Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbSaveRGText_Click(object sender, EventArgs e)
        {
            if (this.FaktLager.Rechnung.ID > 0)
            {
                if (tbRGText.Text.Equals(string.Empty))
                {
                    tbRGText.Text = string.Empty;
                }
                this.FaktLager.Rechnung.InfoText = tbRGText.Text; ;
                string strMes = string.Empty;
                string strHeadline = string.Empty;
                if (this.FaktLager.Rechnung.UpdateRechnungInfoText())
                {
                    strMes = "Der Rechnungstext wurde erfolgreich upgedatet!";
                    strHeadline = "INFORMATION";
                }
                else
                {
                    strMes = "Das Update konnte nicht durchgeführt werden!";
                    strHeadline = "ACHTUNG";
                }
                DialogResult result = MessageBox.Show(strMes, strHeadline, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pbSaveFibuInfoText_Click(object sender, EventArgs e)
        {
            if (this.FaktLager.Rechnung.ID > 0)
            {
                if (tbFibuInfo.Text.Equals(string.Empty))
                {
                    tbFibuInfo.Text = string.Empty;
                }
                this.FaktLager.Rechnung.FibuInfo = tbFibuInfo.Text;
                string strMes = string.Empty;
                string strHeadline = string.Empty;
                if (this.FaktLager.Rechnung.UpdateRechnungFibuInfo())
                {
                    strMes = "Der Fibo - Info - Text wurde erfolgreich upgedatet!";
                    strHeadline = "INFORMATION";
                }
                else
                {
                    strMes = "Das Update konnte nicht durchgeführt werden!";
                    strHeadline = "ACHTUNG";
                }
                DialogResult result = MessageBox.Show(strMes, strHeadline, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbClearRGText_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip((PictureBox)sender, "Rechnungstext leeren");
        }

        private void pbSaveRGText_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip((PictureBox)sender, "manuellen Rechnungstext speichern");
        }

        private void pbDeleteFibuInfo_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip((PictureBox)sender, "Fibu-Info leeren");
        }

        private void pbSaveFibuInfoText_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip((PictureBox)sender, "manuelle Fibu-Info speichern");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtsEInvoice_ValueChanged(object sender, EventArgs e)
        {
            //rtsEInvoice.SetToggleState(this.FaktLager.ZUGFeRDAvailable.IsZUGFeRDAvailable);

            string str = string.Empty;
            if (rtsEInvoice.Value)
            {
                lInvoiceSelection.Text = "e - Rechnung aktiviert";
            }
            else
            {
                lInvoiceSelection.Text = "e - Rechnung nicht möglich";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabInvoice.SelectedIndex == 1)
            {
                SetXInvoiceValueToFrm();
            }
        }


    }
}