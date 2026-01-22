using System;
using System.Data;


namespace LVS
{
    public class clsKontakte
    {
        public clsMailingList MailingList;
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


        private DataTable _dtKontakte; // = new DataTable();

        public DataTable dtKontakte
        {
            get
            {
                _dtKontakte = GetADRKontakte();
                return _dtKontakte;
            }
            set { _dtKontakte = value; }
        }

        public decimal ID { get; set; }
        public string ViewID { get; set; }
        public decimal ADR_ID { get; set; }
        public string Mail { get; set; }
        public string Nachname { get; set; }
        public string Abteilung { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public string Info { get; set; }
        public string Vorname { get; set; }
        public string Anrede { get; set; }
        public string Mobil { get; set; }
        public DateTime Birthday { get; set; }


        public DataTable dtMailKontakte { get; set; }


        /********************************************************************************************************
         *                                              Methoden
         * ******************************************************************************************************/
        ///<summary>clsKontakte / add()</summary>
        ///<remarks>Eintrag neuer Kontaktdaten</remarks>
        public void Add()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO Kontakte (ViewID, ADR_ID, Mail, Nachname, Abteilung, Telefon, Fax, Info, Vorname, Anrede, " +
                                          "Mobil, Birthday) " +
                                "VALUES ('" + ViewID + "'" +
                                         ",'" + ADR_ID + "'" +
                                         ",'" + Mail + "'" +
                                         ",'" + Nachname + "'" +
                                         ",'" + Abteilung + "'" +
                                         ",'" + Telefon + "'" +
                                         ",'" + Fax + "'" +
                                         ",'" + Info + "'" +
                                         ",'" + Vorname + "'" +
                                         ",'" + Anrede + "'" +
                                         ",'" + Mobil + "'" +
                                         ",'" + Birthday + "'" +

                                         ")";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                Fill();
                //Add Logbucheintrag Eintrag
                string beschreibung = "Kontakte: " + ViewID + " hinzugefügt";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), beschreibung);
            }
        }
        ///<summary>clsKontakte / Update</summary>
        ///<remarks>Update Kontaktdaten</remarks>
        public void Update()
        {
            string strSQL = string.Empty;
            strSQL = "Update Kontakte SET ViewID='" + ViewID + "'" +
                                             ", ADR_ID='" + ADR_ID + "'" +
                                             ", Abteilung='" + Abteilung + "'" +
                                             ", Nachname='" + Nachname + "'" +
                                             ", Mail='" + Mail + "'" +
                                             ", Telefon='" + Telefon + "'" +
                                             ", Fax='" + Fax + "'" +
                                             ", Info='" + Info + "' " +
                                             ", Vorname='" + Vorname + "' " +
                                             ", Anrede='" + Anrede + "' " +
                                             ", Mobil='" + Mobil + "' " +
                                             ", Birthday='" + Birthday + "' " +
                                             "WHERE ID='" + ID + "'";
            bool bOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {
                //Add Logbucheintrag update
                string beschreibung = "Kontakt: " + ViewID + " geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), beschreibung);
            }
        }
        ///<summary>clsKontakte / Update</summary>
        ///<remarks>Update Kontaktdaten</remarks>
        public void Fill()
        {
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Kontakte WHERE ID=" + ID;
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kontakte");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.ViewID = dt.Rows[i]["ViewID"].ToString();
                    this.ADR_ID = (decimal)dt.Rows[i]["ADR_ID"];
                    this.Abteilung = dt.Rows[i]["Abteilung"].ToString();
                    this.Nachname = dt.Rows[i]["Nachname"].ToString();
                    this.Mail = dt.Rows[i]["Mail"].ToString();
                    this.Telefon = dt.Rows[i]["Telefon"].ToString();
                    this.Fax = dt.Rows[i]["Fax"].ToString();
                    this.Info = dt.Rows[i]["Info"].ToString();
                    this.Vorname = dt.Rows[i]["Vorname"].ToString();
                    this.Anrede = dt.Rows[i]["Anrede"].ToString();
                    this.Mobil = dt.Rows[i]["Info"].ToString();
                    DateTime tmpBirth = DateTime.MinValue;
                    DateTime.TryParse(dt.Rows[i]["Birthday"].ToString(), out tmpBirth);
                    this.Birthday = tmpBirth;

                    //Alle Kontakte zu einer Adresse
                    //dtKontakte = GetADRKontakte();
                    //Ermitteln alle Verteilerlisten einer Adresse
                    MailingList = new clsMailingList();
                    MailingList.InitClass(this._GL_User, this._GL_System, this.ADR_ID);
                }
            }
        }
        ///<summary>clsKontakte / Update</summary>
        ///<remarks></remarks>
        public void FillbyAdrID()
        {
            string strSQL = string.Empty;
            strSQL = "SELECT TOP(1) * FROM Kontakte WHERE ADR_ID=" + ADR_ID;
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kontakte");
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.ViewID = dt.Rows[i]["ViewID"].ToString();
                    this.ADR_ID = (decimal)dt.Rows[i]["ADR_ID"];
                    this.Abteilung = dt.Rows[i]["Abteilung"].ToString();
                    this.Nachname = dt.Rows[i]["Nachname"].ToString();
                    this.Mail = dt.Rows[i]["Mail"].ToString();
                    this.Telefon = dt.Rows[i]["Telefon"].ToString();
                    this.Fax = dt.Rows[i]["Fax"].ToString();
                    this.Info = dt.Rows[i]["Info"].ToString();
                    this.Vorname = dt.Rows[i]["Vorname"].ToString();
                    this.Anrede = dt.Rows[i]["Anrede"].ToString();
                    this.Mobil = dt.Rows[i]["Mobil"].ToString();
                    DateTime tmpBirth = DateTime.MinValue;
                    DateTime.TryParse(dt.Rows[i]["Birthday"].ToString(), out tmpBirth);
                    this.Birthday = tmpBirth;

                    //Alle Kontakte zu einer Adresse
                    //dtKontakte = GetADRKontakte();
                }
            }
            MailingList = new clsMailingList();
            MailingList.InitClass(this._GL_User, this._GL_System, this.ADR_ID);
        }
        ///<summary>clsKontakte / GetADRKontakte</summary>
        ///<remarks>Ermittelt alle Kontakte einer Adresse</remarks>
        private DataTable GetADRKontakte()
        {
            string strSQL = string.Empty;
            DataTable dt = new DataTable();
            strSQL = "SELECT * FROM Kontakte WHERE ADR_ID=" + ADR_ID + " ORDER BY Abteilung";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "Kontakte");
            return dt;
        }
        ///<summary>clsKontakte / DeleteKontaktEintrag</summary>
        ///<remarks>Löscht den einzelnen Datensatz anhander der ID</remarks>
        public void DeleteKontaktEintrag()
        {
            string strSQL = string.Empty;
            strSQL = "DELETE FROM Kontakte WHERE ID=" + ID;
            bool bDeleted = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            if (bDeleted)
            {
                string beschreibung = "Kontakt: " + ViewID + " von Adress ID [" + ADR_ID + "] gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), beschreibung);
            }
        }
        ///<summary>clsKontakte / DeleteKontakte</summary>
        ///<remarks>Löscht alle Datensätze zu der Adresse. Funktion wird verwendet, wenn eine Adresse glöscht wird.
        ///         Hier müsste einmal das Löschen über den Fremdschlüssel eingefügt werden.</remarks>
        public void DeleteKontakte()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM Kontakte WHERE ADR_ID=" + ADR_ID + ";";
            bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bDeleteOK)
            {
                //Add Logbucheintrag Löschen
                string myViewID = ViewID;
                string beschreibung = "Kontakte: " + myViewID + " gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), beschreibung);
            }
        }
        ///<summary>clsKontakte / GetEMailListForMailingListAdministration</summary>
        ///<remarks>Erstellt eine Liste aller Mailkontakte ohne die, die bereits Member der Verteilerliste sind.</remarks>
        public void GetEMailListForMailingListAdministration()
        {
            dtMailKontakte = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT CAST(0 as bit) as 'Select'" +
                                        ", k.* " +
                                        ", a.ViewID as Firma " +
                                        "FROM Kontakte k " +
                                        "INNER JOIN ADR a ON a.ID = k.ADR_ID " +
                                        //"WHERE (k.Mail IS NOT NULL OR k.Mail<>'') ";  // CF
                                        "WHERE (k.Mail IS NOT NULL AND k.Mail LIKE '%_@_%_.__%' ) ";
            if (this.MailingList != null && this.MailingList.MailingListAssignment != null) // CF
            {
                this.MailingList.MailingListAssignment.FillList(this.MailingList.ID);
                //prüfen, ob bereits Verteiler vorhanden sind, wenn ja kann die Table Mailkontakte gefüllt werden
                if (this.MailingList.MailingListAssignment.ListMailingListKontaktID.Count > 0)
                {
                    strSQL = strSQL + " AND  k.ID NOT IN (" + string.Join(",", this.MailingList.MailingListAssignment.ListMailingListKontaktID.ToArray()) + ");";
                    // dtMailKontakte = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "MailKontakte");
                }
                dtMailKontakte = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "MailKontakte");
            }
        }

        /**************************************************************************************************************
         *                                      public Static procedures
         * ***********************************************************************************************************/
        ///<summary>clsKontakte / GetAllMailContacts</summary>
        ///<remarks>Ermittelt alle Kontakte mit Mailadresse.</remarks>
        public static DataTable GetAllMailContacts(Globals._GL_USER myGLUser)
        {
            DataTable dt = new DataTable("ContactMails");
            string strSQL = string.Empty;
            strSQL = "SELECT CAST(0 as bit) as 'Select'" +
                             ", a.ViewID as Firma" +
                             ", k.* " +
                            " FROM Kontakte k " +
                            "INNER JOIN ADR a ON a.ID = k.ADR_ID " +
                            "WHERE (k.Mail IS NOT NULL OR k.Mail<>'') ";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "ContactMails");
            return dt;
        }




    }
}
