using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks
    {
        /// <summary>
        /// 
        /// </summary>
        //public string sql_Statement
        //{
        //    get
        //    {
        //        string strSql = string.Empty;
        //        strSql += sql_Main;
        //        strSql += sql_MainSelection;
        //        return strSql;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        private string _sql_Statement = string.Empty;
        public string sql_Statement
        {
            get
            {
                return _sql_Statement;
            }
            set
            {
                _sql_Statement = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _sql_MainSelection = string.Empty;
        public string sql_MainSelection
        {
            get
            {
                return _sql_MainSelection;
            }
            set
            {
                _sql_MainSelection = value;
            }
        }

        public string BestandsArt { get; set; } = string.Empty;

        public DateTime dtDeadLine { get; set; } = new DateTime(1900, 1, 1);
        public sqlCreater_Stocks(string strMyBestandArt,
                                int myWorkspaceId,
                                int myStockAdrId,
                                decimal myGArtID,
                                DateTime myDateDeadline,
                                DateTime myDateFrom,
                                DateTime myDateTo,
                                string mySqlGoodsTypeIdString,
                                int myFreeStorageDays,
                                bool bFilterJournal = true,
                                bool bUseBKZ = true,
                                bool mySysModulStockDailyStockExclSPL = true
                                )
        {
            BestandsArt = strMyBestandArt;
            dtDeadLine = myDateDeadline;
            string strSqlMain = sql_Main;
            sql_MainSelection = string.Empty;
            switch (strMyBestandArt)
            {
                //Default
                case clsLager.const_Bestandsart_Select:
                    sqlCreater_Stocks_Select sqlCreater_Select = new sqlCreater_Stocks_Select(myWorkspaceId, myStockAdrId, (int)myGArtID, mySqlGoodsTypeIdString, bFilterJournal, bUseBKZ);
                    sql_MainSelection = sqlCreater_Select.sql_Statement;
                    break;

                //Inventur ist gleich Tagesbestand incl. SPL
                case clsLager.const_Bestandsart_Inventur:
                    //case "Inventur":
                    sqlCreater_Stocks_Inventory sqlCreater_Inventory = new sqlCreater_Stocks_Inventory(myWorkspaceId, myStockAdrId, (int)myGArtID, bFilterJournal, bUseBKZ);
                    sql_MainSelection = sqlCreater_Inventory.sql_Statement;
                    break;

                //Tagesbestand
                /**********************************************************************************************************
                 * Die Abfrage ist in zwei Filter aufgebaut:
                 * 1. Alle Eingänge vor dem Starpunkt des Beobachtungszeitraums. Diese können folgende Merkmale aufweisen:
                 *  - Artikel befindet sich auch zum Zeitpunkt der Abfrage noch im Lager
                 *  - Auslagerung des Artikels hat im Zeitraum zwischen Stichtag und Zeitpunkt Abfrage stattgefunden
                 *  *******************************************************************************************************/
                case clsLager.const_Bestandsart_Tagesbestand:
                    sqlCreater_Stocks_DailyStock sqlCreater_DailyStock = new sqlCreater_Stocks_DailyStock(myWorkspaceId, myStockAdrId, (int)myGArtID, mySqlGoodsTypeIdString, myDateDeadline, myDateFrom, myDateTo, bFilterJournal, bUseBKZ, mySysModulStockDailyStockExclSPL);
                    sql_MainSelection = sqlCreater_DailyStock.sql_Statement;

                    ////case "Tagesbestand":
                    //strSql2 = " From Artikel a " +
                    //          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //          "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                    //          "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //          "WHERE ";

                    //strSql2 += " b.AbBereich=" + myWorkspaceId + " AND ";

                    //strSql2 += "(( " +
                    //                "b.Auftraggeber=" + myStockAdrId + " ";
                    //if (bUseBKZ)
                    //{
                    //    strSql2 += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    //}
                    //else
                    //{
                    //    strSql2 += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    //}
                    ////"AND b.Mandant=" + MandantenID + " " +
                    //strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + myWorkspaceId + " " +
                    //          "AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
                    ////"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                    //if (bFilterJournal)
                    //{
                    //    if (adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                    //    {
                    //        strSql2 = strSql2 + " AND a.GArtID IN (" + adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                    //    }
                    //}
                    //else
                    //{
                    //    if (myGArtID > 0)
                    //    {
                    //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //    }
                    //}
                    //strSql2 = strSql2 +
                    //") " +
                    //"OR " +
                    //"(" +
                    //      "b.Auftraggeber=" + myStockAdrId + " ";
                    //if (bUseBKZ)
                    //{
                    //    strSql2 += " AND a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
                    //}
                    //else
                    //{
                    //    if (myGArtID > 0)
                    //    {
                    //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //    }
                    //}
                    ////"AND b.Mandant=" + MandantenID + " " +
                    //strSql2 += " AND b.DirectDelivery=0 AND b.AbBereich=" + myWorkspaceId + " " +
                    //           " AND c.Datum>='" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
                    //           " AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
                    //if (bFilterJournal)
                    //{
                    //    if (this.ADR.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                    //    {
                    //        strSql2 = strSql2 + " AND a.GArtID IN (" + adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                    //    }
                    //}
                    //else
                    //{
                    //    if (myGArtID > 0)
                    //    {
                    //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //    }
                    //}
                    //strSql2 = strSql2 +
                    //"))";
                    ////ohne sPL
                    //if (this.sys != null)
                    //{
                    //    if (this.sys.Client.Modul.Lager_Bestandsliste_TagesbestandOhneSPL)
                    //    {
                    //        strSql2 = strSql2 + " AND a.ID NOT IN (" +
                    //                                               "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                    //                                                     "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                    //                                             ");";
                    //    }
                    //}
                    break;

                //---nach Empfänger
                case clsLager.const_Bestandsart_TagesbestandEmp:
                    sqlCreater_Stocks_DailyStockReceiver sqlCreater_DailyStockReceiver = new sqlCreater_Stocks_DailyStockReceiver(myWorkspaceId, myStockAdrId, (int)myGArtID, myDateFrom, myDateTo, mySqlGoodsTypeIdString, bFilterJournal, bUseBKZ, mySysModulStockDailyStockExclSPL);
                    sql_MainSelection = sqlCreater_DailyStockReceiver.sql_Statement;

                    //strSql2 = " From Artikel a " +
                    //          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //          "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                    //          "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //          "WHERE ";

                    //strSql2 += " b.AbBereich=" + myWorkspaceId + " AND ";

                    //strSql2 += "(( " +
                    //                "b.Empfaenger=" + myStockAdrId + " ";
                    //if (bUseBKZ)
                    //{
                    //    strSql2 += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    //}
                    //else
                    //{
                    //    strSql2 += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    //}
                    ////"AND b.Mandant=" + MandantenID + " " +
                    //strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + myWorkspaceId + " " +
                    //          "AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
                    ////"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                    //if (bFilterJournal)
                    //{
                    //    if (adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                    //    {
                    //        strSql2 = strSql2 + " AND a.GArtID IN (" + adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                    //    }
                    //}
                    //else
                    //{
                    //    if (myGArtID > 0)
                    //    {
                    //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //    }
                    //}
                    //strSql2 = strSql2 +
                    //") " +
                    //"OR " +
                    //"(" +
                    //      "b.Empfaenger=" + myStockAdrId + " ";
                    //if (bUseBKZ)
                    //{
                    //    strSql2 += " AND a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
                    //}
                    //else
                    //{
                    //    if (myGArtID > 0)
                    //    {
                    //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //    }
                    //}
                    ////"AND b.Mandant=" + MandantenID + " " +
                    //strSql2 += " AND b.DirectDelivery=0 AND b.AbBereich=" + myWorkspaceId + " " +
                    //           " AND c.Datum>='" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
                    //           " AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
                    //if (bFilterJournal)
                    //{
                    //    if (adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                    //    {
                    //        strSql2 = strSql2 + " AND a.GArtID IN (" + adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                    //    }
                    //}
                    //else
                    //{
                    //    if (myGArtID > 0)
                    //    {
                    //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //    }
                    //}
                    //strSql2 = strSql2 +
                    //"))";
                    ////ohne sPL
                    //if (mySystem != null)
                    //{
                    //    if (mySystem.Client.Modul.Lager_Bestandsliste_TagesbestandOhneSPL)
                    //    {
                    //        strSql2 = strSql2 + " AND a.ID NOT IN (" +
                    //                                               "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                    //                                                     "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                    //                                             ");";
                    //    }
                    //}
                    break;

                case clsLager.const_Bestandsart_TagesbestandexSPL:
                    sqlCreater_Stocks_DailyStockExSPL sqlCreater_DailyStockExSpl = new sqlCreater_Stocks_DailyStockExSPL(myWorkspaceId, myStockAdrId, (int)myGArtID, myDateFrom, myDateTo, mySqlGoodsTypeIdString, bFilterJournal, bUseBKZ);
                    sql_MainSelection = sqlCreater_DailyStockExSpl.sql_Statement;

                    // //case "Tagesbestand [ohne SPL]":
                    // strSql2 = " From Artikel a " +
                    //           "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //           "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                    //           "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //           "WHERE ";

                    // // " b.AbBereich=" + AbBereichID + " AND " +
                    // strSql2 += " b.AbBereich=" + myWorkspaceId + " AND ";
                    // strSql2 += "(( " +
                    //                 "b.Auftraggeber=" + myStockAdrId + " ";
                    // if (bUseBKZ)
                    // {
                    //     strSql2 += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
                    // }
                    // else
                    // {
                    //     strSql2 += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
                    // }
                    // //"AND b.Mandant=" + MandantenID + " " +
                    // strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + myWorkspaceId + " " +
                    //           "AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
                    // //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
                    // if (bFilterJournal)
                    // {
                    //     if (adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                    //     {
                    //         strSql2 = strSql2 + " AND a.GArtID IN (" + adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                    //     }
                    // }
                    // else
                    // {
                    //     if (myGArtID > 0)
                    //     {
                    //         strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //     }
                    // }
                    // strSql2 = strSql2 +
                    // ") " +
                    // "OR " +
                    // "(" +
                    //       "b.Auftraggeber=" + myStockAdrId + " ";
                    // if (bUseBKZ)
                    // {
                    //     strSql2 += " AND a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
                    // }
                    // else
                    // {
                    //     if (myGArtID > 0)
                    //     {
                    //         strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //     }
                    // }
                    // //"AND b.Mandant=" + MandantenID + " " +
                    // strSql2 += " AND b.DirectDelivery=0 AND b.AbBereich=" + myWorkspaceId + " " +
                    //            " AND c.Datum>='" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
                    //            " AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
                    // if (bFilterJournal)
                    // {
                    //     if (adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                    //     {
                    //         strSql2 = strSql2 + " AND a.GArtID IN (" + adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                    //     }
                    // }
                    // else
                    // {
                    //     if (myGArtID > 0)
                    //     {
                    //         strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //     }
                    // }
                    // strSql2 = strSql2 +
                    // ")) " +
                    //" AND a.ID NOT IN (" +
                    //                     "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                    //                             "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                    //                     ");";
                    break;

                //Tagesbestand LAger komplett
                /**********************************************************************************************************
                 * Die Abfrage ist in zwei Filter aufgebaut:
                 * 1. Alle Eingänge vor dem Starpunkt des Beobachtungszeitraums. Diese können folgende Merkmale aufweisen:
                 *  - Artikel befindet sich auch zum Zeitpunkt der Abfrage noch im Lager
                 *  - Auslagerung des Artikels hat im Zeitraum zwischen Stichtag und Zeitpunkt Abfrage stattgefunden
                 *  *******************************************************************************************************/
                case clsLager.const_Bestandsart_TagesbestandAll:
                    sqlCreater_Stocks_DailyStockAll sqlCreater_DailyStockAll = new sqlCreater_Stocks_DailyStockAll(myWorkspaceId, myStockAdrId, (int)myGArtID, myDateFrom, myDateTo, bUseBKZ, mySysModulStockDailyStockExclSPL);
                    sql_MainSelection = sqlCreater_DailyStockAll.sql_Statement;
                    //strSql2 = SqlTagesbestandKomplett(myGArtID, bUseBKZ);
                    break;
                case clsLager.const_Bestandsart_TagesbestandAllExclDam:
                    sqlCreater_Stocks_DailyStockAll sqlCreater_DailyStockAllExclDam = new sqlCreater_Stocks_DailyStockAll(myWorkspaceId, myStockAdrId, (int)myGArtID, myDateFrom, myDateTo, bUseBKZ, mySysModulStockDailyStockExclSPL);
                    sql_MainSelection = sqlCreater_DailyStockAllExclDam.sql_Statement;
                    sql_MainSelection += " AND a.ID NOT IN (SELECT DISTINCT ArtikelID FROM SchadenZuweisung) ";

                    //strSql2 = SqlTagesbestandKomplett(myGArtID, bUseBKZ);
                    //strSql2 += " AND a.ID NOT IN (SELECT DISTINCT ArtikelID FROM SchadenZuweisung) ";
                    break;

                case clsLager.const_Bestandsart_TagesbestandAllExclSPL:
                    sqlCreater_Stocks_DailyStockAll sqlCreater_DailyStockAllExclSPL = new sqlCreater_Stocks_DailyStockAll(myWorkspaceId, myStockAdrId, (int)myGArtID, myDateFrom, myDateTo, bUseBKZ, mySysModulStockDailyStockExclSPL);
                    sql_MainSelection = sqlCreater_DailyStockAllExclSPL.sql_Statement;
                    sql_MainSelection += " AND a.ID NOT IN (" +
                                                    "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                            "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                    ") ";

                    //strSql2 = SqlTagesbestandKomplett(myGArtID, bUseBKZ);
                    //strSql2 += " AND a.ID NOT IN (" +
                    //                                "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                    //                                        "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                    //                                ") ";
                    break;

                case clsLager.const_Bestandsart_TagesbestandAllExclDamSPL:
                    sqlCreater_Stocks_DailyStockAll sqlCreater_DailyStockAllExclDamSPL = new sqlCreater_Stocks_DailyStockAll(myWorkspaceId, myStockAdrId, (int)myGArtID, myDateFrom, myDateTo, bUseBKZ, mySysModulStockDailyStockExclSPL);
                    sql_MainSelection = sqlCreater_DailyStockAllExclDamSPL.sql_Statement;
                    sql_MainSelection += " AND a.ID NOT IN (SELECT DISTINCT ArtikelID FROM SchadenZuweisung) ";
                    sql_MainSelection += " AND a.ID NOT IN (" +
                                                    "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                            "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                    ") ";

                    //strSql2 = SqlTagesbestandKomplett(myGArtID, bUseBKZ);
                    //strSql2 += " AND a.ID NOT IN (SELECT DISTINCT ArtikelID FROM SchadenZuweisung) ";
                    //strSql2 += " AND a.ID NOT IN (" +
                    //                                "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                    //                                        "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                    //                                ") ";
                    break;

                case clsLager.const_Bestandsart_TagesbestandAccrossAllWorkspaces:
                    sqlCreater_Stocks_DailyStockAcrossAllWorkspaces sqlCreater_DailyStockAllWs = new sqlCreater_Stocks_DailyStockAcrossAllWorkspaces(myStockAdrId, (int)myGArtID, mySqlGoodsTypeIdString, myDateFrom, myDateTo, bFilterJournal, bUseBKZ);
                    sql_MainSelection = sqlCreater_DailyStockAllWs.sql_Statement;
                    break;

                //Sperrlager - case "Sperrlager[SPL]":
                case clsLager.const_Bestandsart_SPL:
                    sqlCreater_Stocks_DailyStockSPL sqlCreater_SPL = new sqlCreater_Stocks_DailyStockSPL(myWorkspaceId, myStockAdrId, myDateFrom, myDateTo, bFilterJournal, bUseBKZ);
                    sql_MainSelection = sqlCreater_SPL.sql_Statement;

                    //strSql = string.Empty;
                    //strSql2 = GetSperrlagerSQL();
                    break;

                //Rücklieferungen - "Rücklieferungen[RL]":
                case clsLager.const_Bestandsart_RL:
                    sqlCreater_Stocks_RL sqlCreater_RL = new sqlCreater_Stocks_RL(myWorkspaceId, (int)myGArtID, myDateFrom, myDateTo);
                    sql_MainSelection = sqlCreater_RL.sql_Statement;

                    ////case "Rücklieferungen[RL]":
                    //strSql2 = " From Artikel a " +
                    //         "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //         "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                    //         "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //         "WHERE " +
                    //                //" b.Auftraggeber=" + BestandAdrID +
                    //                //"AND "+
                    //                " b.AbBereich=" + myWorkspaceId + " " +
                    //                "AND (c.Datum between '" + myDateFrom.Date.ToShortDateString() + "' AND '" + myDateTo.Date.AddDays(1).ToShortDateString() + "') " +
                    //                " AND a.CheckArt=1 " +
                    //                " AND c.IsRL=1 ";
                    //if (myGArtID > 0)
                    //{
                    //    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //}
                    break;

                //Direktanlieferungen
                case clsLager.const_Bestandsart_DirectDelivery:
                    sqlCreater_DirectDelivery sqlCreater_DirectDelivery = new sqlCreater_DirectDelivery(myWorkspaceId, myStockAdrId, (int)myGArtID, myDateFrom, myDateTo, mySqlGoodsTypeIdString, bFilterJournal, bUseBKZ);
                    sql_MainSelection = sqlCreater_DirectDelivery.sql_Statement;

                    ////case "Direktanlieferungen":
                    //strSql2 = " From Artikel a " +
                    //         "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //         "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                    //         "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //         "WHERE " +
                    //         // b.Date >= '" + BestandVon + "' AND b.Date <'" + BestandBis + "' " +
                    //         " (b.Date between '" + myDateFrom.Date.ToShortDateString() + "' AND '" + myDateTo.Date.AddDays(1).ToShortDateString() + "') " +
                    //         " AND b.Auftraggeber=" + myStockAdrId +
                    //         "  AND b.DirectDelivery=1 " +//"AND b.Mandant=" + MandantenID + " " +
                    //                " AND b.AbBereich=" + myWorkspaceId + " ";
                    //if (bFilterJournal)
                    //{
                    //    if (adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString != string.Empty)
                    //    {
                    //        strSql2 = strSql2 + // " AND b.Auftraggeber=" + BestandAdrID + " " +
                    //                           " AND a.GArtID IN (" + adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString + ") ";
                    //    }
                    //}
                    //else
                    //{
                    //    if (myGArtID > 0)
                    //    {
                    //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //    }
                    //}
                    break;

                //Ungeprüfte Artikel im Eingang
                case clsLager.const_Bestandsart_ArtikelUnchecked_StoreIN:
                    sqlCreater_Stocks_ArticleUnchecked_StoreIN sqlCreater_ArtUn_StoreIN = new sqlCreater_Stocks_ArticleUnchecked_StoreIN(myWorkspaceId, myStockAdrId, (int)myGArtID);
                    sql_MainSelection = sqlCreater_ArtUn_StoreIN.sql_Statement;

                    ////case "Ungeprüfte Artikel im Eingang":
                    //strSql2 = " From Artikel a " +
                    //         "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //         "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                    //         "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //         "WHERE " +
                    //            "a.CheckArt=0 AND a.LEingangTableID>0 " +
                    //                //"AND b.Mandant=" + MandantenID + " " +
                    //                " AND b.AbBereich=" + myStockAdrId + " ";
                    //if (myStockAdrId > 0)
                    //{
                    //    strSql2 += " AND b.Auftraggeber=" + myStockAdrId;
                    //}
                    //if (myGArtID > 0)
                    //{
                    //    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //}
                    break;

                //Ungeprüfte Artikel in Ausgang
                case clsLager.const_Bestandsart_ArtikelUnchecked_StoreOUT:
                    sqlCreater_Stocks_ArticleUnchecked_StoreOUT sqlCreater_ArtUn_StoreOut = new sqlCreater_Stocks_ArticleUnchecked_StoreOUT(myWorkspaceId, myStockAdrId, (int)myGArtID);
                    sql_MainSelection = sqlCreater_ArtUn_StoreOut.sql_Statement;

                    ////case "Ungeprüfte Artikel im Ausgang":
                    //strSql2 = " From Artikel a " +
                    //         "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //         "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                    //         "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //         "WHERE " +
                    //            "a.LA_Checked=0 AND a.LAusgangTableID>0 " +
                    //                " AND b.AbBereich=" + myWorkspaceId + " ";
                    //if (myStockAdrId > 0)
                    //{
                    //    strSql2 += " AND b.Auftraggeber=" + myStockAdrId;
                    //}
                    //if (myGArtID > 0)
                    //{
                    //    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //}
                    break;

                //Artikel in offenen Eingngen
                //case "Artikel in offenen Eingängen":
                case clsLager.const_Bestandsart_Artikel_UncheckedStoreIN:
                    sqlCreater_Article_UncheckedStoreIN sqlCreater_Art_UncheckedStoreIN = new sqlCreater_Article_UncheckedStoreIN(myWorkspaceId, myStockAdrId, (int)myGArtID);
                    sql_MainSelection = sqlCreater_Art_UncheckedStoreIN.sql_Statement;

                    //strSql2 = " From Artikel a " +
                    //         "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //         "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                    //         "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //         "WHERE " +
                    //            " b.[check]=0 AND a.LEingangTableID>0 " +
                    //            " AND b.AbBereich=" + myWorkspaceId + " ";
                    //if (myStockAdrId > 0)
                    //{
                    //    strSql2 += " AND b.Auftraggeber=" + myStockAdrId;
                    //}
                    //if (myGArtID > 0)
                    //{
                    //    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //}
                    break;

                //Ungeprüfte Artikel in Ausgang
                case clsLager.const_Bestandsart_Artikel_UncheckedStoreOUT:
                    sqlCreater_Article_UncheckedStoreOUT sqlCreater_Art_UnStoreOut = new sqlCreater_Article_UncheckedStoreOUT(myWorkspaceId, myStockAdrId, (int)myGArtID);
                    sql_MainSelection = sqlCreater_Art_UnStoreOut.sql_Statement;
                    ////case "Artikel in offenen Ausgängen":
                    //strSql2 = " From Artikel a " +
                    //         "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //         "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                    //         "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //         "WHERE " +
                    //            "c.checked=0 AND a.LAusgangTableID>0 " +
                    //            " AND b.AbBereich=" + myWorkspaceId + " ";
                    //if (myStockAdrId > 0)
                    //{
                    //    strSql2 += "AND b.Auftraggeber=" + myStockAdrId;
                    //}
                    //if (myGArtID > 0)
                    //{
                    //    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //}
                    break;

                //Nicht abgeschlossene Ein-/Ausgänge
                case clsLager.const_Bestandsart_StoreIN_Unchecked:
                    sqlCreater_Stocks_StoreIN_Unchecked sqlCreater_StoreIN_Un = new sqlCreater_Stocks_StoreIN_Unchecked(myWorkspaceId, myStockAdrId);
                    sql_MainSelection = sqlCreater_StoreIN_Un.sql_Statement;
                    strSqlMain = string.Empty;
                    ////case "Nicht abgeschlossene Eingänge":
                    //strSql = string.Empty;
                    //strSql2 = GetOffeneEingänge();
                    break;

                //Nicht abgeschlossene Ein-/Ausgänge
                //case "Nicht abgeschlossene Ausgänge":
                case clsLager.const_Bestandsart_StoreOUT_Unchecked:
                    sqlCreater_Stocks_StoreOUT_Unchecked sqlCreater_StoreOUT_Un = new sqlCreater_Stocks_StoreOUT_Unchecked(myWorkspaceId, myStockAdrId);
                    sql_MainSelection = sqlCreater_StoreOUT_Un.sql_Statement;
                    strSqlMain = string.Empty;
                    //strSql = string.Empty;
                    //strSql2 = GetOffeneAusgänge();
                    break;
                //Nicht platzierte Artikel
                case "Nicht platzierte Artikel":
                    sqlCreater_Stocks_ArticleNotPlaced sqlCreater_ArtNotPlaced = new sqlCreater_Stocks_ArticleNotPlaced(myWorkspaceId);
                    sql_MainSelection = sqlCreater_ArtNotPlaced.sql_Statement;
                    //strSql2 = " From Artikel a " +
                    //             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                    //             "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //             "WHERE " +
                    //                "a.BKZ=1 AND (a.LagerOrt=0 OR a.LagerOrt is Null) " +
                    //                "AND (a.LOTable='') " +
                    //                "AND a.LVSNr_ALTLvs=0 " +
                    //                "AND b.DirectDelivery=0 " +
                    //                "AND b.AbBereich=" + myWorkspaceId + " ";
                    break;

                case clsLager.const_Bestandsart_LagergeldTag:
                    sqlCreater_Stocks_StockChargePerDay sqlCreater_StockChargDay = new sqlCreater_Stocks_StockChargePerDay(myWorkspaceId, myStockAdrId, (int)myGArtID, myDateFrom, myDateTo, myFreeStorageDays);
                    sql_MainSelection = sqlCreater_StockChargDay.sql_Statement;

                    //strSql2 += ", CASE " +
                    //            "WHEN (c.Datum IS NULL) " +
                    //            "THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST('" + myDateFrom.Date.ToString() + "' as Date)) as INT)+1 - " + this.FreieLagertage + " " +
                    //            "ELSE CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as Date)) as INT)+1 - " + this.FreieLagertage + " " +
                    //            "END as AbrDauer ";
                    //strSql2 += " From Artikel a " +
                    //             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                    //             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                    //             "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                    //            " WHERE " +
                    //                "(" +
                    //                  "(" +
                    //                    " a.CheckArt = 1 AND b.[Check] = 1 and(c.Checked is Null or c.Checked = 0) " +
                    //                    " AND b.DirectDelivery = 0 AND b.AbBereich = " + myWorkspaceId + " ";
                    //if (myStockAdrId > 0)
                    //{
                    //    strSql2 += " AND b.Auftraggeber=" + myStockAdrId + " ";
                    //}
                    //if (myGArtID > 0)
                    //{
                    //    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //}
                    //strSql2 += /*" AND DATEDIFF(dd, b.Date, '" + this.BestandBis.ToShortDateString() + "') >= " + this.FreieLagertage + " " +*/
                    //           " AND " +
                    //                "(CASE " +
                    //                    "WHEN(c.Datum IS NULL) THEN(CAST(DATEDIFF(dd, b.Date, '" + myDateTo.ToShortDateString() + "') as INT) + 1) " +
                    //                    "ELSE (CAST(DATEDIFF(dd, b.Date, c.Datum) as INT) + 1) " +
                    //                    " END) >= " + this.FreieLagertage + " " +
                    //           " AND b.Date < '" + myDateFrom.ToShortDateString() + "' " +
                    //        ") OR ( " +
                    //             " a.CheckArt = 1 AND b.[Check] = 1 and c.Checked = 1 " +
                    //             " AND b.DirectDelivery = 0  AND b.AbBereich =" + myWorkspaceId + " ";
                    //if (myStockAdrId > 0)
                    //{
                    //    strSql2 += " AND b.Auftraggeber=" + myStockAdrId + " ";
                    //}
                    //if (myGArtID > 0)
                    //{
                    //    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                    //}

                    //strSql2 += " AND c.Datum >= '" + myDateFrom.ToShortDateString() + "' " +
                    //        //" AND DATEDIFF(dd, b.Date, '" + this.BestandBis.ToShortDateString() + "') >= " + this.FreieLagertage + " " +
                    //        " AND " +
                    //                "(CASE " +
                    //                    "WHEN(c.Datum IS NULL) THEN(CAST(DATEDIFF(dd, b.Date, '" +myDateTo.ToShortDateString() + "') as INT) + 1) " +
                    //                    "ELSE (CAST(DATEDIFF(dd, b.Date, c.Datum) as INT) + 1) " +
                    //                    " END) >= " + this.FreieLagertage + " " +
                    //        " AND b.Date < '" + myDateFrom.ToShortDateString() + "' " +
                    //")" +
                    //")" +
                    //" AND a.ID NOT IN(SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn > 0))";
                    break;
            }


            sql_Statement = strSqlMain;
            sql_Statement += sql_MainSelection;
        }

        /// <summary>
        /// 
        /// </summary>
        public string sql_Main
        {
            get
            {
                string sqlReturn = string.Empty;
                sqlReturn = "Select " +
                            "CAST(a.ID as INT) as ArtikelID " +
                            ", CAST(a.LVS_ID as INT) as LVSNr " +
                            ", a.Werksnummer" +
                            ", a.Produktionsnummer" +
                            ", a.Charge" +
                            ", a.GArtId" +
                            ", e.Bezeichnung as Gut" +
                            ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                            ", CASE " +
                                "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                                "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                                "END as Empfaenger" +
                            ", a.FreigabeAbruf as Freigabe" +
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
                            ", CAST(MONTH(c.Datum) as varchar)+'/'+CAST(YEAR(c.Datum) as varchar)  as Ausgangsmonat" +
                            ", CASE " +
                                "WHEN (c.Datum IS NULL) " +
                                //"THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.dtDeadLine.Date.ToString() + "' as Date)) as INT)+1 " +
                                "THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.dtDeadLine.Date.ToString() + "' as Date)) as INT)+1 " +
                                "ELSE CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as Date)) as INT)+1 " +
                                "END as Lagerdauer " +
                            ", CASE " +
                                "WHEN(a.LAusgangTableID > 0) " +
                                "THEN " +
                                    "CASE " +
                                        //"WHEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.Stichtag.Date.ToString() + "' as Date))> 0 " +
                                        //"THEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.Stichtag.Date.ToString() + "' as Date))+1 " +
                                        "WHEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.dtDeadLine.Date.ToString() + "' as Date))> 0 " +
                                        "THEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.dtDeadLine.Date.ToString() + "' as Date))+1 " +
                                        "ELSE DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as date))+1 " +
                                    "end " +
                                "ELSE " +
                                    //"DATEDIFF(day, CAST(b.Date as Date), CAST('" + this.Stichtag.Date.ToString() + "' as Date)) + 1 " +
                                    "DATEDIFF(day, CAST(b.Date as Date), CAST('" + this.dtDeadLine.Date.ToString() + "' as Date)) + 1 " +
                                "END as LagerdauerST " +
                         ", Case " +
                            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene<>'') AND (Platz<>'') THEN Werk+' | ' +Halle+' | '+Reihe+' | '+Ebene+' | '+Platz " +
                            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene<>'') AND (Platz='')THEN Werk+' | ' +Halle+' | '+Reihe+' | '+Ebene " +
                            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene='') AND (Platz='') THEN Werk+' | ' +Halle+' | '+Reihe " +
                            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe='') AND (Ebene='') AND (Platz='') THEN Werk+' | ' +Halle " +
                            "WHEN (Werk<>'') AND (Halle='') AND (Reihe='') AND (Ebene='') AND (Platz='')THEN Werk " +
                            "END as Lagerort " +
                        ",CASE " +
                             "WHEN EAAusgangAltLVS='0' " +
                             "THEN a.Info " +
                             "ELSE SUBSTRING(a.Info,1, PATINDEX('%- LVS-Ausgang:%', a.Info)) " +
                             "END as LargerortAltLvs " +
                        ", a.BKZ " +
                        ", b.DirectDelivery as DA" +
                        ", b.Retoure as RL" +
                        ", b.Vorfracht as VF" +
                        ", CASE " +
                            "WHEN b.LagerTransport IS NULL " +
                            "THEN CAST(0 as BIT) " +
                            "ELSE b.LagerTransport " +
                            "END as LT_Eingang" +
                        ", CASE " +
                            "WHEN c.LagerTransport IS NULL " +
                            "THEN CAST(0 as BIT) " +
                            "ELSE c.LagerTransport " +
                            "END as LT_Ausgang";
                sqlReturn +=                // 
                            ", a.Werk" +
                            ", a.Halle" +
                            ", a.Reihe" +
                            ", a.Ebene" +
                            ", a.Platz" +
                            ", a.exInfo as Bemerkung" +
                            ", a.intInfo " +
                            ", b.WaggonNo" +
                            ", CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) as LZZ" +
                            ", a.ArtIDRef" +
                            //",CASE WHEN (SELECT COUNT (*) " +
                            //    " FROM Artikel a1 " +
                            //    " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
                            //    " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
                            //    " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
                            //    " WHERE a1.ID=a.ID) > 0 " +
                            //  " THEN (SELECT e2.Bezeichnung + char(10) " +
                            //      " FROM Artikel a2 " +
                            //      " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
                            //      " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
                            //      " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
                            //      " WHERE a2.ID=a.ID " +
                            //      " FOR XML PATH ('')) " +
                            //  " ELSE '' " +
                            //  " END as Schaden " +
                            //",(Select CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ='IN' AND s.ArtikelID=a.ID) as SPL_IN " +
                            //",(Select CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.ArtikelID=a.ID) as SPL_OUT " +
                            " , CASE " +
                            " 	WHEN (a.Laenge>0) " +
                                    " THEN CAST(a.Dicke as varchar (20))+'x'+ CAST(a.Breite as varchar(20))+'x'+CAST(a.Laenge as varchar(20)) " +
                                " 	ELSE CAST(a.Dicke as varchar (20))+'x'+ CAST(a.Breite as varchar(20)) " +
                                " END as Abmessung " +
                                " ,(CASE WHEN IsVerpackt = 1 THEN 'verpackt' + char(10) ELSE '' END) + " +
                                " (CASE WHEN ((exInfo IS NOT NULL) AND (exInfo <> '')) THEN exInfo + char(10) ELSE '' END ) as Bemerkungen " +
                            ", " + clsArtikel.GetStatusColumnSQL("c", "b") + " " +
                            ", ' ' as iO " +
                            ", ' ' as neueReihe " +
                            ", a.EAEingangAltLVS " +
                            ", a.EAAusgangAltLVS " +
                            //", a.GlowDate as Glühdatum " +
                            ", CASE " +
                                  "when(a.GlowDate is null) then CAST('01.01.1900' as Date) " +
                                  "when(a.GlowDate = CAST('01.01.0001' as datetime2)) then CAST('01.01.1900' as Date) " +
                                  "else a.GlowDate end as Glühdatum " +
                            ", b.ID as LEingangTableID " +
                            ", c.ID as LAusgnangTableID ";

                return sqlReturn;
            }
        }

        public static string Sql_Main_Communicator()
        {
            string sqlReturn = string.Empty;
            sqlReturn = "Select " +
                        "CAST(a.ID as INT) as ArtikelID " +
                        ", CAST(a.LVS_ID as INT) as LVSNr " +
                        ", a.Werksnummer" +
                        ", a.Produktionsnummer" +
                        ", a.Charge" +
                        //", a.GArtId" +
                        //", e.Bezeichnung as Gut" +
                        ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber" +
                        ", CASE " +
                            "WHEN a.LAusgangTableID>0 THEN (Select ADR.ViewID FROM ADR WHERE ADR.ID=c.Empfaenger) " +
                            "ELSE (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Empfaenger) " +
                            "END as Empfaenger" +
                        //", a.FreigabeAbruf as Freigabe" +
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
                        ", b.LfsNr as Lieferschein";
            //            ", CAST(c.LAusgangID as INT) as Ausgang" +
            //            ", c.Datum as 'Ausgangsdatum'" +
            //            ", CAST(MONTH(c.Datum) as varchar)+'/'+CAST(YEAR(c.Datum) as varchar)  as Ausgangsmonat" +
            //            ", CASE " +
            //                "WHEN (c.Datum IS NULL) " +
            //                //"THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.dtDeadLine.Date.ToString() + "' as Date)) as INT)+1 " +
            //                "THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.dtDeadLine.Date.ToString() + "' as Date)) as INT)+1 " +
            //                "ELSE CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as Date)) as INT)+1 " +
            //                "END as Lagerdauer " +
            //            ", CASE " +
            //                "WHEN(a.LAusgangTableID > 0) " +
            //                "THEN " +
            //                    "CASE " +
            //                        //"WHEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.Stichtag.Date.ToString() + "' as Date))> 0 " +
            //                        //"THEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.Stichtag.Date.ToString() + "' as Date))+1 " +
            //                        "WHEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.dtDeadLine.Date.ToString() + "' as Date))> 0 " +
            //                        "THEN DATEDIFF(day, CAST(b.Date as Date),CAST('" + this.dtDeadLine.Date.ToString() + "' as Date))+1 " +
            //                        "ELSE DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as date))+1 " +
            //                    "end " +
            //                "ELSE " +
            //                    //"DATEDIFF(day, CAST(b.Date as Date), CAST('" + this.Stichtag.Date.ToString() + "' as Date)) + 1 " +
            //                    "DATEDIFF(day, CAST(b.Date as Date), CAST('" + this.dtDeadLine.Date.ToString() + "' as Date)) + 1 " +
            //                "END as LagerdauerST " +
            //         ", Case " +
            //            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene<>'') AND (Platz<>'') THEN Werk+' | ' +Halle+' | '+Reihe+' | '+Ebene+' | '+Platz " +
            //            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene<>'') AND (Platz='')THEN Werk+' | ' +Halle+' | '+Reihe+' | '+Ebene " +
            //            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe<>'') AND (Ebene='') AND (Platz='') THEN Werk+' | ' +Halle+' | '+Reihe " +
            //            "WHEN (Werk<>'') AND (Halle<>'') AND (Reihe='') AND (Ebene='') AND (Platz='') THEN Werk+' | ' +Halle " +
            //            "WHEN (Werk<>'') AND (Halle='') AND (Reihe='') AND (Ebene='') AND (Platz='')THEN Werk " +
            //            "END as Lagerort " +
            //        ",CASE " +
            //             "WHEN EAAusgangAltLVS='0' " +
            //             "THEN a.Info " +
            //             "ELSE SUBSTRING(a.Info,1, PATINDEX('%- LVS-Ausgang:%', a.Info)) " +
            //             "END as LargerortAltLvs " +
            //        ", a.BKZ " +
            //        ", b.DirectDelivery as DA" +
            //        ", b.Retoure as RL" +
            //        ", b.Vorfracht as VF" +
            //        ", CASE " +
            //            "WHEN b.LagerTransport IS NULL " +
            //            "THEN CAST(0 as BIT) " +
            //            "ELSE b.LagerTransport " +
            //            "END as LT_Eingang" +
            //        ", CASE " +
            //            "WHEN c.LagerTransport IS NULL " +
            //            "THEN CAST(0 as BIT) " +
            //            "ELSE c.LagerTransport " +
            //            "END as LT_Ausgang";
            //sqlReturn +=                // 
            //            ", a.Werk" +
            //            ", a.Halle" +
            //            ", a.Reihe" +
            //            ", a.Ebene" +
            //            ", a.Platz" +
            //            ", a.exInfo as Bemerkung" +
            //            ", a.intInfo " +
            //            ", b.WaggonNo" +
            //            ", CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) as LZZ" +
            //            ", a.ArtIDRef" +
            //            //",CASE WHEN (SELECT COUNT (*) " +
            //            //    " FROM Artikel a1 " +
            //            //    " INNER JOIN LEingang c1 ON c1.ID=a1.LEingangTableID " +
            //            //    " INNER JOIN SchadenZuweisung d1 ON d1.ArtikelID=a1.ID " +
            //            //    " INNER JOIN Schaeden e1 ON e1.ID=d1.SchadenID " +
            //            //    " WHERE a1.ID=a.ID) > 0 " +
            //            //  " THEN (SELECT e2.Bezeichnung + char(10) " +
            //            //      " FROM Artikel a2 " +
            //            //      " INNER JOIN LEingang c2 ON c2.ID=a2.LEingangTableID " +
            //            //      " LEFT OUTER JOIN SchadenZuweisung d2 ON d2.ArtikelID=a2.ID " +
            //            //      " LEFT OUTER JOIN Schaeden e2 ON e2.ID=d2.SchadenID " +
            //            //      " WHERE a2.ID=a.ID " +
            //            //      " FOR XML PATH ('')) " +
            //            //  " ELSE '' " +
            //            //  " END as Schaden " +
            //            //",(Select CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ='IN' AND s.ArtikelID=a.ID) as SPL_IN " +
            //            //",(Select CAST(s.Datum as datetime) FROM Sperrlager s WHERE s.BKZ='OUT' AND s.ArtikelID=a.ID) as SPL_OUT " +
            //            " , CASE " +
            //            " 	WHEN (a.Laenge>0) " +
            //                    " THEN CAST(a.Dicke as varchar (20))+'x'+ CAST(a.Breite as varchar(20))+'x'+CAST(a.Laenge as varchar(20)) " +
            //                " 	ELSE CAST(a.Dicke as varchar (20))+'x'+ CAST(a.Breite as varchar(20)) " +
            //                " END as Abmessung " +
            //                " ,(CASE WHEN IsVerpackt = 1 THEN 'verpackt' + char(10) ELSE '' END) + " +
            //                " (CASE WHEN ((exInfo IS NOT NULL) AND (exInfo <> '')) THEN exInfo + char(10) ELSE '' END ) as Bemerkungen " +
            //            ", " + clsArtikel.GetStatusColumnSQL("c", "b") + " " +
            //            ", ' ' as iO " +
            //            ", ' ' as neueReihe " +
            //            ", a.EAEingangAltLVS " +
            //            ", a.EAAusgangAltLVS " +
            //            ", a.GlowDate as Glühdatum " +
            //            ", b.ID as LEingangTableID " +
            //            ", c.ID as LAusgnangTableID ";

            return sqlReturn;
        }



    }
}
