using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_StockChargePerDay
    {
        //Tagesbestand LAger komplett

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
        public sqlCreater_Stocks_StockChargePerDay(int myWorkspaceId,
                                               int myStockAdrId,
                                               int myGArtID,
                                               DateTime myDateFrom,
                                               DateTime myDateTo,
                                               int myFreeStorageDays
                                               //bool bUseBKZ = true,
                                               //bool mySysModulStockDailyStockExclSPL = true
                                               )
        {
            string strSql2 = string.Empty;

            strSql2 += ", CASE " +
                        "WHEN (c.Datum IS NULL) " +
                        "THEN CAST( DATEDIFF(day, CAST(b.Date as Date),CAST('" + myDateFrom.Date.ToString() + "' as Date)) as INT)+1 - " + myFreeStorageDays + " " +
                        "ELSE CAST( DATEDIFF(day, CAST(b.Date as Date),CAST(c.Datum as Date)) as INT)+1 - " + myFreeStorageDays + " " +
                        "END as AbrDauer ";
            strSql2 += " From Artikel a " +
                         "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                         "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                         "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                        " WHERE " +
                            "(" +
                              "(" +
                                " a.CheckArt = 1 AND b.[Check] = 1 and(c.Checked is Null or c.Checked = 0) " +
                                " AND b.DirectDelivery = 0 AND b.AbBereich = " + myWorkspaceId + " ";
            if (myStockAdrId > 0)
            {
                strSql2 += " AND b.Auftraggeber=" + myStockAdrId + " ";
            }
            if (myGArtID > 0)
            {
                strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            strSql2 += " AND " +
                            "(CASE " +
                                "WHEN(c.Datum IS NULL) THEN(CAST(DATEDIFF(dd, b.Date, '" + myDateTo.ToShortDateString() + "') as INT) + 1) " +
                                "ELSE (CAST(DATEDIFF(dd, b.Date, c.Datum) as INT) + 1) " +
                                " END) >= " + myFreeStorageDays + " " +
                       " AND b.Date < '" + myDateFrom.ToShortDateString() + "' " +
                    ") OR ( " +
                         " a.CheckArt = 1 AND b.[Check] = 1 and c.Checked = 1 " +
                         " AND b.DirectDelivery = 0  AND b.AbBereich =" + myWorkspaceId + " ";
            if (myStockAdrId > 0)
            {
                strSql2 += " AND b.Auftraggeber=" + myStockAdrId + " ";
            }
            if (myGArtID > 0)
            {
                strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }

            strSql2 += " AND c.Datum >= '" + myDateFrom.ToShortDateString() + "' " +
                    " AND " +
                            "(CASE " +
                                "WHEN(c.Datum IS NULL) THEN(CAST(DATEDIFF(dd, b.Date, '" + myDateTo.ToShortDateString() + "') as INT) + 1) " +
                                "ELSE (CAST(DATEDIFF(dd, b.Date, c.Datum) as INT) + 1) " +
                                " END) >= " + myFreeStorageDays + " " +
                    " AND b.Date < '" + myDateFrom.ToShortDateString() + "' " +
            ")" +
            ")" +
            " AND a.ID NOT IN(SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn > 0))";
            sql_Statement = strSql2;
        }

    }
}
