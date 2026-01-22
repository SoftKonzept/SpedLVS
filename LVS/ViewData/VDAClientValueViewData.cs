using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class VDAClientValueViewData
    {
        public VDAClientValues VdaClientValue { get; set; }
        private int BenutzerID { get; set; }
        public List<VDAClientValues> ListVdaClientValues { get; set; }

        public VDAClientValueViewData()
        {
            InitCls();
        }

        public VDAClientValueViewData(VDAClientValues myVdaClientValue)
        {
            this.VdaClientValue = myVdaClientValue;
        }

        public VDAClientValueViewData(int myId, int myUserId, bool mybInclSub) : this()
        {
            //InitCls();
            VdaClientValue.Id = myId;
            BenutzerID = myUserId;
            if (VdaClientValue.Id > 0)
            {
                Fill(mybInclSub);
            }
        }
        private void InitCls()
        {
            VdaClientValue = new VDAClientValues();
            ListVdaClientValues = new List<VDAClientValues>();
        }

        public void Fill(bool mybInclSub)
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClientValue");
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
            VdaClientValue.Id = iTmp;
            iTmp = 0;
            int.TryParse(row["AdrID"].ToString(), out iTmp);
            VdaClientValue.AdrId = iTmp;
            iTmp = 0;
            int.TryParse(row["ASNFieldID"].ToString(), out iTmp);
            VdaClientValue.AsnFieldId = iTmp;

            VdaClientValue.ValueArt = row["ValueArt"].ToString();
            VdaClientValue.Value = row["Value"].ToString();

            VdaClientValue.Fill = (bool)row["Fill0"];
            VdaClientValue.Activ = (bool)row["aktiv"];
            iTmp = 0;
            Int32.TryParse(row["NextSatz"].ToString(), out iTmp);
            VdaClientValue.NextSatz = iTmp;
            VdaClientValue.IsArtSatz = (bool)row["ArtSatz"];
            VdaClientValue.FillValue = row["FillValue"].ToString();
            VdaClientValue.FillLeft = (bool)row["FillLeft"];
            iTmp = 0;
            int.TryParse(row["ASNArtId"].ToString(), out iTmp);
            VdaClientValue.ASNArtId = iTmp;
            VdaClientValue.Kennung = row["Kennung"].ToString();

            if (mybInclSub)
            {

            }
        }


        public void GetEdiSegmentElementList(bool mybInclSub)
        {
            //ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            //string strSQL = sql_Get_Main;
            //DataTable dt = new DataTable("EdiSegmentElementFields");          
            //dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            //foreach (DataRow dr in dt.Rows)
            //{
            //    EdiSegmentElementField = new EdiSegmentElementFields();
            //    SetValue(dr, mybInclSub);
            //    ListEdiSegmentElementFields.Add(EdiSegmentElementField);
            //}
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
                VdaClientValue.Id = decTmp;
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
        ///             DELETE
        /// </summary>
        public bool DeleteByEdiSegementElementFieldId(int myEdiSegemntElementFieldId)
        {
            string strSql = "DELETE VDAClientOUT WHERE ASNFieldID=" + myEdiSegemntElementFieldId;
            bool retVal = clsSQLCOM.ExecuteSQL(strSql, BenutzerID);
            return retVal;
        }
        /// <summary>
        ///             DELETE
        /// </summary>
        public bool DeleteByAsnArtId(int myAsnArtId)
        {
            string strSql = "DELETE VDAClientOUT WHERE ASNArtId=" + myAsnArtId;
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
                string strSQL = "INSERT INTO VDAClientOUT ([AdrID], [ASNFieldID], [ValueArt], [Value], [Fill0], [aktiv] " +
                                                ", [NextSatz], [ArtSatz], [FillValue], [FillLeft], [ASNArtId] " +
                                                ", [Kennung], [Description] " +

                                                ") VALUES (" +
                                                    VdaClientValue.AdrId +
                                                    ", " + VdaClientValue.AsnFieldId +
                                                    ", '" + VdaClientValue.ValueArt + "'" +
                                                    ", '" + VdaClientValue.Value + "'" +
                                                    ", " + Convert.ToInt32(VdaClientValue.Fill) +
                                                    ", " + Convert.ToInt32(VdaClientValue.Activ) +
                                                    ", " + VdaClientValue.NextSatz +
                                                    ", " + Convert.ToInt32(VdaClientValue.IsArtSatz) +
                                                    ", '" + VdaClientValue.FillValue + "'" +
                                                    ", " + Convert.ToInt32(VdaClientValue.FillLeft) +
                                                    ", " + Convert.ToInt32(VdaClientValue.ASNArtId) +
                                                    ", '" + VdaClientValue.Kennung + "' " +
                                                    ", '" + VdaClientValue.Description + "'" +
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
                strSql = "SELECT * FROM VDAClientOUT WHERE ID=" + VdaClientValue.Id + ";";
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
                strSql = "SELECT * FROM VDAClientOUT ";
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
                strSql = "SELECT * FROM VDAClientOUT WHERE Id=" + VdaClientValue.Id;
                return strSql;
            }
        }
        /// <summary>
        ///             DELETE sql - String
        /// </summary>
        public string sql_DeleteByEdiSegemntElementFieldId
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

                strSql = "Update VDAClientOUT SET " +
                        "AdrID = " + VdaClientValue.AdrId +
                        ", ASNFieldID = " + VdaClientValue.AsnFieldId +
                        ", ValueArt = '" + VdaClientValue.ValueArt + "'" +
                        ", Value = '" + VdaClientValue.Value + "'" +
                        ", Fill0 = " + Convert.ToInt32(VdaClientValue.Fill) +
                        ", aktiv = " + Convert.ToInt32(VdaClientValue.Activ) +
                        ", NextSatz =" + VdaClientValue.NextSatz +
                        ", ArtSatz = " + Convert.ToInt32(VdaClientValue.IsArtSatz) +
                        ", FillValue ='" + VdaClientValue.FillValue + "'" +
                        ", FillLeft =" + Convert.ToInt32(VdaClientValue.FillLeft) +
                        ", ASNArtId = " + VdaClientValue.ASNArtId +
                        ", Kennung = '" + VdaClientValue.Kennung + "' " +
                        ", Description = '" + VdaClientValue.Description + "'" +

                        " where Id = " + VdaClientValue.Id + ";";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------

        public static VDAClientValues GetVdaClientValueToImport(clsSQLconComDiverse mySqlComDiv, int myAdrId, int myAsnFieldId)
        {
            VDAClientValueViewData vd = new VDAClientValueViewData();
            string strSql = vd.sql_Get_Main + " WHERE AdrID=" + myAdrId + " and ASNFieldID=" + myAsnFieldId;
            DataTable dt = new DataTable("VDAClientValueViewData");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "VDAClientValue", "VDAClient", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.VdaClientValue = new VDAClientValues();
                vd.SetValue(dr, false);
            }
            return vd.VdaClientValue;
        }

        public static VDAClientValues GetVdaClientValueToImport(clsSQLconComDiverse mySqlComDiv, int mySourceId)
        {
            VDAClientValueViewData vd = new VDAClientValueViewData();
            string strSql = vd.sql_Get_Main + " WHERE Id=" + mySourceId;
            DataTable dt = new DataTable("VDAClientValue");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "VDAClientValue", "VDAClient", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.VdaClientValue = new VDAClientValues();
                vd.SetValue(dr, false);
            }
            return vd.VdaClientValue;
        }


        public static VDAClientValueViewData GetVdaClientValueListbyAdrId(int myAdrId, int myAsnArtId)
        {
            VDAClientValueViewData vd = new VDAClientValueViewData();

            string strSql = vd.sql_Get_Main + " WHERE AdrId=" + myAdrId;
            if (myAsnArtId > 0)
            {
                strSql += " AND ASNArtId=" + myAsnArtId;
            }
            DataTable dt = new DataTable("VDAClientValue");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "VDAClientValue", "VDAClient", 1);
            foreach (DataRow dr in dt.Rows)
            {
                vd.VdaClientValue = new VDAClientValues();
                vd.SetValue(dr, false);
                if (!vd.ListVdaClientValues.Contains(vd.VdaClientValue))
                {
                    vd.ListVdaClientValues.Add(vd.VdaClientValue);
                }
            }
            return vd;
        }

        public static bool CopyShapebyAdrId(int mySourceId, int myDestId, int myAsnArtId)
        {
            bool bReturn = false;
            VDAClientValueViewData vd = VDAClientValueViewData.GetVdaClientValueListbyAdrId(mySourceId, myAsnArtId);
            string strSql = string.Empty;
            if (vd.ListVdaClientValues.Count > 0)
            {
                foreach (VDAClientValues v in vd.ListVdaClientValues)
                {
                    v.AdrId = myDestId;
                    VDAClientValueViewData vdTmp = new VDAClientValueViewData(v);
                    strSql += vdTmp.sql_Add;
                }

                if (strSql.Length > 0)
                {
                    bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "AddVDAClientValue", 1);
                }
            }
            return bReturn;
        }



    }
}

