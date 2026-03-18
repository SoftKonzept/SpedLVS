using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using LVS;

namespace LVS
{
    public class clsVDAClientValue
    {

        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        internal clsASNArt ASNArt;
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

        public Dictionary<string, List<clsVDAClientValue>> DictVDAClientValue { get; set; }
        public List<clsVDAClientValue> listVDAClientValueSatz { get; set; }
        public DataTable dtVDAClientOutByAdrId
        {
            get
            {                
                return GetList((int)this.AdrID);
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
                Int32 iTmp=0;
                if(!Int32.TryParse(strTmp, out iTmp))
                {
                    iTmp=1;
                }
                return iTmp;
            }
        }

        /**********************************************************************************
         *                      Methoden / Procedure
         * *******************************************************************************/
        ///<summary>clsASN / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, decimal myAdrID)
        {
            this.GL_User = myGLUser;
            this.AdrID = myAdrID;
            InitDictVDAClientValue();
        }
        ///<summary>clsASN / InitDictVDAClientValue</summary>
        ///<remarks></remarks>
        public void InitDictVDAClientValue()
        {
            listVDAClientValueSatz = new List<clsVDAClientValue>();
            DictVDAClientValue = new Dictionary<string, List<clsVDAClientValue>>();

            //DataTable dt = new DataTable("VDAClient");
            //string strSQL = string.Empty;
            //strSQL = "SELECT a.*, c.Kennung as Satz " +
            //                            "FROM VDAClientOUT a " +
            //                            "INNER JOIN ASNArtSatzFeld b ON b.ID=a.ASNFieldID " +
            //                            "INNER JOIN ASNArtSatz c ON c.ID = b.ASNSatzID " +
            //                            "WHERE a.AdrID=" + this.AdrID + " AND a.aktiv=1;";
            //dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClient");

            DataTable dt = new DataTable("VDAClient");
            dt = GetList((int)this.AdrID);
            string strSatz = "711";
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAdrId"></param>
        /// <returns></returns>
        public DataTable GetList(int myAdrId)
        {
            DataTable dt = new DataTable("VDAClient");
            string strSQL = string.Empty;
            strSQL = "SELECT a.*, c.Kennung as Satz " +
                                        "FROM VDAClientOUT a " +
                                        "INNER JOIN ASNArtSatzFeld b ON b.ID=a.ASNFieldID " +
                                        "INNER JOIN ASNArtSatz c ON c.ID = b.ASNSatzID " +
                                        "WHERE a.AdrID=" + myAdrId + " AND a.aktiv=1;";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "VDAClient");
            return dt;
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
                if ((Int32)this.ID ==171)
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
                Int32 iTmp=0;
                Int32.TryParse(dt.Rows[i]["NextSatz"].ToString(), out iTmp);
                this.NextSatz = iTmp;
                this.IsArtSatz = (bool)dt.Rows[i]["ArtSatz"];
                this.FillValue = dt.Rows[i]["FillValue"].ToString();
                this.FillLeft = (bool)dt.Rows[i]["FillLeft"];
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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AddSqlString()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO VDAClientOUT ([AdrID], [ASNFieldID], [ValueArt], [Value], [Fill0], [aktiv] "+
                                                ", [NextSatz], [ArtSatz], [FillValue], [FillLeft] "+
                                                
                                                ") VALUES (" +
                                                    this.AdrID +
                                                    ", " + this.ASNFieldID +
                                                    ", '" + this.ValueArt + "'" +
                                                    ", '" + this.Value + "'" +
                                                    ", " + Convert.ToInt32(this.Fill0) +
                                                    ", " + Convert.ToInt32(this.aktiv) +
                                                    ", " + this.NextSatz +
                                                    ", '" + this.FillValue + "'" +
                                                    ", " + Convert.ToInt32(this.FillLeft) +
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
        /// 
        /// </summary>
        /// <param name="myAdrId"></param>
        /// <param name="myBenutzerId"></param>
        /// <returns></returns>
        public static bool InsertShapeSchemaByAdrId(int mySourceAdrId, int myDestAdrId, decimal myBenutzerId)
        {
            string strSql = string.Empty;
            strSql = "SELECT Count(ID) FROM VDAClientOUT WHERE AdrID=" + myDestAdrId + ";";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSql, myBenutzerId);
            int iTmp = 0;
            int.TryParse(strTmp, out iTmp);
            if (iTmp==0)
            {

            }
            return true;
        }




    }
}
