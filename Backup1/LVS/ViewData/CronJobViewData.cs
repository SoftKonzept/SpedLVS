using LVS.Models;
using LVS.sqlStatementCreater;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class CronJobViewData
    {
        public const string const_autoBestand = "#autoBestand#";
        public const string const_autoBestandExcel = "#autoBestandExcel#";
        public const string const_autoGewBestand = "#autoGewBestand#";
        public const string const_autoGewBestandExcel = "#autoGewBestandExcel#";
        public const string const_autoJournal = "#autoJournal#";
        public const string const_autoJournalExcel = "#autoJournalExcel#";
        public const string const_CleanUpEdiMessages = "#CleanUpEdiMessages#";

        public CronJobs CronJob { get; set; }
        private sqlCreater_CronJob sqlCreater_CronJob { get; set; } = new sqlCreater_CronJob(new CronJobs());
        private int BenutzerID { get; set; } = 0;

        public List<CronJobs> ListCronJobs { get; set; }


        public CronJobViewData()
        {
            InitCls();
        }
        public CronJobViewData(int myUserId) : this()
        {
            BenutzerID = myUserId;
        }
        public CronJobViewData(CronJobs myCronJob, int myUserId) : this()
        {
            CronJob = myCronJob.Copy();
            BenutzerID = myUserId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myId"></param>
        /// <param name="myUserId"></param>
        public CronJobViewData(int myId, int myUserId) : this()
        {
            CronJob.Id = myId;
            BenutzerID = myUserId;
            if (CronJob.Id > 0)
            {
                Fill();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            CronJob = new CronJobs();
            FillList();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "CronJobs", "CronJobs", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillList()
        {
            ListCronJobs = new List<CronJobs>();
            string strSQL = sql_GetList;
            DataTable dt = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetDataTable(strSQL, "CronJobs", "CronJobs", BenutzerID);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr);
                    if (!ListCronJobs.Contains(CronJob))
                    {
                        ListCronJobs.Add(CronJob);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        private void SetValue(DataRow row)
        {
            CronJob = new CronJobs();
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            CronJob.Id = iTmp;
            enumCronJobAction tmpEnum = enumCronJobAction.Default;
            Enum.TryParse(row["Aktion"].ToString(), out tmpEnum);
            CronJob.Aktion = tmpEnum;
            CronJob.Beschreibung = row["Beschreibung"].ToString();
            DateTime dtTmp = DateTime.MinValue;
            DateTime.TryParse(row["Aktionsdatum"].ToString(), out dtTmp);
            CronJob.Aktionsdatum = dtTmp;
            CronJob.Periode = row["Periode"].ToString().ToString();
            dtTmp = DateTime.MinValue;
            DateTime.TryParse(row["vZeitraum"].ToString(), out dtTmp);
            CronJob.vZeitraum = dtTmp;
            dtTmp = DateTime.MinValue;
            DateTime.TryParse(row["bZeitraum"].ToString(), out dtTmp);
            CronJob.bZeitraum = dtTmp;
            bool bTmp = false;
            iTmp = 0;
            int.TryParse(row["aktiv"].ToString(), out iTmp);
            if (iTmp == 1)
            {
                bTmp = true;
            }
            CronJob.aktiv = bTmp;
            iTmp = 0;
            int.TryParse(row["AdrId"].ToString(), out iTmp);
            CronJob.AdrId = iTmp;
        }
        /// <summary>
        ///             ADD
        /// </summary>
        public bool Add()
        {
            bool bReturn = false;
            sqlCreater_CronJob = new sqlCreater_CronJob(CronJob);
            string strSql = sqlCreater_CronJob.sql_Add;
            strSql = strSql + "Select @@IDENTITY as 'ID';";
            string strTmp = clsSQLCOM.ExecuteSQLWithTRANSACTIONGetValue(strSql, "Add", BenutzerID);
            int.TryParse(strTmp, out int iTmp);
            CronJob.Id = iTmp;
            bReturn = (this.CronJob.Id > 0);
            return bReturn;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool Delete()
        {
            bool bReturn = false;
            sqlCreater_CronJob = new sqlCreater_CronJob(CronJob);
            if (this.CronJob.Id > 0)
            {
                string strSql = string.Empty;
                strSql = sqlCreater_CronJob.sql_Delete;
                bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "DELETE", BenutzerID);
            }
            return bReturn;
        }
        public bool UpdateNextActiondate()
        {
            bool bReturn = false;
            sqlCreater_CronJob = new sqlCreater_CronJob(CronJob);
            if (this.CronJob.Id > 0)
            {
                string strSql = string.Empty;
                strSql = sqlCreater_CronJob.sql_UpdateNextActiondate;
                bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "UPDATE", BenutzerID);
            }
            return bReturn;
        }
        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
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
                //strSql = "SELECT * FROM CronJobs " +
                //                    " WHERE " +
                //                        //"DATEDIFF(dd, Aktionsdatum, '" + DateTime.Now + "')>=0 " +
                //                        "DATEDIFF(MI, Aktionsdatum, '" + DateTime.Now + "')>=0 " +
                //                        " AND Aktionsdatum between vZeitraum and bZeitraum " +
                //                        " AND aktiv=1 ;";
                sqlCreater_CronJob = new sqlCreater_CronJob(CronJob);
                strSql = sqlCreater_CronJob.sql_GetList;
                return strSql;
            }
        }
        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_Delete
        {
            get
            {
                string strSql = string.Empty;
                //strSql = " Delete FROM Abrufe WHERE ID =" + Abruf.Id + "; " ;
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }
        /// <summary>
        ///             GET sql - String
        /// </summary>
        public string sql_Get_Main
        {
            get
            {
                string strSql = string.Empty;
                return strSql;
            }
        }
    }
}

