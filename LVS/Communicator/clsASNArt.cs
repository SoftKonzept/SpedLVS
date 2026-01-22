using LVS.Constants;
using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsASNArt
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


        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        public Globals._GL_USER GL_User;
        public clsASNArtSatz asnSatz;
        public clsEdiSegment EdiSegment;

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
        public string Typ { get; set; }
        public DateTime Datum { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public List<clsLogbuchCon> ListError;

        /********************************************************************************
         *                      Methoden
         * *****************************************************************************/
        ///<summary>clsASNArt / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(ref Globals._GL_USER myGLUser, clsSQLCOM mySQLCon)
        //public void InitClass(ref Globals._GL_USER myGLUser, clsSQLConnections mySQLCon, decimal myASNID)
        {
            this.GL_User = myGLUser;
            this.SQLConIntern = mySQLCon;
            asnSatz = new clsASNArtSatz();
            asnSatz.InitClass(ref this.GL_User, this.SQLConIntern);

            EdiSegment = new clsEdiSegment();
            EdiSegment.InitClass(ref this.GL_User, this.SQLConIntern);




        }
        ///<summary>clsASNArt / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            string strSQL = "INSERT INTO ASNArt (Typ, Datum, Bezeichnung, Beschreibung) " +
                                  "VALUES ('" + Typ + "' " +
                                         ",'" + Datum + "' " +
                                         ",'" + Bezeichnung + "'" +
                                         ",'" + Beschreibung + "'" +
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
        ///<summary>clsASNArt / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {
            string strSQL = "Update ASNArt SET " +
                                        "Typ ='" + Typ + "'" +
                                        ", Datum ='" + Datum + "' " +
                                        ", Bezeichnung ='" + Bezeichnung + "' " +
                                        ", Beschreibung ='" + Beschreibung + "' " +

                                        "WHERE ID=" + ID + " ;";
            bool bOK = clsSQLCOM.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {

            }
        }
        ///<summary>clsASNArt / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("ASNArt");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNArt WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNArt");
            SetClsValue(dt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsnArt"></param>
        public void FillByAsnArt(string myAsnArt)
        {
            DataTable dt = new DataTable("ASNArt");
            string strSQL = string.Empty;
            strSQL = "SELECT Top(1) * FROM ASNArt WHERE Typ='" + myAsnArt + "' ;";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNArt");
            SetClsValue(dt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void SetClsValue(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Typ = dt.Rows[i]["Typ"].ToString();
                this.Datum = (DateTime)dt.Rows[i]["Datum"];
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
            }

            asnSatz = new clsASNArtSatz();
            asnSatz.InitClass(ref this.GL_User, this.SQLConIntern);

            EdiSegment = new clsEdiSegment();
            EdiSegment.InitClass(ref this.GL_User, this.SQLConIntern);
        }
        ///<summary>clsASNArt / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {

        }
        ///<summary>clsASNArt / CreateASNStrings</summary>
        ///<remarks></remarks>
        //public void CreateASNStrings(string myASNString, ref List<clsASNArtSatz> myListSatz)
        public void CreateASNStrings(string myASNString, clsASN myASN)
        {
            ListError = new List<clsLogbuchCon>();

            if (
                (myASN.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_VDA4905)) ||
                (myASN.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_VDA4913))
              )
            {
                this.asnSatz.Job = myASN.Job;
                this.asnSatz.ListSatz = myASN.ListSatz;
                this.asnSatz.DictVDASatz = myASN.DictVDASatz;
                //this.asnSatz.DictVDA4913Satz = myDictVDA4913Satz;
                this.asnSatz.CreateSatzStringIN(myASNString);
                this.ListError = this.asnSatz.ListError;
                if (this.ListError.Count == 0)
                {
                    this.asnSatz.CreateSatzFieldstringIN();
                }
            }
            else if ((myASN.ASNArt.Typ.Equals(constValue_AsnArt.const_Art_EdifactVDA4984)))
            {
                //myASN.ASNArt.EdiSegment 
            }
        }


        public void CheckAsnArtTableForUpdate()
        {

        }

        /*********************************************************************************
        *                   static
        *********************************************************************************/
        ///<summary>clsASNArt / CreateASNStrings</summary>
        ///<remarks></remarks>
        //public void CreateASNStrings(string myASNString, ref List<clsASNArtSatz> myListSatz)
        public static decimal GetWatchDogID()
        {
            decimal decReturn = 0;
            DataTable dt = new DataTable("ASNArt");
            string strSQL = string.Empty;
            strSQL = "SELECT Top(1) ID FROM ASNArt WHERE Typ='" + constValue_AsnArt.const_Art_WatchDog + "';";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, 0);
            decimal.TryParse(strTmp, out decReturn);
            return decReturn;
        }
        ///<summary>clsASNTyp / GetASNTypList</summary>
        ///<remarks></remarks>
        public static DataTable GetASNArtList(decimal myBenutzer)
        {
            DataTable dt = new DataTable("ASNArt");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNArt Order BY Id";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myBenutzer, "ASNArt");
            return dt;
        }
        ///<summary>clsASNTyp / GetASNArtIdVDA4913</summary>
        ///<remarks></remarks>
        public static int GetASNArtIdVDA4913(decimal myBenutzer)
        {
            DataTable dt = new DataTable("ASNArt");
            string strSQL = string.Empty;
            strSQL = "SELECT ID FROM ASNArt WHERE Typ='" + constValue_AsnArt.const_Art_VDA4913 + "';";
            int iTmp = 0;
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, myBenutzer);
            int.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myASNArtName"></param>
        /// <returns></returns>
        public static clsASNArt GetASNArt(Globals._GL_USER myGLUser, string myASNArtName)
        {
            clsASNArt asnArt = new clsASNArt();
            asnArt.InitClass(ref myGLUser, null);
            asnArt.FillByAsnArt(myASNArtName);

            //DataTable dt = new DataTable("ASNArt");
            //string strSQL = string.Empty;
            //strSQL = "SELECT ID FROM ASNArt WHERE Typ='" + myASNArtName + "';";
            //int iTmp = 0;
            //string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            //int.TryParse(strTmp, out iTmp);
            //if (iTmp > 0)
            //{
            //    asnArt.ID = iTmp;

            //}
            return asnArt;
        }
    }
}
