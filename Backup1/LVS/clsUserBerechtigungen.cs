using System;
using System.Data;
using System.Data.SqlClient;

namespace LVS
{
    public class clsUserBerechtigungen
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

        public DataTable dtBerechtigungen = new DataTable("Berechtigungslist");
        private decimal _ID;
        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        public void DeleteUserAndBerechtigung()
        {
            //Check vorhanden
            if (clsUser.CheckUserIDIsUsed(ID))
            {
                //Check ist eingeloggt
                if (!clsLogin.CheckUserIsLoggedIn(ID))
                {
                    //Add Logbucheintrag Löschen
                    string Name = clsUser.GetBenutzerNameByID(ID);
                    string Beschreibung = "User:  ID: " + ID + " Name: " + Name + " gelöscht";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);

                    //--- initialisierung des sqlcommand---
                    SqlCommand Command = new SqlCommand();
                    Command.Connection = Globals.SQLcon.Connection;
                    Command.CommandText = "DELETE FROM Userberechtigungen WHERE BenutzerID='" + ID + "'";
                    Globals.SQLcon.Open();
                    Command.ExecuteNonQuery();
                    Command.Dispose();
                    Globals.SQLcon.Close();
                    if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                    {
                        Command.Connection.Close();
                    }

                    //Löschen aus DB USER
                    clsUser delUser = new clsUser();
                    delUser.BenutzerID = BenutzerID;
                    delUser.ID = ID;
                    delUser.DeleteUser();

                }
                else
                {
                    clsMessages.Userverwaltung_UserIsLoggedIn();
                }
            }
        }


        /*********************************************/

        private decimal _UserID;
        public decimal UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public string[,] UserAuthArray = {
                            {"Stammdaten - Adressendaten lesen", "read_ADR"},
                            {"Stammdaten - Adressendaten anlegen/ändern", "write_ADR"},

                            {"Stammdaten - Kundendaten lesen", "read_Kunde" },
                            {"Stammdaten - Kundendaten anlegen ändern", "write_Kunde" },

                            {"Stammdaten - Personaldaten lesen", "read_Personal" },
                            {"Stammdaten - Personaldaten anlegen/ändernn", "write_Personal" },

                            {"Stammdaten - Fahrzeugdaten lesen", "read_KFZ" },
                            {"Stammdaten - Fahrzeugdaten anlegen/ändern", "write_KFZ" },

                            {"Stammdaten - Güterartdaten lesen", "read_Gut" },
                            {"Stammdaten - Güterartdaten anlegen/ändern", "write_Gut" },

                            {"Stammdaten - Relationdaten lesen", "read_Relation" },
                            {"Stammdaten - Relationdaten anlegen/ändern", "write_Relation" },

                            {"Stammdaten - Einheiten lesen", "read_Einheit" },
                            {"Stammdaten - Einheiten anlegen/ändern", "write_Einheit" },

                            {"Stammdaten - Schäden lesen", "read_Schaden" },
                            {"Stammdaten - Schäden anlegen/ändern", "write_Schaden" },

                            {"Stammdaten - Lagerort lesen", "read_LagerOrt" },
                            {"Stammdaten - Lagerort anlegen/ändern", "write_LagerOrt" },

                            {"Stammdaten - Sonderkosten lesen", "read_FaktExtraCharge" },
                            {"Stammdaten - Sonderkosten anlegen/ändern", "write_FaktExtraCharge" },

                            {"Auftragserfassung - Auftragsdaten lesen", "read_Order" },
                            {"Auftragserfassung - Auftragdaten anlegen/ändern", "write_Order" },

                            {"Disposition - Frachtvergabe an SU", "write_TransportOrder" },
                            {"Disposition - Frachtvergabe lesen", "read_TransportOrder" },

                            {"Disposition - disponieren", "write_Disposition" },
                            {"Disposition - lesen", "read_Disposition" },

                            {"Fakturierung Lager - lesen", "read_FaktLager" },
                            {"Fakturierung Lager - erstellen/ändern", "write_FaktLager" },

                            {"Fakturierung Spedition - lesen", "read_FaktSpedition" },
                            {"Fakturierung Spedition - erstellen/ändern", "write_FaktSpedition" },

                            {"Lager - Bestand lesen", "read_Bestand" },

                            {"Lager - Einlagerung lesen", "read_LagerEingang" },
                            {"Lager - Einlagerung erstellen/ändern", "write_LagerEingang" },

                            {"Lager - Auslagerung lesen", "read_LagerAusgang" },
                            {"Lager - Auslagerung erstellen/ändern", "write_LagerAusgang" },

                            {"Lager - Storno-/Korrekturverfahren freigeben", "access_StKV" },

                            {"Lager - ASN/DFÜ - Meldungen lesen", "read_ASNTransfer" },
                            {"Lager - ASN/DFÜ - Meldungen anlegen/ändern", "write_ASNTransfer" },

                            {"System - Userdaten lesen", "read_User" },
                            {"System - Userdaten anlegen/ändern", "write_User" },

                            {"System - Arbeitsbereichsdaten lesen", "read_Arbeitsbereich" },
                            {"System - Arbeitsbereichsdaten anlegen/ändern", "write_Arbeitsbereich" },

                            {"System - Mandantendaten lesen", "read_Mandant" },
                            {"System - Mandantendaten anlegen/ändern", "write_Mandant" },

                            {"Statistik - Statistikdaten lesen", "read_Statistik" },

                            {"App - Zugriff ", "access_App" },
                            {"App - Zugriff Einlagerung ", "access_AppStoreIn" },
                            {"App - Zugriff Auslagerung ", "access_AppStoreOut" },
                            {"App - Zugriff Inventur ", "access_AppInventory" },
                         };


        public bool read_ADR { get; set; }
        public bool write_ADR { get; set; }
        public bool read_Kunde { get; set; }
        public bool write_Kunde { get; set; }
        public bool read_Personal { get; set; }
        public bool write_Personal { get; set; }
        public bool read_KFZ { get; set; }
        public bool write_KFZ { get; set; }
        public bool read_Gut { get; set; }
        public bool write_Gut { get; set; }
        public bool read_Relation { get; set; }
        public bool write_Relation { get; set; }
        public bool read_Order { get; set; }
        public bool write_Order { get; set; }
        public bool read_TransportOrder { get; set; }
        public bool write_TransportOrder { get; set; }
        public bool read_Disposition { get; set; }
        public bool write_Disposition { get; set; }
        public bool read_FaktLager { get; set; }
        public bool write_FaktLager { get; set; }
        public bool read_FaktSpedition { get; set; }
        public bool write_FaktSpedition { get; set; }
        public bool read_Bestand { get; set; }
        public bool read_LagerEingang { get; set; }
        public bool write_LagerEingang { get; set; }
        public bool read_LagerAusgang { get; set; }
        public bool write_LagerAusgang { get; set; }
        public bool read_User { get; set; }
        public bool write_User { get; set; }
        public bool read_Arbeitsbereich { get; set; }
        public bool write_Arbeitsbereich { get; set; }
        public bool read_Mandant { get; set; }
        public bool write_Mandant { get; set; }
        public bool read_Statistik { get; set; }
        public bool read_Einheit { get; set; }
        public bool write_Einheit { get; set; }
        public bool read_Schaden { get; set; }
        public bool write_Schaden { get; set; }
        public bool read_LagerOrt { get; set; }
        public bool write_LagerOrt { get; set; }
        public bool read_ASNTransfer { get; set; }
        public bool write_ASNTransfer { get; set; }
        public bool read_FaktExtraCharge { get; set; }
        public bool write_FaktExtraCharge { get; set; }
        public bool access_StKV { get; set; }

        public bool access_App { get; set; }
        public bool access_AppStoreIn { get; set; }
        public bool access_AppStoreOut { get; set; }
        public bool access_AppInventory { get; set; }

        public bool read_Inventory { get; set; }
        public bool write_Inventory { get; set; }



        /*************************************************************************************
         *                            Methoden
         * **********************************************************************************/

        ///<summary>clsUserBerechtigungen / GetUserBerechtigungLogin</summary>
        ///<remarks></remarks>
        public void GetUserBerechtigungLogin(ref Globals._GL_USER GL_User)
        {
            this._GL_User = GL_User;
            this.UserID = GL_User.User_ID;
            this.FillByUserID();
            GL_User = this._GL_User;
        }
        ///<summary>clsUserBerechtigungen / SetUserAuthToTable</summary>
        ///<remarks>Diese Funktion wird für das Füllen und auslesen der Eigenschaften verewendet. 
        ///         - bSetVal (true = Klasse füllen / false = auslesen</remarks>
        private bool GetUserAuthByDBCol(string myDBCol, bool bSetVal)
        {
            bool reVal = false;
            bool bAuth = false;
            Int32 iRowCount = 1;

            if (bSetVal)
            {
                iRowCount = dtBerechtigungen.Rows.Count - 1;
            }

            for (Int32 i = 0; i <= iRowCount; i++)
            {
                string strCol = string.Empty;
                if (bSetVal)
                {
                    strCol = dtBerechtigungen.Rows[i]["dbCol"].ToString();
                    bAuth = (bool)dtBerechtigungen.Rows[i]["Freigabe"];
                }
                else
                {
                    strCol = myDBCol;
                }

                switch (strCol)
                {
                    case "read_ADR":
                        if (!bSetVal) { reVal = this.read_ADR; }
                        else { this.read_ADR = bAuth; }
                        break;

                    case "write_ADR":
                        if (!bSetVal) { reVal = this.write_ADR; }
                        else { this.write_ADR = bAuth; }
                        break;

                    case "read_Kunde":
                        if (!bSetVal) { reVal = this.read_Kunde; }
                        else { this.read_Kunde = bAuth; }
                        break;

                    case "write_Kunde":
                        if (!bSetVal) { reVal = this.write_Kunde; }
                        else { this.write_Kunde = bAuth; }
                        break;

                    case "read_Personal":
                        if (!bSetVal) { reVal = this.read_Personal; }
                        else { this.read_Personal = bAuth; }
                        break;

                    case "write_Personal":
                        if (!bSetVal) { reVal = this.write_Personal; }
                        else { this.write_Personal = bAuth; }
                        break;

                    case "read_KFZ":
                        if (!bSetVal) { reVal = this.read_KFZ; }
                        else { this.read_KFZ = bAuth; }
                        break;
                    case "write_KFZ":
                        if (!bSetVal) { reVal = this.write_KFZ; }
                        else { this.write_KFZ = bAuth; }
                        break;
                    case "read_Gut":
                        if (!bSetVal) { reVal = this.read_Gut; }
                        else { this.read_Gut = bAuth; }
                        break;
                    case "write_Gut":
                        if (!bSetVal) { reVal = this.write_Gut; }
                        else { this.write_Gut = bAuth; }
                        break;
                    case "read_Relation":
                        if (!bSetVal) { reVal = this.read_Relation; }
                        else { this.read_Relation = bAuth; }
                        break;
                    case "write_Relation":
                        if (!bSetVal) { reVal = this.write_Relation; }
                        else { this.write_Relation = bAuth; }
                        break;
                    case "read_Order":
                        if (!bSetVal) { reVal = this.read_Order; }
                        else { this.read_Order = bAuth; }
                        break;
                    case "write_Order":
                        if (!bSetVal) { reVal = this.write_Order; }
                        else { this.write_Order = bAuth; }
                        break;
                    case "write_TransportOrder":
                        if (!bSetVal) { reVal = this.write_TransportOrder; }
                        else { this.write_TransportOrder = bAuth; }
                        break;
                    case "read_TransportOrder":
                        if (!bSetVal) { reVal = this.read_TransportOrder; }
                        else { this.read_TransportOrder = bAuth; }
                        break;
                    case "write_Disposition":
                        if (!bSetVal) { reVal = this.write_Disposition; }
                        else { this.write_Disposition = bAuth; }
                        break;
                    case "read_Disposition":
                        if (!bSetVal) { reVal = this.read_Disposition; }
                        else { this.read_Disposition = bAuth; }
                        break;
                    case "read_FaktLager":
                        if (!bSetVal) { reVal = this.read_FaktLager; }
                        else { this.read_FaktLager = bAuth; }
                        break;
                    case "write_FaktLager":
                        if (!bSetVal) { reVal = this.write_FaktLager; }
                        else { this.write_FaktLager = bAuth; }
                        break;
                    case "read_FaktSpedition":
                        if (!bSetVal) { reVal = this.read_FaktSpedition; }
                        else { this.read_FaktSpedition = bAuth; }
                        break;
                    case "write_FaktSpedition":
                        if (!bSetVal) { reVal = this.write_FaktSpedition; }
                        else { this.write_FaktSpedition = bAuth; }
                        break;
                    case "read_Bestand":
                        if (!bSetVal) { reVal = this.read_Bestand; }
                        else { this.read_Bestand = bAuth; }
                        break;
                    case "read_LagerEingang":
                        if (!bSetVal) { reVal = this.read_LagerEingang; }
                        else { this.read_LagerEingang = bAuth; }
                        break;
                    case "write_LagerEingang":
                        if (!bSetVal) { reVal = this.write_LagerEingang; }
                        else { this.write_LagerEingang = bAuth; }
                        break;
                    case "read_LagerAusgang":
                        if (!bSetVal) { reVal = this.read_LagerAusgang; }
                        else { this.read_LagerAusgang = bAuth; }
                        break;
                    case "write_LagerAusgang":
                        if (!bSetVal) { reVal = this.write_LagerAusgang; }
                        else { this.write_LagerAusgang = bAuth; }
                        break;
                    case "read_User":
                        if (!bSetVal) { reVal = this.read_User; }
                        else { this.read_User = bAuth; }
                        break;
                    case "write_User":
                        if (!bSetVal) { reVal = this.write_User; }
                        else { this.write_User = bAuth; }
                        break;
                    case "read_Arbeitsbereich":
                        if (!bSetVal) { reVal = this.read_Arbeitsbereich; }
                        else { this.read_Arbeitsbereich = bAuth; }
                        break;
                    case "write_Arbeitsbereich":
                        if (!bSetVal) { reVal = this.write_Arbeitsbereich; }
                        else { this.write_Arbeitsbereich = bAuth; }
                        break;
                    case "read_Mandant":
                        if (!bSetVal) { reVal = this.read_Mandant; }
                        else { this.read_Mandant = bAuth; }
                        break;
                    case "write_Mandant":
                        if (!bSetVal) { reVal = this.write_Mandant; }
                        else { this.write_Mandant = bAuth; }
                        break;
                    case "read_Statistik":
                        if (!bSetVal) { reVal = this.read_Statistik; }
                        else { this.read_Statistik = bAuth; }
                        break;
                    case "read_Einheit":
                        if (!bSetVal) { reVal = this.read_Einheit; }
                        else { this.read_Einheit = bAuth; }
                        break;
                    case "write_Einheit":
                        if (!bSetVal) { reVal = this.write_Einheit; }
                        else { this.write_Einheit = bAuth; }
                        break;
                    case "read_Schaden":
                        if (!bSetVal) { reVal = this.read_Schaden; }
                        else { this.read_Schaden = bAuth; }
                        break;
                    case "write_Schaden":
                        if (!bSetVal) { reVal = this.write_Schaden; }
                        else { this.write_Schaden = bAuth; }
                        break;
                    case "read_LagerOrt":
                        if (!bSetVal) { reVal = this.read_LagerOrt; }
                        else { this.read_LagerOrt = bAuth; }
                        break;
                    case "write_LagerOrt":
                        if (!bSetVal) { reVal = this.write_LagerOrt; }
                        else { this.write_LagerOrt = bAuth; }
                        break;
                    case "read_ASNTransfer":
                        if (!bSetVal) { reVal = this.read_ASNTransfer; }
                        else { this.read_ASNTransfer = bAuth; }
                        break;
                    case "write_ASNTransfer":
                        if (!bSetVal) { reVal = this.write_ASNTransfer; }
                        else { this.write_ASNTransfer = bAuth; }
                        break;
                    case "read_FaktExtraCharge":
                        if (!bSetVal) { reVal = this.read_FaktExtraCharge; }
                        else { this.read_FaktExtraCharge = bAuth; }
                        break;
                    case "write_FaktExtraCharge":
                        if (!bSetVal) { reVal = this.write_FaktExtraCharge; }
                        else { this.write_FaktExtraCharge = bAuth; }
                        break;
                    case "access_StKV":
                        if (!bSetVal) { reVal = this.access_StKV; }
                        else { this.access_StKV = bAuth; }
                        break;

                    case "access_App":
                        if (!bSetVal) { reVal = this.access_App; }
                        else { this.access_StKV = bAuth; }
                        break;
                    case "access_AppStoreIn":
                        if (!bSetVal) { reVal = this.access_AppStoreIn; }
                        else { this.access_StKV = bAuth; }
                        break;
                    case "access_AppStoreOut":
                        if (!bSetVal) { reVal = this.access_AppStoreOut; }
                        else { this.access_StKV = bAuth; }
                        break;
                    case "access_AppInventory":
                        if (!bSetVal) { reVal = this.access_AppInventory; }
                        else { this.access_StKV = bAuth; }
                        break;

                    case "read_Inventory":
                        if (!bSetVal) { reVal = this.read_Inventory; }
                        else { this.read_Inventory = bAuth; }
                        break;

                    case "write_Inventory":
                        if (!bSetVal) { reVal = this.write_Inventory; }
                        else { this.write_Inventory = bAuth; }
                        break;

                    default:
                        reVal = false;
                        break;
                }
                if (strCol == myDBCol)
                {
                    break;
                }
            }
            return reVal;
        }
        ///<summary>clsUserBerechtigungen / SetValueToUserAuth</summary>
        ///<remarks></remarks>
        public void SetValueToUserAuth(bool bInsertData)
        {
            GetUserAuthByDBCol(string.Empty, true);
            if (bInsertData)
            {
                if (this.UserID > 0)
                {
                    Add();
                }
            }

        }
        ///<summary>clsUserBerechtigungen / Add</summary>
        ///<remarks>Datensatz hinzufügen</remarks>
        private void Add()
        {
            string strSQL = string.Empty;
            //----- SQL Abfrage -----------------------
            strSQL = "INSERT INTO Userberechtigungen ( UserID " +
                                                        ",read_ADR" +
                                                        ",write_ADR" +
                                                        ",read_Kunde" +
                                                        ",write_Kunde" +        //5

                                                        ",read_Personal" +
                                                        ",write_Personal" +
                                                        ",read_KFZ" +
                                                        ",write_KFZ" +
                                                        ",read_Gut" +           //10

                                                        ",write_Gut" +
                                                        ",read_Relation" +
                                                        ",write_Relation" +
                                                        ",read_Order" +
                                                        ",write_Order" +        //15

                                                        ",write_TransportOrder" +
                                                        ",read_TransportOrder" +
                                                        ",write_Disposition" +
                                                        ",read_Disposition" +
                                                        ",read_FaktLager" +     //20

                                                        ",write_FaktLager" +
                                                        ",read_FaktSpedition" +
                                                        ",write_FaktSpedition" +
                                                        ",read_Bestand" +
                                                        ",read_LagerEingang" +  //25

                                                        ",write_LagerEingang" +
                                                        ",read_LagerAusgang" +
                                                        ",write_LagerAusgang" +
                                                        ",read_User" +
                                                        ",write_User" +         //30    


                                                        ",read_Arbeitsbereich" +
                                                        ",write_Arbeitsbereich" +
                                                        ",read_Mandant" +
                                                        ",write_Mandant" +
                                                        ",read_Statistik" +      //35

                                                        ",read_Einheit" +
                                                        ",write_Einheit" +
                                                        ",read_Schaden" +
                                                        ",write_Schaden" +
                                                        ",read_LagerOrt" +      //40

                                                        ", write_LagerOrt" +
                                                        ", read_ASNTransfer" +
                                                        ", write_ASNTransfer" +
                                                        ", read_FaktExtraCharge" +
                                                        ", write_FaktExtraCharge" + //45

                                                        ", access_StKV" +
                                                        ", access_App" +
                                                        ", access_AppStoreIn" +
                                                        ", access_AppStoreOut" +
                                                        ", access_AppInventory" +
                                                        ", read_Inventory" +
                                                        ", write_Inventory" +

                                                        ") " +

                                            "VALUES (" + UserID +
                                                    "," + Convert.ToInt32(read_ADR) +
                                                    "," + Convert.ToInt32(write_ADR) +
                                                    "," + Convert.ToInt32(read_Kunde) +
                                                    "," + Convert.ToInt32(write_Kunde) +    //5

                                                    "," + Convert.ToInt32(read_Personal) +
                                                    "," + Convert.ToInt32(write_Personal) +
                                                    "," + Convert.ToInt32(read_KFZ) +
                                                    "," + Convert.ToInt32(write_KFZ) +
                                                    "," + Convert.ToInt32(read_Gut) +       //10

                                                    "," + Convert.ToInt32(write_Gut) +
                                                    "," + Convert.ToInt32(read_Relation) +
                                                    "," + Convert.ToInt32(write_Relation) +
                                                    "," + Convert.ToInt32(read_Order) +
                                                    "," + Convert.ToInt32(write_Order) +    //15

                                                    "," + Convert.ToInt32(write_TransportOrder) +
                                                    "," + Convert.ToInt32(read_TransportOrder) +
                                                    "," + Convert.ToInt32(write_Disposition) +
                                                    "," + Convert.ToInt32(read_Disposition) +
                                                    "," + Convert.ToInt32(read_FaktLager) +     //20

                                                    "," + Convert.ToInt32(write_FaktLager) +
                                                    "," + Convert.ToInt32(read_FaktSpedition) +
                                                    "," + Convert.ToInt32(write_FaktSpedition) +
                                                    "," + Convert.ToInt32(read_Bestand) +
                                                    "," + Convert.ToInt32(read_LagerEingang) +  //25

                                                    "," + Convert.ToInt32(write_LagerEingang) +
                                                    "," + Convert.ToInt32(read_LagerAusgang) +
                                                    "," + Convert.ToInt32(write_LagerAusgang) +
                                                    "," + Convert.ToInt32(read_User) +
                                                    "," + Convert.ToInt32(write_User) +         //30

                                                    "," + Convert.ToInt32(read_Arbeitsbereich) +
                                                    "," + Convert.ToInt32(write_Arbeitsbereich) +
                                                    "," + Convert.ToInt32(read_Mandant) +
                                                    "," + Convert.ToInt32(write_Mandant) +
                                                    "," + Convert.ToInt32(read_Statistik) +     //35

                                                    "," + Convert.ToInt32(read_Einheit) +
                                                    "," + Convert.ToInt32(write_Einheit) +
                                                    "," + Convert.ToInt32(read_Schaden) +
                                                    "," + Convert.ToInt32(write_Schaden) +
                                                    "," + Convert.ToInt32(read_LagerOrt) +      //40

                                                    "," + Convert.ToInt32(write_LagerOrt) +
                                                    "," + Convert.ToInt32(read_ASNTransfer) +
                                                    "," + Convert.ToInt32(write_ASNTransfer) +
                                                     "," + Convert.ToInt32(read_FaktExtraCharge) +
                                                    "," + Convert.ToInt32(write_FaktExtraCharge) +  //45

                                                    "," + Convert.ToInt32(access_StKV) +
                                                    "," + Convert.ToInt32(access_App) +
                                                    "," + Convert.ToInt32(access_AppStoreIn) +
                                                    "," + Convert.ToInt32(access_AppStoreOut) +
                                                    "," + Convert.ToInt32(access_AppInventory) +    // 50

                                                    "," + Convert.ToInt32(read_Inventory) +
                                                    "," + Convert.ToInt32(write_Inventory) +

                                                    "); ";

            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                //Logbuch eintrag User           
                this.FillByID();
            }
        }
        ///<summary>clsUserBerechtigungen / Add</summary>
        ///<remarks>Datensatz hinzufügen</remarks>
        public void Update()
        {
            GetUserAuthByDBCol(string.Empty, true);
            string strSQL = "Update Userberechtigungen SET " +
                                                 "read_ADR=" + Convert.ToInt32(read_ADR) +
                                                 ", write_ADR=" + Convert.ToInt32(write_ADR) +
                                                 ", read_Kunde=" + Convert.ToInt32(read_Kunde) +
                                                 ", write_Kunde=" + Convert.ToInt32(write_Kunde) +
                                                 ", read_Personal=" + Convert.ToInt32(read_Personal) +
                                                 ", write_Personal=" + Convert.ToInt32(write_Personal) +
                                                 ", read_KFZ=" + Convert.ToInt32(read_KFZ) +
                                                 ", write_KFZ=" + Convert.ToInt32(write_KFZ) +
                                                 ", read_Gut=" + Convert.ToInt32(read_Gut) +
                                                 ", write_Gut=" + Convert.ToInt32(write_Gut) +
                                                 ", read_Relation=" + Convert.ToInt32(read_Relation) +
                                                 ", write_Relation=" + Convert.ToInt32(write_Relation) +
                                                 ", read_Order=" + Convert.ToInt32(read_Order) +
                                                 ", write_Order=" + Convert.ToInt32(write_Order) +
                                                 ", write_TransportOrder=" + Convert.ToInt32(write_TransportOrder) +
                                                 ", read_TransportOrder=" + Convert.ToInt32(read_TransportOrder) +
                                                 ", write_Disposition=" + Convert.ToInt32(write_Disposition) +
                                                 ", read_Disposition=" + Convert.ToInt32(read_Disposition) +
                                                 ", read_FaktLager=" + Convert.ToInt32(read_FaktLager) +
                                                 ", write_FaktLager=" + Convert.ToInt32(write_FaktLager) +
                                                 ", read_FaktSpedition=" + Convert.ToInt32(read_FaktSpedition) +
                                                 ", write_FaktSpedition=" + Convert.ToInt32(write_FaktSpedition) +
                                                 ", read_Bestand=" + Convert.ToInt32(read_Bestand) +
                                                 ", read_LagerEingang=" + Convert.ToInt32(read_LagerEingang) +
                                                 ", write_LagerEingang=" + Convert.ToInt32(write_LagerEingang) +
                                                 ", read_LagerAusgang=" + Convert.ToInt32(read_LagerAusgang) +
                                                 ", write_LagerAusgang=" + Convert.ToInt32(write_LagerAusgang) +
                                                 ", read_User=" + Convert.ToInt32(read_User) +
                                                 ", write_User=" + Convert.ToInt32(write_User) +
                                                 ", read_Arbeitsbereich=" + Convert.ToInt32(read_Arbeitsbereich) +
                                                 ", write_Arbeitsbereich=" + Convert.ToInt32(write_Arbeitsbereich) +
                                                 ", read_Mandant=" + Convert.ToInt32(read_Mandant) +
                                                 ", write_Mandant=" + Convert.ToInt32(write_Mandant) +
                                                 ", read_Statistik=" + Convert.ToInt32(read_Statistik) +
                                                 ", read_Einheit=" + Convert.ToInt32(read_Einheit) +
                                                 ", write_Einheit=" + Convert.ToInt32(write_Einheit) +
                                                 ", read_Schaden=" + Convert.ToInt32(read_Schaden) +
                                                 ", write_Schaden=" + Convert.ToInt32(write_Schaden) +
                                                 ", read_LagerOrt=" + Convert.ToInt32(read_LagerOrt) +
                                                 ", write_LagerOrt=" + Convert.ToInt32(write_LagerOrt) +
                                                 ", read_ASNTransfer=" + Convert.ToInt32(read_ASNTransfer) +
                                                 ", write_ASNTransfer=" + Convert.ToInt32(write_ASNTransfer) +
                                                 ", read_FaktExtraCharge=" + Convert.ToInt32(read_FaktExtraCharge) +
                                                 ", write_FaktExtraCharge=" + Convert.ToInt32(write_FaktExtraCharge) +
                                                 ", access_StKV=" + Convert.ToInt32(access_StKV) +
                                                 ", access_App =" + Convert.ToInt32(access_App) +
                                                 ", access_AppStoreIn =" + Convert.ToInt32(access_AppStoreIn) +
                                                 ", access_AppStoreOut =" + Convert.ToInt32(access_AppStoreOut) +
                                                 ", access_AppInventory =" + Convert.ToInt32(access_AppInventory) +
                                                 ", read_Inventory = " + Convert.ToInt32(read_Inventory) +
                                                 ", write_Inventory = " + Convert.ToInt32(write_Inventory) +


                                                 " WHERE UserID=" + UserID + " ;";

            bool bOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {
                //Baustelle LOG eintrag???
            }
        }
        ///<summary>clsUserBerechtigungen / FillByUserID</summary>
        ///<remarks>Klasse mit Daten füllen</remarks>
        public void FillByUserID()
        {
            if (UserID > 0)
            {
                string strSQL = "SELECT * FROM Userberechtigungen WHERE UserID=" + UserID;
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserAuth");
                Fill(dt);
            }
        }
        ///<summary>clsUserBerechtigungen / FillByID</summary>
        ///<remarks>Klasse mit Daten füllen</remarks>
        public void FillByID()
        {
            if (ID > 0)
            {
                string strSQL = "SELECT * FROM Userberechtigungen WHERE ID=" + ID;
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserAuth");
                Fill(dt);
            }
        }
        ///<summary>clsUserBerechtigungen / CreateAdminAuth</summary>
        ///<remarks></remarks>
        public void CreateAdminAuth()
        {

            string strSQL = "SELECT * FROM Userberechtigungen WHERE ID=" + this.ID + " ;";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "UserAuth");
            //Berechtiugen füllen alles auf true
            for (Int32 i = 0; i <= dt.Columns.Count - 1; i++)
            {
                switch (i)
                {
                    case 0:
                        //dt.Rows[0][i]=0; 
                        break;

                    case 1:
                        //dt.Rows[0][i]= myUserID;
                        break;

                    default:
                        dt.Rows[0][i] = true;
                        break;
                }
            }
            //Berechtigungen aus dem Table auslesen und die Klasse füllen
            Fill(dt);
            //Insert der Daten
            Update();
        }

        ///<summary>clsUserBerechtigungen / Fill</summary>
        ///<remarks>Klasse mit Daten füllen</remarks>
        private void Fill(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    this.ID = (decimal)dt.Rows[i]["ID"];
                    this.UserID = (decimal)dt.Rows[i]["UserID"];
                    this.read_ADR = (bool)dt.Rows[i]["read_ADR"];
                    this.write_ADR = (bool)dt.Rows[i]["write_ADR"];
                    this.read_Kunde = (bool)dt.Rows[i]["read_Kunde"];
                    this.write_Kunde = (bool)dt.Rows[i]["write_Kunde"];
                    this.read_Personal = (bool)dt.Rows[i]["read_Personal"];
                    this.write_Personal = (bool)dt.Rows[i]["write_Personal"];
                    this.read_KFZ = (bool)dt.Rows[i]["read_KFZ"];
                    this.write_KFZ = (bool)dt.Rows[i]["write_KFZ"];
                    this.read_Gut = (bool)dt.Rows[i]["read_Gut"];
                    this.write_Gut = (bool)dt.Rows[i]["write_Gut"];
                    this.read_Relation = (bool)dt.Rows[i]["read_Relation"];
                    this.write_Relation = (bool)dt.Rows[i]["write_Relation"];
                    this.read_Order = (bool)dt.Rows[i]["read_Order"];
                    this.write_Order = (bool)dt.Rows[i]["write_Order"];
                    this.write_TransportOrder = (bool)dt.Rows[i]["write_TransportOrder"];
                    this.read_TransportOrder = (bool)dt.Rows[i]["read_TransportOrder"];
                    this.write_Disposition = (bool)dt.Rows[i]["write_Disposition"];
                    this.read_Disposition = (bool)dt.Rows[i]["read_Disposition"];
                    this.read_FaktLager = (bool)dt.Rows[i]["read_FaktLager"];
                    this.write_FaktLager = (bool)dt.Rows[i]["write_FaktLager"];
                    this.read_FaktSpedition = (bool)dt.Rows[i]["read_FaktSpedition"];
                    this.write_FaktSpedition = (bool)dt.Rows[i]["write_FaktSpedition"];
                    this.read_Bestand = (bool)dt.Rows[i]["read_Bestand"];
                    this.read_LagerEingang = (bool)dt.Rows[i]["read_LagerEingang"];
                    this.write_LagerEingang = (bool)dt.Rows[i]["write_LagerEingang"];
                    this.read_LagerAusgang = (bool)dt.Rows[i]["read_LagerAusgang"];
                    this.write_LagerAusgang = (bool)dt.Rows[i]["write_LagerAusgang"];
                    this.read_User = (bool)dt.Rows[i]["read_User"];
                    this.write_User = (bool)dt.Rows[i]["write_User"];
                    this.read_Arbeitsbereich = (bool)dt.Rows[i]["read_Arbeitsbereich"];
                    this.write_Arbeitsbereich = (bool)dt.Rows[i]["write_Arbeitsbereich"];
                    this.read_Mandant = (bool)dt.Rows[i]["read_Mandant"];
                    this.write_Mandant = (bool)dt.Rows[i]["write_Mandant"];
                    this.read_Statistik = (bool)dt.Rows[i]["read_Statistik"];
                    this.read_Einheit = (bool)dt.Rows[i]["read_Einheit"];
                    this.write_Einheit = (bool)dt.Rows[i]["write_Einheit"];
                    this.read_Schaden = (bool)dt.Rows[i]["read_Schaden"];
                    this.write_Schaden = (bool)dt.Rows[i]["write_Schaden"];
                    this.read_LagerOrt = (bool)dt.Rows[i]["read_LagerOrt"];
                    this.write_LagerOrt = (bool)dt.Rows[i]["write_LagerOrt"];
                    this.read_ASNTransfer = (bool)dt.Rows[i]["read_ASNTransfer"];
                    this.write_ASNTransfer = (bool)dt.Rows[i]["write_ASNTransfer"];
                    this.read_FaktExtraCharge = (bool)dt.Rows[i]["read_FaktExtraCharge"];
                    this.write_FaktExtraCharge = (bool)dt.Rows[i]["write_FaktExtraCharge"];
                    this.access_StKV = (bool)dt.Rows[i]["access_StKV"];
                    this.access_App = (bool)dt.Rows[i]["access_App"];
                    this.access_AppStoreIn = (bool)dt.Rows[i]["access_AppStoreIn"];
                    this.access_AppStoreOut = (bool)dt.Rows[i]["access_AppStoreOut"];
                    this.access_AppInventory = (bool)dt.Rows[i]["access_AppInventory"];
                    this.read_Inventory = (bool)dt.Rows[i]["read_Inventory"];
                    this.write_Inventory = (bool)dt.Rows[i]["write_Inventory"];

                    //Berechtigungen werden noch einmal in einer Table zusammengefasst
                    // für die Userverwaltung
                    InitDataTableUserAuth();
                }
            }
        }
        ///<summary>clsUserBerechtigungen / tbWerkBezeichnung_Validated</summary>
        ///<remarks>Datenbank Spalte nvarchar(50), Pflichtfeld</remarks>
        public void InitDataTableUserAuth()
        {
            dtBerechtigungen.Clear();

            if (dtBerechtigungen.Columns["Berechtigung"] == null)
            {
                DataColumn column1 = new DataColumn();
                column1.DataType = System.Type.GetType("System.String");
                column1.AllowDBNull = false;
                column1.Caption = "Berechtigung";
                column1.ColumnName = "Berechtigung";
                column1.DefaultValue = string.Empty;

                dtBerechtigungen.Columns.Add(column1);
            }

            //DataColumn column2 = new DataColumn();
            if (dtBerechtigungen.Columns["Freigabe"] == null)
            {
                DataColumn column2 = new DataColumn();
                column2.DataType = System.Type.GetType("System.Boolean");
                column2.AllowDBNull = false;
                column2.Caption = "Freigabe";
                column2.ColumnName = "Freigabe";
                column2.DefaultValue = false;

                dtBerechtigungen.Columns.Add(column2);
            }
            if (dtBerechtigungen.Columns["dbCol"] == null)
            {
                DataColumn column3 = new DataColumn();
                column3.DataType = System.Type.GetType("System.String");
                column3.Caption = "dbCol";
                column3.ColumnName = "dbCol";

                dtBerechtigungen.Columns.Add(column3);
            }
            InitRows();
        }
        ///<summary>clsUserBerechtigungen / InitRows</summary>
        ///<remarks></remarks>
        private void InitRows()
        {
            Int32 Count = UserAuthArray.Length / 2;
            for (Int32 i = 0; i <= Count - 1; i++)
            {
                string strCol = UserAuthArray[i, 1].ToString();
                if (this.ID > 0)
                {
                    strCol = UserAuthArray[i, 1].ToString();
                }
                DataRow row = dtBerechtigungen.NewRow();
                row["Berechtigung"] = UserAuthArray[i, 0].ToString();
                if (this.ID > 0)
                {
                    row["Freigabe"] = GetUserAuthByDBCol(strCol, false);
                }
                else
                {
                    row["Freigabe"] = GetUserAuthByDBCol(string.Empty, false);
                }
                row["dbCol"] = strCol;
                dtBerechtigungen.Rows.Add(row);
            }
        }


    }
}

