using LVS.Communicator.EdiVDA;
using LVS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LVS
{
    public class clsEdiVDACreate
    {
        internal SIL_spFunc silFunc = new SIL_spFunc();
        internal SZG_spFunc szgFunc = new SZG_spFunc();

        internal clsStringCheck StrCheck = new clsStringCheck();
        internal clsVDAClientWorkspaceValue VDAClientWorkspaceValue;
        internal clsEdiVDAAssignValueAlias EdiVdaAssign;
        public List<clsVDAClientValue> listVDAClientValueSatz { get; set; }
        public List<string> ListIgnArticle { get; set; } = new List<string>();
        //internal string UNA1_GruppendatenelementTrennzeichen = ":";
        //internal string UNA2_SegmentDatenelementTrennzeichen = "+";
        //internal string UNA3_Dezimalzeichen = ".";
        //nternal string UNA4_Freigabezeichen = "?";
        //internal string UNA5_Reserviert = " ";
        //internal string UNA6_SegmentEndzeichen = "'";


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

        public List<string> ListEdiVDASatzString;
        public Dictionary<string, string> DictEdiVDAString;
        public clsVDAClientValue VDAClientVal;
        public clsADRVerweis AdrVerweis;
        public clsLagerdaten Lager;
        public clsASN ASN;

        public clsOrga Orga;

        public clsEdiSegment ediSegment;

        internal DataTable dtEingang = new DataTable();
        internal DataTable dtAusgang = new DataTable();
        internal DataTable dtArtikel = new DataTable();

        public List<clsLogbuchCon> ListErrorVDA = new List<clsLogbuchCon>();
        internal clsLogbuchCon tmpLog = new clsLogbuchCon();

        public string strFileID { get; set; }
        public int CounterLoop { get; set; }

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


        /*****************************************************************
         *          Methoden  /  Prozedures
         * **************************************************************/
        ///<summary>clsVDACreate / InitClass</summary>
        ///<remarks></remarks>
        public void InitClass(Globals._GL_USER myGLUser, clsJobs myJob, clsQueue myQueue, clsSystem mySys)
        {
            this.GL_User = myGLUser;
            this.Sys = mySys;

            //--- init Logdateien/Prozess
            ListErrorVDA = new List<clsLogbuchCon>();
            tmpLog = new clsLogbuchCon();
            tmpLog.GL_User = myGLUser;
            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
            tmpLog.TableName = "Queue";
            tmpLog.TableID = myQueue.ID;

            //--- beinhaltet die EdiStrings die hinterher zeile für zeile die DFÜ -Datei ergeben
            ListEdiVDASatzString = new List<string>();
            ListIgnArticle = new List<string>();

            this.ASN = new clsASN();
            this.ASN.InitClass(this.GLSystem, this.GL_User);
            this.ASN.Sys = this.Sys;
            ASN.ASNArt.ID = myJob.ASNArtID;
            ASN.ASNArt.Fill();
            ASN.Queue = myQueue;
            ASN.Job = myJob;
            ASN.ASNTyp = new clsASNTyp();
            ASN.ASNTyp.InitClass(ref this.GL_User, myJob.AsnTyp.ID);

            VDAClientVal = new clsVDAClientValue();
            VDAClientVal.GL_User = this.GL_User;
            VDAClientVal.InitClass(this.GL_User, myJob.AdrVerweisID, this.ASN.ASNArt);

            //prüfen, ob für diese Cliente VDA Definitionen vorliegen
            if (VDAClientVal.DictVDAClientValue.Count > 0)
            {
                //--- füllen der Segmente in Dict. und Liste 
                this.ASN.ASNArt.EdiSegment.ASNArtId = (int)this.ASN.ASNArt.ID;
                this.ASN.ASNArt.EdiSegment.FillbyASNArtID();
                this.ASN.ASNArt.EdiSegment.FillListAndDictEdiSegment();

                dtEingang = new DataTable();
                dtAusgang = new DataTable();
                dtArtikel = new DataTable();

                Lager = new clsLagerdaten();
                Lager.GLUser = this.GL_User;
                Lager.Sys = this.Sys;

                //in der ASN.Queue sind die Angaben Tablename und ID enhalten 
                //hierüber können alle Informationen ermittelt werden
                switch (ASN.Queue.TableName)
                {
                    case "LAusgang":
                        dtAusgang = Lager.GetLAusgangDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                        dtArtikel = Lager.GetArtikelDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                        //dtEingang = Lager.GetLEingangDatenFromLVS(ASN.Queue.TableName, ASN.Queue.TableID);
                        if ((dtAusgang.Rows.Count > 0) && (dtArtikel.Rows.Count > 0))
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

                                decimal decArtId = 0;
                                decimal.TryParse(dtArtikel.Rows[0]["ID"].ToString(), out decArtId);
                                if (decArtId > 0)
                                {
                                    this.Lager.Artikel = new clsArtikel();
                                    this.Lager.Artikel.InitClass(this.GL_User, this.GLSystem);
                                    this.Lager.Artikel.ID = decArtId;
                                    this.Lager.Artikel.GetArtikeldatenByTableID();


                                    Orga = new clsOrga();
                                    Orga.GL_User = this.GL_User;
                                    //Orga.AdrID = this.Lager.Ausgang.Auftraggeber;
                                    Orga.AdrID = ASN.Queue.AdrVerweisID;
                                    Orga.FillByAdrID();
                                    Orga.UpdateSendNr();

                                    CreateEdifactVDA();
                                }
                            }
                        }
                        else
                        {
                            string strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] -> Tablename:[" + myQueue.TableName + "] - TableID[" + myQueue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!";

                            //strError = CreateQueueDetailsString(ref myQueue, strError);

                            tmpLog.LogText = this.Prozess + ".[" + ASN.Queue.ASNArt.Typ + "].[VDACreate]: " + strError;
                            tmpLog.Datum = DateTime.Now;
                            this.ListErrorVDA.Add(tmpLog);
                            //Datensatz löschen
                            if (myQueue.Delete())
                            {
                                tmpLog = new clsLogbuchCon();
                                tmpLog.GL_User = myGLUser;
                                tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                tmpLog.TableName = "Queue";
                                tmpLog.TableID = myQueue.ID;
                                strError = string.Empty;
                                strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] wurde gelöscht.";
                                tmpLog.LogText = this.Prozess + ".[" + ASN.Queue.ASNArt.Typ + "].[VDACreate]: " + strError;
                                tmpLog.Datum = DateTime.Now;
                                this.ListErrorVDA.Add(tmpLog);
                            }
                        }
                        break;


                    case "Artikel":
                    case "LEingang":
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
                            this.Lager.Eingang = new clsLEingang();
                            this.Lager.GLUser = this.GL_User;
                            //this.Lager.Eingang.LEingangTableID = decETableIDTmp;
                            this.Lager.Eingang.LEingangTableID = this.Lager.Artikel.LEingangTableID;
                            this.Lager.Eingang.FillEingang();
                        }

                        decimal decATableIDTmp = 0;
                        if (dtAusgang.Rows.Count > 0)
                        {
                            //decimal.TryParse(dtAusgang.Rows[0]["ID"].ToString(), out decATableIDTmp);
                            if (this.Lager.Artikel.LAusgangTableID > 0)
                            {
                                this.Lager.Ausgang = new clsLAusgang();
                                this.Lager.GLUser = this.GL_User;
                                this.Lager.Ausgang.LAusgangTableID = this.Lager.Artikel.LAusgangTableID;
                                this.Lager.Ausgang.FillAusgang();
                            }
                        }

                        if (dtArtikel.Rows.Count > 0)
                        {
                            Orga = new clsOrga();
                            Orga.GL_User = this.GL_User;
                            Orga.AdrID = ASN.Queue.AdrVerweisID;
                            Orga.FillByAdrID();
                            Orga.UpdateSendNr();

                            CreateEdifactVDA();
                        }
                        else
                        {
                            string strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] -> Tablename:[" + myQueue.TableName + "] - TableID[" + myQueue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!"; ;
                            //strError = ediHelper_QueueDetails.CreateQueueDetailsString(myQueue, strError);

                            strError = helper_Queue_CreateQueueDetailString.CreateQueueDetailString(myQueue, strError);
                            tmpLog.LogText = this.Prozess + ".[" + ASN.Queue.ASNArt.Typ + "].[VDACreate]: " + strError;
                            this.ListErrorVDA.Add(tmpLog);
                            //Datensatz löschen
                            if (myQueue.Delete())
                            {
                                tmpLog = new clsLogbuchCon();
                                tmpLog.GL_User = myGLUser;
                                tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                tmpLog.TableName = "Queue";
                                tmpLog.TableID = myQueue.ID;
                                strError = string.Empty;
                                strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] wurde gelöscht.";
                                tmpLog.LogText = this.Prozess + ".[" + ASN.Queue.ASNArt.Typ + "].[VDACreate]: " + strError;
                                tmpLog.Datum = DateTime.Now;
                                this.ListErrorVDA.Add(tmpLog);
                            }
                        }
                        break;
                }//switch

                //-- Checke ob 
                if (!ListEdiVDASatzString.Any(x => x.Contains("UNZ")))
                {
                    string strError = "Datensatz: Queue ID: [" + myQueue.ID.ToString() + "] -> Tablename:[" + myQueue.TableName + "] - TableID[" + myQueue.TableID.ToString() + "] -> Meldung nicht vollständig !!!"; ;
                    //strError = ediHelper_QueueDetails.CreateQueueDetailsString(myQueue, strError);

                    strError = helper_Queue_CreateQueueDetailString.CreateQueueDetailString(myQueue, strError);
                    string text = this.Prozess + ".[" + ASN.Queue.ASNArt.Typ + "].[VDACreate]: " + strError + Environment.NewLine + Environment.NewLine;

                    text += "fehlerhafte Edifact Datei:" + Environment.NewLine;
                    foreach (string s in ListEdiVDASatzString)
                    {
                        text += s + Environment.NewLine;
                    }
                    tmpLog.LogText = text;

                    this.ListErrorVDA.Add(tmpLog);
                }

            }
        }
        /// <summary>
        ///             Vorgang:
        ///             1. Trennen von Artikel-Segmente und Rest
        ///             2. Trennen Rest
        ///                a) End => UNT, UNT Segmente
        ///                b) Kopf => restlichen Segmente aus Rest
        /// </summary>
        private void CreateEdifactVDA()
        {
            DictEdiVDAString = new Dictionary<string, string>();
            ListEdiVDASatzString = new List<string>();

            if (this.ASN.ASNArt.EdiSegment is clsEdiSegment)
            {
                try
                {
                    CounterLoop = 0;
                    //List<clsEdiSegment> ListEdiSegKopf = new List<clsEdiSegment>();
                    //List<clsEdiSegment> ListEdiSegArtikel = new List<clsEdiSegment>();
                    //List<clsEdiSegment> ListEdiSegEnd = new List<clsEdiSegment>();

                    // Kopfdaten ohne UNZ, UNT
                    foreach (var item in this.ASN.ASNArt.EdiSegment.DictEdiSegment)
                    {
                        string strElementString = string.Empty;
                        //in Vorbereitung zur Überarbeitung
                        //esVD = new EdiSegmentViewData()

                        this.ediSegment = (clsEdiSegment)item.Value;

                        //prüfen, ob das Segement für die Adresse aktiv ist
                        if (VDAClientVal.ListEdiSegments_activ.Contains(this.ediSegment.ID))
                        {
                            //prüfen, ob das Segement für die Adresse aktiv ist
                            if (
                                    (!this.ediSegment.Name.Equals(UNZ.Name)) &&
                                    (!this.ediSegment.Name.Equals(UNT.Name)) &&
                                    (VDAClientVal.ListEdiSegments_Head.Contains(this.ediSegment.ID)) &&
                                    (EdiSegmentCheck.Check(this))
                               )
                            {
                                strElementString = GetSegmentValue(this.ediSegment.Name);
                                if ((strElementString != null) && (!strElementString.Equals(string.Empty)))
                                {
                                    ListEdiVDASatzString.Add(strElementString);
                                }
                            }
                        }
                    }

                    // Daten pro Artikel
                    if (this.dtArtikel.Rows.Count > 0)
                    {
                        foreach (DataRow r in this.dtArtikel.Rows)
                        {
                            decimal decTmp = 0;
                            decimal.TryParse(r["ID"].ToString(), out decTmp);
                            if (decTmp > 0)
                            {
                                CounterLoop++;

                                this.Lager.Artikel = new clsArtikel();
                                this.Lager.Artikel.InitClass(this.GL_User, this.GLSystem);
                                this.Lager.Artikel.ID = decTmp;
                                this.Lager.Artikel.GetArtikeldatenByTableID();

                                if (!this.Lager.Artikel.GArt.IgnoreEdi)
                                {
                                    foreach (var item in this.ASN.ASNArt.EdiSegment.DictEdiSegment)
                                    {
                                        string strElementString = string.Empty;
                                        this.ediSegment = (clsEdiSegment)item.Value;
                                        //TEST
                                        //-- Check 
                                        this.ediSegment.IsActive = EdiSegmentCheck.Check(this);
                                        //prüfen, ob das Segement für die Adresse aktiv ist
                                        if (VDAClientVal.ListEdiSegments_activ.Contains(this.ediSegment.ID))
                                        {
                                            if (
                                                    (VDAClientVal.ListEdiSegments_Artikel.Contains(this.ediSegment.ID)) &&
                                                    (this.ediSegment.IsActive)
                                               )
                                            {
                                                strElementString = GetSegmentValue(this.ediSegment.Name);
                                                if (!strElementString.Equals(string.Empty))
                                                {
                                                    ListEdiVDASatzString.Add(strElementString);
                                                }
                                            }
                                        }
                                    } // Schleife DictEdiSegment
                                }
                                else
                                {
                                    string strTxt = helper_Article_CreateArticleIgnEdiDetailString.CreateArticleIgnEdiDetailString(this.Lager.Artikel);
                                    ListIgnArticle.Add(strTxt);
                                }//this.Lager.Artikel.GArt.IgnoreEdi
                            }
                        } //schleife Rows Artikel
                    }
                    // End-Segmente UNZ, UNT
                    foreach (var item in this.ASN.ASNArt.EdiSegment.DictEdiSegment)
                    {
                        this.ediSegment = (clsEdiSegment)item.Value;
                        //prüfen, ob das Segement für die Adresse aktiv ist
                        if (VDAClientVal.ListEdiSegments_activ.Contains(this.ediSegment.ID))
                        {
                            string strElementString = string.Empty;
                            //prüfen, ob das Segement für die Adresse aktiv ist
                            if (
                                    (
                                        (this.ediSegment.Name.Equals(UNZ.Name)) ||
                                        (this.ediSegment.Name.Equals(UNT.Name))
                                    ) &&
                                    (VDAClientVal.ListEdiSegments_Head.Contains(this.ediSegment.ID))
                               )
                            {
                                strElementString = GetSegmentValue(this.ediSegment.Name);
                                if ((strElementString != null) && (!strElementString.Equals(string.Empty)))
                                {
                                    ListEdiVDASatzString.Add(strElementString);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        private string GetSegmentValue(string mySegmentName)
        {
            string strReturn = string.Empty;
            switch (mySegmentName.ToUpper())
            {
                case ALI.Name:
                    ALI ali = new ALI(this);
                    strReturn = ali.Value;
                    break;

                case BGM.Name:
                    BGM bgm = new BGM(this);
                    strReturn = bgm.Value;
                    break;

                case COD.Name:
                    COD cod = new COD(this);
                    strReturn = cod.Value;
                    break;

                case CPS.Name:
                    CPS cps = new CPS(this);
                    strReturn = cps.Value;
                    break;

                case DGS.Name:
                    DGS dgs = new DGS(this);
                    strReturn = dgs.Value;
                    break;

                case DTM.Name:
                    DTM dtm = new DTM(this);
                    strReturn = dtm.Value;
                    break;

                case EQD.Name:
                    EQD eqd = new EQD(this);
                    strReturn = eqd.Value;
                    break;

                case FTX.Name:
                    FTX ftx = new FTX(this);
                    strReturn = ftx.Value;
                    break;

                case GIN.Name:
                    GIN gin = new GIN(this);
                    strReturn = gin.Value;
                    break;

                case GIR.Name:
                    GIR gir = new GIR(this);
                    strReturn = gir.Value;
                    break;

                case IMD.Name:
                    IMD imd = new IMD(this);
                    strReturn = imd.Value;
                    break;

                case INV.Name:
                    INV inv = new INV(this);
                    strReturn = inv.Value;
                    break;

                case LIN.Name:
                    LIN lin = new LIN(this);
                    strReturn = lin.Value;
                    break;

                case LOC.Name:
                    LOC loc = new LOC(this);
                    strReturn = loc.Value;
                    break;

                case MEA.Name:
                    MEA mea = new MEA(this);
                    strReturn = mea.Value;
                    break;
                case NAD.Name:
                    NAD nad = new NAD(this);
                    strReturn = nad.Value;
                    break;

                case PAC.Name:
                    PAC pac = new PAC(this);
                    strReturn = pac.Value;
                    break;

                case PCI.Name:
                    PCI pci = new PCI(this);
                    strReturn = pci.Value;
                    break;

                case PIA.Name:
                    PIA pia = new PIA(this);
                    strReturn = pia.Value;
                    break;

                case QTY.Name:
                    QTY qty = new QTY(this);
                    strReturn = qty.Value;
                    break;

                case RFF.Name:
                    RFF rff = new RFF(this);
                    strReturn = rff.Value;
                    break;

                case SEL.Name:
                    SEL sel = new SEL(this);
                    strReturn = sel.Value;
                    break;

                case TDT.Name:
                    TDT tdt = new TDT(this);
                    strReturn = tdt.Value;
                    break;

                case TOD.Name:
                    TOD tod = new TOD(this);
                    strReturn = tod.Value;
                    break;

                case UNA.Name:
                    UNA una = new UNA(this.ediSegment);
                    strReturn = una.Value;
                    break;

                case UNB.Name:
                    UNB unb = new UNB(this);
                    strReturn = unb.Value;
                    break;

                case UNH.Name:
                    UNH unh = new UNH(this);
                    strReturn = unh.Value;
                    break;

                case UNT.Name:
                    UNT unt = new UNT(this);
                    strReturn = unt.Value;
                    break;

                case UNZ.Name:
                    UNZ unz = new UNZ(this);
                    strReturn = unz.Value;
                    break;
            }
            return strReturn;
        }
    }
}
