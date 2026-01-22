using LVS;
using Sped4.Classes;
using Sped4.Settings;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace Sped4
{
    public partial class frmAuftrag_Fast : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public Globals._GL_SYSTEM GL_System;
        internal ctrMenu _ctrMenu;
        internal ctrADRManAdd _ctrADRManAdd;
        internal string _lTextGArtID = "Güterart ID: #";
        internal clsAuftrag Auftrag;
        public DataTable KDTable = new DataTable();
        public DataTable VTable = new DataTable();
        public DataTable ETable = new DataTable();
        public DataTable gaTable = new DataTable();

        public Int32 Status = 2;        //vollständig
        internal Int32 iSelRow = 0;
        private decimal Auftraggeber_ADR_ID = 0;
        private decimal Versender_ADR_ID = 0;
        private decimal Empfaenger_ADR_ID = 0;
        private decimal Gewicht = 0.00m;
        public bool bUpdateOrder = false;
        public bool Abbruch = false;
        internal bool bUpdateArtikel = false;
        public bool bo_Update = false;
        public bool bo_AdrTakeOverForDistanceCenter = false;
        internal string manAdrArt = string.Empty;


        public ctrAufträge _ctrAuftrag = new ctrAufträge();
        public delegate void ThreadCtrInvokeEventHandler();
        //SearchButton
        // 1 = KD
        // 2 = Versender
        // 3 = Empfänger
        public Int32 SearchButton = 0;

        /***************************************************************************
         *                      Methoden
         * ************************************************************************/
        ///<summary>frmAuftrag_Fast / frmAuftrag_Fast</summary>
        ///<remarks></param>
        public frmAuftrag_Fast(ctrAufträge ctrAuftrag, bool _Save)
        {
            InitializeComponent();
            _ctrAuftrag = ctrAuftrag;
            this.tsbtnAuftragClose.Visible = false;
            //Tag für die Button setzen
            this.btnManVersender.Tag = 1;
            this.btnManEmpfaenger.Tag = 3;
            //this.btnManEntladestelle.Tag = 4;
            //this.btnManSped.Tag = 5;
        }
        ///<summary>frmAuftrag_Fast / frmAuftrag_Fast_Load</summary>
        ///<remarks></param>
        private void frmAuftrag_Fast_Load(object sender, EventArgs e)
        {
            this.GL_User = _ctrAuftrag._ctrMenu._frmMain.GL_User;
            this.GL_System = _ctrAuftrag._ctrMenu._frmMain.GL_System;

            //Setzen der Textboxen im Menü
            tbOrderID.Text = "0";
            tbOrderPos.Text = "0";
            tbOrderDate.Text = DateTime.Now.ToString();
            //Initialisierung der Klasse Auftrag
            Auftrag = new clsAuftrag();
            Auftrag.InitClass(this.GL_User, this.GL_System, this._ctrMenu._frmMain.system);

            ClearAuftragDaten();
            InitCBRelation();
            LoadLastAuftragsdaten();
            //EnableMSItemTakeOver(false);
        }
        ///<summary>frmAuftrag_Fast / GetLastAuftragsnummer</summary>
        ///<remarks>Ermittelt die letzte gespeicherte Auftragsnummer des Mandanten.</remarks>
        ///<param name="strMC">strMC >>> Matchcode</param>
        ///<param name="decBenutzerID">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        private void LoadLastAuftragsdaten()
        {
            initGArt();
            Auftrag.GetLastOrder();
            bo_Update = true;
            bUpdateOrder = false;
            SetAuftragDatenToFrm();
            InitDGV();
            SetAccessToOrder(false, false);
        }
        ///<summary>frmAuftrag_Fast / SetBackVorInMenuEnable</summary>
        ///<remarks></remarks>
        private void SetBackNextInMenuEnable(bool bEnable)
        {
            tsbtnBack.Enabled = bEnable;
            tsbtnVor.Enabled = bEnable;
            tsbtnFirstOrder.Enabled = bEnable;
            tsbtnLastOrder.Enabled = bEnable;
        }
        ///<summary>frmAuftrag_Fast / SetAuftragEingabefelderEnabled</summary>
        ///<remarks>Wenn noch kein alter Auftrag geladen werden kann, (noch keine Aufträge erfasst) 
        ///         dann müssen die Eingabefelder auf der Form deaktiviert werden</remarks>
        private void SetAuftragEingabefelderEnabled(bool bEnable)
        {
            tsbtnAuftragSpeichern.Enabled = bEnable;
            //Adressen
            btnSearchA.Enabled = bEnable;
            btnSearchE.Enabled = bEnable;
            btnSearchV.Enabled = bEnable;
            btnManVersender.Enabled = bEnable;
            btnManEmpfaenger.Enabled = bEnable;

            tbSearchA.Enabled = bEnable;
            tbSearchE.Enabled = bEnable;
            tbSearchV.Enabled = bEnable;
            cbPrio.Enabled = bEnable;
            cbLadNrRequire.Enabled = bEnable;
            nudFracht.Enabled = bEnable;
            tbLadeNr.Enabled = bEnable;
            tbNotiz.Enabled = bEnable;

            gbTermine.Enabled = bEnable;
        }
        ///<summary>frmAuftrag_Fast / SetArtikelMenuEnabled</summary>
        ///<remarks></remarks>
        private void SetArtikelMenuEnabled(bool bEnabled)
        {
            this.tsbtnArtikelNew.Enabled = bEnabled;
            this.tsbtnArtikelSave.Enabled = bEnabled;
            if (bEnabled)
            {
                //wenn noch keine Artikel vorhanden sind, dann nur Button neue Artikel
                bool bArtikelVorhanden = (this.dgvArtikel.Rows.Count > 0);
                this.tsbtnArtikelDelete.Enabled = bArtikelVorhanden;
                this.tsbtnArtikelCopy.Enabled = bArtikelVorhanden;
            }
            else
            {
                this.tsbtnArtikelDelete.Enabled = bEnabled;
                this.tsbtnArtikelCopy.Enabled = bEnabled;
            }
        }
        ///<summary>frmAuftrag_Fast / ClearAuftragDaten</summary>
        ///<remarks></remarks>
        private void ClearAuftragDaten()
        {
            tbSearchA.Text = string.Empty;
            tbAuftraggeber.Text = string.Empty;
            tbSearchE.Text = string.Empty;
            tbEmpfaenger.Text = string.Empty;
            tbSearchV.Text = string.Empty;
            tbVersender.Text = string.Empty;
            nudFracht.Text = string.Empty;
            tbLadeNr.Text = string.Empty;
            tbNotiz.Text = string.Empty;
            cbRelation.SelectedIndex = -1;
            cbPrio.Checked = false;
            cbLadNrRequire.Checked = false;

            //VSB
            cbVSB.Checked = false;

            //Ladetermin
            cbLadeTerminIsSet.Checked = false;
            cbLadeZFNessesary.Checked = false;
            cbLadeZFIsSet.Enabled = cbLadeZFNessesary.Checked;
            nudLadeZFHour.Enabled = cbLadeZFIsSet.Checked;
            nudLadeZFMin.Enabled = cbLadeZFIsSet.Checked;
            nudLadeZFHour.Value = 0;
            nudLadeZFMin.Value = 0;

            //Liefertermin
            cbLieferTerminIsSet.Checked = false;
            cbLieferZFNessesary.Checked = false;
            cbLieferZFIsSet.Enabled = cbLieferZFNessesary.Checked;
            nudLieferZFHour.Enabled = cbLieferZFIsSet.Checked;
            nudLieferZFMin.Enabled = cbLieferZFIsSet.Checked;
            nudLieferZFHour.Value = 0;
            nudLieferZFMin.Value = 0;

            //dtpLieferTermin.Value = DateTime.Now;
            //dtpVSB.Value = DateTime.Now;
            tbEntfernung.Text = "0";
            SetCtrByTermineEnabled();
        }
        ///<summary>ctrEinlagerung / SetLabelGArdIDInfo</summary>
        ///<remarks>Set den GüterartID Info.</remarks>  
        private void SetLabelGArdIDInfo()
        {
            lGArtID.Text = _lTextGArtID + Auftrag.AuftragPos.Artikel.GArtID.ToString();
        }
        ///<summary>frmAuftrag_Fast / tbGArtSearch_TextChanged</summary>
        ///<remarks></remarks>
        private void tbGArtSearch_TextChanged(object sender, EventArgs e)
        {
            //Güterarten laden
            DataTable dt = new DataTable();
            dt = clsGut.GetGArtenForCombo(this.GL_User.User_ID);
            string Filter = tbGArtSearch.Text.Trim();
            DataTable dtTmp = new DataTable();
            decimal decTmp = 0;
            if (Filter != string.Empty)
            {
                dt.DefaultView.RowFilter = "ViewID ='" + Filter + "'";
                dtTmp = dt.DefaultView.ToTable();

                if (dtTmp.Rows.Count > 0)
                {
                    tbGArtSearch.Text = dtTmp.Rows[0]["ViewID"].ToString();
                    tbGArt.Text = dtTmp.Rows[0]["Bezeichnung"].ToString();

                    Decimal.TryParse(dtTmp.Rows[0]["ID"].ToString(), out decTmp);
                    //---testMR
                    //Auftrag.AuftragPos.Artikel.GArt.ID = decTmp;
                    //Auftrag.AuftragPos.Artikel.GArt.InitClass(this.GL_User, this.GL_System);
                    Auftrag.AuftragPos.Artikel.GArtID = decTmp;
                }
                else
                {
                    tbGArt.Text = string.Empty;
                    decTmp = 0;
                    Auftrag.AuftragPos.Artikel.GArtID = decTmp;
                }
            }
            else
            {
                tbGArt.Text = string.Empty;
                decTmp = 0;
                Auftrag.AuftragPos.Artikel.GArtID = decTmp;
            }
            SetLabelGArdIDInfo();
        }
        ///<summary>frmAuftrag_Fast / btnNewOrder_Click</summary>
        ///<remarks>neuen Auftrag erfassen </remarks>
        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            iSelRow = 0;

            bo_Update = false;
            bUpdateOrder = false;
            tsbtnAuftragSpeichern.Visible = true;
            tsbtnBack.Visible = true;
            tsbtnVor.Visible = true;
            //EnableMSItemTakeOver(true);

            Auftrag = new clsAuftrag();
            Auftrag.InitClass(this.GL_User, this.GL_System, this._ctrMenu._frmMain.system);

            ClearAuftragDaten();
            ClearArtikelEingabefelder();

            InitDGV();
            initEmpfaenger();
            initGArt();
            initKD();
            initVersender();
            tbOrderDate.Text = DateTime.Now.ToString();  /// (DateTime.Now).ToString("dd.MM.yyyy");   // Auftragsdatum = aktuelle Datum

            //Ermitteln der der neuen Auftragsnummer
            LVS.clsPrimeKeys PKey = new LVS.clsPrimeKeys();
            PKey.InitClass(this.GL_User, this.GL_System);
            PKey.AbBereichID = this._ctrMenu._frmMain.system.AbBereich.ID;
            PKey.GetNEWAuftragsNr();
            tbOrderID.Text = string.Empty;
            tbOrderID.Text = PKey.AuftragsNr.ToString();

            //berechnung der Kalenderwochen
            Functions.SetKWValue(ref nudVonKW, DateTime.Now);
            tbSearchA.Focus();

            SetAccessToOrder(true, false);
            tsbtnAuftragClose.Visible = true;
            tsbtnAuftragClose.Enabled = false;
            tsbtnAuftragEdit.Visible = false;
        }
        ///<summary>frmAuftrag_Fast / TakeOverGueterArt</summary>
        ///<remarks>Übernahme der gewählten Güterart</remarks>
        public void TakeOverGueterArt(decimal myGartID)
        {
            //---testMR
            //Auftrag.AuftragPos.Artikel.GArt.ID = myGartID;
            //Auftrag.AuftragPos.Artikel.GArt.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);
            //Auftrag.AuftragPos.Artikel.GArt.Fill();

            Auftrag.AuftragPos.Artikel.GArtID = myGartID;
            SetGArtDatenFromSelectedGut();
        }
        ///<summary>frmAuftrag_Fast / SetGArtDatenFromSelectedGut</summary>
        ///<remarks>Aritkel schliessen.</remarks>  
        private void SetGArtDatenFromSelectedGut()
        {
            tbGArtSearch.Text = Auftrag.AuftragPos.Artikel.GArt.ViewID;
            tbGArt.Text = Auftrag.AuftragPos.Artikel.GArt.Bezeichnung;
            tbGArtZusatz.Text = Auftrag.AuftragPos.Artikel.GArt.Zusatz;

            if (this._ctrMenu._frmMain.system.Client.Modul.Stammdaten_Gut_UseGutDefinition)
            {
                if (clsMessages.Artikel_GetAllGArtenData())
                {
                    nudDicke.Value = Auftrag.AuftragPos.Artikel.GArt.Dicke;
                    nudBreite.Value = Auftrag.AuftragPos.Artikel.GArt.Breite;
                    nudLaenge.Value = Auftrag.AuftragPos.Artikel.GArt.Laenge;
                    nudHoehe.Value = Auftrag.AuftragPos.Artikel.GArt.Hoehe;
                    nudGemGewicht.Value = Auftrag.AuftragPos.Artikel.gemGewicht;
                    nudNetto.Value = Auftrag.AuftragPos.Artikel.GArt.Netto;
                    nudBrutto.Value = Auftrag.AuftragPos.Artikel.GArt.Brutto;
                }
            }
        }
        ///<summary>frmAuftrag_Fast / btnGArt_Click</summary>
        ///<remarks> </remarks>
        private void btnGArt_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenFrmGArtenList(this);
        }
        ///<summary>frmAuftrag_Fast / InitDGV</summary>
        ///<remarks> </remarks>
        public void InitDGV()
        {
            this.dgvArtikel.DataSource = Auftrag.AuftragPos.dtAuftrPosArtikel;
            //festlegen der Spaltenreihenfolge
            for (Int32 i = 0; i <= this.dgvArtikel.Columns.Count - 1; i++)
            {
                string strCol = this.dgvArtikel.Columns[i].Name.ToString();
                switch (strCol)
                {
                    case "Gut":
                    case "Gut Zusatz":
                    case "Werksnummer":
                        this.dgvArtikel.Columns[i].IsVisible = true;
                        break;

                    case "Netto":
                    case "Brutto":
                    case "Dicke":
                    case "Breite":
                    case "Laenge":
                    case "Hoehe":
                    case "gemGewicht":
                        this.dgvArtikel.Columns[i].IsVisible = true;
                        this.dgvArtikel.Columns[i].FormatString = "{0:n}"; ;
                        break;
                    default:
                        this.dgvArtikel.Columns[i].IsVisible = false;
                        break;
                }
            }
            this.dgvArtikel.BestFitColumns();
            decimal decTmp = 0;
            ClearArtikelEingabefelder();
            if (this.dgvArtikel.Rows.Count > 0)
            {
                if (this.dgvArtikel.Rows.Count >= iSelRow)
                {
                    iSelRow = 0;
                }
                this.dgvArtikel.Rows[iSelRow].IsCurrent = true;
                this.dgvArtikel.Rows[iSelRow].IsSelected = true;

                decimal.TryParse(dgvArtikel.Rows[iSelRow].Cells["ID"].Value.ToString(), out decTmp);
                this.Auftrag.AuftragPos.Artikel.ID = decTmp;
                this.Auftrag.AuftragPos.Artikel.GetArtikeldatenByTableID();
                SetArtikelDatenToFrm(false);
            }
            SetOrderTatalWeight();
        }
        ///<summary>frmAuftrag_Fast / SetOrderTatalWeight</summary>
        ///<remarks>Ermittel das Gesamtgewicht des Auftrags</remarks>
        private void SetOrderTatalWeight()
        {
            decimal decTmp = 0;
            object objSum = new object();
            objSum = Auftrag.AuftragPos.dtAuftrPosArtikel.Compute("SUM(gemGewicht)", "");
            Decimal.TryParse(objSum.ToString(), out decTmp);
            this.tstbKumWeight.Text = Functions.FormatDecimal(decTmp);
        }
        ///<summary>frmAuftrag_Fast / toolStripButton1_Click</summary>
        ///<remarks> </remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ctrDistance Distance = new ctrDistance();
            this._ctrAuftrag._ctrMenu.OpenFrmTMP(Distance);
        }
        ///<summary>frmAuftrag_Fast / InitDGV</summary>
        ///<remarks> </remarks>
        private void nudGemGewicht_ValueChanged(object sender, EventArgs e)
        {
            nudNetto.Value = nudGemGewicht.Value;
            nudBrutto.Value = nudGemGewicht.Value;
        }
        ///<summary>frmAuftrag_Fast / InitDGV</summary>
        ///<remarks> </remarks>
        private void tsbtnSaveArtikel_Click(object sender, EventArgs e)
        {
            if (CheckArtikeldaten())
            {
                AssignValueArtikel();
                ClearArtikelEingabefelder();
                SetAccessToOrder(true, true);
                SetArtikelEingabeFelderEnabled(false);
            }
        }
        ///<summary>frmAuftrag_Fast / AssignValueArtikel</summary>
        ///<remarks></remarks>
        private void AssignValueArtikel()
        {
            clsArtikel tmpArt = Auftrag.AuftragPos.Artikel;
            Auftrag.AuftragPos.Artikel = new clsArtikel();
            Auftrag.AuftragPos.Artikel.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System);

            Auftrag.AuftragPos.Artikel.AuftragID = this.Auftrag.ANr;
            Auftrag.AuftragPos.Artikel.AuftragPos = this.Auftrag.AuftragPos.AuftragPos;
            Auftrag.AuftragPos.Artikel.AuftragPosTableID = this.Auftrag.AuftragPos.ID;
            Auftrag.AuftragPos.Artikel.BKZ = 0;
            Auftrag.AuftragPos.Artikel.LVS_ID = 0;
            Auftrag.AuftragPos.Artikel.IsLagerArtikel = false;
            Auftrag.AuftragPos.Artikel.MandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
            Auftrag.AuftragPos.Artikel.AbBereichID = this._ctrMenu._frmMain.GL_System.sys_ArbeitsbereichID;

            Auftrag.AuftragPos.Artikel.GArtID = tmpArt.GArt.ID;
            Auftrag.AuftragPos.Artikel.GutZusatz = tbGArtZusatz.Text.Trim();
            Auftrag.AuftragPos.Artikel.Werksnummer = tbWerksnummer.Text.Trim();
            Auftrag.AuftragPos.Artikel.Anzahl = (Int32)nudAnzahl.Value;
            Auftrag.AuftragPos.Artikel.Dicke = nudDicke.Value;
            Auftrag.AuftragPos.Artikel.Breite = nudBreite.Value;
            Auftrag.AuftragPos.Artikel.Laenge = nudLaenge.Value;
            Auftrag.AuftragPos.Artikel.Hoehe = nudHoehe.Value;
            Auftrag.AuftragPos.Artikel.gemGewicht = nudGemGewicht.Value;
            Auftrag.AuftragPos.Artikel.Netto = nudNetto.Value;
            Auftrag.AuftragPos.Artikel.Brutto = nudBrutto.Value;

            if (bUpdateArtikel)
            {
                Auftrag.AuftragPos.Artikel.ID = tmpArt.ID;
                Auftrag.AuftragPos.Artikel.DoUpdateArtikel();
            }
            else
            {
                Auftrag.AuftragPos.Artikel.ID = 0;
                Auftrag.AuftragPos.Artikel.Add();
            }
            Auftrag.AuftragPos.Fill();
            InitDGV();
        }
        ///<summary>frmAuftrag_Fast / tsbSpeichern_Click</summary>
        ///<remarks>Speichern des Auftrags</remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            SaveOrder();
            bUpdateOrder = true;
            tsbtnAuftragClose.Enabled = true;
            SetAccessToOrder(true, true);
            //EnableMSItemTakeOver(false);
        }
        ///<summary>frmAuftrag_Fast / SaveOrder</summary>
        ///<remarks></remarks>
        private void SaveOrder()
        {
            bool auth = GL_User.write_Order;
            //Berechtigung OK
            if (auth)
            {
                if (CheckInput())
                {
                    tbLadeNr.Text = tbLadeNr.Text.ToString().Trim();
                    tbNotiz.Text = tbNotiz.Text.ToString().Trim();

                    if (!Abbruch)
                    {
                        AssignValueAuftrag();
                        AssignValueAuftragPos();
                    }
                    else
                    {
                        Abbruch = false;
                    }
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }
            InitDGV();
        }
        ///<summary>frmAuftrag_Fast / dgvArtikel_CellClick</summary>
        ///<remarks></remarks>
        private void dgvArtikel_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvArtikel.Rows.Count > 0)
                {
                    if (this.dgvArtikel.CurrentRow != null)
                    {
                        iSelRow = e.RowIndex;
                        decimal decTmp = 0;
                        decimal.TryParse(this.dgvArtikel.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            //alle Eingabefelder leeren
                            ClearArtikelEingabefelder();
                            Auftrag.AuftragPos.Artikel.ID = decTmp;
                            Auftrag.AuftragPos.Artikel.GetArtikeldatenByTableID();
                            SetArtikelDatenToFrm(false);
                            SetArtikelEingabeFelderEnabled(false);
                        }
                    }
                }
            }
        }
        ///<summary>frmAuftrag_Fast / SetArtikelDatenToFrm</summary>
        ///<remarks></remarks>
        private void dgvArtikel_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgvArtikel.Rows.Count > 0)
                {
                    bUpdateArtikel = true;
                    SetArtikelEingabeFelderEnabled(true);
                }
            }
        }
        ///<summary>frmAuftrag_Fast / SetArtikelDatenToFrm</summary>
        ///<remarks></remarks>
        private void SetArtikelDatenToFrm(bool bCopy)
        {
            tbArtikelID.Text = string.Empty;
            if (!bCopy)
            {
                tbArtikelID.Text = Auftrag.AuftragPos.Artikel.ID.ToString();
            }
            else
            {
                //neuer Artikel deshalb 0, damit er als neuer Artikel gespeichert wird
                Auftrag.AuftragPos.Artikel.ID = 0;
            }
            tbWerksnummer.Text = Auftrag.AuftragPos.Artikel.Werksnummer;
            tbGArt.Text = Auftrag.AuftragPos.Artikel.GArt.Bezeichnung;
            tbGArtSearch.Text = Auftrag.AuftragPos.Artikel.GArt.ViewID;
            tbGArtZusatz.Text = Auftrag.AuftragPos.Artikel.GutZusatz;
            decimal decTmp = 0;
            Decimal.TryParse(Auftrag.AuftragPos.Artikel.Anzahl.ToString(), out decTmp);
            nudAnzahl.Value = decTmp;
            nudDicke.Value = Auftrag.AuftragPos.Artikel.Dicke;
            nudBreite.Value = Auftrag.AuftragPos.Artikel.Breite;
            nudLaenge.Value = Auftrag.AuftragPos.Artikel.Laenge;
            nudHoehe.Value = Auftrag.AuftragPos.Artikel.Hoehe;
            nudGemGewicht.Value = Auftrag.AuftragPos.Artikel.gemGewicht;
            nudNetto.Value = Auftrag.AuftragPos.Artikel.Netto;
            nudBrutto.Value = Auftrag.AuftragPos.Artikel.Brutto;
        }
        ///<summary>frmAuftrag_Fast / InitCBRelation</summary>
        ///<remarks></remarks>
        private void InitCBRelation()
        {
            DataTable dtRalationen = clsRelationen.GetRelationsliste();
            frmAuftragFastSettings.FillcbRelation(ref cbRelation, ref dtRalationen);
        }
        ///<summary>frmAuftrag_Fast / initGArt</summary>
        ///<remarks></remarks>
        private void initGArt()
        {
            gaTable.Clear();
            gaTable = clsGut.GetGueterarten(this.GL_User, this._ctrMenu._frmMain.system.AbBereich.ID);
        }
        ///<summary>frmAuftrag_Fast / CheckArtikeldaten</summary>
        ///<remarks></remarks>
        private bool CheckArtikeldaten()
        {
            string strError = string.Empty;
            bool bOK = true;
            //Güterart
            if (this.Auftrag.AuftragPos.Artikel.GArtID < 1)
            {
                strError = strError + "- Es wurde für diesen Artikel keine Güterart ausgewählt" + Environment.NewLine;
            }
            //Netto >0
            if (this.nudNetto.Value < 1)
            {
                strError = strError + "- Das Nettogewicht muss > 0 kg sein" + Environment.NewLine;
            }
            //Netto / Brutto
            if (this.nudNetto.Value > this.nudBrutto.Value)
            {
                strError = strError + "- Das Nettogewicht ist größer als das Bruttogewicht" + Environment.NewLine;
            }
            //Brutto >0
            if (this.nudBrutto.Value < 1)
            {
                strError = strError + "- Das Bruttogewicht muss > 0 kg sein" + Environment.NewLine;
            }

            if (!strError.Equals(string.Empty))
            {
                strError = "Folgende Fehler sind aufgetreten: " + Environment.NewLine + strError;
                clsMessages.Allgemein_ERRORTextShow(strError);
                bOK = false;
            }
            return bOK;
        }
        ///<summary>frmAuftrag_Fast / SetAuftragDatenToFrm</summary>
        ///<remarks></remarks>
        public void SetAuftragDatenToFrm()
        {
            //Save = false;
            Status = 2; // vollständig

            tbOrderID.Text = Auftrag.AuftragPos.Auftrag_ID.ToString();
            tbOrderPos.Text = Auftrag.AuftragPos.AuftragPos.ToString();
            tbOrderDate.Text = Auftrag.ADate.ToString();

            //Auftraggeber
            Auftraggeber_ADR_ID = Auftrag.KD_ID;
            tbAuftraggeber.Text = clsADR.GetADRString(Auftraggeber_ADR_ID);
            tbSearchA.Text = clsADR.GetMatchCodeByID(Auftraggeber_ADR_ID, GL_User.User_ID);

            //Versender
            if (Auftrag.B_ID > 0)
            {
                Versender_ADR_ID = Auftrag.B_ID;
                tbVersender.Text = clsADR.GetADRString(Versender_ADR_ID);
                tbSearchV.Text = clsADR.GetMatchCodeByID(Versender_ADR_ID, GL_User.User_ID);
            }
            else
            {
                tbVersender.Text = string.Empty;
                tbSearchV.Text = string.Empty;
                try
                {
                    clsADRMan tmpADRMan = new clsADRMan();
                    tmpADRMan.InitClass(this.GL_User, this.Auftrag.ID, clsAuftrag.const_DBTableName);
                    tmpADRMan.DictManuellADRAuftrag.TryGetValue(clsADRMan.cont_AdrArtID_Versender, out tmpADRMan);
                    if (tmpADRMan != null)
                    {
                        this.Auftrag.AdrManuell = tmpADRMan;
                        tbVersender.Text = this.Auftrag.AdrManuell.AdrString;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //Empfänger
            if (Auftrag.E_ID > 0)
            {
                Empfaenger_ADR_ID = Auftrag.E_ID;
                tbEmpfaenger.Text = clsADR.GetADRString(Empfaenger_ADR_ID);
                tbSearchE.Text = clsADR.GetMatchCodeByID(Empfaenger_ADR_ID, GL_User.User_ID);
            }
            else
            {
                tbEmpfaenger.Text = string.Empty;
                tbSearchE.Text = string.Empty;
                try
                {
                    clsADRMan tmpADRMan = new clsADRMan();
                    tmpADRMan.InitClass(this.GL_User, this.Auftrag.ID, clsAuftrag.const_DBTableName);
                    tmpADRMan.DictManuellADRAuftrag.TryGetValue(clsADRMan.cont_AdrArtID_Empfaenger, out tmpADRMan);
                    if (tmpADRMan != null)
                    {
                        this.Auftrag.AdrManuell = tmpADRMan;
                        tbEmpfaenger.Text = this.Auftrag.AdrManuell.AdrString;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            //VSB
            cbVSB.Checked = (Auftrag.AuftragPos.VSB > (DateTime)Globals.DefaultDateTimeMinValue);
            Functions.SetDateTimePickerValue(ref dtpVSB, Auftrag.AuftragPos.VSB);
            nudVonKW.Value = (decimal)Auftrag.AuftragPos.vKW;

            //Ladetermin
            cbLadeTerminIsSet.Checked = (Auftrag.AuftragPos.LadeTermin > (DateTime)Globals.DefaultDateTimeMinValue);
            Functions.SetDateTimePickerValue(ref dtpLadeTermin, Auftrag.AuftragPos.LadeTermin);
            cbLadeZFNessesary.Checked = Auftrag.AuftragPos.LadeZFRequire;
            cbLadeZFIsSet.Checked = (Auftrag.AuftragPos.LadeZF > (DateTime)Globals.DefaultDateTimeMinValue);
            nudLadeZFHour.Value = (decimal)Auftrag.AuftragPos.LadeZF.Hour;
            nudLadeZFMin.Value = (decimal)Auftrag.AuftragPos.LadeZF.Minute;

            //Liefertermin
            cbLieferTerminIsSet.Checked = (Auftrag.AuftragPos.LieferTermin < (DateTime)Globals.DefaultDateTimeMinValue);
            Functions.SetDateTimePickerValue(ref dtpLieferTermin, Auftrag.AuftragPos.LieferTermin);
            cbLieferZFNessesary.Checked = Auftrag.AuftragPos.LieferZFRequire;
            cbLieferZFIsSet.Checked = (Auftrag.AuftragPos.LieferZF > (DateTime)Globals.DefaultDateTimeMinValue);
            nudLieferZFHour.Value = (decimal)Auftrag.AuftragPos.LieferZF.Hour;
            nudLieferZFMin.Value = (decimal)Auftrag.AuftragPos.LieferZF.Minute;
            nudBisKW.Value = (decimal)Auftrag.AuftragPos.bKW;

            cbLadNrRequire.Checked = !Auftrag.AuftragPos.LadeNrRequire;
            tbLadeNr.Text = Auftrag.AuftragPos.Ladenummer;
            nudFracht.Value = Auftrag.vFracht;
            tbNotiz.Text = Auftrag.AuftragPos.Notiz;
            cbRelation.Text = Auftrag.Relation;
            //Status
            tsbtnStatusInfo.Image = Functions.GetDataGridCellStatusImage(this.Auftrag.AuftragPos.Status);
            tbEntfernung.Text = Auftrag.km.ToString();
        }
        ///<summary>frmAuftrag_Fast / NextOrder</summary>
        ///<remarks></remarks>
        private void NextOrder(bool bo_Back, bool bFirstOrLastOrder)
        {
            //Beschreibung
            //FirstOrder :  bo_Back=true AND bFirstOrLastOrder=true
            //LastOrder :   bo_Back=false AND bFirstOrLastOrder=true
            //Next :        bo_Back=false AND bFirstOrLastOrder=false
            //prev:         bo_Back=true AND bFirstOrLastOrder=false

            decimal decMandantenID = this._ctrMenu._frmMain.GL_System.sys_MandantenID;
            //Check auf Mandanten ID
            if (decMandantenID > 0)
            {
                if (bFirstOrLastOrder)
                {
                    if (bo_Back)
                    {
                        //FirstOrder
                        Auftrag.GetFirstOrder();
                    }
                    else
                    {
                        //LastOrder
                        Auftrag.GetLastOrder();
                    }
                }
                else
                {
                    if (bo_Back)
                    {
                        //prev
                        Auftrag.GetNextOrder(Auftrag.ANr, true);
                    }
                    else
                    {
                        //next
                        Auftrag.GetNextOrder(Auftrag.ANr, false);
                    }
                }
                Auftrag.AuftragPos.Fill();
                SetAuftragDatenToFrm();
                InitDGV();
            }
        }
        ///<summary>frmAuftrag_Fast / initKD</summary>
        ///<remarks></remarks>
        private void initKD()
        {
            //---- Kunde / Auftraggeber  -------
            KDTable.Clear();
            KDTable = clsKunde.dataTableKD(this.GL_User.User_ID);
        }
        ///<summary>frmAuftrag_Fast / initVersender</summary>
        ///<remarks></remarks>
        private void initVersender()
        {
            //------  Versender ComboBox ---------
            VTable.Clear();
            VTable = clsADR.ADRTable();
        }
        ///<summary>frmAuftrag_Fast / initEmpfaenger</summary>
        ///<remarks></remarks>
        private void initEmpfaenger()
        {
            //------ Empfänger ComboBox  ----------
            ETable.Clear();
            ETable = clsADR.ADRTable();
        }
        ///<summary>frmAuftrag_Fast / cbTermin_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbTermin_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLieferTerminIsSet.Checked)
            {
                //if (DateTime.Today.AddDays(1).Date <= dtpLieferTermin.MaxDate.Date)
                //{                   
                //    dtpLieferTermin.Value = DateTime.Today.AddDays(1);
                //}
                //else
                //{
                //    dtpLieferTermin.Value = dtpLieferTermin.MaxDate.Date;
                //}
                ////wenn lade Temrin gesetzt, dann gleich Ladetermin setzen
                //if (cbLadeTerminIsSet.Checked)
                //{
                //    dtpLieferTermin.Value = ;
                //}
                //else
                //{

                //}
                dtpLieferTermin.Value = dtpLieferTermin.MinDate;
            }
            else
            {
                dtpLieferTermin.Value = Globals.DefaultDateTimeMaxValue;
                dtpLieferTermin.MinDate = Globals.DefaultDateTimeMinValue;
                dtpLieferTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
            }
            dtpLieferTermin.Enabled = cbLieferTerminIsSet.Checked;
            nudBisKW.Enabled = cbLieferTerminIsSet.Checked;
            SetMinMaxValueForTerminDatTimePicker(false, false, true);
        }
        ///<summary>frmAuftrag_Fast / cbLadeTerminIsSet_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbLadeTerminIsSet_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLadeTerminIsSet.Checked)
            {
                if (DateTime.Today < dtpLadeTermin.MinDate)
                {
                    dtpLadeTermin.Value = dtpLadeTermin.MinDate;
                }
                else
                {
                    dtpLadeTermin.Value = DateTime.Today;
                }
            }
            else
            {
                //if (Globals.DefaultDateTimeMinValue < dtpLadeTermin.MinDate)
                //{
                //    dtpLadeTermin.Value = dtpLadeTermin.MinDate;
                //}
                //else
                //{
                //    dtpLadeTermin.Value = Globals.DefaultDateTimeMinValue;
                //}
                dtpLadeTermin.MinDate = Globals.DefaultDateTimeMinValue;
                dtpLadeTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
                dtpLadeTermin.Value = Globals.DefaultDateTimeMinValue;
            }
            dtpLadeTermin.Enabled = cbLadeTerminIsSet.Checked;
            SetMinMaxValueForTerminDatTimePicker(false, true, false);
        }
        ///<summary>frmAuftrag_Fast / cbVSB_CheckedChanged</summary>
        ///<remarks></remarks>/
        private void cbVSB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVSB.Checked)
            {
                dtpVSB.Value = DateTime.Today;
            }
            else
            {
                dtpVSB.MinDate = Globals.DefaultDateTimeMinValue;
                dtpVSB.MaxDate = Globals.DefaultDateTimeMaxValue;
                dtpVSB.Value = Globals.DefaultDateTimeMinValue;
            }
            dtpVSB.Enabled = cbVSB.Checked;
            nudVonKW.Enabled = cbVSB.Checked;
            SetMinMaxValueForTerminDatTimePicker(true, false, false);
        }
        ///<summary>frmAuftrag_Fast / tbSearchA_TextChanged</summary>
        ///<remarks></remarks>/
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchA.Text.ToString();
            string Ausgabe = string.Empty;
            DataTable dtTmp = new DataTable();
            initKD();
            //int i = 0;
            DataRow[] rows = KDTable.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = KDTable.Clone();
            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
            Auftraggeber_ADR_ID = Functions.GetADR_IDFromTable(dtTmp);
            if (Versender_ADR_ID == 0)
            {
                Versender_ADR_ID = Auftraggeber_ADR_ID;
                tbSearchV.Text = tbSearchA.Text; ;
                tbVersender.Text = tbAuftraggeber.Text;
            }

        }
        ///<summary>frmAuftrag_Fast / tbSearchV_TextChanged</summary>
        ///<remarks></remarks>/
        private void tbSearchV_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchV.Text.ToString();
            string Ausgabe = "";
            initVersender();
            DataTable dtTmp = new DataTable();

            DataRow[] rows = VTable.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = VTable.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbVersender.Text = Functions.GetADRStringFromTable(dtTmp);
            Versender_ADR_ID = Functions.GetADR_IDFromTable(dtTmp);

            //Entfernungsberechnung
            if ((Versender_ADR_ID > 0) && (Empfaenger_ADR_ID > 0))
            {
                if (tbEntfernung.Text == "0")
                {
                    CheckKM();
                }
            }
        }
        ///<summary>frmAuftrag_Fast / tbSearchE_TextChanged</summary>
        ///<remarks></remarks>
        private void tbSearchE_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchE.Text.ToString();
            string Ausgabe = "";
            initEmpfaenger();
            DataTable dtTmp = new DataTable();

            DataRow[] rows = ETable.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
            dtTmp = ETable.Clone();

            foreach (DataRow row in rows)
            {
                Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
                dtTmp.ImportRow(row);
            }
            tbEmpfaenger.Text = Functions.GetADRStringFromTable(dtTmp);
            Empfaenger_ADR_ID = Functions.GetADR_IDFromTable(dtTmp);
            //Entfernungsberechnung
            if ((Versender_ADR_ID > 0) && (Empfaenger_ADR_ID > 0))
            {
                if (tbEntfernung.Text == "0")
                {
                    CheckKM();
                }
            }
        }
        ///<summary>frmAuftrag_Fast / CheckInput</summary>
        ///<remarks></remarks>
        private bool CheckInput()
        {
            bool status = true;
            string Help;
            Help = "Folgende Pflichtfelder / Angaben sind nicht korrekt: \n";
            if (tbAuftraggeber.Text == "")
            {
                status = false;
                Help = Help + "Auftraggeber nicht ausgewählt \n";
            }
            if (tbEmpfaenger.Text == "")
            {
                Help = Help + "Empfänger nicht ausgewählt \n";
                status = false;
            }
            if (tbVersender.Text == "")
            {
                Help = Help + "Versender nicht ausgewählt \n";
                status = false;
            }
            if (!bo_Update)
            {
                if (dtpLieferTermin.Value < DateTime.Today)
                {
                    status = false;
                    Help = Help + "Der Liefertermin darf nicht in der Vergangenheit liegen \n";
                }
                if (dtpVSB.Value < DateTime.Today)
                {
                    status = false;
                    Help = Help + "Die Versandbereitschaft darf nicht in der Vergangenheit liegen \n";
                }
            }
            if (status)
            {
                return true;
            }
            else
            {
                MessageBox.Show(Help);
                return false;
            }
        }
        ///<summary>frmAuftrag_Fast / AssignValueAuftrag</summary>
        ///<remarks></remarks>
        private void AssignValueAuftrag()
        {
            string strStd = string.Empty;
            string strMin = string.Empty;

            clsAuftrag tmpAuftrag = Auftrag.Copy();
            //tmpAuftrag.AuftragPos = Auftrag.AuftragPos;
            bUpdateOrder = (this.Auftrag.ID > 0);

            Auftrag = new clsAuftrag();
            Auftrag.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system);
            Auftrag.Mandanten_ID = this._ctrMenu._frmMain.system.AbBereich.MandantenID;
            Auftrag.ArbBereich_ID = this._ctrMenu._frmMain.system.AbBereich.ID;

            if (!bUpdateOrder)
            {
                //auftrag =Convert.ToInt32(tbANr.Text.ToString());
                decimal decTmp = 0.0M;
                Decimal.TryParse(tbOrderID.Text.ToString(), out decTmp);
                Auftrag.ANr = decTmp;
            }
            //CHeck ADR >>> Auftraggeber,Versender,Empfänger
            if (Auftraggeber_ADR_ID == 0)
            {
                tbSearchA.Text = tbSearchA.Text.ToString().Trim();
                Auftraggeber_ADR_ID = clsADR.GetIDByMatchcode(tbSearchA.Text);
            }
            if (Empfaenger_ADR_ID == 0)
            {
                tbSearchE.Text = tbSearchE.Text.ToString().Trim();
                //Empfaenger_ADR_ID = clsADR.GetKD_IDByMatchcode(tbSearchE.Text);
                Empfaenger_ADR_ID = clsADR.GetIDByMatchcode(tbSearchE.Text);
            }
            if (Versender_ADR_ID == 0)
            {
                tbSearchV.Text = tbSearchV.Text.ToString().Trim();
                Versender_ADR_ID = clsADR.GetIDByMatchcode(tbSearchV.Text);
            }
            Auftrag.KD_ID = Auftraggeber_ADR_ID;
            Auftrag.E_ID = Empfaenger_ADR_ID;
            Auftrag.B_ID = Versender_ADR_ID;
            Auftrag.Gewicht = Gewicht;

            //AuftragDatum            
            Auftrag.ADate = Convert.ToDateTime(tbOrderDate.Text.ToString());
            if (cbRelation.SelectedValue != null)
            {
                Auftrag.Relation = cbRelation.SelectedValue.ToString();
            }
            else
            {
                Auftrag.Relation = string.Empty;
            }
            if (
                (tbLadeNr.Text == "") &
                (cbLadNrRequire.Checked)
                )
            {
                Status = 1; //unvollständig;
            }
            Auftrag.vFracht = nudFracht.Value;

            //Entfernung
            Int32 iEntfernung = 0;
            tbEntfernung.Text = tbEntfernung.Text.Trim();
            if (Functions.CheckNum(tbEntfernung.Text))
            {
                Int32.TryParse(tbEntfernung.Text, out iEntfernung);
            }
            Auftrag.km = iEntfernung;

            if (!Abbruch)
            {
                if (!bUpdateOrder)
                {
                    //*** Eintrag DB Artikel und Auftrag
                    Auftrag.Add();
                }
                else
                {
                    //---- Update Datensatz in DB ---
                    if (Auftrag.AuftragPos.AuftragPos == 0)
                    {
                        Auftrag.ID = tmpAuftrag.ID;
                        Auftrag.ANr = tmpAuftrag.ANr;
                        Auftrag.Update();
                        Auftrag.AuftragPos.ID = tmpAuftrag.AuftragPos.ID;
                        Auftrag.AuftragPos.Fill();
                    }
                }
            }
        }
        ///<summary>frmAuftrag_Fast / AssignValueAuftragPos</summary>
        ///<remarks></remarks>
        private void AssignValueAuftragPos()
        {
            bUpdateOrder = (this.Auftrag.AuftragPos.ID > 0);

            clsAuftragPos TmpPos = Auftrag.AuftragPos.Copy();
            //--- Setz Val für AuftragPos ------
            Auftrag.AuftragPos.Auftrag_ID = Auftrag.ANr;
            Auftrag.AuftragPos.AuftragTableID = Auftrag.ID;
            Status = 2;  //vollständigkeit wird vorrausgesetzt und dann geprüft

            //Ladetermin / Ladezeitfenster
            if (cbLadeTerminIsSet.Checked)
            {
                Auftrag.AuftragPos.LadeTermin = dtpLadeTermin.Value;
            }
            else
            {
                Auftrag.AuftragPos.LadeTermin = Globals.DefaultDateTimeMinValue;
            }
            Auftrag.AuftragPos.LadeZFRequire = cbLadeZFNessesary.Checked;
            Auftrag.AuftragPos.LadeZF = Functions.GetStrTimeZF(nudLadeZFHour, nudLadeZFMin);
            // VSB
            if (cbVSB.Checked)
            {
                Auftrag.AuftragPos.VSB = dtpVSB.Value;
            }
            else
            {
                Auftrag.AuftragPos.VSB = Globals.DefaultDateTimeMinValue;
                Status = 1; //unvollständig
            }
            // Liefertermin und Ladezeitfenster
            if (cbLieferTerminIsSet.Checked)
            {
                Auftrag.AuftragPos.LieferTermin = dtpLieferTermin.Value;
            }
            else
            {
                Auftrag.AuftragPos.LieferTermin = Globals.DefaultDateTimeMaxValue;
                Status = 1;  // unvollständig
            }
            Auftrag.AuftragPos.LieferZF = Functions.GetStrTimeZF(nudLieferZFHour, nudLieferZFMin);
            Auftrag.AuftragPos.LieferZFRequire = cbLieferZFNessesary.Checked;

            cbLadNrRequire.Checked = false;
            //Ladenummer
            tbLadeNr.Text.ToString().Trim();
            if (tbLadeNr.Text == string.Empty)
            {
                if (cbLadNrRequire.Checked == true)
                {
                    //Status = 1; //unvollständig;
                }
            }
            else
            {
                Auftrag.AuftragPos.Ladenummer = tbLadeNr.Text;
            }
            Auftrag.AuftragPos.LadeNrRequire = cbLadNrRequire.Checked;
            Auftrag.AuftragPos.Notiz = tbNotiz.Text;
            Auftrag.AuftragPos.Status = Status;


            if (cbPrio.Checked == true)
            {
                Auftrag.AuftragPos.Prioritaet = true;
            }
            else
            {
                Auftrag.AuftragPos.Prioritaet = false;
            }
            Auftrag.AuftragPos.vKW = Convert.ToInt32(nudVonKW.Value);
            Auftrag.AuftragPos.bKW = Convert.ToInt32(nudBisKW.Value);

            if (!Abbruch)
            {
                if (!bUpdateOrder)
                {
                    // -- Eintrag in DB ---
                    Auftrag.AuftragPos.Add(false);
                }
                else
                {
                    Auftrag.AuftragPos.ID = TmpPos.ID;
                    Auftrag.AuftragPos.Update();
                }
            }
        }
        ///<summary>frmAuftrag_Fast / TakeOverAuftragID</summary>
        ///<remarks></remarks>
        public void TakeOverAuftragID(decimal myAuftragPosTableID)
        {
            Auftrag.AuftragPos.ID = myAuftragPosTableID;
            Auftrag.AuftragPos.Fill();
            //Save = false;
            //bo_Update = true;
            SetAuftragDatenToFrm();
            initGArt();
        }
        ///<summary>frmAuftrag_Fast / tsbNeu_Click</summary>
        ///<remarks>Eine neue Zeile wird dem Grid hinzugefügt.</remarks>
        private void tsbNeu_Click(object sender, EventArgs e)
        {
            bUpdateArtikel = false;
            SetArtikelEingabeFelderEnabled(true);
            ClearArtikelEingabefelder();
            tbGArtSearch.Focus();
        }
        ///<summary>frmAuftrag_Fast / SetArtikelEingabeFelderEnabled</summary>
        ///<remarks>Eine neue Zeile wird dem Grid hinzugefügt.</remarks>
        private void SetArtikelEingabeFelderEnabled(bool bEnabledArtikelInputField)
        {
            tbGArtSearch.Enabled = bEnabledArtikelInputField;
            btnGArt.Enabled = bEnabledArtikelInputField;
            tbGArt.Enabled = bEnabledArtikelInputField;
            tbGArtZusatz.Enabled = bEnabledArtikelInputField;
            tbWerksnummer.Enabled = bEnabledArtikelInputField;
            nudAnzahl.Enabled = bEnabledArtikelInputField;
            nudDicke.Enabled = bEnabledArtikelInputField;
            nudBreite.Enabled = bEnabledArtikelInputField;
            nudLaenge.Enabled = bEnabledArtikelInputField;
            nudHoehe.Enabled = bEnabledArtikelInputField;
            nudGemGewicht.Enabled = bEnabledArtikelInputField;
            nudNetto.Enabled = bEnabledArtikelInputField;
            nudBrutto.Enabled = bEnabledArtikelInputField;
        }
        ///<summary>frmAuftrag_Fast / ClearArtikelEingabefelder</summary>
        ///<remarks></remarks>
        private void ClearArtikelEingabefelder()
        {
            tbArtikelID.Text = string.Empty;
            tbGArtSearch.Text = string.Empty;
            tbGArt.Text = string.Empty;
            tbGArtZusatz.Text = string.Empty;
            tbWerksnummer.Text = string.Empty;
            nudAnzahl.Value = 0;
            nudDicke.Value = 0;
            nudBreite.Value = 0;
            nudLaenge.Value = 0;
            nudHoehe.Value = 0;
            nudGemGewicht.Value = 0;
            nudNetto.Value = 0;
            nudBrutto.Value = 0;
            lGArtID.Text = "Güterart ID: #0";
            //ARtikelklasse neue
            Auftrag.AuftragPos.Artikel = new clsArtikel();
            Auftrag.AuftragPos.Artikel.InitClass(this.GL_User, this.GL_System);
        }
        ///<summary>frmAuftrag_Fast / OpenAuftragserfassungADREingabe</summary>
        ///<remarks>Öffnen der Form zur Adresserfassung / Auswahl</remarks>
        private void OpenAuftragserfassungADREingabe()
        {
            this._ctrAuftrag._ctrMenu.OpenADRSearch(this);
        }
        ///<summary>frmAuftrag_Fast / btnSearchV_MouseDown</summary>
        ///<remarks>Info Tool Tip Button</remarks>
        private void btnSearchV_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Suche nach Versender - bitte anklicken";
            info.SetToolTip(this.btnSearchV, strInfo);
        }
        ///<summary>frmAuftrag_Fast / btnSearchE_MouseDown</summary>
        ///<remarks>Info Tool Tip Button</remarks>
        private void btnSearchE_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Suche nach Empfänger - bitte anklicken";
            info.SetToolTip(this.btnSearchE, strInfo);
        }
        ///<summary>frmAuftrag_Fast / btnSearchA_MouseDown</summary>
        ///<remarks>Info Tool Tip Button</remarks>
        private void btnSearchA_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            string strInfo = string.Empty;
            strInfo = "Suche nach Auftraggeber - bitte anklicken";
            info.SetToolTip(this.btnSearchA, strInfo);
        }
        ///<summary>frmAuftrag_Fast / SetKDRecAfterADRSearch</summary>
        ///<remarks>Ermittelt die Adressdaten anhand der übergebenen AdressID und setzt die Adressdaten
        ///         entsprechend auf die Form</remarks>
        public void SetKDRecAfterADRSearch(decimal ADR_ID)
        {
            string strE = string.Empty;
            string strMC = string.Empty;
            DataSet ds = clsADR.ReadADRbyID(ADR_ID);
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strMC = ds.Tables[0].Rows[i]["ViewID"].ToString();
                strMC = strMC.ToString().Trim();
                strE = ds.Tables[0].Rows[i]["ViewID"].ToString() + " - ";
                strE = strE.Trim();

                string strName = string.Empty;
                string strPLZ = string.Empty;
                string strOrt = string.Empty;

                strName = ds.Tables[0].Rows[i]["Name1"].ToString().Trim();
                strPLZ = ds.Tables[0].Rows[i]["PLZ"].ToString().Trim();
                strOrt = ds.Tables[0].Rows[i]["Ort"].ToString().Trim();

                strE = strName + " - " + strPLZ + " - " + strOrt;
                switch (SearchButton)
                {
                    case 1:
                        Auftraggeber_ADR_ID = ADR_ID;
                        tbSearchA.Text = strMC;
                        tbAuftraggeber.Text = strE;
                        if (Versender_ADR_ID == 0)
                        {
                            Versender_ADR_ID = ADR_ID;
                            tbSearchV.Text = strMC;
                            tbVersender.Text = strE;
                        }
                        break;

                    case 2:
                        Versender_ADR_ID = ADR_ID;
                        tbSearchV.Text = strMC;
                        tbVersender.Text = strE;
                        break;

                    case 3:
                        Empfaenger_ADR_ID = ADR_ID;
                        tbSearchE.Text = strMC;
                        tbEmpfaenger.Text = strE;
                        break;
                }
            }
        }
        ///<summary>frmAuftrag_Fast / IsAufragserfassungActiv</summary>
        ///<remarks></remarks>
        private bool IsAufragserfassungActiv()
        {
            //decimal decTmp = -1;
            //if ((tbOrderID.Text != string.Empty) && (tbOrderID.Text !="0"))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }
        ///<summary>frmAuftrag_Fast / btnSearchA_Click</summary>
        ///<remarks></remarks>
        private void btnSearchA_Click(object sender, EventArgs e)
        {
            SearchButton = 1;
            OpenAuftragserfassungADREingabe();
        }
        ///<summary>frmAuftrag_Fast / btnSearchV_Click</summary>
        ///<remarks></remarks>
        private void btnSearchV_Click(object sender, EventArgs e)
        {
            SearchButton = 2;
            OpenAuftragserfassungADREingabe();
        }
        ///<summary>frmAuftrag_Fast / btnSearchE_Click</summary>
        ///<remarks></remarks>
        private void btnSearchE_Click(object sender, EventArgs e)
        {
            SearchButton = 3;
            OpenAuftragserfassungADREingabe();
        }
        ///<summary>frmAuftrag_Fast / tsbtnClose_Click</summary>
        ///<remarks></remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            _ctrAuftrag.InitDGV();
            // this.Close();
            this.Dispose();
        }
        ///<summary>frmAuftrag_Fast / cbZF_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbZF_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLieferZFNessesary.Checked)
            {
                //Zeitfenster erforderlich
                cbLieferZFIsSet.Enabled = true;
            }
            else
            {
                //Zeitfenster nicht erforderlich 
                cbLieferZFIsSet.Enabled = false;
            }
            cbLieferZFIsSet.Checked = false;
        }
        ///<summary>frmAuftrag_Fast / cbZFBekannt_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbZFBekannt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLieferZFIsSet.Checked)
            {
                nudLieferZFHour.Enabled = true;
                nudLieferZFMin.Enabled = true;
                nudLieferZFHour.Value = (decimal)this.Auftrag.AuftragPos.LadeZF.Hour;
                nudLieferZFMin.Value = (decimal)this.Auftrag.AuftragPos.LadeZF.Minute;
            }
            else
            {
                nudLieferZFHour.Enabled = false;
                nudLieferZFMin.Enabled = false;
                nudLieferZFHour.Value = 0;
                nudLieferZFMin.Value = 0;
            }
        }
        ///<summary>frmAuftrag_Fast / dtpLadeTermin_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpLadeTermin_ValueChanged(object sender, EventArgs e)
        {
            //SetMinMaxValueForTerminDatTimePicker();
        }

        private void dtpLadeTermin_Validating(object sender, CancelEventArgs e)
        {
            SetMinMaxValueForTerminDatTimePicker(false, true, false);
        }
        ///<summary>frmAuftrag_Fast / dtpT_date_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpT_date_ValueChanged(object sender, EventArgs e)
        {
            //SetTermin(dtpLieferTermin.Value, dtpVSB.Value, cbLieferTerminIsSet.Checked, false);
            //SetMinMaxValueForTerminDatTimePicker();
            if (
                (dtpLieferTermin.Value.Date != Globals.DefaultDateTimeMinValue.Date) &&
                (dtpLieferTermin.Value.Date != Globals.DefaultDateTimeMaxValue.Date)
               )
            {
                Functions.SetKWValue(ref nudBisKW, dtpLieferTermin.Value);
            }
        }
        private void dtpLieferTermin_Validating(object sender, CancelEventArgs e)
        {
            SetMinMaxValueForTerminDatTimePicker(false, false, true);
        }
        ///<summary>frmAuftrag_Fast / dtpVSB_ValueChanged</summary>
        ///<remarks></remarks>
        private void dtpVSB_ValueChanged(object sender, EventArgs e)
        {
            //SetTermin(dtpLieferTermin.Value, dtpVSB.Value, cbVSB.Checked, true);
            //SetMinMaxValueForTerminDatTimePicker();
            if (
                (dtpVSB.Value.Date != Globals.DefaultDateTimeMinValue.Date) &&
                (dtpVSB.Value.Date != Globals.DefaultDateTimeMaxValue.Date)
               )
            {
                Functions.SetKWValue(ref nudVonKW, dtpVSB.Value);
            }
        }
        private void dtpVSB_Validating(object sender, CancelEventArgs e)
        {
            SetMinMaxValueForTerminDatTimePicker(true, false, false);
        }
        ///<summary>frmAuftrag_Fast / SetTermin</summary>
        ///<remarks>Check Termineingabe auf:
        ///         LT kann nicht vor dem VSB Termin liegen</remarks>
        private void SetTermin(DateTime dtLT, DateTime dtVSB, bool bo_CBisChecked, bool bo_VSB)
        {
            //DateTime retdt = DateTime.MinValue;

            ///**************************************
            // * Liefertermin bekannt / VSB bekannt
            // * ************************************/
            ////
            //if ((cbLieferTerminIsSet.Checked == false) & (cbVSB.Checked == false))
            //{
            //    //Check Eintrag oder Update
            //    if (bo_Update)  //Update
            //    {
            //        //Datum darf in der Vergangenheit liegen
            //        //Orientierung am Liefertermin
            //        if (dtLT < dtVSB)
            //        {
            //            dtVSB = dtLT;
            //        }
            //    }
            //    else // Eintrag
            //    {
            //        //Datum darf NICHT in der Vergangenheit liegen
            //        if (dtLT < DateTime.Now.Date)
            //        {
            //            dtLT = DateTime.Now.Date;
            //        }
            //        if (dtVSB < DateTime.Now.Date)
            //        {
            //            dtVSB = DateTime.Now.Date;
            //        }
            //        //Orientierung am Liefertermin
            //        if (dtLT < dtVSB)
            //        {
            //            dtVSB = dtLT;
            //        }
            //    }
            //}
            ////Nur Liefertermin
            //if ((cbLieferTerminIsSet.Checked == false) & (cbVSB.Checked == true))
            //{
            //    //Check Eintrag oder Update
            //    if (!bo_Update)  // Eintrag
            //    {
            //        //Datum darf NICHT in der Vergangenheit liegen
            //        if (dtLT < DateTime.Now.Date)
            //        {
            //            dtLT = DateTime.Now.Date;
            //        }
            //    }
            //}
            ////Nur VSB
            //if ((cbLieferTerminIsSet.Checked == true) & (cbVSB.Checked == false))
            //{
            //    //Check Eintrag oder Update
            //    if (!bo_Update)  // Eintrag
            //    {
            //        //Datum darf NICHT in der Vergangenheit liegen
            //        if (dtVSB < DateTime.Now.Date)
            //        {
            //            dtVSB = DateTime.Now.Date;
            //        }
            //    }
            //}
            //dtpVSB.Value = dtVSB;
            //if (cbVSB.Checked)
            //{
            //    Functions.SetKWValue(ref nudVonKW, dtpVSB.Value);
            //}
            //else
            //{
            //    nudVonKW.Value = 0;
            //}
            //dtpLieferTermin.Value = dtLT;
            //if (cbLieferTerminIsSet.Checked)
            //{

            //    Functions.SetKWValue(ref nudBisKW, dtpLieferTermin.Value);
            //}
            //else
            //{
            //    nudBisKW.Value = 0;
            //}
        }

        private void SetMinMaxValueForTerminDatTimePicker(bool bIsVSB, bool bIsLade, bool bIsLiefer)
        {
            //VSG
            if (bIsVSB)
            {
                if (cbVSB.Checked)
                {
                    dtpVSB.MinDate = DateTime.Now;
                    //dtpVSB.MaxDate = Globals.DefaultDateTimeMaxValue;
                    //Ladetermin
                    dtpLadeTermin.MinDate = dtpVSB.Value;
                    dtpLadeTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
                    //Liefertermin
                    dtpLieferTermin.MinDate = dtpVSB.Value;
                    dtpLieferTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
                }
                else
                {
                    dtpVSB.MinDate = Globals.DefaultDateTimeMinValue;
                    dtpVSB.MaxDate = Globals.DefaultDateTimeMaxValue;
                    if (cbLadeTerminIsSet.Checked)
                    {
                        dtpLadeTermin.MinDate = DateTime.Now;
                        //dtpLadeTermin.MaxDate=Globals.DefaultDateTimeMaxValue;
                    }
                }
            }
            //Ladetermin
            if (bIsLade)
            {
                if (cbLadeTerminIsSet.Checked)
                {
                    //VSB
                    dtpVSB.MinDate = Globals.DefaultDateTimeMinValue;
                    dtpVSB.MaxDate = dtpLadeTermin.Value;
                    //Liefertermin
                    dtpLieferTermin.MinDate = dtpLadeTermin.Value;
                    dtpLieferTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
                }
                else
                {
                    dtpLadeTermin.MinDate = Globals.DefaultDateTimeMinValue;
                    dtpLadeTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
                    if (cbVSB.Checked)
                    {
                        //dtpVSB.MinDate = DateTime.Now;
                        //dtpVSB.MaxDate = 
                    }

                }
            }
            //Liefer
            if (bIsLiefer)
            {
                if (cbLieferTerminIsSet.Checked)
                {
                    //VSB
                    dtpVSB.MinDate = Globals.DefaultDateTimeMinValue;
                    dtpVSB.MaxDate = dtpLieferTermin.Value;
                    //LadeTermin
                    dtpLadeTermin.MinDate = Globals.DefaultDateTimeMinValue;
                    dtpLadeTermin.MaxDate = dtpLieferTermin.Value;
                }
                else
                {
                    dtpLieferTermin.MinDate = Globals.DefaultDateTimeMinValue;
                    dtpLieferTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
                }
            }


            //wenn alle nicht marikiert, dann Default Werte setzen
            if (
                (!cbLadeTerminIsSet.Checked) &&
                (!cbLieferTerminIsSet.Checked) &&
                (!cbVSB.Checked)
               )
            {
                //VSB
                dtpVSB.MinDate = Globals.DefaultDateTimeMinValue;
                dtpVSB.MaxDate = Globals.DefaultDateTimeMaxValue;
                //LadeTermin
                dtpLadeTermin.MinDate = Globals.DefaultDateTimeMinValue;
                dtpLadeTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
                //Liefertermin
                dtpLieferTermin.MinDate = Globals.DefaultDateTimeMinValue;
                dtpLieferTermin.MaxDate = Globals.DefaultDateTimeMaxValue;
            }
        }
        ///<summary>frmAuftrag_Fast / GetDistance</summary>
        ///<remarks></remarks>
        private void GetDistance()
        {
            if ((tbSearchV.Text != string.Empty) &&
                (tbSearchE.Text != string.Empty))
            {
                Empfaenger_ADR_ID = clsADR.GetIDByMatchcode(tbSearchE.Text);
                Versender_ADR_ID = clsADR.GetIDByMatchcode(tbSearchV.Text);
                if ((Empfaenger_ADR_ID > 0) &&
                    (Versender_ADR_ID > 0))
                {
                    clsDistance distance = new clsDistance();
                    distance.GL_User = this.GL_User;
                    distance.DeleteDistanceZero();                    //
                    distance.FillByAdrID(Versender_ADR_ID, Empfaenger_ADR_ID);
                    if (distance.IsgMaps)
                    {
                        tbEntfernung.Text = distance.kmGMaps.ToString();
                    }
                    else
                    {
                        tbEntfernung.Text = distance.km.ToString();
                    }
                }
            }
        }
        ///<summary>frmAuftrag_Fast / tbSearchE_Validated</summary>
        ///<remarks></remarks>
        private void tbSearchE_Validated(object sender, EventArgs e)
        {
            tbEntfernung.Text = "0";
            CheckKM();
        }
        ///<summary>frmAuftrag_Fast / tbSearchV_Validated</summary>
        ///<remarks></remarks>
        private void tbSearchV_Validated(object sender, EventArgs e)
        {
            tbEntfernung.Text = "0";
            CheckKM();
        }
        ///<summary>frmAuftrag_Fast / CheckKM</summary>
        ///<remarks></remarks>
        private void CheckKM()
        {
            string strTmp = tbEntfernung.Text;
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);

            //wenn km-Wert 0 dann neu ermitteln
            if (iTmp < 1)
            {
                GetDistance();
            }
        }
        ///<summary>frmAuftrag_Fast / SetImageToPicBox</summary>
        ///<remarks></remarks>
        private void SetImageToPicBox(bool boShowP)
        {
            if (boShowP)
            {
                pbDistance.Image = Sped4.Properties.Resources.none;
            }
            else
            {
                pbDistance.Image = null;
            }
        }
        ///<summary>frmAuftrag_Fast / nudAnzahl_Enter</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void nudAnzahl_Enter(object sender, EventArgs e)
        {
            SelectNummericUpDownCtrValue(sender);
        }
        ///<summary>frmAuftrag_Fast / nudDicke_Enter</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void nudDicke_Enter(object sender, EventArgs e)
        {
            SelectNummericUpDownCtrValue(sender);
        }
        ///<summary>frmAuftrag_Fast / nudBreite_Enter</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void nudBreite_Enter(object sender, EventArgs e)
        {
            SelectNummericUpDownCtrValue(sender);
        }
        ///<summary>frmAuftrag_Fast / nudLaenge_Enter</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void nudLaenge_Enter(object sender, EventArgs e)
        {
            SelectNummericUpDownCtrValue(sender);
        }
        ///<summary>frmAuftrag_Fast / nudHoehe_Enter</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void nudHoehe_Enter(object sender, EventArgs e)
        {
            SelectNummericUpDownCtrValue(sender);
        }
        ///<summary>frmAuftrag_Fast / nudGemGewicht_Enter</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void nudGemGewicht_Enter(object sender, EventArgs e)
        {
            SelectNummericUpDownCtrValue(sender);
        }
        ///<summary>frmAuftrag_Fast / nudNetto_Enter</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void nudNetto_Enter(object sender, EventArgs e)
        {
            SelectNummericUpDownCtrValue(sender);
        }
        ///<summary>frmAuftrag_Fast / nudBrutto_Enter</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void nudBrutto_Enter(object sender, EventArgs e)
        {
            SelectNummericUpDownCtrValue(sender);
        }
        ///<summary>frmAuftrag_Fast / SelectNummericUpDownCtrValue</summary>
        ///<remarks>Text/Value wird markiert</remarks>
        private void SelectNummericUpDownCtrValue(object sender)
        {
            string strTmp = Functions.FormatDecimal(((NumericUpDown)sender).Value);
            ((NumericUpDown)sender).Select(0, strTmp.Length);
        }
        ///<summary>frmAuftrag_Fast / tsbtnScan_Click</summary>
        ///<remarks>Dokument scannen</remarks>
        private void tsbtnScan_Click(object sender, EventArgs e)
        {
            this._ctrAuftrag._ctrMenu.OpenScanFrm(this.Auftrag.ID, this.Auftrag.AuftragPos.ID, 0, 0, this);
        }
        ///<summary>frmAuftrag_Fast / pbDistance_MouseClick</summary>
        ///<remarks>Das Menü wird erstellt, wenn es benötigt wird.</remarks>
        private void pbDistance_MouseClick(object sender, MouseEventArgs e)
        {
            //cmsDistancePB.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
            RadContextMenu cmMenuDistance = new RadContextMenu();
            RadMenuSeparatorItem separator;
            RadMenuItem customMenuItem;

            //1. MenüItem
            separator = new RadMenuSeparatorItem();
            cmMenuDistance.Items.Add(separator);
            customMenuItem = new RadMenuItem();
            customMenuItem.Text = "Entfernungscenter öffnen";
            customMenuItem.Enabled = tsbtnAuftragClose.Visible;
            customMenuItem.Click += new EventHandler(this.OpenFrmDistance);
            cmMenuDistance.Items.Add(customMenuItem);
            //2. Menü Item
            separator = new RadMenuSeparatorItem();
            cmMenuDistance.Items.Add(separator);
            customMenuItem = new RadMenuItem();
            customMenuItem.Text = "Adressdaten übernehmen";
            customMenuItem.Enabled = tsbtnAuftragClose.Visible;
            customMenuItem.Click += new EventHandler(this.OpenCtrDistancWithADRTakeOver);
            cmMenuDistance.Items.Add(customMenuItem);
            cmMenuDistance.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
        }
        ///<summary>frmAuftrag_Fast / OpenCtrDistancWithADRTakeOver</summary>
        ///<remarks></remarks>
        private void OpenCtrDistancWithADRTakeOver(object sender, EventArgs e)
        {
            if ((tbSearchV.Text != string.Empty) &
                (tbSearchE.Text != string.Empty))
            {
                bo_AdrTakeOverForDistanceCenter = true;
                OpenFrmDistance(sender, e);
            }
            else
            {
                clsMessages.Auftragserfassung_ADRTakeOverDistanceCenter();
            }
        }
        ///<summary>frmAuftrag_Fast / OpenFrmDistance</summary>
        ///<remarks></remarks>
        private void OpenFrmDistance(object sender, EventArgs e)
        {
            ctrDistance Distance = new ctrDistance();
            Distance._frmAuftragFast = this;
            this._ctrAuftrag._ctrMenu.OpenFrmTMP(Distance);
        }
        ///<summary>frmAuftrag_Fast / msItemADRTakeOver_DoubleClick</summary>
        ///<remarks></remarks>
        private void msItemADRTakeOver_DoubleClick(object sender, EventArgs e)
        {
        }
        ///<summary>frmAuftrag_Fast / tsbtnBack_Click</summary>
        ///<remarks></remarks>
        private void tsbtnBack_Click(object sender, EventArgs e)
        {
            //Beschreibung
            //FirstOrder :  bo_Back=true AND bFirstOrLastOrder=true
            //LastOrder :   bo_Back=false AND bFirstOrLastOrder=true
            //Next :        bo_Back=false AND bFirstOrLastOrder=false
            //prev:         bo_Back=true AND bFirstOrLastOrder=false
            NextOrder(true, false);
        }
        ///<summary>frmAuftrag_Fast / tsbtnVor_Click</summary>
        ///<remarks></remarks>
        private void tsbtnVor_Click(object sender, EventArgs e)
        {
            //Beschreibung
            //FirstOrder :  bo_Back=true AND bFirstOrLastOrder=true
            //LastOrder :   bo_Back=false AND bFirstOrLastOrder=true
            //Next :        bo_Back=false AND bFirstOrLastOrder=false
            //prev:         bo_Back=true AND bFirstOrLastOrder=false
            NextOrder(false, false);
        }
        ///<summary>frmAuftrag_Fast / tsbtnFirstOrder_Click</summary>
        ///<remarks></remarks>
        private void tsbtnFirstOrder_Click(object sender, EventArgs e)
        {
            //Beschreibung
            //FirstOrder :  bo_Back=true AND bFirstOrLastOrder=true
            //LastOrder :   bo_Back=false AND bFirstOrLastOrder=true
            //Next :        bo_Back=false AND bFirstOrLastOrder=false
            //prev:         bo_Back=true AND bFirstOrLastOrder=false
            NextOrder(true, true);
        }
        ///<summary>frmAuftrag_Fast / tsbtnLastOrder_Click</summary>
        ///<remarks></remarks>
        private void tsbtnLastOrder_Click(object sender, EventArgs e)
        {
            //Beschreibung
            //FirstOrder :  bo_Back=true AND bFirstOrLastOrder=true
            //LastOrder :   bo_Back=false AND bFirstOrLastOrder=true
            //Next :        bo_Back=false AND bFirstOrLastOrder=false
            //prev:         bo_Back=true AND bFirstOrLastOrder=false
            NextOrder(false, true);
        }
        ///<summary>frmAuftrag_Fast / tbAuftraggeber_Validated</summary>
        ///<remarks></remarks>
        private void tbAuftraggeber_Validated(object sender, EventArgs e)
        {
            //if (tbAuftraggeber.Text != string.Empty)
            //{
            //    LoadComboTarif();
            //}
        }
        ///<summary>frmAuftrag_Fast / tbAuftraggeber_TextChanged</summary>
        ///<remarks></remarks>
        private void tbAuftraggeber_TextChanged(object sender, EventArgs e)
        {
            //if (tbAuftraggeber.Text != string.Empty)
            //{
            //    LoadComboTarif();
            //}
        }
        ///<summary>frmAuftrag_Fast / tbEntfernung_TextChanged</summary>
        ///<remarks></remarks>
        private void tbEntfernung_TextChanged(object sender, EventArgs e)
        {
            //Wenn das km > 0 dann soll die Warnung (Picture) ausgebledet werden
            Int32 ikm = 0;
            bool boShowPicture = true;
            if (Int32.TryParse(tbEntfernung.Text, out ikm))
            {
                if (ikm > 0)
                {
                    boShowPicture = false;
                }
                else
                {
                    boShowPicture = true;
                }
            }
            else
            {
                tbEntfernung.Text = "0";
                boShowPicture = false;
            }
            SetImageToPicBox(boShowPicture);
            //EnableMSItemTakeOver(boShowPicture);
        }
        ///<summary>frmAuftrag_Fast / tbEntfernung_Validating</summary>
        ///<remarks></remarks>
        private void tbEntfernung_Validating(object sender, CancelEventArgs e)
        {
            Int32 iKm = 0;

            if (Int32.TryParse(tbEntfernung.Text, out iKm))
            {
                tbEntfernung.Text = iKm.ToString();
            }
            else
            {
                clsMessages.Allgemein_EingabeIstKeineGanzzahl();
                tbEntfernung.Focus();
            }
        }
        ///<summary>frmAuftrag_Fast / cbTermin_KeyDown</summary>
        ///<remarks></remarks>
        private void cbTermin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbLieferTerminIsSet.Checked)
                {
                    cbLieferTerminIsSet.Checked = false;
                    dtpLieferTermin.Focus();
                }
                else
                {
                    cbLieferTerminIsSet.Checked = true;
                    cbVSB.Focus();
                }
            }
        }
        ///<summary>frmAuftrag_Fast / cbVSB_KeyDown</summary>
        ///<remarks></remarks>
        private void cbVSB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbVSB.Checked)
                {
                    cbVSB.Checked = false;
                    dtpVSB.Focus();
                }
                else
                {
                    cbVSB.Checked = true;
                    cbLieferZFNessesary.Focus();
                }
            }
        }
        ///<summary>frmAuftrag_Fast / cbZF_KeyDown</summary>
        ///<remarks></remarks>
        private void cbZF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbLieferZFNessesary.Checked)
                {
                    cbLieferZFNessesary.Checked = false;
                    cbLieferZFIsSet.Focus();
                }
                else
                {
                    cbLieferZFNessesary.Checked = true;
                }
            }
        }
        ///<summary>frmAuftrag_Fast / cbLadNrRequire_KeyDown</summary>
        ///<remarks></remarks>
        private void cbLadNrRequire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbLadNrRequire.Checked)
                {
                    cbLadNrRequire.Checked = false;
                }
                else
                {
                    cbLadNrRequire.Checked = true;
                }
            }
        }
        ///<summary>frmAuftrag_Fast / cbPrio_KeyDown</summary>
        ///<remarks></remarks>
        private void cbPrio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbPrio.Checked)
                {
                    cbPrio.Checked = false;
                }
                else
                {
                    cbPrio.Checked = true;
                }
            }
        }
        ///<summary>frmAuftrag_Fast / tsbtnCopyArtikel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnCopyArtikel_Click(object sender, EventArgs e)
        {
            bUpdateArtikel = false;
            //SetArtikelEingabeFelderEnabled(true);
            clsArtikel ArtZP = this.Auftrag.AuftragPos.Artikel.Copy();
            ClearArtikelEingabefelder();
            this.Auftrag.AuftragPos.Artikel = ArtZP;
            tbGArtSearch.Focus();
            SetArtikelDatenToFrm(true);
            //this.tsbtnArtikelSave.Enabled = true;
            //this.tsbtnArtikelDelete.Enabled = true;
        }
        ///<summary>frmAuftrag_Fast / frmAuftrag_Fast_KeyDown</summary>
        ///<remarks></remarks>
        private void frmAuftrag_Fast_KeyDown(object sender, KeyEventArgs e)
        {
            DoKeyDown(e);
        }
        ///<summary>frmAuftrag_Fast / DoKeyDown</summary>
        ///<remarks></remarks>
        private void DoKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //Speichern Auftrag 
                case Keys.F1:
                    break;
                //Speichern Artikel
                case Keys.F2:
                    AssignValueArtikel();
                    break;
            }
        }
        ///<summary>frmAuftrag_Fast / scAuftragEdit_KeyDown</summary>
        ///<remarks></remarks>
        private void scAuftragEdit_KeyDown(object sender, KeyEventArgs e)
        {
            DoKeyDown(e);
        }
        ///<summary>frmAuftrag_Fast / tsbtnDeleteArtikel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDeleteArtikel_Click(object sender, EventArgs e)
        {
            if (clsMessages.Artikel_DeleteDatenSatz())
            {
                Auftrag.AuftragPos.Artikel.DeleteArtikelByIDDISPO();
                Auftrag.AuftragPos.Fill();
                InitDGV();
            }
        }
        ///<summary>frmAuftrag_Fast / tsbtnDeleteAuftrag_Click</summary>
        ///<remarks></remarks>
        private void tsbtnDeleteAuftrag_Click(object sender, EventArgs e)
        {
            if (clsMessages.DeleteAllgemein())
            {
                this.Auftrag.DeleteAuftragKomplett();
                ClearAuftragDaten();
                LoadLastAuftragsdaten();
                this._ctrAuftrag.InitDGV();
            }
        }
        ///<summary>frmAuftrag_Fast / tsbtnDeleteAuftrag_Click</summary>
        ///<remarks>Direkt zu einem bestimmten Auftrag springen</remarks>
        private void tsbtnJumpToOrder_Click(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            Decimal.TryParse(tstbOrderIDToJump.Text, out decTmp);
            if (decTmp > 0)
            {
                //CHeck ob der Auftrag Existiert
                if (clsAuftrag.CheckAuftragIDExistByAB(GL_User, decTmp, this._ctrMenu._frmMain.system.AbBereich.ID))
                {
                    this.Auftrag.ID = clsAuftrag.GetAuftragIDbyANrAndAbBereich(this.GL_User, decTmp, this._ctrMenu._frmMain.system.AbBereich.ID);
                    this.Auftrag.Fill();
                    this.Auftrag.AuftragPos.AuftragTableID = this.Auftrag.ID;
                    this.Auftrag.AuftragPos.FillByAuftragID();
                    //Daten auf die Form
                    SetAuftragDatenToFrm();
                    //Artikel laden
                    InitDGV();
                }
                else
                {
                    clsMessages.Auftrag_AuftragNummerExistiertNicht();
                }
                tstbOrderIDToJump.Text = string.Empty;
            }
        }
        ///<summary>frmAuftrag_Fast / tsbtnStatusInfo_Click</summary>
        ///<remarks>Öffnet die Statuslengende</remarks>
        private void tsbtnStatusInfo_Click(object sender, EventArgs e)
        {
            this._ctrAuftrag.OpenFrmStatusLegend();
        }
        ///<summary>frmAuftrag_Fast / tsbtnEditOrder_Click</summary>
        ///<remarks>Hierüber wird der angezeigte Auftrag zur Bearbeitung freigegeben</remarks>
        private void tsbtnEditOrder_Click(object sender, EventArgs e)
        {
            tsbtnAuftragClose.Visible = true;
            tsbtnAuftragEdit.Visible = false;
            SetAccessToOrder(true, true);
            SetCtrByTermineEnabled();
        }
        ///<summary>frmAuftrag_Fast / SetCtrByTermineEnabled</summary>
        ///<remarks>Hierüber wird der angezeigte Auftrag zur Bearbeitung freigegeben</remarks>
        private void SetCtrByTermineEnabled()
        {
            //Ladetermin
            dtpLadeTermin.Enabled = cbLadeTerminIsSet.Checked;
            nudLadeZFHour.Enabled = cbLadeZFIsSet.Checked;
            nudLadeZFMin.Enabled = cbLadeZFIsSet.Checked;

            //VSB
            dtpVSB.Enabled = cbVSB.Checked;
            nudVonKW.Enabled = cbVSB.Checked;

            //Liefertermin
            dtpLieferTermin.Enabled = cbLieferTerminIsSet.Checked;
            nudBisKW.Enabled = cbLieferTerminIsSet.Checked;
            nudLieferZFHour.Enabled = cbLieferZFIsSet.Checked;
            nudLieferZFMin.Enabled = cbLieferZFIsSet.Checked;
        }
        ///<summary>frmAuftrag_Fast / SetAccessToOrder</summary>
        ///<remarks></remarks>
        private void SetAccessToOrder(bool bAccessOrder, bool bAccessArtikel)
        {
            SetAuftragEingabefelderEnabled(bAccessOrder);
            SetAuftragMenueEnabled(bAccessOrder);
            SetArtikelMenuEnabled(bAccessArtikel);
            SetArtikelEingabeFelderEnabled(false);
            dgvArtikel.Enabled = bAccessArtikel;
        }
        ///<summary>frmAuftrag_Fast / SetOrderMenueEnabled</summary>
        ///<remarks></remarks>
        private void SetAuftragMenueEnabled(bool bEnabled)
        {
            tsbtnAuftragNeu.Enabled = (!bEnabled);
            tsbtnAuftragEdit.Enabled = (!bEnabled);
            SetBackNextInMenuEnable((!bEnabled));

            tsbtnAuftragDelete.Enabled = bEnabled;
            tsbtnAuftragRoute.Enabled = bEnabled;
            tsbtnAuftragScan.Enabled = bEnabled;
            tsbtnAuftragSpeichern.Enabled = bEnabled;
        }
        ///<summary>frmAuftrag_Fast / SetOrderMenueEnabled</summary>
        ///<remarks></remarks>
        private void tsbtnAuftragClose_Click(object sender, EventArgs e)
        {
            tsbtnAuftragClose.Visible = false;
            tsbtnAuftragEdit.Visible = true;
            SetAccessToOrder(false, false);
            bUpdateOrder = false;
            this._ctrAuftrag.InitDGV();
        }
        ///<summary>frmAuftrag_Fast / btnManVersender_Click</summary>
        ///<remarks></remarks>
        private void btnManVersender_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>frmAuftrag_Fast / btnManEmpfaenger_Click</summary>
        ///<remarks></remarks>
        private void btnManEmpfaenger_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>frmAuftrag_Fast / OpenManInput</summary>
        ///<remarks></remarks>
        private void OpenManAdrInput(Button myBtn)
        {
            Int32 iAdrArtID = -1;
            Int32.TryParse(myBtn.Tag.ToString(), out iAdrArtID);
            if (iAdrArtID > -1)
            {
                bUpdateOrder = (this.Auftrag.ID > 0);
                //Falls der der Auftrag noch nicht gespeichert ist,so muss dies hier 
                //geschehen, da sonst keine AuftragID EingangTableID vorhanden ist
                if (this.Auftrag.ID == 0)
                {
                    AssignValueAuftrag();
                    AssignValueAuftragPos();
                }

                if (this.Auftrag.ID > 0)
                {
                    manAdrArt = string.Empty;
                    switch (iAdrArtID)
                    {
                        case 1:
                            manAdrArt = "Versender";
                            break;
                        case 2:
                            manAdrArt = "Auftraggeber";
                            break;
                        case 3:
                            manAdrArt = "Empfänger";
                            break;
                        case 4:
                            manAdrArt = "Entladestelle";
                            break;
                        case 5:
                            manAdrArt = "Spedition/Transportunternehmer";
                            break;
                    }

                    this.Auftrag.AdrManuell.TableName = clsAuftrag.const_DBTableName;
                    this.Auftrag.AdrManuell.TableID = this.Auftrag.ID;
                    this.Auftrag.AdrManuell.AdrArtID = iAdrArtID;
                    this.Auftrag.AdrManuell.FillbyTableAndAdrArtID();

                    this._ctrADRManAdd = new ctrADRManAdd();
                    this._ctrADRManAdd._frmAuftragFast = this;
                    this._ctrMenu.OpenFrmTMP(this._ctrADRManAdd);
                }
            }
        }
        ///<summary>frmAuftrag_Fast / tbOrderID_MouseHover</summary>
        ///<remarks></remarks>
        private void tbOrderID_MouseHover(object sender, EventArgs e)
        {
            ToolTip info = new ToolTip();
            info.SetToolTip(this, this.Auftrag.MouseOverInfo);

        }
        ///<summary>frmAuftrag_Fast / label5_MouseHover</summary>
        ///<remarks></remarks>
        private void label5_MouseHover(object sender, EventArgs e)
        {
            ToolTip info = new ToolTip();
            info.SetToolTip(this, this.Auftrag.MouseOverInfo);
        }
        ///<summary>frmAuftrag_Fast / cbLadeZFIsSet_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbLadeZFIsSet_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbLadeZFIsSet.Checked)
            {
                nudLadeZFHour.Value = 0;
                nudLadeZFMin.Value = 0;
            }
            nudLadeZFHour.Enabled = cbLadeZFIsSet.Checked;
            nudLadeZFMin.Enabled = cbLadeZFIsSet.Checked;
        }
        ///<summary>frmAuftrag_Fast / cbLadeTerminIsSet_KeyDown</summary>
        ///<remarks></remarks>
        private void cbLadeTerminIsSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbLadeTerminIsSet.Checked)
                {
                    cbLadeTerminIsSet.Checked = false;
                    dtpLadeTermin.Focus();
                }
                else
                {
                    cbLadeTerminIsSet.Checked = true;
                    cbVSB.Focus();
                }
            }
        }
        ///<summary>frmAuftrag_Fast / cbLadeZFNessesary_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbLadeZFNessesary_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLadeZFNessesary.Checked)
            {
                //Zeitfenster erforderlich
                cbLadeZFIsSet.Enabled = true;
                cbLadeZFIsSet.Focus();
            }
            else
            {
                //Zeitfenster nicht erforderlich 
                cbLadeZFIsSet.Enabled = false;
            }
        }

        private void nudLadeZFHour_ValueChanged(object sender, EventArgs e)
        {
            string strCheck = nudLadeZFHour.Value.ToString();
        }








    }
}

