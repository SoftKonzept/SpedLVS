using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Sped4.Classes
{
  class clsUserBerechtigungen
  {
    //************  User  ***************
    private Int32 _BenutzerID;
    public Int32 BenutzerID
    {
      get { return _BenutzerID; }
      set { _BenutzerID = value; }
    }
    //************************************

    internal Globals._GL_USER Ber_GL_User;
    /******************************************************************************
     *  Spalten in der Datenbank Userberechtigungen
     *  
     *  S_ADR_an
        S_ADR_loe
        S_ADR_aen 
        S_KD_an
        S_KD_loe
        S_KD_aen
        S_P_an
        S_P_loe
        S_P_aen
        S_FZ_an
        S_FZ_loe
        S_FZ_aen
        S_GA_an
        S_GA_loe
        S_GA_aen
        S_R_an
        S_R_loe
        S_R_aen
        S_AT_an
        S_AT_loe
        S_AT_aen
        S_AT_teil
        D_FVSU
        D_ATSUstorno
        D_dispo
        FK_Lanzeigen
        FK_StatusAen
        FK_Frachten
        FK_drucken
        FK_RGloe
        L_LB_an
        L_LB_loe
        L_LB_aen
        Sy_User_an
        Sy_User_loe
        Sy_User_aen
     **************************************************************************************/
    internal string[,] BerechArray = {
                            {"Stammdaten - Adressen anlegen", "S_ADR_an"},
                            {"Stammdaten - Adressen löschen", "S_ADR_loe"},
                            {"Stammdaten - Adressen ändern", "S_ADR_aen" },
                            {"Stammdaten - Kunden anlegen", "S_KD_an" },
                            {"Stammdaten - Kunden löschen", "S_KD_loe" },
                            {"Stammdaten - Kunden ändern", "S_KD_aen" },
                            {"Stammdaten - Personal anlegen", "S_P_an" },
                            {"Stammdaten - Personal löschen", "S_P_loe" },
                            {"Stammdaten - Personal ändern", "S_P_aen" },
                            {"Stammdaten - Fahrzeuge anlegen", "S_FZ_an" },
                            {"Stammdaten - Fahrzeuge löschen", "S_FZ_loe" },
                            {"Stammdaten - Fahrzeuge ändern", "S_FZ_aen" },
                            {"Stammdaten - Güterart anlegen", "S_GA_an" },
                            {"Stammdaten - Güterart löschen", "S_GA_loe" },
                            {"Stammdaten - Güterart ändern", "S_GA_aen" },
                            {"Stammdaten - Relationen anlegen", "S_R_an" },
                            {"Stammdaten - Relationen löschen", "S_R_loe" },
                            {"Stammdaten - Relationen ändern", "S_R_aen" },
                            {"Auftragserfassung - Aufträge anlegen", "S_AT_an" },
                            {"Auftragserfassung - Aufträge löschen", "S_AT_loe" },
                            {"Auftragserfassung - Aufträge ändern", "S_AT_aen" },
                            {"Auftragserfassung - Aufträge teilen", "S_AT_teil" },
                            {"Disposition - Frachtvergabe an SU", "D_FVSU" },
                            {"Disposition - Auftrag an SU stornieren", "D_ATSUstorno" },
                            {"Disposition - disponieren", "D_dispo" },
                            {"Fakturierung - Liste anzeigen", "FK_Lanzeigen" },
                            {"Fakturierung - Status ändern", "FK_StatusAen" },
                            {"Fakturierung - Frachten berechnen", "FK_Frachten" },
                            {"Fakturierung - RG / GS drucken", "FK_drucken" },
                            {"Fakturierung - RG / GS löschen", "FK_RGloe" },
                            {"Lager - Bestand anlegen", "L_LB_an" },
                            {"Lager - Bestand löschen", "L_LB_loe" },
                            {"Lager - Bestand ändern", "L_LB_aen" },
                            {"System - User anlegen", "Sy_User_an" },
                            {"System - User löschen", "Sy_User_loe" },
                            {"System - User ändern", "Sy_User_aen" }
                         };



    internal DataSet ds = new DataSet();
    internal DataTable dtBerechtigungen = new DataTable("Berechtigungslist");

    private Int32 _ID;

    private bool _S_ADR_an;
    private bool _S_ADR_loe;
    private bool _S_ADR_aen;
    private bool _S_KD_an;
    private bool _S_KD_loe;
    private bool _S_KD_aen;
    private bool _S_P_an;
    private bool _S_P_loe;
    private bool _S_P_aen;
    private bool _S_FZ_an;
    private bool _S_FZ_loe;
    private bool _S_FZ_aen;
    private bool _S_GA_an;
    private bool _S_GA_loe;
    private bool _S_GA_aen;
    private bool _S_R_an;
    private bool _S_R_loe;
    private bool _S_R_aen;
    private bool _S_AT_an;
    private bool _S_AT_loe;
    private bool _S_AT_aen;
    private bool _S_AT_teil;
    private bool _D_FVSU;
    private bool _D_ATSUstorno;
    private bool _D_dispo;
    private bool _FK_Lanzeigen;
    private bool _FK_StatusAen;
    private bool _FK_Frachten;
    private bool _FK_drucken;
    private bool _FK_RGloe;
    private bool _L_LB_an;
    private bool _L_LB_loe;
    private bool _L_LB_aen;
    private bool _Sy_User_an;
    private bool _Sy_User_loe;
    private bool _Sy_User_aen;

    internal Int32 iS_ADR_an=0;
    internal Int32 iS_ADR_loe=0;
    internal Int32 iS_ADR_aen=0;
    internal Int32 iS_KD_an=0;
    internal Int32 iS_KD_loe=0;
    internal Int32 iS_KD_aen=0;
    internal Int32 iS_P_an=0;
    internal Int32 iS_P_loe=0;
    internal Int32 iS_P_aen=0;
    internal Int32 iS_FZ_an=0;
    internal Int32 iS_FZ_loe=0;
    internal Int32 iS_FZ_aen=0;
    internal Int32 iS_GA_an=0;
    internal Int32 iS_GA_loe=0;
    internal Int32 iS_GA_aen=0;
    internal Int32 iS_R_an=0;
    internal Int32 iS_R_loe=0;
    internal Int32 iS_R_aen=0;
    internal Int32 iS_AT_an=0;
    internal Int32 iS_AT_loe=0;
    internal Int32 iS_AT_aen=0;
    internal Int32 iS_AT_teil=0;
    internal Int32 iD_FVSU=0;
    internal Int32 iD_ATSUstorno=0;
    internal Int32 iD_dispo=0;
    internal Int32 iFK_Lanzeigen=0;
    internal Int32 iFK_StatusAen=0;
    internal Int32 iFK_Frachten=0;
    internal Int32 iFK_drucken=0;
    internal Int32 iFK_RGloe = 0;
    internal Int32 iL_LB_an=0;
    internal Int32 iL_LB_loe=0;
    internal Int32 iL_LB_aen=0;
    internal Int32 iSy_User_an=0;
    internal Int32 iSy_User_loe=0;
    internal Int32 iSy_User_aen=0;
    
    
    public Int32 ID
    {
      get { return _ID; }
      set { _ID = value; }
    }
    public bool S_ADR_an
    {
      get { return _S_ADR_an; }
      set { _S_ADR_an = value; }
    }
    public bool S_ADR_loe
    {
      get { return _S_ADR_loe; }
      set { _S_ADR_loe = value; }
    }
    public bool S_ADR_aen
    {
      get { return _S_ADR_aen; }
      set { _S_ADR_aen = value; }
    }
    public bool S_KD_an
    {
      get { return _S_KD_an; }
      set { _S_KD_an = value; }
    }
    public bool S_KD_loe
    {
      get { return _S_KD_loe; }
      set { _S_KD_loe = value; }
    }
    public bool S_KD_aen
    {
      get { return _S_KD_aen; }
      set { _S_KD_aen = value; }
    }
    public bool S_P_an
    {
      get { return _S_P_an; }
      set { _S_P_an = value; }
    }
    public bool S_P_loe
    {
      get { return _S_P_loe; }
      set { _S_P_loe = value; }
    }
    public bool S_P_aen
    {
      get { return _S_P_aen; }
      set { _S_P_aen = value; }
    }
    public bool S_FZ_an
    {
      get { return _S_FZ_an; }
      set { _S_FZ_an = value; }
    }
    public bool S_FZ_loe
    {
      get { return _S_FZ_loe; }
      set { _S_FZ_loe = value; }
    }
    public bool S_FZ_aen
    {
      get { return _S_FZ_aen; }
      set { _S_FZ_aen = value; }
    }
    public bool S_GA_an
    {
      get { return _S_GA_an; }
      set { _S_GA_an = value; }
    }
    public bool S_GA_loe
    {
      get { return _S_GA_loe; }
      set { _S_GA_loe = value; }
    }
    public bool S_GA_aen
    {
      get { return _S_GA_aen; }
      set { _S_GA_aen = value; }
    }
    public bool S_R_an
    {
      get { return _S_R_an; }
      set { _S_R_an = value; }
    }
    public bool S_R_loe
    {
      get { return _S_R_loe; }
      set { _S_R_loe = value; }
    }
    public bool S_R_aen
    {
      get { return _S_R_aen; }
      set { _S_R_aen = value; }
    }
    public bool S_AT_an
    {
      get { return _S_AT_an; }
      set { _S_AT_an = value; }
    }
    public bool S_AT_loe
    {
      get { return _S_AT_loe; }
      set { _S_AT_loe = value; }
    }
    public bool S_AT_aen
    {
      get { return _S_AT_aen; }
      set { _S_AT_aen = value; }
    }
    public bool S_AT_teil
    {
      get { return _S_AT_teil; }
      set { _S_AT_teil = value; }
    }
    public bool D_FVSU
    {
      get { return _D_FVSU; }
      set { _D_FVSU = value; }
    }
    public bool D_ATSUstorno
    {
      get { return _D_ATSUstorno; }
      set { _D_ATSUstorno = value; }
    }
    public bool D_dispo
    {
      get { return _D_dispo; }
      set { _D_dispo = value; }
    }
    public bool FK_Lanzeigen
    {
      get { return _FK_Lanzeigen; }
      set { _FK_Lanzeigen = value; }
    }
    public bool FK_StatusAen
    {
      get { return _FK_StatusAen; }
      set { _FK_StatusAen = value; }
    }
    public bool FK_Frachten
    {
      get { return _FK_Frachten; }
      set { _FK_Frachten = value; }
    }
    public bool FK_drucken
    {
      get { return _FK_drucken; }
      set { _FK_drucken = value; }
    }
    public bool FK_RGloe
    {
      get { return _FK_RGloe; }
      set { _FK_RGloe = value; }
    }
    public bool L_LB_an
    {
      get { return _L_LB_an; }
      set { _L_LB_an = value; }
    }
    public bool L_LB_loe
    {
      get { return _L_LB_loe; }
      set { _L_LB_loe = value; }
    }
    public bool L_LB_aen
    {
      get { return _L_LB_aen; }
      set { _L_LB_aen = value; }
    }
    public bool Sy_User_an
    {
      get { return _Sy_User_an; }
      set { _Sy_User_an = value; }
    }
    public bool Sy_User_loe
    {
      get { return _Sy_User_loe; }
      set { _Sy_User_loe = value; }
    }
    public bool Sy_User_aen
    {
      get { return _Sy_User_aen; }
      set { _Sy_User_aen = value; }
    }



    /************************************************************************************************+
     * 
     * 
     * ***********************************************************************************************/
    //
    public void NewAprovalTable()
    {
      if (ds.Tables["Berechtigungslist"] == null)
      {
        InitColumns();
      }
      InitRows();
      if (ds.Tables["Berechtigungslist"] != null)
      {
        ds.Tables.Remove("Berechtigungslist");
      }
      //ds.Tables.Add(dtBerechtigungen);
    }
    //
    private void InitColumns()
    {
      dtBerechtigungen.Clear();

      if (dtBerechtigungen.Columns["Berechtigung"] == null)
      {
        DataColumn column1 = new DataColumn();
        column1.DataType = System.Type.GetType("System.String");
        column1.AllowDBNull = false;
        column1.Caption = "Berechtigung";
        column1.ColumnName = "Berechtigung";
        column1.DefaultValue = string.Empty;

        dtBerechtigungen.Columns.Add(column1);
      }

      //DataColumn column2 = new DataColumn();
      if (dtBerechtigungen.Columns["Freigabe"] == null)
      {
        DataColumn column2 = new DataColumn();
        column2.DataType = System.Type.GetType("System.Boolean");
        column2.AllowDBNull = false;
        column2.Caption = "Freigabe";
        column2.ColumnName = "Freigabe";
        column2.DefaultValue = false;

        dtBerechtigungen.Columns.Add(column2);
      }
      if (dtBerechtigungen.Columns["dbCol"] == null)
      {
        DataColumn column3 = new DataColumn();
        column3.DataType = System.Type.GetType("System.String");
        column3.Caption = "dbCol";
        column3.ColumnName = "dbCol";

        dtBerechtigungen.Columns.Add(column3);
      }
    }
    //
    private void InitRows()
    {
      Int32 Count = BerechArray.Length / 2;
      for (Int32 i = 0; i <= Count - 1; i++)
      {
        DataRow row = dtBerechtigungen.NewRow();
        row["Berechtigung"] = BerechArray[i, 0].ToString();
        row["Freigabe"] = false;
        row["dbCol"] = BerechArray[i, 1].ToString();
        dtBerechtigungen.Rows.Add(row);
      }
    }
    //
    //----------- Berechtigung wird gesetzt und kann dann in DB gespeichert werden -
    //
    public void GetBerechtigungen(DataSet _ds, bool Insert)
    {
      ds = _ds;
      BenutzerID =(Int32) ds.Tables["Userdaten"].Rows[0]["ID"];
      if (BenutzerID > 0)
      { 
        //neuer Datensatz
        dtBerechtigungen = ds.Tables["Berechtigungslist"];

        SetBerechtigungForUpdate();
        
        if (Insert)
        {
          AddNewBerechtigungenToDB();
        }
        else
        {
          UpdateBerechtigungenInDB();
        }
      }
      //Functions.CheckAndRemoveTableFromDataSet(ref ds, "Berechtigungslist");
      //Functions.CheckAndRemoveTableFromDataSet(ref ds, "Userdaten");
    }
    //
    //------ Load Berechtigungen für eine Benutzer ID -------------------
    //
    private DataTable LoadBerechtigungen()
    {
      DataTable dataTable = new DataTable();
      dataTable.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT * FROM Userberechtigungen WHERE BenutzerID='" + BenutzerID + "'";

      ada.Fill(dataTable);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();
      return dataTable;
    }
    //
    //----- Falls Berechtigungen gelöscht bzw. hinzugefügt werden, dann muss diese Funktion 
    //----- entsprechend angepasst werden
    //
    private void SetBerechtigungen()
    {
      for (Int32 i = 0; i <= ds.Tables["Userberechtigungen"].Rows.Count - 1; i++)
      {
        for (Int32 j = 0; j <= ds.Tables["Userberechtigungen"].Columns.Count - 1; j++)
        {
          if (j > 1)
          {
            string ColName = string.Empty;
            bool Freigabe = false;
            ColName = ds.Tables["Userberechtigungen"].Columns[j].ColumnName.ToString();

            Freigabe = (bool)ds.Tables["Userberechtigungen"].Rows[i][j];

            Int32 iBit = 0;
            if (Freigabe)
            {
              iBit = 1;
            }
            else
            {
              iBit = 0;
            }

            SetDaten(ColName, Freigabe, iBit);

            for (Int32 l = 0; l <= dtBerechtigungen.Rows.Count - 1; l++)
            {
              string Search = dtBerechtigungen.Rows[l]["dbCol"].ToString();

              if (Search == ColName)
              {
                dtBerechtigungen.Rows[l]["Freigabe"] = Freigabe;
                l = dtBerechtigungen.Rows.Count;
              }
            }
          }
        }
      }
    }
    //
    //
    //
    private void SetBerechtigungForUpdate()
    {
      for (Int32 i = 0; i <= ds.Tables["Berechtigungslist"].Rows.Count - 1; i++)
      {
        string ColName = string.Empty;
        bool Freigabe = false;
        ColName = (string)ds.Tables["Berechtigungslist"].Rows[i]["dbCol"];
        Freigabe = (bool)ds.Tables["Berechtigungslist"].Rows[i]["Freigabe"];

        Int32 iBit = 0;
        if (Freigabe)
        {
          iBit = 1;
        }
        else
        {
          iBit = 0;
        }

        SetDaten(ColName, Freigabe, iBit);
      }
    }
    //
    //----------- LAden der Berechtigungen beim Login ---------------
    //
    public void GetUserBerechtigungLogin(ref Globals._GL_USER GL_User)
    {
      BenutzerID = GL_User.User_ID;
      Ber_GL_User = GL_User;
      dtBerechtigungen = ReadUserberechtigungForDGV();
      GL_User = Ber_GL_User;
    }
    //
    //----------------- Zuweisung Werte ------------------
    //
    private void SetDaten(string ColName, bool Freigabe, Int32 iBit)
    { 
      switch (ColName)
      {
        case "S_ADR_aen":
          S_ADR_aen = Freigabe;
          Ber_GL_User.ADR_aendern = Freigabe;
          iS_ADR_aen = iBit;
          break;
        case "S_ADR_loe":
          S_ADR_loe = Freigabe;
          Ber_GL_User.ADR_loeschen = Freigabe;
          iS_ADR_loe = iBit;
          break;
        case "S_ADR_an":
          S_ADR_an = Freigabe;
          Ber_GL_User.ADR_anlegen = Freigabe;
          iS_ADR_an = iBit;
          break;
        case "S_KD_an":
          S_KD_an = Freigabe;
          Ber_GL_User.KD_anlegen = Freigabe;
          iS_KD_an = iBit;
          break;
        case "S_KD_loe":
          S_KD_loe = Freigabe;
          Ber_GL_User.KD_loeschen = Freigabe;
          iS_KD_loe = iBit;
          break;
        case "S_KD_aen":
          S_KD_aen = Freigabe;
          Ber_GL_User.KD_aendern = Freigabe;
          iS_KD_aen = iBit;
          break;
        case "S_P_an":
          S_P_an = Freigabe;
          Ber_GL_User.Personal_anlegen = Freigabe;
          iS_P_an = iBit;
          break;
        case "S_P_loe":
          S_P_loe = Freigabe;
          Ber_GL_User.Personal_loeschen = Freigabe;
          iS_P_loe = iBit;
          break;
        case "S_P_aen":
          S_P_aen = Freigabe;
          Ber_GL_User.Personal_aendern = Freigabe;
          iS_P_aen = iBit;
          break;
        case "S_FZ_an":
          S_FZ_an = Freigabe;
          Ber_GL_User.FZ_anlegen = Freigabe;
          iS_FZ_an = iBit;
          break;
        case "S_FZ_loe":
          S_FZ_loe = Freigabe;
          Ber_GL_User.FZ_loeschen = Freigabe;
          iS_FZ_loe = iBit;
          break;
        case "S_FZ_aen":
          S_FZ_aen = Freigabe;
          Ber_GL_User.FZ_aendern = Freigabe;
          iS_FZ_aen = iBit;
          break;
        case "S_GA_an":
          S_GA_an = Freigabe;
          Ber_GL_User.GA_anlegen = Freigabe;
          iS_GA_an = iBit;
          break;
        case "S_GA_aen":
          S_GA_aen = Freigabe;
          Ber_GL_User.GA_aendern = Freigabe;
          iS_GA_aen = iBit;
          break;
        case "S_GA_loe":
          S_GA_loe = Freigabe;
          Ber_GL_User.GA_loeschen = Freigabe;
          iS_GA_loe = iBit;
          break;
        case "S_R_an":
          S_R_an = Freigabe;
          Ber_GL_User.Relation_anlegen = Freigabe;
          iS_R_an = iBit;
          break;
        case "S_R_loe":
          S_R_loe = Freigabe;
          Ber_GL_User.Relation_loeschen = Freigabe;
          iS_R_loe = iBit;
          break;
        case "S_R_aen":
          S_R_aen = Freigabe;
          Ber_GL_User.Relation_aendern = Freigabe;
          iS_R_aen = iBit;
          break;
        case "S_AT_an":
          S_AT_an = Freigabe;
          Ber_GL_User.Auftrag_anlegen = Freigabe;
          iS_AT_an = iBit;
          break;
        case "S_AT_loe":
          S_AT_loe = Freigabe;
          Ber_GL_User.Auftrag_loeschen = Freigabe;
          iS_AT_loe = iBit;
          break;
        case "S_AT_aen":
          S_AT_aen = Freigabe;
          Ber_GL_User.Auftrag_aendern = Freigabe;
          iS_AT_aen = iBit;
          break;
        case "S_AT_teil":
          S_AT_teil = Freigabe;
          Ber_GL_User.Auftrag_teilen = Freigabe;
          iS_AT_teil = iBit;
          break;
        case "D_FVSU":
          D_FVSU = Freigabe;
          Ber_GL_User.FrachtvergabeSU = Freigabe;
          iD_FVSU = iBit;
          break;
        case "D_ATSUstorno":
          D_ATSUstorno = Freigabe;
          Ber_GL_User.Auftrag_StornoSU = Freigabe;
          iD_ATSUstorno = iBit;
          break;
        case "D_dispo":
          D_dispo = Freigabe;
          Ber_GL_User.Disposition = Freigabe;
          iD_dispo = iBit;
          break;
        case "FK_Lanzeigen":
          FK_Lanzeigen = Freigabe;
          Ber_GL_User.Fakt_Liste = Freigabe;
          iFK_Lanzeigen = iBit;
          break;
        case "FK_StatusAen":
          FK_StatusAen = Freigabe;
          Ber_GL_User.Fakt_StatusAendern = Freigabe;
          iFK_StatusAen = iBit;
          break;
        case "FK_Frachten":
          FK_Frachten = Freigabe;
          Ber_GL_User.Fakt_Frachten = Freigabe;
          iFK_Frachten = iBit;
          break;
        case "FK_drucken":
          FK_drucken = Freigabe;
          Ber_GL_User.Fakt_drucken = Freigabe;
          iFK_drucken = iBit;
          break;
        case "FK_RGloe":
          FK_RGloe = Freigabe;
          Ber_GL_User.Fakt_RGloeschen = Freigabe;
          iFK_RGloe = iBit;
          break;
        case "L_LB_an":
          L_LB_an = Freigabe;
          Ber_GL_User.Lager_BestandAnlegen = Freigabe;
          iL_LB_an = iBit;
          break;
        case "L_LB_loe":
          L_LB_loe = Freigabe;
          Ber_GL_User.Lager_BestandLoeschen = Freigabe;
          iL_LB_loe = iBit;
          break;
        case "L_LB_aen":
          L_LB_aen = Freigabe;
          Ber_GL_User.Lager_BestandAendern = Freigabe;
          iL_LB_aen = iBit;
          break;
        case "Sy_User_an":
          Sy_User_an = Freigabe;
          Ber_GL_User.User_anlegen = Freigabe;
          iSy_User_an = iBit;
          break;
        case "Sy_User_loe":
          Sy_User_loe = Freigabe;
          Ber_GL_User.User_loeschen = Freigabe;
          iSy_User_loe = iBit;
          break;
        case "Sy_User_aen":
          Sy_User_aen = Freigabe;
          Ber_GL_User.User_aendern = Freigabe;
          iSy_User_aen = iBit;
          break;
      }
    }
    //
    //-------------- Berechtigungen zum neuen User anlegen ----------------
    //
    private void AddNewBerechtigungenToDB()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand Command = new SqlCommand();
        Command.Connection = Globals.SQLcon.Connection;

        //----- SQL Abfrage -----------------------
        Command.CommandText = "INSERT INTO Userberechtigungen ( BenutzerID, "+
                                                               "S_ADR_an, "+
                                                               "S_ADR_loe, "+
                                                               "S_ADR_aen, "+
                                                               "S_KD_an, "+ 
                                                               "S_KD_loe, "+
                                                               "S_KD_aen, "+
                                                               "S_P_an, "+
                                                               "S_P_loe, "+
                                                               "S_P_aen, "+
                                                               "S_FZ_an, "+
                                                               "S_FZ_loe, "+
                                                               "S_FZ_aen, " +
                                                               "S_GA_an, "+
                                                               "S_GA_loe, " +
                                                               "S_GA_aen, "+
                                                               "S_R_an, "+
                                                               "S_R_loe, "+
                                                               "S_R_aen, "+
                                                               "S_AT_an, "+
                                                               "S_AT_loe, "+
                                                               "S_AT_aen, "+
                                                               "S_AT_teil, "+
                                                               "D_FVSU, "+
                                                               "D_ATSUstorno, "+
                                                               "D_dispo, "+
                                                               "FK_Lanzeigen, "+
                                                               "FK_StatusAen, "+
                                                               "FK_Frachten, "+
                                                               "FK_drucken, "+
                                                               "FK_RGloe, " +
                                                               "L_LB_an, "+
                                                               "L_LB_loe, "+
                                                               "L_LB_aen, "+
                                                               "Sy_User_an, "+
                                                               "Sy_User_loe, "+
                                                               "Sy_User_aen) " +

                                                    "VALUES ('" + BenutzerID + "','"
                                                                +iS_ADR_an+ "','"
                                                                +iS_ADR_loe+ "','"
                                                                +iS_ADR_aen+ "','"
                                                                +iS_KD_an+ "','"
                                                                +iS_KD_loe+ "','"
                                                                +iS_KD_aen+ "','"
                                                                +iS_P_an+ "','"
                                                                +iS_P_loe+ "','"
                                                                +iS_P_aen+ "','"
                                                                +iS_FZ_an+ "','"
                                                                +iS_FZ_loe+ "','"
                                                                +iS_FZ_aen+ "','"
                                                                +iS_GA_an+ "','"
                                                                +iS_GA_loe+ "','"
                                                                +iS_GA_aen+ "','"
                                                                +iS_R_an+ "','"
                                                                +iS_R_loe+ "','"
                                                                +iS_R_aen+ "','"
                                                                +iS_AT_an+ "','"
                                                                +iS_AT_loe+ "','"
                                                                +iS_AT_aen+ "','"
                                                                +iS_AT_teil+ "','"
                                                                +iD_FVSU+ "','"
                                                                +iD_ATSUstorno+ "','"
                                                                +iD_dispo+ "','"
                                                                +iFK_Lanzeigen+ "','"
                                                                +iFK_StatusAen+ "','"
                                                                +iFK_Frachten+ "','"
                                                                +iFK_drucken+ "','"
                                                                +iFK_RGloe + "','"
                                                                +iL_LB_an+ "','"
                                                                +iL_LB_loe+ "','"
                                                                +iL_LB_aen+ "','"
                                                                +iSy_User_an+ "','"
                                                                +iSy_User_loe+ "','"
                                                                +iSy_User_aen+ "')";

        Globals.SQLcon.Open();
        Command.ExecuteNonQuery();
        Command.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());

      }
    }
    //
    //-------------- update die Berechtigungen eines bestehenden Users -------------
    //
    private void UpdateBerechtigungenInDB()
    {
      try
      {
        //--- initialisierung des sqlcommand---
        SqlCommand UpCommand = new SqlCommand();
        UpCommand.Connection = Globals.SQLcon.Connection;

        UpCommand.CommandText = "Update Userberechtigungen SET S_ADR_an='"+iS_ADR_an+ "', "+
                                                 "S_ADR_loe='"+iS_ADR_loe+ "', "+
                                                 "S_ADR_aen='"+iS_ADR_aen+ "', "+
                                                 "S_KD_an='"+iS_KD_an+ "', "+
                                                 "S_KD_loe='"+iS_KD_loe+ "', "+
                                                 "S_KD_aen='"+iS_KD_aen+ "', "+
                                                 "S_P_an='"+iS_P_an+ "', "+
                                                 "S_P_loe='"+iS_P_loe+ "', "+
                                                 "S_P_aen='"+iS_P_aen+ "', "+
                                                 "S_FZ_an='"+iS_FZ_an+ "', "+
                                                 "S_FZ_loe='"+iS_FZ_loe+ "', "+
                                                 "S_FZ_aen='"+iS_FZ_aen+ "', "+
                                                 "S_GA_an='"+iS_GA_an+ "', "+
                                                 "S_GA_loe='"+iS_GA_loe+ "', "+
                                                 "S_GA_aen='"+iS_GA_aen+ "', "+
                                                 "S_R_an='"+iS_R_an+ "', "+
                                                 "S_R_loe='"+iS_R_loe+ "', "+
                                                 "S_R_aen='"+iS_R_aen+ "', "+
                                                 "S_AT_an='"+iS_AT_an+ "', "+
                                                 "S_AT_loe='"+iS_AT_loe+ "', "+
                                                 "S_AT_aen='"+iS_AT_aen+ "', "+
                                                 "S_AT_teil='"+iS_AT_teil+ "', "+
                                                 "D_FVSU='"+iD_FVSU+ "', "+
                                                 "D_ATSUstorno='"+iD_ATSUstorno+ "', "+
                                                 "D_dispo='"+iD_dispo+ "', "+
                                                 "FK_Lanzeigen='"+iFK_Lanzeigen+ "', "+
                                                 "FK_StatusAen='"+iFK_StatusAen+ "', "+
                                                 "FK_Frachten='"+iFK_Frachten+ "', "+
                                                 "FK_drucken='"+iFK_drucken+ "', "+
                                                 "FK_RGloe='" + iFK_RGloe + "', " +
                                                 "L_LB_an='"+iL_LB_an+ "', "+
                                                 "L_LB_loe='"+iL_LB_loe+ "', "+
                                                 "L_LB_aen='"+iL_LB_aen+ "', "+
                                                 "Sy_User_an='"+iSy_User_an+ "', "+
                                                 "Sy_User_loe='"+iSy_User_loe+ "', "+
                                                 "Sy_User_aen='"+iSy_User_aen+ "' "+
                                                 
                                                 "WHERE BenutzerID='"+BenutzerID+"'";
       
        Globals.SQLcon.Open();
        UpCommand.ExecuteNonQuery();
        UpCommand.Dispose();
        Globals.SQLcon.Close();
      }
      catch (Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());

      }
      finally
      {
        // MessageBox.Show("Update OK!");
      } 
    }
    //
    //--------------- Read Userberechtigung for DGV -------------------
    //
    public DataTable ReadUserberechtigungForDGV()
    {
      NewAprovalTable();
      ds.Tables.Add(dtBerechtigungen);
      ds.Tables.Add(ReadBerechtigungen());
      SetBerechtigungen();
      //dtBerechtigungen.Clear();
      dtBerechtigungen = ds.Tables["Berechtigungslist"];
      ds.Tables.Remove("Berechtigungslist");
      return dtBerechtigungen;
    }
    //
    //
    private DataTable ReadBerechtigungen()
    {
      DataTable dt = new DataTable("Userberechtigungen");
      dt.Clear();
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();

      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT * FROM Userberechtigungen WHERE BenutzerID='"+BenutzerID+"'";

      ada.Fill(dt);
      ada.Dispose();
      Command.Dispose();
      Globals.SQLcon.Close();

      if (ds.Tables["Userberechtigungen"] != null)
      {
        ds.Tables.Remove("Userberechtigungen");
      }
      return dt;
    }
    //
    //------------ Get Passwort for User -----------------------
    //
    public static string GetPassForUserByID(Int32 UserID)
    {
      string pass = string.Empty;
      SqlDataAdapter ada = new SqlDataAdapter();
      SqlCommand Command = new SqlCommand();
      Command.Connection = Globals.SQLcon.Connection;
      ada.SelectCommand = Command;
      Command.CommandText = "SELECT Pass FROM [User] WHERE ID ='" + UserID+"'";
      Globals.SQLcon.Open();

      object obj = Command.ExecuteScalar();

      if(obj != null)
      {
        pass=(string)obj;
      }

      Command.Dispose();
      Globals.SQLcon.Close();
      return pass;
    }
    //
    //
    //
    public void DeleteUserAndBerechtigung()
    {
      //Check vorhanden
      if (clsUser.CheckUserIDIsUsed(ID))
      {
        //Check ist eingeloggt
        if (!clsLogin.CheckUserIsLoggedIn(ID))
        {
          //Add Logbucheintrag Löschen
          string Name = clsUser.GetBenutzerNameByID(ID);
          string Beschreibung = "User:  ID: " + ID + " Name: " + Name + " gelöscht";
          Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

          //--- initialisierung des sqlcommand---
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          Command.CommandText = "DELETE FROM Userberechtigungen WHERE BenutzerID='" + ID + "'";
          Globals.SQLcon.Open();
          Command.ExecuteNonQuery();
          Command.Dispose();
          Globals.SQLcon.Close();
          if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
          {
            Command.Connection.Close();
          }

          //Löschen aus DB USER
          clsUser delUser = new clsUser();
          delUser.BenutzerID = BenutzerID;
          delUser.ID = ID;
          delUser.DeleteUser();

        }
        else
        {
          clsMessages.Userverwaltung_UserIsLoggedIn();
        }
      }
    }
      
      /**************************************************************************************************
       *                                Berechtigungsabfragen
       * 
       * ************************************************************************************************/
      //
      //
      private bool ResponseFromDB(string strSQL)
      {
        bool allowed = false;
        if(strSQL!=string.Empty)
        {
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = strSQL;
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if(obj != null)
            {
              allowed=(bool)obj;
              //pass=(string)obj;
            }

            Command.Dispose();
            Globals.SQLcon.Close();
            return allowed;
        }
        return allowed;
      }
      
      //
      //----- ADR anlegen ---------------------
      //

      public bool Allow_ADRanlegen(Int32 UserID)
      {
        string sql = string.Empty;
        sql = "SELECT S_ADR_an FROM Userberechtigung WHERE BenutzerID ='" + UserID+"'";
        bool allow=ResponseFromDB(sql);
        return allow;
      }
       
    
    }
  }

