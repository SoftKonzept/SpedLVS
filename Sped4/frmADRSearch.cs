using LVS;
using Sped4.Controls.AdminCockpit;
using Sped4.Controls.ASNCenter;
using Sped4.Controls.Edifact;
using Sped4.Controls.Processes;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;


namespace Sped4
{
    public partial class frmADRSearch : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public ctrADR_List ctrADRList;
        internal clsADR ADRSearch;
        public delegate void ThreadCtrInvokeEventHandler();
        //BAUSTELLE private bool ADRSearchEinAuslager = false;    // neu


        // Container für Adressschnelleingabe in Auftragserfassung
        public frmAuftrag_Fast auftragErfassung;
        public frmMandanten mandanten;
        public ctrEinlagerung einlagerung;
        public ctrAuslagerung auslagerung;
        public ctrArtDetails ctrArtDetail;
        public ctrUmbuchung ctrUmbuchung;
        public ctrBestand ctrBestand;
        public ctrPrint ctrPrint;
        public ctrMenu ctrMenu;
        public ctrPrintLager ctrPrintLager;
        public ctrFaktLager ctrFaktLager;
        public ctrFreeForCall ctrFreeForCall;
        public ctrSearch ctrSearch;
        public ctrRGList ctrRGList;
        public ctrJournal ctrJournal;
        public ctrRGManuell ctrRGManuell;
        public ctrStatistik ctrStatistik;
        public ctrGueterArtListe ctrGArtenListe;
        public ctrADRSearch ctrADRSearch;
        public ctrWorklist ctrWorklist;
        //public ctrADR_List ctrADR_ListTmp;
        public ctrReportSetting ctrRepSetting;
        public ctrASNAction ctrASNAction;
        public ctrASNMain ctrASNMain;
        public ctrAdrVerweis ctrAdrVerweis;
        public ctrVDAClientOut ctrVDAClientOut;
        public ctrASNArtFieldAssignment ctrASNArtFieldAssign;
        public ctrASNActionSelectToCopy ctrASNActionSelectToCopy;
        public ctrJob ctrJob;
        public ctrCronJobs ctrCronJob;

        public ctrVDAClientWorkspaceValue ctrVDAClientWorkspaceValue;
        public ctrEdiClientWorkspaceValue ctrEdiAdrWorkspaceAssignment;
        public ctrCreateEdiStruckture ctrCreateEdiStruckture;
        public ctrCustomProcess ctrCustomProcess;
        public ctrEDIFACTClientOut ctrEdifactClientOUT;

        ///<summary>frmADRSearch / InitCtrADRSearch</summary>
        ///<remarks>.</remarks>
        public frmADRSearch()
        {
            InitializeComponent();
        }
        ///<summary>frmADRSearch / InitCtrADRSearch</summary>
        ///<remarks>.</remarks>
        public void InitCtrADRSearch()
        {
            ADRSearch = new clsADR();
            ADRSearch._GL_User = this.GL_User;

            Int32 SearchButton = 0;
            if (this.ctrADRSearch != null)
            {
                this.ctrADRList = null;
                ctrADRSearch.GL_User = this.ctrADRSearch._ctrMenu._frmMain.GL_User;
                this.ctrADRSearch._frmADRSearch = this;
                SearchButton = this.ctrMenu._ctrAdrList.ADRSearchButton;
                //ctrADRList.ADRListAuswahl = SearchButton;

                ctrADRSearch.Dock = DockStyle.Fill;
                ctrADRSearch.Name = "TempADRSearch";
                ctrADRSearch.Parent = panel1;
                ctrADRSearch.iAdrRange = clsADR.const_AdrRange_AdrListeKomplett;
                ctrADRSearch.InitCtr();
                //SearchButton
                // 1 = Auftraggeber
                // 2 = Versender
                // 3 = Empfänger
                // 4 = neutrale Versandadresse
                // 5 = neutrale Empfangsadresse
                // 6 = Mandanten
                // 7 = Spedition
                // 8 = Kunden
                // 9 = neutraler Auftraggeber
                // 10 = Rechnungsadresse
                // 11 = Postadresse
                // 12 = Entladestelle
                // 13 = Beladestelle
                switch (SearchButton)
                {
                    case 1:
                        //ctrADRSearch.ADRListeKomplett = false;
                        ctrADRSearch.SetAFColorLabelMyText("Kunden-/Auftraggeberadressliste");
                        break;
                    case 2:
                        ctrADRSearch.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeVersenderString);
                        break;
                    case 3:
                        ctrADRSearch.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeEmpfaengerString);
                        break;
                    case 4:
                        ctrADRSearch.SetAFColorLabelMyText("neutrale Versandadresse");
                        break;
                    case 5:
                        ctrADRSearch.SetAFColorLabelMyText("neutrale Empfangsadresse");
                        break;
                    case 6:
                        ctrADRSearch.SetAFColorLabelMyText("Mandantenadresse");
                        break;
                    case 7:
                        ctrADRSearch.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeSpeditionString);
                        break;
                    case 8:
                        //ctrADRSearch.ADRListeKomplett = false;
                        ctrADRSearch.SetAFColorLabelMyText("Kundenadressliste");
                        break;
                    case 9:
                        ctrADRSearch.SetAFColorLabelMyText("neutrale Auftraggeberadresse");
                        break;
                    case 10:
                        ctrADRSearch.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeRechnungString);
                        break;
                    case 11:
                        ctrADRSearch.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListePostString);
                        break;
                    case 12:
                        ctrADRSearch.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeBeladeString);
                        break;
                    case 13:
                        ctrADRSearch.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeEntladeString);
                        break;
                }
                this.ctrADRSearch.Show();
                this.BringToFront();
                this.ctrMenu._ctrAdrList.getADRTakeOver += new ctrADR_List.ADRTakeOverEventHandler(SetSearchADR_ID);
                //this.ctrMenu._ctrAdrList.closeFrmADRPanelAuftragserfassung += new ctrADR_List.frmADRPanelAuftragserfassungCloseEventHandler(CloseFrmADRPanelFakturierung);
                this.ctrADRSearch.getADRTakeOver += new ctrADRSearch.ADRTakeOverEventHandler(SetSearchADR_ID);
            }
            else
            {
                if (ctrADRList != null)
                {
                    //ctrADRList = this.ctrMenu.InitADRList();
                    ctrADRList._frmADRSearch = this;
                    //ctrADRList._ctrMenu = this.ctrMenu;

                    if (this.ctrMenu._ctrAdrList != null)
                    {
                        this.GL_User = this.ctrMenu._ctrAdrList.GL_User;
                        SearchButton = this.ctrMenu._ctrAdrList.ADRSearchButton;
                        //this.ctrADR_ListTmp._frmADRSearch = this;

                        //this.GL_User = this.ctrADR_ListTmp.GL_User;
                        //SearchButton = this.ctrADR_ListTmp.ADRSearchButton;
                        //this.ctrADR_ListTmp._frmADRSearch = this;
                    }
                    else
                    {
                        this.GL_User = this.ctrADRList.GL_User;
                        SearchButton = this.ctrADRList.ADRSearchButton;
                        //this.ctrADRList._frmADRSearch = this;
                    }
                }
                else
                {
                    ctrADRList = this.ctrMenu.InitADRList();
                    ctrADRList._frmADRSearch = this;
                    ctrADRList._ctrMenu = this.ctrMenu;
                    //ctrADRList.SetGlobalValue(this.ctrMenu);
                }
                if (auftragErfassung != null)
                {
                    ctrADRList.GL_User = this.auftragErfassung._ctrMenu._frmMain.GL_User;
                    SearchButton = auftragErfassung.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (mandanten != null)
                {
                    ctrADRList.GL_User = this.mandanten.GL_User;
                    SearchButton = mandanten.SearchButton;
                }
                if (einlagerung != null)
                {
                    ctrADRList.GL_User = this.einlagerung.GL_User;
                    SearchButton = einlagerung.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (auslagerung != null)
                {
                    ctrADRList.GL_User = this.auslagerung.GL_User;
                    SearchButton = auslagerung._iSearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrArtDetail != null)
                {
                    ctrADRList.GL_User = this.ctrArtDetail.GL_User;
                    SearchButton = ctrArtDetail.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrPrint != null)
                {
                    ctrADRList.GL_User = this.ctrPrint.GL_User;
                    SearchButton = ctrPrint.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrPrintLager != null)
                {
                    ctrADRList.GL_User = this.ctrPrintLager.GL_User;
                    SearchButton = ctrPrintLager.SearchButton;
                }
                if (ctrUmbuchung != null)
                {
                    ctrADRList.GL_User = this.ctrUmbuchung.GL_User;
                    SearchButton = ctrUmbuchung.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrBestand != null)
                {
                    ctrADRList.GL_User = this.ctrBestand.GL_User;
                    SearchButton = ctrBestand.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrFaktLager != null)
                {
                    ctrADRList.GL_User = this.ctrFaktLager.GL_User;
                    SearchButton = ctrFaktLager.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrFreeForCall != null)
                {
                    ctrFreeForCall.GLUser = this.ctrFreeForCall.GLUser;
                    SearchButton = ctrFreeForCall.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrSearch != null)
                {
                    ctrSearch.GL_User = this.ctrSearch.GL_User;
                    SearchButton = this.ctrSearch._ctrArtSearchFilter.iSearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrRGList != null)
                {
                    ctrRGList.GL_User = this.ctrRGList.GL_User;
                    SearchButton = this.ctrRGList.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrJournal != null)
                {
                    ctrJournal.GL_User = this.ctrJournal.GL_User;
                    SearchButton = this.ctrJournal.SearchButton;
                    ctrADRList.ADRListAuswahl = SearchButton;
                }
                if (ctrStatistik != null)
                {
                    ctrStatistik.GL_User = this.ctrStatistik.GL_User;
                    SearchButton = this.ctrStatistik.SearchButton;
                    ctrStatistik.SearchButton = SearchButton;
                }
                if (ctrGArtenListe != null)
                {
                    ctrGArtenListe.GL_User = this.ctrGArtenListe.GL_User;
                    SearchButton = this.ctrGArtenListe.SearchButton;
                    ctrGArtenListe.SearchButton = SearchButton;
                }
                if (ctrWorklist != null)
                {
                    ctrWorklist.GL_User = this.ctrWorklist.GL_User;
                    SearchButton = this.ctrWorklist.iSearchADRButton;
                    ctrWorklist.iSearchADRButton = SearchButton;
                }
                if (ctrRepSetting != null)
                {
                    //ctrRepSetting.GLUser = this.ctrRepSetting.GLUser;
                    SearchButton = this.ctrRepSetting.SearchButton;
                    //ctrRepSetting.SearchButton = SearchButton;
                }
                if (ctrASNAction is ctrASNAction)
                {
                    SearchButton = this.ctrASNAction.SearchButton;
                }
                if (ctrASNMain is ctrASNMain)
                {
                    SearchButton = this.ctrASNMain.SearchButton;
                }
                if (ctrAdrVerweis is Sped4.Controls.ASNCenter.ctrAdrVerweis)
                {
                    SearchButton = this.ctrAdrVerweis.SearchButton;
                }
                if (ctrVDAClientOut is Sped4.Controls.ASNCenter.ctrVDAClientOut)
                {
                    SearchButton = this.ctrVDAClientOut.SearchButton;
                }
                if (ctrASNArtFieldAssign is Sped4.Controls.ASNCenter.ctrASNArtFieldAssignment)
                {
                    SearchButton = this.ctrASNArtFieldAssign.SearchButton; ;
                }
                if (ctrASNActionSelectToCopy is Sped4.Controls.ASNCenter.ctrASNActionSelectToCopy)
                {
                    SearchButton = this.ctrASNActionSelectToCopy.SearchButton; ;
                }
                if (ctrJob is Sped4.Controls.ASNCenter.ctrJob)
                {
                    SearchButton = this.ctrJob.SearchButton;
                }
                if (ctrCronJob is ctrCronJobs)
                {
                    SearchButton = this.ctrCronJob.SearchButton;
                }
                if (ctrVDAClientWorkspaceValue is Sped4.Controls.ASNCenter.ctrVDAClientWorkspaceValue)
                {
                    SearchButton = this.ctrVDAClientWorkspaceValue.SearchButton;
                }
                if (ctrEdiAdrWorkspaceAssignment is Sped4.Controls.Edifact.ctrEdiClientWorkspaceValue)
                {
                    SearchButton = this.ctrEdiAdrWorkspaceAssignment.SearchButton;
                }
                if (ctrCreateEdiStruckture is Sped4.Controls.Edifact.ctrCreateEdiStruckture)
                {
                    SearchButton = this.ctrCreateEdiStruckture.SearchButton;
                }
                if (ctrCustomProcess is Sped4.Controls.Processes.ctrCustomProcess)
                {
                    SearchButton = this.ctrCustomProcess.SearchButton;
                }
                if (ctrEdifactClientOUT is ctrEDIFACTClientOut)
                {
                    SearchButton = this.ctrEdifactClientOUT.SearchButton;
                }

                ctrADRList.Dock = DockStyle.Fill;
                ctrADRList.Name = "TempADR";
                ctrADRList.Parent = panel1;

                ctrADRList.ADRListeKomplett = true;
                ctrADRList.iAdrRange = clsADR.const_AdrRange_AdrListeKomplett;
                //SearchButton
                // 1 = Auftraggeber
                // 2 = Versender
                // 3 = Empfänger
                // 4 = neutrale Versandadresse
                // 5 = neutrale Empfangsadresse
                // 6 = Mandanten
                // 7 = Spedition
                // 8 = Kunden
                // 9 = neutraler Auftraggeber
                // 10 = Rechnungsadresse
                // 11 = Postadresse
                // 12 = Entladestelle
                // 13 = Beladestelle
                // 14 = Abruf Empfänger
                // 15 =Abruf Spedition
                // 16 = Abruf Entladestelle
                switch (SearchButton)
                {
                    case 1:
                        ctrADRList.ADRListeKomplett = false;
                        ctrADRList.SetAFColorLabelMyText("Kunden-/Auftraggeberadressliste");
                        break;
                    case 2:
                        ctrADRList.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeVersenderString);
                        break;
                    case 3:
                    case 14:
                        ctrADRList.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeEmpfaengerString);
                        break;
                    case 4:
                        ctrADRList.SetAFColorLabelMyText("neutrale Versandadresse");
                        break;
                    case 5:
                        ctrADRList.SetAFColorLabelMyText("neutrale Empfangsadresse");
                        break;
                    case 6:
                        ctrADRList.SetAFColorLabelMyText("Mandantenadresse");
                        break;
                    case 7:
                    case 15:
                        ctrADRList.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeSpeditionString);
                        break;
                    case 8:
                        ctrADRList.ADRListeKomplett = false;
                        ctrADRList.SetAFColorLabelMyText("Kundenadressliste");
                        break;
                    case 9:
                        ctrADRList.SetAFColorLabelMyText("neutrale Auftraggeberadresse");
                        break;
                    case 10:
                        ctrADRList.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeRechnungString);
                        break;
                    case 11:
                        ctrADRList.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListePostString);
                        break;
                    case 12:
                    case 16:
                        ctrADRList.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeBeladeString);
                        break;
                    case 13:
                        ctrADRList.SetAFColorLabelMyText(clsADR.const_AdrRange_AdrListeEntladeString);
                        break;
                }

                ctrADRList.initList(true);//CF
                ctrADRList.SetADRSucheAktiv();
                ctrADRList.Show();
                BringToFront();
                ctrADRList.getADRTakeOver += new ctrADR_List.ADRTakeOverEventHandler(SetSearchADR_ID);
                ctrADRList.closeFrmADRPanelAuftragserfassung += new ctrADR_List.frmADRPanelAuftragserfassungCloseEventHandler(CloseFrmADRPanelFakturierung);
            }
        }
        ///<summary>frmADRSearch / SetSearchADR_ID</summary>
        ///<remarks>übergibt die ADR ID aus der Suche</remarks>
        private void SetSearchADR_ID(decimal ADR_ID)
        {
            ADRSearch.ID = ADR_ID;
            ADRSearch.Fill();

            if (this.ctrMenu._ctrAdrList != null)
            {
                this.ctrMenu._ctrAdrList.SetADRAfterADRSearch(ADRSearch.ID);
            }

            else if (auftragErfassung != null)
            {
                auftragErfassung.SetKDRecAfterADRSearch(ADRSearch.ID);
            }
            else if (mandanten != null)
            {
                mandanten.TakeOverAdressID(ADRSearch.ID);
            }
            else if (einlagerung != null)
            {
                einlagerung.SetKDRecAfterADRSearch(ADRSearch.ID, false);
            }
            else if (auslagerung != null)
            {
                auslagerung.TakeOverAdressID(ADRSearch.ID);
            }
            else if (ctrArtDetail != null)
            {
                ctrArtDetail.SetKDRecAfterADRSearch(ADRSearch.ID);
            }
            else if (ctrPrint != null)
            {
                ctrPrint.TakeOverADRID(ADRSearch.ID);
            }
            else if (ctrPrintLager != null)
            {
                ctrPrintLager.TakeOverADRID(ADRSearch.ID);
            }
            else if (ctrUmbuchung != null)
            {
                ctrUmbuchung.SetADRByID(ADRSearch.ID);
            }
            else if (ctrBestand != null)
            {
                ctrBestand.SetADRByID(ADRSearch.ID);
            }
            else if (ctrFaktLager != null)
            {
                ctrFaktLager.SetADRToFrm(ADRSearch.ID);
            }
            else if (ctrFreeForCall != null)
            {
                ctrFreeForCall.SetADRToFrm(ADRSearch.ID);
            }
            else if (ctrSearch != null)
            {
                ctrSearch._ctrArtSearchFilter.SetADRToFrm(ADRSearch.ID);
            }
            else if (ctrRGList != null)
            {
                ctrRGList.SetADRByID(ADRSearch.ID);
            }
            else if (ctrJournal != null)
            {
                ctrJournal._ctrArtSearchFilter.SetADRToFrm(ADRSearch.ID);
            }
            else if (ctrRGManuell != null)
            {
                ctrRGManuell.SetRechnungsEmpfaenger(ADRSearch.ID);
            }
            else if (ctrStatistik != null)
            {
                ctrStatistik.SetADRToFrm(ADRSearch.ID);
            }
            else if (ctrGArtenListe != null)
            {
                ctrGArtenListe.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrWorklist != null)
            {
                ctrWorklist.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrRepSetting != null)
            {
                ctrRepSetting.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrASNAction is ctrASNAction)
            {
                if (ctrASNAction.SearchButton == 1)
                {
                    ctrASNAction.TakeOverAdrID(ADRSearch.ID);
                }
                else
                {
                    ctrASNAction.SetADRAfterADRSearch(ADRSearch.ID);
                }
            }
            else if (ctrASNMain is ctrASNMain)
            {
                ctrASNMain.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrAdrVerweis is Sped4.Controls.ASNCenter.ctrAdrVerweis)
            {
                ctrAdrVerweis.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrVDAClientOut is Sped4.Controls.ASNCenter.ctrVDAClientOut)
            {
                ctrVDAClientOut.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrEdifactClientOUT is ctrEDIFACTClientOut)
            {
                ctrEdifactClientOUT.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrASNArtFieldAssign is Sped4.Controls.ASNCenter.ctrASNArtFieldAssignment)
            {
                ctrASNArtFieldAssign.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrASNActionSelectToCopy is Sped4.Controls.ASNCenter.ctrASNActionSelectToCopy)
            {
                ctrASNActionSelectToCopy.SetADRAfterADRSearch(ADRSearch.ID);
            }
            else if (ctrJob is Sped4.Controls.ASNCenter.ctrJob)
            {
                ctrJob.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrCronJob is ctrCronJobs)
            {
                ctrCronJob.TakeOverAdrID(ADRSearch.ID);
            }
            else if (ctrVDAClientWorkspaceValue is Sped4.Controls.ASNCenter.ctrVDAClientWorkspaceValue)
            {
                this.ctrVDAClientWorkspaceValue.TakeOverAdrId(ADRSearch.ID);
            }
            else if (ctrEdiAdrWorkspaceAssignment is Sped4.Controls.Edifact.ctrEdiClientWorkspaceValue)
            {
                this.ctrEdiAdrWorkspaceAssignment.TakeOverAdrID((int)ADRSearch.ID);
            }
            else if (ctrCreateEdiStruckture is Sped4.Controls.Edifact.ctrCreateEdiStruckture)
            {
                this.ctrCreateEdiStruckture.TakeOverAdrID((int)ADRSearch.ID);
            }
            else if (ctrCustomProcess is Sped4.Controls.Processes.ctrCustomProcess)
            {
                this.ctrCustomProcess.TakeOverAdrID((int)ADRSearch.ID);
            }
            else
            {
                this.ctrADRList.SetADRAfterADRSearch(ADRSearch.ID);
            }
        }
        ///<summary>frmADRSearch / CloseFrmADRPanelFakturierung</summary>
        ///<remarks>Form schliessen</remarks>
        private void CloseFrmADRPanelFakturierung()
        {
            CloseFrm();
        }
        ///<summary>frmADRSearch / CloseFrm</summary>
        ///<remarks>Form schliessen</remarks>
        public void CloseFrm()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            CloseFrm();
                                                                        }
                                                                    )
                                 );
                return;
            }
            if (this.ctrMenu._ctrAdrList != null)
            {
                this.ctrMenu._ctrAdrList = null;
            }
            this.Close(); // CF
        }
        ///<summary>frmADRSearch / WaitForClosing</summary>
        ///<remarks></remarks>
        public void WaitForClosing()
        {
            bgWorker.RunWorkerAsync();
        }
        ///<summary>frmADRSearch / bgWorker_DoWork</summary>
        ///<remarks></remarks>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            CloseFrm();
        }
    }
}
