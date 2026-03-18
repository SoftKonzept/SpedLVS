using System;
using System.Collections;
using System.Data;


namespace LVS
{
    public class clsFahrzeuge
    {
        public Globals._GL_SYSTEM GLSystem;
        internal clsSystem Sys;
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
        //************************************

        public ArrayList AuflRecourceList = new ArrayList();
        public ArrayList FahrRecourceList = new ArrayList();
        public ArrayList VehiRecourceList = new ArrayList();

        private decimal _ID;
        public decimal ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                CheckZM(value);
            }
        }
        public string KFZ { get; set; }
        public string Fabrikat { get; set; }
        public string Bezeichnung { get; set; }
        public Int32 KIntern { get; set; }
        public string FGNr { get; set; }
        public DateTime Tuev { get; set; }
        public DateTime SP { get; set; }
        public DateTime BJ { get; set; }
        public DateTime seit { get; set; }
        public DateTime Abmeldung { get; set; }
        public Int32 Laufleistung { get; set; }
        public char ZM { get; set; }
        public char Anhaenger { get; set; }
        public char Plane { get; set; }
        public char Sattel { get; set; }
        public char Coil { get; set; }
        public Int32 Leergewicht { get; set; }
        public Int32 zlGG { get; set; }
        public decimal Innenhoehe { get; set; }
        public Int32 Stellplaetze { get; set; }
        public string Besonderheit { get; set; }
        public decimal Laenge { get; set; }
        public string AbgasNorm { get; set; }
        public Int32 Achsen { get; set; }
        //public string Besitzer { get; set; }
        public decimal MandantenID { get; set; }


        /*****************************************************************************************
         *                      Methoden / Procedure
         * **************************************************************************************/
        ///<summary>clsFahrzeuge/InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, clsSystem myClsSystem)
        {
            this.GL_User = myGLUser;
            this.GLSystem = myGLSystem;
            this.Sys = myClsSystem;
        }
        ///<summary>clsFahrzeuge / Copy</summary>
        ///<remarks></remarks>
        public clsFahrzeuge Copy()
        {
            return (clsFahrzeuge)this.MemberwiseClone();
        }
        ///<summary>clsFahrzeuge / AddItem</summary>
        ///<remarks></remarks>
        public void AddItem()
        {
            try
            {
                string strSQL = string.Empty;
                strSQL = "INSERT INTO Fahrzeuge " +
                                                  "( KFZ, " +
                                                  "KIntern, " +
                                                  "Fabrikat, " +
                                                  "Bezeichnung, " +
                                                  "FGNr, " +
                                                  "Tuev, " +
                                                  "SP, " +
                                                  "BJ, " +
                                                  "seit, " +
                                                  "Laufleistung, " +
                                                  "ZM, " +
                                                  "Anhaenger, " +
                                                  "Plane, " +
                                                  "Sattel, " +
                                                  "Coil, " +
                                                  "Leergewicht, " +
                                                  "zlGG, " +
                                                  "Innenhoehe, " +
                                                  "Stellplaetze, " +
                                                  "Besonderheit, " +
                                                  "bis, " +
                                                  "Laenge," +
                                                  "Abgas, " +
                                                  "Achsen, " +
                                                  "MandantenID)" +

                                          "VALUES " +
                                                   "('" + KFZ + "','"
                                                   + KIntern + "','"
                                                   + Fabrikat + "','"
                                                   + Bezeichnung + "','"
                                                   + FGNr + "','"
                                                   + Tuev + "','"
                                                   + SP + "','"
                                                   + BJ + "','"
                                                   + seit + "','"
                                                   + Laufleistung + "','"
                                                   + ZM + "','"
                                                  + Anhaenger + "','"
                                                  + Plane + "','"
                                                  + Sattel + "','"
                                                  + Coil + "','"
                                                  + Leergewicht + "','"
                                                  + zlGG + "','"
                                                  + Innenhoehe.ToString().Replace(",", ".") + "','"
                                                  + Stellplaetze + "','"
                                                  + Besonderheit + "','"
                                                  + Abmeldung + "', '"
                                                  + Laenge.ToString().Replace(",", ".") + "', '"
                                                  + AbgasNorm + "', '"
                                                  + Achsen + "', '"
                                                  + MandantenID + "');";

                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            //Add Logbucheintrag Eintrag
            string Beschreibung = "Fahrzeug: " + KFZ + " hinzugefügt";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
        }

        ///<summary>clsFahrzeuge / updateItem</summary>
        ///<remarks>Update Fahrzeugdaten</remarks>
        public void updateItem(decimal fID)
        {
            ID = fID;
            try
            {
                string strSQL = string.Empty;
                strSQL = "Update Fahrzeuge SET KFZ='" + KFZ + "'" +
                                               ", KIntern=" + KIntern +
                                               ", Fabrikat='" + Fabrikat + "'" +
                                               ", Bezeichnung='" + Bezeichnung + "'" +
                                               ", FGNr='" + FGNr + "'" +
                                               ", Tuev='" + Tuev + "'" +
                                               ", SP='" + SP + "'" +
                                               ", BJ='" + BJ + "'" +
                                               ", seit='" + seit + "'" +
                                               ", Laufleistung='" + Laufleistung + "'" +
                                               ", ZM='" + ZM + "'" +
                                               ", Anhaenger='" + Anhaenger + "'" +
                                               ", Plane='" + Plane + "'" +
                                               ", Sattel='" + Sattel + "'" +
                                               ", Coil='" + Coil + "'" +
                                               ", Leergewicht=" + Leergewicht +
                                               ", zlGG=" + zlGG +
                                               ", Innenhoehe='" + Innenhoehe.ToString().Replace(",", ".") + "'" +
                                               ", Stellplaetze=" + Stellplaetze +
                                               ", Besonderheit='" + Besonderheit + "'" +
                                               ", bis ='" + Abmeldung + "'" +
                                               ", Laenge ='" + Laenge.ToString().Replace(",", ".") + "'" +
                                               ", Abgas='" + AbgasNorm + "'" +
                                               ", Achsen =" + Achsen +
                                               ", MandantenID =" + MandantenID +
                                               " " +
                                               " WHERE ID=" + ID + ";";

                clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());

            }
            //Add Logbucheintrag update
            string Beschreibung = "Fahrzeug: " + KFZ + " - ID: " + ID + " geändert";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
        }
        ///<summary>clsFahrzeuge / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable FahrzTable = new DataTable();
            string strSQL = string.Empty;
            strSQL = "Select a.*" +
                              ", (Select Matchcode FROM Mandanten WHERE ID=a.MandantenID) as Besitzer" +
                              " From Fahrzeuge a " +
                              "WHERE ID=" + ID + ";";

            FahrzTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Fahrzeuge");

            if (FahrzTable.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= FahrzTable.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(FahrzTable.Rows[i]["ID"].ToString(), out decTmp);
                    ID = decTmp;
                    KFZ = FahrzTable.Rows[i]["KFZ"].ToString();
                    Int32 iTmp = 0;
                    Int32.TryParse(FahrzTable.Rows[i]["KIntern"].ToString(), out iTmp);
                    KIntern = iTmp;
                    Fabrikat = FahrzTable.Rows[i]["Fabrikat"].ToString();
                    Bezeichnung = FahrzTable.Rows[i]["Bezeichnung"].ToString();
                    FGNr = FahrzTable.Rows[i]["FGNr"].ToString();
                    DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    DateTime.TryParse(FahrzTable.Rows[i]["Tuev"].ToString(), out dtTmp);
                    Tuev = dtTmp;
                    dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    DateTime.TryParse(FahrzTable.Rows[i]["SP"].ToString(), out dtTmp);
                    SP = dtTmp;
                    dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    DateTime.TryParse(FahrzTable.Rows[i]["BJ"].ToString(), out dtTmp);
                    BJ = dtTmp;
                    dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    DateTime.TryParse(FahrzTable.Rows[i]["seit"].ToString(), out dtTmp);
                    seit = dtTmp;
                    ZM = Convert.ToChar(FahrzTable.Rows[i]["ZM"]);
                    Anhaenger = Convert.ToChar(FahrzTable.Rows[i]["Anhaenger"]);
                    Plane = Convert.ToChar(FahrzTable.Rows[i]["Plane"]);
                    Sattel = Convert.ToChar(FahrzTable.Rows[i]["Sattel"]);
                    Coil = Convert.ToChar(FahrzTable.Rows[i]["Coil"]);
                    iTmp = 0;
                    Int32.TryParse(FahrzTable.Rows[i]["Leergewicht"].ToString(), out iTmp);
                    Leergewicht = iTmp;
                    iTmp = 0;
                    Int32.TryParse(FahrzTable.Rows[i]["zlGG"].ToString(), out iTmp);
                    zlGG = iTmp;
                    decTmp = 0;
                    Decimal.TryParse(FahrzTable.Rows[i]["Innenhoehe"].ToString(), out decTmp);
                    Innenhoehe = decTmp;
                    iTmp = 0;
                    Int32.TryParse(FahrzTable.Rows[i]["Stellplaetze"].ToString(), out iTmp);
                    Stellplaetze = iTmp;

                    if (FahrzTable.Rows[i]["Besonderheit"] is DBNull)
                    {
                        Besonderheit = string.Empty;
                    }
                    else
                    {
                        Besonderheit = (string)FahrzTable.Rows[i]["Besonderheit"];
                    }
                    dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                    DateTime.TryParse(FahrzTable.Rows[i]["bis"].ToString(), out dtTmp);
                    Abmeldung = dtTmp;
                    decTmp = 0;
                    Decimal.TryParse(FahrzTable.Rows[i]["Laenge"].ToString(), out decTmp);
                    Laenge = decTmp;
                    if (FahrzTable.Rows[i]["Abgas"] is DBNull)
                    {
                        AbgasNorm = string.Empty;
                    }
                    else
                    {
                        AbgasNorm = FahrzTable.Rows[i]["Abgas"].ToString();
                    }
                    iTmp = 0;
                    Int32.TryParse(FahrzTable.Rows[i]["Achsen"].ToString(), out iTmp);
                    Achsen = iTmp;
                    decTmp = 0;
                    Decimal.TryParse(FahrzTable.Rows[i]["MandantenID"].ToString(), out decTmp);
                    MandantenID = decTmp;
                    //Besitzer = FahrzTable.Rows[i]["Besitzer"].ToString();
                }
            }
        }
        ///<summary>clsFahrzeuge / CheckZM</summary>
        ///<remarks></remarks>
        private void CheckZM(decimal ID)
        {
            string strSQL = "SELECT ZM  FROM FAHRZEUGE WHERE ID = " + ID;
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            char chTmp = Convert.ToChar("Y");
            Char.TryParse(strTmp, out chTmp);
            this.ZM = chTmp;
        }
        ///<summary>clsFahrzeuge / GetAufliegerListe</summary>
        ///<remarks>Aufliegerdaten für Aufliegerliste Disposition</remarks>
        public DataTable GetAufliegerListe()
        {
            DataTable dataTable = new DataTable();
            string strsQL = "SELECT " +
                                            "ID, " +
                                            "KFZ, " +
                                            "Fabrikat " +
                                            "FROM Fahrzeuge WHERE Anhaenger='T' ORDER BY KFZ ";

            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strsQL, BenutzerID, "Fahrzeugdaten");
            return dataTable;

        }
        ///<summary>clsFahrzeuge / DeleteFahrzeug</summary>
        ///<remarks>löschen Datensatz</remarks>
        public void DeleteFahrzeug()
        {
            //Add Logbucheintrag Löschen
            KFZ = GetKFZByID(this.GL_User, ID);
            string Beschreibung = "Fahrzeug: " + KFZ + " - ID: " + ID + " gelöscht";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

            string strSQL = "DELETE FROM Fahrzeuge WHERE ID=" + ID + " ;";


        }
        /****************************************************************************************************************
         *                                      public / static
         * *************************************************************************************************************/
        ///<summary>clsFahrzeuge / IsKFZIn</summary>
        ///<remarks>Check ob KFZ Kennzeichen schon vorhanden </remarks>
        public static bool IsKFZIn(Globals._GL_USER myGLUser, string _Kennzeichen)
        {
            bool IsIn = false;
            string strSQL = "SELECT ID From Fahrzeuge WHERE KFZ= '" + _Kennzeichen + "'";
            IsIn = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
            return IsIn;
        }
        ///<summary>clsFahrzeuge / GetRecByID</summary>
        ///<remarks>Read Record from Vehicle </remarks>
        public static DataSet GetRecByID(Globals._GL_USER myGLUser, decimal ID)
        {
            DataSet ds = new DataSet();
            string strSQL = "SELECT * FROM Fahrzeuge WHERE ID=" + ID + ";";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Fahzeugdaten");
            ds.Tables.Add(dt);
            return ds;
        }
        ///<summary>clsFahrzeuge / GetVehicleList</summary>
        ///<remarks>Read ZM for Dispoplan</remarks>
        public static DataTable GetFahrzeuge_ZM(decimal decBenutzerID)
        {
            DataTable FahrzTable = new DataTable();

            string strSql = string.Empty;
            strSql = "SELECT ID FROM FAHRZEUGE WHERE ZM='T' Order By KIntern";
            FahrzTable = clsSQLcon.ExecuteSQL_GetDataTable(strSql, decBenutzerID, "ZM");
            return FahrzTable;
        }
        ///<summary>clsFahrzeuge / GetVehicleList</summary>
        ///<remarks></remarks>
        public static DataTable GetVehicleList(Globals._GL_USER myGLUser, string aktuelleListe)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            string strSQL = string.Empty;
            strSQL = "Select a.ID" +
                              ", a.KFZ" +
                              ", a.Fabrikat" +
                              ", a.ZM" +
                              ", a.Anhaenger" +
                              ", a.Tuev" +
                              ", a.SP" +
                              ", (Select Matchcode FROM Mandanten WHERE ID=a.MandantenID) as Besitzer" +
                              ", a.KIntern as intID" +
                              ", a.MandantenID" +
                              " From Fahrzeuge a ";

            switch (aktuelleListe)
            {
                //sortiert nach KIntern
                case "snKIntern":
                    strSQL = strSQL +
                             "WHERE a.bis='" + DateTime.MaxValue.Date + "' ORDER BY a.KIntern, a.ZM DESC";
                    break;

                //sortiert nach KFZ-Kennzeichen aktuelle
                case "snKFZKennAktuell":
                    strSQL = strSQL +
                    "WHERE a.bis='" + DateTime.MaxValue.Date + "' ORDER BY a.KFZ";
                    break;

                //sortiert nach KFZ-Kennzeichen aktuelle
                case "snKFZKennALL":
                    strSQL = strSQL +
                    "ORDER BY a.KFZ";
                    break;
            }
            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Fahrzeuge");
            return dataTable;
        }
        ///<summary>clsFahrzeuge / ExistInterneNr</summary>
        ///<remarks></remarks>
        public static bool ExistInterneNr(Globals._GL_USER myGLUser, Int32 iNummer)
        {
            bool exist = false;
            string strSQL = "SELECT KFZ From Fahrzeuge WHERE KIntern=" + iNummer + " AND ZM='T'";
            exist = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, myGLUser.User_ID);
            return exist;
        }
        ///<summary>clsFahrzeuge / GetMaxKennIntern</summary>
        ///<remarks></remarks>
        public static Int32 GetMaxKennIntern(Globals._GL_USER myGLUser)
        {
            string strSQL = "SELECT MAX(KIntern) From Fahrzeuge WHERE ZM= 'T'";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            return iTmp;
        }
        ///<summary>clsFahrzeuge / GetMaxKennIntern</summary>
        ///<remarks></remarks>
        public static DataTable GetFahrzeuge_AufliegerforCombo(Globals._GL_USER myGLUser)
        {
            DataTable FahrzTable = new DataTable();
            string strSQL = "SELECT ID, KFZ FROM FAHRZEUGE WHERE ZM='F' Order By KFZ";
            FahrzTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Fahrzeuge");
            return FahrzTable;
        }
        ///<summary>clsFahrzeuge / GetVehicleListZM</summary>
        ///<remarks></remarks>
        public static DataTable GetVehicleListZM(Globals._GL_USER myGLUser)
        {
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            string strSQL = string.Empty;
            strSQL = "Select a.ID" +
                              ", a.KFZ" +
                              ", a.Fabrikat" +
                              ", a.ZM" +
                              ", a.Anhaenger" +
                              ", a.Tuev" +
                              ", a.SP" +
                              ", (Select Matchcode FROM Mandanten WHERE ID=a.MandantenID) as Besitzer" +
                              ", a.KIntern as intID" +
                              ", a.MandantenID" +
                              " From Fahrzeuge a " +
                              "WHERE a.bis>'" + DateTime.Now.Date + "' AND ZM='T' ORDER BY a.KFZ";
            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Fahrzeuge");
            return dataTable;
        }
        ///<summary>clsFahrzeuge / updateInterneIDByFahrzeugID</summary>
        ///<remarks>interne ID update</remarks>
        public static void updateInterneIDByFahrzeugID(Globals._GL_USER myGLUser, decimal FahrzeugID, Int32 iKID)
        {
            string strSQL = "Update Fahrzeuge SET KIntern=" + iKID + " WHERE ID=" + FahrzeugID + " ;";
            clsSQLcon.ExecuteSQL(strSQL, myGLUser.User_ID);
        }
        ///<summary>clsFahrzeuge / GetKInternFahrzeug</summary>
        ///<remarks></remarks>
        public static string GetKInternFahrzeug(Globals._GL_USER myGLUser, Int32 iKIntern, bool bo_ZM)
        {
            string strRetrun = string.Empty;
            string strSQL = string.Empty;
            if (bo_ZM)
            {
                strSQL = "SELECT KFZ From Fahrzeuge WHERE KIntern= " + iKIntern + " AND ZM='T'";
            }
            else
            {
                strSQL = "SELECT KFZ From Fahrzeuge WHERE KIntern= " + iKIntern + " AND ZM='F'";
            }
            strRetrun = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            return strRetrun;
        }
        ///<summary>clsFahrzeuge / GetKFZByID</summary>
        ///<remarks></remarks>
        public static string GetKFZByID(Globals._GL_USER myGLUSer, decimal ID)
        {
            string strSQL = "SELECT KFZ  FROM FAHRZEUGE WHERE ID = " + ID;
            string KFZ = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUSer.User_ID);
            return KFZ;
        }
        ///<summary>clsFahrzeuge / GetFahrzeuge_ZMforCombo</summary>
        ///<remarks></remarks>
        public static DataTable GetFahrzeuge_ZMforCombo(Globals._GL_USER myGLUser)
        {
            DataTable FahrzTable = new DataTable();
            string strSQL = "SELECT ID, KFZ FROM FAHRZEUGE WHERE ZM='T' Order By KFZ";
            FahrzTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Fahrzeuge");
            return FahrzTable;
        }
    }
}
