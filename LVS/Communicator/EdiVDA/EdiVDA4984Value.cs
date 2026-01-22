using System;
using System.Data;


namespace LVS
{
    public class clsEdiVDA4984Value
    {
        public clsSQLCOM SQLConIntern = new clsSQLCOM();
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

        public int ID { get; set; }
        public int DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public int Position { get; set; }
        public string SupplierId { get; set; }
        public string ArtikelVerweis { get; set; }
        public int CallNo { get; set; }
        public DateTime CallDate { get; set; }

        public string OrderNo { get; set; }
        public int DiffQTY { get; set; }    //--- Rückstand
        public int EfzQTY { get; set; }     //--- Eingangsfortschrittszahl
        public DateTime EfzDate { get; set; }
        public int DeliveryQTY { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime ResetFSZDate { get; set; }
        public int SSCQualifier { get; set; }
        public int MandantId { get; set; }
        public int ArbeitsbereichId { get; set; }


        public DataTable dtVDA4984_DocNo { get; set; }
        /********************************************************************************
         *                      Methoden
         * *****************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySQLCon"></param>
        public void InitClass(ref Globals._GL_USER myGLUser, int myMandantenId, int myAbBereichId)
        {
            this.GL_User = myGLUser;
            this.MandantId = myMandantenId;
            this.ArbeitsbereichId = myAbBereichId;
            //this.SQLConIntern = mySQLCon;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySQLCon"></param>
        public void InitClass(ref Globals._GL_USER myGLUser, int myDocNo)
        {
            this.GL_User = myGLUser;
            if (myDocNo > 0)
            {
                this.DocNo = myDocNo;
                FillByDocNo();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddString()
        {
            string strSQL = "INSERT INTO EdiVDA4984Value ([DocNo],[DocDate],[Position],[SupplierId],[ArtikelVerweis],[CallNo],[CallDate],[OrderNo] " +
                                                   ",[DiffQTY],[EfzQTY],[EfzDate],[DeliveryQTY],[DeliveryDate],[ResetFSZDate], [SSCQualifier]" +
                                                   ",[MandantId],[ArbeitsbereichId] " +
                                                  ")" +
                                                "VALUES (" + this.DocNo +
                                                        ",'" + this.DocDate + "'" +
                                                        "," + this.Position +
                                                        ", '" + this.SupplierId + "'" +
                                                        ", '" + this.ArtikelVerweis + "'" +
                                                        ", " + this.CallNo +
                                                        ", '" + this.CallDate + "'" +
                                                        ", '" + this.OrderNo + "'" +
                                                        ", " + this.DiffQTY +
                                                        ", " + this.EfzQTY +
                                                        ", '" + this.EfzDate + "'" +
                                                        ", " + this.DeliveryQTY +
                                                        ", '" + this.DeliveryDate + "'" +
                                                        ", '" + this.ResetFSZDate + "'" +
                                                        ", " + this.SSCQualifier +
                                                        ", " + this.MandantId +
                                                        ", " + this.ArbeitsbereichId +
                                                    ")";
            return strSQL;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {
            string strSQL = AddString();
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
            //string strSQL = "Update EdiSegment SET " +
            //                            "ASNArtId =" + this.ASNArtId +
            //                            ", Name ='" + this.Name + "' " +
            //                            ", Status ='" + this.Status + "' " +
            //                            ", RepeatCount =" + this.RepeatCount +
            //                            ", Ebene = "+ this.Ebene +
            //                            ", Description= '" + this.Description + "'" +

            //                            "WHERE ID=" + ID + " ;";

            //bool bOK = clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
            //if (bOK)
            //{

            //}
        }
        /// <summary>
        /// 
        /// </summary>
        public void FillByDocNo()
        {
            DataTable dt = new DataTable("EdiVDA4984");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM EdiVDA4984Value WHERE DocNo=" + this.DocNo + ";";
            this.dtVDA4984_DocNo = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "EdiVDA4984");
            FillClassValue(this.dtVDA4984_DocNo);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {

        }
        ///<summary>clsASNArtSatz / FillClassValue</summary>
        ///<remarks></remarks>
        private void FillClassValue(DataTable dt)
        {
            for (Int32 i = 0; i <= 0; i++)
            {
                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ID"].ToString(), out iTmp);
                this.ID = iTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["DocNo"].ToString(), out iTmp);
                this.DocNo = iTmp;
                DateTime dtTmp = DateTime.MinValue;
                DateTime.TryParse(dt.Rows[i]["DocDate"].ToString(), out dtTmp);
                this.DocDate = dtTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["Position"].ToString(), out iTmp);
                this.Position = iTmp;
                this.SupplierId = dt.Rows[i]["SupplierId"].ToString();
                this.ArtikelVerweis = dt.Rows[i]["ArtikelVerweis"].ToString();
                iTmp = 0;
                int.TryParse(dt.Rows[i]["CallNo"].ToString(), out iTmp);
                this.CallNo = iTmp;
                dtTmp = DateTime.MinValue;
                DateTime.TryParse(dt.Rows[i]["CallDate"].ToString(), out dtTmp);
                this.CallDate = dtTmp;
                this.OrderNo = dt.Rows[i]["OrderNo"].ToString();
                iTmp = 0;
                int.TryParse(dt.Rows[i]["DiffQTY"].ToString(), out iTmp);
                this.DiffQTY = iTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["EfzQTY"].ToString(), out iTmp);
                this.EfzQTY = iTmp;
                dtTmp = DateTime.MinValue;
                DateTime.TryParse(dt.Rows[i]["EfzDate"].ToString(), out dtTmp);
                this.EfzDate = dtTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["DeliveryQTY"].ToString(), out iTmp);
                this.DeliveryQTY = iTmp;
                dtTmp = DateTime.MinValue;
                DateTime.TryParse(dt.Rows[i]["DeliveryDate"].ToString(), out dtTmp);
                this.DeliveryDate = dtTmp;
                dtTmp = DateTime.MinValue;
                DateTime.TryParse(dt.Rows[i]["Created"].ToString(), out dtTmp);
                this.Created = dtTmp;
                dtTmp = DateTime.MinValue;
                DateTime.TryParse(dt.Rows[i]["ResetFSZDate"].ToString(), out dtTmp);
                this.ResetFSZDate = dtTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["SSCQualifier"].ToString(), out iTmp);
                this.SSCQualifier = iTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["MandantId"].ToString(), out iTmp);
                this.MandantId = iTmp;
                iTmp = 0;
                int.TryParse(dt.Rows[i]["ArbeitsbereichId"].ToString(), out iTmp);
                this.ArbeitsbereichId = iTmp;
            }
        }






    }
}
