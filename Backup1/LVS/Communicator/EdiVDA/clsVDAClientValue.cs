using LVS.Constants;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsVDAClientValue
    {

        public clsSQLCOM SQLConIntern = new clsSQLCOM();

        internal clsASNValue ASNValue;
        internal clsASNTyp ASNTyp;
        public DataTable dtVDAClientValue;

        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }


        public decimal ID { get; set; }
        public decimal AdrID { get; set; }
        public decimal ASNFieldID { get; set; }
        public string Value { get; set; }
        public string ValueArt { get; set; }
        public bool Fill0 { get; set; }
        public bool aktiv { get; set; }
        public string Satz { get; set; }
        public Int32 NextSatz { get; set; }
        public bool IsArtSatz { get; set; }
        public string FillValue { get; set; }
        public bool FillLeft { get; set; }

        private int _ASNArtId;
        public int ASNArtId
        {
            get
            {
                return _ASNArtId;
            }
            set
            {
                _ASNArtId = value;
            }
        }

        public string Kennung { get; set; }

        private clsASNArt _ASNArt;
        public clsASNArt ASNArt
        {
            get
            {
                this._ASNArt = new clsASNArt();
                this._ASNArt.InitClass(ref this.GL_User, this.SQLConIntern);
                this._ASNArt.ID = _ASNArtId;
                this._ASNArt.Fill();
                return _ASNArt;
            }
            set
            {
                _ASNArt = value;
            }
        }
        public Dictionary<string, List<clsVDAClientValue>> DictVDAClientValue { get; set; }
        public List<clsVDAClientValue> listVDAClientValueSatz { get; set; }
        public List<int> ListEdiSegments_activ { get; set; }
        public List<int> ListEdiSegments_Head { get; set; }
        public List<int> ListEdiSegments_Artikel { get; set; }
        public List<int> ListEdiSegments_End { get; set; }

        public DataTable dtVDAClientOutByAdrId
        {
            get
            {
                return GetVDAList((int)this.AdrID);
            }
        }
        public DataTable dtEDIFACTClientOutByAdrId
        {
            get
            {
                return GetEDIFACTList((int)this.AdrID);
            }
        }


        public Int32 CountArtSatz
        {
            get
            {
                string strSQL = string.Empty;
                strSQL = "SELECT count(a.ID) as Anzahl " +
                                            "FROM VDAClientOUT a " +
                                            "INNER JOIN ASNArtSatzFeld b ON b.ID=a.ASNFieldID " +
                                            "INNER JOIN ASNArtSatz c ON c.ID = b.ASNSatzID " +
                                            "WHERE a.AdrID=" + this.AdrID +
                                                        " AND a.aktiv=1 " +
                                                        " AND a.NextSatz>0 " +
                                                        " AND a.ArtSatz=1 ;";
                string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
                Int32 iTmp = 0;
                if (!Int32.TryParse(strTmp, out iTmp))
                {
                    iTmp = 1;
                }
                return iTmp;
            }
        }



        /**********************************************************************************
         *                      Methoden / Procedure
         * *******************************************************************************/
        ///<summary>clsASN / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, decimal myAdrID, clsASNArt myAsnArt)
        {
            this.GL_User = myGLUser;
            this.AdrID = myAdrID;
            //this.ASNArt = myAsnArt;
            if (myAsnArt is clsASNArt)
            {
                if ((int)myAsnArt.ID > 0)
                {
                    this.ASNArtId = (int)myAsnArt.ID;
                    InitDictVDAClientValue();
                }
            }
        }
        ///<summary>clsASN / InitDictVDAClientValue</summary>
        ///<remarks></remarks>
        public void InitDictVDAClientValue()
        {
            listVDAClientValueSatz = new List<clsVDAClientValue>();
            DictVDAClientValue = new Dictionary<string, List<clsVDAClientValue>>();

            DataTable dt = new DataTable("VDAClient");

            string strSatz = string.Empty;
            if (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_VDA4913))
            {
                strSatz = "711";
                dt = GetVDAList((int)this.AdrID);
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    clsVDAClientValue tmp = new clsVDAClientValue();
                    tmp.GL_User = this.GL_User;
                    tmp.ID = (decimal)dt.Rows[i]["ID"];
                    tmp.FillByID();
                    this.Satz = dt.Rows[i]["Satz"].ToString().Trim();

                    if (!strSatz.Equals(this.Satz))
                    {
                        DictVDAClientValue.Add(strSatz, listVDAClientValueSatz);
                        listVDAClientValueSatz = new List<clsVDAClientValue>();
                        strSatz = this.Satz;
                    }
                    listVDAClientValueSatz.Add(tmp);
                }
                if (dt.Rows.Count > 0)
                {
                    //Der Satz 719 muss auch noch mit rein
                    DictVDAClientValue.Add(strSatz, listVDAClientValueSatz);
                }
            }
            else if (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_EdifactVDA4987))
            {
                string strSegment = "VDA4987";  // der erste Satz mit Variablen
                dt = GetEDIFACTList((int)this.AdrID);

                //--- Liste der Segmente, die für diese Adresse aktiv sind
                //--- abgleich findet über diese Liste statt, ob eine Segment erstellt wird
                ListEdiSegments_activ = GetSegmentsForReceiver((int)this.AdrID);
                ListEdiSegments_Head = GetSegmentsForReceiverHeadOrArtikel((int)this.AdrID, false);
                ListEdiSegments_Artikel = GetSegmentsForReceiverHeadOrArtikel((int)this.AdrID, true);

                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    clsVDAClientValue tmp = new clsVDAClientValue();
                    tmp.GL_User = this.GL_User;
                    tmp.ID = (decimal)dt.Rows[i]["ID"];
                    tmp.FillByID();
                    listVDAClientValueSatz.Add(tmp);
                }
                if (dt.Rows.Count > 0)
                {
                    DictVDAClientValue.Add(strSegment, listVDAClientValueSatz);
                }
            }
            else if (
                        (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_DESADV_BMW_4a)) ||
                        (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_DESADV_BMW_4b)) ||
                        (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_DESADV_BMW_4b_RL)) ||
                        (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_DESADV_BMW_4b_ST)) ||
                        (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_DESADV_BMW_6)) ||
                        (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_DESADV_BMW_6_UB))
                    )
            {
                string strSegment = "BMW_DESADV";  // der erste Satz mit Variablen
                dt = GetEDIFACTList((int)this.AdrID);

                //--- Liste der Segmente, die für diese Adresse aktiv sind
                //--- abgleich findet über diese Liste statt, ob eine Segment erstellt wird
                ListEdiSegments_activ = GetSegmentsForReceiver((int)this.AdrID);
                ListEdiSegments_Head = GetSegmentsForReceiverHeadOrArtikel((int)this.AdrID, false);
                ListEdiSegments_Artikel = GetSegmentsForReceiverHeadOrArtikel((int)this.AdrID, true);

                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    clsVDAClientValue tmp = new clsVDAClientValue();
                    tmp.GL_User = this.GL_User;
                    tmp.ID = (decimal)dt.Rows[i]["ID"];
                    tmp.FillByID();
                    listVDAClientValueSatz.Add(tmp);
                }
                if (dt.Rows.Count > 0)
                {
                    DictVDAClientValue.Add(strSegment, listVDAClientValueSatz);
                }
            }
            else if (
                        (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_EDIFACT_ASN_D97A)) ||
                        (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_EDIFACT_INVRPT_D96A))
                    )
            {
                //string strSegment = "BMW_DESADV";  // der erste Satz mit Variablen
                dt = GetEDIFACTList((int)this.AdrID);

                //--- Liste der Segmente, die für diese Adresse aktiv sind
                //--- abgleich findet über diese Liste statt, ob eine Segment erstellt wird
                ListEdiSegments_activ = GetSegmentsForReceiver((int)this.AdrID);
                ListEdiSegments_Head = GetSegmentsForReceiverHeadOrArtikel((int)this.AdrID, false);
                ListEdiSegments_Artikel = GetSegmentsForReceiverHeadOrArtikel((int)this.AdrID, true);

                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    clsVDAClientValue tmp = new clsVDAClientValue();
                    tmp.GL_User = this.GL_User;
                    tmp.ID = (decimal)dt.Rows[i]["ID"];
                    tmp.FillByID();
                    listVDAClientValueSatz.Add(tmp);
                }
                if (dt.Rows.Count > 0)
                {
                    DictVDAClientValue.Add(this.ASNArt.Typ, listVDAClientValueSatz);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrId"></param>
        /// <returns></returns>
        public DataTable GetVDAList(int myAdrId)
        {
            DataTable dt = new DataTable("VDAClient");
            string strSQL = string.Empty;
            if (this.ASNArt is clsASNArt)
            {
                //if (this.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_VDA4913.ToString()))
                //{
                //    strSQL = "SELECT a.*, c.Kennung as Satz, b.Kennung, b.Datenfeld " +
                //                                "FROM VDAClientOUT a " +
                //                                "INNER JOIN ASNArtSatzFeld b ON b.ID=a.ASNFieldID " +
                //                                "INNER JOIN ASNArtSatz c ON c.ID = b.ASNSatzID " +
                //                                //"WHERE a.AdrID=" + myAdrId + " AND a.aktiv=1 AND a.ASNArtId=" + this.ASNArt.ID + ";";
                //                                "WHERE a.AdrID=" + myAdrId + " AND a.ASNArtId=" + this.ASNArt.ID + ";";
                //}
                strSQL = "SELECT a.*, c.Kennung as Satz, b.Kennung, b.Datenfeld " +
                            "FROM VDAClientOUT a " +
                            "INNER JOIN ASNArtSatzFeld b ON b.ID=a.ASNFieldID " +
                            "INNER JOIN ASNArtSatz c ON c.ID = b.ASNSatzID " +
                            //"WHERE a.AdrID=" + myAdrId + " AND a.aktiv=1 AND a.ASNArtId=" + this.ASNArt.ID + ";";
                            "WHERE a.AdrID=" + myAdrId + " AND a.ASNArtId=" + this.ASNArt.ID + ";";
            }
            else
            {
                strSQL = "SELECT a.*, c.Kennung as Satz, b.Kennung " +
                            "FROM VDAClientOUT a " +
                            "INNER JOIN ASNArtSatzFeld b ON b.ID=a.ASNFieldID " +
                            "INNER JOIN ASNArtSatz c ON c.ID = b.ASNSatzID " +
                            "WHERE a.AdrID=" + myAdrId + ";";
                //"WHERE a.AdrID=" + myAdrId + " AND a.aktiv=1 ;";
            }
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClient");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrId"></param>
        /// <returns></returns>
        public DataTable GetEDIFACTList(int myAdrId)
        {
            DataTable dt = new DataTable("VDAClient");
            string strSQL = string.Empty;
            if (this.ASNArt is clsASNArt)
            {
                //if (this.ASNArt.Typ.Equals(clsASNArt.const_Art_EdifactVDA4987.ToString()))
                //{
                //    strSQL = "SELECT a.* " +
                //                    ", s.Name as Segment " +
                //                    ", se.Name as Element " +
                //                    ", sef.Shorcut as Field " +
                //                    ", s.Name+'|'+se.Name+'|' + sef.Shorcut as Kennung " +
                //                                //", s.Name+CAST(s.Id as nvarchar(10)) as Satz " +
                //                                "FROM VDAClientOUT a " +
                //                                "INNER JOIN EdiSegmentElementField sef ON sef.Id=a.ASNFieldID " +
                //                                "INNER JOIN EdiSegmentElement se on se.Id = sef.EdiSemgentElementId " +
                //                                "INNER JOIN EdiSegment s on s.Id = se.EdiSegmentId " +
                //                                    "WHERE " +
                //                                        "a.AdrID=" + myAdrId;
                //    //if (myForEdiCreation)
                //    //{
                //    //    strSQL += " AND a.aktiv=1 ";
                //    //}
                //    strSQL += " AND a.ASNArtId=" + this.ASNArt.ID + " ; ";
                //}
                //if (this.ASNArt.Typ.Equals(clsASNArt.const_Art_DESADV_BMW_4a))
                //{
                strSQL = "SELECT a.* " +
                                ", s.Name as Segment " +
                                ", se.Name as Element " +
                                ", sef.Shorcut as Field " +
                                            //", s.Name+'|'+se.Name+'|' + sef.Shorcut as Kennung " +
                                            //", s.Name+CAST(s.Id as nvarchar(10)) as Satz " +
                                            "FROM VDAClientOUT a " +
                                            "INNER JOIN EdiSegmentElementField sef ON sef.Id=a.ASNFieldID " +
                                            "INNER JOIN EdiSegmentElement se on se.Id = sef.EdiSemgentElementId " +
                                            "INNER JOIN EdiSegment s on s.Id = se.EdiSegmentId " +
                                                "WHERE " +
                                                    "a.AdrID=" + myAdrId;
                //if (myForEdiCreation)
                //{
                strSQL += " AND a.aktiv=1 ";
                //}
                strSQL += " AND a.ASNArtId=" + this.ASNArt.ID + " ; ";
                //}
            }
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClient");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrId"></param>
        /// <returns></returns>
        public List<int> GetSegmentsForReceiver(int myAdrId)
        {
            DataTable dt = new DataTable("VDAClient");
            List<int> retList = new List<int>();
            string strSQL = string.Empty;
            if (this.ASNArt is clsASNArt)
            {
                //if ((this.ASNArt.Typ.Equals(clsASNArt.const_Art_EdifactVDA4987.ToString()))
                //{
                //    strSQL = "SELECT DISTINCT s.ID " +
                //                                "FROM VDAClientOUT a " +
                //                                "INNER JOIN EdiSegmentElementField sef ON sef.Id=a.ASNFieldID " +
                //                                "INNER JOIN EdiSegmentElement se on se.Id = sef.EdiSemgentElementId " +
                //                                "INNER JOIN EdiSegment s on s.Id = se.EdiSegmentId " +
                //                                    "WHERE " +
                //                                        "a.AdrID=" + myAdrId +
                //                                        " AND a.aktiv=1 " +
                //                                        " AND a.ASNArtId=" + this.ASNArt.ID + " ; ";
                //}
                strSQL = "SELECT DISTINCT s.ID " +
                            "FROM VDAClientOUT a " +
                            "INNER JOIN EdiSegmentElementField sef ON sef.Id=a.ASNFieldID " +
                            "INNER JOIN EdiSegmentElement se on se.Id = sef.EdiSemgentElementId " +
                            "INNER JOIN EdiSegment s on s.Id = se.EdiSegmentId " +
                                "WHERE " +
                                    "a.AdrID=" + myAdrId +
                                    " AND a.aktiv=1 " +
                                    " AND a.ASNArtId=" + this.ASNArt.ID + " ; ";



            }
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClient");
            foreach (DataRow r in dt.Rows)
            {
                int iTmp = 0;
                int.TryParse(r["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    if (!retList.Contains(iTmp))
                    {
                        retList.Add(iTmp);
                    }
                }
            }
            return retList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrId"></param>
        /// <returns></returns>
        public List<int> GetSegmentsForReceiverHeadOrArtikel(int myAdrId, bool myIsArtSegment)
        {
            DataTable dt = new DataTable("VDAClient");
            List<int> retList = new List<int>();
            string strSQL = string.Empty;
            if (this.ASNArt is clsASNArt)
            {
                //if (this.ASNArt.Typ.Equals(clsASNArt.const_Art_EdifactVDA4987.ToString()))
                //{
                //    strSQL = "SELECT DISTINCT s.ID " +
                //                                "FROM VDAClientOUT a " +
                //                                "INNER JOIN EdiSegmentElementField sef ON sef.Id=a.ASNFieldID " +
                //                                "INNER JOIN EdiSegmentElement se on se.Id = sef.EdiSemgentElementId " +
                //                                "INNER JOIN EdiSegment s on s.Id = se.EdiSegmentId " +
                //                                    "WHERE " +
                //                                        "a.AdrID=" + myAdrId +
                //                                        " AND a.aktiv=1 " +
                //                                        " AND a.ArtSatz= " + Convert.ToInt32(myIsArtSegment) +
                //                                        " AND a.ASNArtId=" + this.ASNArt.ID + " ";


                //}
                strSQL = "SELECT DISTINCT s.ID " +
                            "FROM VDAClientOUT a " +
                            "INNER JOIN EdiSegmentElementField sef ON sef.Id=a.ASNFieldID " +
                            "INNER JOIN EdiSegmentElement se on se.Id = sef.EdiSemgentElementId " +
                            "INNER JOIN EdiSegment s on s.Id = se.EdiSegmentId " +
                                "WHERE " +
                                    "a.AdrID=" + myAdrId +
                                    " AND a.aktiv=1 " +
                                    " AND a.ArtSatz= " + Convert.ToInt32(myIsArtSegment) +
                                    " AND a.ASNArtId=" + this.ASNArt.ID + " ";
            }
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClient");
            foreach (DataRow r in dt.Rows)
            {
                int iTmp = 0;
                int.TryParse(r["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    if (!retList.Contains(iTmp))
                    {
                        retList.Add(iTmp);
                    }
                }
            }
            return retList;
        }
        ///<summary>clsASN / Fill</summary>
        ///<remarks></remarks>
        public void FillVDAClientVAlueTable()
        {
            dtVDAClientValue = new DataTable("VDAClientValue");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM VDAClientOUT WHERE AdrID=" + this.AdrID + ";";
            dtVDAClientValue = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClientValue");
        }
        ///<summary>clsASN / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("ASN");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM VDAClientOUT WHERE AdrID=" + this.AdrID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASN");
            FillClassByTable(dt);
        }
        ///<summary>clsASN / FillClassByTable</summary>
        ///<remarks></remarks>
        private void FillClassByTable(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                this.ID = (decimal)dt.Rows[i]["ID"];
                if ((Int32)this.ID == 171)
                {
                    string str = "89";
                }
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                this.AdrID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ASNFieldID"].ToString(), out decTmp);
                this.ASNFieldID = decTmp;
                this.Value = dt.Rows[i]["Value"].ToString();
                this.ValueArt = dt.Rows[i]["ValueArt"].ToString();
                this.Fill0 = (bool)dt.Rows[i]["Fill0"];
                this.aktiv = (bool)dt.Rows[i]["aktiv"];
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["NextSatz"].ToString(), out iTmp);
                this.NextSatz = iTmp;
                this.IsArtSatz = (bool)dt.Rows[i]["ArtSatz"];
                this.FillValue = dt.Rows[i]["FillValue"].ToString();
                this.FillLeft = (bool)dt.Rows[i]["FillLeft"];
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ASNArtId"].ToString(), out iTmp);
                this.ASNArtId = iTmp;
                this.Kennung = dt.Rows[i]["Kennung"].ToString();
            }
        }
        ///<summary>clsASN / Fill</summary>
        ///<remarks></remarks>
        public void FillByID()
        {
            DataTable dt = new DataTable("VDAClientValue");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM VDAClientOUT WHERE ID=" + this.ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClientValue");
            FillClassByTable(dt);
        }
        ///<summary>clsASN / SetListSatz</summary>
        ///<remarks></remarks>
        public void SetListSatzFromDictByKey(string KeySatz)
        {
            this.listVDAClientValueSatz = new List<clsVDAClientValue>();
            List<clsVDAClientValue> listTmp = new List<clsVDAClientValue>();
            this.DictVDAClientValue.TryGetValue(KeySatz, out listTmp);
            this.listVDAClientValueSatz = listTmp;
            if (this.listVDAClientValueSatz == null)
            {
                this.listVDAClientValueSatz = new List<clsVDAClientValue>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AddSqlString()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO VDAClientOUT ([AdrID], [ASNFieldID], [ValueArt], [Value], [Fill0], [aktiv] " +
                                                ", [NextSatz], [ArtSatz], [FillValue], [FillLeft], [ASNArtId] " +
                                                ", [Kennung]" +

                                                ") VALUES (" +
                                                    this.AdrID +
                                                    ", " + this.ASNFieldID +
                                                    ", '" + this.ValueArt + "'" +
                                                    ", '" + this.Value + "'" +
                                                    ", " + Convert.ToInt32(this.Fill0) +
                                                    ", " + Convert.ToInt32(this.aktiv) +
                                                    ", " + this.NextSatz +
                                                    ", " + Convert.ToInt32(this.IsArtSatz) +
                                                    ", '" + this.FillValue + "'" +
                                                    ", " + Convert.ToInt32(this.FillLeft) +
                                                    ", " + Convert.ToInt32(this.ASNArtId) +
                                                    ", '" + this.Kennung + "' " +
                                                    "); ";

            return strSql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myVDASchema"></param>
        /// <returns></returns>
        public bool AddSchema(List<clsVDAClientValue> myVDASchema)
        {
            string strSql = string.Empty;
            foreach (clsVDAClientValue itm in myVDASchema)
            {
                strSql = strSql + itm.AddSqlString();
            }
            bool bReturn = false;
            bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "AddSchema", this.GL_User.User_ID);
            return bReturn;
        }
        /// <summary>
        ///                 Update 
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            bool retVal = false;
            string strSql = string.Empty;
            strSql = "Update VDAClientOUT SET " +
                                        "[AdrID] = " + this.AdrID +
                                        ", [ASNFieldID] = " + this.ASNFieldID +
                                        ", [ValueArt] = '" + this.ValueArt + "'" +
                                        ", [Value] = '" + this.Value + "'" +
                                        ", [Fill0] = " + Convert.ToInt32(this.Fill0) +
                                        ", [aktiv] = " + Convert.ToInt32(this.aktiv) +
                                        ", [NextSatz] =" + this.NextSatz +
                                        ", [ArtSatz] = " + Convert.ToInt32(this.IsArtSatz) +
                                        ", [FillValue] ='" + this.FillValue + "'" +
                                        ", [FillLeft] =" + Convert.ToInt32(this.FillLeft) +
                                        ", [ASNArtId] = " + this.ASNArtId +
                                        ", [Kennung] = '" + this.Kennung + "' " +

                                        " where Id = " + this.ID + ";";

            retVal = clsSQLCOM.ExecuteSQL(strSql, this.BenutzerID);
            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySourceAdrId"></param>
        /// <param name="myDestAdrId"></param>
        /// <param name="myUserId"></param>
        /// <returns></returns>
        public static bool InsertShapeByRefAdr(int mySourceAdrId, int myDestAdrId, int myAsnArtId, decimal myUserId)
        {
            bool bReturn = false;
            if (!clsVDAClientValue.ExistValue(myDestAdrId, myUserId))
            {
                bReturn = VDAClientValueViewData.CopyShapebyAdrId(mySourceAdrId, myDestAdrId, myAsnArtId);



                //Eintrag
                //string strSql = string.Empty;
                //strSql = " INSERT INTO VDAClientOUT ([AdrID], [ASNFieldID],[ValueArt],[Value],[Fill0],[aktiv] " +
                //                                   ",[NextSatz],[ArtSatz],[FillValue],[FillLeft],[ASNArtId]) " +
                //        "SELECT " +
                //                myDestAdrId +
                //                ", a.ASNFieldID " +
                //                ", a.ValueArt " +
                //                ", a.Value" +
                //                ", a.Fill0 " +
                //                ", a.aktiv " +
                //                ", a.NextSatz " +
                //                ", a.ArtSatz " +
                //                ", a.FillValue " +
                //                ", a.FillLeft " +
                //                ", a.ASNArtId " +
                //                 " FROM VDAClientOUT a " +
                //                 "INNER JOIN ASNArtSatzFeld b ON b.ID = a.ASNFieldID " +
                //                 "INNER JOIN ASNArtSatz c ON c.ID = b.ASNSatzID " +
                //                 "WHERE " +
                //                        "a.AdrID =" + mySourceAdrId + "; ";
                //bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSql, "InsertVDAClientValue", myUserId);

            }
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrId"></param>
        /// <param name="myUserId"></param>
        /// <returns></returns>
        public static bool ExistValue(int myAdrId, decimal myUserId)
        {
            bool bReturn = false;
            string strSQL = string.Empty;
            strSQL = "SELECT count(a.ID) " +
                                        "FROM VDAClientOUT a " +
                                        "INNER JOIN ASNArtSatzFeld b ON b.ID=a.ASNFieldID " +
                                        "INNER JOIN ASNArtSatz c ON c.ID = b.ASNSatzID " +
                                        "WHERE a.AdrID=" + myAdrId +
                                                    ";";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, myUserId);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            bReturn = (iTmp > 0);
            return bReturn;
        }



    }
}
