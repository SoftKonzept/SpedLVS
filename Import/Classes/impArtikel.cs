using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class impArtikel
    {
        internal clsSystemImport SysImport;
        internal ART art;
      
        internal EA EAlt;
        internal clsArtikel Artikel;
        internal clsLEingang Eingang;
        internal clsLAusgang Ausgang;
        //internal clsSchaeden Schaden;
        public List<ART> ListSource;
        public List<string> ListENR;
        public List<string> ListANR;
        public List<ART> ListART;
        public List<clsADR> ListADR;
        public List<clsGut> ListGut;
        public List<string> ListLog;
        public List<clsSchaeden> ListSchaeden;
        public List<clsArtikel> ListArtikel;
        public List<clsSchaeden> ListSchaden;
        internal clsADR adrAuftraggeber;
        internal clsADR adrEmpfägner;
        internal clsADR adrVersender;

        public delegate void SetProzessInfo();
        public event EventHandler SetProzessInfoEventHandler;

        public delegate void SetProzessInforamtion(List<string> myInfoList);
        public event EventHandler<List<string>> SetProzessInforamtionHandler;

        public impArtikel(clsSystemImport mySys)
        {
            this.SysImport = mySys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DoImportUB()
        {
            string strTmp = string.Empty;
            bool bReturn = false;
            ListLog = new List<string>();

            //--- Source Bereinigen 
            CleanUpSource();

            //--- Source Adressen ermitteln
            ListADR = helper_sql_clsADR.GetAdrList(this.SysImport);
            ListENR = helper_sql_ART.GetENRToImportUB(this.SysImport);

            List<int> ListLVSvorUB = new List<int>();
            ListLVSvorUB = helper_sql_ART.GetLNRVorUBList(this.SysImport);

            if (this.ListENR.Count > 0)
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("-------- ARTIKEL - UB");
                ListLog.Add(strTmp);

                ListGut = helper_sql_clsGut.GetGutList(this.SysImport);


                //Eingänge
                //--- Bestehende Daten ermitteln
                int iCount = 0;
                foreach (string item in this.ListENR)
                {
                    iCount++;
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString(" [" + iCount.ToString("000#") + "/" + this.ListENR.Count.ToString("000#") + "] - ");
                    EAlt = helper_sql_EA.GetEA(this.SysImport, item);

                    if ((EAlt.XNR != null) && (!EAlt.XNR.Equals(string.Empty)))
                    {
                        ListART = new List<ART>();
                        ListART = helper_sql_ART.GetARTByENRUB(this.SysImport, item, ListLVSvorUB);
                        //---Artikel vorhanden?
                        if (ListART.Count > 0)
                        {
                            if (!EAlt.KUNR.ToString().Equals(string.Empty))
                            {
                                adrAuftraggeber = ListADR.FirstOrDefault(x => x.ViewID.Trim() == EAlt.KUNR.Trim());
                                adrEmpfägner = ListADR.FirstOrDefault(x => x.ID == 195);
                                adrVersender = ListADR.FirstOrDefault(x => x.ViewID.Trim() == EAlt.VENR.Trim());

                                if ((adrAuftraggeber is clsADR) && (adrAuftraggeber.ID > 0))
                                {
                                    Eingang = new clsLEingang();
                                    Eingang.InitDefaultClsEingang(this.SysImport.GLUser, new clsSystem());
                                    Eingang.AbBereichID = this.SysImport.AbBereich.ID;
                                    Eingang.MandantenID = this.SysImport.AbBereich.MandantenID;
                                    //InsertEingang(this.Eingang);
                                    Eingang = helper_sql_clsLEingang.AddEingang(this.SysImport
                                                                                , EAlt
                                                                                , this.Eingang
                                                                                , adrAuftraggeber
                                                                                , adrEmpfägner
                                                                                , adrVersender
                                                                                );

                                    strTmp = string.Empty;
                                    strTmp = helper_LogStringCreater.CreateString("[add]   -> EINGANG [" + Eingang.LEingangTableID.ToString() + "-" + Eingang.LEingangID.ToString() + "] wurde erstellt");
                                    ListLog.Add(strTmp);

                                    foreach (ART itm in this.ListART)
                                    {
                                        this.art = new ART();
                                        this.art = itm;
                                        Artikel = new clsArtikel();
                                        Artikel = helper_sql_clsArtikel.AddArtikel(this);
                                        if (
                                                (Artikel is clsArtikel) &&
                                                (Artikel.ID > 0)
                                            )
                                        {
                                            strTmp = string.Empty;
                                            strTmp = helper_LogStringCreater.CreateString("[add]   -> EINGANG >>> Artikel [" + Artikel.LVS_ID.ToString() + "] wurde erstellt");
                                            ListLog.Add(strTmp);

                                            if (this.art.LNR.Equals(129421))
                                            {
                                                string str = string.Empty;
                                            }

                                            ////--- Queue -> iDoc erstellen
                                            //if (this.Eingang.Checked)
                                            //{
                                            //    helper_sql_clsQueue.Add(this);
                                            //}

                                            //--- Schäden hinzufügen
                                            if (!this.art.SCHADEN.ToString().Trim().Equals(string.Empty))
                                            {
                                                //Schäden laden und check ob der Schaden schon vorhanden ist
                                                clsSchaeden cSchaden = new clsSchaeden();
                                                cSchaden._GL_User = this.SysImport.GLUser;

                                                ListSchaden = helper_sql_clsSchaden.GetList(this);
                                                cSchaden = ListSchaden.FirstOrDefault(x => x.Bezeichnung.ToUpper() == this.art.SCHADEN.ToString().Trim().ToUpper());
                                                if (cSchaden is clsSchaeden)
                                                {
                                                    if (cSchaden.ID < 1)
                                                    {
                                                        cSchaden.Bezeichnung = this.art.SCHADEN.ToString().Trim();
                                                        cSchaden.Beschreibung = string.Empty;
                                                        cSchaden.aktiv = true;
                                                        cSchaden.Art = 0;
                                                        cSchaden.Code = string.Empty;
                                                        cSchaden.AutoSPL = false;

                                                        cSchaden.AddSchaden();
                                                        strTmp = string.Empty;
                                                        strTmp = helper_LogStringCreater.CreateString("[add]   -> EINGANG >>> Artikel  >>> Schaden [" + cSchaden.Bezeichnung + "] wurde erstellt");
                                                        ListLog.Add(strTmp);

                                                        ListSchaden = helper_sql_clsSchaden.GetList(this);
                                                    }
                                                    if (this.ListSchaden.Count > 0)
                                                    {
                                                        //helper_sql_clsSchaden.AddSchadenL(this);
                                                        helper_sql_clsSchaden.AddSchadenLByCls(cSchaden, this);
                                                    }
                                                }
                                            }
                                            //---SPL 
                                            if (
                                                (this.art.BKZ < 2) &&
                                                (this.art.EDAT <= DateTime.Now) &&
                                                (
                                                    (this.art.ADAT is null) ||
                                                    ((DateTime)this.art.ADAT <= DateTime.MinValue)
                                                ) &&
                                                (!this.art.SCHADEN.ToString().Trim().Equals(string.Empty)) &&
                                                (
                                                    (this.art.ARTCHECK != null) && (this.art.ARTCHECK.ToString().Trim().Equals("Y"))
                                                ) &&
                                                (
                                                    (this.art.SPL != null) && (this.art.SPL.Trim().Equals("Y"))
                                                )
                                              )
                                            {
                                                helper_sql_clsSPL.AddToSPL(this);
                                                strTmp = string.Empty;
                                                strTmp = helper_LogStringCreater.CreateString("[add SPL]-> EINGANG >>> Artikel/SPL [" + Artikel.LVS_ID.ToString() + "] wurde erstellt");
                                                ListLog.Add(strTmp);
                                            }
                                        }
                                    }
                                    helper_sql_clsLEingang.UpdateEingangImport(this.SysImport, this.Eingang);
                                }
                                else
                                {
                                    strTmp = string.Empty;
                                    strTmp = helper_LogStringCreater.CreateString("[Error]   -> Auftraggeber: " + EAlt.KUNR.Trim() + " nicht gefunden !!!");
                                    ListLog.Add(strTmp);
                                }
                            }
                            else
                            {
                                strTmp = string.Empty;
                                strTmp = helper_LogStringCreater.CreateString("[Error]   -> Kein Auftraggeber zugewiesen !!!");
                                ListLog.Add(strTmp);
                            }
                        }
                    }
                }
            }
            else
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("Art/Eingang Source Anzahl: 0 !!!");
                ListLog.Add(strTmp);
            }


            //Ausgänge
            //--- Bestehende Daten ermitteln
            ListANR = helper_sql_ART.GetANRToImportUB(this.SysImport);
            if (this.ListANR.Count > 0)
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("==> Ausgänge");

                int iCount = 0;
                foreach (string item in this.ListANR)
                {
                    iCount++;
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString(" [" + iCount.ToString("000#") + "/" + this.ListANR.Count.ToString("000#") + "] - ");

                    EAlt = helper_sql_EA.GetEA(this.SysImport, item);

                    if ((EAlt.XNR != null) && (!EAlt.XNR.Equals(string.Empty)))
                    {
                        ListArtikel = new List<clsArtikel>();
                        ListArtikel = helper_sql_clsArtikel.GetArtikelByANrUB(item, this, ListLVSvorUB);
                        //---Artikel vorhanden?
                        if (ListArtikel.Count > 0)
                        {
                            if (!EAlt.KUNR.ToString().Equals(string.Empty))
                            {
                                adrAuftraggeber = ListADR.FirstOrDefault(x => x.ViewID == EAlt.KUNR.Trim());
                                //adrEmpfägner = ListADR.FirstOrDefault(x => x.ViewID == "VW Sachsen");
                                adrEmpfägner = ListADR.FirstOrDefault(x => x.ViewID == EAlt.VENR);
                                adrVersender = ListADR.FirstOrDefault(x => x.ViewID == EAlt.KUNR.Trim());

                                if ((adrAuftraggeber is clsADR) && (adrAuftraggeber.ID > 0))
                                {

                                    Ausgang = new clsLAusgang();
                                    Ausgang.InitDefaultClsAusgang(this.SysImport.GLUser, new clsSystem());
                                    Ausgang.AbBereichID = this.SysImport.AbBereich.ID;
                                    Ausgang.MandantenID = this.SysImport.AbBereich.MandantenID;
                                    Ausgang = helper_sql_clsLAusgang.AddAusgang(this.SysImport
                                                                                , EAlt
                                                                                , this.Ausgang
                                                                                , adrAuftraggeber
                                                                                , adrEmpfägner
                                                                                , adrVersender
                                                                                );

                                    strTmp = string.Empty;
                                    strTmp = helper_LogStringCreater.CreateString("[add]   -> Ausgang [" + Ausgang.LAusgangTableID.ToString() + "-" + Ausgang.LAusgangID.ToString() + "] wurde erstellt");
                                    ListLog.Add(strTmp);

                                    foreach (clsArtikel itm in this.ListArtikel)
                                    {
                                        if (helper_sql_clsArtikel.UpdateArtikelAfterImportEA(this, itm, false))
                                        {
                                            strTmp = string.Empty;
                                            strTmp = helper_LogStringCreater.CreateString("[add]   -> Ausgang >>> Artikel [" + itm.LVS_ID.ToString() + "] wurde erstellt");
                                            ListLog.Add(strTmp);
                                        }
                                    }
                                    helper_sql_clsLAusgang.UpdateAusgangImport(this.SysImport, this.Ausgang);
                                }
                                else
                                {
                                    strTmp = string.Empty;
                                    strTmp = helper_LogStringCreater.CreateString("[Error]   -> Auftraggeber: " + EAlt.KUNR.Trim() + " nicht gefunden !!!");
                                    ListLog.Add(strTmp);
                                }
                            }
                            else
                            {
                                strTmp = string.Empty;
                                strTmp = helper_LogStringCreater.CreateString("[Error]   -> Kein Auftraggeber zugewiesen !!!");
                                ListLog.Add(strTmp);
                            }
                        }
                    }
                }
            }
            else
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("Art/Ausgang Source Anzahl: 0 !!!");
                ListLog.Add(strTmp);
            }


            return bReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DoImport()
        {
            string strTmp = string.Empty;
            bool bReturn = false;
            ListLog = new List<string>();

            //--- Source Bereinigen 
            CleanUpSource();

            //--- Source Adressen ermitteln
            ListADR = helper_sql_clsADR.GetAdrList(this.SysImport);
            ListENR = helper_sql_ART.GetENRToImport(this.SysImport);            

            if (this.ListENR.Count > 0)
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("-------- ARTIKEL");
                ListLog.Add(strTmp);

                ListGut = helper_sql_clsGut.GetGutList(this.SysImport);

                //Eingänge
                //--- Bestehende Daten ermitteln
                int iCount = 0;
                foreach (string item in this.ListENR)
                {
                    iCount++;
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString(" ["+iCount.ToString("000#")+"/"+this.ListENR.Count.ToString("000#") +"] - ");
                    EAlt = helper_sql_EA.GetEA(this.SysImport, item);

                    if ((EAlt.XNR != null) && (!EAlt.XNR.Equals(string.Empty)))
                    {
                        ListART = new List<ART>();
                        ListART = helper_sql_ART.GetARTByENR(this.SysImport, item);
                        //---Artikel vorhanden?
                        if (ListART.Count > 0)
                        {
                            if(!EAlt.KUNR.ToString().Equals(string.Empty))
                            { 
                                adrAuftraggeber = ListADR.FirstOrDefault(x => x.ViewID.Trim() == EAlt.KUNR.Trim());
                                //adrEmpfägner = ListADR.FirstOrDefault(x => x.ViewID == "VW Sachsen");
                                adrEmpfägner = ListADR.FirstOrDefault(x => x.ID == 195);
                                adrVersender = ListADR.FirstOrDefault(x => x.ViewID.Trim() == EAlt.VENR.Trim());

                                if ((adrAuftraggeber is clsADR) && (adrAuftraggeber.ID > 0))
                                {
                                    Eingang = new clsLEingang();
                                    Eingang.InitDefaultClsEingang(this.SysImport.GLUser, new clsSystem());
                                    Eingang.AbBereichID = this.SysImport.AbBereich.ID;
                                    Eingang.MandantenID = this.SysImport.AbBereich.MandantenID;
                                    //InsertEingang(this.Eingang);
                                    Eingang = helper_sql_clsLEingang.AddEingang(this.SysImport
                                                                                , EAlt
                                                                                , this.Eingang
                                                                                , adrAuftraggeber
                                                                                , adrEmpfägner
                                                                                , adrVersender
                                                                                );

                                    strTmp = string.Empty;
                                    strTmp = helper_LogStringCreater.CreateString("[add]   -> EINGANG [" + Eingang.LEingangTableID.ToString() + "-" + Eingang.LEingangID.ToString() + "] wurde erstellt");
                                    ListLog.Add(strTmp);

                                    foreach (ART itm in this.ListART)
                                    { 
                                        // für BMW reicht einmal laden
                                        //ListGut = helper_sql_clsGut.GetGutList(this.SysImport);

                                        this.art = new ART();
                                        this.art = itm;
                                        Artikel = new clsArtikel();
                                        Artikel = helper_sql_clsArtikel.AddArtikel(this);
                                        if (
                                                (Artikel is clsArtikel) &&
                                                (Artikel.ID > 0)
                                            )
                                        {
                                            strTmp = string.Empty;
                                            strTmp = helper_LogStringCreater.CreateString("[add]   -> EINGANG >>> Artikel [" + Artikel.LVS_ID.ToString() + "] wurde erstellt");
                                            ListLog.Add(strTmp);

                                            if (this.art.LNR.Equals(129421))
                                            {
                                                string str = string.Empty;
                                            }


                                            //--- Queue -> iDoc erstellen
                                            if (this.Eingang.Checked)
                                            {
                                                helper_sql_clsQueue.AddEM(this);
                                            }

                                            //--- Schäden hinzufügen
                                            if (!this.art.SCHADEN.ToString().Trim().Equals(string.Empty))
                                            {
                                                //Schäden laden und check ob der Schaden schon vorhanden ist
                                                clsSchaeden cSchaden = new clsSchaeden();
                                                cSchaden._GL_User = this.SysImport.GLUser;
                                                
                                                ListSchaden = helper_sql_clsSchaden.GetList(this);
                                                cSchaden = ListSchaden.FirstOrDefault(x=>x.Bezeichnung.ToUpper() == this.art.SCHADEN.ToString().Trim().ToUpper());
                                                if (cSchaden is clsSchaeden)
                                                {
                                                    if (cSchaden.ID < 1)
                                                    {
                                                        cSchaden.Bezeichnung = this.art.SCHADEN.ToString().Trim();
                                                        cSchaden.Beschreibung = string.Empty;
                                                        cSchaden.aktiv = true;
                                                        cSchaden.Art = 0;
                                                        cSchaden.Code = string.Empty;
                                                        cSchaden.AutoSPL = false;

                                                        cSchaden.AddSchaden();
                                                        strTmp = string.Empty;
                                                        strTmp = helper_LogStringCreater.CreateString("[add]   -> EINGANG >>> Artikel  >>> Schaden [" + cSchaden.Bezeichnung + "] wurde erstellt");
                                                        ListLog.Add(strTmp);

                                                        ListSchaden = helper_sql_clsSchaden.GetList(this);
                                                    }
                                                    if (this.ListSchaden.Count > 0)
                                                    {
                                                        //helper_sql_clsSchaden.AddSchadenL(this);
                                                        helper_sql_clsSchaden.AddSchadenLByCls(cSchaden,this);
                                                    }
                                                }

                                            }

                                            //---SPL 
                                            if (
                                                (this.art.BKZ<2) &&
                                                (this.art.EDAT <= DateTime.Now) &&
                                                (
                                                    (this.art.ADAT is null) ||
                                                    ((DateTime)this.art.ADAT <=DateTime.MinValue)
                                                ) &&
                                                (!this.art.SCHADEN.ToString().Trim().Equals(string.Empty)) &&
                                                (
                                                    (this.art.ARTCHECK != null) && (this.art.ARTCHECK.ToString().Trim().Equals("Y"))
                                                ) &&
                                                (
                                                    (this.art.SPL != null) && (this.art.SPL.Trim().Equals("Y")) 
                                                )
                                              )
                                            {
                                                helper_sql_clsSPL.AddToSPL(this);
                                                strTmp = string.Empty;
                                                strTmp = helper_LogStringCreater.CreateString("[add SPL]-> EINGANG >>> Artikel/SPL [" + Artikel.LVS_ID.ToString() + "] wurde erstellt");
                                                ListLog.Add(strTmp);
                                            }
                                        }
                                    }
                                    helper_sql_clsLEingang.UpdateEingangImport(this.SysImport, this.Eingang);
                                }
                                else
                                {
                                    strTmp = string.Empty;
                                    strTmp = helper_LogStringCreater.CreateString("[Error]   -> Auftraggeber: "+ EAlt.KUNR.Trim() + " nicht gefunden !!!");
                                    ListLog.Add(strTmp);
                                }
                            }
                            else
                            {
                                strTmp = string.Empty;
                                strTmp = helper_LogStringCreater.CreateString("[Error]   -> Kein Auftraggeber zugewiesen !!!");
                                ListLog.Add(strTmp);
                            }
                        }
                    }
                }
            }
            else
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("Art/Eingang Source Anzahl: 0 !!!");
                ListLog.Add(strTmp);
            }


            //Ausgänge
            //--- Bestehende Daten ermitteln
            ListANR = helper_sql_ART.GetANRToImport(this.SysImport);
            if (this.ListANR.Count > 0)
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("==> Ausgänge");

                int iCount = 0;
                foreach (string item in this.ListANR)
                {
                    iCount++;
                    strTmp = string.Empty;
                    strTmp = helper_LogStringCreater.CreateString(" [" + iCount.ToString("000#") + "/" + this.ListANR.Count.ToString("000#") + "] - ");

                    EAlt = helper_sql_EA.GetEA(this.SysImport, item);

                    if ((EAlt.XNR != null) && (!EAlt.XNR.Equals(string.Empty)))
                    {
                        ListArtikel = new List<clsArtikel>();
                        ListArtikel = helper_sql_clsArtikel.GetArtikelByANr(item, this);
                        //---Artikel vorhanden?
                        if (ListArtikel.Count > 0)
                        {
                            if (!EAlt.KUNR.ToString().Equals(string.Empty))
                            {
                                adrAuftraggeber = ListADR.FirstOrDefault(x => x.ViewID == EAlt.KUNR.Trim());
                                //adrEmpfägner = ListADR.FirstOrDefault(x => x.ViewID == "VW Sachsen");
                                adrEmpfägner = ListADR.FirstOrDefault(x => x.ViewID == EAlt.VENR);
                                adrVersender = ListADR.FirstOrDefault(x => x.ViewID == EAlt.KUNR.Trim());

                                if ((adrAuftraggeber is clsADR) && (adrAuftraggeber.ID > 0))
                                {

                                    Ausgang = new clsLAusgang();
                                    Ausgang.InitDefaultClsAusgang(this.SysImport.GLUser, new clsSystem());
                                    Ausgang.AbBereichID = this.SysImport.AbBereich.ID;
                                    Ausgang.MandantenID = this.SysImport.AbBereich.MandantenID;
                                    Ausgang = helper_sql_clsLAusgang.AddAusgang(this.SysImport
                                                                                , EAlt
                                                                                , this.Ausgang
                                                                                , adrAuftraggeber
                                                                                , adrEmpfägner
                                                                                , adrVersender
                                                                                );

                                    strTmp = string.Empty;
                                    strTmp = helper_LogStringCreater.CreateString("[add]   -> Ausgang [" + Ausgang.LAusgangTableID.ToString() + "-" + Ausgang.LAusgangID.ToString() + "] wurde erstellt");
                                    ListLog.Add(strTmp);

                                    foreach (clsArtikel itm in this.ListArtikel)
                                    {
                                        if (helper_sql_clsArtikel.UpdateArtikelAfterImportEA(this, itm, false))
                                        {
                                            strTmp = string.Empty;
                                            strTmp = helper_LogStringCreater.CreateString("[add]   -> Ausgang >>> Artikel [" + Artikel.LVS_ID.ToString() + "] wurde erstellt");
                                            ListLog.Add(strTmp);
                                        }

                                        //--- Queue -> iDoc erstellen
                                        if (this.Ausgang.Checked)
                                        {
                                            helper_sql_clsQueue.AddAM(this);
                                        }
                                    }
                                    helper_sql_clsLAusgang.UpdateAusgangImport(this.SysImport, this.Ausgang);
                                }
                                else
                                {
                                    strTmp = string.Empty;
                                    strTmp = helper_LogStringCreater.CreateString("[Error]   -> Auftraggeber: " + EAlt.KUNR.Trim() + " nicht gefunden !!!");
                                    ListLog.Add(strTmp);
                                }
                            }
                            else
                            {
                                strTmp = string.Empty;
                                strTmp = helper_LogStringCreater.CreateString("[Error]   -> Kein Auftraggeber zugewiesen !!!");
                                ListLog.Add(strTmp);
                            }
                        }
                    }
                }
            }
            else
            {
                strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("Art/Ausgang Source Anzahl: 0 !!!");
                ListLog.Add(strTmp);
            }


            return bReturn;
        }
    
        /// <summary>
        ///             DELETE WHERE SUCHB = null
        /// </summary>
        private void CleanUpSource()
        {
            string strSql = "Delete EA WHERE Typ='E' AND XNR NOT IN (SELECT DISTINCT ENR FROM ART WHERE ENR IS NOT NULL AND ENR<>'');";
            clsSQLconImport.ExecuteSQLWithTRANSACTION(strSql, "CleanUpEA", this.SysImport.GLUser.User_ID);      
            
        }

        public void CreateMissingLM()
        {
            ListLog = new List<string>();
            FillListArtikel();
            if (this.ListArtikel.Count > 0)
            {
                string strTmp = string.Empty;
                strTmp = helper_LogStringCreater.CreateString("-------- ASN Communication / fehlendet Lagermeldungen");
                ListLog.Add(strTmp);
                foreach (clsArtikel art in this.ListArtikel)
                {
                    this.Artikel = new clsArtikel();
                    this.Artikel = art.Copy();

                    this.Eingang = new clsLEingang();
                    this.Eingang = art.Eingang.Copy();
                         

                    //--- Queue -> iDoc erstellen
                    if ((this.Eingang is clsLEingang)&& (this.Eingang.Checked))
                    {
                        helper_sql_clsQueue.AddEM(this);
                        strTmp = string.Empty;
                        strTmp = helper_LogStringCreater.CreateString("[add]   -> Artikel >>> EME / EML [" + this.Artikel.LVS_ID.ToString() + "] wurde erstellt");
                        ListLog.Add(strTmp);
                    }
                }
            }
            SetProzessInforamtionHandler(this, ListLog);
        
        }
        /// <summary>
        /// 
        /// </summary>
        private void FillListArtikel()
        {
            ListArtikel = new List<clsArtikel>();
            string strSql = "SELECT ID FROM Artikel where ID not in (Select Distinct ArtikelId FROM SZG_LVS.dbo.LagerMeldungen) Order by ID;";
            DataTable dt = clsSQLcon.ExecuteSQLWithTRANSACTIONGetDataTable(strSql, "Artikel", "Artikel", this.SysImport.GLUser.User_ID);
            foreach (DataRow row in dt.Rows)
            {
                decimal decTmp = 0;
                decimal.TryParse(row["ID"].ToString(), out decTmp);
                if (decTmp > 0)
                {
                    clsArtikel art = new clsArtikel();
                    art.InitClass(this.SysImport.GLUser, this.SysImport.GLSystem);
                    art.ID = decTmp;
                    art.GetArtikeldatenByTableID();
                    ListArtikel.Add(art);
                }
            }
        }

    }
}
