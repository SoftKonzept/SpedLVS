using System;
using System.Data;

/// <summary>
/// Zusammenfassungsbeschreibung für clsBestand
/// </summary>
public class clsBestand
{
    public const string const_Bestandart_Tagesbestand = "Tagesbestand";
    public const string const_Bestandart_TagesbestandEigen = "TagesbestandEigen";
    public const string const_Bestandart_BestandSPL = "Sperrlager[SPL]";
    public const string const_Bestandart_BestandMaterial = "BestandMaterial";




    public clsSQLcon_LVS SQLconLVS = new clsSQLcon_LVS();
    public DateTime BestandVon { get; set; }
    public DateTime BestandBis { get; set; }
    public DataTable dtBestand = new DataTable();
    public Int32 AbBereichID { get; set; }
    public Int32 BestandAdrID { get; set; }


    ///<summary>clsBestand / Copy</summary>
    ///<remarks></remarks>
    public clsBestand Copy()
    {
        return (clsBestand)this.MemberwiseClone();
    }
    ///<summary>clsBestand / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public string GetSQLMainBestandsdaten(bool myInclSelect)
    {
        string strSql = string.Empty;
        strSql = "Select ";
        if (myInclSelect)
        {
            strSql = strSql + "CAST(0 as bit) as 'Selected' " +
                            ", CAST(a.ID as INT) as ArtikelID ";
        }
        else
        {
            strSql = strSql + "CAST(a.ID as INT) as ArtikelID ";
        }
        strSql = strSql + ", CAST(a.LVS_ID as INT) as LVSNr " +
                       ", a.Werksnummer" +
                       ", a.Produktionsnummer" +
                       ", a.Charge" +
                        ", a.LVS_ID as LVSNr" +
                       ", e.Bezeichnung as Gut" +
                       ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                       ", b.Auftraggeber as AuftraggeberID" +
                        //", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) as Empfaenger" +
                        //Abmessungen einzeln
                        //", CAST( a.Dicke as varchar(30))+'x'+CAST(a.Breite as varchar(30))+'x'+CAST(a.Laenge as varchar(30))+'x'+CAST(a.Hoehe as varchar(30)) as 'Abmessung'" +
                        ", a.Anzahl" +
                        ", a.Einheit" +
                        ", a.Dicke" +
                        ", a.Breite" +
                        ", a.Laenge" +
                        ", a.Hoehe" +
                        ", a.Netto" +
                       ", a.Brutto" +
                        ", a.exBezeichnung" +
                        ", a.Bestellnummer" +
                        ", a.exMaterialnummer" +
                       ", CAST(b.LEingangID as INT) as Eingang" +
                       ", b.Date as 'Eingangsdatum'" +
                       ", CAST(MONTH(b.Date) as varchar)+'/'+CAST(YEAR(b.Date) as varchar)  as Eingangsmonat" +
                       ", b.LfsNr as Lieferschein" +
                       ", CAST(c.LAusgangID as INT) as Ausgang" +
                       ", c.Datum as 'Ausgangsdatum'" +
                       ", a.GArtID " +
                       ", CAST(MONTH(c.Datum) as varchar)+'/'+CAST(YEAR(c.Datum) as varchar)  as Ausgangsmonat" +
                       ", (Select Top(1) s.Bezeichnung FROM SchadenZuweisung sz INNER JOIN Schaeden s on s.ID=sz.SchadenID WHERE sz.ArtikelID=a.ID ORDER BY sz.Datum) as Schaden " +
                       ", CASE " +
                           "WHEN (c.Datum IS NULL) " +
                           "THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(GETDATE()as Date)) as INT)+1 " +
                           "ELSE CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as Date)) as INT)+1 " +
                           "END as Lagerdauer " +
                       //", CASE " +
                       //     "WHEN (Select Top(1) ID FROM Sperrlager WHERE ArtikelID=a.ID)>0 THEN CAST(1 as bit) " +
                       //     "ELSE CAST(0 as bit) " +
                       //     "END as IsSPL " +

                       ", CASE " +
                            "WHEN(Select Top(1) spl.ID FROM Sperrlager spl WHERE spl.ArtikelID = a.ID AND BKZ = 'IN') > 0 " +
                            "THEN " +
                                "CASE " +
                                "WHEN(Select Top(1) spl.ID FROM Sperrlager spl WHERE spl.ArtikelID = a.ID AND BKZ = 'OUT') > 0 " +
                                "THEN CAST(0 as bit) " +
                                "ELSE CAST(1 as bit) " +
                                "END " +
                            "ELSE CAST(0 as bit) END as IsSPL " +
                        " ";
        return strSql;
    }
    ///<summary>clsBestand / GetSQLMainBestandsdaten</summary>
    ///<remarks>.</remarks>
    public string GetSQLMainGroupedBestandsdaten()
    //public string GetSQLMainGroupedBestandsdaten(bool myInclSelect)
    {
        string strSql = string.Empty;
        strSql = "Select " +
                    "a.Werksnummer " +
                    ", e.Bezeichnung as Gut" +
                    ", a.Dicke" +
                    ", a.Breite" +
                    ", SUM(a.Anzahl) as Menge " +
                    ", SUM(a.Netto) as Netto " +
                    ", SUM(a.Brutto) as Brutto " +
                    ", a.GArtID " +
                    ", e.ArtikelArt as Art";
        return strSql;
    }
    ///<summary>clsBestand / GetSperrlager</summary>
    ///<remarks>.</remarks>
    public string GetSperrlagerSQL(bool bForJournal, bool mySelectIncl, bool bGroup, clsCompany myClsCompany)
    {
        this.AbBereichID = myClsCompany.AbBereichID;
        string strSql = string.Empty;
        DataTable dt = new DataTable();
        if (!bForJournal)
        {
            if (bGroup)
            {
                //strSql = GetSQLMainGroupedBestandsdaten(mySelectIncl);
                strSql = GetSQLMainGroupedBestandsdaten();
            }
            else
            {
                strSql = GetSQLMainBestandsdaten(mySelectIncl);
                strSql = strSql + ", (Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Datum SPL' ";
            }
        }

        //strSql = strSql + ", (Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Datum SPL' ";
        // ", (Select TOP(1) BKZ FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Buchung'";
        //", CAST(0 as bit) as 'ausbuchen'";
        //if (bForJournal)
        //{
        //    //strSql = strSql +
        //    //", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber";
        //}
        strSql = strSql + " From Artikel a " +
                        "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                        "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                        "INNER JOIN Sperrlager spl on spl.ArtikelID = a.ID " +
                        "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                        "WHERE " +
                                //"(Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) > '" + BestandVon.Date.AddDays(-1) + "' AND " +
                                //"(Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) <'" + BestandBis.Date.AddDays(1) + "' " +
                                "a.AB_ID = " + this.AbBereichID.ToString() +
                                " and a.ID IN (SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                    "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)) ";

        return strSql;
    }
    ///<summary>clsLager / GetBestand</summary>
    ///<remarks></remarks>
    public DataTable GetBestand(string strMyBestandArt, clsCompany myClsCompany, bool bInclSelect, bool bGroup)
    {
        this.AbBereichID = myClsCompany.AbBereichID;
        clsAbruf tmpAb = new clsAbruf();
        string exArtikelIDString = tmpAb.GetOpenAbrufArtikelIDSting(myClsCompany);

        string strSql = string.Empty;
        DataTable dt = new DataTable();
        string strSql2 = string.Empty;

        //SQL Main
        if (bGroup)
        {
            //strSql = GetSQLMainGroupedBestandsdaten(bInclSelect);
            strSql = GetSQLMainGroupedBestandsdaten();
        }
        else
        {
            strSql = GetSQLMainBestandsdaten(bInclSelect);
        }


        switch (strMyBestandArt)
        {
            //Tagesbestand
            /**********************************************************************************************************
             * Die Abfrage ist in zwei Filter aufgebaut:
             * 1. Alle Eingänge vor dem Starpunkt des Beobachtungszeitraums. Diese können folgende Merkmale aufweisen:
             *  - Artikel befindet sich auch zum Zeitpunkt der Abfrage noch im Lager
             *  - Auslagerung des Artikels hat im Zeitraum zwischen Stichtag und Zeitpunkt Abfrage stattgefunden
             *  *******************************************************************************************************/
            case const_Bestandart_Tagesbestand:
                strSql2 = " From Artikel a " +
                          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                          "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                          "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                          "WHERE " +
                                " b.AbBereich=" + AbBereichID + " " +
                                "AND " +
                                "(" +
                                    "( " +
                                        "a.CheckArt=1 " +
                                        "AND b.[Check]=1 " +
                                        "AND (c.Checked is Null or c.Checked=0) ";

                if (myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSql2 = strSql2 + "AND b.Auftraggeber IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }
                else
                {
                    strSql2 = strSql2 + "AND b.Empfaenger IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }
                strSql2 = strSql2 + "AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " +
                        ") " +
                        " OR " +
                        "(";

                if (myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSql2 = strSql2 + " b.Auftraggeber IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }
                else
                {
                    strSql2 = strSql2 + " b.Empfaenger IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }

                strSql2 = strSql2 + " AND a.CheckArt=1 AND b.[Check]=1 AND b.DirectDelivery=0 AND b.AbBereich=" + AbBereichID + " " +
                             " AND c.Datum>='" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
                             " AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
                       ")" +
                   ") " +
                   " AND a.ID NOT IN (" +
                                       "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                               "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                       ") ";
                if (exArtikelIDString != string.Empty)
                {
                    strSql2 = strSql2 + " AND a.ID NOT IN (" + exArtikelIDString + ") "; ;
                }
                //strSql2 = strSql2 + "Order by b.Date ";
                break;


            //EIgener Bestand    
            case const_Bestandart_TagesbestandEigen:
                strSql2 = " From Artikel a " +
                          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                          "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                          "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                          "WHERE " +
                                " b.AbBereich=" + AbBereichID + " " +
                                "AND " +
                                "(" +
                                    "( " +
                                        "a.CheckArt=1 " +
                                        "AND b.[Check]=1 " +
                                        "AND (c.Checked is Null or c.Checked=0) " +
                                        "AND b.Auftraggeber IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") " +
                                        "AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " +
                                    ") " +
                                    " OR " +
                                    "(" +

                                          " b.Auftraggeber IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") " +
                                          " AND a.CheckArt=1 AND b.[Check]=1 AND b.DirectDelivery=0 AND b.AbBereich=" + AbBereichID + " " +
                                          " AND c.Datum>='" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
                                          " AND b.Date <'" + BestandVon.Date.AddDays(1).ToShortDateString() + "' " +
                                    ")" +
                                ") " +
                                " AND a.ID NOT IN (" +
                                                    "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                            "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                    ") ";
                if (exArtikelIDString != string.Empty)
                {
                    strSql2 = strSql2 + " AND a.ID NOT IN (" + exArtikelIDString + ") "; ;
                }
                //strSql2 = strSql2 + "Order by b.Date ";
                break;
            case const_Bestandart_BestandSPL:
                strSql2 = GetSperrlagerSQL(true, bInclSelect, bGroup, myClsCompany);
                break;

        }

        if (strSql2 != string.Empty)
        {
            if (bGroup)
            {
                strSql2 = strSql2 + " Group By e.ArtikelArt , a.Werksnummer,e.Bezeichnung, a.Dicke, a.Breite, a.GArtID";
            }
            else
            {
                strSql2 = strSql2 + " Order by b.Date ";
            }
            strSql = strSql + strSql2;
            dt = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Bestand");
        }
        return dt;
    }

}