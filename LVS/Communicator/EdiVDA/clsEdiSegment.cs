using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace LVS
{
    public class clsEdiSegment
    {
        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        public Globals._GL_USER GL_User;

        public clsASNArt AsnArt;
        internal EdiSegmentViewData esVD = new EdiSegmentViewData();

        public Dictionary<string, clsEdiSegment> DictEdiSegment;


        internal List<clsEdiSegment> ListEdiSegment;
        internal List<clsEdiSegmentElement> ListEdiSegmentElement;
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

        public int ID { get; set; }
        public int ASNArtId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int RepeatCount { get; set; }
        public int Ebene { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int tmpId { get; set; }
        public string Code { get; set; }
        public int SortId { get; set; }

        private string kennung;
        public string Kennung
        {
            get
            {
                kennung = this.Name + this.ID.ToString();
                return kennung;
            }
            set { kennung = value; }
        }
        public bool Storable { get; set; }
        public bool IsActive { get; set; }
        public string EdiSegmentCheckFunction { get; set; }

        public List<clsEdiSegment> ListStorableSemgents
        {
            get
            {
                List<clsEdiSegment> tmpList = new List<clsEdiSegment>();
                if (ListEdiSegment.Count > 0)
                {
                    tmpList = ListEdiSegment.Where(x => x.Storable = true).ToList();
                }
                return tmpList;
            }
        }

        /********************************************************************************
         *                      Methoden
         * *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySQLCon"></param>
        public void InitClass(ref Globals._GL_USER myGLUser, clsSQLCOM mySQLCon)
        //public void InitClass(ref Globals._GL_USER myGLUser, clsSQLConnections mySQLCon, decimal myASNID)
        {
            this.GL_User = myGLUser;
            this.SQLConIntern = mySQLCon;
            //asnSatz = new clsASNArtSatz();
            //asnSatz.InitClass(ref this.GL_User, this.SQLConIntern);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {
            if (this.Name.Contains("'"))
            {
                this.Name = this.Name.Replace("'", "");
            }
            if (this.Description.Contains("'"))
            {
                this.Description = this.Description.Replace("'", "");
            }

            string strSQL = "INSERT INTO EdiSegment (" +
                                                      "[ASNArtId],[Name],[Status],[RepeatCount],[Ebene],[Description] " +
                                                      ", [Created], [tmpId], [Storable], [Code], [SortId], [IsActive] " +
                                                      ", [EdiSegmentCheckFunction]" +
                                                    ")" +
                                  "VALUES (" + this.ASNArtId +
                                            ",'" + this.Name + "'" +
                                            ",'" + this.Status + "'" +
                                            ", " + this.RepeatCount +
                                            ", " + this.Ebene +
                                            ",'" + this.Description + "'" +
                                            ", " + Convert.ToInt32(this.Storable) +
                                            ", '" + this.Created + "'" +
                                            ", " + this.tmpId +
                                            ", " + Convert.ToInt32(this.Storable) +
                                            ", '" + this.Code + "'" +
                                            "," + this.SortId +
                                            ", " + Convert.ToInt32(this.IsActive) +
                                            ", '" + this.EdiSegmentCheckFunction + "'" +
                                         ")";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                this.ID = iTmp;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            string strSQL = "Update EdiSegment SET " +
                                        "ASNArtId =" + this.ASNArtId +
                                        ", Name ='" + this.Name + "' " +
                                        ", Status ='" + this.Status + "' " +
                                        ", RepeatCount =" + this.RepeatCount +
                                        ", Ebene = " + this.Ebene +
                                        ", Description= '" + this.Description + "'" +
                                        ", Storable=" + Convert.ToInt32(this.Storable) +
                                        ", IsActive=" + Convert.ToInt32(this.IsActive) +
                                        ", EdiSegmentCheckFunction = '" + this.EdiSegmentCheckFunction + "' " +

                                        "WHERE ID=" + ID + " ;";

            bool bOK = clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Fill(bool mybInclSub)
        {
            //DataTable dt = new DataTable("EdiSegment");
            //string strSQL = string.Empty;
            //strSQL = "SELECT * FROM EdiSegment WHERE ID=" + ID + ";";
            //dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegment");
            //FillClassValue(dt, mybInclSub);

            esVD = new EdiSegmentViewData(ID, (int)this.GL_User.User_ID, mybInclSub);
            FillAndConvertToCls(esVD.EdiSegment, mybInclSub);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public void FillbyASNArtID()
        {
            //DataTable dt = new DataTable("EdiSegment");
            //string strSQL = string.Empty;
            //strSQL = "SELECT Top(1) * FROM EdiSegment WHERE ASNArtID=" + this.ASNArtId + ";";
            //dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegment");
            //FillClassValue(dt, false);

            esVD = new EdiSegmentViewData(this.ASNArtId, (int)GL_User.User_ID);
            FillAndConvertToCls(esVD.EdiSegment, false);
        }

        public void FillAndConvertToCls(EdiSegments myEdiSegment, bool mybInclSub)
        {
            this.ID = (int)myEdiSegment.Id;
            this.ASNArtId = (int)myEdiSegment.AsnArtId;
            this.Name = myEdiSegment.Name;
            this.Status = myEdiSegment.Status;
            this.RepeatCount = myEdiSegment.RepeatCount;
            this.Ebene = myEdiSegment.Ebene;
            this.Description = myEdiSegment.Description;
            this.Created = myEdiSegment.Created;
            this.Storable = myEdiSegment.Storable;
            this.Code = myEdiSegment.Code;
            this.IsActive = myEdiSegment.IsActive;
            this.EdiSegmentCheckFunction = myEdiSegment.EdiSegmentCheckFunction;

            if (this.ASNArtId > 0)
            {
                this.AsnArt = new clsASNArt();
                this.AsnArt.InitClass(ref this.GL_User, this.SQLConIntern);
                this.AsnArt.ID = this.ASNArtId;
                this.AsnArt.Fill();
            }
            this.ListEdiSegmentElement = new List<clsEdiSegmentElement>();
            if (mybInclSub)
            {
                clsEdiSegmentElement tmpSE = new clsEdiSegmentElement();
                tmpSE.InitClass(ref this.GL_User, this.SQLConIntern);
                this.ListEdiSegmentElement = tmpSE.GetListBySegmentId(this.ID);
            }
        }
        ///<summary>clsASNArtSatz / FillClassValue</summary>
        ///<remarks></remarks>
        //private void FillClassValue(DataTable dt, bool mybInclSub)
        //{
        //    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
        //    {
        //        int iTmp = 0;
        //        int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
        //        this.ID = iTmp;
        //        iTmp = 0;
        //        int.TryParse(dt.Rows[i]["ASNArtId"].ToString(), out iTmp);
        //        this.ASNArtId = iTmp;
        //        this.Name = dt.Rows[i]["Name"].ToString();
        //        if (this.Name.Contains("'"))
        //        {
        //            this.Name = this.Name.Replace("'", "");
        //        }

        //        this.Status = dt.Rows[i]["Status"].ToString();
        //        iTmp = 0;
        //        int.TryParse(dt.Rows[i]["RepeatCount"].ToString(), out iTmp);
        //        this.RepeatCount = iTmp;
        //        iTmp = 0;
        //        int.TryParse(dt.Rows[i]["Ebene"].ToString(), out iTmp);
        //        this.Ebene = iTmp;
        //        this.Description = dt.Rows[i]["Description"].ToString();
        //        if (this.Description.Contains("'"))
        //        {
        //            this.Description = this.Description.Replace("'", "");
        //        }

        //        this.Created = (DateTime)dt.Rows[i]["Created"];
        //        this.Storable = (bool)dt.Rows[i]["Storable"];
        //        this.Code = dt.Rows[i]["Code"].ToString();
        //        this.IsActive = (bool)dt.Rows[i]["IsActive"];
        //        this.EdiSegmentCheckFunction = dt.Rows[i]["EdiSegmentCheckFunction"].ToString();

        //        if (this.ASNArtId > 0)
        //        {
        //            this.AsnArt = new clsASNArt();
        //            this.AsnArt.InitClass(ref this.GL_User, this.SQLConIntern);
        //            this.AsnArt.ID = this.ASNArtId;
        //            this.AsnArt.Fill();
        //        }

        //        this.ListEdiSegmentElement = new List<clsEdiSegmentElement>();
        //        if (mybInclSub)
        //        {
        //            clsEdiSegmentElement tmpSE = new clsEdiSegmentElement();
        //            tmpSE.InitClass(ref this.GL_User, this.SQLConIntern);
        //            this.ListEdiSegmentElement = tmpSE.GetListBySegmentId(this.ID);
        //        }
        //    }
        //}
        ///<summary>clsASNArtSatz / FillListSatz</summary>
        ///<remarks></remarks>
        public void FillListAndDictEdiSegment()
        {
            DictEdiSegment = new Dictionary<string, clsEdiSegment>();
            ListEdiSegment = new List<clsEdiSegment>();
            DataTable dt = GetSegmente();
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                clsEdiSegment tmpSeg = new clsEdiSegment();
                tmpSeg.InitClass(ref this.GL_User, this.SQLConIntern);
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    tmpSeg.ID = iTmp;
                    tmpSeg.Fill(true);

                    ListEdiSegment.Add(tmpSeg);
                    string strKey = tmpSeg.ID.ToString() + "#" + tmpSeg.Name;
                    DictEdiSegment.Add(strKey, tmpSeg);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetSegmente()
        {
            string strSQL = "SELECT " +
                                "DISTINCT s.* " +
                                "FROM EdiSegment s " +
                                    "INNER JOIN EdiSegmentElement se on s.ID = se.EdiSegmentId " +
                                    "INNER JOIN EdiSegmentElementField sef on se.Id = sef.EdiSemgentElementId " +
                                        "where " +
                                            "s.ASNArtId=" + this.ASNArtId + ";";
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegment");
            return dt;
        }

        ///<summary>clsASNArt / CreateASNStrings</summary>
        ///<remarks></remarks>
        //public void CreateASNStrings(string myASNString, ref List<clsASNArtSatz> myListSatz)
        public void CreateASNStrings(string myASNString, clsASN myASN)
        {
            //ListError = new List<clsLogbuchCon>();
            //this.asnSatz.Job = myASN.Job;
            //this.asnSatz.ListSatz = myASN.ListSatz;
            //this.asnSatz.DictVDASatz = myASN.DictVDASatz;            
            ////this.asnSatz.DictVDA4913Satz = myDictVDA4913Satz;
            //this.asnSatz.CreateSatzStringIN(myASNString);
            //this.ListError = this.asnSatz.ListError;
            //if (this.ListError.Count == 0)
            //{
            //    this.asnSatz.CreateSatzFieldstringIN();
            //}
        }



    }
}
