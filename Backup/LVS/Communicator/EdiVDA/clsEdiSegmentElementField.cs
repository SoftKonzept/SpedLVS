using System;
using System.Collections.Generic;
using System.Data;


namespace LVS
{
    public class clsEdiSegmentElementField
    {
        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        public Globals._GL_USER GL_User;

        public clsEdiSegmentElement EdiSegmentElement;


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
        public int EdiSemgentElementId { get; set; }
        public string Shortcut { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Format { get; set; }
        public string FormatString { get; set; }
        public string Description { get; set; }
        public string constValue { get; set; }
        public int Position { get; set; }
        public DateTime Created { get; set; }

        public int tmpId { get; set; }
        public int SortId { get; set; }
        public string Code { get; set; }
        public string Kennung { get; set; }
        public int EdiSegmentId { get; set; }
        public int AsnArtId { get; set; }

        private int minLenght;
        public int MinLength
        {
            get
            {
                enumEdifactStatus tmpStatus = (enumEdifactStatus)Enum.Parse(typeof(enumEdifactStatus), this.Status);
                minLenght = ediHelper_Format.GetMinLength(this.Format, tmpStatus);
                return minLenght;
            }
        }
        private int maxLenght;
        public int MaxLength
        {
            get
            {
                maxLenght = ediHelper_Format.GetMaxLength(this.Format);
                return maxLenght;
            }
        }

        private int length;
        public int Length
        {
            get
            {
                length = MaxLength;
                return length;
            }
            set
            {
                length = value;
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
        {
            this.GL_User = myGLUser;
            this.SQLConIntern = mySQLCon;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {
            string strSQL = "INSERT INTO[dbo].[EdiSegmentElementField] ([EdiSemgentElementId], [Shorcut], [Name], [Status]" +
                                                                            ", [Format], [Description], [constValue], [Position], [Created] " +
                                                                            ", [FormatString], [Code], [SortId], [Kennung], [EdiSegmentId] " +
                                                                            ", [ASNArtId]) " +
                                  "VALUES (" + this.EdiSemgentElementId +
                                            ",'" + this.Shortcut + "'" +
                                            ",'" + this.Name + "'" +
                                            ",'" + this.Status + "'" +
                                            ",'" + this.Description + "'" +
                                            ", " + this.Position +
                                            ",'" + this.Created + "'" +
                                            ", " + this.tmpId +
                                            ",'" + this.Code + "'" +
                                            ", " + this.SortId +
                                            ",'" + this.Kennung + "'" +
                                            ", " + this.EdiSegmentId +
                                            ", " + this.AsnArtId +

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
            string strSQL = "Update EdiSegmentElementField SET " +
                                        "EdiSemgentElementId =" + this.EdiSemgentElementId +
                                        ", Shorcut = '" + this.Shortcut + "' " +
                                        ", Name = '" + this.Name + "'" +
                                        ", Status = '" + this.Status + "'" +
                                        ", Format = '" + this.Format + "'" +
                                        ", Decription ='" + this.Description + "' " +
                                        ", constValue ='" + this.constValue + "' " +
                                        ", Position =" + this.Position +
                                        ", FormatString = '" + this.FormatString + "'" +
                                        ", Code = '" + this.Code + "'" +
                                        ", SortId = " + this.SortId +
                                        ", Kennung = '" + this.Kennung + "'" +
                                        ", EdiSegmentId = " + this.EdiSegmentId +
                                        ", ASNArtId = " + this.AsnArtId +

                                        "WHERE ID=" + this.ID + " ;";

            bool bOK = clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Fill()
        {
            DataTable dt = new DataTable("EdiSegmentElementField");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM EdiSegmentElementField WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegmentElementField");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["EdiSemgentElementId"].ToString(), out iTmp);
                this.EdiSemgentElementId = iTmp;
                this.Shortcut = dt.Rows[i]["Shorcut"].ToString();
                this.Name = dt.Rows[i]["Name"].ToString();
                if (this.Name.Contains("'"))
                {
                    this.Name = this.Name.Replace("'", "");
                }

                this.Status = dt.Rows[i]["Status"].ToString();
                this.Format = dt.Rows[i]["Format"].ToString();
                this.Description = dt.Rows[i]["Description"].ToString();
                if (this.Description.Contains("'"))
                {
                    this.Description = this.Description.Replace("'", "");
                }

                this.constValue = dt.Rows[i]["constValue"].ToString();
                iTmp = 0;
                int.TryParse(dt.Rows[i]["Position"].ToString(), out iTmp);
                this.Position = iTmp;
                this.Created = (DateTime)dt.Rows[i]["Created"];
                this.FormatString = dt.Rows[i]["FormatString"].ToString();


                if (this.EdiSemgentElementId > 0)
                {
                    this.EdiSegmentElement = new clsEdiSegmentElement();
                    this.EdiSegmentElement.InitClass(ref this.GL_User, this.SQLConIntern);
                    this.EdiSegmentElement.ID = this.EdiSemgentElementId;
                    this.EdiSegmentElement.Fill(false);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {

        }

        public List<clsEdiSegmentElementField> GetListBySegmentElementId(int mySegElementId)
        {
            List<clsEdiSegmentElementField> tmpList = new List<clsEdiSegmentElementField>();
            DataTable dt = new DataTable("EdiSegmentElement");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM EdiSegmentElementField WHERE EdiSemgentElementId=" + mySegElementId + " Order by Position;";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiSegmentElement");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                if (iTmp > 0)
                {
                    clsEdiSegmentElementField tmpSEF = new clsEdiSegmentElementField();
                    tmpSEF.InitClass(ref this.GL_User, this.SQLConIntern);
                    tmpSEF.ID = iTmp;
                    tmpSEF.Fill();
                    tmpList.Add(tmpSEF);
                }
            }
            return tmpList;
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
        /// <summary>
        /// 
        /// </summary>
        public List<clsEdiSegmentElementField> GetDefaultEdiFieldsList(string myAsnArt)
        {
            List<clsEdiSegmentElementField> retList = new List<clsEdiSegmentElementField>();
            string strSQL = string.Empty;
            strSQL = "SELECT sef.* " +
                            " FROM EdiSegmentElementField sef " +
                                "INNER JOIN EdiSegmentElement se on se.Id = sef.EdiSemgentElementId " +
                                "INNER JOIN EdiSegment s on s.Id = se.EdiSegmentId " +
                                " WHERE " +
                                " s.ASNArtId=(SELECT ID FROM ASNArt where Typ='" + myAsnArt + "')" +
                                " ; ";
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "VDAClientOut");
            foreach (DataRow r in dt.Rows)
            {
                int iTmpId = 0;
                int.TryParse(r["ID"].ToString(), out iTmpId);
                if (iTmpId > 0)
                {
                    clsEdiSegmentElementField edi = new clsEdiSegmentElementField();
                    edi.InitClass(ref this.GL_User, this.SQLConIntern);
                    edi.ID = iTmpId;
                    edi.Fill();
                    if (!retList.Contains(edi))
                    {
                        retList.Add(edi);
                    }
                }
            }
            return retList;
        }


    }
}
