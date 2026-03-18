using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdiSegmentElementFieldViewData
    {
        public EdiSegmentElementFields EdiSegmentElementField { get; set; }
        private int BenutzerID { get; set; }
        public List<EdiSegmentElementFields> ListEdiSegmentElementFields { get; set; }

        public EdiSegmentElementFieldViewData()
        {
            InitCls();
        }

        public EdiSegmentElementFieldViewData(EdiSegmentElementFields myEdiSegmentElementField)
        {
            this.EdiSegmentElementField = myEdiSegmentElementField;
        }

        public EdiSegmentElementFieldViewData(int myId, int myUserId, bool mybInclSub) : this()
        {
            //InitCls();
            EdiSegmentElementField.Id = myId;
            BenutzerID = myUserId;
            if (EdiSegmentElementField.Id > 0)
            {
                Fill(mybInclSub);
            }
        }
        private void InitCls()
        {
            EdiSegmentElementField = new EdiSegmentElementFields();
        }

        public void Fill(bool mybInclSub)
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegmentElementField");
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
            EdiSegmentElementField.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["EdiSegmentId"].ToString(), out iTmp);
            EdiSegmentElementField.EdiSemgentElementId = iTmp;
            EdiSegmentElementField.Shortcut = row["Shorcut"].ToString();

            EdiSegmentElementField.Name = row["Name"].ToString();
            if (EdiSegmentElementField.Name.Contains("'"))
            {
                EdiSegmentElementField.Name = EdiSegmentElementField.Name.Replace("'", "");
            }

            EdiSegmentElementField.Status = row["Status"].ToString();
            EdiSegmentElementField.Format = row["Format"].ToString();
            EdiSegmentElementField.Description = row["Description"].ToString();
            if (EdiSegmentElementField.Description.Contains("'"))
            {
                EdiSegmentElementField.Description = EdiSegmentElementField.Description.Replace("'", "");
            }
            EdiSegmentElementField.constValue = row["constValue"].ToString();

            iTmp = 0;
            int.TryParse(row["Position"].ToString(), out iTmp);
            EdiSegmentElementField.Position = iTmp;

            DateTime tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Created"].ToString(), out tmpDate);
            EdiSegmentElementField.Created = tmpDate;

            EdiSegmentElementField.FormatString = row["FormatString"].ToString();
            EdiSegmentElementField.Code = row["Code"].ToString();
            iTmp = 0;
            int.TryParse(row["SortId"].ToString(), out iTmp);
            EdiSegmentElementField.SortId = iTmp;
            EdiSegmentElementField.Kennung = row["Kennung"].ToString();


            iTmp = 0;
            int.TryParse(row["EdiSegmentId"].ToString(), out iTmp);
            EdiSegmentElementField.EdiSegmentId = iTmp;

            iTmp = 0;
            int.TryParse(row["ASNArtId"].ToString(), out iTmp);
            EdiSegmentElementField.AsnArtId = iTmp;

            if (mybInclSub)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public List<EdiSegmentElementFields> GetEdiSegmentElementFieldListByEdiSegmentElementId(int myEdiSegmentElementId)
        {
            EdiSegmentElementField = new EdiSegmentElementFields();
            EdiSegmentElementField.EdiSemgentElementId = myEdiSegmentElementId;

            ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            string strSQL = sql_Get_Main + " WHERE EdiSemgentElementId=" + EdiSegmentElementField.EdiSemgentElementId;
            DataTable dt = new DataTable("EdiSegmentElementFields");
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                EdiSegmentElementField = new EdiSegmentElementFields();
                SetValue(dr, false);
                ListEdiSegmentElementFields.Add(EdiSegmentElementField);
            }
            return ListEdiSegmentElementFields;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mybInclSub"></param>
        public void GetEdiSegmentElementList(bool mybInclSub)
        {
            ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            string strSQL = sql_Get_Main;
            DataTable dt = new DataTable("EdiSegmentElementFields");
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                EdiSegmentElementField = new EdiSegmentElementFields();
                SetValue(dr, mybInclSub);
                ListEdiSegmentElementFields.Add(EdiSegmentElementField);
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
                EdiSegmentElementField.Id = decTmp;
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
                string strSQL = "INSERT INTO[dbo].[EdiSegmentElementField] ([EdiSemgentElementId], [Shorcut], [Name], [Status]" +
                                                                            ", [Format], [Description], [constValue], [Position], [Created] " +
                                                                            ", [FormatString], [Code], [SortId], [Kennung], [EdiSegmentId] " +
                                                                            ", [ASNArtId]) " +
                                  "VALUES (" + EdiSegmentElementField.EdiSemgentElementId +
                                            ",'" + EdiSegmentElementField.Shortcut + "'" +
                                            ",'" + EdiSegmentElementField.Name + "'" +
                                            ",'" + EdiSegmentElementField.Status + "'" +
                                            ",'" + EdiSegmentElementField.Format + "'" +
                                            ",'" + EdiSegmentElementField.Description + "'" +
                                            ",'" + EdiSegmentElementField.constValue + "'" +
                                            ", " + EdiSegmentElementField.Position +
                                            ",'" + EdiSegmentElementField.Created + "'" +
                                            ",'" + EdiSegmentElementField.FormatString + "'" +
                                            ",'" + EdiSegmentElementField.Code + "'" +
                                            ", " + EdiSegmentElementField.SortId +
                                            ",'" + EdiSegmentElementField.Kennung + "'" +
                                            ", " + EdiSegmentElementField.EdiSegmentId +
                                            ", " + EdiSegmentElementField.AsnArtId +
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
                strSql = "SELECT * FROM EdiSegmentElementField WHERE ID=" + EdiSegmentElementField.Id + ";";
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
                strSql = "SELECT * FROM EdiSegmentElementField ";
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
                strSql = "DELETE EdiSegmentElementField WHERE Id=" + EdiSegmentElementField.Id;
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
                string strSQL = "Update EdiSegmentElementField SET " +
                                        "EdiSemgentElementId =" + EdiSegmentElementField.EdiSemgentElementId +
                                        ", Shorcut = '" + EdiSegmentElementField.Shortcut + "' " +
                                        ", Name = '" + EdiSegmentElementField.Name + "'" +
                                        ", Status = '" + EdiSegmentElementField.Status + "'" +
                                        ", Format = '" + EdiSegmentElementField.Format + "'" +
                                        ", Decription ='" + EdiSegmentElementField.Description + "' " +
                                        ", constValue ='" + EdiSegmentElementField.constValue + "' " +
                                        ", Position =" + EdiSegmentElementField.Position +
                                        ", FormatString = '" + EdiSegmentElementField.FormatString + "'" +
                                        ", Code = '" + EdiSegmentElementField.Code + "'" +
                                        ", SortId = " + EdiSegmentElementField.SortId +
                                        ", Kennung = '" + EdiSegmentElementField.Kennung + "'" +
                                        ", EdiSegmentId = " + EdiSegmentElementField.EdiSegmentId +
                                        ", ASNArtId = " + EdiSegmentElementField.AsnArtId +

                                        "WHERE ID=" + EdiSegmentElementField.Id + " ;";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------

        public static List<EdiSegments> GetEdiSegmentListToImport(clsSQLconComDiverse mySqlComDiv)
        {
            List<EdiSegments> ListEdiSegments = new List<EdiSegments>();

            EdiSegmentViewData vd = new EdiSegmentViewData();
            string strSql = vd.sql_Get_Main;
            DataTable dt = new DataTable("EdiSegment");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetEdiSegmentListToImport", "ListEdiSegments", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.EdiSegment = new EdiSegments();
                vd.SetValue(dr, false);
                ListEdiSegments.Add(vd.EdiSegment);
            }
            return ListEdiSegments;
        }

        public static EdiSegmentElementFields GetEdiSegmentElementFieldValueToImport(clsSQLconComDiverse mySqlComDiv, int myId)
        {
            EdiSegmentElementFieldViewData vd = new EdiSegmentElementFieldViewData();
            string strSql = vd.sql_Get_Main + " WHERE ID=" + myId;
            DataTable dt = new DataTable("EdiSegmentElementField");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetEdiSegmentElementFieldValueToImport", "EdiSegmentElementField", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.EdiSegmentElementField = new EdiSegmentElementFields();
                vd.SetValue(dr, false);
            }
            return vd.EdiSegmentElementField;
        }

        public static List<EdiSegmentElementFields> GetEdiSegmentElementFieldListByEdiSegmentIdToImport(clsSQLconComDiverse mySqlComDiv, int myEdiSegmentElementId)
        {
            List<EdiSegmentElementFields> retList = new List<EdiSegmentElementFields>();
            EdiSegmentElementFieldViewData vd = new EdiSegmentElementFieldViewData();
            string strSql = vd.sql_Get_Main + " WHERE EdiSemgentElementId=" + myEdiSegmentElementId;
            DataTable dt = new DataTable("EdiSegmentElements");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "EdiSegmentElementField", "EdiSegmentElementField", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.EdiSegmentElementField = new EdiSegmentElementFields();
                vd.SetValue(dr, false);
                if (vd.EdiSegmentElementField.Id > 0)
                {
                    retList.Add(vd.EdiSegmentElementField);  //EdiSegmentElementFieldViewData.GetEdiSegmentElementFieldValueToImport(mySqlComDiv, (int)vd.AsnArt.Id);
                }
            }
            return retList;
        }


    }
}

