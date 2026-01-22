using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using LVS;

namespace LVS
{
    public class clsVDACreate
    {
        internal SIL_spFunc silFunc = new SIL_spFunc();
        internal SZG_spFunc szgFunc = new SZG_spFunc();

        internal clsStringCheck StrCheck = new clsStringCheck();
        internal clsVDAClientWorkspaceValue VDAClientWorkspaceValue;

        /*************************************************************************
         *                      spezielle Client Function
         * **********************************************************************/
        public const string const_cFunction_VDAClientConstValue = "#VDAClientConstValue#";
        
        public const string const_cFunction_SIL_716F03 = "#SIL_716F03#";
        public const string const_cFunction_SIL_ProdNrCHeck = "#SIL_SIL_ProdNrCHeck#";

        public const string const_cFunction_SZG_TMN = "#SZG_TMN#";

        public const string const_Reciever = "#Receiver#";
        public const string const_Sender = "#Sender#";
        public const string const_Lieferantennummer = "#Lieferantennummer#";
        public const string const_SIDOld = "#SIDOld#";
        public const string const_SIDNew = "#SIDNew#";
        public const string const_Vorgang = "#VGS#";
        public const string const_TransportMittelSchlüssel = "#TMS#";    //Satz712F14
        public const string const_TransportMittelNummer = "#TMN#";       //Satz712F15
        public const string const_VDA_Value_NOW = "#NOW#";              // DAtum YYmmdd
        public const string const_VDA_Value_TimeNow = "#TIMENOW#";              // DAtum YYmmdd
        public const string const_VDA_Value_NOWTIME = "#NOWTIME#";
        public const string const_VDA_Value_Blanks = "#BLANKS#";
        public const string const_VDA_Value_const = "const";

        public const string const_TransportMittelNummerTrim = "#TMNTrim#";

        /*******************************************************************************
         *                  function auf Artikeldaten
         * ****************************************************************************/
        public const string const_ArtFunc_WerksnummerOhneBlank = "#WerksnummerOhneBlank#";
        public const string const_ArtFunc_WerksnummerMitFBlank = "#WerksnummerMitFBlank#";
        public const string const_ArtFunk_WerksnummerFormatVW = "#WerksnummerVWFormat#";
        public const string const_ArtFunc_BruttoKGorSt = "#BruttoKGorST#";
        public const string const_ArtFunc_NettoKGorSt = "#NettoKGorST#";
        public const string const_ArtFunc_EinheitKGorSt = "#EinheitKGorSt#";
        public const string const_ArtFunc_ProduktionsnummerFillTo9With0 = "#ProduktionsnummerFillTo9With0#";
        public const string const_ArtFunc_Anzahlx1000 = "#Anzahlx1000#";


        public static List<string> ListValue_Functions
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_cFunction_VDAClientConstValue
                    ,const_cFunction_SIL_716F03
                    ,const_cFunction_SIL_ProdNrCHeck
                    ,const_cFunction_SZG_TMN
                    ,const_Reciever
                    ,const_Sender
                    ,const_Lieferantennummer
                    ,const_SIDOld
                    ,const_SIDNew
                    ,const_Vorgang
                    ,const_TransportMittelSchlüssel
                    ,const_TransportMittelNummer
                    ,const_TransportMittelNummerTrim
                    ,const_VDA_Value_NOW
                    ,const_VDA_Value_TimeNow
                    ,const_VDA_Value_Blanks
                    ,const_ArtFunc_WerksnummerOhneBlank
                    ,const_ArtFunc_WerksnummerMitFBlank
                    ,const_ArtFunk_WerksnummerFormatVW
                    ,const_ArtFunc_BruttoKGorSt
                    ,const_ArtFunc_NettoKGorSt
                    ,const_ArtFunc_EinheitKGorSt
                    ,const_ArtFunc_ProduktionsnummerFillTo9With0
                    ,const_ArtFunc_Anzahlx1000
                    ,const_EFunc_LieferantennummerDeleteSlash

                    ,const_VDA_Value_const
                };
                tmp.Sort();
                return tmp;
            }
        }


        /*************************************************************************
         *                LEingangsdaten
         * **********************************************************************/
        public const string const_Eingang_ID = "Eingang.ID";
        public const string const_Eingang_EingangID = "Eingang.EingangID";
        public const string const_Eingang_Datum = "Eingang.Datum";
        public const string const_Eingang_LfsNr = "Eingang.LfsNr";
        public const string const_Eingang_KFZ = "Eingang.KFZ";
        public const string const_Eingang_Waggon = "Eingang.Waggon";        
        public const string const_Eingang_ExTransportRef = "Eingang.ExTransportRef";
        public const string const_Eingang_ExAuftragRef = "Eingang.ExAuftragRef";
        public const string const_Eingang_Brutto = "Eingang.Brutto";
        public const string const_Eingang_Netto = "Eingang.Netto";
        public const string const_Eingang_Anzahl = "Eingang.Anzahl";


        public static List<string> ListValue_Eingang
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_Eingang_ID
                    ,const_Eingang_EingangID
                    ,const_Eingang_Datum
                    ,const_Eingang_LfsNr
                    ,const_Eingang_KFZ
                    ,const_Eingang_Waggon
                    ,const_Eingang_ExTransportRef
                    ,const_Eingang_ExAuftragRef
                    ,const_Eingang_Brutto
                    ,const_Eingang_Netto
                    ,const_Eingang_Anzahl

                };
                tmp.Sort();
                return tmp;
            }
        }


        public const string const_EFunc_LieferantennummerDeleteSlash = "#LiefNrDeleteSlash#";

        /*************************************************************************
         *                   EA Daten 
         * **********************************************************************/
        public const string const_EA_ID = "EA.ID";
        public const string const_EA_EANr = "EA.Nr";
        public const string const_EA_Datum = "EA.Datum";
        public const string const_EA_LfsNr = "EA.LfsNr";
        public const string const_EA_ExTransportRef = "EA.ExTransportRef";
        public const string const_EA_ExAuftragRef = "EA.ExAuftragRef";
        public const string const_EA_Brutto = "EA.Brutto";
        public const string const_EA_Netto = "EA.Netto";
        public const string const_EA_Anzahl = "EA.Anzahl";
        public const string const_EA_Termin = "EA.Termin";
        public const string const_EA_SLB = "EA.SLB";
        public const string const_EA_MATDate = "EA.MATDate";
        public const string const_EA_MATTime = "EA.MATTime";


        public static List<string> ListValue_EA
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_EA_ID
                    ,const_EA_EANr
                    ,const_EA_Datum
                    ,const_EA_LfsNr
                    ,const_EA_ExTransportRef
                    ,const_EA_ExAuftragRef
                    ,const_EA_Brutto
                    ,const_EA_Netto
                    ,const_EA_Anzahl
                    ,const_EA_Termin
                    ,const_EA_SLB
                    ,const_EA_MATDate
                    ,const_EA_MATTime
                };
                tmp.Sort();
                return tmp;
            }
        }


        /**************************************************************************
         *           Artikeldaten
         * ***********************************************************************/
        public const string const_Artikel_ID = "Artikel.ID";
        public const string const_Artikel_LVSNr = "Artikel.LVS_ID";
        public const string const_Artikel_Netto = "Artikel.Netto";
        public const string const_Artikel_Brutto = "Artikel.Brutto";
        public const string const_Artikel_Gut = "Artikel.Gut";
        public const string const_Artikel_Dicke = "Artikel.Dicke";
        public const string const_Artikel_Breite = "Artikel.Breite";
        public const string const_Artikel_Laenge = "Artikel.Laenge";
        public const string const_Artikel_Anzahl = "Artikel.Anzahl";
        public const string const_Artikel_Einheit = "Artikel.Einheit";
        public const string const_Artikel_Werksnummer = "Artikel.Werksnummer";
        public const string const_Artikel_Produktionsnummer = "Artikel.Produktionsnummer";
        public const string const_Artikel_Charge = "Artikel.Charge";
        public const string const_Artikel_BestellNr = "Artikel.BestellNr";
        public const string const_Artikel_exMaterialNr = "Artikel.ExMaterialnummer";
        public const string const_Artikel_Pos = "Artikel.Pos";
        public const string const_Artikel_AbrufRef = "Artikel.AbrufRef";
        public const string const_Artikel_TARef = "Artikel.TARef";
        public const string const_Artikel_ArtIDRef = "Artikel.ArtIDRef";
        public const string const_Artikel_SchadenTopOne = "#SchadenText#";


        public static List<string> ListValue_Artikel
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_Artikel_ID
                    ,const_Artikel_LVSNr
                    ,const_Artikel_Netto
                    ,const_Artikel_Brutto
                    ,const_Artikel_Gut
                    ,const_Artikel_Dicke
                    ,const_Artikel_Breite
                    ,const_Artikel_Laenge
                    ,const_Artikel_Anzahl
                    ,const_Artikel_Einheit
                    ,const_Artikel_Werksnummer
                    ,const_Artikel_Produktionsnummer
                    ,const_Artikel_Charge
                    ,const_Artikel_BestellNr
                    ,const_Artikel_exMaterialNr
                    ,const_Artikel_Pos
                    ,const_Artikel_AbrufRef
                    ,const_Artikel_TARef
                    ,const_Artikel_ArtIDRef
                    ,const_Artikel_SchadenTopOne
                };
                tmp.Sort();
                return tmp;
            }
        }


        /*************************************************************************
         *                      allgemeine Function
         * **********************************************************************/


        public static DataTable GetInputSelections()
        {
            DataTable dt = new DataTable("InputSelections");
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Art", typeof(string));

            DataRow row;
            //-- Artikel
            foreach (string itm in clsVDACreate.ListValue_Artikel)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "Artikel";
                dt.Rows.Add(row);
            }
            //-- EA
            foreach (string itm in clsVDACreate.ListValue_EA)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "EA";
                dt.Rows.Add(row);
            }
            //-- Eingang
            foreach (string itm in clsVDACreate.ListValue_Eingang)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "Eingang";
                dt.Rows.Add(row);
            }
            //-- Functions
            foreach (string itm in clsVDACreate.ListValue_Functions)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "Function";
                dt.Rows.Add(row);
            }
            return dt;
        }


        private string prozess;

        public string Prozess
        {
            get { return prozess; }
            set { prozess = value; }
        }





        public Globals._GL_SYSTEM GLSystem;
        public Globals._GL_USER GL_User;
        public clsSystem Sys;
        //************  User  ***************
        private decimal _BenutzerID;
        public decimal BenutzerID
        {
            get
            {
                _BenutzerID = GL_User.User_ID;
                return _BenutzerID;
            }
            set { _BenutzerID = value; }
        }

        public List<string> ListVDASatzString;
        internal clsVDAClientValue VDAClientVal;
        internal clsADRVerweis AdrVerweis;
        internal clsLagerdaten Lager;
        internal clsASN ASN;
        internal clsOrga Orga;

        internal clsASNArtSatzFeld s711Field;
        internal clsASNArtSatzFeld s712Field;
        internal clsASNArtSatzFeld s713Field;
        internal clsASNArtSatzFeld s714Field;
        internal clsASNArtSatzFeld s715Field;
        internal clsASNArtSatzFeld s716Field;
        internal clsASNArtSatzFeld s717Field;
        internal clsASNArtSatzFeld s718Field;
        internal clsASNArtSatzFeld s719Field;

        internal DataTable dtEingang = new DataTable();
        internal DataTable dtAusgang = new DataTable();
        internal DataTable dtArtikel = new DataTable();

        public List<clsLogbuchCon> ListErrorVDA = new List<clsLogbuchCon>();

        internal Int32 iNextSatz = 0;
        internal Int32 iArtSatzCount = 1;
        private string _SENDER;
        public string SENDER
        {
            get
            {
                //string strTmp = string.Empty;
                //string sql = "select LieferantenVerweis from ADRVerweis where VerweisAdrID=" + ASN.Job.AdrVerweisID + " AND SenderAdrID=" + ASN.Job.AdrVerweisID;
                //strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, this.BenutzerID);
                //_SENDER = strTmp;
                return _SENDER;
            }
            private set { }
        }

        private string _LIEFERANT;
        public string LIEFERANT
        {
            get
            {
                string strTmp = string.Empty;
                //string sql = "select LieferantenVerweis from ADRVerweis where VerweisAdrID=" + ASN.Job.AdrVerweisID + " AND SenderAdrID=" +ASN. ;
                //strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, this.BenutzerID);
                //string[] array = strTmp.Split('#');
                //strTmp = string.Empty;
                //if (array.Length == 3)
                //{
                //    strTmp = array[0];
                //}
                ////_LIEFERANT = FillValueWithstringToLenth(" ", strTmp, s712Field.Length, false);
                //_LIEFERANT = strTmp;
                return strTmp;
            }
            private set { }
        }
        private string GetLieferantenNummer(ref clsLagerdaten myLager)
        {
            string strRetrun = string.Empty;

            string strTmp = string.Empty;
            string sql = string.Empty;
            if (myLager.Ausgang != null)
            {
                sql = "select LieferantenVerweis from ADRVerweis where VerweisAdrID=" + (Int32)ASN.Job.AdrVerweisID + " AND SenderAdrID=" + (Int32)myLager.Ausgang.Auftraggeber;
            }
            else
            {

                sql = "select LieferantenVerweis from ADRVerweis where VerweisAdrID=" + (Int32)ASN.Job.AdrVerweisID + " AND SenderAdrID=" + (Int32)myLager.Eingang.Auftraggeber;
            }
            strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, this.BenutzerID);
            strRetrun = strTmp;
            return strRetrun;
        }

        private string _EMPFAENGER;
        public string EMPFAENGER
        {
            get
            {
                string strTmp = string.Empty;
                string sql = "select Verweis from ADRVerweis where VerweisAdrID=" + ASN.Job.AdrVerweisID + " AND SenderAdrID=" + ASN.Job.AdrVerweisID;
                strTmp = LVS.clsSQLcon.ExecuteSQL_GetValue(sql, this.BenutzerID);
                string[] array = strTmp.Split('#');
                strTmp = string.Empty;
                if (array.Length == 3)
                {
                    strTmp = array[0];
                }
                //_EMPFAENGER = FillValueWithstringToLenth(" ", strTmp, s711Field.Length, false);
                _EMPFAENGER = strTmp;
                return _EMPFAENGER;
            }
            private set { }
        }
        //Interne Variablen
        private string _SIDOld;
        public string SIDOld
        {
            get
            {
                string strTmp = string.Empty;
                if (Orga != null)
                {
                    strTmp = Orga.SendNrOld.ToString();
                }
                //_SIDOld = FillValueWithstringToLenth(true,"0", strTmp, s711Field.Length, true);
                _SIDOld = strTmp;
                return _SIDOld;
            }
            set { _SIDOld = value; }
        }
        private string _SIDNew;
        public string SIDNew
        {
            get
            {
                string strTmp = string.Empty;
                strTmp = Orga.SendNrOld.ToString();
                if (Orga != null)
                {
                    strTmp = Orga.SendNrNew.ToString();
                }
                //_SIDNew = FillValueWithstringToLenth("0", strTmp, s711Field.Length, true);
                _SIDNew = strTmp;
                return _SIDNew;
            }
            set { _SIDNew = value; }
        }
        private string _NOW;
        public string NOW
        {
            get
            {
                string strYear = String.Format("{0:yy}", DateTime.Now);
                string strMonth = String.Format("{0:MM}", DateTime.Now);
                string strDay = String.Format("{0:dd}", DateTime.Now);
                _NOW = strYear + strMonth + strDay;
                return _NOW;
            }
            set { _NOW = value; }
        }
        private string _NOWTIME;
        public string NOWTIME
        {
            get
            {
                string strHour = String.Format("{0:HH}", DateTime.Now);
                string strMinute = String.Format("{0:mm}", DateTime.Now);
                _NOWTIME = strHour + strMinute;
                return _NOWTIME;
            }
            set { _NOWTIME = value; }
        }
        private string _BLKANKS;
        public string BLKANKS
        {
            get
            {
                //_BLKANKS = FillValueWithstringToLenth(" ", "", s711Field.Length, true);
                return _BLKANKS;
            }
            set { _BLKANKS = value; }
        }
        public string VerwLieferant
        {
            get
            {
                return string.Empty;
            }
        }
        public string VGS { get; set; }
        //public string Count711 { get; set; }
        //public string Count712 { get; set; }
        //public string Count713 { get; set; }
        //public string Count714 { get; set; }
        //public string Count715 { get; set; }
        //public string Count716 { get; set; }
        //public string Count717 { get; set; }
        //public string Count718 { get; set; }
        //public string Count719 { get; set; }

        public string Count711 = "0";
        public string Count712 = "0";
        public string Count713 = "0";
        public string Count714 = "0";
        public string Count715 = "0";
        public string Count716 = "0";
        public string Count717 = "0";
        public string Count718 = "0";
        public string Count719 = "0";

        internal clsLogbuchCon tmpLog = new clsLogbuchCon();


        public string strFileID { get; set; }

        /*****************************************************************
         *          Methoden  /  Prozedures
         * **************************************************************/
        ///<summary>clsVDACreate / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, clsJobs myJob, clsQueue myQueue, clsSystem mySys)
        {
            ListErrorVDA = new List<clsLogbuchCon>();
            tmpLog = new clsLogbuchCon();
            tmpLog.GL_User = myGLUser;
            tmpLog.Typ = Globals.enumLogArtItem.ERROR.ToString();
            tmpLog.TableName = "Queue";
            tmpLog.TableID = myQueue.ID;

            ListVDASatzString = new List<string>();

            this.GL_User = myGLUser;
            this.Sys = mySys;
            this.ASN = new clsASN();
            this.ASN.InitClass(this.GLSystem, this.GL_User);
            this.ASN.Sys = this.Sys;
            ASN.ASNArt.ID = myJob.ASNArtID;
            ASN.ASNArt.Fill();
            ASN.Queue = myQueue;
            ASN.Job = myJob;

            VDAClientVal = new clsVDAClientValue();
            VDAClientVal.GL_User = this.GL_User;
            VDAClientVal.InitClass(this.GL_User, myJob.AdrVerweisID);

            //prüfen, ob für diese Cliente VDA Definitionen vorliegen
            if (VDAClientVal.DictVDAClientValue.Count > 0)
            {
                this.ASN.ASNArt.asnSatz.ASNArtID = this.ASN.ASNArt.ID;
                this.ASN.ASNArt.asnSatz.FillbyASNArtID();
                //Das DictVDA4913Satz enthält nun alle Definitionen der verschiedenen Sätze
                this.ASN.ASNArt.asnSatz.FillListSatzAndDictVDA4913Satz();

                dtEingang = new DataTable();
                dtAusgang = new DataTable();
                dtArtikel = new DataTable();

                Lager = new clsLagerdaten();
                Lager.GLUser = this.GL_User;

                if (
                    (this.ASN.Job.AsnTyp.Typ.Equals("BME")) ||
                    (this.ASN.Job.AsnTyp.Typ.Equals("BML"))
                  )
                {
                    dtEingang = new DataTable();
                    dtAusgang = new DataTable();
                    dtArtikel = Lager.GetArtikelForBM(this.ASN.Job);

                    if (dtArtikel.Rows.Count > 0)
                    {
                        Orga = new clsOrga();
                        Orga.GL_User = this.GL_User;
                        Orga.AdrID = ASN.Job.AdrVerweisID;
                        Orga.FillByAdrID();
                        Orga.UpdateSendNr();
                        if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
                        {
                            CreateVDA();
                        }
                        else
                        {
                            CreateVDAFirst();
                        }

                        if (this.Sys.DebugModeCOM)
                        {
                            //---info mit ausgeben zur kontrolle
                            string strTmp = string.Empty;
                            decimal decBrutto = 0;
                            decimal decNetto = 0;
                            int iAnzahl = 0;

                            object objBrutto;
                            objBrutto = dtArtikel.Compute("SUM(Brutto)", "");
                            object objNetto;
                            objNetto = dtArtikel.Compute("SUM(Netto)", "");
                            object objAnzahl;
                            objAnzahl = dtArtikel.Compute("COUNT(ID)", "");


                            int.TryParse(objAnzahl.ToString(), out iAnzahl);
                            strTmp = string.Empty;
                            strTmp = "Anzahl: " + iAnzahl.ToString();
                            this.ListVDASatzString.Add(strTmp);

                            Decimal.TryParse(objBrutto.ToString(), out decBrutto);
                            strTmp = string.Empty;
                            strTmp = "Gesamt-Brutto: " + (decBrutto / 1000).ToString();
                            this.ListVDASatzString.Add(strTmp);

                            Decimal.TryParse(objNetto.ToString(), out decNetto);
                            strTmp = string.Empty;
                            strTmp = "Gesamt-Netto: " + (decNetto / 1000).ToString();
                            this.ListVDASatzString.Add(strTmp);
                        }

                        //---neues Datum setzen
                        //if (!this.Sys.DebugModeCOM)
                        //{
                            if (this.ListErrorVDA.Count == 0)
                            {
                                //enumPeriode tmpP = enumPeriode.immer;
                                enumPeriode tmpP = (enumPeriode)Enum.Parse(typeof(enumPeriode), this.ASN.Job.Periode);
                                DateTime dtActionDate = DateTime.Now; 
                                TimeSpan dtDiff = new TimeSpan();
                                dtDiff = (dtActionDate - this.ASN.Job.ActionDate);
                                switch (tmpP)
                                {
                                    case enumPeriode.täglich:
                                        if (dtDiff.Days >- 1)
                                        {
                                            dtActionDate = this.ASN.Job.ActionDate.AddDays(1);
                                        }
                                        break;
                                    case enumPeriode.wöchtentlich:
                                        if (dtDiff.Days == 7)
                                        {
                                            dtActionDate = this.ASN.Job.ActionDate.AddDays(7);
                                        }
                                        break;

                                    case enumPeriode.monatlich:
                                        if (dtDiff.Days < 32)
                                        {
                                            dtActionDate = this.ASN.Job.ActionDate.AddMonths(1);
                                        }
                                        else
                                        {
                                            dtActionDate = Convert.ToDateTime(this.ASN.Job.ActionDate.Day.ToString() + "." + DateTime.Now.AddMonths(1).Month.ToString() + "." + DateTime.Now.AddMonths(1).Year.ToString());
                                        }
                                        break;

                                    case enumPeriode.jährlich:
                                        if (dtDiff.Days < 366)
                                        {
                                            dtActionDate = this.ASN.Job.ActionDate.AddYears(1);
                                        }
                                        else
                                        {
                                            dtActionDate = Convert.ToDateTime(this.ASN.Job.ActionDate.Day.ToString() + "." + this.ASN.Job.ActionDate.Month.ToString() + "." + DateTime.Now.AddYears(1).Year.ToString());
                                        }
                                        break;

                                    case enumPeriode.immer:
                                        //this.ASN.Job.ActionDate = this.ASN.Job.ActionDate.AddDays(1);
                                        break;
                                }
                                this.ASN.Job.ActionDate = dtActionDate;
                                //-- Update in DB durchführen
                                this.ASN.Job.Update();
                            }
                    }
                    else
                    {
                        //string strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] -> Tablename:[" + myQueue.TableName + "] - TableID[" + myQueue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!"; ;
                        //strError = CreateQueueDetailsString(ref myQueue, strError);
                        //tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                        //this.ListErrorVDA.Add(tmpLog);
                        ////Datensatz löschen
                        //if (myQueue.Delete())
                        //{
                        //    tmpLog = new clsLogbuchCon();
                        //    tmpLog.GL_User = myGLUser;
                        //    tmpLog.Typ = Globals.enumLogArtItem.ERROR.ToString();
                        //    tmpLog.TableName = "Queue";
                        //    tmpLog.TableID = myQueue.ID;
                        //    strError = string.Empty;
                        //    strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] wurde gelöscht.";
                        //    tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                        //    tmpLog.Datum = DateTime.Now;
                        //    this.ListErrorVDA.Add(tmpLog);
                        //}
                    }
                }
                else
                {
                    //in der ASN.Queue sind die Angaben Tablename und ID enhalten 
                    //hierüber können alle Informationen ermittelt werden
                    switch (ASN.Queue.TableName)
                    {
                        case "LEingang":
                            dtEingang = Lager.GetLEingangDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                            dtArtikel = Lager.GetArtikelDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                            if (dtEingang.Rows.Count > 0)
                            {
                                decimal decETableID = 0;
                                decimal.TryParse(dtEingang.Rows[0]["ID"].ToString(), out decETableID);
                                if (decETableID > 0)
                                {
                                    this.Lager.Eingang = new clsLEingang();
                                    this.Lager.GLUser = this.GL_User;
                                    this.Lager.Eingang.LEingangTableID = decETableID;
                                    this.Lager.Eingang.FillEingang();
                                    this.strFileID = this.Lager.Eingang.LEingangTableID.ToString();

                                    Orga = new clsOrga();
                                    Orga.GL_User = this.GL_User;
                                    //Orga.AdrID = this.Lager.Eingang.Auftraggeber;
                                    Orga.AdrID = ASN.Queue.AdrVerweisID;
                                    Orga.FillByAdrID();
                                    Orga.UpdateSendNr();
                                    if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
                                    {
                                        CreateVDA();
                                    }
                                    else
                                    {
                                        CreateVDAFirst();
                                    }
                                }
                            }
                            else
                            {
                                string strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] -> Tablename:[" + myQueue.TableName + "] - TableID[" + myQueue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!";
                                strError = CreateQueueDetailsString(ref myQueue, strError);
                                tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                tmpLog.Datum = DateTime.Now;
                                this.ListErrorVDA.Add(tmpLog);

                                //Datensatz löschen
                                if (myQueue.Delete())
                                {
                                    tmpLog = new clsLogbuchCon();
                                    tmpLog.GL_User = myGLUser;
                                    tmpLog.Typ = Globals.enumLogArtItem.ERROR.ToString();
                                    tmpLog.TableName = "Queue";
                                    tmpLog.TableID = myQueue.ID;
                                    strError = string.Empty;
                                    strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] wurde gelöscht.";
                                    tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                    tmpLog.Datum = DateTime.Now;
                                    this.ListErrorVDA.Add(tmpLog);
                                }
                            }
                            break;

                        case "LAusgang":
                            dtAusgang = Lager.GetLAusgangDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                            dtArtikel = Lager.GetArtikelDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                            //dtEingang = Lager.GetLEingangDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                            if (dtAusgang.Rows.Count > 0)
                            {
                                decimal decATableID = 0;
                                decimal.TryParse(dtAusgang.Rows[0]["ID"].ToString(), out decATableID);
                                if (decATableID > 0)
                                {
                                    this.Lager.Ausgang = new clsLAusgang();
                                    this.Lager.GLUser = this.GL_User;
                                    this.Lager.Ausgang.LAusgangTableID = decATableID;
                                    this.Lager.Ausgang.FillAusgang(); // Fill();
                                    this.strFileID = this.Lager.Ausgang.LAusgangTableID.ToString();

                                    Orga = new clsOrga();
                                    Orga.GL_User = this.GL_User;
                                    //Orga.AdrID = this.Lager.Ausgang.Auftraggeber;
                                    Orga.AdrID = ASN.Queue.AdrVerweisID;
                                    Orga.FillByAdrID();
                                    Orga.UpdateSendNr();
                                    if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
                                    {
                                        CreateVDA();
                                    }
                                    else
                                    {
                                        CreateVDAFirst();
                                    }
                                }
                            }
                            else
                            {
                                string strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] -> Tablename:[" + myQueue.TableName + "] - TableID[" + myQueue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!";
                                strError = CreateQueueDetailsString(ref myQueue, strError);
                                tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                tmpLog.Datum = DateTime.Now;
                                this.ListErrorVDA.Add(tmpLog);
                                //Datensatz löschen
                                if (myQueue.Delete())
                                {
                                    tmpLog = new clsLogbuchCon();
                                    tmpLog.GL_User = myGLUser;
                                    tmpLog.Typ = Globals.enumLogArtItem.ERROR.ToString();
                                    tmpLog.TableName = "Queue";
                                    tmpLog.TableID = myQueue.ID;
                                    strError = string.Empty;
                                    strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] wurde gelöscht.";
                                    tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                    tmpLog.Datum = DateTime.Now;
                                    this.ListErrorVDA.Add(tmpLog);
                                }
                            }
                            break;

                        case "Artikel":
                            dtEingang = Lager.GetLEingangDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                            dtAusgang = Lager.GetLAusgangDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                            dtArtikel = Lager.GetArtikelDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);

                            this.Lager.Artikel = new clsArtikel();
                            this.Lager.Artikel.InitClass(this.GL_User, this.GLSystem);
                            this.Lager.Artikel.ID = this.ASN.Queue.TableID;
                            this.Lager.Artikel.GetArtikeldatenByTableID();

                            decimal decETableIDTmp = 0;
                            if (dtEingang.Rows.Count > 0)
                            {
                                decimal.TryParse(dtEingang.Rows[0]["ID"].ToString(), out decETableIDTmp);
                            }
                            if (decETableIDTmp > 0)
                            {
                                this.Lager.Eingang = new clsLEingang();
                                this.Lager.GLUser = this.GL_User;
                                this.Lager.Eingang.LEingangTableID = decETableIDTmp;
                                this.Lager.Eingang.FillEingang();
                            }
                            decimal decATableIDTmp = 0;
                            if (dtAusgang.Rows.Count > 0)
                            {
                                decimal.TryParse(dtAusgang.Rows[0]["ID"].ToString(), out decATableIDTmp);
                            }
                            if (decATableIDTmp > 0)
                            {
                                this.Lager.Ausgang = new clsLAusgang();
                                this.Lager.GLUser = this.GL_User;
                                this.Lager.Ausgang.LAusgangTableID = decATableIDTmp;
                                this.Lager.Ausgang.FillAusgang(); // Fill();
                            }
                            if (dtArtikel.Rows.Count > 0)
                            {
                                Orga = new clsOrga();
                                Orga.GL_User = this.GL_User;
                                //Orga.AdrID = this.Lager.Eingang.Auftraggeber;
                                Orga.AdrID = ASN.Queue.AdrVerweisID;
                                Orga.FillByAdrID();
                                Orga.UpdateSendNr();
                                if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
                                {
                                    CreateVDA();
                                }
                                else
                                {
                                    CreateVDAFirst();
                                }
                            }
                            else
                            {
                                string strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] -> Tablename:[" + myQueue.TableName + "] - TableID[" + myQueue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!"; ;
                                strError = CreateQueueDetailsString(ref myQueue, strError);
                                tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                this.ListErrorVDA.Add(tmpLog);
                                //Datensatz löschen
                                if (myQueue.Delete())
                                {
                                    tmpLog = new clsLogbuchCon();
                                    tmpLog.GL_User = myGLUser;
                                    tmpLog.Typ = Globals.enumLogArtItem.ERROR.ToString();
                                    tmpLog.TableName = "Queue";
                                    tmpLog.TableID = myQueue.ID;
                                    strError = string.Empty;
                                    strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] wurde gelöscht.";
                                    tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                    tmpLog.Datum = DateTime.Now;
                                    this.ListErrorVDA.Add(tmpLog);
                                }
                            }
                            break;
                    }
                }
            }
            else
            {
                //Error lsite

            }
        }
        ///<summary>clsVDACreate / CreateQueueDetailsString</summary>
        ///<remarks></remarks>
        private string CreateQueueDetailsString(ref clsQueue myQueue, string myErrorText)
        {
            return myErrorText + Environment.NewLine +
                                           "Details:" + Environment.NewLine +
                                           "ID: [ " + myQueue.ID.ToString() + " ] " + Environment.NewLine +
                                           "Tablename: [ " + myQueue.TableName + " ] " + Environment.NewLine +
                                           "TableID: [ " + myQueue.TableID.ToString() + " ] " + Environment.NewLine +
                                           "Datum: [ " + myQueue.Datum.ToString() + " ] " + Environment.NewLine +
                                           "ASNTypID: [ " + myQueue.ASNTypID.ToString() + " - " + myQueue.ASNTyp.Typ + " ]" + Environment.NewLine +
                                           "ASNID: [ " + myQueue.ASNID.ToString() + " ]" + Environment.NewLine +
                                           "AdrVerweisID: [ " + myQueue.AdrVerweisID.ToString() + " ]" + Environment.NewLine +
                                           "ASNAction: [ " + myQueue.ASNAction.ToString() + " ] " + Environment.NewLine;
        }
        ///<summary>clsVDACreate / CreateVDA</summary>
        ///<remarks></remarks>
        private void CreateVDAFirst()
        {
            Int32 i711 = 0;
            Int32 i712 = 0;
            Int32 i713 = 0;
            Int32 i714 = 0;
            Int32 i715 = 0;
            Int32 i716 = 0;
            Int32 i717 = 0;
            Int32 i718 = 0;


            //Erzeugen der einzelnen Satzstrings
            s711Create(ref i711);
            s712Create(ref i712);
            s713Create(ref i713);

            //Artikel
            for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
            {
                decimal decTmp = 0;
                Decimal.TryParse(dtArtikel.Rows[i]["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    this.Lager.Artikel = new clsArtikel();
                    this.Lager.Artikel._GL_User = this.GL_User;
                    this.Lager.Artikel.ID = decTmp;
                    this.Lager.Artikel.GetArtikeldatenByTableID();

                    s714Create(ref i714);
                    s715Create(ref i715);
                    s716Create(ref i716);
                    s717Create(ref i717);
                    s718Create(ref i718);
                }
            }
            Dictionary<Int32, string> DictSatzCount = new Dictionary<int, string>();
            DictSatzCount.Add(711, Count711);
            DictSatzCount.Add(712, Count712);
            DictSatzCount.Add(713, Count713);
            DictSatzCount.Add(714, Count714);
            DictSatzCount.Add(715, Count715);
            DictSatzCount.Add(716, Count716);
            DictSatzCount.Add(717, Count717);
            DictSatzCount.Add(718, Count718);

            s719Create(DictSatzCount);
        }
        ///<summary>clsVDACreate / CreateVDA</summary>
        ///<remarks></remarks>
        private void CreateVDA()
        {
            Int32 i711 = 0;
            Int32 i712 = 0;
            Int32 i713 = 0;
            Int32 i714 = 0;
            Int32 i715 = 0;
            Int32 i716 = 0;
            Int32 i717 = 0;
            Int32 i718 = 0;

            Count711 = "0";
            Count712 = "0";
            Count713 = "0";
            Count714 = "0";
            Count715 = "0";
            Count716 = "0";
            Count717 = "0";
            Count718 = "0";

            try
            {
                //Erzeugen der einzelnen Satzstrings
                s711Create(ref i711);
                s712Create(ref i712);

                bool IsArtBezogen = false;
                if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
                {
                    this.VDAClientVal.SetListSatzFromDictByKey("713");
                    if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                    {
                        clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                        IsArtBezogen = tmpCV.IsArtSatz;
                    }
                }
                if (!IsArtBezogen)
                {
                    this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("713");
                    this.VDAClientVal.SetListSatzFromDictByKey("713");
                    s713Create(ref i713);
                }
                //Artikel
                for (Int32 i = 0; i <= dtArtikel.Rows.Count - 1; i++)
                {
                    decimal decTmp = 0;
                    Decimal.TryParse(dtArtikel.Rows[i]["ID"].ToString(), out decTmp);
                    if (decTmp > 0)
                    {
                        this.Lager.Artikel = new clsArtikel();
                        this.Lager.Artikel._GL_User = this.GL_User;
                        this.Lager.Artikel.ID = decTmp;
                        this.Lager.Artikel.GetArtikeldatenByTableID();
                        //neuer Schleifendurchlauf, für die mögliche Sätze 713 bis 718 
                        // Anzahl wird in DB festgelegt VSAClientoUt Feld ArtSatz im 1.Feld des Satz und NextSatz muss größer 0 sein
                        Int32 x = 0;
                        while (x <= iArtSatzCount - 1)
                        {
                            switch (iNextSatz)
                            {
                                case 713:
                                    if (IsArtBezogen)
                                    {
                                        s713Create(ref i713);
                                    }
                                    break;
                                case 714:
                                    s714Create(ref i714);
                                    break;
                                case 715:
                                    s715Create(ref i715);
                                    break;
                                case 716:
                                    s716Create(ref i716);
                                    break;
                                case 717:
                                    s717Create(ref i717);
                                    break;
                                case 718:
                                    s718Create(ref i718);
                                    break;

                            }
                            x++;
                        }
                    }
                }
                Dictionary<Int32, string> DictSatzCount = new Dictionary<int, string>();
                DictSatzCount.Add(711, Count711);
                DictSatzCount.Add(712, Count712);
                DictSatzCount.Add(713, Count713);
                DictSatzCount.Add(714, Count714);
                DictSatzCount.Add(715, Count715);
                DictSatzCount.Add(716, Count716);
                DictSatzCount.Add(717, Count717);
                DictSatzCount.Add(718, Count718);

                s719Create(DictSatzCount);
            }
            catch (Exception ex)
            {
                string strEx = ex.ToString();
                if (!this.tmpLog.LogText.Equals(string.Empty))
                {
                    string strTmp = "[Task_WriteVDA].[VDA4913].[VDACreate]: " + Environment.NewLine;
                    strTmp = strTmp + "Table: " + tmpLog.TableName + Environment.NewLine;
                    strTmp = strTmp + "TableID: " + tmpLog.TableID.ToString() + Environment.NewLine + Environment.NewLine;

                    this.tmpLog.LogText = strTmp + Environment.NewLine + this.tmpLog.LogText;
                    this.ListErrorVDA.Add(tmpLog);
                }
            }
            finally
            {
                if (
                     (this.tmpLog.LogText != null) &&
                     (!this.tmpLog.LogText.Equals(string.Empty))
                   )
                {
                    string strTmp = "[Task_WriteVDA].[VDA4913].[VDACreate]: " + Environment.NewLine;
                    strTmp = strTmp + "Table: " + tmpLog.TableName + Environment.NewLine;
                    strTmp = strTmp + "TableID: " + tmpLog.TableID.ToString() + Environment.NewLine + Environment.NewLine;

                    this.tmpLog.LogText = strTmp + Environment.NewLine + this.tmpLog.LogText;
                    this.ListErrorVDA.Add(tmpLog);
                }
            }
        }
        ///<summary>clsVDACreate / GetErrorExceptionString</summary>
        ///<remarks></remarks>
        private string GetErrorExceptionString(string myException, ref clsASNArtSatzFeld tmpFeld)
        {
            string strReturnException = Environment.NewLine;

            if (tmpFeld is clsASNArtSatzFeld)
            {
                strReturnException += "Kennung: " + tmpFeld.Kennung + Environment.NewLine;

                if (tmpFeld.ASNVal is clsASNValue)
                {
                    strReturnException += "Feld: " + tmpFeld.ASNVal.FieldName + Environment.NewLine +
                                    "Value: " + tmpFeld.ASNVal.Value + Environment.NewLine;
                }
                else
                {

                    strReturnException += "tmpFeld.ASNVal is NULL " + Environment.NewLine;
                }
            }
            else
            {
                strReturnException += "tmpFeld is NULL " + Environment.NewLine;
            }
            strReturnException += "Excecption: " + Environment.NewLine + myException.ToString();
            return strReturnException;
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s711Create(ref Int32 i711)
        {
            string str711 = string.Empty;
            //Liste mit den Values wird durchlaufen
            this.VDAClientVal.SetListSatzFromDictByKey("711");
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("711");
            string strException = string.Empty;
            if (this.VDAClientVal.listVDAClientValueSatz != null)
            {
                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
                {
                    clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];
                    switch (tmpFeld.Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ711F01:
                        case clsASN.const_VDA4913SatzField_SATZ711F02:
                        case clsASN.const_VDA4913SatzField_SATZ711F03:
                        case clsASN.const_VDA4913SatzField_SATZ711F04:
                        case clsASN.const_VDA4913SatzField_SATZ711F05:
                        case clsASN.const_VDA4913SatzField_SATZ711F06:
                        case clsASN.const_VDA4913SatzField_SATZ711F07:
                        case clsASN.const_VDA4913SatzField_SATZ711F08:
                        case clsASN.const_VDA4913SatzField_SATZ711F09:
                        case clsASN.const_VDA4913SatzField_SATZ711F10:
                        case clsASN.const_VDA4913SatzField_SATZ711F11:
                        case clsASN.const_VDA4913SatzField_SATZ711F12:
                            try
                            {
                                str711 = str711 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz);
                                Int32 iCh = str711.Length;
                            }
                            catch (Exception ex)
                            {
                                strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                            }
                            break;
                        default:
                            break;
                    }

                }
                Int32 iCheck = str711.Length;

                if (iCheck == 128)
                {
                    ListVDASatzString.Add(str711);
                }
                else
                {
                    tmpLog.LogText = tmpLog.LogText + str711 + "|-> 711 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                    tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
                }
                i711++;
            }
            if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
            {
                iNextSatz = 712;
                if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                    iNextSatz = tmpCV.NextSatz;
                }
            }
            Count711 = i711.ToString();
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s712Create(ref Int32 i712)
        {
            string str712 = string.Empty;
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("712");
            this.VDAClientVal.SetListSatzFromDictByKey("712");
            string strException = string.Empty;
            if (this.VDAClientVal.listVDAClientValueSatz != null)
            {
                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
                {
                    clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];

                    switch (tmpFeld.Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ712F01:
                        case clsASN.const_VDA4913SatzField_SATZ712F02:
                        case clsASN.const_VDA4913SatzField_SATZ712F03:
                        case clsASN.const_VDA4913SatzField_SATZ712F04:
                        case clsASN.const_VDA4913SatzField_SATZ712F05:
                        case clsASN.const_VDA4913SatzField_SATZ712F06:
                        case clsASN.const_VDA4913SatzField_SATZ712F07:
                        case clsASN.const_VDA4913SatzField_SATZ712F08:
                        case clsASN.const_VDA4913SatzField_SATZ712F09:
                        case clsASN.const_VDA4913SatzField_SATZ712F10:
                        case clsASN.const_VDA4913SatzField_SATZ712F11:
                        case clsASN.const_VDA4913SatzField_SATZ712F12:
                        case clsASN.const_VDA4913SatzField_SATZ712F13:
                        case clsASN.const_VDA4913SatzField_SATZ712F14:
                        case clsASN.const_VDA4913SatzField_SATZ712F15:
                        case clsASN.const_VDA4913SatzField_SATZ712F16:
                        case clsASN.const_VDA4913SatzField_SATZ712F17:
                        case clsASN.const_VDA4913SatzField_SATZ712F18:
                        case clsASN.const_VDA4913SatzField_SATZ712F19:
                        case clsASN.const_VDA4913SatzField_SATZ712F20:
                        case clsASN.const_VDA4913SatzField_SATZ712F21:
                            try
                            {
                                str712 = str712 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz);
                                Int32 iCh = str712.Length;
                            }
                            catch (Exception ex)
                            {
                                strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                            }
                            break;

                        default:
                            break;
                    }

                }
                Int32 iCheck = str712.Length;

                if (iCheck == 128)
                {
                    ListVDASatzString.Add(str712);
                }
                else
                {
                    tmpLog.LogText = tmpLog.LogText + str712 + "|-> 712 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                    tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
                }
                i712++;
            }
            if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
            {
                iNextSatz = 713;
                if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                    iNextSatz = tmpCV.NextSatz;
                }
            }
            Count712 = i712.ToString();
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s713Create(ref Int32 i713)
        {
            string str713 = string.Empty;
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("713");
            this.VDAClientVal.SetListSatzFromDictByKey("713");
            string strException = string.Empty;
            if (this.VDAClientVal.listVDAClientValueSatz != null)
            {
                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
                {
                    clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];

                    switch (tmpFeld.Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ713F01:
                        case clsASN.const_VDA4913SatzField_SATZ713F02:
                        case clsASN.const_VDA4913SatzField_SATZ713F03:
                        case clsASN.const_VDA4913SatzField_SATZ713F04:
                        case clsASN.const_VDA4913SatzField_SATZ713F05:
                        case clsASN.const_VDA4913SatzField_SATZ713F06:
                        case clsASN.const_VDA4913SatzField_SATZ713F07:
                        case clsASN.const_VDA4913SatzField_SATZ713F08:
                        case clsASN.const_VDA4913SatzField_SATZ713F09:
                        case clsASN.const_VDA4913SatzField_SATZ713F10:
                        case clsASN.const_VDA4913SatzField_SATZ713F11:
                        case clsASN.const_VDA4913SatzField_SATZ713F12:
                        case clsASN.const_VDA4913SatzField_SATZ713F13:
                        case clsASN.const_VDA4913SatzField_SATZ713F14:
                        case clsASN.const_VDA4913SatzField_SATZ713F15:
                        case clsASN.const_VDA4913SatzField_SATZ713F16:
                        case clsASN.const_VDA4913SatzField_SATZ713F17:
                        case clsASN.const_VDA4913SatzField_SATZ713F18:
                        case clsASN.const_VDA4913SatzField_SATZ713F19:
                        case clsASN.const_VDA4913SatzField_SATZ713F20:
                        case clsASN.const_VDA4913SatzField_SATZ713F21:
                            try
                            {
                                str713 = str713 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz);
                                Int32 iCh = str713.Length;
                            }
                            catch (Exception ex)
                            {
                                strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                            }
                            break;

                        default:
                            break;
                    }
                }
                Int32 iCheck = str713.Length;

                if (iCheck == 128)
                {
                    ListVDASatzString.Add(str713);
                }
                else
                {
                    tmpLog.LogText = tmpLog.LogText + str713 + "|-> 713 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                    tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
                }
                i713++;
            }
            if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
            {
                iNextSatz = 714;
                if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                    iNextSatz = tmpCV.NextSatz;
                    iArtSatzCount = tmpCV.CountArtSatz;
                }
            }
            Count713 = i713.ToString();
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s714Create(ref Int32 i714)
        {
            string str714 = string.Empty;
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("714");
            this.VDAClientVal.SetListSatzFromDictByKey("714");
            string strException = string.Empty;
            if (this.VDAClientVal.listVDAClientValueSatz != null)
            {
                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
                {
                    clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];

                    switch (tmpFeld.Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ714F01:
                        case clsASN.const_VDA4913SatzField_SATZ714F02:
                        case clsASN.const_VDA4913SatzField_SATZ714F03:
                        case clsASN.const_VDA4913SatzField_SATZ714F04:
                        case clsASN.const_VDA4913SatzField_SATZ714F05:
                        case clsASN.const_VDA4913SatzField_SATZ714F06:
                        case clsASN.const_VDA4913SatzField_SATZ714F07:
                        case clsASN.const_VDA4913SatzField_SATZ714F08:
                        case clsASN.const_VDA4913SatzField_SATZ714F09:
                        case clsASN.const_VDA4913SatzField_SATZ714F10:
                        case clsASN.const_VDA4913SatzField_SATZ714F11:
                        case clsASN.const_VDA4913SatzField_SATZ714F12:
                        case clsASN.const_VDA4913SatzField_SATZ714F13:
                        case clsASN.const_VDA4913SatzField_SATZ714F14:
                        case clsASN.const_VDA4913SatzField_SATZ714F15:
                        case clsASN.const_VDA4913SatzField_SATZ714F16:
                        case clsASN.const_VDA4913SatzField_SATZ714F17:
                        case clsASN.const_VDA4913SatzField_SATZ714F18:
                        case clsASN.const_VDA4913SatzField_SATZ714F19:
                        case clsASN.const_VDA4913SatzField_SATZ714F20:
                        case clsASN.const_VDA4913SatzField_SATZ714F21:
                        case clsASN.const_VDA4913SatzField_SATZ714F22:
                            try
                            {
                                str714 = str714 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz);
                                Int32 iCh = str714.Length;
                            }
                            catch (Exception ex)
                            {
                                strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                            }
                            break;

                        default:
                            break;
                    }
                }
                Int32 iCheck = str714.Length;

                if (iCheck == 128)
                {
                    ListVDASatzString.Add(str714);
                    i714++;
                }
                else
                {
                    tmpLog.LogText = tmpLog.LogText + str714 + "|-> 714 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                    tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
                }
            }
            if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
            {
                iNextSatz = 715;
                if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                    iNextSatz = tmpCV.NextSatz;
                }
            }
            Count714 = i714.ToString();
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s715Create(ref Int32 i715)
        {
            string str715 = string.Empty;
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("715");
            this.VDAClientVal.SetListSatzFromDictByKey("715");
            string strException = string.Empty;
            if (this.VDAClientVal.listVDAClientValueSatz != null)
            {
                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
                {
                    clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];

                    switch (tmpFeld.Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ715F01:
                        case clsASN.const_VDA4913SatzField_SATZ715F02:
                        case clsASN.const_VDA4913SatzField_SATZ715F03:
                        case clsASN.const_VDA4913SatzField_SATZ715F04:
                        case clsASN.const_VDA4913SatzField_SATZ715F05:
                        case clsASN.const_VDA4913SatzField_SATZ715F06:
                        case clsASN.const_VDA4913SatzField_SATZ715F07:
                        case clsASN.const_VDA4913SatzField_SATZ715F08:
                        case clsASN.const_VDA4913SatzField_SATZ715F09:
                        case clsASN.const_VDA4913SatzField_SATZ715F10:
                        case clsASN.const_VDA4913SatzField_SATZ715F11:
                        case clsASN.const_VDA4913SatzField_SATZ715F12:
                        case clsASN.const_VDA4913SatzField_SATZ715F13:
                        case clsASN.const_VDA4913SatzField_SATZ715F14:
                        case clsASN.const_VDA4913SatzField_SATZ715F15:
                        case clsASN.const_VDA4913SatzField_SATZ715F16:
                            try
                            {
                                str715 = str715 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz);
                                Int32 iCh = str715.Length;
                            }
                            catch (Exception ex)
                            {
                                strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                            }
                            break;

                        default:
                            break;
                    }
                }

                Int32 iCheck = str715.Length;
                if (iCheck == 128)
                {
                    ListVDASatzString.Add(str715);
                    i715++;
                }
                else
                {
                    tmpLog.LogText = tmpLog.LogText + str715 + "|-> 715 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                    tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
                }
            }
            if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
            {
                iNextSatz = 716;
                if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                    iNextSatz = tmpCV.NextSatz;
                }
            }
            Count715 = i715.ToString();
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s716Create(ref Int32 i716)
        {
            string str716 = string.Empty;
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("716");
            this.VDAClientVal.SetListSatzFromDictByKey("716");
            string strException = string.Empty;
            if (this.VDAClientVal.listVDAClientValueSatz != null)
            {
                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
                {
                    clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];

                    switch (tmpFeld.Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ716F01:
                        case clsASN.const_VDA4913SatzField_SATZ716F02:
                        case clsASN.const_VDA4913SatzField_SATZ716F03:
                        case clsASN.const_VDA4913SatzField_SATZ716F04:
                        case clsASN.const_VDA4913SatzField_SATZ716F05:
                        case clsASN.const_VDA4913SatzField_SATZ716F06:
                            try
                            {
                                str716 = str716 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz);
                                Int32 iCh = str716.Length;
                            }
                            catch (Exception ex)
                            {
                                strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                            }
                            break;

                        default:
                            break;
                    }
                }
                Int32 iCheck = str716.Length;
                if (iCheck == 128)
                {
                    ListVDASatzString.Add(str716);
                    i716++;
                }
                else
                {
                    tmpLog.LogText = tmpLog.LogText + str716 + "|-> 716 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                    tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
                }
            }
            if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
            {
                iNextSatz = 717;
                if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                    iNextSatz = tmpCV.NextSatz;
                }
            }
            Count716 = i716.ToString();
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s717Create(ref Int32 i717)
        {
            string str717 = string.Empty;
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("717");
            this.VDAClientVal.SetListSatzFromDictByKey("717");
            string strException = string.Empty;
            if (this.VDAClientVal.listVDAClientValueSatz != null)
            {
                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
                {
                    clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];

                    switch (tmpFeld.Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ717F01:
                        case clsASN.const_VDA4913SatzField_SATZ717F02:
                        case clsASN.const_VDA4913SatzField_SATZ717F03:
                        case clsASN.const_VDA4913SatzField_SATZ717F04:
                        case clsASN.const_VDA4913SatzField_SATZ717F05:
                        case clsASN.const_VDA4913SatzField_SATZ717F06:
                        case clsASN.const_VDA4913SatzField_SATZ717F07:
                        case clsASN.const_VDA4913SatzField_SATZ717F08:
                        case clsASN.const_VDA4913SatzField_SATZ717F09:
                            try
                            {
                                str717 = str717 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz);
                                Int32 iCh = str717.Length;
                            }
                            catch (Exception ex)
                            {
                                strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                            }
                            break;

                        default:
                            break;
                    }
                }
                Int32 iCheck = str717.Length;
                if (iCheck == 128)
                {
                    ListVDASatzString.Add(str717);
                    i717++;
                }
                else
                {
                    tmpLog.LogText = tmpLog.LogText + str717 + "|-> 717 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                    tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
                }
            }
            if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
            {
                iNextSatz = 718;
                if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                    iNextSatz = tmpCV.NextSatz;
                }
            }
            Count717 = i717.ToString();
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s718Create(ref Int32 i718)
        {
            string str718 = string.Empty;
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzFieldBySatz("718");
            this.VDAClientVal.SetListSatzFromDictByKey("718");
            string strException = string.Empty;
            if (this.VDAClientVal.listVDAClientValueSatz != null)
            {
                for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
                {
                    clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];

                    switch (tmpFeld.Kennung)
                    {
                        case clsASN.const_VDA4913SatzField_SATZ718F01:
                        case clsASN.const_VDA4913SatzField_SATZ718F02:
                        case clsASN.const_VDA4913SatzField_SATZ718F03:
                        case clsASN.const_VDA4913SatzField_SATZ718F04:
                        case clsASN.const_VDA4913SatzField_SATZ718F05:
                        case clsASN.const_VDA4913SatzField_SATZ718F06:
                        case clsASN.const_VDA4913SatzField_SATZ718F07:
                        case clsASN.const_VDA4913SatzField_SATZ718F08:
                        case clsASN.const_VDA4913SatzField_SATZ718F09:
                        case clsASN.const_VDA4913SatzField_SATZ718F10:
                        case clsASN.const_VDA4913SatzField_SATZ718F11:
                        case clsASN.const_VDA4913SatzField_SATZ718F12:
                        case clsASN.const_VDA4913SatzField_SATZ718F13:
                        case clsASN.const_VDA4913SatzField_SATZ718F14:
                        case clsASN.const_VDA4913SatzField_SATZ718F15:
                            try
                            {
                                str718 = str718 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz);
                                Int32 iCh = str718.Length;
                            }
                            catch (Exception ex)
                            {
                                strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                            }
                            break;

                        default:
                            break;
                    }
                }

                Int32 iCheck = str718.Length;
                if (iCheck == 128)
                {
                    ListVDASatzString.Add(str718);
                    i718++;
                }
                else
                {
                    tmpLog.LogText = tmpLog.LogText + str718 + "|-> 718 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                    tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
                }
            }
            if (this.Sys.Client.Modul.ASN_UseNewASNCreateFunction)
            {
                iNextSatz = 719;
                if (this.VDAClientVal.listVDAClientValueSatz.Count > 0)
                {
                    clsVDAClientValue tmpCV = (clsVDAClientValue)this.VDAClientVal.listVDAClientValueSatz[0];
                    iNextSatz = tmpCV.NextSatz;
                }
            }
            Count718 = i718.ToString();
        }
        ///<summary>clsVDACreate / s711Create</summary>
        ///<remarks></remarks>
        private void s719Create(Dictionary<Int32, string> myDicCount)
        {
            Int32 i719 = 1;
            string str719 = string.Empty;
            this.ASN.ASNArt.asnSatz.asnSatzFeld.GetSatzField719();
            string strException = string.Empty;
            for (Int32 i = 0; i <= this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField.Count - 1; i++)
            {
                clsASNArtSatzFeld tmpFeld = (clsASNArtSatzFeld)this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i];
                string strTmp = "0";
                switch (i)
                {
                    //Konstante
                    case 0:
                        str719 = "719";
                        break;
                    case 1:
                        str719 = str719 + "03";
                        break;
                    //711
                    case 2:
                        string strTrmp711 = string.Empty;
                        myDicCount.TryGetValue(711, out strTrmp711);
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, strTrmp711, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //712
                    case 3:
                        string strTrmp712 = string.Empty;
                        myDicCount.TryGetValue(712, out strTrmp712);
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, strTrmp712, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //713
                    case 4:
                        string strTmp713 = string.Empty;
                        myDicCount.TryGetValue(713, out strTmp713);
                        if (strTmp713 != null)
                        {
                            strTmp = strTmp713;
                        }
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, strTmp, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //714
                    case 5:
                        string strTmp714 = string.Empty;
                        myDicCount.TryGetValue(714, out strTmp714);
                        if (strTmp714 != null)
                        {
                            strTmp = strTmp714;
                        }
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, strTmp, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //715
                    case 6:
                        string strTmp715 = string.Empty;
                        myDicCount.TryGetValue(715, out strTmp715);
                        if (strTmp715 != null)
                        {
                            strTmp = strTmp715;
                        }
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, strTmp, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //716
                    case 7:
                        string strTmp716 = string.Empty;
                        myDicCount.TryGetValue(716, out strTmp716);
                        if (strTmp716 != null)
                        {
                            strTmp = strTmp716;
                        }
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, strTmp, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //718
                    case 8:
                        string strTmp718 = string.Empty;
                        myDicCount.TryGetValue(718, out strTmp718);
                        if (strTmp718 != null)
                        {
                            strTmp = strTmp718;
                        }
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, strTmp, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //719
                    case 9:
                        Count719 = i719.ToString();
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, Count719, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //717
                    case 10:
                        string strTmp717 = string.Empty;
                        myDicCount.TryGetValue(717, out strTmp717);
                        if (strTmp717 != null)
                        {
                            strTmp = strTmp717;
                        }
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, strTmp, this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, true);
                        break;
                    //letzte leer 60x
                    case 11:
                        str719 = str719 + FillValueWithstringToLenth(true, tmpFeld.FillValue, "", this.ASN.ASNArt.asnSatz.asnSatzFeld.ListSatzField[i].Length, false);
                        break;
                }
            }
            Int32 iCheck = str719.Length;
            if (iCheck == 128)
            {
                ListVDASatzString.Add(str719);
            }
            else
            {
                tmpLog.LogText = tmpLog.LogText + str719 + "|-> 719 - Länge[" + iCheck.ToString() + "]" + Environment.NewLine;
                tmpLog.LogText = tmpLog.LogText + Environment.NewLine + strException;
            }
        }
        ///<summary>clsVDACreate / FillValueWithstringToLenth</summary>
        ///<remarks></remarks>
        private string FillValueWithstringToLenth(bool bFillValue, string StrToFill, string myValue, Int32 myLength, bool myLeft)
        {
            string retVal = myValue;
            if (bFillValue)
            {
                string strFillValue = string.Empty;
                if (
                    (StrToFill.Equals(clsVDACreate.const_VDA_Value_Blanks)) || (StrToFill.Equals(""))
                   )
                {
                    strFillValue = " ";
                }
                else
                {
                    strFillValue = StrToFill;
                }
                while (retVal.Length < myLength)
                {
                    if (myLeft)
                    {
                        //0 voranstelle
                        retVal = strFillValue + retVal;
                    }
                    else
                    {
                        retVal = retVal + strFillValue;
                    }
                }
                if (retVal.Length > myLength)
                {
                    retVal = retVal.Substring(0, myLength);
                }
            }
            return retVal;
        }
        ///<summary>clsVDACreate / GetFieldValueForVDASatz</summary>
        ///<remarks></remarks>
        private string GetFieldValueForVDASatz(ref clsASNArtSatzFeld myClFeld, List<clsVDAClientValue> myListVDAClientValue)
        {
            clsASNTyp asnTyp = new clsASNTyp();
            asnTyp.GL_User = this.GL_User;
            asnTyp.ID = 0;
            asnTyp.TypID = (Int32)this.ASN.Job.ASNTypID;
            asnTyp.FillbyTypID();

            string SatzString = string.Empty;

            //Liste mit den Values wird durchlaufen
            for (Int32 i = 0; i <= myListVDAClientValue.Count - 1; i++)
            {
                clsVDAClientValue tmpCV = (clsVDAClientValue)myListVDAClientValue[i];

                if (myClFeld.ID == tmpCV.ASNFieldID)
                {
                    string StringFillValue = myClFeld.FillValue;
                    bool bFillLeft = myClFeld.FillLeft;
                    if (!tmpCV.FillValue.Equals(string.Empty))
                    {
                        StringFillValue = tmpCV.FillValue;
                        bFillLeft = tmpCV.FillLeft;
                    }
                    string strFeldSub = string.Empty;
                    string strTmp = string.Empty;
                    GetValue(tmpCV.ValueArt, ref tmpCV, ref myClFeld, ref strFeldSub, ref StringFillValue, ref asnTyp, ref strTmp, bFillLeft);

                    if (myClFeld.Length != strFeldSub.Length)
                    {
                        //Error Log für das fehlerhafte Feld
                        tmpLog.LogText = tmpLog.LogText + "|" + strFeldSub + "|-> " + myClFeld.Kennung + " - ["+myClFeld.Datenfeld+"] - Länge: SOLL[" + myClFeld.Length + "] / IST[" + strFeldSub.Length.ToString() + "]" + Environment.NewLine;
                        tmpLog.LogText = tmpLog.LogText + "-> strFeldSub = [" + strFeldSub + "]"+ Environment.NewLine;
                    }
                    SatzString = SatzString + strFeldSub;
                    i = myListVDAClientValue.Count;
                }
            }
            return SatzString;
        }
        ///<summary>clsVDACreate / GetValue</summary>
        ///<remarks></remarks>
        private void GetValue(string strValueArt, ref clsVDAClientValue tmpCV, ref clsASNArtSatzFeld myClFeld, ref string strFeldSub, ref string StringFillValue, ref clsASNTyp asnTyp, ref string strTmp, bool bFillLeft)
        {       
            switch (strValueArt)
            {
                case "const":
                    if (tmpCV.Value.Length == myClFeld.Length)
                    {
                        strFeldSub = tmpCV.Value;
                    }
                    else
                    {
                        //Length ist kürzer
                        string strFeld = string.Empty;
                        if (tmpCV.Value.Length < myClFeld.Length)
                        {
                            //strFeld = FillValueWithstringToLenth(tmpCV.Fill0, myClFeld.FillValue, tmpCV.Value, myClFeld.Length, myClFeld.FillLeft);
                            strFeld = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, tmpCV.Value, myClFeld.Length, bFillLeft);
                        }
                        else
                        {
                            //length ist größer ==> kürzen
                            strTmp = string.Empty;
                            strTmp = tmpCV.Value.Substring(0, myClFeld.Length);
                            strFeld = strTmp;
                        }
                        //
                        //strFeld = FillValueWithstringToLenth(tmpCV.Fill0, " ", strFeld, s711Field.Length, false);
                        //SatzString = SatzString + strFeld;
                        strFeldSub = strFeld;
                    }
                    break;
                case const_Lieferantennummer:
                    strTmp = string.Empty;
                    strTmp = GetLieferantenNummer(ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //******************************************************************** 711
                case const_Reciever:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, EMPFAENGER, myClFeld.Length, bFillLeft);
                    break;
                case const_Sender:
                    strTmp = string.Empty;
                    strTmp = GetSenderID(ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_SIDOld:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, SIDOld, myClFeld.Length, bFillLeft);
                    break;
                case const_SIDNew:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, SIDNew, myClFeld.Length, bFillLeft);
                    break;
                case const_VDA_Value_NOW:
                    strFeldSub = this.NOW;
                    break;
                case const_VDA_Value_TimeNow:
                    strFeldSub = this.NOWTIME;
                    break;

                case const_VDA_Value_Blanks:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, tmpCV.Value, myClFeld.Length, bFillLeft);
                    break;
                case const_TransportMittelSchlüssel:
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            if (this.Lager.Eingang.IsWaggon)
                            {
                                strFeldSub = "08";
                            }
                            else
                            {
                                strFeldSub = "01";
                            }
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            if (this.Lager.Ausgang.IsWaggon)
                            {
                                strFeldSub = "08";
                            }
                            else
                            {
                                strFeldSub = "01";
                            }
                            break;
                    }
                    break;
                case const_TransportMittelNummer:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            if (this.Lager.Eingang.IsWaggon)
                            {
                                strTmp = this.Lager.Eingang.WaggonNr;
                            }
                            else
                            {
                                strTmp = this.Lager.Eingang.KFZ;
                            }
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            if (this.Lager.Ausgang.IsWaggon)
                            {
                                strTmp = this.Lager.Ausgang.WaggonNr;
                            }
                            else
                            {
                                strTmp = this.Lager.Ausgang.KFZ;
                            }
                            break;

                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                        
                 case const_TransportMittelNummerTrim:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            if (this.Lager.Eingang.IsWaggon)
                            {
                                strTmp = Regex.Replace(this.Lager.Eingang.WaggonNr, "-", ""); 
                            }
                            else if (this.Lager.Eingang.IsShip)
                            {
                                strTmp = this.Lager.Eingang.Ship;
                            }
                            else
                            {
                                strTmp = this.Lager.Eingang.KFZ;
                            }
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            if (this.Lager.Eingang.IsWaggon)
                            {
                                strTmp = Regex.Replace(this.Lager.Ausgang.WaggonNr, "-", "");
                            }
                            else if (this.Lager.Eingang.IsShip)
                            {
                                strTmp = string.Empty;
                            }
                            else
                            {
                                strTmp = this.Lager.Ausgang.KFZ;
                            }
                            break;

                    }
                    string strTMName = Regex.Replace(strTmp, " ", "");
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTMName, myClFeld.Length, bFillLeft);
                    break;

                case const_Vorgang:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                            strTmp = "30";
                            break;

                        case "TSL":
                        case "TSE":
                            //case "STE":
                            //case "STL":
                            strTmp = "32";
                            break;

                        case "RLL":
                        case "RLE":
                            strTmp = "33";
                            break;

                        case "BML":
                        case "BME":
                            strTmp = "35";
                            break;

                        case "AML":
                        case "AME":
                            strTmp = "36";
                            break;

                        case "AVL":
                        case "AVE":
                        case "STE":
                        case "STL":
                            strTmp = "40";
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //********************************************************************** LEingangdaten
                case const_Eingang_ID:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangTableID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Eingang_EingangID:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Eingang_Datum:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangDate.ToString("yyMMdd");
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Eingang_LfsNr:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang is clsLEingang)
                    {
                        strTmp = this.Lager.Eingang.LEingangLfsNr.ToString();
                    }
                    else
                    {
                        if (this.Lager.Artikel.Eingang is clsLEingang)
                        {
                            strTmp = this.Lager.Artikel.Eingang.LEingangLfsNr.ToString();
                        }
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case const_Eingang_ExTransportRef:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ExTransportRef.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Eingang_ExAuftragRef:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ExAuftragRef.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Eingang_Brutto:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.Brutto.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Eingang_Netto:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.Netto.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Eingang_Anzahl:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ArtikelCount.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case const_EFunc_LieferantennummerDeleteSlash:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang is clsLEingang)
                    {
                        strTmp = this.Lager.Eingang.Lieferant.Trim();
                        strTmp = strTmp.Replace("/", "");
                    }
                    else
                    {
                        if (this.Lager.Ausgang is clsLAusgang)
                        {
                            strTmp = this.Lager.Ausgang.Lieferant.Trim();
                            strTmp = strTmp.Replace("/", "");
                        }
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //***************************************************************************************** EA
                case const_EA_ID:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.LEingangTableID.ToString();
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.LAusgangTableID.ToString();
                            break;

                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_EANr:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.LEingangID.ToString();
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.LAusgangID.ToString();
                            break;

                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_Datum:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.LEingangDate.ToString("yyMMdd");
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.LAusgangsDate.ToString("yyMMdd");
                            break;

                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_LfsNr:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.LEingangID.ToString();
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.LAusgangID.ToString();
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_ExTransportRef:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.ExTransportRef;
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.exTransportRef;
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_ExAuftragRef:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.ExAuftragRef;
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = string.Empty;
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_Brutto:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.Brutto.ToString();
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.Brutto.ToString();
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_Netto:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.Netto.ToString();
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.Netto.ToString();
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_Anzahl:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.ArtikelCount;
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.ArtikelCount;
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_EA_Termin:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = string.Empty;
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.Termin.ToString("yyMMdd");
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case const_EA_SLB:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = this.Lager.Eingang.LEingangID.ToString();
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = this.Lager.Ausgang.SLB.ToString();
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case const_EA_MATDate:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = string.Empty;
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = string.Empty;
                            DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                            if (DateTime.TryParse(this.Lager.Ausgang.MAT.ToString(), out dtTmp))
                            {
                                strTmp = string.Format("{0:yyMMdd}", dtTmp);
                            }
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case const_EA_MATTime:
                    strTmp = string.Empty;
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            strTmp = string.Empty;
                            break;

                        case "AML":
                        case "AME":
                        case "AVL":
                        case "AVE":
                        case "RLL":
                        case "RLE":
                            strTmp = string.Empty;
                            DateTime dtTmp = clsSystem.const_DefaultDateTimeValue_Min;
                            if (DateTime.TryParse(this.Lager.Ausgang.MAT.ToString(), out dtTmp))
                            {
                                strTmp = string.Format("{0:HHMM}", dtTmp);
                            }
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //****************************************************************************************** Artikeldaten
                case const_Artikel_ID:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.ID.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_LVSNr:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.LVS_ID.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Netto:
                    strTmp = GetNetto(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Brutto:
                    strTmp = GetBrutto(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Gut:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.GArt.Bezeichnung, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Dicke:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Dicke);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Breite:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Breite);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Laenge:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Laenge);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Anzahl:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Anzahl.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Einheit:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Einheit.ToUpper(), myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Werksnummer:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Werksnummer, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Produktionsnummer:
                    //check Produktionsnummer
                    strTmp = string.Empty;
                    strTmp = this.Lager.Artikel.Produktionsnummer; 
                    switch (asnTyp.Typ)
                    {
                        case "EML":
                        case "EME":
                        case "BML":
                        case "BME":
                            //strTmp = this.Lager.Artikel.Produktionsnummer;
                            this.Lager.Artikel.UpdateASNProduktionsnummer();                                                                    
                            break;
                        default:
                            //strTmp = this.Lager.Artikel.Produktionsnummer;
                            break;
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Charge:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Charge, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_BestellNr:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Bestellnummer, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_exMaterialNr:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exMaterialnummer, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_Pos:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Position, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_ArtIDRef:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.ArtIDRef, myClFeld.Length, bFillLeft);
                    break;
                case const_Artikel_SchadenTopOne:
                    strTmp = string.Empty;
                    strTmp = this.Lager.Artikel.SchadenTopOne.Trim();
                    StrCheck.CheckString(ref strTmp);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //****************************************************************************************** Functions Artikeldaten
                case const_ArtFunc_WerksnummerOhneBlank:
                    string strWNrOhneBlank = WerksnummerOhneBlank(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrOhneBlank, myClFeld.Length, bFillLeft);
                    break;
                case const_ArtFunc_WerksnummerMitFBlank:
                    string strWNrMitFBlank = WerksnummerMitBlank(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrMitFBlank, myClFeld.Length, bFillLeft);
                    break;
                case const_ArtFunk_WerksnummerFormatVW:
                    string strWNrVWFormat= WerksnummerVWFormat(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrVWFormat, myClFeld.Length, bFillLeft);
                    break;
                case const_ArtFunc_BruttoKGorSt:
                    string strBruttoKGorSt = GetBruttoOrAnzahl(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strBruttoKGorSt, myClFeld.Length, bFillLeft);
                    break;
                case const_ArtFunc_NettoKGorSt:
                    string strNettoKGorSt = GetNettoOrAnzahl(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strNettoKGorSt, myClFeld.Length, bFillLeft);
                    break;
                case const_ArtFunc_EinheitKGorSt:
                    string EinheitKGorSt = GetEinheitKGorST(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, EinheitKGorSt, myClFeld.Length, bFillLeft);
                    break;

                //******************************************************************************************* spezielle Client Function

                case const_cFunction_VDAClientConstValue:
                    VDAClientWorkspaceValue = new clsVDAClientWorkspaceValue();
                    if (this.Lager.Eingang is clsLEingang)
                    {
                        VDAClientWorkspaceValue.InitClass(this.GL_User, this.Lager.Eingang.Auftraggeber, Lager.Eingang.Empfaenger, this.ASN.Job.ArbeitsbereichID); 
                    }
                    else
                    {
                        if (this.Lager.Ausgang is clsLAusgang)
                        {
                            VDAClientWorkspaceValue.InitClass(this.GL_User, this.Lager.Ausgang.Auftraggeber, Lager.Ausgang.Empfaenger, this.ASN.Job.ArbeitsbereichID);                                     
                        }
                    }
                    strTmp = string.Empty;
                    if (this.VDAClientWorkspaceValue.DictVDAClientWorkspaceValue.Count > 0)
                    {
                        if (this.VDAClientWorkspaceValue.DictVDAClientWorkspaceValue.ContainsKey(tmpCV.ASNFieldID))
                        {
                            clsVDAClientWorkspaceValue tmpClientWorkspaceValue = null;
                            if (this.VDAClientWorkspaceValue.DictVDAClientWorkspaceValue.TryGetValue(tmpCV.ASNFieldID, out tmpClientWorkspaceValue))
                            {
                                if (tmpClientWorkspaceValue is clsVDAClientWorkspaceValue)
                                {
                                    if (tmpClientWorkspaceValue.ID>0)
                                    {
                                        //check auf Function
                                        if (tmpClientWorkspaceValue.IsFunction)
                                        {
                                            GetValue(tmpClientWorkspaceValue.Value, ref tmpCV, ref myClFeld, ref strFeldSub, ref StringFillValue, ref asnTyp, ref strTmp, bFillLeft);
                                            strTmp = strFeldSub;
                                        }
                                        else
                                        {
                                            strTmp = tmpClientWorkspaceValue.Value;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case const_cFunction_SIL_716F03:
                    string strSIL_716F03 = silFunc.SIL_716F03(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strSIL_716F03, myClFeld.Length, bFillLeft);
                    break;

                case const_cFunction_SIL_ProdNrCHeck:
                    ////check Artikel Einlagerung bis 03.08.2015
                    strTmp = string.Empty;
                    strTmp = silFunc.SIL_ProdNrCHeck(ref this.Lager, asnTyp.Typ);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case const_cFunction_SZG_TMN:
                    strTmp = string.Empty;
                    strTmp = szgFunc.GetTMN(asnTyp.Typ, ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //******************************************************************************************* allgemeine Function
                case const_ArtFunc_ProduktionsnummerFillTo9With0:
                    string strFillTo9With0 = FillTo9With0(this.Lager.Artikel.Produktionsnummer);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strFillTo9With0, myClFeld.Length, bFillLeft);
                    break;
                case const_ArtFunc_Anzahlx1000:
                    strTmp = string.Empty;
                    strTmp = (this.Lager.Artikel.Anzahl*1000).ToString();
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                default:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, tmpCV.Value, myClFeld.Length, bFillLeft);
                    break;
            }
        }
        /******************************************************************************************
         *                          spez. Client Function
         * ***************************************************************************************/
        ///<summary>clsVDACreate / WerksnummerOhneBlank</summary>
        ///<remarks>Leerzeichen werden aus dem String entfernt</remarks>
        private string WerksnummerOhneBlank(clsArtikel myArt)
        {
            string strReturn = myArt.Werksnummer.Replace(" ", "");
            return strReturn;
        }
        ///<summary>clsVDACreate / WerksnummerMitBlank</summary>
        ///<remarks>Werknummer inc. führendem Leerzeichen</remarks>
        private string WerksnummerMitBlank(clsArtikel myArt)
        {
            string strReturn = " " + myArt.Werksnummer;
            return strReturn;
        }
        ///<summary>clsVDACreate / WerksnummerMitBlank</summary>
        ///<remarks>Werksnummer soll folgendes Format aufweisen:
        ///  Die "supertolle-Syntax" von VW (Achtung, Leerzeichen vorn):
        ///  " 123 123 123 12"
        ///  " 123 123 123 123"
        ///  " 123 123 123 PLA" -> wird NICHT berücksichtigt!
        ///  " 123 123 123 A  PLA"
        ///  " 123 123 123    PLA" 
        /// </remarks>
        private string WerksnummerVWFormat(clsArtikel myArt)
        {
            string strTmp = Regex.Replace(myArt.Werksnummer, " ", "");
            Int32 iLen = strTmp.Length;
            string str1To9 = string.Empty;
            string strRest = string.Empty;
            string strReturn = string.Empty;

            if (strTmp.Length > 8)
            {
                str1To9 = strTmp.Substring(0, 9);
                strRest = strTmp.Substring(9, (strTmp.Length - 9));

                str1To9 = str1To9.Insert(6, " ");
                str1To9 = str1To9.Insert(3, " ");
                strRest.Trim();

                if (strRest.Equals("PLA"))
                {
                    strRest = "    " + strRest;
                }
                else if (strRest.Equals("APLA"))
                {
                    strRest = " A  PLA";
                }
                strReturn = " " + str1To9 + strRest;
            }
            else
            {
                strReturn = " " + myArt.Werksnummer;
            }
            
            return strReturn;
        }
        ///<summary>clsVDACreate / GetBrutto</summary>
        ///<remarks></remarks>
        private string GetBrutto(clsArtikel myArt)
        {
            string strReturn = string.Empty;
            decimal decTmp = 0;
            decTmp = this.Lager.Artikel.Brutto * 1000;
            strReturn = Functions.FormatDecimalNoDiggits(decTmp);
            return strReturn;
        }
        ///<summary>clsVDACreate / GetBrutto</summary>
        ///<remarks></remarks>
        private string GetNetto(clsArtikel myArt)
        {
            string strReturn = string.Empty;
            decimal decTmp = 0;
            decTmp = this.Lager.Artikel.Netto * 1000;
            strReturn = Functions.FormatDecimalNoDiggits(decTmp);
            return strReturn;
        }
        ///<summary>clsVDACreate / GetBrutto</summary>
        ///<remarks></remarks>
        private string GetMenge(clsArtikel myArt)
        {
            string strReturn = string.Empty;
            decimal decTmp = 0;
            decTmp = this.Lager.Artikel.Anzahl * 1000;
            strReturn = Functions.FormatDecimalNoDiggits(decTmp);
            return strReturn;
        }
        ///<summary>clsVDACreate / GetBruttoOrAnzahl</summary>
        ///<remarks></remarks>
        private string GetBruttoOrAnzahl(clsArtikel myArt)
        {
            string strReturn = string.Empty;
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();
            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                strReturn = GetBrutto(myArt);
            }
            else
            {
                strReturn = GetMenge(myArt);
            }
            return strReturn;
        }
        ///<summary>clsVDACreate / GetNettoOrAnzahl</summary>
        ///<remarks></remarks>
        private string GetNettoOrAnzahl(clsArtikel myArt)
        {
            string strReturn = string.Empty;
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();
            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                strReturn = GetNetto(myArt);
            }
            else
            {
                strReturn = GetMenge(myArt);
            }
            return strReturn;
        }
        ///<summary>clsVDACreate / GetEinheitKGorST</summary>
        ///<remarks></remarks>
        private string GetEinheitKGorST(clsArtikel myArt)
        {
            string strReturn = string.Empty;
            myArt.GArt.ID = myArt.GArtID;
            myArt.GArt.Fill();
            if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
            {
                strReturn = "KG";
            }
            else
            {
                strReturn = "ST";
            }
            return strReturn;
        }
        /******************************************************************************************
         *                          allgemeine Function
         * ***************************************************************************************/
        ///<summary>clsVDACreate / FillValueWithstringToLenth</summary>
        ///<remarks>Value wird linksbündig auf 9stellen aufgefüllt</remarks>
        private string FillTo9With0(string myValue)
        {
            string strReturn = string.Empty;
            if (myValue.Length == 9)
            {
                strReturn = myValue;
            }
            else
            {
                if (myValue.Length > 9)
                {
                    strReturn = myValue.Substring(myValue.Length - 9);
                }
                else
                {
                    while (myValue.Length < 9)
                    {
                        //0 voranstelle
                        myValue = "0" + myValue;
                    }
                    strReturn = myValue;
                }
            }
            return strReturn;
        }
        ///<summary>clsVDACreate / GetSenderID</summary>
        ///<remarks>SENDEr ist bei ausgehenden ASN immer der Client.</remarks>
        private string GetSenderID(ref clsLagerdaten myLager)
        {
            string strReturn = string.Empty;
            if (myLager.Eingang is clsLEingang)
            {
                strReturn = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(this.Sys.Client.AdrID, myLager.Eingang.Auftraggeber, this.GL_User.User_ID, clsASN.const_ASNFiledTyp_VDA4913, this.ASN.Job.ArbeitsbereichID);

            }
            else
            {
                if (myLager.Ausgang is clsLAusgang)
                {
                    strReturn = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(this.Sys.Client.AdrID, myLager.Ausgang.Auftraggeber, this.GL_User.User_ID, clsASN.const_ASNFiledTyp_VDA4913, this.ASN.Job.ArbeitsbereichID);
                }
                else
                {
                    strReturn = string.Empty;
                }
            }
            return strReturn;
        }
    }
}
