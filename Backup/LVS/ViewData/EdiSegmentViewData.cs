using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class EdiSegmentViewData
    {
        public EdiSegments EdiSegment { get; set; }
        private int BenutzerID { get; set; }
        public List<EdiSegments> ListEdiSegments { get; set; } = new List<EdiSegments>();
        public Dictionary<string, EdiSegments> DictEdiSegment { get; set; } = new Dictionary<string, EdiSegments>();
        //internal clsSQLconComDiverse sqlComDiv { get; set; }

        public EdiSegmentViewData()
        {
            InitCls();
        }

        public EdiSegmentViewData(EdiSegments myEdiSegment)
        {
            this.EdiSegment = myEdiSegment;
        }

        public EdiSegmentViewData(int myId, int myUserId, bool mybInclSub) : this()
        {
            //InitCls();
            EdiSegment.Id = myId;
            BenutzerID = myUserId;
            if (EdiSegment.Id > 0)
            {
                Fill(mybInclSub);
            }
        }

        public EdiSegmentViewData(int myAsnArtId, int myUserId) : this()
        {
            //InitCls();
            EdiSegment.AsnArtId = myAsnArtId;
            BenutzerID = myUserId;
            if (EdiSegment.AsnArtId > 0)
            {
                FillbyAsnArtId();
            }
        }
        private void InitCls()
        {
            EdiSegment = new EdiSegments();
        }

        public void Fill(bool mybInclSub)
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegment");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, mybInclSub);
                }
            }
        }
        public void FillbyAsnArtId()
        {
            string strSQL = sql_Get_byAsnArtId;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegment");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, false);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillListAndDictEdiSegment()
        {
            DictEdiSegment = new Dictionary<string, EdiSegments>();
            ListEdiSegments = new List<EdiSegments>();

            string strSQL = sql_Get_ListbyAsnArtId;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegment");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    EdiSegmentViewData vd = new EdiSegmentViewData(iTmp, BenutzerID, true);
                    ListEdiSegments.Add(vd.EdiSegment);

                    string strKey = vd.EdiSegment.Id.ToString() + "#" + vd.EdiSegment.Name;
                    DictEdiSegment.Add(strKey, vd.EdiSegment);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="mybInclSub"></param>

        public void SetValue(DataRow row, bool mybInclSub)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            EdiSegment.Id = iTmp;

            if (EdiSegment.Id == 289)
            {
                string str = string.Empty;
            }

            iTmp = 0;
            int.TryParse(row["ASNArtId"].ToString(), out iTmp);
            EdiSegment.AsnArtId = iTmp;
            EdiSegment.Name = row["Name"].ToString();
            EdiSegment.Status = row["Status"].ToString();
            iTmp = 0;
            int.TryParse(row["RepeatCount"].ToString(), out iTmp);
            EdiSegment.RepeatCount = iTmp;
            iTmp = 0;
            int.TryParse(row["Ebene"].ToString(), out iTmp);
            EdiSegment.Ebene = iTmp;
            EdiSegment.Description = row["Description"].ToString();
            DateTime tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Created"].ToString(), out tmpDate);
            EdiSegment.Created = tmpDate;
            iTmp = 0;
            int.TryParse(row["tmpId"].ToString(), out iTmp);
            EdiSegment.tmpId = iTmp;
            EdiSegment.Storable = (bool)row["Storable"];
            EdiSegment.Code = row["Code"].ToString();
            iTmp = 0;
            int.TryParse(row["SortId"].ToString(), out iTmp);
            EdiSegment.SortId = iTmp;
            EdiSegment.IsActive = (bool)row["IsActive"];
            EdiSegment.EdiSegmentCheckFunction = row["EdiSegmentCheckFunction"].ToString();

            if (mybInclSub)
            {
                EdiSegmentElementViewData vd = new EdiSegmentElementViewData();
                var list = vd.GetEdiSegmentElementListByEdiSegmentId((int)EdiSegment.Id);
                EdiSegment.ListEdiSegmentElements = new List<EdiSegmentElements>(list);
            }
        }


        public void GetEdiElementList(bool mybInclSub)
        {
            ListEdiSegments = new List<EdiSegments>();

            string strSQL = sql_Get_Main;
            DataTable dt = new DataTable("EdiSegments");
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                EdiSegment = new EdiSegments();
                SetValue(dr, mybInclSub);
                ListEdiSegments.Add(EdiSegment);
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
                EdiSegment.Id = decTmp;
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
                if (EdiSegment.Name.Contains("'"))
                {
                    EdiSegment.Name = EdiSegment.Name.Replace("'", "");
                }
                if (EdiSegment.Description.Contains("'"))
                {
                    EdiSegment.Description = EdiSegment.Description.Replace("'", "");
                }
                string strSQL = "INSERT INTO EdiSegment ([ASNArtId],[Name],[Status],[RepeatCount],[Ebene],[Description] " +
                                                        ", [Created], [tmpId], [Storable], [Code], [SortId], [IsActive] " +
                                                        ", [EdiSegmentCheckFunction])" +
                                      "VALUES (" + EdiSegment.AsnArtId +
                                                ", '" + EdiSegment.Name + "'" +
                                                ", '" + EdiSegment.Status + "'" +
                                                ", " + EdiSegment.RepeatCount +
                                                ", " + EdiSegment.Ebene +
                                                ", '" + EdiSegment.Description + "'" +
                                                ", '" + EdiSegment.Created + "'" +
                                                ", " + EdiSegment.tmpId +
                                                ", " + Convert.ToInt32(EdiSegment.Storable) +
                                                ", '" + EdiSegment.Code + "'" +
                                                ", " + EdiSegment.SortId +
                                                ", " + Convert.ToInt32(EdiSegment.IsActive) +
                                                ", '" + EdiSegment.EdiSegmentCheckFunction + "' " +
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
                strSql = "SELECT * FROM EdiSegment WHERE ID=" + EdiSegment.Id + "; ";
                return strSql;
            }
        }
        /// <summary>
        ///             GET by ASNArtID
        /// </summary>
        public string sql_Get_byAsnArtId
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT TOP(1) * FROM EdiSegment WHERE AsnArtId=" + EdiSegment.AsnArtId + "; ";
                return strSql;
            }
        }
        /// <summary>
        ///             GETList by ASNArtID
        /// </summary>
        public string sql_Get_ListbyAsnArtId
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT " +
                                "DISTINCT s.* " +
                                "FROM EdiSegment s " +
                                    "INNER JOIN EdiSegmentElement se on s.ID = se.EdiSegmentId " +
                                    "INNER JOIN EdiSegmentElementField sef on se.Id = sef.EdiSemgentElementId " +
                                        "where " +
                                            "s.ASNArtId=" + EdiSegment.AsnArtId + ";";
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
                strSql = "SELECT * FROM EdiSegment";
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
                strSql = "DELETE EdiSegment WHERE Id=" + EdiSegment.Id;
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
                string strSQL = "Update EdiSegment SET " +
                                        "ASNArtId =" + EdiSegment.AsnArtId +
                                        ", Name ='" + EdiSegment.Name + "' " +
                                        ", Status ='" + EdiSegment.Status + "' " +
                                        ", RepeatCount =" + EdiSegment.RepeatCount +
                                        ", Ebene = " + EdiSegment.Ebene +
                                        ", Description= '" + EdiSegment.Description + "'" +
                                        ", Storable=" + Convert.ToInt32(EdiSegment.Storable) +
                                        ", Code ='" + EdiSegment.Code + "'" +
                                        ", SortId = " + EdiSegment.SortId +

                                            "WHERE ID=" + EdiSegment.Id + " ;";
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

        public static EdiSegments GetEdiSegmentValueToImport(clsSQLconComDiverse mySqlComDiv, int myId)
        {
            EdiSegmentViewData vd = new EdiSegmentViewData();
            string strSql = vd.sql_Get_Main + " WHERE ID=" + myId;
            DataTable dt = new DataTable("EdiSgement");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetEdiSegmentValueToImport", "EdiSegment", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.EdiSegment = new EdiSegments();
                vd.SetValue(dr, false);
            }
            return vd.EdiSegment;
        }

        public static List<EdiSegments> GetEdiSegmentsByAsnArtIdToImport(clsSQLconComDiverse mySqlComDiv, int myAsnArtId)
        {
            List<EdiSegments> retList = new List<EdiSegments>();
            EdiSegmentViewData vd = new EdiSegmentViewData();
            string strSql = vd.sql_Get_Main + " WHERE ASNArtId=" + myAsnArtId;
            DataTable dt = new DataTable("EdiSgement");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "EdiSegmentValue", "EdiSegment", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.EdiSegment = new EdiSegments();
                vd.SetValue(dr, false);
                if (vd.EdiSegment.Id > 0)
                {
                    vd.EdiSegment.ListEdiSegmentElements = new List<EdiSegmentElements>();
                    vd.EdiSegment.ListEdiSegmentElements = EdiSegmentElementViewData.GetEdiSegmentElementListByEdiSegmentIdToImport(mySqlComDiv, (int)vd.EdiSegment.Id);
                    //EdiSegmentViewData.GetEdiSegmentsByAsnArtIdToImport(mySqlComDiv, (int)vd.EdiSegment.AsnArtId);
                }
                retList.Add(vd.EdiSegment);
            }
            return retList;
        }
    }
}

