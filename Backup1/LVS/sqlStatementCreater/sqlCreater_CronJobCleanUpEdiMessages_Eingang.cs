using LVS.Models;

namespace LVS.sqlStatementCreater
{
    internal class sqlCreater_CronJobCleanUpEdiMessages_Eingang
    {
        internal Asn AsnHead { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsn"></param>
        public sqlCreater_CronJobCleanUpEdiMessages_Eingang(Asn myAsn)
        {
            AsnHead = myAsn;
        }
        /// <summary>
        /// 
        /// </summary>
        public string sql_GetEingaengeByAsn
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Select e.* FROM LEingang e " +
                                    " WHERE e.ASN = " + AsnHead.Id;
                return strSql;
            }
        }
    }
}
