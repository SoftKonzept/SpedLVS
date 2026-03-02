using DocumentFormat.OpenXml.Office2010.Excel;
using LVS.ASN.EDIFACT;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using Org.BouncyCastle.Utilities.Zlib;
using Svg;
using System;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_Select
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
        ///              adr.Kunde.Tarif.TarifGArtZuweisung.SQLGArtIDString
        /// </summary>
        internal string SqlGoodsTypeIdString { get; set; } = string.Empty;


        public sqlCreater_Stocks_Select(int myWorkspaceId,
                                           int myStockAdrId,
                                           int myGArtID,
                                           string mySqlGoodsTypeIdString,
                                           bool bFilterJournal = true,
                                           bool bUseBKZ = true)
        {
            SqlGoodsTypeIdString = mySqlGoodsTypeIdString;
            string strSql2 = string.Empty;

            strSql2 = " From Artikel a " +
                      "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                      "INNER JOIN Gueterart e ON .ID=a.GArtID " +
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
            strSql2 += " b.AbBereich=" + myWorkspaceId + " AND ";

            if (bUseBKZ)
            {
                strSql2 += " a.BKZ=1 AND a.CheckArt=1 AND b.[Check]=1 ";
            }
            else
            {
                strSql2 += "  a.CheckArt=1 AND b.[Check]=1 and (c.Checked is Null or c.Checked=0) ";
            }
            if (bFilterJournal)
            {
                strSql2 = strSql2 + " AND b.Auftraggeber=" + myStockAdrId + " " +
                                   " AND a.GArtID IN (" + SqlGoodsTypeIdString + ") ";
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


        //public sqlCreater_Stocks_Select(int myWorkspaceId,
        //                                   int myStockAdrId,
        //                                   int myGArtID,
        //                                   string mySqlGoodsTypeIdString,
        //                                   bool bFilterJournal = true,
        //                                   bool bUseBKZ = true)
        //{
        //    SqlGoodsTypeIdString = mySqlGoodsTypeIdString;
        //    string strSql2 = string.Empty;

        //    strSql2  = " FROM Artikel       AS a ";
        //    strSql2 += "JOIN LEingang AS b ON b.ID = a.LEingangTableID ";
        //    strSql2 += "JOIN Gueterart AS g ON g.ID = a.GArtID ";
        //    strSql2 += "LEFT JOIN LAusgang AS c ON c.ID = a.LAusgangTableID ";
        //    strSql2 += "LEFT JOIN ( ";
        //    /* --- performante Schaden-Ermittlung: voraggregieren und einmal joinen --- */
        //    strSql2 += "SELECT sz.ArtikelID, ";
        //    /* Variante 1: ohne Sortierung (immer verfügbar ab SQL 2017) */
        //    strSql2 += "STRING_AGG(CONVERT(nvarchar(4000), s.Bezeichnung), CHAR(10)) AS Schaden ";
        //    /* Variante 2 (optional): mit definierter Sortierung
        //            -> aktivieren, falls deine SQL-Version WITHIN GROUP unterstützt
        //        STRING_AGG(CONVERT(nvarchar(4000), s.Bezeichnung), CHAR(10))
        //            WITHIN GROUP (ORDER BY s.Bezeichnung) AS Schaden
        //        */
        //    strSql2 += "FROM SchadenZuweisung AS sz ";
        //    strSql2 +=          "JOIN Schaeden AS s ON s.ID = sz.SchadenID ";
        //    strSql2 +=          "GROUP BY sz.ArtikelID ";
        //    strSql2 +=          " ) AS sch ON sch.ArtikelID = a.ID ";

        //    strSql2 += "WHERE ";
        //    strSql2 +=      "b.AbBereich=" + myWorkspaceId; 
        //    strSql2 +=      " AND a.CheckArt=1 ";
        //    strSql2 +=      " AND b.[Check]=1 ";
        //    if (bUseBKZ)
        //    {
        //        strSql2 += "AND a.BKZ=1 ";
        //    }
        //    else
        //    {
        //        strSql2 += " AND (c.Checked is Null or c.Checked=0) ";
        //    }
        //    if (bFilterJournal)
        //    {
        //        strSql2 += " AND b.Auftraggeber=" + myStockAdrId;
        //        strSql2 += " AND a.GArtID IN (" + SqlGoodsTypeIdString + ") ";
        //    }
        //    else
        //    {
        //        if (myGArtID > 0)
        //        {
        //            strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
        //        }
        //    }
        //    sql_Statement = strSql2;
        //}
    }
}
