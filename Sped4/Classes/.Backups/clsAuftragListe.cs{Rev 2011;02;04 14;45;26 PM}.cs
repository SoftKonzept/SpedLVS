using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Sped4.Classes
{
    class clsAuftragListe
    {
      // Verarbeitung / Prüfung der Daten, wie die Auftragsdaten in der Auftragsliste angezeigt werden

        private Int32 _m_i_ID;
        private DateTime _m_dt_Liefertermin = default (DateTime);
        private DateTime _m_dt_WAvon=default(DateTime);         // Warenannahme von
        private DateTime _m_dt_WAbis=default(DateTime);         // Warenannhame bis
        private DateTime _m_dt_monitoringPeriode = default(DateTime);
        private DateTime _m_dt_VSB = default(DateTime);
        private DateTime _m_dt_ZF = default(DateTime);

        private string _m_str_Beladestelle = string.Empty;
        private string _m_str_B_Strasse = string.Empty; 
        private string _m_str_B_PLZ=string.Empty;
        private string _m_str_B_Ort = string.Empty;
        private DateTime _m_dt_B_Date;

        private string _m_str_Entladestelle = string.Empty;
        private string _m_str_E_Strasse = string.Empty;
        private string _m_str_E_PLZ=string.Empty;
        private string _m_str_E_Ort = string.Empty;
        private DateTime _m_dt_E_Date;

        private Int32 _m_i_AuftragID;
        private Int32 _m_i_AuftragPos;
        private string _m_str_Gut = string.Empty;
        private Int32 _m_i_Status;

        private decimal _m_dec_Gewicht=0.00m;
        private decimal _m_dec_gemPosGewicht = 0.00m;
        private decimal _m_dec_tatPosGewicht = 0.00m;
        private decimal _m_dec_gemGesamtGewicht = 0.00m;
        private decimal _m_dec_tatGesamtGewicht = 0.00m;
        private decimal _m_dec_VerlGewicht;               //erstmal drin lassen 20.07.2010
        private decimal _m_dec_Restgewicht;               // erstmal drin lassen 20.07.2010

        private Int32 _m_i_KD_ID;

        private Int32 _m_i_Ber_A;
        private Int32 _m_i_Ber_V;
        private Int32 _m_i_Ber_E;
        private bool _m_bo_Papiere;
        private bool _m_bo_Fahrer;
        private bool _m_bo_neuerAuftrag;
        private bool _m_bo_Prioritaet;
        private bool _m_bo_disponieren;
        private string _m_str_Ladenummer = string.Empty;
        private bool _m_bo_LadeNrRequire;
        private DateTime _m_dt_drDatum;
        private string _InfoWarning = string.Empty;
        private Int32 _ADR_ID_SU;
        private string _SU;


        public Int32 m_i_ID
        {
          get { return _m_i_ID; }
          set { _m_i_ID  = value; }
        }
        public DateTime m_dt_Liefertermin
        {
            get { return _m_dt_Liefertermin; }
            set { _m_dt_Liefertermin = value; }
        }
        public DateTime m_dt_WAvon
        {
            get { return _m_dt_WAvon;  }
            set { _m_dt_WAvon = value; }
        }
        public DateTime m_dt_WAbis
        {
            get { return _m_dt_WAbis; }
            set { _m_dt_WAbis = value; }
        }
        public DateTime m_dt_monitoringPeriode
        {
            get { return _m_dt_monitoringPeriode; }
            set { _m_dt_monitoringPeriode = value; }
        }
        public DateTime m_dt_VSB
        {
            get { return _m_dt_VSB;  }
            set { _m_dt_VSB = value; }
        }
        public DateTime m_dt_ZF
        {
            get { return _m_dt_ZF; }
            set { _m_dt_ZF = value; }
        }
        public DateTime m_dt_drDatum
        {
            get { return _m_dt_drDatum; }
            set { _m_dt_drDatum = value; }
        }
        public DateTime m_dt_B_Date
        {
          get { return _m_dt_B_Date; }
          set { _m_dt_B_Date = value; }
        }
        public DateTime m_dt_E_Date
        {
          get { return _m_dt_E_Date; }
          set { _m_dt_E_Date = value; }
        }
        public string m_str_Beladestelle
        {
            get { return _m_str_Beladestelle; }
            set {  _m_str_Beladestelle = value; }
        }
        public string m_str_Entladestelle
        {
            get { return _m_str_Entladestelle; }
            set { _m_str_Entladestelle = value; }
        }
        public string m_str_E_Strasse
        {
            get { return _m_str_E_Strasse; }
            set { _m_str_E_Strasse = value; }
        }
        public string m_str_B_Strasse
        {
            get { return _m_str_B_Strasse; }
            set { _m_str_B_Strasse = value; }
        }
        public string m_str_E_PLZ
        {
            get { return _m_str_E_PLZ; }
            set { _m_str_E_PLZ = value; }
        }
        public string m_str_E_Ort
        {
            get { return _m_str_E_Ort; }
            set { _m_str_E_Ort = value; }
        }
        public string m_str_B_PLZ
        {
            get { return _m_str_B_PLZ; }
            set { _m_str_B_PLZ = value; }
        }
        public string m_str_B_Ort
        {
            get { return _m_str_B_Ort; }
            set { _m_str_B_Ort = value; }
        }
        public Int32 m_i_AuftragID
        {
            get { return _m_i_AuftragID; }
            set { _m_i_AuftragID = value; }
        }
        public Int32 m_i_AuftragPos
        {
            get { return _m_i_AuftragPos; }
            set { _m_i_AuftragPos = value; }
        }
        public string m_str_Gut
        {
            get { return _m_str_Gut; }
            set { _m_str_Gut = value; }
        }
        public Int32 m_i_Status
        {
            get 
            {               
                return _m_i_Status; 
            }
            set { _m_i_Status = value; }
        }
        public decimal m_dec_Gewicht
        {
            get { return _m_dec_Gewicht; }
            set { _m_dec_Gewicht = value; }
        }
        public decimal m_dec_gemGesamtGewicht
        {
          get { return _m_dec_gemGesamtGewicht; }
          set { _m_dec_gemGesamtGewicht = value; }
        }
        public decimal m_dec_tatGesamtGewicht
        {
          get { return _m_dec_tatGesamtGewicht; }
          set { _m_dec_tatGesamtGewicht = value; }
        }
        public decimal m_dec_gemPosGewicht
        {
          get { return _m_dec_gemPosGewicht; }
          set { _m_dec_gemPosGewicht = value; }
        }
        public decimal m_dec_tatPosGewicht
        {
          get { return _m_dec_tatPosGewicht; }
          set { _m_dec_tatPosGewicht = value; }
        }

        public decimal m_dec_VerlGewicht
        {
          get { return _m_dec_VerlGewicht; }
          set { _m_dec_VerlGewicht = value; }
        }

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

        public Int32 m_i_KD_ID
        {
            get { return _m_i_KD_ID; }
            set { _m_i_KD_ID = value; }
        }
        public Int32 m_i_Ber_A
        {
            get { return _m_i_Ber_A; }
            set { _m_i_Ber_A = value; }
        }
        public Int32 m_i_Ber_V
        {
            get { return _m_i_Ber_V; }
            set { _m_i_Ber_V = value; }
        }
        public Int32 m_i_Ber_E
        {
            get { return _m_i_Ber_E; }
            set { _m_i_Ber_E = value; }
        }
        public bool m_bo_Papiere
        {
            get { return _m_bo_Papiere; }
            set { _m_bo_Papiere = value; }
        }
        public bool m_bo_Faherer
        {
            get { return _m_bo_Fahrer; }
            set { _m_bo_Fahrer = value; }
        }
        public bool m_bo_disponieren
        {
            get { return _m_bo_disponieren; }
            set { _m_bo_disponieren = value; }
        }
        public bool m_bo_neuerAuftrag
        {
            get { return _m_bo_neuerAuftrag; }
            set { _m_bo_neuerAuftrag = value; }
        }
        public bool m_bo_Prioritaet
        {
            get { return _m_bo_Prioritaet; }
            set { _m_bo_Prioritaet = value; }
        }
        public bool m_bo_LadeNrRequire
        {
            get 
            {
              if (m_i_AuftragID > 0)
              {
                _m_bo_LadeNrRequire=GetLadeNrRequire();
              }
              return _m_bo_LadeNrRequire; }
            set { _m_bo_LadeNrRequire = value; }
        }
        public string m_str_Ladenummer
        {
            get { return _m_str_Ladenummer; }
            set { _m_str_Ladenummer = value; }
        }
        public string InfoWarning
        {
            get { return _InfoWarning; }
            set { _InfoWarning = value; }
        }

        public Int32 ADR_ID_SU
        {
          get { return _ADR_ID_SU; }
          set { _ADR_ID_SU = value; }
        }
        public string SU
        {
          get
          {
            _SU = clsADR.ReadViewIDbyID(ADR_ID_SU);
            return _SU;
          }
          set { _SU = value; }
        }
        //------------------------------------------ Checks   -------------------------------------------------------------------------
        //                                        Auftragsliste 
        //
        //----------------------------------------------------------------------------------------------------------------------------
        //
        //
        //
        public DataTable SetAuftragsListe(DataTable dataTable, Int32 listenArt)
        {
            for (int i = 0; i <= dataTable.Rows.Count - 1; i++)
            {
                if (dataTable.Rows[i]["ID"] != DBNull.Value)
                {
                  m_i_ID = Convert.ToInt32(dataTable.Rows[i]["ID"]);
                }
                if (dataTable.Rows[i]["AuftragID"] != DBNull.Value)
                {
                  m_i_AuftragID = Convert.ToInt32(dataTable.Rows[i]["AuftragID"]);
                }
                if (dataTable.Rows[i]["Beladestelle"] != DBNull.Value)
                {
                  m_str_Beladestelle = Convert.ToString(dataTable.Rows[i]["Beladestelle"]);
                }
                if (dataTable.Rows[i]["B_Strasse"] != DBNull.Value)
                {
                  m_str_B_Strasse = Convert.ToString(dataTable.Rows[i]["B_Strasse"]);
                }
                if (dataTable.Rows[i]["B_PLZ"] != DBNull.Value)
                {
                  m_str_B_PLZ = Convert.ToString(dataTable.Rows[i]["B_PLZ"]);
                }
                if (dataTable.Rows[i]["B_Ort"] != DBNull.Value)
                {
                  m_str_B_Ort = Convert.ToString(dataTable.Rows[i]["B_Ort"]);
                }
                if (dataTable.Rows[i]["Entladestelle"] != DBNull.Value)
                {
                  m_str_Entladestelle = Convert.ToString(dataTable.Rows[i]["Entladestelle"]);
                }
                if (dataTable.Rows[i]["E_Strasse"] != DBNull.Value)
                {
                  m_str_E_Strasse = Convert.ToString(dataTable.Rows[i]["E_Strasse"]);
                }
                if (dataTable.Rows[i]["E_PLZ"] != DBNull.Value)
                {
                  m_str_E_PLZ= Convert.ToString(dataTable.Rows[i]["E_PLZ"]);
                }
                if (dataTable.Rows[i]["E_Ort"] != DBNull.Value)
                {
                  m_str_E_Ort = Convert.ToString(dataTable.Rows[i]["E_Ort"]);
                }
                if (dataTable.Rows[i]["Gut"] is DBNull)
                {
                    m_str_Gut = string.Empty;
                }
                else
                {
                    m_str_Gut = (string)dataTable.Rows[i]["Gut"];
                }
                if (dataTable.Rows[i]["gemGesamtGewicht"] != DBNull.Value)
                {
                    m_dec_gemGesamtGewicht = Convert.ToDecimal(dataTable.Rows[i]["gemGesamtGewicht"]);
                }
                if (dataTable.Rows[i]["tatGesamtGewicht"] != DBNull.Value)
                {
                    m_dec_tatGesamtGewicht = Convert.ToDecimal(dataTable.Rows[i]["tatGesamtGewicht"]);
                }
                if (dataTable.Rows[i]["gemPosGewicht"] != DBNull.Value)
                {
                    m_dec_gemPosGewicht = Convert.ToDecimal(dataTable.Rows[i]["gemPosGewicht"]);
                }
                if (dataTable.Rows[i]["tatPosGewicht"] != DBNull.Value)
                {
                    m_dec_tatPosGewicht = Convert.ToDecimal(dataTable.Rows[i]["tatPosGewicht"]);
                }
                if (Convert.ToChar(dataTable.Rows[i]["Pri"]) == 'T')
                {
                    m_bo_Prioritaet = true;
                }
                else
                {
                    m_bo_Prioritaet = false;
                }
                m_i_Status = (Int32)dataTable.Rows[i]["Stat"];
                m_dt_ZF = (DateTime)dataTable.Rows[i]["ZF"];

                //if (Convert.ToChar(dataTable.Rows[i]["Papiere"]) == 'T')
                if(Convert.ToInt32(dataTable.Rows[i]["Papiere"]) ==1)
                {
                    m_bo_Papiere = true;
                }
                else
                {
                    m_bo_Papiere = false;
                }
                if (Convert.ToChar(dataTable.Rows[i]["Fahrer"]) == 'T')
                {
                    m_bo_Faherer = true;
                }
                else
                {
                    m_bo_Faherer = false;
                }
                m_i_AuftragPos = (Int32)dataTable.Rows[i]["Pos"];
                m_dt_Liefertermin = (System.DateTime)dataTable.Rows[i]["faellig"];
                m_dt_VSB = (System.DateTime)dataTable.Rows[i]["VSB"];
                if (Convert.ToChar(dataTable.Rows[i]["New"]) == 'T')
                {
                    m_bo_neuerAuftrag = true;
                }
                else
                {
                    m_bo_neuerAuftrag = false;
                }

                if (dataTable.Rows[i]["Ber_A"] is DBNull)
                {
                  m_i_Ber_A = 2;
                }
                else 
                {
                  m_i_Ber_A = (Int32)dataTable.Rows[i]["Ber_A"];
                }

                if (dataTable.Rows[i]["Ber_E"] is DBNull)
                {
                  m_i_Ber_E = 2;
                }
                else
                {
                  m_i_Ber_E = (Int32)dataTable.Rows[i]["Ber_E"];
                }

                if (dataTable.Rows[i]["Ber_V"] is DBNull)
                {
                  m_i_Ber_V =2;
                }
                else
                {
                  m_i_Ber_V = (Int32)dataTable.Rows[i]["Ber_V"];
                }
                
                

                if (dataTable.Rows[i]["WAv"] is DBNull)
                {
                  m_dt_WAvon = Convert.ToDateTime("01.01.1900 00:00");
                }
                else
                { 
                  m_dt_WAvon = (DateTime)dataTable.Rows[i]["WAv"];
                }
                if (dataTable.Rows[i]["WAb"] is DBNull)
                {
                  m_dt_WAbis = Convert.ToDateTime("01.01.1900 00:00");
                }
                else
                {
                  m_dt_WAbis = (DateTime)dataTable.Rows[i]["WAb"];
                }



                m_str_Ladenummer = (string)dataTable.Rows[i]["Ladenummer"];

                if (dataTable.Rows[i]["VerlGewicht"].ToString() != "")
                {
                    m_dec_VerlGewicht = (decimal)dataTable.Rows[i]["VerlGewicht"];
                }

                //Name SU
                if (listenArt == 5)
                {
                    ADR_ID_SU = (Int32)dataTable.Rows[i]["SU_ID"];
                    dataTable.Rows[i]["SU"] = SU;
                }

                //disponierbar
                if (m_i_Status == 1) //unvollständig
                {
                    dataTable.Rows[i]["dispo"] = "true";
                }
                else
                {
                    dataTable.Rows[i]["dispo"] = "false";
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
                dataTable.Rows[i]["FAngaben"] = strAusgabe;

            }

            return dataTable;
        }
              
              
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
                if(m_i_Ber_E==1)
                {
                    InfoWarning=InfoWarning+ "- Berechtigung des Empfängers falsch \n";
                    _m_bo_okBerechtigung = false;
                }
                if(m_i_Ber_V==1)
                {
                    InfoWarning=InfoWarning+ "- Berechtigung des Versenders falsch \n";
                    _m_bo_okBerechtigung = false;                
                }
                if(m_i_Ber_A==1)
                {
                    InfoWarning=InfoWarning+ "- Berechtigung des Auftraggebers falsch \n";
                    _m_bo_okBerechtigung = false;                
                }             
                return _m_bo_okBerechtigung;
            }
            set 
            { 
                _m_bo_okBerechtigung=value;
            }
        }
        //
        // Ausgabe Zeitfenster
        //
        private string _m_str_ZFOutput= string.Empty;
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
        private int _m_i_Dringlichkeit;
        public int m_i_Dringlichkeit
        {
            get 
            { 
                //Beschreibung: Dringlichkeitsstufen 1-3
                // 1: Liefertermin innerhalb 2 W-Tage
                // 2: Liefertermin in 2 W-Tagen
                // 3: Liefertermin größer 2 Tage entfernt
                //Sonderbedingen Do/Fr - das Wochenende darf nicht berücksichtigt werden 
                if(m_dt_Liefertermin.Date==DateTime.Today.Date)
                {
                    _m_i_Dringlichkeit = 1;
                    _m_bo_Prioritaet = true;
                }
                if (m_dt_Liefertermin.Date > DateTime.Today.Date)
                {

                    if (DateTime.Today.Date.AddDays(2).Date>=m_dt_Liefertermin.Date)
                    {
                        if (DateTime.Today.Date.AddDays(2) == m_dt_Liefertermin.Date)
                        {
                            _m_i_Dringlichkeit = 2;
                        }
                        else
                        { 
                            _m_i_Dringlichkeit = 1;
                            _m_bo_Prioritaet = true;
                        }
                    }
                    if (DateTime.Today.Date.AddDays(3) <= m_dt_Liefertermin.Date)
                    {
                        m_i_Dringlichkeit = 3;
                    }
                    if ((DateTime.Today.Day.ToString() == "Donnerstag") & (m_dt_Liefertermin.Day.ToString() == "Montag"))
                    {
                        _m_i_Dringlichkeit = 1;
                        _m_bo_Prioritaet = true;
                    }
                    if ((DateTime.Today.Day.ToString() == "Donnerstag") & (m_dt_Liefertermin.Day.ToString() == "Dienstag"))
                    {
                        _m_i_Dringlichkeit = 2;
                    }
                    if (((DateTime.Today.Day.ToString() == "Freitag") & (m_dt_Liefertermin.Day.ToString() == "Montag")) |
                        ((DateTime.Today.Day.ToString() == "Freitag") & (m_dt_Liefertermin.Day.ToString() != "Dienstag")))
                    {
                        _m_i_Dringlichkeit = 1;
                        _m_bo_Prioritaet = true;
                    }
                }
                return _m_i_Dringlichkeit; 
            }
            set 
            {
                _m_i_Dringlichkeit=value;
            }
        }
        //
        //-------------- Ladenummer Pflichtfeld? ----------------
        //
        private bool GetLadeNrRequire()
        {
          bool IsRequire = false;
          SqlDataAdapter ada = new SqlDataAdapter();
          SqlCommand Command = new SqlCommand();
          Command.Connection = Globals.SQLcon.Connection;
          ada.SelectCommand = Command;
          Command.CommandText = "SELECT LadeNrRequire FROM AuftragPos " +
                                         "WHERE Auftrag_ID='" + m_i_AuftragID + "' AND AuftragPos='" + m_i_AuftragPos + "'";

          Globals.SQLcon.Open();
          object obj = Command.ExecuteScalar();
          if (obj == null)
          {
            IsRequire = false;
          }
          else
          {
            IsRequire =(bool)obj;
          }
          Command.Dispose();
          Globals.SQLcon.Close();
          return IsRequire;
        }
    }
}
