using System;
using System.Data;


namespace LVS
{
    public class clsMandanten
    {
        ///<summary>clsMandanten
        ///         Eigenschaften:
        ///             - ID
        ///             - Beschreibung
        ///             - MatchCode
        ///             - ADR_ID
        ///             - aktiv
        ///             - BenutzerID</summary>

        //************  User  ***************
        internal Globals._GL_USER GL_User;

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
        //************************************
        clsADR cADR = new clsADR();

        public decimal ID { get; set; }
        public string Beschreibung { get; set; }
        public string MatchCode { get; set; }
        public decimal ADR_ID { get; set; }
        public bool aktiv { get; set; }
        public bool Default_Sped { get; set; }
        public bool Default_Lager { get; set; }
        public string VDA4905Verweis { get; set; }
        public string ReportPath { get; set; }

        /*********************************************************************************
         * 
         * ******************************************************************************/

        public clsMandanten()
        {
        }
        public clsMandanten(int myUserId) : this()
        {
            this.GL_User = new Globals._GL_USER();
            this.GL_User.User_ID = myUserId;
        }
        public clsMandanten(int myUserId, int myMandantId) : this()
        {
            this.GL_User = new Globals._GL_USER();
            this.GL_User.User_ID = myUserId;
            if (myMandantId > 0)
            {
                this.ID = myMandantId;
                this.GetMandantByID();
            }
        }

        ///<summary>clsMandanten / InitCls</summary>
        ///<remarks></remarks>
        public void InitCls(Globals._GL_USER myGLUser, int myMandantenId)
        {
            this.GL_User = myGLUser;
            if (myMandantenId > 0)
            {
                this.ID = myMandantenId;
                this.GetMandantByID();
            }
        }
        ///<summary>clsMandanten / Add</summary>
        ///<remarks>Eintrag eines neuen Datensatzes in der DB. Falls einer der Default Werte (Sped, Lager) 
        ///         true ist, dann muss vorher ein Update dieser Spalte über die gesamte Table gemacht werden,
        ///         da immer nur ein Mandant den Default Wert = true haben kann</remarks>
        public bool Add()
        {
            //Baustelle 06.03.2013
            bool bEintragOK = false;
            if (ADR_ID > 0)
            {
                string strSql = string.Empty;
                //CHeck der Default-Werte
                //Sped
                if (Default_Sped)
                {
                    strSql = strSql + "Update Mandanten Set Default_Sped=0; ";
                }
                //Lager
                if (Default_Lager)
                {
                    strSql = strSql + "Update Mandanten Set Default_Lager=0; ";
                }
                //Insert
                strSql = strSql + "INSERT INTO Mandanten (ADR_ID, Matchcode, Beschreibung, aktiv, Default_Sped, Default_Lager, VDA4905Verweis,ReportPath) " +
                                               "VALUES (" + ADR_ID + ",'"
                                                           + MatchCode + "','"
                                                           + Beschreibung + "', "
                                                           + Convert.ToInt32(aktiv) + ", "
                                                           + Convert.ToInt32(Default_Sped) + ","
                                                           + Convert.ToInt32(Default_Lager) +
                                                           ", '" + VDA4905Verweis + "'" +
                                                           ", '" + this.ReportPath + "'" +
                                                           "); ";

                bEintragOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "MandantenInsert", BenutzerID);
                //bEintragOK=clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                //Eintrag OK dann Defaul PrimeKeys setzen
                if (bEintragOK)
                {
                    clsPrimeKeys pk = new clsPrimeKeys();
                    pk.BenutzerID = BenutzerID;
                    pk.Mandanten_ID = GetMandantenIDbyMatchcode();
                    pk.AddNewPrimeKeys();
                }
            }
            return bEintragOK;
        }
        ///<summary>clsMandanten / CheckButtons</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public bool UpdateMandant()
        {
            bool bTransActionOK = false;
            if (ID > 0)
            {
                string strSql = string.Empty;
                //CHeck der Default-Werte
                //Sped
                if (Default_Sped)
                {
                    strSql = strSql + "Update Mandanten Set Default_Sped=0; ";
                }
                //Lager
                if (Default_Lager)
                {
                    strSql = strSql + "Update Mandanten Set Default_Lager=0; ";
                }
                strSql = strSql + " Update Mandanten SET " +
                                                    "ADR_ID =" + ADR_ID + " " +
                                                    ", Matchcode = '" + MatchCode + "'" +
                                                    ", Beschreibung ='" + Beschreibung + "'" +
                                                    ", aktiv =" + Convert.ToInt32(aktiv) + " " +
                                                    ", Default_Sped =" + Convert.ToInt32(Default_Sped) + " " +
                                                    ", Default_Lager =" + Convert.ToInt32(Default_Lager) + " " +
                                                    ", VDA4905Verweis ='" + VDA4905Verweis + "' " +
                                                    ", ReportPath = '" + this.ReportPath + "'" +

                                                    " WHERE ID='" + ID + "'";
                bTransActionOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "MandantenUpdate", BenutzerID);
            }
            return bTransActionOK;
        }
        ///<summary>clsMandanten / ExistMandantByMC</summary>
        ///<remarks>Prüft anhand des Matchcodes ob der Matchcode/Mandant bereits existiert.</remarks>
        ///<param name="strMC">strMC >>> Matchcode</param>
        ///<param name="decBenutzerID">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        public static bool ExistMandantByMC(string strMC, decimal decBenutzerID)
        {
            bool boVal = true;
            strMC = strMC.Trim();
            if (strMC != string.Empty)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Mandanten WHERE Matchcode='" + strMC + "'";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Arbeitsbereiche");

                if (dt.Rows.Count == 0)
                {
                    boVal = false;
                }
            }
            return boVal;
        }
        ///<summary>clsMandanten / ExistMandantByID</summary>
        ///<remarks>Prüft anhand der Mandanten ID, ob der Mandant existiert.</remarks>
        ///<param name="decIDC">decIDC >>> Mandanten ID</param>
        ///<param name="decBenutzerID">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        public static bool ExistMandantByID(decimal decIDC, decimal decBenutzerID)
        {
            bool boVal = true;
            if (decIDC > 0)
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "Select * FROM Mandanten WHERE ID='" + decIDC + "'";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "Arbeitsbereiche");

                if (dt.Rows.Count == 0)
                {
                    boVal = false;
                }
            }
            return boVal;
        }
        ///<summary>clsMandanten / GetMandatenList</summary>
        ///<remarks>Statische Funktion, die alle Mandanten ausliest.</remarks>
        ///<param name="decBenutzer">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        public static DataTable GetMandatenList(decimal decBenutzer)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select ID AS Mandanten_ID, ADR_ID, Matchcode, Beschreibung, aktiv FROM Mandanten ORDER BY ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzer, "Mandantenliste");
            return dt;
        }
        ///<summary>clsMandanten / AddRowEmptyToDataTableMandenList</summary>
        ///<remarks>fügt der Tabelle eine leere Zeile hinzu</remarks>
        public static void AddRowEmptyToDataTableMandenList(ref DataTable dt)
        {
            DataRow row = dt.NewRow();
            row["Mandanten_ID"] = 0;
            row["ADR_ID"] = 0;
            row["Matchcode"] = string.Empty;
            row["Beschreibung"] = string.Empty;
            row["aktiv"] = 1;
            dt.Rows.Add(row);
            dt.DefaultView.Sort = "Mandanten_ID";
        }
        ///<summary>clsMandanten / GetMandatenListAktiv</summary>
        ///<remarks>Statische Funktion, die alle aktive Mandanten ausliest.</remarks>
        ///<param name="decBenutzer">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        public static DataTable GetMandatenListAktiv(decimal decBenutzer)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select ID AS Mandanten_ID, ADR_ID, Matchcode, Beschreibung, aktiv, Default_Sped, Default_Lager FROM Mandanten " +
                     "WHERE aktiv='1' ORDER BY ID ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzer, "Mandantenliste");
            return dt;
        }
        ///<summary>clsMandanten / GetMandantByID</summary>
        ///<remarks>Ermittelt die Mandantendaten anhand der MandantenID und setzt direkt die Eigenschaftsdaten der Klasse.</remarks>
        ///<returns>Returns Boolean </returns>
        public Boolean GetMandantByID()
        {
            bool bOK = false;
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select * FROM Mandanten WHERE ID=" + (int)ID;
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Mandantenliste");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                ID = (decimal)dt.Rows[i]["ID"];
                ADR_ID = (decimal)dt.Rows[i]["ADR_ID"];
                MatchCode = dt.Rows[i]["Matchcode"].ToString();
                Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                aktiv = (bool)dt.Rows[i]["aktiv"];
                Default_Lager = (bool)dt.Rows[i]["Default_Lager"];
                Default_Sped = (bool)dt.Rows[i]["Default_Sped"];
                this.VDA4905Verweis = dt.Rows[i]["VDA4905Verweis"].ToString();
                this.ReportPath = dt.Rows[i]["ReportPath"].ToString();

                bOK = true;
            }
            return bOK;
        }
        ///<summary>clsMandanten / GetMandantenIDbyMatchcode</summary>
        ///<remarks>Ermittelt die MandantenID anhand des Matchcodes.</remarks>
        ///<returns>Returns decimal Mandanten ID </returns>
        private decimal GetMandantenIDbyMatchcode()
        {
            string strMandantID = string.Empty;
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "Select ID FROM Mandanten WHERE Matchcode='" + MatchCode + "'";
            strMandantID = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);

            if (strMandantID == string.Empty)
            {
                strMandantID = "0";
            }
            return Convert.ToDecimal(strMandantID);
        }
        ///<summary>clsMandanten / GetMandantenIDFromAuftragByAuftragPosTableID</summary>
        ///<remarks>Ermittelt die Mandanten ID anhand der AuftragPosTableID aus der Datenbank.</param>
        ///<param name="decBenutzerID">decBenutzerID >>> User ID zum Eintrag in die LOG DB.</param>
        ///<param name="myAuftragPostTableID">>> AuftragPosTableID.</param>
        public static decimal GetMandantenIDFromAuftragByAuftragPosTableID(decimal myAuftragPostTableID, decimal decBenutzerID)
        {
            decimal decTmp = 0;
            if (myAuftragPostTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select b.MandantenID FROM AuftragPos a " +
                                                        "INNER JOIN Auftrag b ON b.ID=a.AuftragTableID " +
                                                        "WHERE a.ID='" + myAuftragPostTableID + "' ";
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, decBenutzerID);

                decimal.TryParse(strTmp, out decTmp);
            }
            return decTmp;
        }
    }
}
