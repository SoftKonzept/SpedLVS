namespace LVS.sqlStatementCreater
{
    public class sqlCreater_CronJobCleanUpEdiMessages_EingangArticle
    {
        internal int EingangTableId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEingangTableId"></param>
        public sqlCreater_CronJobCleanUpEdiMessages_EingangArticle(int myEingangTableId)
        {
            EingangTableId = myEingangTableId;
        }
        /// <summary>
        /// 
        /// </summary>
        public string sql_GetEingaengArtikel
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT " +
                                "a.ID " +
                                ", a.LAusgangTableID as LAusgangTableID " +
                                ", a.LVS_ID as LVSNR " +
                                ", e.ASN as ASN " +
                                ", aus.Datum as Ausgangdatum " +
                                "FROM Artikel a " +
                                "Inner join LEingang e on e.ID = a.LEingangTableID " +
                                "Left join LAusgang aus on aus.ID = a.LAusgangTableID " +
                                "Where " +
                                    "a.LEingangTableID =" + EingangTableId;
                return strSql;
            }
        }
    }
}
