using System.Data;
namespace LVS
{
    public class clsASNValue
    {
        public clsSQLCOM SQLConIntern = new clsSQLCOM();
        public const string const_VerweisVersender = "VerweisVersender";
        public const string const_VerweisEmpfaenger = "VerweisEmpfaenger";
        public const string const_VerweisAuftraggeber = "VerweisAuftraggeber";

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
        public decimal ASNID { get; set; }
        public decimal ASNFieldID { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
        public string Kennung { get; set; }

        /***********************************************************************
         *                  Methoden / Procedure
         * *********************************************************************/
        ///<summary>clsASNValue / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_SYSTEM myGLSyste, Globals._GL_USER myGLUser)
        {
            this.GL_User = myGLUser;
            this.GLSystem = myGLSyste;
        }
        ///<summary>clsASNValue / Add</summary>
        ///<remarks></remarks>
        public string Add()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO ASNValue (ASNID, ASNFieldID, FieldName, Value) " +
                                            "VALUES ( @ASNTableID" +
                                                    ", " + ASNFieldID +
                                                    ", '" + FieldName + "'" +
                                                    ", '" + Value + "'" +
                                                    ");";
            return strSql;
        }

        /******************************************************************
         *              public static
         * ***************************************************************/
        ///<summary>clsASNValue / GetASNValueDictByASNId</summary>
        ///<remarks></remarks>
        public static DataTable GetASNValueDataTableByASNId(decimal myBenutzer, decimal myAsnID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ASNValue WHERE ASNID=" + myAsnID + " ORDER BY ID;";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, myBenutzer, "ASNValue");
            return dt;
        }
        ///<summary>clsASNValue / GetASNValueDataTableByASNIdinclKennung</summary>
        ///<remarks></remarks>
        public static DataTable GetASNValueDataTableByASNIdinclKennung(decimal myBenutzer, decimal myAsnID)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select a.*,c.Kennung " +
                                    "FROM ASNValue a " +
                                    "INNER JOIN ASNArtSatzFeld c ON c.ID=a.ASNFieldID " +
                                    " WHERE ASNID=" + myAsnID +
                                    " AND ASNFieldID IN (" +
                                            "148 , 150, 153" +
                                            ", 172, 173, 174, 175, 176, 177, 178 , 179" +
                                            ", 180, 181, 182, 186 , 187, 188, 189" +
                                            ", 190, 191, 192, 193 , 194 , 195, 196, 197, 198, 199" +
                                            ", 200 , 201" +
                                            ")" +
                                    " ORDER BY ID;";
            dt = clsSQLCOM.ExecuteSQL_GetDataTable(strSql, myBenutzer, "ASNValue");
            return dt;
        }
        ///<summary>clsASNValue / UpdateXMLUniportFieldID</summary>
        ///<remarks></remarks>
        public static void UpdateXMLUniportFieldID(decimal myBenutzer, decimal AsnID, decimal myValueForFieldID)
        {
            string strSql = string.Empty;
            strSql = "Update ASNValue " +
                                    "SET Value ='" + myValueForFieldID.ToString() + "' " +
                                    "WHERE ASNID=" + AsnID + " AND FieldName='ID';";
            clsSQLCOM.ExecuteSQL(strSql, myBenutzer);
        }
        ///<summary>clsASNValue / UpdateXMLUniportFieldID</summary>
        ///<remarks></remarks>
        public string GetVerweisFromVDA4913(string VerweisArt)
        {
            string strSql = string.Empty;
            //switch (VerweisArt)
            //{
            //    case clsASNValue.const_VerweisVersender:
            //        strSql = "Select TOP(1) " +
            //                           "((SELECT v.Value FROM ASNValue v WHERE v.ASNFieldID=4 AND v.ASNID=ASNValue.ASNID) " +
            //                           "+'#'+ " +
            //                           "(SELECT w.Value FROM ASNValue w WHERE w.ASNFieldID=16 AND w.ASNID=ASNValue.ASNID)) as Verweis " +
            //                        "FROM ASNValue " +
            //                                " WHERE ASNID IN (" + this.ASNID + ")";
            //        break;
            //    case clsASNValue.const_VerweisEmpfaenger:
            //        strSql = "Select TOP(1) " +
            //                       "((SELECT v.Value FROM ASNValue v WHERE v.ASNFieldID=4 AND v.ASNID=ASNValue.ASNID) " +
            //                       "+'#'+ " +
            //                       "(SELECT w.Value FROM ASNValue w WHERE w.ASNFieldID=46 AND w.ASNID=ASNValue.ASNID)) as Verweis " +
            //                       "FROM ASNValue " +
            //                            " WHERE ASNID IN (" + this.ASNID + ")";
            //        break;
            //    case clsASNValue.const_VerweisAuftraggeber:
            //        strSql = "Select TOP(1) " +
            //                       "((SELECT v.Value FROM ASNValue v WHERE v.ASNFieldID=3 AND v.ASNID=ASNValue.ASNID) " +
            //                       "+'#'+ " +
            //                       "(SELECT w.Value FROM ASNValue w WHERE w.ASNFieldID=4 AND w.ASNID=ASNValue.ASNID)) as Verweis " +
            //                       "FROM ASNValue " +
            //                            " WHERE ASNID IN (" + this.ASNID + ")";
            //        break;

            //}
            //string strTmp = clsSQLCOM.ExecuteSQL_GetValue(strSql, BenutzerID);
            return string.Empty;
        }
    }
}
