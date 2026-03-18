using LVS;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sped4.Classes
{
    class clsFrachtvergabe
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
        //************************************
        //public decimal MandantenID;
        public DataSet dsTransportauftrag = new DataSet();

        private decimal _ID;
        private decimal _RGNr;
        private decimal _GSNr;
        private decimal _ID_AP;     //ID DB AuftragsPos von Auftrag und AuftragPos
        private decimal _AuftragID;
        private decimal _AuftragPos;
        private decimal _KundeID;
        private string _KD_Name;
        private decimal _B_ID;
        private string _B_Name;
        private string _B_PLZ;
        private string _B_Ort;
        private decimal _E_ID;
        private string _E_Name;
        private string _E_PLZ;
        private string _E_Ort;
        private decimal _Artikel_ID;
        private decimal _Gewicht;
        private decimal _Fracht;
        private string _Gut;
        private Int32 _Status;
        private Int32 _km;
        private decimal _PreisTo;
        private decimal _PreisPal;
        private decimal _PreisKm;
        private Int32 _Abrechnungsliste;
        private DateTime _vonZeitraum;
        private DateTime _bisZeitraum;
        private DateTime _B_Date;           // angegebene Beladedatum im Transportauftrag an SU
        private DateTime _B_Time;           // angegebene Beladezeit im Transportauftrag an SU
        private DateTime _E_Date;           // angegebene Beladedatum im Transportauftrag an SU
        private DateTime _E_Time;           // angegebene Beladezeit im Transportauftrag an SU
        private DateTime _VSB;
        private string _Info;
        private string _strFracht;
        private string _zHd;
        private string _Ladenummer;
        private DateTime _Datum;
        private DateTime _Dr_Datum;
        private decimal _Dr_User;



        public decimal ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public decimal ID_AP
        {
            get { return _ID_AP; }
            set { _ID_AP = value; }
        }
        public decimal RGNr
        {
            get { return _RGNr; }
            set { _RGNr = value; }
        }
        public decimal GSNr
        {
            get { return _GSNr; }
            set { _GSNr = value; }
        }
        public decimal AuftragID
        {
            get { return _AuftragID; }
            set { _AuftragID = value; }
        }
        public decimal AuftragPos
        {
            get { return _AuftragPos; }
            set { _AuftragPos = value; }
        }
        public decimal KundeID
        {
            get { return _KundeID; }
            set { _KundeID = value; }
        }

        public decimal Fracht
        {
            get { return _Fracht; }
            set { _Fracht = value; }
        }
        public string KD_Name
        {
            get { return _KD_Name; }
            set { _KD_Name = value; }
        }
        public decimal B_ID
        {
            get { return _B_ID; }
            set { _B_ID = value; }
        }
        public string B_Name
        {
            get { return _B_Name; }
            set { _B_Name = value; }
        }
        public string B_PLZ
        {
            get { return _B_PLZ; }
            set { _B_PLZ = value; }
        }
        public string B_Ort
        {
            get { return _B_Ort; }
            set { _B_Ort = value; }
        }
        public decimal E_ID
        {
            get { return _E_ID; }
            set { _E_ID = value; }
        }
        public string E_Name
        {
            get { return _E_Name; }
            set { _E_Name = value; }
        }
        public string E_PLZ
        {
            get { return _E_PLZ; }
            set { _E_PLZ = value; }
        }
        public string E_Ort
        {
            get { return _E_Ort; }
            set { _E_Ort = value; }
        }
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Gut
        {
            get { return _Gut; }
            set { _Gut = value; }
        }
        public Int32 km
        {
            get { return _km; }
            set { _km = value; }
        }
        public decimal PreisTo
        {
            get { return _PreisTo; }
            set { _PreisTo = value; }
        }
        public decimal PreisPal
        {
            get { return _PreisPal; }
            set { _PreisPal = value; }
        }
        public decimal PreisKm
        {
            get { return _PreisKm; }
            set { _PreisKm = value; }
        }
        public decimal Gewicht
        {
            get { return _Gewicht; }
            set { _Gewicht = value; }
        }
        public Int32 Abrechnungsliste
        {
            get { return _Abrechnungsliste; }
            set { _Abrechnungsliste = value; }
        }
        public DateTime vonZeitraum
        {
            get { return _vonZeitraum; }
            set { _vonZeitraum = value; }
        }
        public DateTime bisZeitraum
        {
            get { return _bisZeitraum; }
            set { _bisZeitraum = value; }
        }
        public DateTime B_Date
        {
            get { return _B_Date; }
            set { _B_Date = value; }
        }
        public DateTime B_Time
        {
            get { return _B_Time; }
            set { _B_Time = value; }
        }
        public DateTime E_Date
        {
            get { return _E_Date; }
            set { _E_Date = value; }
        }
        public DateTime E_Time
        {
            get { return _E_Time; }
            set { _E_Time = value; }
        }
        public DateTime VSB
        {
            get { return _VSB; }
            set { _VSB = value; }
        }
        public string Info
        {
            get { return _Info; }
            set { _Info = value; }
        }
        public string strFracht
        {
            get { return _strFracht; }
            set { _strFracht = value; }
        }
        public string zHd
        {
            get { return _zHd; }
            set { _zHd = value; }
        }
        public string Ladenummer
        {
            get { return _Ladenummer; }
            set { _Ladenummer = value; }
        }
        public DateTime Datum
        {
            get { return _Datum; }
            set { _Datum = value; }
        }
        public DateTime Dr_Datum
        {
            get { return _Dr_Datum; }
            set { _Dr_Datum = value; }
        }
        public decimal Dr_User
        {
            get { return _Dr_User; }
            set { _Dr_User = value; }
        }

        /************************************************************************************************
         *                            DataSET füllen
         *              
         * ********************************************************************************************/

        //-----------------------------------------ADR - DATEN ---------------------------------------
        //
        //--------- Auftraggeber --------------
        //
        public void InsertAuftraggebertoDataSet(DataSet ds)
        {
            InitADRTable("Auftraggeber");
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row;
                row = dsTransportauftrag.Tables["Auftraggeber"].NewRow();
                row["ID"] = ds.Tables[0].Rows[i]["ID"];
                row["FBez"] = ds.Tables[0].Rows[i]["FBez"];
                row["Name1"] = ds.Tables[0].Rows[i]["Name1"];
                row["Name2"] = ds.Tables[0].Rows[i]["Name2"];
                row["Name3"] = ds.Tables[0].Rows[i]["Name3"];
                row["Str"] = ds.Tables[0].Rows[i]["Str"];
                row["PLZ"] = ds.Tables[0].Rows[i]["PLZ"];
                row["Ort"] = ds.Tables[0].Rows[i]["Ort"];

                dsTransportauftrag.Tables["Auftraggeber"].Rows.Add(row);
            }
        }
        //Versender
        public void InsertVersendertoDataSet(DataSet ds)
        {
            InitADRTable("Versender");
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row;
                row = dsTransportauftrag.Tables["Versender"].NewRow();
                row["ID"] = ds.Tables[0].Rows[i]["ID"];
                row["FBez"] = ds.Tables[0].Rows[i]["FBez"];
                row["Name1"] = ds.Tables[0].Rows[i]["Name1"];
                row["Name2"] = ds.Tables[0].Rows[i]["Name2"];
                row["Name3"] = ds.Tables[0].Rows[i]["Name3"];
                row["Str"] = ds.Tables[0].Rows[i]["Str"];
                row["PLZ"] = ds.Tables[0].Rows[i]["PLZ"];
                row["Ort"] = ds.Tables[0].Rows[i]["Ort"];

                dsTransportauftrag.Tables["Versender"].Rows.Add(row);
            }
        }
        //Empfänger
        public void InsertEmpfaengertoDataSet(DataSet ds)
        {
            InitADRTable("Empfänger");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row;
                row = dsTransportauftrag.Tables["Empfänger"].NewRow();
                row["ID"] = ds.Tables[0].Rows[i]["ID"];
                row["FBez"] = ds.Tables[0].Rows[i]["FBez"];
                row["Name1"] = ds.Tables[0].Rows[i]["Name1"];
                row["Name2"] = ds.Tables[0].Rows[i]["Name2"];
                row["Name3"] = ds.Tables[0].Rows[i]["Name3"];
                row["Str"] = ds.Tables[0].Rows[i]["Str"];
                row["PLZ"] = ds.Tables[0].Rows[i]["PLZ"];
                row["Ort"] = ds.Tables[0].Rows[i]["Ort"];

                dsTransportauftrag.Tables["Empfänger"].Rows.Add(row);
            }
        }
        //Subunternehmer
        public void InsertSUtoDataSet(DataSet ds)
        {
            InitADRTable("SU");
            for (Int32 i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow row;
                row = dsTransportauftrag.Tables["SU"].NewRow();
                row["ID"] = ds.Tables[0].Rows[i]["ID"];
                row["FBez"] = ds.Tables[0].Rows[i]["FBez"];
                row["Name1"] = ds.Tables[0].Rows[i]["Name1"];
                row["Name2"] = ds.Tables[0].Rows[i]["Name2"];
                row["Name3"] = ds.Tables[0].Rows[i]["Name3"];
                row["Str"] = ds.Tables[0].Rows[i]["Str"];
                row["PLZ"] = ds.Tables[0].Rows[i]["PLZ"];
                row["Ort"] = ds.Tables[0].Rows[i]["Ort"];
                if (zHd == string.Empty)
                {
                    row["zHd"] = zHd;
                }
                else
                {
                    row["zHd"] = "z.Hd.: " + zHd;
                }
                dsTransportauftrag.Tables["SU"].Rows.Add(row);
            }
        }
        //
        private void InitADRTable(string ADRDaten)
        {
            if (dsTransportauftrag.Tables[ADRDaten] == null)
            {
                dsTransportauftrag.Tables.Add(ADRDaten);
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("ID");
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("FBez");
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("Name1");
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("Name2");
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("Name3");
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("Str");
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("PLZ");
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("Ort");
                dsTransportauftrag.Tables[ADRDaten].Columns.Add("zHd");
            }
        }
        //
        //
        //------------------------------------- Auftragsdaten ---------------------------------
        //Artikeldaten
        public void InsertArtikeltoDataSet(DataTable dt)
        {
            dt.TableName = "Artikel";
            dsTransportauftrag.Tables.Add(dt);
        }
        //
        //
        //
        public void GetAuftragsDaten()
        {
            DataTable dt = clsAuftragPos.ReadDataByAuftragIDandAuftragPos(AuftragID, AuftragPos);
            //DataSet ds1 = clsAuftragPos.ReadDataByID(AuftragID, AuftragPos);
            dsTransportauftrag.Tables.Add(dt);
        }
        //
        //----------- Tabelle Angaben Transportauftrag
        //
        private void InitAuftragTable(string Tabellenname)
        {
            dsTransportauftrag.Tables.Add(Tabellenname);
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("ID_AP");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("KD_ID");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("B_Date");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("B_Time");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("E_Date");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("E_Time");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("Fracht");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("Info");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("VSB");
            dsTransportauftrag.Tables[Tabellenname].Columns.Add("Ladenummer");
        }
        //
        //
        //Transportauftrag
        public void InsertTransportauftragDatentoDataSet()
        {
            InitAuftragTable("Transportauftrag");
            DataRow row;
            row = dsTransportauftrag.Tables["Transportauftrag"].NewRow();
            row["ID_AP"] = ID_AP;
            row["KD_ID"] = KundeID;
            row["B_Date"] = B_Date;
            row["B_Time"] = B_Time;
            row["E_Date"] = E_Date;
            row["E_Time"] = E_Time;
            row["Fracht"] = strFracht;
            row["Info"] = Info;
            row["VSB"] = VSB;
            row["Ladenummer"] = Ladenummer;

            dsTransportauftrag.Tables["Transportauftrag"].Rows.Add(row);

        }
        //
        public void UpdateSU()
        {
            for (Int32 i = 0; i < dsTransportauftrag.Tables["SU"].Rows.Count; i++)
            {
                dsTransportauftrag.Tables["SU"].Rows[i]["zHd"] = "z.Hd.: " + zHd;
            }
        }
        //
        //------ Prüfen ob der Datensatz schon vorhanden ist -------------
        //
        public static bool IsIDIn(decimal ID_AP)
        {
            bool IsIn = false;
            SqlDataAdapter ada = new SqlDataAdapter();
            SqlCommand Command = new SqlCommand();
            Command.Connection = Globals.SQLcon.Connection;
            ada.SelectCommand = Command;
            Command.CommandText = "SELECT ID FROM Frachtvergabe WHERE ID_AP='" + ID_AP + "'";
            Globals.SQLcon.Open();

            object obj = Command.ExecuteScalar();

            if ((obj == null) | (obj is DBNull))
            {
                IsIn = false;
            }
            else
            {
                IsIn = true;
                decimal ID = (decimal)obj;
            }
            Command.Dispose();
            Globals.SQLcon.Close();
            return IsIn;
        }
        //
        //---------- Frachtangabe aus Frachtauftrag------------------
        //
        public static string GetFrachtLtAuftrag(decimal ID_AP)
        {
            string Fracht = string.Empty;
            if (ID_AP > 0)
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT Fracht FROM Frachtvergabe WHERE ID_AP='" + ID_AP + "'";
                Globals.SQLcon.Open();

                object obj = Command.ExecuteScalar();

                if ((obj == null) | (obj is DBNull))
                {
                    Fracht = string.Empty;
                }
                else
                {
                    Fracht = (string)obj;
                }
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            return Fracht;
        }





        /************************************************************************************************
         * 
         *                              DB Insert Frachtvergabe 
         *                             
         * **********************************************************************************************/
        //
        //
        public void AddTransportauftrag()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("INSERT INTO Frachtvergabe (ID_AP, SU, B_Date, B_Time, E_Date, E_Time, Fracht, Ladenummer, Info, Datum) " +
                                                "VALUES ('" + ID_AP + "','"
                                                            + KundeID + "','" //SU
                                                            + B_Date + "','"
                                                            + B_Time + "','"
                                                            + E_Date + "','"
                                                            + E_Time + "','"
                                                            + strFracht + "','"
                                                            + Ladenummer + "','"
                                                            + Info + "','"
                                                            + DateTime.Today + "')");

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string Beschreibung = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), Beschreibung);
            }
            finally
            {
                clsAuftragsstatus ast = new clsAuftragsstatus();
                ast.Auftrag_ID = AuftragID;
                ast.AuftragPos = AuftragPos;
                ast.SetStatusDisposition();

                //AuftragPos.T_Date muss mit dem E_Date Datum übereinstimmen
                //Update
                clsAuftragPos.updateTDatInAuftragPosition(ID_AP, E_Date, BenutzerID, true);


                //Add Logbucheintrag Eintrag
                string SU = clsADR.GetMatchCodeByID(KundeID, BenutzerID);
                string Beschreibung = "Disposition - Auftrag/Pos: " + AuftragID + "/" + AuftragPos + " an SU: " + SU + " - Datum:" + B_Date.ToShortDateString() + " vergeben";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.VergabeSU.ToString(), Beschreibung);
            }
        }
        //
        //------- löschen Transportauftrag an SU in Frachtvergabe per ID ------------
        //
        public void DeleteTransportauftrag()
        {
            //  decimal ID_AP = clsAuftragPos.GetIDbyAuftragAndAuftragPos(this._GL_User,AuftragID, AuftragPos, MandantenID, this._GL_User.sys_ArbeitsbereichID);
            //Logbuch
            GetDatenByID_AP(ID_AP);
            string SU = clsADR.GetMatchCodeByID(KundeID, BenutzerID);
            string Beschreibung = "Disposition - Auftrag/Pos: " + AuftragID + "/" + AuftragPos + " an SU: " + SU + " - Datum:" + B_Date.ToShortDateString() + " storniert";
            Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.VergabeSU.ToString(), Beschreibung);

            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "DELETE FROM Frachtvergabe WHERE ID_AP ='" + ID_AP + "'";
                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();

                Command.Dispose();
                Globals.SQLcon.Close();
                if ((Command.Connection != null) && (Command.Connection.State == ConnectionState.Open))
                {
                    Command.Connection.Close();
                }
                //

            }
            catch (Exception ex)
            {
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            //Status zurücksetzen
            clsAuftragsstatus ast = new clsAuftragsstatus();
            ast.Auftrag_ID = AuftragID;
            ast.AuftragPos = AuftragPos;
            ast.SetStatusBackFromDispo();
        }
        //
        //---------- Update DB ADR     -------------------------
        //
        public static void updateSU_ID(decimal AP_ID, decimal ADR_ID)
        {
            string sql = string.Empty;
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                Command.CommandText = "Update Frachtvergabe SET SU='" + ADR_ID + "' WHERE ID_AP='" + AP_ID + "'";

                Globals.SQLcon.Open();
                Command.ExecuteNonQuery();
                Command.Dispose();
                Globals.SQLcon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                decimal BenutzerID = 0;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
            finally
            {
                //MessageBox.Show("Update durchgeführt");
            }
        }
        //
        //
        //
        private void GetDatenByID_AP(decimal AP_ID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT " +
                                                "ID, " +
                                                "ID_AP, " +
                                                "SU, " +
                                                "B_Date " +
                                            //   "B_Time, " +
                                            //   "E_Date, " +
                                            //   "E_Time, " +
                                            //  "Fracht, " +
                                            //  "Ladenummer, " +
                                            //  "Info, " +
                                            //  "Datum, " +
                                            //  "Dr_Datum, " +
                                            //  "Dr_User " +
                                            "FROM " +
                                                "Frachtvergabe " +
                                            "WHERE ID_AP = '" + AP_ID + "'";

                ada.Fill(dt);
                if ((dt.Rows.Count > 0))
                {
                    ID = (decimal)dt.Rows[0]["ID"];
                    ID_AP = (decimal)dt.Rows[0]["ID_AP"];
                    KundeID = (decimal)dt.Rows[0]["SU"];
                    B_Date = (DateTime)dt.Rows[0]["B_Date"];
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //Add Logbucheintrag Exception
                string BeschreibungEx = "Exception: " + ex;
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Exception.ToString(), BeschreibungEx);
            }
        }
    }
}
