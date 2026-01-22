using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sped4;
using Sped4.Classes;

namespace Sped4
{
  public partial class ctrLager : UserControl
  {

    public Globals._GL_USER GL_User;
    public DataTable dtABestand = new DataTable("Artikelbestand");
    public DataTable dtKDBestand = new DataTable("Kundenbestand");
    public DateTime vonZR = DateTime.Today.AddDays(-7);
    public DateTime bisZR = DateTime.Today.Date;
    public bool boAusgang = false;

    internal decimal LVS_ID = 0;
    internal decimal LEingangID = 0;
    internal decimal LAusgangID = 0;
    internal decimal KD_ADR_ID = 0;
    public Int32 SuchKriterium = -1;
    internal new bool Update = false;
    public string strSuchKriterium = string.Empty;
    /**************************************
     * Suchkriterium
     * 1: LVS
     * 2:Eingang
     * 3:Ausgang
     * 4:Produktionsnummer
     * 5:Werksnummer
     * **********************************/

    public Int32 ListeArt = -1;
    /**************************************
     * Suchkriterium
     * 0: aktuellerArtikelbestand
     * 1: LVS
     * 2:Eingang
     * 3:Ausgang
     * 4:Produktionsnummer
     * 5:Werksnummer
     * 6: Kunde-gelagerte Artikel
     * 7: Dicke
     * 8. Breite
     * 9. Laenge
     * 10. Hoehe
     * 11. Nettogewicht
     * 12. Bruttogewicht
     * 13. Gueterart
     * 14. Charge
     * 15. exBezeichnung
     * 16. exMaterialnummer   
     * 20. Artikelsuche
     * 
     * 30. Ausgang nach Kunden 
     * 31. Ausgang nach Artikel
     * **********************************/


    public ctrLager()
    {
      InitializeComponent();
    }
    //
    //--------------------------- ON LOAD -----------------------
    //
    private void ctrLager_Load(object sender, EventArgs e)
    {
      InitComboSuchfilter();
      InitCtr();
      gbArtikel.Enabled = false;
      gbAuftraggeber.Enabled = false;

      tbMCSearch.Text = string.Empty;
      tbNameSearch.Text = string.Empty;
    }
    //
    //
    private void InitComboSuchfilter()
    {
      cbFilter.DataSource = Enum.GetNames(typeof(Globals.enumLagerSuchFilter));
    }
    //
    //
    private void InitCtr()
    {
        /*****
      clsLager lager = new clsLager();
      switch (ListeArt)
      {
        case 0:
          dtABestand.Clear();
          InitColToTableBestandArtikel();
          dtABestand = lager.GetLagerDaten(ref dtABestand, "aktuellerArtikelbestand", "");
          InitDGV();
          break;

        case 1:
          dtKDBestand.Clear();
          InitColToTableBestandKunde();
          dtKDBestand = lager.GetBestandKundenGesamt();
          gbAuftraggeber.Enabled = true;
          InitDGV();
          break;
           
        case 6:
          OpenAuslagerungsForm();            
          break;

        case 20:
          dtABestand.Clear();
          InitColToTableBestandArtikel();
          GetDaten();
          break;

        case 30:
          dtKDBestand.Clear();
          InitColToTableBestandKunde();
          dtKDBestand = lager.GetAusgangBestandKundenGesamt();
          gbAuftraggeber.Enabled = true;
          InitDGV();
          break;

        case 31:
          dtABestand.Clear();
          InitColToTableBestandArtikel();
          lager.bisZeitraum = bisZR;
          lager.vonZeitraum = vonZR;
          dtABestand = lager.GetLagerDaten(ref dtABestand, "Ausgang_nach_Artikel", "");
          InitDGV();
          break;
      }
         * ***/
    }

    //
    //------------- Initialisierung Liste - DGV ---------------
    //
    private void InitDGV()
    {
      switch (ListeArt)
      { 
        //alle aktuellen Artikel
        case 0:
        case 31:
          if (dtABestand.Rows.Count > 0)
          {
            this.dgv.DataSource = dtABestand;
            this.dgv.Columns["ArtikelID"].Visible = false;
            this.dgv.Columns["EAID"].Visible = false;
            this.dgv.Columns["ADRID"].Visible = false;
            this.dgv.Columns["ME"].Visible = false;
            this.dgv.Columns["Einheit"].Visible = false;
            this.dgv.Columns["Position"].Visible = false;
            this.dgv.Columns["Schaden"].Visible = false;
            this.dgv.Columns["Schadensbeschreibung"].Visible = false;


            for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            {
              this.dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              this.dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
           // this.dgv.Columns["Datum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (this.dgv.Columns["Datum"] != null)
            {
              this.dgv.Columns["Datum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (this.dgv.Columns["Datum"] != null)
            {

            }

            this.dgv.Columns["Auftraggeber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dgv.Columns["Gueterart"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
          }
          else
          {
            this.dgv.DataSource = null;
          }
          break;



        case 1:
        case 30:
          if (dtKDBestand.Rows.Count > 0)
          {
            dgv.DataSource = dtKDBestand;
            dgv.Columns["ADR_ID"].Visible = false;
            dgv.Columns["Kundennummer"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Auftraggeber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Saldo Artikel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Saldo Netto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["Saldo Brutto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          }
          else
          {
            this.dgv.DataSource = null;
          }
          break;

        //Artikle Bestand - Suche
        case 2:
          if (dtABestand.Rows.Count > 0)
          {
            dgv.DataSource = dtABestand;
            for (Int32 i = 0; i <= this.dgv.Columns.Count - 1; i++)
            {
              this.dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              this.dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            this.dgv.Columns["ArtikelID"].Visible = false;
            this.dgv.Columns["EAID"].Visible = false;
            this.dgv.Columns["ADRID"].Visible = false;
            this.dgv.Columns["ME"].Visible = false;
            this.dgv.Columns["Einheit"].Visible = false;
            this.dgv.Columns["Position"].Visible = false;
            this.dgv.Columns["Schaden"].Visible = false;
            this.dgv.Columns["Schadensbeschreibung"].Visible = false;
            //this.dgv.Columns["EADatum"].Visible = false;
          }
          else
          {
            this.dgv.DataSource = null;
          }
          break;
      }

      
    }
    //
    //--------- Form close --------------------
    //
    private void tsbClose_Click(object sender, EventArgs e)
    {
      Int32 Count = this.ParentForm.Controls.Count;

      for (Int32 i = 0; (i <= (Count - 1)); i++)
      {
        if (this.ParentForm.Controls[i].Name == "TempSplitterLager")
        {
          this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
          //i = Count - 1;      // ist nur ein Controll vorhanden
        }
        if (this.ParentForm.Controls[i].GetType() == typeof(ctrLager))
        {
          this.ParentForm.Controls.Remove(this.ParentForm.Controls[i]);
          i = Count - 1;      // ist nur ein Controll vorhanden
        }
      }
    }
    //
    //---------------------- Einlagerung --------------------------
    //
    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      OpenFrmEinlagerung();
    }
    //
    private void OpenFrmEinlagerung()
    {
        if (Functions.frm_IsFormAlreadyOpen(typeof(frmEinlagerung)) != null)
      {
          Functions.frm_FormClose(typeof(frmEinlagerung));
      }

        /***
      frmEinlagerung lager = new frmEinlagerung();
      lager.Update = Update;
      lager.LVSNr = LVS_ID;
      lager.GL_User = GL_User;
      lager.StartPosition = FormStartPosition.CenterScreen;
      lager.Show();
      lager.BringToFront();
        ****/

    }
    //
    //
    /********************************************************************************+
     *                            DGV - DataTable 
     * *******************************************************************************/
    //
    //
    private void InitColToTableBestandArtikel()
    {
      /**************************
       * Reihenfolge Spalten
       * - LVS
       * - Eingang
       * - Datum
       * - Auftraggeber
       * - Güterart
       * - Nettogewicht
       * - Bruttogewicht
       * - Dicke
       * - Breite
       * - Hoehe 
       * - Laenge
       * - Produktionsnummer
       * - exBezeichnung
       * - exMaterialnummer
       * - Charge
       * - Werk
       * - Halle
       * - Reihe
       * - Platz 
       * **************************/

      if (dtABestand.Columns["LVS"] == null)
      {
        DataColumn LVS = new DataColumn();
        LVS.DataType = System.Type.GetType("System.Int32");
        LVS.Caption = "LVS";
        LVS.ColumnName = "LVS";
        dtABestand.Columns.Add(LVS);
      }
      if (boAusgang)
      {
        if (dtABestand.Columns["Eingang"] == null)
        {
          DataColumn Eingang = new DataColumn();
          Eingang.DataType = System.Type.GetType("System.Int32");
          Eingang.Caption = "Eingang";
          Eingang.ColumnName = "Eingang";
          dtABestand.Columns.Add(Eingang);
        }
        if (dtABestand.Columns["E_Datum"] == null)
        {
          DataColumn E_Datum = new DataColumn();
          E_Datum.DataType = System.Type.GetType("System.DateTime");
          E_Datum.Caption = "E_Datum";
          E_Datum.ColumnName = "E_Datum";
          dtABestand.Columns.Add(E_Datum);
        }
        if (dtABestand.Columns["Ausgang"] == null)
        {
          DataColumn Ausgang = new DataColumn();
          Ausgang.DataType = System.Type.GetType("System.Int32");
          Ausgang.Caption = "Ausgang";
          Ausgang.ColumnName = "Ausgang";
          dtABestand.Columns.Add(Ausgang);
        }
        if (dtABestand.Columns["A_Datum"] == null)
        {
          DataColumn A_Datum = new DataColumn();
          A_Datum.DataType = System.Type.GetType("System.DateTime");
          A_Datum.Caption = "A_Datum";
          A_Datum.ColumnName = "A_Datum";
          dtABestand.Columns.Add(A_Datum);
        }
      }
      else
      { 
        if (dtABestand.Columns["Eingang"] == null)
        {
          DataColumn Eingang = new DataColumn();
          Eingang.DataType = System.Type.GetType("System.Int32");
          Eingang.Caption = "Eingang";
          Eingang.ColumnName = "Eingang";
          dtABestand.Columns.Add(Eingang);
        }
        if (dtABestand.Columns["Datum"] == null)
        {
          DataColumn Datum = new DataColumn();
          Datum.DataType = System.Type.GetType("System.DateTime");
          Datum.Caption = "Datum";
          Datum.ColumnName = "Datum";
          dtABestand.Columns.Add(Datum);
        }      
      }


      if (dtABestand.Columns["Auftraggeber"] == null)
      {
        DataColumn Auftraggeber = new DataColumn();
        Auftraggeber.DataType = System.Type.GetType("System.String");
        Auftraggeber.Caption = "Auftraggeber";
        Auftraggeber.ColumnName = "Auftraggeber";
        dtABestand.Columns.Add(Auftraggeber);
      }
      if (dtABestand.Columns["Gueterart"] == null)
      {
        DataColumn Gueterart = new DataColumn();
        Gueterart.DataType = System.Type.GetType("System.String");
        Gueterart.Caption = "Gueterart";
        Gueterart.ColumnName = "Gueterart";
        dtABestand.Columns.Add(Gueterart);
      }
      if (dtABestand.Columns["Nettogewicht"] == null)
      {
        DataColumn Nettogewicht = new DataColumn();
        Nettogewicht.DataType = System.Type.GetType("System.Decimal");
        Nettogewicht.Caption = "Nettogewicht";
        Nettogewicht.ColumnName = "Nettogewicht";
        dtABestand.Columns.Add(Nettogewicht);
      }
      if (dtABestand.Columns["Bruttogewicht"] == null)
      {
        DataColumn Bruttogewicht = new DataColumn();
        Bruttogewicht.DataType = System.Type.GetType("System.Decimal");
        Bruttogewicht.Caption = "Bruttogewicht";
        Bruttogewicht.ColumnName = "Bruttogewicht";
        dtABestand.Columns.Add(Bruttogewicht);
      }
      if (dtABestand.Columns["Dicke"] == null)
      {
        DataColumn Dicke = new DataColumn();
        Dicke.DataType = System.Type.GetType("System.Decimal");
        Dicke.Caption = "Dicke";
        Dicke.ColumnName = "Dicke";
        dtABestand.Columns.Add(Dicke);
      }
      if (dtABestand.Columns["Breite"] == null)
      {
        DataColumn Breite = new DataColumn();
        Breite.DataType = System.Type.GetType("System.Decimal");
        Breite.Caption = "Breite";
        Breite.ColumnName = "Breite";
        dtABestand.Columns.Add(Breite);
      }
      if (dtABestand.Columns["Hoehe"] == null)
      {
        DataColumn Hoehe = new DataColumn();
        Hoehe.DataType = System.Type.GetType("System.Decimal");
        Hoehe.Caption = "Hoehe";
        Hoehe.ColumnName = "Hoehe";
        dtABestand.Columns.Add(Hoehe);
      }
      if (dtABestand.Columns["Laenge"] == null)
      {
        DataColumn Laenge = new DataColumn();
        Laenge.DataType = System.Type.GetType("System.Decimal");
        Laenge.Caption = "Laenge";
        Laenge.ColumnName = "Laenge";
        dtABestand.Columns.Add(Laenge);
      }    
      if (dtABestand.Columns["Produktionsnummer"] == null)
      {
        DataColumn Produktionsnummer = new DataColumn();
        Produktionsnummer.DataType = System.Type.GetType("System.String");
        Produktionsnummer.Caption = "Produktionsnummer";
        Produktionsnummer.ColumnName = "Produktionsnummer";
        dtABestand.Columns.Add(Produktionsnummer);
      }
      if (dtABestand.Columns["Werksnummer"] == null)
      {
        DataColumn Werksnummer = new DataColumn();
        Werksnummer.DataType = System.Type.GetType("System.String");
        Werksnummer.Caption = "Werksnummer";
        Werksnummer.ColumnName = "Werksnummer";
        dtABestand.Columns.Add(Werksnummer);
      }
      if (dtABestand.Columns["exBezeichnung"] == null)
      {
        DataColumn exBezeichnung = new DataColumn();
        exBezeichnung.DataType = System.Type.GetType("System.String");
        exBezeichnung.Caption = "exBezeichnung";
        exBezeichnung.ColumnName = "exBezeichnung";
        dtABestand.Columns.Add(exBezeichnung);
      }
      if (dtABestand.Columns["exMaterialnummer"] == null)
      {
        DataColumn exMaterialnummer = new DataColumn();
        exMaterialnummer.DataType = System.Type.GetType("System.String");
        exMaterialnummer.Caption = "exMaterialnummer";
        exMaterialnummer.ColumnName = "exMaterialnummer";
        dtABestand.Columns.Add(exMaterialnummer);
      }
      if (dtABestand.Columns["Charge"] == null)
      {
        DataColumn Charge = new DataColumn();
        Charge.DataType = System.Type.GetType("System.String");
        Charge.Caption = "Charge";
        Charge.ColumnName = "Charge";
        dtABestand.Columns.Add(Charge);
      }
      if (dtABestand.Columns["Werk"] == null)
      {
        DataColumn Werk = new DataColumn();
        Werk.DataType = System.Type.GetType("System.String");
        Werk.Caption = "Werk";
        Werk.ColumnName = "Werk";
        dtABestand.Columns.Add(Werk);
      }
      if (dtABestand.Columns["Halle"] == null)
      {
        DataColumn Halle = new DataColumn();
        Halle.DataType = System.Type.GetType("System.String");
        Halle.Caption = "Halle";
        Halle.ColumnName = "Halle";
        dtABestand.Columns.Add(Halle);
      }
      if (dtABestand.Columns["Reihe"] == null)
      {
        DataColumn Reihe = new DataColumn();
        Reihe.DataType = System.Type.GetType("System.String");
        Reihe.Caption = "Reihe";
        Reihe.ColumnName = "Reihe";
        dtABestand.Columns.Add(Reihe);
      }
      if (dtABestand.Columns["Platz"] == null)
      {
        DataColumn Platz = new DataColumn();
        Platz.DataType = System.Type.GetType("System.String");
        Platz.Caption = "Platz";
        Platz.ColumnName = "Platz";
        dtABestand.Columns.Add(Platz);
      }
    }
    //
    //------------------- 
    //
    private void InitColToTableBestandKunde()
    {
      if (dtKDBestand.Columns["Kunde ID"] == null)
      {
        DataColumn KundeID = new DataColumn();
        KundeID.DataType = System.Type.GetType("System.Int32");
        KundeID.Caption = "Kunde ID";
        KundeID.ColumnName = "Kunde ID";
        dtKDBestand.Columns.Add(KundeID);
      }
      if (dtKDBestand.Columns["Kunde"] == null)
      {
        DataColumn Kunde = new DataColumn();
        Kunde.DataType = System.Type.GetType("System.String");
        Kunde.Caption = "Kunde";
        Kunde.ColumnName = "Kunde";
        dtKDBestand.Columns.Add(Kunde);
      }
      if (dtKDBestand.Columns["Anzahl"] == null)
      {
        DataColumn Anzahl = new DataColumn();
        Anzahl.DataType = System.Type.GetType("System.Int32");
        Anzahl.Caption = "Anzahl";
        Anzahl.ColumnName = "Anzahl";
        dtKDBestand.Columns.Add(Anzahl);
      }
      if (dtKDBestand.Columns["Nettogewicht"] == null)
      {
        DataColumn Nettogewicht = new DataColumn();
        Nettogewicht.DataType = System.Type.GetType("System.Decimal");
        Nettogewicht.Caption = "Nettogewicht";
        Nettogewicht.ColumnName = "Nettogewicht";
        dtKDBestand.Columns.Add(Nettogewicht);
      }
      if (dtKDBestand.Columns["Bruttogewicht"] == null)
      {
        DataColumn Bruttogewicht = new DataColumn();
        Bruttogewicht.DataType = System.Type.GetType("System.Decimal");
        Bruttogewicht.Caption = "Bruttogewicht";
        Bruttogewicht.ColumnName = "Bruttogewicht";
        dtKDBestand.Columns.Add(Bruttogewicht);
      }
    }
    //
    //---------------- Bestandsliste des Kunden anzeigen -----------
    //
    private void bestandslisteDesKundenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (dgv.Rows.Count>0)
      {
        ListeArt = 6;
        //Int32 iKD = (Int32)dgv.CurrentRow.Cells["KD Nr,"].Value;
        //KD_ADR_ID = (Int32)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ADR_ID"].Value;
        //KD_ADR_ID = ((DataGridView)sender)
        InitCtr();
      }
    }
    //
    //
    //
    private void dgv_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgv.CurrentRow != null)
      {
        if (dgv.Rows.Count <= this.dgv.CurrentRow.Index)
        {
            KD_ADR_ID = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ADR_ID"].Value;
            LVS_ID = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["LVS_ID"].Value;
        }
      }
    }
    //
    //
    //
    private void OpenAuslagerungsForm()
    {
        if (Functions.frm_IsFormAlreadyOpen(typeof(frmAuslagerung)) != null)
      {
          Functions.frm_FormClose(typeof(frmAuslagerung));
      }

      frmAuslagerung lager = new frmAuslagerung();
      lager.GL_User = GL_User;
      lager._KD_ADR_ID = KD_ADR_ID;
      lager._LEingangID = LEingangID;
      lager._LAusgangID = LAusgangID;
      //lager._LVS_ID = _Lvs;
      lager._Update = Update;
      lager.StartPosition = FormStartPosition.CenterScreen;
      lager.Show();
      lager.BringToFront();    
    }
    //
    //
    //
    private void dgv_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      //in Mousedown musste die Verknüpfung erstellt werden,damit diese Methode aufgerufen werden kann
      if (e.Button == MouseButtons.Right)
      {
        contextMenuStrip1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
      }
    }
    //
    //-------------- Search Auftraggeber über Kundennummer -----------
    //
    private void pictureBox1_Click(object sender, EventArgs e)
    {
      tbNameSearch.Text = string.Empty;

      if(dtKDBestand.Rows.Count>0)
      {
        tbMCSearch.Text = tbMCSearch.Text.ToString().Trim();
        if (tbMCSearch.Text != string.Empty)
        {
          string SearchText = tbMCSearch.Text;
          string ColName = "Kundennummer";
          dtKDBestand = Functions.FilterDataTable(dtKDBestand, SearchText, ColName);
          dgv.DataSource = dtKDBestand;
        }
        else
        {
          InitDGV();
        }
      }
    }
    //
    //-------------- Search Auftraggeber über Kundennummer -----------
    //
    private void pbFilterArtikel_Click(object sender, EventArgs e)
    {
      tbMCSearch.Text = string.Empty;

      if (dtKDBestand.Rows.Count > 0)
      {
        tbNameSearch.Text = tbNameSearch.Text.ToString().Trim();
        if (tbNameSearch.Text != string.Empty)
        {
          string SearchText = tbNameSearch.Text;
          string ColName = "Auftraggeber";
          dtKDBestand = Functions.FilterDataTableLIKEForString(dtKDBestand, SearchText, ColName);
          dgv.DataSource = dtKDBestand;
        }
        else
        {
          InitCtr();
        }
      }
    }
    //
    //-------------- Filter löschen ---------------
    //
    private void pbSucheDelete_Click(object sender, EventArgs e)
    {
      tbNameSearch.Text = string.Empty;
      tbMCSearch.Text = string.Empty;
      InitCtr();
    }
    //
    //--------------- Formular bereinigen ------------
    //
    private void tsbtnClear_Click(object sender, EventArgs e)
    {
      CtrClear();
    }
    //
    private void CtrClear()
    {
      dtABestand.Clear();
      dtKDBestand.Clear();
      ListeArt = -1;
      SuchKriterium = -1;
      this.dgv.DataSource = null;
    }
    //
    //--------------- Artikel direkt in DB suchen -----------
    //
    private void tsbtnSearch_Click(object sender, EventArgs e)
    {
      if (gbArtikel.Enabled == true)
      {
        gbArtikel.Enabled = false;
        tsbtnSearch.Text = "Artikelsuche aktivieren";
      }
      else
      {
        gbArtikel.Enabled = true;
        tsbtnSearch.Text = "Artikelsuche deaktivieren";
      }
      tbArtikelSearch.Text = string.Empty;
    }
    //
    //---------- Artikelsuche bereinigen / löschen -----------
    //
    private void pbSucheArtikelDelete_Click(object sender, EventArgs e)
    {
      tbArtikelSearch.Text = string.Empty;
      CtrClear();
    }
    //
    //---------------- Artikel suchen -------------------
    //
    private void pbSeachArtikel_Click(object sender, EventArgs e)
    {
      //sind Artikel im DGV, so kann die Suche im DGV stattfinden, 
      //sonst neue Daten aus DB
      strSuchKriterium = cbFilter.SelectedItem.ToString();
      ListeArt = 20;
      InitCtr();

      if (dtABestand.Rows.Count > 0)
      {
        tbArtikelSearch.Text=tbArtikelSearch.Text.ToString().Trim();
        string txtSearch = tbArtikelSearch.Text;
        string colName = cbFilter.SelectedItem.ToString();

        dtABestand=Functions.FilterDataTable(dtABestand, txtSearch, colName);
        InitDGV();
      }
      else
      {
        //strSuchKriterium = cbFilter.SelectedItem.ToString();
        //ListeArt = 20;
        //InitCtr();
      
      }
    }
    //
    //
    //
    private void bestandNachKundeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      afColorLabel1.myText = "aktuelle Bestandssalden nach Kunde";
      ListeArt = 1;
      InitCtr();
    }
    //
    //
    //
    private void bestandArtikelToolStripMenuItem_Click(object sender, EventArgs e)
    {
      afColorLabel1.myText = "aktueller Artikelbestand";
      ListeArt = 0;
      InitCtr();
    }
    //
    //---------- Update Lagereingang ---------------
    //
    private void zumEingangToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (dtABestand.Rows.Count > 0)
      {
         Update = true;
        OpenFrmEinlagerung();
        Update = false;
      }
    }
    //
    //
    //
    private void dgv_MouseClick(object sender, MouseEventArgs e)
    {
      if (dgv.Rows.Count > 0)
      {
        if (this.dgv.CurrentRow != null)
        {
          //Bestand nach Auftraggeber
          if (this.dgv.Columns["ADR_ID"] != null)
          {
            Int32 i = dtABestand.Rows.Count;
            Int32 ii = dtKDBestand.Rows.Count;
            KD_ADR_ID = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ADR_ID"].Value;
          }
          //Bestand nach Artikel
          if (this.dgv.Columns["ADRID"] != null)
          {
            Int32 j = dtABestand.Rows.Count;
            Int32 jj = dtKDBestand.Rows.Count;
            KD_ADR_ID = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ADRID"].Value;
          }
          if (this.dgv.Columns["LVS_ID"] != null)
          {
              LVS_ID = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["LVS_ID"].Value;
          }
          if (this.dgv.Columns["LVS"] != null)
          {
              LVS_ID = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["LVS"].Value;
          }
          if (this.dgv.Columns["Eingang"] != null)
          {
              LEingangID = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["Eingang"].Value;
          }
          if (this.dgv.Columns["Ausgang"] != null)
          {
              LAusgangID = (decimal)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["Ausgang"].Value;
          }
        }
      }
    }
    //
    //
    //
    private void OpenFrmAuftragsListenZeitraum()
    {
      frmAuftragslisteZeitraum az = new frmAuftragslisteZeitraum();
      az.ctrLager= this;
      az.StartPosition = FormStartPosition.CenterScreen;
      az.Show();
      az.BringToFront();
    }
    //
    //------------- Übergabe Zeitraum ----------------------------
    //
    public void SetSearchTimeDistance(DateTime dtVon, DateTime dtBis)
    {
      vonZR = dtVon;
      bisZR = dtBis;

    }
    //
    //-------------- Lagerausgang nach Kunde und Zeitraum ----------------
    //
    private void bestandNachKundeAusgangToolStripMenuItem_Click(object sender, EventArgs e)
    {
      boAusgang = true;
      afColorLabel1.myText = "Ausgänge nach Kunde";
      ListeArt = 30;
      InitCtr();
      boAusgang = false;
    }
    //
    //---------------- Lagerausgang nach Artikeln -------------
    //
    private void miAusgangArtikel_Click(object sender, EventArgs e)
    {
      boAusgang = true;
      afColorLabel1.myText = "Ausgänge nach Artikel";
      ListeArt = 31;
      InitCtr();
      boAusgang = false;
    }
    //
    //------------ Ausgang bearbeiten - Menü rechte Maustaste ------------
    //
    private void ausgangBearbeitenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (dtABestand.Rows.Count > 0)
      {
        Update = true;
        OpenAuslagerungsForm();
        Update = false;
      }
    }
    //
    //---------- Auslagerung über doppelclick -----------------
    //
    private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (GL_User.Lager_BestandAendern)
      {
        if(this.dgv.Rows.Count >=1)
        {
          //--- ausgewählte Datensatz ------
          if (this.dgv.Rows[this.dgv.CurrentRow.Index].Cells[0].Value != null)
          {
            //KD_ADR_ID = (Int32)this.dgv.Rows[this.dgv.CurrentRow.Index].Cells["ADR_ID"].Value;

            //Form Auslagerung öffnen
            frmAuslagerung auslagerung = new frmAuslagerung();
            auslagerung._KD_ADR_ID = KD_ADR_ID;
            auslagerung.StartPosition = FormStartPosition.CenterScreen;
            auslagerung.Show();
            auslagerung.BringToFront();
          }
        }
      }
      else
      {
        clsMessages.User_NoAuthen();
      }
    }


   
  }
}
