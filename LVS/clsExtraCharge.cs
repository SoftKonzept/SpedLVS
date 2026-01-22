using System;
using System.Data;

namespace LVS
{
    public class clsExtraCharge
    {
        public Globals._GL_USER _GL_User;

        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = _GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public decimal ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public bool IsGlobal { get; set; }
        public DateTime Erstellt { get; set; }
        public string Einheit { get; set; }
        public decimal Preis { get; set; }
        public string RGText { get; set; }
        public decimal ArbeitsbereichID { get; set; }
        public decimal AdrID { get; set; }
        public decimal KontoID { get; set; }
        internal bool _IsUsed;
        public bool IsUsed
        {
            get
            {
                if (ExistExtraCharge(ID, BenutzerID))
                {
                    _IsUsed = IsExtraChargeUsed(ID, BenutzerID);
                }
                else
                {
                    _IsUsed = false;
                }
                return _IsUsed;
            }
            set
            {
                _IsUsed = value;
            }
        }



        /***********************************************************************************
         *                          Procedure / Methoden
         * ********************************************************************************/
        ///<summary>clsExtraCharge / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser)
        {
            this._GL_User = myGLUser;
        }
        ///<summary>clsExtraCharge / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            Erstellt = DateTime.Now;
            string strSQL = string.Empty;
            strSQL = "INSERT INTO ExtraCharge (Bezeichnung, Beschreibung, IsGlobal, erstellt,RGText, ArbeitsbereichID, " +
                                               " Einheit, Preis, UserID, AdrID, KontoID) " +
                                            "VALUES ('" + Bezeichnung + "'" +
                                                    ", '" + Beschreibung + "'" +
                                                    "," + Convert.ToInt32(IsGlobal) +
                                                    ", '" + Erstellt + "'" +
                                                    ", '" + RGText + "'" +
                                                    ", " + ArbeitsbereichID +
                                                    ", '" + Einheit + "'" +
                                                    ", '" + Preis.ToString().Replace(",", ".") + "'" +
                                                    ", " + BenutzerID +
                                                    ", " + AdrID +
                                                    ", " + KontoID +
                                                    "); ";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                //Add Logbucheintrag Eintrag
                string beschreibung = "Extrakosten : [" + this.ID + "] - " + Bezeichnung + " hinzugefügt";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), beschreibung);
            }
        }
        ///<summary>clsExtraCharge / Fill</summary>
        ///<remarks></remarks>
        public void Fill()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ExtraCharge WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ExtraCharge");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                this.IsGlobal = (bool)dt.Rows[i]["IsGlobal"];
                this.Erstellt = (DateTime)dt.Rows[i]["Erstellt"];
                this.RGText = dt.Rows[i]["RGText"].ToString();
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ArbeitsbereichID"].ToString(), out decTmp);
                this.ArbeitsbereichID = decTmp;
                this.Einheit = dt.Rows[i]["Einheit"].ToString();
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["Preis"].ToString(), out decTmp);
                this.Preis = decTmp;
                this.AdrID = (decimal)dt.Rows[i]["AdrID"];
                this.KontoID = (decimal)dt.Rows[i]["KontoID"];
            }
        }
        ///<summary>clsExtraCharge / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM ExtraCharge WHERE ID=" + ID;
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bDeleteOK)
            {
                string logBeschreibung = "Nebenkosten: [" + this.ID + "] - " + Bezeichnung + "  gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), logBeschreibung);
            }
        }
        ///<summary>clsExtraCharge / Update</summary>
        ///<remarks>Update Daten.</remarks>
        public void Update()
        {
            if (this.ID > 0)
            {
                string strSQL = string.Empty;
                Erstellt = DateTime.Now;
                strSQL = "Update ExtraCharge SET " +
                                            "Bezeichnung='" + Bezeichnung + "'" +
                                            ", Beschreibung ='" + Beschreibung + "'" +
                                            ", IsGlobal =" + Convert.ToInt32(this.IsGlobal) +
                                            ", RGText='" + this.RGText + "'" +
                                            ", ArbeitsbereichID=" + this.ArbeitsbereichID +
                                            ", Einheit='" + Einheit + "'" +
                                            ", Preis='" + Preis.ToString().Replace(",", ".") + "'" +
                                            ", KontoID= " + KontoID +
                                                            " WHERE ID=" + ID + ";";

                bool bUpdateOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
                if (bUpdateOK)
                {
                    string logBeschreibung = "Sonderkosten: [" + this.ID + "] - " + Bezeichnung + "  geändert";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), logBeschreibung);
                }
            }
        }
        ///<summary>clsExtraCharge / GetExtraChargeList</summary>
        ///<remarks>Ermittelt alle Extrakosten des Arbeitsbereichs und alle globalen Extrakosten</remarks>
        public DataTable GetExtraChargeList()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM ExtraCharge " +
                                "WHERE  " +
                                    " IsGlobal=1 OR " +
                                    " ArbeitsbereichID=" + this.ArbeitsbereichID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "ExtraCharge");
            return dt;
        }


        /**************************************************************************************************
         *                      public Static Procedures
         * ***********************************************************************************************/
        ///<summary>clsExtraCharge / GetExtraCharge</summary>
        ///<remarks>Update Daten.</remarks>
        public static DataTable GetExtraCharge(Globals._GL_USER myGLUser, decimal _AdrID = 0)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            if (_AdrID > 0)
            {
                strSql = "Select a.ID, a.Bezeichnung, a.RGText," +
                            "case " +
                            "when (b.Preis is NULL) Then a.Preis " +
                            "else  b.Preis " +
                            "end as Preis, " +
                                           "case " +
                            "when (b.Preis is NULL) Then Cast(0 as bit) " +
                            "else  Cast(1 as bit) " +
                            "end as Kundenbezogen " +
                            "from ExtraCharge a " +
                            "left join (Select * from ExtraChargeADR where AdrId = " + _AdrID + ") as  b on  a.ID = b.ExtraChargeID; ";

            }
            else
            {
                strSql = "SELECT * FROM ExtraCharge;";
            }
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "ExtraCharge");
            return dt;
        }

        public override string ToString()
        {
            return this.Bezeichnung;
        }

        private static bool IsExtraChargeUsed(decimal myID, decimal decBenutzerID)
        {
            bool bIsUsed = false;
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ExtraChargeAssignment WHERE ExtraChargeID=" + myID + ";";
            bIsUsed = clsSQLcon.ExecuteSQL_GetValueBool(strSql, decBenutzerID);
            return bIsUsed;
        }

        private static bool ExistExtraCharge(decimal myID, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM ExtraCharge WHERE ID='" + myID + "'";
            bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, decBenutzerID);
            return reVal;
        }
    }
}
