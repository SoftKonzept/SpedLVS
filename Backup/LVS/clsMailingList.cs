using System;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsMailingList
    {
        public clsMailingListAssignment MailingListAssignment;
        public Globals._GL_USER _GL_User;
        public Globals._GL_SYSTEM _GL_System;

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
        //************************************

        public decimal ID { get; set; }
        public string Bezeichnung { get; set; }
        public decimal AdrID { get; set; }
        public DateTime Erstellt { get; set; }
        public decimal Benuzter { get; set; }
        public string Beschreibung { get; set; }
        public decimal ArbeitsbereichID { get; set; }


        public Dictionary<decimal, string> DictMailingList { get; set; }
        public DataTable dtMailingList { get; set; }
        public List<String> ListMailadressen { get; set; }


        /****************************************************************************************************
         *                      Procedure Mailing List
         * *************************************************************************************************/
        ///<summary>clsMailingList / InitClass</summary>
        ///<remarks></remarks>>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, decimal myAdrId)
        {
            this._GL_User = myGLUser;
            this._GL_System = myGLSystem;
            this.AdrID = myAdrId;
            this.ArbeitsbereichID = this._GL_System.sys_ArbeitsbereichID;

            MailingListAssignment = new clsMailingListAssignment();
            MailingListAssignment._GL_User = this._GL_User;

            GetTOPMailingListIDByAdrIDAndArbeitsBereich(this.AdrID, this.ArbeitsbereichID);
        }
        ///<summary>clsMailingList / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            Erstellt = DateTime.Now;
            string strSQL = string.Empty;
            strSQL = "INSERT INTO MailingList (Bezeichnung, AdrID, erstellt, Benutzer, Beschreibung, Arbeitsbereich) " +
                                            "VALUES ('" + Bezeichnung + "'" +
                                                    " , " + AdrID +
                                                    " , '" + Erstellt + "'" +
                                                    " , " + BenutzerID +
                                                    " , '" + Beschreibung + "'" +
                                                    " , " + ArbeitsbereichID +
                                                    ");";
            strSQL = strSQL + " Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                this.FillByID();
                //Add Logbucheintrag Eintrag
                string beschreibung = "Mailing List: " + ID + " - " + Bezeichnung + " hinzugefügt";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), beschreibung);
            }
        }
        ///<summary>clsMailingList / Update</summary>
        ///<remarks></remarks>>
        public void Update()
        {
            string strSQL = string.Empty;
            strSQL = "Update MailingList SET " +
                                            "Bezeichnung='" + Bezeichnung + "'" +
                                             ", AdrID='" + AdrID + "'" +
                                             ", Beschreibung ='" + Beschreibung + "' " +
                                             ", Arbeitsbereich =" + ArbeitsbereichID +
                                             " WHERE ID=" + ID + ";";
            bool bOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {
                this.FillByID();
                //Add Logbucheintrag update
                string beschreibung = "Verteiler: [" + this.ID + "] - Verteilername:" + Bezeichnung + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), beschreibung);
            }
        }

        ///<summary>clsMailingList / Fill</summary>
        ///<remarks></remarks>>
        public void FillByID()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM MailingList WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "MailingList");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.Bezeichnung = dt.Rows[i]["Bezeichnung"].ToString();
                this.AdrID = (decimal)dt.Rows[i]["AdrID"];
                this.Erstellt = (DateTime)dt.Rows[i]["Erstellt"];
                this.Benuzter = (decimal)dt.Rows[i]["Benutzer"];
                this.Beschreibung = dt.Rows[i]["Beschreibung"].ToString();
                this.ArbeitsbereichID = (decimal)dt.Rows[i]["Arbeitsbereich"];

                FillDictMailingList(this.AdrID, this.ArbeitsbereichID);

                MailingListAssignment = new clsMailingListAssignment();
                MailingListAssignment._GL_User = this._GL_User;
                MailingListAssignment.FillList(this.ID);
            }
        }
        ///<summary>clsMailingList / FillDictMailingList</summary>
        ///<remarks></remarks>>
        public void FillDictMailingList(decimal myAdrID, decimal myArbeitsbereichID)
        {
            if ((myAdrID > 0) & (myArbeitsbereichID > 0))
            {
                this.DictMailingList = new Dictionary<decimal, string>();
                dtMailingList = new DataTable();
                string strSql = string.Empty;
                strSql = "SELECT * FROM MailingList WHERE AdrID=" + myAdrID + " AND Arbeitsbereich=" + myArbeitsbereichID + ";";
                dtMailingList = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "MailingList");
                for (Int32 i = 0; i <= dtMailingList.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(dtMailingList.Rows[i]["ID"].ToString(), out decTmp);
                    DictMailingList.Add(decTmp, dtMailingList.Rows[i]["Bezeichnung"].ToString());
                }
            }
        }
        ///<summary>clsMailingList / Delete</summary>
        ///<remarks></remarks>
        public void Delete()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM MailingList WHERE ID=" + ID + " ;";
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bDeleteOK)
            {
                //Add Logbucheintrag Löschen
                string beschreibung = "Mailing List: " + this.ID + " - " + this.Bezeichnung + " gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), beschreibung);
                //Mailinglist muss neu geladen werden
                this.FillDictMailingList(this.AdrID, this.ArbeitsbereichID);
            }
        }
        ///<summary>clsMailingList / ExistMailingList</summary>
        ///<remarks>Prüft, ob </remarks>
        public static bool ExistMailingListName(Globals._GL_USER myGLUser, string myMailingListName, decimal myAdrID, decimal myArbeitsbereichID)
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM MailingList WHERE Bezeichnung='" + myMailingListName + "' " +
                                                        "AND AdrID=" + myAdrID + " " +
                                                        "AND Arbeitsbereich=" + myArbeitsbereichID +
                                                        " ;";
            bool bOK = clsSQLcon.ExecuteSQL_GetValueBool(strSql, myGLUser.User_ID);
            return bOK;
        }
        ///<summary>clsMailingList / ExistMailingList</summary>
        ///<remarks>Prüft, ob </remarks>
        private void GetTOPMailingListIDByAdrIDAndArbeitsBereich(decimal myAdrID, decimal myArbeitsbereichID)
        {
            string strSql = string.Empty;
            strSql = "SELECT TOP(1) ID FROM MailingList WHERE AdrID=" + myAdrID + " " +
                                                        "AND Arbeitsbereich=" + myArbeitsbereichID +
                                                        " ;";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            Decimal.TryParse(strTmp, out decTmp);
            this.ID = decTmp;
            if (this.ID > 0)
            {
                this.FillByID();
            }
        }

        /*************************************************************************************************
         *                                public static Procedures
         * **********************************************************************************************/
        ///<summary>clsMailingList / GetAllMailingList</summary>
        ///<remarks>Prüft, ob </remarks>
        public static DataTable GetAllMailingList(Globals._GL_USER myGLUser)
        {
            DataTable dt = new DataTable("MailingList");
            string strSQL = "SELECT CAST(0 as bit) as 'Select'" +
                                        ", a.* " +
                                        ", c.ViewID as Firma" +
                                        ", d.Nachname" +
                                        ", d.Vorname" +
                                        ", d.Mail" +
                                    " FROM MailingList a " +
                                    "INNER JOIN MailingListAssignment b ON b.MailingListID =a.ID " +
                                    "INNER JOIN ADR c ON c.ID=a.AdrID " +
                                    "INNER JOIN Kontakte d ON d.ID =b.KontaktID ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "MailingList");
            return dt;
        }

        ///<summary>clsMailingList / FillListMailAdressenForAutoJournal</summary>
        ///<remarks></remarks>>
        public void FillListMailAdressenForAuto(decimal myAdrID, string myConstString, bool bAdd = false)
        {
            if (!bAdd)
            {
                ListMailadressen = new List<string>();
            }
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT DISTINCT d.Mail " +
                                "FROM MailingList a " +
                                "INNER JOIN MailingListAssignment b ON b.MailingListID =a.ID " +
                                "INNER JOIN ADR c ON c.ID=a.AdrID " +
                                "INNER JOIN Kontakte d ON d.ID =b.KontaktID " +
                                        "WHERE a.Bezeichnung = '" + myConstString + "' " +
                                        " AND a.AdrID=" + myAdrID + " ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "MailingList");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                ListMailadressen.Add(dt.Rows[i]["Mail"].ToString());
            }
        }
        ///<summary>clsMailingList / FillListMailAdressenForAutoJournal</summary>
        ///<remarks></remarks>>
        public void FillListMailAdressenForAutoJournal(decimal myAdrID)
        {
            ListMailadressen = new List<string>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT DISTINCT d.Mail " +
                                "FROM MailingList a " +
                                "INNER JOIN MailingListAssignment b ON b.MailingListID =a.ID " +
                                "INNER JOIN ADR c ON c.ID=a.AdrID " +
                                "INNER JOIN Kontakte d ON d.ID =b.KontaktID " +
                                        "WHERE a.Bezeichnung = '" + clsCronJobs.const_autoJournal + "' " +
                                        " AND a.AdrID=" + myAdrID + " ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "MailingList");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                ListMailadressen.Add(dt.Rows[i]["Mail"].ToString());
            }
        }
        ///<summary>clsMailingList / FillListMailAdressenForAutoJournal</summary>
        ///<remarks></remarks>>
        public void FillListMailAdressenForAutoBestand(decimal myAdrID, string myConstString)
        {
            ListMailadressen = new List<string>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT DISTINCT d.Mail " +
                                "FROM MailingList a " +
                                "INNER JOIN MailingListAssignment b ON b.MailingListID =a.ID " +
                                "INNER JOIN ADR c ON c.ID=a.AdrID " +
                                "INNER JOIN Kontakte d ON d.ID =b.KontaktID " +
                                        "WHERE a.Bezeichnung = '" + myConstString + "' " +
                                        " AND a.AdrID=" + myAdrID + " ";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "MailingList");

            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                ListMailadressen.Add(dt.Rows[i]["Mail"].ToString());
            }
        }
        ///<summary>clsMailingList / GetAllMailingList</summary>
        ///<remarks>Prüft, ob </remarks>
        public static List<decimal> GetAutoMailingList(Globals._GL_USER myGLUser, string myAutoList)
        {
            List<decimal> ListAdrID = new List<decimal>();
            DataTable dt = new DataTable("MailingList");
            string strSQL = "SELECT DISTINCT a.AdrID " +
                                    " FROM MailingList a " +
                                    "INNER JOIN MailingListAssignment b ON b.MailingListID =a.ID " +
                                    "INNER JOIN ADR c ON c.ID=a.AdrID " +
                                    "INNER JOIN Kontakte d ON d.ID =b.KontaktID " +
                                    " WHERE a.Bezeichnung = '" + myAutoList + "'";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "MailingList");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    ListAdrID.Add(decTmp);
                }
            }
            return ListAdrID;
        }
        ///<summary>clsMailingList / GetAllMailingList</summary>
        ///<remarks>Prüft, ob </remarks>
        public static List<decimal> GetAutoMailingList(int myAdrId, string myAutoList)
        {
            List<decimal> ListAdrID = new List<decimal>();
            DataTable dt = new DataTable("MailingList");
            string strSQL = "SELECT DISTINCT a.AdrID " +
                                    " FROM MailingList a " +
                                    "INNER JOIN MailingListAssignment b ON b.MailingListID =a.ID " +
                                    "INNER JOIN ADR c ON c.ID=a.AdrID " +
                                    "INNER JOIN Kontakte d ON d.ID =b.KontaktID " +
                                    " WHERE a.Bezeichnung = '" + myAutoList + "' and a.AdrID=" + myAdrId;

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, 1, "MailingList");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["AdrID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    ListAdrID.Add(decTmp);
                }
            }
            return ListAdrID;
        }
        public static List<clsMailingList> GetAllAuto(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem, string myAutoList)
        {
            List<clsMailingList> ListMailingList = new List<clsMailingList>();
            DataTable dt = new DataTable("MailingList");
            string strSQL = "SELECT DISTINCT a.ID " +
                                    " FROM MailingList a " +
                                    "INNER JOIN MailingListAssignment b ON b.MailingListID =a.ID " +
                                    "INNER JOIN ADR c ON c.ID=a.AdrID " +
                                    "INNER JOIN Kontakte d ON d.ID =b.KontaktID " +
                                    " WHERE a.Bezeichnung = '" + myAutoList + "'";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "MailingList");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    ListMailingList.Add(clsMailingList.GetSingle(decTmp, myGLUser, myGLSystem));
                }
            }
            return ListMailingList;
        }

        public static clsMailingList GetSingle(decimal decTmp, Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            clsMailingList ml = new clsMailingList();
            ml._GL_User = myGLUser;
            ml._GL_System = myGLSystem;
            ml.ID = decTmp;
            ml.FillByID();
            return ml;
        }


    }
}
