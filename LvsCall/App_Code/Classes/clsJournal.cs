using System;
using System.Data;


public class clsJournal
{
    public const string const_ComboLieferant_FirstItem = "--- wählen Sie einen Lieferanten ---";

    public const string const_Journalart_komplett = "JournalKomplett";
    public const string const_Journalart_Abrufe = "JournalAbrufe";
    public const string const_Journalart_EingangJournal = "JournalEingang";
    public const string const_Journalart_AusgangJournal = "JournalAusgang";
    public const string const_Journalart_KomplettMitSchaden = "JournalKomplettMitSchaden";
    public const string const_Journalart_KomplettOhneSchaden = "JournalKomplettOhneSchaden";
    public const string const_Journalart_SPL = "JournalSPL";

    public clsSQLcon_LVS SQLconLVS = new clsSQLcon_LVS();
    public DateTime dtVon { get; set; }
    public DateTime dtBis { get; set; }
    public DataTable dtJournal = new DataTable();
    public DataTable dtLieferanten = new DataTable();

    ///<summary>clsJournal / Copy</summary>
    ///<remarks></remarks>
    public clsJournal Copy()
    {
        return (clsJournal)this.MemberwiseClone();
    }
    ///<summary>clsCallJournal / GetJournalDaten</summary>
    ///<remarks>.</remarks>
    //public DataTable GetJournalDaten(string myJournalArt, DateTime vonDT, DateTime bisDT, clsCompany myClsCompany)
    public void GetJournalDaten(string myJournalArt, clsCompany myClsCompany)
    {
        string strSql = string.Empty;
        this.dtJournal = new DataTable();

        strSql = "Select  ";
        if (myJournalArt == clsJournal.const_Journalart_KomplettMitSchaden)
            strSql = strSql + "distinct ";
        strSql = strSql +
                        "CAST(a.ID as INT) as ArtikelID " +
                        ", CAST(a.LVS_ID as INT) as LVSNr " +
                        ", a.Werksnummer" +
                        ", a.Produktionsnummer" +
                        ", a.Charge" +
                        ", e.Bezeichnung as Gut" +
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
                        ", CAST(b.Date as Date) as 'Eingangsdatum'" +
            ", b.LfsNr as Lieferschein" +
                        ", CAST(c.LAusgangID as INT) as Ausgang" +
                        ", CAST(c.Datum as Date) as 'Ausgangsdatum'" +
                        ", CAST( DATEDIFF(day, CAST(b.Date as Date), CAST(c.Datum as Date)) as INT)+1 as Lagerdauer ";
        //", a.Werk" +
        //", a.Halle" +
        //", a.Reihe" +
        //", a.Ebene" +
        //", a.Platz" +
        //", a.exInfo as Bemerkung" +
        //", a.BKZ" +
        //", b.WaggonNo" +
        //", a.FreigabeAbruf as Freigabe " +
        //", CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) as LZZ" +
        //", e.ID as GueterartID" +
        //", a.ArtIDRef" +
        //", " + clsArtikel.GetStatusColumnSQL("c", "b");

        if (!myClsCompany.CompanyGroup.IsLieferant)
        {
            strSql += ", f.[Status] as CallStatus";
        }
        strSql += " ";

        string strSQL2 = string.Empty;
        switch (myJournalArt)
        {
            //Alle
            case clsJournal.const_Journalart_komplett:
                strSQL2 = //", (Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                            ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            " From Artikel a " +
                            "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                            "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                            "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID ";
                if (!myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSQL2 += "INNER JOIN Abrufe f on f.ArtikelID = a.ID ";
                }

                strSQL2 += "WHERE " +
                                    "(" +
                                        "b.AbBereich = " + myClsCompany.AbBereichID + " ";
                if (myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSQL2 = strSQL2 + "AND b.Auftraggeber IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }
                else
                {
                    strSQL2 = strSQL2 + "AND b.Empfaenger IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }

                strSQL2 = strSQL2 + "AND (b.Date between '" + dtVon.Date.ToShortDateString() + "' AND '" + dtBis.Date.ToShortDateString() + "') " +
                                    ") " +
                                    "OR " +
                                    "(" +
                                        "c.AbBereich= " + myClsCompany.AbBereichID + " ";

                if (myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSQL2 = strSQL2 + "AND c.Auftraggeber IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }
                else
                {
                    strSQL2 = strSQL2 + "AND c.Empfaenger IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }


                strSQL2 = strSQL2 + "AND (c.Datum between '" + dtVon.Date.ToShortDateString() + "' AND '" + dtBis.Date.ToShortDateString() + "') " +
                                    ")" +
                                     " ORDER BY b.Date";
                break;

            // nur Eingänge
            case clsJournal.const_Journalart_EingangJournal:
                strSQL2 = ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            " From Artikel a " +
                            "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                            "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                            "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID ";
                if (!myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSQL2 += "INNER JOIN Abrufe f on f.ArtikelID = a.ID ";
                }
                strSQL2 += "WHERE " +
                                    "(" +
                                        "b.AbBereich = " + myClsCompany.AbBereichID + " " +
                                        "AND b.[Check]=1 AND b.DirectDelivery=0 ";
                if (myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSQL2 = strSQL2 + "AND b.Auftraggeber IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }
                else
                {
                    strSQL2 = strSQL2 + "AND b.Empfaenger IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }

                strSQL2 = strSQL2 + "AND (b.Date between '" + dtVon.Date.ToShortDateString() + "' AND '" + dtBis.Date.ToShortDateString() + "') " +
                                    ") " +
                                    " ORDER BY b.Date";
                break;
            //nur Ausgänge
            case clsJournal.const_Journalart_AusgangJournal:
                strSQL2 = ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Auftraggeber) as Auftraggeber" +
                            " From Artikel a " +
                            "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                            "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                            "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID ";
                if (!myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSQL2 += "INNER JOIN Abrufe f on f.ArtikelID = a.ID ";
                }
                strSQL2 += "WHERE " +
                                    "(" +
                                        "c.AbBereich= " + myClsCompany.AbBereichID + " " +
                                        "AND c.Checked=1 AND c.DirectDelivery=0 AND c.IsRL=0";
                if (myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSQL2 = strSQL2 + "AND c.Auftraggeber IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }
                else
                {
                    strSQL2 = strSQL2 + "AND c.Empfaenger IN (" + string.Join(",", myClsCompany.CompanyGroup.ListCompanyGroupAdrID.ToArray()) + ") ";
                }


                strSQL2 = strSQL2 + "AND (c.Datum between '" + dtVon.Date.ToShortDateString() + "' AND '" + dtBis.Date.ToShortDateString() + "') " +
                                    ")" +
                                     " ORDER BY c.Datum";
                break;
            //mit Schaden
            case clsJournal.const_Journalart_KomplettMitSchaden:
                strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                            ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            " From Artikel a " +
                            "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                            "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                            "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                            "INNER JOIN Abrufe f on f.ArtikelID = a.ID " +
                            "INNER JOIN SchadenZuweisung d ON d.ArtikelID=a.ID " +
                            "WHERE " +
                            "(b.Date between '" + dtVon.Date.ToShortDateString() + "' AND '" + dtBis.Date.AddDays(1).ToShortDateString() + "') ";
                break;
            //ohne Schaden
            case clsJournal.const_Journalart_KomplettOhneSchaden:
                strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                            ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            " From Artikel a " +
                            "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                            "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                            "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                            "INNER JOIN Abrufe f on f.ArtikelID = a.ID " +
                            "WHERE " +
                            "(b.Date between '" + dtVon.Date.ToShortDateString() + "' AND '" + dtBis.Date.AddDays(1).ToShortDateString() + "') " +
                            "AND a.ID NOT IN (Select DISTINCT ArtikelID FROM SchadenZuweisung) ";
                break;
            //Sperrlager
            case clsJournal.const_Journalart_SPL:
                //strSQL2 = GetSperrlagerSQL(true);
                break;

            case const_Journalart_Abrufe:
                strSql = GetMainJournalAbrufe();
                strSQL2 = " From Artikel a " +
                          "INNER JOIN Abrufe g on g.ArtikelID=a.ID " +
                          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                          "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                          "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID ";
                if (!myClsCompany.CompanyGroup.IsLieferant)
                {
                    strSQL2 += "INNER JOIN Abrufe f on f.ArtikelID = a.ID ";
                }
                strSQL2 += "where " +
                               "g.Aktion='Abruf' " +
                               "AND g.IsCreated=1 " +
                               "AND f.Aktion='" + clsAbruf.const_AbrufAktion_Abruf.ToString() + "' " +
                               "AND f.IsCreated =1 " +
                               "AND g.CompanyID IN(" + myClsCompany.ID.ToString() + ")" +
                               "AND (CAST(g.EintreffDatum as Date) between '" + dtVon.Date.ToShortDateString() + "' AND '" + dtBis.Date.ToShortDateString() + "') " +
                            "ORDER BY g.EintreffDatum, g.EintreffZeit";
                break;

            case "":
                break;
        }

        if (strSQL2 != string.Empty)
        {
            strSql = strSql + strSQL2;

            this.dtJournal = this.SQLconLVS.ExecuteSQL_GetDataTable(strSql, "Journal");
            InitComboLieferant(ref this.dtJournal);
        }
    }
    ///<summary>clsCallJournal / InitComboLieferant</summary>
    ///<remarks>.</remarks>
    private void InitComboLieferant(ref DataTable dt)
    {
        dtLieferanten = new DataTable();
        //List<string> listLieferanten = new List<string>();

        dtLieferanten.Columns.Add("Auftraggeber", typeof(string));
        if (dt.Rows.Count > 0)
        {
            dtLieferanten = dt.DefaultView.ToTable(true, "Auftraggeber");
        }
        DataRow row = dtLieferanten.NewRow();
        row["Auftraggeber"] = const_ComboLieferant_FirstItem;
        dtLieferanten.Rows.InsertAt(row, 0);
    }
    ///<summary>clsCallJournal / GetMainJournalAbrufe</summary>
    ///<remarks>.</remarks>
    private string GetMainJournalAbrufe()
    {
        string strSQL = string.Empty;
        string strSql = string.Empty;
        strSql = "Select ";
        strSql = strSql + "CAST(a.ID as INT) as ArtikelID " +
                   ", CAST(a.LVS_ID as INT) as LVSNr " +
                   ", g.ID as AbrufID" +
                   ", g.Datum as Abrufdatum" +
                   ", g.EintreffDatum as Eintreffdatum" +
                   ", g.EintreffZeit as Eintreffzeit" +
                   ", g.Benutzername as Benutzer" +
                   ", g.Schicht " +
                   ", g.Abladestelle" +
                   ", g.Referenz " +
                   ", g.Aktion as Abrufaktion" +
                   ", a.Werksnummer" +
                   ", a.Produktionsnummer" +
                   ", a.Charge" +
                    ", a.LVS_ID as LVSNr" +
                   ", e.Bezeichnung as Gut" +
                   ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
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
                   ", f.[Status] as CallStatus";
        return strSql;
    }
}
