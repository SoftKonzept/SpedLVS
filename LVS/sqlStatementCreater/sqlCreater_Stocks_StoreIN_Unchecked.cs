namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_StoreIN_Unchecked
    {
        ///----Nicht abgeschlossene Ein-/Ausgänge
        ///----Nicht abgeschlossene Eingänge

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
        public sqlCreater_Stocks_StoreIN_Unchecked(
                                           int myWorkspaceId,
                                           int myStockAdrId
                                           //int myGArtID
                                           //DateTime myDateFrom,
                                           //DateTime myDateTo,
                                           //string mySqlGoodsTypeIdString,
                                           //bool bFilterJournal = true,
                                           //bool bUseBKZ = true
                                           //bool mySysModulStockDailyStockExclSPL = true
                                           )
        {
            string strSql2 = string.Empty;

            strSql2 = "Select " +
                           " LEingangID as Eingang " +
                           ",Convert(date, [Date],104) as Eingangsdatum" +
                           ",ViewID as Kunde" +
                           ",ExTransportRef" +
                           ",ExAuftragRef " +
                           ",LEingang.ID as LEingangTableID " +

                           " from LEingang " +
                               "left join ADR on Leingang.Auftraggeber=ADR.ID " +
                               "where " +
                                   "LEingang.AbBereich=" + myWorkspaceId + " " +
                                   "AND LEingang.[check] = 0 ";

            if (myStockAdrId > 0)
            {
                strSql2 += " AND Leingang.Auftraggeber=" + myStockAdrId;
            }

            strSql2 += " order by viewID";
            sql_Statement = strSql2;
        }

    }
}
