using LVS.ASN.ASNFormatFunctions;
using LVS.Communicator.EdiVDA;
using LVS.Communicator.EdiVDA.EdiVDAValues;
using LVS.Constants;
using LVS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace LVS
{
    public class clsVDACreate
    {
        internal SIL_spFunc silFunc = new SIL_spFunc();
        internal SZG_spFunc szgFunc = new SZG_spFunc();
        internal SLE_spFunc sleFunc = new SLE_spFunc();

        internal clsStringCheck StrCheck = new clsStringCheck();
        internal clsVDAClientWorkspaceValue VDAClientWorkspaceValue;

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

        private string _EMPFAENGER;
        public string EMPFAENGER
        {
            get
            {
                string strTmp = string.Empty;
                string sql = "select Verweis from ADRVerweis " +
                                                "where VerweisAdrID=" + (int)ASN.Job.AdrVerweisID +
                                                " AND SenderAdrID=" + (int)ASN.Job.AdrVerweisID +
                                                " AND ArbeitsbereichID= " + (int)ASN.Job.ArbeitsbereichID;
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

        public string ReceiverNo
        {
            get
            {
                string strTmp = string.Empty;
                return strTmp;
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
        public string Count711 { get; set; }
        public string Count712 { get; set; }
        public string Count713 { get; set; }
        public string Count714 { get; set; }
        public string Count715 { get; set; }
        public string Count716 { get; set; }
        public string Count717 { get; set; }
        public string Count718 { get; set; }
        public string Count719 { get; set; }

        //public string Count711 = "0";
        //public string Count712 = "0";
        //public string Count713 = "0";
        //public string Count714 = "0";
        //public string Count715 = "0";
        //public string Count716 = "0";
        //public string Count717 = "0";
        //public string Count718 = "0";
        //public string Count719 = "0";

        internal clsLogbuchCon tmpLog = new clsLogbuchCon();
        public List<string> ListIgnArticle { get; set; } = new List<string>();

        public string strFileID { get; set; }

        /*****************************************************************
         *          Methoden  /  Prozedures
         * **************************************************************/
        ///<summary>clsVDACreate / InitClass</summary>
        ///<remarks></remarks>
        //public void InitClass(Globals._GL_USER myGLUser, clsJobs myJob, clsQueue myQueue, clsSystem mySys)
        public void InitClass(Globals._GL_USER myGLUser, clsASN myASN)
        {
            ListErrorVDA = new List<clsLogbuchCon>();
            tmpLog = new clsLogbuchCon();
            tmpLog.GL_User = myGLUser;
            tmpLog.Typ = enumLogArtItem.ERROR.ToString();
            tmpLog.TableName = "Queue";
            tmpLog.TableID = myASN.Queue.ID; // myQueue.ID;

            ListVDASatzString = new List<string>();

            this.GL_User = myGLUser;
            this.Sys = myASN.Sys; // mySys;
            this.ASN = myASN.Copy(); // new clsASN();
                                     //this.ASN.InitClass(this.GLSystem, this.GL_User);
                                     //this.ASN.Sys = this.Sys;
            if (
                    (myASN.Queue is clsQueue) &&
                    (myASN.Queue.ID > 0)
               )
            {
                ASN.ASNArt.ID = myASN.Queue.ASNArtId; // myJob.ASNArtID;
                ASN.ASNArt.Fill();
            }
            else
            {
            }

            //ASN.Queue = myASN.Queue; // myQueue;
            //ASN.Job = myASN.Job; // myJob;

            VDAClientVal = new clsVDAClientValue();
            VDAClientVal.GL_User = this.GL_User;
            VDAClientVal.InitClass(this.GL_User, myASN.Job.AdrVerweisID, ASN.ASNArt);

            //prüfen, ob für diese Cliente VDA Definitionen vorliegen
            if ((VDAClientVal.DictVDAClientValue != null) && (VDAClientVal.DictVDAClientValue.Count > 0))
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
                Lager.Sys = this.Sys;

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

                        bool bDoUpdate = true;
                        if (this.ListErrorVDA.Count == 0)
                        {
                            if (!this.Sys.DebugModeCOM)
                            {
                                enumPeriode tmpP = enumPeriode.täglich;
                                if (!this.ASN.Job.Periode.Equals(string.Empty))
                                {
                                    tmpP = (enumPeriode)Enum.Parse(typeof(enumPeriode), this.ASN.Job.Periode);
                                    if (tmpP == enumPeriode.NotSet)
                                    {
                                        bDoUpdate = false;
                                    }
                                }
                                if (bDoUpdate)
                                {
                                    this.ASN.Job.ActionDate = helper_Job_ActionDate.GetNextActionDate(tmpP, this.ASN.Job.ActionDate);
                                    //-- Update in DB durchführen
                                    this.ASN.Job.Update();
                                }
                                //DateTime dtActionDate = DateTime.Now;
                                //TimeSpan dtDiff = new TimeSpan();
                                //dtDiff = (dtActionDate - this.ASN.Job.ActionDate);
                                //switch (tmpP)
                                //{
                                //    case enumPeriode.täglich:
                                //        if (dtDiff.Days > -1)
                                //        {
                                //            dtActionDate = this.ASN.Job.ActionDate.AddDays(1);
                                //            while (dtActionDate < DateTime.Now)
                                //            {
                                //                dtActionDate = dtActionDate.AddDays(1);
                                //            }
                                //        }
                                //        else
                                //        {
                                //            dtActionDate = DateTime.Now.AddDays(1);
                                //        }
                                //        break;
                                //    case enumPeriode.wöchtentlich:
                                //        if (dtDiff.Days > 7)
                                //        {
                                //            dtActionDate = this.ASN.Job.ActionDate.AddDays(7);
                                //            while (dtActionDate < DateTime.Now)
                                //            {
                                //                dtActionDate = dtActionDate.AddDays(7);
                                //            }
                                //        }
                                //        else
                                //        {
                                //            dtActionDate = this.ASN.Job.ActionDate.AddDays(7);
                                //        }
                                //        break;

                                //    case enumPeriode.monatlich:
                                //        if (dtDiff.Days < 32)
                                //        {
                                //            while (dtActionDate < DateTime.Now)
                                //            {
                                //                dtActionDate = this.ASN.Job.ActionDate.AddMonths(1);
                                //            }
                                //        }
                                //        else
                                //        {
                                //            dtActionDate = Convert.ToDateTime(this.ASN.Job.ActionDate.Day.ToString() + "." + DateTime.Now.AddMonths(1).Month.ToString() + "." + DateTime.Now.AddMonths(1).Year.ToString());
                                //        }
                                //        break;

                                //    case enumPeriode.jährlich:
                                //        if (dtDiff.Days < 366)
                                //        {
                                //            dtActionDate = this.ASN.Job.ActionDate.AddYears(1);
                                //        }
                                //        else
                                //        {
                                //            dtActionDate = Convert.ToDateTime(this.ASN.Job.ActionDate.Day.ToString() + "." + this.ASN.Job.ActionDate.Month.ToString() + "." + DateTime.Now.AddYears(1).Year.ToString());
                                //        }
                                //        break;

                                //    case enumPeriode.immer:
                                //        //this.ASN.Job.ActionDate = this.ASN.Job.ActionDate.AddDays(1);
                                //        break;
                                //    case enumPeriode.NotSet:
                                //        bDoUpdate = false;
                                //        break;
                                //}
                                //Update nur wenn Periode auch gesetzt ist
                                //if (bDoUpdate)
                                //{
                                //    this.ASN.Job.ActionDate = dtActionDate;
                                //    //-- Update in DB durchführen
                                //    this.ASN.Job.Update();
                                //}
                            }
                        }
                    }
                    else
                    {
                        // keine Artikel vorhanden, also kann auch keine BM erstellt werden
                        // es muss aber in Job.ActionDate upgedatet werden

                        string strError = "Aktuell sind keine Artikel im Bestand!";
                        tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate].[" + this.ASN.Job.AsnTyp.Typ + "]: " + strError;
                        tmpLog.Datum = DateTime.Now;
                        this.ListErrorVDA.Add(tmpLog);

                        bool bDoUpdate = true;
                        enumPeriode tmpP = enumPeriode.täglich;
                        if (!this.ASN.Job.Periode.Equals(string.Empty))
                        {
                            tmpP = (enumPeriode)Enum.Parse(typeof(enumPeriode), this.ASN.Job.Periode);
                            if (tmpP == enumPeriode.NotSet)
                            {
                                bDoUpdate = false;
                            }
                        }
                        if (bDoUpdate)
                        {
                            this.ASN.Job.ActionDate = helper_Job_ActionDate.GetNextActionDate(tmpP, this.ASN.Job.ActionDate);
                            //-- Update in DB durchführen
                            this.ASN.Job.Update();
                        }
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
                                string strError = "Datensatz: Queue ID: [" + myASN.Queue.ID.ToString() + "] -> Tablename:[" + myASN.Queue.TableName + "] - TableID[" + myASN.Queue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!";
                                //strError = CreateQueueDetailsString(ref myASN.Queue, strError);
                                strError = Helper.helper_Queue_CreateQueueDetailString.CreateQueueDetailString(myASN.Queue, strError);
                                tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                tmpLog.Datum = DateTime.Now;
                                this.ListErrorVDA.Add(tmpLog);

                                //Datensatz löschen
                                if (myASN.Queue.Delete())
                                {
                                    tmpLog = new clsLogbuchCon();
                                    tmpLog.GL_User = myGLUser;
                                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                    tmpLog.TableName = "Queue";
                                    tmpLog.TableID = myASN.Queue.ID;
                                    strError = string.Empty;
                                    strError = "Datensatz: Queue ID: [" + myASN.Queue.ID.ToString() + "] wurde gelöscht.";
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
                                string strError = "Datensatz: Queue ID: [" + myASN.Queue.ID.ToString() + "] -> Tablename:[" + myASN.Queue.TableName + "] - TableID[" + myASN.Queue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!";
                                //strError = CreateQueueDetailsString(ref myASN.Queue, strError);

                                strError = helper_Queue_CreateQueueDetailString.CreateQueueDetailString(myASN.Queue, strError);
                                tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                tmpLog.Datum = DateTime.Now;
                                this.ListErrorVDA.Add(tmpLog);
                                //Datensatz löschen
                                if (myASN.Queue.Delete())
                                {
                                    tmpLog = new clsLogbuchCon();
                                    tmpLog.GL_User = myGLUser;
                                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                    tmpLog.TableName = "Queue";
                                    tmpLog.TableID = myASN.Queue.ID;
                                    strError = string.Empty;
                                    strError = "Datensatz: Queue ID: [" + myASN.Queue.ID.ToString() + "] wurde gelöscht.";
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
                                string strError = "Datensatz: Queue ID: [" + myASN.Queue.ID.ToString() + "] -> Tablename:[" + myASN.Queue.TableName + "] - TableID[" + myASN.Queue.TableID.ToString() + "] -> Datensatz zum Table und ID nicht vorhanden!!!"; ;
                                //strError = CreateQueueDetailsString(ref myASN.Queue, strError);

                                strError = helper_Queue_CreateQueueDetailString.CreateQueueDetailString(myASN.Queue, strError);
                                tmpLog.LogText = this.Prozess + ".[VDA4913].[VDACreate]: " + strError;
                                this.ListErrorVDA.Add(tmpLog);
                                //Datensatz löschen
                                if (myASN.Queue.Delete())
                                {
                                    tmpLog = new clsLogbuchCon();
                                    tmpLog.GL_User = myGLUser;
                                    tmpLog.Typ = enumLogArtItem.ERROR.ToString();
                                    tmpLog.TableName = "Queue";
                                    tmpLog.TableID = myASN.Queue.ID;
                                    strError = string.Empty;
                                    strError = "Datensatz: Queue ID: [" + myASN.Queue.ID.ToString() + "] wurde gelöscht.";
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
        /////<summary>clsVDACreate / CreateQueueDetailsString</summary>
        /////<remarks></remarks>
        //private string CreateQueueDetailsString(ref clsQueue myQueue, string myErrorText)
        //{
        //    return myErrorText + Environment.NewLine +
        //                                   "Details:" + Environment.NewLine +
        //                                   "ID: [ " + myQueue.ID.ToString() + " ] " + Environment.NewLine +
        //                                   "Tablename: [ " + myQueue.TableName + " ] " + Environment.NewLine +
        //                                   "TableID: [ " + myQueue.TableID.ToString() + " ] " + Environment.NewLine +
        //                                   "Datum: [ " + myQueue.Datum.ToString() + " ] " + Environment.NewLine +
        //                                   "ASNTypID: [ " + myQueue.ASNTypID.ToString() + " - " + myQueue.ASNTyp.Typ + " ]" + Environment.NewLine +
        //                                   "ASNID: [ " + myQueue.ASNID.ToString() + " ]" + Environment.NewLine +
        //                                   "AdrVerweisID: [ " + myQueue.AdrVerweisID.ToString() + " ]" + Environment.NewLine +
        //                                   "ASNAction: [ " + myQueue.ASNAction.ToString() + " ] " + Environment.NewLine;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="myArticle"></param>
        ///// <returns></returns>
        //private string CreateArticleIgnDetailString(clsArtikel myArticle)
        //{
        //    string strMes = string.Empty;
        //    strMes += Environment.NewLine;
        //    strMes += string.Format("[Artikel-Id] LVSNr: \t [{0}] - {1}", myArticle.ID, myArticle.LVS_ID) + Environment.NewLine;
        //    strMes += string.Format("[GArt-Id] Gut: \t [{0}] - {1}", myArticle.GArt.ID, myArticle.GArt.ViewID) + Environment.NewLine;
        //    strMes += string.Format("IgnEDI: \t [{0}] ", myArticle.GArt.IgnoreEdi.ToString()) + Environment.NewLine;
        //    return strMes;
        //}
        ///<summary>clsVDACreate / CreateVDA</summary>
        ///<remarks></remarks>
        private void CreateVDAFirst()
        {
            ListIgnArticle = new List<string>();

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

                    if (!this.Lager.Artikel.GArt.IgnoreEdi)
                    {
                        s714Create(ref i714);
                        s715Create(ref i715);
                        s716Create(ref i716);
                        s717Create(ref i717);
                        s718Create(ref i718);
                    }//this.Lager.Artikel.GArt.IgnoreEdi
                    else
                    {
                        string strTxt = helper_Article_CreateArticleIgnEdiDetailString.CreateArticleIgnEdiDetailString(this.Lager.Artikel);
                        ListIgnArticle.Add(strTxt);
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
        ///<summary>clsVDACreate / CreateVDA</summary>
        ///<remarks></remarks>
        private void CreateVDA()
        {
            ListIgnArticle = new List<string>();

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
                if (dtArtikel.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < 1; i++)
                    {
                        decimal decTmp = 0;
                        Decimal.TryParse(dtArtikel.Rows[i]["ID"].ToString(), out decTmp);
                        if (decTmp > 0)
                        {
                            this.Lager.Artikel = new clsArtikel();
                            this.Lager.Artikel._GL_User = this.GL_User;
                            this.Lager.Artikel.ID = decTmp;
                            this.Lager.Artikel.GetArtikeldatenByTableID();
                        }
                    }
                }


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

                        if (!this.Lager.Artikel.GArt.IgnoreEdi)
                        {
                            //notwendig, falls iDocs auf letzten geänderte Daten zugreifen müssen
                            //-Beispiel BMW
                            if ((this.ASN.Queue.ASNActionCls is clsASNAction) && (this.ASN.Queue.ASNActionCls.UseOldPropertyValue))
                            {
                                switch (this.ASN.Queue.ASNActionCls.ASNActionProcessNr)
                                {
                                    case clsASNAction.const_ASNAction_StornoKorrektur:

                                        switch (this.ASN.Queue.ASNTyp.TypID)
                                        {
                                            case clsASNTyp.const_ASNTyp_STE:
                                            case clsASNTyp.const_ASNTyp_STL:
                                                this.Lager.Artikel = ediHelper_CheckArtChangedProperties.ResetLastPropertyChangesByArtikel(this.Lager.Artikel, this.GL_User);
                                                break;
                                        }
                                        break;
                                }
                            }
                            //neuer Schleifendurchlauf, für die mögliche Sätze 713 bis 718 
                            // Anzahl wird in DB festgelegt VDAClientOut Feld ArtSatz im 1.Feld des Satz und NextSatz muss größer 0 sein
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
                        }// GArt.IgnoreEdi
                        else
                        {
                            string strTxt = helper_Article_CreateArticleIgnEdiDetailString.CreateArticleIgnEdiDetailString(this.Lager.Artikel);
                            ListIgnArticle.Add(strTxt);
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
                    string strTmp = Helper.helper_VDALogText.VDACreate(this);
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
                    string strTmp = Helper.helper_VDALogText.VDACreate(this);
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
                strReturnException += "ASNArtSatzFeld/ID: " + tmpFeld.ID.ToString() + Environment.NewLine;
                strReturnException += "ASNArtSatzFeld/Pos: " + tmpFeld.Pos.ToString() + Environment.NewLine;
                strReturnException += "ASNArtSatzFeld/Datenfeld: " + tmpFeld.Datenfeld + Environment.NewLine;
                strReturnException += "ASNArtSatzFeld/Length: " + tmpFeld.Length.ToString() + Environment.NewLine;
                strReturnException += "ASNArtSatzFeld/von: " + tmpFeld.VonLength.ToString() + Environment.NewLine;
                strReturnException += "ASNArtSatzFeld/bis: " + tmpFeld.BisLength.ToString() + Environment.NewLine;

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
                    if (tmpFeld.Kennung.Equals(clsASN.const_VDA4913SatzField_SATZ712F12))
                    {
                        string str = string.Empty;
                    }


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
                                if (tmpFeld.Kennung.Equals(clsASN.const_VDA4913SatzField_SATZ714F14))
                                {
                                    string str = string.Empty;
                                }
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

            this.VDAClientVal.SetListSatzFromDictByKey("719");  //mr 2019_07_11
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
                        try
                        {
                            str719 = str719 + GetFieldValueForVDASatz(ref tmpFeld, this.VDAClientVal.listVDAClientValueSatz); //mr 2019_07_11
                            Int32 iCh = str719.Length;
                        }
                        catch (Exception ex)
                        {
                            strException = GetErrorExceptionString(ex.ToString(), ref tmpFeld);
                        }
                        //str719 = str719 + "03";   //mr 2019_07_11
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
                    (StrToFill.Equals(clsEdiVDAValueAlias.const_VDA_Value_Blanks)) || (StrToFill.Equals(""))
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
                string str = string.Empty;
                //test
                switch ((int)tmpCV.ASNFieldID)
                {
                    //711F03
                    case 3:
                        str = string.Empty;
                        break;
                    //714F03
                    case 57:
                        str = string.Empty;
                        break;
                    //714F14
                    case 68:
                        str = string.Empty;
                        break;
                    //716G03
                    case 95:
                        str = string.Empty;
                        break;
                }

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
                        tmpLog.LogText = tmpLog.LogText + "|" + strFeldSub + "|-> " + myClFeld.Kennung + " - [" + myClFeld.Datenfeld + "] - Länge: SOLL[" + myClFeld.Length + "] / IST[" + strFeldSub.Length.ToString() + "]" + Environment.NewLine;
                        tmpLog.LogText = tmpLog.LogText + "-> strFeldSub = [" + strFeldSub + "]" + Environment.NewLine;
                    }
                    SatzString = SatzString + strFeldSub;
                    i = myListVDAClientValue.Count;
                }
            }
            return SatzString;
        }
        ///<summary>clsVDACreate / GetValue</summary>
        ///<remarks></remarks>
        public void GetValue(string strValueArt, ref clsVDAClientValue tmpCV, ref clsASNArtSatzFeld myClFeld, ref string strFeldSub, ref string StringFillValue, ref clsASNTyp asnTyp, ref string strTmp, bool bFillLeft)
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
                            strFeld = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, tmpCV.Value, myClFeld.Length, bFillLeft);
                        }
                        else
                        {
                            //length ist größer ==> kürzen
                            strTmp = string.Empty;
                            strTmp = tmpCV.Value.Substring(0, myClFeld.Length);
                            strFeld = strTmp;
                        }
                        strFeldSub = strFeld;
                    }
                    break;

                case clsEdiVDAValueAlias.const_Lieferantennummer:
                    strTmp = string.Empty;
                    strTmp = SupplierNo.Execute(this.GL_User, this.Lager, this.ASN); //GetLieferantenNummer(ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_SupplierNo:
                    strTmp = string.Empty;
                    strTmp = SupplierNo.Execute(this.GL_User, this.Lager, this.ASN);  //GetSupplierNo(ref this.Lager);                    
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_DUNSNrSF:
                case clsEdiVDAValueAlias.const_DUNSNrST:
                case clsEdiVDAValueAlias.const_DUNSNrSE:
                case clsEdiVDAValueAlias.const_DUNSNrFW:
                    strTmp = string.Empty;
                    strTmp = DUNSNr.Execute(this.Lager, strValueArt);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //******************************************************************** 711
                case clsEdiVDAValueAlias.const_Reciever:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, EMPFAENGER, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_RecieverNo:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, ReceiverNo, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Sender:
                    strTmp = string.Empty;
                    strTmp = Sender.Execute(this.GL_User, this.ASN, asnTyp, this.Lager);  //GetSenderID(ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_SIDOld:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, SIDOld, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_SIDNew:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, SIDNew, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_SystemId_Queue:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.ASN.Queue.ID.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_VDA_Value_NOW:
                    strFeldSub = this.NOW;
                    break;
                case clsEdiVDAValueAlias.const_VDA_Value_TimeNow:
                    strFeldSub = this.NOWTIME;
                    break;

                case clsEdiVDAValueAlias.const_VDA_Value_Blanks:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, tmpCV.Value, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_TransportMittelSchlüssel:
                    strFeldSub = TMS.Execute(asnTyp, this.Lager);
                    break;
                case clsEdiVDAValueAlias.const_TransportMittelNummer:
                    strTmp = string.Empty;
                    strTmp = TMN.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_TransportMittelNummerTrim:
                    strTmp = string.Empty;
                    strTmp = TMN.Execute(asnTyp, this.Lager);
                    strTmp = Regex.Replace(strTmp, " ", "");
                    strTmp = Regex.Replace(strTmp, "-", "");
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_Vorgang:
                    strTmp = string.Empty;
                    strTmp = LVS.Communicator.EdiVDA.EdiVDAValues.VGS.Execute(asnTyp);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //********************************************************************** LEingangdaten
                case clsEdiVDAValueAlias.const_Eingang_ID:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangTableID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_EingangID:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_Datum:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.LEingangDate.ToString("yyMMdd");
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_LfsNr:
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

                case clsEdiVDAValueAlias.const_Eingang_ExTransportRef:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ExTransportRef.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_ExAuftragRef:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ExAuftragRef.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_Brutto:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.Brutto.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_Netto:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.Netto.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Eingang_Anzahl:
                    strTmp = string.Empty;
                    if (this.Lager.Eingang != null)
                    {
                        strTmp = this.Lager.Eingang.ArtikelCount.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EFunc_LieferantennummerDeleteSlash:
                    strTmp = string.Empty;
                    strTmp = LiefNrDeleteSlash.Execute(this.Lager, this.ASN.ASNArt);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //********************************************************************** LAusgangsdaten
                case clsEdiVDAValueAlias.const_Ausgang_ID:
                    strTmp = string.Empty;
                    if (this.Lager.Ausgang != null)
                    {
                        strTmp = this.Lager.Ausgang.LAusgangTableID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Ausgang_LAusgangID:
                    strTmp = string.Empty;
                    if (this.Lager.Ausgang != null)
                    {
                        strTmp = this.Lager.Ausgang.LAusgangID.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Ausgang_SLB:
                    strTmp = string.Empty;
                    if (this.Lager.Ausgang != null)
                    {
                        strTmp = this.Lager.Ausgang.SLB.ToString();
                    }
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //***************************************************************************************** EA

                case clsEdiVDAValueAlias.const_EA_ArtPosEoA:
                    strTmp = string.Empty;
                    strTmp = EA_ArtPosEoA.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_ID:
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
                case clsEdiVDAValueAlias.const_EA_EingangId:
                    strTmp = string.Empty;
                    strTmp = EA_EingangId.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_AusgangId:
                    strTmp = string.Empty;
                    strTmp = EA_AusgangId.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_EANr:
                    strTmp = string.Empty;
                    strTmp = EA_No.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EAFunc_EANrWithPrefix:
                    strTmp = string.Empty;
                    strTmp = EA_NoWithPrefix.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_Datum:
                    strTmp = string.Empty;
                    strTmp = EA_Datum.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_KFZ:
                    strTmp = string.Empty;
                    strTmp = EA_KFZ.Execute(asnTyp, this.Lager, this.ASN.ASNArt);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_LfsNr:
                    strTmp = string.Empty;
                    strTmp = EA_LfsNr.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_ExTransportRef:
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
                case clsEdiVDAValueAlias.const_EA_ExAuftragRef:
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
                case clsEdiVDAValueAlias.const_EA_Brutto:
                    strTmp = string.Empty;
                    strTmp = EA_Brutto.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_Netto:
                    strTmp = string.Empty;
                    strTmp = EA_Netto.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_Anzahl:
                    strTmp = string.Empty;
                    strTmp = EA_Anzahl.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EAFunc_EAAnzahlX1000:
                    strTmp = string.Empty;
                    strTmp = EA_AnzahlX1000.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_ArtikelCount:
                    strTmp = string.Empty;
                    strTmp = EA_ArtikelCount.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_EA_Termin:
                    strTmp = string.Empty;
                    strTmp = EA_Anzahl.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_SLB:
                    strTmp = string.Empty;
                    strTmp = EA_SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_MATDate:
                    strTmp = string.Empty;
                    strTmp = EA_MATDate.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_EA_MATTime:
                    strTmp = string.Empty;
                    strTmp = EA_MATTime.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //****************************************************************************************** Artikeldaten
                case clsEdiVDAValueAlias.const_Artikel_ID:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.ID.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_LVSNr:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.LVS_ID.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_LVSNrBeforeUB:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.LVSNrBeforeUB.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Netto:
                    //strTmp = GetNetto(this.Lager.Artikel);
                    strTmp = Artikel_Netto.ExecuteXFactor(this.Lager.Artikel, 1000);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Brutto:
                    //strTmp = GetBrutto(this.Lager.Artikel);
                    strTmp = Artikel_Brutto.ExecuteXFactor(this.Lager.Artikel, 1000);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Gut:
                    strTmp = Artikel_Gueterart.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Dicke:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Dicke);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Breite:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Breite);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Laenge:
                    strTmp = Functions.FormatDecimalNoDiggits(this.Lager.Artikel.Laenge);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Anzahl:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Anzahl.ToString(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Einheit:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Einheit.ToUpper(), myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Werksnummer:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Werksnummer, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Produktionsnummer:
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
                case clsEdiVDAValueAlias.const_Artikel_Charge:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Charge, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_BestellNr:
                case "Artikel.BestellNr":
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Bestellnummer, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_exAuftrag:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exAuftrag, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_exAuftragPos:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exAuftragPos, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_exBezeichnung:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exBezeichnung, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_exMaterialNr:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.exMaterialnummer, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Pos:
                case "Artikel.Pos":
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Position, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_ArtIDRef:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.ArtIDRef, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_SchadenTopOne:
                    strTmp = string.Empty;
                    //strTmp = this.Lager.Artikel.SchadenTopOne.Trim();
                    //StrCheck.CheckString(ref strTmp);
                    strTmp = VW_SchadenText.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_AbrufRef:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.AbrufReferenz, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_TARef:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.TARef, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Güte:
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, this.Lager.Artikel.Guete, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_Artikel_Glühdatum:
                    strTmp = string.Empty;
                    strTmp = this.Lager.Artikel.GlowDate.ToString("ddMMyyyy");
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;




                //****************************************************************************************** Functions Artikeldaten
                case clsEdiVDAValueAlias.const_ArtFunc_WerksnummerOhneBlank:
                    string strWNrOhneBlank = WerksnummerOhneBlank.Execute(this.Lager); //WerksnummerOhneBlank(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrOhneBlank, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_WerksnummerMitFuehrendemBlank:
                    string strWNrMitFBlank = WerksnummerMitFBlank.Execute(this.Lager); // WerksnummerMitBlank(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrMitFBlank, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunk_WerksnummerFormatVW:
                    string strWNrVWFormat = VW_WerksnummerFormat.Execute(this.Lager.Artikel); // WerksnummerVWFormat(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrVWFormat, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunk_WerksnummerWihtHyphen:
                    string strWNrWithHypen = WerksnummerWithHyphen.Execute(this.Lager.Artikel.Werksnummer);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrWithHypen, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunk_WerksnummerWihtOutHyphen:
                    string strWNrWithOutHypen = WerksnummerWithOutHyphen.Execute(this.Lager.Artikel.Werksnummer);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strWNrWithOutHypen, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_BruttoKGorSt:
                    //string strBruttoKGorSt = GetBruttoOrAnzahl(this.Lager.Artikel);
                    string strBruttoKGorSt = VW_BruttoKGorST.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strBruttoKGorSt, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_QTY_BruttoOrStk:
                    strTmp = VW_QTY_BruttoOrStk.Execute(this.Lager.Artikel);
                    strFeldSub = ediHelper_FormatString.FillValueToLength(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_NettoKGorSt:
                    //string strNettoKGorSt = GetNettoOrAnzahl(this.Lager.Artikel);
                    string strNettoKGorSt = VW_NettoKGorST.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strNettoKGorSt, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_EinheitKGorSt:
                    //string EinheitKGorSt = GetEinheitKGorST(this.Lager.Artikel);
                    string EinheitKGorSt = VW_EinheitKGorST.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, EinheitKGorSt, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_QTY_EinheitPCIorKGM:
                    strTmp = VW_QTY_EinheitPCIorKGM.Execute(this.Lager.Artikel);
                    strFeldSub = ediHelper_FormatString.FillValueToLength(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_BruttoTO:
                    strTmp = string.Empty;
                    strTmp = Artikel_BruttoTO.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_NettoTO:
                    strTmp = string.Empty;
                    strTmp = Artikel_NettoTO.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_cFunction_VW_SupplierNo:
                    strTmp = VW_SupplierNo.Execute(this.GL_User, this.Lager, this.ASN);
                    strFeldSub = ediHelper_FormatString.FillValueToLength(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                //******************************************************************************************* spezielle Client Function

                case clsEdiVDAValueAlias.const_cFunction_VDACustomizedValue:
                    VDAClientWorkspaceValue = new clsVDAClientWorkspaceValue();
                    if (this.Lager.Eingang is clsLEingang)
                    {
                        VDAClientWorkspaceValue.InitClass(this.GL_User, this.Lager.Eingang.Auftraggeber, Lager.Eingang.Empfaenger, this.ASN.Job.ArbeitsbereichID);
                    }
                    else if (this.Lager.Artikel.Eingang is clsLEingang)
                    {
                        VDAClientWorkspaceValue.InitClass(this.GL_User, this.Lager.Artikel.Eingang.Auftraggeber, Lager.Artikel.Eingang.Empfaenger, this.ASN.Job.ArbeitsbereichID);
                    }
                    else
                    {
                        if (this.Lager.Ausgang is clsLAusgang)
                        {
                            VDAClientWorkspaceValue.InitClass(this.GL_User, this.Lager.Ausgang.Auftraggeber, Lager.Ausgang.Empfaenger, this.ASN.Job.ArbeitsbereichID);
                        }
                        else if (this.Lager.Artikel.Ausgang is clsLAusgang)
                        {
                            VDAClientWorkspaceValue.InitClass(this.GL_User, this.Lager.Artikel.Ausgang.Auftraggeber, Lager.Artikel.Ausgang.Empfaenger, this.ASN.Job.ArbeitsbereichID);
                        }
                    }
                    strTmp = string.Empty;
                    if (this.VDAClientWorkspaceValue.DictVDAClientWorkspaceValue.Count > 0)
                    {
                        if (tmpCV.ASNFieldID > 90)
                        {
                            string str = string.Empty;
                        }

                        if (this.VDAClientWorkspaceValue.DictVDAClientWorkspaceValue.ContainsKey(tmpCV.ASNFieldID))
                        {
                            clsVDAClientWorkspaceValue tmpClientWorkspaceValue = null;
                            if (this.VDAClientWorkspaceValue.DictVDAClientWorkspaceValue.TryGetValue(tmpCV.ASNFieldID, out tmpClientWorkspaceValue))
                            {
                                if (tmpClientWorkspaceValue is clsVDAClientWorkspaceValue)
                                {
                                    if (tmpClientWorkspaceValue.ID > 0)
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

                case clsEdiVDAValueAlias.const_cFunction_SIL_716F03:
                    string strSIL_716F03 = silFunc.SIL_716F03(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strSIL_716F03, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_SIL_ProdNrCHeck:
                    ////check Artikel Einlagerung bis 03.08.2015
                    strTmp = string.Empty;
                    strTmp = silFunc.SIL_ProdNrCHeck(ref this.Lager, asnTyp.Typ);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_SZG_TMN:
                    strTmp = string.Empty;
                    //strTmp = szgFunc.GetTMN(asnTyp.Typ, ref this.Lager);
                    strTmp = SZG_TMN.Execute(asnTyp.Typ, ref this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_SZG_LVSForVW:
                    strTmp = string.Empty;
                    //strTmp = szgFunc.GetLVSIdForVW(ref this.Lager);
                    strTmp = VW_LVSForVW.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_SLE_VGS:
                    strTmp = string.Empty;
                    strTmp = sleFunc.GetVGS(asnTyp.Typ);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_Tata_713F03LfsNr:
                    strTmp = string.Empty;
                    strTmp = Tata_713F03LfsNr.Execute(asnTyp, this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_VOEST_EA_SLB:
                    strTmp = string.Empty;
                    strTmp = VOEST_EA_SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_SLBWithPrefix:
                    strTmp = string.Empty;
                    strTmp = SLBWithPrefix.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_Arcelor_EABmwFormat:
                    strTmp = string.Empty;
                    strTmp = Arcelor_EA_BMWFormat.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_SLB:
                    strTmp = string.Empty;
                    strTmp = BMW_SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_VGS:
                    strTmp = string.Empty;
                    strTmp = BMW_VGS.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_EANo:
                    strTmp = string.Empty;
                    strTmp = BMW_EANo.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_EANoWithPrefix:
                    strTmp = string.Empty;
                    strTmp = BMW_EANoWithPrefix.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_PACNumberBolzen:
                    strTmp = string.Empty;
                    strTmp = BMW_PACNumberBolzen.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_GlowDate:
                    strTmp = string.Empty;
                    strTmp = BMW_GlowDate.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_MATDate:
                    strTmp = string.Empty;
                    strTmp = BMW_MATDate.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_MATTime:
                    strTmp = string.Empty;
                    strTmp = BMW_MATTime.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_Schaden:
                    strTmp = string.Empty;
                    strTmp = BMW_Schaden.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_EDIFACTSenderId:
                    strTmp = string.Empty;
                    strTmp = BMW_UNB_S002_0004.const_BMW_SenderIdentification;
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_Einheit:
                    strTmp = string.Empty;
                    strTmp = BMW_Einheit.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_713F17_KGorSTK:
                    strTmp = string.Empty;
                    strTmp = BMW_713F17KGorSTK.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_714F06Brutto:
                    strTmp = string.Empty;
                    strTmp = BMW_714F06Brutto.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_714F08Netto:
                    strTmp = string.Empty;
                    strTmp = BMW_714F08Netto.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_714F09Einheit:
                    strTmp = string.Empty;
                    strTmp = BMW_714F09Einheit.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_BGM_C002_1000:
                    strTmp = string.Empty;
                    strTmp = BMW_BGM_C002_1000.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_BGM_C106_1004_EANr:
                    strTmp = string.Empty;
                    strTmp = BMW_BGM_C106_1004_EANr.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_RFF_ACD_C506_1154:
                    strTmp = string.Empty;
                    strTmp = BMW_RFF_ACD_C506_1154.Execute(this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_MEA_C174_6314_ArticleNettoTO:
                    strTmp = string.Empty;
                    strTmp = BMW_MEA_C174_6314_ArticleNettoTO.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_BMW_MEA_C174_6314_KGorStkBruttoTO:
                    strTmp = string.Empty;
                    strTmp = BMW_MEA_C174_6314_KGorStkBruttoTO.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_4501_4501_MessageType:
                    strTmp = string.Empty;
                    strTmp = EdifactINVRPT_INV_4501_4501_MessageType.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_7491_7491_TypeMovement:
                    strTmp = string.Empty;
                    strTmp = EdifactINVRPT_INV_7491_7491_TypeMovement.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_4499_4499_MovementReason:
                    strTmp = string.Empty;
                    strTmp = EdifactINVRPT_INV_4499_4499_MovementReason.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_4503_4503_BalanceMethod:
                    strTmp = string.Empty;
                    strTmp = EdifactINVRPT_INV_4503_4503_BalanceMethod.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_BY_Buyer:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_BY_Buyer.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_CN_Consignee:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_CN_Consignee.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_DP_DeliveryPart:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_DP_DeliveryPart.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_GM_InventoryController:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_GM_InventoryController.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_ST_Recipient:
                case clsEdiVDAValueAlias.const_cFunction_EdiAdrWorkspaceAssign_NAD_C082_3039_ST_Recipient:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_ST_RecipienNo.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SU_Supplier:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_SU_SupplierNo.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SE_Seller:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_SE_Seller.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_UNB_S002_0004_Sender:
                case clsEdiVDAValueAlias.const_cFunction_EdiAdrWorkspaceAssign_UNB_S002_0004_Sender:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_UNB_S002_0004_Sender.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_UNB_S003_0010_Receiver:
                case clsEdiVDAValueAlias.const_cFunction_EdiAdrWorkspaceAssign_UNB_S003_0010_Receiver:
                    strTmp = string.Empty;
                    strTmp = EdiClientWorkspaceValue_UNB_S003_0010_Receiver.Execute(ASN, asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_Hydro_712F03SLB:
                    strTmp = string.Empty;
                    strTmp = Hydro_712F03SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_MUBEA_712F03SLB:
                    strTmp = string.Empty;
                    strTmp = MUBEA_712F03SLB.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_MENDRITZKI_Produktionsnummer:
                    strTmp = string.Empty;
                    strTmp = MENDRITZKI_Produktionsnummer.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_Novelis_F13F03_LfsNo:
                    strTmp = string.Empty;
                    strTmp = Novelis_F13F03_LfsNo.Execute(asnTyp, this.Lager);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                case clsEdiVDAValueAlias.const_cFunction_VW_GIN_ML_C208:
                    strTmp = string.Empty;
                    strTmp = VW_GIN_ML_C208.Execute(this.Lager.Ausgang.AdrAuftraggeber, this.SIDNew);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;

                case clsEdiVDAValueAlias.const_cFunction_VW_RFF_ON_C506:
                    strTmp = string.Empty;
                    strTmp = VW_RFF_ON_C506.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;


                //******************************************************************************************* allgemeine Function
                case clsEdiVDAValueAlias.const_ArtFunc_ProduktionsnummerFillTo9With0:
                    string strFillTo9With0 = string.Empty; // FillTo9With0(this.Lager.Artikel.Produktionsnummer);
                    strFillTo9With0 = ProduktionsnummerFillTo9With0.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strFillTo9With0, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_Anzahlx1000:
                    strTmp = string.Empty;
                    strTmp = (this.Lager.Artikel.Anzahl * 1000).ToString();
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_LVSNr8Stellig:
                    strTmp = string.Empty;
                    strTmp = ediHelper_FormatString.FillValueToLength(true, "0", this.Lager.Artikel.LVS_ID.ToString(), 8, true);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_PACBolzenQuantity:
                    strTmp = string.Empty;
                    strTmp = PACBolzenQuantity.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_PACNumber:
                    strTmp = string.Empty;
                    strTmp = PACNumber.Execute(this.Lager.Artikel);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_ddMMyy:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Format_ddMMyy);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_ddMMyyyy:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Format_ddMMyyyy);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_yyyyMMdd:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Format_yyyyMMdd);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_yyyyMMddOrBlank:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyyyMMddOrBlank);
                    strFeldSub = FillValueWithstringToLenth(tmpCV.Fill0, StringFillValue, strTmp, myClFeld.Length, bFillLeft);
                    break;
                case clsEdiVDAValueAlias.const_ArtFunc_GlowDateToEdi_yyMMdd:
                    strTmp = string.Empty;
                    strTmp = Format_GlowDateToEdi.Execute(this.Lager.Artikel, Format_GlowDateToEdi.const_Format_yyMMdd);
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
        //private string WerksnummerOhneBlank(clsArtikel myArt)
        //{
        //    string strReturn = myArt.Werksnummer.Replace(" ", "");
        //    return strReturn;
        //}
        ///<summary>clsVDACreate / WerksnummerMitBlank</summary>
        ///<remarks>Werknummer inc. führendem Leerzeichen</remarks>
        //private string WerksnummerMitBlank(clsArtikel myArt)
        //{
        //    string strReturn = " " + myArt.Werksnummer;
        //    return strReturn;
        //}
        ///<summary>clsVDACreate / WerksnummerMitBlank</summary>
        ///<remarks>Werksnummer soll folgendes Format aufweisen:
        ///  Die "supertolle-Syntax" von VW (Achtung, Leerzeichen vorn):
        ///  " 123 123 123 12"
        ///  " 123 123 123 123"
        ///  " 123 123 123 PLA" -> wird NICHT berücksichtigt!
        ///  " 123 123 123 A  PLA"
        ///  " 123 123 123    PLA" 
        /// </remarks>
        //private string WerksnummerVWFormat(clsArtikel myArt)
        //{
        //    string strTmp = Regex.Replace(myArt.Werksnummer, " ", "");
        //    Int32 iLen = strTmp.Length;
        //    string str1To9 = string.Empty;
        //    string strRest = string.Empty;
        //    string strReturn = string.Empty;

        //    if (strTmp.Length > 8)
        //    {
        //        str1To9 = strTmp.Substring(0, 9);
        //        strRest = strTmp.Substring(9, (strTmp.Length - 9));

        //        str1To9 = str1To9.Insert(6, " ");
        //        str1To9 = str1To9.Insert(3, " ");
        //        strRest.Trim();

        //        if (strRest.Equals("PLA"))
        //        {
        //            strRest = "    " + strRest;
        //        }
        //        else if (strRest.Equals("APLA"))
        //        {
        //            strRest = " A  PLA";
        //        }
        //        strReturn = " " + str1To9 + strRest;
        //    }
        //    else
        //    {
        //        strReturn = " " + myArt.Werksnummer;
        //    }

        //    return strReturn;
        //}
        ///<summary>clsVDACreate / GetBrutto</summary>
        ///<remarks></remarks>
        //private string GetBrutto(clsArtikel myArt)
        //{
        //    string strReturn = string.Empty;
        //    decimal decTmp = 0;
        //    decTmp = this.Lager.Artikel.Brutto * 1000;
        //    strReturn = Functions.FormatDecimalNoDiggits(decTmp);
        //    return strReturn;
        //}
        ///<summary>clsVDACreate / GetBrutto</summary>
        ///<remarks></remarks>
        //private string GetNetto(clsArtikel myArt)
        //{
        //    string strReturn = string.Empty;
        //    decimal decTmp = 0;
        //    decTmp = this.Lager.Artikel.Netto * 1000;
        //    strReturn = Functions.FormatDecimalNoDiggits(decTmp);
        //    return strReturn;
        //}
        /////<summary>clsVDACreate / GetBrutto</summary>
        /////<remarks></remarks>
        //private string GetMenge(clsArtikel myArt)
        //{
        //    string strReturn = string.Empty;
        //    decimal decTmp = 0;
        //    decTmp = this.Lager.Artikel.Anzahl * 1000;
        //    strReturn = Functions.FormatDecimalNoDiggits(decTmp);
        //    return strReturn;
        //}
        ///<summary>clsVDACreate / GetBruttoOrAnzahl</summary>
        ///<remarks></remarks>
        //private string GetBruttoOrAnzahl(clsArtikel myArt)
        //{
        //    string strReturn = string.Empty;
        //    myArt.GArt.ID = myArt.GArtID;
        //    myArt.GArt.Fill();
        //    if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
        //    {
        //        strReturn = GetBrutto(myArt);
        //    }
        //    else
        //    {
        //        strReturn = GetMenge(myArt);
        //    }
        //    return strReturn;
        //}
        ///<summary>clsVDACreate / GetNettoOrAnzahl</summary>
        ///<remarks></remarks>
        //private string GetNettoOrAnzahl(clsArtikel myArt)
        //{
        //    string strReturn = string.Empty;
        //    myArt.GArt.ID = myArt.GArtID;
        //    myArt.GArt.Fill();
        //    if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
        //    {
        //        strReturn = GetNetto(myArt);
        //    }
        //    else
        //    {
        //        strReturn = GetMenge(myArt);
        //    }
        //    return strReturn;
        //}
        ///<summary>clsVDACreate / GetEinheitKGorST</summary>
        ///<remarks></remarks>
        //private string GetEinheitKGorST(clsArtikel myArt)
        //{
        //    string strReturn = string.Empty;
        //    myArt.GArt.ID = myArt.GArtID;
        //    myArt.GArt.Fill();
        //    if (myArt.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
        //    {
        //        strReturn = "KG";
        //    }
        //    else
        //    {
        //        strReturn = "ST";
        //    }
        //    return strReturn;
        //}
        /******************************************************************************************
         *                          allgemeine Function
         * ***************************************************************************************/
        ///<summary>clsVDACreate / GetSenderID</summary>
        ///<remarks>SENDEr ist bei ausgehenden ASN immer der Client.</remarks>
        private string GetSenderID(ref clsLagerdaten myLager)
        {
            string strReturn = string.Empty;
            if (myLager.Eingang is clsLEingang)
            {
                //strReturn = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(this.Sys.Client.AdrID, myLager.Eingang.Auftraggeber, this.GL_User.User_ID, clsASNArt.const_Art_VDA4913, this.ASN.Job.ArbeitsbereichID);
                strReturn = clsADRVerweis.GetSenderVerweisBySenderAndReceiverAdr(myLager.Eingang.Auftraggeber, myLager.Eingang.Empfaenger, this.GL_User.User_ID, constValue_AsnArt.const_Art_VDA4913, this.ASN.Job.ArbeitsbereichID);

            }
            else
            {
                if (myLager.Ausgang is clsLAusgang)
                {
                    //strReturn = clsADRVerweis.GetLieferantenVerweisBySenderAndReceiverAdr(this.Sys.Client.AdrID, myLager.Ausgang.Auftraggeber, this.GL_User.User_ID, clsASNArt.const_Art_VDA4913, this.ASN.Job.ArbeitsbereichID);
                    strReturn = clsADRVerweis.GetSenderVerweisBySenderAndReceiverAdr(myLager.Ausgang.Auftraggeber, myLager.Ausgang.Empfaenger, this.GL_User.User_ID, constValue_AsnArt.const_Art_VDA4913, this.ASN.Job.ArbeitsbereichID);
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
