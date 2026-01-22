using LVS.Models;
using System;

namespace LVS.sqlStatementCreater
{
    public class sqlCreater_CronJob
    {
        private CronJobs CronJob { get; set; } = new CronJobs();
        public sqlCreater_CronJob(CronJobs myCronJob)
        {
            CronJob = myCronJob;
        }
        /// <summary>
        /// 
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSql = string.Empty;
                strSql = "INSERT INTO CronJobs ( Aktion, Beschreibung, Aktionsdatum, Periode, vZeitraum, bZeitraum, aktiv, AdrId" +
                                                ") " +
                                            "VALUES ('" + CronJob.Aktion.ToString() + "'" +
                                                    ", '" + CronJob.Beschreibung + "'" +
                                            ", '" + CronJob.Aktionsdatum + "'" +
                                                    ", '" + CronJob.Periode + "'" +
                                                    ", '" + CronJob.vZeitraum + "'" +
                                                    ", '" + CronJob.bZeitraum + "'" +
                                                    ", " + Convert.ToInt32(CronJob.aktiv) +
                                                    ", " + CronJob.AdrId +
                                                    ");";
                return strSql;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                strSql = "DELETE FROM CronJobs WHERE ID=" + CronJob.Id + ";";
                return strSql;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_GetList
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM CronJobs " +
                                    " WHERE " +
                                        //"DATEDIFF(dd, Aktionsdatum, '" + DateTime.Now + "')>=0 " +
                                        "DATEDIFF(MI, Aktionsdatum, '" + DateTime.Now + "')>=0 " +
                                        " AND Aktionsdatum between vZeitraum and bZeitraum " +
                                        " AND aktiv=1 ;";
                return strSql;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sql_UpdateNextActiondate
        {
            get
            {
                switch (CronJob.Periode)
                {
                    case "stündlich":
                        CronJob.Aktionsdatum = CronJob.Aktionsdatum.AddHours(1);
                        break;
                    case "täglich":
                        //this.Aktionsdatum = this.Aktionsdatum.AddDays(1);
                        while (CronJob.Aktionsdatum < DateTime.Now)
                        {
                            CronJob.Aktionsdatum = CronJob.Aktionsdatum.AddDays(1);
                        }
                        break;
                    case "wöchentlich":
                        CronJob.Aktionsdatum = CronJob.Aktionsdatum.AddDays(7);
                        while (CronJob.Aktionsdatum < DateTime.Now)
                        {
                            CronJob.Aktionsdatum = CronJob.Aktionsdatum.AddDays(7);
                        }

                        break;
                    case "monatlich":
                        CronJob.Aktionsdatum = CronJob.Aktionsdatum.AddMonths(1);
                        while (CronJob.Aktionsdatum < DateTime.Now)
                        {
                            CronJob.Aktionsdatum = CronJob.Aktionsdatum.AddMonths(1);
                        }
                        break;
                    case "jährlich":
                        CronJob.Aktionsdatum = CronJob.Aktionsdatum.AddYears(1);
                        while (CronJob.Aktionsdatum < DateTime.Now)
                        {
                            CronJob.Aktionsdatum = CronJob.Aktionsdatum.AddYears(1);
                        }
                        break;
                }

                string strSql = string.Empty;
                strSql = "Update CronJobs " +
                                "SET Aktionsdatum='" + CronJob.Aktionsdatum + "' " +
                                "WHERE ID=" + CronJob.Id + ";";
                return strSql;
            }
        }




    }
}
