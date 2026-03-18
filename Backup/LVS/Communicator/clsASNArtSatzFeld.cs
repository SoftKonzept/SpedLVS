using System;
using System.Collections.Generic;
using System.Data;
namespace LVS
{
    public class clsASNArtSatzFeld
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
        internal clsASNValue ASNVal;

        public Dictionary<decimal, clsASNArtSatzFeld> DictASNSatzFeld;
        public List<clsASNArtSatzFeld> ListSatzField;
        //List<string> ListFieldString;

        public decimal ID { get; set; }
        public decimal ASNSatzID { get; set; }
        public Int32 Pos { get; set; }
        public string Datenfeld { get; set; }
        public Int32 Length { get; set; }
        public Int32 VonLength { get; set; }
        public Int32 BisLength { get; set; }
        public string Beschreibung { get; set; }
        public bool IsNum { get; set; }
        public string Datentyp { get; set; }
        public DataTable dtASNSatzField { get; set; }
        public string Kennung { get; set; }
        public string FillValue { get; set; }
        public bool FillLeft { get; set; }
        public string Value { get; set; }  //als Varialbe zur  Zwischenspeicherung

        /**********************************************************************************
         *                              Methoden
         * ********************************************************************************/
        ///<summary>clsASNArtSatzFeld / InitClass</summary>
        ///<remarks>Initialisiert und füllt die Klasse beim Initialisieren</remarks>
        //public void InitClass(ref Globals._GL_USER myGLUser, clsSQLConnections mySQLCon, decimal myASNSatzID)
        public void InitClass(ref Globals._GL_USER myGLUser, clsSQLCOM mySQLCon)
        {
            this.GL_User = myGLUser;
            this.SQLConIntern = mySQLCon;
        }
        ///<summary>clsASNArtSatzFeld / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {

        }
        ///<summary>clsASNArtSatzFeld / Update</summary>
        ///<remarks></remarks>
        public void Update()
        {

        }
        ///<summary>clsASNArtSatzFeld / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("ASNField");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNArtSatzFeld WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNField");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.ASNSatzID = (decimal)dt.Rows[i]["ASNSatzID"];
                this.Pos = (Int32)dt.Rows[i]["Pos"];
                this.Datenfeld = dt.Rows[i]["Datenfeld"].ToString();
                this.Length = (Int32)dt.Rows[i]["Length"];
                this.VonLength = (Int32)dt.Rows[i]["Von"];
                this.BisLength = (Int32)dt.Rows[i]["Bis"];
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                this.IsNum = (bool)dt.Rows[i]["IsNum"];
                this.Datentyp = dt.Rows[i]["Datentyp"].ToString();
                this.Kennung = dt.Rows[i]["Kennung"].ToString();
                this.FillValue = dt.Rows[i]["FillValue"].ToString();
                this.FillLeft = (bool)dt.Rows[i]["FillLeft"];
                //dtASNSatzField = GetSatzFields();
            }
        }

        public void FillDictASNSatzField()
        {

        }
        ///<summary>clsASNArtSatzFeld / FillByASNSatzID</summary>
        ///<remarks>Ermittel den ersten Datensatz mit der entsprechenden SatzID</remarks>
        public void FillByASNSatzID()
        {
            DataTable dt = new DataTable("ASNField");
            string strSQL = string.Empty;
            strSQL = "SELECT TOP(1) * FROM ASNArtSatzFeld WHERE ASNSatzID=" + ASNSatzID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNField");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.ASNSatzID = (decimal)dt.Rows[i]["ASNSatzID"];
                this.Pos = (Int32)dt.Rows[i]["Pos"];
                this.Datenfeld = dt.Rows[i]["Beschreibung"].ToString();
                this.Length = (Int32)dt.Rows[i]["Length"];
                this.VonLength = (Int32)dt.Rows[i]["Von"];
                this.BisLength = (Int32)dt.Rows[i]["Bis"];
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                this.IsNum = (bool)dt.Rows[i]["IsNum"];
                this.Datentyp = dt.Rows[i]["Datentyp"].ToString();
                this.Kennung = dt.Rows[i]["Kennung"].ToString();
                this.FillValue = dt.Rows[i]["FillValue"].ToString();
                this.FillLeft = (bool)dt.Rows[i]["FillLeft"];
                //dtASNSatzField = GetSatzFields();
            }
        }
        ///<summary>clsASNArtSatzFeld / GetSatzFields</summary>
        ///<remarks></remarks>
        public void GetSatzFields()
        {
            this.ListSatzField = new List<clsASNArtSatzFeld>();
            this.dtASNSatzField = new DataTable();
            string strSQL = "Select c.* FROM ASNArtSatzFeld c " +
                                "INNER JOIN ASNArtSatz b ON b.ID = c.ASNSatzID " +
                                "WHERE c.ASNSatzID=" + ASNSatzID + " ORDER By Pos ;";
            dtASNSatzField = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNFields");
            for (Int32 i = 0; i <= dtASNSatzField.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dtASNSatzField.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsASNArtSatzFeld Tmp = new clsASNArtSatzFeld();
                    Tmp.GL_User = this.GL_User;
                    Tmp.ID = decTmp;
                    Tmp.Fill();
                    this.ListSatzField.Add(Tmp);
                }
            }
        }
        ///<summary>clsASNArtSatzFeld / GetSatzFields</summary>
        ///<remarks></remarks>
        public void GetSatzField719()
        {
            this.ListSatzField = new List<clsASNArtSatzFeld>();
            this.dtASNSatzField = new DataTable();
            string strSQL = "Select c.* FROM ASNArtSatzFeld c " +
                                "INNER JOIN ASNArtSatz b ON b.ID = c.ASNSatzID " +
                                "WHERE LTRIM(RTRIM(b.Kennung))='719' ";
            dtASNSatzField = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNFields");
            for (Int32 i = 0; i <= dtASNSatzField.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dtASNSatzField.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsASNArtSatzFeld Tmp = new clsASNArtSatzFeld();
                    Tmp.GL_User = this.GL_User;
                    Tmp.ID = decTmp;
                    Tmp.Fill();
                    this.ListSatzField.Add(Tmp);
                }
            }
        }
        ///<summary>clsASNArtSatzFeld / GetSatzFields</summary>
        ///<remarks></remarks>
        public void GetSatzFieldBySatz(string mySatz)
        {
            this.ListSatzField = new List<clsASNArtSatzFeld>();
            this.dtASNSatzField = new DataTable();
            string strSQL = "Select c.* FROM ASNArtSatzFeld c " +
                                "INNER JOIN ASNArtSatz b ON b.ID = c.ASNSatzID " +
                                "WHERE LTRIM(RTRIM(b.Kennung))='" + mySatz + "' ";
            dtASNSatzField = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNFields");
            for (Int32 i = 0; i <= dtASNSatzField.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dtASNSatzField.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsASNArtSatzFeld Tmp = new clsASNArtSatzFeld();
                    Tmp.GL_User = this.GL_User;
                    Tmp.ID = decTmp;
                    Tmp.Fill();
                    this.ListSatzField.Add(Tmp);
                }
            }
        }
        ///<summary>clsASNArtSatzFeld / CreateClassValueFromASNSatzString</summary>
        ///<remarks></remarks>
        public List<string> CreateClassValueFromASNSatzString(string mySatzStringIN)
        {
            List<string> ListFieldString = new List<string>();
            return ListFieldString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myAsnArtId"></param>
        /// <param name="myGL_USER"></param>
        /// <returns></returns>
        public static DataTable GetASNFieldsByASNArt(int myAsnArtId, Globals._GL_USER myGL_USER)
        {
            string strSQL = "Select c.Kennung " +
                                    ", c.ID " +
                                    ", c.Datenfeld " +
                                    " FROM ASNArtSatzFeld c " +
                                        "INNER JOIN ASNArtSatz b ON b.ID = c.ASNSatzID " +
                                        "INNER JOIN ASNArt a on a.ID= b.ASNArtID " +
                                        "WHERE " +
                                        " a.ID=" + myAsnArtId + " ORDER By c.Kennung;";
            DataTable dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myGL_USER.User_ID, "ASNFields");
            return dt;
        }
    }
}
