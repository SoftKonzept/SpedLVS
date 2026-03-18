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
  public partial class frmEinlagerung : frmTEMPLATE
  {
    public Globals._GL_USER GL_User;
    public ctrMenu _ctrMenu = null;
    public Int32 SearchButton = 0;
    internal clsLager Lager = new clsLager();
    internal DataTable dtMandanten;
    internal DataTable dtArtikel;
    internal bool bUpdate = false;
    internal bool bUpdateArtikel = false;
    internal bool bBack = false;
    internal DateTime EDatum = DateTime.Today.Date;

    internal decimal _LVSNr = 0;
    internal decimal _ArtikelTableID = 0;
    internal decimal _ADR_ID_A = 0;
    internal decimal _ADR_ID_E = 0;
    internal decimal _ADR_ID_V = 0;
    internal decimal _ADR_ID_Sped = 0;
    //internal decimal _LEingangID = 0;
    //internal decimal _LEingangTableID;
    internal decimal _MandantenID = 0;
    internal decimal _decGArtID = 0;
    //internal decimal _defaultLEingangTableID = -1;  //Null Default in Artikel ist 0 und es würden alle Daten aus Dispo angezeigt
    internal string _MandantenName=string.Empty;
    internal string _lTextGArtID = "Güterart ID: #";
    internal bool _bLEingangIsChecked;
    internal bool _bArtikelIsChecked;


    ///<summary>frmEinlagerung / frmEinlagerung</summary>
    ///<remarks></remarks>
    public frmEinlagerung()
    {
      InitializeComponent();
      bUpdate = true;
      bUpdateArtikel = true;
    }
    ///<summary>frmEinlagerung / frmLager_Load</summary>
    ///<remarks></remarks>
    private void frmLager_Load(object sender, EventArgs e)
    {
        dtMandanten = new DataTable("Mandanten");
        Functions.InitComboMandanten(GL_User, ref tscbMandanten, ref dtMandanten, true);

        //Combo Fahrzeuge
        Functions.InitComboFahrzeuge(this.GL_User, ref cbFahrzeug);

        Lager.LEingangTableID = 0;
        Lager._GL_User = this.GL_User;
        Lager.MandantenID = _MandantenID;
        Lager.FillLagerDaten();

        InitLoad();  

        //CHeck ob ein Auftrag vorhanden ist, es kann nur kein Auftrag vorhanden sein, wenn komplett neu begonnen wird
        //Datenbank ist leer
        decimal decTmp=-1;
        if (!decimal.TryParse(tbLEingangID.Text, out decTmp))
        {
            gboxLEDaten.Enabled = false;
        }
        else
        {
            gboxLEDaten.Enabled = true;
        }
    }
    ///<summary>frmEinlagerung / InitLoad</summary>
    ///<remarks></remarks>
    private void InitLoad()
    {
        //setzten einiger Infos / Variablen
        SetLabelGArdIDInfo();
        dtArtikel = new DataTable();

        //Button LEingang Speichern
        tsbtnEinlagerungSpeichern.Enabled = false;
        tsbtnDeleteLEingang.Enabled = false;

        //Button Artikelbereich
        SetArtikelMenuBtnEnabled(false);

        //Setz den letzten Lagereingang und die Artikel für das Grid
        SetLEingangskopfdatenToFrm(false);
        
        //Artikeleingabefelder deaktivieren
        SetArtikelEingabefelderDatenEnable(false); 
    }
    ///<summary>frmEinlagerung / CreateArtIDRef</summary>
    ///<remarks>Erstellung der Artikel - ID - Ref. Zusammensetzung:
    ///         Lieferantennummer (9stellig)
    ///         +
    ///         LVSNR ( 8Stellig)
    ///         +
    ///         Produktionsnummer (9stellig)</remarks>
    private void CreateArtIDRef()
    {
        string strLieferantennummer = "000000000";
        string strLVSNr="00000000";
        string strProduktionsnummer="000000000";

        //1. Teil Lieferantennummer 
        strLieferantennummer=strLieferantennummer+tbLieferantenID.Text.ToString().Trim();
        strLieferantennummer=strLieferantennummer.Substring(strLieferantennummer.Length-9);

        //2. Teil LVSNR
        strLVSNr = strLVSNr+_LVSNr.ToString().Trim();
        strLVSNr=strLVSNr.Substring(strLVSNr.Length-8);

        //3. Teil Produktionsnummer
        strProduktionsnummer=strProduktionsnummer+tbProduktionsNr.Text.ToString().Trim();
        strProduktionsnummer=strProduktionsnummer.Substring(strProduktionsnummer.Length-9);

        string strTmp =strLieferantennummer+strLVSNr+strProduktionsnummer;
        if (strTmp.Length == 26)
        {
            tbArtIDRef.Text = strTmp;
        }
        else
        {
            if (strTmp.Length > 26)
            {
                strTmp = strTmp.Substring(strTmp.Length - 26);
            }
            if (strTmp.Length < 26)
            {
                strTmp = strTmp + "0000000000";
                strTmp = strTmp.Substring(1, 26);
            }
        }
    }

    /*********************************************************************************************
     *                              Methoden für Eingangsdaten / Eingangskopf
     * ******************************************************************************************/
    ///<summary>frmEinlagerung / tsbtnDeleteLEingang_Click</summary>
    ///<remarks>Der angezeigte, nicht abgeschlossene Lagereingang wird gelöscht.</remarks>  
    private void tsbtnDeleteLEingang_Click(object sender, EventArgs e)
    {
        if (!_bLEingangIsChecked)
        {
            if (GL_User.Lager_BestandLoeschen)
            {                
                if (Lager.Eingang.ExistLEingangTableID())
                {
                    Lager.Eingang.DeleteLEingangByLEingangTableID();
                    //nächsten Eingang anzeigen
                    tsbtnForward_Click(this, e);
                }
            }
            else
            {
                clsMessages.User_NoAuthen();
            }           
        }
    }
    ///<summary>frmEinlagerung / tbLfsNr_Validated</summary>
    ///<remarks></remarks>  
    private void tbLfsNr_Validated(object sender, EventArgs e)
    {
        if (!bUpdate)
        {
            CreateArtIDRef();
        }
    }
    ///<summary>frmEinlagerung / toolStripButton3_Click</summary>
    ///<remarks>Schließt die Einlagerungsform.</remarks>  
    private void tsbtnClose_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    ///<summary>frmEinlagerung / tsbtnArtGrid_Click</summary>
    ///<remarks>Öffnet / Schließt das Panel zur Ansicht des Artikel-Grids.</remarks>       
    private void tscbMandanten_SelectedIndexChanged(object sender, EventArgs e)
    {
        Functions.SetMandantenDaten(ref tscbMandanten, ref _MandantenID, ref _MandantenName);
        Lager.MandantenID = _MandantenID;
        _ArtikelTableID = 0;
        //Mandant wurde geändert, so muss LEingang- und LAusgangsID =0 gesetzt werden
        Lager.LEingangTableID = 0;
        Lager.LAusgangTableID = 0;
        Lager.FillLagerDaten();
        ClearEingangsuebersicht();
        InitLoad();
        InitArtikelLoad();
    }
    ///<summary>frmEinlagerung / InitEingabe</summary>
    ///<remarks>Form wird für die Eingabe von Daten vorbereite.</remarks>
    private void InitEingabe()
    {
        //aktivieren der Eingabefelder
        SetLagerEingangsFelderEnabled(true);
        ClearEingangsuebersicht();
        //Defaultwert in die Eingabefelder setzen
        SetLEingangsFelderToDefault();
        if (!bUpdate)
        {
            //Ermitteln der neuen EingangsID
            SetLEingangsID();
        }
        //Eingang abgeschlossen?
        SetLEingangCheck();
        //Artikel GRD Neuladen
        InitDGV();
    }
    ///<summary>frmEinlagerung / SetEingabefelderDatenEnable</summary>
    ///<remarks>Gibt die Eingabefelder für einen neuen Eingang frei.</remarks>
    private void SetLEingangsFelderToDefault()
    {
        dtpEinlagerungDate.Value = DateTime.Now;
        tbLEingangID.Text = "0";
        tbDFUE.Text = "0";
        tbEArtAnzahl.Text = "0";
        tbBruttoGesamt.Text = "0";
        tbNettoGesamt.Text = "0";
        tbLieferantenID.Text = "0";
        tbLfsNr.Text = string.Empty;
        tbMCSpedition.Text = string.Empty;
        tbADRSpedition.Text = string.Empty;
        tbKFZ.Text = string.Empty;
        cbFahrzeug.SelectedIndex = -1;
        //cbFahrzeug.SelectedIndex = 0;

    }
    ///<summary>frmEinlagerung / btnSearchA_Click</summary>
    ///<remarks>Pffnet die ADR-From zur Adresssuche.</remarks>
    private void btnSearchA_Click(object sender, EventArgs e)
    {
        SearchButton = 1;
        _ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmEinlagerung / btnSearchV_Click</summary>
    ///<remarks>Pffnet die ADR-From zur Adresssuche.</remarks>
    private void btnSearchV_Click(object sender, EventArgs e)
    {
        SearchButton = 2;
        _ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmEinlagerung / btnSearchE_Click</summary>
    ///<remarks>Pffnet die ADR-From zur Adresssuche.</remarks>
    private void btnSearchE_Click(object sender, EventArgs e)
    {
        SearchButton = 3;
        _ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmEinlagerung / btnSpedition_Click</summary>
    ///<remarks>.</remarks>
    private void btnSpedition_Click(object sender, EventArgs e)
    {
        //_ADRSearch = "Spedition";
        SearchButton = 5;
        this._ctrMenu.OpenADRSearch(this);
    }
    ///<summary>frmEinlagerung / tbSearchA_TextChanged</summary>
    ///<remarks>Auftraggeber Adresssuche</remarks>
    private void tbSearchA_TextChanged(object sender, EventArgs e)
    {
        //Adressdaten laden
        DataTable dt = new DataTable();
        dt = clsADR.GetADRList();
        DataTable dtTmp = new DataTable();

        string SearchText = tbSearchA.Text.ToString();
        string Ausgabe = string.Empty;

        DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
        dtTmp = dt.Clone();
        foreach (DataRow row in rows)
        {
            Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
            dtTmp.ImportRow(row);
        }
        tbAuftraggeber.Text = Functions.GetADRStringFromTable(dtTmp);
        _ADR_ID_A = Functions.GetADR_IDFromTable(dtTmp);
    }
    ///<summary>frmEinlagerung / tbSearchV_TextChanged</summary>
    ///<remarks>Versender Adresssuche</remarks>
    private void tbSearchV_TextChanged(object sender, EventArgs e)
    {
        //Adressdaten laden
        DataTable dt = new DataTable();
        dt = clsADR.GetADRList();
        DataTable dtTmp = new DataTable();

        string SearchText = tbSearchV.Text.ToString();
        string Ausgabe = "";
        
        DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
        dtTmp = dt.Clone();

        foreach (DataRow row in rows)
        {
            Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
            dtTmp.ImportRow(row);
        }
        tbVersender.Text = Functions.GetADRStringFromTable(dtTmp);
        _ADR_ID_V = Functions.GetADR_IDFromTable(dtTmp);
    }
    ///<summary>frmEinlagerung / tbSearchE_TextChanged</summary>
    ///<remarks>Empfänger Adresssuche</remarks>
    private void tbSearchE_TextChanged(object sender, EventArgs e)
    {
        //Adressdaten laden
        DataTable dt = new DataTable();
        dt = clsADR.GetADRList();
        DataTable dtTmp = new DataTable();

        //Suchtext
        string SearchText = tbSearchE.Text.ToString();
        string Ausgabe = "";

        DataRow[] rows = dt.Select("Suchbegriff LIKE '" + SearchText + "'", "Suchbegriff");
        dtTmp = dt.Clone();

        foreach (DataRow row in rows)
        {
            Ausgabe = Ausgabe + row["Suchbegriff"].ToString() + "\n";
            dtTmp.ImportRow(row);
        }
        tbEmpfaenger.Text = Functions.GetADRStringFromTable(dtTmp);
        _ADR_ID_E = Functions.GetADR_IDFromTable(dtTmp);
    }
     ///<summary>frmEinlagerung / SetKDRecAfterADRSearch</summary>
    ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
    ///<param name="ADR_ID">ADR_ID</param>
    public void SetKDRecAfterADRSearch(decimal myDecADR_ID)
    {
        string strE = string.Empty;
        string strMC = string.Empty;
        DataSet ds = clsADR.ReadADRbyID(myDecADR_ID);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            strMC = ds.Tables[0].Rows[i]["ViewID"].ToString();
            strE = ds.Tables[0].Rows[i]["ViewID"].ToString() + " - ";
            strE = strE + ds.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
            strE = strE + ds.Tables[0].Rows[i]["Name1"].ToString() + " - ";
            strE = strE + ds.Tables[0].Rows[i]["PLZ"].ToString() + " - ";
            strE = strE + ds.Tables[0].Rows[i]["Ort"].ToString();

            //SearchButton
            // 1 = KD /Auftraggeber
            // 2 = Versender
            // 3 = Empfänger
            // 5 = Spedition
            switch (SearchButton)
            {
                case 1:
                    _ADR_ID_A = myDecADR_ID;
                    tbSearchA.Text = strMC;
                    tbAuftraggeber.Text = strE;
                    break;

                case 2:
                    _ADR_ID_V= myDecADR_ID;
                    tbSearchV.Text = strMC;
                    tbVersender.Text= strE;
                    break;

                case 3:
                    _ADR_ID_E = myDecADR_ID;
                    tbSearchE.Text = strMC;
                    tbEmpfaenger.Text = strE;
                    break;

                case 5:
                    _ADR_ID_Sped = myDecADR_ID;
                    tbMCSpedition.Text = strMC;
                    tbADRSpedition.Text = strE;
                    break;
            }
        }
    }
    ///<summary>frmEinlagerung / SetKDRecAfterADRSearch</summary>
    ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
    ///<param name="ADR_ID">ADR_ID</param>
    private void tsbtnEinlagerungSpeichern_Click(object sender, EventArgs e)
    {
        if(!_bLEingangIsChecked)
        {
            //Userberechtigung prüfen
            if (GL_User.Lager_BestandAnlegen)
            {
                if (CheckLEingangkopfdaten())
                {
                    Lager.Eingang.AbBereichID = this.GL_User.sys_ArbeitsbereichID;
                    Lager.Eingang.MandantenID = this._MandantenID;
                    decimal decTmp = 0;
                    Decimal.TryParse(tbLEingangID.Text, out decTmp);
                    Lager.Eingang.LEingangID = decTmp;
                    Lager.Eingang.LEingangDate = dtpEinlagerungDate.Value;
                    Lager.Eingang.LEingangLfsNr = tbLfsNr.Text.ToString().Trim();
                    Lager.Eingang.Auftraggeber = this._ADR_ID_A;
                    Lager.Eingang.Empfaenger = this._ADR_ID_E;
                    Lager.Eingang.Versender = this._ADR_ID_V;
                    Lager.Eingang.LieferantenID = Convert.ToDecimal(tbLieferantenID.Text.ToString().Trim());
                    Lager.Eingang.SpedID = this._ADR_ID_Sped;
                    Lager.Eingang.KFZ = tbKFZ.Text;

                    if (bUpdate)
                    {
                        //Update Daten
                        if (!Lager.Eingang.ExistLEingangTableID())
                        {
                            Lager.Eingang.GetLEingangTableID();
                        }
                        Lager.Eingang.UpdateLagerEingang();
                    }
                    else
                    {
                        //Insert Daten
                        Lager.Eingang.AddLagerEingang();
                        Lager.LEingangTableID = Lager.Eingang.LEingangTableID;
                    }
                    Lager.FillLagerDaten();
                    //Da die Daten jetzt sichtbar sind und geblättert werden kann muss nun Update = true gesetzt werden
                    bUpdate = true;

                    //Button ArtikelNeu aktivieren
                    tsbtnArtikelNeu.Enabled = true;
                }
            }  
            else
            {
                clsMessages.User_NoAuthen();
            }
        }
        //ArtikelVita aktualisieren
        InitGrdArtVita();
    }
    ///<summary>frmEinlagerung / SetKDRecAfterADRSearch</summary>
    ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
    ///<param name="ADR_ID">ADR_ID</param>
    private bool CheckLEingangkopfdaten()
    {
        bool EingabeOK = true;
        string strHelp = string.Empty;
        Int32 result = 0;
        decimal decResult = 0.0m;


        //Auftraggeber
        if (tbLEingangID.Text == string.Empty)
        {
            EingabeOK = false;
            strHelp = strHelp + "Eingangsnummer fehlt \n\r";
        }
        //Auftraggeber
        if (tbAuftraggeber.Text==string.Empty)
        {
            EingabeOK = false;
            strHelp = strHelp + "Auftraggeber fehlt \n\r";
        }
        if (tbEmpfaenger.Text == string.Empty)
        {
            EingabeOK = false;
            strHelp = strHelp + "Empfänger fehlt \n\r";
        }
        //Lieferscheinnummer
        if (tbLfsNr.Text == string.Empty)
        {
            EingabeOK = false;
            strHelp = strHelp + "Lieferscheinnummer fehlt \n\r";           
        }
        if (!EingabeOK)
        {
            MessageBox.Show(strHelp, "Achtung");
        }
        return EingabeOK;
    }
    ///<summary>frmEinlagerung / tsbtnNeueEinlagerung_Click</summary>
    ///<remarks>Init neue Einlagerung.</remarks>
    private void tsbtnNeueEinlagerung_Click(object sender, EventArgs e)
    {
        if (_MandantenID > 0)
        {
            Lager.LEingangTableID = 0;
            Lager.FillLagerDaten();
            _ArtikelTableID = 0;
            _bLEingangIsChecked = false;
            _bArtikelIsChecked = false;
            _LVSNr = 0;
            bUpdate = false;

            //Eingabefelder leeren und neue LEingangsID wird ermittelt
            InitEingabe();
            //Artikelbereich Reset
            SetArtikelMenuBtnEnabled(false);
            //ArtikelEingabefelder leeren
            ClearArtikelEingabeFelder();
        }
        else
        {
            clsMessages.Allgemein_MandantFehlt();
        }
    }
    ///<summary>frmEinlagerung / SetLagerEingangsFelderEnabled</summary>
    ///<remarks>Aktiviert die entsprechenden Button und Eingabefelder für die neue Einlagerung.</remarks>
    private void SetLagerEingangsFelderEnabled(bool Enable)
    {
        //Angaben in Menüzeile
        tbLEingangID.Enabled = Enable;
        dtpEinlagerungDate.Enabled = Enable;
        tbDFUE.Enabled = Enable;

        gboxLEDaten.Enabled = Enable;
    }
    ///<summary>frmEinlagerung / tsbtnBack_Click</summary>
    ///<remarks>Eingänge blättern</remarks>
    private void tsbtnBack_Click(object sender, EventArgs e)
    {
        bUpdate = true;
        bBack = false;
        InitArtikelLoad();
        //Eingabefelder leeren und neue LEingangsID wird ermittelt
        InitEingabe();
        SetLEingangskopfdatenToFrm(false);        
    }
    ///<summary>frmEinlagerung / tsbtnForward_Click</summary>
    ///<remarks>Eingänge blättern</remarks>
    private void tsbtnForward_Click(object sender, EventArgs e)
    {
        bUpdate = true;
        bBack = true;
        InitArtikelLoad();
        //Eingabefelder leeren und neue LEingangsID wird ermittelt
        InitEingabe();
        SetLEingangskopfdatenToFrm(false);
    }
    ///<summary>frmEinlagerung / InitArtikelLoad</summary>
    ///<remarks></remarks>
    private void InitArtikelLoad()
    {
        //ArtikelEingabefelder leeren
        ClearArtikelEingabeFelder();
        //ArtikelVita aktualisieren
        InitGrdArtVita();
        //ArtikelGrid laden
        InitDGV();
    }
    ///<summary>frmEinlagerung / tsbtnForward_Click</summary>
    ///<remarks>Eingänge blättern</remarks>
    private void SetLEingangskopfdatenToFrm(bool bReload)
    {
        if (_MandantenID > 0)
        {
            if (!bReload)
            {
                Lager.Eingang.GetNextLEingangsID(bBack);
            }
            Lager.LEingangTableID = Lager.Eingang.LEingangTableID;
            Lager.FillLagerDaten();

            if (Lager.Eingang.ExistLEingangTableID())
            {              
                dtpEinlagerungDate.Value = Lager.Eingang.LEingangDate;
                tbLEingangID.Text = Lager.Eingang.LEingangID.ToString();
                tbDFUE.Text = Lager.Eingang.ASN.ToString();

                //Adressen setzen -> damit die richtige Adresse erkannt wird muss der SearchButton gesetzt werden
                //SearchButton
                // 1 = KD
                // 2 = Versender
                // 3 = Empfänger
                _ADR_ID_A = Lager.Eingang.Auftraggeber;
                SearchButton = 1;
                SetKDRecAfterADRSearch(_ADR_ID_A);
                _ADR_ID_V = Lager.Eingang.Versender;
                SearchButton = 2;
                SetKDRecAfterADRSearch(_ADR_ID_V);
                _ADR_ID_E = Lager.Eingang.Empfaenger;
                SearchButton = 3;
                SetKDRecAfterADRSearch(_ADR_ID_E);
                _ADR_ID_Sped = Lager.Eingang.SpedID;
                SearchButton = 5;
                SetKDRecAfterADRSearch(_ADR_ID_Sped);

                tbBruttoGesamt.Text = Functions.FormatDecimal(Lager.Eingang.GewichtBrutto);
                tbNettoGesamt.Text = Functions.FormatDecimal(Lager.Eingang.GewichtNetto);

                tbLieferantenID.Text = Lager.Eingang.LieferantenID.ToString();
                tbLfsNr.Text = Lager.Eingang.LEingangLfsNr;

                if (Lager.Eingang.KFZ != string.Empty)
                {
                    Functions.SetComboToSelecetedItem(ref cbFahrzeug, Lager.Eingang.KFZ);
                }
                tbKFZ.Text = Lager.Eingang.KFZ;
                SetLEingangCheck();            

            }
            //Artikel in Datagrid laden
            InitDGV();
        }
    }
    ///<summary>frmEinlagerung / SetLEingangCheck</summary>
    ///<remarks>.</remarks>    
    private void SetLEingangArtikelCheck()
    {
        if (_bArtikelIsChecked)
        {
            //Eingang bereits geprüft
            pbCheckArtikel.Image =(Image) Sped4.Properties.Resources.check;
            SetArtikelMenuBtnEnabled(false);
            //tsbtnArtikelNeu.Enabled = true;
            SetArtikelEingabefelderDatenEnable(false);
        }
        else
        {
            pbCheckArtikel.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
            SetArtikelMenuBtnEnabled(true);
            SetArtikelEingabefelderDatenEnable(true);
        }
        //this.pbCheckArtikel.Refresh();
    } 
    ///<summary>frmEinlagerung / ClearEingangsuebersicht</summary>
    ///<remarks>Leert alle Eingabefelder der Eingangsdaten.</remarks>     
    private void ClearEingangsuebersicht()
    {
        tbLEingangID.Text = string.Empty;
        tbEArtAnzahl.Text = string.Empty;
        tbNettoGesamt.Text = string.Empty;
        tbBruttoGesamt.Text = string.Empty;
        tbSearchA.Text = string.Empty;
        tbSearchV.Text = string.Empty;
        tbSearchE.Text = string.Empty;
        tbAuftraggeber.Text = string.Empty;
        tbVersender.Text = string.Empty;
        tbEmpfaenger.Text = string.Empty;
        tbLfsNr.Text = string.Empty;

        tbMCSpedition.Text = string.Empty;
        tbADRSpedition.Text = string.Empty;
        tbKFZ.Text = string.Empty;
        cbFahrzeug.SelectedIndex = -1;
    }
    ///<summary>frmEinlagerung / pbCheckEingang_Click</summary>
    ///<remarks>Eingang kann abgeschlossen werden, wernn alle Artikel im Eingang geprüft wurden.</remarks> 
    private void pbCheckEingang_Click(object sender, EventArgs e)
    {
        if (!_bLEingangIsChecked)
        {
            //Alle Artikel auf CheckArt prüfen
            if (clsArtikel.CheckAllArtikelChecked(GL_User.User_ID, Lager.LEingangTableID))
            {
                _bLEingangIsChecked = true;
            }
            else
            {
                _bLEingangIsChecked = false;
            }
            clsLager.UpdateLEingangCheck(GL_User.User_ID, _bLEingangIsChecked, Lager.LEingangTableID);
        }

        SetLEingangCheck();
        InitGrdArtVita();
    }
    ///<summary>frmEinlagerung / SetLEingangCheck</summary>
    ///<remarks>.</remarks>    
    private void SetLEingangCheck()
    {
        //_bLEingangIsChecked = clsLager.GetLEingangCheck(GL_User, Lager.LEingangTableID);
        Lager.FillLagerDaten();
        if (Lager.Eingang.Checked)
        {
            //Eingang bereits geprüft
            pbCheckEingang.Image = (Image)Sped4.Properties.Resources.check;
            //Artikelmenu enabled setzen
            SetArtikelMenuBtnEnabled(false);
           // tsbtnArtikelNeu.Enabled = false;
            tsbtnDeleteLEingang.Enabled = false;
        }
        else
        {
            pbCheckEingang.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
           // tsbtnArtikelNeu.Enabled = true;
            tsbtnDeleteLEingang.Enabled = true;
        }
        //Deaktiviert die Eingabefelder im Lager Eingangsbereich
        SetLagerEingangsFelderEnabled(!Lager.Eingang.Checked);
        _bLEingangIsChecked = Lager.Eingang.Checked;
    }

    /***************************************************************************************************
    *                                Methoden für Artikel
    * ************************************************************************************************/
    ///<summary>frmEinlagerung / tbBreite_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
    private void tbProduktionsNr_Validated(object sender, EventArgs e)
    {
        if (!bUpdateArtikel)
        {
            CreateArtIDRef();
        }
    }
    ///<summary>frmEinlagerung / tbBreite_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
    private void tbBreite_Validated(object sender, EventArgs e)
    {
        decimal decTmp = 0;
        if (!Decimal.TryParse(tbBreite.Text, out decTmp))
        {
            clsMessages.Allgemein_EingabeFormatFehlerhaft();
        }
        if (decTmp < 0)
        {
            decTmp = decTmp * (-1);
        }
        tbBreite.Text = Functions.FormatDecimal(decTmp);
    }
    ///<summary>frmEinlagerung / tbLaenge_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
    private void tbLaenge_Validated(object sender, EventArgs e)
    {
        decimal decTmp = 0;
        if (!Decimal.TryParse(tbLaenge.Text, out decTmp))
        {
            clsMessages.Allgemein_EingabeFormatFehlerhaft();
        }
        if (decTmp < 0)
        {
            decTmp = decTmp * (-1);
        }
        tbLaenge.Text = Functions.FormatDecimal(decTmp);
    }
    ///<summary>frmEinlagerung / tbHoehe_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
    private void tbHoehe_Validated(object sender, EventArgs e)
    {
        decimal decTmp = 0;
        if (!Decimal.TryParse(tbHoehe.Text, out decTmp))
        {
            clsMessages.Allgemein_EingabeFormatFehlerhaft();
        }
        if (decTmp < 0)
        {
            decTmp = decTmp * (-1);
        }
        tbHoehe.Text = Functions.FormatDecimal(decTmp);
    }
    ///<summary>frmEinlagerung / tbNetto_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
    private void tbNetto_Validated(object sender, EventArgs e)
    {
        decimal decTmp = 0;
        if (!Decimal.TryParse(tbNetto.Text, out decTmp))
        {
            clsMessages.Allgemein_EingabeFormatFehlerhaft();
        }
        if (decTmp < 0)
        {
            decTmp = decTmp * (-1);
        }
        tbNetto.Text = Functions.FormatDecimal(decTmp);
    }
    ///<summary>frmEinlagerung / tbBrutto_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
    private void tbBrutto_Validated(object sender, EventArgs e)
    {
        decimal decTmp = 0;
        if (!Decimal.TryParse(tbBrutto.Text, out decTmp))
        {
            clsMessages.Allgemein_EingabeFormatFehlerhaft();
        }
        if (decTmp < 0)
        {
            decTmp = decTmp * (-1);
        }
        tbBrutto.Text = Functions.FormatDecimal(decTmp);

        //Bruttowert >0 dann Berechnung des Packmittels
        if(decTmp>0)
        {
            decimal decNet = Convert.ToDecimal(tbNetto.Text);
            decimal decBru = Convert.ToDecimal(tbBrutto.Text);
            decimal decPack = 0;
            if (decNet > decBru)
            {
                clsMessages.Allgemein_BruttoKleinerNetto();
            }
            else
            {
                decPack = decBru - decNet;
            }
            tbPackmittelGewicht.Text = Functions.FormatDecimal(decPack);
        }

    }
    ///<summary>frmEinlagerung / tbPackmittelGewicht_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>  
    private void tbPackmittelGewicht_Validated(object sender, EventArgs e)
    {
        decimal decTmp = 0;
        if (!Decimal.TryParse(tbPackmittelGewicht.Text, out decTmp))
        {
            clsMessages.Allgemein_EingabeFormatFehlerhaft();
        }
        if (decTmp < 0)
        {
            decTmp = decTmp * (-1);
        }
        tbPackmittelGewicht.Text = Functions.FormatDecimal(decTmp);
    }
    ///<summary>frmEinlagerung / tbAnzahl_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>     
    private void tbAnzahl_Validated(object sender, EventArgs e)
    {
        Int32 iTmp = 0;
        if (!Int32.TryParse(tbAnzahl.Text, out iTmp))
        {
            clsMessages.Allgemein_EingabeFormatFehlerhaft();
        } if (iTmp < 0)
        {
            iTmp = iTmp * (-1);
        }
        tbAnzahl.Text = iTmp.ToString();
    }
    ///<summary>frmEinlagerung / tbDicke_Validated</summary>
    ///<remarks>Die Eingabe wird direkt auf das Format geprüft.</remarks>    
    private void tbDicke_Validated(object sender, EventArgs e)
    {
        decimal decTmp = 0;
        if (!Decimal.TryParse(tbDicke.Text, out decTmp))
        {
            clsMessages.Allgemein_EingabeFormatFehlerhaft();
        }
        if (decTmp < 0)
        {
            decTmp = decTmp * (-1);
        }
        tbDicke.Text = Functions.FormatDecimal(decTmp);
    }
    ///<summary>frmEinlagerung / TrimmEingabe</summary>
    ///<remarks>Trimm über alle Artikeleingabefelder.</remarks> 
    private void TrimmEingabe()
    {
        //ID-Ref
        tbAnzahl.Text = tbAnzahl.Text.ToString().Trim();
        tbGArt.Text = tbGArt.Text.ToString().Trim();
        tbGArtZusatz.Text = tbGArtZusatz.Text.ToString().Trim();
        tbWerksnummer.Text = tbWerksnummer.Text.ToString().Trim();
        tbProduktionsNr.Text = tbProduktionsNr.Text.ToString().Trim();
        tbexBezeichnung.Text = tbexBezeichnung.Text.ToString().Trim();
        tbBestellnummer.Text = tbBestellnummer.Text.ToString().Trim();
        tbCharge.Text = tbCharge.Text.ToString().Trim();
        tbPos.Text = tbPos.Text.ToString().Trim();
        tbexMaterialnummer.Text = tbexMaterialnummer.Text.ToString().Trim();
        tbArtIDRef.Text = tbArtIDRef.Text.ToString().Trim();

        //Abmessungen - Gewichte
        tbDicke.Text = tbDicke.Text.ToString().Trim();
        tbBreite.Text = tbBreite.Text.ToString().Trim();
        tbLaenge.Text = tbLaenge.Text.ToString().Trim();
        tbHoehe.Text = tbHoehe.Text.ToString().Trim();
        tbNetto.Text = tbNetto.Text.ToString().Trim();
        tbBrutto.Text = tbBrutto.Text.ToString().Trim();
    }      
    ///<summary>frmEinlagerung / ClearArtikelEingabeFelder</summary>
    ///<remarks>Leert alle Eingabefelder der Artikeldaten.</remarks>       
    private void ClearArtikelEingabeFelder()
    {
        //ID - RefNr
        tbLVSNr.Text = string.Empty;
        tbGArtZusatz.Text = string.Empty;
        tbGArt.Text = string.Empty;
        tbWerksnummer.Text = string.Empty;
        tbProduktionsNr.Text = string.Empty;
        tbCharge.Text = string.Empty;
        tbBestellnummer.Text = string.Empty;
        tbexMaterialnummer.Text = string.Empty;
        tbexBezeichnung.Text = string.Empty;
        tbPos.Text = string.Empty;
        tbArtIDRef.Text = string.Empty;

        //Maße/Gewichte
        tbAnzahl.Text = "0";
        tbDicke.Text = "0";
        tbBreite.Text = "0";
        tbLaenge.Text = "0";
        tbHoehe.Text = "0";
        tbNetto.Text = "0";
        tbBrutto.Text = "0";
        tbPackmittelGewicht.Text = "0"; ;

        //Lagerort
        tbLagerort.Text = string.Empty;

        //Label Güterart ID
        _decGArtID = 0;
        SetLabelGArdIDInfo();

        //Einheit
        cbEinheit.DataSource = Enum.GetNames(typeof(Globals.enumEinheit));
    }     
    ///<summary>frmEinlagerung / SetArtikelEingabefelderDatenEnable</summary>
    ///<remarks>Gibt die Eingabefelder für einen neuen Eingang frei.</remarks>
    private void SetArtikelEingabefelderDatenEnable(bool bEnabled)
    {
        //ID - Ref
        tbLVSNr.Enabled = bEnabled;
        tbGArt.Enabled = bEnabled;
        tbGArtZusatz.Enabled = bEnabled;
        tbWerksnummer.Enabled = bEnabled;
        tbProduktionsNr.Enabled = bEnabled;
        tbCharge.Enabled = bEnabled;
        tbBestellnummer.Enabled = bEnabled;
        tbexMaterialnummer.Enabled = bEnabled;
        tbexBezeichnung.Enabled = bEnabled;
        tbPos.Enabled = bEnabled;
        tbArtIDRef.Enabled = bEnabled;

        //Gewichte Abmessungen
        tbAnzahl.Enabled = bEnabled;
        tbDicke.Enabled = bEnabled;
        tbBreite.Enabled = bEnabled;
        tbLaenge.Enabled = bEnabled;
        tbHoehe.Enabled = bEnabled;
        tbNetto.Enabled = bEnabled;
        tbBrutto.Enabled = bEnabled;
        tbPackmittelGewicht.Enabled = bEnabled;

        //Lagerort
        tbLagerort.Enabled = bEnabled;
       
        //Button Güterarten 
        btnGArt.Enabled = bEnabled;
    }
    ///<summary>frmEinlagerung / SetEingangsID</summary>
    ///<remarks>Ermittelt und setzt die neue Lager EIngangsID.</remarks>
    private void SetLEingangsID()
    {
        decimal decTemp = 0;
        if (_MandantenID > 0)
        {
            decTemp = clsLager.GetNextLEingangID(GL_User, _MandantenID);
        }
        tbLEingangID.Text = decTemp.ToString();
    }
    ///<summary>frmEinlagerung / SetLVSNummer</summary>
    ///<remarks>Ermittelt für den neuen Eingang die nächste LVSNr.</remarks>
    private void SetLVSNummer()
    {
        if (_MandantenID > 0)
        {
            tbLVSNr.Text = clsLager.GetNextLVSNr(GL_User, _MandantenID).ToString();
        }
    }
    ///<summary>frmEinlagerung / CheckArtikelEingabe</summary>
    ///<remarks>Prüflogik für die Eingabefelder der Artikeldaten.</remarks>
    private bool CheckArtikelEingabe()
    {
        bool EingabeOK = true;
        string strHelp = string.Empty;
        Int32 result = 0;
        decimal decResult = 0.0m;

        //Güterart
        if (tbGArt.Text == "")
        {
            EingabeOK = false;
            strHelp = strHelp + "Güterart fehlt \n\r";
        }
        //ME
        if (!Int32.TryParse(tbAnzahl.Text, out result))
        {
            EingabeOK = false;
            strHelp = strHelp + "Anzahl hat das falsche Eingabeformat \n\r";
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

        //Baustelle 
        //für Heisiep ist der Lagerort kein Pflichtfeld
        //Lagerort
        /***
        if (tbWerk.Text.ToString().Trim() == string.Empty)
        {
            EingabeOK = false;
            strHelp = strHelp + "Lagerort Werkt keine Eingabe \n\r";
        }
        if (tbHalle.Text.ToString().Trim() == string.Empty)
        {
            EingabeOK = false;
            strHelp = strHelp + "Lagerort Halle keine Eingabe \n\r";
        }
        if (tbReihe.Text.ToString().Trim() == string.Empty)
        {
            EingabeOK = false;
            strHelp = strHelp + "Lagerort Reihe keine Eingabe \n\r";
        }
        if (tbPlatz.Text.ToString().Trim() == string.Empty)
        {
            EingabeOK = false;
            strHelp = strHelp + "Lagerort Platz keine Eingabe \n\r";
        }
        ****/
        if (!EingabeOK)
        {
            MessageBox.Show(strHelp, "Achtung");
        }
        return EingabeOK;
    }
    ///<summary>frmEinlagerung / button1_Click</summary>
    ///<remarks>Öffnen der Güterartenliste zur Suche / Übernahme der Güterart. Nur wenn ein Artikel (LVSNR) vorhanden ist.</remarks>
    private void button1_Click(object sender, EventArgs e)
    {
        decimal decTmp = -1;
        if(Decimal.TryParse(tbLVSNr.Text, out decTmp))
        {
            this._ctrMenu.OpenFrmGArtenList(this);
        }
    }
    ///<summary>frmEinlagerung / TakeOverGueterArt</summary>
    ///<remarks>Übernahme der gewählten Güterart</remarks>
    public void TakeOverGueterArt(decimal gaID)
    {
        _decGArtID = gaID;
        tbGArt.Text = clsGut.GetBezeichnungByID(_decGArtID);
        SetLabelGArdIDInfo();
    }
    ///<summary>frmEinlagerung / tsbtnArtVita_Click</summary>
    ///<remarks>Öffnet / Schließt das Panel zur Ansicht der Artikel-Vita.</remarks>  
    private void tsbtnArtikelSave_Click(object sender, EventArgs e)
    {
        //Userberechigung prüfen
        if (GL_User.Lager_BestandAnlegen)
        {
            TrimmEingabe();
            if (CheckArtikelEingabe())
            {
                //Check ob ArtIDRef erzeugt ist
                if (tbArtIDRef.Text == string.Empty)
                {
                    CreateArtIDRef();
                }                
                Lager.Artikel = new clsArtikel();
                Lager.Artikel._GL_User = GL_User;
                Lager.Artikel.MandantenID = _MandantenID;
                Lager.Artikel.AbBereichID = GL_User.sys_ArbeitsbereichID;

                Lager.Artikel.LEingangTableID = Lager.LEingangTableID;
                Lager.Artikel.LVS_ID = Convert.ToDecimal(tbLVSNr.Text);
                Lager.Artikel.Gut = tbGArt.Text;
                Lager.Artikel.GutZusatz = tbGArtZusatz.Text;
                Lager.Artikel.Werksnummer = tbWerksnummer.Text;
                Lager.Artikel.Produktionsnummer = tbProduktionsNr.Text;
                Lager.Artikel.Charge = tbCharge.Text;
                Lager.Artikel.Bestellnummer = tbBestellnummer.Text;
                Lager.Artikel.exMaterialnummer = tbexMaterialnummer.Text;
                Lager.Artikel.exBezeichnung = tbexBezeichnung.Text;
                Lager.Artikel.Position = tbPos.Text;
                Lager.Artikel.ArtIDRef = tbArtIDRef.Text;
                Lager.Artikel.ME = Convert.ToInt32(tbAnzahl.Text.ToString().Trim());

                Lager.Artikel.Dicke = Convert.ToDecimal(tbDicke.Text);
                Lager.Artikel.Breite = Convert.ToDecimal(tbBreite.Text);
                Lager.Artikel.Laenge = Convert.ToDecimal(tbLaenge.Text);
                Lager.Artikel.Hoehe = Convert.ToDecimal(tbHoehe.Text);
                Lager.Artikel.Netto = Convert.ToDecimal(tbNetto.Text);
                Lager.Artikel.Brutto = Convert.ToDecimal(tbBrutto.Text);

                //Lagerort
                Lager.Artikel.LagerOrt = Lager.LagerOrt.LagerPlatzID;

                //Einheiten
                if (cbEinheit.SelectedValue != null)
                {
                    Lager.Artikel.Einheit = cbEinheit.SelectedValue.ToString();
                }

                //CHeck, ob die LVS schon vergeben und UpdateArtikel = false
                //dann muss UpdateArtikel = true, damit sichergestellt ist, dass 
                //die LVSNR nicht zweimal existiert ==> da kann man hier auch über
                //die ArtikelTableID gehen 
                if ((_ArtikelTableID == 0) && (bUpdateArtikel))
                { 
                    bUpdateArtikel=false;
                }

                if (bUpdateArtikel)
                {
                    if (_ArtikelTableID > 0)
                    {
                        //Update Daten
                        Lager.Artikel.ID = _ArtikelTableID;
                        Lager.Artikel.UpdateArtikelLager();
                    }
                }
                else
                { 
                    //Insert Daten
                    Lager.Artikel.AddArtikelLager();
                }
                //this.LEingangTableID = LEingang.LEingangTableID;
                //Da die Daten jetzt sichtbar sind und geblättert werden kann muss nun Update = true gesetzt werden
                bUpdate = true;
                InitDGV();
            }
        }
        
    }
    ///<summary>frmEinlagerung / SetArtikelMenuBtnEnabled</summary>
    ///<remarks>Das Artikelmenü wird aktiviert / deaktiviert.</remarks>   
    private void SetArtikelMenuBtnEnabled(bool Enable)
    {
        if (_bArtikelIsChecked)
        {
            tsbtnArtikelNeu.Enabled = _bArtikelIsChecked;
            tsbtnArtikelSave.Enabled = false;
        }
        else
        {
            tsbtnArtikelNeu.Enabled = Enable;
            tsbtnArtikelSave.Enabled = Enable;
        }
        tsbtnArtikelCopy.Enabled = Enable;
        tsbtnArtikelDelete.Enabled = Enable;
        //tsbtnArtikelVita.Enabled = Enable;
        if (Lager.Eingang.Checked)
        {
            tsbtnArtikelNeu.Enabled = false;
        }
    }
    ///<summary>frmEinlagerung / pbCheckArtikel_Click</summary>
    ///<remarks>Artikel prüfen.</remarks>   
    private void pbCheckArtikel_Click(object sender, EventArgs e)
    {
        if (_ArtikelTableID > 0)
        {
            //if (!_bLEingangIsChecked)
            if (!Lager.Eingang.Checked)
            {                
                if (!_bArtikelIsChecked)
                {
                    //Artikel checked
                    pbCheckArtikel.Image = (Image)Sped4.Properties.Resources.check;
                    _bArtikelIsChecked = true;
                    SetArtikelMenuBtnEnabled(!Lager.Eingang.Checked);
                    tsbtnArtikelNeu.Enabled = true;
                }
                else
                {
                    pbCheckArtikel.Image = (Image)Sped4.Properties.Resources.warning.ToBitmap();
                    _bArtikelIsChecked = false;
                    SetArtikelMenuBtnEnabled(!_bArtikelIsChecked);                    
                }                
                SetArtikelEingabefelderDatenEnable(!_bArtikelIsChecked);
                this.pbCheckArtikel.Refresh();
                clsArtikel.UpdateArtikelCheck(this.GL_User, _bArtikelIsChecked, _ArtikelTableID);
            }

        }
        InitGrdArtVita();
    }
    ///<summary>frmEinlagerung / SetLabelGArdIDInfo</summary>
    ///<remarks>Set den GüterartID Info.</remarks>  
    private void SetLabelGArdIDInfo()
    {
        lGArtID.Text = _lTextGArtID + _decGArtID.ToString();
    }


    /*************************************************************************************************
     * 
     *                                      D G V
     *
     * ***********************************************************************************************/
 
    ///<summary>frmEinlagerung / InitDGV</summary>
    ///<remarks>Init - füllt das Datagrid mit den Artikel des Eingangs.</remarks>
    private void InitDGV()
    {
        dtArtikel.Rows.Clear();
        clsArtikel art = new clsArtikel();
        art._GL_User = GL_User;
        dtArtikel = art.GetArtikelForLEingangGrd(Lager.LEingangTableID);

        if (Lager.LEingangTableID == 0)
        {
            dtArtikel.Rows.Clear();
            _ArtikelTableID = 0;
        }
        
        dgv.DataSource = null;
        dgv.DataSource = dtArtikel;

        dgv.Columns["ArtikelTableID"].Visible = false;
        //Ermittlung und Anzeige im Eingangskopf von Anzahl und Gewicht
        tbEArtAnzahl.Text = dtArtikel.Rows.Count.ToString();
        object objNetto =0;
        object objBrutto =0;
        if (dtArtikel.Rows.Count > 0)
        {
            objNetto = dtArtikel.Compute("SUM(Netto)", "LVSNr>0");
            objBrutto = dtArtikel.Compute("SUM(Brutto)", "LVSNr>0");
            _ArtikelTableID = (decimal)dtArtikel.Rows[0]["ArtikelTableID"];
        }
        tbNettoGesamt.Text = Functions.FormatDecimal(Convert.ToDecimal(objNetto.ToString()));
        tbBruttoGesamt.Text = Functions.FormatDecimal(Convert.ToDecimal(objBrutto.ToString()));
    
        SetArtikelToForm(_ArtikelTableID, false);

        //Init Artikel Vita
        InitGrdArtVita();
    }
    ///<summary>frmEinlagerung / SetArtikelToForm</summary>
    ///<remarks>Init - füllt das Datagrid mit den Artikel des Eingangs.</remarks>
    private void SetArtikelToForm(decimal myDecArtikelTableID, bool IsArtCopy)
    {
        this.Lager.Artikel = new clsArtikel();
        this.Lager.Artikel._GL_User = GL_User;
        this.Lager.Artikel.ID = myDecArtikelTableID;
        _ArtikelTableID = this.Lager.Artikel.ID;

        if (this.Lager.Artikel.GetArtikeldatenByTableID())
        {
            if (IsArtCopy)
            {
                //Bei einer Artikelkopie muss eine neue LVSNr ermittelt werden und die wichtigsten 
                //Refdaten bleiben leer
                GetNewLVSNr();
                tbProduktionsNr.Text = string.Empty;
                tbWerksnummer.Text = string.Empty;
                tbArtIDRef.Text = string.Empty;

                //Flag für AritkelUpdate setzen
                bUpdateArtikel = false;

                //Lagerort
                tbLagerort.Text = string.Empty;
            }
            else
            {
                _LVSNr = this.Lager.Artikel.LVS_ID;
                tbArtIDRef.Text = this.Lager.Artikel.ArtIDRef;
                tbProduktionsNr.Text = this.Lager.Artikel.Produktionsnummer;

                //Flag für AritkelUpdate setzen
                bUpdateArtikel = true;

                //Lagerort
                Lager.LagerOrt.LagerPlatzID=Lager.Artikel.LagerOrt;
                Lager.LagerOrt.InitLagerPlatz();
                tbLagerort.Text = Lager.LagerOrt.LagerPlatzBezeichungListe;
            }
            tbGArt.Text = this.Lager.Artikel.Gut;
            tbGArtZusatz.Text = this.Lager.Artikel.GutZusatz;             
            tbLVSNr.Text = _LVSNr.ToString();
            tbWerksnummer.Text = this.Lager.Artikel.Werksnummer;


            tbCharge.Text = this.Lager.Artikel.Charge;
            tbBestellnummer.Text = this.Lager.Artikel.Bestellnummer;
            tbexMaterialnummer.Text = this.Lager.Artikel.exMaterialnummer;
            tbexBezeichnung.Text = this.Lager.Artikel.exBezeichnung;
            tbPos.Text = this.Lager.Artikel.Position;

            //Einheit
            Functions.SetComboToSelecetedItem(ref cbEinheit, this.Lager.Artikel.Einheit);
            //ArtikelCheck
            _bArtikelIsChecked = this.Lager.Artikel.CheckArt;
            SetLEingangArtikelCheck();
            //Maße und Gewicht
            tbAnzahl.Text = this.Lager.Artikel.ME.ToString();
            cbEinheit.SelectedText = this.Lager.Artikel.Einheit;

            tbDicke.Text = Functions.FormatDecimal(this.Lager.Artikel.Dicke);
            tbBreite.Text = Functions.FormatDecimal(this.Lager.Artikel.Breite);
            tbLaenge.Text = Functions.FormatDecimal(this.Lager.Artikel.Laenge);
            tbHoehe.Text = Functions.FormatDecimal(this.Lager.Artikel.Hoehe);
            tbNetto.Text = Functions.FormatDecimal(this.Lager.Artikel.Netto);
            tbBrutto.Text = Functions.FormatDecimal(this.Lager.Artikel.Brutto);
            tbPackmittelGewicht.Text = Functions.FormatDecimal((this.Lager.Artikel.Brutto - this.Lager.Artikel.Netto));
            //LagerOrt
            Lager.LagerOrt.LagerPlatzID = this.Lager.Artikel.LagerOrt;
            Lager.LagerOrt.InitLagerPlatz();
            tbLagerort.Text = Lager.LagerOrt.LagerPlatzBezeichungListe;
        }
        else
        {
            _bArtikelIsChecked = false;
            SetLEingangArtikelCheck();
        }
    }
    ///<summary>frmEinlagerung / dgv_SelectionChanged</summary>
    ///<remarks>Ermittel die Artikeldaten des gewählten Artikels.</remarks>
    private void dgv_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv.Rows.Count > 0)
        {
            decimal decTmp = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ArtikelTableID"].Value;
            if (decTmp > 0)
            {
                //alle Eingabefelder leeren
                ClearArtikelEingabeFelder();
                _ArtikelTableID = decTmp;
                SetArtikelToForm(_ArtikelTableID, false);
                InitGrdArtVita();
            }
        }
    }
    ///<summary>frmEinlagerung / tsbtnArtikelCopy_Click_1</summary>
    ///<remarks>Ein ausgewähter Artikle soll kopriert werden.</remarks>
    private void tsbtnArtikelCopy_Click_1(object sender, EventArgs e)
    {
        if (_ArtikelTableID > 0)
        { 
            SetArtikelToForm(_ArtikelTableID, true);
            _ArtikelTableID = 0;
        }
    }


    /*************************************************************************************
     *                                    DGV / Artikel  - Menü
     * ***********************************************************************************/
    ///<summary>frmEinlagerung / tsbtnArtikelDelete_Click</summary>
    ///<remarks>Löschen des gewählten Artikels aus dem Eingang.</remarks> 
    private void tsbtnArtikelDelete_Click(object sender, EventArgs e)
    {
        if (!_bLEingangIsChecked)
        {
            if (GL_User.Lager_BestandLoeschen)
            {
                if (_ArtikelTableID > 0)
                {
                    if (clsMessages.Artikel_DeleteDatenSatz())
                    {
                        clsArtikel art = new clsArtikel();
                        art._GL_User = this.GL_User;
                        art.DeleteArtikelByID(_ArtikelTableID);
                        _ArtikelTableID = 0;
                    }
                }
                InitArtikelLoad();
            }
        }
    }
    ///<summary>frmEinlagerung / tsbtnArtGrid_Click</summary>
    ///<remarks>Öffnet / Schließt das Panel zur Ansicht des Artikel-Grids.</remarks>       
    private void tsbtnArtGrid_Click(object sender, EventArgs e)
    {
        if (splittConLager.Panel1Collapsed)
        {
            splittConLager.Panel1Collapsed = false;
            tsbtnArtGrid.Image = Sped4.Properties.Resources.layout_left;
            tsbtnArtGrid.Text = "Artikelliste ausblenden!";
        }
        else
        {
            splittConLager.Panel1Collapsed = true;
            tsbtnArtGrid.Image = Sped4.Properties.Resources.layout;
            tsbtnArtGrid.Text = "Artikelliste einblenden!";
        }
    }
    ///<summary>frmEinlagerung / tsbtnArtVita_Click</summary>
    ///<remarks>Öffnet / Schließt das Panel zur Ansicht der Artikel-Vita.</remarks>       
    private void tsbtnArtVita_Click(object sender, EventArgs e)
    {
        if (splitConArtikelDaten.Panel2Collapsed)
        {
            splitConArtikelDaten.Panel2Collapsed = false;
        }
        else
        {
            splitConArtikelDaten.Panel2Collapsed = true;
        }
    }
    ///<summary>frmEinlagerung / tsbtnArtikelNeu_Click</summary>
    ///<remarks>Ein neuer Artikel soll angelegt werden. Vorgänge:
    ///         - Eingabefelder leeren
    ///         - neue LVS Nr holen</remarks>       
    private void tsbtnArtikelNeu_Click(object sender, EventArgs e)
    {
        //Flag für update setzen
        _LVSNr = 0;
        _ArtikelTableID = 0;
        bUpdateArtikel = false;
        _bArtikelIsChecked = false;
        //Eingabefelder leeren
        ClearArtikelEingabeFelder();
        //Freigeben der Eingabefelder
        SetArtikelEingabefelderDatenEnable(true);
        //neue LVS NR
        if (GetNewLVSNr())
        {
            //neue LVSNR
            tbLVSNr.Text = _LVSNr.ToString();
            //ArtikelMenübutton aktivieren
            SetArtikelMenuBtnEnabled(true);
        }
    }
    ///<summary>frmEinlagerung / GetNewLVSNr</summary>
    ///<remarks>Eine neue LVSNr wird ermittelt.</remarks>    
    private bool GetNewLVSNr()
    {
        if (_MandantenID > 0)
        {
            clsPrimeKeys clsPK = new clsPrimeKeys();
            clsPK._GL_User = GL_User;
            clsPK.Mandanten_ID = _MandantenID;
            clsPK.GetNEWLvsNr();
            _LVSNr = clsPK.LvsNr;
            return true;
        }
        else
        {
            clsMessages.Allgemein_MandantFehlt();
            return false;
        }
    }

    /****************************************************************************************
     *                  Datagridview grdVita - ArtikelVita
     * *************************************************************************************/
    ///<summary>frmEinlagerung / GetNewLVSNr</summary>
    ///<remarks>ArtikleVita wird gefüllt.</remarks>    
    private void InitGrdArtVita()
    {
        if (Lager.LEingangTableID > 0)
        {
            DataTable dt = clsArtikelVita.GetArtikelVitaByLEingangTableID(this.GL_User, Lager.LEingangTableID, _ArtikelTableID);
            this.dgvVita.DataSource = dt;
            this.dgvVita.Columns["ID"].Visible = false;
            this.dgvVita.Columns["TableID"].Visible = false;
            this.dgvVita.Columns["TableName"].Visible = false;
            this.dgvVita.Columns["Aktion"].Visible = false;
            this.dgvVita.Columns["UserID"].Visible = false;

            Int32 i = 0;
            i++;
            this.dgvVita.Columns["Datum"].DisplayIndex = i;
            i++;
            this.dgvVita.Columns["Beschreibung"].DisplayIndex = i;
            i++;
            this.dgvVita.Columns["User"].DisplayIndex = i;
        }
        else
        {
            this.dgvVita.DataSource = null;
        }
    }
    ///<summary>frmEinlagerung / dgvVita_CellFormatting</summary>
    ///<remarks>Formatierung der Griddarstellung.</remarks>  
    private void dgvVita_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        string tmpAction = string.Empty;

        if (this.dgvVita.Columns["Aktion"] != null)
        {
            if ((!object.ReferenceEquals(dgvVita.Rows[e.RowIndex].Cells["Aktion"].Value, DBNull.Value)))
            {
                if (dgvVita.Rows[e.RowIndex].Cells["Aktion"].Value != null)
                {
                    tmpAction = dgvVita.Rows[e.RowIndex].Cells["Aktion"].Value.ToString().Trim();
                }
            }
        }

        if (e.ColumnIndex == 0)
        {
            if (tmpAction != String.Empty)
            {
                Functions.InitActionImageToArtikelVitaGrid(ref e, tmpAction);
            }
        }
    }
    ///<summary>frmEinlagerung / tbLEingangID_TextChanged</summary>
    ///<remarks>Sobald ein Eingang im Kopf angezeigt wird, so müssen die beiden Button Speichern / Delete 
    ///         ein- bzw. ausgeblendet werden.</remarks> 
    private void tbLEingangID_TextChanged(object sender, EventArgs e)
    {
        decimal decTmp = -1;
        if (Decimal.TryParse(tbLEingangID.Text, out decTmp))
        {
            if (decTmp < 1)
            {
                tsbtnEinlagerungSpeichern.Enabled = false;
                tsbtnDeleteLEingang.Enabled = false;
            }
            else
            {
                tsbtnEinlagerungSpeichern.Enabled = true;
                tsbtnDeleteLEingang.Enabled = true;
            }
        }
        else
        {
            tsbtnEinlagerungSpeichern.Enabled = false;
            tsbtnDeleteLEingang.Enabled = false;
        }
    }
    ///<summary>frmEinlagerung / cbFahrzeug_SelectedIndexChanged</summary>
    ///<remarks></remarks> 
    private void cbFahrzeug_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbFahrzeug.SelectedIndex > -1)
        {
            string strTmp = cbFahrzeug.SelectedValue.ToString();
            if (cbFahrzeug.SelectedValue.ToString() == "-1")
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
    ///<summary>frmEinlagerung / SetFelderFremdfahrzeugeEnabled</summary>
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
    ///<summary>frmEinlagerung / SetFelderFremdfahrzeugeEnabled</summary>
    ///<remarks>.</remarks>
    private void tsbtnLagerort_Click(object sender, EventArgs e)
    {
        this._ctrMenu.OpenCtrLagerOrtInFrm(true, this);
    }
    ///<summary>frmEinlagerung / label14_Click</summary>
    ///<remarks>Zur Vereinfachung</remarks>
    private void label14_Click(object sender, EventArgs e)
    {
        this._ctrMenu.OpenCtrLagerOrtInFrm(true, this);
    }
    ///<summary>frmEinlagerung / TakeOverLagerOrt</summary>
    ///<remarks>Übernimmt den Lagerort, speichert den Lagerot im Artikeldatensatz und zeigt den Lagerort im 
    ///         Ctr an.</remarks>
    public void TakeOverLagerOrt(decimal myDecLagerOrtID)
    {
        //myDecLagerOrtID=19;
        Lager.LagerOrt.ArtikelID = Lager.Artikel.ID;
        Lager.LagerOrt.LagerPlatzID = myDecLagerOrtID;
        if (!Lager.LagerOrt.UpdateArtikelLagerOrt())
        {
            //Update war nicht erfolgreich, Lagerplatz besetzt(Check in Updatefunktion)
            //Der alte Lagerort muss wieder zugewiesen werden, damit die Daten in der 
            //LagerPlatzBezeichnungListe richtig angezeigt werden
            Lager.LagerOrt.LagerPlatzID = Lager.Artikel.LagerOrt;
        }
        else 
        {
            //Update erfolgreich der neue Lagerort muss nun noch dem Arikel
            //zugewiesen werden, da die Daten nicht noch einmal aus der DB geholt werden
            Lager.Artikel.LagerOrt = Lager.LagerOrt.LagerPlatzID;
        }
        //Lagerplatzbezeichnung werden ermittelt
        Lager.LagerOrt.InitLagerPlatz();
        tbLagerort.Text = Lager.LagerOrt.LagerPlatzBezeichungListe; 
    }
    ///<summary>frmEinlagerung / tsbtnChangeEingang_Click</summary>
    ///<remarks>abgeschlossener Eingang wird zurückgesetz, damit er wieder editiert werden kann</remarks>
    private void tsbtnChangeEingang_Click(object sender, EventArgs e)
    {
        //Check Userberechtigung

        //Eingang Checked zürücksetzen
        _bLEingangIsChecked = false;
        clsLager.UpdateLEingangCheck(GL_User.User_ID, _bLEingangIsChecked, Lager.LEingangTableID);
        //neuladen
        SetLEingangskopfdatenToFrm(true);
    }

  }
}
