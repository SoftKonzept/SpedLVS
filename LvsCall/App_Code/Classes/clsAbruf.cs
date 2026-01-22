using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using Telerik.Web.UI;


/// <summary>
/// Zusammenfassungsbeschreibung für clsAbruf
/// </summary>
public class clsAbruf
{
    public const string const_GArtArt_Bleche = "Bleche";
    public const string const_GArtArt_Coils = "Coils";
    public const string const_GArtArt_EuroPaletten = "EURO-Paletten";
    public const string const_GArtArt_Paletten = "Paletten";
    public const string const_GArtArt_Platinen = "Platinen";
    public const string const_GArtArt_Rohre = "Rohre";
    public const string const_GArtArt_Stabstahl = "Stabstahl";

    public const string const_AbrufAktion_Abruf = "Abruf";
    public const string const_AbrufAktion_UB = "UB";

    public const string const_Status_erstellt = "erstellt";
    public const string const_Status_bearbeitet = "bearbeitet";
    public const string const_Status_MAT = "MAT";
    public const string const_Status_ENTL = "ENTL";

    public Color const_StatusColor_erstellt = Color.Red;
    public Color const_StatusColor_bearbeitet = Color.Blue;
    public Color const_StatusColor_MAT = Color.YellowGreen;
    public Color const_StatusColor_ENTL = Color.Black;

    public clsSQLcon_Call SQLconCall = new clsSQLcon_Call();
    public clsSQLcon_LVS SQLconLVS = new clsSQLcon_LVS();
    public clsBestand Bestand = new clsBestand();

    public Int32 ID { get; set; }
    public bool IsRead { get; set; }
    public Int32 ArtikelID { get; set; }
    public Int32 LVSNr { get; set; }
    public string Werksnummer { get; set; }
    public string Produktionsnummer { get; set; }
    public string Charge { get; set; }
    public decimal Netto { get; set; }
    public decimal Brutto { get; set; }
    public decimal Dicke { get; set; }
    public decimal Breite { get; set; }


    public Int32 CompanyID { get; set; }
    public string CompanyName { get; set; }
    public Int32 AbBereichID { get; set; }
    public DateTime Datum { get; set; }
    public DateTime EintreffDatum { get; set; }
    public DateTime EintreffZeit { get; set; }
    public Int32 BenutzerID { get; set; }
    public string Benutzername { get; set; }
    public string Schicht { get; set; }
    public string Referenz { get; set; }
    public string Abladestelle { get; set; }
    public string Aktion { get; set; }
    public DateTime Erstellt { get; set; }
    public bool IsCreated { get; set; }
    public string Status { get; set; }
    public Int32 LiefAdrID { get; set; }
    public Int32 EmpAdrID { get; set; }

    public DataTable dtAbrufUBList = new DataTable();
    public List<Int32> ListAbrufArtikelID = new List<int>();
    public Dictionary<Int32, clsAbruf> DictAbrufeToInsert = new Dictionary<int, clsAbruf>();
    public string InfoText { get; set; }
    List<clsAbruf> ListOpenAbrufList = new List<clsAbruf>();

    //public Int32 SelectedGArtID { get; set; }
    public List<Int32> ListSelectedGArtID = new List<int>();

    /*************************************************************************
     *                      Methoden / Procedure
     * ***********************************************************************/
    ///<summary>clsBestand / Copy</summary>
    ///<remarks></remarks>
    public clsAbruf Copy()
    {
        return (clsAbruf)this.MemberwiseClone();
    }
    ///<summary>clsAbruf / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public DataTable GetBestandToday(string strAbrufArt, clsCompany myClsCompany, bool bInclSelect, bool bGroup, string myComp = null)
    {
        clsAbruf tmpAb = new clsAbruf();
        string exArtikelIDString = tmpAb.GetOpenAbrufArtikelIDSting(myClsCompany);
        this.AbBereichID = myClsCompany.AbBereichID;

        string strSql = string.Empty;
        string strSql2 = string.Empty;
        DataTable dt = new DataTable();
        myComp = myClsCompany.Shortname;

        switch (myComp)
        {
            case clsCompany.const_CompanyName_VWSachsen:
                DataTable dtTmp = new DataTable();
                switch (strAbrufArt)
                {
                    case const_AbrufAktion_Abruf:
                        strSql = GetBestandVWSachsen(const_AbrufAktion_Abruf, const_GArtArt_Platinen, myClsCompany, bGroup, bInclSelect);
                        dt = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Bestand");
                        //strSql = strSql + " UNION ";
                        strSql = string.Empty;
                        strSql = GetBestandVWSachsen(const_AbrufAktion_Abruf, const_GArtArt_Coils, myClsCompany, bGroup, bInclSelect);
                        dtTmp = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Bestand");
                        foreach (DataRow row in dtTmp.Rows)
                        {
                            dt.ImportRow(row);
                        }
                        break;

                    case const_AbrufAktion_UB:
                        strSql = GetBestandVWSachsen(const_AbrufAktion_UB, const_GArtArt_Platinen, myClsCompany, bGroup, bInclSelect);
                        dt = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Bestand");
                        strSql = string.Empty;
                        strSql = strSql + GetBestandVWSachsen(const_AbrufAktion_UB, const_GArtArt_Coils, myClsCompany, bGroup, bInclSelect);
                        dtTmp = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Bestand");
                        foreach (DataRow row in dtTmp.Rows)
                        {
                            dt.ImportRow(row);
                        }
                        break;
                }
                break;

            default:
                //if (bGroup)
                //{
                //    //strSql = Bestand.GetSQLMainGroupedBestandsdaten(bInclSelect);
                //    strSql = Bestand.GetSQLMainGroupedBestandsdaten();
                //}
                //else
                //{
                //    strSql = Bestand.GetSQLMainBestandsdaten(bInclSelect);
                //}
                //strSql2 = string.Empty;


                //switch (strAbrufArt)
                //{
                //    case const_AbrufAktion_Abruf:
                //        strSql2 = " From Artikel a " +
                //                    "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                //                    "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                //                    "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                //                    "WHERE " +
                //                        " a.CheckArt=1 " +
                //                        " AND b.[Check]=1 " +
                //                        " AND a.LAusgangTableID=0 " +
                //                        " AND b.Empfaenger IN (" + String.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") " +
                //                        " AND b.AbBereich=" + AbBereichID + " "+
                //                        " AND a.ID NOT IN (Select DISTINCT ArtikelID FROM Abrufe WHERE Aktion='" + const_AbrufAktion_Abruf + "') ";                              
                //        break;

                //    case const_AbrufAktion_UB:
                //        strSql2 = " From Artikel a " +
                //                    "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                //                    "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                //                    "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                //                    "WHERE " +
                //                        " a.CheckArt=1 " +
                //                        " AND b.[Check]=1 " +
                //                        " AND a.LAusgangTableID=0 " +
                //                        " AND b.Auftraggeber NOT IN (" + String.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") " +
                //                        " AND b.AbBereich=" + AbBereichID + " "+
                //                            " AND a.ID NOT IN (Select DISTINCT ArtikelID FROM Abrufe WHERE Aktion='" + const_AbrufAktion_UB + "') ";
                //        break;
                //}

                //if (strSql2 != string.Empty)
                //{
                //    if (bGroup)
                //    {
                //        strSql2 = strSql2 + " Group By e.ArtikelArt , a.Werksnummer,e.Bezeichnung, a.Dicke, a.Breite, a.GArtID Order by e.ArtikelArt desc, a.Dicke, a.Breite, a.Werksnummer";
                //    }
                //    else
                //    {
                //        strSql2 = strSql2 + " Order by b.Date ";
                //    }
                //}
                //                strSql = strSql + strSql2;
                //dt = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Bestand");
                break;
        }

        return dt;
    }
    ///<summary>clsAbruf / GetBestandVWSachsen</summary>
    ///<remarks></remarks>
    private string GetBestandVWSachsen(string AbrufArt, string myArt, clsCompany myClsCompany, bool myGroup, bool myIncSelect)
    {
        string strReturnSQL = string.Empty;
        switch (AbrufArt)
        {
            case const_AbrufAktion_Abruf:
                if (myGroup)
                {
                    strReturnSQL = Bestand.GetSQLMainGroupedBestandsdaten();
                }
                else
                {
                    strReturnSQL = Bestand.GetSQLMainBestandsdaten(myIncSelect);
                }
                strReturnSQL = strReturnSQL +
                                " From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                "WHERE " +
                                    " a.CheckArt=1 " +
                                    " AND b.[Check]=1 " +
                                    " AND a.LAusgangTableID=0 " +
                                    " AND b.Empfaenger IN (" + String.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") " +
                                    " AND b.AbBereich=" + AbBereichID + " " +
                                    " AND e.ArtikelArt ='" + myArt + "' " +
                                    " AND a.ID NOT IN (Select DISTINCT ArtikelID FROM Abrufe where IsRead=0) ";
                //" AND a.ID NOT IN (Select DISTINCT ArtikelID FROM Abrufe WHERE Aktion='" + const_AbrufAktion_Abruf + "') ";
                break;

            case const_AbrufAktion_UB:
                if (myGroup)
                {
                    strReturnSQL = Bestand.GetSQLMainGroupedBestandsdaten();
                }
                else
                {
                    strReturnSQL = Bestand.GetSQLMainBestandsdaten(myIncSelect);
                }
                strReturnSQL = strReturnSQL +
                               " From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                "WHERE " +
                                    " a.CheckArt=1 " +
                                    " AND b.[Check]=1 " +
                                    " AND a.LAusgangTableID=0 " +
                                    " AND b.Auftraggeber NOT IN (" + String.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") " +
                                    " AND b.AbBereich=" + AbBereichID + " " +
                                    " AND e.ArtikelArt ='" + myArt + "' " +
                                    " AND a.ID NOT IN (Select DISTINCT ArtikelID FROM Abrufe where IsRead=0) ";
                //" AND a.ID NOT IN (Select DISTINCT ArtikelID FROM Abrufe WHERE Aktion='" + const_AbrufAktion_UB + "') ";
                break;
        }
        if (myGroup)
        {
            switch (myArt)
            {
                case const_GArtArt_Platinen:
                    strReturnSQL = strReturnSQL + " Group By e.ArtikelArt , a.Werksnummer,e.Bezeichnung, a.Dicke, a.Breite, a.GArtID ";
                    break;

                case const_GArtArt_Coils:
                    strReturnSQL = strReturnSQL + " Group By e.ArtikelArt , a.Werksnummer,e.Bezeichnung, a.Dicke, a.Breite, a.GArtID ";

                    break;

                default:
                    break;
            }
            strReturnSQL = strReturnSQL + "Order by e.ArtikelArt desc, a.Dicke, a.Breite, a.Werksnummer";
        }
        else
        {
            strReturnSQL = strReturnSQL + "Order by a.Dicke, a.Breite, b.Date";
        }

        return strReturnSQL;
    }
    ///<summary>clsAbruf / GetListAbrufToCreate</summary>
    ///<remarks>Aus dem Dictionary werden die Item-Classen in einer Liste zusammengefasst</remarks>
    public List<clsAbruf> GetListAbrufToCreate()
    {
        List<clsAbruf> RetList = new List<clsAbruf>();
        foreach (KeyValuePair<Int32, clsAbruf> itm in DictAbrufeToInsert)
        {
            RetList.Add(itm.Value);
        }
        return RetList;
    }
    ///<summary>clsAbruf / RemoveItemFromDicAbrufeToInsert</summary>
    ///<remarks>Entfernt den Item mit dem entsprechendne Key</remarks>
    public void RemoveItemFromDicAbrufeToInsert(Int32 myArtikelID)
    {
        if (this.DictAbrufeToInsert.ContainsKey(myArtikelID))
        {
            this.DictAbrufeToInsert.Remove(myArtikelID);
        }
    }
    ///<summary>clsAbruf / Add</summary>
    ///<remarks></remarks>
    public bool Add()
    {
        bool bOK = false;
        string strSQL = string.Empty;
        this.IsCreated = false;
        strSQL = "INSERT INTO Abrufe (IsRead, ArtikelID, LVSNr, Werksnummer, Produktionsnummer, Charge, Brutto, CompanyID, CompanyName, AbBereich, Datum" +
                                       ", EintreffDatum, EintreffZeit,BenutzerID, Benutzername, Schicht, Referenz, Abladestelle, Aktion, Erstellt, IsCreated" +
                                       ", Status, LiefAdrID, EmpAdrID" +
                                     ") " +
                 "VALUES (" + Convert.ToInt32(this.IsRead) +
                             ", " + this.ArtikelID +
                             ", " + this.LVSNr +
                             ", '" + this.Werksnummer + "'" +
                             ", '" + this.Produktionsnummer + "'" +
                             ", '" + this.Charge + "'" +
                             ", '" + this.Brutto.ToString().Replace(",", ".") + "'" +
                             ", " + this.CompanyID +
                             ", '" + this.CompanyName + "'" +
                             ", " + this.AbBereichID +
                             ", '" + this.Datum + "'" +
                             ", '" + this.EintreffDatum + "'" +
                             ", '" + this.EintreffZeit + "' " +
                             ", " + this.BenutzerID +
                             ", '" + this.Benutzername + "'" +
                             ", '" + this.Schicht + "'" +
                             ", '" + this.Referenz + "'" +
                             ", '" + this.Abladestelle + "'" +
                             ", '" + this.Aktion + "'" +
                             ", '" + this.Erstellt + "'" +
                             ", " + Convert.ToInt32(IsCreated) +
                             ", '" + this.Status + "'" + //Status
                             ", " + this.LiefAdrID +
                             ", " + this.EmpAdrID +
                             ");";
        //strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
        try
        {
            bOK = this.SQLconLVS.ExecuteSQL(strSQL);
            this.InfoText = "Der Datensatz konnte erfolgreich hinzugefügt werden.";

        }
        catch (Exception ex)
        {
            this.InfoText = "Der neue Datensatz konnte nicht eingetragen werden. Fehlercode [" + clsErrorCode.const_ErrorCode_200 + "]";
        }
        return bOK;
    }
    ///<summary>clsAbruf / Update</summary>
    ///<remarks></remarks>
    public bool Update()
    {
        bool bOK = false;
        DataTable dt = new DataTable();
        string strSQL = "Update Abrufe SET " +
                                "IsRead =" + Convert.ToInt32(this.IsRead) +
                                ", ArtikelID=" + this.ArtikelID +
                                ", LVSNr =" + this.LVSNr +
                                ", Werksnummer ='" + this.Werksnummer + "'" +
                                ", Produktionsnummer ='" + this.Produktionsnummer + "'" +
                                ", Charge ='" + this.Charge + "'" +
                                ", Brutto = '" + this.Brutto.ToString().Replace(",", ".") + "'" +
                                ", CompanyID= " + this.CompanyID +
                                ", CompanyName ='" + this.CompanyName + "'" +
                                ", AbBereich = " + this.AbBereichID +
                                ", Datum ='" + this.Datum + "'" +
                                ", EintreffDatum ='" + this.EintreffDatum + "'" +
                                ", EintreffZeit ='" + this.EintreffZeit + "'" +
                                ", BenutzerID =" + this.BenutzerID +
                                ", Benutzername = '" + this.Benutzername + "'" +
                                ", Schicht='" + this.Schicht + "'" +
                                ", Referenz = '" + this.Referenz + "'" +
                                ", Abladestelle ='" + this.Abladestelle + "'" +
                                ", Aktion ='" + this.Aktion + "'" +
                                ", Erstellt ='" + this.Erstellt + "'" +
                                ", Status='" + this.Status + "'" +
                                ", LiefAdrID= " + this.LiefAdrID +
                                ", EmpAdrID = " + this.EmpAdrID +
                                " WHERE ID=" + this.ID + ";";
        bOK = this.SQLconLVS.ExecuteSQL(strSQL);
        if (bOK)
        {
            this.InfoText = "Der Datensatz konnte erfolgreich upgedatet werden!";
        }
        else
        {
            this.InfoText = "Der Datensatz konnte nicht upgedatet werden. Fehlercode[" + clsErrorCode.const_ErrorCode_201.ToString() + "]";
        }
        return bOK;
    }
    ///<summary>clsAbruf / Update_SetCreated</summary>
    ///<remarks></remarks>
    public bool Update_SetCreated()
    {
        bool bOK = false;
        DataTable dt = new DataTable();
        string strSQL = "Update Abrufe SET " +
                                "IsCreated = " + Convert.ToInt32(IsCreated) +
                                ", Status ='" + this.Status + "'" +
                                " WHERE ID=" + this.ID + ";";
        bOK = this.SQLconLVS.ExecuteSQL(strSQL);
        return bOK;
    }
    ///<summary>clsAbruf / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public List<clsAbruf> GetSelecteddAbrufList(clsCompany myClsCompany, string myAktion)
    {
        string strSql = string.Empty;
        DataTable dt = new DataTable();
        strSql = "Select * FROM Abrufe WHERE AbBereich=" + myClsCompany.AbBereichID + " AND IsRead=0 AND Aktion='" + myAktion + "' AND IsCreated=0 ";
        dt = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Abrufe");
        FillClass(ref dt);
        ListOpenAbrufList = this.GetListAbrufToCreate();
        return ListOpenAbrufList;
    }
    ///<summary>clsAbruf / GetMainSQLAbrufe</summary>
    ///<remarks>.</remarks>
    private string GetMainSQLAbrufe()
    {
        string strSQL = "Select " +
                                "Abrufe.ID as AbrufID" +
                                ", Abrufe.ArtikelID" +
                                ", Abrufe.IsRead" +
                                ", Abrufe.CompanyID" +
                                ", Abrufe.CompanyName" +
                                ", Abrufe.AbBereich" +
                                ", Abrufe.Datum Abrufdatum" +
                                ", Abrufe.EintreffDatum as Eintreffdatum" +
                                ", Abrufe.EintreffZeit as Eintreffzeit" +
                                ", Abrufe.Referenz" +
                                ", Abrufe.Abladestelle" +
                                ", Abrufe.Aktion" +
                                ", Abrufe.Erstellt" +
                                ", Artikel.Dicke" +
                                ", Artikel.Breite" +
                                ", Gut.Bezeichnung as Gut" +
                                ", Artikel.Werksnummer" +
                                ", Artikel.Anzahl" +
                                ", Artikel.Brutto" +
                                ", Artikel.LVS_ID as LVSNr" +
                                ", LEingang.Date as Eingangsdatum" +
                                ", Artikel.Charge" +
                                 ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=LEingang.Auftraggeber) as Auftraggeber" +
                                ", Artikel.Produktionsnummer" +
                                ", LEingang.LfsNr as Lieferschein" +
                                ", Benutzer.Name as Benutzer " +
                                ", Benutzer.Schicht " +
                                ", (Select Top(1) s.Bezeichnung FROM SchadenZuweisung sz INNER JOIN Schaeden s on s.ID=sz.SchadenID WHERE sz.ArtikelID=Artikel.ID ORDER BY sz.Datum) as Schaden " +
                                //", CASE " +
                                //   "WHEN (Select spl.ID FROM Sperrlager spl WHERE spl.ArtikelID=Artikel.ID)>0 THEN CAST(1 as bit) " +
                                //   "ELSE CAST(0 as bit) " +
                                //   "END as IsSPL " +
                                //", CASE "+
                                //"WHEN(Select spl.ID FROM Sperrlager spl WHERE spl.ArtikelID = Artikel.ID AND BKZ = 'IN') > 0 "+
                                // "THEN "+
                                //     "CASE "+
                                //         "WHEN(Select spl.ID FROM Sperrlager spl WHERE spl.ArtikelID = Artikel.ID AND BKZ = 'OUT') > 0 "+
                                //         "THEN CAST(0 as bit) "+
                                //"ELSE CAST(1 as bit) "+
                                //"END "+
                                // "ELSE CAST(0 as bit) END as IsSPL "+
                                ", CASE " +
                                    "WHEN(Select TOP(1) spl.ID FROM Sperrlager spl WHERE spl.ArtikelID = Artikel.ID AND BKZ = 'IN') > 0 " +
                                    "THEN " +
                                        "CASE " +
                                            "WHEN(Select TOP(1) spl.ID FROM Sperrlager spl WHERE spl.ArtikelID = Artikel.ID AND BKZ = 'OUT') > 0 " +
                                            "THEN CAST(0 as bit) " +
                                            "ELSE CAST(1 as bit) " +
                                            "END " +
                                    "ELSE CAST(0 as bit) END as IsSPL " +
                                "FROM Abrufe " +
                                "INNER JOIN Artikel on Artikel.ID=Abrufe.ArtikelID " +
                                "INNER JOIN Gueterart Gut on Gut.ID=Artikel.GArtID " +
                                "INNER JOIN LEingang on LEingang.ID=Artikel.LEingangTableID " +
                                "LEFT JOIN SZG_Call.dbo.Benutzer  on Benutzer.ID=Abrufe.BenutzerID ";
        return strSQL;
    }
    ///<summary>clsAbruf / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public DataTable GetdVorgemerkteAbrufUBList(clsCompany myClsCompany, string myAktion)
    {
        dtAbrufUBList = new DataTable("AbrufUBList");
        string strSql = string.Empty;
        DataTable dt = new DataTable();
        strSql = GetMainSQLAbrufe();
        strSql = strSql + " WHERE " +
                            " Abrufe.AbBereich=" + myClsCompany.AbBereichID +
                            " AND Abrufe.IsRead=0 " +
                            " AND Abrufe.Aktion='" + myAktion + "' " +
                            " AND Abrufe.IsCreated=0 ";

        dtAbrufUBList = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "AbrufUBList");
        return dtAbrufUBList;
    }
    ///<summary>clsAbruf / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public DataTable GetCreatedAbrufUBList(clsCompany myClsCompany, string myAktion)
    {
        string strSql = string.Empty;
        dtAbrufUBList = new DataTable("AbrufUBList");
        strSql = GetMainSQLAbrufe();
        strSql = strSql + " WHERE " +
                    " Abrufe.AbBereich=" + myClsCompany.AbBereichID +
                    " AND Abrufe.IsRead=0 " +
                    " AND Abrufe.Aktion='" + myAktion + "' " +
                    " AND Abrufe.IsCreated=1 ";
        dtAbrufUBList = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "AbrufUBList");
        return dtAbrufUBList;
    }

    ///<summary>clsAbruf / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public List<clsAbruf> GetCreatedAbrufList(clsCompany myClsCompany, string myAktion)
    {
        string strSql = string.Empty;
        DataTable dt = new DataTable();
        strSql = "Select * FROM Abrufe WHERE AbBereich=" + myClsCompany.AbBereichID + " AND IsRead=0 AND Aktion='" + myAktion + "' AND IsCreated=1 ";
        dt = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Abrufe");
        FillClass(ref dt);
        ListOpenAbrufList = this.GetListAbrufToCreate();
        return ListOpenAbrufList;
    }
    ///<summary>clsAbruf / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public string GetOpenAbrufArtikelIDSting(clsCompany myClsCompany)
    {
        string strSql = string.Empty;
        DataTable dt = new DataTable();
        strSql = "SELECT " +
                        "STUFF(" +
                                "(SELECT ', ' + CAST(a.ArtikelID as nvarchar) " +
                                                " FROM Abrufe a " +
                                                " WHERE a.AbBereich=" + myClsCompany.AbBereichID +
                                                " AND a.IsRead=0 " +
                                                " Order By a.ArtikelID " +
                                                "FOR XML PATH ('') " +
                                ")" +
                                ",1,2,'')";
        string retVal = this.SQLconLVS.ExecuteSQL_GetValue(strSql);
        return retVal;
    }

    ///<summary>clsAbruf / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public void Fill()
    {
        string strSql = string.Empty;
        DataTable myDT = new DataTable();
        strSql = "SELECT * FROM Abrufe WHERE ID=" + this.ID;

        myDT = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Abrufe");
        for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
        {
            Int32 iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["ID"].ToString(), out iTmp);
            this.ID = iTmp;
            this.IsRead = (bool)myDT.Rows[i]["IsRead"];
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["ArtikelID"].ToString(), out iTmp);
            this.ArtikelID = iTmp;
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["LVSNr"].ToString(), out iTmp);
            this.LVSNr = iTmp;
            this.Werksnummer = myDT.Rows[i]["Werksnummer"].ToString();
            this.Produktionsnummer = myDT.Rows[i]["Produktionsnummer"].ToString();
            this.Charge = myDT.Rows[i]["Charge"].ToString();
            decimal decTmp = 0;
            Decimal.TryParse(myDT.Rows[i]["Brutto"].ToString(), out decTmp);
            this.Brutto = decTmp;
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["CompanyID"].ToString(), out iTmp);
            this.CompanyID = iTmp;
            this.CompanyName = myDT.Rows[i]["CompanyName"].ToString();
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["AbBereich"].ToString(), out iTmp);
            this.AbBereichID = iTmp;
            this.Datum = (DateTime)myDT.Rows[i]["Datum"];
            this.EintreffDatum = (DateTime)myDT.Rows[i]["EintreffDatum"];
            this.EintreffZeit = (DateTime)myDT.Rows[i]["EintreffZeit"];
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["BenutzerID"].ToString(), out iTmp);
            this.BenutzerID = iTmp;
            this.Benutzername = myDT.Rows[i]["Benutzername"].ToString();
            this.Schicht = myDT.Rows[i]["Schicht"].ToString();
            this.Referenz = myDT.Rows[i]["Referenz"].ToString();
            this.Abladestelle = myDT.Rows[i]["Abladestelle"].ToString();
            this.Aktion = myDT.Rows[i]["Aktion"].ToString();
            this.Erstellt = (DateTime)myDT.Rows[i]["Erstellt"];
            this.Status = myDT.Rows[i]["Status"].ToString();
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["LiefAdrID"].ToString(), out iTmp);
            this.LiefAdrID = iTmp;
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["EmpAdrID"].ToString(), out iTmp);
            this.EmpAdrID = iTmp;
            //clsAbruf tmpAb = this.Copy();
            //if (!this.DictAbrufeToInsert.ContainsKey(tmpAb.ArtikelID))
            //{
            //    DictAbrufeToInsert.Add(tmpAb.ArtikelID, tmpAb);
            //}
        }
    }
    ///<summary>clsAbruf / FillClass</summary>
    ///<remarks>.</remarks>
    private void FillClass(ref DataTable myDT)
    {
        DictAbrufeToInsert = new Dictionary<int, clsAbruf>();
        for (Int32 i = 0; i <= myDT.Rows.Count - 1; i++)
        {
            Int32 iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["ID"].ToString(), out iTmp);
            this.ID = iTmp;
            this.IsRead = (bool)myDT.Rows[i]["IsRead"];
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["ArtikelID"].ToString(), out iTmp);
            this.ArtikelID = iTmp;
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["LVSNr"].ToString(), out iTmp);
            this.LVSNr = iTmp;
            this.Werksnummer = myDT.Rows[i]["Werksnummer"].ToString();
            this.Produktionsnummer = myDT.Rows[i]["Produktionsnummer"].ToString();
            this.Charge = myDT.Rows[i]["Charge"].ToString();
            decimal decTmp = 0;
            Decimal.TryParse(myDT.Rows[i]["Brutto"].ToString(), out decTmp);
            this.Brutto = decTmp;
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["CompanyID"].ToString(), out iTmp);
            this.CompanyID = iTmp;
            this.CompanyName = myDT.Rows[i]["CompanyName"].ToString();
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["AbBereich"].ToString(), out iTmp);
            this.AbBereichID = iTmp;
            this.Datum = (DateTime)myDT.Rows[i]["Datum"];
            this.EintreffDatum = (DateTime)myDT.Rows[i]["EintreffDatum"];
            this.EintreffZeit = (DateTime)myDT.Rows[i]["EintreffZeit"];
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["BenutzerID"].ToString(), out iTmp);
            this.BenutzerID = iTmp;
            this.Benutzername = myDT.Rows[i]["Benutzername"].ToString();
            this.Schicht = myDT.Rows[i]["Schicht"].ToString();
            this.Referenz = myDT.Rows[i]["Referenz"].ToString();
            this.Abladestelle = myDT.Rows[i]["Abladestelle"].ToString();
            this.Aktion = myDT.Rows[i]["Aktion"].ToString();
            this.Erstellt = (DateTime)myDT.Rows[i]["Erstellt"];
            this.Status = myDT.Rows[i]["Status"].ToString();
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["LiefAdrID"].ToString(), out iTmp);
            this.LiefAdrID = iTmp;
            iTmp = 0;
            Int32.TryParse(myDT.Rows[i]["EmpAdrID"].ToString(), out iTmp);
            this.EmpAdrID = iTmp;
            clsAbruf tmpAb = this.Copy();
            if (!this.DictAbrufeToInsert.ContainsKey(tmpAb.ArtikelID))
            {
                DictAbrufeToInsert.Add(tmpAb.ArtikelID, tmpAb);
            }

        }
    }
    ///<summary>clsAbruf / Delete</summary>
    ///<remarks></remarks>
    public bool Delete()
    {
        bool bOK = false;
        string strSQL = "Delete Abrufe WHERE ID=" + this.ID + " ;";
        bOK = this.SQLconLVS.ExecuteSQL(strSQL);
        if (bOK)
        {
            this.InfoText = "Der Datensatz konnte erfolgreich gelöscht werden.";
        }
        else
        {
            this.InfoText = "Der Datensatz konnte nicht gelöscht werden.";
        }
        return bOK;
    }

}