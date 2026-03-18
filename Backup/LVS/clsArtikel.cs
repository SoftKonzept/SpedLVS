using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace LVS
{
    public class clsArtikel
    {
        public const string Artikel_ValSeparator = "/";

        public const string ArtikelField_ID = "Artikel.ID";
        public const string ArtikelField_LVSID = "Artikel.LVS_ID";
        public const string ArtikelField_Netto = "Artikel.Netto";
        public const string ArtikelField_Brutto = "Artikel.Brutto";
        public const string ArtikelField_Tara = "Artikel.Tara";
        public const string ArtikelField_Gut = "Artikel.Gut";
        public const string ArtikelField_Dicke = "Artikel.Dicke";
        public const string ArtikelField_Breite = "Artikel.Breite";
        public const string ArtikelField_Länge = "Artikel.Laenge";
        public const string ArtikelField_Höhe = "Artikel.Hoehe";
        public const string ArtikelField_Abmessungen = "Artikel.Abmessungen";

        public const string ArtikelField_Anzahl = "Artikel.Anzahl";
        public const string ArtikelField_Einheit = "Artikel.Einheit";
        public const string ArtikelField_Werksnummer = "Artikel.Werksnummer";
        public const string ArtikelField_Produktionsnummer = "Artikel.Produktionsnummer";
        public const string ArtikelField_ProduktionsnummerASN = "Artikel.ASNProduktionsnummer";
        public const string ArtikelField_exBezeichnung = "Artikel.exBezeichnung";
        public const string ArtikelField_Charge = "Artikel.Charge";
        public const string ArtikelField_Bestellnummer = "Artikel.Bestellnummer";
        public const string ArtikelField_exMaterialnummer = "Artikel.exMaterialnummer";
        public const string ArtikelField_Position = "Artikel.Position";
        public const string ArtikelField_Güte = "Artikel.Guete";
        public const string ArtikelField_exAuftrag = "Artikel.exAuftrag";
        public const string ArtikelField_exAuftragPos = "Artikel.exAuftragPos";
        public const string ArtikelField_ArtikelIDRef = "Artikel.ArtIDRef";
        public const string ArtikelField_LVSNrBeforeUB = "Artikel.LVSNrBeforeUB";
        //public const string ArtikelField_LVSNrAfterUB = "Artikel.LVSNrAfterUB";
        public const string ArtikelField_AbrufRef = "Artikel.AbrufRef";
        public const string ArtikelField_TARef = "Artikel.TARef";
        public const string ArtikelField_GlowDate = "Artikel.GlowDate";
        public const string ArtikelField_TransportRef = "Artikel.TransportRef";


        public const string ArtikelField_Werk = "Artikel.Werk";
        public const string ArtikelField_Halle = "Artikel.Halle";
        public const string ArtikelField_Reihe = "Artikel.Reihe";
        public const string ArtikelField_Ebene = "Artikel.Ebene";
        public const string ArtikelField_Platz = "Artikel.Platz";

        public const string ArtikelFunction_ArtikelPosition = "#ArtikelPosition#";

        private clsSystem _sys;
        public clsSystem sys
        {
            get { return _sys; }
            set
            {
                _sys = value;
                if ((this._sys != null) && (_sys.AbBereich is clsArbeitsbereiche))
                {
                    this.AbBereichID = _sys.AbBereich.ID;
                    this.MandantenID = _sys.AbBereich.MandantenID;
                }
            }
        }
        public Globals._GL_SYSTEM _GL_System;
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
        //public clsGut GArt = new clsGut();
        private clsGut _GArt;
        public clsGut GArt
        {
            get
            {
                _GArt = new clsGut();
                _GArt.InitClass(this._GL_User, this._GL_System);
                if (this.GArtID > 0)
                {
                    _GArt.ID = this.GArtID;
                    _GArt.Fill();
                }
                return _GArt;
            }
            set
            {
                _GArt = value;
            }
        }
        public clsExtraChargeAssignment ExtraChargeAssignment;

        public decimal SelectedSchadenID = 0;
        public DataTable artTable = new DataTable();
        private clsLAusgang _Ausgang;
        public clsLAusgang Ausgang
        {
            get
            {
                if (this._Ausgang == null)
                {
                    this._Ausgang = new clsLAusgang();
                    this._Ausgang.InitDefaultClsAusgang(this._GL_User, this.sys);
                }
                if (_Ausgang.LAusgangTableID != this.LAusgangTableID)
                {
                    _Ausgang.LAusgangTableID = this.LAusgangTableID;
                    _Ausgang.FillAusgang();
                }
                this.IsRL = _Ausgang.IsRL;
                return _Ausgang;
            }
            set
            {
                _Ausgang = value;
            }
        }
        private clsLEingang _Eingang;
        public clsLEingang Eingang
        {
            get
            {
                if (this._Eingang == null)
                {
                    this._Eingang = new clsLEingang();
                    this._Eingang.InitDefaultClsEingang(this._GL_User, this.sys);
                }
                if (
                        (this.LEingangTableID > 0) &&
                        (_Eingang.LEingangTableID != this.LEingangTableID)
                   )
                {
                    _Eingang.LEingangTableID = this.LEingangTableID;
                    _Eingang.FillEingang();
                }
                return _Eingang;
            }
            set
            {
                _Eingang = value;
            }
        }
        public clsLagerMeldungen _Lagermeldungen;
        public clsLagerMeldungen Lagermeldungen
        {
            get
            {
                if (this._Lagermeldungen == null)
                {
                    this._Lagermeldungen = new clsLagerMeldungen();
                }
                return this._Lagermeldungen;
            }
            set
            {
                this._Lagermeldungen = value;
            }
        }

        private clsASNCall _Call;
        public clsASNCall Call
        {
            get
            {
                return _Call;
            }
            set
            {
                _Call = value;
            }
        }
        public clsSPL SPL;


        //Artikelzusammenführung

        public decimal IDforUnion { get; set; } = 0;
        public decimal IDforDelete { get; set; } = 0;
        public Int32 MEforUnion { get; set; } = 0;
        public decimal BruttoForUnion { get; set; } = 0;
        public decimal NettoForUnion { get; set; } = 0;
        public decimal gemGewichtforUnion { get; set; } = 0;

        public decimal ID { get; set; } = 0;

        private decimal artIdAlt = 0;
        public decimal ArtIDAlt
        {
            get
            {
                return artIdAlt;
            }
            set
            {
                artIdAlt = value;
            }
        }
        public decimal AuftragID { get; set; } = 0;
        public decimal AuftragPos { get; set; } = 0;
        private string _Gut = string.Empty;
        public string Gut
        {
            get
            {
                GArt.BenutzerID = this.BenutzerID;
                GArt.ID = GArtID;
                if (clsGut.ExistGArtByID(this._GL_User, GArtID))
                {
                    GArt.Fill();
                    Gut = GArt.Bezeichnung;
                }
                else
                {
                    _Gut = string.Empty;
                }
                return _Gut;
            }
            set { _Gut = value; }
        }
        public decimal GArtID { get; set; } = 0;
        public string GutZusatz { get; set; } = string.Empty;
        public decimal LVS_ID { get; set; } = 0;
        public string Werksnummer { get; set; } = string.Empty;
        public string Produktionsnummer { get; set; } = string.Empty;
        public string Einheit { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string exBezeichnung { get; set; } = string.Empty;
        public bool AusgangChecked { get; set; } = false;
        public string Charge { get; set; } = string.Empty;
        private string _Bestellnummer = string.Empty;
        public string Bestellnummer
        {
            get
            {
                if (_Bestellnummer == null)
                {
                    _Bestellnummer = string.Empty;
                }
                return _Bestellnummer;
            }
            set { _Bestellnummer = value; }
        }
        public string exMaterialnummer { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public decimal AuftragPosTableID { get; set; } = 0;
        public string AbrufReferenz { get; set; } = string.Empty;
        public string TARef { get; set; } = string.Empty;
        public Int32 BKZ { get; set; } = 0;
        public bool Umbuchung { get; set; } = false;
        public decimal LVSNrVorUB { get; set; } = 0;
        public decimal LVSNrNachUB { get; set; } = 0;
        public bool bSPL { get; set; } = false;
        public bool bSPLartCert { get; set; } = false;
        public bool IsRL { get; set; } = false;
        public bool bSchaden { get; set; } = false;
        public bool IsLagerArtikel { get; set; } = true;
        public string EAAusgangAltLVS { get; set; } = string.Empty;
        public string EAEingangAltLVS { get; set; } = string.Empty;
        public decimal ADRLagerNr { get; set; } = 0;
        public bool FreigabeAbruf { get; set; } = false;
        public DateTime LZZ { get; set; } = new DateTime(1900, 1, 1);
        public bool IsMulde { get; set; } = false;
        public bool IsLabelPrint { get; set; } = false;
        public bool IsProblem { get; set; } = false;
        public bool IsKorStVerUse { get; set; } = false;
        public bool IgnLM { get; set; } = false;         // Lagermeldung erstellen?
        public string Abladestelle { get; set; } = string.Empty; // kurzinfo für die Entladestelle bsp Tor 3 uws.   

        //*******************************************************

        //Abmessungen / Gewichte

        public decimal Dicke { get; set; } = 0;
        public decimal Breite { get; set; } = 0;
        public decimal Laenge { get; set; } = 0;
        public decimal Hoehe { get; set; } = 0;
        public Int32 Anzahl { get; set; } = 0;
        public decimal gemGewicht { get; set; } = 0;
        public decimal Netto { get; set; } = 0;
        public decimal Brutto { get; set; } = 0;

        //**********************************************

        //Lager
        public decimal LEingangTableID { get; set; } = 0;
        public decimal LAusgangTableID { get; set; } = 0;
        public decimal MandantenID { get; set; } = 0;
        public decimal AbBereichID { get; set; } = 0;
        public string ArtIDRef { get; set; } = string.Empty;
        public bool EingangChecked { get; set; } = false;

        //*************************************

        //Lagerort
        public decimal LagerOrt { get; set; } = 0;
        public string exLagerOrt { get; set; } = string.Empty;
        public string LagerOrtTable { get; set; } = string.Empty;
        public string Werk { get; set; } = string.Empty;
        public string Halle { get; set; } = string.Empty;
        public string Reihe { get; set; } = string.Empty;
        public string Ebene { get; set; } = string.Empty;
        public string Platz { get; set; } = string.Empty;

        public string exAuftrag { get; set; } = string.Empty;
        public string exAuftragPos { get; set; } = string.Empty;
        public string exLsNoA { get; set; } = string.Empty;
        public string exLsPosA { get; set; } = string.Empty;
        public string ASNVerbraucher { get; set; } = string.Empty;
        public bool UB_AltCalcEinlagerung { get; set; } = false;
        public bool UB_AltCalcAuslagerung { get; set; } = false;
        public bool UB_AltCalcLagergeld { get; set; } = false;
        public bool UB_NeuCalcEinlagerung { get; set; } = false;
        public bool UB_NeuCalcAuslagerung { get; set; } = false;
        public bool UB_NeuCalcLagergeld { get; set; } = false;
        public bool IsVerpackt { get; set; } = false;
        public string interneInfo { get; set; } = string.Empty;
        public string externeInfo { get; set; } = string.Empty;
        public string Guete { get; set; } = string.Empty;
        public bool IsStackable { get; set; } = false;
        public bool IsEMECreate { get; set; } = false;  // prüft in Vita ob die EME oder EML erstellt wurde => Artikel 
        public bool IsEMLCreate { get; set; } = false;  // prüft in Vita ob die EME oder EML erstellt wurde => Artikel 

        private string _ASNProduktionsnummer = string.Empty;   //beinhaltet den Wert, der für die Produktionsnummer bei letzten EM verwendet wurde
        public string ASNProduktionsnummer
        {
            get
            {
                if (_ASNProduktionsnummer.Equals(string.Empty))
                {
                    _ASNProduktionsnummer = this.Produktionsnummer;
                }
                return _ASNProduktionsnummer;
            }
            set { _ASNProduktionsnummer = value; }
        }

        public decimal LVSNrBeforeUB { get; set; } = 0;
        public decimal LVSNrAfterUB { get; set; } = 0;
        public decimal ArtIDAfterUB { get; set; } = 0;
        public bool isUBNew
        {
            get
            {
                return clsUmbuchung.isArtikelUBNew(this.ID, this.BenutzerID);
            }
        }
        public bool isUBAlt
        {
            get
            {
                return clsUmbuchung.isArtikelUBAlt(this.ID, this.BenutzerID);
            }
        }
        public bool isUBNew_CalcEinlagerung
        {
            get
            {
                return clsUmbuchung.isArtikelUBNew(this.ID, this.BenutzerID, "Einlagerung");
            }
        }
        public bool isUBNew_CalcAuslagerung
        {
            get
            {
                return clsUmbuchung.isArtikelUBNew(this.ID, this.BenutzerID, "Auslagerung");
            }
        }
        public bool isUBNew_CalcLagergeld
        {
            get
            {
                return clsUmbuchung.isArtikelUBNew(this.ID, this.BenutzerID, "Lagergeld");
            }
        }

        public bool isUBAlt_CalcEinlagerung
        {
            get
            {
                return clsUmbuchung.isArtikelUBAlt(this.ID, this.BenutzerID, "Einlagerung");
            }
        }
        public bool isUBAlt_CalcAuslagerung
        {
            get
            {
                return clsUmbuchung.isArtikelUBAlt(this.ID, this.BenutzerID, "Auslagerung");
            }
        }
        public bool isUBAlt_CalcLagergeld
        {
            get
            {
                return clsUmbuchung.isArtikelUBAlt(this.ID, this.BenutzerID, "Lagergeld");
            }
        }
        public decimal UBAltArtID
        {
            get
            {
                return clsUmbuchung.getArtikelUBAlt(this.ID, this.BenutzerID);
            }
        }
        public decimal UBNewArtID
        {
            get
            {
                return clsUmbuchung.getArtikelUBNew(this.ID, this.BenutzerID);
            }
        }

        public string ArtikelChangingText { get; set; } = string.Empty;

        public string SchadenTopOne
        {
            get
            {
                string strReturn = string.Empty;
                string strSql = "Select Top(1) s.Bezeichnung FROM Artikel a " +
                                                "INNER JOIN SchadenZuweisung sz on sz.ArtikelID=a.ID " +
                                                "INNER JOIN Schaeden s on s.ID = sz.SchadenID " +
                                                "WHERE a.ID=" + (Int32)this.ID + ";";
                strReturn = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
                return strReturn;
            }
        }
        public List<clsArtikel> listArt = new List<clsArtikel>();
        public List<clsArtikel> listArtUB
        {
            get
            {
                List<clsArtikel> listTmp = new List<clsArtikel>();
                if (this.listArt.Count > 0)
                {
                    foreach (clsArtikel art in this.listArt)
                    {
                        if ((art.Umbuchung) && (art.UBNewArtID > 0))
                        {
                            clsArtikel tmp = new clsArtikel();
                            tmp.InitClass(this._GL_User, this._GL_System);
                            tmp.ID = art.UBNewArtID;
                            tmp.GetArtikeldatenByTableID();

                            listTmp.Add(tmp);
                        }
                    }
                }
                return listTmp;
            }
        }

        //--- Dient als Zwischenspeicher bei der VDA Verarbeitung
        public string ASN_TMS { get; set; } = string.Empty;
        public string ASN_VehicleNo { get; set; } = string.Empty;
        public DateTime glowDate { get; set; } = new DateTime(1900, 1, 1);

        public DateTime GlowDate
        {
            get
            {
                //if (glowDate < Globals.DefaultDateTimeMinValue)
                //{
                //    glowDate = Globals.DefaultDateTimeMinValue;
                //}
                return glowDate;
            }
            set
            {
                glowDate = value;
            }
        }

        public DateTime ScanIn { get; set; }
        public int ScanInUser { get; set; } = 0;
        public DateTime ScanOut { get; set; }
        public int ScanOutUser { get; set; } = 0;

        /****************************************************************************
         *                  Methoden Artikel 
         * *************************************************************************/
        ///<summary>clsArtikel / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, Globals._GL_SYSTEM myGLSystem)
        {
            this._GL_System = myGLSystem;
            this._GL_User = myGLUser;
        }
        ///<summary>clsArtikel / Copy</summary>
        ///<remarks></remarks>
        public clsArtikel Copy()
        {
            return (clsArtikel)this.MemberwiseClone();
        }
        ///<summary>clsArtikel / GetArtikeldatenByTableID</summary>
        ///<remarks>Die Daten mit der Artikel ID werden ermittelt den den entsprechenden 
        ///         Variablen der Klasse zugwiesen.</remarks>
        public bool GetArtikeldatenByTableID()
        {
            try
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "SELECT a.* " +
                                    ", CASE " +
                                            "WHEN (Select COUNT(ID) FROM SchadenZuweisung WHERE ArtikelID=a.ID)>0  THEN CAST(1 as bit) " +
                                            "ELSE CAST(0 as bit) END  as IsSchaden " +
                                    //", CASE " +
                                    //        "WHEN (Select COUNT(ID) FROM Ruecklieferung WHERE ArtikelID=a.ID)>0  THEN CAST(1 as bit) " +
                                    //        "ELSE CAST(0 as bit) END  as IsRL " +

                                    ", (SELECT ub.LVS_ID FROM Artikel ub where ub.Id = a.ArtIDAlt) as LVSNrBeforeUB " +
                                    ", (Select Top(1)b.ID FROM Artikel b WHERE b.ArtIDAlt = a.ID Order by ID desc ) as ArtIdAfterUB " +
                                    ", (Select Top(1)b.LVS_ID FROM Artikel b WHERE b.ArtIDAlt = a.ID Order by ID desc) as LVSNrAfterUB " +
                                    "FROM Artikel a " +
                                    //"LEFT JOIN SchadenZuweisung b ON b.ArtikelID=a.ID " +
                                    //"LEFT JOIN Ruecklieferung c ON c.ArtikelID=a.ID " +
                                    //"LEFT JOIN Sperrlager d ON d.ArtikelID=a.ID " +
                                    " WHERE a.ID=" + ID + ";";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Artikel");
                SetValToCls(dt);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool GetArtikeldatenByProductionNr(string myProdNr, int myAbBereich)
        {
            try
            {
                DataTable dt = new DataTable();
                string strSql = string.Empty;
                strSql = "SELECT a.* " +
                                    ", CASE " +
                                            "WHEN (Select COUNT(ID) FROM SchadenZuweisung WHERE ArtikelID=a.ID)>0  THEN CAST(1 as bit) " +
                                            "ELSE CAST(0 as bit) END  as IsSchaden " +
                                    ", (SELECT ub.LVS_ID FROM Artikel ub where ub.Id = a.ArtIDAlt) as LVSNrBeforeUB " +
                                    ", (Select Top(1)b.ID FROM Artikel b WHERE b.ArtIDAlt = a.ID Order by ID desc ) as ArtIdAfterUB " +
                                    ", (Select Top(1)b.LVS_ID FROM Artikel b WHERE b.ArtIDAlt = a.ID Order by ID desc) as LVSNrAfterUB " +
                                    "FROM Artikel a " +
                                      " WHERE " +
                                       "a.Produktionsnummer LIKE '" + Produktionsnummer + "' " +
                                       " and a.AB_ID=" + myAbBereich + " ;";
                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Artikel");
                SetValToCls(dt);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myProdNr"></param>
        /// <param name="myAbBereich"></param>
        /// <returns></returns>
        //public DataTable GetArtikellistByProductionNr(string myProdNr, int myAbBereich)
        public DataTable GetArtikellistForCallCorrection(clsASNCall myCall, string mySearchLVSNR)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            try
            {
                strSql = "SELECT a.* " +
                                    ", CASE ";
                if (!mySearchLVSNR.Equals(string.Empty))
                {
                    strSql += "when (a.LVS_ID = " + mySearchLVSNR + ") then 0 ";
                }

                strSql += "when (a.Produktionsnummer = '" + myCall.LVSNr.ToString() + "') and " +
                                "(a.Werksnummer = '" + myCall.Werksnummer + "') and " +
                                "(a.Charge = '" + myCall.Charge + "') AND " +
                                "(a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") " +
                          " then 1 " +

                          "when (a.Werksnummer = '" + myCall.Werksnummer + "') and " +
                                "(a.Charge = '" + myCall.Charge + "') AND " +
                                "(a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") " +
                          " then 2 " +

                          "when (a.Charge = '" + myCall.Charge + "') AND " +
                                "(a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") " +
                          "then 3 " +

                          "when (a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") " +
                          "then 4 " +

                          "else 10000 " +
                          " end as OrderID " +
                          ", CASE ";

                if (!mySearchLVSNR.Equals(string.Empty))
                {
                    strSql += "when (a.LVS_ID = " + mySearchLVSNR + ") then '" + clsArtikel.ArtikelField_LVSID.Replace("Artikel.", "") + "' ";
                }
                strSql += "when (a.Produktionsnummer = '" + myCall.LVSNr.ToString() + "') and " +
                                 "(a.Werksnummer = '" + myCall.Werksnummer + "') and " +
                                 "(a.Charge = '" + myCall.Charge + "') AND " +
                                 "(a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") " +
                           " then '" + clsArtikel.ArtikelField_Produktionsnummer.Replace("Artikel.", "") + ","
                                     + clsArtikel.ArtikelField_Werksnummer.Replace("Artikel.", "") + ","
                                     + clsArtikel.ArtikelField_Charge.Replace("Artikel.", "") + ","
                                     + clsArtikel.ArtikelField_Brutto.Replace("Artikel.", "")
                           + "' " +

                           "when (a.Werksnummer = '" + myCall.Werksnummer + "') and " +
                                 "(a.Charge = '" + myCall.Charge + "') AND " +
                                 "(a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") " +
                           " then '" + clsArtikel.ArtikelField_Werksnummer.Replace("Artikel.", "") + ","
                                     + clsArtikel.ArtikelField_Charge.Replace("Artikel.", "") + ","
                                     + clsArtikel.ArtikelField_Brutto.Replace("Artikel.", "")
                           + "' " +

                           "when (a.Charge = '" + myCall.Charge + "') AND " +
                                 "(a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") " +
                           " then '" + clsArtikel.ArtikelField_Charge.Replace("Artikel.", "") + ","
                                     + clsArtikel.ArtikelField_Brutto.Replace("Artikel.", "")
                           + "' " +

                           "when (a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") " +
                           " then '" + clsArtikel.ArtikelField_Brutto.Replace("Artikel.", "")
                           + "' " +
                           "else '' " +
                           "end as Match " +

                 "FROM Artikel a " +
                        //"INNER JOIN LEingang e on e.ID = a.LEingangTableID "+
                        " WHERE ";

                if (!mySearchLVSNR.Equals(string.Empty))
                {
                    strSql += "( (a.LVS_ID = " + mySearchLVSNR + ") ) OR (";
                }
                else
                {
                    strSql += "a.LAusgangTableID=0 " +
                              " and a.AB_ID=" + (int)myCall.AbBereichID + " " +
                              " and (";
                }



                //Produktionsnummer im Feld LVSNR
                if ((!myCall.LVSNr.Equals(string.Empty)) && (myCall.LVSNr > 0))
                {
                    strSql += "(a.Produktionsnummer = '" + myCall.LVSNr.ToString() + "') ";
                }

                //Werksnummer
                if (!myCall.Werksnummer.Equals(string.Empty))
                {
                    if ((!myCall.LVSNr.Equals(string.Empty)) && (myCall.LVSNr > 0))
                    {
                        strSql += " OR ";
                    }
                    strSql += " (a.Werksnummer = '" + myCall.Werksnummer + "') ";
                }
                //Charge
                if (!myCall.Charge.Equals(string.Empty))
                {
                    if (!myCall.Werksnummer.Equals(string.Empty))
                    {
                        strSql += " OR ";
                    }
                    strSql += " (a.Charge LIKE '" + myCall.Charge + "') ";
                }
                //Brutto
                if (!myCall.Brutto.Equals(0))
                {
                    if (!myCall.Charge.Equals(string.Empty))
                    {
                        strSql += " OR ";
                    }

                    strSql += " (a.Brutto = " + myCall.Brutto.ToString().Replace(",", ".") + ") ";
                }

                //if (!mySearchLVSNR.Equals(string.Empty))
                //{
                //    strSql += ") ";
                //}
                strSql += ") ";

                strSql += " ORDER BY OrderID";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "Artikel");
            }
            catch (Exception ex)
            {
                //return false;
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        private void SetValToCls(DataTable dt)
        {
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                this.ID = (decimal)dt.Rows[i]["ID"];
                this.AuftragID = (decimal)dt.Rows[i]["AuftragID"];
                this.AuftragPos = (decimal)dt.Rows[i]["AuftragPos"];
                this.LVS_ID = (decimal)dt.Rows[i]["LVS_ID"];
                this.GArtID = (decimal)dt.Rows[i]["GArtID"];
                this.Dicke = (decimal)dt.Rows[i]["Dicke"];
                this.Breite = (decimal)dt.Rows[i]["Breite"];
                this.Laenge = (decimal)dt.Rows[i]["Laenge"];
                this.Hoehe = (decimal)dt.Rows[i]["Hoehe"];
                this.Anzahl = (Int32)dt.Rows[i]["Anzahl"];
                this.Einheit = dt.Rows[i]["Einheit"].ToString();
                this.gemGewicht = (decimal)dt.Rows[i]["gemGewicht"];
                this.Netto = (decimal)dt.Rows[i]["Netto"];
                this.Werksnummer = dt.Rows[i]["Werksnummer"].ToString();
                this.Produktionsnummer = dt.Rows[i]["Produktionsnummer"].ToString();
                this.exBezeichnung = dt.Rows[i]["exBezeichnung"].ToString();
                this.Charge = dt.Rows[i]["Charge"].ToString();
                this.Bestellnummer = dt.Rows[i]["Bestellnummer"].ToString();
                this.exMaterialnummer = dt.Rows[i]["exMaterialnummer"].ToString();
                this.Position = dt.Rows[i]["Position"].ToString();
                this.Brutto = (decimal)dt.Rows[i]["Brutto"];
                this.GutZusatz = dt.Rows[i]["GutZusatz"].ToString();
                this.BKZ = Convert.ToInt32(dt.Rows[i]["BKZ"]);
                this.Umbuchung = (bool)dt.Rows[i]["UB"];
                this.AbrufReferenz = dt.Rows[i]["AbrufRef"].ToString();
                this.TARef = dt.Rows[i]["TARef"].ToString();
                this.EingangChecked = (bool)dt.Rows[i]["CheckArt"];
                this.AbBereichID = (decimal)dt.Rows[i]["AB_ID"];
                this.MandantenID = (decimal)dt.Rows[i]["Mandanten_ID"];
                this.LEingangTableID = (decimal)dt.Rows[i]["LEingangTableID"];
                this.LAusgangTableID = (decimal)dt.Rows[i]["LAusgangTableID"];
                this.ArtIDRef = dt.Rows[i]["ArtIDRef"].ToString();
                this.AuftragPosTableID = (decimal)dt.Rows[i]["AuftragPosTableID"];
                this.AusgangChecked = (bool)dt.Rows[i]["LA_Checked"];
                this.ArtIDAlt = (decimal)dt.Rows[i]["ArtIDAlt"];
                this.Info = dt.Rows[i]["Info"].ToString();
                decimal decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["LagerOrt"].ToString(), out decTmp);
                this.LagerOrt = decTmp;
                this.LagerOrtTable = dt.Rows[i]["LOTable"].ToString();
                this.exLagerOrt = dt.Rows[i]["exLagerOrt"].ToString();

                //Schaden
                this.bSchaden = (bool)dt.Rows[i]["IsSchaden"];

                this.EAEingangAltLVS = dt.Rows[i]["EAEingangAltLVS"].ToString();
                this.EAAusgangAltLVS = dt.Rows[i]["EAAusgangAltLVS"].ToString();
                this.IsLagerArtikel = (bool)dt.Rows[i]["IsLagerArtikel"];
                decTmp = 0;
                decimal.TryParse(dt.Rows[i]["ADRLagerNr"].ToString(), out decTmp);
                this.ADRLagerNr = decTmp;
                this.FreigabeAbruf = (bool)dt.Rows[i]["FreigabeAbruf"];
                DateTime dtTmp = DateTime.MaxValue;
                DateTime.TryParse(dt.Rows[i]["LZZ"].ToString(), out dtTmp);
                this.LZZ = dtTmp;
                this.Werk = dt.Rows[i]["Werk"].ToString();
                this.Halle = dt.Rows[i]["Halle"].ToString();
                this.Ebene = dt.Rows[i]["Ebene"].ToString();
                this.Reihe = dt.Rows[i]["Reihe"].ToString();
                this.Platz = dt.Rows[i]["Platz"].ToString();
                this.exAuftrag = dt.Rows[i]["exAuftrag"].ToString();
                this.exAuftragPos = dt.Rows[i]["exAuftragPos"].ToString();
                this.ASNVerbraucher = dt.Rows[i]["ASNVerbraucher"].ToString();
                this.UB_AltCalcEinlagerung = (bool)dt.Rows[i]["UB_AltCalcEinlagerung"];
                this.UB_AltCalcAuslagerung = (bool)dt.Rows[i]["UB_AltCalcAuslagerung"];
                this.UB_AltCalcLagergeld = (bool)dt.Rows[i]["UB_AltCalcLagergeld"];
                this.UB_NeuCalcEinlagerung = (bool)dt.Rows[i]["UB_NeuCalcEinlagerung"];
                this.UB_NeuCalcAuslagerung = (bool)dt.Rows[i]["UB_NeuCalcAuslagerung"];
                this.UB_NeuCalcLagergeld = (bool)dt.Rows[i]["UB_NeuCalcLagergeld"];
                this.IsVerpackt = (bool)dt.Rows[i]["IsVerpackt"];
                this.interneInfo = dt.Rows[i]["intInfo"].ToString();
                this.externeInfo = dt.Rows[i]["exInfo"].ToString();
                this.Guete = dt.Rows[i]["Guete"].ToString();
                this.IsMulde = (bool)dt.Rows[i]["IsMulde"];
                this.IsLabelPrint = (bool)dt.Rows[i]["IsLabelPrint"];
                this.IsProblem = (bool)dt.Rows[i]["IsProblem"];
                this.IsKorStVerUse = (bool)dt.Rows[i]["IsKorStVerUse"];
                this.ASNProduktionsnummer = dt.Rows[i]["ASNProduktionsnummer"].ToString();
                this.IsStackable = (bool)dt.Rows[i]["IsStackable"];
                dtTmp = new DateTime(1900, 1, 1);
                DateTime.TryParse(dt.Rows[i]["GlowDate"].ToString(), out dtTmp);
                this.GlowDate = dtTmp;

                dtTmp = new DateTime(1900, 1, 1);
                DateTime.TryParse(dt.Rows[i]["ScanIn"].ToString(), out dtTmp);
                this.ScanIn = dtTmp;

                int iTmp = 0;
                int.TryParse(dt.Rows[i]["ScanInUser"].ToString(), out iTmp);
                this.ScanInUser = iTmp;

                dtTmp = new DateTime(1900, 1, 1);
                DateTime.TryParse(dt.Rows[i]["ScanOut"].ToString(), out dtTmp);
                this.ScanOut = dtTmp;

                iTmp = 0;
                int.TryParse(dt.Rows[i]["ScanOutUser"].ToString(), out iTmp);
                this.ScanOutUser = iTmp;

                //--- Zusatz INFOS
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["LVSNrBeforeUB"].ToString(), out decTmp);
                this.LVSNrBeforeUB = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["LVSNrAfterUB"].ToString(), out decTmp);
                this.LVSNrAfterUB = decTmp;
                decTmp = 0;
                Decimal.TryParse(dt.Rows[i]["ArtIDAfterUB"].ToString(), out decTmp);
                this.ArtIDAfterUB = decTmp;

                //Güterarten
                //GArt = new clsGut();
                //GArt.BenutzerID = this.BenutzerID;
                //GArt.ID = GArtID;
                //if (clsGut.ExistGArtByID(this._GL_User, GArtID))
                //{
                //    GArt.Fill();
                //}
                this.IsRL = false;

                this.IsEMECreate = false;
                this.IsEMLCreate = false;

                if (this.IsLagerArtikel)
                {
                    //GetArtIDAfterUB();

                    SPL = new clsSPL();
                    SPL._GL_User = this._GL_User;
                    SPL.ArtikelID = this.ID;
                    SPL.FillLastINByArtikelID();
                    this.bSPL = SPL.IsInSPL;
                    this.bSPLartCert = SPL.IsCustomCertificateMissing;

                    artTable = clsArtikel.GetArtikelInEingangByArtID(this._GL_User, this.ID);
                    //Eingang
                    //Eingang = new clsLEingang();
                    //Eingang._GL_User = this._GL_User;
                    //Eingang.LEingangTableID = this.LEingangTableID;
                    //Eingang.FillEingang();

                    //Ausgang
                    //Ausgang = new clsLAusgang();
                    //Ausgang._GL_User = this._GL_User;
                    //Ausgang.LAusgangTableID = this.LAusgangTableID;
                    //Ausgang.FillAusgang();
                    //this.IsRL = this.Ausgang.IsRL;

                    //ExtraCharge / Sonderkosten
                    ExtraChargeAssignment = new clsExtraChargeAssignment();
                    ExtraChargeAssignment.InitClass(this._GL_User);

                    //Lagermeldungen
                    Lagermeldungen = new clsLagerMeldungen();
                    Lagermeldungen.InitLagerMeldungen(this);

                    //Lagermeldungen.FillDictLagermeldungenSender(this.ID);
                    //Lagermeldungen.FillDictLagermeldungenReceiver(this.ID);

                    //Call
                    Call = new clsASNCall();
                    Call.InitClass(this._GL_User, this._GL_System, this.sys);
                    Call.ArtikelID = (Int32)this.ID;
                    Call.FillbyArtikelID();
                }
            }
        }



        ///<summary>clsArtikel / AddArtikelLager_SQL</summary>
        ///<remarks></remarks>
        public string AddArtikelLager_SQL(bool bUseOldLvsNr = true, bool bIsAuto = false)
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Artikel (AuftragID, AuftragPos, LVS_ID, Mandanten_ID, AB_ID, BKZ, GArtID, " +
                                            "Dicke, Breite, Laenge, Hoehe, Anzahl, Einheit, gemGewicht, Netto, " +
                                            "Brutto, Werksnummer, Produktionsnummer, exBezeichnung, Charge, " +
                                            "Bestellnummer, exMaterialnummer, Position, GutZusatz, CheckArt, " +
                                            "AbrufRef, TARef, LEingangTableID, LAusgangTableID, " +
                                            "ArtIDRef, AuftragPosTableID, ArtIDAlt, Info, LagerOrt, LOTable, exLagerOrt," +
                                            "ADRLagerNr, FreigabeAbruf, LZZ, Werk, Halle, Ebene, Reihe, Platz, exAuftrag, " +
                                            "exAuftragPos, ASNVerbraucher, UB_AltCalcEinlagerung, UB_AltCalcAuslagerung, " +
                                            "UB_AltCalcLagergeld, UB_NeuCalcEinlagerung, UB_NeuCalcAuslagerung, UB_NeuCalcLagergeld, " +
                                            "IsVerpackt, intInfo, exInfo, Guete, IsStackable, GlowDate" +

                                            ") VALUES ("
                                                        + "0,"   //AuftragID
                                                        + "0,";   //AuftragPos
            if (bUseOldLvsNr)
            {
                strSql += ((Int32)LVS_ID).ToString();
            }
            else
            {
                strSql += ((Int32)GetNewLVSNr()).ToString();
            }
            strSql +=
                      "," + MandantenID +
                      "," + AbBereichID +
                      ", 1" +              //BKZ
                      "," + GArtID +
                      ",'" + Dicke.ToString().Replace(",", ".") + "'" +
                      ",'" + Breite.ToString().Replace(",", ".") + "'" +
                      ",'" + Laenge.ToString().Replace(",", ".") + "'" +
                      ",'" + Hoehe.ToString().Replace(",", ".") + "'" +
                      "," + Anzahl +
                      ",'" + Einheit + "'" +
                      ",'" + gemGewicht.ToString().Replace(",", ".") + "'" +
                      ",'" + Netto.ToString().Replace(",", ".") + "'" +
                      ",'" + Brutto.ToString().Replace(",", ".") + "'" +
                      ",'" + Werksnummer + "'" +
                      ",'" + Produktionsnummer + "'" +
                      ",'" + exBezeichnung + "'" +
                      ",'" + Charge + "'" +
                      ",'" + Bestellnummer + "'" +
                      ",'" + exMaterialnummer + "'" +
                      ",'" + Position + "'" +
                      ",'" + GutZusatz + "'" +
                      "," + Convert.ToInt32(EingangChecked) +
                      ",'" + AbrufReferenz + "'" +                             //AbrufRef
                      ",'" + TARef + "'";                             //TARef

            if (!bIsAuto)
                strSql += "," + LEingangTableID;
            else
                strSql += ", @LEingangTableID";
            strSql += "," + LAusgangTableID +                            //LagerausgangTableID              
                      ",'" + ArtIDRef + "'" +
                      "," + AuftragPosTableID +
                      "," + ArtIDAlt +
                       ",'" + Info + "'" +
                      "," + LagerOrt +
                      ", '" + LagerOrtTable + "'" +
                      ",'" + exLagerOrt + "'" +
                      ", " + ADRLagerNr +
                      ", " + Convert.ToInt32(FreigabeAbruf) +
                      ", '" + LZZ + "'" +
                      ", '" + Werk + "' " +
                      ", '" + Halle + "'" +
                      ", '" + Ebene + "'" +
                      ", '" + Reihe + "'" +
                      ", '" + Platz + "'" +
                      ", '" + exAuftrag + "'" +
                      ", '" + exAuftragPos + "'" +
                      ", '" + ASNVerbraucher + "'" +
                      ", " + Convert.ToInt32(UB_AltCalcEinlagerung) +
                      ", " + Convert.ToInt32(UB_AltCalcAuslagerung) +
                      ", " + Convert.ToInt32(UB_AltCalcLagergeld) +
                      ", " + Convert.ToInt32(UB_NeuCalcEinlagerung) +
                      ", " + Convert.ToInt32(UB_NeuCalcAuslagerung) +
                      ", " + Convert.ToInt32(UB_NeuCalcLagergeld) +
                      ", " + Convert.ToInt32(this.IsVerpackt) +
                      ", '" + this.interneInfo + "'" +
                      ", '" + this.externeInfo + "'" +
                      ", '" + this.Guete + "'" +
                      ", " + Convert.ToInt32(this.IsStackable) +
                      ", '" + GlowDate + "'" +
                      "); ";
            return strSql;
        }
        ///<summary>clsArtikel / GetArtValueByField</summary>
        ///<remarks></remarks> 
        public string GetArtValueByField(string strArtField)
        {
            string strReturn = string.Empty;
            switch (strArtField)
            {
                case clsArtikel.ArtikelField_Anzahl:
                    strReturn = this.Anzahl.ToString();
                    break;
                case clsArtikel.ArtikelField_LVSID:
                    strReturn = this.LVS_ID.ToString();
                    break;
                case clsArtikel.ArtikelField_Dicke:
                    strReturn = this.Dicke.ToString();
                    break;
                case clsArtikel.ArtikelField_Breite:
                    strReturn = this.Breite.ToString();
                    break;
                case clsArtikel.ArtikelField_Länge:
                    strReturn = this.Laenge.ToString();
                    break;
                case clsArtikel.ArtikelField_Höhe:
                    strReturn = this.Hoehe.ToString();
                    break;
                case clsArtikel.ArtikelField_Netto:
                    strReturn = this.Netto.ToString();
                    break;
                case clsArtikel.ArtikelField_Brutto:
                    strReturn = this.Brutto.ToString();
                    break;
                case clsArtikel.ArtikelField_Einheit:
                    if (this.Einheit != null)
                    {
                        strReturn = this.Einheit.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_Produktionsnummer:
                    if (this.Produktionsnummer != null)
                    {
                        strReturn = this.Produktionsnummer.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_Werksnummer:
                    if (this.Werksnummer != null)
                    {
                        strReturn = this.Werksnummer.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_Charge:
                    if (this.Charge != null)
                    {
                        strReturn = this.Charge.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_Bestellnummer:
                    if (this.Bestellnummer != null)
                    {
                        strReturn = this.Bestellnummer.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_exBezeichnung:
                    if (this.exBezeichnung != null)
                    {
                        strReturn = this.exBezeichnung.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_exMaterialnummer:
                    if (this.exMaterialnummer != null)
                    {
                        strReturn = this.exMaterialnummer.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_Gut:
                    if (this.GArt.Bezeichnung != null)
                    {
                        strReturn = this.GArt.Bezeichnung.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_Güte:
                    //strReturn = this.GArt..ToString();
                    break;
                case clsArtikel.ArtikelField_Position:
                    if (this.Position != null)
                    {
                        strReturn = this.Position.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_exAuftrag:
                    if (this.exAuftrag != null)
                    {
                        strReturn = this.exAuftrag.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_exAuftragPos:
                    if (this.exAuftragPos != null)
                    {
                        strReturn = this.exAuftragPos.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_ArtikelIDRef:
                    if (this.ArtIDRef != null)
                    {
                        strReturn = this.ArtIDRef.ToString();
                    }
                    break;
                case clsArtikel.ArtikelField_GlowDate:
                    if (this.GlowDate != null)
                    {
                        strReturn = this.GlowDate.ToString("dd.MM.yyyy");
                    }
                    break;

                case clsArtikel.ArtikelField_TARef:
                    if (this.TARef != null)
                    {
                        strReturn = this.TARef;
                    }
                    break;
                default:
                    break;
            }
            return strReturn;
        }
        ///<summary>clsArtikel / AddArtikelLager</summary>
        ///<remarks></remarks>
        public void AddArtikelLager(bool bUseOldLvsNr = true)
        {
            string strSql = AddArtikelLager_SQL(bUseOldLvsNr);
            strSql = strSql + "Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;

            if (ID > 0)
            {
                //Eintrag Artikel Vita
                clsLager lager = new clsLager();
                lager._GL_User = _GL_User;
                lager.LEingangTableID = LEingangTableID;
                lager.FillLagerDaten(true);
                clsArtikelVita.AddArtikelManualLEingang(_GL_User, ID, lager.Eingang.LEingangID);

                //Add Logbucheintrag 
                string myBeschreibung = "Artikel hinzugefügt: LVS-NR [" + LVS_ID.ToString() + "] / Eingang [" + lager.Eingang.LEingangID.ToString() + "]";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsArtikel / SetDefaultValueToProperties</summary>
        ///<remarks>Hier werden die Defaultwerte für bestimmte Standard Properties zurückgesetzt </remarks>
        public void SetDefaultValueToDefaultProperties(bool myBKZ)
        {
            this.Werk = string.Empty;
            this.Halle = string.Empty;
            this.Reihe = string.Empty;
            this.Ebene = string.Empty;
            this.Platz = string.Empty;

            this.BKZ = Convert.ToInt32(myBKZ);
            this.EingangChecked = false;
            this.AusgangChecked = false;
            this.Umbuchung = false;
            this.IsLagerArtikel = true;
            this.IsLabelPrint = false;
            this.IsProblem = false;
            this.IsKorStVerUse = false;
            this.IsVerpackt = false;
            this.IsStackable = false;
            this.IgnLM = false;
            this.UB_AltCalcEinlagerung = false;
            this.UB_AltCalcAuslagerung = false;
            this.UB_AltCalcLagergeld = false;
            this.UB_NeuCalcEinlagerung = false;
            this.UB_NeuCalcAuslagerung = false;
            this.UB_NeuCalcLagergeld = false;

        }
        ///<summary>clsArtikel / AddArtikelLager_UB</summary>
        ///<remarks></remarks>
        public void AddArtikelLager_UB()
        {
            string strSql = AddArtikelLager_SQL(false);
            strSql = strSql + "Select @@IDENTITY as 'ID'; ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            ID = decTmp;

            //Eingtrag in Artikel Vita
            if (ID > 0)
            {
                //alte Artikeldaten ermitteln
                clsArtikel artALT = new clsArtikel();
                artALT.ID = ArtIDAlt;
                artALT.LVSNrNachUB = LVS_ID;
                artALT._GL_User = this._GL_User;
                artALT.GetArtikeldatenByTableID();

                clsLager lager = new clsLager();
                lager._GL_User = _GL_User;
                lager.LEingangTableID = LEingangTableID;
                lager.FillLagerDaten(true);

                //Eintrag neue Artikeldaten
                clsArtikelVita.AddArtikelLEingang_UB_ArtikelNeu(_GL_User, artALT, this);

                //Add Logbucheintrag 
                string myBeschreibung = "UB Artikel: LVS-NR [" + LVS_ID.ToString() + "] / Eingang [" + lager.Eingang.LEingangID.ToString() + "] / LVSNr alt [" + LVSNrVorUB.ToString() + "] / ID alt [" + ArtIDAlt.ToString() + "]";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Eintrag.ToString(), myBeschreibung);


                clsLager lg = new clsLager();
                lg._GL_User = _GL_User;
                lg.LEingangTableID = artALT.LEingangTableID;
                lg.FillLagerDaten(true);
                //Eintrag alte Artikeldaten
                clsArtikelVita.AddArtikelLEingang_UB_ArtikelAlt(_GL_User, artALT, this);
            }
        }
        ///<summary>clsArtikel / UpdateArtikelLager</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public void UpdateArtikelLager()
        {
            if (ID > 0)
            {
                clsArtikel tmpArt = this.Copy();
                tmpArt.GetArtikeldatenByTableID();

                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "AuftragID=0" +
                                        ", AuftragPos=0" +
                                        ", Mandanten_ID=" + MandantenID +
                                        ", AB_ID=" + AbBereichID +
                                        ", BKZ = " + BKZ +
                                        //ID REF
                                        ", LVS_ID=" + LVS_ID +
                                        ", GArtID='" + GArtID + "'" +
                                        ", GutZusatz='" + GutZusatz + "'" +
                                        ", Werksnummer='" + Werksnummer + "'" +
                                        ", Produktionsnummer='" + Produktionsnummer + "'" +
                                        ", Charge='" + Charge + "'" +
                                        ", Bestellnummer='" + Bestellnummer + "'" +
                                        ", exMaterialnummer='" + exMaterialnummer + "'" +
                                        ", exBezeichnung='" + exBezeichnung + "'" +
                                        ", Position='" + Position + "'" +
                                        ", ArtIDRef='" + ArtIDRef + "'" +

                                        //Maße - Gewichte
                                        ", Anzahl=" + Anzahl +
                                        ", Einheit='" + Einheit + "'" +
                                        ", Dicke='" + Dicke.ToString().Replace(",", ".") + "'" +
                                        ", Breite='" + Breite.ToString().Replace(",", ".") + "'" +
                                        ", Laenge='" + Laenge.ToString().Replace(",", ".") + "'" +
                                        ", Hoehe='" + Hoehe.ToString().Replace(",", ".") + "'" +
                                        ", Netto='" + Netto.ToString().Replace(",", ".") + "'" +
                                        ", Brutto='" + Brutto.ToString().Replace(",", ".") + "'" +
                                        ", TARef= '" + TARef + "'" +
                                        ", LEingangTableID=" + LEingangTableID +
                                        ", LAusgangTableID=" + LAusgangTableID +
                                        ", ArtIDAlt =" + ArtIDAlt +
                                        //Flags
                                        ", UB=" + Convert.ToInt32(Umbuchung) +
                                        ", AbrufRef ='" + AbrufReferenz + "'" +
                                        ", CheckArt= '" + EingangChecked + "'" +
                                        ", LA_Checked ='" + AusgangChecked + "'" +
                                        ", Info='" + Info + "'" +
                                        ", LagerOrt=" + LagerOrt +
                                        ", LOTable='" + LagerOrtTable + "'" +
                                        ", exLagerOrt = '" + exLagerOrt + "'" +
                                        //", IsLagerArtikel ="+ Convert.ToInt32(IsLagerArtikel)+
                                        ", ADRLagerNr=" + ADRLagerNr +
                                        //", FreigabeAbruf="+Convert.ToInt32(FreigabeAbruf)+   //Flag wird nicht hier upgedatet
                                        ", LZZ ='" + LZZ + "'" +
                                        ", Werk ='" + Werk + "'" +
                                        ", Halle ='" + Halle + "'" +
                                        ", Reihe ='" + Reihe + "'" +
                                        ", Ebene = '" + Ebene + "'" +
                                        ", Platz ='" + Platz + "'" +
                                        ", exAuftrag ='" + exAuftrag + "'" +
                                        ", exAuftragPos ='" + exAuftragPos + "'" +
                                        ", ASNVerbraucher ='" + ASNVerbraucher + "'" +
                                        ", UB_AltCalcEinlagerung =" + Convert.ToInt32(UB_AltCalcEinlagerung) +  //nur über UB
                                        ", UB_AltCalcAuslagerung =" + Convert.ToInt32(UB_AltCalcAuslagerung) +
                                        ", UB_AltCalcLagergeld =" + Convert.ToInt32(UB_AltCalcLagergeld) +
                                        ", UB_NeuCalcEinlagerung =" + Convert.ToInt32(UB_NeuCalcEinlagerung) +
                                        ", UB_NeuCalcAuslagerung =" + Convert.ToInt32(UB_NeuCalcAuslagerung) +
                                        ", UB_NeuCalcLagergeld =" + Convert.ToInt32(UB_NeuCalcLagergeld) +
                                        ", IsVerpackt =" + Convert.ToInt32(IsVerpackt) +
                                        ", intInfo='" + interneInfo + "'" +
                                        ", exInfo='" + externeInfo + "'" +
                                        ", Guete='" + Guete + "'" +
                                        ", IsMulde=" + Convert.ToInt32(IsMulde) +
                                        ", IsLabelPrint=" + Convert.ToInt32(IsLabelPrint) +
                                        ", IsProblem=" + Convert.ToInt32(IsProblem) +
                                        ", IsStackable=" + Convert.ToInt32(this.IsStackable) +
                                        ", GlowDate='" + this.GlowDate + "'" +

                                        "  WHERE ID=" + ID + "; ";

                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                if (bExecOK)
                {
                    clsLager lager = new clsLager();
                    lager._GL_User = _GL_User;
                    lager.LEingangTableID = LEingangTableID;
                    lager.FillLagerDaten(true);
                    //ArtikelVita
                    string strChangeInfo = this.CheckArtikelChangingValue(ref tmpArt);
                    if (!strChangeInfo.Equals(string.Empty))
                    {
                        clsArtikelVita.ArtikelChange(this._GL_User, this.ID, lager.Eingang.LEingangID, strChangeInfo);
                        //Add Logbucheintrag 
                        string myBeschreibung = "Artikel geändert: LVS-NR [" + LVS_ID.ToString() + "] / Eingang [" + lager.Eingang.LEingangID.ToString() + "] ";
                        myBeschreibung = myBeschreibung + Environment.NewLine + strChangeInfo;
                        Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                    }
                }
            }
        }
        /// <summary>
        ///             speichert die Lagerorte des Artikels
        /// </summary>
        /// <param name="propDestination"></param>
        /// <param name="strReplaceValue"></param>      

        public void SetArtValueLagerOrt(string propDestination, string strReplaceValue, bool bAddVita)
        {
            string strProperty = propDestination.Remove(0, propDestination.IndexOf('.') + 1);
            try
            {
                //ArtikelVita
                if (bAddVita)
                {
                    clsArtikel tmpArt = this.Copy();
                    tmpArt.GetArtikeldatenByTableID();

                    this.GetType().GetProperty(strProperty).SetValue(this, strReplaceValue, null);

                    clsLager lager = new clsLager();
                    lager._GL_User = _GL_User;
                    lager.LEingangTableID = LEingangTableID;
                    lager.FillLagerDaten(true);

                    this.UpdateArtikelLagerOrt();

                    string strChangeInfo = this.CheckArtikelChangingValue(ref tmpArt);
                    if (!strChangeInfo.Equals(string.Empty))
                    {
                        clsArtikelVita.ArtikelChange(this._GL_User, this.ID, lager.Eingang.LEingangID, strChangeInfo);
                        //Add Logbucheintrag 
                        string myBeschreibung = "Artikel geändert: LVS-NR [" + LVS_ID.ToString() + "] / Eingang [" + lager.Eingang.LEingangID.ToString() + "] ";
                        myBeschreibung = myBeschreibung + Environment.NewLine + strChangeInfo;
                        Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                    }
                }
                else
                {
                    this.GetType().GetProperty(strProperty).SetValue(this, strReplaceValue, null);
                    this.UpdateArtikelLagerOrt();
                }

            }
            catch (Exception ex)
            { }
        }
        ///<summary>clsArtikel / CheckArtikelChangingValue</summary>
        ///<remarks></remarks>
        private string CheckArtikelChangingValue(ref clsArtikel clsArtToCompare)
        {
            ArtikelChangingText = string.Empty;
            Type typeSource = this.GetType();
            PropertyInfo[] pInfoSource = typeSource.GetProperties();
            Type typeCompare = clsArtToCompare.GetType();
            PropertyInfo[] pInfoCompare = typeCompare.GetProperties();

            clsObjPropertyChanges objPropCh = new clsObjPropertyChanges();
            List<clsObjPropertyChanges> listToAdd = new List<clsObjPropertyChanges>();

            foreach (PropertyInfo info in pInfoSource)
            {
                objPropCh = new clsObjPropertyChanges();
                objPropCh.TableId = (int)this.ID;
                objPropCh.TableName = clsObjPropertyChanges.TableName_Artikel;
                objPropCh.UserId = (int)this.BenutzerID;

                if ((info.CanRead) & (info.CanWrite))
                {
                    object NewValue;
                    object oldValue;
                    string PropName = info.Name.ToString();
                    objPropCh.Property = PropName;

                    switch ("Artikel." + PropName)
                    {
                        case clsArtikel.ArtikelField_Bestellnummer:
                        case clsArtikel.ArtikelField_Charge:
                        case clsArtikel.ArtikelField_exAuftrag:
                        case clsArtikel.ArtikelField_exAuftragPos:
                        case clsArtikel.ArtikelField_exBezeichnung:
                        case clsArtikel.ArtikelField_exMaterialnummer:
                        case clsArtikel.ArtikelField_Gut:
                        case clsArtikel.ArtikelField_Produktionsnummer:
                        case clsArtikel.ArtikelField_Werksnummer:
                        case clsArtikel.ArtikelField_Einheit:
                        case clsArtikel.ArtikelField_Werk:
                        case clsArtikel.ArtikelField_Reihe:
                        case clsArtikel.ArtikelField_Ebene:
                        case clsArtikel.ArtikelField_Platz:
                        case clsArtikel.ArtikelField_Halle:
                        case clsArtikel.ArtikelField_Position:

                            NewValue = string.Empty;
                            oldValue = string.Empty;
                            NewValue = info.GetValue(this, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsArtToCompare, null);
                            if (NewValue is object)
                            {
                                if (!NewValue.ToString().Equals(oldValue.ToString()))
                                {
                                    objPropCh.ValueOld = oldValue.ToString();
                                    objPropCh.ValueNew = NewValue.ToString();
                                    listToAdd.Add(objPropCh);

                                    ArtikelChangingText += String.Format("{0}\t{1}{2}", PropName + ":", "[" + oldValue.ToString() + "] ", ">>> [" + NewValue.ToString() + "]") + Environment.NewLine;
                                }
                            }
                            break;

                        case clsArtikel.ArtikelField_Anzahl:
                            Int32 iNewValue = 0;
                            Int32 ioldValue = 0;
                            NewValue = info.GetValue(this, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsArtToCompare, null);
                            Int32.TryParse(NewValue.ToString(), out iNewValue);
                            Int32.TryParse(oldValue.ToString(), out ioldValue);
                            if (iNewValue != ioldValue)
                            {
                                objPropCh.ValueOld = oldValue.ToString();
                                objPropCh.ValueNew = NewValue.ToString();
                                listToAdd.Add(objPropCh);

                                ArtikelChangingText += String.Format("{0}\t{1}{2}", PropName + ":", "[" + ioldValue.ToString() + "] ", ">>> [" + NewValue.ToString() + "]") + Environment.NewLine;
                            }
                            break;

                        case clsArtikel.ArtikelField_Dicke:
                        case clsArtikel.ArtikelField_Breite:
                        case clsArtikel.ArtikelField_Länge:
                        case clsArtikel.ArtikelField_Höhe:
                        case clsArtikel.ArtikelField_Netto:
                        case clsArtikel.ArtikelField_Brutto:
                            decimal decNewValue = 0;
                            decimal decoldValue = 0;
                            NewValue = info.GetValue(this, null);
                            oldValue = typeCompare.GetProperty(PropName).GetValue(clsArtToCompare, null);
                            decimal.TryParse(NewValue.ToString(), out decNewValue);
                            decimal.TryParse(oldValue.ToString(), out decoldValue);
                            if (decNewValue != decoldValue)
                            {
                                objPropCh.ValueOld = oldValue.ToString();
                                objPropCh.ValueNew = NewValue.ToString();
                                listToAdd.Add(objPropCh);

                                ArtikelChangingText += String.Format("{0}\t{1}{2}", PropName + ":", "[" + Functions.FormatDecimal(decoldValue) + "] ", ">>> [" + Functions.FormatDecimal(decNewValue) + "]") + Environment.NewLine;
                            }
                            break;
                    }
                }

            }
            if (!ArtikelChangingText.Equals(string.Empty))
            {
                ArtikelChangingText = "Folgende Ängerungen wurden vorgenommen: " + Environment.NewLine + ArtikelChangingText;

                clsObjPropertyChanges.AddObjPropertyChanges(this._GL_User, listToAdd);
            }
            return ArtikelChangingText;
        }
        ///<summary>clsArtikel / GetArtikelForLEingangGrd</summary>
        ///<remarks></remarks>
        public DataTable GetArtikelForLEingangGrd(decimal myDecLEinangTableID)
        {
            DataTable dt = new DataTable();
            if (myDecLEinangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "SELECT " +
                                    " a.ID as ArtikelID" +
                                    ",a.LVS_ID as LVSNr" +
                                    ", a.Produktionsnummer" +
                                    ", a.Werksnummer" +
                                    ", a.Netto" +
                                    ", a.Brutto" +
                                    ", a.Dicke " +
                                    ", a.Breite " +
                                    ", a.Laenge as Länge" +
                                    ", a.Hoehe as Höhe" +
                                    ", b.Bezeichnung as Güterart" +
                                    ", a.GutZusatz as Zusatz" +
                                    ", a.Charge" +
                                    ", a.Bestellnummer" +
                                    ", a.exMaterialnummer" +
                                    ", a.exBezeichnung" +
                                    ", a.Position" +
                                    ", a.ArtIDRef" +
                                    ", a.Anzahl as Anzahl" +
                                    ", a.Einheit" +
                                    ", (a.Brutto - a.Netto) as Packmittel" +

                                    ", a.FreigabeAbruf as Freigabe" +
                                    ", CAST(DATEPART(YYYY, a.LZZ) as varchar)+CAST(DATEPART(ISOWK, a.LZZ)as varchar) as LZZ" +
                                    ", " + clsArtikel.GetStatusColumnSQL("d", "c") +

                                    " FROM Artikel a " +
                                    "LEFT JOIN Gueterart b ON b.ID=a.GArtID " +
                                        "LEFT JOIN LEingang c ON a.LEingangTableID=c.ID " +
                                        "LEFT JOIN LAUSGANG d ON a.LAusgangTableID = d.ID " +
                                    "WHERE a.LEingangTableID='" + myDecLEinangTableID + "'";

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, BenutzerID, "LEingangArtikel");
            }
            return dt;
        }

        ///<summary>clsArtikel / UpdateArtikelLagerOrt</summary>
        ///<remarks>Update Lagerort des Artikels</remarks>
        public void UpdateArtikelLagerOrt()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "LagerOrt=" + LagerOrt +
                                        ", LOTable='" + LagerOrtTable + "'" +
                                        ", Werk ='" + Werk + "'" +
                                        ", Halle='" + Halle + "'" +
                                        ", Reihe='" + Reihe + "'" +
                                        ", Ebene='" + Ebene + "'" +
                                        ", Platz='" + Platz + "'" +
                                        "  WHERE ID=" + ID + "; ";

                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsArtikel / ResetFreeForCallByArtikel</summary>
        ///<remarks>Update Lagerort des Artikels</remarks>
        public void ResetFreeForCallByArtikel()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "FreigabeAbruf=" + Convert.ToInt32(FreigabeAbruf) +
                                        "  WHERE ID=" + ID + "; ";

                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsArtikel / UpdateArtikelLagerOrt</summary>
        ///<remarks>Update Lagerort des Artikels</remarks>
        public void UpdateArtikelUBCostAssignment()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                "UB_AltCalcEinlagerung=" + Convert.ToInt32(this.UB_AltCalcEinlagerung) +
                                ", UB_AltCalcAuslagerung=" + Convert.ToInt32(this.UB_AltCalcAuslagerung) +
                                ", UB_AltCalcLagergeld=" + Convert.ToInt32(this.UB_AltCalcLagergeld) +
                                ", UB_NeuCalcEinlagerung=" + Convert.ToInt32(this.UB_NeuCalcEinlagerung) +
                                ", UB_NeuCalcAuslagerung=" + Convert.ToInt32(this.UB_NeuCalcAuslagerung) +
                                ", UB_NeuCalcLagergeld=" + Convert.ToInt32(this.UB_NeuCalcLagergeld) +

                                        "  WHERE ID=" + ID + "; ";

                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsArtikel / UpdateArtikelLagerOrt</summary>
        ///<remarks>Update Lagerort des Artikels</remarks>
        public string UpdateArtikelforCall(bool mySetChecked)
        {
            string strSql = string.Empty;
            if (ID > 0)
            {
                //int iSetChecked = 0;
                //int.TryParse(mySetChecked.ToString(), out iSetChecked); 

                string Info = "autom. Abruf " + Environment.NewLine + Environment.NewLine + this.interneInfo;
                strSql = " Update Artikel SET " +
                                "BKZ=0 " +
                                ", intInfo='" + Info + "'" +
                                ", LAusgangTableID = @LAusgangTableID" +
                                //", LA_Checked=1 " +
                                ", LA_Checked=" + +Convert.ToInt32(mySetChecked) +
                                ", AbrufRef='" + this.AbrufReferenz + "'" +
                                ", Abladestelle ='" + this.Abladestelle + "'" +

                                "  WHERE ID=" + (Int32)ID + "; ";
                return strSql;
            }
            return strSql;
        }
        ///<summary>clsArtikel / UpdateArtikelLagerOrt</summary>
        ///<remarks>Update Lagerort des Artikels</remarks>
        public bool UpdateASNProduktionsnummer()
        {
            string strSql = string.Empty;
            strSql = " Update Artikel SET ASNProduktionsnummer='" + this.Produktionsnummer + "'" +
                                                            "  WHERE ID=" + (Int32)ID + "; ";
            bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            return bExecOK;
        }
        ///<summary>clsArtikel / UpdateArtikelLagerOrt</summary>
        ///<remarks>Update Lagerort des Artikels</remarks>
        public bool UpdatePosition()
        {
            string strSql = string.Empty;
            strSql = " Update Artikel SET Position='" + this.Position + "'" +
                                                            "  WHERE ID=" + (Int32)ID + "; ";
            bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bExecOK)
            {
                clsArtikel tmpArt = new clsArtikel();
                tmpArt = this.Copy();
                string strChangeInfo = this.CheckArtikelChangingValue(ref tmpArt);
                if (!strChangeInfo.Equals(string.Empty))
                {
                    clsArtikelVita.ArtikelChange(this._GL_User, this.ID, tmpArt.Eingang.LEingangID, strChangeInfo);
                    //Add Logbucheintrag 
                    string myBeschreibung = "Artikel geändert: LVS-NR [" + LVS_ID.ToString() + "] / Eingang [" + tmpArt.Eingang.LEingangID.ToString() + "] ";
                    myBeschreibung = myBeschreibung + Environment.NewLine + strChangeInfo;
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), myBeschreibung);
                }
            }
            return bExecOK;
        }
        ///<summary>clsArtikel / UpdateArtikelLagerOrt</summary>
        ///<remarks>Update Lagerort des Artikels</remarks>
        public void UpdateArtikelExternerLagerOrt()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "exLagerOrt=" + exLagerOrt +
                                        //                           ", exLOTable='" + exLagerOrtTable + "'" +
                                        "  WHERE ID=" + (Int32)ID + "; ";

                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsArtikel / UpdateArtikelLagerOrt</summary>
        ///<remarks>Update Lagerort des Artikels</remarks>
        public bool UpdateArtikelForKorrekturStorVerfahren(bool bIsUsed)
        {
            bool bReturn = false;
            if (this.ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                        "IsKorStVerUse=" + Convert.ToInt32(bIsUsed) + " ";
                if (bIsUsed)
                {
                    strSql = strSql + " , CheckArt= 0 ";
                }
                strSql = strSql + "  WHERE ID=" + (Int32)this.ID + "; ";

                bReturn = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            return bReturn;
        }
        ///<summary>clsArtikel / UpdateLabelPrint</summary>
        ///<remarks></remarks>
        public bool UpdateLabelPrint(bool myIsLabelPrint)
        {
            bool bExecOK = false;
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                "IsLabelPrint=" + Convert.ToInt32(myIsLabelPrint) +
                                "  WHERE ID=" + (Int32)ID + "; ";

                bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            return bExecOK;
        }
        ///<summary>clsArtikel / UpdateLabelPrint</summary>
        ///<remarks></remarks>
        public string SQLUpdateForRL()
        {
            string Info = "Rücklieferung zum SL am " + DateTime.Now.Date.ToShortDateString();
            string strSql = string.Empty;
            strSql = "Update Artikel SET " +
                            "BKZ=0 " +
                            ", intInfo='" + Info + "'" +
                            ", LAusgangTableID = @LAusgangTableID" +
                            ", LA_Checked=1 " +

                            "  WHERE ID=" + (Int32)ID + "; ";
            return strSql;
        }
        ///<summary>clsArtikel / UpdateLabelPrint</summary>
        ///<remarks></remarks>
        public bool UpdateLabelPrintByEingang(bool myIsLabelPrint, decimal myLEingangTableID)
        {
            bool bExecOK = false;
            if (myLEingangTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Update Artikel SET " +
                                "IsLabelPrint=" + Convert.ToInt32(myIsLabelPrint) +
                                "  WHERE LEingangTableID=" + (Int32)myLEingangTableID + "; ";

                bExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
            return bExecOK;
        }
        ///<summary>clsArtikel / ExistArtikelTableID</summary>
        ///<remarks>Check, ob die Artikel ID in der Datenbank vergeben ist.</remarks>
        public bool ExistArtikelTableID()
        {
            string strSql = string.Empty;
            strSql = "SELECT ID FROM Artikel WHERE ID='" + ID + "'";
            bool reVal = clsSQLcon.ExecuteSQL_GetValueBool(strSql, BenutzerID);
            return reVal;
        }
        ///<summary>clsLager / GetArtikelTableIDLager</summary>
        ///<remarks>Ermittelt anhand der Mandaten-, Arbeitsbereichs- und LVSID die ArtikelTableID.</remarks>
        public static decimal GetArtikelTableIDLager(decimal decBenutzer, decimal myMandantenID, decimal myAbereichID, decimal myLVSNr)
        {
            decimal decTmp = 0;
            string strSql = string.Empty;
            strSql = "SELECT ID FROM Artikel WHERE Mandanten_ID='" + myMandantenID + "' AND AB_ID='" + myAbereichID + "' AND LVS_ID='" + myLVSNr + "' ";
            string tmp = clsSQLcon.ExecuteSQL_GetValue(strSql, decBenutzer);

            if (tmp != string.Empty)
            {
                if (!decimal.TryParse(tmp, out decTmp))
                {
                    decTmp = 0;
                }
            }
            return decTmp;
        }
        ///<summary>clsArtikel / GetArtikelByLEingangTableID</summary>
        ///<remarks></remarks>
        public static List<string> GetArtikelByLEingangTableID(decimal myBenutzer, decimal myLEingangTableID)
        {
            List<string> list = new List<string>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT a.ID FROM Artikel a " +
                                                " WHERE a.LEingangTableID=" + myLEingangTableID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myBenutzer, "Artikel");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list.Add(dt.Rows[i]["ID"].ToString());
            }
            return list;
        }
        ///<summary>clsArtikel / GetArtikelByLEingangTableID</summary>
        ///<remarks></remarks>
        public static List<string> GetArtikelByLAusgangTableID(decimal myBenutzer, decimal myLAusgangTableID)
        {
            List<string> list = new List<string>();
            DataTable dt = new DataTable();
            string strSql = string.Empty;
            strSql = "SELECT a.ID FROM Artikel a " +
                                                " WHERE a.LAusgangTableID=" + myLAusgangTableID + ";";
            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myBenutzer, "Artikel");
            for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
            {
                list.Add(dt.Rows[i]["ID"].ToString());
            }
            return list;
        }
        ///<summary>clsLager / UpdateArtikelLager</summary>
        ///<remarks>Update eines Datensatzes in der DB über die ID.</remarks>
        public static void UpdateArtikelCheck(Globals._GL_USER myGLUser, bool myArtCheck, decimal myDecArtTableID)
        {
            if (clsArtikel.ExistsArtikelTableID(myGLUser.User_ID, myDecArtTableID))
            {
                string strSql = string.Empty;
                if (myArtCheck)
                {
                    strSql = "Update Artikel SET CheckArt='1' WHERE ID='" + myDecArtTableID + "' ";
                }
                else
                {
                    strSql = "Update Artikel SET CheckArt='0' WHERE ID='" + myDecArtTableID + "' ";
                }
                bool bExecOK = clsSQLcon.ExecuteSQL(strSql, myGLUser.User_ID);

                if ((bExecOK) && (myArtCheck))
                {
                    decimal tmpLEingangTableID = clsArtikel.GetLEingangTableIDByID(myGLUser, myDecArtTableID);
                    decimal tmpLEingangID = clsLager.GetLEingangIDByLEingangTableID(myGLUser.User_ID, tmpLEingangTableID);
                    decimal tmpLVSNR = clsArtikel.GetLVSNrByArtikelTableID(myGLUser, myDecArtTableID);
                    clsArtikelVita.ArtikelChecked(myGLUser.User_ID, myDecArtTableID, tmpLEingangID, tmpLVSNR);
                }
            }
        }
        ///<summary>clsLager / ExistsArtikelTableID</summary>
        ///<remarks>Prüft, ob die angegebene ArtikeltableID vorhanden ist.</remarks>
        public static bool ExistsArtikelTableID(decimal decBenuzter, decimal myArtTableID)
        {
            if (myArtTableID > 0)
            {
                string strSQL = "SELECT ID FROM Artikel WHERE ID='" + myArtTableID + "'";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, decBenuzter);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsLager / ExistsArtikelTableID</summary>
        ///<remarks>Prüft, ob die angegebene ArtikeltableID vorhanden ist.</remarks>
        public static bool ExistsLVSID(decimal decBenuzter, decimal myLVSNr, decimal myArBereichId)
        {
            if (myArBereichId > 0)
            {
                string strSQL = "SELECT ID FROM Artikel WHERE LVS_ID=" + (int)myLVSNr + " AND AB_ID=" + (int)myArBereichId + "; ";
                return clsSQLcon.ExecuteSQL_GetValueBool(strSQL, decBenuzter);
            }
            else
            {
                return false;
            }
        }
        ///<summary>clsLager / ExistsArtikelTableID</summary>
        ///<remarks>Prüft, ob die angegebene ArtikeltableID vorhanden ist.</remarks>
        public static bool ExistProdNr(decimal decBenuzter, string myProdNr, decimal myAbBereichID)
        {
            string strSQL = "SELECT a.ID " +
                                        "FROM Artikel a " +
                                        "INNER JOIN LEingang b ON b.ID = a.LEingangTableID " +
                                        "WHERE " +
                                            "a.Produktionsnummer='" + myProdNr + "' " +
                                            "AND b.AbBereich=" + myAbBereichID + " ;";
            bool retVal = clsSQLcon.ExecuteSQL_GetValueBool(strSQL, decBenuzter);
            return retVal;
        }
        ///<summary>clsLager / ExistsArtikelTableID</summary>
        ///<remarks>Es wird geprüft, ob alle Artikel im Eingang geprüft worden sind.</remarks>
        public static bool CheckAllArtikelChecked_Eingang(decimal decBenuzter, decimal myDecLEingangTableID)
        {
            bool bAllArtChecked = false;
            if (myDecLEingangTableID > 0)
            {
                bAllArtChecked = true;
                string strSQL = "SELECT CheckArt FROM Artikel WHERE LEingangTableID='" + myDecLEingangTableID + "'";
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, decBenuzter, "Artikel");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (!(Boolean)dt.Rows[i]["CheckArt"])
                        {
                            bAllArtChecked = false;
                        }
                    }
                }
                else
                {
                    bAllArtChecked = false;
                }
            }
            return bAllArtChecked;
        }
        ///<summary>clsLager / ExistsArtikelTableID</summary>
        ///<remarks>Es wird geprüft, ob alle Artikellabel im Eingang gedruckt worden sind.</remarks>
        public static bool CheckAllArtikelLabelPrinted_Eingang(decimal decBenuzter, decimal myDecLEingangTableID)
        {
            bool bAllPrinted = false;
            if (myDecLEingangTableID > 0)
            {
                bAllPrinted = true;
                string strSQL = "SELECT IsLabelPrint FROM Artikel WHERE LEingangTableID=" + (Int32)myDecLEingangTableID + ";";
                DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, decBenuzter, "Artikel");
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        if (!(Boolean)dt.Rows[i]["IsLabelPrint"])
                        {
                            bAllPrinted = false;
                            i = dt.Rows.Count;
                        }
                    }
                }
                else
                {
                    bAllPrinted = false;
                }
            }
            return bAllPrinted;
        }
        ///<summary>clsLager / ExistsArtikelTableID</summary>
        ///<remarks>Es wird geprüft, ob alle Artikel im Eingang geprüft worden sind.</remarks>
        public static bool CheckAllArtikelChecked_Ausgang(decimal decBenuzter, decimal myDecTableID)
        {
            bool bAllArtChecked = false;
            if (myDecTableID > 0)
            {
                string strSQLCHeck = "Select Count(ID) FROM Artikel WHERE LAusgangTableID=" + myDecTableID + ";";
                string strTmpCheck = clsSQLcon.ExecuteSQL_GetValue(strSQLCHeck, decBenuzter);
                Int32 iTmpCheck = 0;
                Int32.TryParse(strTmpCheck, out iTmpCheck);
                if (iTmpCheck > 0)
                {
                    bAllArtChecked = true;
                    string strSQL = "SELECT Count(ID) FROM Artikel WHERE LAusgangTableID='" + myDecTableID + "' AND LA_Checked=0; ";
                    string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, decBenuzter);
                    Int32 iTmp = 0;
                    Int32.TryParse(strTmp, out iTmp);

                    if (iTmp > 0)
                    {
                        bAllArtChecked = false;
                    }
                    else
                    {
                        bAllArtChecked = true;
                    }
                }
                else
                {
                    bAllArtChecked = false;
                }
            }
            return bAllArtChecked;
        }
        ///<summary>clsLager / CheckAllArtikelPacedinHalle</summary>
        ///<remarks>Ermittelt die Anzahl der Artikel, bei denen das Feld Halle leer ist</remarks>
        public bool CheckAllArtikelInEingangPacedinHalle()
        {
            string strSQL = string.Empty;
            strSQL = "Select Count(ID) as 'Count' From Artikel " +
                                                    "WHERE LEingangTableID=" + LEingangTableID + " " +
                                                    "AND Halle='' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            //MessageBox.Show(strSQL);
            if (iTmp > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>clsLager / CheckAllArtikelPacedinReihe</summary>
        ///<remarks>Ermittelt die Anzahl der Artikel bei denen das Feld Riehe leer ist.</remarks>
        public bool CheckAllArtikelInEingangPacedinReihe()
        {
            string strSQL = string.Empty;
            strSQL = "Select Count(ID) as 'Count' From Artikel " +
                                                    "WHERE LEingangTableID=" + LEingangTableID + " " +
                                                    "AND Reihe='' ";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            if (iTmp > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>clsLager / CheckAllArtikelArePlacedInStore</summary>
        ///<remarks>Check, ob jedem Artikel ein Lagerplatz zugewiesen wurde</remarks>
        public static bool CheckAllArtikelArePlacedInStore(decimal decBenuzter, decimal myTableID, bool bEingang)
        {
            bool bPlaced = false;
            if (myTableID > 0)
            {
                string strSQL = string.Empty;
                if (bEingang)
                {
                    strSQL = "Select Count(ID) as 'Count' From Artikel " +
                                                        "WHERE LEingangTableID=" + myTableID + " " +
                                                        "AND LagerOrt=0;";
                }
                else
                {
                    strSQL = "Select Count(ID) as 'Count' From Artikel " +
                                        "WHERE LAusgangTableID=" + myTableID + " " +
                                        "AND LagerOrt=0;";
                }
                string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, decBenuzter);
                Int32 iTmp = 100;
                Int32.TryParse(strTmp, out iTmp);

                if (iTmp > 0)
                {
                    bPlaced = false;
                }
                else
                {
                    bPlaced = true;
                }
            }
            return bPlaced;
        }
        ///<summary>clsLager / GetSQLDeleteArtikelLager</summary>
        ///<remarks>Löschen der Artikel eines LEingangs. Durchgeführt wird der Delete über eine Transaction in der
        ///         Klasse Lager.</remarks>
        public string GetSQLDeleteArtikelLager()
        {
            string strSql = string.Empty;
            strSql = "Delete FROM Artikel WHERE Mandanten_ID='" + MandantenID + "' AND " +
                                                "AB_ID='" + AbBereichID + "' AND " +
                                                "LEingangTableID='" + LEingangTableID + "' ;";
            return strSql;
        }
        ///<summary>clsLager / GetSQLDeleteArtikelLager</summary>
        ///<remarks>Löschen der Artikel eines LEingangs. Durchgeführt wird der Delete über eine Transaction in der
        ///         Klasse Lager.</remarks>
        public string GetSQLDeleteArtikelbyID()
        {
            string strSql = string.Empty;
            strSql = "Delete FROM Artikel WHERE ID=" + this.ID + " ;";
            return strSql;
        }
        ///<summary>clsLager / DeleteArtikelByID</summary>
        ///<remarks>Löschen des Artikels mit der TableID.</remarks>
        public void DeleteArtikelByAuftragPosTableID(decimal myAuftragPosTableID)
        {
            if (myAuftragPosTableID > 0)
            {
                ID = myAuftragPosTableID;
                GetArtikeldatenByTableID();
                decimal decTmpLagerEingangID = clsLager.GetLEingangIDByLEingangTableID(this._GL_User.User_ID, this.LEingangTableID);

                string strSql = string.Empty;
                strSql = "DELETE FROM Artikel WHERE AuftragPosTableID=" + myAuftragPosTableID + ";";
                bool mybExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                //Logbuch Eintrag
                if (mybExecOK)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Artikel gelöscht: Artikel ID [" + myAuftragPosTableID.ToString() + "] ";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                    //fehlen Lagereingang ID / Lagereingangtable
                    clsArtikelVita.ArtikelDelete(_GL_User, this.LEingangTableID, decTmpLagerEingangID, this.LVS_ID);
                }
            }
        }
        ///<summary>clsLager / DeleteArtikelByID</summary>
        ///<remarks>Löschen des Artikels mit der TableID.</remarks>
        public void DeleteArtikelByID(decimal myArtID)
        {
            if (myArtID > 0)
            {
                ID = myArtID;
                GetArtikeldatenByTableID();
                decimal decTmpLagerEingangID = clsLager.GetLEingangIDByLEingangTableID(this._GL_User.User_ID, this.LEingangTableID);

                string strSql = string.Empty;
                strSql = "DELETE FROM Artikel WHERE ID=" + myArtID + ";";
                bool mybExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

                //Logbuch Eintrag
                if (mybExecOK)
                {
                    //Add Logbucheintrag 
                    string myBeschreibung = "Artikel gelöscht: Artikel ID [" + myArtID.ToString() + "] ";
                    Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);

                    //fehlen Lagereingang ID / Lagereingangtable
                    clsArtikelVita.ArtikelDelete(_GL_User, this.LEingangTableID, decTmpLagerEingangID, this.LVS_ID);

                    this.Eingang.LEingangTableID = this.LEingangTableID;
                    this.Eingang.FillEingang();

                    //Update der anderen Artikelpositionen
                    if (this.Eingang.dtArtInLEingang.Rows.Count > 0)
                    {
                        int iCount = 0;
                        foreach (DataRow row in this.Eingang.dtArtInLEingang.Rows)
                        {
                            if (row["ID"] != null)
                            {
                                decimal decTmp = (decimal)row["ID"];
                                clsArtikel tmpArt = new clsArtikel();
                                tmpArt.InitClass(this._GL_User, this._GL_System);
                                tmpArt.ID = decTmp;
                                tmpArt.GetArtikeldatenByTableID();
                                iCount++;
                                tmpArt.Position = iCount.ToString();
                                tmpArt.UpdatePosition();
                            }
                        }
                    }
                }
            }
        }
        ///<summary>clsLager / GetNewLVSNr</summary>
        ///<remarks>Ermittelt eine neue LVSNR</remarks>
        public decimal GetNewLVSNr(bool bUpdate = true)
        {
            decimal decTmp = 0;
            if (this.sys != null)
            {
                clsPrimeKeys clsPK = new clsPrimeKeys();
                clsPK.sys = this.sys;
                clsPK.AbBereichID = this.sys.AbBereich.ID;
                clsPK._GL_User = this._GL_User;
                clsPK.Mandanten_ID = this.MandantenID;
                clsPK.GetNEWLvsNr(bUpdate);
                decTmp = clsPK.LvsNr;
            }
            return decTmp;
        }
        ///<summary>clsLager / GetLVSNrByArtikelTableID</summary>
        ///<remarks>Ermittelt die LVSNR aus dem Artikel mit der übergebenene ID.</remarks>
        public static decimal GetLVSNrByArtikelTableID(Globals._GL_USER myGLUser, decimal myDecArtTableID)
        {
            decimal decTmp = 0;

            if (myDecArtTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select LVS_ID FROM Artikel WHERE ID =" + myDecArtTableID + " ;";
                string tmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGLUser.User_ID);
                if (tmp != string.Empty)
                {
                    decTmp = Convert.ToDecimal(tmp);
                }
            }
            return decTmp;
        }
        ///<summary>clsLager / GetLVSNrByArtikelTableID</summary>
        ///<remarks>Ermittelt die LVSNR aus dem Artikel mit der übergebenene ID.</remarks>
        public static decimal GetArtikelIDByLVSNr(Globals._GL_USER myGLUser, clsSystem myClsSys, decimal myLVSNr)
        {
            decimal decTmp = 0;
            if (myLVSNr > 0)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Artikel a " +
                              //"INNER JOIN LEingang b on b.ID=a.LEingangTableID " +
                              "WHERE a.LVS_ID=" + myLVSNr +
                                    " AND a.LEingangTableID>0";
                //" AND a.AB_ID=" + myClsSys.AbBereich.ID +
                //" AND a.Mandanten_ID=" + myClsSys.AbBereich.MandantenID + " ;";
                string tmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGLUser.User_ID);
                if (tmp != string.Empty)
                {
                    decTmp = Convert.ToDecimal(tmp);
                }
            }
            return decTmp;
        }
        ///<summary>clsLager / GetArtikelIDByLVSNrAndArbeitsbereich</summary>
        ///<remarks>Ermittelt die ID anhand der LVSNR in Kombination mit dem Arbeitsbereich.</remarks>
        public static decimal GetArtikelIDByLVSNrAndArbeitsbereich(Globals._GL_USER myGLUser, clsSystem myClsSys, decimal myLVSNr, decimal myAbBereichId)
        {
            decimal decTmp = 0;
            if (myLVSNr > 0)
            {
                string strSql = string.Empty;
                strSql = "Select a.ID FROM Artikel a " +
                              "INNER JOIN LEingang b on b.ID=a.LEingangTableID " +
                              " WHERE " +
                                    "a.LVS_ID=" + myLVSNr +
                                    " AND a.LEingangTableID>0 " +
                                    " AND a.AB_ID=" + myAbBereichId + " ;";

                string tmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGLUser.User_ID);
                if (tmp != string.Empty)
                {
                    decTmp = Convert.ToDecimal(tmp);
                }
            }
            return decTmp;
        }
        ///<summary>clsLager / GetLVSNrByArtikelTableID</summary>
        ///<remarks>Ermittelt die LVSNR aus dem Artikel mit der übergebenene ID.</remarks>
        private void GetArtIDAfterUB()
        {
            decimal decTmp = 0;
            string strSql = string.Empty;
            strSql = "Select ID FROM Artikel WHERE ArtIDAlt =" + ID + ";";
            string tmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decTmp = 0;
            Decimal.TryParse(tmp, out decTmp);
            ArtIDAfterUB = decTmp;
        }
        ///<summary>clsLager / GetLVSNrByArtikelTableID</summary>
        ///<remarks>Ermittelt die LVSNR aus dem Artikel mit der übergebenene ID.</remarks>
        public static decimal GetLEingangTableIDByID(Globals._GL_USER myGLUser, decimal myDecArtTableID)
        {
            decimal decTmp = 0;

            if (myDecArtTableID > 0)
            {
                string strSql = string.Empty;
                strSql = "Select LEingangTableID FROM Artikel WHERE ID ='" + myDecArtTableID + "'";
                string tmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGLUser.User_ID);
                decTmp = Convert.ToDecimal(tmp);
            }
            return decTmp;
        }
        ///<summary>clsLager / DeleteArtikelByID</summary>
        ///<remarks>Löschen des Artikels mit der TableID.</remarks>
        public void DeleteArtikelByIDDISPO()
        {
            string strSql = string.Empty;
            strSql = "DELETE FROM Artikel WHERE ID=" + this.ID + ";";
            bool mybExecOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);

            //Logbuch Eintrag
            if (mybExecOK)
            {
                //Add Logbucheintrag 
                string myBeschreibung = "Artikel gelöscht: Artikel ID [" + ID.ToString() + "] ";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
            }
        }
        ///<summary>clsLager / GetAllArtikeldateDispoByID</summary>
        ///<remarks>Ermittelt die Artikel für die entsprechende Artikel ID für die DISPO.</remarks>
        public static DataTable GetAllArtikeldateDispoByID(Globals._GL_USER myGLUser, decimal myDecArtikelID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            if (clsArtikel.ExistsArtikelTableID(myGLUser.User_ID, myDecArtikelID))
            {
                strSql = "SELECT a.ID" +
                                ", a.AuftragID" +
                                ", a.AuftragPos" +
                                ", a.GArtID" +
                                ", e.Bezeichnung as Gut" +
                                ", a.GutZusatz as GutZusatz" +
                                ", a.Dicke" +
                                ", a.Breite" +
                                ", a.Laenge as Länge" +
                                ", a.Hoehe as Höhe" +
                                ", a.Anzahl" +
                                ", a.gemGewicht" +
                                ", a.Netto" +
                                ", a.Brutto" +
                                ", a.Werksnummer" +
                                ", a.Produktionsnummer" +
                                ", a.exBezeichnung" +
                                ", a.Charge" +
                                ", a.Bestellnummer" +
                                ", a.exMaterialnummer" +
                                ", a.Position" +
                                ", a.Mandanten_ID" +
                                " FROM Artikel a " +
                                "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                                   "WHERE a.ID=" + myDecArtikelID;

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Artikel");
            }
            return dt;
        }

        ///<summary>clsLager / GetAllArtikeldateDispoByAuftragPosTableID</summary>
        ///<remarks>Ermittelt die Artikel für die entsprechende Artikel ID für die DISPO.</remarks>
        public static DataTable GetAllArtikeldateDispoByAuftragPosTableID(Globals._GL_USER myGLUser, decimal myAuftragPosTableID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            if (clsAuftragPos.IsAuftragPosInByID(myGLUser, myAuftragPosTableID))
            {
                strSql = "SELECT a.ID" +
                                ", a.AuftragID" +
                                ", a.AuftragPos" +
                                ", e.Bezeichnung as Gut" +
                                ", a.GutZusatz as GutZusatz" +
                                ", a.Dicke" +
                                ", a.Breite" +
                                ", a.Laenge as Länge" +
                                ", a.Hoehe as Höhe" +
                                ", a.Anzahl" +
                                ", a.gemGewicht" +
                                ", a.Netto" +
                                ", a.Brutto" +
                                ", a.Werksnummer" +
                                ", a.Produktionsnummer" +
                                ", a.exBezeichnung" +
                                ", a.Charge" +
                                ", a.Bestellnummer" +
                                ", a.exMaterialnummer" +
                                ", a.Position" +
                                ", c.MandantenID" +
                                " FROM Artikel a " +
                                "INNER JOIN AuftragPos b ON b.ID=a.AuftragPosTableID " +
                                "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                                "INNER JOIN Auftrag c ON c.ID = b.AuftragTableID " +
                                "WHERE AuftragPosTableID=" + myAuftragPosTableID;

                dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Artikel");
            }
            return dt;
        }
        ///<summary>clsLager / GetArtikelTableIDByAuftragID</summary>
        ///<remarks>Ermittelt die Artikel für die entsprechende Artikel ID für die DISPO.</remarks>
        public static decimal GetArtikelTableIDByAuftragID(Globals._GL_USER myGLUser, decimal myMandantenID, decimal myAuftragID, decimal myAuftragPosID, decimal myABereichID)
        {
            string strSql = string.Empty;
            decimal decVal = 0;
            strSql = "SELECT ID FROM Artikel " +
                                                "WHERE AuftragID=" + myAuftragID.ToString() + " " +
                                                       "AND AuftragPos=" + myAuftragPosID.ToString() + " " +
                                                       "AND Mandanten_ID=" + myMandantenID.ToString() + " " +
                                                       "AND AB_ID=" + myABereichID.ToString() + " ;";

            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, myGLUser.User_ID);
            if (strTmp != string.Empty)
            {
                if (!Decimal.TryParse(strTmp, out decVal))
                {
                    decVal = 0;
                }
            }
            return decVal;
        }



        //************************************************************************
        //-------------  Methoden  Dispo
        //************************************************************************
        ///<summary>clsLager / GetMainSQLForFacturierung</summary>
        ///<remarks>Beinhaltet die MainSQLAnweisung für folgende Funktionen:
        ///         -GetArtikelForDispoCalcDefault
        ///         -GetArtikelForDispoStatusDisponiert
        ///         -GetArtikelForDispoStatusDone
        ///         -GetArtikelForDispoStatusFreigabeAbrechnung
        ///         -GetArtikelForDispoStatusAbgerechnet
        ///         -GetArtikelForDispoStatusBezahlt</remarks>
        public static string GetMainSQLForFacturierung()
        {
            string sql = "Select a.*, " +
                                "(Select Name1 FROM ADR WHERE ID=a.KD_ID) as Auftraggeber, " +
                                "(Select Name1 FROM ADR WHERE ID=a.B_ID) as Versender, " +
                                "((Select PLZ FROM ADR WHERE ID=a.B_ID)+ '-'+(Select Ort FROM ADR WHERE ID=a.B_ID)) as von," +
                                "(Select Name1 FROM ADR WHERE ID=a.E_ID) as Empfaenger, " +
                                "((Select PLZ FROM ADR WHERE ID=a.E_ID)+ '-'+(Select Ort FROM ADR WHERE ID=a.E_ID)) as nach, " +
                                "c.AuftragPos, " +
                                "c.GArt, " +
                                "c.Netto, " +
                                "c.Brutto, " +
                                "b.Status as Stat, " +
                                "b.ID as AuftragPosTableID " +
                                                "FROM Auftrag a " +
                                                "INNER JOIN AuftragPos b ON a.ID=b.AuftragTableID " +
                                                "INNER JOIN Artikel c ON c.AuftragPosTableID=b.ID ";
            return sql;
        }

        ///<summary>clsLager / GetArtikelForDispoStatusDone</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status >4</remarks>
        public static DataTable GetArtikelForDispoCalcDefault(Globals._GL_USER myGLUser, decimal myMandantenID, decimal myABereichID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = GetMainSQLForFacturierung() +
                                                "WHERE b.Status>=4 AND a.MandantenID='" + myMandantenID + "' " +
                                                "AND ArbeitsbereichID='" + myABereichID + "' " +
                                                "Order BY a.ANr;";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Aufträge");
            return dt;
        }
        ///<summary>clsLager / GetArtikelForDispoStatusDone</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 4 = disponiert.</remarks>
        public static DataTable GetArtikelForDispoStatusDisponiert(Globals._GL_USER myGLUser, decimal myMandantenID, decimal myABereichID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = strSql = GetMainSQLForFacturierung() +
                                                "WHERE b.Status=4 AND a.MandantenID='" + myMandantenID + "' " +
                                                "AND ArbeitsbereichID='" + myABereichID + "' " +
                                                "Order BY a.ANr;";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Aufträge");
            return dt;
        }
        ///<summary>clsLager / GetArtikelForDispoStatusDone</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 5 = Auftrag durchgeführ.</remarks>
        public static DataTable GetArtikelForDispoStatusDone(Globals._GL_USER myGLUser, decimal myMandantenID, decimal myABereichID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = strSql = GetMainSQLForFacturierung() +
                                                "WHERE b.Status=5 AND a.MandantenID='" + myMandantenID + "' " +
                                                "AND ArbeitsbereichID='" + myABereichID + "' " +
                                                "Order BY a.ANr;";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Aufträge");
            return dt;
        }
        ///<summary>clsLager / GetArtikelForDispoStatusFreigabeAbrechnung</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 6 = Freigabe zur Abrechnung</remarks>
        public static DataTable GetArtikelForDispoStatusFreigabeAbrechnung(Globals._GL_USER myGLUser, decimal myMandantenID, decimal myABereichID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = strSql = GetMainSQLForFacturierung() +
                                                "WHERE b.Status=6 AND a.MandantenID='" + myMandantenID + "' " +
                                                "AND ArbeitsbereichID='" + myABereichID + "' " +
                                                "Order BY a.ANr;";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Aufträge");
            return dt;
        }
        ///<summary>clsLager / GetArtikelForDispoStatusAbgerechnet</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 7 = abgerechnet</remarks>
        public static DataTable GetArtikelForDispoStatusAbgerechnet(Globals._GL_USER myGLUser, decimal myMandantenID, decimal myABereichID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = strSql = strSql = GetMainSQLForFacturierung() +
                                                "WHERE b.Status=7 AND a.MandantenID='" + myMandantenID + "' " +
                                                "AND ArbeitsbereichID='" + myABereichID + "' " +
                                                "Order BY a.ANr;";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Aufträge");
            return dt;
        }
        ///<summary>clsLager / GetArtikelForDispoStatusAbgerechnet</summary>
        ///<remarks>Ermittelt alle Artikel mit dem Status 8 = Bezahlt </remarks>
        public static DataTable GetArtikelForDispoStatusBezahlt(Globals._GL_USER myGLUser, decimal myMandantenID, decimal myABereichID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = strSql = strSql = strSql = GetMainSQLForFacturierung() +
                                                "WHERE b.Status=8 AND a.MandantenID='" + myMandantenID + "' " +
                                                "AND ArbeitsbereichID='" + myABereichID + "' " +
                                                "Order BY a.ANr;";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Aufträge");
            return dt;
        }
        /*******************************************************************************************************************/

        ///<summary>clsLager / GetAuftraggeberForDispoStatusDone</summary>
        ///<remarks>Ermittelt alle Auftraggeber mit dem Status 5 = Auftrag durchgeführt.</remarks>
        public static DataTable GetAuftraggeberForDispoStatusDone(Globals._GL_USER myGLUser, decimal myMandantenID, decimal myABereichID)
        {
            string strSql = string.Empty;
            DataTable dt = new DataTable();
            strSql = "Select DISTINCT a.KD_ID, (Select Name1 FROM ADR WHERE ID=a.KD_ID) as Auftraggeber " +
                                                "FROM Auftrag a " +
                                                "INNER JOIN AuftragPos b ON a.ID=b.AuftragTableID " +
                                                "WHERE b.Status=5 AND a.MandantenID='" + myMandantenID + "' " +
                                                "AND ArbeitsbereichID='" + myABereichID + "' " +
                                                "Order BY Auftraggeber;";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myGLUser.User_ID, "Auftraggeber");
            return dt;
        }
        ///<summary>clsArtikel / SetColArtTable</summary>
        ///<remarks></remarks>
        public void SetColArtTable()
        {
            //DataColumn col;

            //----  init and add the columns ----
            //Gut
            DataColumn col1 = new DataColumn();
            col1.DataType = System.Type.GetType("System.Decimal");
            col1.ColumnName = "GArtID";
            col1.ReadOnly = false;
            artTable.Columns.Add(col1);

            DataColumn colGutZusatz = new DataColumn();
            colGutZusatz.DataType = System.Type.GetType("System.String");
            colGutZusatz.ColumnName = "GutZusatz";
            colGutZusatz.ReadOnly = false;
            artTable.Columns.Add(colGutZusatz);

            //Dicke
            DataColumn col2 = new DataColumn();
            col2.DataType = System.Type.GetType("System.Decimal");
            col2.ColumnName = "Dicke";
            col2.ReadOnly = false;
            artTable.Columns.Add(col2);

            //Breite
            DataColumn col3 = new DataColumn();
            col3.DataType = System.Type.GetType("System.Decimal");
            col3.ColumnName = "Breite";
            col3.ReadOnly = false;
            artTable.Columns.Add(col3);

            //Länge
            DataColumn col4 = new DataColumn();
            col4.DataType = System.Type.GetType("System.Decimal");
            col4.ColumnName = "Laenge";
            col4.ReadOnly = false;
            artTable.Columns.Add(col4);

            //Höhe
            DataColumn col5 = new DataColumn();
            col5.DataType = System.Type.GetType("System.Decimal");
            col5.ColumnName = "Hoehe";
            col5.ReadOnly = false;
            artTable.Columns.Add(col5);

            //ME
            DataColumn col6 = new DataColumn();
            col6.DataType = System.Type.GetType("System.Int32");
            col6.ColumnName = "Anzahl";
            col6.ReadOnly = false;
            artTable.Columns.Add(col6);

            //gemGewicht
            DataColumn col7 = new DataColumn();
            col7.DataType = System.Type.GetType("System.Decimal");
            col7.ColumnName = "gemGewicht";
            col7.ReadOnly = false;
            artTable.Columns.Add(col7);

            //Netto
            DataColumn col8 = new DataColumn();
            col8.DataType = System.Type.GetType("System.Decimal");
            col8.ColumnName = "Netto";
            col8.ReadOnly = false;
            artTable.Columns.Add(col8);

            //Werksnummer
            DataColumn col9 = new DataColumn();
            col9.DataType = System.Type.GetType("System.String");
            col9.ColumnName = "Werksnummer";
            col9.ReadOnly = false;
            artTable.Columns.Add(col9);

            //ID
            DataColumn col10 = new DataColumn();
            col10.DataType = System.Type.GetType("System.String");
            col10.ColumnName = "Table_ID";
            col10.ReadOnly = false;
            artTable.Columns.Add(col10);

            //Brutto
            DataColumn col11 = new DataColumn();
            col11.DataType = System.Type.GetType("System.Decimal");
            col11.ColumnName = "Brutto";
            col11.ReadOnly = false;
            artTable.Columns.Add(col11);
        }
        ///<summary>clsArtikel / ValueToArtTable</summary>
        ///<remarks></remarks>
        public void ValueToArtTable()
        {
            DataRow dr = artTable.NewRow();
            dr["GArtID"] = GArtID;
            dr["GutZusatz"] = GutZusatz;
            dr["Dicke"] = Dicke;
            dr["Breite"] = Breite;
            dr["Laenge"] = Laenge;
            dr["Hoehe"] = Hoehe;
            dr["Anzahl"] = Anzahl;
            dr["gemGewicht"] = gemGewicht;
            dr["Netto"] = Netto;
            dr["Werksnummer"] = Werksnummer;
            dr["Table_ID"] = ID;
            dr["Brutto"] = Brutto;
            artTable.Rows.Add(dr);
        }
        ///<summary>clsArtikel / DeleteArtikel</summary>
        ///<remarks></remarks>
        public static void DeleteArtikel(decimal decAuftrag, decimal decAuftragPos, decimal decBenutzerID)
        {
            string strSql = string.Empty;
            strSql = "Delete FROM Artikel WHERE AuftragID='" + decAuftrag + "' AND AuftragPos='" + decAuftragPos + "'";
            clsSQLcon.ExecuteSQL(strSql, decBenutzerID);
        }
        ///<summary>clsArtikel / Add</summary>
        ///<remarks></remarks>
        public void Add()
        {
            AddArtikelSped();
        }
        ///<summary>clsArtikel / GetSQLAddArtikelSped</summary>
        ///<remarks></remarks>
        private string GetSQLAddArtikelSped()
        {
            string strSql = string.Empty;
            strSql = "INSERT INTO Artikel (AuftragID, AuftragPos, LVS_ID, BKZ, GArtID, GutZusatz, Dicke, Breite, Laenge, Hoehe ,Anzahl " +
                                          ",gemGewicht, Netto, Brutto, Werksnummer, AuftragPosTableID, IsLagerArtikel, TARef, AB_ID, Mandanten_ID " +
                                          ", LEingangTableID, LAusgangTableID, intInfo, exInfo, IsMulde, IsLabelPrint, IsProblem, IsKorStVerUse" +
                                          ", IgnLM, Abladestelle,IsStackable, GlowDate) " +
                                         "VALUES (" + AuftragID +
                                                  "," + AuftragPos +
                                                  ", 0" +   //LVS
                                                  ", 0" +   //BKZ
                                                  "," + GArtID +
                                                  ",'" + GutZusatz + "'" +
                                                  ",'" + Dicke.ToString().Replace(",", ".") + "'" +
                                                  ",'" + Breite.ToString().Replace(",", ".") + "'" +
                                                  ",'" + Laenge.ToString().Replace(",", ".") + "'" +
                                                  ",'" + Hoehe.ToString().Replace(",", ".") + "'" +
                                                  "," + Anzahl +
                                                  ",'" + gemGewicht.ToString().Replace(",", ".") + "'" +
                                                  ",'" + Netto.ToString().Replace(",", ".") + "'" +
                                                  ",'" + Brutto.ToString().Replace(",", ".") + "'" +
                                                  ",'" + Werksnummer + "'" +
                                                  "," + AuftragPosTableID +
                                                  ", 0" + //+ Convert.ToInt32(IsLagerArtikel) +
                                                  ", '" + this.TARef + "'" +
                                                  ", " + AbBereichID +
                                                  ", " + MandantenID +
                                                  ", 0" +  //LEingangTableID
                                                  ", 0" +  //LAusgangTableID
                                                  ", '" + interneInfo + "'" +
                                                  ", '" + externeInfo + "'" +
                                                  ", " + Convert.ToInt32(IsMulde) +
                                                  ", " + Convert.ToInt32(IsLabelPrint) +
                                                  ", " + Convert.ToInt32(IsProblem) +
                                                  ", 0 " + // IsKorStVerUse
                                                  ",  " + Convert.ToInt32(IgnLM) +
                                                  ", '" + this.Abladestelle + "'" +
                                                  ", " + Convert.ToInt32(IsStackable) +
                                                  ", '" + GlowDate + "'" +
                                                  ") ";
            strSql = strSql + "Select @@IDENTITY as 'ID' ;";
            return strSql;
        }
        ///<summary>clsArtikel / AddArtikel</summary>
        ///<remarks></remarks>
        public void AddArtikelSped()
        {
            string strSql = string.Empty;
            strSql = GetSQLAddArtikelSped();
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSql, BenutzerID);
            decimal decTmp = 0;
            decimal.TryParse(strTmp, out decTmp);
            if (decTmp > 0)
            {
                ID = decTmp;
                GetArtikeldatenByTableID();
            }
        }
        ///<summary>clsArtikel / GetSQLForUpdateArtikel</summary>
        ///<remarks>SQL Statement für Artikelupdate per ID</remarks>
        public string GetSQLForUpdateArtikel()
        {
            string strSql = string.Empty;
            strSql = "Update Artikel SET " +
                                        "AuftragID =" + AuftragID +
                                        ", AuftragPos =" + AuftragPos +
                                        ", Mandanten_ID=" + MandantenID +
                                        ", AB_ID=" + AbBereichID +
                                        ", BKZ = " + BKZ +
                                        ", LVS_ID=" + LVS_ID +
                                        ", GArtID =" + GArtID +
                                        ", GutZusatz ='" + GutZusatz + "'" +
                                        ", Werksnummer ='" + Werksnummer + "'" +
                                        ", Produktionsnummer ='" + Produktionsnummer + "'" +
                                        ", Charge ='" + Charge + "'" +
                                        ", Bestellnummer ='" + Bestellnummer + "'" +
                                        ", exMaterialnummer ='" + exMaterialnummer + "'" +
                                        ", exBezeichnung ='" + exBezeichnung + "'" +
                                        ", Position ='" + Position + "' " +
                                        ", ArtIDRef='" + ArtIDRef + "'" +

                                        ", Anzahl=" + Anzahl +
                                        ", Einheit='" + Einheit + "'" +
                                        ", Dicke='" + Dicke.ToString().Replace(",", ".") + "'" +
                                        ", Breite='" + Breite.ToString().Replace(",", ".") + "'" +
                                        ", Laenge='" + Laenge.ToString().Replace(",", ".") + "'" +
                                        ", Hoehe='" + Hoehe.ToString().Replace(",", ".") + "'" +
                                        ", gemGewicht ='" + gemGewicht.ToString().Replace(",", ".") + "'" +
                                        ", Netto='" + Netto.ToString().Replace(",", ".") + "'" +
                                        ", Brutto='" + Brutto.ToString().Replace(",", ".") + "'" +
                                        ", LEingangTableID=" + LEingangTableID +
                                        ", LAusgangTableID=" + LAusgangTableID +
                                        ", ArtIDAlt =" + ArtIDAlt +

                                        ", UB=" + Convert.ToInt32(Umbuchung) +
                                        ", TARef ='" + this.TARef + "'" +
                                        ", CheckArt= '" + EingangChecked + "'" +
                                        ", LA_Checked ='" + AusgangChecked + "'" +
                                        ", Info='" + Info + "'" +
                                        ", LagerOrt=" + LagerOrt +
                                        ", LOTable='" + LagerOrtTable + "'" +
                                        ", exLagerOrt = '" + exLagerOrt + "'" +
                                        //", IsLagerArtikel ="+ Convert.ToInt32(IsLagerArtikel)+
                                        ", ADRLagerNr=" + ADRLagerNr +
                                        //", FreigabeAbruf="+Convert.ToInt32(FreigabeAbruf)+   //Flag wird nicht hier upgedatet
                                        ", LZZ ='" + LZZ + "'" +
                                        ", Werk ='" + Werk + "'" +
                                        ", Halle ='" + Halle + "'" +
                                        ", Reihe ='" + Reihe + "'" +
                                        ", Ebene = '" + Ebene + "'" +
                                        ", Platz ='" + Platz + "'" +
                                        ", exAuftrag ='" + exAuftrag + "'" +
                                        ", exAuftragPos ='" + exAuftragPos + "'" +
                                        ", ASNVerbraucher ='" + ASNVerbraucher + "'" +
                                        ", UB_AltCalcEinlagerung =" + Convert.ToInt32(UB_AltCalcEinlagerung) +  //nur über UB
                                        ", UB_AltCalcAuslagerung =" + Convert.ToInt32(UB_AltCalcAuslagerung) +
                                        ", UB_AltCalcLagergeld =" + Convert.ToInt32(UB_AltCalcLagergeld) +
                                        ", UB_NeuCalcEinlagerung =" + Convert.ToInt32(UB_NeuCalcEinlagerung) +
                                        ", UB_NeuCalcAuslagerung =" + Convert.ToInt32(UB_NeuCalcAuslagerung) +
                                        ", UB_NeuCalcLagergeld =" + Convert.ToInt32(UB_NeuCalcLagergeld) +
                                        ", IsVerpackt =" + Convert.ToInt32(IsVerpackt) +
                                        ", intInfo='" + interneInfo + "'" +
                                        ", exInfo='" + externeInfo + "'" +
                                        ", Guete='" + Guete + "'" +
                                        ", IsMulde=" + Convert.ToInt32(IsMulde) +
                                        ", IsLabelPrint=" + Convert.ToInt32(IsLabelPrint) +
                                        ", IsProblem =" + Convert.ToInt32(IsProblem) +
                                        ", IsKorStVerUse =" + Convert.ToInt32(IsKorStVerUse) +
                                        ", IgnLM =" + Convert.ToInt32(IgnLM) +
                                        ", Abladestelle ='" + this.Abladestelle + "'" +
                                        ", IsStackable=" + Convert.ToInt32(this.IsStackable) +
                                        ", GlowDate ='" + GlowDate + "'" +

                                        " WHERE ID=" + ID + " ;";
            return strSql;
        }
        ///<summary>clsArtikel / UpdateArtikelALLDispo</summary>
        ///<remarks></remarks>
        public void UpdateArtikelALLDispo()
        {
            string strSql = string.Empty;
            strSql = GetSQLForUpdateArtikel();
            bool bOK = clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            if (bOK)
            {
                //Add Logbucheintrag update
                string Beschreibung = "Artikel Update - Artikel ID:" + ID + " Daten geändert";
                Functions.AddLogbuch(BenutzerID, enumLogbuchAktion.Aenderung.ToString(), Beschreibung);
            }
        }
        ///<summary>clsArtikel / DoUpdateArtikel</summary>
        ///<remarks></remarks>
        public void DoUpdateArtikel()
        {
            //Artikel schon vorhanden und kann über die ID upgedatet werden
            if (ID > 0)
            {
                UpdateArtikelSped();
            }
            else  // Insert, da Artikel keine ID und noch nicht vorhanden
            {
                AddArtikelSped();
            }
        }
        ///<summary>clsArtikel / UpdateArtikelSped</summary>
        ///<remarks></remarks>
        public void UpdateArtikelSped()
        {
            if (ID > 0)
            {
                string strSql = string.Empty;
                strSql = ("Update Artikel SET AuftragID =" + AuftragID +
                                              ", AuftragPos =" + AuftragPos +
                                              ", GArtID =" + GArtID +
                                              ", GutZusatz ='" + GutZusatz + "'" +
                                              ", Dicke ='" + Dicke.ToString().Replace(",", ".") + "' " +
                                              ", Breite ='" + Breite.ToString().Replace(",", ".") + "' " +
                                              ", Laenge = '" + Laenge.ToString().Replace(",", ".") + "' " +
                                              ", Hoehe ='" + Hoehe.ToString().Replace(",", ".") + "' " +
                                              ", Anzahl =" + Anzahl +
                                              ", gemGewicht ='" + gemGewicht.ToString().Replace(",", ".") + "' " +
                                              ", Netto ='" + Netto.ToString().Replace(",", ".") + "' " +
                                              ", Brutto ='" + Brutto.ToString().Replace(",", ".") + "' " +
                                              ", AuftragPosTableID ='" + AuftragPosTableID + "' " +
                                              ", Werksnummer ='" + Werksnummer + "' " +
                                              ", IsLagerArtikel = " + Convert.ToInt32(IsLagerArtikel) + " " +
                                              ", IsMulde=" + Convert.ToInt32(IsMulde) +
                                              ", IsLabelPrint=" + Convert.ToInt32(IsLabelPrint) +
                                              ", IsProblem =" + Convert.ToInt32(IsProblem) +
                                              ", IsKorStVerUse =" + Convert.ToInt32(IsKorStVerUse) +
                                              ", IgnLM =" + Convert.ToInt32(IgnLM) +
                                              ", Abladestelle ='" + this.Abladestelle + "'" +
                                              ", IsStackable=" + Convert.ToInt32(this.IsStackable) +
                                              ", GlowDate ='" + GlowDate + "'" +

                                              " WHERE ID='" + ID + "'");
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);
            }
        }
        ///<summary>clsArtikel / UpdateFreeForCall</summary>
        ///<remarks></remarks>
        public void UpdateFreeForCall(List<string> myList)
        {
            if (myList.Count > 0)
            {
                string strArtikelIDList = string.Join(",", myList.ToArray());
                string strSql = "Update Artikel " +
                                        "SET FreigabeAbruf=1 " +
                                        "WHERE ID IN (" + strArtikelIDList + ");";
                clsSQLcon.ExecuteSQL(strSql, BenutzerID);

            }
        }
        ///<summary>clsArtikel / DoArtikelSplitt</summary>
        ///<remarks>Die verwendete Artikelinstanz gibt den alten, zu splittenden Artikel an. Die 
        ///         Referenzinstanz "myArtNeu" gibt den neu anzulegenden Artikel an.</remarks>
        public void DoArtikelSplitt(ref clsArtikel myArtNeu)
        {
            //da hier für der Splitt für den Dispobereich durchgeführt werden müssten noch die Lagerbezogenenartikeldaten
            //entsprechend angepasst werden
            // - LZZ 
            this.LZZ = clsSystem.const_DefaultDateTimeValue_Min;
            myArtNeu.LZZ = clsSystem.const_DefaultDateTimeValue_Min;


            string strSQL = string.Empty;
            //Update alter Artikel
            strSQL = this.GetSQLForUpdateArtikel();
            //Insert neuen Artikel
            strSQL = strSQL + myArtNeu.GetSQLAddArtikelSped();
            string strTmp = clsSQLcon.ExecuteSQLWithTRANSACTIONGetValue(strSQL, "ArtSplitt", BenutzerID);
        }
        ///<summary>clsArtikel / DoArtikelSplitt</summary>
        ///<remarks>Die verwendete Artikelinstanz gibt den alten, zu splittenden Artikel an. Die 
        ///         Referenzinstanz "myArtNeu" gibt den neu anzulegenden Artikel an.</remarks>
        public bool DoArtikelMerge(ref clsArtikel myArtToMerge)
        {
            string strSQL = string.Empty;
            //Update Merge Artikel
            strSQL = myArtToMerge.GetSQLForUpdateArtikel();
            //Delete alten Artikel
            strSQL = strSQL + GetSQLDeleteArtikelbyID();
            bool bOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "MergeArtikel", BenutzerID);
            return bOK;
        }
        /***********************************************************************************
         *                           publci static
         * *******************************************************************************/
        ///<summary>clsArtikel / GetArtikelAnzahlInEingang</summary>
        ///<remarks>Ermittelt die Anzahl der Artikel in einem Lagereingang.</remarks>
        public static Int32 GetArtikelAnzahlInEingang(Globals._GL_USER myGLUser, decimal myLEingangTableID)
        {
            Int32 Anzahl = 0;
            string strSQL = string.Empty;
            strSQL = "SELECT Count(ID) FROM Artikel WHERE LEingangTableID=" + myLEingangTableID + ";";
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, myGLUser.User_ID);
            Int32 iTmp = 0;
            Int32.TryParse(strTmp, out iTmp);
            Anzahl = iTmp;
            return Anzahl;
        }
        ///<summary>clsArtikel / GetArtikelInEingang</summary>
        ///<remarks>Ermittelt alle Artikel in einem Lagereingang.</remarks>
        public static DataTable GetArtikelInEingang(Globals._GL_USER myGLUser, decimal myLEingangTableID)
        {
            Int32 Anzahl = 0;
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Artikel WHERE LEingangTableID=" + myLEingangTableID + ";";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "ArtikelInEingang");
            return dt;
        }
        ///<summary>clsArtikel / GetArtikelInEingang</summary>
        ///<remarks>Ermittelt alle Artikel in einem Lagereingang.</remarks>
        public static DataTable GetArtikelInAusgang(Globals._GL_USER myGLUser, decimal myLAsugangTableID)
        {
            Int32 Anzahl = 0;
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Artikel WHERE LAusgangTableID=" + myLAsugangTableID + ";";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "ArtikelInAusgang");
            return dt;
        }
        ///<summary>clsArtikel / GetArtikelInEingangByArtID</summary>
        ///<remarks>Ermittelt den angegebenen Artikel in einem Lagereingang.</remarks>
        public static DataTable GetArtikelInEingangByArtID(Globals._GL_USER myGLUser, decimal myArtID)
        {
            Int32 Anzahl = 0;
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Artikel WHERE ID=" + myArtID + ";";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "ArtikelInEingang");
            return dt;
        }
        ///<summary>clsArtikel / GetDataTableForArtikelGrdSplitting</summary>
        ///<remarks></remarks>
        public static DataTable GetDataTableForArtikelGrdSplitting(Globals._GL_USER myGLUser, decimal myAuftragPosTableID)
        {
            DataTable dataTable = new DataTable();
            string strSQL = string.Empty;
            strSQL = "SELECT " +
                            "CAST(0 as Bit) as 'Select'" +
                            ", e.Bezeichnung as Gut " +
                                ", a.Dicke as Dicke " +
                                ", a.Breite as Breite " +
                                ", a.Laenge as Länge " +
                                ", a.Hoehe as Höhe " +
                                ", a.Anzahl as Anzahl " +
                                //", a.gemGewicht as ArtikelGewicht " +
                                ", a.Netto as Netto " +
                                ", a.Brutto as Brutto " +
                                ", a.Werksnummer as Werksnummer " +
                                ", a.Produktionsnummer" +
                                ", a.ID as ID " +
                                ", a.AuftragPosTableID " +
                                // ", a.AuftragID " +
                                //", a.AuftragPos " +
                                "FROM Artikel a " +
                                "INNER JOIN Gueterart e ON e.ID = a.GArtID " +
                                "WHERE AuftragPosTableID=" + myAuftragPosTableID + ";";

            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Artikel");
            return dataTable;
        }
        ///<summary>clsArtikel / UpdateArtikelSped</summary>
        ///<remarks></remarks>
        public static DataTable GetDataTableForArtikelGrd(Globals._GL_USER myGLUser, Decimal myAuftragPosTableID)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT b.Bezeichnung as Gut " +
                                         ", a.GutZusatz as 'Gut Zusatz' " +
                                         ", a.Werksnummer as Werksnummer " +
                                         ", a.Dicke as Dicke " +
                                         ", a.Breite as Breite " +
                                         ", a.Laenge as Länge " +
                                         ", a.Hoehe as Höhe " +
                                         ", a.Anzahl as Anzahl " +
                                         ", a.gemGewicht as gemGewicht " +
                                         ", a.Netto as Netto " +
                                         ", a.Brutto as Brutto " +
                                         ", a.AuftragPosTableID " +
                                         ", a.Werksnummer as Werksnummer " +
                                         ", a.ID " +
                                         ", a.AuftragID " +
                                         ", a.AuftragPos " +
                                         ", a.GArtID " +
                                         "FROM Artikel a " +
                                         "LEFT JOIN Gueterart b ON b.ID = a.GArtID " +
                                         "WHERE a.LVS_ID=0 AND a.IsLagerArtikel=0 AND a.AuftragPosTableID=" + myAuftragPosTableID + ";";

            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Artikel");
            return dt;
        }
        ///<summary>clsArtikel.cs / GetStatusColumnSQL</summary>
        ///<remarks>Subquery für den Status</remarks>
        public static string GetStatusColumnSQL(string ausgang = "LAusgang", string eingang = "LEingang")
        {
            string strSql = string.Empty;
            string status10 = eingang + ".ID > 0 ";
            string status20 = eingang + ".IsPrintDoc > 0 ";
            string status30 = eingang + ".[check] > 0";
            string status40 = eingang + ".IsPrintAnzeige > 0 AND " + eingang + ".IsPrintDoc > 0 ";
            string status50 = ausgang + ".ID is NOT NULL AND " + ausgang + ".ID > 0 ";
            string status60 = ausgang + ".IsPrintDoc is NOT NULL AND " + ausgang + ".IsPrintDoc > 0 ";
            string status70 = ausgang + ".Checked is NOT NULL AND " + ausgang + ".Checked > 0 ";
            strSql = "CASE " +
                        "WHEN " + status70 + " AND " + status60 + " AND " + status50 + " AND " + status40 + " AND " + status30 + " AND " + status20 + " AND " + status10 +
                            "THEN 70 " +
                        "WHEN " + status60 + " AND " + status50 + " AND " + status40 + " AND " + status30 + " AND " + status20 + " AND " + status10 +
                            "THEN 60 " +
                        "WHEN " + status50 + " AND " + status40 + " AND " + status30 + " AND " + status20 + " AND " + status10 +
                            "THEN 50 " +
                        "WHEN " + status40 + " AND " + status30 + " AND " + status20 + " AND " + status10 +
                            "THEN 40 " +
                        "WHEN " + status30 + " AND " + status20 + " AND " + status10 +
                            "THEN 30 " +
                        "WHEN " + status20 + " AND " + status10 +
                            "THEN 20 " +
                        "WHEN " + status10 +
                            "THEN 10 " +
                        "ELSE 0 " +
                    "END as [Status] ";
            return strSql;
        }
        ///<summary>clsArtikel.cs / GetDataTableArtikelSchema</summary>
        ///<remarks></remarks>
        public static DataTable GetDataTableArtikelSchema(Globals._GL_USER myGLUser)
        {
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Artikel WHERE ID=0";
            DataTable dt = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Artikel");
            return dt;
        }
        ///<summary>clsArtikel.cs / GetDataTableArtikelForDocPrint</summary>
        ///<remarks></remarks>
        public static DataTable GetDataTableArtikelForDocPrint(Globals._GL_USER myGLUser, decimal myAuftragPosTableID)
        {
            DataTable dataTable = new DataTable("Artikel");
            string strSQL = string.Empty;
            strSQL = "SELECT * FROM Artikel WHERE AuftragPosTableID=" + myAuftragPosTableID + ";";
            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Artikel");
            return dataTable;
        }
        ///<summary>clsArtikel.cs / GetDataTableArtikelForDocPrint</summary>
        ///<remarks></remarks>
        public static string GetSQLUpdateArtikelFieldAuftragPosTableID(List<decimal> myList, decimal myAuftragPosTableID)
        {
            string strSql = string.Empty;
            if (myList.Count > 0)
            {
                string strArtikelIDList = string.Join(",", myList.ToArray());
                strSql = "Update Artikel " +
                                        "SET AuftragPosTableID= " + myAuftragPosTableID +
                                        " WHERE ID IN (" + strArtikelIDList + ") ;";
            }
            return strSql;
        }
        ///<summary>clsArtikel.cs / GetDataTableArtikelForDocPrint</summary>
        ///<remarks></remarks>
        public static DataTable GetArtikelStoredOutByAuftraggeber(Globals._GL_USER myGLUser, decimal myAdrID, decimal myAbBereichID)
        {
            DataTable dataTable = new DataTable("Artikel");
            string strSQL = string.Empty;
            strSQL = "SELECT " +
                                "CAST(0 as bit) as Selected" +
                                ",a.ID as ArtikelID " +
                                ",a.LVS_ID as LVSNr " +
                                ",a.Werksnummer " +
                                ",a.Produktionsnummer " +

                                " FROM Artikel a " +
                                "INNER JOIN LAusgang b on b.ID=a.LAusgangTableID " +
                                "WHERE " +
                                    "b.Auftraggeber = " + (Int32)myAdrID + " " +
                                    "AND b.AbBereich= " + (Int32)myAbBereichID + " " +
                                    "ORDER BY b.Datum desc";
            dataTable = clsSQLcon.ExecuteSQL_GetDataTable(strSQL, myGLUser.User_ID, "Artikel");
            return dataTable;
        }
        //
        //--------------- Update Artikel Brutto ----------------
        //
        public static void UpdateArtikelBrutto(Globals._GL_USER myGLUser, decimal artikelID, decimal BruttoGewicht)
        {
            string strSQL = "Update Artikel SET Brutto ='" + BruttoGewicht.ToString().Replace(",", ".") + "' " +
                                                              "WHERE ID=" + artikelID + ";";
            clsSQLcon.ExecuteSQL(strSQL, myGLUser.User_ID);
        }
        //
        //----------------- Auflösen einer AuftragPos / Artikel ----------------------
        //
        public static void UpdateArtikelNachAuftragPosAufloesung(Globals._GL_USER myGLUser, decimal myAuftragPosTableID)
        {
            /****************************************************************************
            * - Artikel einer Auftragsposition werden der Auftragspos. 0 zugeordnet
            *   ->Vorgehen:
            *      > Check Artikel (Güterart und Abmessungen) unter der Auftragsnummer noch einmal vorhanden
            *      > ja - und Position == 0 Mengen zusammenfassen unter Pos=0
            *      > ja - und Position != 0 Artikel Positionsupdate auf Pos=0
            *      > nein - Artikel Positionsupdate auf Pos=0
            ****************************************************************************/

            //Artikel der Position
            DataTable dtArtikel = new DataTable("Artikel");
            dtArtikel = clsArtikel.GetDataTableForArtikelGrd(myGLUser, myAuftragPosTableID);

            for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
            {

                clsArtikel clsArt = new clsArtikel();
                clsArt._GL_User = myGLUser;
                clsArt.ID = (decimal)dtArtikel.Rows[i]["ID"];
                clsArt.SetArtikelDaten(ref clsArt, dtArtikel, i);
                clsArt.AuftragPosTableID = clsArt.GetAuftragPosTableIDFromAuftragPosZero(myAuftragPosTableID);
                if (clsArt.CheckArtikelByAuftragAuftragPosANDAbmessungen(ref clsArt))
                {
                    string strSQL = string.Empty;

                    //ME und Gewicht addieren
                    strSQL = "Update Artikel " +
                                    "SET " +
                                    "Anzahl=Anzahl+(Select a.Anzahl FROM Artikel a WHERE a.ID=" + clsArt.ID + ") " +
                                    ", Netto = Netto + (Select b.Netto From Artikel b WHERE b.ID=" + clsArt.ID + ") " +
                                    ", Brutto = Netto + (Select c.Brutto FROM Artikel c WHERE c.ID=" + clsArt.ID + ") " +
                                    ", gemGewicht = gemGewicht +(Select d.gemGewicht From Artikel d WHERE d.ID=" + clsArt.ID + ") " +
                                    "WHERE ID =" + clsArt.IDforUnion + ";";


                    //alten Artikel löschen
                    strSQL = strSQL + "DELETE FROM Artikel WHERE ID=" + clsArt.ID + ";";

                    bool mybExecOK = clsSQLcon.ExecuteSQLWithTRANSACTION(strSQL, "AuftragPosAuflösen", myGLUser.User_ID);

                    //Logbuch Eintrag
                    if (mybExecOK)
                    {
                        //Add Logbucheintrag 
                        string myBeschreibung = "Artikel gelöscht: Artikel ID [" + clsArt.ID.ToString() + "] ";
                        Functions.AddLogbuch(myGLUser.User_ID, enumLogbuchAktion.Loeschung.ToString(), myBeschreibung);
                    }
                }
                else
                {
                    //Update der Auftragsposition
                    clsArt.AuftragPos = 0;
                    clsArt.UpdateArtikelAuftragPosByID();
                }
            }
        }
        //
        //--- Update Artikel - Auftragsplittung - > AuftragPos wird geändert
        //
        public void UpdateArtikelAuftragPosByID()
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("Update Artikel SET AuftragPos ='" + AuftragPos + "', " +
                                                              "AuftragPosTableID ='" + AuftragPosTableID + "' " +
                                                              "WHERE ID='" + ID + "'");
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
        //
        //
        public bool CheckArtikelByAuftragAuftragPosANDAbmessungen(ref clsArtikel clsArt)
        {
            bool retVal = false;

            try
            {
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand();

                Command.Connection = Globals.SQLcon.Connection;
                ada.SelectCommand = Command;
                Command.CommandText = "SELECT ID FROM Artikel WHERE AuftragID='" + AuftragID + "' AND " +
                                                                    "AuftragPos='0' AND " +
                                                                    "GArt='" + Gut + "' AND " +
                                                                    "Dicke='" + Dicke.ToString().Replace(",", ".") + "' AND " +
                                                                    "Breite='" + Breite.ToString().Replace(",", ".") + "' AND " +
                                                                    "Laenge='" + Laenge.ToString().Replace(",", ".") + "' AND " +
                                                                    "Hoehe='" + Hoehe.ToString().Replace(",", ".") + "'";

                Globals.SQLcon.Open();
                object obj = Command.ExecuteScalar();
                if ((obj == null) | (obj is DBNull))
                {
                    retVal = false;
                }
                else
                {
                    retVal = true;
                    clsArt.IDforUnion = (decimal)obj;
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
                decimal decUser = -1.0M;
                Functions.AddLogbuch(decUser, "CheckArtikelByAuftragAuftragPosANDAbmessungen", ex.ToString());
            }
            return retVal;
        }
        //
        //
        //
        public void SetArtikelDaten(ref clsArtikel clsArt, DataTable dt, Int32 i)
        {
            DataTable dtArtikel = dt;
            clsArt.ID = (decimal)dtArtikel.Rows[i]["ID"];
            clsArt.AuftragID = (decimal)dtArtikel.Rows[i]["AuftragID"];
            clsArt.AuftragPos = (decimal)dtArtikel.Rows[i]["AuftragPos"];
            clsArt.Gut = (string)dtArtikel.Rows[i]["Gut"];
            clsArt.gemGewicht = (decimal)dtArtikel.Rows[i]["gemGewicht"];
            clsArt.Netto = (decimal)dtArtikel.Rows[i]["Netto"];
            clsArt.Brutto = (decimal)dtArtikel.Rows[i]["Brutto"];
            clsArt.Anzahl = Convert.ToInt32(dtArtikel.Rows[i]["Anzahl"].ToString());
            clsArt.Werksnummer = dtArtikel.Rows[i]["Werksnummer"].ToString();
            clsArt.AuftragPosTableID = (decimal)dtArtikel.Rows[i]["AuftragPosTableID"];
            decimal dicke = 0.0M;
            decimal breite = 0.0M;
            decimal hoehe = 0.0M;
            decimal laenge = 0.0M;

            if (!decimal.TryParse(dtArtikel.Rows[i]["Dicke"].ToString(), out dicke))
            {
                dicke = 0.0M;
            }
            if (!decimal.TryParse(dtArtikel.Rows[i]["Länge"].ToString(), out laenge))
            {
                laenge = 0.0M;
            }
            if (!decimal.TryParse(dtArtikel.Rows[i]["Breite"].ToString(), out breite))
            {
                breite = 0.0M;
            }
            if (!decimal.TryParse(dtArtikel.Rows[i]["Höhe"].ToString(), out hoehe))
            {
                hoehe = 0.0M;
            }
            clsArt.Dicke = dicke;
            clsArt.Breite = breite;
            clsArt.Laenge = laenge;
            clsArt.Hoehe = hoehe;

        }
        //
        //--- Update Artikel - AuftragPos wird aufgelöst - Artikel Pos=0 zugewiesen ----
        //
        public static void UpdateArtikelChangeAuftragPosToZero(decimal Auftrag, decimal AuftragPos)
        {
            try
            {
                //--- initialisierung des sqlcommand---
                SqlCommand Command = new SqlCommand();
                Command.Connection = Globals.SQLcon.Connection;

                //----- SQL Abfrage -----------------------
                Command.CommandText = ("Update Artikel SET AuftragPos ='0' WHERE AuftragID='" + Auftrag + "' AND AuftragPos='" + AuftragPos + "'");
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
        public decimal GetAuftragPosTableIDFromAuftragPosZero(decimal myAuftragPosTableIDToDelete)
        {
            string strSQL = string.Empty;
            strSQL = "Select ID FROM AuftragPos a WHERE a.AuftragTableID=" +
                                    "(Select b.AuftragTableID FROM AuftragPos b WHERE b.ID=" + myAuftragPosTableIDToDelete + ")" +
                                    " AND AuftragPos=0 ;";

            Decimal decTmp = 0;
            string strTmp = clsSQLcon.ExecuteSQL_GetValue(strSQL, BenutzerID);
            Decimal.TryParse(strTmp, out decTmp);
            return decTmp;
        }
        //<summary>clsArtikel / GetArtikelForAbrufByAuftraggeberAndArtIDRef</summary>
        ///<remarks></remarks>
        public static DataTable GetArtikelForAbrufByAuftraggeberAndArtIDRef(decimal myBenutzer, decimal myAuftraggeber, List<string> myListArtIDRef, bool bIsAlreadyOUT = false)
        {
            DataTable dt = new DataTable();
            string strSql = string.Empty;

            StringBuilder builder = new StringBuilder();
            List<string> tmpList = new List<string>();
            foreach (string ArtRef in myListArtIDRef)
            {
                builder.Append("'").Append(ArtRef).Append("'");
                tmpList.Add(builder.ToString());
                builder.Clear();
            }

            string strArtIDRefList = string.Join(",", tmpList.ToArray());
            strSql = "SELECT a.ID, a.Charge, a.Brutto, a.ArtIDRef " +
                                    " FROM Artikel a " +
                                        "INNER JOIN LEingang b ON b.ID=a.LEingangTableID " +
                                            " WHERE b.Auftraggeber=" + myAuftraggeber + " ";

            if (!bIsAlreadyOUT) { strSql += "AND a.LAusgangTableID=0 "; }
            else { }
            strSql += "AND a.CheckArt=1 " +
                                                    "AND b.[Check]=1 " +
                                                    "AND LTRIM(RTRIM(a.Charge))+'#'+LTRIM(RTRIM(a.exAuftrag))+'#'+LTRIM(RTRIM(a.exAuftragPos)) IN (" + strArtIDRefList + ") ;";

            dt = clsSQLcon.ExecuteSQL_GetDataTable(strSql, myBenutzer, "Artikel");
            return dt;
        }
        ///<summary>clsArtikel / ExistIDRef</summary>
        ///<remarks></remarks>
        public static bool ExistIDRef(string myArtIDRef, decimal myBenutzerID)
        {
            string sql = "Select * from Artikel where ArtIDRef='" + myArtIDRef + "'";
            DataTable dtTmp = clsSQLcon.ExecuteSQL_GetDataTable(sql, myBenutzerID, "Artikel");
            return (dtTmp.Rows.Count > 0);
        }
        ///<summary>clsArtikel / CombinateValue</summary>
        ///<remarks></remarks>
        public void CombinateValue(string propDestination, List<string> listPropSource)
        {
            string strSetVal = string.Empty;
            for (Int32 i = 0; i <= listPropSource.Count - 1; i++)
            {
                if (strSetVal.Length == 0)
                {
                    if (this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null) != null)
                    {
                        strSetVal = strSetVal + this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null).ToString();
                    }
                }
                else
                {
                    if (this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null) != null)
                    {
                        strSetVal = strSetVal + clsArtikel.Artikel_ValSeparator + this.GetType().GetProperty(listPropSource[i].ToString()).GetValue(this, null).ToString();
                    }
                }
            }
            this.GetType().GetProperty(propDestination).SetValue(this, strSetVal, null);
            //string strTest = this.Werksnummer;
        }
        ///<summary>clsArtikel / CombinateValue</summary>
        ///<remarks></remarks>
        public void CopyArtValue(string propDestination, ref clsArtikel SourceArt)
        {
            string strProperty = propDestination.Remove(0, propDestination.IndexOf('.') + 1);
            try
            {
                string strValue = SourceArt.GetType().GetProperty(strProperty).GetValue(SourceArt, null).ToString();

                this.GetType().GetProperty(strProperty).SetValue(this, SourceArt.GetType().GetProperty(strProperty).GetValue(SourceArt, null), null);
            }
            catch (Exception ex)
            { }
        }
        ///<summary>clsArtikel / CombinateValue</summary>
        ///<remarks></remarks>
        public void SetArtValue(string propDestination, string strReplaceValue)
        {
            string strProperty = propDestination.Remove(0, propDestination.IndexOf('.') + 1);
            try
            {
                this.GetType().GetProperty(strProperty).SetValue(this, strReplaceValue, null);
                //this.GetType().GetProperty(strProperty).SetValue(this, SourceArt.GetType().GetProperty(strProperty).GetValue(SourceArt, null), null);
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtikel"></param>
        /// <param name="myDicChanges"></param>
        /// <returns></returns>
        public static clsArtikel ChangeArtikelPorpertiesToOldValue(clsArtikel myArtikel, Dictionary<string, clsObjPropertyChanges> myDicChanges)
        {
            clsArtikel retArtikel = myArtikel.Copy();
            if (myDicChanges.Count > 0)
            {
                Type typeArtikel = myArtikel.GetType();
                PropertyInfo[] pInfoArtikel = typeArtikel.GetProperties();

                Type typeArtikelChanged = retArtikel.GetType();
                PropertyInfo[] pInfoArtikelChanged = typeArtikelChanged.GetProperties();

                foreach (KeyValuePair<string, clsObjPropertyChanges> itm in myDicChanges)
                {
                    string strKey = itm.Key;
                    string strProperty = strKey.Replace(clsObjPropertyChanges.TableName_Artikel + ".", "");
                    clsObjPropertyChanges tmpOPC = (clsObjPropertyChanges)itm.Value;

                    string ValueOld = string.Empty;
                    switch (strKey)
                    {
                        case clsArtikel.ArtikelField_Bestellnummer:
                        case clsArtikel.ArtikelField_Charge:
                        case clsArtikel.ArtikelField_exAuftrag:
                        case clsArtikel.ArtikelField_exAuftragPos:
                        case clsArtikel.ArtikelField_exBezeichnung:
                        case clsArtikel.ArtikelField_exMaterialnummer:
                        case clsArtikel.ArtikelField_Gut:
                        case clsArtikel.ArtikelField_Produktionsnummer:
                        case clsArtikel.ArtikelField_Werksnummer:
                        case clsArtikel.ArtikelField_Einheit:
                        case clsArtikel.ArtikelField_Werk:
                        case clsArtikel.ArtikelField_Reihe:
                        case clsArtikel.ArtikelField_Ebene:
                        case clsArtikel.ArtikelField_Platz:
                        case clsArtikel.ArtikelField_Halle:
                            typeArtikel.GetProperty(strProperty).SetValue(retArtikel, tmpOPC.ValueOld, null);
                            break;

                        case clsArtikel.ArtikelField_Anzahl:
                            int iValueOld = 0;
                            int.TryParse(tmpOPC.ValueOld, out iValueOld);
                            typeArtikel.GetProperty(strProperty).SetValue(retArtikel, iValueOld, null);
                            break;

                        case clsArtikel.ArtikelField_Dicke:
                        case clsArtikel.ArtikelField_Breite:
                        case clsArtikel.ArtikelField_Länge:
                        case clsArtikel.ArtikelField_Höhe:
                        case clsArtikel.ArtikelField_Netto:
                        case clsArtikel.ArtikelField_Brutto:
                            decimal decValueOld = 0;
                            decimal.TryParse(tmpOPC.ValueOld, out decValueOld);
                            typeArtikelChanged.GetProperty(strProperty).SetValue(retArtikel, decValueOld, null);
                            break;
                    }
                }
            }
            return retArtikel;
        }
    }
}
