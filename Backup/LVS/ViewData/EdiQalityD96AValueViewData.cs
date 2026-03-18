using LVS.Models;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdiQalityD96AValueViewData
    {
        public WorkspaceViewData workspaceVD { get; set; }
        public _EdiQalityD96AValue ediQalityD96AValue { get; set; }
        private int BenutzerID { get; set; } = 1;

        public EdiQalityD96AValueViewData()
        {
            InitCls();
        }
        public EdiQalityD96AValueViewData(_EdiQalityD96AValue myEdiQualityD96AValue)
        {
            this.ediQalityD96AValue = myEdiQualityD96AValue;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            ediQalityD96AValue = new _EdiQalityD96AValue();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiQualityD96AValue");
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
            ediQalityD96AValue = new _EdiQalityD96AValue();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            ediQalityD96AValue.Id = iTmp;

            iTmp = 0;
            int.TryParse(row["EdiQualityId"].ToString(), out iTmp);
            ediQalityD96AValue.EdiQalityId = iTmp;

            ediQalityD96AValue.EdiSegmentElement = row["EdiSegmentElement"].ToString();
            ediQalityD96AValue.EdiSegmentElementValue = row["EdiSegmentElementValue"].ToString();
            ediQalityD96AValue.Property = row["Property"].ToString();
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
                ediQalityD96AValue.Id = iTmp;
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
                string strSQL = "INSERT INTO EdiQalityD96AValue ([EdiQalityId], [EdiSegmentElement], [EdiSegmentElementValue], [Property])" +
                                                            "VALUES " +
                                                            "(" +
                                                                 ediQalityD96AValue.EdiQalityId +
                                                                 ", '" + ediQalityD96AValue.EdiSegmentElement + "'" +
                                                                 ", '" + ediQalityD96AValue.EdiSegmentElementValue + "'" +
                                                                 ", '" + ediQalityD96AValue.Property + "'" +
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
                strSql = "SELECT * FROM EdiQalityD96AValue WHERE ID=" + ediQalityD96AValue.Id + "; ";
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
                strSql = "SELECT * FROM EdiQalityD96AValue WHERE IsActive=1; ";
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
                strSql = "SELECT * FROM EdiQalityD96AValue";
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
                strSql = "Update EdiQalityD96AValue SET " +
                                            " [EdiQalityId] = " + ediQalityD96AValue.EdiQalityId +
                                            ",[EdiSegmentElement] = '" + ediQalityD96AValue.EdiSegmentElement + "'" +
                                            ",[EdiSegmentElementValue]  = '" + ediQalityD96AValue.EdiSegmentElementValue + "'" +
                                            ",[Property] = '" + ediQalityD96AValue.Property + "'" +

                                            "WHERE ID=" + ediQalityD96AValue.Id + " ;";
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

