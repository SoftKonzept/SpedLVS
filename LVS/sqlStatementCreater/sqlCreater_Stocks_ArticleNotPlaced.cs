namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_ArticleNotPlaced
    {
        ///----Nicht abgeschlossene Ein-/Ausgänge
        ///----Nicht abgeschlossene Ausgänge
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
        public sqlCreater_Stocks_ArticleNotPlaced(
                                           int myWorkspaceId
                                           //int myStockAdrId
                                           //int myGArtID
                                           //DateTime myDateFrom,
                                           //DateTime myDateTo,
                                           //string mySqlGoodsTypeIdString,
                                           //bool bFilterJournal = true,
                                           //bool bUseBKZ = true
                                           //bool mySysModulStockDailyStockExclSPL = true
                                           )
        {
            string strSql = string.Empty;

            strSql = " From Artikel a " +
                         "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                         "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                         "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                         "WHERE " +
                            "a.BKZ=1 AND (a.LagerOrt=0 OR a.LagerOrt is Null) " +
                            "AND (a.LOTable='') " +
                            "AND a.LVSNr_ALTLvs=0 " +
                            "AND b.DirectDelivery=0 " +
                            "AND b.AbBereich=" + myWorkspaceId + " ";
            sql_Statement = strSql;
        }

    }
}
