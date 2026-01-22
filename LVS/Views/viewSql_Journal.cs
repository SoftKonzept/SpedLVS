namespace LVS.Views
{
    public class viewSql_Journal
    {
        public const string const_Customized_NotSet = "-Gruppierung wählen-";
        public const string const_Customized_EingangCharge = "Eingang|TransportReferenz";

        /// <summary>
        ///             SQL-Main Standardabfrage
        /// </summary>
        /// <param name="myJournalArtID"></param>
        /// <param name="myBestandBis"></param>
        /// <returns></returns>
        public static string GetDefaultMainSQL(int myJournalArtID, clsLager myLager)
        {
            string strReturn = string.Empty;
            strReturn = "Select ";
            if (myJournalArtID == 3)
                strReturn = strReturn + "distinct ";
            strReturn = strReturn +
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
                            ", b.Date as 'Eingangsdatum'" +
                            ", b.LfsNr as Lieferschein" +
                            ", CAST(c.LAusgangID as INT) as Ausgang" +
                            ", c.Datum as 'Ausgangsdatum'" +
                            ", CASE " +
                                   "WHEN a.LAusgangTableID>0 THEN CAST( DATEDIFF(day, CAST(b.Date as Date), CAST(c.Datum as Date)) as INT)+1 " +
                                   "ELSE CAST( DATEDIFF(day, CAST(b.Date as Date), '" + myLager.BestandBis + "') as INT)+1 " +
                                   "END as Lagerdauer " +
                            ", Case " +
                                "WHEN (a.Werk<>'') AND (a.Halle<>'') AND (a.Reihe<>'') AND (a.Ebene<>'') AND (a.Platz<>'') THEN a.Werk+' | ' +a.Halle+' | '+a.Reihe+' | '+a.Ebene+' | '+a.Platz " +
                                "WHEN (a.Werk<>'') AND (a.Halle<>'') AND (a.Reihe<>'') AND (a.Ebene<>'') AND (a.Platz='') THEN a.Werk+' | ' +a.Halle+' | '+a.Reihe+' | '+a.Ebene " +
                                "WHEN (a.Werk<>'') AND (a.Halle<>'') AND (a.Reihe<>'') AND (a.Ebene='') AND (a.Platz='') THEN a.Werk+' | ' +a.Halle+' | '+a.Reihe " +
                                "WHEN (a.Werk<>'') AND (a.Halle<>'') AND (a.Reihe='') AND (a.Ebene='') AND (a.Platz='') THEN a.Werk+' | ' +a.Halle " +
                                "WHEN (a.Werk<>'') AND (a.Halle='') AND (a.Reihe='') AND (a.Ebene='') AND (a.Platz='') THEN a.Werk " +
                                "END as Lagerort " +
                            ", c.Info as Ausgangsinfo " +
                            ", a.Werk" +
                            ", a.Halle" +
                            ", a.Reihe" +
                            ", a.Ebene" +
                            ", a.Platz" +
                            ", a.exInfo as Bemerkung" +
                            ", a.BKZ" +
                            ", b.WaggonNo as Waggon_IN" +
                            ", b.KFZ as KFZ_IN " +
                            ", a.FreigabeAbruf as Freigabe " +
                            ", CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) as LZZ" +
                            ", e.ID as GueterartID" +
                            ", a.ArtIDRef" +
                            ", c.LfsNr  as Ausgangslieferschein " +
                            ", c.SLB" +
                            ", c.WaggonNo as Waggon_OUT " +
                            ", c.KFZ as KFZ_OUT " +
                            ", c.exTransportRef" +
                            ", a.TARef as TransportId" +
                            ", e.ArtikelArt as Art " +
                            ", " + clsArtikel.GetStatusColumnSQL("c", "b");

            return strReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myLager"></param>
        /// <returns></returns>
        public static string GetSQLFilterSelection(int myJournalArtID, clsLager myLager)
        {
            string strSQL2 = string.Empty;
            switch (myJournalArtID)
            {
                //Alle
                case 0:
                    strSQL2 = ", (Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                              ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            ", CASE " +
                                "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                "END as Empfaenger " +
                            ",CASE WHEN (SELECT COUNT (*) " +
                                    " FROM Artikel a1 " +
                                    " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                    " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                    " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                    " WHERE a1.ID=a.ID) > 0 " +
                                  " THEN (SELECT e2.Bezeichnung + char(10) " +
                                      " FROM Artikel a2 " +
                                      " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                      " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                      " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                      " WHERE a2.ID=a.ID " +
                                      " FOR XML PATH ('')) " +
                                  " ELSE '' " +
                                  " END as Schaden " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +

                              " From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                "WHERE " +
                                     "b.AbBereich=" + myLager.AbBereichID + " " +
                                    "AND (" +
                                        "(CAST(b.Date as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.AddDays(1).ToShortDateString() + "') " +
                                        " OR " +
                                        "(CAST(c.Datum as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.AddDays(1).ToShortDateString() + "') " +
                                        ") ";
                    //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
                    //Filterangaben zur SQL-Anweisung hinzufügen
                    if (myLager.bFilterJournal)
                    {
                        if (myLager.ADR.ID > 0)
                        {
                            strSQL2 = strSQL2 + " AND b.Auftraggeber=" + myLager.ADR.ID + " ";
                            if (myLager.ADR.Kunde.Tarif.ID > 0)
                            {
                                if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                                {
                                    if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                                    {
                                        strSQL2 = strSQL2 + " AND e.ID IN (" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                                    }
                                }
                            }
                        }
                    }

                    break;

                // nur Eingänge
                case 1:
                    strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                              ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            ", CASE " +
                                "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                "END as Empfaenger " +
                            ",CASE WHEN (SELECT COUNT (*) " +
                                    " FROM Artikel a1 " +
                                    " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                    " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                    " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                    " WHERE a1.ID=a.ID) > 0 " +
                                  " THEN (SELECT e2.Bezeichnung + char(10) " +
                                      " FROM Artikel a2 " +
                                      " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                      " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                      " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                      " WHERE a2.ID=a.ID " +
                                      " FOR XML PATH ('')) " +
                                  " ELSE '' " +
                                  " END as Schaden " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +


                              " From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE " +
                              "b.[Check]=1 AND b.DirectDelivery=0 AND b.AbBereich=" + myLager.AbBereichID + " " +
                             "AND (CAST(b.Date as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.ToShortDateString() + "') ";
                    //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
                    //Filterangaben zur SQL-Anweisung hinzufügen
                    if (myLager.bFilterJournal)
                    {
                        if (myLager.ADR.ID > 0)
                        {
                            strSQL2 += " AND b.Auftraggeber=" + myLager.ADR.ID + " ";
                            if (myLager.ADR.Kunde.Tarif.ID > 0)
                            {
                                if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                                {
                                    if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                                    {
                                        strSQL2 += " AND e.ID IN(" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                                    }
                                }
                            }
                        }
                    }
                    //--- RL incl
                    if (myLager.RLJournalExcl)
                    {
                        strSQL2 += " AND ( a.LAusgangTableID=0 OR c.IsRL=0 ) ";
                    }
                    //--- mit oder ohne Schäden
                    if (myLager.SchadenJournalExcl)
                    {
                        strSQL2 += " AND a.ID NOT IN (Select DISTINCT ArtikelID FROM SchadenZuweisung) ";
                    }
                    break;
                //nur Ausgänge
                case 2:
                    strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                              ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Auftraggeber) as Auftraggeber" +
                            ", CASE " +
                                "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                "END as Empfaenger " +
                            ",CASE WHEN (SELECT COUNT (*) " +
                                    " FROM Artikel a1 " +
                                    " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                    " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                    " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                    " WHERE a1.ID=a.ID) > 0 " +
                                  " THEN (SELECT e2.Bezeichnung + char(10) " +
                                      " FROM Artikel a2 " +
                                      " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                      " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                      " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                      " WHERE a2.ID=a.ID " +
                                      " FOR XML PATH ('')) " +
                                  " ELSE '' " +
                                  " END as Schaden " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +


                              " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                              "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE " +
                              "c.Checked=1 AND c.DirectDelivery=0 AND c.IsRL=0" +
                              " AND c.AbBereich=" + myLager.AbBereichID + " " +
                              // "AND (c.Datum between '" + BestandVon.Date.ToShortDateString() + "' AND '" + BestandBis.Date.AddDays(1).ToShortDateString() + "') ";
                              "AND (CAST(c.Datum as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.ToShortDateString() + "') ";

                    //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
                    //Filterangaben zur SQL-Anweisung hinzufügen
                    if (myLager.bFilterJournal)
                    {
                        if (myLager.ADR.ID > 0)
                        {
                            strSQL2 = strSQL2 + " AND c.Auftraggeber=" + myLager.ADR.ID + " ";

                            if (myLager.ADR.Kunde.Tarif.ID > 0)
                            {
                                if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                                {
                                    if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                                    {
                                        strSQL2 = strSQL2 + " AND e.ID IN(" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                                    }
                                }
                            }
                        }
                    }
                    break;
                //mit Schaden
                case 3:
                    strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                              ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            ", CASE " +
                                "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                "END as Empfaenger " +
                            ",CASE WHEN (SELECT COUNT (*) " +
                                    " FROM Artikel a1 " +
                                    " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                    " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                    " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                    " WHERE a1.ID=a.ID) > 0 " +
                                  " THEN (SELECT e2.Bezeichnung + char(10) " +
                                      " FROM Artikel a2 " +
                                      " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                      " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                      " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                      " WHERE a2.ID=a.ID " +
                                      " FOR XML PATH ('')) " +
                                  " ELSE '' " +
                                  " END as Schaden " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +

                              " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "INNER JOIN SchadenZuweisung d ON d.ArtikelID=a.ID " +
                              "WHERE " +
                              "(CAST(b.Date as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.ToShortDateString() + "') " +
                    " AND b.AbBereich=" + myLager.AbBereichID + " ";
                    //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
                    //Filterangaben zur SQL-Anweisung hinzufügen
                    if (myLager.bFilterJournal)
                    {
                        if (myLager.ADR.ID > 0)
                        {
                            strSQL2 = strSQL2 + " AND b.Auftraggeber=" + myLager.ADR.ID + " ";
                            if (myLager.ADR.Kunde.Tarif.ID > 0)
                            {
                                if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                                {
                                    if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                                    {
                                        strSQL2 = strSQL2 + " AND e.ID IN(" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                                    }
                                }
                            }
                        }
                    }
                    strSQL2 += " And b.[Check]=1 ";
                    break;
                //ohne Schaden
                case 4:
                    strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                              ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                                ", CASE " +
                                    "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                    "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                    "END as Empfaenger " +
                            ",CASE WHEN (SELECT COUNT (*) " +
                                    " FROM Artikel a1 " +
                                    " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                    " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                    " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                    " WHERE a1.ID=a.ID) > 0 " +
                                  " THEN (SELECT e2.Bezeichnung + char(10) " +
                                      " FROM Artikel a2 " +
                                      " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                      " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                      " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                      " WHERE a2.ID=a.ID " +
                                      " FOR XML PATH ('')) " +
                                  " ELSE '' " +
                                  " END as Schaden " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                            ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +

                              " From Artikel a " +
                              "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                              "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                              "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                              "WHERE " +
                              //"(b.Date between '" + BestandVon.Date.ToShortDateString() + "' AND '" + BestandBis.Date.AddDays(1).ToShortDateString() + "') " +
                              "(CAST(b.Date as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.ToShortDateString() + "') " +
                              " AND b.AbBereich=" + myLager.AbBereichID + " " +
                              " AND a.ID NOT IN (Select DISTINCT ArtikelID FROM SchadenZuweisung) ";
                    //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
                    //Filterangaben zur SQL-Anweisung hinzufügen
                    if (myLager.bFilterJournal)
                    {
                        if (myLager.ADR.ID > 0)
                        {
                            strSQL2 = strSQL2 + " AND b.Auftraggeber=" + myLager.ADR.ID + " ";
                            if (myLager.ADR.Kunde.Tarif.ID > 0)
                            {
                                if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                                {
                                    if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                                    {
                                        strSQL2 = strSQL2 + " AND e.ID IN(" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                                    }
                                }
                            }
                        }
                    }
                    strSQL2 += " And b.[Check]=1 ";
                    break;
                //Sperrlager
                case 5:
                    strSQL2 = viewSql_Journal.SQLGetJournalSPL(myLager);
                    break;
                //Rücklieferungen
                case 6:
                    strSQL2 = strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                                        ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                                        ", CASE " +
                                          "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                          "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                          "END as Empfaenger " +
                                        ",CASE WHEN (SELECT COUNT (*) " +
                                                " FROM Artikel a1 " +
                                                " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                                " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                                " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                                " WHERE a1.ID=a.ID) > 0 " +
                                              " THEN (SELECT e2.Bezeichnung + char(10) " +
                                                  " FROM Artikel a2 " +
                                                  " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                                  " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                                  " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                                  " WHERE a2.ID=a.ID " +
                                                  " FOR XML PATH ('')) " +
                                              " ELSE '' " +
                                              " END as Schaden " +
                                        ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                                        ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +

                                        " From Artikel a " +
                                        "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                        "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                        "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                        " WHERE " +
                                        "(CAST(c.Datum as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.ToShortDateString() + "') " +
                                        " AND b.AbBereich=" + myLager.AbBereichID + " " +
                                        "AND c.IsRL=1 ";
                    break;

                //Direktanlieferung
                case 7:
                    strSQL2 = strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                                        ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                                        ", CASE " +
                                          "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                          "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                          "END as Empfaenger " +
                                        ",CASE WHEN (SELECT COUNT (*) " +
                                                " FROM Artikel a1 " +
                                                " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                                " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                                " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                                " WHERE a1.ID=a.ID) > 0 " +
                                              " THEN (SELECT e2.Bezeichnung + char(10) " +
                                                  " FROM Artikel a2 " +
                                                  " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                                  " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                                  " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                                  " WHERE a2.ID=a.ID " +
                                                  " FOR XML PATH ('')) " +
                                              " ELSE '' " +
                                              " END as Schaden " +
                                        ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                                        ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +

                                        " From Artikel a " +
                                        "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                        "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                        "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                        " WHERE " +
                                        "(CAST(b.Date as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.ToShortDateString() + "') " +
                                        " AND b.AbBereich=" + myLager.AbBereichID + " " +
                                        "AND b.DirectDelivery=1 ";
                    //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
                    //Filterangaben zur SQL-Anweisung hinzufügen
                    if (myLager.bFilterJournal)
                    {
                        if (myLager.ADR.Kunde.Tarif.ID > 0)
                        {
                            strSQL2 = strSQL2 + " AND " +
                                                "b.Auftraggeber=" + myLager.ADR.ID + " ";
                            if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                            {
                                if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                                {
                                    strSQL2 = strSQL2 + " AND e.ID IN(" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                                }
                            }
                        }
                    }
                    break;

                //Lagertransporte IN
                case 8:
                    strSQL2 = strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=b.Mandant) as Mandant " +
                                        ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                                        ", CASE " +
                                          "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                          "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                          "END as Empfaenger " +
                                          ",CASE WHEN (SELECT COUNT (*) " +
                                                " FROM Artikel a1 " +
                                                " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                                " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                                " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                                " WHERE a1.ID=a.ID) > 0 " +
                                              " THEN (SELECT e2.Bezeichnung + char(10) " +
                                                  " FROM Artikel a2 " +
                                                  " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                                  " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                                  " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                                  " WHERE a2.ID=a.ID " +
                                                  " FOR XML PATH ('')) " +
                                              " ELSE '' " +
                                              " END as Schaden " +
                                        ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                                        ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +



                                        " From Artikel a " +
                                        "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                        "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                        "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                        " WHERE " +
                                        "(CAST(b.Date as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.ToShortDateString() + "') " +
                                        "AND b.LagerTransport=1 " +
                                        "AND b.AbBereich=" + myLager.AbBereichID + " " +
                                        "AND b.[Check]=1 ";
                    //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
                    //Filterangaben zur SQL-Anweisung hinzufügen
                    if (myLager.bFilterJournal)
                    {
                        if (myLager.ADR.ID > 0)
                        {
                            strSQL2 = strSQL2 + " AND b.Auftraggeber=" + myLager.ADR.ID + " ";
                            if (myLager.ADR.Kunde.Tarif.ID > 0)
                            {
                                if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                                {
                                    if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                                    {
                                        strSQL2 = strSQL2 + " AND e.ID IN(" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                                    }
                                }
                            }
                        }
                    }
                    break;
                //Lagertransporte OUT
                case 9:
                    strSQL2 = strSQL2 = ",(Select x.Matchcode FROM Mandanten x WHERE x.ID=c.MandantenID) as Mandant " +
                                        ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                                        ", CASE " +
                                          "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                          "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                          "END as Empfaenger " +
                                         ",CASE WHEN (SELECT COUNT (*) " +
                                                " FROM Artikel a1 " +
                                                " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                                                " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                                                " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                                                " WHERE a1.ID=a.ID) > 0 " +
                                              " THEN (SELECT e2.Bezeichnung + char(10) " +
                                                  " FROM Artikel a2 " +
                                                  " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                                                  " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                                                  " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                                                  " WHERE a2.ID=a.ID " +
                                                  " FOR XML PATH ('')) " +
                                              " ELSE '' " +
                                              " END as Schaden " +
                                        ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'IN' AND s.ArtikelID = a.ID) as SPL_IN " +
                                        ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ = 'OUT' AND s.ArtikelID = a.ID) as SPL_OUT " +


                                        " From Artikel a " +
                                        "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                        "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                        "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                        " WHERE " +
                                        "(CAST(c.Datum as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.ToShortDateString() + "') " +
                                        "AND c.LagerTransport=1 " +
                                        "AND c.AbBereich=" + myLager.AbBereichID + " " +
                                        "AND c.Checked=1 ";
                    //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
                    //Filterangaben zur SQL-Anweisung hinzufügen
                    if (myLager.bFilterJournal)
                    {
                        if (myLager.ADR.ID > 0)
                        {
                            strSQL2 = strSQL2 + " AND b.Auftraggeber=" + myLager.ADR.ID + " ";
                            if (myLager.ADR.Kunde.Tarif.ID > 0)
                            {
                                if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                                {
                                    if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                                    {
                                        strSQL2 = strSQL2 + " AND e.ID IN(" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            return strSQL2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string SQLGetJournalSPL(clsLager myLager)
        {
            string strSql = string.Empty;
            strSql += ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber " +
                      ", spl.Datum as SPL_IN " +
                      ",(Select Top(1) CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID) as SPL_OUT ";


            strSql += " From Artikel a " +
                      "INNER JOIN Sperrlager spl on spl.ArtikelID = a.ID " +
                      "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                      "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                      "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                      "WHERE " +
                           " ( " +
                               //--- Bsp: 1 -> IN vor dem Zeitraum und Out im Zeitraum 
                               "(Datediff(dd,'" + myLager.BestandVon.Date.ToShortDateString() + "',spl.Datum)<0) AND spl.BKZ='IN' " +
                               "AND " +
                               "(" +
                                    "(Datediff(dd,(Select CAST(s.Datum as DATE) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),'" + myLager.BestandBis.Date.ToShortDateString() + "')<0) " +
                                ")" +
                            ")" +
                          " OR " +
                            "(" +
                               // --    Bsp: 2 -> IN und OUT im Zeitraum
                               "(Datediff(dd,'" + myLager.BestandVon.Date.ToShortDateString() + "',spl.Datum)>=0) AND spl.BKZ='IN' " +
                               " AND " +
                               "(Datediff(dd,(Select CAST(s.Datum as DATE) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),'" + myLager.BestandBis.Date.ToShortDateString() + "')>=0) " +
                            ")" +
                            " OR " +
                           "(" +
                               //-- Bsp: 3 -> IN im Zeitraum und Out nach dem Zeitraum
                               "(Datediff(dd,'" + myLager.BestandVon.Date.ToShortDateString() + "',spl.Datum)>=0) AND spl.BKZ='IN' " +
                               " AND " +
                               "(" +
                                   "(Datediff(dd,(Select CAST(s.Datum as DATE) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),'" + myLager.BestandBis.Date.ToShortDateString() + "')>0) " +
                                   " OR " +
                                   " ISNULL((Select s.ID FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),0)=0 " +
                                ")" +
                           ")" +
                           " OR " +
                           "( " +
                               //--- Bsp: 4 -> IN und OUT liegen nicht im Zeitraum
                               "(Datediff(dd,'" + myLager.BestandVon.Date.ToShortDateString() + "',spl.Datum)<0) AND spl.BKZ='IN' " +
                               " AND " +
                               "(" +
                                   " (Datediff(dd,'" + myLager.BestandBis.Date.ToShortDateString() + "',(Select CAST(s.Datum as DATE) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID))>0) " +
                                   " OR " +
                                   "ISNULL((Select s.ID FROM Sperrlager s WHERE s.BKZ='OUT' AND s.SPLIDIn=spl.ID),0)=0 " +
                               ")" +
                           ")";
            return strSql;
        }
        /// <summary>
        ///             Gruppierung Customized für SLE (Frau Langheld) vom 27.09.2019
        ///             mit der folgenden View
        ///             TARef	Eingang	Eingangsdatum	Waggon/KFZ_IN	Anzahl	Brutto
        /// </summary>
        /// <param name="myJournalArtID"></param>
        /// <param name="myBestandBis"></param>
        /// <returns></returns>
        public static string GetCustomizedTransportRefEingang(int myJournalArtID, clsLager myLager)
        {
            string strReturn = string.Empty;
            strReturn = "Select " +
                            " CAST(b.LEingangID as INT) as Eingang " +
                            ", CAST(b.Date as Date) as 'Eingangsdatum' " +
                            ", a.TARef as TransportId " +
                            ", b.WaggonNo as Waggon_IN " +
                            ", SUM(a.Anzahl) as Anzahl " +
                            ", SUM(a.Brutto) as Brutto " +

                              " From Artikel a " +
                                "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                                "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                                "WHERE " +
                                    "b.[Check]=1 AND b.DirectDelivery=0 AND b.AbBereich=" + myLager.AbBereichID + " " +
                                    "AND (" +
                                        "(CAST(b.Date as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.AddDays(1).ToShortDateString() + "') " +
                                        " OR " +
                                        "(CAST(c.Datum as Date) between '" + myLager.BestandVon.Date.ToShortDateString() + "' AND '" + myLager.BestandBis.Date.AddDays(1).ToShortDateString() + "') " +
                                        ") ";
            //ermitteln, ob der FIlter im Journal gesetzt wurde und dann entsprechend die 
            //Filterangaben zur SQL-Anweisung hinzufügen
            if (myLager.bFilterJournal)
            {
                if (myLager.ADR.ID > 0)
                {
                    strReturn = strReturn + " AND b.Auftraggeber=" + myLager.ADR.ID + " ";
                    if (myLager.ADR.Kunde.Tarif.ID > 0)
                    {
                        if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != null)
                        {
                            if (myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                            {
                                strReturn = strReturn + " AND e.ID IN (" + myLager.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ")";
                            }
                        }
                    }
                }
            }

            strReturn = strReturn + " GROUP BY b.LEingangID, b.Date,a.TARef, b.WaggonNo";
            return strReturn;
        }
    }
}
