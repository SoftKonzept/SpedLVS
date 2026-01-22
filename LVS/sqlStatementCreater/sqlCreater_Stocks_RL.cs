using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_RL
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
        public sqlCreater_Stocks_RL(int myWorkspaceId,
                                               int myGArtID,
                                               DateTime myDateFrom,
                                               DateTime myDateTo
                                               )
        {
            string strSql2 = string.Empty;

            strSql2 = " From Artikel a " +
                     "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                     "INNER JOIN Gueterart e ON e.ID=a.GArtID " +
                     "INNER JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                     "WHERE " +
                            " b.AbBereich=" + myWorkspaceId + " " +
                            "AND (c.Datum between '" + myDateFrom.Date.ToShortDateString() + "' AND '" + myDateTo.Date.AddDays(1).ToShortDateString() + "') " +
                            " AND a.CheckArt=1 " +
                            " AND c.IsRL=1 ";
            if (myGArtID > 0)
            {
                strSql2 += " AND a.GArtID IN (" + (Int32)myGArtID + ") ";
            }
            sql_Statement = strSql2;
        }

    }
}
