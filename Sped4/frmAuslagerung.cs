using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sped4;
using Sped4.Classes;

namespace Sped4
{
  public partial class frmAuslagerung : frmTEMPLATE
  {
    internal clsLager Lager = new clsLager();
    public Globals._GL_USER GL_User;
    public ctrMenu _ctrMenu;
    public decimal _KD_ADR_ID = 0;
    public bool _Update = false;
    public Int32 _ListenArt = 0;
    public decimal _LVS_ID = 0;
    public decimal _LEingangID = 0;
    public decimal _LAusgangID = 0;
    public decimal _MandantenID = 0;
    public string _MandantenName = string.Empty;
    public decimal _LAusgangTableID = 0;
    internal bool _bLAusgangIsCHecked;
    private Int32 _iSearchButton = 0;
    public DateTime ADatum = DateTime.Today.Date;


    public DataTable dtAuftrag = new DataTable("Auftrag");  //eingelesene daten Einlagerung
    public DataTable dtArtikel = new DataTable("Artikel");  //Artikel Einlagerung
    public DataTable dtAuslagerung = new DataTable("Auslagerung"); //AUftragsdaten Auslagerung
    public DataTable dtArtikelAuslagerung = new DataTable("Artikel"); //Artikel Auslagerung DataSource dgvAuslagerung
    public DataTable dtSearch = new DataTable();

    public DataTable dtMandanten;

    internal decimal _ADRAuftraggeber = 0;
    internal decimal _ADRVersender = 0;
    internal decimal _ADREmpfänger = 0;
    internal decimal _ADREntladestelle = 0;
    internal decimal _ADRSpedition = 0;
    internal string _SearchTXT = string.Empty;
    internal string _SearchCriteron = string.Empty;
      
    internal string _ADRSearch = string.Empty;

    internal bool _bAusgangAktive = false;
    public Int32 SearchButton = 0;
    /**********************************
     * 1: Kunde / Auftraggeber
     * 2: Versand
     * 3: Empfänger oder Entladestelle
     * ********************************/

    internal clsLager _lager = new clsLager();

    public frmAuslagerung()
    {
      InitializeComponent();
    }
    ///<summary>frmAuslagerung / frmAuslagerung_Load</summary>
    ///<remarks>Folgende Funktionen werden ausgeführt:
    ///         - ComboMandanten füllen
    ///         - Ermittel des letzten Lagerausgangs
    ///         - Daten des Lagerausgangs auf die Form
    ///         - Laden der entsprechenden Artikeldaten</remarks>
    private void frmAuslagerung_Load(object sender, EventArgs e)
    {
        _bLAusgangIsCHecked = false;
        //combo Mandanten füllen
        dtMandanten = new DataTable("Mandanten");
        Functions.InitComboMandanten(GL_User, ref tscbMandanten, ref dtMandanten, true);
        //Combobox für Fahrzeug füllen
        Functions.InitComboFahrzeuge(this.GL_User, ref cbFahrzeug);

        //Klasse füllen
        Lager.LEingangTableID = 0;
        Lager.LAusgangTableID = 0;
        Lager._GL_User = this.GL_User;
        Lager.MandantenID = _MandantenID;
        Lager.FillLagerDaten();

        InitLoad();
    }
    ///<summary>frmAuslagerung / tsbtnClose_Click</summary>
    ///<remarks>Schliessen der Form.</remarks>
    private void InitLoad()
    { 
        //Leere der Felder
        ClearLAusgangEingabefelder();
        //letzten LAusgang laden
        GetNextLAusgang(false);
        SetLAusgangsdatenToFrm();
        //Menü freigeben
        SetMenuLAusgangEnabled((tbAusgangsnummer.Text!=string.Empty));


        //Ausgangskopfdaten aktivieren
        //SetLAusgangsKopfDatenEnabled(false);
        //Table AusgangArtikel leeren
        //dtArtikelAuslagerung.Rows.Clear();
        //Gewichte und Menge aktualisieren
        //SumArtikelAusgang();
        //Speicherbutton aktivieren
        //tsbtnAuslagerungSpeichern.Enabled = false;
        //ArtikelMoveButton erst nach Speichern der Kopfdaten aktivieren
        //SetArtikelMoveButtonEnabled(false);
        //Artikelgrd neu laden
        //InitDGV();
    }
    ///<summary>frmAuslagerung / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks>Auswahl des Mandanten.</remarks>
    private void tscbMandanten_SelectedIndexChanged(object sender, EventArgs e)
    {
        Functions.SetMandantenDaten(ref this.tscbMandanten, ref this._MandantenID, ref this._MandantenName);
        Lager.MandantenID = _MandantenID;
        //Mandant wurde geändert, so muss LEingang- und LAusgangsID =0 gesetzt werden
        Lager.LEingangTableID = 0;
        Lager.LAusgangTableID = 0;
        Lager.FillLagerDaten();
        InitLoad();
    }
    ///<summary>frmAuslagerung / tsbtnClose_Click</summary>
    ///<remarks>Schliessen der Form.</remarks>
    private void tsbtnClose_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    ///<summary>frmAuslagerung / GetNextLAusgang</summary>
    ///<remarks>Blättern 
    ///         - true = zurück
    ///         - false = vorwärts</remarks>
    private void GetNextLAusgang(bool myDirection)
    {
        if (_MandantenID > 0)
        {
            /***
            clsLager lg = new clsLager();
            lg._GL_User = this.GL_User;
            lg.LAusgangTableID = _LAusgangTableID;
            lg.LAusgangID = _LAusgangID;
            lg.MandantenID = _MandantenID;
            lg.AbBereichID = this.GL_User.sys_ArbeitsbereichID;
            lg.GetNextLAusgangsID(myDirection);
            _LAusgangID = lg.LAusgangID;
            _LAusgangTableID = lg.LAusgangTableID;
             * ***/

            //Lager.Ausgang.LAusgangTableID = _LAusgangTableID;
            //Lager.Ausgang.LAusgangID = _LAusgangID;
            //Lager.Ausgang.MandantenID = _MandantenID;
            //Lager.Ausgang.AbBereichID = this.GL_User.sys_ArbeitsbereichID;
            Lager.Ausgang.GetNextLAusgangsID(myDirection);
            Lager.LAusgangTableID = Lager.Ausgang.LAusgangTableID;
            Lager.FillLagerDaten();
        }
    }
    ///<summary>frmAuslagerung / tscbMandanten_SelectedIndexChanged</summary>
    ///<remarks>Auswahl des Mandanten.</remarks>
    private void SetLAusgangsdatenToFrm()
    {
        if (_MandantenID > 0)
        {
            _bAusgangAktive = false;
            dtArtikelAuslagerung.Rows.Clear();
            if (Lager.Ausgang.ExistLAusgangTableID())
            {
                _bLAusgangIsCHecked = Lager.Ausgang.Checked;
                //LAgerausgang abgeschlossen
                if (Lager.Ausgang.Checked)
                {
                    pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.check;
                }
                else
                {
                    pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
                }

                //ADRESSEN setzen
                //Auftraggeber
                _iSearchButton = 1;
                SetADRByID(Lager.Ausgang.Auftraggeber);
                //Empfänger
                _iSearchButton = 3;
                SetADRByID(Lager.Ausgang.Empfaenger);
                //Entladestelle
                _iSearchButton = 4;
                SetADRByID(Lager.Ausgang.Entladestelle);

                //Lagerausgangkopfdaten
                dtpAusgangDate.Value = Lager.Ausgang.LAusgangsDate;
                tbAusgangsnummer.Text = Lager.Ausgang.LAusgangID.ToString();
                tbSLB.Text = Lager.Ausgang.SLB.ToString(); 

                //Fahrzeugdaten
                if (Lager.Ausgang.SpedID == 0)
                {
                    //Eigenfahrzeug
                    cbFahrzeug.Text = Lager.Ausgang.KFZ;
                    tbKFZ.Text = Lager.Ausgang.KFZ;
                }
                else
                { 
                    //Fremdfahrzeug
                    //Spediteur
                    _iSearchButton = 5;
                    SetADRByID(Lager.Ausgang.SpedID);
                    cbFahrzeug.SelectedIndex = 0;
                    tbKFZ.Text = Lager.Ausgang.KFZ;                        
                }                    
                //Lagerausgangartikel laden
                dtArtikelAuslagerung = Lager.Ausgang.GetLagerLAusgangArtikelDaten();
                this.dgvAArtikel.DataSource = dtArtikelAuslagerung;
                SumArtikelAusgang();
                //Ausgangskopfdaten aktivieren
                SetLAusgangsKopfDatenEnabled(!Lager.Ausgang.Checked);
                //Menü aktivieren
                SetMenuLAusgangEnabled(true);
                //ArtikelMove Button 
                SetArtikelMoveButtonEnabled(!Lager.Ausgang.Checked);
            }
        }
        else
        {
            SetLAusgangsKopfDatenEnabled(false);
        }
    }
    ///<summary>frmAuslagerung / tsbtnNeueAuslagerung_Click</summary>
    ///<remarks>Ein neuer Lagerausgang wird angelegt.</remarks>
    private void tsbtnNeueAuslagerung_Click(object sender, EventArgs e)
    {
        Lager.LAusgangTableID = 0;
        Lager.LEingangTableID = 0;
        Lager.FillLagerDaten();

        //alle Ausgangkopfdaten leeren
        ClearLAusgangEingabefelder();
        //Neue Auslagerungsnummer
        SetAuslagerungsIDToForm();
        //Ausgangskopfdaten aktivieren
        SetLAusgangsKopfDatenEnabled(true);
        //Table AusgangArtikel leeren
        dtArtikelAuslagerung.Rows.Clear();
        //Gewichte und Menge aktualisieren
        SumArtikelAusgang();
        //Speicherbutton aktivieren
        tsbtnAuslagerungSpeichern.Enabled = true;
        //ArtikelMoveButton erst nach Speicher der Kopfdaten aktivieren
        SetArtikelMoveButtonEnabled(false);
    }
    ///<summary>frmAuslagerung / SetAuslagerungsIDToForm</summary>
    ///<remarks>Ermittelt eine neue Auslagerungs-ID.</remarks>
    private void SetAuslagerungsIDToForm()
    {
        if (_MandantenID > 0)
        {
            clsPrimeKeys pk = new clsPrimeKeys();
            pk._GL_User = this.GL_User;
            pk.Mandanten_ID = _MandantenID;
            pk.GetNEWLAusgangID();
            _LAusgangID = pk.LAusgangID;
            tbAusgangsnummer.Text = _LAusgangID.ToString();
            dtpAusgangDate.Value = DateTime.Now;
        }
    }
    ///<summary>frmAuslagerung / ClearLAusgangEingabefelder</summary>
    ///<remarks>Eingabefelder im Lagerausgangskopf deaktivieren.</remarks>
    private void ClearLAusgangEingabefelder()
    {
        tbMCAuftraggeber.Text = string.Empty;
        tbMCEmpfänger.Text = string.Empty;
        tbMCEntladestelle.Text = string.Empty;

        tbADRAuftraggeber.Text = string.Empty;
        tbADREmpfänger.Text = string.Empty;
        tbADREntladestelle.Text = string.Empty;
        
        tbAusgangsnummer.Text = string.Empty;
        tbAAnzahl.Text = string.Empty;
        tbANetto.Text = string.Empty;
        tbABrutto.Text = string.Empty;

        txtSearch.Text = string.Empty;
        _ADRAuftraggeber = 0;
        _ADREmpfänger = 0;
        _ADREntladestelle = 0;
        _ADRVersender = 0;
        _ADRSpedition = 0;
        _iSearchButton = 0;

        tbMCSpedition.Text = string.Empty;
        tbADRSpedition.Text = string.Empty;
        tbKFZ.Text = string.Empty;
        cbFahrzeug.SelectedIndex = -1;
        //cbFahrzeug.SelectedIndex = 0;

        dtpAusgangDate.Value = DateTime.Now;
        dtpT_date.Value = DateTime.Now;

        pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
    }
    ///<summary>frmAuslagerung / SetEnableLAusgangsKopfDaten</summary>
    ///<remarks>Eingabefelder im Lagerausgangskopf deaktivieren.</remarks>
    private void SetLAusgangsKopfDatenEnabled(bool boEnable)
    {
        tbAusgangsnummer.Enabled = boEnable;
        tbAAnzahl.Enabled = boEnable;
        tbANetto.Enabled = boEnable;
        tbABrutto.Enabled = boEnable;

        //ADR-Button
        btnAuftraggeber.Enabled = boEnable;
        btnEmpfänger.Enabled = boEnable;
        btnEntladestelle.Enabled = boEnable;
        btnSpedition.Enabled = boEnable;

        tbMCAuftraggeber.Enabled = boEnable;
        tbMCEmpfänger.Enabled = boEnable;
        tbMCEntladestelle.Enabled = boEnable;
        tbMCSpedition.Enabled = boEnable;

        tbADRAuftraggeber.Enabled = boEnable;
        tbADREmpfänger.Enabled = boEnable;
        tbADREntladestelle.Enabled = boEnable;
        tbADRSpedition.Enabled = boEnable;

        dtpAusgangDate.Enabled = boEnable;
        dtpT_date.Enabled = boEnable;
        nudStd.Enabled = boEnable;
        nudMin.Enabled = boEnable;

        cbFahrzeug.Enabled = boEnable;
        tbKFZ.Enabled = boEnable;
        gbFilter.Enabled = boEnable;
    }
    ///<summary>frmAuslagerung / SetEnableMenuLAusgang</summary>
    ///<remarks>Menü LAusgang aktivieren / deaktivieren.</remarks>
    private void SetMenuLAusgangEnabled(bool bEnable)
    {
            tsbtnAuslagerungSpeichern.Enabled = bEnable;
            tsbtnBack.Enabled = bEnable;
            tsbtnForward.Enabled = bEnable;
    }
    ///<summary>frmAuslagerung / btnEntladestelle_Click</summary>
    ///<remarks>Adresssuche.</remarks>
    private void btnEntladestelle_Click(object sender, EventArgs e)
    {
        _ADRSearch = "Entladestelle";
        _iSearchButton = 4;
        this._ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmAuslagerung / btnEmpfänger_Click</summary>
    ///<remarks>Adresssuche.</remarks>
    private void btnEmpfänger_Click(object sender, EventArgs e)
    {
        _ADRSearch = "Empfänger";
        _iSearchButton = 3;
        this._ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmAuslagerung / btnAuftraggeber_Click</summary>
    ///<remarks>Adresssuche.</remarks>
    private void btnAuftraggeber_Click(object sender, EventArgs e)
    {
        _ADRSearch = "Auftraggeber";
        _iSearchButton = 1;
        this._ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmAuslagerung / btnVersender_Click</summary>
    ///<remarks>Adresssuche.</remarks>
    private void btnVersender_Click(object sender, EventArgs e)
    {
        _ADRSearch = "Versender";
        _iSearchButton = 2;
        this._ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmAuslagerung / btnSpedition_Click</summary>
    ///<remarks>Adresssuche.</remarks>
    private void btnSpedition_Click(object sender, EventArgs e)
    {
        _ADRSearch = "Spedition";
        _iSearchButton = 5;
        this._ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmAuslagerung/TakeOverAdressID</summary>
    ///<remarks>Delegatenfunktion aus der ADRSuche zur Übergabe der Adress ID. Anhand der ADR-ID 
    ///         werden die entsprechenden Adressdaten ausgelesen und die Daten entsprechend der 
    ///         Eingabefelder auf der Form gesetzt. Entsprechend müssen nun die Button entsprechend
    ///         aktiviert werden.</remarks>
    ///<param name="decTmp">Adress ID</param>
    ///<returns>decTmp ist der Rückgabewert, den wir aus der Adresssuche erhalten.</returns>
    public void TakeOverAdressID(decimal decTmp)
    {
        if (decTmp > 0)
        {
            SetADRByID(decTmp);
        }
    }
    ///<summary>frmAuslagerung/SetADRByID</summary>
    ///<remarks>Adressdaten werden anhand der ID ermittelt und in die entsprechenden
    ///         Eingabefelder übergeben. Der Matchcode aus den Adressen wird vorerst
    ///         hier mitübernommen wird später noch einmal überschrieben.</remarks>
    public void SetADRByID(decimal ADR_ID)
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
            switch (_iSearchButton)
            {
                case 1:
                    _ADRAuftraggeber = ADR_ID;
                    tbMCAuftraggeber.Text = strMC;
                    tbADRAuftraggeber.Text = strE;
                    //Laden der entsprechenden Artikeldaten
                    InitDGV();
                    break;

                case 2:
                    _ADRVersender = ADR_ID;
                    //tbMCV.Text = strMC;
                    //tbVersender.Text = strE;
                    break;

                case 3:
                    _ADREmpfänger = ADR_ID;
                    tbMCEmpfänger.Text = strMC;
                    tbADREmpfänger.Text = strE;
                    //Beim setzten der Empfängerdaten diese sofort in ENtladestelle mit übernehmen
                    _iSearchButton = 4;
                    SetADRByID(_ADREmpfänger);
                    break;

                case 4:
                    _ADREntladestelle = ADR_ID;
                    tbMCEntladestelle.Text = strMC;
                    tbADREntladestelle.Text = strE;
                    break;

                case 5:
                    _ADRSpedition = ADR_ID;
                    tbMCSpedition.Text = strMC;
                    tbADRSpedition.Text = strE;
                    break;
            }
        }
        _iSearchButton = 0;
    }
    ///<summary>frmAuslagerung/tbMCAuftraggeber_Validated</summary>
    ///<remarks>Sobald die Auftraggeberdaten gesetzt wurden werden alle eingelagerten Artikel im Grid angezeigt.</remarks>
    private void tbMCAuftraggeber_Validated(object sender, EventArgs e)
    {
        if (_ADRAuftraggeber > 0)
        { 
           //Initialisieren der ArtikelGriddaten
            InitDGV();
        }
    }
    ///<summary>frmAuslagerung/tsbtnAuslagerungSpeichern_Click</summary>
    ///<remarks>Sobald die Auftraggeberdaten gesetzt wurden werden alle eingelagerten Artikel im Grid angezeigt.</remarks>
    private void tsbtnAuslagerungSpeichern_Click(object sender, EventArgs e)
    {
        if (GL_User.Lager_BestandAnlegen)
        {
            if (_MandantenID > 0)
            {
                decimal decTmp = 0;
                Lager.Ausgang._GL_User = this.GL_User;
                Lager.Ausgang.AbBereichID = this.GL_User.sys_ArbeitsbereichID;
                Lager.Ausgang.MandantenID = _MandantenID;

                //Ausgangsdaten zuweisen
                Lager.Ausgang.LAusgangID = _LAusgangID;
                Lager.Ausgang.LAusgangsDate = dtpAusgangDate.Value;
                Lager.Ausgang.GewichtNetto = 0;
                Lager.Ausgang.GewichtBrutto = 0;
                Lager.Ausgang.Auftraggeber = _ADRAuftraggeber;
                //Baustelle
                //Lager.Ausgang. = _ADRVersender;
                Lager.Ausgang.Empfaenger = _ADREmpfänger;
                Lager.Ausgang.Entladestelle = _ADREntladestelle;
                Lager.Ausgang.LieferantenID = 0;
                decTmp = 0;
                decimal.TryParse(tbSLB.Text, out decTmp);
                Lager.Ausgang.SLB = decTmp;
                Lager.Ausgang.MAT = string.Empty;
                Lager.Ausgang.Checked = false;
                Lager.Ausgang.SpedID = _ADRSpedition;
                Lager.Ausgang.KFZ = tbKFZ.Text;
                //Baustelle
                Lager.Ausgang.Info = string.Empty;

                if (Lager.Ausgang.CheckLAusgangByLAusgangDaten())
                {
                    Lager.Ausgang.UpdateLagerAusgang();                   
                }
                else
                {
                    Lager.Ausgang.AddLAusgang();
                }                 
                //_LAusgangTableID = lg.LAusgangTableID;


                //Button ArtikelMOve freigebben
                SetArtikelMoveButtonEnabled(true);
                //Am Ende alle Button im Menü freigeben
                SetMenuLAusgangEnabled(true);
            }
            else
            {
                clsMessages.Allgemein_MandantFehlt();
            }
        }
        else
        {
            clsMessages.User_NoAuthen();
        }
    }
    ///<summary>frmAuslagerung/InitDGV</summary>
    ///<remarks>Ermittelt alle eingelagerten Artikel des Auftraggebers und zeigt diese im Grid an.</remarks>
    private void InitDGV()
    {
        if ((_ADRAuftraggeber > 0) && (_MandantenID > 0))
        {
            // Lager.Ausgang.AbBereichID = this.GL_User.sys_ArbeitsbereichID;
            Lager.Eingang.Auftraggeber = _ADRAuftraggeber;
            dtArtikel = Lager.Eingang.GetLagerArtikelDatenByAuftraggeber();

            //Baustelle
            if ((!Lager.Ausgang.Checked) || (Lager.Ausgang.ExistLAusgangTableID()))
            {
                this.dgv.DataSource = dtArtikel;
                this.dgv.Columns["Check"].Visible = false;
                this.dgv.AutoResizeColumns();
                this.dgv.AutoResizeRows();

                //Auslagerungsartikel
                if (!_bAusgangAktive)
                {
                    dtArtikelAuslagerung = dtArtikel.Copy();
                    dtArtikelAuslagerung.Clear();
                    this.dgvAArtikel.DataSource = dtArtikelAuslagerung;
                } 
                //sorgt dafür, dass bei abgeschlossenen Ausgängen das Artikelgrd geleert wird
                if (Lager.Ausgang.Checked)
                {
                    dtArtikel.Rows.Clear();
                    this.dgv.DataSource = dtArtikel;
                    this.dgvAArtikel.Enabled = false;
                }
                else
                {
                    this.dgvAArtikel.Enabled = true;
                }
            }
            else
            {
                dtArtikel.Rows.Clear();
                this.dgv.DataSource = dtArtikel;
            }
        }
        else
        {
            dtArtikel.Rows.Clear();
            this.dgv.DataSource = dtArtikel;
        }


    }
    ///<summary>frmAuslagerung/CopyDataRowdtAArtikel</summary>
    ///<remarks>Copiert den gewählten Datensatz in die Ausgangstable und löscht den entsprechenden 
    ///         Datensatz aus der Aritkeltabelle.</remarks>
    private void CopyDataRowdtoAArtikel(bool bAll)
    {
        if (this.dgv.CurrentCell != null)
        {
            decimal decTmpArtID = (decimal)this.dgv.Rows[this.dgv.CurrentCell.RowIndex].Cells["ID"].Value;
            if (decTmpArtID > 0)
            {
                for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
                {                    
                    if (bAll)
                    {  
                        decTmpArtID = (decimal)this.dgv.Rows[i].Cells["ID"].Value;
                        if (decTmpArtID > 0)
                        {  
                            //Update des Artikeldatensatzes 
                            clsLager.UpdateLArtikelLAusgang(GL_User, Lager.LAusgangTableID,decTmpArtID);                            
                        }
                        dtArtikelAuslagerung.ImportRow(dtArtikel.Rows[i]);
                        if (i == dtArtikel.Rows.Count - 1)
                        {
                            dtArtikel.Clear();
                        }
                    }
                    else
                    {
                        if (decTmpArtID == (decimal)dtArtikel.Rows[i]["ID"])
                        {
                            clsLager.UpdateLArtikelLAusgang(GL_User, Lager.LAusgangTableID, decTmpArtID);
                            dtArtikelAuslagerung.ImportRow(dtArtikel.Rows[i]);
                            dtArtikel.Rows.RemoveAt(i);
                            break;
                        }
                    }
                }             
            }
        }        
    }
    ///<summary>frmAuslagerung/tsbtnSelectedArtToAusgang_Click</summary>
    ///<remarks>Der markierte Artikeldatensatz wird in das Auslagerungsgrid übernommen.</remarks>
    private void tsbtnSelectedArtToAusgang_Click(object sender, EventArgs e)
    {
        _bAusgangAktive = true;
        CopyDataRowdtoAArtikel(false);
        SumArtikelAusgang();

        InitDGV();
    }
    ///<summary>frmAuslagerung/tsbtnAllToAusgang_Click</summary>
    ///<remarks>Übernimmt den kompletten Artikelbestand in die Ausgangstabelle.</remarks>
    private void tsbtnAllToAusgang_Click(object sender, EventArgs e)
    {
        _bAusgangAktive = true;
        CopyDataRowdtoAArtikel(true);
        SumArtikelAusgang();
    }
    ///<summary>frmAuslagerung/tsbtnDelAllFromAAusgang_Click</summary>
    ///<remarks>Löscht alle Artikel aus der Auslagerungstable und läd das Artikelgrid neu.</remarks>
    private void tsbtnDelAllFromAAusgang_Click(object sender, EventArgs e)
    {
        _bAusgangAktive = true;
        RemoveRowFromArtAusgang(true);
        SumArtikelAusgang();
    }
    ///<summary>frmAuslagerung/tsbtnDelArtFromAAusgang_Click</summary>
    ///<remarks>Löscht den .</remarks>
    private void tsbtnDelArtFromAAusgang_Click(object sender, EventArgs e)
    {
        _bAusgangAktive = true;
        RemoveRowFromArtAusgang(false);
        SumArtikelAusgang();
    }
    ///<summary>frmAuslagerung/CopyDataRowdtAArtikel</summary>
    ///<remarks>Copiert den gewählten Datensatz in die Ausgangstable und löscht den entsprechenden 
    ///         Datensatz aus der Aritkeltabelle.</remarks>
    private void RemoveRowFromArtAusgang(bool bAll)
    {
        if (this.dgvAArtikel.CurrentCell != null)
        {
            decimal decTmpArtID = (decimal)this.dgvAArtikel.Rows[this.dgvAArtikel.CurrentCell.RowIndex].Cells["ID"].Value;
            if (decTmpArtID > 0)
            {
                if (bAll)
                {
                    Int32 i=0;
                    while (i <= dtArtikelAuslagerung.Rows.Count-1)
                    {
                        if(dtArtikelAuslagerung.Rows.Count>0)
                        {
                            if (!(bool)this.dgvAArtikel.Rows[i].Cells["Check"].Value)
                            {
                                decTmpArtID = (decimal)this.dgvAArtikel.Rows[i].Cells["ID"].Value;
                                if (decTmpArtID > 0)
                                {
                                    clsLager.UpdateLArtikelLAusgang(GL_User, 0, decTmpArtID);
                                    clsLager.UpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, false);
                                    dtArtikelAuslagerung.Rows.RemoveAt(i);
                                    i = 0;
                                }
                                else
                                {
                                    i++;
                                }
                            }
                            else
                            {
                                i++;
                            }
                        }                    
                    }
                }
                else
                {
                    for (Int32 i = 0; i <= dtArtikelAuslagerung.Rows.Count - 1; i++)
                    {
                        if (!(bool)this.dgvAArtikel.Rows[i].Cells["Check"].Value)
                        {
                            if (decTmpArtID == (decimal)dtArtikelAuslagerung.Rows[i]["ID"])
                            {
                                clsLager.UpdateLArtikelLAusgang(GL_User, 0, decTmpArtID);
                                dtArtikelAuslagerung.Rows.RemoveAt(i);
                                break;
                            }
                        }                        
                    }
                }
            }
            InitDGV();
        }
    }
    ///<summary>frmAuslagerung/SetArtikelMoveButtonEnabled</summary>
    ///<remarks>Akiviert / Deaktiviert die Artikel Move Button.</remarks>
    private void SetArtikelMoveButtonEnabled(bool bEnabled)
    {
        tsbtnDelArtFromAAusgang.Enabled = bEnabled;
        tsbtnAllToAusgang.Enabled = bEnabled;
        tsbtnDelAllFromAAusgang.Enabled = bEnabled;
        tsbtnSelectedArtToAusgang.Enabled = bEnabled;
    }
    ///<summary>frmAuslagerung / tbMCAuftraggeber_TextChanged</summary>
    ///<remarks>Auftraggeber Adresssuche</remarks>
    private void tbMCAuftraggeber_TextChanged(object sender, EventArgs e)
    {
        //Adressdaten laden
        DataTable dt = new DataTable();
        dt = clsADR.GetADRList();
        DataTable dtTmp = new DataTable();

        string SearchText = tbMCAuftraggeber.Text.ToString();
        string Ausgabe = string.Empty;

        DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
        dtTmp = dt.Clone();
        foreach (DataRow row in rows)
        {
            Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
            dtTmp.ImportRow(row);
        }
        tbADRAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
        _ADRAuftraggeber = Functions.GetADR_IDFromTable(dtTmp);
    }
    ///<summary>frmAuslagerung / tbMCEmpfänger_TextChanged</summary>
    ///<remarks>Empfänger Adresssuche</remarks>
    private void tbMCEmpfänger_TextChanged(object sender, EventArgs e)
    {
        //Adressdaten laden
        DataTable dt = new DataTable();
        dt = clsADR.GetADRList();
        DataTable dtTmp = new DataTable();

        //Suchtext
        string SearchText = tbMCEmpfänger.Text.ToString();
        string Ausgabe = "";

        DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
        dtTmp = dt.Clone();

        foreach (DataRow row in rows)
        {
            Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
            dtTmp.ImportRow(row);
        }
        tbADREmpfänger.Text = Functions.GetADRStringFromTable(dtTmp);
        _ADREmpfänger = Functions.GetADR_IDFromTable(dtTmp);
        //Entladestelle gleich Empfänger setzen
        tbADREntladestelle.Text = tbADREmpfänger.Text;
        tbMCEntladestelle.Text = tbMCEmpfänger.Text;
        _ADREntladestelle = _ADREmpfänger;
    }
    ///<summary>frmAuslagerung / tbMCEntladestelle_TextChanged</summary>
    ///<remarks>Empfänger Adresssuche</remarks>
    private void tbMCEntladestelle_TextChanged(object sender, EventArgs e)
    {
        //Adressdaten laden
        DataTable dt = new DataTable();
        dt = clsADR.GetADRList();
        DataTable dtTmp = new DataTable();

        //Suchtext
        string SearchText = tbMCEntladestelle.Text.ToString();
        string Ausgabe = "";

        DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
        dtTmp = dt.Clone();

        foreach (DataRow row in rows)
        {
            Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
            dtTmp.ImportRow(row);
        }
        tbADREntladestelle.Text = Functions.GetADRStringFromTable(dtTmp);
        _ADREntladestelle = Functions.GetADR_IDFromTable(dtTmp);
    }
    ///<summary>frmAuslagerung / SumArtikelAusgang</summary>
    ///<remarks>Ermittel bei jeder Veränderung des Ausgangs Gewicht und Anzahl neu.</remarks>
    private void SumArtikelAusgang()
    {
        //Ermittlung und Anzeige im Eingangskopf von Anzahl und Gewicht
        object objNetto = 0;
        object objBrutto = 0;
        if (dtArtikelAuslagerung.Rows.Count > 0)
        {
            objNetto = dtArtikelAuslagerung.Compute("SUM(Netto)", "LVSNr>0");
            objBrutto = dtArtikelAuslagerung.Compute("SUM(Brutto)", "LVSNr>0");
        }
        if (_LAusgangTableID > 0)
        {
            clsLager.UpdateLAusgangGewichtNetto(GL_User, _LAusgangTableID, Convert.ToDecimal(objNetto.ToString()));
            clsLager.UpdateLAusgangGewichtBrutto(GL_User, _LAusgangTableID, Convert.ToDecimal(objBrutto.ToString()));
        }
        tbANetto.Text = Functions.FormatDecimal(Convert.ToDecimal(objNetto.ToString()));
        tbABrutto.Text = Functions.FormatDecimal(Convert.ToDecimal(objBrutto.ToString()));
        tbAAnzahl.Text = dtArtikelAuslagerung.Rows.Count.ToString();
    }
    ///<summary>frmAuslagerung / tsbtnSaveArtikelAusgang_Click</summary>
    ///<remarks>Artikel werden gespeichert für den Lagerausgang.</remarks>
    private void tsbtnSaveArtikelAusgang_Click(object sender, EventArgs e)
    {
        if (GL_User.Lager_BestandAnlegen)
        {
            //Checken ob LAusgangsID gespeichert ist
            Lager.Ausgang.LAusgangTableID = _LAusgangTableID;
            if (Lager.Ausgang.ExistLAusgangTableID())
            {
                Lager.Ausgang.MandantenID = _MandantenID;
                Lager.Ausgang.AbBereichID = GL_User.sys_ArbeitsbereichID;
                Lager.Ausgang.AddArtikelToLAusgang(ref dtArtikelAuslagerung);
            }
        }
        else
        {
            clsMessages.User_NoAuthen();
        }
    }
    ///<summary>frmAuslagerung / dgvAArtikel_CellClick</summary>
    ///<remarks>Artikel geprüft, Flag wird gesetzt.</remarks>
    private void dgvAArtikel_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == 0)
        {
            decimal decTmpArtID = (decimal)this.dgvAArtikel.Rows[e.RowIndex].Cells["ID"].Value;
            string strTmp = this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value.ToString();
            if ((bool)this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value == true)
            {
                this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value = false;
                clsLager.UpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, false);
            }
            else
            {
                this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value = true;
                clsLager.UpdateLAusgangArtikelChecked(this.GL_User, decTmpArtID, true);
            }
        }
    }
    ///<summary>frmAuslagerung / dgvAArtikel_CellFormatting</summary>
    ///<remarks>Anpassen des Hintergrunds im Grid bei setzen des Flag Aritkel geprüft.</remarks>
    private void dgvAArtikel_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.ColumnIndex == 0)
        {
            if ((bool)this.dgvAArtikel.Rows[e.RowIndex].Cells["Check"].Value == true)
            {
                e.CellStyle.BackColor = Color.Green;
            }
            else
            {
                e.CellStyle.BackColor = Color.Red;
            }        
        }
    }
    ///<summary>frmAuslagerung / pbCheckEingang_Click</summary>
    ///<remarks>Anpassen des Hintergrunds im Grid bei setzen des Flag Aritkel geprüft.</remarks>
    private void pbCheckAusgang_Click(object sender, EventArgs e)
    {
            //check ob alle geprüft sind
        if (CheckDTArtikelAuslagerungForArtCheck())
        {
            //Lagerausgang kann abgeschlossen werden
            pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.check;
            //LEingang entsprechend updaten
            clsLager.UpdateLAusgangSetAusgangAbgeschlossen(this.GL_User, Lager.LAusgangTableID, true);

            //Lagerausgangkopf enable
            SetLAusgangsKopfDatenEnabled(false);

            // enable MoveButtons
            SetArtikelMoveButtonEnabled(false);
            _bAusgangAktive = false;
            _bLAusgangIsCHecked = true;

            //Grid Artikel leeren
            dtArtikel.Rows.Clear();
        }
        else 
        {
            pbCheckAusgang.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
        }
    }
    ///<summary>frmAuslagerung / CheckDTArtikelAuslagerungForArtCheck</summary>
    ///<remarks>Prüft, ob alle Artikel im Lagerausgang gecheckt sind.</remarks>
    private bool CheckDTArtikelAuslagerungForArtCheck()
    {
        if (dtArtikelAuslagerung.Rows.Count > 0)
        {
            bool retVal = true;
            for (Int32 i = 0; i <= dtArtikelAuslagerung.Rows.Count - 1; i++)
            {
                if ((bool)dtArtikelAuslagerung.Rows[i]["Check"] == false)
                {
                    retVal = false;
                }
            }
            return retVal;
        }
        else
        {
            return false;
        }
    }
    ///<summary>frmAuslagerung / tsbtnBack_Click</summary>
    ///<remarks>Zurückblättern.</remarks>
    private void tsbtnBack_Click(object sender, EventArgs e)
    {
        GetNextLAusgang(true);
        SetLAusgangsdatenToFrm();
    }
    ///<summary>frmAuslagerung / tsbtnForward_Click</summary>
    ///<remarks>vorwärts.</remarks>
    private void tsbtnForward_Click(object sender, EventArgs e)
    {
        GetNextLAusgang(false);
        SetLAusgangsdatenToFrm();
    }
    ///<summary>frmAuslagerung / tsbtnDeleteLAusgang_Click</summary>
    ///<remarks>Ausgang löschen nur wenn noch nicht abgeschlossen.</remarks>
    private void tsbtnDeleteLAusgang_Click(object sender, EventArgs e)
    {
        if (GL_User.Lager_BestandLoeschen)
        {
            if (!_bLAusgangIsCHecked)
            {
                if (Lager.Ausgang.ExistLAusgangTableID())
                {
                    Lager.Ausgang.DeleteLAusgangByLAusgangTableID();
                    GetNextLAusgang(false);
                    SetLAusgangsdatenToFrm();
                }
            }
        }
    }
    ///<summary>frmAuslagerung / cbFahrzeug_SelectedIndexChanged</summary>
    ///<remarks>Fahrzeug wurde ausgewählt.</remarks>
    private void cbFahrzeug_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(cbFahrzeug.SelectedIndex>-1)
        {
            string strTmp = cbFahrzeug.SelectedValue.ToString();   
            if(cbFahrzeug.SelectedValue.ToString()=="-1")
            {
                //Auswahl Fremdfahrzeuge
                //Felder aktivieren
                SetFelderFremdfahrzeugeEnabled(true);
            }
            else
            {
                //Auswahl eigene Fahrzeuge
                SetFelderFremdfahrzeugeEnabled(false);
            }
            tbKFZ.Focus();
            tbKFZ.Text = cbFahrzeug.Text;
        }
        else
        {
            SetFelderFremdfahrzeugeEnabled(false);
        }
    }
    ///<summary>frmAuslagerung / SetFelderFremdfahrzeugeEnabled</summary>
    ///<remarks>.</remarks>
    private void SetFelderFremdfahrzeugeEnabled(bool bEndabled)
    {
        tbMCSpedition.Enabled = bEndabled;
        tbADRSpedition.Enabled = bEndabled;
        btnSpedition.Enabled = bEndabled;
        if (!bEndabled)
        {
            tbMCSpedition.Text = string.Empty;
            tbADRSpedition.Text = string.Empty;
        }
    }
    ///<summary>frmAuslagerung / GetTerminDateTime</summary>
    ///<remarks>Setzt den Termin anhand von dem Datum und den gewählten Stunden / Min zusammen.</remarks>
    private DateTime GetTerminDateTime()
    { 
        string strStd= string.Empty;
        string strMin = string.Empty;
        string strDateTime = string.Empty;

        //Stunden
        if(nudStd.Value<10)
        {
            strStd = "0" + nudStd.Value.ToString();
        }
        else
        {
            strStd = nudStd.Value.ToString();
        }
        //Minuten
        if (nudMin.Value < 10)
        {
            strMin = "0" + nudMin.Value.ToString();
        }
        else
        {
            strMin = nudMin.Value.ToString();
        }

        strDateTime = dtpT_date.Value.ToShortDateString() + " " + strStd + ":" + strMin;
        DateTime dtTermin = Convert.ToDateTime(strDateTime);
        return dtTermin;
    }
    ///<summary>frmAuslagerung / dtpT_date_ValueChanged</summary>
    ///<remarks>Termin muss gleich oder jünger als das Lagerausgangsdatum sein</remarks>
    private void dtpT_date_ValueChanged(object sender, EventArgs e)
    {
        if (dtpT_date.Value < dtpAusgangDate.Value)
        {
            dtpT_date.Value = dtpAusgangDate.Value;
        }

    }


    


  }
}
