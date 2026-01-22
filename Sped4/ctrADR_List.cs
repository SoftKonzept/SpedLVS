using Common.Enumerations;
using LVS;
using LVS.Constants;
using LVS.Helper;
using LVS.ViewData;
using Sped4.Classes;
using Sped4.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Localization;

namespace Sped4
{
    public partial class ctrADR_List : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        public frmADRSearch _frmADRSearch;
        internal clsADR ADR;
        internal clsADRCat AdrKat;


        internal bool bUpdateADRDaten;
        internal bool bUpdateContact;
        internal bool bUpdateKundenDaten;
        internal bool bUpdateLieferantenDaten;
        internal bool bUpdateMailingList;
        internal bool bUpdateText;
        internal bool bUpdateLiefGroupDaten;

        internal Int32 iListWidth = 0;
        internal Int32 iADRTabWidth = 0;
        internal Int32 ADRSearchButton = 0;
        //internal Int32 iAdrRange = 1;
        internal Int32 iAdrRange = -1;


        internal delegate void ThreadCtrInvokeEventHandler();
        BackgroundWorker worker;

        //public const Int32 const_AdrRange_Kundenliste = 0;
        //public const Int32 const_AdrRange_AdrListeKomplett = 1;
        //public const Int32 const_AdrRange_AdrListAktiv = 2;
        //public const Int32 const_AdrRange_AdrListePassiv = 3;
        //public const Int32 const_AdrRange_AdrListeKunde = 4;
        //public const Int32 const_AdrRange_AdrListeVersender = 5;
        //public const Int32 const_AdrRange_AdrListeEmpfaenger = 6;
        //public const Int32 const_AdrRange_AdrListeEntlade = 7;
        //public const Int32 const_AdrRange_AdrListeSpedition = 8;
        //public const Int32 const_AdrRange_AdrListeBelade = 9;
        //public const Int32 const_AdrRange_AdrListePost = 10;
        //public const Int32 const_AdrRange_AdrListeRechnung = 11;
        //public const Int32 const_AdrRange_AdrListeDiverse = -1;

        //public const string const_AdrRange_KundenlisteString = "";
        //public const string const_AdrRange_AdrListAktivString = "Adressliste [aktiv]";
        //public const string const_AdrRange_AdrListePassivString = "Adressliste [passiv]";
        //public const string const_AdrRange_AdrListeKundeString = "Kunden-/Auftraggeberadressliste";
        //public const string const_AdrRange_AdrListeVersenderString = "Versandadressliste";
        //public const string const_AdrRange_AdrListeEmpfaengerString = "Empfangsadressliste";
        //public const string const_AdrRange_AdrListeEntladeString = "Entladestellenadressen";
        //public const string const_AdrRange_AdrListeSpeditionString = "Spedition / Transportunternehmer";
        //public const string const_AdrRange_AdrListeBeladeString = "Beladestellenadressen";
        //public const string const_AdrRange_AdrListePostString = "Postadresse";
        //public const string const_AdrRange_AdrListeRechnungString = "Rechnungsadresse";
        //public const string const_AdrRange_AdrListeDiverseString = "Sonstige Adressen";
        internal clsUserBerechtigungen UserBerechtigung = new clsUserBerechtigungen();

        public DataTable adrList = new DataTable();
        public DataTable tempTable = new DataTable();
        internal DataTable dtArbeitsbereiche = new DataTable();
        private Dictionary<Int32, string> DictMcbDocumentArtSource;
        public delegate void ADRTakeOverEventHandler(decimal TakeOverID);
        public event ADRTakeOverEventHandler getADRTakeOver;

        public delegate void frmADRPanelAuftragserfassungCloseEventHandler();
        public event frmADRPanelAuftragserfassungCloseEventHandler closeFrmADRPanelAuftragserfassung;

        public delegate void frmADRPanelFakturierungCloseEventHandler();
        public event frmADRPanelFakturierungCloseEventHandler closeFrmADRPanelFakturierung;


        //public event ADRTakeOverEventHandler getADRTakeOver;  
        private bool AdrSucheAktiv = false;   //wird über frmADRPanelAuftragerfassung auf true gesetzt sonst immer false
        public bool ADRListeKomplett = true;
        public Int32 ADRListAuswahl = 0;
        internal Int32 iSelectedRow = 0;

        //Listenart ADR
        public decimal ADR_Liste = 0;

        Dictionary<int, string> dictSalesKeyDebitor;
        Dictionary<int, string> dictSalesKeyKreditor;


        // ExtraCharge Variablen
        internal clsExtraChargeADR ExtraChargeADR;
        private clsExtraCharge ExtraCharge;

        internal bool bUpdate;
        private DataTable dtTableOfAccount;

        internal clsKundGArtDefault GueterArtDefault;

        // Verweise
        clsADRVerweis ADRVerweis;

        // aktueller Tab
        internal decimal decActivTab = 0;

        internal bool isGridReady = false;

        internal AddressViewData adrVD = new AddressViewData();

        /********************************************************************************
         * 
         * *****************************************************************************/
        ///<summary>ctrADR_List / ctrADR_List</summary>
        ///<remarks>ctr Überschrift</remarks>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        ///<summary>ctrADR_List / ctrADR_List</summary>
        ///<remarks>ctr Überschrift</remarks>
        public ctrADR_List()
        //public ctrADR_List(bool bLoadADRList = true)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            this.scADR.Panel2Collapsed = true;

            this.Visible = false;

            //Dictionary füllen
            dictSalesKeyDebitor = new Dictionary<int, string>();
            dictSalesKeyDebitor.Add(80, "steuerfrei");
            dictSalesKeyDebitor.Add(89, "MwSt. 19,00 %");
            dictSalesKeyDebitor.Add(91, "nicht steuerbarer Umsatz gem. §18b(EU)");
            cbSalesTaxKeyDebitor.DataSource = dictSalesKeyDebitor.ToList();
            cbSalesTaxKeyDebitor.DisplayMember = "Value";
            cbSalesTaxKeyDebitor.ValueMember = "Key";
            cbSalesTaxKeyDebitor.SelectedIndex = 1;

            dictSalesKeyKreditor = new Dictionary<int, string>();
            dictSalesKeyKreditor.Add(40, "steuerfrei");
            dictSalesKeyKreditor.Add(49, "MwSt. 19,00 %");
            dictSalesKeyKreditor.Add(60, "19,00 % EU gem. §13b");
            cbSalesTaxKeyKreditor.DataSource = dictSalesKeyKreditor.ToList();
            cbSalesTaxKeyKreditor.DisplayMember = "Value";
            cbSalesTaxKeyKreditor.ValueMember = "Key";
            cbSalesTaxKeyKreditor.SelectedIndex = 1;

            cbLKZ.DataSource = helper_Laenderkennzeichen.DicCountry().ToList();
            cbLKZ.DisplayMember = "Key";
            cbLKZ.ValueMember = "Key";
            cbLKZ.SelectedIndex = 0;
            cbLand.DataSource = helper_Laenderkennzeichen.DicCountry().ToList();
            cbLand.DisplayMember = "Value";
            cbLand.ValueMember = "Key";
            cbLand.SelectedIndex = 0;

            RadGridLocalizationProvider.CurrentProvider = new clsGermanRadGridLocalizationProvider();
            bUpdateADRDaten = false;
            bUpdateContact = false;
            bUpdateKundenDaten = false;
            bUpdateLieferantenDaten = false;
            bUpdateMailingList = false;
            bUpdateText = false;

            iAdrRange = clsADR.const_AdrRange_AdrListAktiv;

            iListWidth = this.scADR.Panel1.Width;
            iADRTabWidth = this.scADR.Panel2.Width;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted +=
                 new RunWorkerCompletedEventHandler(worker_CompleteWork);

            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
            ResetCtrADRListWidth();
            this.scADR.Panel2Collapsed = true;
            ResetCtrADRListWidth();

            //Combo ASN/DFÜ FileTyp füllen
            cbASNFileTyp.DataSource = clsASN.GetListASNFileTyp();
        }
        ///<summary>ctrADR_List / SetAFColorLabelMyText</summary>
        ///<remarks>ctr Überschrift</remarks>
        private void ctrADR_List_Load(object sender, EventArgs e)
        {
            this.ADR = new clsADR();
            this.ADR.sys = this._ctrMenu._frmMain.system;
            this.ADR.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, 0, true);

            dtArbeitsbereiche.Clear();
            dtArbeitsbereiche = clsArbeitsbereiche.GetArbeitsbereichListByStatus(GL_User.User_ID, true);

            //Customer Setting
            SetCustomerSettings();
        }
        ///<summary>clsADR / CustomerSettings</summary>
        ///<remarks></remarks>
        private void SetCustomerSettings()
        {
            this.menuASNMain.Enabled = (this.GL_User.IsAdmin);
            this.menuVerweisEdit.Enabled = (this.GL_User.IsAdmin);
            //erst mal immer ausblenden wird eventuell nicht mehr gebraucht
            this.tabPageLieferantenGroup.Hide();
            //Ausblenden Tab für DFÜ Kommunikation
            if (!this._ctrMenu._frmMain.system.Client.Modul.ASNTransfer)
            {
                this.tabPageKommunikation.Hide();
            }
            else
            {
                this.tabPageKommunikation.Show();
            }
        }
        ///<summary>ctrADR_List / Copy</summary>
        ///<remarks></remarks>
        public ctrADR_List Copy()
        {
            return (ctrADR_List)this.MemberwiseClone();
        }
        /// <summary>
        /// ctrADR_List / worker_DoWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            isGridReady = false;
            initList1();
        }
        /// <summary>
        /// ctrADR_List / worker_CompleteWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            initList2();
            isGridReady = true;
        }
        ///<summary>ctrADR_List / SetGlobalValue</summary>
        ///<remarks></remarks>
        public void SetGlobalValue(ctrMenu myCtrMenu)
        {
            this._ctrMenu = myCtrMenu;
            this.GL_User = myCtrMenu.GL_User;

            //Klasse Adresse initialisieren
            ADR = new clsADR();
            ADR.sys = this._ctrMenu._frmMain.system;
            ADR.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, 0, false);
        }
        ///<summary>ctrADR_List / ResetCtrADRListWidth</summary>
        ///<remarks>Anpassen der Ctr-Breite</remarks>
        private void ResetCtrADRListWidth()
        {
            if (this.scADR.Panel2Collapsed)
            {
                this.Width = iListWidth + 10;
            }
            else
            {
                this.Width = iListWidth + iADRTabWidth + 10;
            }
            this.Refresh();
        }
        ///<summary>ctrADR_List / SetAFColorLabelMyText</summary>
        ///<remarks>ctr Überschrift</remarks>
        public void SetAFColorLabelMyText(string labelText)
        {
            afColorLabel1.myText = labelText;
        }
        ///<summary>ctrADR_List / SetADRSucheAktiv</summary>
        ///<remarks></remarks>
        public void SetADRSucheAktiv()
        {
            AdrSucheAktiv = true;
        }
        ///<summary>ctrADR_List / ReadADRDataTable</summary>
        ///<remarks></remarks>
        private DataTable ReadADRDataTable(Int32 iAdrMenge)
        {
            /***********************************************
             * Adresslisten
             * 0 = Kundenliste
             * 1 = Adressliste komplett
             * 2 = Adressliste nur aktiv
             * 3 = Adressliste nur passiv
             * 4 = Adressliste aktive Auftraggeber
             * 5 = Adressliste aktive Lieferanten / Versender 
             * 6 = Adressliste aktive Empfänger
             * 7 = Adressliste aktive Entladestellen
             * 8 = Adressliste aktive Speditionen
             * 9 = Adressliste aktive Beladestellen
             * 10= Adressliste aktive Postadressen
             * 11= Adressliste aktive Rechnungsadressen
             * 
             * ********************************************/
            adrList.Rows.Clear();
            switch (iAdrMenge)
            {
                case 0:
                    adrList = clsKunde.dataTableKD(this.GL_User.User_ID);
                    break;
                case 1:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case 2:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case 3:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeKunde:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeVersender:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeEmpfaenger:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeEntlade:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeSpedition:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListePost:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeBelade:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                case clsADR.const_AdrRange_AdrListeRechnung:
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
                default:
                    // Diverse ?? 
                    adrList = clsADR.GetADRListForAdrListCtr(this.GL_User.User_ID, iAdrMenge);
                    break;
            }
            return adrList;
        }
        ///<summary>ctrADR_List / initList</summary>
        ///<remarks></remarks>
        private void initList1()
        {
            DataTable tmpTable = ReadADRDataTable(iAdrRange);
            Thread.Sleep(500);
        }
        ///<summary>ctrADR_List / initList2</summary>
        ///<remarks></remarks>
        private void initList2()
        {
            try
            {
                this.grdADRList.DataSource = null;
                this.grdADRList.DataSource = adrList;
            }
            catch (Exception ex)
            {

            }
            if (this.grdADRList.Rows.Count > 0)
            {
                this.grdADRList.Rows[0].IsSelected = true;
                grdADRList.Columns["ID"].IsVisible = true; // (this.GL_User.IsAdmin);

                for (Int32 i = 0; i <= this.grdADRList.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    //strTmp = string.Empty;
                    string strTmp = this.grdADRList.Rows[i].Cells["ID"].Value.ToString();
                    Decimal.TryParse(strTmp, out decTmp);
                    if (ADR.ID == decTmp)
                    {
                        this.grdADRList.Rows[i].IsSelected = true;
                        this.grdADRList.Rows[i].IsCurrent = true;
                        //SetAdrInfoOnFrm(i);
                    }
                }
                grdADRList.BestFitColumns();
            }
            Functions.SetComboToSelecetedValue(ref cbLand, "D");
            cbFBez.DataSource = Enum.GetNames(typeof(enumFBez));
        }
        ///<summary>ctrADR_List / initList</summary>
        ///<remarks></remarks>
        public void initList(bool bRdy = false)
        {
            if (bRdy)
            {
                isGridReady = true;
            }
            //while (!isGridReady)
            //{

            //}
            isGridReady = false;
            setAdrRange();

            initList1();
            initList2();
            isGridReady = true;
        }
        ///<summary>ctrADR_List / SearchGrdADRList</summary>
        ///<remarks></remarks>
        private void SearchGrdADRList(string Search)
        {
            adrList = ReadADRDataTable(iAdrRange);
            //Spalte hinzufügen
            DataColumn col1 = adrList.Columns.Add("Find", typeof(Boolean));
            bool isFound = false;
            if (Search.ToString() == "")
            {
                iAdrRange = clsADR.const_AdrRange_AdrListeKomplett;
                this.initList();
            }
            else
            {
                if (Convert.ToBoolean(Search.Length))
                {
                    // If the item is not found and you haven't looked at every cell, keep searching
                    //while ((!isFound) & (idx < maxSearches))
                    for (Int32 _Row = 0; _Row <= adrList.Rows.Count - 1; _Row++)
                    {
                        for (Int32 _Column = 1; _Column <= adrList.Columns.Count - 1; _Column++)
                        {
                            // Do all comparing in UpperCase so it is case insensitive
                            //if (grdADRList[_Column, _Row].Value.ToString().ToUpper().Contains(Search))
                            if (adrList.Rows[_Row][_Column].ToString().ToUpper().Contains(Search))
                            {
                                // If found position on the item
                                //grdADRList.FirstDisplayedScrollingRowIndex = _Row;
                                //grdADRList[_Column, _Row].Selected = true;
                                string test = adrList.Rows[_Row][_Column].ToString().ToUpper().Contains(Search).ToString();
                                isFound = true;
                                _Column = adrList.Columns.Count;
                            }

                        }
                        adrList.Rows[_Row]["Find"] = isFound;
                        isFound = false;
                    }
                }
                string Ausgabe = string.Empty;
                DataRow[] rows = adrList.Select("Find =true", "Find");
                tempTable.Clear();
                tempTable = adrList.Clone();
                foreach (DataRow row in rows)
                {
                    Ausgabe = Ausgabe + row["Find"].ToString() + "\n";
                    tempTable.ImportRow(row);
                }
                tempTable.Columns.Remove("Find");
                adrList.Columns.Remove("Find");
                adrList.Clear();
                adrList = tempTable;
                grdADRList.DataSource = adrList;
            }
        }
        ///<summary>ctrADR_List / grdADRList_MouseClick</summary>
        ///<remarks>Contextmenü rechtsklick</remarks>
        private void grdADRList_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.grdADRList != null)
            {
                if (this.grdADRList.CurrentRow != null)
                {
                    if (this.grdADRList.CurrentRow.Index > -1)
                    {
                        if (!this.scADR.Panel2Collapsed)
                        {
                            this.scADR.Panel2Collapsed = true;
                            ResetCtrADRListWidth();
                        }
                        string strTmp = string.Empty;
                        Int32 iCurRow = this.grdADRList.CurrentRow.Index;
                        //SetAdrInfoOnFrm(iCurRow);

                        if (e.Button == MouseButtons.Right)
                            contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
                    }
                }
            }
        }
        ///<summary>ctrADR_List / miUpdate_Click</summary>
        ///<remarks>Contextmenü rechtsklick</remarks>
        private void miUpdate_Click(object sender, EventArgs e)
        {
            if (grdADRList.Rows.Count > 0)
            {
                //--- ausgewählte Datensatz ------
                if (grdADRList.Rows[grdADRList.CurrentRow.Index].Cells[0].Value != null)
                {
                    bUpdateADRDaten = true;
                    string strTmp = grdADRList.Rows[grdADRList.CurrentRow.Index].Cells[0].Value.ToString();
                    decimal decTmp = 0;
                    Decimal.TryParse(strTmp, out decTmp);
                    if (decTmp > 0)
                    {
                        InitTabADREdit();
                        ADR = new clsADR();
                        ADR.ID = decTmp;
                        ADR._GL_User = this.GL_User;
                        ADR._GL_System = this._ctrMenu._frmMain.GL_System;
                        ADR.Fill();
                        this.tabADR.SelectedTab = tabPageADREdit;
                        SetTabEingabeFelder();
                    }
                }
            }
        }
        ///<summary>ctrADR_List / miKontakt_Click</summary>
        ///<remarks>Kontakte</remarks>
        private void miKontakt_Click(object sender, EventArgs e)
        {
            if (ADR.ID > 0)
            {
                SetTabEingabeFelder();
                this.tabADR.SelectedTab = tabPageContactEdit;
                this.scADR.Panel2Collapsed = false;
                ResetCtrADRListWidth();
            }
        }
        ///<summary>ctrADR_List / miAdd_Click</summary>
        ///<remarks>Eine neue Adresse soll erfasst werden. TabADREdit soll eingeblendet werden und alle
        ///         Eingabefelder entsprechend geleert werden über die INIT-Procedure.</remarks>
        private void miAdd_Click(object sender, EventArgs e)
        {
            this.decActivTab = 0;
            tabADR.SelectedIndex = 0;
            this.ADR = new clsADR();
            bUpdateADRDaten = false;
            this.tabADR.SelectedTab = tabPageADREdit;
            SetTabEingabeFelder();
            ClearTabADREdit();
            ClearTABContactEdit();
            ClearTabKundenEdit();
            InitTabASNEdit();
            this.scADR.Panel2Collapsed = false;
            ResetCtrADRListWidth();
            //InitTabADREdit();

            mmADRAnschrift.SetExpandCollapse(AFMinMaxPanel.EStatus.Collapsed);
            mmPanelADRCommunication.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
            mmPanelADRKommission.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
            mmPanelUserInfoText.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
            mmPanelADRZusweisung.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
            mmPanelADRCat.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
            mmPanelADRPost.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);


        }
        ///<summary>ctrADR_List / ClearTabVerweise</summary>
        ///<remarks></remarks>
        private void ClearTabVerweise()
        {
            InitTabASNEdit();
        }
        ///<summary>ctrADR_List / SetOldSearchList</summary>
        ///<remarks></remarks>
        public void SetOldSearchList()
        {
            string oldSearch = txtSearch.Text;
            if (oldSearch != string.Empty)
            {
                EventArgs e = new EventArgs();
                this.txtSearch_TextChanged(this.txtSearch.Text, e);
            }
        }
        ///<summary>ctrADR_List / miDetails_Click</summary>
        ///<remarks>Details zur gewählten Adresse</remarks>
        private void miDetails_Click(object sender, EventArgs e)
        {
            //--- ausgewählte Datensatz ------
            if (this.grdADRList.CurrentRow != null)
            {
                //if (this.grdADRList.Rows[iSelectedRow].Cells[0].Value != null)
                if (this.grdADRList.Rows[this.grdADRList.CurrentRow.Index].Cells[0].Value != null)
                {
                    decimal decTmp = 0;
                    string strTmp = grdADRList.Rows[this.grdADRList.CurrentRow.Index].Cells[0].Value.ToString();
                    // strID = grdADRList.Rows[iSelectedRow].Cells[0].Value.ToString();
                    Decimal.TryParse(strTmp, out decTmp);
                    if (decTmp > 0)
                    {
                        ADR.ID = decTmp;
                        ADR.Fill();
                    }
                }
            }
        }
        ///<summary>ctrADR_List / miNewKD_Click</summary>
        ///<remarks>neuen Kunden anlegen</remarks>
        private void miNewKD_Click(object sender, EventArgs e)
        {
            if (grdADRList.Rows.Count >= 1)
            {
                if (ADR.ID > 0)
                {
                    SetTabEingabeFelder();
                    this.tabADR.SelectedTab = tabPageKundeEdit;
                    this.scADR.Panel2Collapsed = false;
                    ResetCtrADRListWidth();
                }
            }
        }
        ///<summary>ctrADR_List / txtSearch_TextChanged</summary>
        ///<remarks></remarks>
        public void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAdr();
        }
        ///<summary>ctrADR_List / SearchAdr</summary>
        ///<remarks>Liste der Aktuellen Adressen durchsuchen</remarks>
        private void SearchAdr()
        {
            if (cbSearchArt.Checked)
            {
                //SearchGrd(txtSearch.Text.ToUpper()); // Volltextsuche
                SearchGrdADRList(txtSearch.Text.ToUpper());
            }
            else
            {
                if (txtSearch.Text.ToString() == "")
                {
                    //initList(ADRListeKomplett);
                    initList();
                }
                else
                {
                    //Block verschoben .. doppeltes laden der adrList bei leerem suchstring verhindert
                    adrList = ReadADRDataTable(iAdrRange);
                    string SearchText = txtSearch.Text.ToString();
                    string Ausgabe = string.Empty;

                    DataRow[] rows = adrList.Select("Suchbegriff LIKE '" + SearchText + "%'", "Suchbegriff");
                    tempTable.Clear();
                    tempTable = adrList.Clone();

                    foreach (DataRow row in rows)
                    {
                        Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                        tempTable.ImportRow(row);
                    }
                    grdADRList.DataSource = tempTable;
                    if (grdADRList.RowCount > 0)
                    {
                        grdADRList.Rows[0].IsSelected = true;
                        grdADRList.CurrentRow = grdADRList.Rows[0];
                        Int32 iCurRow = this.grdADRList.CurrentRow.Index;
                        //SetAdrInfoOnFrm(iCurRow);
                    }
                }
            }
        }
        ///<summary>ctrADR_List / miCloseCtr_Click</summary>
        ///<remarks></remarks>
        private void miCloseCtr_Click(object sender, EventArgs e)
        {
            CloseCtr();
        }
        ///<summary>ctrADR_List / CloseCtr</summary>
        ///<remarks></remarks>
        private void CloseCtr()
        {
            if (this._frmADRSearch == null)
            {
                Int32 Count = this.ParentForm.Controls.Count;
                for (Int32 i = 0; (i <= (Count - 1)); i++)
                {
                    if (this.ParentForm.Controls[i].Name == "TempSplitterADR")
                    {
                        this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
                        //i = Count - 1;      // ist nur ein Controll vorhanden
                    }
                }

                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRPanelFakturierung)) != null)
                {
                    closeFrmADRPanelFakturierung();
                }
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRSearch)) != null)
                {
                    CloseFrmADRPanelAuftragserfassung();
                }
                this.Dispose();
            }
            else
            {
                this._frmADRSearch.Hide();
                //this._frmADRSearch.CloseFrm();
                if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRSearch)) != null)
                {
                    Functions.frm_FormTypeClose(typeof(frmADRSearch));
                }
            }
        }
        ///<summary>ctrADR_List / cbMatchcodeSearch_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbMatchcodeSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSearchArt.Checked)
            {
                cbSearchArt.Text = "Volltextsuche aktiviert";
                lSSuchbegriff.Text = "Suche nach...";

            }
            else
            {
                cbSearchArt.Text = "Volltextsuche deaktiviert";
                lSSuchbegriff.Text = "Suchbegriff";
            }
        }
        ///<summary>ctrADR_List / miExportExcel_Click</summary>
        ///<remarks></remarks>
        private void miExportExcel_Click(object sender, EventArgs e)
        {
            string strFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_Adressliste.xls";
            saveFileDialog.FileName = strFileName;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            strFileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.grdADRList, strFileName, ref openExportFile, this.GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(strFileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "ADR - LIST - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrADR_List / toolStripButton1_Click</summary>
        ///<remarks>Auflistung aktualisieren.</remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            initList();
        }
        ///<summary>ctrADR_List / grdADRList_MouseDoubleClick</summary>
        ///<remarks>Wertezuweisung der Klasseneigenschaften.</remarks>
        private void grdADRList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }
        ///<summary>ctrADR_List / grdADRList_CellClick</summary>
        ///<remarks></remarks>
        private void grdADRList_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                decimal decTmp = 0;
                string strTmp = this.grdADRList.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    ADR = new clsADR();
                    ADR._GL_User = this.GL_User;
                    ADR._GL_System = this._ctrMenu._frmMain.GL_System;
                    ADR.ID = decTmp;
                    ADR.Fill();
                    SetAdrInfo();
                }
            }
        }
        ///<summary>ctrADR_List / grdADRList_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void grdADRList_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (AdrSucheAktiv)
            {
                //Adresssuche ID wird übergeben und frm geschlossen
                getADRTakeOver(ADR.ID);
                if (this._frmADRSearch != null)
                {
                    //wenn die Frm nach der ID übergabe direkt geschlossen wird, dann kommte es zur 
                    //Exception im Telerik Grid. Um dies zu umgehen wird die Frm hier per Hide() 
                    //ausgeblendet und dann mit Hilfe eines Backgroundworkers in der FrmADRSearch
                    //der Sleepbefehl 1 s aufgerufen, so dass der normale Thread dieses Event 
                    //dann verlassen hat und die Frm geschlossen werden kann.
                    this._frmADRSearch.Hide();
                    //this.scADR.Panel2Collapsed = true;
                    //ResetCtrADRListWidth();
                    this._frmADRSearch.WaitForClosing();
                }
            }
            else
            {
                if (this.scADR.Panel2Collapsed)
                {
                    this.scADR.Panel2Collapsed = false;
                }
                //Init TabADREdit         
                tabADR.SelectedTab = tabPageADREdit;
                bUpdateADRDaten = true;
                InitSelectedTabPage(this.tabADR.SelectedTab.Name);
            }
        }
        ///<summary>ctrADR_List / ClearCtrForDataChange</summary>
        ///<remarks></remarks>
        private void ClearCtrForDataChange()
        {
            ClearADRTextInputFields();
            ClearMailingListInputField();
            ClearTABContactEdit();
            ClearTabKundenEdit();

            //gridsleeren
            grdKontakte.Rows.Clear();
            dgvMailingList.Rows.Clear();
            dgvMailingListAdministrationMailingList.Rows.Clear();
            dgvKontaktMails.Rows.Clear();
            dgvMailingListMember.Rows.Clear();
        }
        ///<summary>ctrADR_List / grdKontakte_CellClick</summary>
        ///<remarks>Wenn ein Kontaktdatensatz angeklickt wird, soll folgendes passieren:
        ///         - Kontakteingabefelder leeren
        ///         - Kontakteingabefelder enabled=false
        ///         - Deletebutton enabled=false</remarks>
        private void grdKontakte_CellClick(object sender, GridViewCellEventArgs e)
        {
            ClearTabKundenEdit();
            SetTabContaktEingabefelderEnabled(false);
            tsbtnDeleteContact.Enabled = false;
        }
        ///<summary>ctrADR_List / miDelete_Click</summary>
        ///<remarks>Wertezuweisung der Klasseneigenschaften.</remarks>
        private void miDelete_Click(object sender, EventArgs e)
        {
            if (GL_User.write_ADR)
            {
                if (this.grdADRList.Rows.Count >= 1)
                {
                    if ((this.ADR is clsADR) && (this.ADR.ID > 0))
                    {
                        if (!ADR.IsUsed)
                        {
                            if (clsMessages.ADR_DeleteDatenSatz())
                            {
                                ADR.DeleteADR();

                                clsMessages.ADR_ADRgeloescht();
                                txtSearch.Text = string.Empty;
                                //initList(ADRListeKomplett);
                                initList1();
                                initList2();
                            }
                        }
                        else
                        {
                            clsMessages.DeleteDenied();
                        }
                    }


                    ////--- ausgewählte Datensatz ------
                    //if (this.grdADRList.Rows[this.grdADRList.CurrentRow.Index].Cells[0].Value != null)
                    //{
                    //    decimal adrID = (decimal)this.grdADRList.Rows[this.grdADRList.CurrentRow.Index].Cells["ID"].Value;

                    //    ADR.ID = adrID;
                    //    ADR.Fill();

                    //    if (!ADR.IsUsed)
                    //    {
                    //        if (clsMessages.ADR_DeleteDatenSatz())
                    //        {
                    //            ADR.DeleteADR();

                    //            clsMessages.ADR_ADRgeloescht();
                    //            txtSearch.Text = string.Empty;
                    //            //initList(ADRListeKomplett);
                    //            initList1();
                    //            initList2();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        clsMessages.DeleteDenied();
                    //    }
                    //}
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        ///<summary>ctrADR_List / miKDListe_Click</summary>
        ///<remarks></remarks>
        private void CloseFrmADRPanelAuftragserfassung()
        {
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRSearch)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmADRSearch));
            }
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmADRPanelFrachtvergabe)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmADRPanelFrachtvergabe));
            }
        }
        ///<summary>ctrADR_List / miKDListe_Click</summary>
        ///<remarks></remarks>
        private void miKDListe_Click(object sender, EventArgs e)
        {
            ADRListeKomplett = false;
            iAdrRange = clsADR.const_AdrRange_Kundenliste;
            afColorLabel1.myText = "Adressliste [Kunden]";
            initList();
        }
        ///<summary>ctrADR_List / miListeKomplett_Click</summary>
        ///<remarks></remarks>
        private void miListeKomplett_Click(object sender, EventArgs e)
        {
            //ADRListeKomplett = true;
            iAdrRange = clsADR.const_AdrRange_AdrListeKomplett;
            afColorLabel1.myText = "Adressliste [komplett]";
            initList();
        }
        ///<summary>ctrADR_List / miFrachtkonditionen_Click</summary>
        ///<remarks>Öffnet die Tariferfasssung für die Adresse</remarks>
        private void miFrachtkonditionen_Click(object sender, EventArgs e)
        {
            //wenn die Kundenliste angezeigt wird, dann kann die Tariferfassung 
            //geöffnet werden, sonst muss erst die Kundenliste geladen werden
            if (isGridReady)
            {
                if (iAdrRange == clsADR.const_AdrRange_Kundenliste)
                {
                    if (this.grdADRList.CurrentRow != null)
                    {
                        //decimal myDecADR_ID = (decimal)this.grdADRList.Rows[this.grdADRList.CurrentRow.Index].Cells["ID"].Value;
                        decimal myDecADR_ID = (decimal)this.grdADRList.CurrentRow.Cells["ID"].Value;
                        this._ctrMenu.OpenCtrTariferfassung(myDecADR_ID);
                    }
                }
                else
                {
                    iAdrRange = clsADR.const_AdrRange_Kundenliste;
                    initList();
                }
            }
        }
        /////<summary>ctrADR_List / SetAdrInfoOnFrm</summary>
        /////<remarks></remarks>
        //private void SetAdrInfoOnFrm(Int32 iRow)
        //{
        //    string strTmp = string.Empty;
        //    if (this.grdADRList.CurrentRow != null)
        //    {                
        //        decimal decTmp = 0;
        //        //string strID = this.grdADRList.Rows[iRow].Cells["ID"].Value.ToString();
        //        string strID = this.grdADRList.CurrentRow.Cells["ID"].Value.ToString();
        //        Decimal.TryParse(strID, out decTmp);
        //        if (decTmp > 0)
        //        {
        //            ADR.ID = decTmp;
        //            ADR.Fill();
        //            strTmp = strTmp + this.grdADRList.CurrentRow.Cells["Name1"].Value.ToString() + Environment.NewLine;
        //            strTmp = strTmp + this.grdADRList.CurrentRow.Cells["Strasse"].Value.ToString();
        //            strTmp = strTmp + " " + this.grdADRList.CurrentRow.Cells["HausNr"].Value.ToString() + Environment.NewLine;
        //            strTmp = strTmp + this.grdADRList.CurrentRow.Cells["PLZ"].Value.ToString() + " -  ";
        //            strTmp = strTmp + this.grdADRList.CurrentRow.Cells["Ort"].Value.ToString() + Environment.NewLine;
        //            strTmp = strTmp + this.grdADRList.CurrentRow.Cells["Land"].Value.ToString();
        //        }
        //    }
        //    tbAdrInfo.Text = strTmp;
        //}
        ///<summary>ctrADR_List / SetAdrInfo</summary>
        ///<remarks></remarks>
        private void SetAdrInfo()
        {
            tbAdrInfo.Text = ADR.ADRString;
        }
        ///<summary>ctrADR_List / SetTabEingabeFelder</summary>
        ///<remarks>Füllt alle Eingabefelder in den Tabs</remarks>
        private void SetTabEingabeFelder()
        {
            SetADRDatenToTabADREdit();
        }
        ///<summary>ctrADR_List / grdADRList_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void grdADRList_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrADR_List / dgvMailingListAdministrationMailingList_MouseClick</summary>
        ///<remarks>Es werden die Mitglieder der Verteilerliste und zum Anderen alle übrigen Mailkontakte geladen.</remarks>
        private void dgvMailingListAdministrationMailingList_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvMailingListAdministrationMailingList.Rows.Count > 0)
            {
                decimal decTmp = -1;
                string strTmp = this.dgvMailingListAdministrationMailingList.Rows[this.dgvMailingListAdministrationMailingList.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > -1)
                {
                    this.ADR.Kontakt.MailingList.ID = decTmp;
                    this.ADR.Kontakt.MailingList.FillByID();
                    InitDGVContactMails();
                    InitDGVMailingListMemeber();
                }
            }
        }
        ///<summary>ctrADR_List / dgvMailingListAdministrationMailingList_MouseDoubleClick</summary>
        ///<remarks>Es werden die Mitglieder der Verteilerliste und zum Anderen alle übrigen Mailkontakte geladen.</remarks>
        private void dgvMailingListAdministrationMailingList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgvMailingListAdministrationMailingList.Rows.Count > 0)
            {
                InitDGVContactMails();
                InitDGVMailingListMemeber();
            }
        }
        ///<summary>ctrADR_List / dgvKontaktMails_CellClick</summary>
        ///<remarks>Setzen des Select-Flags.</remarks>
        private void dgvKontaktMails_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name.Equals("Select"))
            {
                if ((bool)e.Value == true)
                {
                    this.dgvKontaktMails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                }
                else
                {
                    this.dgvKontaktMails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                }
            }
        }
        ///<summary>ctrADR_List / dgvMailingListMember_CellClick</summary>
        ///<remarks>Setzen des Select-Flags.</remarks>
        private void dgvMailingListMember_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name.Equals("Select"))
            {
                if ((bool)e.Value == true)
                {
                    this.dgvMailingListMember.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                }
                else
                {
                    this.dgvMailingListMember.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                }
            }
        }
        ///<summary>ctrADR_List / tsbtnMoveOutOfMember_Click</summary>
        ///<remarks>MailingList Member entfernen</remarks>
        private void tsbtnMoveOutOfMember_Click(object sender, EventArgs e)
        {
            UpdateMailingList();
        }
        ///<summary>ctrADR_List / tsbtnMoveToMember_Click</summary>
        ///<remarks>Neue Member zur Mailing List hinzufügen</remarks>
        private void tsbtnMoveToMember_Click(object sender, EventArgs e)
        {
            UpdateMailingList();
        }
        ///<summary>ctrADR_List / UpdateMailingList</summary>
        ///<remarks></remarks>
        private void UpdateMailingList()
        {
            this.ADR.Kontakt.MailingList.MailingListAssignment.Update(this.ADR.Kontakt.dtMailKontakte, this.ADR.Kontakt.MailingList.MailingListAssignment.dtMailingListMemeber, this.ADR.Kontakt.MailingList.ID);
            this.InitDGVContactMails();
            this.InitDGVMailingListMemeber();
        }
        ///<summary>ctrADR_List / tsbtnMailingListMailRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnMailingListMailRefresh_Click(object sender, EventArgs e)
        {
            InitDGVContactMails();
        }
        ///<summary>ctrADR_List / tsbtnMailingListMemberRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnMailingListMemberRefresh_Click(object sender, EventArgs e)
        {
            InitDGVMailingListMemeber();
        }
        ///<summary>ctrADR_List / InitDGVMailingListAdministration</summary>
        ///<remarks></remarks>
        private void InitDGVMailingListAdministration()
        {
            this.dgvMailingListAdministrationMailingList.DataSource = this.ADR.Kontakt.MailingList.dtMailingList;
            if (this.dgvMailingListAdministrationMailingList.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvMailingListAdministrationMailingList.Columns.Count - 1; i++)
                {
                    Int32 iWidthCol = 0;
                    switch (this.dgvMailingListAdministrationMailingList.Columns[i].HeaderText.ToString())
                    {
                        case "Bezeichnung":
                            this.dgvMailingListAdministrationMailingList.Columns[i].IsVisible = true;
                            //Spaltenbreite mindestens DisplayedCells sonst 1/3 der Gesamtbreite des GRID
                            iWidthCol = this.dgvMailingListAdministrationMailingList.Width / 3;
                            this.dgvMailingListAdministrationMailingList.Columns["Bezeichnung"].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            if (this.dgvMailingListAdministrationMailingList.Columns["Bezeichnung"].Width < iWidthCol)
                            {
                                this.dgvMailingListAdministrationMailingList.Columns["Bezeichnung"].Width = iWidthCol;
                            }
                            break;

                        case "Beschreibung":
                            this.dgvMailingListAdministrationMailingList.Columns[i].IsVisible = true;
                            this.dgvMailingListAdministrationMailingList.Columns[i].WrapText = true;
                            iWidthCol = this.dgvMailingListAdministrationMailingList.Width * 2 / 3;
                            this.dgvMailingListAdministrationMailingList.Columns["Beschreibung"].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            if (this.dgvMailingListAdministrationMailingList.Columns["Beschreibung"].Width < iWidthCol)
                            {
                                this.dgvMailingListAdministrationMailingList.Columns["Beschreibung"].Width = iWidthCol;
                            }
                            break;

                        default:
                            this.dgvMailingListAdministrationMailingList.Columns[i].IsVisible = false;
                            break;

                    }
                }
            }
        }
        ///<summary>ctrADR_List / dgvMailingListAdministrationMailingList_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void dgvMailingListAdministrationMailingList_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrADR_List / InitDGVContactMails</summary>
        ///<remarks></remarks>
        private void InitDGVContactMails()
        {
            this.ADR.Kontakt.GetEMailListForMailingListAdministration();
            this.dgvKontaktMails.DataSource = this.ADR.Kontakt.dtMailKontakte;

            if (this.grdKontakte.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvKontaktMails.Columns.Count - 1; i++)
                {
                    string strColName = this.dgvKontaktMails.Columns[i].HeaderText.ToString();

                    switch (strColName)
                    {
                        case "Select":
                            this.dgvKontaktMails.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 0);
                            break;

                        case "Nachname":
                            this.dgvKontaktMails.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 1);
                            break;
                        case "Vorname":
                            this.dgvKontaktMails.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 2);
                            break;
                        case "Abteilung":
                            this.dgvKontaktMails.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 3);
                            break;
                        case "Mail":
                            this.dgvKontaktMails.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 4);
                            break;

                        //Spalten ausblenden
                        default:
                            this.dgvKontaktMails.Columns[i].IsVisible = false;
                            break;
                    }
                }
                this.dgvKontaktMails.BestFitColumns();
                //Grouping
                GroupDescriptor grFirma = new GroupDescriptor();
                grFirma.GroupNames.Add("Firma", System.ComponentModel.ListSortDirection.Ascending);
                this.dgvKontaktMails.GroupDescriptors.Clear();
                this.dgvKontaktMails.GroupDescriptors.Add(grFirma);

                this.dgvKontaktMails.MasterTemplate.EnableGrouping = true;
                this.dgvKontaktMails.MasterTemplate.AllowDragToGroup = false;
                this.dgvKontaktMails.MasterTemplate.AutoExpandGroups = true;
                this.dgvKontaktMails.ShowGroupPanel = false;
            }
        }
        ///<summary>ctrADR_List / dgvKontaktMails_ToolTipTextNeeded</summary>
        ///<remarks></remarks> 
        private void dgvKontaktMails_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrADR_List / InitDGVMailingListMemeber</summary>
        ///<remarks></remarks>
        private void InitDGVMailingListMemeber()
        {
            if (this.ADR.Kontakt.MailingList.MailingListAssignment != null)
            {
                this.dgvMailingListMember.DataSource = this.ADR.Kontakt.MailingList.MailingListAssignment.dtMailingListMemeber;
            }
            if (this.dgvMailingListMember.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= this.dgvMailingListMember.Columns.Count - 1; i++)
                {
                    string strColName = this.dgvMailingListMember.Columns[i].HeaderText.ToString();

                    switch (strColName)
                    {
                        case "Select":
                            this.dgvMailingListMember.Columns.Move(this.dgvMailingListMember.Columns[i].Index, 0);
                            break;

                        case "ViewID":
                            //this.dgvKontaktMails.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 1);
                            this.dgvMailingListMember.Columns[i].HeaderText = "Firma";
                            break;
                        case "Nachname":
                            this.dgvMailingListMember.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 1);
                            break;
                        case "Vorname":
                            this.dgvMailingListMember.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 2);
                            break;
                        case "Abteilung":
                            this.dgvMailingListMember.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 3);
                            break;
                        case "Mail":
                            this.dgvMailingListMember.Columns.Move(this.dgvKontaktMails.Columns[i].Index, 4);
                            break;

                        //Spalten ausblenden
                        default:
                            this.dgvMailingListMember.Columns[i].IsVisible = false;
                            break;
                    }
                }
                this.dgvKontaktMails.BestFitColumns();
                //Grouping
                GroupDescriptor grFirma = new GroupDescriptor();
                grFirma.GroupNames.Add("ViewID", System.ComponentModel.ListSortDirection.Ascending);
                this.dgvMailingListMember.GroupDescriptors.Clear();
                this.dgvMailingListMember.GroupDescriptors.Add(grFirma);

                this.dgvMailingListMember.MasterTemplate.EnableGrouping = true;
                this.dgvMailingListMember.MasterTemplate.AllowDragToGroup = false;
                this.dgvMailingListMember.MasterTemplate.AutoExpandGroups = true;
                this.dgvMailingListMember.ShowGroupPanel = false;
            }
        }
        ///<summary>ctrADR_List / dgvMailingListMember_ToolTipTextNeeded</summary>
        ///<remarks></remarks>
        private void dgvMailingListMember_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrADR_List / tsbtnSelectAllContactMail_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSelectAllContactMail_Click(object sender, EventArgs e)
        {
            SetSelectColumn(this.ADR.Kontakt.dtMailKontakte, true);
        }
        ///<summary>ctrADR_List / tsbtnUnSelectAllContactMail_Click</summary>
        ///<remarks></remarks>
        private void tsbtnUnSelectAllContactMail_Click(object sender, EventArgs e)
        {
            SetSelectColumn(this.ADR.Kontakt.dtMailKontakte, false);
        }
        ///<summary>ctrADR_List / tsbtnSelectAllMailingListMember_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSelectAllMailingListMember_Click(object sender, EventArgs e)
        {
            SetSelectColumn(this.ADR.Kontakt.MailingList.MailingListAssignment.dtMailingListMemeber, true);
        }
        ///<summary>ctrADR_List / tsbtnUnSelectAllMailingListMember_Click</summary>
        ///<remarks></remarks>
        private void tsbtnUnSelectAllMailingListMember_Click(object sender, EventArgs e)
        {
            SetSelectColumn(this.ADR.Kontakt.MailingList.MailingListAssignment.dtMailingListMemeber, false);
        }
        ///<summary>ctrEinlagerung / tsbtnAllCheck_Click</summary>
        ///<remarks>Markiert alle Artikeldatensätze in der Table als markiert / unmarkiert</remarks> 
        private void SetSelectColumn(DataTable dt, bool bChecked)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                dt.Rows[i]["Select"] = bChecked;
            }
        }

        /************************************************************************************************************
         * 
         *                                  STeuerung der Tabs
         * 
         * **********************************************************************************************************/
        ///<summary>ctrADR_List / tabADR_SelectedIndexChanged</summary>
        ///<remarks>Hierüber werden die einzelen Tabe geseuter, was bedeutet, dass für den ausgewählten Tab die folgenden 
        ///         Procedures durchgeführt werden:
        ///         - Tab mit Daten füllen
        ///         - MinMaxPanels ein/ausklappen
        ///         
        ///         Indexe der Tab
        ///         0 = ADR Edit
        ///         1 = Kontakte
        ///         3 = Kundendaten
        ///         </remarks>
        private void tabADR_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitSelectedTabPage(tabADR.SelectedTab.Name);
        }
        ///<summary>ctrADR_List / InitSelectedTabPage</summary>
        ///<remarks></remarks>
        private void InitSelectedTabPage(string tabPageName)
        {
            switch (tabPageName)
            {
                case "tabPageADREdit":
                    //MinMaxPanel
                    this.mmADRAnschrift.SetExpandCollapse(AFMinMaxPanel.EStatus.Collapsed);
                    this.mmPanelADRCommunication.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelADRKommission.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelUserInfoText.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelADRZusweisung.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelADRPost.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelADRCat.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.scADR.Panel2Collapsed = false;
                    InitTabADREdit();
                    break;
                case "tabPageKommunikation":
                    InitTabASNEdit();
                    break;

                case "tabPageContactEdit":
                    InitTabContactEdit();
                    //MinMaxPanel
                    this.mmPanelContactEdit.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelContactMailingListAdd.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelContactMailingListAdministration.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    break;

                case "tabPageKundeEdit":
                    InitTabKundeEdit();
                    //MinMaxPanel
                    this.mmPanelKundeFibuDaten.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelKundeBank.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelKundeKontaktRechnung.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    break;

                case "tabPageDocuments":
                    InitTabDocumentsEdit();
                    //MinMaxPanel
                    //this.mmPanelDocuments.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    this.mmPanelTexte.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    break;

                case "tabPageExtraCharge":
                    InitTabExtraChargeEdit();
                    break;

                case "tabPageLieferantenGroup":
                    //Baustelle ausblenben
                    //InitTabPageLiefGroup();
                    tabADR.SelectedTab = tabPageADREdit;
                    break;

                case "tabPageDefaultGuterart":
                    InitTabDefaultGueterartEdit();
                    break;

            }
            decActivTab = this.tabADR.SelectedIndex;
        }
        /*********************************************************************************************************
         *                              TabADREdit -  Procedures
         * ******************************************************************************************************/
        ///<summary>ctrADR_List / tsbtnOpenEdit_Click</summary>
        ///<remarks>ADR-Datenerfassung ein-/ausblenden</remarks>
        private void tsbtnOpenEdit_Click(object sender, EventArgs e)
        {
            ShowAndHideADREdit();
        }
        ///<summary>ctrADR_List / ShowAndHideADREdit</summary>
        ///<remarks>ADR-Datenerfassung ein-/ausblenden.</remarks>
        public void ShowAndHideADREdit()
        {
            if (this.scADR.Panel2Collapsed == true)
            {
                this.scADR.Panel2Collapsed = false;
                SettsbtnOpenEditImage();
            }
            else
            {
                this.scADR.Panel2Collapsed = true;
                SettsbtnOpenEditImage();
            }
            ResetCtrADRListWidth();
        }
        ///<summary>ctrADR_List / SettsbtnOpenEditImage</summary>
        ///<remarks></remarks>
        private void SettsbtnOpenEditImage()
        {
            if (this.scADR.Panel2Collapsed == true)
            {
                this.tsbtnOpenEdit.Image = Sped4.Properties.Resources.layout_left;
            }
            else
            {
                this.tsbtnOpenEdit.Image = Sped4.Properties.Resources.layout;
            }
        }
        ///<summary>ctrADR_List / InitTabADREdit</summary>
        ///<remarks>Initialisiert den Tab</remarks>
        private void InitTabADREdit()
        {
            ClearTabADREdit();
            if (this.ADR.ID > 0)
            {
                SetTabEingabeFelder();
            }
            ResetCtrADRListWidth();
            SettsbtnOpenEditImage();
            //Functions.SetComboToSelecetedValue(ref cbLand, "D");
            //Adresskategorien
            InitAdrCategory();
            //Post Eintstellungen 
            if (this.ADR.Kontakt.MailingList != null)
            {
                if (this.ADR.Kontakt.MailingList.dtMailingList != null)
                {

                    DataTable VerteilerSource = this.ADR.Kontakt.MailingList.dtMailingList;
                    DataTable dtPostVerteilerLfs = VerteilerSource.Copy();
                    DataTable dtPostVerteilerRG = VerteilerSource.Copy();
                    DataTable dtPostVerteilerAnlagen = VerteilerSource.Copy();
                    DataTable dtPostVerteilerListen = VerteilerSource.Copy();
                    DataTable dtPostVerteilerAnzeigen = VerteilerSource.Copy();

                    //Postverteiler Lfs
                    cbPostVerteilerLfs.DataSource = dtPostVerteilerLfs;
                    cbPostVerteilerLfs.DisplayMember = "Bezeichnung";
                    cbPostVerteilerLfs.ValueMember = "ID";

                    //Postverteiler RG
                    cbPostVerteilerRG.DataSource = dtPostVerteilerRG;
                    cbPostVerteilerRG.DisplayMember = "Bezeichnung";
                    cbPostVerteilerRG.ValueMember = "ID";

                    //Postverteiler Anlagen
                    cbPostVerteilerAnlage.DataSource = dtPostVerteilerAnlagen;
                    cbPostVerteilerAnlage.DisplayMember = "Bezeichnung";
                    cbPostVerteilerAnlage.ValueMember = "ID";

                    //Postverteiler Listen
                    cbPostVerteilerListen.DataSource = dtPostVerteilerListen;
                    cbPostVerteilerListen.DisplayMember = "Bezeichnung";
                    cbPostVerteilerListen.ValueMember = "ID";

                    //Postverteiler Anzeigen
                    cbPostVerteilerAnzeigen.DataSource = dtPostVerteilerAnzeigen;
                    cbPostVerteilerAnzeigen.DisplayMember = "Bezeichnung";
                    cbPostVerteilerAnzeigen.ValueMember = "ID";
                }
            }
        }
        ///<summary>ctrADR_List / ClearTabADREdit</summary>
        ///<remarks>Leert alle Eingabefelder</remarks>
        private void ClearTabADREdit()
        {
            decimal deCTMp = ADR.ID;
            numStdVon.Value = 6;
            numMinVon.Value = 0;
            numStdBis.Value = 15;
            numMinBis.Value = 0;

            pbINr.Visible = false;
            btnKDNrCheck.Visible = false;
            cbDummyADR.Checked = false;
            cbAdrIsActiv.Checked = true;

            tbSuchname.Text = string.Empty;
            tbName1.Text = string.Empty;
            tbName2.Text = string.Empty;
            tbName3.Text = string.Empty;
            tbStr.Text = string.Empty;
            tbHausNr.Text = string.Empty;
            tbPLZ.Text = string.Empty;
            tbOrt.Text = string.Empty;
            tbPF.Text = string.Empty;
            tbPLZPF.Text = string.Empty;
            tbOrtPF.Text = string.Empty;
            //LKZ - Länder
            Functions.SetComboToSelecetedValue(ref cbLand, "D");

            tbKundennummer.Text = "0";
            tbDunsnr.Text = "0";
            //cbASNCom.Checked = false;
            tbASNVerweis.Text = string.Empty;
            tbUserInfoTxt.Text = string.Empty;
            tbLagernummer.Text = "0";

            //Adr Zuweisung
            tbADRAssBelade.Text = string.Empty;
            tbADRAssEntlade.Text = string.Empty;
            tbADRAssPost.Text = string.Empty;
            tbADRAssRG.Text = string.Empty;

            tbKontaktRGAnsprechpartner.Text = string.Empty;
            tbKontaktRGPhone.Text = string.Empty;
            tbKontaktRGMail.Text = string.Empty;
            tbKontaktRGOrganisation.Text = string.Empty;


        }
        ///<summary>ctrADR_List / cbLand_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void cbLand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLand.SelectedValue != null)
            {
                if (cbLand.SelectedIndex > -1)
                {
                    Functions.SetComboToSelecetedItem(ref cbLKZ, cbLand.SelectedValue.ToString());
                }
            }
        }
        ///<summary>ctrADR_List / InitTabADREdit</summary>
        ///<remarks>Initialisiert den Tab</remarks>
        private void SetADRDatenToTabADREdit()
        {
            //decimal decTmp = ADR.ID;
            //DateTime dtDefault = Convert.ToDateTime("01.01.1900 00:00:00");
            DateTime dtDefault = Globals.DefaultDateTimeMinValue;
            DateTime WAvon = default(DateTime);
            DateTime WAbis = default(DateTime);
            //Dummy
            cbDummyADR.Checked = ADR.Dummy;
            cbAdrIsActiv.Checked = ADR.activ;

            if (cbDummyADR.Checked)
            {
                SetFrmForDummy(cbDummyADR.Checked);
                tbSuchname.Text = ADR.ViewID;
                tbPLZ.Text = ADR.PLZ;
                tbOrt.Text = ADR.Ort;
                tbKundennummer.Text = ADR.KD_ID.ToString();
                tbDunsnr.Text = ADR.DUNSNr.ToString();
            }
            else
            {
                tbSuchname.Text = ADR.ViewID;
                tbKundennummer.Text = ADR.KD_ID.ToString();
                tbDunsnr.Text = ADR.DUNSNr.ToString();
                cbFBez.Text = ADR.FBez;
                tbName1.Text = ADR.Name1;
                tbName2.Text = ADR.Name2;
                tbName3.Text = ADR.Name3;
                tbStr.Text = ADR.Str;
                tbHausNr.Text = ADR.HausNr;
                tbPF.Text = ADR.PF;
                tbPLZ.Text = ADR.PLZ;
                tbPLZPF.Text = ADR.PLZPF;
                tbOrt.Text = ADR.Ort;
                tbOrtPF.Text = ADR.OrtPF;

                //Die Combobox cbLKZ wird durch das SelectionChange Event von cbLand gesetzt
                Functions.SetComboToSelecetedItem(ref cbLand, ADR.Land);
                WAvon = ADR.WAvon;
                WAbis = ADR.WAbis;
                tbUserInfoTxt.Text = ADR.UserInfoTxt;
                tbLagernummer.Text = ADR.Lagernummer.ToString();

                //Adresszuweisung Adressen
                //SearchButton
                // 1 = KD /Auftraggeber
                // 2 = Versender
                // 3 = Empfänger
                // 4 = neutrale Versandadresse
                // 5 = neutrale Empfangsadresse
                // 6 = Mandanten
                // 7 = Spedition
                // 10 = Rechnungsadresse
                // 11 = Postadresse
                //// - Postadresse
                ADRSearchButton = 11;
                SetADRAfterADRSearch(this.ADR.AdrID_Post);
                // - RGadresse
                ADRSearchButton = 10;
                SetADRAfterADRSearch(this.ADR.AdrID_RG);
                // - Beladeadresse
                ADRSearchButton = 2;
                SetADRAfterADRSearch(this.ADR.AdrID_Be);
                // - Entladeadresse
                ADRSearchButton = 3;
                SetADRAfterADRSearch(this.ADR.AdrID_Ent);
                ADRSearchButton = 0;

                //Post Einstellungen
                //--Lfs
                if (ADR.PostLfsBy == 0)
                {
                    cbPostPrintLfs.Checked = true;
                    cbPostVerteilerLfs.SelectedIndex = -1;
                    cbPostVerteilerLfs.Enabled = false;
                }
                else
                {
                    cbPostPrintLfs.Checked = false;
                    cbPostVerteilerLfs.Enabled = true;
                    Functions.SetComboToSelecetedValue(ref cbPostVerteilerLfs, ADR.PostLfsBy.ToString());
                }

                ////--RG
                if (ADR.PostRGBy == 0)
                {
                    cbPostPrintRG.Checked = true;
                    cbPostVerteilerRG.SelectedIndex = -1;
                    cbPostVerteilerRG.Enabled = false;
                }
                else
                {
                    cbPostPrintRG.Checked = false;
                    cbPostVerteilerRG.Enabled = true;
                    Functions.SetComboToSelecetedValue(ref cbPostVerteilerRG, ADR.PostRGBy.ToString());
                }

                //--Anlage
                if (ADR.PostAnlageBy == 0)
                {
                    cbPostPrintAnlage.Checked = true;
                    cbPostVerteilerAnlage.SelectedIndex = -1;
                    cbPostVerteilerAnlage.Enabled = false;
                }
                else
                {
                    cbPostPrintAnlage.Checked = false;
                    cbPostVerteilerAnlage.Enabled = true;
                    Functions.SetComboToSelecetedValue(ref cbPostVerteilerAnlage, ADR.PostAnlageBy.ToString());
                }


                //--Listen
                if (ADR.PostListBy == 0)
                {
                    cbPostPrintListen.Checked = true;
                    cbPostVerteilerListen.SelectedIndex = -1;
                    cbPostVerteilerListen.Enabled = false;
                }
                else
                {
                    cbPostPrintListen.Checked = false;
                    cbPostVerteilerListen.Enabled = true;
                    Functions.SetComboToSelecetedValue(ref cbPostVerteilerListen, ADR.PostListBy.ToString());
                }

                //---Anzeige
                if (ADR.PostAnzeigeBy == 0)
                {
                    cbPostPrintAnzeigen.Checked = true;
                    cbPostVerteilerAnzeigen.SelectedIndex = -1;
                    cbPostVerteilerAnzeigen.Enabled = false;
                }
                else
                {
                    cbPostPrintAnzeigen.Checked = false;
                    cbPostVerteilerAnzeigen.Enabled = true;
                    //Functions.SetRADMultiColumnBoxComboToSelecetedValueByValue(ref cbPostVerteilerAnzeigen, ADR.PostAnzeigeBy.ToString());
                    Functions.SetComboToSelecetedValue(ref cbPostVerteilerAnzeigen, ADR.PostAnzeigeBy.ToString());
                }
            }
            //------ WA
            numStdVon.Value = Convert.ToDecimal(WAvon.Hour.ToString());
            numMinVon.Value = Convert.ToDecimal(WAvon.Minute.ToString());
            numStdBis.Value = Convert.ToDecimal(WAbis.Hour.ToString());
            numMinVon.Value = Convert.ToDecimal(WAbis.Minute.ToString());
        }
        ///<summary>ctrADR_List / SetFrmForDummy</summary>
        ///<remarks>Auswahl Dummy Adresse.</remarks> 
        private void SetFrmForDummy(bool bDummy)
        {
            if (bDummy)
            {
                //Textboxen deak.
                tbSuchname.Enabled = false;
                cbFBez.Enabled = false;
                tbName1.Enabled = false;
                tbName2.Enabled = false;
                tbName3.Text = "MusterAdresse";
                tbStr.Enabled = false;
                tbPLZPF.Enabled = false;
                tbOrtPF.Enabled = false;
                tbPF.Enabled = false;
                cbLand.Enabled = false;
                numStdVon.Enabled = false;
                numStdBis.Enabled = false;
                numMinVon.Enabled = false;
                numMinBis.Enabled = false;
                //gbADRStatus.Enabled = false;
                tbKundennummer.Enabled = false;
                tbDunsnr.Enabled = false;

                //Vorgaben setzen
                tbSuchname.Text = "_";
            }
            else
            {
                //Textboxen deak.
                tbSuchname.Enabled = true;
                cbFBez.Enabled = true;
                tbName1.Enabled = true;
                tbName2.Enabled = true;
                //tbName3.Enabled = false;
                tbStr.Enabled = true;
                tbPLZPF.Enabled = true;
                tbOrtPF.Enabled = true;
                tbPF.Enabled = true;
                cbLand.Enabled = true;
                numStdVon.Enabled = true;
                numStdBis.Enabled = true;
                numMinVon.Enabled = true;
                numMinBis.Enabled = true;
                //gbADRStatus.Enabled = true;
                tbKundennummer.Enabled = true;
                tbDunsnr.Enabled = true;
                cbDummyADR.Checked = false;
            }

            mmPanelADRKommission.Enabled = (!bDummy);
            mmPanelUserInfoText.Enabled = (!bDummy);
            mmPanelADRCommunication.Enabled = (!bDummy);
            mmPanelADRKommission.Enabled = (!bDummy);
        }
        ///<summary>ctrADR_List / tbLagernummer_TextChanged</summary>
        ///<remarks>Eingabecheck auf Zahlen</remarks> 
        private void tbLagernummer_TextChanged(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbLagernummer.Text, out decTmp))
            {
                this.tbLagernummer.Text = ADR.Lagernummer.ToString();
            }
        }
        ///<summary>ctrADR_List / tsbSpeichern_Click</summary>
        ///<remarks>Adressdaten prüfen und speichern.</remarks> 
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            bool bSave = false;
            if (cbDummyADR.Checked)
            {
                bSave = true;
            }
            else
            {
                //Check Suchbegriff / Matchcode (darf nicht doppelt vergeben sein)
                //kein CHeck, wenn es um das Update geht
                if (!bUpdateADRDaten)
                {
                    if (!clsADR.CheckMatchcodeIsUsed(tbSuchname.Text.Trim()))
                    {
                        bSave = true;
                        if (tbSuchname.Text.ToString() == "0")
                        {
                            bSave = false;
                            clsMessages.ADR_ViewIDnotZero();
                        }
                    }
                    else
                    {
                        clsMessages.ADR_ViewIDIsUsed();
                        bSave = false;
                    }
                }
                else
                {
                    bSave = true;
                }
            }

            //Speichervorgang
            if (bSave)
            {
                if (cbDummyADR.Checked)
                {
                    //Eingabefeld hier Name3 darf nicht leer sein
                    if (tbName3.Text != "")
                    {
                        AssignVarADR();
                        //-- Eingabemaske auf Null setzen
                        ClearTabADREdit();
                    }
                    SetFrmForDummy(false);
                    SetOldSearchList();
                }
                else
                {
                    if (CheckMissingInput())
                    {
                        AssignVarADR();
                        InitTabADREdit();
                        bUpdateADRDaten = true;
                        SetTabEingabeFelder();
                    }
                }
            }
        }
        ///<summary>ctrADR_List / SetADRAfterADRSearch</summary>
        ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
        ///<param name="ADR_ID">ADR_ID</param>
        public void SetADRAfterADRSearch(decimal myDecADR_ID)
        {
            //SearchButton
            // 1 = KD /Auftraggeber
            // 2 = Versender
            // 3 = Empfänger
            // 4 = neutrale Versandadresse
            // 5 = neutrale Empfangsadresse
            // 6 = Mandanten
            // 7 = Spedition
            // 10 = Rechnungsadresse
            // 11 = Postadresse

            clsADR tmpADR = ADR.Copy();

            switch (ADRSearchButton)
            {
                case 1:
                    //if (tabADR.SelectedTab.Equals("tabPageLieferantenGroup"))
                    //{
                    this.ADR.LiefGroup.AdrIDKomPartner = myDecADR_ID;
                    this.ADR.LiefGroup.ADRKomPartner.ID = myDecADR_ID;
                    this.ADR.LiefGroup.ADRKomPartner.Fill();
                    this.tbLiefGroupComPartner.Text = this.ADR.LiefGroup.ADRKomPartner.ADRStringShort;
                    //}
                    break;

                case 2:
                    //ADR.AdrID_Be = myDecADR_ID;
                    if (myDecADR_ID > 0)
                    {
                        ADR.AdrID_Be = myDecADR_ID;
                    }
                    else
                    {
                        ADR.AdrID_Be = ADR.ID;
                    }
                    tmpADR.ID = ADR.AdrID_Be;
                    tmpADR.FillClassOnly();
                    tbADRAssBelade.Text = "[" + tmpADR.ID.ToString() + "] - " + tmpADR.ADRStringShort;
                    break;

                case 3:
                    //ADR.AdrID_Ent = myDecADR_ID;
                    if (myDecADR_ID > 0)
                    {
                        ADR.AdrID_Ent = myDecADR_ID;
                    }
                    else
                    {
                        ADR.AdrID_Ent = ADR.ID;
                    }

                    tmpADR.ID = ADR.AdrID_Ent;
                    tmpADR.FillClassOnly();
                    tbADRAssEntlade.Text = "[" + tmpADR.ID.ToString() + "] - " + tmpADR.ADRStringShort;
                    break;

                case 10:
                    if (myDecADR_ID > 0)
                    {
                        ADR.AdrID_RG = myDecADR_ID;
                    }
                    else
                    {
                        ADR.AdrID_RG = ADR.ID;
                    }
                    //tbADRRGadresse.Text = strE;#                        
                    tmpADR.ID = ADR.AdrID_RG;
                    tmpADR.FillClassOnly();
                    tbADRAssRG.Text = "[" + tmpADR.ID.ToString() + "] - " + tmpADR.ADRStringShort;
                    break;

                case 11:
                    string strADRShort = string.Empty;
                    if (myDecADR_ID > 0)
                    {
                        ADR.AdrID_Post = myDecADR_ID;
                    }
                    else
                    {
                        ADR.AdrID_Post = ADR.ID;
                    }

                    tmpADR.ID = ADR.AdrID_Post;
                    tmpADR.FillClassOnly();
                    tbADRAssPost.Text = "[" + tmpADR.ID.ToString() + "] - " + tmpADR.ADRStringShort;
                    break;
            }
        }
        ///<summary>ctrADR_List / AssignVar</summary>
        ///<remarks>Zuweisung der Eingabefelder und Update bzw. Eintrag der Daten.</remarks> 
        private void AssignVarADR()
        {
            decimal decAdrID = ADR.ID;
            decimal decPostID = ADR.AdrID_Post;
            decimal decRGID = ADR.AdrID_RG;
            decimal decBeladeID = ADR.AdrID_Be;
            decimal decEntladeID = ADR.AdrID_Ent;
            if (!bUpdateADRDaten)
            {
                ADR = new clsADR();
            }
            ADR._GL_User = this.GL_User;
            ADR._GL_System = this._ctrMenu._frmMain.GL_System;
            ADR.ViewID = tbSuchname.Text.Trim();

            ADR.FBez = cbFBez.Text.ToString();
            if (ADR.FBez.Equals(string.Empty))
            {
                ADR.FBez = "Firma";
            }
            ADR.Name1 = tbName1.Text.Trim();
            ADR.Name2 = tbName2.Text.Trim();
            ADR.Name3 = tbName3.Text.Trim();
            ADR.Str = tbStr.Text.Trim();
            ADR.HausNr = tbHausNr.Text.Trim();
            ADR.PF = tbPF.Text.Trim();
            ADR.PLZ = tbPLZ.Text.Trim();
            ADR.Ort = tbOrt.Text.Trim();
            ADR.PLZPF = tbPLZPF.Text.Trim();
            ADR.OrtPF = tbOrtPF.Text.Trim();
            ADR.Land = cbLand.Text.ToString();
            if (cbLand.SelectedIndex > -1)
            {
                ADR.LKZ = cbLand.SelectedValue.ToString();
            }
            else
            {
                ADR.LKZ = string.Empty;
            }
            ADR.activ = cbAdrIsActiv.Checked;
            ADR.UserInfoTxt = tbUserInfoTxt.Text.Trim();

            string von = numStdVon.Value.ToString() + ":" + numMinVon.Value.ToString();
            string bis = numStdBis.Value.ToString() + ":" + numMinBis.Value.ToString();
            ADR.WAvon = Convert.ToDateTime("01.01.1900 " + von + ":00");
            ADR.WAbis = Convert.ToDateTime("01.01.1900 " + bis + ":00");

            ADR.Dummy = cbDummyADR.Checked;
            // ADR.ASNCommunication = cbASNCom.Checked;
            //ADR.Verweis = tbASNVerweis.Text.Trim();
            decimal decTmp = 0;
            Decimal.TryParse(tbLagernummer.Text, out decTmp);
            //Check LAgernummer
            if (decTmp < ADR.LagernummerMax + 1)
            {
                decTmp = ADR.LagernummerMax + 1;
            }
            ADR.Lagernummer = decTmp;
            int iTmp = 0;
            int.TryParse(tbDunsnr.Text, out iTmp);
            ADR.DUNSNr = iTmp;
            //Kundennummer nicht bei Dummy
            if (!cbDummyADR.Checked)
            {
                if (!bUpdateADRDaten)
                {
                    SetKundennummerToFrm();
                }
                ADR.KD_ID = Convert.ToInt32(tbKundennummer.Text);
            }
            else
            {
                ADR.KD_ID = 0;
            }
            if (!bUpdateADRDaten)
            {
                //Neueintrag
                ADR.Add();
                if (!ADR.Dummy)
                {
                    ADR.SetKundenDaten();
                }
            }
            else
            {
                //Update
                ADR.ID = decAdrID;
                ADR.AdrID_Post = decPostID;
                ADR.AdrID_RG = decRGID;
                ADR.AdrID_Be = decBeladeID;
                ADR.AdrID_Ent = decEntladeID;
                // IS  variablen speichern ...

                ADR.Update();
            }
            this.initList();
            //ClearTabADREdit(); // CF auf wunsch von HN
            //this.scADR.Panel2Collapsed = true;  // Auf Wunsch von HN 27.06
            //ResetCtrADRListWidth(); // 27.06
        }
        ///<summary>ctrADR_List / CheckMissingInput</summary>
        ///<remarks>prüft die Eingabefelder.</remarks> 
        private bool CheckMissingInput()
        {
            bool retVal = true;

            string strHelp;

            strHelp = "Folgende Pflichtfelder wurden nicht ausgefüllt:\n";
            //-----  Info welche Felder fehlen  ------------------------
            if (tbSuchname.Text == "")
            {
                retVal = false;
                strHelp = strHelp + "Matchcode \n";
            }
            if (tbName1.Text == "")
            {
                retVal = false;
                strHelp = strHelp + "Name1 \n";
            }
            if (tbStr.Text == "")
            {
                retVal = false;
                strHelp = strHelp + "Strasse \n";
            }
            if (tbPLZ.Text == "")
            {
                retVal = false;
                strHelp = strHelp + "PLZ \n";
            }
            if (tbOrt.Text == "")
            {
                retVal = false;
                strHelp = strHelp + "Ort \n";
            }
            if (tbPF.Text == "")
            {
                strHelp = strHelp + "Postfach \n";
            }
            if (tbPLZPF.Text == "")
            {
                strHelp = strHelp + "PLZ Postfach \n";
            }
            if (tbOrtPF.Text == "")
            {
                strHelp = strHelp + "Ort Postfach \n";
            }

            if (!retVal)
            {
                MessageBox.Show(strHelp);
            }

            return retVal;
        }
        ///<summary>ctrADR_List / SetKundennummerToFrm</summary>
        ///<remarks></remarks> 
        private void SetKundennummerToFrm()
        {
            decimal autoKundennummer = ADR.Kunde.NewKD_ID;
            tbKundennummer.Text = autoKundennummer.ToString();
        }
        ///<summary>ctrADR_List / tsbtnClose_Click</summary>
        ///<remarks>Eingabefelder ausblenden</remarks> 
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            ShowAndHideADREdit();
        }
        ///<summary>ctrADR_List / btnADRPostadresse_Click</summary>
        ///<remarks></remarks>
        private void btnADRPostadresse_Click(object sender, EventArgs e)
        {
            ADRSearchButton = 11;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrADR_List / tsbtnClose_Click</summary>
        ///<remarks>Eingabefelder ausblenden</remarks>
        private void btnADRRechnungsadresse_Click(object sender, EventArgs e)
        {
            ADRSearchButton = 10;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrADR_List / tsbtnClose_Click</summary>
        ///<remarks>Eingabefelder ausblenden</remarks>
        private void btnADRBeladeadresse_Click(object sender, EventArgs e)
        {
            ADRSearchButton = 2;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrADR_List / tsbtnClose_Click</summary>
        ///<remarks>Eingabefelder ausblenden</remarks>
        private void btnADREntladeadresse_Click(object sender, EventArgs e)
        {
            ADRSearchButton = 3;
            this._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrADR_List / mcbADRPost_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void mcbADRPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            //decimal decTmp = 0;
            //string adrTmp = string.Empty;
            //if (mcbADRPost.SelectedIndex > 0)
            //{
            //    if (mcbADRPost.EditorControl.Rows.Count > 0)
            //    {
            //        Decimal.TryParse(mcbADRPost.EditorControl.Rows[mcbADRPost.SelectedIndex].Cells["ID"].Value.ToString(), out decTmp);
            //        adrTmp = mcbADRPost.EditorControl.Rows[mcbADRPost.SelectedIndex].Cells["Adresse"].Value.ToString();
            //    }
            //    ADR.AdrID_Post = decTmp;
            //    tbADRAssPost.Text = adrTmp;
            //}
        }
        ///<summary>ctrADR_List / mcbADRRG_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void mcbADRRG_SelectedIndexChanged(object sender, EventArgs e)
        {
            //decimal decTmp = 0;
            //string adrTmp = string.Empty;
            //if (mcbADRRG.SelectedIndex > 0)
            //{
            //    if (mcbADRRG.EditorControl.Rows.Count > 0)
            //    {
            //        Decimal.TryParse(mcbADRRG.EditorControl.Rows[mcbADRRG.SelectedIndex].Cells["ID"].Value.ToString(), out decTmp);
            //        adrTmp = mcbADRRG.EditorControl.Rows[mcbADRRG.SelectedIndex].Cells["Adresse"].Value.ToString();
            //    }
            //    ADR.AdrID_RG = decTmp;
            //    tbADRAssRG.Text = adrTmp;
            //}
        }
        ///<summary>ctrADR_List / mcbADRBelade_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void mcbADRBelade_SelectedIndexChanged(object sender, EventArgs e)
        {
            //decimal decTmp = 0;
            //string adrTmp = string.Empty;
            //if (mcbADRBelade.SelectedIndex > 0)
            //{
            //    if (mcbADRBelade.EditorControl.Rows.Count > 0)
            //    {
            //        Decimal.TryParse(mcbADRBelade.EditorControl.Rows[mcbADRBelade.SelectedIndex].Cells["ID"].Value.ToString(), out decTmp);
            //        adrTmp = mcbADRBelade.EditorControl.Rows[mcbADRBelade.SelectedIndex].Cells["Adresse"].Value.ToString();
            //    }
            //    ADR.AdrID_Be = decTmp;
            //    tbADRAssBelade.Text = adrTmp;
            //}
        }
        ///<summary>ctrADR_List / mcbADREntlade_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void mcbADREntlade_SelectedIndexChanged(object sender, EventArgs e)
        {
            //decimal decTmp = 0;
            //string adrTmp = string.Empty;
            //if (mcbADREntlade.SelectedIndex > 0)
            //{
            //    if (mcbADREntlade.EditorControl.Rows.Count > 0)
            //    {
            //        Decimal.TryParse(mcbADREntlade.EditorControl.Rows[mcbADREntlade.SelectedIndex].Cells["ID"].Value.ToString(), out decTmp);
            //        adrTmp = mcbADREntlade.EditorControl.Rows[mcbADREntlade.SelectedIndex].Cells["Adresse"].Value.ToString();
            //    }
            //    ADR.AdrID_Ent = decTmp;
            //    tbADRAssEntlade.Text = adrTmp;
            //}
        }
        /*********************************************************************************************
         *                                  tabContactEdit
         * ******************************************************************************************/
        ///<summary>ctrADR_List / v</summary>
        ///<remarks>Neuen Kontakt anlegen</remarks> 
        private void tsbtnAddContact_Click(object sender, EventArgs e)
        {
            bUpdateContact = false;
            ClearTABContactEdit();
            SetTabContaktEingabefelderEnabled(true);
        }
        ///<summary>ctrADR_List / ClearTABContactEdit</summary>
        ///<remarks></remarks> 
        private void SetTabContaktEingabefelderEnabled(bool bEnabled)
        {
            tbContactSearchContact.Enabled = bEnabled;
            tbContactAbt.Enabled = bEnabled;
            tbContactNachname.Enabled = bEnabled;
            tbContactVorname.Enabled = bEnabled;
            tbContactTel.Enabled = bEnabled;
            tbContactFax.Enabled = bEnabled;
            tbContactMobil.Enabled = bEnabled;
            tbMail.Enabled = bEnabled;
            tbInfo.Enabled = bEnabled;
            dtpBirthday.Enabled = bEnabled;

        }
        ///<summary>ctrADR_List / tsbtnMailingListAssignmentRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnMailingListAssignmentRefresh_Click(object sender, EventArgs e)
        {
            InitTabContactEdit();
        }
        ///<summary>ctrADR_List / ClearTABContactEdit</summary>
        ///<remarks></remarks> 
        private void ClearTABContactEdit()
        {
            tbContactSearchContact.Text = string.Empty;
            cbContactAnrede.SelectedIndex = 0;
            tbContactNachname.Text = string.Empty;
            tbContactVorname.Text = string.Empty;
            tbContactAbt.Text = string.Empty;
            tbContactTel.Text = string.Empty;
            tbContactFax.Text = string.Empty;
            tbContactMobil.Text = string.Empty;
            tbMail.Text = string.Empty;
            tbInfo.Text = string.Empty;
            dtpBirthday.Value = dtpBirthday.MinDate;

            tstbContactSearch.Text = string.Empty;
        }
        ///<summary>ctrADR_List / InitTabContactEdit</summary>
        ///<remarks></remarks> 
        private void InitTabContactEdit()
        {
            InitDGVKontakte();
            InitDGVMailingList();
            InitDGVMailingListAdministration();
        }
        ///<summary>ctrADR_List / InitDGVKontakte</summary>
        ///<remarks></remarks> 
        private void InitDGVKontakte()
        {
            //this.grdKontakte.DataSource = ADR.Kontakt.dtKontakte; // CF
            this.grdKontakte.DataSource = ADR.Kontakt.dtKontakte;
            if (this.grdKontakte.Rows.Count > 0)
            {
                //Spalten ausblenden
                if (this.grdKontakte.Columns["ID"] != null)
                {
                    this.grdKontakte.Columns["ID"].IsVisible = false;
                }
                if (this.grdKontakte.Columns["ADR_ID"] != null)
                {
                    this.grdKontakte.Columns["ADR_ID"].IsVisible = false;
                }
                if (this.grdKontakte.Columns["Anrede"] != null)
                {
                    this.grdKontakte.Columns["Anrede"].IsVisible = false;
                }
                if (this.grdKontakte.Columns["Info"] != null)
                {
                    this.grdKontakte.Columns["Info"].IsVisible = false;
                }
                //Reihefolge der spalten festlegen
                if (this.grdKontakte.Columns["ViewID"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["ViewID"].Index, 0);
                    this.grdKontakte.Columns["ViewID"].HeaderText = "Matchcode";
                }
                if (this.grdKontakte.Columns["Nachname"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["Nachname"].Index, 1);
                }
                if (this.grdKontakte.Columns["Vorname"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["Vorname"].Index, 2);
                }
                if (this.grdKontakte.Columns["Abteilung"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["Abteilung"].Index, 3);
                }
                if (this.grdKontakte.Columns["Mail"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["Mail"].Index, 4);
                }
                if (this.grdKontakte.Columns["Telefon"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["Telefon"].Index, 5);
                }
                if (this.grdKontakte.Columns["Fax"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["Fax"].Index, 6);
                }
                if (this.grdKontakte.Columns["Mobil"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["Mobil"].Index, 7);
                }
                if (this.grdKontakte.Columns["Birthday"] != null)
                {
                    this.grdKontakte.Columns.Move(this.grdKontakte.Columns["Birthday"].Index, 8);
                    this.grdKontakte.Columns["Birthday"].FormatString = "{0:d}";
                    this.grdKontakte.Columns["Birthday"].HeaderText = "Geburtstag";
                }

                //Selected Row auf ADR.Kontakt.ID setzen
                for (Int32 i = 0; i <= this.grdKontakte.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    //strTmp = string.Empty;
                    string strTmp = this.grdKontakte.Rows[i].Cells["ID"].Value.ToString();
                    Decimal.TryParse(strTmp, out decTmp);
                    if (ADR.Kontakt.ID == decTmp)
                    {
                        this.grdKontakte.Rows[i].IsSelected = true;
                        this.grdKontakte.Rows[i].IsCurrent = true;
                        break;
                    }
                }
                this.grdKontakte.BestFitColumns();
            }
        }
        ///<summary>ctrADR_List / grdKontakte_ToolTipTextNeeded_1</summary>
        ///<remarks></remarks> 
        private void grdKontakte_ToolTipTextNeeded_1(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrADR_List / tsbtnCloseContact_Click</summary>
        ///<remarks>TabKontakteEdit schließen</remarks> 
        private void tsbtnCloseContact_Click(object sender, EventArgs e)
        {
            this.scADR.Panel2Collapsed = true;
            ResetCtrADRListWidth();
            ClearTABContactEdit();
        }
        ///<summary>ctrADR_List / tsbtnContactMainClose_Click</summary>
        ///<remarks>schließen</remarks> 
        private void tsbtnContactMainClose_Click(object sender, EventArgs e)
        {
            this.scADR.Panel2Collapsed = true;
            ResetCtrADRListWidth();
            ClearTABContactEdit();
        }
        ///<summary>ctrADR_List / grdKontakte_MouseClick</summary>
        ///<remarks></remarks
        private void grdKontakte_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.grdKontakte.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.grdKontakte.Rows[this.grdKontakte.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    ADR.Kontakt = new clsKontakte();
                    ADR.Kontakt._GL_User = this.GL_User;
                    ADR.Kontakt.ID = decTmp;
                    ADR.Kontakt.Fill();

                    SetContactDataToTabContactEdit();
                    SetTabContaktEingabefelderEnabled(false);
                }
            }
        }
        ///<summary>ctrADR_List / grdKontakte_MouseDoubleClick</summary>
        ///<remarks></remarks
        private void grdKontakte_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.grdKontakte.Rows.Count > 0)
            {
                bUpdateContact = true;
                SetContactDataToTabContactEdit();
                SetTabContaktEingabefelderEnabled(true);
                tsbtnDeleteContact.Enabled = true;
            }
        }
        ///<summary>ctrADR_List / AssignVar</summary>
        ///<remarks>Zuweisung der Eingabe</remarks> 
        private void AssignVarContact()
        {
            decimal decTmp = ADR.Kontakt.ID;
            ADR.Kontakt = new clsKontakte();
            ADR.Kontakt.BenutzerID = GL_User.User_ID;

            //---------- Zusweisung der Werte
            ADR.Kontakt.ViewID = tbContactSearchContact.Text.Trim();
            ADR.Kontakt.ADR_ID = ADR.ID;

            ADR.Kontakt.Nachname = tbContactNachname.Text.Trim();
            ADR.Kontakt.Vorname = tbContactVorname.Text.Trim();
            ADR.Kontakt.Abteilung = tbContactAbt.Text.Trim();
            ADR.Kontakt.Telefon = tbContactTel.Text.Trim();
            ADR.Kontakt.Fax = tbContactFax.Text.Trim();
            ADR.Kontakt.Mobil = tbContactMobil.Text.Trim();
            ADR.Kontakt.Mail = tbMail.Text.Trim();
            ADR.Kontakt.Info = tbInfo.Text.Trim();
            ADR.Kontakt.Birthday = dtpBirthday.Value;
            ADR.Kontakt.Anrede = cbContactAnrede.SelectedItem.ToString();

            if (!bUpdateContact)
            {
                // --- Eintrag in DB ----
                ADR.Kontakt.Add();
            }
            else
            {
                //---- Update Datensatz in DB ---
                ADR.Kontakt.ID = decTmp;
                ADR.Kontakt.Update();
            }
            //ADR.Kontakt.Fill(); // CF
            ADR.Fill();
        }
        ///<summary>ctrADR_List / tsbtnMailingListClose_Click</summary>
        ///<remarks>Tab schliessen</remarks>
        private void tsbtnMailingListClose_Click(object sender, EventArgs e)
        {
            this.scADR.Panel2Collapsed = true;
            ResetCtrADRListWidth();
            ClearTABContactEdit();
        }
        ///<summary>ctrADR_List / tsbtnMailingListAdd_Click</summary>
        ///<remarks>Eine neue Verteilerliste wird erstellt. Folgende Procedure müssen dabei durchgeführt werden.
        ///         - Eingabefelder leeren
        ///         - Save Button aktivieren</remarks>
        private void tsbtnMailingListAdd_Click(object sender, EventArgs e)
        {
            //MailinglistID = 0 setzten
            this.ADR.Kontakt.MailingList.ID = 0;

            //Eingabefelder leeren
            ClearMailingListInputField();
            SetMailingListInputFieldsEnable(true);
            //Button aktivieren
            SetMailingListMenuButtonEnabled(true);
        }
        ///<summary>ctrADR_List / tsbtnMailingListExcel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnMailingListExcel_Click(object sender, EventArgs e)
        {
            string strFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_Kontaktliste_" + ADR.ViewID + ".xls";
            saveFileDialog.FileName = strFileName;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            strFileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.grdKontakte, strFileName, ref openExportFile, this.GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(strFileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "ADR-Kontakt - LIST - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrADR_List / ClearMailingListInputField</summary>
        ///<remarks>Eingabefelder leeren </remarks>
        private void ClearMailingListInputField()
        {
            tbMailingListBezeichnung.Text = string.Empty;
            tbMailingListBeschreibung.Text = string.Empty;
        }
        ///<summary>ctrADR_List / SetMailingListMenuButtonEnabled</summary>
        ///<remarks>Hier werden entsprechende die Menübutton gesetzt. ForInput = für eine neue Eingabe </remarks>
        private void SetMailingListMenuButtonEnabled(bool bForInput)
        {
            if (!bForInput)
            {
                tsbtnMailingListAdd.Enabled = false;
                tsbtnMailingListSave.Enabled = true;
                tsbtnMailingListExcel.Enabled = false;
                tsbtnMailingListDelete.Enabled = true;
            }
            else
            {
                tsbtnMailingListAdd.Enabled = true;
                tsbtnMailingListSave.Enabled = true;
                tsbtnMailingListExcel.Enabled = true;
                tsbtnMailingListDelete.Enabled = false;
                //tsbtnMailingListClose.Enabled = bEnabled; //Button zum Schließen der Eingabe Tabs soll immer aktiv sein
            }
        }
        ///<summary>ctrADR_List / SetMailingListInputFieldsEnable</summary>
        ///<remarks>Aktiviert oder deaktiviert die Eingabefelder</remarks>
        private void SetMailingListInputFieldsEnable(bool bEnabled)
        {
            tbMailingListBeschreibung.Enabled = bEnabled;
            tbMailingListBezeichnung.Enabled = bEnabled;
        }
        ///<summary>ctrADR_List / dgvMailingList_MouseDoubleClick</summary>
        ///<remarks>per Doppelklick wird der entsprechende Datensatz aus dem DGV ausgewählt</remarks>
        private void dgvMailingList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgvMailingList.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvMailingList.Rows[this.dgvMailingList.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    this.ADR.Kontakt.MailingList.ID = decTmp;
                    this.ADR.Kontakt.MailingList.FillByID();

                    SetMailingListDatenToCtr();
                    SetMailingListInputFieldsEnable(true);
                    SetMailingListMenuButtonEnabled(false);
                    bUpdateMailingList = true;
                }
            }
        }
        ///<summary>ctrADR_List / SetMailingListDatenToCtr</summary>
        ///<remarks>Füllt die entsprechendne Datenfelder mit den Werten aus der Datenbank</remarks>
        private void SetMailingListDatenToCtr()
        {
            this.tbMailingListBeschreibung.Text = this.ADR.Kontakt.MailingList.Beschreibung;
            this.tbMailingListBezeichnung.Text = this.ADR.Kontakt.MailingList.Bezeichnung;
        }
        ///<summary>ctrADR_List / tsbtnMailingListSave_Click</summary>
        ///<remarks>Speichern der eingegebenen Daten</remarks>
        private void tsbtnMailingListSave_Click(object sender, EventArgs e)
        {
            if (tbMailingListBezeichnung.Text != string.Empty)
            {
                clsMailingList tmpMailingList = new clsMailingList();
                tmpMailingList._GL_User = this.GL_User;
                tmpMailingList.Bezeichnung = this.tbMailingListBezeichnung.Text;
                tmpMailingList.Beschreibung = this.tbMailingListBeschreibung.Text;
                tmpMailingList.ArbeitsbereichID = this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID;
                tmpMailingList.AdrID = this.ADR.ID;

                if (bUpdateMailingList)
                {
                    //Update Mailinglist
                    tmpMailingList.ID = this.ADR.Kontakt.MailingList.ID;
                    this.ADR.Kontakt.MailingList = new clsMailingList();
                    this.ADR.Kontakt.MailingList = tmpMailingList;
                    this.ADR.Kontakt.MailingList.Update();
                }
                else
                {
                    //Check Eingabe
                    //Die Eingabe darf nicht leer sein und der Verteilername für diesen ADR Datensatz noch nicht vorhanden sein
                    if (!clsMailingList.ExistMailingListName(this.GL_User, tbMailingListBezeichnung.Text, this.ADR.ID, this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID))
                    {
                        this.ADR.Kontakt.MailingList = new clsMailingList();
                        this.ADR.Kontakt.MailingList = tmpMailingList;
                        this.ADR.Kontakt.MailingList.Add();
                    }
                    else
                    {
                        clsMessages.MailingList_MailingListNameExist();
                        tbMailingListBezeichnung.Focus();
                    }
                }
                //Updateflag wieder auf False setzen
                bUpdateMailingList = false;
                ClearMailingListInputField();
                SetMailingListInputFieldsEnable(true);
                InitTabContactEdit();
            }
        }
        ///<summary>ctrADR_List / tsbtnMailingListDelete_Click</summary>
        ///<remarks>Der ausgewählte Daensatz wird gelöscht.</remarks>
        private void tsbtnMailingListDelete_Click(object sender, EventArgs e)
        {
            //Abfrage ob der Datensatz wirklich gelöscht werden soll
            if (clsMessages.MailingList_Delete())
            {
                //Datensatz löschen
                //dadurch werden automatisch auch die Zuweisungen des Verteilers gelöscht
                this.ADR.Kontakt.MailingList.Delete();
                //Datagridview neu laden
                InitDGVMailingList();
                ClearMailingListInputField();
                SetMailingListMenuButtonEnabled(true);
                SetMailingListInputFieldsEnable(false);
            }
        }
        ///<summary>ctrADR_List / InitDGVMailingList</summary>
        ///<remarks></remarks>
        private void InitDGVMailingList()
        {
            if (this.ADR.Kontakt.MailingList != null)
            {
                this.ADR.Kontakt.MailingList.FillDictMailingList(this.ADR.ID, this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID);
                this.dgvMailingList.DataSource = this.ADR.Kontakt.MailingList.dtMailingList;
            }
            //Spalten ausblenden
            if (this.dgvMailingList.Rows.Count > 0)
            {

                for (Int32 i = 0; i <= this.dgvMailingList.Columns.Count - 1; i++)
                {
                    string ColName = this.dgvMailingList.Columns[i].Name;
                    switch (ColName)
                    {
                        case "Bezeichnung":
                            Int32 iWidthCol = this.dgvMailingList.Width / 3;
                            this.dgvMailingList.Columns[i].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            if (this.dgvMailingList.Columns[i].Width < iWidthCol)
                            {
                                this.dgvMailingList.Columns[i].Width = iWidthCol;
                            }
                            break;
                        case "Beschreibung":
                            Int32 iWidthCol1 = this.dgvMailingList.Width * 2 / 3;
                            this.dgvMailingList.Columns[i].AutoSizeMode = BestFitColumnMode.DisplayedCells;
                            if (this.dgvMailingList.Columns[i].Width < iWidthCol1)
                            {
                                this.dgvMailingList.Columns[i].Width = iWidthCol1;
                            }
                            break;
                        default:
                            this.dgvMailingList.Columns[i].IsVisible = false;
                            break;

                    }
                }
            }
            InitDGVContactMails();
            InitDGVMailingListMemeber();
        }
        ///<summary>ctrADR_List / dgvMailingList_ToolTipTextNeeded</summary>
        ///<remarks></remarks> 
        private void dgvMailingList_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrADR_List / CheckInputContactData</summary>
        ///<remarks>Überprüfung der Eingabedaten</remarks> 
        private bool CheckInputContactData()
        {
            string strHelp;
            bool eingabeOK = true;
            strHelp = "Folgende Eingaben sind nicht korrekt:\n";
            char[] ad = { '@' };
            char[] tele = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '/' };
            char[] num = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] bst = { 'a', 'A', 'b', 'c', 'C', 'd', 'D', 'e', 'E', 'F', 'f', 'g', 'G', 'h', 'H', 'i', 'I', 'j', 'J', 'k', 'K', 'l', 'L', 'm', 'M', 'n', 'N', 'o', 'O', 'p', 'P', 'q', 'Q', 'r', 'R', 's', 'S', 't', 'T', 'u', 'U', 'v', 'V', 'w', 'W', 'x', 'X', 'y', 'Y', 'z', 'Z' };
            char[] Uml = { 'ä', 'Ä', 'ö', 'Ö', 'ü', 'Ü' };

            //Abteilung
            if (tbContactAbt.Text == "")
            {
                strHelp = strHelp + "Abteiung wurde nicht ausgefüllt \n";
                eingabeOK = false;
            }
            //Ansprechpartner
            if (tbContactNachname.Text == "")
            {
                strHelp = strHelp + "Ansprechpartner wurde nicht ausgefüllt \n";
                eingabeOK = false;
            }
            //Telefon
            if (tbContactTel.Text != "")
            {
                if (tbContactTel.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Telefonnummer enthält Buchstaben \n";
                    eingabeOK = false;
                }
            }
            // Fax
            if (tbContactFax.Text != "")
            {
                if (tbContactFax.Text.ToString().IndexOfAny(bst) != -1)
                {
                    strHelp = strHelp + "Fax enthält Buchstaben \n";
                    eingabeOK = false;
                }
            }
            //--- Mail kein Pflichtfeld, dennoch prüfen auf korrekte Eingabe
            if (tbMail.Text != "")
            {
                if (tbMail.Text.ToString().IndexOfAny(ad) == -1)
                {
                    strHelp = strHelp + "E-Mail beinhaltet kein '@' \n";
                    eingabeOK = false;
                }

                if (tbMail.Text.ToString().IndexOfAny(Uml) != -1)
                {
                    strHelp = strHelp + "E-Mail beinhaltet Umlaute \n";
                    eingabeOK = false;
                }
            }

            if (!eingabeOK)
            {
                MessageBox.Show(strHelp);
            }
            return eingabeOK;
        }
        ///<summary>ctrADR_List / tsbtnSave_Click</summary>
        ///<remarks>Eingabedaten speichern</remarks> 
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            if (CheckInputContactData())
            {
                AssignVarContact();
                //-- Eingabemaske auf Null setzen --
                ClearTABContactEdit();
                SetTabContaktEingabefelderEnabled(false);
                InitTabContactEdit();
            }
        }
        ///<summary>ctrADR_List / SetKontaktDatenToTabContactEdit</summary>
        ///<remarks>Füllt die Eingabefelder der Kontaktdaten</remarks> 
        private void SetContactDataToTabContactEdit()
        {
            tbContactSearchContact.Text = ADR.Kontakt.ViewID;
            tbContactAbt.Text = ADR.Kontakt.Abteilung;
            tbContactNachname.Text = ADR.Kontakt.Nachname;
            tbContactVorname.Text = ADR.Kontakt.Vorname;
            tbMail.Text = ADR.Kontakt.Mail;
            tbContactTel.Text = ADR.Kontakt.Telefon;
            tbContactFax.Text = ADR.Kontakt.Fax;
            tbInfo.Text = ADR.Kontakt.Info;
            tbContactMobil.Text = ADR.Kontakt.Mobil;
            if (ADR.Kontakt.Birthday > Globals.DefaultDateTimeMinValue)
                dtpBirthday.Value = ADR.Kontakt.Birthday;
            else
                dtpBirthday.Value = Globals.DefaultDateTimeMinValue;
            Functions.SetComboToSelecetedItem(ref cbContactAnrede, ADR.Kontakt.Anrede);

        }
        ///<summary>ctrADR_List / grdKontakte_ToolTipTextNeeded</summary>
        ///<remarks></remarks> 
        private void grdKontakte_ToolTipTextNeeded(object sender, Telerik.WinControls.ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement cell = sender as GridDataCellElement;
            if (cell != null)
            {
                e.ToolTipText = cell.Value.ToString();
            }
        }
        ///<summary>ctrADR_List / tsbtnExcelExportContakt_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnExcelExportContakt_Click(object sender, EventArgs e)
        {
            string strFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_Kontaktliste_" + ADR.ViewID + ".xls";
            saveFileDialog.FileName = strFileName;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            strFileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.grdKontakte, strFileName, ref openExportFile, this.GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(strFileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "ADR-Kontakt - LIST - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrADR_List / tstbContactSearch_TextChanged</summary>
        ///<remarks></remarks> 
        private void tstbContactSearch_TextChanged(object sender, EventArgs e)
        {
            SearchGrdKontakte(tstbContactSearch.Text);
        }
        ///<summary>ctrADR_List / SearchGrdADRList</summary>
        ///<remarks></remarks>
        private void SearchGrdKontakte(string Search)
        {
            //Spalte hinzufügen
            DataColumn col1 = ADR.Kontakt.dtKontakte.Columns.Add("Find", typeof(Boolean));
            bool isFound = false;
            if (Search.ToString() == string.Empty)
            {
                this.InitTabContactEdit();
            }
            else
            {
                if (Convert.ToBoolean(Search.Length))
                {
                    // If the item is not found and you haven't looked at every cell, keep searching
                    //while ((!isFound) & (idx < maxSearches))
                    for (Int32 _Row = 0; _Row <= ADR.Kontakt.dtKontakte.Rows.Count - 1; _Row++)
                    {
                        for (Int32 _Column = 1; _Column <= ADR.Kontakt.dtKontakte.Columns.Count - 1; _Column++)
                        {
                            // Do all comparing in UpperCase so it is case insensitive
                            //if (grdADRList[_Column, _Row].Value.ToString().ToUpper().Contains(Search))
                            if (ADR.Kontakt.dtKontakte.Rows[_Row][_Column].ToString().ToUpper().Contains(Search))
                            {
                                // If found position on the item
                                //grdADRList.FirstDisplayedScrollingRowIndex = _Row;
                                //grdADRList[_Column, _Row].Selected = true;
                                string test = ADR.Kontakt.dtKontakte.Rows[_Row][_Column].ToString().ToUpper().Contains(Search).ToString();
                                isFound = true;
                                _Column = ADR.Kontakt.dtKontakte.Columns.Count;
                            }

                        }
                        ADR.Kontakt.dtKontakte.Rows[_Row]["Find"] = isFound;
                        isFound = false;
                    }

                    string Ausgabe = string.Empty;
                    DataRow[] rows = adrList.Select("Find =true", "Find");
                    tempTable.Clear();
                    tempTable = ADR.Kontakt.dtKontakte.Clone();
                    foreach (DataRow row in rows)
                    {
                        Ausgabe = Ausgabe + row["Find"].ToString() + "\n";
                        tempTable.ImportRow(row);
                    }
                    tempTable.Columns.Remove("Find");
                    ADR.Kontakt.dtKontakte.Columns.Remove("Find");
                    ADR.Kontakt.dtKontakte.Clear();
                    ADR.Kontakt.dtKontakte = tempTable;
                    grdADRList.DataSource = ADR.Kontakt.dtKontakte;
                }
            }
        }
        ///<summary>ctrADR_List / tsbDeleteContact_Click</summary>
        ///<remarks></remarks>
        private void tsbDeleteContact_Click(object sender, EventArgs e)
        {
            if (clsMessages.Kontakte_KontaktDelete())
            {
                ADR.Kontakt.DeleteKontaktEintrag();
                ClearTABContactEdit();
                InitTabContactEdit();
                SetTabContaktEingabefelderEnabled(false);
                tsbtnDeleteContact.Enabled = false;
            }
        }
        /**************************************************************************************************
         *                                      tabKundeEdit
         * ***********************************************************************************************/
        ///<summary>ctrADR_List / tsbDeleteContact_Click</summary>
        ///<remarks></remarks>
        private void InitTabKundeEdit()
        {
            ClearTabKundenEdit();
            SetKundenDataToTabKundeEdit();
        }
        ///<summary>ctrADR_List / SetKundenDataToTabKundeEdit</summary>
        ///<remarks></remarks>
        private void SetKundenDataToTabKundeEdit()
        {
            tbKD_ID.Text = ADR.Kunde.KD_ID.ToString();
            tbKD_IDman.Text = ADR.Kunde.KD_IDman.ToString();
            tbDebitor.Text = ADR.Kunde.Debitor.ToString();
            Functions.SetComboToSelecetedValue(ref cbSalesTaxKeyDebitor, ADR.Kunde.SalesTaxKeyDebitor.ToString());
            tbKreditor.Text = ADR.Kunde.Kreditor.ToString();
            Functions.SetComboToSelecetedValue(ref cbSalesTaxKeyKreditor, ADR.Kunde.SalesTaxKeyKreditor.ToString());
            tbStNr.Text = ADR.Kunde.SteuerNr;
            tbUStID.Text = ADR.Kunde.USt_ID;
            tbMwStSatz.Text = ADR.Kunde.MwStSatz.ToString();
            cbMwSt.Checked = ADR.Kunde.MwSt;
            decimal decTmp = 0;
            Decimal.TryParse(ADR.Kunde.Zahlungziel.ToString(), out decTmp);
            nudZZ.Value = decTmp;

            tbB1.Text = ADR.Kunde.Bank1;
            tbB1_Kto.Text = ADR.Kunde.Kto1.ToString();
            tbB1_BLZ.Text = ADR.Kunde.BLZ1.ToString();
            tbSwift1.Text = ADR.Kunde.Swift1;
            tbIBAN1.Text = ADR.Kunde.IBAN1;

            tbKontaktRGAnsprechpartner.Text = ADR.Kunde.Contact;
            tbKontaktRGPhone.Text = ADR.Kunde.Phone;
            tbKontaktRGMail.Text = ADR.Kunde.Mailaddress;
            tbKontaktRGOrganisation.Text = ADR.Kunde.Organisation;



        }
        ///<summary>ctrADR_List / ClearTabKundenEdit</summary>
        ///<remarks></remarks>
        private void ClearTabKundenEdit()
        {
            tbKD_ID.Text = string.Empty;
            tbDebitor.Text = string.Empty;
            tbKreditor.Text = string.Empty;
            tbStNr.Text = string.Empty;
            tbUStID.Text = string.Empty;
            tbMwStSatz.Text = string.Empty;
            cbMwSt.Checked = true;

            tbB1.Text = string.Empty;
            tbB1_Kto.Text = string.Empty;
            tbB1_BLZ.Text = string.Empty;
            tbSwift1.Text = string.Empty;
            tbIBAN1.Text = string.Empty;
            nudZZ.Value = 0;

            tbKontaktRGAnsprechpartner.Text = string.Empty;
            tbKontaktRGPhone.Text = string.Empty;
            tbKontaktRGMail.Text = string.Empty;
            tbKontaktRGOrganisation.Text = string.Empty;
        }
        ///<summary>ctrADR_List / ClearTabKundenEdit</summary>
        ///<remarks></remarks>
        private void tsbtnSaveKunde_Click(object sender, EventArgs e)
        {
            if (CheckMissingInputTabKundeEdit())
            {
                //Fibu-Daten
                decimal decTmp = 0;
                Decimal.TryParse(tbKD_IDman.Text, out decTmp);
                ADR.Kunde.KD_IDman = decTmp;
                Int32 iTmp = 0;
                Int32.TryParse(tbDebitor.Text, out iTmp);
                ADR.Kunde.Debitor = iTmp;
                iTmp = 0;
                if (cbSalesTaxKeyDebitor.SelectedValue != null)
                {
                    Int32.TryParse(cbSalesTaxKeyDebitor.SelectedValue.ToString(), out iTmp);
                }
                ADR.Kunde.SalesTaxKeyDebitor = iTmp;

                iTmp = 0;
                Int32.TryParse(tbKreditor.Text, out iTmp);
                ADR.Kunde.Kreditor = iTmp;
                iTmp = 0;
                if (cbSalesTaxKeyKreditor.SelectedValue != null)
                {
                    Int32.TryParse(cbSalesTaxKeyKreditor.SelectedValue.ToString(), out iTmp);
                }
                ADR.Kunde.SalesTaxKeyKreditor = iTmp;

                ADR.Kunde.SteuerNr = tbStNr.Text;
                ADR.Kunde.USt_ID = tbUStID.Text;
                decTmp = 0;
                Decimal.TryParse(tbMwStSatz.Text, out decTmp);
                ADR.Kunde.MwStSatz = decTmp;
                ADR.Kunde.MwSt = cbMwSt.Checked;
                ADR.Kunde.Zahlungziel = (Int32)nudZZ.Value;

                //Bank
                ADR.Kunde.Bank1 = tbB1.Text;
                iTmp = 0;
                Int32.TryParse(tbB1_Kto.Text, out iTmp);
                ADR.Kunde.Kto1 = iTmp;
                iTmp = 0;
                Int32.TryParse(tbB1_BLZ.Text, out iTmp);
                ADR.Kunde.BLZ1 = iTmp;
                ADR.Kunde.Swift1 = tbSwift1.Text;
                ADR.Kunde.IBAN1 = tbIBAN1.Text;

                ADR.Kunde.Contact = tbKontaktRGAnsprechpartner.Text.Trim();
                ADR.Kunde.Phone = tbKontaktRGPhone.Text.Trim();
                ADR.Kunde.Mailaddress = tbKontaktRGMail.Text.Trim();
                ADR.Kunde.Organisation = tbKontaktRGOrganisation.Text.Trim();

                ADR.Kunde.updateKD();
            }
        }
        ///<summary>ctrADR_List / CheckMissingInputTabKundeEdit</summary>
        ///<remarks>Check Eingaben vor Speicherung</remarks>
        private bool CheckMissingInputTabKundeEdit()
        {
            bool CheckOK = true;
            string strHelp = string.Empty;

            //-- check USTId
            if (tbUStID.Text.Trim() == string.Empty)
            {
                strHelp = strHelp + "Das Eingabefeld USId ist leer!" + Environment.NewLine;
                CheckOK = false;
            }
            else
            {
                Helper_VAT_Validation vat = new Helper_VAT_Validation(tbUStID.Text);
                CheckOK = vat.ValidationOK;
                if (!CheckOK)
                {
                    strHelp = strHelp + "Die UStId: " + tbUStID.Text + " entspricht nicht den Vorgaben eienr UStId!" + Environment.NewLine;
                }
            }
            //-- check E-Mail
            if ((tbKontaktRGMail.Text.Length > 0) && (!helper_EmailValidator.IsValidEmail(tbKontaktRGMail.Text)))
            {
                strHelp = strHelp + "Die E-Mail:[ " + tbKontaktRGMail.Text + " ] ist keine gültige E-Mailadresse!" + Environment.NewLine;
                CheckOK = false;
            }

            //Wenn Bankname ausgefüllt,dann check auf Konto und BLZ
            if (tbB1.Text != string.Empty)
            {
                if (tbIBAN1.Text == string.Empty)
                {
                    CheckOK = false;
                    strHelp = strHelp + "IBAN fehlt" + Environment.NewLine;
                }
                if (tbSwift1.Text == string.Empty)
                {
                    CheckOK = false;
                    strHelp = strHelp + "SWIFT fehlt" + Environment.NewLine;
                }
                if (tbB1_Kto.Text == string.Empty)
                {
                    CheckOK = false;
                    strHelp = strHelp + "Kontonummer fehlt" + Environment.NewLine;
                }
                if (tbB1_BLZ.Text == string.Empty)
                {
                    CheckOK = false;
                    strHelp = strHelp + "BLZ fehlt" + Environment.NewLine;
                }
                //if (tbKontaktRGMail.Text == string.Empty)
                //{
                //    CheckOK = false;
                //    strHelp = strHelp + "E-Mail für die e-Rechnung fehlt" + Environment.NewLine;
                //}
                //if (tbKontaktRGAnsprechpartner.Text == string.Empty)
                //{
                //    CheckOK = false;
                //    strHelp = strHelp + "Ansprechpartner für die e-Rechnung fehlt" + Environment.NewLine;
                //}
            }
            else
            {
                tbIBAN1.Text = string.Empty;
                tbSwift1.Text = string.Empty;
                tbB1_Kto.Text = "0";
                tbB1_BLZ.Text = "0";
            }

            if (!CheckOK)
            {
                strHelp = "Folgende Felder / Pflichtfelder wurden nicht oder fehlerfahft ausgefüllt:\n" + strHelp;
                MessageBox.Show(strHelp);
            }
            return CheckOK;
        }
        ///<summary>ctrADR_List / tbDebitor_Validated</summary>
        ///<remarks>Check Eingabe - nur Zahl</remarks>
        private void tbDebitor_Validated(object sender, EventArgs e)
        {
            Int32 iTmp = 0;
            if (!Int32.TryParse(tbDebitor.Text, out iTmp))
            {
                tbDebitor.Text = "0";
                clsMessages.Allgemein_EingabeIstKeineGanzzahl();
            }
        }
        ///<summary>ctrADR_List / tbKreditor_TextChanged</summary>
        ///<remarks>Check Eingabe - nur Zahl</remarks>
        private void tbKreditor_Validated(object sender, EventArgs e)
        {
            Int32 iTmp = 0;
            if (!Int32.TryParse(tbKreditor.Text, out iTmp))
            {
                tbKreditor.Text = "0";
                clsMessages.Allgemein_EingabeIstKeineGanzzahl();
            }
        }
        ///<summary>ctrADR_List / tbKreditor_TextChanged</summary>
        ///<remarks>Sobald eine Bank angegeben ist, so werden die restlichen Felder freigegben</remarks>
        private void tbB1_TextChanged(object sender, EventArgs e)
        {

        }
        ///<summary>ctrADR_List / tbB1_Kto_TextChanged</summary>
        ///<remarks>Check Eingabe - nur Zahl</remarks>
        private void tbB1_Kto__Validated(object sender, EventArgs e)
        {
            Int32 iTmp = 0;
            if (!Int32.TryParse(tbB1_Kto.Text, out iTmp))
            {
                tbB1_Kto.Text = "0";
                clsMessages.Allgemein_EingabeIstKeineGanzzahl();
            }
        }
        ///<summary>ctrADR_List / tbB1_BLZ_TextChanged</summary>
        ///<remarks>Check Eingabe - nur Zahl</remarks>
        private void tbB1_BLZ__Validated(object sender, EventArgs e)
        {
            Int32 iTmp = 0;
            if (!Int32.TryParse(tbB1_BLZ.Text, out iTmp))
            {
                tbB1_BLZ.Text = "0";
                clsMessages.Allgemein_EingabeIstKeineGanzzahl();
            }
        }
        ///<summary>ctrADR_List / tbB1_Validated</summary>
        ///<remarks>Check Eingabe - nur Zahl</remarks>
        private void tbB1_Validated(object sender, EventArgs e)
        {
            tbB1_BLZ.Enabled = (tbB1.Text != string.Empty);
            tbB1_Kto.Enabled = (tbB1.Text != string.Empty);
            tbSwift1.Enabled = (tbB1.Text != string.Empty);
            tbIBAN1.Enabled = (tbB1.Text != string.Empty);
        }
        ///<summary>ctrADR_List / tbMwStSatz_Validated</summary>
        ///<remarks>Check Eingabe - nur Decimal</remarks>
        private void tbMwStSatz_Validated(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbMwStSatz.Text, out decTmp))
            {
                tbMwStSatz.Text = Functions.FormatDecimal(19.00M);
            }
            else
            {
                tbMwStSatz.Text = Functions.FormatDecimal(decTmp);
            }
        }
        ///<summary>ctrADR_List / tbMwStSatz_Validated</summary>
        ///<remarks>Check Eingabe - nur Decimal</remarks>
        private void tsbtnCloseKunde_Click(object sender, EventArgs e)
        {
            this.scADR.Panel2Collapsed = true;
            ResetCtrADRListWidth();
            ClearTabKundenEdit();
        }
        ///<summary>ctrADR_List / tbKD_IDman_TextChanged</summary>
        ///<remarks>Check Eingabe - nur Ganzzahlen</remarks>
        private void tbKD_IDman_TextChanged(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(this.tbKD_IDman.Text, out decTmp))
            {
                clsMessages.Allgemein_EingabeIstKeineGanzzahl();
                this.tbKD_IDman.Text = string.Empty;
            }
        }
        /******************************************************************************************************
         *                          Edit AdrCat
         * ****************************************************************************************************/
        ///<summary>ctrADR_List / mmPanelContactEdit_Resize</summary>
        ///<remarks></remarks>
        private void mmPanelContactEdit_Resize(object sender, EventArgs e)
        {
            this.tabPageContactEdit.Refresh();
        }
        ///<summary>ctrADR_List / mmPanelContactMailingListAdministration_Resize</summary>
        ///<remarks></remarks>
        private void mmPanelContactMailingListAdministration_Resize(object sender, EventArgs e)
        {
            this.tabPageContactEdit.Refresh();
        }
        ///<summary>ctrADR_List / InitAdrCategory</summary>
        ///<remarks></remarks>
        private void InitAdrCategory()
        {
            //lvSource
            lvAdrCatSource.DataSource = ADR.DictAdrCatagoryUnSelect;
            lvAdrCatSource.DisplayMember = "Value";
            lvAdrCatSource.ValueMember = "Key";
            //lvSelected
            lvAdrCatSelected.DataSource = ADR.DictAdrCatagorySelect;
            lvAdrCatSelected.DisplayMember = "Value";
            lvAdrCatSelected.ValueMember = "Key";
        }
        ///<summary>ctrADR_List / btnADRCatChange_Click</summary>
        ///<remarks></remarks>
        private void btnADRCatChange_Click(object sender, EventArgs e)
        {
            Dictionary<Int32, bool> dictUpdate = new Dictionary<int, bool>();
            //dgvSelected
            //alle nicht selektierten Kategorien werden beibehalten
            for (Int32 i = 0; i <= this.lvAdrCatSelected.Items.Count - 1; i++)
            {
                Int32 iTmp = -1;
                Int32.TryParse(this.lvAdrCatSelected.Items[i].Value.ToString(), out iTmp);
                if (this.lvAdrCatSelected.Items[i].CheckState.Equals(Telerik.WinControls.Enumerations.ToggleState.Off))
                {
                    dictUpdate.Add(iTmp, true);
                }
            }
            //dgvSource
            //alle selektierten werden übernommen
            for (Int32 i = 0; i <= this.lvAdrCatSource.Items.Count - 1; i++)
            {
                Int32 iTmp = -1;
                Int32.TryParse(this.lvAdrCatSource.Items[i].Value.ToString(), out iTmp);
                string strTmp = this.lvAdrCatSource.Items[i].Text.ToString();
                if (this.lvAdrCatSource.Items[i].CheckState.Equals(Telerik.WinControls.Enumerations.ToggleState.On))
                {
                    dictUpdate.Add(iTmp, true);
                }
            }
            ADR.UpdateAdrCategory(dictUpdate);
            InitAdrCategory();
        }

        /**********************************************************************************************************************
         *                              Tab Page Documents / Texte
         * *******************************************************************************************************************/
        ///<summary>ctrADR_List / InitAdrDocumentsEdit</summary>
        ///<remarks></remarks>
        private void InitTabDocumentsEdit()
        {
            //DictMcbDocumentArtSource = new Dictionary<int, string>();
            bUpdateText = false;
            //InitMultiColumnsComboDocumentsAssignment();

            clsDocKey dKey = new clsDocKey();
            comboAdrText_DocumentArt.DataSource = dKey.dtDocKeysForAdrText;
            comboAdrText_DocumentArt.DisplayMember = "DocKey";
            comboAdrText_DocumentArt.ValueMember = "ID";

            comboADRTextArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(this._ctrMenu._frmMain.system.BenutzerID);
            comboADRTextArbeitsbereich.DisplayMember = "Arbeitsbereich";
            comboADRTextArbeitsbereich.ValueMember = "ID";

            ClearADRTextInputFields();
            SetADRTextInputFieldsEnabeld(false);
            InitDVGTextBaustein();
        }
        ///<summary>ctrADR_List / tsbtnDocSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDocSave_Click(object sender, EventArgs e)
        {
            ADR.Update();
            InitTabDocumentsEdit();
        }
        ///<summary>ctrADR_List / tsbtnDocClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDocClose_Click(object sender, EventArgs e)
        {
            this.scADR.Panel2Collapsed = true;
            ResetCtrADRListWidth();
            ClearTabKundenEdit();
        }
        ///<summary>ctrADR_List / tsbtnADRTextAdd_Click</summary>
        ///<remarks></remarks>
        private void tsbtnADRTextAdd_Click(object sender, EventArgs e)
        {
            this.ADR.ADRTexte = new clsADRText();
            this.ADR.ADRTexte.InitClass(this._ctrMenu._frmMain.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0);

            ClearADRTextInputFields();
            SetADRTextInputFieldsEnabeld(true);

            this.comboAdrText_DocumentArt.SelectedIndex = 0;
            //Standard soll für alle Arbeitsbereiche sein
            comboADRTextArbeitsbereich.SelectedIndex = -1;
        }
        ///<summary>ctrADR_List / tsbtnADRTextSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnADRTextSave_Click(object sender, EventArgs e)
        {
            if (this.comboAdrText_DocumentArt.SelectedIndex > -1)
            {
                if (this.tbADRTextTextBaustein.Text != string.Empty)
                {
                    clsADRText tmpText = new clsADRText();
                    tmpText = this.ADR.ADRTexte;

                    tmpText.AdrID = this.ADR.ID;
                    tmpText.Text = tbADRTextTextBaustein.Text.Trim();
                    tmpText.DocumentArtID = (int)this.comboAdrText_DocumentArt.SelectedValue;
                    tmpText.DocumentArtName = this.comboAdrText_DocumentArt.Text;
                    tmpText.ArbeitsbereichID = 0;
                    tmpText.UseForAll = cbADRText_UseForAll.Checked;
                    tmpText.IsReceiver = cbADRText_IsReceiver.Checked;
                    if (!cbADRText_UseForAll.Checked)
                    {
                        int iId = 0;
                        int.TryParse(this.comboADRTextArbeitsbereich.SelectedValue.ToString(), out iId);
                        tmpText.ArbeitsbereichID = iId;
                    }

                    this.ADR.ADRTexte = new clsADRText();
                    this.ADR.ADRTexte = tmpText.Copy();

                    if (this.ADR.ADRTexte.ID > 0)
                    {
                        //this.ADR.ADRTexte.ID = tmpText.ID;
                        this.ADR.ADRTexte.Update();
                    }
                    else
                    {
                        this.ADR.ADRTexte.Add();
                    }
                    SetADRTextInputFieldsEnabeld(false);
                    ClearADRTextInputFields();
                    InitDVGTextBaustein();
                }
                else
                {
                    clsMessages.ADRText_TextForTextModulIsEmty();
                }
            }
            else
            {
                clsMessages.ADRText_NoDocumtenArtSelection();
            }
        }
        ///<summary>ctrADR_List / tsbtnADRTextDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnADRTextDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvText.Rows.Count > 0)
            {
                if (this.ADR.ADRTexte.ID > 0)
                {
                    if (clsMessages.DeleteAllgemein())
                    {
                        this.ADR.ADRTexte.Delete();
                        ClearADRTextInputFields();
                        SetADRTextInputFieldsEnabeld(false);
                        InitDVGTextBaustein();
                        //InitMultiColumnsComboDocumentsTextModul();
                    }
                }
            }
        }
        ///<summary>ctrADR_List / ClearADRTextInputFields</summary>
        ///<remarks></remarks>
        private void ClearADRTextInputFields()
        {
            if (this.comboAdrText_DocumentArt.SelectedIndex > -1)
            {
                this.comboAdrText_DocumentArt.SelectedIndex = 0;
            }
            this.tbADRTextTextBaustein.Text = string.Empty;
            this.comboADRTextArbeitsbereich.SelectedIndex = -1;
            this.cbADRText_UseForAll.Checked = true;
            this.cbADRText_IsReceiver.Checked = false;
        }
        ///<summary>ctrADR_List / SetADRTextInputFieldsEnabeld</summary>
        ///<remarks></remarks>
        private void SetADRTextInputFieldsEnabeld(bool bEnabled)
        {
            this.comboAdrText_DocumentArt.Enabled = bEnabled;
            this.comboADRTextArbeitsbereich.Enabled = bEnabled;
            this.tbADRTextTextBaustein.Enabled = bEnabled;
            this.cbADRText_UseForAll.Enabled = bEnabled;
            this.cbADRText_IsReceiver.Enabled = bEnabled;
        }
        ///<summary>ctrADR_List / InitDVGTextBaustein</summary>
        ///<remarks></remarks>
        private void InitDVGTextBaustein()
        {
            this.dgvText.DataSource = this.ADR.ADRTexte.dtAdrText;
            for (Int32 i = 0; i <= this.dgvText.Columns.Count - 1; i++)
            {
                string colName = this.dgvText.Columns[i].Name.ToString();
                switch (colName)
                {
                    case "DocumentArtName":
                        this.dgvText.Columns[i].HeaderText = "Dokumentenart";
                        break;

                    case "Text":
                        Int32 iTmpWidth = this.dgvText.Width - this.dgvText.Columns["DocumentArtName"].Width;
                        this.dgvText.Columns[i].Width = iTmpWidth;
                        this.dgvText.Columns[i].WrapText = true;
                        break;

                    case "Arbeitsbereich":
                    case "Name":
                        this.dgvText.Columns[i].HeaderText = "Arbeitsbereich";
                        this.dgvText.Columns[i].IsVisible = true;
                        this.dgvText.Columns[i].WrapText = true;
                        break;

                    case "IsReceiver":
                        this.dgvText.Columns[i].HeaderText = "Empfänger";
                        this.dgvText.Columns[i].IsVisible = true;
                        break;

                    case "UseForAll":
                        this.dgvText.Columns[i].IsVisible = true;
                        break;

                    default:
                        this.dgvText.Columns[i].IsVisible = false;
                        break;
                }
            }
            this.dgvText.BestFitColumns();
            //SetSelected and Current Row
            for (Int32 i = 0; i <= this.dgvText.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgvText.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                if (decTmp == this.ADR.ADRTexte.ID)
                {
                    this.dgvText.Rows[i].IsSelected = true;
                    this.dgvText.Rows[i].IsCurrent = true;
                }
            }
        }
        ///<summary>ctrADR_List / dgvText_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvText_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvText.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvText.Rows[this.dgvText.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    ADR.ADRTexte = new clsADRText();
                    ADR.ADRTexte._GL_User = this.GL_User;
                    ADR.ADRTexte.ID = decTmp;
                    ADR.ADRTexte.Fill();
                }
            }
        }
        ///<summary>ctrADR_List / dgvText_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvText_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.dgvText.Rows.Count > 0)
            {
                bUpdateText = true;
                ClearADRTextInputFields();
                SetADRTextInputFieldsEnabeld(true);
                SetADRTextDatenToCtr();
            }
        }
        /// <summary>
        ///             
        /// </summary>
        private void SetADRTextDatenToCtr()
        {
            //zuerst muss jetzt wieder der DokumentenARtenName udn ID 
            //zur MultiColumnBox hinzufügen, dazu vorher noch einmal die
            //MultiColumns Combo aktuallisieren
            //InitMultiColumnsComboDocumentsTextModul();
            try
            {
                this.DictMcbDocumentArtSource.Add((Int32)this.ADR.ADRTexte.DocumentArtID, this.ADR.ADRTexte.DocumentArtName);
            }
            catch (Exception ex)
            {
                clsError Error = new clsError();
                Error._GL_User = this.GL_User;
                Error.Code = clsError.code5_401;
                Error.Aktion = "ctrADRList";
                Error.ErrorText = ex.ToString();
                Error.WriteError();
            }
            //FormatMultiColumnsDocumentsTextModul(this.DictMcbDocumentArtSource);

            Functions.SetComboToSelecetedValue(ref comboAdrText_DocumentArt, ((int)ADR.ADRTexte.DocumentArtID).ToString());

            //Functions.SetRADMultiColumnBoxComboToSelecetedValueByValue(ref mcbADRTextDocumtenArt, ADR.ADRTexte.DocumentArtID.ToString());
            this.tbADRTextTextBaustein.Text = ADR.ADRTexte.Text;
            Functions.SetComboToSelecetedValue(ref this.comboADRTextArbeitsbereich, ADR.ADRTexte.ArbeitsbereichID.ToString());
            this.cbADRText_UseForAll.Checked = ADR.ADRTexte.UseForAll;
            this.cbADRText_IsReceiver.Checked = ADR.ADRTexte.IsReceiver;
        }
        ///<summary>ctrADR_List / tsbtnADRTextRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnADRTextRefresh_Click(object sender, EventArgs e)
        {
            //this.InitMultiColumnsComboDocumentsTextModul();
            this.InitDVGTextBaustein();
        }
        ///<summary>ctrADR_List / adresslisteaktivToolStripMenuItem_Click</summary>
        ///<remarks>Adressliste nur ative Adressen</remarks>
        private void adresslisteaktivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ADRListeKomplett = true;
            iAdrRange = clsADR.const_AdrRange_AdrListAktiv;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListAktivString;
            SearchAdr();
            //initList();
        }
        ///<summary>ctrADR_List / adresslistepassivToolStripMenuItem_Click</summary>
        ///<remarks>Adressliste nur passive Adressen</remarks>
        private void adresslistepassivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ADRListeKomplett = true;
            iAdrRange = clsADR.const_AdrRange_AdrListePassiv;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListePassivString;
            SearchAdr();
            //initList();
        }
        ///<summary>ctrADR_List / cbPostPrintAnzeigen_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbPostPrintAnzeigen_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPostPrintAnzeigen.Checked)
            {
                cbPostVerteilerAnzeigen.SelectedIndex = -1;
                cbPostVerteilerAnzeigen.Enabled = false;
            }
            else
            {
                Functions.SetComboToSelecetedValue(ref this.cbPostVerteilerAnzeigen, this.ADR.PostAnzeigeBy.ToString());
                cbPostVerteilerAnzeigen.Enabled = true;
            }
        }
        ///<summary>ctrADR_List / cbPostPrintListen_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbPostPrintListen_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPostPrintListen.Checked)
            {
                cbPostVerteilerListen.SelectedIndex = -1;
                cbPostVerteilerListen.Enabled = false;
            }
            else
            {
                Functions.SetComboToSelecetedValue(ref this.cbPostVerteilerListen, this.ADR.PostListBy.ToString());
                cbPostVerteilerListen.Enabled = true;
            }
        }

        ///<summary>ctrADR_List / cbPostPrintAnlage_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbPostPrintAnlage_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPostPrintAnlage.Checked)
            {
                cbPostVerteilerAnlage.SelectedIndex = -1;
                cbPostVerteilerAnlage.Enabled = false;
            }
            else
            {
                Functions.SetComboToSelecetedValue(ref this.cbPostVerteilerAnlage, this.ADR.PostAnlageBy.ToString());
                cbPostVerteilerAnlage.Enabled = true;
            }
        }
        ///<summary>ctrADR_List / cbPostPrintRG_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbPostPrintRG_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPostPrintRG.Checked)
            {
                cbPostVerteilerRG.SelectedIndex = -1;
                cbPostVerteilerRG.Enabled = false;
            }
            else
            {
                Functions.SetComboToSelecetedValue(ref this.cbPostVerteilerRG, this.ADR.PostRGBy.ToString());
                cbPostVerteilerRG.Enabled = true;
            }
        }
        ///<summary>ctrADR_List / cbPostPrintLfs_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbPostPrintLfs_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPostPrintLfs.Checked)
            {
                cbPostVerteilerLfs.SelectedIndex = -1;
                cbPostVerteilerLfs.Enabled = false;
            }
            else
            {
                if (this.cbPostVerteilerLfs.Items.Count > 0)
                {
                    Functions.SetComboToSelecetedValue(ref this.cbPostVerteilerLfs, this.ADR.PostLfsBy.ToString());
                    cbPostVerteilerLfs.Enabled = true;
                }
                else
                {
                    cbPostVerteilerLfs.SelectedIndex = -1;
                }
            }
        }
        ///<summary>ctrADR_List / tsbtnADRPostSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnADRPostSave_Click(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            //Post Lfs
            if (!cbPostPrintLfs.Checked)
            {
                if (cbPostVerteilerLfs.SelectedValue != null)
                {
                    Decimal.TryParse(cbPostVerteilerLfs.SelectedValue.ToString(), out decTmp);
                }
                else
                {
                    cbPostVerteilerLfs.SelectedIndex = -1;
                }
            }
            ADR.PostLfsBy = decTmp;
            decTmp = 0;
            //Post RG
            if (!cbPostPrintRG.Checked)
            {
                if (cbPostVerteilerRG.SelectedValue != null)
                {
                    Decimal.TryParse(cbPostVerteilerRG.SelectedValue.ToString(), out decTmp);
                }
                else
                {
                    cbPostVerteilerRG.SelectedIndex = -1;
                }
            }
            ADR.PostRGBy = decTmp;
            decTmp = 0;
            //Post Anlagen
            if (!cbPostPrintAnlage.Checked)
            {
                if (cbPostVerteilerAnlage.SelectedValue != null)
                {
                    Decimal.TryParse(cbPostVerteilerAnlage.SelectedValue.ToString(), out decTmp);
                }
                else
                {
                    cbPostVerteilerAnlage.SelectedIndex = -1;
                }
            }
            ADR.PostAnlageBy = decTmp;
            decTmp = 0;
            //Post Listen
            if (!cbPostPrintListen.Checked)
            {
                if (cbPostVerteilerListen.SelectedValue != null)
                {
                    Decimal.TryParse(cbPostVerteilerListen.SelectedValue.ToString(), out decTmp);
                }
                else
                {
                    cbPostVerteilerListen.SelectedIndex = -1;
                }
            }
            ADR.PostListBy = decTmp;
            decTmp = 0;
            //Post Anzeigen
            if (!cbPostPrintAnzeigen.Checked)
            {
                if (cbPostVerteilerAnzeigen.SelectedValue != null)
                {
                    Decimal.TryParse(cbPostVerteilerAnzeigen.SelectedValue.ToString(), out decTmp);
                }
                else
                {
                    cbPostVerteilerAnzeigen.SelectedIndex = -1;
                }
            }
            ADR.PostAnzeigeBy = decTmp;
            decTmp = 0;

            ADR.Update();
            InitTabADREdit();
            SetADRDatenToTabADREdit();
        }
        ///<summary>ctrADR_List / setAdrRange</summary>
        ///<remarks>Wandelt die Nummer der Searchbuttons in das Interne-Suchschema um</remarks>
        private void setAdrRange()
        {
            // deaktivieren des Buttons zum Kategorie wählen
            this.ttbListe.Enabled = false;
            switch (ADRListAuswahl)
            {
                case 1:
                case 8:
                    iAdrRange = clsADR.const_AdrRange_AdrListeKunde;
                    break;
                case 2:
                    iAdrRange = clsADR.const_AdrRange_AdrListeVersender;
                    break;
                case 3:
                    iAdrRange = clsADR.const_AdrRange_AdrListeEmpfaenger;
                    break;
                case 7:
                    iAdrRange = clsADR.const_AdrRange_AdrListeSpedition;
                    break;
                case 12:
                    iAdrRange = clsADR.const_AdrRange_AdrListeEntlade;
                    break;
                case 13:
                    iAdrRange = clsADR.const_AdrRange_AdrListeBelade;
                    break;
                case 11:
                    iAdrRange = clsADR.const_AdrRange_AdrListePost;
                    break;
                case 10:
                    iAdrRange = clsADR.const_AdrRange_AdrListeRechnung;
                    break;
                default:
                    // falls kein Searchbutton aktiv ist
                    this.ttbListe.Enabled = true;
                    break;
            }
        }
        ///<summary>ctrADR_List / tabADR_Deselecting</summary>
        ///<remarks>Verhindert das Wechseln des Tabs falls keine AdressenID vorliegt</remarks>
        private void tabADR_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if ((ADR.ID == 0 && e.TabPageIndex == 0) || (ADR.ID == 0 && (decActivTab != 0 || (decActivTab == 0 && tabADR.SelectedIndex != 0))))
            {

                e.Cancel = true;
                clsMessages.ADR_KeineAdressenIdVorhanden();
            }
        }

        /**********************************************************************************************************************
        *                              Tab Page ExtraCharge
        * *******************************************************************************************************************/
        ///<summary>ctrADR_List / InitTabExtraChargeEdit</summary>
        ///<remarks></remarks>
        private void InitTabExtraChargeEdit()
        {
            ExtraCharge = new clsExtraCharge();

            SetExtraChargeEditInputFieldEnabled(false);
            ClearExtraChargeInputField();
            //DGV laden
            InitDGVExtraCharge();


        }
        ///<summary>ctrADR_List / tsbExtraCharge_Click</summary>
        ///<remarks>neue Sonderkosten anlegen</remarks>
        private void tsbExtraCharge_Click(object sender, EventArgs e)
        {
            if (grdADRList.Rows.Count >= 1)
            {
                if (ADR.ID > 0)
                {
                    SetTabEingabeFelder();
                    this.tabADR.SelectedTab = tabPageExtraCharge;
                    this.scADR.Panel2Collapsed = false;
                    ResetCtrADRListWidth();
                    InitDGVExtraCharge();
                }
            }
        }
        ///<summary>ctrADR_List / tsbtnOpenEditExtraCharge_Click</summary>
        ///<remarks></remarks>
        private void tsbtnOpenEditExtraCharge_Click(object sender, EventArgs e)
        {
            ShowAndHideExtraChargeEdit();
        }
        ///<summary>ctrADR_List / tsbtnExtraChargeSave_Click</summary>
        ///<remarks>Eingabedaten speichern</remarks>
        private void tsbtnExtraChargeSave_Click(object sender, EventArgs e)
        {
            if (!tbExtraChargeBezeichnung.Text.Equals(string.Empty))
            {

                decimal decTmp = 0;
                Decimal.TryParse(tbExtraChargePreis.Text, out decTmp);
                ExtraChargeADR.Preis = decTmp;
                // prüfen ob die ADR bereits einen Kundenbezogenen PReis besitzt



                if (bUpdate)
                {
                    ExtraChargeADR.Update();
                }
                else
                {

                    ExtraChargeADR.Add();
                    // ExtraCharge.Add();
                }
                InitDGVExtraCharge();
            }
            ShowAndHideExtraChargeEdit();
            ClearExtraChargeInputField();
            SetExtraChargeEditInputFieldEnabled(false);
            bUpdate = false;
        }
        ///<summary>ctrADR_List / InitDGVExtraCharge</summary>
        ///<remarks></remarks>
        private void InitDGVExtraCharge()
        {
            if (this.ADR.ID > 0)
            {
                ExtraCharge = new clsExtraCharge();
                DataTable dt = clsExtraCharge.GetExtraCharge(this.GL_User, this.ADR.ID);

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
                            case "erstellt":
                                this.dgvExtraCharge.Columns[i].IsVisible = false;
                                break;
                            case "Preis":
                                this.dgvExtraCharge.Columns[i].FormatString = "{0:#,##0.00}";
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
        }
        ///<summary>ctrADR_List / ClearExtraChargeInputField</summary>
        ///<remarks></remarks>
        private void ClearExtraChargeInputField()
        {
            this.tbExtraChargeBezeichnung.Text = string.Empty;
            this.tbExtraChargePreis.Text = string.Empty;

        }
        ///<summary>ctrADR_List / SetExtraChargeEditInputFieldEnabled</summary>
        ///<remarks></remarks>
        private void SetExtraChargeEditInputFieldEnabled(bool bEnabled)
        {
            this.tbExtraChargeBezeichnung.Enabled = bEnabled;
            this.tbExtraChargePreis.Enabled = bEnabled;
        }
        ///<summary>ctrADR_List / tsbNewExtraCharge_Click</summary>
        ///<remarks></remarks>
        private void tsbNewExtraCharge_Click(object sender, EventArgs e)
        {
            bUpdate = false;
            panExtraChargeEdit.Visible = true;
            InitExtraChargeEdit();
        }
        ///<summary>ctrADR_List / InitExtraChargeEdit</summary>
        ///<remarks></remarks>
        private void InitExtraChargeEdit()
        {
            ClearExtraChargeInputField();
            SetExtraChargeEditInputFieldEnabled(true);
            // InitDGVExtraCharge();
        }
        ///<summary>ctrADR_List / SettsbtnOpenEditExtraChargeImage</summary>
        ///<remarks></remarks>
        private void SettsbtnOpenEditExtraChargeImage()
        {
            if (this.panExtraChargeEdit.Visible == true)
            {
                this.tsbtnOpenEditExtraCharge.Image = Sped4.Properties.Resources.layout_top;
            }
            else
            {
                this.tsbtnOpenEditExtraCharge.Image = Sped4.Properties.Resources.layout;
            }
        }
        ///<summary>ctrADR_List / ShowAndHideADREdit</summary>
        ///<remarks>ADR-Datenerfassung ein-/ausblenden.</remarks>
        public void ShowAndHideExtraChargeEdit()
        {
            this.panExtraChargeEdit.Visible = !this.panExtraChargeEdit.Visible;
            SettsbtnOpenEditExtraChargeImage();
        }
        ///<summary>ctrExtraCharge / SetExtraChargeDatenToCtr</summary>
        ///<remarks>Eingabedaten speichern</remarks>
        private void SetExtraChargeDatenToCtr()
        {
            if (ExtraCharge.ID > 0)
            {
                ExtraChargeADR = new clsExtraChargeADR();
                ExtraChargeADR.ExtraChargeID = ExtraCharge.ID;
                ExtraChargeADR.AdrID = this.ADR.ID;

                if ((bool)this.dgvExtraCharge.SelectedRows[0].Cells["Kundenbezogen"].Value == true)
                {
                    ExtraChargeADR.Fill();
                    bUpdate = true;
                    tbExtraChargePreis.Text = Functions.FormatDecimal(ExtraChargeADR.Preis);
                }
                else
                {
                    tbExtraChargePreis.Text = Functions.FormatDecimal(ExtraCharge.Preis);
                }
                this.tbExtraChargeBezeichnung.Text = ExtraCharge.Bezeichnung;


            }
            else
            {
                ClearExtraChargeInputField();
                SetExtraChargeEditInputFieldEnabled(false);
            }
        }
        ///<summary>ctrADR_List / dgvExtraCharge_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvExtraCharge_MouseDoubleClick(object sender, MouseEventArgs e)
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
                if (this.ExtraCharge.ID > 0)
                {
                    ClearExtraChargeInputField();
                    //InitDGVExtraCharge();
                    panExtraChargeEdit.Visible = true;
                    SettsbtnOpenEditExtraChargeImage();
                    SetExtraChargeEditInputFieldEnabled(true);
                    SetExtraChargeDatenToCtr();

                    //ResetCtrExtraChargeWidth();
                }
                //bUpdate = true;
            }
        }
        ///<summary>ctrADR_List / dgvExtraCharge_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvExtraCharge_MouseClick(object sender, MouseEventArgs e)
        {
            //if (this.dgvExtraCharge.Rows.Count > 0)
            //{
            //    decimal decTmp = 0;
            //    string strTmp = this.dgvExtraCharge.Rows[this.dgvExtraCharge.CurrentRow.Index].Cells["ID"].Value.ToString();
            //    Decimal.TryParse(strTmp, out decTmp);
            //    if (decTmp > 0)
            //    {
            //        ExtraCharge.ID = decTmp;
            //        ExtraCharge.Fill();
            //        this.dgvExtraCharge.CurrentRow.IsSelected = true;
            //    }
            //}
        }
        ///<summary>ctrADR_List / tsbnExtraChargeEditClose_Click</summary>
        ///<remarks></remarks>
        private void tsbnExtraChargeEditClose_Click(object sender, EventArgs e)
        {
            ShowAndHideExtraChargeEdit();
            ClearExtraChargeInputField();
            SetExtraChargeEditInputFieldEnabled(false);
        }
        ///<summary>ctrADR_List / tsbEditExtraCharge_Click</summary>
        ///<remarks></remarks>
        private void tsbEditExtraCharge_Click(object sender, EventArgs e)
        {
            //bUpdate = true;
            //panExtraChargeEdit.Visible = true;
            //SettsbtnOpenEditExtraChargeImage();
            //InitExtraChargeEdit();
            //SetExtraChargeEditInputFieldEnabled(true);
            //SetExtraChargeDatenToCtr();



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
                if (this.ExtraCharge.ID > 0)
                {
                    ClearExtraChargeInputField();
                    //InitDGVExtraCharge();
                    panExtraChargeEdit.Visible = true;
                    SettsbtnOpenEditExtraChargeImage();
                    SetExtraChargeEditInputFieldEnabled(true);
                    SetExtraChargeDatenToCtr();

                    //ResetCtrExtraChargeWidth();
                }
                //bUpdate = true;
            }
        }
        ///<summary>ctrADR_List / tsbRefreshExtraCharge_Click</summary>
        ///<remarks></remarks>
        private void tsbRefreshExtraCharge_Click(object sender, EventArgs e)
        {
            InitDGVExtraCharge();
        }
        ///<summary>ctrADR_List / tsbCloseExtraCharge_Click</summary>
        ///<remarks></remarks>
        private void tsbCloseExtraCharge_Click(object sender, EventArgs e)
        {
            this.scADR.Panel2Collapsed = true;
            ResetCtrADRListWidth();
            ClearExtraChargeInputField();

        }

        private void tsbtnListDeleteExtraCharge_Click(object sender, EventArgs e)
        {
            DeleteExtraChargeItem();
        }

        ///<summary>ctrADR_List / DelelteExtraChargeItem</summary>
        ///<remarks>Löschen des gewählten Datensatzes</remarks>
        private void DeleteExtraChargeItem()
        {
            if (this.ExtraCharge.ID > 0)
            {
                if (clsMessages.DeleteAllgemein())
                {
                    //this.ExtraCharge.Delete();
                    this.ExtraChargeADR.Delete();
                    InitDGVExtraCharge();
                    this.panExtraChargeEdit.Visible = false;
                    ResetCtrADRListWidth();
                    InitExtraChargeEdit();
                }
            }
        }
        ///<summary>ctrADR_List / dgvExtraCharge_SelectionChanged</summary>
        ///<remarks></remarks>
        private void dgvExtraCharge_SelectionChanged(object sender, EventArgs e)
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
                    bool bKundenbezogen = (bool)this.dgvExtraCharge.CurrentRow.Cells["Kundenbezogen"].Value;
                    tsbtnListDeleteExtraCharge.Visible = bKundenbezogen;
                    if (bKundenbezogen == true)
                    {
                        ExtraChargeADR = new clsExtraChargeADR();
                        ExtraChargeADR.ExtraChargeID = ExtraCharge.ID;
                        ExtraChargeADR.AdrID = this.ADR.ID;
                    }

                }
            }
        }
        ///<summary>ctrADR_List / tsbListExcelExportExtraCharge_Click</summary>
        ///<remarks></remarks>
        private void tsbListExcelExportExtraCharge_Click(object sender, EventArgs e)
        {
            string strFileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_" + ADR.Name1 + "_Sonderkosten.xls";
            saveFileDialog.FileName = strFileName;
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            strFileName = this.saveFileDialog.FileName;
            bool openExportFile = false;

            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvExtraCharge, strFileName, ref openExportFile, this.GL_User, true);

            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(strFileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Sonderkosten - LIST - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }

        private void miListeKunde_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeKunde;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListeKundeString;
            SearchAdr();
            //initList();
        }

        private void miListeVersender_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeVersender;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListeVersenderString;
            SearchAdr();
            //initList();
        }

        private void miListeBelade_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeBelade;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListeBeladeString;
            SearchAdr();
            //initList();
        }

        private void miListeEntlade_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeEntlade;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListeEntladeString;
            SearchAdr();
            //initList();
        }

        private void miListePost_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListePost;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListePostString;
            SearchAdr();
            //initList();
        }

        private void miListeRechnung_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeRechnung;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListeRechnungString;
            SearchAdr();
            //initList();
        }

        private void miListeSpedition_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeSpedition;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListeSpeditionString;
            SearchAdr();
            //initList();
        }

        private void miListeDiverse_Click(object sender, EventArgs e)
        {
            iAdrRange = clsADR.const_AdrRange_AdrListeDiverse;
            afColorLabel1.myText = clsADR.const_AdrRange_AdrListeDiverseString;
            SearchAdr();
            //initList();
        }

        private void ctrADR_List_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                //worker = new BackgroundWorker();
                //worker.WorkerReportsProgress = true;
                //worker.WorkerSupportsCancellation = true;

                //worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                //worker.RunWorkerCompleted +=
                //     new RunWorkerCompletedEventHandler(worker_CompleteWork);
                //worker.RunWorkerAsync();
            }
        }

        private void tbExtraChargePreis_Validated(object sender, EventArgs e)
        {

            decimal decTmp = 0;
            if (!Decimal.TryParse(tbExtraChargePreis.Text.Trim(), out decTmp))
            {
                if (tbExtraChargePreis.Text != string.Empty)
                {
                    clsMessages.Allgemein_EingabeIstKeineDecimalzahl();
                }
                tbExtraChargePreis.Text = "0";
            }
            else
            {
                tbExtraChargePreis.Text = Functions.FormatDecimal(decTmp);
            }
        }
        /**********************************************************************************************************************
       *                              Tab Page Kommuniaktion
       * *******************************************************************************************************************/
        ///<summary>ctrADR_List / InitTabASNEdit</summary>
        ///<remarks></remarks>
        private void InitTabASNEdit()
        {
            clsADRVerweis ADRVerweise = new clsADRVerweis();
            //cbArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichListByStatus(GL_User.User_ID, true);
            cbArbeitsbereich.DataSource = dtArbeitsbereiche;
            cbArbeitsbereich.ValueMember = "ID";
            cbArbeitsbereich.DisplayMember = "Arbeitsbereich";

            List<string> ListVerweisArtTmp = clsADRVerweis.ListVerweisArt();
            comboVerweisArt.DataSource = ListVerweisArtTmp;
            comboVerweisArt.SelectedIndex = -1;

            SetASNEditInputFieldEnabled(false);
            ClearASNInputField();
            InitDGVASN();
        }
        ///<summary>ctrADR_List / InitDGVASN</summary>
        ///<remarks></remarks>
        private void InitDGVASN()
        {
            clsADRVerweis ADRVerweise = new clsADRVerweis();
            ADRVerweise.InitClass(this.GL_User);
            ADRVerweise.SenderAdrID = this.ADR.ID;
            dgvKommunikation.DataSource = ADRVerweise.GetADRVerweiseList();

            for (Int32 i = 0; i <= this.dgvKommunikation.Columns.Count - 1; i++)
            {
                string colName = this.dgvKommunikation.Columns[i].Name.ToString();
                switch (colName)
                {
                    case "ID":
                    case "MandantenID":
                    case "VerweisAdrID":
                    case "LieferantenID":
                        this.dgvKommunikation.Columns[i].IsVisible = false;
                        break;
                    case "ViewID":
                        this.dgvKommunikation.Columns[i].Name = "Sender";
                        this.dgvKommunikation.Columns[i].IsVisible = false;
                        break;
                    case "ADRVerweis":
                        //this.dgvKommunikation.Columns[i].Name = "Sender";
                        this.dgvKommunikation.Columns[i].IsVisible = true;
                        break;
                    case "Name":
                        this.dgvKommunikation.Columns[i].Name = "Arbeitsbereich";
                        break;
                    case "LieferantenVerweis":
                        this.dgvKommunikation.Columns[i].Name = "LieferantenNr";
                        break;
                    case "Typ":
                        this.dgvKommunikation.Columns[i].Name = "Typ";
                        break;
                }
                this.dgvKommunikation.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
            }
            this.dgvKommunikation.BestFitColumns();
        }
        ///<summary>ctrADR_List / ClearASNInputField</summary>
        ///<remarks></remarks>
        private void ClearASNInputField()
        {
            tbVerweisADR.Text = string.Empty;
            cbArbeitsbereich.SelectedIndex = 0;
            comboVerweisArt.SelectedIndex = -1;
            tbASNVerweis.Text = string.Empty;
            tbASNLieferantenNummer.Text = string.Empty;
        }
        ///<summary>ctrADR_List / SetASNEditInputFieldEnabled</summary>
        ///<remarks></remarks>
        private void SetASNEditInputFieldEnabled(bool bEnabled)
        {
            tbVerweisADR.Enabled = bEnabled;
            cbArbeitsbereich.Enabled = bEnabled;
            comboVerweisArt.Enabled = bEnabled;
            tbASNVerweis.Enabled = bEnabled;
            tbASNLieferantenNummer.Enabled = bEnabled;
            tbADRVerweisBemerkung.Enabled = bEnabled;
        }
        ///<summary>ctrADR_List / tbSender_TextChanged</summary>
        ///<remarks></remarks>
        private void tbSender_TextChanged(object sender, EventArgs e)
        {
            //Adressdaten laden
            DataTable dt = new DataTable();
            dt = clsADR.GetADRList(this.GL_User.User_ID);
            DataTable dtTmp = new DataTable();

            string SearchText = tbVerweisADR.Text.ToString();
            string Ausgabe = "";

            DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = dt.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbVerweisADRLong.Text = Functions.GetADRStringFromTable(dtTmp);
        }
        ///<summary>ctrADR_List / tsbtnASNSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnASNSave_Click(object sender, EventArgs e)
        {

            string strError = string.Empty;
            decimal decLieferant = -1;
            if (tbVerweisADRLong.Text == string.Empty)
            {
                strError += "Es wurde kein (gültige) Verweisadresse eingetragen!\n";
            }
            if (tbASNVerweis.Text == string.Empty)
            {
                strError += "Es wurde kein Verweis eingetragen!\n";
            }
            else
            {
                //decimal.TryParse(tbASNVerweis.Text, out decLieferant);
                //Console.WriteLine(decLieferant);
                //if (tbASNLieferantenNummer.Text == string.Empty)
                //{
                //    if (decLieferant > -1)
                //    {
                //        tbASNLieferantenNummer.Text = decLieferant.ToString();
                //    }
                //    else
                //    {
                //        tbASNLieferantenNummer.Text = "0";
                //    }
                //}
            }
            //Wenn VDA4905 dann darf die Lieferantennummer 0 sein, sonst nicht
            if ((tbASNLieferantenNummer.Text == "0" || tbASNLieferantenNummer.Text == string.Empty) & (cbASNFileTyp.Text != constValue_AsnArt.const_Art_VDA4905))
            {
                strError += "Es wurde keine Lieferantennummer eingetragen!\n";
            }

            if (strError == string.Empty)
            {
                //decLieferant = -1;
                //decimal.TryParse(tbASNLieferantenNummer.Text, out decLieferant);

                if (tbASNLieferantenNummer.Text != string.Empty)
                {
                    ADRVerweis.SupplierNo = string.Empty;
                    ADRVerweis.LieferantenVerweis = tbASNLieferantenNummer.Text;
                    ADRVerweis.VerweisAdrID = clsADR.GetIDByMatchcode(tbVerweisADR.Text);
                    ADRVerweis.SenderAdrID = this.ADR.ID;
                    ADRVerweis.ArbeitsbereichID = (decimal)cbArbeitsbereich.SelectedValue;
                    ADRVerweis.Verweis = tbASNVerweis.Text.Trim();
                    ADRVerweis.MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
                    ADRVerweis.VerweisArt = comboVerweisArt.SelectedItem.ToString();
                    ADRVerweis.aktiv = cbAktiv.Checked;
                    ADRVerweis.ASNFileTyp = cbASNFileTyp.Text;
                    ADRVerweis.Bemerkung = tbADRVerweisBemerkung.Text;

                    if (bUpdate)
                    {
                        ADRVerweis.Update();
                    }
                    else
                    {

                        ADRVerweis.Add();
                    }
                    InitDGVASN();
                    ClearASNInputField();
                    SetASNEditInputFieldEnabled(false);
                    bUpdate = false;
                    mmPanelADRCommunication.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                }

            }
            else
            {
                clsMessages.ADRVerweis_InputError(strError);
            }
        }
        ///<summary>ctrADR_List / tsbtnASNNew_Click</summary>
        ///<remarks></remarks>
        private void tsbtnASNNew_Click(object sender, EventArgs e)
        {
            ADRVerweis = new clsADRVerweis();
            ClearASNInputField();
            mmPanelADRCommunication.SetExpandCollapse(AFMinMaxPanel.EStatus.Collapsed);
            SetASNEditInputFieldEnabled(true);
        }
        ///<summary>ctrADR_List / tsbtnASNDelete_Click</summary>
        ///<remarks></remarks>
        private void tsbtnASNDelete_Click(object sender, EventArgs e)
        {
            if (dgvKommunikation.RowCount > 0)
            {
                if (dgvKommunikation.SelectedRows.Count > 0)
                {
                    clsADRVerweis ADRVerweis = new clsADRVerweis();
                    ADRVerweis.InitClass(this.GL_User);
                    if (clsMessages.ADRVerweis_Delete())
                    {
                        decimal tmpID = -1;

                        ADRVerweis.ID = decimal.Parse(dgvKommunikation.SelectedRows[0].Cells["ID"].Value.ToString());
                        ADRVerweis.Delete();
                        InitDGVASN();
                        SetASNEditInputFieldEnabled(false);
                        ClearASNInputField();
                        mmPanelADRCommunication.SetExpandCollapse(AFMinMaxPanel.EStatus.Expanded);
                    }
                }
            }
        }
        ///<summary>ctrADR_List / tsbtnASNEdit_Click</summary>
        ///<remarks></remarks>
        private void tsbtnASNEdit_Click(object sender, EventArgs e)
        {
            bUpdate = true;
            SetADRVerweisToCtr();
            SetASNEditInputFieldEnabled(true);
        }
        ///<summary>ctrADR_List / SetADRVerweisToCtr</summary>
        ///<remarks></remarks>
        private void SetADRVerweisToCtr()
        {
            if (dgvKommunikation.RowCount > 0)
            {
                if (dgvKommunikation.SelectedRows.Count > 0)
                {
                    mmPanelADRCommunication.SetExpandCollapse(AFMinMaxPanel.EStatus.Collapsed);
                    ADRVerweis = new clsADRVerweis();
                    ADRVerweis.InitClass(this.GL_User);

                    decimal tmpID = -1;
                    ADRVerweis.ID = decimal.Parse(dgvKommunikation.SelectedRows[0].Cells["ID"].Value.ToString());
                    ADRVerweis.Fill();

                    tbVerweisADR.Text = ADRVerweis.ADRVerwAdr.ViewID;
                    Functions.SetComboToSelecetedValue(ref cbArbeitsbereich, ADRVerweis.ArbeitsbereichID.ToString());
                    Functions.SetComboToSelecetedItem(ref comboVerweisArt, ADRVerweis.VerweisArt);
                    tbASNVerweis.Text = ADRVerweis.Verweis;
                    tbASNLieferantenNummer.Text = ADRVerweis.LieferantenVerweis;
                    cbAktiv.Checked = ADRVerweis.aktiv;
                    //Functions.SetComboToSelecetedText(ref cbASNFileTyp, ADRVerweis.ASNFileTyp);
                    Functions.SetComboToSelecetedValue(ref cbASNFileTyp, ADRVerweis.ASNFileTyp);
                    tbADRVerweisBemerkung.Text = ADRVerweis.Bemerkung;
                }
            }
        }
        ///<summary>ctrExtraChargeAssignment / dgvExtraChargeAssignment_MouseClick</summary>
        ///<remarks></remarks>
        private void dgvKommunikation_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.dgvKommunikation.Rows.Count > 0)
            {
                this.dgvKommunikation.CurrentRow.IsSelected = true;
            }
        }
        ///<summary>ctrExtraChargeAssignment / dgvExtraChargeAssignment_MouseDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvKommunikation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bUpdate = true;
            SetADRVerweisToCtr();
            SetASNEditInputFieldEnabled(true);
        }
        ///<summary>ctrADR_List / tsbtnASNRefresh_Click</summary>
        ///<remarks></remarks>
        private void tsbtnASNRefresh_Click(object sender, EventArgs e)
        {
            InitDGVASN();
        }
        ///<summary>ctrADR_List / tsbtnASNClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnASNClose_Click(object sender, EventArgs e)
        {
            ShowAndHideADREdit();
        }
        ///<summary>ctrADR_List / toolStripButton2_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (ADR.ID > 0)
            {
                SetTabEingabeFelder();
                InitTabASNEdit();
                this.tabADR.SelectedTab = tabPageKommunikation;
                this.scADR.Panel2Collapsed = false;
                ResetCtrADRListWidth();
            }
        }
        ///<summary>ctrADR_List / tsbtnPrintADRList_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrintADRList_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrintRepViewer _frmPrintRepViewer = new frmPrintRepViewer();
                _frmPrintRepViewer._ctrADRList = this;
                _frmPrintRepViewer.iPrintCount = 1;
                _frmPrintRepViewer.DokumentenArt = enumDokumentenArt.Adressliste.ToString();
                _frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;
                _frmPrintRepViewer.InitFrm();
                //this._frmPrintRepViewer.InitReportView();
                _frmPrintRepViewer.PrintDirect();
                _frmPrintRepViewer.Dispose();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        ///<summary>ctrADR_List / tsbtnPrintKundenDaten_Click</summary>
        ///<remarks></remarks>
        private void tsbtnPrintKundenDaten_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrintRepViewer _frmPrintRepViewer = new frmPrintRepViewer();
                _frmPrintRepViewer._ctrADRList = this;
                _frmPrintRepViewer.iPrintCount = 1;
                _frmPrintRepViewer.DokumentenArt = enumDokumentenArt.KundenInformationen.ToString();
                _frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;
                _frmPrintRepViewer.InitFrm();
                //this._frmPrintRepViewer.InitReportView();
                _frmPrintRepViewer.PrintDirect();
                _frmPrintRepViewer.Dispose();

                _frmPrintRepViewer = new frmPrintRepViewer();
                _frmPrintRepViewer._ctrADRList = this;
                _frmPrintRepViewer.iPrintCount = 1;
                _frmPrintRepViewer.DokumentenArt = enumDokumentenArt.KundenInformationen.ToString();
                _frmPrintRepViewer.GL_System = this._ctrMenu._frmMain.GL_System;
                _frmPrintRepViewer.InitFrm();
                //this._frmPrintRepViewer.InitReportView();
                _frmPrintRepViewer.PrintDirect();
                _frmPrintRepViewer.Dispose();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        ///<summary>ctrADR_List / button3_Click</summary>
        ///<remarks></remarks>
        private void button3_Click(object sender, EventArgs e)
        {
            ADRSearchButton = 11;
            _ctrMenu.OpenADRSearch(this);
        }
        /*********************************************************************************************
         *                                  tabContactEdit
         * ******************************************************************************************/
        ///<summary>ctrADR_List / InitTabPageLiefGroup</summary>
        ///<remarks></remarks>
        private void InitTabPageLiefGroup()
        {
            bUpdateLiefGroupDaten = false;
            comboLiefGroupAbBereich.DataSource = dtArbeitsbereiche;
            comboLiefGroupAbBereich.ValueMember = "ID";
            comboLiefGroupAbBereich.DisplayMember = "Arbeitsbereich";

            ClearTabPageLiefGroup();
            SetTabLiefGroupEingabeFelderEnabled(false);
            InitDGVLiefGroup();
        }
        ///<summary>ctrADR_List / ClearTabPageLiefGroup</summary>
        ///<remarks></remarks>
        private void ClearTabPageLiefGroup()
        {
            this.tbLiefGroupComPartner.Text = string.Empty;
            this.tbLiefGroupLieferantennummer.Text = string.Empty;
            this.tbLiefGroupName.Text = string.Empty;
            this.cbLiefGroupActiv.Checked = false;
            this.comboLiefGroupAbBereich.SelectedIndex = -1;
        }
        ///<summary>ctrADR_List / SetTabLiefGroupEingabeFelderEnabled</summary>
        ///<remarks></remarks>
        private void SetTabLiefGroupEingabeFelderEnabled(bool bEnabled)
        {
            this.tbLiefGroupComPartner.Enabled = bEnabled;
            this.tbLiefGroupLieferantennummer.Enabled = bEnabled;
            this.tbLiefGroupName.Enabled = bEnabled;
            this.cbLiefGroupActiv.Enabled = bEnabled;
            this.comboLiefGroupAbBereich.Enabled = bEnabled;
            this.btnComPartnerSearch.Enabled = bEnabled;
        }
        ///<summary>ctrADR_List / InitDGVLiefGroup</summary>
        ///<remarks></remarks>
        private void InitDGVLiefGroup()
        {
            DataTable dt = new DataTable();
            dt = this.ADR.LiefGroup.GetLieferantenGruppen();
            this.dgvLiefGroup.DataSource = dt;
            foreach (GridViewColumn colArt in this.dgvLiefGroup.Columns)
            {
                switch (colArt.Name)
                {
                    case "Name":
                        colArt.HeaderText = "Liefantengruppe";
                        //this.dgvLiefGroup.Columns.Move(colArt.Index, 0);
                        break;
                    case "Matchcode":
                        colArt.HeaderText = "Kom. Partner";
                        //this.dgvLiefGroup.Columns.Move(colArt.Index, 1);
                        break;
                    case "Lieferantennummer":
                        colArt.IsVisible = true;
                        //this.dgvLiefGroup.Columns.Move(colArt.Index, 2);
                        break;
                    case "Arbeitsbereich":
                        colArt.IsVisible = true;
                        //this.dgvLiefGroup.Columns.Move(colArt.Index, 3);
                        break;
                    case "activ":
                        colArt.IsVisible = true;
                        //this.dgvLiefGroup.Columns.Move(colArt.Index, 4);
                        break;
                    default:
                        colArt.IsVisible = false;
                        break;
                }
            }
            this.dgvLiefGroup.BestFitColumns();

            //Lieferanten
            GridViewTemplate Lieferanten = new GridViewTemplate();
            Lieferanten.DataSource = this.ADR.LiefGroup.Lieferanten.GetLieferanten();
            dgvLiefGroup.MasterTemplate.Templates.Clear();
            dgvLiefGroup.MasterTemplate.Templates.Add(Lieferanten);
            foreach (GridViewColumn colArt in Lieferanten.Columns)
            {
                switch (colArt.Name)
                {
                    case "LiefGruppenID":
                    case "AdrIDLieferant":
                        colArt.IsVisible = false;
                        break;

                    case "ViewID":
                        colArt.HeaderText = "Matchcode";
                        colArt.Width = (Int32)(this.dgvLiefGroup.Width / 3);
                        break;

                    case "Name1":
                        colArt.HeaderText = "Lieferant";
                        colArt.Width = (Int32)(this.dgvLiefGroup.Width * 2 / 3);
                        break;

                    default:
                        colArt.IsVisible = true;
                        colArt.AutoSizeMode = BestFitColumnMode.DisplayedCells;
                        break;
                }
            }
            Lieferanten.BestFitColumns();

            //////Setzt die Verknüpfung zwischen den Templates            
            GridViewRelation relLief = new GridViewRelation(this.dgvLiefGroup.MasterTemplate);
            relLief.ChildTemplate = Lieferanten;
            relLief.RelationName = "Lieferanten";
            relLief.ParentColumnNames.Add("ID");
            relLief.ChildColumnNames.Add("LiefGruppenID");
            this.dgvLiefGroup.Relations.Clear();
            this.dgvLiefGroup.Relations.Add(relLief);

            //SEt Selected 
            for (Int32 i = 0; i <= this.dgvLiefGroup.Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                    this.dgvLiefGroup.Rows[i].IsCurrent = true;
                    this.dgvLiefGroup.Rows[i].IsSelected = true;
                }
                else
                {
                    decimal decTmp = 0;
                    decimal.TryParse(this.dgvLiefGroup.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        if (decTmp == this.ADR.LiefGroup.ID)
                        {
                            this.dgvLiefGroup.Rows[i].IsCurrent = true;
                            this.dgvLiefGroup.Rows[i].IsSelected = true;
                            i = this.dgvLiefGroup.Rows.Count;
                        }
                    }
                }
            }
        }
        ///<summary>ctrADR_List / dgvLiefGroup_ContextMenuOpening</summary>
        ///<remarks>Menü erstellen</remarks>
        private void dgvLiefGroup_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            RadMenuSeparatorItem separator;
            RadMenuItem customMenuItem;

            //Menüpunkt Add
            if (!this.ADR.LiefGroup.Lieferanten.ListLieferantenAdrID.Contains(this.ADR.ID))
            {
                separator = new RadMenuSeparatorItem();
                e.ContextMenu.Items.Add(separator);
                customMenuItem = new RadMenuItem();
                customMenuItem.Text = "Adresse zur Lieferantengruppe hinzufügen";
                customMenuItem.Click += new EventHandler(this.AddAdreToGroup);
                e.ContextMenu.Items.Add(customMenuItem);
            }
            else
            {
                separator = new RadMenuSeparatorItem();
                e.ContextMenu.Items.Add(separator);
                customMenuItem = new RadMenuItem();
                customMenuItem.Text = "Adresse aus Lieferantengruppe entfernen";
                customMenuItem.Click += new EventHandler(this.RemoveAdreFromGroup);
                e.ContextMenu.Items.Add(customMenuItem);
            }
        }
        ///<summary>ctrADR_List / AddAdreToGroup</summary>
        ///<remarks></remarks>
        private void AddAdreToGroup(object sender, EventArgs e)
        {
            if ((this.ADR.ID > 0) && (this.ADR.LiefGroup.ID > 0))
            {
                clsLieferanten tmpLief = new clsLieferanten();
                tmpLief = this.ADR.LiefGroup.Lieferanten.Copy();
                tmpLief.LiefGruppenID = this.ADR.LiefGroup.ID;
                tmpLief.AdrIDLieferant = this.ADR.ID;
                if (tmpLief.Add())
                {
                    this.ADR.LiefGroup.Lieferanten = tmpLief.Copy();
                    InitDGVLiefGroup();
                }
                else
                {
                    clsMessages.Error_ContactToSupport();
                }
            }
            else
            {
                clsMessages.Error_ContactToSupport();
            }
        }
        ///<summary>ctrADR_List / RemoveAdreFromGroup</summary>
        ///<remarks></remarks>
        private void RemoveAdreFromGroup(object sender, EventArgs e)
        {
            if ((this.ADR.ID > 0) && (this.ADR.LiefGroup.ID > 0))
            {
                clsLieferanten tmpLief = new clsLieferanten();
                tmpLief = this.ADR.LiefGroup.Lieferanten.Copy();
                tmpLief.LiefGruppenID = this.ADR.LiefGroup.ID;
                tmpLief.AdrIDLieferant = this.ADR.ID;
                if (!tmpLief.Delete())
                {
                    clsMessages.Error_ContactToSupport();
                }
                else
                {
                    this.ADR.LiefGroup.Lieferanten = tmpLief.Copy();
                    InitDGVLiefGroup();
                }
            }
            else
            {
                clsMessages.Error_ContactToSupport();
            }
        }
        ///<summary>ctrADR_List / dgvLiefGroup_CellClick</summary>
        ///<remarks></remarks>
        private void dgvLiefGroup_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                decimal decTmp = 0;
                if (e.RowIndex <= this.dgvLiefGroup.Rows.Count - 1)
                {
                    Decimal.TryParse(this.dgvLiefGroup.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.ADR.LiefGroup.ID = decTmp;
                        this.ADR.LiefGroup.Fill();
                    }
                }
            }
        }
        ///<summary>ctrADR_List / dgvLiefGroup_CellDoubleClick</summary>
        ///<remarks>Übernahme der Daten zum Editieren.</remarks>
        private void dgvLiefGroup_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (this.ADR.LiefGroup.ID > 0)
            {
                bUpdateLiefGroupDaten = true;
                SetLiefGroupValueToInputFields();
                //Buttons freigeben
                this.tsbtnLiefGroupSave.Enabled = true;
                this.tsbtnDeleteLiefGroup.Enabled = true;
            }
        }
        ///<summary>ctrADR_List / SetLiefGroupValueToInputFields</summary>
        ///<remarks></remarks>
        private void SetLiefGroupValueToInputFields()
        {
            this.tbLiefGroupName.Text = this.ADR.LiefGroup.Name;
            this.tbLiefGroupLieferantennummer.Text = this.ADR.LiefGroup.Lieferantennummer;
            Functions.SetComboToSelecetedValue(ref comboLiefGroupAbBereich, this.ADR.LiefGroup.AbBereichID.ToString());
            this.tbLiefGroupComPartner.Text = this.ADR.LiefGroup.ADRKomPartner.ADRStringShort;
            this.cbLiefGroupActiv.Checked = this.ADR.LiefGroup.IsActiv;
        }
        ///<summary>ctrADR_List / SetLiefGroupValueToInputFields</summary>
        ///<remarks></remarks>
        private void tsbtnAddLiefGroup_Click(object sender, EventArgs e)
        {
            bUpdateLiefGroupDaten = false;
            ClearTabPageLiefGroup();
            SetTabLiefGroupEingabeFelderEnabled(true);
            //Buttons freigeben
            this.tsbtnLiefGroupSave.Enabled = true;
        }
        ///<summary>ctrADR_List / tsbtnLiefGroupSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnLiefGroupSave_Click(object sender, EventArgs e)
        {
            //Eingabedaten prüfen
            SaveValue();
            bUpdateLiefGroupDaten = false;
            InitTabPageLiefGroup();
            //Buttons freigeben
            this.tsbtnLiefGroupSave.Enabled = false;
            this.tsbtnDeleteLiefGroup.Enabled = false;
        }
        ///<summary>ctrADR_List / SaveValue</summary>
        ///<remarks></remarks>
        private void SaveValue()
        {
            if (tbLiefGroupName.Text.Trim() != string.Empty)
            {
                clsLieferantenGruppe tmpLiefGroup = new clsLieferantenGruppe();
                tmpLiefGroup.InitClass(this.GL_User, this.ADR._GL_System, this.ADR.LiefGroup.Sys);

                bool bCHeckOK = true;
                if ((!bUpdateLiefGroupDaten) && (this.ADR.LiefGroup.ListUsedLiefGroupNames.Contains(tbLiefGroupName.Text.Trim())))
                {
                    bCHeckOK = false;
                }

                if (bCHeckOK)
                {
                    //OK - Speichern
                    tmpLiefGroup.Name = tbLiefGroupName.Text.Trim();
                    tmpLiefGroup.Lieferantennummer = tbLiefGroupLieferantennummer.Text.Trim();
                    tmpLiefGroup.AdrIDKomPartner = this.ADR.LiefGroup.ADRKomPartner.ID;
                    tmpLiefGroup.AbBereichID = (decimal)this.comboLiefGroupAbBereich.SelectedValue;
                    tmpLiefGroup.IsActiv = this.cbLiefGroupActiv.Checked;
                    if (bUpdateLiefGroupDaten)
                    {
                        tmpLiefGroup.ID = this.ADR.LiefGroup.ID;
                        if (tmpLiefGroup.Update())
                        {
                            this.ADR.LiefGroup = tmpLiefGroup.Copy();
                        }
                    }
                    else
                    {
                        tmpLiefGroup.ID = 0;
                        if (tmpLiefGroup.Add())
                        {
                            this.ADR.LiefGroup = tmpLiefGroup.Copy();
                        }
                    }
                }
            }

        }
        ///<summary>ctrADR_List / btnComPartnerSearch_Click</summary>
        ///<remarks></remarks>
        private void btnComPartnerSearch_Click(object sender, EventArgs e)
        {
            ADRSearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrADR_List / btnComPartnerSearch_Click</summary>
        ///<remarks></remarks>
        private void dgvLiefGroup_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo.Cells["ID"] != null)
            {
                decimal decLiefGroupID = 0;
                decimal.TryParse(e.RowElement.RowInfo.Cells["ID"].Value.ToString(), out decLiefGroupID);
                if (decLiefGroupID > 0)
                {
                    clsLieferantenGruppe tmpGroup = new clsLieferantenGruppe();

                    if (this.ADR.LiefGroup.Lieferanten.DictGroupLieferanten.ContainsKey(decLiefGroupID))
                    {
                        List<decimal> listTmpLieferant;
                        this.ADR.LiefGroup.Lieferanten.DictGroupLieferanten.TryGetValue(decLiefGroupID, out listTmpLieferant);
                        if (listTmpLieferant != null)
                        {
                            //Check ob AdrID vorhanden ist
                            if (listTmpLieferant.Contains(this.ADR.ID))
                            {
                                e.RowElement.DrawFill = true;
                                e.RowElement.GradientStyle = GradientStyles.Solid;
                                e.RowElement.BackColor = Color.GreenYellow;
                            }
                            else
                            {
                                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                            }
                        }
                    }
                    else
                    {
                        e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    }
                }
            }
        }
        ///<summary>ctrADR_List / btnPostADR_Click</summary>
        ///<remarks></remarks>
        private void btnPostADR_Click(object sender, EventArgs e)
        {
            ADRSearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
        }

        /**********************************************************************************************************************
        *                              Tab Page ExtraCharge
        * *******************************************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        private void InitTabDefaultGueterartEdit()
        {
            GueterArtDefault = new clsKundGArtDefault();
            tbDefaultGArtAdr.Text = ADR.ADRStringShort;
            SetDefaultGueterartenEditInputFieldEnabled(false);
            ClearDefaultGueterartInputFields();

            //--- combo Arbeitsbereich füllen 
            this.comboDefaultGArtArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(decBenutzerID: _ctrMenu._frmMain.GL_User.User_ID); ;
            this.comboDefaultGArtArbeitsbereich.ValueMember = "ID";
            this.comboDefaultGArtArbeitsbereich.DisplayMember = "Arbeitsbereich";

            //DGV laden
            InitDGVDefaultGueterart();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnabled"></param>
        private void SetDefaultGueterartenEditInputFieldEnabled(bool bEnabled)
        {
            btnGut.Enabled = true; // bEnabled;
            tbDefaultGArtGut.Enabled = bEnabled;
            comboDefaultGArtArbeitsbereich.Enabled = bEnabled;
        }
        /// <summary>
        /// 
        /// </summary>
        private void ClearDefaultGueterartInputFields()
        {
            lId.Text = "0";
            tbDefaultGArtGut.Text = string.Empty;
            comboDefaultGArtArbeitsbereich.SelectedIndex = -1;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitDGVDefaultGueterart()
        {
            if (this.ADR.ID > 0)
            {
                GueterArtDefault = new clsKundGArtDefault();
                GueterArtDefault.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this.ADR.ID);

                this.dgvDefaultGueterarten.DataSource = GueterArtDefault.dtDefaultGueterarten;
                //Spalten ausschalten
                if (this.dgvDefaultGueterarten.Rows.Count > 0)
                {
                    //for (Int32 i = 0; i <= this.dgvDefaultGueterarten.Columns.Count - 1; i++)
                    //{
                    //    string colName = this.dgvDefaultGueterarten.Columns[i].Name.ToString();
                    //    switch (colName)
                    //    {
                    //        case "ID":
                    //        case "erstellt":
                    //            this.dgvDefaultGueterarten.Columns[i].IsVisible = false;
                    //            break;
                    //        case "Preis":
                    //            this.dgvDefaultGueterarten.Columns[i].FormatString = "{0:#,##0.00}";
                    //            break;
                    //    }
                    //    this.dgvDefaultGueterarten.Columns[i].AutoSizeMode = Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells;
                    //}
                    this.dgvDefaultGueterarten.BestFitColumns();

                    //SetSelected and Current Row
                    //for (Int32 i = 0; i <= this.dgvDefaultGueterarten.Rows.Count - 1; i++)
                    //{
                    //    decimal decTmp = 0;
                    //    Decimal.TryParse(this.dgvDefaultGueterarten.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                    //    if (decTmp == ExtraCharge.ID)
                    //    {
                    //        this.dgvExtraCharge.Rows[i].IsSelected = true;
                    //        this.dgvExtraCharge.Rows[i].IsCurrent = true;
                    //    }
                    //}
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGut_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenFrmGArtenList(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDefaultGueterarten_MouseClick(object sender, MouseEventArgs e)
        {
            string str = string.Empty;
            if (this.dgvDefaultGueterarten.Rows.Count > 0)
            {
                decimal decTmp = 0;
                string strTmp = this.dgvDefaultGueterarten.Rows[this.dgvDefaultGueterarten.CurrentRow.Index].Cells["ID"].Value.ToString();
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    GueterArtDefault.ID = (int)decTmp;
                    GueterArtDefault.FillById();
                    this.dgvDefaultGueterarten.CurrentRow.IsSelected = true;
                }
                if (this.GueterArtDefault.ID > 0)
                {
                    ClearDefaultGueterartInputFields();
                    SetDefaultGueterartenEditInputFieldEnabled(true);
                    SetDefaultGueterartToInputFields();
                }
            }
        }
        private void dgvDefaultGueterarten_CellClick(object sender, GridViewCellEventArgs e)
        {
            string str = string.Empty;
        }

        private void dgvDefaultGueterarten_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            string str = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetDefaultGueterartToInputFields()
        {
            lId.Text = this.GueterArtDefault.ID.ToString();
            tbDefaultGArtAdr.Text = this.ADR.ADRStringShort;
            tbDefaultGArtGut.Text = this.GueterArtDefault.Gut.Bezeichnung;
            comboDefaultGArtArbeitsbereich.SelectedIndex = -1;
            Functions.SetComboToSelecetedValue(ref comboDefaultGArtArbeitsbereich, this.GueterArtDefault.AbBereichID.ToString());
        }
        /// <summary>
        ///             Neue Standardgüterart hinzufügen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAddDefaulGueterart_Click(object sender, EventArgs e)
        {
            SetDefaultGueterartenEditInputFieldEnabled(true);
            ClearDefaultGueterartInputFields();
            this.GueterArtDefault = new clsKundGArtDefault();
            this.GueterArtDefault.AdrID = this.ADR.ID;
        }
        /// <summary>
        ///             DAten speichern 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveDefaultGueterart_Click(object sender, EventArgs e)
        {
            this.GueterArtDefault.GArtID = this.GueterArtDefault.Gut.ID;
            decimal decTmp = 0;
            decimal.TryParse(comboDefaultGArtArbeitsbereich.SelectedValue.ToString(), out decTmp);
            this.GueterArtDefault.AbBereichID = decTmp;
            if (decTmp > 0)
            {
                if (this.GueterArtDefault.ID > 0)
                {
                    this.GueterArtDefault.Update();
                    SetDefaultGueterartenEditInputFieldEnabled(false);
                    ClearDefaultGueterartInputFields();
                    InitDGVDefaultGueterart();
                }
                else
                {
                    if (!this.GueterArtDefault.ExistDefaultGueterartForSeletektedWorkspace)
                    {
                        this.GueterArtDefault.Add();
                        SetDefaultGueterartenEditInputFieldEnabled(false);
                        ClearDefaultGueterartInputFields();
                        InitDGVDefaultGueterart();
                    }
                    else
                    {
                        string strError = "Für den ausgewählten Arbeitsbereich existiert bereits eine Standard-Güterart!!!";
                        clsMessages.Allgemein_ERRORTextShow(strError);
                    }
                }
            }
            else
            {
                string strError = "Arbeitsbereich-Id = 0, kein Arbeitsbereich ausgewählt!";
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TakeOver_ID"></param>
        public void TakeOverGueterArt(decimal TakeOver_ID)
        {
            if (this.GueterArtDefault is clsKundGArtDefault)
            {
                this.GueterArtDefault.Gut = new clsGut();
                this.GueterArtDefault.Gut.ID = TakeOver_ID;
                this.GueterArtDefault.Gut.Fill();
                this.GueterArtDefault.GArtID = this.GueterArtDefault.Gut.ID;
                SetDefaultGueterartToInputFields();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeleteDefaultGueterart_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                if (this.GueterArtDefault is clsKundGArtDefault)
                {
                    this.GueterArtDefault.Delete();
                    SetDefaultGueterartenEditInputFieldEnabled(false);
                    ClearDefaultGueterartInputFields();
                    InitDGVDefaultGueterart();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbADRText_UseForAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbADRText_UseForAll.Checked)
            {
                this.comboADRTextArbeitsbereich.SelectedIndex = -1;
            }
            else
            {
                if (this.ADR.ADRTexte.ArbeitsbereichID > 0)
                {
                    Functions.SetComboToSelecetedValue(ref comboADRTextArbeitsbereich, this.ADR.ADRTexte.ArbeitsbereichID.ToString());
                }
                else
                {
                    this.comboADRTextArbeitsbereich.SelectedIndex = 0;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboADRTextArbeitsbereich_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboADRTextArbeitsbereich.SelectedIndex > -1)
            {
                this.cbADRText_UseForAll.Checked = false;
            }
        }
    }
}
