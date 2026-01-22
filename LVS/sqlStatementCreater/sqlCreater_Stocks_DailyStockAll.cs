using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_DailyStockAll
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
        public sqlCreater_Stocks_DailyStockAll(int myWorkspaceId,
                                               int myStockAdrId,
                                               int myGArtID,
                                               DateTime myDateFrom,
                                               DateTime myDateTo,
                                               bool bUseBKZ = true,
                                               bool mySysModulStockDailyStockExclSPL = true
                                               )
        {
            string strSql2 = string.Empty;

            strSql2 = " From Artikel a " +
                          "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                          "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                          "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                          "WHERE " +
                             " b.AbBereich=" + myWorkspaceId + " AND " +
                             "(( " +
                                "";
            if (bUseBKZ)
            {
                strSql2 += " a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql2 += " a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
            }
            if (myGArtID > 0)
            {
                strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            strSql2 += " AND b.DirectDelivery=0  AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
                        ") " +
                        "OR " +
                        "(";
            if (bUseBKZ)
            {
                strSql2 += "a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql2 += "a.CheckArt=1 AND b.[Check]=1 and (c.Checked=1) ";
            }
            if (myGArtID > 0)
            {
                strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            strSql2 += " AND c.Datum>='" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
                       "AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
                        "))";
            sql_Statement = strSql2;
        }

    }
}
