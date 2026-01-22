using System;
using System.Collections.Generic;
using System.Data;


namespace LVS
{
    public class clsEdiSegmentElement
    {
        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        public Globals._GL_USER GL_User;

        public clsEdiSegment EdiSegment;
        internal List<clsEdiSegmentElementField> ListEdiSegmentElementFields;

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
        public int EdiSegmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public DateTime Created { get; set; }
        public int tmpId { get; set; }
        public string Code { get; set; }
        public int SortId { get; set; }
        public string Kennung { get; set; }
        public bool IsActive { get; set; }

        /********************************************************************************
         *                      Methoden
         * *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySQLCon"></param>
        public void InitClass(ref Globals._GL_USER myGLUser, clsSQLCOM mySQLCon)
        {
            this.GL_User = myGLUser;
            this.SQLConIntern = mySQLCon;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {
            string strSQL = "INSERT INTO [EdiSegmentElement] ([EdiSegmentId], [Name], [Description] , [Position] " +
                                                             ", [Created], [tmpId], [Code], [SortId], [Kennung], [IsActive]) " +
                                  "VALUES (" + this.EdiSegmentId +
                                            ",'" + this.Name + "'" +
                                            ",'" + this.Description + "'" +
                                            ", " + this.Position +
                                            ",'" + this.Created + "'" +
                                            ", " + this.tmpId +
                                            ",'" + this.Code + "'" +
                                            ", " + this.SortId +
                                            ",'" + this.Kennung + "'" +
                                            ", " + Convert.ToInt32(this.IsActive) +

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
            string strSQL = "Update EdiSegmentElement SET " +
                                        "EdiSegmentId =" + this.EdiSegmentId +
                                        ", Decription ='" + this.Description + "' " +
                                        ", Name= '" + this.Name + "'" +
                                        ", Position =" + this.Position +
                                        ", IsActive = " + Convert.ToInt32(this.IsActive) +

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
            DataTable dt = new DataTable("EdiSegmentElement");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM EdiSegmentElement WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegmentElement");
            FillClassValue(dt, mybInclSub);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void FillClassValue(DataTable dt, bool mybInclSub)
        {
            try
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    int iTmp = 0;
                    int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                    this.ID = iTmp;
                    iTmp = 0;
                    int.TryParse(dt.Rows[i]["EdiSegmentId"].ToString(), out iTmp);
                    this.EdiSegmentId = iTmp;
                    this.Description = dt.Rows[i]["Description"].ToString();
                    this.Name = dt.Rows[i]["Name"].ToString();
                    iTmp = 0;
                    int.TryParse(dt.Rows[i]["Position"].ToString(), out iTmp);
                    this.Position = iTmp;
                    this.Created = (DateTime)dt.Rows[i]["Created"];
                    iTmp = 0;
                    int.TryParse(dt.Rows[i]["tmpId"].ToString(), out iTmp);
                    this.tmpId = iTmp;
                    this.Code = dt.Rows[i]["Code"].ToString();
                    iTmp = 0;
                    int.TryParse(dt.Rows[i]["SortId"].ToString(), out iTmp);
                    this.SortId = iTmp;
                    this.Kennung = dt.Rows[i]["Kennung"].ToString();
                    this.IsActive = (bool)dt.Rows[i]["IsActive"];

                    if (this.EdiSegmentId > 0)
                    {
                        this.EdiSegment = new clsEdiSegment();
                        this.EdiSegment.InitClass(ref this.GL_User, this.SQLConIntern);
                        this.EdiSegment.ID = this.EdiSegmentId;
                        this.EdiSegment.Fill(false);
                    }

                    this.ListEdiSegmentElementFields = new List<clsEdiSegmentElementField>();
                    if (mybInclSub)
                    {
                        clsEdiSegmentElementField tmpSEF = new clsEdiSegmentElementField();
                        tmpSEF.InitClass(ref this.GL_User, this.SQLConIntern);
                        this.ListEdiSegmentElementFields = tmpSEF.GetListBySegmentElementId(this.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {

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

        public List<clsEdiSegmentElement> GetListBySegmentId(int mySegId)
        {
            List<clsEdiSegmentElement> tmpList = new List<clsEdiSegmentElement>();
            DataTable dt = new DataTable("EdiSegmentElement");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM EdiSegmentElement WHERE EdiSegmentId=" + mySegId + " Order by Position;";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegmentElement");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    clsEdiSegmentElement tmpSE = new clsEdiSegmentElement();
                    tmpSE.InitClass(ref this.GL_User, this.SQLConIntern);
                    tmpSE.ID = iTmp;
                    tmpSE.Fill(true);
                    tmpList.Add(tmpSE);

                }
            }
            return tmpList;
        }


    }
}
