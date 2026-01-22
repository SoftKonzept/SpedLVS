using LVS;
using LVS.Constants;
using Sped4.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4
{
    public partial class ctrTarifErfassung : UserControl
    {
        internal const string const_Headline = "Tariferfassung";
        internal const string const_FileName = "_Tarifgüterarten";

        internal clsTarif Tarif;
        internal ctrMenu _ctrMenu;
        public Globals._GL_USER GL_User;
        internal DataTable _dtTarif = new DataTable();
        internal DataTable _dtTarifPos = new DataTable();
        internal DataTable _dtStaffelPos = new DataTable();
        internal DataTable _dtAbrArtenAktiv = new DataTable();
        public decimal _decAdrID = 0;
        internal decimal _decKDNr = 0;
        //internal decimal _decTarifId=0;
        internal decimal _decTarifPosID = 0;
        internal string _TarifArt;
        internal bool _bUpdateTarif = true;
        internal bool _bUpdateTarifPos = true;
        internal Int32 _igrdTarifRow = 0;
        internal Int32 _igrdTarifPosRow = 0;
        internal bool bUpdateStaffel = false;
        internal bool bStaffelEditAktiv = false;
        internal string AttachmentPath;
        internal string FileName;

        //internal Dictionary<Int32, string> DictModus = new Dictionary<int, string>()
        //{
        //    {1, "monatlich"},
        //    {2, "halbmonatlich"},
        //    {3, "täglich"},
        //};

        internal Dictionary<Int32, string> DictTeilabrechnungsarten = new Dictionary<int, string>()
        {
            {1, "Einlagerungskosten abrechnen"},
            {2, "Auslagerungskosten abrechnen"},
            {3, "Lagerkosten abrechnen"},
            {4, "Sperrlagerkosten abrechnen"},
            {5, "Transportkosten abrechnen"},
            {6, "Direktanlieferung abrechnen"},
            {7, "Rücklieferung abrechnen"},
            {8, "Vorfracht abrechnen"},
            {9, "Nebenkosten abrechnen"},
            {10, "Gleisstellgebühr abrechnen"},
            {11, "Maut abrechnen"}
        };

        ///<summary>ctrTarifErfassung/ctrTarifErfassung</summary>
        ///<remarks></remarks>
        public ctrTarifErfassung()
        {
            InitializeComponent();
            afColorLabel1.myText = const_Headline;
            //modus
            cbModus.DataSource = Enum.GetValues(typeof(enumCalcultationModus));
        }
        ///<summary>ctrTarifErfassung/InitCtr</summary>
        ///<remarks>Ermittelt alle notwendigen Daten zum Anzeigen der Form</remarks>
        public void InitCtr(decimal myAdrID)
        {
            InitListViewTeilabrechnungsarten();
            AttachmentPath = this._ctrMenu._frmMain.GL_System.sys_WorkingPathExport;
            _decAdrID = myAdrID;
            Tarif = new clsTarif();
            Tarif._GL_User = this.GL_User;
            Tarif.AdrID = myAdrID;

            if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
            {
                //-- combo Datafelder
                cbDatenfeldArtikel.DataSource = constValue_Tarifposition_DataFields.GetDataFields();

                nudBisEinheit.Maximum = (decimal)Tarif.TarifPosition.MaxMengeVorgabe;
                cbDatenfeldArtikel.SelectedIndex = 0;
                cbStaffelDatenfeld.SelectedIndex = 0;

                //comboEinheit füllen
                comboBasisEinheiten.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
                comboBasisEinheiten.DisplayMember = "Bezeichnung";
                comboBasisEinheiten.ValueMember = "Bezeichnung";

                comboAbrEinheiten.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
                comboAbrEinheiten.DisplayMember = "Bezeichnung";
                comboAbrEinheiten.ValueMember = "Bezeichnung";

                comboQuantityBase.DataSource = clsTarifPosition.ListStringQuantityBase();

                // Tarifpositionsmodus
                comboTarifPosModus.DataSource = Enum.GetValues(typeof(enumCalcultationModus));

                //Combos für die Staffel
                cbStaffelEinheitAbr.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
                cbStaffelEinheitAbr.DisplayMember = "Bezeichnung";
                cbStaffelEinheitAbr.ValueMember = "Bezeichnung";
                cbStaffelEinheitBasis.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
                cbStaffelEinheitBasis.DisplayMember = "Bezeichnung";
                cbStaffelEinheitBasis.ValueMember = "Bezeichnung";

                if (this.dgvStaffelPos.Rows.Count == 0)
                {
                    gbStaffelAbreArten.Enabled = true;
                    dgvAbrArten.Enabled = true;
                }
                //Tarifpositionen TabPages ausblenden
                Functions.HideTabPage(ref tabTarifPosition, "tabPageAllInOne");
                this.tabTarifPosition.SelectedTab = tabPageBeschreibung;
            }
            else
            {
                //Button umbennen im TabTarife
                this.tsbtnGArtAdd.Text = "Warengruppe zuweisen";
                this.tsbtnGArtDelete.Text = "Warengruppe aus der Zuweisung löschen";
                //TabPage Überschrift umbennen
                this.tabPageGArten.Text = "Warengruppen";

                //Tarifpositionen TabPages 
                this.tsbtnStaffel.Visible = false;
                Functions.HideTabPage(ref tabTarifPosition, "tabPageBeschreibung");
                Functions.HideTabPage(ref tabTarifPosition, "tabPageStaffelEinheiten");

                //comboEinheit füllen
                comboBasisEinheit2.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
                comboBasisEinheit2.DisplayMember = "Bezeichnung";
                comboBasisEinheit2.ValueMember = "Bezeichnung";

                comboAbrEinheit2.DataSource = clsEinheiten.GetEinheiten(this.GL_User);
                comboAbrEinheit2.DisplayMember = "Bezeichnung";
                comboAbrEinheit2.ValueMember = "Bezeichnung";
                this.tabTarifPosition.SelectedTab = tabPageAllInOne;
            }
            //Adressdaten laden
            SetSearchADR_ID();
            InitAfMinMaxTarif();

            //Tarifeingabefelder deaktivieren
            SetTarifInputFieldsEnable(false);
            //TarifPos noch deaktiviert
            SetTarifPosInputFieldsEnable(false);
            SetTarifPosMenuButtonEnable(false);
            InitLVAbBereich();
        }
        ///<summary>ctrTarifErfassung/InitLVAbBereich</summary>
        ///<remarks></remarks>
        private void InitLVAbBereich()
        {
            lvAbBereiche.ShowCheckBoxes = true;
            DataTable dtAbBereich = clsArbeitsbereiche.GetArbeitsbereichList(this.GL_User.User_ID);
            this.lvAbBereiche.Items.Clear();

            foreach (DataRow row in dtAbBereich.Rows)
            {
                string ArbeitsbereichName = row["Arbeitsbereich"].ToString();
                decimal decTmp = 0;
                if (Decimal.TryParse(row["ID"].ToString(), out decTmp))
                {
                    clsArbeitsbereichTarif tmpAssign = new clsArbeitsbereichTarif();
                    tmpAssign.AbBereichID = decTmp;
                    tmpAssign.TarifID = this.Tarif.ID;
                    bool bIsAssin = tmpAssign.IsAssign;

                    ListViewDataItem Item = new ListViewDataItem();
                    Item.Tag = (object)tmpAssign;
                    Item.Key = decTmp;
                    Item.Text = ArbeitsbereichName;
                    Item.Value = ArbeitsbereichName;
                    if (bIsAssin)
                    {
                        Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                    }
                    else
                    {
                        Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    }
                    Item.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
                    lvAbBereiche.Items.Add(Item);
                }
            }
        }
        ///<summary>ctrTarifErfassung/InitListViewTeilabrechnungsarten</summary>
        ///<remarks> </remarks>
        private void InitListViewTeilabrechnungsarten()
        {
            lvTeilabrechnungsarten.ShowCheckBoxes = true;
            foreach (KeyValuePair<Int32, string> dictItem in DictTeilabrechnungsarten)
            {
                ListViewDataItem Item;
                if (!this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
                {
                    switch (dictItem.Key)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 9:
                        case 10:
                            Item = new ListViewDataItem();
                            Item.Tag = (object)dictItem.Key;
                            Item.Text = dictItem.Value;
                            Item.Value = dictItem.Value;
                            Item.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
                            lvTeilabrechnungsarten.Items.Add(Item);
                            break;
                    }
                }
                else
                {
                    Item = new ListViewDataItem();
                    Item.Tag = (object)dictItem.Key;
                    Item.Text = dictItem.Value;
                    Item.Value = dictItem.Value;
                    Item.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
                    lvTeilabrechnungsarten.Items.Add(Item);
                }
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnTarifNeu_Click</summary>
        ///<remarks>Ein neuer Tarif wird angelegt. Dazu müsse folgende Punkte erfüllt sein:
        ///         - bUpdate = false 
        ///         - Eingabefelder für die Tarifdaten leeren
        ///         - die entsprechenden Button werden Enable = true gesetzt
        ///         - </remarks>
        private void tsbtnTarifNeu_Click(object sender, EventArgs e)
        {
            _bUpdateTarif = false;
            dgvGArten.DataSource = null;
            SetTarifInputFieldsEnable(true);
            //Eingabefelder leeren
            ClearAfMinMaxTarifInput();
            //Tarifpositionen Eingabefelder leeren und enabled setzen
            //Eingabefelder leeren
            ClearAfMinMaxTarifPosInput();
            InitGrdTarifPos(false);
            SetTarifPosMenuButtonEnable(true);
            SetCheckedEKAKLK();
        }
        ///<summary>ctrTarifErfassung/afMinMaxTarifInit</summary>
        ///<remarks>Initialisiert den Formbereich / Tarif</remarks>
        private void InitAfMinMaxTarif()
        {
            //Eingabefelder leeren
            ClearAfMinMaxTarifInput();
            //comboTarifArt füllen
            comboTarifArt.DataSource = Enum.GetNames(typeof(enumTarifArt));
            comboTarifArt.SelectedIndex = -1;
            //Eingabefelder leeren
            ClearAfMinMaxTarifInput();
            //Button deaktivieren- Button Save und Delete Enable = false gesetzt
            SetTarifMenuButtonEnable(true, false);
            //EIngabefelder aktivieren
            SetTarifInputFieldsEnable(true);

            //Laden der Tarife anhand der Adressnummer
            if (Tarif.AdrID > 0)
            {
                InitGrd();
                //wenn Tarife vorhanden dann Eingabefelder editierbar machen sonst erst bei tsbtnNew..
                if (_dtTarif.Rows.Count > 0)
                {
                    SetTarifInputFieldsEnable(true);
                    SetTarifMenuButtonEnable(true, false);
                    //SetSelectedTarifToAfMinMaxTarifInput();
                    _bUpdateTarif = true;
                }
                else
                {
                    _bUpdateTarif = false;
                }
            }
        }
        ///<summary>ctrTarifErfassung/InitAfMinMaxTarifPosition</summary>
        ///<remarks></remarks>
        private void InitAfMinMaxTarifPos()
        {
            //Eingabefelder leeren
            ClearAfMinMaxTarifPosInput();
            if ((Tarif.ID > 0) && (Tarif.AdrID > 0))
            {
                Tarif.GetTarifArtByTarifeTableID();
                //Läd die TarifPos-Daten und füllt das Grid
                InitGrdTarifPos(false);
                if (_dtTarifPos.Rows.Count > 0)
                {
                    ClearAfMinMaxTarifPosInput();
                }
            }
            else
            {
                //da keine Tarife vorliegen, werden Button Save und Delete Enable = false gesetzt
                SetTarifPosMenuButtonEnable(false);
            }
            //lTarifName.Text = this.dgvTarif.Rows[this.dgvTarif.CurrentRow.Index].Cells["Tarifname"].Value.ToString();
            tbTarifPosTarifBeschreibung.Text = this.dgvTarif.Rows[this.dgvTarif.CurrentRow.Index].Cells["Beschreibung"].Value.ToString();
            lTarifName.Text = Tarif.ID.ToString() + "/" + Tarif.Tarifname;
        }
        ///<summary>ctrTarifErfassung/FillGrdTarif</summary>
        ///<remarks>Tarifdatensätze werden geladen und als DataSource dem Grid zugewiesen.</remarks>
        private void InitGrd()
        {
            _dtTarif.Clear();
            _dtTarif = clsTarif.GetTarife(GL_User, Tarif.AdrID);
            dgvTarif.DataSource = _dtTarif;
            for (Int32 i = 0; i <= this.dgvTarif.Columns.Count - 1; i++)
            {
                string strColName = this.dgvTarif.Columns[i].Name;
                switch (strColName)
                {
                    case "Tarifname":
                        this.dgvTarif.Columns[i].IsVisible = true;
                        this.dgvTarif.Columns[i].Width = 300;
                        break;
                    case "Art":
                        this.dgvTarif.Columns[i].IsVisible = true;
                        this.dgvTarif.Columns[i].Width = 100;
                        break;
                    case "aktiv":
                        this.dgvTarif.Columns[i].IsVisible = true;
                        this.dgvTarif.Columns[i].Width = 50;
                        break;
                    default:
                        this.dgvTarif.Columns[i].IsVisible = false;
                        break;
                }
            }
            //this.dgvTarif.BestFitColumns();
            //RowSelect auf die entsprechende Row festlegen
            if (this.dgvTarif.Rows.Count > 0)
            {
                if (Tarif.ID > 0)
                {
                    //Row Select
                    for (Int32 i = 0; i <= this.dgvTarif.Rows.Count - 1; i++)
                    {
                        decimal decTmp = 0;
                        Decimal.TryParse(this.dgvTarif.Rows[i].Cells["ID"].Value.ToString(), out decTmp);
                        if (decTmp == Tarif.ID)
                        {
                            this.dgvTarif.Rows[i].IsCurrent = true;
                            this.dgvTarif.Rows[i].IsSelected = true;
                            break;
                        }
                    }
                }
                else
                {
                    string strTmp = dgvTarif.Rows[0].Cells["ID"].Value.ToString();
                    decimal decTmp = 0;
                    Decimal.TryParse(strTmp, out decTmp);
                    Tarif.ID = decTmp;
                }

                Tarif.Fill();
                SetTarifInputFieldsEnable(false);
                SetSelectedTarifToAfMinMaxTarifInput();
                InitGrdTarifPos(false);
                lTarifName.Text = Tarif.Tarifname;
                tbTarifPosTarifBeschreibung.Text = Tarif.Beschreibung;
                ClearAfMinMaxTarifPosInput();
                this.dgvTarif.ClearSelection();
                SetPauschalSLVS();
            }
        }
        ///<summary>ctrTarifErfassung/FillGrdTarifPos</summary>
        ///<remarks>Füllt die das Datagrid grdTarifPos.</remarks>
        private void InitGrdTarifPos(bool bFromSTaffel)
        {
            if (bFromSTaffel)
            {
                tabTarifPosition.SelectedTab = tabPageStaffelEinheiten;
            }
            else
            {
                tabTarifPosition.SelectedTab = tabPageBeschreibung;
            }
            bStaffelEditAktiv = false;
            _dtTarifPos.Clear();
            _dtTarifPos = clsTarifPosition.GetTarifePositionen(GL_User, Tarif.ID);

            //vorbereitungen für die Table dtAbrArtAktiv
            _dtTarifPos.DefaultView.RowFilter = "aktiv=true";
            _dtAbrArtenAktiv.Clear();
            _dtAbrArtenAktiv = _dtTarifPos.DefaultView.ToTable("AbrArtenAktiv", true, "Art");
            DataColumn col = new DataColumn("Check", typeof(Boolean));
            col.DefaultValue = false;
            _dtAbrArtenAktiv.Columns.Add(col);
            this.dgvTarifPos.DataSource = _dtTarifPos.DefaultView;
            for (Int32 i = 0; i <= this.dgvTarifPos.Columns.Count - 1; i++)
            {
                string strColName = this.dgvTarifPos.Columns[i].Name;
                if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
                {
                    switch (strColName)
                    {
                        case "ID":
                        case "TarifID":
                        case "MasterPos":
                            this.dgvTarifPos.Columns[i].IsVisible = false;
                            //this.dgvTarifPos.Columns[i].Width = 300;
                            break;
                        default:
                            this.dgvTarifPos.Columns[i].IsVisible = true;
                            break;
                    }
                }
                else
                {
                    switch (strColName)
                    {
                        case "BasisEinheit":
                        case "AbrEinheit":
                        case "€/Einheit":
                        case "Beschreibung":
                        case "Lagerdauer":
                        case "aktiv":
                        case "TarifID":
                        case "TPosVerweis":
                        case "Gewicht von":
                        case "Gewicht bis":
                        case "Dicke von":
                        case "Dicke bis":
                        case "Breite von":
                        case "Breite bis":
                        case "Länge von":
                        case "Länge bis":
                        case "Art":
                            this.dgvTarifPos.Columns[i].IsVisible = true;
                            break;
                        default:
                            this.dgvTarifPos.Columns[i].IsVisible = false;
                            break;
                    }
                }
            }
            this.dgvTarifPos.BestFitColumns();
            this.dgvTarifPos.ClearSelection();
            InitTabPageStaffelEinheiten();
        }
        ///<summary>ctrTarifErfassung/InitTabPageStaffelEinheiten</summary>
        ///<remarks>Durchläuft die Tabelle Tarifpositionen und fügt die aktiven Abrechnungsarten zur Checkedlistbox hinzu.</remarks>
        private void InitTabPageStaffelEinheiten()
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
            {
                this.dgvAbrArten.DataSource = _dtAbrArtenAktiv;
                this.dgvAbrArten.Columns["Check"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                this.dgvAbrArten.Columns["Art"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.dgvAbrArten.Columns["Check"].DisplayIndex = 0;
                this.dgvAbrArten.Columns["Art"].DisplayIndex = 1;
                InitStaffelPositionen();
            }
        }
        ///<summary>ctrTarifErfassung/InitStaffelPositionen</summary>
        ///<remarks></remarks>    
        private void InitStaffelPositionen()
        {
            //Ermitteln der einzelnen Staffelpositionen
            _dtStaffelPos.Rows.Clear();
            _dtStaffelPos = Tarif.TarifPosition.GetTarifePositionenStaffel(Tarif.ID);
            this.dgvStaffelPos.DataSource = _dtStaffelPos;
            if (this.dgvStaffelPos.Rows.Count > 0)
            {
                this.dgvStaffelPos.Columns["TPosVerweisID"].Visible = false;
                this.dgvStaffelPos.Columns["EinheitVon"].Visible = false;
                this.dgvStaffelPos.Columns["EinheitBis"].Visible = false;
                this.dgvStaffelPos.Columns["DatenfeldArtikel"].Visible = false;
                this.dgvStaffelPos.Columns["BasisEinheit"].Visible = false;
                this.dgvStaffelPos.Columns["AbrEinheit"].Visible = false;
                this.dgvStaffelPos.Columns["MasterPos"].Visible = false;
                this.dgvStaffelPos.Columns["Beschreibung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dgvStaffelPos.Enabled = true;
            }
            else
            {

                dgvStaffelPos.Enabled = false;
            }
            SetStaffelAbrArten();
            this.dgvStaffelPos.ClearSelection();
        }
        ///<summary>ctrTarifErfassung/SetStaffelAbrArten</summary>
        ///<remarks></remarks>    
        private void SetStaffelAbrArten()
        {
            //Abgleich mit dtAbrArtenAktiv 
            //ab hier sind noch alle Abrechnungsarten auf False gesetzt
            for (Int32 i = 0; i <= _dtAbrArtenAktiv.Rows.Count - 1; i++)
            {
                if (this.dgvStaffelPos.Rows.Count > 0)
                {
                    //Die Abrechnungsarten, die im dgvStaffelpos vorhanden sind, müssen auf true gesetzt werden
                    if (this.dgvStaffelPos.CurrentCell != null)
                    {
                        Int32 iVerweis = 0;
                        string strVerweis = this.dgvStaffelPos.Rows[this.dgvStaffelPos.CurrentCell.RowIndex].Cells["TPosVerweisID"].Value.ToString();
                        Int32.TryParse(strVerweis, out iVerweis);
                        if (iVerweis > 0)
                        {
                            string strAbrArt = _dtAbrArtenAktiv.Rows[i]["Art"].ToString();
                            bool bIsInStaffel = Tarif.TarifPosition.CheckStaffelMember(Tarif.ID, iVerweis, strAbrArt);
                            _dtAbrArtenAktiv.Rows[i]["Check"] = bIsInStaffel;
                        }
                    }
                    else
                    {
                        _dtAbrArtenAktiv.Rows[i]["Check"] = false;
                    }
                }
            }
        }
        ///<summary>ctrTarifErfassung/ClearAfMinMaxTarifInput</summary>
        ///<remarks>Reset der Eingabefelder der Form / Tarif</remarks>
        private void ClearAfMinMaxTarifInput()
        {
            //Felder leeren
            tbTarifID.Text = string.Empty;
            tbBeschreibung.Text = string.Empty;
            tbTarifName.Text = string.Empty;
            cbTarifAktiv.Checked = true;
            nudVersicherung.Value = 0;
            dtpVon.Value = Functions.GetLastDayOfMonth(DateTime.Now.AddMonths(-1));
            dtpBis.Value = Globals.DefaultDateTimeMaxValue;
            nudVersicherung.Value = 0;
            cbVersicherung.Checked = false;

            //tabRGZZ
            tbZZText.Text = string.Empty;
            tbZZTextEdit.Text = string.Empty;
            nudZZ.Value = 0;

            //tabRGText
            tbRGText.Text = string.Empty;
            tbRGTextEdit.Text = string.Empty;

            //entsprechenen Variablen
            _TarifArt = string.Empty;
            cbModus.SelectedIndex = 0;

        }
        ///<summary>ctrTarifErfassung/ClearAfMinMaxTarifPosInput</summary>
        ///<remarks>Reset der Eingabefelder der Form / TarifPos</remarks>
        private void ClearAfMinMaxTarifPosInput()
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
            {
                //Felder leeren
                tbEuroEinheit.Text = "0,0000";
                tbMargePreisEinheit.Text = "0,0000";
                cbTarifZeitraumbezogen.Checked = false;
                /****
                nudVonEinheit.Minimum = 0;
                nudVonEinheit.Value = nudVonEinheit.Minimum;
                nudBisEinheit.Minimum = 0;
                nudBisEinheit.Value = nudBisEinheit.Minimum;
                 * ***/
                nudLagerdauer.Value = 0;
                nudMargeProzentEinheit.Value = 0;
                cbDatenfeldArtikel.SelectedIndex = 0;

                comboBasisEinheiten.SelectedIndex = 0;
                comboAbrEinheiten.SelectedIndex = 0;
                comboZeiteinheiten.Text = "Monat";
                comboQuantityBase.SelectedIndex = 0;
                comboTarifPosModus.SelectedIndex = 0;
                //cbTarifEinheitenbezogen.Checked = false;
                cbTarifZeitraumbezogen.Checked = false;
                tbTPosBeschreibung.Text = string.Empty;
            }
            else
            {
                //tabpage Allinone
                //tabAllinone
                tbTPosBeschreibung2.Text = string.Empty;
                cbActiv2.Checked = true;
                //BasisEinheit
                comboBasisEinheit2.Text = "kg";
                //Abr. EinheitTarif.TarifPosition.AbrEinheit
                comboAbrEinheit2.Text = "to";
                //Datenfeld Artikel
                cbDatenfeldArtikel.Text = "Brutto";

                nudGewichtVon.Minimum = 0;
                nudGewichtBis.Minimum = 0;
                nudDickeVon.Minimum = 0;
                nudDickeBis.Minimum = 0;
                nudBreiteVon.Minimum = 0;
                nudBreiteBis.Minimum = 0;
                nudLaengeVon.Minimum = 0;
                nudLaengeBis.Minimum = 0;

                nudGewichtVon.Value = 0;
                nudGewichtBis.Value = 0;
                nudDickeVon.Value = 0;
                nudDickeBis.Value = 0;
                nudBreiteVon.Value = 0;
                nudBreiteBis.Value = 0;
                nudLaengeVon.Value = 0;
                nudLaengeBis.Value = 0;
                //Lagerdauer
                nudLagerdauer2.Value = 0;
                //EuroEinheit
                nudPreis2.Value = 0;
            }
        }
        ///<summary>ctrTarifErfassung/SetTarifInputFieldsEnable</summary>
        ///<remarks>Tarifeingabefelder werden Enabled = true / false (editierbar/nicht editierbar) gesetzt.</remarks>
        ///<param name="bEnable">true/false</param>
        private void SetTarifInputFieldsEnable(bool bEnable)
        {
            tbTarifID.Enabled = bEnable;
            tbBeschreibung.Enabled = bEnable;
            tbTarifName.Enabled = bEnable;
            cbTarifAktiv.Enabled = bEnable;
            comboTarifArt.Enabled = bEnable;
            gbAbrArten.Enabled = bEnable;
            dtpVon.Enabled = bEnable;
            dtpBis.Enabled = bEnable;
            cbModus.Enabled = bEnable;
            cbVersicherung.Enabled = bEnable;
            nudVersicherung.Enabled = bEnable;
            nudVersMaterialWert.Enabled = bEnable;

            //tabRGZZ
            tbZZText.Enabled = bEnable;
            tbZZTextEdit.Enabled = bEnable;
            nudZZ.Enabled = bEnable;

            //tabRGText
            tbRGText.Enabled = bEnable;
        }
        ///<summary>ctrTarifErfassung/SetTarifPosInputFieldsEnable</summary>
        ///<remarks>Tarifeingabefelder werden Enabled = true / false (editierbar/nicht editierbar) gesetzt.</remarks>
        ///<param name="bEnable">true/false</param>
        private void SetTarifPosInputFieldsEnable(bool bEnable)
        {
            if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
            {
                cbAktiv.Enabled = bEnable;
                tbBeschreibung.Enabled = bEnable;
                comboBasisEinheiten.Enabled = bEnable;
                comboAbrEinheiten.Enabled = bEnable;
                //wenn Staffel checked, dann Checkbox Einheitenbezogen freigeben
                nudBisEinheit.Enabled = false;
                nudVonEinheit.Enabled = false;
                comboZeiteinheiten.Enabled = false;
                nudVonTEinheit.Enabled = false;
                nudBisTEinheit.Enabled = false;
                nudLagerdauer.Enabled = bEnable;
                tbMargePreisEinheit.Enabled = bEnable;
                nudMargeProzentEinheit.Enabled = bEnable;
                tbEuroEinheit.Enabled = bEnable;

                lQuantityBase.Visible = false;
                comboQuantityBase.Visible = false;
                comboTarifPosModus.Visible = bEnable;
            }
            else
            {

            }
        }
        ///<summary>ctrTarifErfassung/SetTarifMenuButtonEnable</summary>
        ///<remarks>Tarifmenübutton werden Enabled = true / false gesetzt. Über bNewButtonEnableCHange kann
        ///         die Eigenschaftsänderung für den tsbtnNewTarif gesteuert werden.</remarks>
        ///<param name="bEnable">true/false</param>
        ///<param name="bNewButtonEnableChange">true/false</param>
        private void SetTarifMenuButtonEnable(bool bEnable, bool bNewButtonEnableChange)
        {
            if (bNewButtonEnableChange)
            {
                tsbtnTarifNeu.Enabled = bEnable;
            }

            tsbtnTarifNeu.Enabled = bEnable;
            tsbtnTarifDelete.Enabled = bEnable;
            tsbtnTarifSave.Enabled = bEnable;
        }
        ///<summary>ctrTarifErfassung/SetTarifPosMenuButtonEnable</summary>
        ///<remarks>Tarifmenübutton werden Enabled = true / false gesetzt.</remarks>
        ///<param name="bEnable">true/false</param>
        ///<param name="bNewButtonEnableChange">true/false</param>
        private void SetTarifPosMenuButtonEnable(bool bEnable)
        {
            tsbtnTarifPosNeu.Enabled = bEnable;
            tsbtnTarifPosDelete.Enabled = bEnable;
            tsbtnTarifPosSave.Enabled = bEnable;
        }
        ///<summary>ctrTarifErfassung/SetSearchADR_ID</summary>
        ///<remarks>Ermittel anhand der Adresse ID die Adressdaten.</remarks>
        public void SetSearchADR_ID()
        {
            if (Tarif.AdrID > 0)
            {
                string Name = string.Empty;
                string Str = string.Empty;
                string Ort = string.Empty;
                string KDNr = string.Empty;
                clsADR adr = new clsADR();
                adr.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, Tarif.AdrID, true);
                tbKDADR.Text = adr.ID.ToString() + "/" + adr.ViewID + " - " + adr.Name1 + Environment.NewLine +
                               adr.Str + " " + adr.HausNr + Environment.NewLine +
                               adr.PLZ + " " + adr.Ort + Environment.NewLine +
                               "Kundennummer: " + adr.KD_ID.ToString();
            }
            else
            {
                tbKDADR.Text = string.Empty;
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnClose_Click</summary>
        ///<remarks>Form wird geschlossen.</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrADR_List();
            this.Dispose();
            //this._ctrMenu.CloseCtrTarifErfassung();
        }
        ///<summary>ctrTarifErfassung/tsbtnTarifSave_Click</summary>
        ///<remarks>Speichert die eingegebenen Daten als Tarifdatensatz in die Table Tarife.</remarks>
        private void tsbtnTarifSave_Click(object sender, EventArgs e)
        {
            SaveTarifValue();
            InitAfMinMaxTarifPos();
        }
        ///<summary>ctrTarifErfassung/SaveTarifValue</summary>
        ///<remarks></remarks>
        private void SaveTarifValue()
        {
            if (CheckTarifInputField())
            {
                decimal decTmpTarifID = Tarif.ID;

                Tarif = new clsTarif();
                Tarif._GL_User = GL_User;
                Tarif.ID = decTmpTarifID;
                Tarif.AdrID = _decAdrID;
                Tarif.Tarifname = tbTarifName.Text.Trim();
                Tarif.Beschreibung = tbBeschreibung.Text.Trim();
                Tarif.aktiv = cbTarifAktiv.Checked;
                Tarif.Art = comboTarifArt.Items[comboTarifArt.SelectedIndex].ToString();
                SetValueToLvTeilabrechnungsarten(true);

                Int32 iTmp = 1;  //Standard 1=monatlich
                if (cbModus.SelectedIndex > -1)
                {
                    Int32.TryParse(cbModus.SelectedValue.ToString(), out iTmp);
                }
                Tarif.Modus = iTmp;
                if (cbVersicherung.Checked)
                {
                    Tarif.VersPreis = nudVersicherung.Value;
                    Tarif.VersMaterialWert = nudVersMaterialWert.Value;
                    Tarif.ISVersPauschal = cbSLVSPauschal.Checked;

                }
                else
                {
                    Tarif.VersPreis = 0;
                    Tarif.VersMaterialWert = 0;
                    Tarif.ISVersPauschal = false;
                }

                //Tarif gültig von / bis
                Tarif.Von = dtpVon.Value;
                Tarif.Bis = dtpBis.Value;

                //tabRGZZ
                Tarif.Zahlungsziel = (Int32)nudZZ.Value;
                Tarif.ZZText = tbZZText.Text.Trim();
                Tarif.ZZTextEdit = tbZZTextEdit.Text.Trim();

                //tabRGText
                Tarif.RGText = tbRGText.Text.Trim();


                _TarifArt = Tarif.Art;

                //Speichern
                if (_bUpdateTarif)
                {
                    Tarif.UpdateTarif();
                }
                else
                {
                    Tarif.ID = 0;
                    Tarif.AddTarif();
                    //Tarif Grd neuladen
                    InitGrd();
                }

                _bUpdateTarif = true;
                SetSelectedTarifToAfMinMaxTarifInput();

                ////Tarif Grd neuladen
                //InitGrd();
                ////Tarif Button deaktivieren
                //tsbtnTarifSave.Enabled = true;
                //tsbtnTarifDelete.Enabled = true;
                //ClearAfMinMaxTarifInput();
                //comboTarifArt.SelectedIndex = -1;
            }
        }
        ///<summary>ctrTarifErfassung/SetValueToLvTeilabrechnungsarten</summary>
        ///<remarks></remarks>
        private void SetValueToLvTeilabrechnungsarten(bool bSaveAction)
        {
            //listViewTeilabrechnung durchlaufen und die zu berechnenden Kosten ermitteln
            //foreach (ListViewDataItem item in lvTeilabrechnungsarten)
            for (Int32 i = 0; i <= this.lvTeilabrechnungsarten.Items.Count - 1; i++)
            {
                Int32 iTmp = 0;
                Int32.TryParse(this.lvTeilabrechnungsarten.Items[i].Tag.ToString(), out iTmp);

                switch (iTmp)
                {
                    case 1:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcEinlagerunskosten = true;
                            }
                            else
                            {
                                Tarif.CalcEinlagerunskosten = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcEinlagerunskosten)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 2:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcAuslagerungskosten = true;
                            }
                            else
                            {
                                Tarif.CalcAuslagerungskosten = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcAuslagerungskosten)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 3:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcLagerkosten = true;
                            }
                            else
                            {
                                Tarif.CalcLagerkosten = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcLagerkosten)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 4:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcSPLKosten = true;
                            }
                            else
                            {
                                Tarif.CalcSPLKosten = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcSPLKosten)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 5:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcTransportkosten = true;
                            }
                            else
                            {
                                Tarif.CalcTransportkosten = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcTransportkosten)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 6:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcDirectDeliveryKosten = true;
                            }
                            else
                            {
                                Tarif.CalcDirectDeliveryKosten = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcDirectDeliveryKosten)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 7:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcRLKosten = true;
                            }
                            else
                            {
                                Tarif.CalcRLKosten = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcRLKosten)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 8:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcVorfracht = true;
                            }
                            else
                            {
                                Tarif.CalcVorfracht = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcVorfracht)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 9:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcNebenkosten = true;
                            }
                            else
                            {
                                Tarif.CalcNebenkosten = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcNebenkosten)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 10:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcGleis = true;
                            }
                            else
                            {
                                Tarif.CalcGleis = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcGleis)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;
                    case 11:
                        if (bSaveAction)
                        {
                            if (this.lvTeilabrechnungsarten.Items[i].CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                            {
                                Tarif.CalcToll = true;
                            }
                            else
                            {
                                Tarif.CalcToll = false;
                            }
                        }
                        else
                        {
                            if (Tarif.CalcToll)
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                            }
                            else
                            {
                                this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        ///<summary>ctrTarifErfassung/CheckTarifInputField</summary>
        ///<remarks>Check der Eingabefelder auf folgende Eigenschaften:
        ///         - Tarifname ist Pflichtfeld
        ///         - Tarifname muss eindeutig in der Datenbank mit Tarifname für diese Adresse sein
        ///         - Tarifart
        ///         </remarks>
        ///<param name="bEnable">true/false</param>
        ///<param name="bNewButtonEnableChange">true/false</param>
        private bool CheckTarifInputField()
        {
            string myErrMes = String.Empty;
            //Tarifname
            if (tbTarifName.Text == string.Empty)
            {
                if (myErrMes != string.Empty)
                {
                    myErrMes = myErrMes + Environment.NewLine;
                }
                myErrMes = myErrMes + "Das Feld Tarifname ist leer!";
            }
            if ((clsTarif.ExistTarifName(GL_User, Tarif.AdrID, tbTarifName.Text.Trim()))
                &&
                (!_bUpdateTarif)
              )
            {
                if (myErrMes != string.Empty)
                {
                    myErrMes = myErrMes + Environment.NewLine;
                }
                myErrMes = myErrMes + "Das Tarifname existiert für diese Adresse bereits!";
            }
            //Tarifart muss ausgewählt sein
            if (comboTarifArt.SelectedIndex < 0)
            {
                if (myErrMes != string.Empty)
                {
                    myErrMes = myErrMes + Environment.NewLine;
                }
                myErrMes = myErrMes + "Die Tarifart wurde nicht gewählt!";
            }

            if (myErrMes != string.Empty)
            {
                myErrMes = "Folgende Fehler sind bei der Tarifeingabe aufgetreten:" + Environment.NewLine + myErrMes;
                clsMessages.Allgemein_EingabeDatenFehlerhaft(myErrMes);
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>ctrTarifErfassung/SetSelectedTarifToAfMinMaxTarifInput</summary>
        ///<remarks>Set die Werte des gewählten Tarifs in die Eingabefelder.</remarks>
        private void SetSelectedTarifToAfMinMaxTarifInput()
        {
            _TarifArt = string.Empty;

            ClearAfMinMaxTarifInput();
            InitDGVTarifGArt();
            InitLVAbBereich();
            tbTarifID.Text = ((int)Tarif.ID).ToString();
            tbTarifName.Text = Tarif.Tarifname;
            tbBeschreibung.Text = Tarif.Beschreibung;

            for (Int32 i = 0; i <= this.comboTarifArt.Items.Count - 1; i++)
            {
                if (this.comboTarifArt.Items[i].ToString().Trim() == Tarif.Art)
                {
                    this.comboTarifArt.SelectedIndex = i;
                    _TarifArt = this.comboTarifArt.Items[i].ToString();
                    break;
                }
            }
            cbTarifAktiv.Checked = Tarif.aktiv;
            //Abrechnungsarten
            /****
            cbEinlagerungskosten.Checked = Tarif.CalcEinlagerunskosten;
            cbAuslagerungskosten.Checked = Tarif.CalcAuslagerungskosten;
            cbLagerkosten.Checked = Tarif.CalcLagerkosten;
            cbSperrlagerkosten.Checked = Tarif.CalcSPLKosten;
            cbTransportkosten.Checked = Tarif.CalcTransportkosten;
            cbDirektanlieferung.Checked = Tarif.CalcDirectDeliveryKosten;
            cbRL.Checked = Tarif.CalcRLKosten;
            cbVorfracht.Checked = Tarif.CalcVorfracht;
            *****/
            SetValueToLvTeilabrechnungsarten(false);
            Functions.SetComboToSelecetedValue(ref cbModus, Tarif.enumModus.ToString());

            dtpVon.Value = Tarif.Von;
            dtpBis.Value = Tarif.Bis;
            nudVersicherung.Value = Tarif.VersPreis;
            cbVersicherung.Checked = (Tarif.VersPreis > 0);
            cbSLVSPauschal.Checked = Tarif.ISVersPauschal;
            nudVersMaterialWert.Value = Tarif.VersMaterialWert;

            //tabMoreVal
            Int32 iTmp = 0;
            Int32.TryParse(Tarif.Zahlungsziel.ToString(), out iTmp);
            nudZZ.Value = iTmp;

            tbZZTextEdit.Text = Tarif.ZZTextEdit;
            string strTmp = tbZZText.Text;

            tbRGText.Text = Tarif.RGText;
            if (
                    (!Tarif.ZZText.Equals(string.Empty)) &&
                    (Tarif.RGText.Contains(Tarif.ZZText))
               )
            {
                tbRGTextEdit.Text = Tarif.RGText.Replace(Tarif.ZZText, "#ZZTEXT#");
            }

            _bUpdateTarif = true;
        }
        ///<summary>ctrTarifErfassung/SetSelectedTarifPosToAfMinMaxTarifInput</summary>
        ///<remarks>Set die Werte des gewählten Tarifs in die Eingabefelder.</remarks>
        private void SetSelectedTarifPosToAfMinMaxTarifInput()
        {
            ClearAfMinMaxTarifPosInput();
            if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
            {
                //aktiv
                cbAktiv.Checked = Tarif.TarifPosition.aktiv;
                //StaffelPos
                cbStaffel.Checked = Tarif.TarifPosition.StaffelPos;
                //wenn Staffel dann Delete deaktivieren
                tsbtnTarifPosDelete.Visible = (!cbStaffel.Checked);
                tsbtnTarifPosNeu.Visible = (!cbStaffel.Checked);

                //cbTarifEinheitenbezogen.Checked = Tarif.TarifPosition.StaffelPos;
                //Beschreibung
                tbTPosBeschreibung.Text = Tarif.TarifPosition.Beschreibung;

                //BasisEinheit
                comboBasisEinheiten.Text = Tarif.TarifPosition.BasisEinheit;
                //Abr. EinheitTarif.TarifPosition.AbrEinheit
                comboAbrEinheiten.Text = Tarif.TarifPosition.AbrEinheit;
                //Quantity Base
                comboQuantityBase.Text = Tarif.TarifPosition.QuantityBase;
                //Tarifpos Abrechnungsmodus 

                Functions.SetComboToSelecetedValue(ref comboTarifPosModus, Tarif.TarifPosition.CalcModus.ToString());

                if (
                         (this.Tarif.TarifPosition.TarifPosArt.Equals(enumTarifArtLager.Lagerkosten.ToString())) ||
                         (this.Tarif.TarifPosition.TarifPosArt.Equals(enumTarifArtLager.Maut.ToString())) ||
                         (this.Tarif.TarifPosition.TarifPosArt.Equals(enumTarifArtLager.Einlagerungskosten.ToString())) ||
                         (this.Tarif.TarifPosition.TarifPosArt.Equals(enumTarifArtLager.Auslagerungskosten.ToString()))
                    )
                {
                    lQuantityBase.Visible = true;
                    comboQuantityBase.Visible = true;
                }
                else
                {
                    lQuantityBase.Visible = false;
                    comboQuantityBase.Visible = false;
                }

                //einheitenbezogn
                cbStaffel.Checked = Tarif.TarifPosition.StaffelPos;
                //Einheiten
                nudVonEinheit.Value = (decimal)Tarif.TarifPosition.EinheitenVon;
                nudBisEinheit.Value = (decimal)Tarif.TarifPosition.EinheitenBis;

                //Datenfeld Artikel
                //cbDatenfeldArtikel.Text = Tarif.TarifPosition.DatenfeldArtikel;               
                switch (Tarif.TarifPosition.DatenfeldArtikel)
                {
                    case "ID":
                        Functions.SetComboToSelecetedValue(ref cbDatenfeldArtikel, constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_ID);
                        break;
                    case "Netto":
                        Functions.SetComboToSelecetedValue(ref cbDatenfeldArtikel, constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Netto);
                        break;
                    case "Brutto":
                        Functions.SetComboToSelecetedValue(ref cbDatenfeldArtikel, constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Brutto);
                        break;
                    case "Anzahl":
                        Functions.SetComboToSelecetedValue(ref cbDatenfeldArtikel, constValue_Tarifposition_DataFields.const_Tarifposition_DataField_Artikel_Anzahl);
                        break;
                    default:
                        Functions.SetComboToSelecetedValue(ref cbDatenfeldArtikel, Tarif.TarifPosition.DatenfeldArtikel);
                        break;
                }

                //Zeitraumbezogen
                cbTarifZeitraumbezogen.Checked = Tarif.TarifPosition.zeitraumbezogen;
                comboZeiteinheiten.Text = Tarif.TarifPosition.TEinheiten;
                nudVonTEinheit.Value = (decimal)Tarif.TarifPosition.TEinheitenVon;
                if (nudBisTEinheit.Maximum > Tarif.TarifPosition.TEinheitenBis)
                {
                    nudBisTEinheit.Value = (decimal)Tarif.TarifPosition.TEinheitenBis;
                }
                else
                {
                    nudBisTEinheit.Value = nudBisTEinheit.Maximum;
                }
                //Lagerdauer
                nudLagerdauer.Value = Tarif.TarifPosition.Lagerdauer;

                //Pauschal
                cbPauschal.Checked = Tarif.TarifPosition.Pauschal;
                //Marge
                nudMargeProzentEinheit.Value = Tarif.TarifPosition.MargeProzentEinheit;
                tbMargePreisEinheit.Text = Functions.FormatDecimalMoney(Tarif.TarifPosition.MargePreisEinheit);
                //EuroEinheit
                tbEuroEinheit.Text = Functions.FormatDecimalMoney(Tarif.TarifPosition.PreisEinheit);
            }
            else
            {
                //tabAllinone
                tbTPosBeschreibung2.Text = Tarif.TarifPosition.Beschreibung;
                cbActiv2.Checked = Tarif.TarifPosition.aktiv;
                //BasisEinheit
                comboBasisEinheit2.Text = Tarif.TarifPosition.BasisEinheit;
                //Abr. EinheitTarif.TarifPosition.AbrEinheit
                comboAbrEinheit2.Text = Tarif.TarifPosition.AbrEinheit;
                //Datenfeld Artikel
                cbDatenfeldArtikel.Text = "Brutto";

                //..Bis.Minimum wieder auf 0 setzen
                nudGewichtBis.Minimum = 0;
                nudDickeBis.Minimum = 0;
                nudBreiteBis.Minimum = 0;
                nudLaengeBis.Minimum = 0;

                nudGewichtVon.Value = Tarif.TarifPosition.BruttoVon;
                nudGewichtBis.Value = Tarif.TarifPosition.BruttoBis;
                nudDickeVon.Value = Tarif.TarifPosition.DickeVon;
                nudDickeBis.Value = Tarif.TarifPosition.DickeBis;
                nudBreiteVon.Value = Tarif.TarifPosition.BreiteVon;
                nudBreiteBis.Value = Tarif.TarifPosition.BreiteBis;
                nudLaengeVon.Value = Tarif.TarifPosition.LaengeVon;
                nudLaengeBis.Value = Tarif.TarifPosition.LaengeBis;
                //Lagerdauer
                nudLagerdauer2.Value = Tarif.TarifPosition.Lagerdauer;
                //EuroEinheit
                nudPreis2.Value = Tarif.TarifPosition.PreisEinheit;
            }
        }
        ///<summary>ctrTarifErfassung/grdTarif_CellClick</summary>
        ///<remarks>Beim anklicken einer Zeile im GrdTarife werden folgende Vorgänge durchgeführt:
        ///         - selectierten Tarifdaten werden in die EIngabefelder übernommen
        ///         - die TarifID wird ermittelt</remarks>
        ///<param name="iRow">gewählter Zeilenindex</param>
        private void dgvTarif_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgvTarif.Rows.Count > 0)
                {
                    string strTmp = this.dgvTarif.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    decimal decTmp = 0;
                    Decimal.TryParse(strTmp, out decTmp);
                    if (decTmp > 0)
                    {
                        Tarif.ID = decTmp;
                        Tarif.Fill();
                        //Laden der TarifPositionen
                        tsbtnTarifPosNeu.Enabled = true;
                        InitAfMinMaxTarifPos();
                        SetTarifInputFieldsEnable(true);
                        if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
                        {
                            this.tabTarifPosition.SelectedTab = tabPageBeschreibung;
                        }
                        else
                        {
                            this.tabTarifPosition.SelectedTab = tabPageAllInOne;
                        }
                        _bUpdateTarif = true;
                        SetSelectedTarifToAfMinMaxTarifInput();
                        tsbtnTarifDelete.Enabled = true;
                    }
                }
            }
        }
        ///<summary>ctrTarifErfassung/dgvTarif_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvTarif_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //Tarifdaten in Eingabefelder setzen
            _bUpdateTarif = true;
            SetSelectedTarifToAfMinMaxTarifInput();
            //Laden der TarifPositionen
            //tsbtnTarifPosNeu.Enabled = true;
            //InitAfMinMaxTarifPos();
            //SetTarifInputFieldsEnable(true);
        }
        ///<summary>ctrTarifErfassung/grdTarif_CellDoubleClick</summary>
        ///<remarks>aktivieren der EIngabefelder.</remarks>
        //private void grdTarif_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    SetTarifInputFieldsEnable(true);
        //    //Tarif Button aktivieren
        //    tsbtnTarifSave.Enabled = true;
        //    tsbtnTarifDelete.Enabled = true;
        //}
        ///<summary>ctrTarifErfassung/tsbtnTarifPosNeu_Click</summary>
        ///<remarks>Weitere TarifPositionen werden hinzugefügt.</remarks>
        private void tsbtnTarifPosNeu_Click(object sender, EventArgs e)
        {
            _bUpdateTarifPos = true;
            //Alle Eingabefelder bis auf 
            //-aktiv
            //-Staffel Enabled=false
            SetTarifPosInputFieldsEnable(true);
            cbAktiv.Enabled = true;

            //Datensatzkopie wird eingetragen
            clsTarifPosition newPos = Tarif.TarifPosition;
            newPos.ID = 0;
            //newPos.StaffelPos = true;
            if (Tarif.TarifPosition.einheitenbezogen)
            {
                newPos.EinheitenVon = Tarif.TarifPosition.MinMengenEinheitenNewItem;
                newPos.EinheitenBis = Tarif.TarifPosition.MaxMengenEinheitenNewItem;
            }
            else
            {
                newPos.EinheitenVon = 0;
                newPos.EinheitenBis = 0;
            }
            newPos.Beschreibung = string.Empty;

            //Order ID 
            //Generell wird ein Datensatz hinter dem markierten Tarifpositionendatensatz eingefügt
            //die nächste Order ID ist somit die OrderID des markierten Datensatzes +1
            //daraufhin müssen alle folgenden ORder ID um 1 erhöht werden
            newPos.OrderID = Tarif.TarifPosition.OrderID + 1;

            //Wenn es eine zusätzliche Tarifposition ist, dann immmer 
            newPos.MasterPos = false;
            newPos.AddTarifPositionen();

            Tarif.TarifPosition.ID = newPos.ID;
            Tarif.TarifPosition.Fill();

            InitGrdTarifPos(false);
            ClearAfMinMaxTarifPosInput();
            SetSelectedTarifPosToAfMinMaxTarifInput();
        }
        ///<summary>ctrTarifErfassung/SetMaxMinEinheiten</summary>
        ///<remarks></remarks>
        private void SetMaxMinEinheiten()
        {
            if (!cbStaffel.Checked)
            {
                nudVonEinheit.Minimum = 0;
                nudVonEinheit.Value = 0;
                nudBisEinheit.Minimum = 0;
                nudBisEinheit.Value = 0;
            }
            else
            {
                //Wenn eine zusätzliche Staffel erfasst werden soll,
                //dann müssen die von / bis Werte direkt gesetzt werden
                nudVonEinheit.Minimum = (decimal)Tarif.TarifPosition.MinMengenEinheiten;

                nudBisEinheit.Minimum = nudVonEinheit.Minimum;
                if (Tarif.TarifPosition.MaxMengeEinheiten < 1)
                {
                    nudBisEinheit.Maximum = Tarif.TarifPosition.MaxMengeVorgabe;
                }
                else
                {
                    nudBisEinheit.Maximum = Tarif.TarifPosition.MaxMengeEinheiten;
                }

                //Von
                if (
                    (Tarif.TarifPosition.EinheitenVon >= nudVonEinheit.Minimum) &&
                    (Tarif.TarifPosition.EinheitenVon <= nudVonEinheit.Maximum)
                    )
                {
                    nudVonEinheit.Value = (decimal)Tarif.TarifPosition.EinheitenVon;
                }
                else
                {
                    nudVonEinheit.Value = nudVonEinheit.Minimum;
                }

                //bis
                if (
                    (Tarif.TarifPosition.EinheitenBis >= nudBisEinheit.Minimum) &&
                    (Tarif.TarifPosition.EinheitenBis <= nudBisEinheit.Maximum)
                    )
                {
                    nudBisEinheit.Value = (decimal)Tarif.TarifPosition.EinheitenBis;
                }
                else
                {
                    nudBisEinheit.Value = (decimal)Tarif.TarifPosition.MinMengenEinheiten + 1;
                }
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnTarifPosSave_Click</summary>
        ///<remarks>Neue oder geänderte Tarif Position wird gespeichert.</remarks>
        private void tsbtnTarifPosSave_Click(object sender, EventArgs e)
        {
            if (CheckTarifPosInputField())
            {
                clsTarifPosition tp = new clsTarifPosition();
                tp._GL_User = GL_User;
                if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
                {
                    //tp = new clsTarifPosition();
                    //tp._GL_User = GL_User;
                    tp.TarifID = Tarif.TarifPosition.TarifID;
                    tp.ID = Tarif.TarifPosition.ID;
                    tp.TarifPosArt = Tarif.TarifPosition.TarifPosArt;
                    tp.Beschreibung = tbTPosBeschreibung.Text.Trim();
                    tp.aktiv = cbAktiv.Checked;
                    tp.StaffelPos = cbStaffel.Checked;
                    tp.einheitenbezogen = cbStaffel.Checked;
                    tp.AbrEinheit = comboAbrEinheiten.Text;
                    tp.BasisEinheit = comboBasisEinheiten.Text;
                    tp.QuantityBase = comboQuantityBase.Text;
                    //tp.CalcModus = (Int32)comboTarifPosModus.SelectedValue;
                    tp.CalcModus = EnumConverter.GetEnumObjectByValue<enumCalcultationModus>((Int32)comboTarifPosModus.SelectedValue);
                    //Test
                    Int32 iTmp = tp.QuantityCalcBase;

                    tp.EinheitenVon = Tarif.TarifPosition.EinheitenVon;
                    tp.EinheitenBis = Tarif.TarifPosition.EinheitenBis;
                    tp.DatenfeldArtikel = cbDatenfeldArtikel.Text;

                    tp.BruttoVon = 0;
                    tp.BruttoBis = 0;
                    tp.DickeVon = 0;
                    tp.DickeBis = 0;
                    tp.BreiteVon = 0;
                    tp.BreiteBis = 0;
                    tp.LaengeVon = 0;
                    tp.LaengeBis = 0;

                    tp.zeitraumbezogen = cbTarifZeitraumbezogen.Checked;
                    //ist die Eigenschaft zeitraumbezogen nicht ausgewählt, so muss dies neutral in die Datenbank
                    //geschrieben werden 
                    if (tp.zeitraumbezogen)
                    {
                        tp.TEinheiten = comboZeiteinheiten.Text;
                        tp.TEinheitenVon = (Int32)nudVonTEinheit.Value;
                        tp.TEinheitenBis = (Int32)nudBisTEinheit.Value;
                    }
                    else
                    {
                        tp.TEinheiten = string.Empty;
                        tp.TEinheitenVon = 0;
                        tp.TEinheitenBis = 0;
                    }
                    tp.Lagerdauer = System.Convert.ToInt32(nudLagerdauer.Value.ToString());

                    tp.Pauschal = cbPauschal.Checked;
                    tp.MargePreisEinheit = System.Convert.ToDecimal(tbMargePreisEinheit.Text.Trim());
                    tp.MargeProzentEinheit = nudMargeProzentEinheit.Value;
                    tp.PreisEinheit = System.Convert.ToDecimal(tbEuroEinheit.Text.Trim());
                    tp.TarifPosVerweis = Tarif.TarifPosition.TarifPosVerweis;

                    //Speichern
                    if (_bUpdateTarifPos)
                    {
                        tp.MasterPos = Tarif.TarifPosition.MasterPos;
                        tp.OrderID = Tarif.TarifPosition.OrderID;
                        tp.UpdateTarifPositionen();

                        //Falls es eine Staffel ist, so müssen alle weiteren e
                        //entsprechenden Staffelbezeichnungen geändert werden
                        if (tp.StaffelPos)
                        {
                            tp.UpdateTarifPositionenAllStaffelPositonBezeichnung();
                        }
                    }
                    else
                    {
                        //Order ID 
                        //Generell wird ein Datensatz hinter dem markierten Tarifpositionendatensatz eingefügt
                        //die nächste Order ID ist somit die OrderID des markierten Datensatzes +1
                        //daraufhin müssen alle folgenden ORder ID um 1 erhöht werden
                        tp.OrderID = Tarif.TarifPosition.OrderID + 1;

                        //Wenn es eine zusätzliche Tarifposition ist, dann immmer 
                        tp.MasterPos = false;
                        tp.AddTarifPositionen();
                        SetTarifPosMenuButtonEnable(false);
                        ClearAfMinMaxTarifPosInput();
                        SetTarifPosInputFieldsEnable(false);
                    }
                }
                else
                {
                    tp.TarifID = Tarif.TarifPosition.TarifID;
                    tp.ID = Tarif.TarifPosition.ID;
                    tp.TarifPosArt = Tarif.TarifPosition.TarifPosArt;
                    tp.Beschreibung = tbTPosBeschreibung2.Text.Trim();
                    tp.aktiv = cbActiv2.Checked;
                    tp.AbrEinheit = comboAbrEinheit2.Text;
                    tp.BasisEinheit = comboBasisEinheit2.Text;
                    tp.DatenfeldArtikel = "Brutto";
                    tp.BruttoVon = nudGewichtVon.Value;
                    tp.BruttoBis = nudGewichtBis.Value;
                    tp.DickeVon = nudDickeVon.Value;
                    tp.DickeBis = nudDickeBis.Value;
                    tp.BreiteVon = nudBreiteVon.Value;
                    tp.BreiteBis = nudBreiteBis.Value;
                    tp.LaengeVon = nudLaengeVon.Value;
                    tp.LaengeBis = nudLaengeBis.Value;
                    tp.Lagerdauer = System.Convert.ToInt32(nudLagerdauer2.Value.ToString());
                    tp.PreisEinheit = nudPreis2.Value;
                    tp.TarifPosVerweis = Tarif.TarifPosition.TarifPosVerweis;


                    tp.StaffelPos = false;
                    tp.einheitenbezogen = false;
                    tp.EinheitenVon = 0;
                    tp.EinheitenBis = 0;
                    tp.zeitraumbezogen = false;
                    tp.TEinheiten = string.Empty;
                    tp.Pauschal = false;
                    tp.MargePreisEinheit = 0;
                    tp.MargeProzentEinheit = 0;
                    //Speichern
                    if (_bUpdateTarifPos)
                    {
                        tp.MasterPos = Tarif.TarifPosition.MasterPos;
                        tp.OrderID = Tarif.TarifPosition.OrderID;
                        tp.UpdateTarifPositionen();

                        //Falls es eine Staffel ist, so müssen alle weiteren e
                        //entsprechenden Staffelbezeichnungen geändert werden
                        if (tp.StaffelPos)
                        {
                            tp.UpdateTarifPositionenAllStaffelPositonBezeichnung();
                        }
                    }
                    else
                    {
                        //Order ID 
                        //Generell wird ein Datensatz hinter dem markierten Tarifpositionendatensatz eingefügt
                        //die nächste Order ID ist somit die OrderID des markierten Datensatzes +1
                        //daraufhin müssen alle folgenden ORder ID um 1 erhöht werden
                        tp.OrderID = Tarif.TarifPosition.OrderID + 1;

                        //Wenn es eine zusätzliche Tarifposition ist, dann immmer 
                        tp.MasterPos = false;
                        tp.AddTarifPositionen();
                        SetTarifPosMenuButtonEnable(false);
                        ClearAfMinMaxTarifPosInput();
                        SetTarifPosInputFieldsEnable(false);
                    }
                }
                Tarif.TarifPosition.ID = tp.ID;
                Tarif.TarifPosition.Fill();
                InitGrdTarifPos(false);
                SetTarifPosMenuButtonEnable(true);
            }
            if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
            {
                this.tabTarifPosition.SelectedTab = tabPageBeschreibung;
            }
            else
            {
                this.tabTarifPosition.SelectedTab = tabPageAllInOne;
            }
        }
        ///<summary>ctrTarifErfassung/CheckTarifPosInputField</summary>
        ///<remarks>Die Tarifpositionen müssen auf folgende Eingaben überprüft werden:
        ///         - Combo Grund- und AbrEinheit SelectedIndex >0
        ///         - Checkbox Zeitraumbezeogen aktiv >>> Lagerdauer aktiv
        ///         - von Einheit immer >= bis Einheit
        ///         - Check ob es bereits eine Trarifposition in dem Einheitenbereich gibt</remarks>
        private bool CheckTarifPosInputField()
        {
            string myErrMes = String.Empty;
            decimal myDecTmp = 0;
            if (this._ctrMenu._frmMain.system.Client.Modul.Fakt_LagerManuellSelection)
            {
                //Comboboxen 
                if (comboAbrEinheiten.SelectedIndex < 0)
                {
                    if (myErrMes != string.Empty)
                    {
                        myErrMes = myErrMes + Environment.NewLine;
                    }
                    myErrMes = myErrMes + "Es wurde keine Abrechnungseinheit ausgewählt.";
                }
                if (comboBasisEinheiten.SelectedIndex < 0)
                {
                    if (myErrMes != string.Empty)
                    {
                        myErrMes = myErrMes + Environment.NewLine;
                    }
                    myErrMes = myErrMes + "Es wurde keine Basiseinheit ausgewählt.";
                }
                //
                if (!Decimal.TryParse(tbEuroEinheit.Text.Trim(), out myDecTmp))
                {
                    if (myErrMes != string.Empty)
                    {
                        myErrMes = myErrMes + Environment.NewLine;
                    }
                    myErrMes = myErrMes + "Das Feld €/Einheit muss eine Zahl beinhalten!";
                }
                if (!Decimal.TryParse(tbMargePreisEinheit.Text.Trim(), out myDecTmp))
                {
                    if (myErrMes != string.Empty)
                    {
                        myErrMes = myErrMes + Environment.NewLine;
                    }
                    myErrMes = myErrMes + "Das Feld Marge €/Einheit muss eine Zahl beinhalten!";
                }
            }
            else
            {
                //Comboboxen 
                if (comboAbrEinheit2.SelectedIndex < 0)
                {
                    if (myErrMes != string.Empty)
                    {
                        myErrMes = myErrMes + Environment.NewLine;
                    }
                    myErrMes = myErrMes + "Es wurde keine Abrechnungseinheit ausgewählt.";
                }
                if (comboBasisEinheit2.SelectedIndex < 0)
                {
                    if (myErrMes != string.Empty)
                    {
                        myErrMes = myErrMes + Environment.NewLine;
                    }
                    myErrMes = myErrMes + "Es wurde keine Basiseinheit ausgewählt.";
                }
            }

            if (myErrMes != string.Empty)
            {
                clsMessages.Allgemein_EingabeDatenFehlerhaft(myErrMes);
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnTarifPosDelete_Click</summary>
        ///<remarks>Der gewählte Datensatz wird gelöscht.</remarks>
        private void tsbtnTarifPosDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvTarifPos.CurrentRow != null)
            {
                //Check ob Master Pos (kann nicht gelöscht werden)
                if (!Tarif.TarifPosition.MasterPos)
                {
                    //Abfrage, ob die Position wirklich gelöscht werden soll
                    if (clsMessages.Tarife_DeleteTarif())
                    {
                        Tarif.TarifPosition.DeleteTarifPositionenByID();
                        InitAfMinMaxTarifPos();
                    }

                }
                else
                {
                    clsMessages.Tarife_DeleteDeniedofMasterTarifPos();
                    Tarif.TarifPosition.aktiv = false;
                    Tarif.TarifPosition.UpdateTarifPositionen();
                    InitGrdTarifPos(false);
                }
            }
            else
            {
                MessageBox.Show("GRID NULL");
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnTarifDelete_Click</summary>
        ///<remarks>Der gewählte Datensatz wird gelöscht.</remarks>
        private void tsbtnTarifDelete_Click(object sender, EventArgs e)
        {
            if (Tarif.ID > 0)
            {
                if (clsMessages.Tarife_DeleteTarif())
                {
                    Tarif.DelteTarifByID();
                    Tarif.ID = 0;
                    InitAfMinMaxTarif();
                    _dtTarifPos.Clear();
                    //Eingabefelder Tarifpositionen leeren
                    tabTarifPosition.SelectedTab = tabPageBeschreibung;
                    bStaffelEditAktiv = false;
                    ClearAfMinMaxTarifPosInput();
                    InitGrd();
                    //Tarif Button deaktivieren
                    tsbtnTarifSave.Enabled = true;
                    tsbtnTarifDelete.Enabled = false;
                }
            }
        }
        ///<summary>ctrTarifErfassung/tbEuroEinheit_Validated</summary>
        ///<remarks>Check der Eingabe auf folgende Kriterien:
        ///         - Eingabe muss eine Zahl sein
        ///         - Eingabewert muss > Marge €/Eineit sein</remarks>
        private void tbEuroEinheit_Validated(object sender, EventArgs e)
        {
            string myErrMes = string.Empty;
            decimal myDecTmp = 0;
            decimal myDecTmp1 = 0;
            if (!decimal.TryParse(tbEuroEinheit.Text.Trim(), out myDecTmp))
            {
                if (myErrMes != string.Empty)
                {
                    myErrMes = myErrMes + Environment.NewLine;
                }
                myErrMes = myErrMes + "Die Eingabe muss eine Zahl sein.";
            }
            else
            {
                if (!Decimal.TryParse(tbMargePreisEinheit.Text.Trim(), out myDecTmp1))
                {
                    if (myErrMes != string.Empty)
                    {
                        myErrMes = myErrMes + Environment.NewLine;
                    }
                    myErrMes = myErrMes + "Die Eingabe im Feld Marge €/Einheit muss eine Zahl sein.";
                }
                else
                {
                    if (myDecTmp < myDecTmp1)
                    {
                        if (myErrMes != string.Empty)
                        {
                            myErrMes = myErrMes + Environment.NewLine;
                        }
                        myErrMes = myErrMes + "Die Eingabe muss größer als der Wert  Marge €/Einheit sein.";
                    }
                }
            }
            if (myErrMes != string.Empty)
            {
                clsMessages.Allgemein_EingabeDatenFehlerhaft(myErrMes);
                tbEuroEinheit.Text = "0,00";
            }
            tbEuroEinheit.Text = Functions.FormatDecimalMoney(System.Convert.ToDecimal(tbEuroEinheit.Text));
        }
        ///<summary>ctrTarifErfassung/tbMargePreisEinheit_Validated</summary>
        ///<remarks>Check der Eingabe auf folgende Kriterien:
        ///         - Eingabe muss eine Zahl sein
        ///         - Eingabewert muss kleiner €/Eineit sein</remarks>
        private void tbMargePreisEinheit_Validated(object sender, EventArgs e)
        {
            string myErrMes = string.Empty;
            decimal myDecTmp = 0;
            decimal myDecTmp1 = 0;
            if (!decimal.TryParse(tbMargePreisEinheit.Text.Trim(), out myDecTmp))
            {
                if (myErrMes != string.Empty)
                {
                    myErrMes = myErrMes + Environment.NewLine;
                }
                myErrMes = myErrMes + "Die Eingabe muss eine Zahl sein.";
            }
            else
            {
                if (!Decimal.TryParse(tbEuroEinheit.Text.Trim(), out myDecTmp1))
                {
                    if (myErrMes != string.Empty)
                    {
                        myErrMes = myErrMes + Environment.NewLine;
                    }
                    myErrMes = myErrMes + "Die Eingabe im Feld €/Einheit muss eine Zahl sein.";
                }
                else
                {
                    if (myDecTmp > myDecTmp1)
                    {
                        if (myErrMes != string.Empty)
                        {
                            myErrMes = myErrMes + Environment.NewLine;
                        }
                        myErrMes = myErrMes + "Die Eingabe muss kleiner als der Wert  Marge €/Einheit sein.";
                    }
                }
            }
            if (myErrMes != string.Empty)
            {
                clsMessages.Allgemein_EingabeDatenFehlerhaft(myErrMes);
                tbMargePreisEinheit.Text = "0,00";
            }
            tbMargePreisEinheit.Text = Functions.FormatDecimalMoney(System.Convert.ToDecimal(tbMargePreisEinheit.Text));
        }
        ///<summary>ctrTarifErfassung/cbTarifZeitraumbezogen_CheckedChanged</summary>
        ///<remarks>Setzt ensprechend das Element nudLagerdauer enabled.</remarks>
        private void cbTarifZeitraumbezogen_CheckedChanged(object sender, EventArgs e)
        {
            comboZeiteinheiten.Enabled = cbTarifZeitraumbezogen.Checked;
            nudVonTEinheit.Enabled = cbTarifZeitraumbezogen.Checked;
            nudBisTEinheit.Enabled = cbTarifZeitraumbezogen.Checked;
            nudVonTEinheit.Value = 0;
            nudBisTEinheit.Value = 0;
            //erste Zeiteinheit im DropDownfeld auswählen
            if (cbTarifZeitraumbezogen.Checked)
            {
                comboZeiteinheiten.SelectedIndex = 0;
            }
            else
            {
                comboZeiteinheiten.SelectedIndex = -1;
            }
        }
        ///<summary>ctrTarifErfassung/nudVonEinheit_Validated</summary>
        ///<remarks>Der eingegebene Wert muss kleiner als Bis Einheit sein.</remarks>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            InitAfMinMaxTarif();
            SetTarifInputFieldsEnable(false);
        }
        ///<summary>ctrTarifErfassung/tsbtnGArtAdd_Click</summary>
        ///<remarks>Tarifverweis hinzufügen</remarks>
        private void tsbtnGArtAdd_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenFrmGArtenList(this);
        }
        ///<summary>ctrTarifErfassung/TakeOverGueterArt</summary>
        ///<remarks></remarks>
        public void TakeOverGueterArt(decimal TakeOver_ID)
        {
            //Eintrag Verweis GArt TarifID
            clsTarifGArtZuweisung tgz = new clsTarifGArtZuweisung();
            tgz._GL_User = this.GL_User;
            tgz.TarifID = this.Tarif.ID;
            tgz.GArtID = TakeOver_ID;
            tgz.Add();
            Tarif.TarifGArtZuweisung = tgz;

            //Gartgrd neuladen
            InitDGVTarifGArt();
        }

        ///<summary>ctrTarifErfassung/InitDGVTarifGArt</summary>
        ///<remarks></remarks>
        private void InitDGVTarifGArt()
        {
            Tarif.TarifGArtZuweisung.TarifID = Tarif.ID;
            Tarif.TarifGArtZuweisung.GetTarifGArten();
            this.dgvGArten.DataSource = Tarif.TarifGArtZuweisung.dtTarifGArt;
            Int32 iDGVWidth = this.dgvGArten.Width - 10;
            for (Int32 i = 0; i <= this.dgvGArten.Columns.Count - 1; i++)
            {
                string strColName = this.dgvGArten.Columns[i].Name;
                switch (strColName)
                {
                    case "ID":
                    case "GArtID":
                    case "TarifID":
                        this.dgvGArten.Columns[i].IsVisible = false;
                        //this.dgvGArten.Columns[i].Width = 300;
                        break;
                    case "Bezeichnung":
                        this.dgvGArten.Columns[i].IsVisible = true;
                        this.dgvGArten.Columns[i].Width = iDGVWidth * 2 / 3;
                        break;
                    case "Matchcode":
                        this.dgvGArten.Columns[i].IsVisible = true;
                        this.dgvGArten.Columns[i].Width = iDGVWidth * 1 / 3;
                        break;
                    default:
                        this.dgvGArten.Columns[i].IsVisible = false;
                        break;
                }
            }

            //Lösch-Button aktivieren nur wenn Güterartenverweise vorhanden sind
            tsbtnGArtDelete.Enabled = (this.dgvGArten.Rows.Count > 0);
            //nach dem Laden des Grid muss der erste Datensatz in die Klasse geladen werden
            if (this.dgvGArten.Rows.Count > 0)
            {
                string strTmp = this.dgvGArten.Rows[0].Cells["ID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    Tarif.TarifGArtZuweisung.ID = decTmp;
                    Tarif.TarifGArtZuweisung.Fill();
                }
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnGArtDelete_Click</summary>
        ///<remarks>Die ausgewählte Tarif/GArt - Zuweisung wird gelöscht</remarks>
        private void tsbtnGArtDelete_Click(object sender, EventArgs e)
        {
            Tarif.TarifGArtZuweisung.Delete();
            InitDGVTarifGArt();
        }
        ///<summary>frmTarifErfassung/dgvGArten_CellClick</summary>
        ///<remarks></remarks>
        private void dgvGArten_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (this.dgvGArten.Rows.Count > 0)
                {
                    if (e.RowIndex > -1)
                    {
                        string strTmp = this.dgvGArten.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        decimal decTmp = 0;
                        Decimal.TryParse(strTmp, out decTmp);
                        if (decTmp > 0)
                        {
                            Tarif.TarifGArtZuweisung.ID = decTmp;
                            Tarif.TarifGArtZuweisung.Fill();
                        }
                    }
                }
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnStaffelPosAdd_Click</summary>
        ///<remarks>Hier soll eine neue STaffelposition angelegt werden. </remarks>
        private void tsbtnStaffelPosAdd_Click(object sender, EventArgs e)
        {
            //Kopie von der gewählten Position ertellen
            clsTarifPosition tpCopy = new clsTarifPosition();
            tpCopy._GL_User = this.GL_User;
            tpCopy.ID = Tarif.TarifPosition.ID;
            tpCopy.Fill();
            //entsprechende Werte ändern
            // - ID=0;
            // - Einheiten von Bis
            // - Ordernummer
            // - MasterPos immer false
            tpCopy.ID = 0;
            tpCopy.EinheitenVon = Tarif.TarifPosition.MinMengenEinheiten + 1;
            tpCopy.EinheitenBis = Tarif.TarifPosition.MaxMengeEinheiten;
            tpCopy.OrderID = tpCopy.NextOrderID + 1;
            tpCopy.MasterPos = false;

            tpCopy.AddTarifPositionen();
            Tarif.TarifPosition.ID = tpCopy.ID;
            Tarif.TarifPosition.Fill();
            //Tarifpositionsgrid neu laden
            InitGrdTarifPos(true);
        }
        ///<summary>ctrTarifErfassung/tsbtnRefreshPos_Click</summary>
        ///<remarks>Grid aktuallisieren</remarks>
        private void tsbtnRefreshPos_Click(object sender, EventArgs e)
        {
            //InitAfMinMaxTarifPos();
            InitGrdTarifPos(false);
        }
        ///<summary>ctrTarifErfassung/tabTarifPosition_SelectedIndexChanged</summary>
        ///<remarks>Wenn Staffel bearbeiten aktiviert ist, darf nur die tabPage der Staffel auswählbar sein. Wenn nicht,
        ///         dann darf die tabPage Staffel nicht erreichbar sein</remarks>
        private void tabTarifPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bStaffelEditAktiv)
            {
                tabTarifPosition.SelectedTab = tabPageStaffelEinheiten;
            }
            else
            {
                if (tabTarifPosition.SelectedTab == tabPageStaffelEinheiten)
                {
                    tabTarifPosition.SelectedTab = tabPageBeschreibung;
                }
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnStaffel_Click</summary>
        ///<remarks>Aktiviert / Deaktiviert den Status Staffel bearbeiten</remarks>
        private void tsbtnStaffel_Click(object sender, EventArgs e)
        {
            //setzen des neuen Status
            if (bStaffelEditAktiv)
            {
                bStaffelEditAktiv = false;
            }
            else
            {
                bStaffelEditAktiv = true;
            }

            //entsprechende Aktione je Status
            if (bStaffelEditAktiv)
            {
                //alle anderen Button im Menu deaktivieren
                SetTarifPosMenuButtonEnable(false);
                //alle Felder auf tabPageStaffel leeren
                ClearEingabeFelderStaffel();
                nudBisEinheit.Enabled = true;
                nudVonEinheit.Enabled = true;

                //Selected Tab = tabPageStaffel
                tabTarifPosition.SelectedTab = tabPageStaffelEinheiten;
            }
            else
            {
                //alle anderen Button im Menu aktivieren
                SetTarifPosMenuButtonEnable(true);
                //Selected Tab = tabPageStaffel
                tabTarifPosition.SelectedTab = tabPageBeschreibung;

            }
            //Falls Staffelpositionen vorhanden sind, dann gbStaffelAbreArten.Enabled=false
            //sont true , damit die erste Staffel angelegt werden kann
            gbStaffelAbreArten.Enabled = !(this.dgvStaffelPos.Rows.Count > 0);
        }
        ///<summary>ctrTarifErfassung/tsbtnStaffelNeu_Click</summary>
        ///<remarks>Staffelposition </remarks>
        private void tsbtnStaffelNeu_Click(object sender, EventArgs e)
        {
            if (this.dgvStaffelPos.Rows.Count > 0)
            {
                Int32 iTPosVerweis = Tarif.TarifPosition.GetNextTarifPosVerweis();
                //1. Update von der Tarifposition, in der man sich befindet
                //Schleife durchlaufen und die Staffel für alle ausgewählten 
                //Abrehnungsteile anlegen
                for (Int32 i = 0; i <= _dtAbrArtenAktiv.Rows.Count - 1; i++)
                {
                    bool bText = (bool)_dtAbrArtenAktiv.Rows[i]["Check"];
                    if ((bool)_dtAbrArtenAktiv.Rows[i]["Check"])
                    {
                        string strTarifPosArt = _dtAbrArtenAktiv.Rows[i]["Art"].ToString();
                        clsTarifPosition newPos = Tarif.TarifPosition;
                        newPos.ID = newPos.GetLastTarifPositionStaffel(strTarifPosArt, Tarif.ID);
                        newPos.Fill();
                        newPos.Beschreibung = "Staffel - " + iTPosVerweis.ToString();
                        newPos.TarifPosArt = strTarifPosArt;
                        newPos.AbrEinheit = "to";
                        newPos.BasisEinheit = "kg";
                        newPos.aktiv = true;
                        newPos.MasterPos = false;
                        newPos.StaffelPos = true;
                        newPos.DatenfeldArtikel = "Brutto";
                        newPos.EinheitenVon = newPos.MinMengenEinheitenNewItem;
                        newPos.EinheitenBis = newPos.MaxMengenEinheitenNewItem;

                        //Order ID 
                        //Generell wird ein Datensatz hinter dem markierten Tarifpositionendatensatz eingefügt
                        //die nächste Order ID ist somit die OrderID des markierten Datensatzes +1
                        //daraufhin müssen alle folgenden ORder ID um 1 erhöht werden
                        newPos.OrderID = newPos.GetNextOrderID() + 1;

                        newPos.TarifPosVerweis = iTPosVerweis;
                        newPos.AddTarifPositionen();
                    }
                }
                //InitStaffelPositionen();
                InitGrdTarifPos(true);
                //Currentrow setz
                this.dgvStaffelPos.Rows[this.dgvStaffelPos.Rows.Count - 1].Selected = true;
                //Werte auf das CTR setzen
                ClearEingabeFelderStaffel();
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnStaffelNeu_Click</summary>
        ///<remarks>Staffelposition </remarks>
        private void tsbtnStaffelSave_Click(object sender, EventArgs e)
        {
            //Staffelname muss vergeben sein
            if (tbNameStaffelEinheit.Text != string.Empty)
            {
                bool bStaffel = false;
                string Bezeichnung = string.Empty;
                string strTarifPosArt = string.Empty;
                Int32 iTPosStaffelVerweis = 0;

                for (Int32 i = 0; i <= _dtAbrArtenAktiv.Rows.Count - 1; i++)
                {
                    if ((bool)_dtAbrArtenAktiv.Rows[i]["Check"])
                    {
                        clsTarifPosition UpPos = Tarif.TarifPosition;
                        UpPos.TarifID = Tarif.ID;
                        UpPos.EinheitenVon = (Int32)nudVonEinheit.Value;
                        UpPos.EinheitenBis = (Int32)nudBisEinheit.Value;
                        UpPos.BasisEinheit = cbStaffelEinheitBasis.Text;
                        UpPos.AbrEinheit = cbStaffelEinheitAbr.Text;
                        UpPos.DatenfeldArtikel = cbStaffelDatenfeld.Text;
                        UpPos.StaffelPos = true;
                        UpPos.einheitenbezogen = true;
                        UpPos.Beschreibung = tbNameStaffelEinheit.Text;
                        UpPos.TarifPosVerweis = (Int32)this.dgvStaffelPos.Rows[this.dgvStaffelPos.CurrentCell.RowIndex].Cells["TPosVerweisID"].Value;
                        UpPos.TarifPosArt = _dtAbrArtenAktiv.Rows[i]["Art"].ToString();
                        bool bIsMasterPos = (bool)this.dgvStaffelPos.Rows[this.dgvStaffelPos.CurrentCell.RowIndex].Cells["MasterPos"].Value;
                        UpPos.UpdateTarifPositionenStaffel(bIsMasterPos);
                    }
                }
                //InitStaffelPositionen();
                InitGrdTarifPos(true);
                ClearEingabeFelderStaffel();
            }
            else
            {
                clsMessages.Tarife_StaffelBeschreibungFehlt();
            }
        }
        ///<summary>ctrTarifErfassung/tbEuroEinheit_TextChanged</summary>
        ///<remarks> </remarks>
        private void tbEuroEinheit_TextChanged(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            if (!Decimal.TryParse(tbEuroEinheit.Text, out decTmp))
            {
                tbEuroEinheit.Text = Functions.FormatDecimal(decTmp);
            }
        }
        /*************************************************************************************
         *                      Combos Einheiten Basis
         * *********************************************************************************/
        ///<summary>ctrTarifErfassung/comboBasisEinheiten_SelectedIndexChanged</summary>
        ///<remarks> </remarks>
        private void comboBasisEinheiten_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBasisEinheiten.SelectedIndex != cbStaffelEinheitBasis.SelectedIndex)
            {
                if (cbStaffelEinheitBasis.Items.Count > 0)
                {
                    cbStaffelEinheitBasis.SelectedIndex = comboBasisEinheiten.SelectedIndex;
                }
            }
        }
        ///<summary>ctrTarifErfassung/cbStaffelEinheitBasis_SelectedIndexChanged</summary>
        ///<remarks> </remarks>
        private void cbStaffelEinheitBasis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStaffelEinheitBasis.SelectedIndex != comboBasisEinheiten.SelectedIndex)
            {
                if (comboBasisEinheiten.Items.Count > 0)
                {
                    comboBasisEinheiten.SelectedIndex = cbStaffelEinheitBasis.SelectedIndex;
                }
            }
        }
        /*************************************************************************************
        *                      Combos Einheiten Abrechnung
        * *********************************************************************************/
        ///<summary>ctrTarifErfassung/comboAbrEinheiten_SelectedIndexChanged</summary>
        ///<remarks> </remarks>
        private void comboAbrEinheiten_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAbrEinheiten.SelectedIndex != cbStaffelEinheitAbr.SelectedIndex)
            {
                if (cbStaffelEinheitAbr.Items.Count > 0)
                {
                    cbStaffelEinheitAbr.SelectedIndex = comboAbrEinheiten.SelectedIndex;
                }
            }
        }
        ///<summary>ctrTarifErfassung/cbStaffelEinheitAbr_SelectedIndexChanged</summary>
        ///<remarks> </remarks>
        private void cbStaffelEinheitAbr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStaffelEinheitAbr.SelectedIndex != comboAbrEinheiten.SelectedIndex)
            {
                if (comboAbrEinheiten.Items.Count > 0)
                {
                    comboAbrEinheiten.SelectedIndex = cbStaffelEinheitAbr.SelectedIndex;
                }
            }
        }
        /*******************************************************************************
         *                   Combo Datenfelder
         * ***************************************************************************/
        ///<summary>ctrTarifErfassung/comboAbrEinheiten_SelectedIndexChanged</summary>
        ///<remarks> </remarks>
        private void cbDatenfeldArtikel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDatenfeldArtikel.Text != cbStaffelDatenfeld.Text)
            {
                if (cbStaffelDatenfeld.Items.Count > 0)
                {
                    cbStaffelDatenfeld.Text = cbDatenfeldArtikel.Text;
                }
            }
        }
        ///<summary>ctrTarifErfassung/cbStaffelDatenfeld_SelectedIndexChanged</summary>
        ///<remarks> </remarks>
        private void cbStaffelDatenfeld_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStaffelDatenfeld.Text != cbDatenfeldArtikel.Text)
            {
                if (cbDatenfeldArtikel.Items.Count > 0)
                {
                    cbDatenfeldArtikel.Text = cbStaffelDatenfeld.Text;
                }
            }
        }
        ///<summary>ctrTarifErfassung/SetStaffelToFrm</summary>
        ///<remarks> </remarks>
        private void SetStaffelToFrm()
        {
            Int32 iRowIndex = this.dgvStaffelPos.CurrentCell.RowIndex;
            gbStaffelAbreArten.Enabled = true;
            bUpdateStaffel = true;
            Int32 i = 0;
            Int32.TryParse(_dtStaffelPos.Rows[iRowIndex]["EinheitVon"].ToString(), out i);
            nudVonEinheit.Value = i;
            nudVonEinheit.Value = i;
            i = 0;
            Int32.TryParse(_dtStaffelPos.Rows[iRowIndex]["EinheitBis"].ToString(), out i);
            nudBisEinheit.Value = i;
            //nudVonEinheit.Maximum =
            cbStaffelDatenfeld.Text = _dtStaffelPos.Rows[iRowIndex]["DatenfeldArtikel"].ToString();
            cbStaffelEinheitAbr.Text = _dtStaffelPos.Rows[iRowIndex]["AbrEinheit"].ToString();
            cbStaffelEinheitBasis.Text = _dtStaffelPos.Rows[iRowIndex]["BasisEinheit"].ToString();
            tbNameStaffelEinheit.Text = _dtStaffelPos.Rows[iRowIndex]["Beschreibung"].ToString();

            //Abgleich der Abrechnungsarten mit der entsprechenden Staffel
            SetStaffelAbrArten();
        }
        ///<summary>ctrTarifErfassung/ClearEingabeFelderStaffel</summary>
        ///<remarks> </remarks>
        private void ClearEingabeFelderStaffel()
        {
            gbStaffelAbreArten.Enabled = false;
            nudVonEinheit.Value = 0;
            nudBisEinheit.Value = 0;
            tbNameStaffelEinheit.Text = string.Empty;
            nudVonEinheit.Minimum = 0;
            nudVonEinheit.Value = nudVonEinheit.Minimum;
            nudBisEinheit.Minimum = 0;
            nudBisEinheit.Value = nudBisEinheit.Minimum;
        }
        ///<summary>ctrTarifErfassung/dgvStaffelPos_CellClick</summary>
        ///<remarks> </remarks>
        private void dgvStaffelPos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_dtStaffelPos.Rows.Count > 0)
            {
                gbStaffelAbreArten.Enabled = true;
                SetStaffelToFrm();
            }
        }
        ///<summary>ctrTarifErfassung/tsbtnStaffelDelete_Click</summary>
        ///<remarks>Löscht die gewählte Staffel</remarks>
        private void tsbtnStaffelDelete_Click(object sender, EventArgs e)
        {
            if (tbNameStaffelEinheit.Text != string.Empty)
            {
                if (clsMessages.Tarife_DeleteSelectedStaffelTarifPosition())
                {
                    DeleteSelectedStaffel();
                }
            }
            else
            {
                if (clsMessages.Tarife_DeleteStaffelTarifPosition())
                {
                    DeleteSelectedStaffel();
                }
            }
        }
        ///<summary>ctrTarifErfassung/DeleteSelectedStaffel</summary>
        ///<remarks></remarks>
        private void DeleteSelectedStaffel()
        {
            Int32 iDelVerweis = 0;
            string strDelVerweis = this.dgvStaffelPos.Rows[this.dgvStaffelPos.CurrentCell.RowIndex].Cells["TPosVerweisID"].Value.ToString();
            Int32.TryParse(strDelVerweis, out iDelVerweis);
            if (iDelVerweis > 0)
            {
                if ((bool)this.dgvStaffelPos.Rows[this.dgvStaffelPos.CurrentCell.RowIndex].Cells["MasterPos"].Value)
                {
                    //jetzt müssen die MasterPositionen wieder auf neutral zurückgesetzt werden
                    for (Int32 i = 0; i <= _dtAbrArtenAktiv.Rows.Count - 1; i++)
                    {
                        if ((bool)_dtAbrArtenAktiv.Rows[i]["Check"])
                        {
                            string strTarifPosArt = _dtAbrArtenAktiv.Rows[i]["Art"].ToString();
                            Tarif.TarifPosition.DeleteStaffelTarifPositionen(Tarif.ID, strTarifPosArt);
                            Tarif.TarifPosition.UpdateTarifPositionenSetRestStaffelMaster(strTarifPosArt,
                                                                                            "Standard",
                                                                                            Tarif.ID,
                                                                                            0,
                                                                                            false);
                        }
                    }
                }
                else
                {
                    Tarif.TarifPosition.DeleteStaffelTarifPositionenByTarifPosVerweis(Tarif.ID, iDelVerweis);
                }
                //InitStaffelPositionen();
                InitGrdTarifPos(true);
                ClearEingabeFelderStaffel();
            }
        }
        private void dgvAbrArten_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Bezeichnung = string.Empty;
        }
        ///<summary>ctrTarifErfassung/dgvAbrArten_CellDoubleClick</summary>
        ///<remarks>Markiert die entsprechende Abrechnungsart zur Staffel</remarks>
        private void dgvAbrArten_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool bStaffel = false;
            string Bezeichnung = string.Empty;
            string strTarifPosArt = string.Empty;
            Int32 iTPosStaffelVerweis = 0;
            bool bIsMasterPos = true;
            strTarifPosArt = this.dgvAbrArten.Rows[e.RowIndex].Cells["Art"].Value.ToString();

            if (dgvStaffelPos.Rows.Count == 0)
            {
                //ganz neue Staffel wird angelegt
                _dtAbrArtenAktiv.Rows[e.RowIndex]["Check"] = true;
                iTPosStaffelVerweis = Tarif.TarifPosition.GetNextTarifPosVerweis();
                Bezeichnung = "Staffel " + iTPosStaffelVerweis.ToString();

                clsTarifPosition UpPos = Tarif.TarifPosition;
                UpPos.TarifID = Tarif.ID;
                UpPos.EinheitenVon = (Int32)nudVonEinheit.Value;
                UpPos.EinheitenBis = (Int32)nudBisEinheit.Value;
                UpPos.BasisEinheit = cbStaffelEinheitBasis.Text;
                UpPos.AbrEinheit = cbStaffelEinheitAbr.Text;
                UpPos.DatenfeldArtikel = cbStaffelDatenfeld.Text;
                UpPos.StaffelPos = (bool)_dtAbrArtenAktiv.Rows[e.RowIndex]["Check"];
                UpPos.einheitenbezogen = true;
                UpPos.Beschreibung = Bezeichnung;
                UpPos.TarifPosVerweis = iTPosStaffelVerweis;
                UpPos.TarifPosArt = strTarifPosArt;
                UpPos.UpdateTarifPositionenStaffel(bIsMasterPos);
            }
            else
            {
                //vorhandene werden bearbeitet
                if (!(bool)_dtAbrArtenAktiv.Rows[e.RowIndex]["Check"])
                {
                    //1. Update der aktuelle Masterpostion
                    decimal decMasterPos = clsTarifPosition.GetTarifPosIDFromMasterPos(this.GL_User, strTarifPosArt, Tarif.ID);
                    Tarif.TarifPosition.ID = decMasterPos;
                    Tarif.TarifPosition.Fill();

                    //2.TarifPosID der STaffel holen
                    DataTable dtTPosID = new DataTable();
                    //jetzt muss für die neue Abrechenungsart jeweils eine Staffelposition angelegt werden
                    for (Int32 i = 0; i <= _dtAbrArtenAktiv.Rows.Count - 1; i++)
                    {
                        if (!_dtAbrArtenAktiv.Rows[i]["Art"].Equals(strTarifPosArt))
                        {
                            dtTPosID.Clear();
                            dtTPosID = clsTarifPosition.GetTarifPositionStaffelbyAbrArtAndTarifID(this.GL_User, _dtAbrArtenAktiv.Rows[i]["Art"].ToString(), Tarif.ID);
                            if (dtTPosID.Rows.Count > 0)
                            {
                                break;
                            }
                        }
                    }

                    //3.die Tarifpositonen durchlaufen                
                    //jetzt werden anhand der ermittelten TarifPosID kopien erstellt und korrigiert und gespeichert
                    for (Int32 i = 0; i <= dtTPosID.Rows.Count - 1; i++)
                    {
                        decimal decTmp = 0;
                        if (Decimal.TryParse(dtTPosID.Rows[i]["ID"].ToString(), out decTmp))
                        {
                            //Neue Staffelpos anlegen
                            clsTarifPosition nTP = new clsTarifPosition();
                            nTP._GL_User = this.GL_User;
                            nTP.ID = decTmp;
                            nTP.Fill();

                            if ((bool)dtTPosID.Rows[i]["MasterPos"])
                            {
                                Tarif.TarifPosition.EinheitenVon = nTP.EinheitenVon;
                                Tarif.TarifPosition.EinheitenBis = nTP.EinheitenBis;
                                Tarif.TarifPosition.BasisEinheit = nTP.BasisEinheit;
                                Tarif.TarifPosition.AbrEinheit = nTP.AbrEinheit;
                                Tarif.TarifPosition.DatenfeldArtikel = nTP.DatenfeldArtikel;
                                Tarif.TarifPosition.StaffelPos = true;
                                Tarif.TarifPosition.einheitenbezogen = true;
                                Tarif.TarifPosition.Beschreibung = nTP.Beschreibung;
                                Tarif.TarifPosition.TarifPosVerweis = nTP.TarifPosVerweis;
                                //Tarif.TarifPosition.TarifPosArt =   //bleibt wird nicht verändert
                                Tarif.TarifPosition.UpdateTarifPositionen();
                            }
                            else
                            {
                                //neue Staffelposition wird angelegt
                                //hier wird nur die ID auf 0 gesetzt und die TarifPosArt geändert
                                nTP.ID = 0;
                                nTP.TarifPosArt = strTarifPosArt;
                                nTP.AddTarifPositionen();
                            }
                        }
                    }
                    _dtAbrArtenAktiv.Rows[e.RowIndex]["Check"] = true;
                }
                else
                {
                    //Zelle soll auf true gesetzt werden - keine Staffel           
                    Bezeichnung = "Standard";
                    iTPosStaffelVerweis = 0;
                    _dtAbrArtenAktiv.Rows[e.RowIndex]["Check"] = false;
                    clsTarifPosition UpPos = Tarif.TarifPosition;
                    UpPos.TarifID = Tarif.ID;
                    if (!(bool)_dtAbrArtenAktiv.Rows[e.RowIndex]["Check"])
                    {
                        nudVonEinheit.Value = 0;
                        nudBisEinheit.Value = 0;
                    }
                    UpPos.AbrEinheit = cbStaffelEinheitAbr.Text;
                    UpPos.EinheitenVon = (Int32)nudVonEinheit.Value;
                    UpPos.EinheitenBis = (Int32)nudBisEinheit.Value;
                    UpPos.BasisEinheit = cbStaffelEinheitBasis.Text;
                    UpPos.DatenfeldArtikel = cbStaffelDatenfeld.Text;
                    UpPos.StaffelPos = (bool)_dtAbrArtenAktiv.Rows[e.RowIndex]["Check"];
                    UpPos.einheitenbezogen = true;
                    UpPos.MasterPos = true;
                    UpPos.Beschreibung = Bezeichnung;
                    UpPos.TarifPosVerweis = iTPosStaffelVerweis;
                    UpPos.TarifPosArt = strTarifPosArt;
                    UpPos.UpdateTarifPositionenStaffel(UpPos.MasterPos);
                    //Abrechnungsart wurde auf false gesetz
                    //alle restlichen Staffelpositionen müssen gelöscht werden
                    UpPos.DeleteStaffelTarifPositionen(Tarif.ID, UpPos.TarifPosArt);
                }

            }
            InitGrdTarifPos(true);
            ClearEingabeFelderStaffel();
        }
        ///<summary>ctrTarifErfassung/SetCheckedEKAKLK</summary>
        ///<remarks>Einlagerungs-, Auslagerungs- und</remarks>
        //private void SetCheckedEKAKLK(bool myCheck)
        private void SetCheckedEKAKLK()
        {
            //foreach (ListViewDataItem item in lvTeilabrechnungsarten)
            for (Int32 i = 0; i <= this.lvTeilabrechnungsarten.Items.Count - 1; i++)
            {
                switch ((Int32)this.lvTeilabrechnungsarten.Items[i].Tag)
                {
                    case 1:
                    case 2:
                    case 3:
                        this.lvTeilabrechnungsarten.Items[i].CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                        break;
                }
            }
        }
        ///<summary>ctrTarifErfassung/cbLagerkosten_CheckedChanged</summary>
        ///<remarks></remarks>
        private void dgvTarifPos_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //Ermittelt die TarifId aus dem grdTarif 
                string strTmp = this.dgvTarifPos.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                decimal decTmp = 0;
                Decimal.TryParse(strTmp, out decTmp);
                if (decTmp > 0)
                {
                    Tarif.TarifPosition = new clsTarifPosition();
                    Tarif.TarifPosition._GL_User = this.GL_User;
                    Tarif.TarifPosition.ID = decTmp;
                    Tarif.TarifPosition.Fill();
                }
            }
        }
        ///<summary>ctrTarifErfassung/dgvTarifPos_CellDoubleClick</summary>
        ///<remarks></remarks>
        private void dgvTarifPos_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            this.tabTarifPosition.SelectedIndex = 0;
            SetTarifPosInputFieldsEnable(true);
            ClearAfMinMaxTarifPosInput();
            _bUpdateTarifPos = true;
            SetSelectedTarifPosToAfMinMaxTarifInput();
            SetTarifPosMenuButtonEnable(true);
        }
        ///<summary>ctrTarifErfassung/dgvTarifPos_RowFormatting</summary>
        ///<remarks></remarks>
        private void dgvTarifPos_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            bool IsActiv = (bool)e.RowElement.RowInfo.Cells["aktiv"].Value;
            if (IsActiv)
            {
                if ((bool)e.RowElement.RowInfo.Cells["MasterPos"].Value == true)
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.ForeColor = Color.Black;
                    e.RowElement.BackColor = Color.Bisque;
                }
                else
                {
                    e.RowElement.DrawFill = true;
                    e.RowElement.ForeColor = Color.Black;
                    e.RowElement.BackColor = Color.White;
                }
            }
            else
            {
                e.RowElement.DrawFill = true;
                e.RowElement.ForeColor = Color.LightGray;
                e.RowElement.BackColor = Color.Gray;
            }
            //e.RowElement.Enabled = IsActiv;
        }
        ///<summary>ctrTarifErfassung/stbtnExcelExport_Click</summary>
        ///<remarks></remarks>
        private void stbtnExcelExport_Click(object sender, EventArgs e)
        {
            FileName = DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + const_FileName + ".xls";
            saveFileDialog.InitialDirectory = AttachmentPath;
            saveFileDialog.FileName = AttachmentPath + "\\" + FileName;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName.Equals(String.Empty))
            {
                return;
            }
            FileName = this.saveFileDialog.FileName;
            bool openExportFile = false;
            Functions.Telerik_RunExportToExcelML(ref this._ctrMenu._frmMain, ref this.dgvGArten, FileName, ref openExportFile, this.GL_User, true);
            if (openExportFile)
            {
                try
                {
                    System.Diagnostics.Process.Start(FileName);
                }
                catch (Exception ex)
                {
                    clsError error = new clsError();
                    error._GL_User = this.GL_User;
                    error.Code = clsError.code1_501;
                    error.Aktion = "Tariferfassung Güterarten / Warengruppen - Excelexport öffnen";
                    error.exceptText = ex.ToString();
                    error.WriteError();
                }
            }
        }
        ///<summary>ctrTarifErfassung/nudGewichtVon_Validated</summary>
        ///<remarks></remarks>
        private void nudGewichtVon_Validated(object sender, EventArgs e)
        {
            this.nudGewichtBis.Minimum = this.nudGewichtVon.Value;
        }
        ///<summary>ctrTarifErfassung/nudDickeVon_Validated</summary>
        ///<remarks></remarks>
        private void nudDickeVon_Validated(object sender, EventArgs e)
        {
            this.nudDickeBis.Minimum = this.nudDickeVon.Value;
        }
        ///<summary>ctrTarifErfassung/nudBreiteVon_Validated</summary>
        ///<remarks></remarks>
        private void nudBreiteVon_Validated(object sender, EventArgs e)
        {
            this.nudBreiteBis.Minimum = this.nudBreiteVon.Value;
        }
        ///<summary>ctrTarifErfassung/nudLaengeVon_Validated</summary>
        ///<remarks></remarks>
        private void nudLaengeVon_Validated(object sender, EventArgs e)
        {
            this.nudLaengeBis.Minimum = this.nudLaengeVon.Value;
        }

        /// <summary>
        /// ctrTarifErfassung/tsbtnTarifCheck_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnTarifCheck_Click(object sender, EventArgs e)
        {
            // Hier werden die TarifePositionen eines Tarifes auf Korrektheit geprüft

            // Einlagerung
            // AUslagerung
            // LAgergeld
            DataTable dtCheckPos = clsTarifPosition.GetTarifePositionen(this.GL_User, this.Tarif.ID);
            dtCheckPos = dtCheckPos;


        }
        ///<summary>ctrTarifErfassung/tsbtnBackToKunden_Click</summary>
        ///<remarks></remarks>
        private void tsbtnBackToKunden_Click(object sender, EventArgs e)
        {
            this._ctrMenu.OpenADRFrmAndList();

            this.Dispose();

        }
        ///<summary>ctrTarifErfassung/cbSLVSPauschal_CheckedChanged</summary>
        ///<remarks></remarks>
        private void cbSLVSPauschal_CheckedChanged(object sender, EventArgs e)
        {
            lbSLVSEinheit.Visible = !cbSLVSPauschal.Checked;
            cbVersicherung.Visible = !cbSLVSPauschal.Checked;
            nudVersicherung.Visible = !cbSLVSPauschal.Checked;

        }
        ///<summary>ctrTarifErfassung/btnSavePauschalSLVS_Click</summary>
        ///<remarks></remarks>
        private void btnSavePauschalSLVS_Click(object sender, EventArgs e)
        {
            if (cbSLVSPauschal.Checked)
            {
                //this.Tarif.VersPreis = nudSLVSPauschale.Value;
            }
            else
            {
                this.Tarif.VersPreis = 0;
            }

            this.Tarif.ISVersPauschal = cbSLVSPauschal.Checked;
            this.Tarif.UpdateTarifePauschalSLVS();


        }
        ///<summary>ctrTarifErfassung/SetPauschalSLVS</summary>
        ///<remarks></remarks>
        private void SetPauschalSLVS()
        {
            cbSLVSPauschal.Checked = this.Tarif.ISVersPauschal;
            if (this.Tarif.ISVersPauschal)
            {
                //nudSLVSPauschale.Value = Tarif.VersPreis;

            }
            else
            {
                //nudSLVSPauschale.Value = 0;
            }

        }
        ///<summary>ctrTarifErfassung/tabTarife_Selected</summary>
        ///<remarks></remarks>
        private void tabTarife_Selected(object sender, TabControlEventArgs e)
        {
            TabPage pageTmp = (TabPage)this.tabTarif.SelectedTab;

            switch (pageTmp.Name)
            {
                case "tabPageAbBereich":
                    InitLVAbBereich();
                    break;
            }
        }
        ///<summary>ctrTarifErfassung/lvAbBereiche_ItemCheckedChanged</summary>
        ///<remarks></remarks>
        private void lvAbBereiche_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            //ListViewDataItem Item = (ListViewDataItem)e.Item;
            //string strTest = Item.CheckState.ToString();            
            clsArbeitsbereichTarif AbTarifAssign = (clsArbeitsbereichTarif)((ListViewDataItem)e.Item).Tag;
            if (AbTarifAssign.IsAssign)
            {
                //Daten in DB ArbeitsbereichTarif löschen
                AbTarifAssign.Delete();
            }
            else
            {
                //Daten in DB ArbeitsbereichTarif hinzufügen
                AbTarifAssign.ID = 0;
                AbTarifAssign.Add();
            }
            InitLVAbBereich();
        }
        ///<summary>ctrTarifErfassung/tsbtnSaveZZ_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSaveZZ_Click(object sender, EventArgs e)
        {
            SaveTarifValue();
        }
        ///<summary>ctrTarifErfassung/tbZZText_TextChanged</summary>
        ///<remarks></remarks>
        private void tbZZText_TextChanged(object sender, EventArgs e)
        {
            //ChangePreviewTextZZ();
        }
        ///<summary>ctrTarifErfassung/ChangePreviewTextZZ</summary>
        ///<remarks></remarks>
        private void ChangePreviewTextZZ()
        {
            string strHelp = string.Empty;
            if (
                (tbZZTextEdit.Text.Equals(string.Empty)) ||
                (nudZZ.Value < 1)
                )
            {
                tbZZText.Text = string.Empty;
            }
            else
            {
                tbZZText.Text = tbZZTextEdit.Text.Replace("#ZZ#", " " + nudZZ.Value.ToString());
            }
        }
        ///<summary>ctrTarifErfassung/nudZZ_ValueChanged</summary>
        ///<remarks></remarks>
        private void nudZZ_ValueChanged(object sender, EventArgs e)
        {
            ChangePreviewTextZZ();
        }
        ///<summary>ctrTarifErfassung/tbZZTextEdit_TextChanged</summary>
        ///<remarks></remar
        private void tbZZTextEdit_TextChanged(object sender, EventArgs e)
        {
            ChangePreviewTextZZ();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveRGText_Click(object sender, EventArgs e)
        {
            SaveTarifValue();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRGTxtCreate_Click(object sender, EventArgs e)
        {
            string Txt = string.Empty;
            if (tbRGTextEdit.Text.Contains("#ZZTEXT#"))
            {
                Txt = tbRGTextEdit.Text.Replace("#ZZTEXT#", this.Tarif.ZZText);
            }
            else
            {
                Txt = tbRGTextEdit.Text;
            }
            tbRGText.Text = Txt;
        }
    }
}
