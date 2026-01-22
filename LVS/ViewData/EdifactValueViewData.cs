using LVS.Models;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdifactValueViewData
    {
        public WorkspaceViewData workspaceVD { get; set; }
        public EdifactValue edifactValue { get; set; }
        private int BenutzerID { get; set; } = 1;

        public EdifactValueViewData()
        {
            InitCls();
        }
        public EdifactValueViewData(EdifactValue myEdifactValue)
        {
            this.edifactValue = myEdifactValue;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitCls()
        {
            edifactValue = new EdifactValue();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void Fill()
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "edifactValue");
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
            edifactValue = new EdifactValue();

            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            edifactValue.Id = iTmp;

            iTmp = 0;
            int.TryParse(row["AsnId"].ToString(), out iTmp);
            edifactValue.AsnId = iTmp;

            edifactValue.EdiSegmentElement = row["EdiSegmentElement"].ToString();
            edifactValue.EdiSegmentElementValue = row["EdiSegmentElementValue"].ToString();
            edifactValue.Property = row["Property"].ToString();

            iTmp = 0;
            int.TryParse(row["OrderId"].ToString(), out iTmp);
            edifactValue.OrderId = iTmp;
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
                edifactValue.Id = iTmp;
            }
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public void Delete()
        {
        }

        public bool DeleteByAsnId(Asn myAsn)
        {
            string strSQL = "DELETE FROM EdifactValue WHERE AsnId=" + myAsn.Id + "; ";
            return clsSQLCOM.ExecuteSQLWithTRANSACTION(strSQL, "DeleteEdifactValueByAsnId", 1);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myList"></param>
        /// <returns></returns>
        public bool AddStringList(List<string> myList)
        {
            bool retBool = false;
            string strSql = string.Empty;
            foreach (string str in myList)
            {
                strSql += str;
            }
            retBool = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "ListInsert", 1);
            return retBool;
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
                string strSQL = "INSERT INTO EdifactValue ([AsnId], [EdiSegmentElement], [EdiSegmentElementValue], [Property], [OrderId])" +
                                                            "VALUES " +
                                                            "(" +
                                                                 edifactValue.AsnId +
                                                                 ", '" + edifactValue.EdiSegmentElement + "'" +
                                                                 ", '" + edifactValue.EdiSegmentElementValue + "'" +
                                                                 ", '" + edifactValue.Property + "'" +
                                                                 ", " + edifactValue.OrderId +
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
                strSql = "SELECT * FROM EdifactValue WHERE ID=" + edifactValue.Id + "; ";
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
                strSql = "SELECT * FROM EdifactValue WHERE IsActive=1; ";
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
                strSql = "SELECT * FROM EdifactValue";
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
                strSql = "Update EdifactValue SET " +
                                            " [AsnId] = " + edifactValue.AsnId +
                                            ",[EdiSegmentElement] = '" + edifactValue.EdiSegmentElement + "'" +
                                            ",[EdiSegmentElementValue]  = '" + edifactValue.EdiSegmentElementValue + "'" +
                                            ",[Property] = '" + edifactValue.Property + "'" +
                                            ",[OrderId] = " + edifactValue.OrderId +

                                            "WHERE ID=" + edifactValue.Id + " ;";
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

