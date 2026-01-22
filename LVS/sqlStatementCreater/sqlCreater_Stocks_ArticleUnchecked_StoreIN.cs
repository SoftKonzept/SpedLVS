using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_ArticleUnchecked_StoreIN
    {
        /// Ungeprüfte Artikel im Eingangn

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
        public sqlCreater_Stocks_ArticleUnchecked_StoreIN(
                                               int myWorkspaceId,
                                               int myStockAdrId,
                                               int myGArtID
                                               //DateTime myDateFrom,
                                               //DateTime myDateTo,
                                               //string mySqlGoodsTypeIdString,
                                               //bool bFilterJournal = true,
                                               //bool bUseBKZ = true
                                               //bool mySysModulStockDailyStockExclSPL = true
                                               )
        {
            string strSql2 = string.Empty;

            strSql2 = " From Artikel a " +
                     "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                     "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                     "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                     "WHERE " +
                            "a.CheckArt=0 AND a.LEingangTableID>0 " +
                            " AND b.AbBereich=" + myWorkspaceId + " ";
            if (myStockAdrId > 0)
            {
                strSql2 += " AND b.Auftraggeber=" + myStockAdrId;
            }
            if (myGArtID > 0)
            {
                strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            sql_Statement = strSql2;
        }

    }
}
