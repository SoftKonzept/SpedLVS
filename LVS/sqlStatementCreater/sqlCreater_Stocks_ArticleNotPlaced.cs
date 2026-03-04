namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_ArticleNotPlaced
    {
        ///----Nicht abgeschlossene Ein-/Ausgänge
        ///----Nicht abgeschlossene Ausgänge
        /// <summary>
        /// 
        /// 
        ///                     //strSql2 = " From Artikel a " +
        //             "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
        //             "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
        //             "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
        //             "WHERE " +
        //                "a.BKZ=1 AND (a.LagerOrt=0 OR a.LagerOrt is Null) " +
        //                "AND (a.LOTable='') " +
        //                "AND a.LVSNr_ALTLvs=0 " +
        //                "AND b.DirectDelivery=0 " +
        //                "AND b.AbBereich=" + myWorkspaceId + " ";


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
                         "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                         "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID ";

            strSql += "LEFT JOIN ( ";
            /* --- performante Schaden-Ermittlung: voraggregieren und einmal joinen --- */
            strSql += "SELECT sz.ArtikelID, ";
            /* Variante 1: ohne Sortierung (immer verfügbar ab SQL 2017) */
            strSql += "STRING_AGG(CONVERT(nvarchar(4000), s.Bezeichnung), CHAR(10)) AS Schaden ";
            /* Variante 2 (optional): mit definierter Sortierung
                    -> aktivieren, falls deine SQL-Version WITHIN GROUP unterstützt
                STRING_AGG(CONVERT(nvarchar(4000), s.Bezeichnung), CHAR(10))
                    WITHIN GROUP (ORDER BY s.Bezeichnung) AS Schaden
                */
            strSql += "FROM SchadenZuweisung AS sz ";
            strSql += "JOIN Schaeden AS s ON s.ID = sz.SchadenID ";
            strSql += "GROUP BY sz.ArtikelID ";
            strSql += " ) AS sch ON sch.ArtikelID = a.ID ";


            strSql += "WHERE " +
                            "a.BKZ=1 AND (a.LagerOrt=0 OR a.LagerOrt is Null) " +
                            "AND (a.LOTable='') " +
                            "AND a.LVSNr_ALTLvs=0 " +
                            "AND b.DirectDelivery=0 " +
                            "AND b.AbBereich=" + myWorkspaceId + " ";
            sql_Statement = strSql;
        }

    }
}
