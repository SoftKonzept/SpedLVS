using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_Stocks_DailyStockSPL
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
        public sqlCreater_Stocks_DailyStockSPL(int myWorkspaceId,
                                               int myStockAdrId,
                                               //int myGArtID,
                                               DateTime myDateFrom,
                                               DateTime myDateTo,
                                               bool bFilterJournal = true,
                                               bool bUseBKZ = true
                                               //bool mySysModulStockDailyStockExclSPL = true
                                               )
        {
            string strSql = string.Empty;

            strSql += ", (Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Datum SPL' " +
            ", (Select TOP(1) BKZ FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Buchung'" +
            ", (Select TOP(1) IsCustomCertificateMissing FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Zert'" +
            ", (Select TOP(1) Sperrgrund FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) as 'Sperrgrund'" +
            ", CAST(0 as bit) as 'ausbuchen'";

            if (bFilterJournal)
            {
                strSql += ", (Select ADR.ViewID FROM ADR WHERE ADR.ID=b.Auftraggeber) as Auftraggeber";
            }

            strSql += " From Artikel a " +
                            "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                            "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                            "LEFT JOIN LAusgang c ON c.ID = a.LAusgangTableID " +
                            "WHERE " +
                            //"(Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) > '" + BestandVon.Date.AddDays(-1) + "' AND " +
                            //"(Select TOP(1) Datum FROM Sperrlager WHERE ArtikelID=a.ID ORDER BY ID DESC) <'" + BestandBis.Date.AddDays(1) + "' AND" +
                            " b.AbBereich=" + myWorkspaceId + " ";
            if (myStockAdrId > 0)
            {
                strSql += " AND b.Auftraggeber =" + myStockAdrId + " ";
            }


            if (bFilterJournal)
            {
                strSql += " AND a.ID IN (SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID IN " +
                            "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE " +
                                                                            "c.SPLIDIn>0 " +
                                                                            " AND c.Datum between '" + myDateFrom.Date + "' AND '" + myDateTo.Date.AddDays(1) + "' ))";
            }
            else
            {
                strSql += " AND a.ID IN (SELECT a.ArtikelID FROM Sperrlager a WHERE a.BKZ = 'IN' AND a.ID NOT IN " +
                           "(SELECT DISTINCT c.SPLIDIn FROM Sperrlager c WHERE c.SPLIDIn>0)) ";
            }

            sql_Statement = strSql;
        }

    }
}
