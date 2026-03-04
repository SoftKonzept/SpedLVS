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
        //public sqlCreater_Stocks_DailyStockAll(int myWorkspaceId,
        //                                       int myStockAdrId,
        //                                       int myGArtID,
        //                                       DateTime myDateFrom,
        //                                       DateTime myDateTo,
        //                                       bool bUseBKZ = true,
        //                                       bool mySysModulStockDailyStockExclSPL = true
        //                                       )
        //{
        //    string strSql2 = string.Empty;

        //    strSql2 = " From Artikel a " +
        //                  "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
        //                  "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
        //                  "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
        //                  "WHERE " +
        //                     " b.AbBereich=" + myWorkspaceId + " AND " +
        //                     "(( " +
        //                        "";
        //    if (bUseBKZ)
        //    {
        //        strSql2 += " a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
        //    }
        //    else
        //    {
        //        strSql2 += " a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
        //    }
        //    if (myGArtID > 0)
        //    {
        //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
        //    }
        //    strSql2 += " AND b.DirectDelivery=0  AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
        //                ") " +
        //                "OR " +
        //                "(";
        //    if (bUseBKZ)
        //    {
        //        strSql2 += "a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
        //    }
        //    else
        //    {
        //        strSql2 += "a.CheckArt=1 AND b.[Check]=1 and (c.Checked=1) ";
        //    }
        //    if (myGArtID > 0)
        //    {
        //        strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
        //    }
        //    strSql2 += " AND c.Datum>='" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
        //               "AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' " +
        //                "))";
        //    sql_Statement = strSql2;
        //}

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

            strSql2 = " FROM Artikel AS a ";
            strSql2 += "JOIN LEingang AS b ON b.ID = a.LEingangTableID ";
            strSql2 += "JOIN Gueterart AS e ON e.ID = a.GArtID ";
            strSql2 += "LEFT JOIN LAusgang AS c ON c.ID = a.LAusgangTableID ";

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
            strSql2 += "b.AbBereich=" + myWorkspaceId;
            strSql2 += " AND (";

            strSql2 +=      "(";
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
                strSql2 +=      " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            strSql2 +=          " AND b.DirectDelivery=0  AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
            strSql2 +=          ") OR (";

            if (bUseBKZ)
            {
                strSql2 +=      "a.BKZ=0 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql2 +=      "a.CheckArt=1 AND b.[Check]=1 and (c.Checked=1) ";
            }
            if (myGArtID > 0)
            {
                strSql2 +=      " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            strSql2 +=          " AND c.Datum>='" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
            strSql2 +=          " AND b.Date <'" + myDateFrom.Date.AddDays(1).ToShortDateString() + "' ";
            strSql2 +=          "))";

            sql_Statement = strSql2;
        }
    }
}
