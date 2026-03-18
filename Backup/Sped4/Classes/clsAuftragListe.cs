using LVS;
using LVS.Dokumente;
using System;
using System.Data;

namespace Sped4.Classes
{
    class clsAuftragListe
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
        //***********************************
        // Verarbeitung / Prüfung der Daten, wie die Auftragsdaten in der Auftragsliste angezeigt werden

        //private DateTime _m_dt_Liefertermin = default(DateTime);
        private DateTime _m_dt_WAvon = default(DateTime);         // Warenannahme von
        //private DateTime _m_dt_WAbis = default(DateTime);         // Warenannhame bis  
        //private DateTime _m_dt_monitoringPeriode = default(DateTime);
        //private DateTime _m_dt_VSB = default(DateTime);
        private DateTime _m_dt_ZF = default(DateTime);

        //private string _m_str_Auftraggeber_Name1 = string.Empty;
        //private string _m_str_Auftraggeber_PLZ = string.Empty;
        //private string _m_str_Auftraggeber_Ort = string.Empty;
        private string _m_str_Auftraggeber_ADR;

        //private string _m_str_Beladestelle = string.Empty;
        //private string _m_str_B_Strasse = string.Empty;
        //private string _m_str_B_PLZ = string.Empty;
        private string _m_str_B_Ort = string.Empty;
        private string _m_str_B_ADR;

        //private string _m_str_Entladestelle = string.Empty;
        //private string _m_str_E_Strasse = string.Empty;
        //private string _m_str_E_PLZ = string.Empty;
        private string _m_str_E_Ort = string.Empty;
        private string _m_str_E_ADR;
        private decimal _m_i_AuftragPos;
        //private string _m_str_Gut = string.Empty;

        private decimal _m_dec_Gewicht = 0.00m;
        //private decimal _m_dec_gemPosGewicht = 0.00m;
        //private decimal _m_dec_Netto = 0.00m;
        //private decimal _m_dec_Brutto = 0.00m;
        //private decimal _m_dec_gemGesamtGewicht = 0.00m;
        //private decimal _m_dec_GesamtNetto = 0.00m;
        //private decimal _m_dec_GesamtBrutto = 0.00m;
        private decimal _m_dec_VerlGewicht;               //erstmal drin lassen 20.07.2010
        private decimal _m_dec_Restgewicht;               // erstmal drin lassen 20.07.2010
        private bool _m_bo_Prioritaet = false;
        //private string _m_str_Ladenummer = string.Empty;
        private bool _m_bo_LadeNrRequire;
        //private string _InfoWarning = string.Empty;
        private string _SU;
        //private string _m_str_FAngaben = string.Empty;


        public decimal m_dec_ID { get; set; }
        public DateTime m_dt_Liefertermin { get; set; }
        public DateTime m_dt_WAvon { get; set; }
        public string m_str_WAvon { get; set; }
        public DateTime m_dt_WAbis { get; set; }
        public string m_str_WAbis { get; set; }
        public DateTime m_dt_monitoringPeriode { get; set; }
        public DateTime m_dt_VSB { get; set; }
        public DateTime m_dt_ZF { get; set; }
        public DateTime m_dt_drDatum { get; set; }
        public DateTime m_dt_B_Date { get; set; }
        public DateTime m_dt_E_Date { get; set; }
        public Int32 m_i_vKW { get; set; }
        public Int32 m_i_bKW { get; set; }


        //Entladeadresse
        public string m_str_E_ADR
        {
            get
            {
                _m_str_E_ADR = m_str_Entladestelle + Environment.NewLine +
                             m_str_E_PLZ + " - " + _m_str_E_Ort;
                return _m_str_E_ADR;
            }
            set { _m_str_E_ADR = value; }
        }

        public string m_str_Entladestelle { get; set; }
        public string m_str_E_Strasse { get; set; }

        //Beladeadresse
        public string m_str_B_Strasse { get; set; }
        public string m_str_E_PLZ { get; set; }
        public string m_str_E_Ort { get; set; }
        public string m_str_B_PLZ { get; set; }
        public string m_str_B_Ort { get; set; }
        public string m_str_Beladestelle { get; set; }
        public string m_str_B_ADR
        {
            get
            {
                _m_str_B_ADR = m_str_Beladestelle + Environment.NewLine +
                             m_str_B_PLZ + " - " + _m_str_B_Ort;
                return _m_str_B_ADR;
            }
            set { _m_str_B_ADR = value; }
        }

        //Auftraggeber
        public string m_str_Auftraggeber_PLZ { get; set; }
        public string m_str_Auftraggeber_Ort { get; set; }
        public string m_str_Auftraggeber_Name1 { get; set; }
        public string m_str_Auftraggeber_ADR
        {
            get
            {
                _m_str_Auftraggeber_ADR = m_str_Auftraggeber_Name1 + Environment.NewLine +
                             m_str_Auftraggeber_PLZ + " - " + m_str_Auftraggeber_Ort;
                return _m_str_Auftraggeber_ADR;
            }
            set { _m_str_Auftraggeber_ADR = value; }
        }

        public string m_str_FAngaben { get; set; }
        public decimal m_i_AuftragID { get; set; }
        public decimal m_i_AuftragPos { get; set; }
        public string m_str_Gut { get; set; }
        public Int32 m_i_Status { get; set; }
        public decimal m_dec_Gewicht { get; set; }
        public decimal m_dec_gemGesamtGewicht { get; set; }
        public decimal m_dec_GesamtNetto { get; set; }
        public decimal m_dec_GesamtBrutto { get; set; }
        public decimal m_dec_gemPosGewicht { get; set; }
        public decimal m_dec_Netto { get; set; }
        public decimal m_dec_Brutto { get; set; }
        public decimal m_dec_VerlGewicht { get; set; }

        public decimal m_dec_Restgewicht
        {
            get
            {
                if (_m_i_AuftragPos > 0)
                {
                    _m_dec_Restgewicht = _m_dec_Gewicht;

                }
                else
                {
                    _m_dec_Restgewicht = _m_dec_Gewicht - _m_dec_VerlGewicht;
                }
                return _m_dec_Restgewicht;
            }
            set
            {
                _m_dec_Restgewicht = value;
            }
        }

        public decimal m_i_KD_ID { get; set; }
        public Int32 m_i_Ber_A { get; set; }
        public Int32 m_i_Ber_V { get; set; }
        public Int32 m_i_Ber_E { get; set; }
        public bool m_bo_Papiere { get; set; }
        public bool m_bo_Faherer { get; set; }
        public bool m_bo_disponieren { get; set; }
        public bool m_bo_neuerAuftrag { get; set; }
        public bool m_bo_Prioritaet { get; set; }
        public bool m_bo_LadeNrRequire
        {
            get
            {
                if (m_i_AuftragID > 0)
                {
                    _m_bo_LadeNrRequire = GetLadeNrRequire();
                }
                return _m_bo_LadeNrRequire;
            }
            set { _m_bo_LadeNrRequire = value; }
        }
        public string m_str_Ladenummer { get; set; }
        public string InfoWarning { get; set; }
        public decimal ADR_ID_SU { get; set; }
        public string SU
        {
            get
            {
                _SU = clsADR.ReadViewIDbyID(ADR_ID_SU);
                return _SU;
            }
            set { _SU = value; }
        }
        public string m_str_Resscource { get; set; }

        //------------------------------------------ Checks   -------------------------------------------------------------------------
        //                                        Auftragsliste 
        //
        //----------------------------------------------------------------------------------------------------------------------------
        //
        //
        //public void CheckDispoAndFAngaben(ref DataTable dataTable, Globals._GL_USER GL_User)
        public void CheckDispoAndFAngaben(ref DataTable dataTable, Globals._GL_USER GL_User, Int32 iRow)
        {
            Int32 i = iRow;
            //for (Int32 i = 0; i <= dataTable.Rows.Count - 1; i++)
            //{
            /*****************************************************
             * Dispitionscheck wenn Status < 2 dann false
             *                 wenn Status = 2 dann true
             * **************************************************/
            if (dataTable.Rows[i]["Stat"] != DBNull.Value)
            {
                m_i_Status = Convert.ToInt32(dataTable.Rows[i]["Stat"]);
            }
            if (m_i_Status == 1) //unvollständig
            {
                m_bo_disponieren = false;
            }
            else
            {
                m_bo_disponieren = true;
            }
            //Eintrag in Table
            dataTable.Rows[i]["dispo"] = m_bo_disponieren;

            /*******************************************************
             * FAngaben Check
             * ****************************************************/
            if (dataTable.Rows[i]["ZF"] != DBNull.Value)
            {
                m_dt_ZF = (DateTime)dataTable.Rows[i]["ZF"];
            }
            if (dataTable.Rows[i]["faellig"] != DBNull.Value)
            {
                m_dt_Liefertermin = (System.DateTime)dataTable.Rows[i]["faellig"];
            }
            if (dataTable.Rows[i]["VSB"] != DBNull.Value)
            {
                m_dt_VSB = (System.DateTime)dataTable.Rows[i]["VSB"];
            }
            if (dataTable.Rows[i]["Ladenummer"] != DBNull.Value)
            {
                m_str_Ladenummer = (string)dataTable.Rows[i]["Ladenummer"];
            }
            if (dataTable.Rows[i]["Entladestelle"] != DBNull.Value)
            {
                m_str_Entladestelle = Convert.ToString(dataTable.Rows[i]["Entladestelle"]);
            }
            if (dataTable.Rows[i]["Beladestelle"] != DBNull.Value)
            {
                m_str_Beladestelle = Convert.ToString(dataTable.Rows[i]["Beladestelle"]);
            }


            //fehlendeAngaben
            string strAusgabe = string.Empty;
            if (m_dt_Liefertermin.ToString() == DateTime.MaxValue.ToString())
            {
                strAusgabe = strAusgabe + " Liefertermin,";
            }
            if (m_dt_VSB.ToString() == DateTime.MaxValue.ToString())
            {
                strAusgabe = strAusgabe + " VSB,";
            }
            if ((m_str_Ladenummer == "") & (m_bo_LadeNrRequire))
            {
                strAusgabe = strAusgabe + " Ladenummer,";
            }
            if (m_dt_ZF.ToString() == "01.01.1900 00:00:00")
            {
                strAusgabe = strAusgabe + " Zeitfenster,";
            }
            if (!m_bo_okAdressdate)
            {
                strAusgabe = strAusgabe + " Adressberechtigung,";
            }
            m_str_FAngaben = strAusgabe;
            dataTable.Rows[i]["FAngaben"] = m_str_FAngaben;
            dataTable.Rows[i]["okADR"] = m_bo_okAdressdate;

            //SCAN Exist
            if (dataTable.Rows[i]["AuftragID"] != DBNull.Value)
            {
                m_i_AuftragID = Convert.ToDecimal(dataTable.Rows[i]["AuftragID"]);
            }
            //Baustelle
            clsDocScan SCAN = new clsDocScan();
            SCAN.m_dec_AuftragID = Convert.ToDecimal(dataTable.Rows[i]["ID"]);
            dataTable.Rows[i]["ScanExist"] = SCAN.CheckAuftragScanIsIn();

            //READ - Auftrag gelesen
            m_bo_neuerAuftrag = false;
            if (dataTable.Rows[i]["ID"] != DBNull.Value)
            {
                m_dec_ID = Convert.ToInt32(dataTable.Rows[i]["ID"]);
                clsAuftragRead read = new clsAuftragRead();
                m_bo_neuerAuftrag = read.UserReadAuftrag(GL_User.User_ID, m_dec_ID);
            }
            dataTable.Rows[i]["Read"] = m_bo_neuerAuftrag;


            //Dringlichkeit
            if (dataTable.Rows[i]["faellig"] != DBNull.Value)
            {
                m_dt_Liefertermin = Convert.ToDateTime(dataTable.Rows[i]["faellig"]);
            }
            dataTable.Rows[i]["Dringlichkeit"] = m_i_Dringlichkeit;
            if (m_i_Dringlichkeit == 1)
            {
                dataTable.Rows[i]["Prio"] = true;
            }
            //}
        }

        //
        //
        //
        public void SetAuftragsListe(ref clsAuftragListe liste, DataTable dt, Int32 listenArt, Int32 iRow, Globals._GL_USER GL_User)
        {
            Int32 i = iRow;


            //Status
            if (dt.Columns["Stat"] != null)
            {
                if (dt.Rows[i]["Stat"] != DBNull.Value)
                {
                    liste.m_i_Status = Convert.ToInt32(dt.Rows[i]["Stat"]);
                }
            }
            if (dt.Columns["ID"] != null)
            {
                if (dt.Rows[i]["ID"] != DBNull.Value)
                {
                    liste.m_dec_ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                }
            }
            if (dt.Columns["AuftragID"] != null)
            {
                if (dt.Rows[i]["AuftragID"] != DBNull.Value)
                {
                    liste.m_i_AuftragID = Convert.ToInt32(dt.Rows[i]["AuftragID"]);
                }
            }
            if (dt.Columns["Pos"] != null)
            {
                if (dt.Rows[i]["Pos"] != DBNull.Value)
                {
                    liste.m_i_AuftragPos = Convert.ToInt32(dt.Rows[i]["Pos"]);
                }
            }
            if (dt.Columns["AuftraggeberName"] != null)
            {
                //Auftraggeber
                if (dt.Rows[i]["AuftraggeberName"] != DBNull.Value)
                {
                    liste.m_str_Auftraggeber_Name1 = Convert.ToString(dt.Rows[i]["AuftraggeberName"]);
                    liste.m_str_Auftraggeber_Name1 = liste.m_str_Auftraggeber_Name1.Trim();
                }
            }

            if (dt.Columns["AuftraggeberPLZ"] != null)
            {
                if (dt.Rows[i]["AuftraggeberPLZ"] != DBNull.Value)
                {
                    liste.m_str_Auftraggeber_PLZ = Convert.ToString(dt.Rows[i]["AuftraggeberPLZ"]);
                    liste.m_str_Auftraggeber_PLZ = liste.m_str_Auftraggeber_PLZ.Trim();
                }
            }
            if (dt.Columns["AuftraggeberOrt"] != null)
            {
                if (dt.Rows[i]["AuftraggeberOrt"] != DBNull.Value)
                {
                    liste.m_str_Auftraggeber_Ort = Convert.ToString(dt.Rows[i]["AuftraggeberOrt"]);
                    liste.m_str_Auftraggeber_Ort = liste.m_str_Auftraggeber_Ort.Trim();
                }
            }
            if (dt.Columns["Beladestelle"] != null)
            {
                //Beladestelle
                if (dt.Rows[i]["Beladestelle"] != DBNull.Value)
                {
                    liste.m_str_Beladestelle = Convert.ToString(dt.Rows[i]["Beladestelle"]);
                    liste.m_str_Beladestelle = liste.m_str_Beladestelle.Trim();
                }
            }
            if (dt.Columns["B_Strasse"] != null)
            {
                if (dt.Rows[i]["B_Strasse"] != DBNull.Value)
                {
                    liste.m_str_B_Strasse = Convert.ToString(dt.Rows[i]["B_Strasse"]);
                    liste.m_str_B_Strasse = liste.m_str_B_Strasse.Trim();
                }
            }
            if (dt.Columns["B_PLZ"] != null)
            {
                if (dt.Rows[i]["B_PLZ"] != DBNull.Value)
                {
                    liste.m_str_B_PLZ = Convert.ToString(dt.Rows[i]["B_PLZ"]);
                }
            }

            if (dt.Columns["B_Ort"] != null)
            {
                if (dt.Rows[i]["B_Ort"] != DBNull.Value)
                {
                    liste.m_str_B_Ort = Convert.ToString(dt.Rows[i]["B_Ort"]);
                    liste.m_str_B_Ort = m_str_B_Ort.Trim();
                }
            }
            if (dt.Columns["Entladestelle"] != null)
            {
                //Entladestelle
                if (dt.Rows[i]["Entladestelle"] != DBNull.Value)
                {
                    liste.m_str_Entladestelle = Convert.ToString(dt.Rows[i]["Entladestelle"]);
                    liste.m_str_Entladestelle = m_str_Entladestelle.Trim();
                }
            }
            if (dt.Columns["E_Strasse"] != null)
            {
                if (dt.Rows[i]["E_Strasse"] != DBNull.Value)
                {
                    liste.m_str_E_Strasse = Convert.ToString(dt.Rows[i]["E_Strasse"]);
                    liste.m_str_E_Strasse = m_str_E_Strasse.Trim();
                }
            }
            if (dt.Columns["E_PLZ"] != null)
            {
                if (dt.Rows[i]["E_PLZ"] != DBNull.Value)
                {
                    liste.m_str_E_PLZ = Convert.ToString(dt.Rows[i]["E_PLZ"]);
                }
            }
            if (dt.Columns["E_Ort"] != null)
            {
                if (dt.Rows[i]["E_Ort"] != DBNull.Value)
                {
                    liste.m_str_E_Ort = Convert.ToString(dt.Rows[i]["E_Ort"]);
                    liste.m_str_E_Ort = m_str_E_Ort.Trim();
                }
            }

            /******************************************************************
             *                   separarte Abfragen 
             * ****************************************************************/
            //Artikel Liste
            //Liste der Güter wird separart ermittel, da es mehrere sein können
            if ((liste.m_i_AuftragID > 0) & (liste.m_i_AuftragPos > -1))
            {
                //liste.m_str_Gut = clsArtikel.GetAllArtikelString(liste.m_i_AuftragID, liste.m_i_AuftragPos);
                //liste.m_dec_GesamtNetto = clsAuftragPos.GetGesamtGewichtFromAuftragPos(liste.m_i_AuftragID, true, GL_User.User_ID);
                //liste.m_dec_GesamtBrutto = clsAuftragPos.GetGesamtGewichtFromAuftragPos(liste.m_i_AuftragID, false, GL_User.User_ID);
            }
            else
            {
                liste.m_str_Gut = string.Empty;
                liste.m_dec_GesamtNetto = 0.0M;
                liste.m_dec_GesamtBrutto = 0.0M;
            }


            if (dt.Columns["Netto"] != null)
            {
                //separarte Abfrage Gewichte
                if (dt.Rows[i]["Netto"] != DBNull.Value)
                {
                    liste.m_dec_Netto = Convert.ToDecimal(dt.Rows[i]["Netto"]);
                }
            }
            if (dt.Columns["Brutto"] != null)
            {
                if (dt.Rows[i]["Brutto"] != DBNull.Value)
                {
                    liste.m_dec_Brutto = Convert.ToDecimal(dt.Rows[i]["Brutto"]);
                }
            }
            if (dt.Columns["Read"] != null)
            {
                if (dt.Rows[i]["Read"] != DBNull.Value)
                {
                    liste.m_bo_neuerAuftrag = (bool)dt.Rows[i]["Read"];
                }
            }
            if (dt.Columns["Prio"] != null)
            {
                if (dt.Rows[i]["Prio"] != DBNull.Value)
                {
                    liste.m_bo_Prioritaet = (bool)dt.Rows[i]["Prio"];
                }
            }
            if (dt.Columns["ZF"] != null)
            {
                if (dt.Rows[i]["ZF"] != DBNull.Value)
                {
                    liste.m_dt_ZF = (DateTime)dt.Rows[i]["ZF"];
                }
                else
                {
                    liste.m_dt_ZF = Convert.ToDateTime("01.01.1900 00:00");
                }
            }
            if (dt.Columns["faellig"] != null)
            {
                if (dt.Rows[i]["faellig"] != DBNull.Value)
                {
                    liste.m_dt_Liefertermin = (System.DateTime)dt.Rows[i]["faellig"];
                }
            }
            if (dt.Columns["VSB"] != null)
            {
                if (dt.Rows[i]["VSB"] != DBNull.Value)
                {
                    liste.m_dt_VSB = (System.DateTime)dt.Rows[i]["VSB"];
                }
            }
            if (dt.Columns["Ladenummer"] != null)
            {
                if (dt.Rows[i]["Ladenummer"] != DBNull.Value)
                {
                    liste.m_str_Ladenummer = (string)dt.Rows[i]["Ladenummer"];
                }
            }
            if (dt.Columns["Ber_A"] != null)
            {
                if (dt.Rows[i]["Ber_A"] is DBNull)
                {
                    liste.m_i_Ber_A = 2;
                }
                else
                {
                    liste.m_i_Ber_A = (Int32)dt.Rows[i]["Ber_A"];
                }
            }
            if (dt.Columns["Ber_E"] != null)
            {
                if (dt.Rows[i]["Ber_E"] is DBNull)
                {
                    liste.m_i_Ber_E = 2;
                }
                else
                {
                    liste.m_i_Ber_E = (Int32)dt.Rows[i]["Ber_E"];
                }
            }
            if (dt.Columns["Ber_V"] != null)
            {
                if (dt.Rows[i]["Ber_V"] is DBNull)
                {
                    liste.m_i_Ber_V = 2;
                }
                else
                {
                    liste.m_i_Ber_V = (Int32)dt.Rows[i]["Ber_V"];
                }
            }
            if (dt.Columns["WAv"] != null)
            {
                if (dt.Rows[i]["WAv"] is DBNull)
                {
                    liste.m_dt_WAvon = Convert.ToDateTime("01.01.1900 00:00");
                }
                else
                {
                    liste.m_dt_WAvon = (DateTime)dt.Rows[i]["WAv"];
                }
            }
            if (dt.Columns["WAb"] != null)
            {
                if (dt.Rows[i]["WAb"] is DBNull)
                {
                    liste.m_dt_WAbis = Convert.ToDateTime("01.01.1900 00:00");
                }
                else
                {
                    liste.m_dt_WAbis = (DateTime)dt.Rows[i]["WAb"];
                }
            }
            if (dt.Columns["Entladezeit"] != null)
            {
                if (dt.Rows[i]["Entladezeit"] is DBNull)
                {
                    liste.m_dt_E_Date = Convert.ToDateTime("01.01.1900 00:00");
                }
                else
                {
                    liste.m_dt_E_Date = (DateTime)dt.Rows[i]["Entladezeit"];
                }
            }
            if (dt.Columns["Beladezeit"] != null)
            {
                if (dt.Rows[i]["Beladezeit"] is DBNull)
                {
                    liste.m_dt_B_Date = Convert.ToDateTime("01.01.1900 00:00");
                }
                else
                {
                    liste.m_dt_B_Date = (DateTime)dt.Rows[i]["Beladezeit"];
                }
            }
            if (dt.Columns["vKW"] != null)
            {
                if (dt.Rows[i]["vKW"] is DBNull)
                {
                    liste.m_i_vKW = 0;
                }
                else
                {
                    liste.m_i_vKW = (Int32)dt.Rows[i]["vKW"];
                }
            }
            else
            {
                liste.m_i_vKW = 0;
            }
            if (dt.Columns["bKW"] != null)
            {
                if (dt.Rows[i]["bKW"] is DBNull)
                {
                    liste.m_i_bKW = 0;
                }
                else
                {
                    liste.m_i_bKW = (Int32)dt.Rows[i]["bKW"];
                }
            }
            else
            {
                liste.m_i_bKW = 0;
            }


            if (listenArt == 5)
            {
                if (dt.Columns["SU_ID"] != null)
                {
                    if (dt.Rows[i]["SU_ID"] is DBNull)
                    {
                        dt.Rows[i]["SU"] = string.Empty;
                    }
                    else
                    {
                        ADR_ID_SU = (decimal)dt.Rows[i]["SU_ID"];
                        dt.Rows[i]["SU"] = SU;
                    }
                }
            }

            if (listenArt == 7)
            {
                m_str_Resscource = string.Empty;
                if (dt.Columns["Ressource"] != null)
                {
                    if (dt.Rows[i]["Ressource"] is DBNull)
                    {
                        dt.Rows[i]["Ressource"] = string.Empty;
                    }
                    else
                    {
                        m_str_Resscource = dt.Rows[i]["Ressource"].ToString();
                    }
                }
            }

            // Abfrage Lieferscheine gedruckt !!!!!!!! 
            liste.m_bo_Papiere = clsLieferscheine.LieferscheinExist(liste.m_dec_ID);

            //Druck Lieferscheine / Rechnungen
            if (liste.m_bo_Papiere)
            {
                clsLieferscheine drLfs = new clsLieferscheine();
                drLfs.AP_ID = liste.m_dec_ID;
                liste.m_dt_drDatum = drLfs.GetLieferscheinDatum();
            }
            else
            {
                liste.m_dt_drDatum = DateTime.MaxValue;
            }
        }
        //
        //
        //              
        private bool _m_bo_okAdressdate;
        //
        //--- Beladestelle
        //
        public bool m_bo_okAdressdate
        {
            get
            {
                if ((m_str_Entladestelle == "") | (m_str_Beladestelle == ""))
                {
                    _m_bo_okAdressdate = false;
                    InfoWarning = InfoWarning + "- Be- / Entladestelle nicht bekannt \n";
                }
                else
                {
                    _m_bo_okAdressdate = true;
                }
                return _m_bo_okAdressdate;
            }
            set
            {
                _m_bo_okAdressdate = value;
            }
        }
        //
        //----- Berechtigungen
        //Einteilung von 1-3 (1 ja, 2 neutral, 3 nein)
        private bool _m_bo_okBerechtigung;

        public bool m_bo_okBerechtigung
        {
            get
            {
                _m_bo_okBerechtigung = true;
                if (m_i_Ber_E == 1)
                {
                    InfoWarning = InfoWarning + "- Berechtigung des Empfängers falsch \n";
                    _m_bo_okBerechtigung = false;
                }
                if (m_i_Ber_V == 1)
                {
                    InfoWarning = InfoWarning + "- Berechtigung des Versenders falsch \n";
                    _m_bo_okBerechtigung = false;
                }
                if (m_i_Ber_A == 1)
                {
                    InfoWarning = InfoWarning + "- Berechtigung des Auftraggebers falsch \n";
                    _m_bo_okBerechtigung = false;
                }
                return _m_bo_okBerechtigung;
            }
            set
            {
                _m_bo_okBerechtigung = value;
            }
        }
        //
        // Ausgabe Zeitfenster
        //
        private string _m_str_ZFOutput = string.Empty;
        public string m_str_ZFOutput
        {
            get
            {
                _m_str_ZFOutput = string.Empty;
                if (_m_dt_ZF.Hour < 10)
                {
                    _m_str_ZFOutput = _m_str_ZFOutput + "0" + _m_dt_ZF.Hour.ToString();
                }
                else
                {
                    _m_str_ZFOutput = _m_str_ZFOutput + _m_dt_ZF.Hour.ToString();
                }

                if (_m_dt_ZF.Minute < 10)
                {
                    _m_str_ZFOutput = _m_str_ZFOutput + ":0" + _m_dt_ZF.Minute.ToString();
                }
                else
                {
                    _m_str_ZFOutput = _m_str_ZFOutput + ":" + _m_dt_ZF.Minute.ToString();
                }
                return _m_str_ZFOutput;
            }
            set
            {

                _m_str_ZFOutput = value;
            }
        }
        //
        // Ausgabe WA Empfänger
        //
        private string _m_str_WAOutput = string.Empty;
        public string m_str_WAOutput
        {
            get
            {
                _m_str_ZFOutput = string.Empty;
                if (_m_dt_WAvon.Hour < 10)
                {
                    _m_str_ZFOutput = _m_str_ZFOutput + "0" + _m_dt_WAvon.Hour.ToString();
                }
                else
                {
                    _m_str_ZFOutput = _m_str_ZFOutput + _m_dt_WAvon.Hour.ToString();
                }

                if (_m_dt_WAvon.Minute < 10)
                {
                    _m_str_ZFOutput = _m_str_ZFOutput + ":0" + _m_dt_WAvon.Minute.ToString();
                }
                else
                {
                    _m_str_ZFOutput = _m_str_ZFOutput + ":" + _m_dt_WAvon.Minute.ToString();
                }
                return _m_str_ZFOutput;
            }
            set
            {
                _m_str_WAOutput = value;
            }
        }
        //
        //---  Cellformatierung für die Dringlichkeit des Transportes
        //   1 : Liefertermin in einem Werktag
        //   2 : Lieferterimn innerhalb 2 Werktage
        //   3 : Liefertermin weiter entfernt
        //
        private Int32 _m_i_Dringlichkeit;
        public Int32 m_i_Dringlichkeit
        {
            get
            {
                //Beschreibung: Dringlichkeitsstufen 1-3
                // 1: Liefertermin innerhalb 2 W-Tage
                // 2: Liefertermin in 2 W-Tagen
                // 3: Liefertermin größer 2 Tage entfernt
                //Sonderbedingen Do/Fr - das Wochenende darf nicht berücksichtigt werden 
                if (m_dt_Liefertermin.Date == DateTime.Today.Date)
                {
                    _m_i_Dringlichkeit = 1;
                    _m_bo_Prioritaet = true;
                }
                if (m_dt_Liefertermin.Date > DateTime.Today.Date)
                {

                    if (DateTime.Today.Date.AddDays(2).Date >= m_dt_Liefertermin.Date)
                    {
                        if (DateTime.Today.Date.AddDays(2) == m_dt_Liefertermin.Date)
                        {
                            _m_i_Dringlichkeit = 2;
                        }
                        else
                        {
                            _m_i_Dringlichkeit = 1;
                            //_m_bo_Prioritaet = true;
                        }
                    }
                    if (DateTime.Today.Date.AddDays(3) <= m_dt_Liefertermin.Date)
                    {
                        m_i_Dringlichkeit = 3;
                    }
                    if ((DateTime.Today.Day.ToString() == "Donnerstag") & (m_dt_Liefertermin.Day.ToString() == "Montag"))
                    {
                        _m_i_Dringlichkeit = 1;
                        //_m_bo_Prioritaet = true;
                    }
                    if ((DateTime.Today.Day.ToString() == "Donnerstag") & (m_dt_Liefertermin.Day.ToString() == "Dienstag"))
                    {
                        _m_i_Dringlichkeit = 2;
                    }
                    if (((DateTime.Today.Day.ToString() == "Freitag") & (m_dt_Liefertermin.Day.ToString() == "Montag")) |
                        ((DateTime.Today.Day.ToString() == "Freitag") & (m_dt_Liefertermin.Day.ToString() != "Dienstag")))
                    {
                        _m_i_Dringlichkeit = 1;
                        //_m_bo_Prioritaet = true;
                    }
                }
                else
                {
                    _m_i_Dringlichkeit = 1;
                    _m_bo_Prioritaet = true;
                }
                return _m_i_Dringlichkeit;
            }
            set
            {
                _m_i_Dringlichkeit = value;
            }
        }
        //
        //-------------- Ladenummer Pflichtfeld? ----------------
        //
        private bool GetLadeNrRequire()
        {
            bool IsRequire = false;
            string strSQL = string.Empty;

            strSQL = "SELECT LadeNrRequire FROM AuftragPos " +
                                           "WHERE Auftrag_ID='" + m_i_AuftragID + "' AND AuftragPos='" + m_i_AuftragPos + "'";

            IsRequire = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, BenutzerID);
            return IsRequire;
        }
    }
}
