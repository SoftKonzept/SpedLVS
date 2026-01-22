using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LVS.Dokumente
{
    public class clsRechnungen
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
        //************************************#

        internal Int32 iBit1 = 1;
        internal Int32 iBit0 = 0;

        private decimal _RGNr;
        private decimal _Rechnungsnummer;
        private decimal _RG_ID;
        //Rechnungsempfänger
        private string _RGEName1;
        private string _RGEName2;
        private string _RGEName3;
        private string _RGEStr;
        private string _RGEPLZ;
        private string _RGEOrt;

        private DateTime _Faelligkeit;
        private decimal _User;
        private DateTime _UserDate;
        private decimal _AP_ID;
        private DateTime _RG_Date;
        private DateTime _Faellig_Date;
        private DateTime _Druck_Date;
        private decimal _KD_Nr;
        private string _Kundenname;
        private decimal _RG_Betrag;
        private DateTime _Bezahlt_Date;
        private decimal _MwStSatz;

        public decimal RGNr
        {
            get
            {
                try
                {
                    SqlDataAdapter ada = new SqlDataAdapter();
                    SqlCommand Command = new SqlCommand();
                    Command.Connection = Globals.SQLcon.Connection;
                    ada.SelectCommand = Command;
                    Command.CommandText = "DECLARE @NewID table( NewAID decimal ); " +
                                            "UPDATE Rechnungsnummer SET ID= ID + 1 " +
                                            "OUTPUT INSERTED.ID INTO @NewID; " +
                                            "SELECT * FROM @NewId;";

                    Globals.SQLcon.Open();

                    _RGNr = (decimal)Command.ExecuteScalar();
                    Command.Dispose();
                    Globals.SQLcon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return _RGNr;
            }
            set { _RGNr = value; }
        }
        public decimal RG_ID
        {
            get { return _RG_ID; }
            set { _RG_ID = value; }
        }
        public decimal Rechnungsnummer
        {
            get { return _Rechnungsnummer; }
            set { _Rechnungsnummer = value; }
        }

        //Rechnungsempfänger RGE
        public string RGEName1
        {
            get { return _RGEName1; }
            set { _RGEName1 = value; }
        }
        public string RGEName2
        {
            get { return _RGEName2; }
            set { _RGEName2 = value; }
        }
        public string RGEName3
        {
            get { return _RGEName3; }
            set { _RGEName3 = value; }
        }
        public string RGEStr
        {
            get { return _RGEStr; }
            set { _RGEStr = value; }
        }
        public string RGEPLZ
        {
            get { return _RGEPLZ; }
            set { _RGEPLZ = value; }
        }
        public string RGEOrt
        {
            get { return _RGEOrt; }
            set { _RGEOrt = value; }
        }
        public DateTime Faelligkeit
        {
            get
            {
                _Faelligkeit = DateTime.Today.Date.AddDays(30);
                return _Faelligkeit;
            }
            set { _Faelligkeit = value; }
        }
        public decimal User
        {
            get
            {
                _User = 1;
                return _User;
            }
            set { _User = value; }
        }
        public DateTime UserDate
        {
            get
            {
                //vorer
                _UserDate = DateTime.Today.Date;
                return _UserDate;
            }
            set { _UserDate = value; }
        }
        public decimal AP_ID
        {
            get { return _AP_ID; }
            set { _AP_ID = value; }
        }
        public DateTime RG_Date
        {
            get { return _RG_Date; }
            set { _RG_Date = value; }
        }
        public DateTime Faellig_Date
        {
            get { return _Faellig_Date; }
            set { _Faellig_Date = value; }
        }
        public DateTime Druck_Date
        {
            get { return _Druck_Date; }
            set { _Druck_Date = value; }
        }
        public decimal KD_Nr
        {
            get { return _KD_Nr; }
            set { _KD_Nr = value; }
        }
        public string Kundenname
        {
            get { return _Kundenname; }
            set { _Kundenname = value; }
        }
        public decimal RG_Betrag
        {
            get { return _RG_Betrag; }
            set { _RG_Betrag = value; }
        }
        public DateTime Bezahlt_Date
        {
            get { return _Bezahlt_Date; }
            set { _Bezahlt_Date = value; }
        }
        public decimal MwStSatz
        {
            get { return _MwStSatz; }
            set { _MwStSatz = value; }
        }
        /************************************************************************************
         *                          DB Rechnung 
         * *********************************************************************************/
        //
        //-------- Eintrag --------------
        //
        public void AddRGGS(bool GS)
        {

            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                if (!GS)
                {
                    //----- SQL Abfrage -----------------------
                    Command.CommandText = ("INSERT INTO Rechnungen (RGNr, Datum, faellig, Druck, Druckdatum, Benutzer, Benutzer_Date, GS, MwStSatz) " +
                                                    "VALUES ('" + RG_ID + "','"
                                                                + DateTime.Today.Date + "','"
                                                                + Faelligkeit + "','"
                                                                + iBit1 + "','"
                                                                + DateTime.Today.Date + "','"
                                                                + User + "','"
                                                                + UserDate + "','"
                                                                + iBit0 + "','"
                                                                + MwStSatz.ToString().Replace(",", ".") + "')");
                }
                else
                {
                    Command.CommandText = ("INSERT INTO Rechnungen (RGNr, Datum, faellig, Druck, Druckdatum, Benutzer, Benutzer_Date, GS, MwStSatz) " +
                                            "VALUES ('" + RG_ID + "','"
                                                        + DateTime.Today.Date + "','"
                                                        + Faelligkeit + "','"
                                                        + iBit1 + "','"
                                                        + DateTime.Today.Date + "','"
                                                        + User + "','"
                                                        + UserDate + "','"
                                                        + iBit1 + "','"
                                                        + MwStSatz.ToString().Replace(",", ".") + "')");
                }
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        //
        //-------------------- Update Frachten RGNr setzen -----------
        //
        public void SetRGNrToFrachten(DataSet ds)
        {
            if (ds.Tables["Frachtdaten"] != null)
            {
                bool bGS = (bool)ds.Tables["Frachtdaten"].Rows[0]["GS"];
                for (Int32 i = 0; i <= ds.Tables["Frachtdaten"].Rows.Count - 1; i++)
                {
                    AP_ID = (decimal)ds.Tables["Frachtdaten"].Rows[i]["AP_ID"];
                    try
                    {
                        //--- initialisierung des sqlcommand---
                        SqlCommand Command = new SqlCommand();
                        Command.Connection = Globals.SQLcon.Connection;
                        if (bGS) //GS
                        {
                            Command.CommandText = "Update Frachten SET RG_ID='" + RG_ID + "' WHERE AP_ID='" + AP_ID + "' AND GS='" + iBit1 + "'";
                        }
                        else // RECHNUNG
                        {
                            Command.CommandText = "Update Frachten SET RG_ID='" + RG_ID + "' WHERE AP_ID='" + AP_ID + "' AND GS='" + iBit0 + "'";
                        }
                        Globals.SQLcon.Open();
                        Command.ExecuteNonQuery();
                        Command.Dispose();
                        Globals.SQLcon.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString());

                    }
                    finally
                    {
                        //MessageBox.Show("Update durchgeführt");
                    }
                }
            }
            //Update RGNr für manuelle RG / GS
            if (ds.Tables["RGGSDaten"] != null)
            {
                for (Int32 i = 0; i <= ds.Tables["Auftragsdaten"].Rows.Count - 1; i++)
                {
                    decimal Frachten_ID = (decimal)ds.Tables["Auftragsdaten"].Rows[i]["ID"];
                    try
                    {
                        //--- initialisierung des sqlcommand---
                        SqlCommand Command = new SqlCommand();
                        Command.Connection = Globals.SQLcon.Connection;

                        Command.CommandText = "Update Frachten SET RG_ID='" + RG_ID + "' WHERE ID='" + Frachten_ID + "'";

                        Globals.SQLcon.Open();
                        Command.ExecuteNonQuery();
                        Command.Dispose();
                        Globals.SQLcon.Close();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.ToString());

                    }
                    finally
                    {
                        //MessageBox.Show("Update durchgeführt");
                    }
                }
            }
        }
        //
        //------------------ Update Column Bezahlt -------------
        //
        public void UpdateRGBezahlt()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = "Update Rechnungen SET Bezahlt='" + DateTime.Now + "' WHERE RGNr='" + Rechnungsnummer + "'";


                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //
        //--------- Check Auftragsposition, ob alle notwendigen Dokumente (RG/GS) erstellt wurden ------------
        //
        public static bool AbrechnungAbgeschlossen(ref DataSet dsRG)
        {
            bool abeschlossen = false;
            bool BerechnungKunde = false;
            bool BerechnungSU = false;

            decimal AP_ID = 0;
            for (Int32 i = 0; i <= dsRG.Tables["Frachtdaten"].Rows.Count - 1; i++)
            {
                if (AP_ID != (decimal)dsRG.Tables["Frachtdaten"].Rows[i]["AP_ID"])
                {
                    AP_ID = (decimal)dsRG.Tables["Frachtdaten"].Rows[i]["AP_ID"];

                    //CHECK Ausgangsrechnung / Eingangsgutschrift vom Kunden
                    BerechnungKunde = CheckForRG(AP_ID);

                    //auf Ausgangsrechnung geprüft, falls BerechnungKunde=false noch prüfen auf Eingangsgutschrift
                    if (!BerechnungKunde)
                    {
                        BerechnungKunde = CheckForGS(AP_ID);
                    }
                    /***
                    //CHECK Selbsteintritt oder Frachtvergabe
                    if (clsKommission.IsAuftragPositionIn(_, AP_ID)) //Selbsteintritt
                    {
                      if (BerechnungKunde)
                      {
                        abeschlossen = true;
                      }
                    }
                    else
                    { 
                      //Check auf Frachtvergabe an SU
                      if (clsFrachtvergabe.IsIDIn(AP_ID))
                      {

                        //CHECK auf Eingangsrechnung / Gutschrift an SU 
                        BerechnungSU = CheckForGSSU(AP_ID);

                        if ((BerechnungSU) & (BerechnungKunde))
                        {
                          abeschlossen = true;
                        }
                      }

                    }
                        * ***/
                }
            }
            return abeschlossen;
        }
        //
        //----- Rechnungsnummer vom AuftragspositionsID -----------------
        //
        public static decimal GetRGNrByAP_ID(decimal ap_id)
        {
            decimal rgnr = 0;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT DISTINCT(RG_ID) FROM Frachten WHERE AP_ID='" + ap_id + "'";
                Globals.SQLcon.Open();

                object obj = Command.ExecuteScalar();
                if (obj == null)
                {
                    rgnr = 0;
                }
                else
                {
                    rgnr = (decimal)obj;
                }
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return rgnr;
        }
        //
        //------ cHeck ob schon Rechnung existiert -------------
        //
        public static bool CheckForRG(decimal AP_ID)
        {
            bool RechnungExist = false;
            RechnungExist = RGGSisCreated(AP_ID, false, false);
            return RechnungExist;
        }
        //
        //--------- CHeck auf GS ------------------------
        //
        public static bool CheckForGS(decimal AP_ID)
        {
            //bool IsGS = clsFakturierung.IsAPisInRG(AP_ID, false);
            bool IsGS = RGGSisCreated(AP_ID, true, false);
            return IsGS;
        }
        //
        //
        //
        public static bool CheckForGSSU(decimal AP_ID)
        {
            //bool IsGS = clsFakturierung.IsAPisInRGSU(AP_ID);
            bool IsGS = RGGSisCreated(AP_ID, true, true);
            return IsGS;
        }
        //
        //----------- RG / GS erstellt und gedruckt -------------- 
        //
        public static bool RGGSisCreated(decimal AuftragspositionsID, bool GS, bool GS_SU)
        {
            bool IsIn = false;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;

                string sql = "SELECT ID FROM Rechnungen WHERE " +
                                                  "RGNr=" +
                                                  "(SELECT RG_ID FROM Frachten WHERE AP_ID='" + AuftragspositionsID + "'";

                // Rechnung
                if ((!GS))
                {
                    if (!GS_SU)
                    {
                        sql = sql + " AND GS='0' AND GS_SU='0') AND GS='0'";
                    }
                    else
                    {
                        sql = sql + " AND GS='0') AND GS='0'";
                    }
                }
                // Gutschrift
                if ((GS) & (!GS_SU))
                {
                    sql = sql + " AND GS='1' AND GS_SU='0') AND GS='1'";
                }
                // Gutschrift an SU
                if ((GS) & (GS_SU))
                {
                    sql = sql + " AND GS='1' AND GS_SU='1') AND GS='1'";
                }

                Command.CommandText = sql;

                Globals.SQLcon.Open();

                object obj = Command.ExecuteScalar();
                if (obj != null)
                {
                    IsIn = true; ;
                }
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return IsIn;
        }
        //
        //----------- FVGS ist erstellt und gedruckt -------------- 
        //
        public static bool FVGSisCreated(decimal AuftragspositionsID)
        {
            bool IsIn = false;
            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;

                string sql = "SELECT ID FROM Rechnungen WHERE " +
                                                  "RGNr=" +
                                                  "(SELECT RG_ID FROM Frachten WHERE AP_ID='" + AuftragspositionsID + "' AND GS='1' AND FVGS='1')";

                Command.CommandText = sql;
                Globals.SQLcon.Open();

                object obj = Command.ExecuteScalar();
                if (obj != null)
                {
                    IsIn = true; ;
                }
                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return IsIn;
        }
        //
        //---------- Druckdatum by AuftragspositionsID ------------------
        //
        public static DateTime GetDruckDate(decimal AuftragspositionsID, bool RG, bool GS_SU)
        {
            Int32 iBit = 0;

            if (!RG)
            {
                iBit = 1;
            }
            string sql = string.Empty;
            DateTime printDate = DateTime.MinValue;

            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;

            sql = "SELECT Druckdatum FROM Rechnungen WHERE RGNr=" +
                    "(SELECT RG_ID FROM Frachten WHERE AP_ID='" + AuftragspositionsID + "' AND";


            if (GS_SU)
            {
                sql = sql + " GS='" + iBit + "' AND GS_SU='1')";
            }
            else
            {
                sql = sql + " GS='" + iBit + "'  AND GS_SU='0')";
            }

            Command.CommandText = sql;
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();
            if (obj != null)
            {
                printDate = (DateTime)obj; ;
            }

            Command.Dispose();
            Globals.SQLcon.Close();
            return printDate;
        }
        //
        //------------- Liste RG / GS für Fakturierungsliste -------------
        //
        public static DataTable GetRGGSDateForList(bool GS, DateTime von, DateTime bis)
        {
            DataTable dt = new DataTable("Rechnungsdaten");
            try
            {
                string sql = string.Empty;
                dt.Clear();
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;

                sql = "SELECT " +
                                "ID, " +
                                "RGNr, " +
                                "Datum, " +
                                "faellig, " +
                                "Druck, " +
                                "Druckdatum, " +
                                "Benutzer, " +
                                "Benutzer_Date, " +
                                "Bezahlt, " +
                                "MwStSatz, " +
                                "(SELECT KD_ID FROM Kunde WHERE ADR_ID=(SELECT DISTINCT Fracht_ADR FROM Frachten WHERE RG_ID = RGNr)) as 'KDNr', " +
                                "(SELECT Name1 FROM ADR WHERE ID=(SELECT DISTINCT Fracht_ADR FROM Frachten WHERE RG_ID = RGNr)) as 'Kunde', " +
                                "(SELECT SUM(Fracht) FROM Frachten WHERE RG_ID = RGNr) as 'RG_Betrag' " +

                                "FROM Rechnungen " +
                                "WHERE Datum>'" + von + "' AND Datum <'" + bis + "'";

                if (GS)
                {
                    sql = sql + " AND GS='1'";
                }
                else
                {
                    sql = sql + " AND GS='0'";
                }
                Command.CommandText = sql;

                Globals.SQLcon.Open();
                ada.Fill(dt);
                ada.Dispose();
                Command.Dispose();
                Globals.SQLcon.Close();

                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;
        }
        //
        //-------------- Offene Posten -----------------------
        //
        public static DataTable GetOffenePosten(bool GS, bool GSanSU, DateTime von, DateTime bis)
        {
            DataTable dt = new DataTable("Rechnungsdaten");
            try
            {
                string sql = string.Empty;
                dt.Clear();
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;

                sql = "SELECT " +
                                "Rechnungen.ID, " +
                                "Rechnungen.RGNr, " +
                                "Rechnungen.Datum, " +
                                "Rechnungen.faellig, " +
                                "Rechnungen.Druck, " +
                                "Rechnungen.Druckdatum, " +
                                "Rechnungen.Benutzer, " +
                                "Rechnungen.Benutzer_Date, " +
                                "Rechnungen.Bezahlt, " +
                                "Rechnungen.MwStSatz, " +
                                "(SELECT KD_ID FROM Kunde WHERE ADR_ID=(SELECT DISTINCT Fracht_ADR FROM Frachten WHERE RG_ID = RGNr)) as 'KDNr', " +
                                "(SELECT Name1 FROM ADR WHERE ID=(SELECT DISTINCT Fracht_ADR FROM Frachten WHERE RG_ID = RGNr)) as 'Kunde', " +
                                "(SELECT SUM(Fracht) FROM Frachten WHERE RG_ID = RGNr) as 'RG_Betrag' " +

                                "FROM Rechnungen " +
                                "INNER JOIN Frachten ON Frachten.RG_ID=Rechnungen.RGNr " +
                                "WHERE " +
                                "((SELECT Status FROM AuftragPos WHERE ID=(Select Distinct AP_ID FROM Frachten WHERE RG_ID=RGNr))='7') " +
                                "AND Rechnungen.Datum>'" + von + "' AND Rechnungen.Datum <'" + bis + "'";



                if (GS)
                {
                    if (GSanSU)
                    {
                        sql = sql + " AND Rechnungen.GS='1' AND Rechnungen.GSanSU='1'";
                    }
                    else
                    {
                        sql = sql + " AND Rechnungen.GS='1'";
                    }
                }
                else
                {
                    sql = sql + " AND Rechnungen.GS='0'";
                }
                Command.CommandText = sql;

                Globals.SQLcon.Open();
                ada.Fill(dt);
                ada.Dispose();
                Command.Dispose();
                Globals.SQLcon.Close();

                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;
        }

        //
        //----------- Löschvorgang Rechnungen -------------
        //
        public void Delete(DataTable dt)
        {
            Rechnungsnummer = Convert.ToInt32(dt.Rows[0]["RGNr"]);
            DataTable dtAP = clsFakturierung.GetAP_IDFromFrachten(Rechnungsnummer);
            RG_ID = (decimal)dt.Rows[0]["ID"];

            //löschen aus DB Rechnungen
            DeleteFromRechnungen();

            //Update Frachten - setzt RG
            clsFakturierung.UpdateRGNrToZero(Rechnungsnummer);

            //DB AuftragPos Update 
            for (Int32 i = 0; i <= dtAP.Rows.Count - 1; i++)
            {
                decimal AP_ID = 0;
                AP_ID = (decimal)dtAP.Rows[i]["AP_ID"];
                clsAuftragsstatus ast = new clsAuftragsstatus();
                ast.SetStatusForBerechnungByAP_ID(AP_ID);
            }
        }
        //
        //--------- löschen RG by ID -----------
        //
        private void DeleteFromRechnungen()
        {
            //--- initialisierung des sqlcommand---
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            Command.CommandText = "DELETE FROM Rechnungen WHERE ID='" + RG_ID + "'";
            Globals.SQLcon.Open();
            Command.ExecuteNonQuery();
            Command.Dispose();
            Globals.SQLcon.Close();
            if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
            {
                Command.Connection.Close();
            }
        }

    }
}
