using LVS.Models;
using System;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdiQalityD96AViewData
    {
        public WorkspaceViewData workspaceVD { get; set; }
        public _EdiQalityD96A ediQalityD96A { get; set; }
        private int BenutzerID { get; set; }

        public EdiQalityD96AViewData()
        {
            InitCls();
        }
        public EdiQalityD96AViewData(_EdiQalityD96A myEdiQualityD96A)
        {
            this.ediQalityD96A = myEdiQualityD96A;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            ediQalityD96A = new _EdiQalityD96A();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
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
        /// <param name="row"></param>
        public void SetValue(DataRow row)
        {
            ediQalityD96A = new _EdiQalityD96A();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            ediQalityD96A.Id = iTmp;

            ediQalityD96A.iDocNo = row["iDocNo"].ToString();
            ediQalityD96A.Path = row["Path"].ToString();
            ediQalityD96A.FileName = row["FileName"].ToString();

            DateTime tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Datum"].ToString(), out tmpDate);
            ediQalityD96A.Datum = tmpDate;

            iTmp = 0;
            int.TryParse(row["WorkspaceId"].ToString(), out iTmp);
            ediQalityD96A.WorkspaceId = iTmp;

            if (ediQalityD96A.WorkspaceId > 0)
            {
                workspaceVD = new WorkspaceViewData(ediQalityD96A.WorkspaceId);
                ediQalityD96A.Workspace = workspaceVD.Workspace.Copy();
            }

            iTmp = 0;
            int.TryParse(row["ArticleId"].ToString(), out iTmp);
            ediQalityD96A.ArticleId = iTmp;

            ediQalityD96A.IsActive = (bool)row["IsActive"];
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSQL = sql_Add;
            strSQL = strSQL + " Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                ediQalityD96A.Id = iTmp;
            }
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
        }
        /// <summary>
        ///             UPDATE
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            string strSql = sql_Update;
            bool retVal = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void FillList(bool mybInclSub)
        {
            //ListEdiDelforValue = new List<EdiDelforD97AValues>();
            //string strSQL = sql_GetList;
            //DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        SetValue(dr, mybInclSub);
            //        ListEdiDelforValue.Add(EdiDelforValue);
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            string strSQL = sql_GetList;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiDelforValue");
            return dt;
        }

        ///-----------------------------------------------------------------------------------------------------
        ///                             sql Statements
        ///-----------------------------------------------------------------------------------------------------

        /// <summary>
        ///             Add sql - String
        /// </summary>
        public string sql_Add
        {
            get
            {
                string strSQL = "INSERT INTO EdiQualityD96A ([iDocNo], [Path], [FileName], [Datum], [WorkspaceId], [ArticleId], [IsActive])" +
                                                            "VALUES " +
                                                            "(" + "" +
                                                                 "'" + ediQalityD96A.iDocNo + "'" +
                                                                 ", '" + ediQalityD96A.Path + "'" +
                                                                 ", '" + ediQalityD96A.FileName + "'" +
                                                                 ", '" + ediQalityD96A.Datum + "'" +
                                                                 ", " + ediQalityD96A.WorkspaceId +
                                                                 ", " + ediQalityD96A.ArticleId +
                                                                 ", " + Convert.ToInt32(ediQalityD96A.IsActive) +
                                                             ") ";
                return strSQL;
            }
        }
        /// <summary>
        ///             GET
        /// </summary>
        public string sql_Get
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM EdiQualityD96A WHERE ID=" + ediQalityD96A.Id + "; ";
                return strSql;
            }
        }

        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetList
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM EdiQualityD96A WHERE IsActive=1; ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET_Main
        /// </summary>
        public string sql_Get_Main
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM EdiQualityD96A";
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
                strSql = "Update EdiQualityD96A SET " +
                                            " [iDocNo] = '" + ediQalityD96A.iDocNo + "'" +
                                            ", [Path] = '" + ediQalityD96A.Path + "'" +
                                            ", [FileName] = '" + ediQalityD96A.Path + "'" +
                                            ", [Datum] = '" + ediQalityD96A.Datum + "'" +
                                            ", [WorkspaceId] = " + ediQalityD96A.WorkspaceId +
                                            ", [ArticleId] = " + ediQalityD96A.ArticleId +
                                            ", [IsActive] = " + Convert.ToInt32(ediQalityD96A.IsActive) + " " +
                                            "WHERE ID=" + ediQalityD96A.Id + " ;";
                return strSql;
            }
        }
        /// <summary>
        ///             Update sql - String
        /// </summary>
        public string sql_Update_IsActive
        {
            get
            {
                string strSql = string.Empty;
                strSql = "Update EdiQualityD96A SET " +
                                            "IsActive=" + Convert.ToInt32(ediQalityD96A.IsActive) + " " +
                                            "WHERE ID=" + ediQalityD96A.Id + " ;";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------

        //public static bool ExistNewDelforCallToProceed(int myUserId, int myWorkspaceId)
        //{
        //    string strSql = "SELECT Id FROM EdiDelforD97AValue where IsActive=1 and WorkspaceId="+myWorkspaceId+";";
        //    bool bReturn = clsSQLCOM.ExecuteSQL_GetValueBool(strSql, myUserId);
        //    return bReturn;
        //}
    }
}

