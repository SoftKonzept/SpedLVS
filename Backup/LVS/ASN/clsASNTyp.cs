using System;
using System.Collections.Generic;
using System.Data;


namespace LVS
{
    public class clsASNTyp
    {
        public const Int32 const_ASNTyp_LSL = 1;
        public const Int32 const_ASNTyp_EML = 2;
        public const Int32 const_ASNTyp_EME = 3;
        public const Int32 const_ASNTyp_AML = 4;
        public const Int32 const_ASNTyp_AME = 5;
        public const Int32 const_ASNTyp_AVL = 6;
        public const Int32 const_ASNTyp_AVE = 7;
        public const Int32 const_ASNTyp_STL = 8;
        public const Int32 const_ASNTyp_STE = 9;
        public const Int32 const_ASNTyp_AbE = 10;
        public const Int32 const_ASNTyp_TAA = 11;
        public const Int32 const_ASNTyp_AVA = 12;
        public const Int32 const_ASNTyp_AbA = 14;
        public const Int32 const_ASNTyp_LFE = 15;  //Liefereinteilung VDA4905
        public const Int32 const_ASNTyp_RLL = 16;
        public const Int32 const_ASNTyp_RLE = 17;
        public const Int32 const_ASNTyp_TSL = 18;
        public const Int32 const_ASNTyp_TSE = 19;
        public const Int32 const_ASNTyp_BML = 20;
        public const Int32 const_ASNTyp_BME = 21;
        public const Int32 const_ASNTyp_UBL = 22;
        public const Int32 const_ASNTyp_UBE = 23;



        public const string const_string_ASNTyp_LSL = "LSL";
        public const string const_string_ASNTyp_EML = "EML";
        public const string const_string_ASNTyp_EME = "EME";
        public const string const_string_ASNTyp_AML = "AML";
        public const string const_string_ASNTyp_AME = "AME";
        public const string const_string_ASNTyp_AVL = "AVL";
        public const string const_string_ASNTyp_AVE = "AVE";
        public const string const_string_ASNTyp_STL = "STL";
        public const string const_string_ASNTyp_STE = "STE";
        public const string const_string_ASNTyp_AbE = "AbE";
        public const string const_string_ASNTyp_TAA = "TAA";
        public const string const_string_ASNTyp_AVA = "AVA";
        public const string const_string_ASNTyp_AbA = "AbA";
        public const string const_string_ASNTyp_LFE = "LFE";  //Liefereinteilung VDA4905
        public const string const_string_ASNTyp_RLL = "RLL";
        public const string const_string_ASNTyp_RLE = "RLE";
        public const string const_string_ASNTyp_TSL = "TSL";
        public const string const_string_ASNTyp_TSE = "TSE";
        public const string const_string_ASNTyp_BME = "BME";
        public const string const_string_ASNTyp_BML = "BML";
        public const string const_string_ASNTyp_UBE = "UBE";
        public const string const_string_ASNTyp_UBL = "UBL";

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
        public string Typ { get; set; }
        public Int32 TypID { get; set; }
        public string Beschreibung { get; set; }

        public Dictionary<Int32, clsASNTyp> dictASNTypCls;
        public Dictionary<string, Int32> dictASNTyp;


        /**********************************************************************
         *                  Methoden / Procedure
         * *******************************************************************/
        ///<summary>clsASNTyp / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(ref Globals._GL_USER myGLUser)
        {
            this.GL_User = myGLUser;
        }
        public void InitClass(ref Globals._GL_USER myGLUser, decimal myId)
        {
            this.GL_User = myGLUser;
            this.ID = myId;
            this.Fill();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string AddSql()
        {
            string strSql = "INSERT INTO ASNTyp ([Typ],[Beschreibung], [TypID]) " +
                                        " VALUES ('" +
                                                  this.Typ + "'" +
                                                  ", '" + this.Beschreibung + "'" +
                                                  ", " + this.TypID +
                                                  "); ";
            return strSql;
        }
        ///<summary>clsASNTyp / FillDictASNTyp</summary>
        ///<remarks></remarks>
        public void FillDictASNTyp()
        {
            dictASNTypCls = new Dictionary<Int32, clsASNTyp>();
            dictASNTyp = new Dictionary<string, Int32>();

            DataTable dt = new DataTable("ASNTyp");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNTyp;";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, this.BenutzerID, "ASNTyp");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsASNTyp tmpTyp = new clsASNTyp();
                    tmpTyp.GL_User = this.GL_User;
                    tmpTyp.ID = decTmp;
                    tmpTyp.Fill();

                    this.dictASNTypCls.Add(tmpTyp.TypID, tmpTyp);
                    this.dictASNTyp.Add(tmpTyp.Typ.ToString(), tmpTyp.TypID);
                }
            }
        }
        ///<summary>clsASNTyp / Fill</summary>
        ///<remarks></remarks>
        public void FillbyTypID()
        {
            DataTable dt = new DataTable("ASNTyp");
            string strSQL = string.Empty;
            strSQL = "SELECT Top(1) * FROM ASNTyp WHERE TypID=" + this.TypID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNTyp");
            FillClassbyDatatable(ref dt);
        }
        ///<summary>clsASNTyp / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable("ASNTyp");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNTyp WHERE ID=" + ID + ";";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "ASNTyp");
            FillClassbyDatatable(ref dt);
        }
        ///<summary>clsASNTyp / FillClassbyDatatable</summary>
        ///<remarks></remarks>
        private void FillClassbyDatatable(ref DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Typ = dt.Rows[i]["Typ"].ToString();
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["TypID"].ToString(), out iTmp);
                this.TypID = iTmp;
            }
        }
        /*************************************************************************
         *                      public static
         * **********************************************************************/
        ///<summary>clsASNTyp / GetASNTypDict</summary>
        ///<remarks></remarks>
        public static Dictionary<Int32, string> GetASNTypDict(decimal myBenutzer)
        {
            Dictionary<Int32, string> dict = new Dictionary<Int32, string>();
            DataTable dt = new DataTable("ASNTyp");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNTyp Order BY TypID";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myBenutzer, "ASNTyp");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decID = (decimal)dt.Rows[i]["ID"];
                string strTyp = dt.Rows[i]["Typ"].ToString();
                Int32 itmp = 0;
                Int32.TryParse(dt.Rows[i]["TypID"].ToString(), out itmp);
                if (itmp > 0)
                {
                    dict.Add(itmp, strTyp);
                }
            }
            return dict;
        }
        ///<summary>clsASNTyp / GetASNTypList</summary>
        ///<remarks></remarks>
        public static DataTable GetASNTypList(decimal myBenutzer)
        {
            DataTable dt = new DataTable("ASNTyp");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM ASNTyp Order BY TypID";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSQL, myBenutzer, "ASNTyp");
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="myTyp"></param>
        /// <returns></returns>
        public static decimal GetAsnTypId(Globals._GL_USER myGLUser, string myTyp)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT TypID FROM ASNTyp WHERE Typ='" + myTyp + "';";
            string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }

        public clsASNTyp Copy()
        {
            return (clsASNTyp)this.MemberwiseClone();
        }
    }
}
