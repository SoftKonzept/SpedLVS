using System;
using System.Data;

namespace LVS
{
    public class clsQueue
    {
        public Globals._GL_USER GL_User;
        public clsASN ASN;
        public clsASNTyp ASNTyp;
        public clsArbeitsbereiche Arbeitsbereich;
        public clsASNAction ASNActionCls;
        public clsASNArt ASNArt;

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
        public string TableName { get; set; }
        public decimal TableID { get; set; }
        public DateTime Datum { get; set; }
        public decimal ASNTypID { get; set; }
        public bool activ { get; set; }
        public decimal ASNID { get; set; }
        public decimal AdrVerweisID { get; set; }
        public Int32 ASNAction { get; set; }     // ASN prozess nicht die ID aus ASNAction
        public bool IsVirtFile { get; set; }
        public int ASNActionTableID { get; set; }
        public bool UseOldPropertyValue { get; set; }
        public int AbBereichID { get; set; }
        public bool IsCreateByASNMesTestCtr { get; set; }
        public string Description { get; set; }

        public int ASNArtId { get; set; }

        /*****************************************************************************************
         *                                      Methoden 
         * **************************************************************************************/
        ///<summary>clsQueue / GetQueueActiv</summary>
        ///<remarks>Ermittelt die aktiven / passiven Prozesse</remarks>
        public DataTable GetQueue(bool bActiv, bool bSingleMod)
        {
            string strSQL = string.Empty;
            if (bSingleMod)
            {
                strSQL = "Select TOP(1)a.*, b.Typ as ASNTyp " +
                                    "From [Queue] a " +
                                    "INNER JOIN ASNTyp b ON b.TypID=a.ASNTypID ";
            }
            else
            {
                strSQL = "Select a.*, b.Typ as ASNTyp " +
                                    "From [Queue] a " +
                                    "INNER JOIN ASNTyp b ON b.TypID=a.ASNTypID ";
            }

            if (bActiv)
            {
                strSQL = strSQL + " WHERE activ=1 Order by ID;";
            }
            else
            {
                strSQL = strSQL + " WHERE activ=0 Order by ID;";
            }
            // DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "aktiveProzess");
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "aktiveProzess");
            return dt;
        }
        ///<summary>clsQueue / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSQL = "INSERT INTO QUEUE (TableName, TableID, Datum, ASNTypID, activ, ASNID, AdrVerweisID, ASNAction, IsVirtFile, " +
                                                "ASNActionTableID, UseOldPropertyValue, AbBereichID, IsCreateByASNMesTestCtr, Description, ASNArtId) " +
                                  "VALUES ('" + TableName + "' " +
                                         "," + TableID +
                                         ",'" + Datum + "'" +
                                         "," + ASNTypID +
                                         "," + Convert.ToInt32(activ) +
                                         "," + ASNID +
                                         ", " + AdrVerweisID +
                                         ", " + ASNAction +
                                         ", " + Convert.ToInt32(this.IsVirtFile) +
                                         ", " + ASNActionTableID +
                                         ", " + Convert.ToInt32(this.UseOldPropertyValue) +
                                         ", " + this.AbBereichID +
                                         ", " + Convert.ToInt32(this.IsCreateByASNMesTestCtr) +
                                         ", '" + this.Description + "'" +
                                         ", " + this.ASNArtId +
                                         ")";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
            }
        }
        ///<summary>clsQueue / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("Queue");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Queue WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Queue");
            decimal decTmp = 0;
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                this.ID = decTmp;
                this.TableName = dt.Rows[i]["TableName"].ToString();
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["TableID"].ToString(), out decTmp);
                this.TableID = decTmp;
                this.Datum = (DateTime)dt.Rows[i]["Datum"];
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ASNTypID"].ToString(), out decTmp);
                this.ASNTypID = decTmp;
                this.activ = (bool)dt.Rows[i]["activ"];
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ASNID"].ToString(), out decTmp);
                this.ASNID = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrVerweisID"].ToString(), out decTmp);
                this.AdrVerweisID = decTmp;
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ASNAction"].ToString(), out iTmp);
                this.ASNAction = iTmp;
                this.IsVirtFile = (bool)dt.Rows[i]["IsVirtFile"];
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ASNActionTableID"].ToString(), out iTmp);
                this.ASNActionTableID = iTmp;
                this.UseOldPropertyValue = (bool)dt.Rows[i]["UseOldPropertyValue"];
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["AbBereichID"].ToString(), out iTmp);
                this.AbBereichID = iTmp;
                this.IsCreateByASNMesTestCtr = (bool)dt.Rows[i]["IsCreateByASNMesTestCtr"];
                this.Description = dt.Rows[i]["Description"].ToString();
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ASNArtId"].ToString(), out iTmp);
                this.ASNArtId = iTmp;

                //ASN
                ASN = new clsASN();
                ASN.GL_User = this.GL_User;
                ASN.ID = this.ASNID;
                ASN.Fill();

                ASNTyp = new clsASNTyp();
                ASNTyp.InitClass(ref this.GL_User);
                ASNTyp.ID = 0;
                ASNTyp.TypID = (Int32)this.ASNTypID;
                ASNTyp.FillbyTypID();

                ASNArt = new clsASNArt();
                ASNArt.InitClass(ref this.GL_User, null);
                ASNArt.ID = this.ASNArtId;
                ASNArt.Fill();

                Arbeitsbereich = new clsArbeitsbereiche();
                Arbeitsbereich.InitCls(this.GL_User, this);

                ASNActionCls = new clsASNAction();
                ASNActionCls.InitClass(ref this.GL_User);
                ASNActionCls.ID = this.ASNActionTableID;
                ASNActionCls.Fill();

            }
        }
        ///<summary>clsQueue / UpdateActivToFalse</summary>
        ///<remarks></remarks>
        public void UpdateActivToFalse()
        {
            string strSQL = string.Empty;
            strSQL = "Update Queue " +
                            "SET activ=0 " +
                            "WHERE ID=" + ID + ";";
            clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
        }
        ///<summary>clsQueue / Delete</summary>
        ///<remarks></remarks>
        public bool Delete()
        {
            string strSQL = string.Empty;
            strSQL = "Delete Queue WHERE ID=" + (Int32)ID + ";";
            bool bReturn = clsSQLCOM.ExecuteSQLWithTRANSACTION(strSQL, "QueueDelete", BenutzerID);
            return bReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <returns></returns>
        public static DataTable GetQueue(Globals._GL_USER myGLUser)
        {
            string strSQL = string.Empty;
            strSQL = "Select a.*, b.Typ as ASNTyp " +
                        " From [Queue] a " +
                        "INNER JOIN ASNTyp b ON b.TypID=a.ASNTypID " +
                        " Order by ID desc";

            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Queue");
            return dt;
        }

    }
}
