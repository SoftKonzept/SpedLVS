using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_Inventory
    {
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
        ///             //Inventur ist gleich Tagesbestand incl. SPL
        /// </summary>
        /// <param name="myWorkspaceId"></param>
        /// <param name="myStockAdrId"></param>
        /// <param name="myGArtID"></param>
        /// <param name="bFilterJournal"></param>
        /// <param name="bUseBKZ"></param>
        public sqlCreater_Stocks_Inventory(int myWorkspaceId,
                                            int myStockAdrId,
                                            int myGArtID,
                                            bool bFilterJournal = true,
                                            bool bUseBKZ = true)
        {
            //SqlGoodsTypeIdString = mySqlGoodsTypeIdString;
            string strSql2 = string.Empty;

            strSql2 = " From Artikel a " +
                      "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                      "LEFT JOIN Gueterart e ON e.ID=a.GArtID " +
                      "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID ";

            strSql2 += "LEFT JOIN ( ";
            /* --- performante Schaden-Ermittlung: voraggregieren und einmal joinen --- */
            strSql2 += "SELECT sz.ArtikelID, ";
            /* Variante 1: ohne Sortierung (immer verfügbar ab SQL 2017) */
            strSql2 += "STRING_AGG(CONVERT(nvarchar(4000), s.Bezeichnung), CHAR(10)) AS Schaden ";
            /* Variante 2 (optional): mit definierter Sortierung
                    -> aktivieren, falls deine SQL-Version WITHIN GROUP unterstützt
                STRING_AGG(CONVERT(nvarchar(4000), s.Bezeichnung), CHAR(10))
                    WITHIN GROUP (ORDER BY s.Bezeichnung) AS Schaden
                */
            strSql2 += "FROM SchadenZuweisung AS sz ";
            strSql2 += "JOIN Schaeden AS s ON s.ID = sz.SchadenID ";
            strSql2 += "GROUP BY sz.ArtikelID ";
            strSql2 += " ) AS sch ON sch.ArtikelID = a.ID ";

            strSql2 += "WHERE ";
            if (myStockAdrId > 0)
            {
                strSql2 += "b.Auftraggeber=" + myStockAdrId + " AND ";
            }
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
            strSql2 += " AND b.DirectDelivery=0  AND b.AbBereich=" + myWorkspaceId + " ";

            sql_Statement = strSql2;
        }
    }
}
