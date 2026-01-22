using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_DailyStock
    {
        //Tagesbestand
        /**********************************************************************************************************
         * Die Abfrage ist in zwei Filter aufgebaut:
         * 1. Alle Eingänge vor dem Starpunkt des Beobachtungszeitraums. Diese können folgende Merkmale aufweisen:
         *  - Artikel befindet sich auch zum Zeitpunkt der Abfrage noch im Lager
         *  - Auslagerung des Artikels hat im Zeitraum zwischen Stichtag und Zeitpunkt Abfrage stattgefunden
         *  *******************************************************************************************************/

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
        ///              adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString
        /// </summary>
        internal string SqlGoodsTypeIdString { get; set; } = string.Empty;


        public sqlCreater_Stocks_DailyStock(int myWorkspaceId,
                                           int myStockAdrId,
                                           int myGArtID,
                                           string mySqlGoodsTypeIdString,
                                           DateTime myDateDeadline,
                                           DateTime myDateFrom,
                                           DateTime myDateTo,
                                           bool bFilterJournal = true,
                                           bool bUseBKZ = true,
                                           bool mySysModulStockDailyStockExclSPL = true
                                           )
        {
            SqlGoodsTypeIdString = mySqlGoodsTypeIdString;
            string strSql2 = string.Empty;

            strSql2 = " From Artikel a " +
                      "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                      "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                      "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                      "WHERE ";

            strSql2 += " b.AbBereich=" + myWorkspaceId + " AND ";
            strSql2 += "((b.Auftraggeber=" + myStockAdrId + " ";

            if (bUseBKZ)
            {
                strSql2 += " AND a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql2 += " AND a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
            }
            //"AND b.Mandant=" + MandantenID + " " +
            strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + myWorkspaceId + " " +
                       " AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
            //"AND b.Date <'" + BestandVon.Date.ToShortDateString() + "' " ;
            if (bFilterJournal)
            {
                if (SqlGoodsTypeIdString != string.Empty)
                {
                    strSql2 = strSql2 + " AND a.GArtID IN (" + SqlGoodsTypeIdString + ") ";
                }
            }
            else
            {
                if (myGArtID > 0)
                {
                    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                }
            }
            strSql2 = strSql2 +
                                ") " +
                                "OR " +
                                "(" +
                                    "b.Auftraggeber=" + myStockAdrId + " ";
            if (bUseBKZ)
            {
                strSql2 += " AND a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                if (myGArtID > 0)
                {
                    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                }
            }
            //"AND b.Mandant=" + MandantenID + " " +
            strSql2 += " AND b.DirectDelivery=0 AND b.AbBereich=" + myWorkspaceId + " " +
                       " AND c.Datum>='" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
                       " AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
            if (bFilterJournal)
            {
                if (SqlGoodsTypeIdString != string.Empty)
                {
                    strSql2 = strSql2 + " AND a.GArtID IN (" + SqlGoodsTypeIdString + ") ";
                }
            }
            else
            {
                if (myGArtID > 0)
                {
                    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                }
            }
            strSql2 = strSql2 +
                      "))";

            //ohne sPL
            if (mySysModulStockDailyStockExclSPL)
            {
                strSql2 = strSql2 + " AND a.ID NOT IN (" +
                                                       "SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                                                             "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)" +
                                                     ");";
            }
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
            sql_Statement = strSql2;
        }

    }
}
