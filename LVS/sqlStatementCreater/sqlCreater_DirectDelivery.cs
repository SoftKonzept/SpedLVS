using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_DirectDelivery
    {
        //Direktanlieferungen

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
        public sqlCreater_DirectDelivery(int myWorkspaceId,
                                               int myStockAdrId,
                                               int myGArtID,
                                               DateTime myDateFrom,
                                               DateTime myDateTo,
                                               string mySqlGoodsTypeIdString,
                                               bool bFilterJournal = true,
                                               bool bUseBKZ = true
                                               //bool mySysModulStockDailyStockExclSPL = true
                                               )
        {
            string strSql2 = string.Empty;

            strSql2 = " From Artikel a " +
                     "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                     "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                     "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                     "WHERE " +

                     " (b.Date between '" + myDateFrom.Date.ToShortDateString() + "' AND '" + myDateTo.Date.AddDays(1).ToShortDateString() + "') " +
                     " AND b.Auftraggeber=" + myStockAdrId +
                     " AND b.DirectDelivery=1 " +
                     " AND b.AbBereich=" + myWorkspaceId + " ";
            if (bFilterJournal)
            {
                if (mySqlGoodsTypeIdString != string.Empty)
                {
                    strSql2 = strSql2 + " AND a.GArtID IN (" + mySqlGoodsTypeIdString + ") ";
                }
            }
            else
            {
                if (myGArtID > 0)
                {
                    strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
                }
            }
            sql_Statement = strSql2;
        }

    }
}
