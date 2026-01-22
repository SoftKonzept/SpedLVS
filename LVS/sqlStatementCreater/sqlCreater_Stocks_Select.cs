using System;

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
                      "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                      "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                      "WHERE ";
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

    }
}
