using Common.Enumerations;
using LVS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrPrintLager : UserControl
    {
        internal clsReportDocSetting RepDocSettings;
        public Globals._GL_USER GL_User;
        public bool _bEingang = true;
        internal clsLager LagerPrint;
        internal ctrEinlagerung _ctrEinlagerung;
        internal ctrAuslagerung _ctrAuslagerung;
        internal ctrMenu _ctrMenu;
        internal frmTmp _frmTmp;
        internal frmPrintRepViewer _frmPrintRepView;
        internal string _DokumentenArt;
        internal string _DocPath;
        public Int32 SearchButton = 0;
        BackgroundWorker worker;
        internal bool bCountfromGUI = false;
        internal List<clsReportDocSetting> ListReportDoc;
        internal Int32 cbPrintDocsIndex = -1;

        //bool bDirekt = false;

        ///<summary>ctrPrintLager / ctrPrintLager</summary>
        ///<remarks></remarks>
        public ctrPrintLager()
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted +=
                    new RunWorkerCompletedEventHandler(worker_CompleteWork);

            this.Height = 300;
            gbNeutralADR.Enabled = false;
        }
        ///<summary>ctrPrintLager / worker_CompleteWork</summary>
        ///<remarks></remarks>
        private void worker_CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this._ctrEinlagerung != null)
                this._ctrEinlagerung.InitDGV();
        }
        ///<summary>ctrPrintLager / worker_DoWork</summary>
        ///<remarks></remarks>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (Int32 i = 0; i <= this.clbPrintDocs.CheckedItems.Count - 1; i++)
            {
                for (Int32 j = 0; j <= this.ListReportDoc.Count - 1; j++)
                {
                    clsReportDocSetting tmpRep = (clsReportDocSetting)this.ListReportDoc[j];
                    if (this.clbPrintDocs.CheckedItems[i].ToString() == tmpRep.ctrDocView.ToString())
                    {
                        _DokumentenArt = string.Empty;
                        _DocPath = string.Empty;
                        _DokumentenArt = tmpRep.IniDocKeyName;
                        _DocPath = tmpRep.IniDocKeyValuePath;
                        StartPrint(true);
                    }
                }
            }
        }
        ///<summary>ctrPrintLager / ctrPrintLager_Load</summary>
        ///<remarks>Atkionen:
        ///         - Kalssen und Variablen initialisieren</remarks>
        private void ctrPrintLager_Load(object sender, EventArgs e)
        {
            InitCtr();
        }
        ///<summary>ctrPrintLager / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {
            //init
            LagerPrint = new clsLager();
            _DokumentenArt = string.Empty;
            SetLagerDatenToFrm();

            //Druck LEingang
            if (this._ctrEinlagerung != null)
            {
                //checkboxen ausblenden
                cbLAusgangsLieferschein.Visible = false;
                cbLAusgangDocument.Visible = false;
                cbLAusgangAllDocs.Visible = false;
                cbNeutraleLAusgangDocs.Visible = false;
                cbAusgangAnzeige.Visible = false;
                cbEingangsLieferschein.Visible = false;
                cbEinangDocKomplett.Visible = false;
                cbLEingangDokument.Visible = false;
                //checkboxlist fill 
                clbPrintDocs.Dock = DockStyle.Fill;


                if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                {
                    this._ctrMenu._frmMain.system.CustomizeDocPath(ref this._ctrMenu._frmMain.GL_System, this._ctrEinlagerung.Lager.Eingang.Auftraggeber);
                    this.ListReportDoc = this._ctrMenu._frmMain.system.Client.ListPrintCenterReportEinlagerung.ToList();
                }
                else
                {
                    this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this._ctrEinlagerung.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                    this.ListReportDoc = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang;
                }
                InitCblPrint();
            }
            //Druck Lagerausgang
            if (this._ctrAuslagerung != null)
            {
                //checkboxen ausblenden
                cbLAusgangsLieferschein.Visible = false;
                cbLAusgangDocument.Visible = false;
                cbLAusgangAllDocs.Visible = false;
                cbNeutraleLAusgangDocs.Visible = false;
                cbAusgangAnzeige.Visible = false;
                cbEingangsLieferschein.Visible = false;
                cbEinangDocKomplett.Visible = false;
                cbLEingangDokument.Visible = false;

                //checkboxlist fill 
                clbPrintDocs.Dock = DockStyle.Fill;

                if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                {
                    this._ctrMenu._frmMain.system.CustomizeDocPath(ref this._ctrMenu._frmMain.GL_System, this._ctrAuslagerung.Lager.Ausgang.Auftraggeber);
                    this.ListReportDoc = this._ctrMenu._frmMain.system.Client.ListPrintCenterReportAuslagerung.ToList();
                }
                else
                {
                    this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this._ctrAuslagerung.Lager.Ausgang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                    this.ListReportDoc = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocAusgang;
                }
                InitCblPrint();
            }
        }
        ///<summary>ctrPrintLager / InitCblPrint</summary>
        ///<remarks></remarks>
        private void InitCblPrint()
        {
            this.clbPrintDocs.Items.Clear();
            for (Int32 i = 0; i <= this.ListReportDoc.Count - 1; i++)
            {
                clsReportDocSetting repTmp = (clsReportDocSetting)this.ListReportDoc[i];

                if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                {
                    if (
                            (repTmp.IniDocKeyName.Equals(enumIniDocKey.SPLDoc.ToString())) ||
                            (repTmp.IniDocKeyName.Equals(enumIniDocKey.SPLLabel.ToString()))
                       )
                    {
                        //Check ob SPL-Artikel vorhanden
                        if (this._ctrEinlagerung.Lager.Eingang.ListArtInSPL.Count > 0)
                        {
                            this.clbPrintDocs.Items.Add(repTmp.ctrDocView, false);
                        }
                    }
                    else if (
                                (repTmp.IniDocKeyName.Equals(enumIniDocKey.SchadenLabel.ToString())) ||
                                (repTmp.IniDocKeyName.Equals(enumIniDocKey.SchadenDoc.ToString()))
                        )
                    {
                        //Check ob SPL Artikel vorhanden sind
                        if (this._ctrEinlagerung.Lager.Eingang.ListArtWithSchaden.Count > 0)
                        {
                            this.clbPrintDocs.Items.Add(repTmp.ctrDocView, false);
                        }
                    }
                    else
                    {
                        this.clbPrintDocs.Items.Add(repTmp.ctrDocView, false);
                    }
                }
                else
                {
                    if (
                        (repTmp.DocKey.Equals(enumIniDocKey.SPLDoc.ToString())) ||
                        (repTmp.DocKey.Equals(enumIniDocKey.SPLLabel.ToString()))
                       )
                    {
                        if (this._ctrEinlagerung is ctrEinlagerung)
                        {
                            //Check ob SPL-Artikel vorhanden
                            if (this._ctrEinlagerung.Lager.Eingang.ListArtInSPL.Count > 0)
                            {
                                if (this._ctrEinlagerung._bArtPrint)
                                {
                                    if (this._ctrEinlagerung.Lager.Eingang.ListArtInSPL.Contains(this._ctrEinlagerung.Lager.Artikel.ID))
                                    {
                                        if (!this._ctrMenu._frmMain.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                                        {
                                            this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                                        }
                                        else
                                        {
                                            if (repTmp.DocKey.Equals(enumIniDocKey.SPLLabel.ToString()))
                                            {
                                                this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                                    if (!this._ctrMenu._frmMain.system.Client.MatchCode.Equals(clsClient.const_ClientMatchcode_SZG + "_"))
                                    {
                                        this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                                    }
                                    else
                                    {
                                        if (repTmp.DocKey.Equals(enumIniDocKey.SPLLabel.ToString()))
                                        {
                                            this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (
                                (repTmp.DocKey.Equals(enumIniDocKey.SchadenLabel.ToString())) ||
                                (repTmp.DocKey.Equals(enumIniDocKey.SchadenDoc.ToString()))
                            )
                    {
                        if (this._ctrEinlagerung is ctrEinlagerung)
                        {
                            //Check ob SPL Artikel vorhanden sind
                            if (this._ctrEinlagerung.Lager.Eingang.ListArtWithSchaden.Count > 0)
                            {
                                if (this._ctrEinlagerung.Lager.Eingang.ListArtWithSchaden.Contains(this._ctrEinlagerung.Lager.Artikel.ID))
                                {
                                    this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                                }
                            }
                        }
                    }
                    else if (
                               //(repTmp.DocKey.Equals(Globals.enumIniDocKey.LabelAll.ToString())) ||
                               (repTmp.DocKey.Equals(enumIniDocKey.LabelOne.ToString())) ||
                               (repTmp.DocKey.Equals(enumIniDocKey.LabelOneNeutral.ToString()))
                            )
                    {
                        if (
                            (this._ctrEinlagerung is ctrEinlagerung) &&
                            (this._ctrEinlagerung._bArtPrint)
                          )
                        {
                            this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                        }
                    }
                    else if (
                               (repTmp.DocKey.Equals(enumIniDocKey.LabelAll.ToString()))
                            )
                    {
                        if (
                            (this._ctrEinlagerung is ctrEinlagerung) &&
                            (!this._ctrEinlagerung._bArtPrint)
                          )
                        {
                            this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                        }
                    }
                    else
                    {
                        this.clbPrintDocs.Items.Add(repTmp.ViewID, false);
                    }
                }
            }
            //Check Customize clbPrintDocs
            if (this._ctrEinlagerung is ctrEinlagerung)
            {
                this._ctrMenu._frmMain.system.Client.ctrPrintLager_CustomizeSetPrintDoc(ref this.clbPrintDocs, this._ctrEinlagerung.Lager);
            }
            if (this._ctrAuslagerung is ctrAuslagerung)
            {
                this._ctrMenu._frmMain.system.Client.ctrPrintLager_CustomizeSetPrintDoc(ref this.clbPrintDocs, this._ctrAuslagerung.Lager);
            }
        }
        ///<summary>ctrPrintLager / tsbtnClose_Click</summary>
        ///<remarks>Fenster schliessen</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this._ctrMenu.CloseCtrPrintLagerFrmTmp();
        }
        ///<summary>ctrPrintLager / tsbtnClose_Click</summary>
        ///<remarks>Fenster schliessen</remarks>
        public void SetLagerDatenToFrm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { SetLagerDatenToFrm(); }));
            }
            else
            {
                LagerPrint = new clsLager();
                if (this._ctrEinlagerung != null)
                {
                    //Dokumtente Einlagerung
                    LagerPrint = this._ctrEinlagerung.Lager;

                    //Auftraggeber
                    SearchButton = 1;
                    SetADRToFrm(LagerPrint.Eingang.Auftraggeber);
                    //Versender
                    SearchButton = 2;
                    SetADRToFrm(LagerPrint.Eingang.Versender);
                    //Empfänger
                    SearchButton = 3;
                    SetADRToFrm(LagerPrint.Eingang.Empfaenger);
                    //Entladestelle
                    btnEntladestelle.Enabled = false;
                    tbSearchEnt.Enabled = false;
                    tbEntladestelle.Enabled = false;
                    //Spedition
                    SearchButton = 7;
                    SetADRToFrm(LagerPrint.Eingang.SpedID);
                    //KFZ
                    tbKFZ.Text = LagerPrint.Eingang.KFZ;
                    //Dokumentenname
                    tbDocName.Text = "LAGEREINGANG";
                }
                else
                {
                    //Dokumente Auslagerung
                    LagerPrint = this._ctrAuslagerung.Lager;
                    //Auftraggeber
                    SearchButton = 1;
                    SetADRToFrm(LagerPrint.Ausgang.Auftraggeber);
                    //Versender
                    SearchButton = 2;
                    SetADRToFrm(LagerPrint.Ausgang.Versender);
                    //Empfänger
                    SearchButton = 3;
                    SetADRToFrm(LagerPrint.Ausgang.Empfaenger);
                    //Entladestelle
                    SearchButton = 4;
                    SetADRToFrm(LagerPrint.Ausgang.Entladestelle);
                    //Spedition
                    SearchButton = 7;
                    SetADRToFrm(LagerPrint.Ausgang.SpedID);
                    //Neutraler Empfänger
                    SearchButton = 5;
                    SetADRToFrm(LagerPrint.Ausgang.NeutralerEmpfaenger);
                    //neutraler Auftraggeber
                    SearchButton = 9;
                    SetADRToFrm(LagerPrint.Ausgang.NeutralerAuftraggeber);

                    //KFZ
                    tbKFZ.Text = LagerPrint.Ausgang.KFZ;
                    //Dokumentenname
                    tbDocName.Text = "LIEFERSCHEIN";
                }
            }
        }
        ///<summary>ctrPrintLager / SetADRToFrm</summary>
        ///<remarks>Ermittelt anhander der übergebenen Adresse ID die Adresse und setzt diese in die From.</remarks>
        ///<param name="ADR_ID">ADR_ID</param>
        public void SetADRToFrm(decimal myDecADR_ID)
        {
            string strE = string.Empty;
            string strMC = string.Empty;
            DataSet ds = clsADR.ReadADRbyID(myDecADR_ID);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strMC = ds.Tables[0].Rows[i]["ViewID"].ToString();
                strE = ds.Tables[0].Rows[i]["ViewID"].ToString() + " - ";
                strE = strE + ds.Tables[0].Rows[i]["KD_ID"].ToString() + " - ";
                strE = strE + ds.Tables[0].Rows[i]["Name1"].ToString() + " - ";
                strE = strE + ds.Tables[0].Rows[i]["PLZ"].ToString() + " - ";
                strE = strE + ds.Tables[0].Rows[i]["Ort"].ToString();

                //SearchButton
                // 1 = KD /Auftraggeber
                // 2 = Versender
                // 3 = Empfänger
                // 4 = neutrale Versandadresse - Entladeadresse - Auftraggeber
                // 5 = neutrale Empfangsadresse
                // 6 = Mandanten
                // 7 = Spedition
                // 8 = Kunden
                // 9 = neutralger Auftraggeber
                switch (SearchButton)
                {
                    case 1:
                        //_ADR_ID_A = myDecADR_ID;
                        tbSearchA.Text = strMC;
                        tbAuftraggeber.Text = strE;
                        break;

                    case 2:
                        //_ADR_ID_V = myDecADR_ID;
                        tbSearchV.Text = strMC;
                        tbVersender.Text = strE;
                        break;

                    case 3:
                        //_ADR_ID_E = myDecADR_ID;
                        tbSearchE.Text = strMC;
                        tbEmpfaenger.Text = strE;
                        break;

                    case 4:
                        //_ADREntladestelle = ADR_ID;
                        tbSearchEnt.Text = strMC;
                        tbEntladestelle.Text = strE;
                        break;

                    case 5:
                        LagerPrint.Ausgang.NeutralerEmpfaenger = myDecADR_ID;
                        tbSearchNeutrEmpfaenger.Text = strMC;
                        tbNeutralerEmpfaenger.Text = strE;
                        break;
                    case 7:
                        //_ADR_ID_Sped = myDecADR_ID;
                        tbSearchSped.Text = strMC;
                        tbSpedition.Text = strE;
                        break;

                    case 9:
                        LagerPrint.Ausgang.NeutralerAuftraggeber = myDecADR_ID;
                        tbSearchNeutrAuftraggeber.Text = strMC;
                        tbNeutralerAuftraggeber.Text = strE;
                        break;
                }
            }
        }
        ///<summary>ctrPrintLager / tsbtnPrint_Click</summary>
        ///<remarks>Druckvorschau erstellen. Daraus kann dann aber auch anschließend gedruckt werden.</remarks>
        ///<param name="ADR_ID">ADR_ID</param>
        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            bCountfromGUI = true;
            DoPrint(false);
        }
        ///<summary>ctrPrintLager / tsbtnDirectPrint_Click</summary>
        ///<remarks>Drucken ohne Druckvorschau.</remarks>
        private void tsbtnDirectPrint_Click(object sender, EventArgs e)
        {
            bCountfromGUI = true;
            DoPrint(true);
        }
        ///<summary>ctrPrintLager / DoPrint</summary>
        ///<remarks></remarks>
        public void DoPrint(bool bDirectPrint)
        {
            for (Int32 i = 0; i <= this.clbPrintDocs.CheckedItems.Count - 1; i++)
            {
                for (Int32 j = 0; j <= this.ListReportDoc.Count - 1; j++)
                {
                    clsReportDocSetting tmpRep = (clsReportDocSetting)this.ListReportDoc[j];
                    _DokumentenArt = string.Empty;
                    _DocPath = string.Empty;
                    this.RepDocSettings = tmpRep;

                    if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                    {
                        _DokumentenArt = tmpRep.IniDocKeyName;
                        _DocPath = tmpRep.IniDocKeyValuePath;

                        if (this.clbPrintDocs.CheckedItems[i].ToString() == tmpRep.ctrDocView.ToString())
                        {
                            if (
                                (_DokumentenArt.Equals(enumIniDocKey.SPLDoc.ToString())) ||
                                (_DokumentenArt.Equals(enumIniDocKey.SPLLabel.ToString())) ||
                                (_DokumentenArt.Equals(enumIniDocKey.SchadenLabel.ToString()))
                                )
                            {
                                if (this._ctrEinlagerung is ctrEinlagerung)
                                {
                                    if (this._ctrEinlagerung._bArtPrint)
                                    {
                                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                                    }
                                    else
                                    {
                                        for (Int32 x = 0; x <= this._ctrEinlagerung.Lager.Eingang.ListArtWithSchaden.Count - 1; x++)
                                        {
                                            decimal decTmp = 0;
                                            Decimal.TryParse(this._ctrEinlagerung.Lager.Eingang.ListArtWithSchaden[x].ToString(), out decTmp);
                                            if (decTmp > 0)
                                            {
                                                this._ctrEinlagerung.Lager.Artikel.ID = decTmp;
                                                this._ctrEinlagerung.Lager.Artikel.GetArtikeldatenByTableID();
                                                this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //this._ctrMenu.OpenFrmReporView(this, false, bCountfromGUI);
                                this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                            }
                        }
                    }
                    else
                    {
                        _DokumentenArt = tmpRep.DocKey;
                        _DocPath = tmpRep.DocFileNameAndPath;

                        if (this.clbPrintDocs.CheckedItems[i].ToString().Equals(tmpRep.ViewID.ToString()))
                        {
                            if (
                                (_DokumentenArt.Equals(enumIniDocKey.SchadenLabel.ToString())) ||
                                (_DokumentenArt.Equals(enumIniDocKey.SchadenDoc.ToString()))
                                )
                            {
                                if (this._ctrEinlagerung is ctrEinlagerung)
                                {
                                    if (this._ctrEinlagerung._bArtPrint)
                                    {
                                        this._ctrMenu.OpenFrmReporView(this, false, bCountfromGUI);
                                    }
                                    else
                                    {
                                        for (Int32 x = 0; x <= this._ctrEinlagerung.Lager.Eingang.ListArtWithSchaden.Count - 1; x++)
                                        {
                                            decimal decTmp = 0;
                                            Decimal.TryParse(this._ctrEinlagerung.Lager.Eingang.ListArtWithSchaden[x].ToString(), out decTmp);
                                            if (decTmp > 0)
                                            {
                                                this._ctrEinlagerung.Lager.Artikel.ID = decTmp;
                                                this._ctrEinlagerung.Lager.Artikel.GetArtikeldatenByTableID();
                                                this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                                            }
                                        }
                                    }
                                }
                            }
                            else if (
                                    (_DokumentenArt.Equals(enumIniDocKey.SPLDoc.ToString())) ||
                                    (_DokumentenArt.Equals(enumIniDocKey.SPLLabel.ToString()))
                                )
                            {
                                if (this._ctrEinlagerung is ctrEinlagerung)
                                {
                                    if (this._ctrEinlagerung._bArtPrint)
                                    {
                                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                                    }
                                    else
                                    {
                                        for (Int32 x = 0; x <= this._ctrEinlagerung.Lager.Eingang.ListArtInSPL.Count - 1; x++)
                                        {
                                            decimal decTmp = 0;
                                            Decimal.TryParse(this._ctrEinlagerung.Lager.Eingang.ListArtInSPL[x].ToString(), out decTmp);
                                            if (decTmp > 0)
                                            {
                                                this._ctrEinlagerung.Lager.Artikel.ID = decTmp;
                                                this._ctrEinlagerung.Lager.Artikel.GetArtikeldatenByTableID();
                                                this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //this._ctrMenu.OpenFrmReporView(this, false, bCountfromGUI);
                                this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                            }
                        }
                    }
                }
            }
        }
        ///<summary>ctrPrintLager / StartPrint</summary>
        ///<remarks>Start des Druckvorgangs.</remarks>
        ///<param name="bDirectPrint">bDirectPrint</param>
        public void StartPrint(bool bDirectPrint)
        {
            if (_DokumentenArt != string.Empty)
            {
                //Lagereingang
                if (this._ctrEinlagerung != null)
                {
                    LagerPrint.Eingang.FillEingang();
                    LagerPrint.Artikel.GetArtikeldatenByTableID();

                    //Lagereingang Komplett, hier muss der ReportView einmal 
                    //für den Eingangslieferschein und einmal für das Label
                    //ausgedruckt werden
                    if (cbEinangDocKomplett.Checked)
                    {
                        //Eingangslieferschein
                        //_DokumentenArt = Globals.enumDokumentenart.LagerEingangDoc.ToString();
                        List<decimal> AuftraggeberMat = new List<decimal>();

                        bool bUseMatDoc = false;
                        clsSystem sys = new clsSystem(Application.StartupPath);
                        sys.GetAuftraggeberMat(ref this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_System.sys_MandantenID);
                        if (this._ctrMenu._frmMain.GL_System.AuftraggeberMat != string.Empty)
                        {

                            string[] AuftraggeberSplit = this._ctrMenu._frmMain.GL_System.AuftraggeberMat.Split(',');
                            foreach (string auftraggeber in AuftraggeberSplit)
                            {
                                if (this.LagerPrint.Eingang.Auftraggeber == clsADR.GetIDByMatchcode(auftraggeber))
                                {
                                    bUseMatDoc = true;
                                }
                            }
                        }

                        if (!bUseMatDoc)
                        {
                            this._DokumentenArt = enumDokumentenArt.LagerEingangDoc.ToString();
                        }
                        else
                        {
                            this._DokumentenArt = enumDokumentenArt.LagerEingangDocMat.ToString();
                        }
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                        _DokumentenArt = enumDokumentenArt.LagerEingangLfs.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                        _DokumentenArt = enumDokumentenArt.ArtikelLabel.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                    }
                    else
                    {
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                    }
                }
                //LagerAusgang
                if (this._ctrAuslagerung != null)
                {
                    //Alle Lagerausgangsdokumente (Lieferschein / Lagerausgang) erstelle
                    if (cbLAusgangAllDocs.Checked)
                    {
                        //Lagerausgang
                        if (this._ctrMenu._frmMain.system.Client.Modul.Print_LieferscheinOhneAbschluss)
                        { }
                        else
                        {
                            if (_ctrAuslagerung.Lager.Ausgang.Checked)
                            {
                                _DokumentenArt = enumDokumentenArt.LagerAusgangDoc.ToString();
                                this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                            }
                        }
                        //Lagerausgangslieferschein
                        List<decimal> LieferscheinMat = new List<decimal>();
                        bool bUseMatLfs = false;
                        clsSystem sys = new clsSystem(Application.StartupPath);
                        sys.GetLieferscheinMat(ref this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_System.sys_MandantenID);

                        if (this._ctrMenu._frmMain.GL_System.LieferscheinMat != string.Empty)
                        {
                            string[] AuftraggeberSplit = this._ctrMenu._frmMain.GL_System.AuftraggeberMat.Split(',');
                            foreach (string auftraggeber in AuftraggeberSplit)
                            {
                                if (this.LagerPrint.Eingang.Auftraggeber == clsADR.GetIDByMatchcode(auftraggeber))
                                {
                                    bUseMatLfs = true;
                                }
                            }
                        }

                        if (!bUseMatLfs)
                        {
                            this._DokumentenArt = enumDokumentenArt.LagerAusgangLfs.ToString();
                        }
                        else
                        {
                            this._DokumentenArt = enumDokumentenArt.LagerAusgangLfsMat.ToString();
                        }
                        //_DokumentenArt = Globals.enumDokumentenart.LagerAusgangLfs.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                        //Lagerausgangslieferschein
                        _DokumentenArt = enumDokumentenArt.LagerAusgangAnzeige.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                    }
                    else
                    {
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                    }
                }
            }
            else
            {
                clsMessages.Print_NoDocArtSelected();
            }
        }
        ///<summary>ctrPrintLager / StartDirectPrint</summary>
        ///<remarks></remarks>
        public void StartDirectPrint()
        {
            if (_DokumentenArt != string.Empty)
            {
                //Lagereingang
                if (this._ctrEinlagerung != null)
                {
                    LagerPrint.Eingang.FillEingang();
                    LagerPrint.Artikel.GetArtikeldatenByTableID();

                    //Eingangsliste
                    if (_DokumentenArt.Equals(enumDokumentenArt.Eingangsliste.ToString()))
                    {
                        _DocPath = this._ctrMenu._frmMain.GL_System.docPath_EingangList;
                        this._DokumentenArt = enumDokumentenArt.Eingangsliste.ToString();
                        this._ctrMenu.OpenFrmReporView(this, true, bCountfromGUI);
                    }
                    //SPL - Label
                    if (_DokumentenArt.Equals(enumDokumentenArt.SPLLabel.ToString()))
                    {
                        if (LagerPrint.Artikel.bSPL)
                        {
                            _DocPath = this._ctrMenu._frmMain.GL_System.docPath_SPLLabel;
                            this._DokumentenArt = enumDokumentenArt.SPLLabel.ToString();
                            this._ctrMenu.OpenFrmReporView(this, true, bCountfromGUI);
                        }
                    }
                    //LAbelAll
                    if (_DokumentenArt.Equals(enumDokumentenArt.LabelAll.ToString()))
                    {
                        _DocPath = this._ctrMenu._frmMain.GL_System.docPath_LabelAll;
                        this._DokumentenArt = enumDokumentenArt.LabelAll.ToString();
                        this._ctrMenu.OpenFrmReporView(this, true, bCountfromGUI);
                    }
                    //SPL Doc
                    //Lagereingang
                    if (_DokumentenArt.Equals(enumDokumentenArt.SPLDoc.ToString()))
                    {
                        if (LagerPrint.Artikel.bSPL)
                        {
                            _DocPath = this._ctrMenu._frmMain.GL_System.docPath_SPLDoc;
                            this._DokumentenArt = enumDokumentenArt.SPLDoc.ToString();
                            this._ctrMenu.OpenFrmReporView(this, true, bCountfromGUI);
                        }
                    }
                }

                if (this._ctrAuslagerung != null)
                {
                    LagerPrint.Ausgang.FillAusgang();
                    LagerPrint.Artikel.GetArtikeldatenByTableID();

                    //Ausgangsliste
                    if (_DokumentenArt.Equals(enumDokumentenArt.Ausgangsliste.ToString()))
                    {
                        _DocPath = this._ctrMenu._frmMain.GL_System.docPath_AusgangList;
                        this._DokumentenArt = enumDokumentenArt.Ausgangsliste.ToString();
                        this._ctrMenu.OpenFrmReporView(this, true, bCountfromGUI);
                    }
                }
            }
            else
            {
                clsMessages.Print_NoDocArtSelected();
            }
        }
        ///<summary>ctrPrintLager / StartPrint</summary>
        ///<remarks>Start des Druckvorgangs.</remarks>
        ///<param name="bDirectPrint">bDirectPrint</param>
        public void StartPrintSPLLable(bool bDirectPrint)
        {
            if (_DokumentenArt != string.Empty)
            {
                //Lagereingang
                if (this._ctrEinlagerung != null)
                {
                    LagerPrint.Eingang.FillEingang();
                    LagerPrint.Artikel.GetArtikeldatenByTableID();
                    if (LagerPrint.Artikel.bSPL)
                    {

                        _DocPath = this._ctrMenu._frmMain.GL_System.docPath_SPLLabel;
                        this._DokumentenArt = enumDokumentenArt.SPLLabel.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                    }
                }
            }

            else
            {
                clsMessages.Print_NoDocArtSelected();
            }
        }
        ///<summary>ctrPrintLager / StartPrint</summary>
        ///<remarks>Start des Druckvorgangs.</remarks>
        ///<param name="bDirectPrint">bDirectPrint</param>
        public void StartPrintArtikelLabel(bool bDirectPrint)
        {
            if (_DokumentenArt != string.Empty)
            {
                //Lagereingang
                if (this._ctrEinlagerung != null)
                {
                    LagerPrint.Eingang.FillEingang();
                    LagerPrint.Artikel.GetArtikeldatenByTableID();
                    _DocPath = this._ctrMenu._frmMain.GL_System.docPath_LabelAll;
                    this._DokumentenArt = enumDokumentenArt.LabelAll.ToString();
                    this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                }
            }
            else
            {
                clsMessages.Print_NoDocArtSelected();
            }
        }
        ///<summary>ctrPrintLager / StartPrint</summary>
        ///<remarks>Start des Druckvorgangs.</remarks>
        ///<param name="bDirectPrint">bDirectPrint</param>
        public void StartPrintSPLDoc(bool bDirectPrint)
        {
            if (_DokumentenArt != string.Empty)
            {
                //Lagereingang
                if (this._ctrEinlagerung != null)
                {
                    LagerPrint.Eingang.FillEingang();
                    LagerPrint.Artikel.GetArtikeldatenByTableID();
                    if (LagerPrint.Artikel.bSPL)
                    {
                        _DocPath = this._ctrMenu._frmMain.GL_System.docPath_SPLDoc;
                        this._DokumentenArt = enumDokumentenArt.SPLDoc.ToString();
                        this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                    }
                }
            }
            else
            {
                clsMessages.Print_NoDocArtSelected();
            }
        }
        ///<summary>ctrPrintLager / StartPrint</summary>
        ///<remarks>Start des Druckvorgangs.</remarks>
        ///<param name="bDirectPrint">bDirectPrint</param>
        public void StartPrintDoc(bool bDirectPrint)
        {
            if (_DokumentenArt != string.Empty)
            {
                //Lagereingang
                if (this._ctrEinlagerung != null)
                {
                    LagerPrint.Eingang.FillEingang();
                    LagerPrint.Artikel.GetArtikeldatenByTableID();
                    if (this._ctrMenu._frmMain.system.Client.Modul.Print_OldVersion)
                    {
                        this._ctrMenu._frmMain.system.CustomizeDocPath(ref this._ctrMenu._frmMain.GL_System, this._ctrEinlagerung.Lager.Eingang.Auftraggeber);
                        this.ListReportDoc = this._ctrMenu._frmMain.system.Client.ListPrintCenterReportEinlagerung.ToList();
                    }
                    else
                    {
                        this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, this._ctrEinlagerung.Lager.Eingang.Auftraggeber, this._ctrMenu._frmMain.system.AbBereich.ID);
                        this.ListReportDoc = this._ctrMenu._frmMain.system.ReportDocSetting.ListReportDocEingang;
                    }
                    this.RepDocSettings = (clsReportDocSetting)this.ListReportDoc.Find(x => x.DocKey.Equals(_DokumentenArt));
                    //_DocPath = this.RepDocSettings.DocPath;
                    _DocPath = this.RepDocSettings.DocFileNameAndPath;
                    this._ctrMenu.OpenFrmReporView(this, bDirectPrint, bCountfromGUI);
                }
            }
            else
            {
                clsMessages.Print_NoDocArtSelected();
            }
        }
        ///<summary>ctrPrintLager / cbArtikelLabel_CheckedChanged</summary>
        ///<remarks>Dokumentenart = Label. Alle anderen Dokumente müssen dann false gesetzt werden</remarks>
        private void cbArtikelLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (cbArtikelLabel.Checked)
            {
                _DokumentenArt = enumDokumentenArt.ArtikelLabel.ToString();
                tbDocName.Text = "Artikellabel";
                cbEinangDocKomplett.Checked = false;
                cbEingangsLieferschein.Checked = false;
                cbLEingangDokument.Checked = false;
            }
        }
        ///<summary>ctrPrintLager / cbArtikelLabel_CheckedChanged</summary>
        ///<remarks>Dokumentenart = Eingangslieferschein. Alle anderen Dokumente müssen dann false gesetzt werden</remarks>
        private void cbEingangsLieferschein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEingangsLieferschein.Checked)
            {
                _DokumentenArt = enumDokumentenArt.LagerEingangAnzeige.ToString();
                tbDocName.Text = "EINGANGSANZEIGE";
                cbEinangDocKomplett.Checked = false;
                cbLEingangDokument.Checked = false;
                cbArtikelLabel.Checked = false;
            }
        }
        ///<summary>ctrPrintLager / cbEinangDocKomplett_CheckedChanged</summary>
        ///<remarks>Dokumentenart = Eingang Doc Komplett. Alle anderen Dokumente müssen dann false gesetzt werden</remarks>
        private void cbEinangDocKomplett_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEinangDocKomplett.Checked)
            {
                _DokumentenArt = enumDokumentenArt.LagerEingangKomplett.ToString();
                tbDocName.Text = "EINGANGSLIEFERSCHEIN";
                cbArtikelLabel.Checked = false;
                cbEingangsLieferschein.Checked = false;
                cbLEingangDokument.Checked = false;
            }
        }
        ///<summary>ctrPrintLager / cbLAusgangsLieferschein</summary>
        ///<remarks>Dokumentenart = Lagerausgang - keine Wahlmölgichkeit, da nur das eine Dokument erstell werden kann</remarks>
        private void cbLAusgangsLieferschein_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLAusgangsLieferschein.Checked)
            {
                cbLAusgangAllDocs.Checked = false;
                cbLAusgangDocument.Checked = false;

                cbNeutraleLAusgangDocs.Checked = false;
                List<decimal> LieferscheinMat = new List<decimal>();
                bool bUseMatLfs = false;
                clsSystem sys = new clsSystem(Application.StartupPath);
                sys.GetLieferscheinMat(ref this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_System.sys_MandantenID);

                if (this._ctrMenu._frmMain.GL_System.LieferscheinMat != string.Empty)
                {

                    string[] AuftraggeberSplit = this._ctrMenu._frmMain.GL_System.LieferscheinMat.Split(',');
                    foreach (string auftraggeber in AuftraggeberSplit)
                    {
                        if (this.LagerPrint.Ausgang.Auftraggeber == clsADR.GetIDByMatchcode(auftraggeber))
                        {
                            bUseMatLfs = true;
                        }
                    }
                }

                if (!bUseMatLfs)
                {
                    this._DokumentenArt = enumDokumentenArt.LagerAusgangLfs.ToString();
                }
                else
                {
                    this._DokumentenArt = enumDokumentenArt.LagerAusgangLfsMat.ToString();
                }
                //_DokumentenArt = Globals.enumDokumentenart.LagerAusgangLfs.ToString();
                tbDocName.Text = "AUSGANGSLIEFERSCHEIN";
            }
        }
        ///<summary>ctrPrintLager / cbLAusgangsLieferschein</summary>
        ///<remarks>Dokumentenart = Lagerausgang - keine Wahlmölgichkeit, da nur das eine Dokument erstell werden kann</remarks>
        private void cbLAusgangDocument_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLAusgangDocument.Checked)
            {
                cbLAusgangAllDocs.Checked = false;
                cbLAusgangsLieferschein.Checked = false;
                cbNeutraleLAusgangDocs.Checked = false;
                cbAusgangAnzeige.Checked = false;
                _DokumentenArt = enumDokumentenArt.LagerAusgangDoc.ToString();
                tbDocName.Text = "LAGERAUSGANG";
            }
        }
        ///<summary>ctrPrintLager / cbAusgangAnzeige_CheckedChanged</summary>
        ///<remarks></remarks>  
        private void cbAusgangAnzeige_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAusgangAnzeige.Checked)
            {
                cbLAusgangDocument.Checked = false;
                cbLAusgangAllDocs.Checked = false;
                cbLAusgangsLieferschein.Checked = false;
                cbNeutraleLAusgangDocs.Checked = false;
                _DokumentenArt = enumDokumentenArt.LagerAusgangAnzeige.ToString();
                tbDocName.Text = "LAGERAUSGANG";
            }
        }
        ///<summary>ctrPrintLager / cbLEingangDokument_CheckedChanged</summary>
        ///<remarks></remarks>     
        private void cbLEingangDokument_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLEingangDokument.Checked)
            {
                // _DokumentenArt = Globals.enumDokumentenart.LagerEingangDoc.ToString();
                List<decimal> AuftraggeberMat = new List<decimal>();
                bool bUseMatDoc = false;
                clsSystem sys = new clsSystem(Application.StartupPath);

                sys.GetAuftraggeberMat(ref this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.GL_System.sys_MandantenID);
                if (this._ctrMenu._frmMain.GL_System.AuftraggeberMat != string.Empty)
                {

                    string[] AuftraggeberSplit = this._ctrMenu._frmMain.GL_System.AuftraggeberMat.Split(',');
                    foreach (string auftraggeber in AuftraggeberSplit)
                    {
                        if (this.LagerPrint.Eingang.Auftraggeber == clsADR.GetIDByMatchcode(auftraggeber))
                        {
                            bUseMatDoc = true;
                        }
                    }
                }

                if (!bUseMatDoc)
                {
                    this._DokumentenArt = enumDokumentenArt.LagerEingangDoc.ToString();
                }
                else
                {
                    this._DokumentenArt = enumDokumentenArt.LagerEingangDocMat.ToString();
                }
                tbDocName.Text = "LAGEREINGANG";
                cbEinangDocKomplett.Checked = false;
                cbEingangsLieferschein.Checked = false;
                cbArtikelLabel.Checked = false;
            }
        }
        ///<summary>ctrPrintLager / cbLAusgangsLieferschein</summary>
        ///<remarks>Dokumentenart = Lagerausgang - keine Wahlmölgichkeit, da nur das eine Dokument erstell werden kann</remarks>
        private void cbLAusgangAllDocs_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLAusgangAllDocs.Checked)
            {
                cbLAusgangDocument.Checked = false;
                cbLAusgangsLieferschein.Checked = false;
                cbNeutraleLAusgangDocs.Checked = false;
                cbAusgangAnzeige.Checked = false;
                _DokumentenArt = enumDokumentenArt.LagerAusgangKomplett.ToString();
                tbDocName.Text = "LAGERAUSGANG";
            }
        }
        ///<summary>ctrPrintLager / cbNeutraleLAusgangDocs_CheckedChanged</summary>
        ///<remarks>Dokumentenart = Lagerausgang - keine Wahlmölgichkeit, da nur das eine Dokument erstell werden kann</remarks>
        private void cbNeutraleLAusgangDocs_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNeutraleLAusgangDocs.Checked)
            {
                cbLAusgangDocument.Checked = false;
                cbLAusgangsLieferschein.Checked = false;
                cbLAusgangAllDocs.Checked = false;
                cbAusgangAnzeige.Checked = false;
                _DokumentenArt = enumDokumentenArt.LagerAusgangNeutralDoc.ToString();
                tbDocName.Text = "AUSGANGSLIEFERSCHEIN";

                //Neutrale Adresseeingabe aktivieren
                gbNeutralADR.Enabled = true;
            }
            else
            {
                gbNeutralADR.Enabled = false;
            }
        }
        ///<summary>ctrPrintLager / btnNeutrAuftraggeber_Click</summary>
        ///<remarks></remarks>
        private void btnNeutrAuftraggeber_Click(object sender, EventArgs e)
        {
            SearchButton = 9;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrPrintLager / btnNeutralerEmp_Click</summary>
        ///<remarks></remarks>
        private void btnNeutralerEmp_Click(object sender, EventArgs e)
        {
            SearchButton = 5;
            _ctrMenu.OpenADRSearch(this);
        }
        ///<summary>ctrPrintLager / TakeOverADRID</summary>
        ///<remarks>Übernahme und Speichern der gewählten Adresse</remarks>
        public void TakeOverADRID(decimal myAdrID)
        {
            SetADRToFrm(myAdrID);
            LagerPrint.Ausgang.UpdateLagerAusgang();
            SetLagerDatenToFrm();
        }
        ///<summary>ctrPrintLager / btnNeutrDelete_Click</summary>
        ///<remarks>löschen der neutralen Adressen</remarks>
        private void btnNeutrDelete_Click(object sender, EventArgs e)
        {
            LagerPrint.Ausgang.NeutralerAuftraggeber = 0;
            LagerPrint.Ausgang.NeutralerEmpfaenger = 0;
            LagerPrint.Ausgang.UpdateLagerAusgang();
            tbSearchNeutrAuftraggeber.Text = string.Empty;
            tbSearchNeutrEmpfaenger.Text = string.Empty;
            tbNeutralerAuftraggeber.Text = string.Empty;
            tbNeutralerEmpfaenger.Text = string.Empty;
            SetLagerDatenToFrm();
        }

        private void nudPrintCount_ValueChanged(object sender, EventArgs e)
        {

        }
        ///<summary>ctrPrintLager / btnNeutrDelete_Click</summary>
        ///<remarks>löschen der neutralen Adressen</remarks>
        private void clbPrintDocs_MouseHover(object sender, EventArgs e)
        {
            if (clsUser.CheckUserIsComTECAdmin(this.GL_User))
            {
                clbPrintDocsToolTip();
            }
        }
        ///<summary>ctrPrintLager / clbPrintDocsToolTip</summary>
        ///<remarks>Tool Tip erstellen</remarks>
        private void clbPrintDocsToolTip()
        {
            Point pos = clbPrintDocs.PointToClient(MousePosition);
            this.cbPrintDocsIndex = clbPrintDocs.IndexFromPoint(pos);

            if (
                (this.clbPrintDocs.Items.Count > 0) &&
                 (this.cbPrintDocsIndex > -1)
               )
            {
                pos = this.PointToClient(MousePosition);
                ToolTip clbToolTip = new ToolTip();
                clbToolTip.AutoPopDelay = 3000;
                string strToolTip = string.Empty;

                //cbToolTip.ToolTipTitle = "Admin-Info";
                string strTmpDocKey = this.clbPrintDocs.Items[this.cbPrintDocsIndex].ToString();
                try
                {
                    clsReportDocSetting tmpRep = (clsReportDocSetting)ListReportDoc.First(x => x.ViewID == strTmpDocKey);

                    if (tmpRep is clsReportDocSetting)
                    {
                        strToolTip = "DocKey: [" + tmpRep.DocKey.ToString() + "]" + Environment.NewLine +
                                     "Path:   [" + tmpRep.Path.ToString() + "]" + Environment.NewLine +
                                     "Report:   [" + tmpRep.ReportFileName.ToString() + "]" + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    string strError = ex.ToString();
                }
                clbToolTip.SetToolTip(clbPrintDocs, strToolTip);
            }
        }
        ///<summary>ctrPrintLager / clbPrintDocs_MouseMove</summary>
        ///<remarks>Tool Tip erstellen</remarks>
        private void clbPrintDocs_MouseMove(object sender, MouseEventArgs e)
        {
            if (clsUser.CheckUserIsComTECAdmin(this.GL_User))
            {
                int index = clbPrintDocs.IndexFromPoint(e.Location);
                if (this.cbPrintDocsIndex != index)
                {
                    clbPrintDocsToolTip();
                }
            }
        }
    }
}
