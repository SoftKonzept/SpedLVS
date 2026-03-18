using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrArtDetails : UserControl
    {
        internal clsAuftrag Auftrag;
        internal ctrADRManAdd _ctrADRManAdd;
        //internal clsGut Gut;
        public Globals._GL_USER GL_User;
        internal DataTable dtCheck = new DataTable();
        public bool do_ChangesAuftragdetails = false;
        public bool do_ChangesArtikel = false;
        internal bool ctrForPrint = false;
        internal bool bo_Neutralitaet = false;
        public string strZF = String.Empty;
        internal string manAdrArt = string.Empty;
        public string strLadenummer = String.Empty;
        public DataTable KDTable = new DataTable();
        public DataTable VTable = new DataTable();
        public DataTable ETable = new DataTable();
        public DataTable NTable = new DataTable();

        public DataTable tempTableKD = new DataTable();
        public DataTable tempTableV = new DataTable();
        public DataTable tempTableE = new DataTable();
        public DataTable tempTableN = new DataTable();
        internal string viewName = "AuftragDetailsArtikel";
        public ctrPrint _ctrPrint;
        public ctrMenu _ctrMenu;

        /********************************
         * SearchButton
         * 1: Auftraggeber
         * 2: Versender
         * 3: Empfaenger
         * 4: neutral Versender
         * 5: neutral Empfaenger
         * *****************************/
        public Int32 SearchButton = 0;

        internal decimal ADR_ID_A = 0;
        internal decimal ADR_ID_E = 0;
        internal decimal ADR_ID_V = 0;
        public decimal ADR_ID_nV = 0;
        public decimal ADR_ID_nE = 0;

        public string Notiz;
        DataTable dtAuftragsdaten = new DataTable("Auftragsdaten");
        DataTable dtArtikel = new DataTable("Artikel");
        public bool boCreatOwnDoc = false;

        ///<summary>ctrArtDetails/ ctrArtDetails</summary>
        ///<remarks></remarks>
        public ctrArtDetails()
        {
            InitializeComponent();

        }
        ///<summary>ctrArtDetails/ ctrArtDetails_Load</summary>
        ///<remarks></remarks>
        private void ctrArtDetails_Load(object sender, EventArgs e)
        {
            if (this.Auftrag.ID != 0)
            {
                //beim Laden immer ohne Neutralität - muss extra aktiviert werden
                cbNeutralitaet.Checked = false;
                EnableNeutraleADRFelder(false);
                tbArtID.Enabled = false;
                Notiz = tbNotiz.Text;
                InitCBRelation();
                this.Auftrag.InitADRinClass();
                this.tabDaten.SelectedTab = tabPageAuftragsdaten;
                InitTabPage(this.tabDaten.SelectedTab.Name.ToString());
            }
            else
            {
                //Auftrag = 0 keine Daten zu laden
                //ENDE?
                //MEssage
            }
            do_ChangesAuftragdetails = false;
            do_ChangesArtikel = false;
        }
        ///<summary>ctrArtDetails/ tabDaten_SelectedIndexChanged</summary>
        ///<remarks></remarks>
        private void tabDaten_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitTabPage(tabDaten.SelectedTab.Name.ToString());
        }
        ///<summary>ctrArtDetails/ InitTabPage</summary>
        ///<remarks></remarks>
        private void InitTabPage(string strTabName)
        {
            switch (strTabName)
            {
                case "tabPageAuftragsdaten":
                    InitAuftragsdatenToFrm();
                    break;
                case "tabPageArtikeldaten":
                    Functions.InitComboViews(_ctrMenu._frmMain.GL_System, ref this.tsbcViews, viewName);
                    InitDGV();
                    break;
            }
        }
        ///<summary>ctrArtDetails/ btnSearchA_Click_1</summary>
        ///<remarks></remarks>
        private void btnSearchA_Click_1(object sender, EventArgs e)
        {
            boCreatOwnDoc = false;
            SearchButton = 1;
            _ctrMenu.OpenADRSearch(this);
            boCreatOwnDoc = true;
        }
        ///<summary>ctrArtDetails/ btnSearchV_Click</summary>
        ///<remarks></remarks>
        private void btnSearchV_Click(object sender, EventArgs e)
        {
            SearchButton = 2;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrArtDetails/ btnSearchE_Click</summary>
        ///<remarks></remarks>
        private void btnSearchE_Click(object sender, EventArgs e)
        {
            SearchButton = 3;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrArtDetails/ button1_Click</summary>
        ///<remarks></remarks>
        private void button1_Click(object sender, EventArgs e)
        {
            SearchButton = 4;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrArtDetails/ button2_Click</summary>
        ///<remarks></remarks>
        private void button2_Click(object sender, EventArgs e)
        {
            SearchButton = 5;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrArtDetails/ InitCBRelation</summary>
        ///<remarks></remarks>
        private void InitCBRelation()
        {
            //cbRelation.DataSource = Enum.GetNames(typeof(Globals.enumRelation));
            DataTable dtRalationen = clsRelationen.GetRelationsliste();
            AddLeerZeileToTableRelation(ref dtRalationen);
            cbRelation.DataSource = dtRalationen;
            cbRelation.DisplayMember = "Relation";
            cbRelation.ValueMember = "Relation";
            cbRelation.SelectedValue = "";

            string test = cbRelation.SelectedValue.ToString();
        }
        ///<summary>ctrArtDetails/ cbNeutralitaet_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbNeutralitaet_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNeutralitaet.Checked == true)
            {
                bo_Neutralitaet = true;
                EnableNeutraleADRFelder(true);
            }
            else
            {
                bo_Neutralitaet = false;
                EnableNeutraleADRFelder(false);
            }
        }
        ///<summary>ctrArtDetails/ EnableNeutraleADRFelder</summary>
        ///<remarks>Neutrale Adressfelder aktivieren/deaktivieren</remarks>
        private void EnableNeutraleADRFelder(bool bo_Enable)
        {
            btnNeutrE.Enabled = bo_Enable;
            btnNeutrV.Enabled = bo_Enable;
            tbSearchNE.Enabled = bo_Enable;
            tbSearchNV.Enabled = bo_Enable;
            tbNEmpfaenger.Enabled = bo_Enable;
            tbNVersender.Enabled = bo_Enable;
            btnNeutrADRDelete.Enabled = bo_Enable;
        }
        ///<summary>ctrArtDetails/ InitEigDoc</summary>
        ///<remarks></remarks>
        public void InitEigDoc(bool boEigDoc)
        {
            EnableNeutraleADRFelder(boEigDoc);
            if (boEigDoc)
            {
                cbNeutralitaet.Text = "Adresse für eigenes Dokument";
                cbNeutralitaet.Checked = true;
                btnNeutrADRDelete.Enabled = false; ;
            }
            else
            {
                cbNeutralitaet.Text = "Adresse für neutrales Dokument";
                btnNeutrADRDelete.Enabled = false;
                cbNeutralitaet.Checked = false;
                ADR_ID_nE = 0;
                ADR_ID_nV = 0;
                tbSearchNE.Text = string.Empty;
                tbNEmpfaenger.Text = string.Empty;
                tbSearchNV.Text = string.Empty;
                tbNVersender.Text = string.Empty;
            }
        }
        ///<summary>ctrArtDetails/ cbLadeTerminIsSet_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbLadeTerminIsSet_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLadeTerminIsSet.Checked)
            {
                dtpLadeTermin.Value = DateTime.Today;
            }
            dtpLadeTermin.Enabled = cbLadeTerminIsSet.Checked;
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ cbLadeZFNessesary_CheckedChanged</summary>
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
        ///<summary>ctrArtDetails/ cbLadeZFIsSet_CheckedChanged</summary>
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
        ///<summary>ctrArtDetails/ cbLieferTerminIsSet_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbLieferTerminIsSet_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLieferTerminIsSet.Checked)
            {
                dtpLieferTermin.Value = DateTime.Today.AddDays(1);
            }
            dtpLieferTermin.Enabled = cbLieferTerminIsSet.Checked;
            nudBisKW.Enabled = cbLieferTerminIsSet.Checked;
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ cbLieferZFNessesary_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbLieferZFNessesary_CheckedChanged(object sender, EventArgs e)
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
        ///<summary>ctrArtDetails/ cbLieferZFIsSet_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbLieferZFIsSet_CheckedChanged(object sender, EventArgs e)
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
        /*******************************************************************************
         *              INIT Auftragdetails
         * *****************************************************************************/
        ///<summary>ctrArtDetails/ SetKDRecAfterADRSearch</summary>
        ///<remarks></remarks>
        public void SetKDRecAfterADRSearch(decimal ADR_ID)
        {
            string strE = string.Empty;
            string strMC = string.Empty;
            string ADRBezeichnung = string.Empty;

            strE = clsADR.GetADRString(ADR_ID);
            strMC = clsADR.GetMatchCodeByID(ADR_ID, GL_User.User_ID);

            switch (SearchButton)
            {
                case 1:
                    if (ADR_ID_A != ADR_ID)
                    {
                        ADRBezeichnung = "Auftraggeber";
                        ADR_ID_A = ADR_ID;
                        tbSearchA.Text = strMC;
                        tbAuftraggeber.Text = strE;
                        this.Auftrag.KD_ID = ADR_ID_A;
                        this.Auftrag.Update();
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, clsAuftrag.const_DBTableName, this.Auftrag.ID, clsADRMan.cont_AdrArtID_Auftraggeber);
                    }
                    break;

                case 2:
                    if (ADR_ID_V != ADR_ID)
                    {
                        ADRBezeichnung = "Versender";
                        clsAuftrag.updateADR_ID(this.Auftrag.ID, "Versender", ADR_ID);
                        ADR_ID_V = ADR_ID;
                        tbSearchV.Text = strMC;
                        tbVersender.Text = strE;
                        this.Auftrag.B_ID = ADR_ID_V;
                        this.Auftrag.Update();
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, clsAuftrag.const_DBTableName, this.Auftrag.ID, clsADRMan.cont_AdrArtID_Versender);
                    }
                    break;

                case 3:
                    if (ADR_ID_E != ADR_ID)
                    {
                        ADRBezeichnung = "Empfänger";
                        clsAuftrag.updateADR_ID(this.Auftrag.ID, "Empfänger", ADR_ID);
                        ADR_ID_E = ADR_ID;
                        tbSearchE.Text = strMC;
                        tbEmpfaenger.Text = strE;
                        this.Auftrag.E_ID = ADR_ID_E;
                        this.Auftrag.Update();
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, clsAuftrag.const_DBTableName, this.Auftrag.ID, clsADRMan.cont_AdrArtID_Empfaenger);
                    }
                    break;

                case 4:
                    if (ADR_ID_nV != ADR_ID)
                    {
                        ADRBezeichnung = "nVersender";
                        //clsAuftrag.updateADR_ID(Auftrag, "nVersender", ADR_ID);
                        ADR_ID_nV = ADR_ID;
                        tbSearchNV.Text = strMC;
                        tbNVersender.Text = strE;
                        if ((_ctrPrint != null) && (_ctrPrint.cbNeutrLfSchein.Checked))
                        {
                            _ctrPrint.SetADRToFrm(ADR_ID, true);
                        }
                        this.Auftrag.nB_ID = ADR_ID_nV;
                        this.Auftrag.Update();
                    }
                    break;

                case 5:
                    if (ADR_ID_nE != ADR_ID)
                    {
                        ADRBezeichnung = "nEmpfänger";
                        //clsAuftrag.updateADR_ID(Auftrag, "nEmpfänger", ADR_ID);
                        ADR_ID_nE = ADR_ID;
                        tbSearchNE.Text = strMC;
                        tbNEmpfaenger.Text = strE;
                        if ((_ctrPrint != null) && (_ctrPrint.cbNeutrLfSchein.Checked))
                        {
                            _ctrPrint.SetADRToFrm(ADR_ID, false);
                        }
                        this.Auftrag.nE_ID = ADR_ID_nE;
                        this.Auftrag.Update();
                    }
                    break;
            }

            //if (!boCreatOwnDoc)
            //{
            //    clsAuftrag.updateADR_ID(this.Auftrag.ID, ADRBezeichnung, ADR_ID);
            //}

        }
        ///<summary>ctrArtDetails/ SetNeutraleADRtoFrm</summary>
        ///<remarks></remarks>
        public void SetManAdrToCtr()
        {

        }
        //
        private void AddLeerZeileToTableRelation(ref DataTable dt)
        {
            DataRow row = dt.NewRow();
            row["ID"] = 1;
            row["Relation"] = "";
            dt.Rows.Add(row);
        }
        ///<summary>ctrArtDetails/ SetNeutraleADRtoFrm</summary>
        ///<remarks></remarks>
        public void SetNeutraleADRtoFrm()
        {
            decimal decVersender = clsAuftrag.GetNeutraleADR(this.Auftrag.ID, "Versender");
            //Versender
            tbNVersender.Text = clsADR.GetADRString(decVersender);
            tbSearchNV.Text = clsADR.GetMatchCodeByID(decVersender, GL_User.User_ID);
            tbSearchNV.Text = tbSearchNV.Text.ToString().Trim();
            cbNeutralitaet.Checked = true;

            decimal decEm = clsAuftrag.GetNeutraleADR(this.Auftrag.ID, "Empfaenger");
            tbNEmpfaenger.Text = clsADR.GetADRString(decEm);
            tbSearchNE.Text = clsADR.GetMatchCodeByID(decEm, GL_User.User_ID);
            tbSearchNE.Text = tbSearchNE.Text.ToString().Trim();
            cbNeutralitaet.Checked = true;
        }
        ///<summary>ctrArtDetails/ InitAuftragsdatenToFrm</summary>
        ///<remarks></remarks>
        public void InitAuftragsdatenToFrm()
        {
            if (boCreatOwnDoc)
            {
                ADR_ID_nV = 0;
                ADR_ID_nE = 0;
            }

            if (this.Auftrag != null)
            {
                SetADRDatenToCtr();
                //Auftragsdaten
                Functions.SetComboToSelecetedValue(ref cbRelation, this.Auftrag.Relation);
                tbLadeNr.Text = this.Auftrag.AuftragPos.Ladenummer;
                tbNotiz.Text = this.Auftrag.AuftragPos.Notiz;

                //VSB
                cbVSB.Checked = (Auftrag.AuftragPos.VSB > (DateTime)Globals.DefaultDateTimeMinValue);
                Functions.SetDateTimePickerValue(ref dtpVSB, Auftrag.AuftragPos.VSB);
                nudVonKW.Value = (decimal)Auftrag.AuftragPos.vKW;
                dtpVSB.Enabled = cbVSB.Checked;
                nudVonKW.Enabled = cbVSB.Checked;

                //Ladetermin
                cbLadeTerminIsSet.Checked = (Auftrag.AuftragPos.LadeTermin > (DateTime)Globals.DefaultDateTimeMinValue);
                Functions.SetDateTimePickerValue(ref dtpLadeTermin, Auftrag.AuftragPos.LadeTermin);
                cbLadeZFNessesary.Checked = Auftrag.AuftragPos.LadeZFRequire;
                cbLadeZFIsSet.Checked = (Auftrag.AuftragPos.LadeZF > (DateTime)Globals.DefaultDateTimeMinValue);
                nudLadeZFHour.Value = (decimal)Auftrag.AuftragPos.LadeZF.Hour;
                nudLadeZFMin.Value = (decimal)Auftrag.AuftragPos.LadeZF.Minute;
                dtpLadeTermin.Enabled = cbLieferTerminIsSet.Checked;
                nudLadeZFHour.Enabled = cbLadeZFIsSet.Checked;
                nudLadeZFMin.Enabled = cbLadeZFIsSet.Checked;

                //Liefertermin
                cbLieferTerminIsSet.Checked = (Auftrag.AuftragPos.LieferTermin > (DateTime)Globals.DefaultDateTimeMinValue);
                Functions.SetDateTimePickerValue(ref dtpLieferTermin, Auftrag.AuftragPos.LieferTermin);
                cbLieferZFNessesary.Checked = Auftrag.AuftragPos.LieferZFRequire;
                cbLieferZFIsSet.Checked = (Auftrag.AuftragPos.LieferZF > (DateTime)Globals.DefaultDateTimeMinValue);
                nudLieferZFHour.Value = (decimal)Auftrag.AuftragPos.LieferZF.Hour;
                nudLieferZFMin.Value = (decimal)Auftrag.AuftragPos.LieferZF.Minute;
                nudBisKW.Value = (decimal)Auftrag.AuftragPos.bKW;
                dtpLieferTermin.Enabled = cbLieferTerminIsSet.Checked;
                nudBisKW.Enabled = cbLieferTerminIsSet.Checked;
                nudLieferZFHour.Enabled = cbLieferZFIsSet.Checked;
                nudLieferZFMin.Enabled = cbLieferZFIsSet.Checked;
            }
            do_ChangesAuftragdetails = false;
        }
        ///<summary>ctrArtDetails/ SetADRDatenToCtr</summary>
        ///<remarks></remarks>
        public void SetADRDatenToCtr()
        {
            //ADR
            //Auftraggeber
            ADR_ID_A = this.Auftrag.KD_ID;
            tbAuftraggeber.Text = this.Auftrag.adrAuftraggeber.ADRStringShort;
            tbSearchA.Text = this.Auftrag.adrAuftraggeber.ViewID;
            //Versender
            if (this.Auftrag.B_ID > 0)
            {
                ADR_ID_V = this.Auftrag.B_ID;
                tbVersender.Text = this.Auftrag.adrBS.ADRStringShort;
                tbSearchV.Text = this.Auftrag.adrBS.ViewID;
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
                ADR_ID_E = this.Auftrag.E_ID;
                tbEmpfaenger.Text = this.Auftrag.adrES.ADRStringShort;
                tbSearchE.Text = this.Auftrag.adrES.ViewID;
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
        }
        ///<summary>ctrArtDetails/ SetAuftragPosGewichtToFrm</summary>
        ///<remarks></remarks>
        private void SetAuftragPosGewichtToFrm()
        {
            decimal decTmpGewicht = 0;
            if (this.Auftrag.AuftragPos.dtAuftrPosArtikel.Rows.Count > 0)
            {
                object obGewicht = new object();
                obGewicht = this.Auftrag.AuftragPos.dtAuftrPosArtikel.Compute("SUM(Brutto)", string.Empty);
                Decimal.TryParse(obGewicht.ToString(), out decTmpGewicht);
            }
            tsbtnGesamtgewicht.Text = Functions.FormatDecimal(decTmpGewicht);
        }
        ///<summary>ctrArtDetails/ InitDGV</summary>
        ///<remarks></remarks>
        private void InitDGV()
        {
            this.dgvArtikel.DataSource = this.Auftrag.AuftragPos.dtAuftrPosArtikel;
            Functions.setView(ref this.Auftrag.AuftragPos.dtAuftrPosArtikel, ref dgvArtikel, viewName, tsbcViews.SelectedItem.ToString(), this._ctrMenu._frmMain.GL_System, false);
            //for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            //{
            //    string ColName = this.dgv.Columns[i].Name;
            //    switch (ColName)
            //    { 
            //        case "Anzahl":
            //        case "gemGewicht":
            //        case "Werksnummer":
            //        case "AuftragPosTableID":
            //            this.dgv.Columns[i].Visible = false;
            //            break;
            //        default:
            //            this.dgv.Columns[i].Visible = true;
            //            break;
            //    }
            //}
            //ersten ARtikel setzen
            this.dgvArtikel.BestFitColumns();
            if (this.dgvArtikel.Rows.Count > 0)
            {
                this.dgvArtikel.Rows[0].IsSelected = true;
                this.dgvArtikel.Rows[0].IsCurrent = true;
                decimal decTmp = 0;
                if (this.dgvArtikel.Columns.Contains("ID"))
                {
                    decimal.TryParse(this.dgvArtikel.Rows[0].Cells["ID"].Value.ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.Auftrag.AuftragPos.Artikel.ID = decTmp;
                        this.Auftrag.AuftragPos.Artikel.GetArtikeldatenByTableID();
                        ClearArtikelDatenInputFields();
                        SetArtikelDatenToFrm();
                        do_ChangesArtikel = false;
                        CheckWarning();
                    }
                }
            }
            SetAuftragPosGewichtToFrm();
        }
        ///<summary>ctrArtDetails/ SetArtikelDatenToFrm</summary>
        ///<remarks></remarks>
        private void SetArtikelDatenToFrm()
        {
            tbArtID.Text = this.Auftrag.AuftragPos.Artikel.ID.ToString();
            tbGut.Text = this.Auftrag.AuftragPos.Artikel.Gut;
            tbGutZusatz.Text = this.Auftrag.AuftragPos.Artikel.GutZusatz;
            tbDicke.Text = Functions.FormatDecimal(this.Auftrag.AuftragPos.Artikel.Dicke);
            tbBreite.Text = Functions.FormatDecimal(this.Auftrag.AuftragPos.Artikel.Breite);
            tbLaenge.Text = Functions.FormatDecimal(this.Auftrag.AuftragPos.Artikel.Laenge);
            tbHoehe.Text = Functions.FormatDecimal(this.Auftrag.AuftragPos.Artikel.Hoehe);
            tbgemGwicht.Text = Functions.FormatDecimal(this.Auftrag.AuftragPos.Artikel.gemGewicht);
            tbNetto.Text = Functions.FormatDecimal(this.Auftrag.AuftragPos.Artikel.Netto);
            tbBrutto.Text = Functions.FormatDecimal(this.Auftrag.AuftragPos.Artikel.Brutto);
            tbAnzahl.Text = this.Auftrag.AuftragPos.Artikel.Anzahl.ToString();

            tbWerksnummer.Text = this.Auftrag.AuftragPos.Artikel.Werksnummer;
            tbCP.Text = this.Auftrag.AuftragPos.Artikel.Produktionsnummer;
            tbPos.Text = this.Auftrag.AuftragPos.Artikel.Position;
            tbCharge.Text = this.Auftrag.AuftragPos.Artikel.Charge;
            tbBestellnummer.Text = this.Auftrag.AuftragPos.Artikel.Bestellnummer;
            tbexMaterialnummer.Text = this.Auftrag.AuftragPos.Artikel.exMaterialnummer;
            tbexBezeichung.Text = this.Auftrag.AuftragPos.Artikel.exBezeichnung;
            do_ChangesArtikel = false;
        }
        ///<summary>ctrArtDetails/ ClearArtikelDatenInputFields</summary>
        ///<remarks></remarks>
        private void ClearArtikelDatenInputFields()
        {
            tbArtID.Text = string.Empty;
            tbGut.Text = string.Empty;
            tbDicke.Text = string.Empty;
            tbBreite.Text = string.Empty;
            tbLaenge.Text = string.Empty;
            tbHoehe.Text = string.Empty;
            tbgemGwicht.Text = string.Empty;
            tbNetto.Text = string.Empty;
            tbAnzahl.Text = string.Empty;

            tbWerksnummer.Text = string.Empty;
            tbCP.Text = string.Empty;
            tbPos.Text = string.Empty;
            tbCharge.Text = string.Empty;
            tbBestellnummer.Text = string.Empty;
            tbexMaterialnummer.Text = string.Empty;
            tbexBezeichung.Text = string.Empty;
        }

        ///<summary>ctrArtDetails/ cbVSB_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbVSB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVSB.Checked)
            {
                dtpVSB.Value = DateTime.Today;
            }
            dtpVSB.Enabled = cbVSB.Checked;
            nudVonKW.Enabled = cbVSB.Checked;
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ CheckEingabe</summary>
        ///<remarks></remarks>
        private bool CheckEingabe()
        {
            bool EingabeOK = true;
            string strHelp = string.Empty;
            Int32 result = 0;
            decimal decResult = 0.0m;

            //Güterart
            if (tbGut.Text == "")
            {
                EingabeOK = false;
                strHelp = strHelp + "Güterart / Warengruppe fehlt \n\r";
            }
            //ME
            if (!Int32.TryParse(tbAnzahl.Text, out result))
            {
                EingabeOK = false;
                strHelp = strHelp + "Anzahl hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbAnzahl.Text = result.ToString();
                result = 0;
            }
            //Dicke
            if (!decimal.TryParse(tbDicke.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Dicke hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbDicke.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Breite
            if (!decimal.TryParse(tbBreite.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Breite hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbBreite.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Länge
            if (!decimal.TryParse(tbLaenge.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Länge hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbLaenge.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Höhe
            if (!decimal.TryParse(tbHoehe.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Höhe hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbHoehe.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //gemGewicht
            if (!decimal.TryParse(tbgemGwicht.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "gemGewicht hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbgemGwicht.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Netto
            if (!decimal.TryParse(tbNetto.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Netto hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbNetto.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            //Brutto
            if (!decimal.TryParse(tbBrutto.Text, out decResult))
            {
                EingabeOK = false;
                strHelp = strHelp + "Brutto hat das falsche Eingabeformat \n\r";
            }
            else
            {
                tbBrutto.Text = Functions.FormatDecimal(decResult);
                decResult = 0.0m;
            }
            if (!EingabeOK)
            {
                MessageBox.Show(strHelp, "Achtung");
            }
            return EingabeOK;
        }
        ///<summary>ctrArtDetails/ AssignValue</summary>
        ///<remarks></remarks>
        private void AssignValue()
        {
            this.Auftrag.AuftragPos.Artikel = new clsArtikel();
            this.Auftrag.AuftragPos.Artikel.BenutzerID = GL_User.User_ID;

            decimal decTmp = 0;
            Decimal.TryParse(tbArtID.Text, out decTmp);
            this.Auftrag.AuftragPos.Artikel.ID = decTmp;
            this.Auftrag.AuftragPos.Artikel.AuftragID = this.Auftrag.ANr;
            this.Auftrag.AuftragPos.Artikel.AuftragPos = this.Auftrag.AuftragPos.AuftragPos;
            this.Auftrag.AuftragPos.Artikel.GArtID = this.Auftrag.AuftragPos.Artikel.GArt.ID;
            this.Auftrag.AuftragPos.Artikel.GutZusatz = tbGutZusatz.Text;
            this.Auftrag.AuftragPos.Artikel.Dicke = Convert.ToDecimal(tbDicke.Text);
            this.Auftrag.AuftragPos.Artikel.Breite = Convert.ToDecimal(tbBreite.Text);
            this.Auftrag.AuftragPos.Artikel.Laenge = Convert.ToDecimal(tbLaenge.Text);
            this.Auftrag.AuftragPos.Artikel.Hoehe = Convert.ToDecimal(tbHoehe.Text);
            this.Auftrag.AuftragPos.Artikel.Anzahl = Convert.ToInt32(tbAnzahl.Text);
            this.Auftrag.AuftragPos.Artikel.gemGewicht = Convert.ToDecimal(tbgemGwicht.Text);
            this.Auftrag.AuftragPos.Artikel.Netto = Convert.ToDecimal(tbNetto.Text);
            this.Auftrag.AuftragPos.Artikel.Brutto = Convert.ToDecimal(tbBrutto.Text);
            this.Auftrag.AuftragPos.Artikel.Werksnummer = tbWerksnummer.Text;
            this.Auftrag.AuftragPos.Artikel.Produktionsnummer = tbCP.Text;
            this.Auftrag.AuftragPos.Artikel.exBezeichnung = tbexBezeichung.Text;
            this.Auftrag.AuftragPos.Artikel.Charge = tbCharge.Text;
            this.Auftrag.AuftragPos.Artikel.Bestellnummer = tbBestellnummer.Text;
            this.Auftrag.AuftragPos.Artikel.exMaterialnummer = tbexMaterialnummer.Text;
            this.Auftrag.AuftragPos.Artikel.Position = tbPos.Text;

            if (this.Auftrag.AuftragPos.Artikel.ID == 0)
            {
                //Neueintrag
            }
            else
            {
                this.Auftrag.AuftragPos.Artikel.UpdateArtikelALLDispo();
            }
        }
        ///<summary>ctrArtDetails/ tsbtnSave_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            SaveArtDaten();
            SetAuftragPosGewichtToFrm();
        }
        ///<summary>ctrArtDetails/ SaveArtDaten</summary>
        ///<remarks></remarks>
        public void SaveArtDaten()
        {
            if (CheckEingabe())
            {
                AssignValue();
                //Feldreset wird im DGV - SelectionChange durch Init DGV durchgeführt und alle Daten neu geladen
                InitDGV();
                do_ChangesArtikel = false;
                CheckWarning();
            }
        }
        //
        /***********************************************************************************
         *          Update Auftragsdaten           * 
         * 
         * ********************************************************************************/
        ///<summary>ctrArtDetails/ tsbtnAuftragSpeichern_Click</summary>
        ///<remarks></remarks>
        private void tsbtnAuftragSpeichern_Click(object sender, EventArgs e)
        {
            SaveAuftragsDaten();
        }
        ///<summary>ctrArtDetails/ SaveAuftragsDaten</summary>
        ///<remarks></remarks>
        public void SaveAuftragsDaten()
        {
            if (CheckInput())
            {
                AssignValueAuftragsdaten();
                //InitAuftragsdaten();
                do_ChangesAuftragdetails = false;
                CheckWarning();
            }
        }
        ///<summary>ctrArtDetails/ tsbtnAuftragSpeichern_Click</summary>
        ///<remarks>ADR werden direkt bei änderung gespeichert</remarks>
        private bool CheckInput()
        {
            //hier werden die Artikeldaten bearbeitet und die dabei kann das Datum
            // in der Vergangenheit liegen
            string Help = string.Empty;

            //Ladetermin
            if (cbLadeTerminIsSet.Checked)
            {
                if (dtpLadeTermin.Value.Date < DateTime.Now.Date)
                {
                    Help = Help + "Der Ladetermin darf nicht in der Vergangenheit liegen \n";
                }
            }

            //VSB
            if (cbVSB.Checked)
            {
                if (dtpVSB.Value.Date < DateTime.Now.Date)
                {
                    Help = Help + "Die Versandbereitschaft darf nicht in der Vergangenheit liegen \n";
                }
                //Vergleich VSB / Liefertermin
                if ((cbLadeTerminIsSet.Checked) && (dtpVSB.Value.Date > dtpLadeTermin.Value.Date))
                {
                    Help = Help + "Die Versandbereitschaft darf nach dem Ladetermin liegen \n";
                }
            }


            //Liefertermin
            if (cbLieferTerminIsSet.Checked)
            {
                //Check Vergangenheit
                if (dtpLieferTermin.Value.Date < DateTime.Now.Date)
                {
                    Help = Help + "Der Liefertermin darf nicht in der Vergangenheit liegen \n";
                }
                //Vergleich Liefertermin / Ladetermin
                if ((cbLadeTerminIsSet.Checked) && (dtpLieferTermin.Value.Date < dtpLadeTermin.Value.Date))
                {
                    Help = Help + "Der Liefertermin darf nicht vor dem Ladetermin liegen \n";
                }
                //Vergleich Liefertermin / VSB
                if ((cbVSB.Checked) && (dtpLieferTermin.Value.Date < dtpVSB.Value.Date))
                {
                    Help = Help + "Der Liefertermin darf nicht vor der Versandbereitschaft liegen \n";
                }
            }



            //CHECK
            if (Help == string.Empty)
            {
                return true;
            }
            else
            {
                Help = "Folgende Pflichtfelder / Angaben sind nicht korrekt: \n" + Help;
                clsMessages.Allgemein_ERRORTextShow(Help);
                return false;
            }
        }
        ///<summary>ctrArtDetails/ AssignValueAuftragsdaten</summary>
        ///<remarks></remarks>
        private void AssignValueAuftragsdaten()
        {
            clsAuftragPos ap = this.Auftrag.AuftragPos.Copy();

            //folgende Werte müssen nicht neu zugewiesen werden, da diese nicht verändert werden können
            // - Auftrag / AuftragPos

            Int32 CheckStatus = 2;
            // Liefertermin
            if (cbLadeTerminIsSet.Checked)
            {
                ap.LadeTermin = dtpLadeTermin.Value;
            }
            else
            {
                ap.LadeTermin = Globals.DefaultDateTimeMinValue;
                //CheckStatus = 1;  // unvollständig
            }
            ap.LadeZFRequire = cbLadeZFNessesary.Checked;
            ap.LadeZF = Functions.GetStrTimeZF(nudLadeZFHour, nudLadeZFMin);
            // VSB
            if (cbVSB.Checked)
            {
                ap.VSB = dtpVSB.Value;
            }
            else
            {
                ap.VSB = Globals.DefaultDateTimeMinValue;
                CheckStatus = 1; //unvollständig
            }
            //Lieferadresse
            if (cbLieferTerminIsSet.Checked)
            {
                ap.LieferTermin = dtpLieferTermin.Value;
            }
            else
            {
                ap.LieferTermin = Globals.DefaultDateTimeMinValue;
                CheckStatus = 1;  // unvollständig
            }
            ap.LieferZFRequire = cbLieferZFNessesary.Checked;
            ap.LieferZF = Functions.GetStrTimeZF(nudLieferZFHour, nudLieferZFMin);
            ap.Ladenummer = tbLadeNr.Text;
            ap.Notiz = tbNotiz.Text;

            ap.Status = CheckStatus;
            ap.vKW = Convert.ToInt32(nudVonKW.Value);
            ap.bKW = Convert.ToInt32(nudBisKW.Value);

            ap.Update();

            //Update Relation in cls Auftrag
            string strRelation = cbRelation.Text.ToString();
            clsAuftrag.updateRelation(this.Auftrag.ID, strRelation, this.GL_User);
            this.Auftrag.AuftragPos = ap.Copy();
        }
        ///<summary>ctrArtDetails/ tbNotiz_TextChanged</summary>
        ///<remarks></remarks>
        private void tbNotiz_TextChanged(object sender, EventArgs e)
        {
            //tbNotiz.Text = tbNotiz.Text.Trim();
            Notiz = tbNotiz.Text;
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbLadeNr_TextChanged</summary>
        ///<remarks></remarks>
        private void tbLadeNr_TextChanged(object sender, EventArgs e)
        {
            tbLadeNr.Text = tbLadeNr.Text.Trim();
            strLadenummer = tbLadeNr.Text;
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ cbRelation_SelectedValueChanged</summary>
        ///<remarks></remarks>
        private void cbRelation_SelectedValueChanged(object sender, EventArgs e)
        {
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbArtID_TextChanged</summary>
        ///<remarks></remarks>
        private void tbArtID_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbArtikel_TextChanged</summary>
        ///<remarks></remarks>
        private void tbArtikel_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbGutZusatz_TextChanged</summary>
        ///<remarks></remarks>
        private void tbGutZusatz_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbDicke_TextChanged</summary>
        ///<remarks></remarks>
        private void tbDicke_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbBreite_TextChanged</summary>
        ///<remarks></remarks>
        private void tbBreite_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbLaenge_TextChanged</summary>
        ///<remarks></remarks>
        private void tbLaenge_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbHoehe_TextChanged</summary>
        ///<remarks></remarks>
        private void tbHoehe_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbNetto_TextChanged</summary>
        ///<remarks></remarks>
        private void tbNetto_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbBrutto_TextChanged</summary>
        ///<remarks></remarks>
        private void tbBrutto_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbBrutto_TextChanged_1</summary>
        ///<remarks></remarks>
        private void tbBrutto_TextChanged_1(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbAnzahl_TextChanged</summary>
        ///<remarks></remark
        private void tbAnzahl_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbWerksnummer_TextChanged</summary>
        ///<remarks></remark
        private void tbWerksnummer_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbCP_TextChanged</summary>
        ///<remarks></remark
        private void tbCP_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbPos_TextChanged</summary>
        ///<remarks></remark
        private void tbPos_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbCharge_TextChanged</summary>
        ///<remarks></remark
        private void tbCharge_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbBestellnummer_TextChanged</summary>
        ///<remarks></remark
        private void tbBestellnummer_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbexMaterialnummer_TextChanged</summary>
        ///<remarks></remark
        private void tbexMaterialnummer_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ tbexBezeichung_TextChanged</summary>
        ///<remarks></remark
        private void tbexBezeichung_TextChanged(object sender, EventArgs e)
        {
            do_ChangesArtikel = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ CheckWarning</summary>
        ///<remarks></remark
        public void CheckWarning()
        {
            //Artikel
            if (do_ChangesArtikel)
            {
                tsbtnWarning2.Enabled = true;
            }
            else
            {
                tsbtnWarning2.Enabled = false;
            }

            //Auftragdetails
            if (do_ChangesAuftragdetails)
            {
                tsbtnWarning1.Enabled = true;
            }
            else
            {
                tsbtnWarning1.Enabled = false;
            }
        }
        ///<summary>ctrArtDetails/ btnArtikel_Click</summary>
        ///<remarks></remark
        private void btnArtikel_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenFrmGArtenList(this);
            // OpenGueterartenListe();
        }
        ///<summary>ctrArtDetails/ TakeOverGueterArt</summary>
        ///<remarks></remark
        public void TakeOverGueterArt(decimal gaID)
        {
            //---testMR
            //this.Auftrag.AuftragPos.Artikel.GArt.ID = gaID;
            //this.Auftrag.AuftragPos.Artikel.GArt.Fill();
            this.Auftrag.AuftragPos.Artikel.GArtID = gaID;
            tbGut.Text = this.Auftrag.AuftragPos.Artikel.GArt.Bezeichnung;
        }
        ///<summary>ctrArtDetails/ SetTermin</summary>
        ///<remarks></remark
        private void SetTermin(DateTime dtLT, DateTime dtVSB, bool bo_CBisChecke, bool bo_VSB)
        {
            //In ctrArtDetail werden nur Updates durchgeführt, es 
            //können keine neuen Datensätze angelegt werden
            //deshalb wird hier bo_Update = true fest gesetzt.
            bool bo_Update = true;
            DateTime retdt = DateTime.MinValue;

            /**************************************
             * Liefertermin bekannt / VSB bekannt
             * ************************************/
            //
            if ((cbLieferTerminIsSet.Checked == false) & (cbVSB.Checked == false))
            {
                //Check Eintrag oder Update
                if (bo_Update)  //Update
                {
                    //Datum darf in der Vergangenheit liegen
                    //Orientierung am Liefertermin
                    if (dtLT < dtVSB)
                    {
                        dtVSB = dtLT;
                    }
                }
                else // Eintrag
                {
                    //Datum darf NICHT in der Vergangenheit liegen
                    if (dtLT < DateTime.Now.Date)
                    {
                        dtLT = DateTime.Now.Date;
                    }
                    if (dtVSB < DateTime.Now.Date)
                    {
                        dtVSB = DateTime.Now.Date;
                    }
                    //Orientierung am Liefertermin
                    if (dtLT < dtVSB)
                    {
                        dtVSB = dtLT;
                    }
                }
            }
            //Nur Liefertermin
            if ((cbLieferTerminIsSet.Checked == false) & (cbVSB.Checked == true))
            {
                //Check Eintrag oder Update
                if (!bo_Update)  // Eintrag
                {
                    //Datum darf NICHT in der Vergangenheit liegen
                    if (dtLT < DateTime.Now.Date)
                    {
                        dtLT = DateTime.Now.Date;
                    }
                }
            }
            //Nur VSB
            if ((cbLieferTerminIsSet.Checked == true) & (cbVSB.Checked == false))
            {
                //Check Eintrag oder Update
                if (!bo_Update)  // Eintrag
                {
                    //Datum darf NICHT in der Vergangenheit liegen
                    if (dtVSB < DateTime.Now.Date)
                    {
                        dtVSB = DateTime.Now.Date;
                    }
                }
            }

            dtpVSB.Value = dtVSB;
            //Functions.SetKWValue(ref nudVonKW, dtpVSB.Value);
            //dtpT_date.Value = dtLT;
            //Functions.SetKWValue(ref nudBisKW, dtpT_date.Value);
        }
        ///<summary>ctrArtDetails/ dtpLadeTermin_ValueChanged</summary>
        ///<remarks></remark
        private void dtpLadeTermin_ValueChanged(object sender, EventArgs e)
        {
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ dtpVSB_ValueChanged_1</summary>
        ///<remarks></remark
        private void dtpVSB_ValueChanged_1(object sender, EventArgs e)
        {
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ dtpLieferTermin_ValueChanged</summary>
        ///<remarks></remark
        private void dtpLieferTermin_ValueChanged(object sender, EventArgs e)
        {
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ initKD</summary>
        ///<remarks>Adresssuche</remark
        private void initKD()
        {
            //---- Kunde / Auftraggeber  -------
            KDTable = clsKunde.dataTableKD(this.GL_User.User_ID);
        }
        ///<summary>ctrArtDetails/ initVersender</summary>
        ///<remarks>Adresssuche</remark
        public void initVersender()
        {
            //------  Versender ComboBox ---------
            VTable = clsADR.ADRTable();
        }
        ///<summary>ctrArtDetails/ initEmpfaenger</summary>
        ///<remarks>Adresssuche</remark
        public void initEmpfaenger()
        {
            //------ Empfänger ComboBox  ----------
            ETable = clsADR.ADRTable();
        }
        ///<summary>ctrArtDetails/ tbSearchA_TextChanged</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchA_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchA.Text.ToString();
            if (KDTable.Rows.Count < 1)
            {
                initKD();
            }
            tbAuftraggeber.Text = Functions.GetADRTableSearchResult(KDTable, SearchText);
        }
        ///<summary>ctrArtDetails/ tbSearchV_TextChanged</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchV_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchV.Text.ToString();
            if (VTable.Rows.Count < 1)
            {
                initVersender();
            }
            tbVersender.Text = Functions.GetADRTableSearchResult(VTable, SearchText);
        }
        ///<summary>ctrArtDetails/ tbSearchE_TextChanged</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchE_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchE.Text.ToString();

            if (ETable.Rows.Count < 1)
            {
                initEmpfaenger();
            }
            tbEmpfaenger.Text = Functions.GetADRTableSearchResult(ETable, SearchText);
        }
        ///<summary>ctrArtDetails/ tbSearchNV_TextChanged</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchNV_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchNV.Text.ToString();

            if (VTable.Rows.Count < 1)
            {
                initVersender();
            }
            tbNVersender.Text = Functions.GetADRTableSearchResult(VTable, SearchText);
            if (_ctrPrint != null)
            {
                _ctrPrint.tbSearchV.Text = SearchText;
            }
        }
        ///<summary>ctrArtDetails/ tbSearchNE_TextChanged</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchNE_TextChanged(object sender, EventArgs e)
        {
            string SearchText = tbSearchNE.Text.ToString();
            if (ETable.Rows.Count < 1)
            {
                initEmpfaenger();
            }
            tbNEmpfaenger.Text = Functions.GetADRTableSearchResult(ETable, SearchText);
            if (_ctrPrint != null)
            {
                _ctrPrint.tbSearchE.Text = SearchText;
            }
        }
        /*********************************************************************************************
         *                    ADR werden gespeichert, wenn die Textbox verlassen wird
         * ******************************************************************************************/
        ///<summary>ctrArtDetails/ CheckAndUpdateSelectedADR</summary>
        ///<remarks>Adresssuche</remark
        private bool CheckAndUpdateSelectedADR(string strADRSuchbegriff, string strADRBezeichnung)
        {
            bool bo_ret = false;
            if (clsADR.CheckMatchcodeIsUsed(strADRSuchbegriff))
            {
                decimal decADR = clsADR.GetIDByMatchcode(strADRSuchbegriff);
                //clsAuftrag.updateADR_ID(this.Auftrag.ID, strADRBezeichnung, decADR);

                switch (strADRBezeichnung)
                {
                    case "Auftraggeber":
                        this.Auftrag.KD_ID = decADR;
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, clsAuftrag.const_DBTableName, this.Auftrag.ID, clsADRMan.cont_AdrArtID_Auftraggeber);
                        break;
                    case "Versender":
                        this.Auftrag.B_ID = decADR;
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, clsAuftrag.const_DBTableName, this.Auftrag.ID, clsADRMan.cont_AdrArtID_Versender);
                        break;
                    case "Empfänger":
                        this.Auftrag.E_ID = decADR;
                        clsADRMan.DeleteAllByTableIDAndAdrArtID(this.GL_User, clsAuftrag.const_DBTableName, this.Auftrag.ID, clsADRMan.cont_AdrArtID_Empfaenger);
                        break;
                    case "nVersender":
                        this.Auftrag.nB_ID = decADR;
                        break;
                    case "nEmpfänger":
                        this.Auftrag.nE_ID = decADR;
                        break;
                }
                this.Auftrag.Update();
                bo_ret = true;
            }
            return bo_ret;
        }
        ///<summary>ctrArtDetails/ tbSearchA_Leave</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchA_Leave(object sender, EventArgs e)
        {
            if (!CheckAndUpdateSelectedADR(tbSearchA.Text, enumADRBezeichung.Auftraggeber.ToString()))
            {
                clsMessages.ADR_ADRChangeFailed(enumADRBezeichung.Auftraggeber.ToString());
                //InitAuftragsdaten();
            }
        }
        ///<summary>ctrArtDetails/ tbSearchV_Leave</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchV_Leave(object sender, EventArgs e)
        {
            if (!CheckAndUpdateSelectedADR(tbSearchV.Text, enumADRBezeichung.Versender.ToString()))
            {
                clsMessages.ADR_ADRChangeFailed(enumADRBezeichung.Versender.ToString());
                // InitAuftragsdaten();
            }
        }
        ///<summary>ctrArtDetails/ tbSearchE_Leave</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchE_Leave(object sender, EventArgs e)
        {
            if (!CheckAndUpdateSelectedADR(tbSearchE.Text, enumADRBezeichung.Empfänger.ToString()))
            {
                clsMessages.ADR_ADRChangeFailed(enumADRBezeichung.Empfänger.ToString());
                //InitAuftragsdaten();
            }
        }
        ///<summary>ctrArtDetails/ tbSearchNV_Leave</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchNV_Leave(object sender, EventArgs e)
        {
            if (!CheckAndUpdateSelectedADR(tbSearchV.Text, enumADRBezeichung.nVersender.ToString()))
            {
                clsMessages.ADR_ADRChangeFailed(enumADRBezeichung.nVersender.ToString());
                //InitAuftragsdaten();
            }
        }
        ///<summary>ctrArtDetails/ tbSearchNE_Leave</summary>
        ///<remarks>Adresssuche</remark
        private void tbSearchNE_Leave(object sender, EventArgs e)
        {
            if (!CheckAndUpdateSelectedADR(tbSearchV.Text, enumADRBezeichung.nEmpfänger.ToString()))
            {
                clsMessages.ADR_ADRChangeFailed(enumADRBezeichung.nEmpfänger.ToString());
                //InitAuftragsdaten();
            }
        }
        ///<summary>ctrArtDetails/ DecimalValidatedTextbox</summary>
        ///<remarks>Adresssuche</remark
        private void DecimalValidatedTextbox(TextBox tb)
        {
            decimal tmp = 0.0M;

            if (!decimal.TryParse(tb.Text, out tmp))
            {
                clsMessages.Allgemein_EingabeIstKeineDecimalzahl();
                tmp = 0.0M;
            }
            tb.Text = Functions.FormatDecimal(tmp);
        }
        ///<summary>ctrArtDetails/ tbDicke_Validated</summary>
        ///<remarks>Adresssuche</remark
        private void tbDicke_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            DecimalValidatedTextbox(tb);
        }
        ///<summary>ctrArtDetails/ tbBreite_Validated</summary>
        ///<remarks>Adresssuche</remark
        private void tbBreite_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            DecimalValidatedTextbox(tb);
        }
        ///<summary>ctrArtDetails/ tbLaenge_Validated</summary>
        ///<remarks>Adresssuche</remark
        private void tbLaenge_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            DecimalValidatedTextbox(tb);
        }
        ///<summary>ctrArtDetails/ tbHoehe_Validated</summary>
        ///<remarks>Adresssuche</remark
        private void tbHoehe_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            DecimalValidatedTextbox(tb);
        }
        ///<summary>ctrArtDetails/ tbgemGwicht_Validated</summary>
        ///<remarks>Adresssuche</remark
        private void tbgemGwicht_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            DecimalValidatedTextbox(tb);
        }
        ///<summary>ctrArtDetails/ tbNetto_Validated</summary>
        ///<remarks>Adresssuche</remark
        private void tbNetto_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            DecimalValidatedTextbox(tb);
        }
        ///<summary>ctrArtDetails/ tbBrutto_Validated</summary>
        ///<remarks>Adresssuche</remark
        private void tbBrutto_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            DecimalValidatedTextbox(tb);
        }
        ///<summary>ctrArtDetails/ nudBisKW_ValueChanged</summary>
        ///<remarks>Adresssuche</remark
        private void nudBisKW_ValueChanged(object sender, EventArgs e)
        {
            if (nudVonKW.Value > nudBisKW.Value)
            {
                nudBisKW.Value = nudVonKW.Value;
            }
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ nudVonKW_ValueChanged</summary>
        ///<remarks>Adresssuche</remark
        private void nudVonKW_ValueChanged(object sender, EventArgs e)
        {
            if (nudVonKW.Value > nudBisKW.Value)
            {
                nudVonKW.Value = nudBisKW.Value;
            }
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ nudLadeZFHour_ValueChanged</summary>
        ///<remarks>Adresssuche</remark
        private void nudLadeZFHour_ValueChanged(object sender, EventArgs e)
        {
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ nudLadeZFMin_ValueChanged</summary>
        ///<remarks>Adresssuche</remark
        private void nudLadeZFMin_ValueChanged(object sender, EventArgs e)
        {
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ nudLieferZFHour_ValueChanged</summary>
        ///<remarks>Adresssuche</remark
        private void nudLieferZFHour_ValueChanged(object sender, EventArgs e)
        {
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ nudLieferZFMin_ValueChanged</summary>
        ///<remarks>Adresssuche</remark
        private void nudLieferZFMin_ValueChanged(object sender, EventArgs e)
        {
            do_ChangesAuftragdetails = true;
            CheckWarning();
        }
        ///<summary>ctrArtDetails/ btnManEmpfaenger_Click</summary>
        ///<remarks></remark
        private void btnManEmpfaenger_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrArtDetails/ btnManVersender_Click</summary>
        ///<remarks></remark
        private void btnManVersender_Click(object sender, EventArgs e)
        {
            OpenManAdrInput((Button)sender);
        }
        ///<summary>ctrArtDetails/ OpenManAdrInput</summary>
        ///<remarks>Adresssuche</remark
        private void OpenManAdrInput(Button myBtn)
        {
            Int32 iAdrArtID = -1;
            Int32.TryParse(myBtn.Tag.ToString(), out iAdrArtID);
            if (iAdrArtID > -1)
            {
                //bUpdateOrder = (this.Auftrag.ID > 0);
                //Falls der der Auftrag noch nicht gespeichert ist,so muss dies hier 
                //geschehen, da sonst keine AuftragID EingangTableID vorhanden ist
                if (this.Auftrag.ID == 0)
                {
                    //AssignValueAuftrag();
                    //AssignValueAuftragPos();
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
                    this._ctrADRManAdd._ctrArtDetails = this;
                    this._ctrMenu.OpenFrmTMP(this._ctrADRManAdd);
                }
            }
        }





        //
        //------- Lieferschein neutral soll gedruckt werden ----------
        //
        public void SetNeutraleADR()
        {
            cbNeutralitaet.Checked = true;
            EnableNeutraleADRFelder(true);
        }
        //
        //------------ Neutrale Adressen können gelöscht werden ----------
        //
        private void btnNeutrADRDelete_Click(object sender, EventArgs e)
        {
            ADR_ID_nE = 0;
            ADR_ID_nV = 0;
            clsAuftrag.updateADR_ID(this.Auftrag.ID, "nEmpfänger", ADR_ID_nE);
            clsAuftrag.updateADR_ID(this.Auftrag.ID, "nVersender", ADR_ID_nV);

            tbSearchNV.Text = string.Empty;
            tbNVersender.Text = string.Empty;

            tbSearchNE.Text = string.Empty;
            tbNEmpfaenger.Text = string.Empty;

            cbNeutralitaet.Checked = false;
            EnableNeutraleADRFelder(false);
            CheckWarning();

            //Update der ADR-Daten on ctrPrint wenn 
            //neutrale Lieferscheine ausgewählt
            if (_ctrPrint != null)
            {
                if (_ctrPrint.cbNeutrLfSchein.Checked)
                {
                    _ctrPrint.SetADRToFrm(ADR_ID_nE, true);
                    _ctrPrint.SetADRToFrm(ADR_ID_nV, false);
                }
            }
        }
        ///<summary>ctrArtDetails/ dgvArtikel_CellClick</summary>
        ///<remarks></remarks>
        private void dgvArtikel_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                decimal decTmp = 0;
                Decimal.TryParse(this.dgvArtikel.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out decTmp);
                if ((decTmp > 0))
                {
                    this.Auftrag.AuftragPos.Artikel.ID = decTmp;
                    this.Auftrag.AuftragPos.Artikel.GetArtikeldatenByTableID();
                    ClearArtikelDatenInputFields();
                    SetArtikelDatenToFrm();
                    do_ChangesArtikel = false;
                    CheckWarning();
                }
            }
        }
        ///<summary>ctrArtDetails/ tsbtnRefreshArtikel_Click</summary>
        ///<remarks></remarks>
        private void tsbtnRefreshArtikel_Click(object sender, EventArgs e)
        {
            InitDGV();
        }















    }
}
