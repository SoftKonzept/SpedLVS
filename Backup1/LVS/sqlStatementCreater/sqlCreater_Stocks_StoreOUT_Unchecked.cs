namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_StoreOUT_Unchecked
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
        public sqlCreater_Stocks_StoreOUT_Unchecked(
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
            string strSql = string.Empty;

            strSql = "Select " +
                           " LAusgangID as Ausgang " +
                           ",convert(date,Datum,104) as Ausgangsdatum" +
                           ",ViewID as Kunde" +
                           ",ExTransportRef " +
                           ",LAusgang.ID as LAusgangTableID " +
                               " from LAusgang " +
                                       "left join ADR on LAusgang.Auftraggeber=ADR.ID ";

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


            strSql += "where " +
                        "LAusgang.AbBereich=" + myWorkspaceId + " " +
                        "AND LAusgang.checked = 0";

            if (myStockAdrId > 0)
            {
                strSql += " AND LAusgang.Auftraggeber=" + myStockAdrId;
            }
            strSql += " order by viewID";
            sql_Statement = strSql;
        }

    }
}
