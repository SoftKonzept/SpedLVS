using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LVS
{
    public class clsKunde
    {
        public clsTarif Tarif = new clsTarif();

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
        //************************************

        public string ViewID;
        public decimal ID { get; set; }
        public decimal KD_ID { get; set; }
        public decimal KD_IDman { get; set; }  //manuelle Kundennummer - manuelle Eingabe
        private decimal _NewKD_ID;
        public decimal NewKD_ID
        {
            get
            {
                bool NewKDisUsed = true;
                decimal decTmp = 0;
                while (NewKDisUsed)
                {
                    string strSQL = "SELECT MAX(KD_ID)+1 FROM Kunde";
                    string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
                    Decimal.TryParse(strTmp, out decTmp);
                    NewKDisUsed = clsKunde.CheckKundenNummerIsUsed(decTmp);
                }
                _NewKD_ID = decTmp;
                return _NewKD_ID;
            }
            set
            {

                _NewKD_ID = value;
            }
        }

        public decimal ADR_ID { get; set; }
        public string SteuerNr { get; set; } = string.Empty;
        public string USt_ID { get; set; } = string.Empty;
        public bool MwSt { get; set; }
        public decimal MwStSatz { get; set; }
        public string Bank1 { get; set; } = string.Empty;
        public Int32 Kto1 { get; set; }
        public Int32 BLZ1 { get; set; }
        public string Swift1 { get; set; } = string.Empty;
        public string IBAN1 { get; set; } = string.Empty;
        public string Bank2 { get; set; } = string.Empty;
        public Int32 Kto2 { get; set; }
        public Int32 BLZ2 { get; set; }
        public string Swift2 { get; set; } = string.Empty;
        public string IBAN2 { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Mailaddress { get; set; } = string.Empty;
        public string Organisation { get; set; } = string.Empty;

        private Int32 _Debitor;
        public Int32 Debitor
        {
            get { return _Debitor; }
            set { _Debitor = value; }

        }
        private Int32 _Kreditor;
        public Int32 Kreditor
        {
            get { return _Kreditor; }
            set { _Kreditor = value; }

        }
        private Dictionary<string, int> _dictDebitorDefaultNo;
        public Dictionary<string, int> dictDebitorDefaultNo
        {
            get
            {
                _dictDebitorDefaultNo = new Dictionary<string, int>();
                string strSQL = string.Empty;
                strSQL = "SELECT * FROM DebitorDefaultNo ;";

                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, BenutzerID, "DebitorDefaulNo");
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string strKey = dt.Rows[i]["Key"].ToString();
                    Int32 iTmp = 0;
                    Int32.TryParse(dt.Rows[i]["Value"].ToString(), out iTmp);
                    _dictDebitorDefaultNo.Add(strKey, iTmp);
                }
                return _dictDebitorDefaultNo;
            }
            set { _dictDebitorDefaultNo = value; }
        }
        public Int32 Zahlungziel { get; set; }
        public Int32 SalesTaxKeyDebitor { get; set; }
        public Int32 SalesTaxKeyKreditor { get; set; }

        //**************************************************************
        //-----------------         Methoden
        //**************************************************************
        ///<summary>clsADR / InitClass</summary>
        ///<remarks></remarks>>
        public void InitClass(Globals._GL_USER myGLUser, decimal myAdrID, decimal myTarifID)
        {
            this._GL_User = myGLUser;
            this.ADR_ID = myAdrID;
            FillbyAdrID();
            if (myTarifID > 0)
            {
                InitSubClasses(myTarifID);
            }
        }
        ///<summary>clsADR / InitSubClasses</summary>
        ///<remarks></remarks>>
        private void InitSubClasses(decimal myTarifID)
        {
            Tarif = new clsTarif();
            Tarif.InitClass(this._GL_User, myTarifID, this.ADR_ID);
        }
        ///<summary>clsADR / FillbyID</summary>
        ///<remarks>Füllt die Klasse</remarks>>
        public void FillbyID()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM Kunde WHERE ID=" + ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Kunde");
            SETKundenDatenToClass(ref dt);
        }
        ///<summary>clsADR / FillbyAdrID</summary>
        ///<remarks>Füllt die Klasse</remarks>>
        public void FillbyAdrID()
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT * FROM Kunde WHERE ADR_ID=" + ADR_ID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Kunde");
            SETKundenDatenToClass(ref dt);
        }
        ///<summary>clsADR / SETKundenDatenToClass</summary>
        ///<remarks>Füllt die Klasse</remarks>>
        private void SETKundenDatenToClass(ref DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.KD_ID = (decimal)dt.Rows[i]["KD_ID"];
                this.ADR_ID = (decimal)dt.Rows[i]["ADR_ID"];
                this.SteuerNr = dt.Rows[i]["SteuerNr"].ToString();
                this.USt_ID = dt.Rows[i]["USt_ID"].ToString();
                this.MwSt = (bool)dt.Rows[i]["MwSt"];
                this.MwStSatz = (decimal)dt.Rows[i]["MwStSatz"];
                this.Bank1 = dt.Rows[i]["Bank1"].ToString();
                Int32 iTmp = 0;
                Int32.TryParse(dt.Rows[i]["BLZ1"].ToString(), out iTmp);
                this.BLZ1 = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["Kto1"].ToString(), out iTmp);
                this.Kto1 = iTmp;
                this.Swift1 = dt.Rows[i]["Swift1"].ToString();
                this.IBAN1 = dt.Rows[i]["IBAN1"].ToString();
                this.Bank2 = dt.Rows[i]["Bank2"].ToString();
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["BLZ2"].ToString(), out iTmp);
                this.BLZ2 = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["Kto2"].ToString(), out iTmp);
                this.Kto2 = iTmp;
                this.Swift2 = dt.Rows[i]["Swift2"].ToString();
                this.IBAN2 = dt.Rows[i]["IBAN2"].ToString();
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["Kreditor"].ToString(), out iTmp);
                this.Kreditor = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["Debitor"].ToString(), out iTmp);
                this.Debitor = iTmp;
                iTmp = 0;
                Int32.TryParse(dt.Rows[i]["ZZ"].ToString(), out iTmp);
                this.Zahlungziel = iTmp;
                decimal decTmp = 0;
                decimal.TryParse(dt.Rows[i]["KD_IDman"].ToString(), out decTmp);
                this.KD_IDman = decTmp;
                this.SalesTaxKeyDebitor = (Int32)dt.Rows[i]["SalesTaxKeyDebitor"];
                this.SalesTaxKeyKreditor = (Int32)dt.Rows[i]["SalesTaxKeyKreditor"];

                if (dt.Columns.Contains("Contact"))
                {
                    this.Contact = dt.Rows[i]["Contact"].ToString();
                }
                if (dt.Columns.Contains("Phone"))
                {
                    this.Phone = dt.Rows[i]["Phone"].ToString();
                }
                if (dt.Columns.Contains("Mailaddress"))
                {
                    this.Mailaddress = dt.Rows[i]["Mailaddress"].ToString();
                }
                if (dt.Columns.Contains("Organisation"))
                {
                    this.Organisation = dt.Rows[i]["Organisation"].ToString();
                }

                Tarif = new clsTarif();
                Tarif.InitClass(this._GL_User, 0, this.ADR_ID);
            }
        }

        ///<summary>clsADR / SetDefValueToKDDaten</summary>
        ///<remarks></remarks>>
        public void SetDefValueToKDDaten()
        {
            //Standardwerte pro Kunde auslesen
            this.Debitor = 0;
            this.Kreditor = 0;
            this.SteuerNr = string.Empty;
            this.MwSt = true;
            this.MwStSatz = 19;
            this.USt_ID = string.Empty;
            this.Bank1 = string.Empty;
            this.BLZ1 = 0;
            this.Kto1 = 0;
            this.Swift1 = string.Empty;
            this.IBAN1 = string.Empty;
            this.Bank2 = string.Empty;
            this.BLZ2 = 0;
            this.Kto2 = 0;
            this.IBAN2 = string.Empty;
            this.Swift2 = string.Empty;
            this.SalesTaxKeyDebitor = 0;
            this.SalesTaxKeyKreditor = 0;
            this.Contact = string.Empty;
            this.Phone = string.Empty;
            this.Mailaddress = string.Empty;
            this.Organisation = string.Empty;
        }
        ///<summary>clsADR / Add</summary>
        ///<remarks></remarks>>
        public void Add()
        {
            string strSQL = string.Empty;
            strSQL = "INSERT INTO Kunde (KD_ID, ADR_ID, SteuerNr, USt_ID, MwSt, MwStSatz, Bank1, BLZ1, Kto1, " +
                                       "Swift1, IBAN1, Bank2, BLZ2, Kto2,Swift2, IBAN2, Kreditor, Debitor, ZZ, " +
                                       "SalesTaxKeyDebitor, SalesTaxKeyKreditor, Contact, Phone, Mailaddress, Organisation) " +
                                        "VALUES (" +//+ KD_ID +
                                                "(SELECT MAX(KD_ID)+1 FROM Kunde)" +
                                                ", " + ADR_ID +
                                                ", '" + SteuerNr + "'" +
                                                ", '" + USt_ID + "'" +
                                                ", " + Convert.ToInt32(MwSt) +
                                                ", '" + MwStSatz.ToString().Replace(",", ".") + "'" +
                                                ", '" + Bank1 + "'" +
                                                ", '" + BLZ1 + "'" +
                                                ", " + Kto1 +
                                                ", '" + Swift1 + "'" +
                                                ", '" + IBAN1 + "'" +
                                                ", '" + Bank2 + "'" +
                                                ", '" + BLZ2 + "'" +
                                                ", " + Kto2 +
                                                ", '" + Swift2 + "'" +
                                                ", '" + IBAN2 + "'" +
                                                ", " + Kreditor +
                                                ", " + Debitor +
                                                ", " + Zahlungziel +
                                                ", " + SalesTaxKeyDebitor +
                                                ", " + SalesTaxKeyKreditor +
                                                ", '" + Contact + "'" +
                                                ", '" + Phone + "'" +
                                                ", '" + Mailaddress + "'" +
                                                ", '" + Organisation + "'" +
                                                ")";
            strSQL = strSQL + "Select @@IDENTITY as 'ID' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                this.ID = decTmp;
                FillbyID();
                //MessageBox.Show("Der Adressdatensatz wurde erfolgreich in die Datenbank geschrieben!");
                clsADR.updateADRforKD(KD_ID, ADR_ID, BenutzerID);
                //Add Logbucheintrag Eintrag
                string ViewID = clsADR.GetMatchCodeByID(ADR_ID, BenutzerID);
                string Beschreibung = "Kunde: " + ViewID + " ID:" + ADR_ID + " Kundennummer:" + KD_ID + "  hinzugefügt";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), Beschreibung);
            }
        }
        ///<summary>clsADR / updateKD</summary>
        ///<remarks></remarks>>
        public void updateKD()
        {
            string strSQL = string.Empty;
            strSQL = "Update Kunde SET KD_ID=" + KD_ID +
                                    ", ADR_ID=" + ADR_ID +
                                    ", SteuerNr='" + SteuerNr + "'" +
                                    ", USt_ID='" + USt_ID + "'" +
                                    ", MwSt=" + Convert.ToInt32(MwSt) +
                                    ", MwStSatz='" + MwStSatz.ToString().Replace(",", ".") + "'" +
                                    ", Bank1='" + Bank1 + "'" +
                                    ", BLZ1='" + BLZ1 + "'" +
                                    ", Kto1=" + Kto1 +
                                    ", Swift1='" + Swift1 + "'" +
                                    ", IBAN1='" + IBAN1 + "'" +

                                    ", Bank2='" + Bank2 + "'" +
                                    ", BLZ2=" + BLZ2 +
                                    ", Kto2=" + Kto2 +
                                    ", Swift2='" + Swift2 + "'" +
                                    ", IBAN2='" + IBAN2 + "'" +
                                    ", Kreditor=" + Kreditor +
                                    ", Debitor=" + Debitor +
                                    ", ZZ=" + Zahlungziel +
                                    ", KD_IDman=" + (Int32)KD_IDman + " " +
                                    ", SalesTaxKeyDebitor=" + SalesTaxKeyDebitor +
                                    ", SalesTaxKeyKreditor=" + SalesTaxKeyKreditor +
                                    ", Contact='" + Contact + "'" +
                                    ", Phone='" + Phone + "'" +
                                    ", Mailaddress='" + Mailaddress + "'" +
                                    ", Organisation='" + Organisation + "'" +

                                    "WHERE ID=" + ID;

            bool bOK = clsSQLcon.ExecuteSQL(strSQL, BenutzerID);
            if (bOK)
            {
                //Add Logbucheintrag update
                string ViewID = clsADR.GetMatchCodeByID(ADR_ID, BenutzerID);
                string Beschreibung = "Kunde: " + ViewID + " ID:" + ADR_ID + " Kundennummer:" + KD_ID + " >>> Daten geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
                clsMessages.Kunde_SaveKundenDataOK();
            }
        }
        ///<summary>clsADR / dataTableKD</summary>
        ///<remarks></remarks>>
        public static DataTable dataTableKD(decimal myBenutzer)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT " +
                                          "ID, " +
                                          "ViewID as 'Suchbegriff', " +
                                          "KD_ID, " +
                                          "Name1, " +
                                          "Str as 'Strasse', " +
                                          "HausNr," +
                                          "PLZ, " +
                                          "Ort, " +
                                          "Land " +
                                          "FROM ADR WHERE KD_ID!=0";

            DataTable KDTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myBenutzer, "KundenAdressen");
            return KDTable;
        }



        //
        //----------------- liest die Kontaktdaten  zu einer ADR-ID --------------
        //
        public static DataSet ReadKDbyID(decimal ID)
        {
            DataSet ds = new DataSet();
            ds.Clear();
            string strSQL = "SELECT " +
                                            "ID " +
                                            ", KD_ID " +
                                            ", SteuerNr " +
                                            ", USt_ID " +
                                            ", MwSt " +
                                            ", MwStSatz " +
                                            ", Bank1 " +
                                            ", BLZ1 " +
                                            ", Kto1 " +
                                            ", Swift1 " +
                                            ", IBAN1" +
                                            ", Bank2 " +
                                            ", BLZ2 " +
                                            ", Kto2 " +
                                            ", Swift2 " +
                                            ", IBAN2 " +
                                            ", Kreditor " +
                                            ", Debitor " +
                                            ", ZZ" +
                                            ", SalesTaxKeyDebitor" +
                                            ", SalesTaxKeyKreditor" +
                                            ", Contact" +
                                            ", Phone" +
                                            ", Mailaddress" +
                                            ", Organisation" +
                                                        "FROM Kunde WHERE ADR_ID=" + ID + "; ";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, 0, "KundenAdressen");
            ds.Tables.Add(dt);
            return ds;
        }
        //
        //------------ check auf vergebene Kundennummer -----
        //
        public static bool IsADR_KD(decimal adr_ID)
        {
            bool IsKD = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM Kunde WHERE ADR_ID='" + adr_ID + "'";
            Globals.SQLcon.Open();

            if (Command.ExecuteScalar() == null)
            {
                IsKD = false;
            }
            else
            {
                IsKD = true;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return IsKD;
        }
        ///<summary>clsArbeitsbereiche / DeleteKunde</summary>
        ///<remarks>Löscht einen Kunden aus der Datenbank.</remarks>
        public void DeleteKunde()
        {
            if (IsADR_KD(ADR_ID))
            {
                string strSql = string.Empty;
                strSql = "DELETE FROM Kunde WHERE ADR_ID='" + ADR_ID + "'";
                bool bDeleteOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                //Add Logbucheintrag Löschen
                KD_ID = GetKD_IDbyADR_ID(ADR_ID);
                string Beschreibung = "ID:" + ADR_ID + " Kundennummer:" + KD_ID + " gelöscht";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), Beschreibung);
            }
        }
        //
        //
        //
        public decimal GetMwStSatz()
        {
            decimal MwStSatz = 0.00m;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT MwStSatz FROM Kunde WHERE ADR_ID='" + ADR_ID + "'";
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                MwStSatz = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return MwStSatz;
        }
        //
        //--------------- MwSt-Satz für einen kunden ----------------------
        //
        public static decimal GetMwStSatz(decimal adrID)
        {
            decimal MwStSatz = 0.00m;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT MwStSatz FROM Kunde WHERE ADR_ID='" + adrID + "'";
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                MwStSatz = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return MwStSatz;
        }
        //
        //--------------- MwSt-Satz für einen kunden ----------------------
        //
        public static decimal GetKD_IDbyADR_ID(decimal adrID)
        {
            decimal KD = 0;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT KD_ID FROM ADR WHERE ID='" + adrID + "'";
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                KD = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return KD;
        }
        //
        //--------- Check ob Kundenummer schon verwendet wird ---------------------
        //
        public static bool CheckKundenNummerIsUsed(decimal Kundennummer)
        {
            bool IsUsed = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM Kunde WHERE KD_ID='" + Kundennummer + "'";
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if (obj != null)
            {
                IsUsed = true;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
            return IsUsed;
        }
    }
}
