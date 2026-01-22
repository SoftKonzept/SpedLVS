using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class CustomProcessExceptionsViewData
    {
        public LVS.Models.CustomProcessExceptions CustomProcessException { get; set; }
        public List<LVS.Models.CustomProcessExceptions> ListCustomProcessExpeptions { get; set; }


        public DataTable dtCustomProcessExceptions { get; set; }
        public Globals._GL_USER GL_USER { get; set; }
        private int _BenutzerID;
        public int BenutzerID
        {
            get
            {
                if (GL_USER.User_ID > 0)
                {
                    _BenutzerID = (int)GL_USER.User_ID;
                }
                return _BenutzerID;
            }
            set
            {
                _BenutzerID = value;
            }
        }

        public CustomProcessExceptionsViewData()
        {
            InitCls();
        }
        public CustomProcessExceptionsViewData(Globals._GL_USER myGLUser)
        {
            this.GL_USER = myGLUser;
            InitCls();
        }

        public CustomProcessExceptionsViewData(int myCustomProcessId)
        {
            InitCls();
            if (myCustomProcessId > 0)
            {
                CustomProcessException.CustomProcessId = myCustomProcessId;
            }
        }
        public CustomProcessExceptionsViewData(CustomProcessExceptions myCPException)
        {
            InitCls();
            this.CustomProcessException = myCPException.Copy();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            CustomProcessException = new LVS.Models.CustomProcessExceptions();
            ListCustomProcessExpeptions = new List<LVS.Models.CustomProcessExceptions>();
            FillList(true);
        }

        public void FillList(bool mybInclSub)
        {
            ListCustomProcessExpeptions = new List<LVS.Models.CustomProcessExceptions>();
            string strSQL = sql_Get_Main;
            DataTable dtCustomProcessExceptions = new DataTable();
            dtCustomProcessExceptions = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "CustomProcessException");
            if (dtCustomProcessExceptions.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCustomProcessExceptions.Rows)
                {
                    CustomProcessException = new CustomProcessExceptions();
                    SetValue(dr);
                    ListCustomProcessExpeptions.Add(CustomProcessException);
                }
            }
        }

        public void FillListByCustomProcessId()
        {
            ListCustomProcessExpeptions = new List<LVS.Models.CustomProcessExceptions>();
            string strSQL = sql_Get_Main;
            strSQL += " WHERE CustomProcessId=" + CustomProcessException.CustomProcessId;
            DataTable dtCustomProcessExceptions = new DataTable();
            dtCustomProcessExceptions = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "CustomProcessException");
            if (dtCustomProcessExceptions.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCustomProcessExceptions.Rows)
                {
                    CustomProcessException = new CustomProcessExceptions();
                    SetValue(dr);
                    ListCustomProcessExpeptions.Add(CustomProcessException);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "CustomProcessExceptions");
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
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            CustomProcessException.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["CustomProcessId"].ToString(), out iTmp);
            CustomProcessException.CustomProcessId = iTmp;

            iTmp = 0;
            int.TryParse(row["GoodsTypeId"].ToString(), out iTmp);
            CustomProcessException.GoodsTypeId = iTmp;

            if (CustomProcessException.CustomProcessId > 0)
            {
                CustomProcessesViewData cpVD = new CustomProcessesViewData(CustomProcessException.CustomProcessId, this.BenutzerID, true);
                this.CustomProcessException.CustomProcess = cpVD.CustomProcess.Copy();
            }
            if (CustomProcessException.GoodsTypeId > 0)
            {
                GoodstypeViewData gtVD = new GoodstypeViewData(CustomProcessException.GoodsTypeId, this.BenutzerID, false);
                this.CustomProcessException.GoodsType = gtVD.Gut.Copy();
            }

            DateTime dtCreatd = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Created"].ToString(), out dtCreatd);
            CustomProcessException.Created = dtCreatd;
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
                CustomProcessException.Id = iTmp;
            }
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool Delete()
        {
            string strSql = sql_Delete;
            bool retVal = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return retVal;
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
        /// <returns></returns>
        private DataTable GetList()
        {
            ListCustomProcessExpeptions = new List<LVS.Models.CustomProcessExceptions>();
            string strSQL = sql_GetListActive;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "CustomProcessExceptions");
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
                //CustomProcess.Created = DateTime.Now;
                string strSQL = "INSERT INTO [CustomProcessExceptions] ([CustomProcessId], [GoodsTypeId], [Created]) " +
                                                                    "VALUES (" + CustomProcessException.CustomProcessId +
                                                                            ", " + CustomProcessException.GoodsTypeId +
                                                                            ", '" + CustomProcessException.Created.ToString() + "'" +
                                                                            "); ";
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
                strSql = "SELECT * FROM CustomProcessExceptions WHERE Id=" + CustomProcessException.Id + "; ";
                return strSql;
            }
        }

        /// <summary>
        ///             GET List
        /// </summary>
        public string sql_GetListActive
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM CustomProcessExceptions ";
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
                strSql = "SELECT * FROM CustomProcessExceptions ";
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
                strSql = "DELETE CustomProcessExceptions WHERE Id =" + CustomProcessException.Id + "; ";
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
                strSql = "Update CustomProcessExceptions SET " +
                                            " [CustomProcessId] = " + CustomProcessException.CustomProcessId +
                                            ", [GoodsTypeId]= " + CustomProcessException.GoodsTypeId +

                                            "WHERE Id=" + CustomProcessException.Id + " ;";
                return strSql;
            }
        }

        public static List<CustomProcessExceptions> GetListCustomerProcessExceptionByCustomerProcessId(int myCustomerProcessId)
        {
            CustomProcessExceptionsViewData cpVP = new CustomProcessExceptionsViewData();
            List<CustomProcessExceptions> retList = cpVP.ListCustomProcessExpeptions.Where(x => x.CustomProcessId == myCustomerProcessId).ToList();
            return retList;
        }
    }
}

