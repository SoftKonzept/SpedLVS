using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdiSegmentElementViewData
    {
        public EdiSegmentElements EdiSegmentElement { get; set; }
        private int BenutzerID { get; set; }
        public List<EdiSegmentElements> ListEdiSegmentElements { get; set; }
        //internal clsSQLconComDiverse sqlComDiv { get; set; }

        public EdiSegmentElementViewData()
        {
            InitCls();
        }

        public EdiSegmentElementViewData(EdiSegmentElements myEdiSegmentElement)
        {
            this.EdiSegmentElement = myEdiSegmentElement;
        }

        public EdiSegmentElementViewData(int myId, int myUserId, bool mybInclSub) : this()
        {
            //InitCls();
            EdiSegmentElement.Id = myId;
            BenutzerID = myUserId;
            if (EdiSegmentElement.Id > 0)
            {
                Fill(mybInclSub);
            }
        }
        private void InitCls()
        {
            EdiSegmentElement = new EdiSegmentElements();
        }

        public void Fill(bool mybInclSub)
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegmentElement");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, mybInclSub);
                }
            }
        }

        public void SetValue(DataRow row, bool mybInclSub)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            EdiSegmentElement.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["EdiSegmentId"].ToString(), out iTmp);
            EdiSegmentElement.EdiSegmentId = iTmp;
            EdiSegmentElement.Name = row["Name"].ToString();
            if (EdiSegmentElement.Name.Contains("'"))
            {
                EdiSegmentElement.Name = EdiSegmentElement.Name.Replace("'", "");
            }
            EdiSegmentElement.Description = row["Description"].ToString();
            if (EdiSegmentElement.Description.Contains("'"))
            {
                EdiSegmentElement.Description = EdiSegmentElement.Description.Replace("'", "");
            }
            iTmp = 0;
            int.TryParse(row["Position"].ToString(), out iTmp);
            EdiSegmentElement.Position = iTmp;
            DateTime tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Created"].ToString(), out tmpDate);
            EdiSegmentElement.Created = tmpDate;
            iTmp = 0;
            int.TryParse(row["tmpId"].ToString(), out iTmp);
            EdiSegmentElement.tmpId = iTmp;
            EdiSegmentElement.Code = row["Code"].ToString();
            iTmp = 0;
            int.TryParse(row["SortId"].ToString(), out iTmp);
            EdiSegmentElement.SortId = iTmp;
            EdiSegmentElement.Kennung = row["Kennung"].ToString();
            EdiSegmentElement.IsActive = true;
            if (row.Table.Columns.Contains("IsActive"))
            {
                EdiSegmentElement.IsActive = (bool)row["IsActive"];
            }

            if (mybInclSub)
            {
                EdiSegmentElementFieldViewData vd = new EdiSegmentElementFieldViewData();
                var list = vd.GetEdiSegmentElementFieldListByEdiSegmentElementId((int)EdiSegmentElement.Id);
                EdiSegmentElement.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>(list);
            }
        }


        public List<EdiSegmentElements> GetEdiSegmentElementListByEdiSegmentId(int myEdiSegmentId)
        {
            ListEdiSegmentElements = new List<EdiSegmentElements>();
            EdiSegmentElement = new EdiSegmentElements();
            EdiSegmentElement.EdiSegmentId = myEdiSegmentId;

            string strSQL = sql_Get_Main + " Where EdiSegmentId=" + EdiSegmentElement.EdiSegmentId;
            DataTable dt = new DataTable("EdiSegmentElements");
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                EdiSegmentElement = new EdiSegmentElements();
                SetValue(dr, true);
                ListEdiSegmentElements.Add(EdiSegmentElement);
            }
            return ListEdiSegmentElements;
        }

        public void GetEdiSegmentElementList(bool mybInclSub)
        {
            ListEdiSegmentElements = new List<EdiSegmentElements>();

            string strSQL = sql_Get_Main;
            DataTable dt = new DataTable("EdiSegmentElements");
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                EdiSegmentElement = new EdiSegmentElements();
                SetValue(dr, mybInclSub);
                ListEdiSegmentElements.Add(EdiSegmentElement);
            }
        }

        /// <summary>
        ///             ADD
        /// </summary>
        public void Add()
        {
            string strSQL = sql_Add;
            strSQL = strSQL + " Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                EdiSegmentElement.Id = decTmp;
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
                string strSQL = "INSERT INTO [EdiSegmentElement] ([EdiSegmentId], [Name], [Description] , [Position] " +
                                                             ", [Created], [tmpId], [Code], [SortId], [Kennung], [IsActive]) " +
                                  "VALUES (" + EdiSegmentElement.EdiSegmentId +
                                            ",'" + EdiSegmentElement.Name + "'" +
                                            ",'" + EdiSegmentElement.Description + "'" +
                                            ", " + EdiSegmentElement.Position +
                                            ",'" + EdiSegmentElement.Created + "'" +
                                            ", " + EdiSegmentElement.tmpId +
                                            ",'" + EdiSegmentElement.Code + "'" +
                                            ", " + EdiSegmentElement.SortId +
                                            ",'" + EdiSegmentElement.Kennung + "'" +
                                            ", " + Convert.ToInt32(EdiSegmentElement.IsActive) +

                                         ")";
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
                strSql = "SELECT * FROM EdiSegmentElement WHERE ID=" + EdiSegmentElement.Id + ";";
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
                strSql = "SELECT * FROM EdiSegmentElement ";
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
                strSql = "DELETE EdiSegmentElement WHERE Id=" + EdiSegmentElement.Id;
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
                string strSQL = "Update EdiSegmentElement SET " +
                                        "EdiSegmentId =" + EdiSegmentElement.EdiSegmentId +
                                        ", Decription ='" + EdiSegmentElement.Description + "' " +
                                        ", Name= '" + EdiSegmentElement.Name + "'" +
                                        ", Position =" + EdiSegmentElement.Position +
                                        ", Code = '" + EdiSegmentElement.Code + "'" +
                                        ", SortId = " + EdiSegmentElement.SortId +
                                        ", Kennung = '" + EdiSegmentElement.Kennung + "'" +
                                        ", IsActvie = " + Convert.ToInt32(EdiSegmentElement.IsActive) +
                                        "WHERE ID=" + EdiSegmentElement.Id + " ;";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------

        public static List<EdiSegmentElements> GetEdiSegmentElementListByEdiSegmentIdToImport(clsSQLconComDiverse mySqlComDiv, int myEdiSegmentId)
        {
            List<EdiSegmentElements> retList = new List<EdiSegmentElements>();
            EdiSegmentElementViewData vd = new EdiSegmentElementViewData();
            string strSql = vd.sql_Get_Main + " WHERE EdiSegmentId=" + myEdiSegmentId;
            DataTable dt = new DataTable("EdiSegmentElements");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "EdiSegmentElementList", "EdiSegmentElement", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.EdiSegmentElement = new EdiSegmentElements();
                vd.SetValue(dr, false);
                if (vd.EdiSegmentElement.Id > 0)
                {
                    vd.EdiSegmentElement.ListEdiSegmentElementFields = EdiSegmentElementFieldViewData.GetEdiSegmentElementFieldListByEdiSegmentIdToImport(mySqlComDiv, (int)vd.EdiSegmentElement.Id);
                }
                retList.Add(vd.EdiSegmentElement);
            }
            return retList;
        }

    }
}

