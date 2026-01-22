using LVS.Constants;
using LVS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataTable = System.Data.DataTable;

namespace LVS.ViewData
{
    public class AsnArtViewData
    {
        //public const string const_Art_XML_Uniport = "XMLUniport";  //honselmann
        //public const string const_Art_VDA4913 = "VDA4913";
        //public const string const_Art_VDA4905 = "VDA4905";
        //public const string const_Art_BMWCall4913 = "BMWCall4913";
        //public const string const_Art_WatchDog = "WatchDog";
        //public const string const_Art_EdifactVDA4987 = "EdiVDA4987";
        //public const string const_Art_EdifactVDA4984 = "EdiVDA4984";
        //public const string const_Art_DESADV_BMW_4a = "DESADV_BMW_4a";
        //public const string const_Art_DESADV_BMW_4b = "DESADV_BMW_4b";
        //public const string const_Art_DESADV_BMW_4b_RL = "DESADV_BMW_4b_RL";
        //public const string const_Art_DESADV_BMW_4b_ST = "DESADV_BMW_4b_ST";
        //public const string const_Art_DESADV_BMW_6 = "DESADV_BMW_6";
        //public const string const_Art_DESADV_BMW_6_UB = "DESADV_BMW_6_UB";
        //public const string const_Art_EDIFACT_DELFOR_D97A = "EDIFACT_DELFOR_D97A";
        //public const string const_Art_EDIFACT_ASN_D97A = "EDIFACT_ASN_D97A";
        //public const string const_Art_EDIFACT_Qality_D96A = "EDIFACT_Qality_D96A";

        //public const string const_ArtBeschreibung_VDA4913 = "VDA4913 Schema";
        //public const string const_ArtBeschreibung_VDA4905 = "VDA4905 Schema";
        //public const string const_ArtBeschreibung_BMWCall4913 = "BMWCall4913 spezielles BMW Schema";
        //public const string const_ArBeschreibungt_WatchDog = "WatchDog zur Überwachung per FTP";
        //public const string const_ArtBeschreibung_EdifactVDA4987 = "EdiVDA4987 für VW";
        //public const string const_ArtBeschreibung_EdifactVDA4984 = "EdiVDA4984 für VW";
        //public const string const_ArtBeschreibung_DESADV_BMW_4a = "DESADV_BMW_4a spezielles BMW Schema";
        //public const string const_ArtBeschreibung_DESADV_BMW_4b = "DESADV_BMW_4b spezielles BMW Schema";
        //public const string const_ArtBeschreibung_DESADV_BMW_4b_RL = "DESADV_BMW_4b_RL spezielles BMW Schema";
        //public const string const_ArtBeschreibung_DESADV_BMW_4b_ST = "DESADV_BMW_4b_ST spezielles BMW Schema";
        //public const string const_ArtBeschreibung_DESADV_BMW_6 = "DESADV_BMW_6 spezielles BMW Schema";
        //public const string const_ArtBeschreibung_DESADV_BMW_6_UB = "DESADV_BMW_6_UB spezielles BMW Schema";
        //public const string const_ArtBeschreibung_EDIFACT_DELFOR_D97A = "EDIFACT_DELFOR_D97A";
        //public const string const_ArtBeschreibung_EDIFACT_ASN_D97A = "EDIFACT_ASN_D97A";
        //public const string const_ArtBeschreibung_EDIFACT_Qality_D96A = "EDIFACT_Qality_D96A";

        public AsnArt AsnArt { get; set; }
        private int BenutzerID { get; set; }
        //public AddressViewData adrViewData { get; set; }

        public List<AsnArt> ListAsnArt { get; set; }
        //internal clsSQLconComDiverse sqlComDiv { get; set; }

        public AsnArtViewData()
        {
            InitCls();
            GetAsnArtList(false);
            InitCls();
        }

        public AsnArtViewData(AsnArt myAsnArt)
        {
            this.AsnArt = myAsnArt;
        }

        public AsnArtViewData(int myId, int myUserId, bool mybInclSub) : this()
        {
            AsnArt.Id = myId;
            BenutzerID = myUserId;
            if (AsnArt.Id > 0)
            {
                Fill(mybInclSub);
            }
        }

        private void InitCls()
        {
            AsnArt = new AsnArt();
        }

        public void Fill(bool mybInclSub)
        {
            string strSQL = sql_Get;
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "AsnArt");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetValue(dr, mybInclSub);
                }
            }
        }

        private void SetValue(DataRow row, bool mybInclSub)
        {
            int iTmp = 0;
            int.TryParse(row["ID"].ToString(), out iTmp);
            AsnArt.Id = iTmp;
            AsnArt.Typ = row["Typ"].ToString();
            DateTime tmpDate = new DateTime(1900, 1, 1);
            DateTime.TryParse(row["Datum"].ToString(), out tmpDate);
            AsnArt.Datum = tmpDate;
            AsnArt.Bezeichnung = row["Bezeichnung"].ToString();
            AsnArt.Beschreibung = row["Beschreibung"].ToString();

            AsnArt.ListEdiSegments = new List<EdiSegments>();
            //if (mybInclSub)
            //{
            //    AsnArt.ListEdiSegments = EdiSegmentViewData.GetEdiSegmentsByAsnArtIdToImport()
            //    EdiSegmentViewData viewData = new EdiSegmentViewData();

            //}
        }


        public void GetAsnArtList(bool mybInclSub)
        {
            ListAsnArt = new List<AsnArt>();
            string strSQL = sql_Get_Main;
            DataTable dt = new DataTable("AsnArt");
            //dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());

            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, dt.TableName.ToString());
            foreach (DataRow dr in dt.Rows)
            {
                AsnArt = new AsnArt();
                SetValue(dr, mybInclSub);
                ListAsnArt.Add(AsnArt);
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
                AsnArt.Id = decTmp;
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

        public void GetAsnList(bool my)
        {
            string strSql = sql_Get_List;
            ListAsnArt = new List<AsnArt>();
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, BenutzerID, "AsnArt");
            foreach (DataRow r in dt.Rows)
            {
                int iTmp = 0;
                int.TryParse(r["Id"].ToString(), out iTmp);

                AsnArtViewData vd = new AsnArtViewData(iTmp, BenutzerID, false);
                ListAsnArt.Add(vd.AsnArt);
            }
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
                AsnArt.Datum = DateTime.Now;
                string strSQL = "INSERT INTO ASNArt (Typ, Datum, Bezeichnung, Beschreibung) " +
                      "VALUES ('" + AsnArt.Typ + "' " +
                             ",'" + AsnArt.Datum + "' " +
                             ",'" + AsnArt.Bezeichnung + "'" +
                             ",'" + AsnArt.Beschreibung + "'" +
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
                strSql = "SELECT * FROM ASNArt WHERE ID=" + AsnArt.Id + ";";
                return strSql;
            }
        }
        /// <summary>
        ///             GET list
        /// </summary>
        public string sql_Get_List
        {
            get
            {
                string strSql = string.Empty;
                strSql = "SELECT * FROM ASNArt;";
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
                strSql = "SELECT * FROM ASNArt";
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
                string strSQL = "Update ASNArt SET " +
                                            "Typ ='" + AsnArt.Typ + "'" +
                                            ", Datum ='" + AsnArt.Datum + "' " +
                                            ", Bezeichnung ='" + AsnArt.Bezeichnung + "' " +
                                            ", Beschreibung ='" + AsnArt.Beschreibung + "' " +

                                            "WHERE ID=" + AsnArt.Id + " ;";
                return strSql;
            }
        }



        //-------------------------------------------------------------------------------------------------------
        //                                          static
        //-------------------------------------------------------------------------------------------------------

        public static List<AsnArt> GetAsnArtListToImport(clsSQLconComDiverse mySqlComDiv)
        {
            List<AsnArt> ListAsnArt = new List<AsnArt>();

            AsnArtViewData vd = new AsnArtViewData();
            string strSql = vd.sql_Get_Main;
            DataTable dt = new DataTable("AsnArt");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetAsnArtListToImport", "AsnArtList", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.AsnArt = new AsnArt();
                vd.SetValue(dr, false);
                ListAsnArt.Add(vd.AsnArt);
            }
            return ListAsnArt;
        }

        public static AsnArt GetAsnArtValueToImport(clsSQLconComDiverse mySqlComDiv, int myId)
        {
            AsnArtViewData vd = new AsnArtViewData();
            string strSql = vd.sql_Get_Main + " WHERE ID=" + myId;
            DataTable dt = new DataTable("AsnArt");
            dt = clsSQLconComDiverse.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "GetAsnArtValueToImport", "AsnArtValue", 1, mySqlComDiv);
            foreach (DataRow dr in dt.Rows)
            {
                vd.AsnArt = new AsnArt();
                vd.SetValue(dr, false);
                if (vd.AsnArt.Id > 0)
                {
                    vd.AsnArt.ListEdiSegments = new List<EdiSegments>();
                    vd.AsnArt.ListEdiSegments = EdiSegmentViewData.GetEdiSegmentsByAsnArtIdToImport(mySqlComDiv, (int)vd.AsnArt.Id);
                }
            }
            return vd.AsnArt;
        }


        public static List<AsnArt> CheckTableAsnArtForUpdate()
        {
            List<AsnArt> asnArts = new List<AsnArt>();

            AsnArt art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_VDA4913;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_VDA4913;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_VDA4905;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_VDA4905;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_BMWCall4913;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_BMWCall4913;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_WatchDog;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArBeschreibungt_WatchDog;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_WatchDog;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArBeschreibungt_WatchDog;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_EdifactVDA4987;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_EdifactVDA4987;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_EdifactVDA4984;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_EdifactVDA4984;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_DESADV_BMW_4a;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_DESADV_BMW_4a;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_DESADV_BMW_4b;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_DESADV_BMW_4b;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_DESADV_BMW_4b_RL;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_DESADV_BMW_4b_RL;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_DESADV_BMW_4b_ST;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_DESADV_BMW_4b_ST;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_DESADV_BMW_6;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_DESADV_BMW_6;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_DESADV_BMW_6_UB;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_DESADV_BMW_6_UB;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_EDIFACT_DELFOR_D97A;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DELFOR_D97A;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_EDIFACT_ASN_D97A;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_EDIFACT_Qality_D96A;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_EDIFACT_Qality_D96A;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_XML_ZQM_QALITY02;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_XML_ZQM_QALITY02;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_EDIFACT_ASN_D96A;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_Art_EDIFACT_ASN_D96A;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_EDIFACT_INVRPT_D96A;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A;
            asnArts.Add(art);

            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A;
            asnArts.Add(art);


            art = new AsnArt();
            art.Typ = constValue_AsnArt.const_Art_Textdatei;
            art.Datum = DateTime.Now;
            art.Bezeichnung = art.Typ;
            art.Beschreibung = constValue_AsnArt.const_ArtBeschreibung_Textdatei;
            asnArts.Add(art);


            List<AsnArt> retList = new List<AsnArt>();
            AsnArtViewData vd = new AsnArtViewData();

            if (asnArts.Count != vd.ListAsnArt.Count)
            {
                foreach (AsnArt item in asnArts)
                {
                    AsnArt tmpArt = vd.ListAsnArt.FirstOrDefault(x => x.Typ == item.Typ);
                    if (tmpArt is null)
                    {
                        retList.Add(item);
                    }
                }
            }
            return retList;
        }
    }
}

