using LVS;
using Sped4.Controls;
using Sped4.Controls.ASNCenter;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmTmp : frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public AFKalenderItemTour ctrAFKalenderItemTour;
        public frmDispoKalender Kalender;
        public ctrMenu ctrMenu;
        public ctrTourDetails _ctrTourDetails;
        public ctrDistance _ctrDistance;
        public ctrLagerOrt _ctrLagerort;
        public ctrAuslagerung _ctrAuslagerung;
        public ctrEinlagerung _ctrEinlagerung;
        public ctrUmbuchung _ctrUmbuchung;
        public ctrSearch _ctrSearch;
        public ctrSchaeden _ctrSchaeden;
        public ctrPrintLager _ctrPrintLager;
        public ctrFaktArtikelList _ctrFaktArtikelList;
        public ctrMailCockpit _ctrMailCockpit;
        public ctrExtraChargeAssignment _ctrExtraChargeAssignment;
        public ctrFreeForCall _ctrFreeForCall;
        public ctrADRManAdd _ctrAdrManAdd;
        public ctrPostCenter _ctrPostCenter;
        public ctrArbeitsliste _ctrArbeitsliste;
        public ctrInfoPanel _ctrInfoPanel;
        public ctrSPLAdd _ctrSPLAdd;
        public ctrRetoure _ctrRetoureEingang;
        public ctrBSInfo4905 _ctrBSInfo4905;
        public ctrVDA4984Details _ctrVDA4984Details;
        public ctrASNArtFieldAssignSelectToCopy _ctrASNArtFieldAssignSelectToCopy;
        public ctrASNActionSelectToCopy _ctrASNActionSelectToCopy;
        public ctrJobSelectToCopy _ctrJobSelectToCopy;
        public ctrVDAClientWorkspaceValueSelectToCopy _ctrVDAClientWorkspaceValueSelectToCopy;
        public ctrInventoryAdd _ctrInventoryAdd;

        /***************************************************************************************
         * 
         * ************************************************************************************/
        public frmTmp()
        {
            InitializeComponent();
        }
        ///<summary>frmTmp / frmTmp_Load</summary>
        ///<remarks>Folgende Funktionen werden ausgeführt:
        ///         - ComboMandanten füllen
        ///         - Ermittel des letzten Lagerausgangs
        ///         - Daten des Lagerausgangs auf die Form
        ///         - Laden der entsprechenden Artikeldaten</remarks>
        private void frmTmp_Load(object sender, EventArgs e)
        {
            //DispoKalender
            if (this.Kalender != null)
            {
                this.ctrMenu = Kalender.menue;
                OpenCtrTourDetails();
            }
            //Entfernungscockpit
            if (this._ctrDistance != null)
            {
                OpenCtrDistance();
            }
            //Lagerort
            if (this._ctrLagerort != null)
            {
                OpenCtrLagerort();
            }
            //Einlagerung
            if (this._ctrEinlagerung != null)
            {
                OpenCtrEinlagerung();
            }
            //Auslagerung
            if (this._ctrAuslagerung != null)
            {
                OpenCtrAuslagerung();
            }
            //Umbuchung
            if (this._ctrUmbuchung != null)
            {
                OpenCtrUmbuchung();
            }
            //Search
            if (this._ctrSearch != null)
            {
                OpenCtrSearch();
            }
            //Schaden
            if (this._ctrSchaeden != null)
            {
                OpenCtrSchaden();
            }
            //Print Lager
            if (this._ctrPrintLager != null)
            {
                OpenCtrPrintLager();
            }
            //Print Lager
            if (this._ctrFaktArtikelList != null)
            {
                OpenCtrFaktArtikelList();
            }
            //MailCockpit
            if (this._ctrMailCockpit != null)
            {
                OpenCtrMailCockpit();
            }
            //ExtraCharge
            if (this._ctrExtraChargeAssignment != null)
            {
                OpenCtrExtraChargeAssignment();
            }
            //FreeForCall - Freigabe Abrufe
            if (this._ctrFreeForCall != null)
            {
                OpenCtrFreeForCall();
            }
            //FreeForCall - Freigabe Abrufe
            if (this._ctrAdrManAdd != null)
            {
                OpenCtrAdrManAdd();
            }
            //PostCenter
            if (this._ctrPostCenter != null)
            {
                OpenCtrPostCenter();
            }
            if (this._ctrArbeitsliste != null)
            {
                OpenCtrArbeitsliste();
            }
            //Infopanel
            if (this._ctrInfoPanel != null)
            {
                OpenCtrInfoPanel();
            }
            //ctrSPLADD
            if (this._ctrSPLAdd != null)
            {
                OpenCtrSPLAdd();
            }
            //ctrRetoure
            if (this._ctrRetoureEingang is ctrRetoure)
            {
                OpenCtrRetoureEingang();
            }
            //_ctrBSInfo4905
            if (this._ctrBSInfo4905 is ctrBSInfo4905)
            {
                OpenCtBSInfo4905();
            }
            //_ctrVDA4905Details
            if (this._ctrVDA4984Details is ctrVDA4984Details)
            {
                OpenCtrVDA4984Details();
            }
            //
            if (this._ctrASNArtFieldAssignSelectToCopy is ctrASNArtFieldAssignSelectToCopy)
            {
                OpenCtrASNArtFieldAssignSelectToCopy();
            }
            //
            if (this._ctrASNActionSelectToCopy is ctrASNActionSelectToCopy)
            {
                OpenCtrASNActionSelectToCopy();
            }
            //
            if (this._ctrJobSelectToCopy is ctrJobSelectToCopy)
            {
                OpenCtrJobSelectToCopy();
            }
            if (this._ctrVDAClientWorkspaceValueSelectToCopy is ctrVDAClientWorkspaceValueSelectToCopy)
            {
                OpenCtrVDAClientWorkspaceValueSelectToCopy();
            }
            if (this._ctrInventoryAdd is ctrInventoryAdd)
            {
                OpenCtrInventoryAdd();
            }

            ctrMenu.PropertyChanged += new PropertyChangedEventHandler(UpdateProgress);
        }
        ///<summary>frmTmp / UpdateProgress</summary>
        ///<remarks></remarks>
        private void UpdateProgress(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Close":
                    //MessageBox.Show(FaktLager.strProgress);
                    this.CloseFrmTmp();
                    break;
            }
        }
        ///<summary>frmTmp / _ctrLagerort_ctrLagerClose</summary>
        ///<remarks></remarks>
        void _ctrLagerort_ctrLagerClose()
        {
            throw new NotImplementedException();
        }
        ///<summary>frmTmp / tsbClose_Click</summary>
        ///<remarks></remarks>
        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseFrmTmp();
        }
        ///<summary>frmTmp / CloseFrmTmp</summary>
        ///<remarks></remarks>
        public void CloseFrmTmp()
        {
            if (this.Kalender != null)
            {
                this.Kalender.KalenderRefresh();
            }
            if (this._ctrEinlagerung != null)
            {
                this._ctrEinlagerung.Dispose();
            }
            if (this._ctrSearch != null)
            {
                this._ctrSearch._ctrEinlagerung = null;
                this._ctrSearch._ctrAuslagerung = null;
                this._ctrSearch.Dispose();
            }
            if (this._ctrUmbuchung != null)
            {
                this._ctrUmbuchung.Dispose();
            }
            //this.Close();
            this.Dispose();
        }
        ///<summary>frmTmp / OpenCtrTourDetails</summary>
        ///<remarks></remarks>
        private void OpenCtrTourDetails()
        {
            if (this.Kalender != null)
            {
                this.Text = "Anzeige Tourdetails";
                _ctrTourDetails = new ctrTourDetails();
                this.Width = _ctrTourDetails.Width + 30;
                this.Height = _ctrTourDetails.Height + 80;
                _ctrTourDetails.GL_User = GL_User;
                _ctrTourDetails.ctrMenu = this.ctrMenu;
                _ctrTourDetails.Kalender = this.ctrMenu._Kalender;
                _ctrTourDetails.Dock = DockStyle.Fill;
                _ctrTourDetails.Parent = this.panAusgabe;
                _ctrTourDetails.Show();
                _ctrTourDetails.BringToFront();
            }
        }
        ///<summary>frmTmp / OpenCtrDistance</summary>
        ///<remarks></remarks>
        private void OpenCtrDistance()
        {
            this.Text = "Entfernungscockpit";
            this.Width = _ctrDistance.Width + 30;
            this.Height = _ctrDistance.Height + 50;
            _ctrDistance.GL_User = GL_User;
            _ctrDistance._ctrMenu = this.ctrMenu;
            _ctrDistance.ctrDistanceClose += new ctrDistance.ctrDistanceCloseEventHandler(this.CloseFrmTmp);
            _ctrDistance.Dock = DockStyle.Fill;
            _ctrDistance.Parent = this.panAusgabe;
            _ctrDistance.Show();
            _ctrDistance.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrLagerort</summary>
        ///<remarks></remarks>
        private void OpenCtrLagerort()
        {
            this.Text = "Lagerort";
            this.Width = _ctrLagerort.Width + 30;
            this.Height = _ctrLagerort.Height + 50;
            _ctrLagerort.GL_User = GL_User;
            _ctrLagerort._ctrMenu = this.ctrMenu;
            _ctrLagerort.afColorLabel1.Visible = false; //ColorLabel ausblenden
            _ctrLagerort.afToolStrip1.Visible = false; //Menü ausblenden
            _ctrLagerort._frmTmp = this;
            _ctrLagerort.ctrLagerClose += new ctrLagerOrt.ctrLagerOrtCloseEventHandler(this.CloseFrmTmp);
            _ctrLagerort.Dock = DockStyle.Fill;
            _ctrLagerort.Parent = this.panAusgabe;
            _ctrLagerort.Show();
            _ctrLagerort.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrLagerort</summary>
        ///<remarks></remarks>
        private void OpenCtrAuslagerung()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = "Lagerausgang";
            this.Width = _ctrAuslagerung.Width + 30;
            this.Height = _ctrAuslagerung.Height + 100;
            this.AutoSize = true;
            _ctrAuslagerung.GL_User = GL_User;
            _ctrAuslagerung._ctrMenu = this.ctrMenu;
            _ctrAuslagerung._frmTmp = this;
            _ctrAuslagerung.InitCtrAuslagerung();
            _ctrAuslagerung.Dock = DockStyle.Fill;
            _ctrAuslagerung.Parent = this.panAusgabe;
            _ctrAuslagerung.Show();
            _ctrAuslagerung.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrEinlagerung</summary>
        ///<remarks></remarks>
        private void OpenCtrEinlagerung()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = "Lagereingang";
            this.Width = _ctrEinlagerung.Width + 30;
            this.Height = _ctrEinlagerung.Height + 100;
            this.AutoSize = true;
            _ctrEinlagerung.GL_User = GL_User;
            _ctrEinlagerung._ctrMenu = this.ctrMenu;
            _ctrEinlagerung._frmTmp = this;
            _ctrEinlagerung.InitCtrEinlagerung();
            _ctrEinlagerung.Dock = DockStyle.Fill;
            _ctrEinlagerung.Parent = this.panAusgabe;
            _ctrEinlagerung.Show();
            _ctrEinlagerung.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrUmbuchung</summary>
        ///<remarks></remarks>
        private void OpenCtrUmbuchung()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = "Umbuchung";
            this.Width = _ctrUmbuchung.Width + 30;
            this.Height = _ctrUmbuchung.Height + 100;
            this.AutoSize = true;
            _ctrUmbuchung.GL_User = GL_User;
            _ctrUmbuchung._ctrMenu = this.ctrMenu;
            _ctrUmbuchung._frmTmp = this;
            _ctrUmbuchung.InitCtr();
            _ctrUmbuchung.Dock = DockStyle.Fill;
            _ctrUmbuchung.Parent = this.panAusgabe;
            _ctrUmbuchung.Show();
            _ctrUmbuchung.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrSearch</summary>
        ///<remarks></remarks>
        private void OpenCtrSearch()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = "Suche";
            this.Width = _ctrSearch.Width + 50;
            this.Height = _ctrSearch.Height + 180;
            _ctrSearch.GL_User = GL_User;
            _ctrSearch._ctrMenu = this.ctrMenu;
            _ctrSearch._ctrMenu._FrmTmp = this;
            _ctrSearch._frmTmp = this;
            _ctrSearch.Parent = this.panAusgabe;
            _ctrSearch.Dock = DockStyle.Fill;
            _ctrSearch.Show();
            _ctrSearch.BringToFront();
            _ctrSearch.InitCtr();
        }

        private void OpenCtrArbeitsliste()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = "Arbeitsliste";
            this.Width = _ctrArbeitsliste.Width + 50;
            this.Height = _ctrArbeitsliste.Height + 180;
            _ctrArbeitsliste.GL_User = GL_User;
            _ctrArbeitsliste._ctrMenu = this.ctrMenu;
            _ctrArbeitsliste._ctrMenu._FrmTmp = this;
            _ctrArbeitsliste._frmTmp = this;
            _ctrArbeitsliste.Parent = this.panAusgabe;
            _ctrArbeitsliste.Dock = DockStyle.Fill;
            _ctrArbeitsliste.Show();
            _ctrArbeitsliste.BringToFront();
            _ctrArbeitsliste.InitCtr();
        }
        ///<summary>frmTmp / OpenCtrSchaden</summary>
        ///<remarks></remarks>
        private void OpenCtrSchaden()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = "Schadensauswahl";
            this.Width = _ctrSchaeden.Width + 85;
            this.Height = _ctrSchaeden.Height + 300;
            _ctrSchaeden.GL_User = GL_User;
            _ctrSchaeden._ctrMenu = this.ctrMenu;
            _ctrSchaeden._frmTmp = this;
            _ctrSchaeden.Dock = DockStyle.Fill;
            _ctrSchaeden.Parent = this.panAusgabe;
            _ctrSchaeden.Show();
            _ctrSchaeden.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrSchaden</summary>
        ///<remarks></remarks>
        private void OpenCtrPrintLager()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = "Print-Center LAGER";
            this.Width = _ctrPrintLager.Width + 30;
            this.Height = _ctrPrintLager.Height + 100;
            _ctrPrintLager.GL_User = GL_User;
            _ctrPrintLager._ctrMenu = this.ctrMenu;
            _ctrPrintLager._ctrAuslagerung = this.ctrMenu._ctrPrintLager._ctrAuslagerung;
            _ctrPrintLager._ctrEinlagerung = this.ctrMenu._ctrPrintLager._ctrEinlagerung;
            _ctrPrintLager._frmTmp = this;
            _ctrPrintLager.Dock = DockStyle.Fill;
            _ctrPrintLager.Parent = this.panAusgabe;
            _ctrPrintLager.Show();
            _ctrPrintLager.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrFaktArtikelList</summary>
        ///<remarks></remarks>
        private void OpenCtrFaktArtikelList()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = "Fakturierung: Artikelliste";
            this.Width = _ctrFaktArtikelList.Width + 30;
            this.Height = _ctrFaktArtikelList.Height + 100;
            _ctrFaktArtikelList.GL_User = GL_User;
            _ctrFaktArtikelList._ctrMenu = this.ctrMenu;
            _ctrFaktArtikelList._frmTmp = this;
            _ctrFaktArtikelList.Dock = DockStyle.Fill;
            _ctrFaktArtikelList.Parent = this.panAusgabe;
            _ctrFaktArtikelList.InitCtr();
            _ctrFaktArtikelList.Show();
            _ctrFaktArtikelList.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrMailCockpit</summary>
        ///<remarks></remarks>
        private void OpenCtrMailCockpit()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrMailCockpit.Width + 30;
            this.Height = _ctrMailCockpit.Height + 100;
            _ctrMailCockpit.GL_User = GL_User;
            _ctrMailCockpit.ctrMenu = this.ctrMenu;
            _ctrMailCockpit.frmTmp = this;
            _ctrMailCockpit.Dock = DockStyle.Fill;
            _ctrMailCockpit.Parent = this.panAusgabe;
            //_ctrMailCockpit.InitCtr();
            _ctrMailCockpit.Show();
            _ctrMailCockpit.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrExtraChargeAssignment</summary>
        ///<remarks></remarks>
        private void OpenCtrExtraChargeAssignment()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrExtraChargeAssignment.Width + 30;
            this.Height = _ctrExtraChargeAssignment.Height + 100;
            _ctrExtraChargeAssignment.GLUser = GL_User;
            _ctrExtraChargeAssignment.InitGlobals(this.ctrMenu);
            _ctrExtraChargeAssignment._frmTmp = this;
            _ctrExtraChargeAssignment.Dock = DockStyle.Fill;
            _ctrExtraChargeAssignment.Parent = this.panAusgabe;
            _ctrExtraChargeAssignment.Show();
            _ctrExtraChargeAssignment.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrExtraChargeAssignment</summary>
        ///<remarks></remarks>
        private void OpenCtrFreeForCall()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrFreeForCall.Width + 30;
            this.Height = _ctrFreeForCall.Height + 100;
            _ctrFreeForCall.GLUser = GL_User;
            _ctrFreeForCall.InitGlobals(this.ctrMenu);
            _ctrFreeForCall._frmTmp = this;
            _ctrFreeForCall.Dock = DockStyle.Fill;
            _ctrFreeForCall.Parent = this.panAusgabe;
            _ctrFreeForCall.Show();
            _ctrFreeForCall.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrAdrManAdd</summary>
        ///<remarks></remarks>
        private void OpenCtrAdrManAdd()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrAdrManAdd.Width + 30;
            this.Height = _ctrAdrManAdd.Height + 100;
            _ctrAdrManAdd.GLUser = GL_User;
            //_ctrAdrManAdd.ini(this.ctrMenu);
            _ctrAdrManAdd._frmTmp = this;
            _ctrAdrManAdd.Dock = DockStyle.Fill;
            _ctrAdrManAdd.Parent = this.panAusgabe;
            _ctrAdrManAdd.Show();
            _ctrAdrManAdd.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrPostCenter</summary>
        ///<remarks></remarks>
        private void OpenCtrPostCenter()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrPostCenter.Width + 10;
            this.Height = _ctrPostCenter.Height + 10;
            _ctrPostCenter.GL_User = GL_User;
            _ctrPostCenter._frmTmp = this;
            _ctrPostCenter.Dock = DockStyle.Fill;
            _ctrPostCenter.Parent = this.panAusgabe;
            _ctrPostCenter.Show();
            _ctrPostCenter.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrInfoPanel</summary>
        ///<remarks></remarks>
        private void OpenCtrInfoPanel()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrPostCenter.Width + 10;
            this.Height = _ctrPostCenter.Height + 10;
            //_ctrInfoPanel.GL_User = GL_User;
            _ctrInfoPanel._frmTmp = this;
            _ctrInfoPanel.Dock = DockStyle.Fill;
            _ctrInfoPanel.Parent = this.panAusgabe;
            _ctrInfoPanel.Show();
            _ctrInfoPanel.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrSPLAdd</summary>
        ///<remarks></remarks>
        private void OpenCtrSPLAdd()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrSPLAdd.Width + 10;
            this.Height = _ctrSPLAdd.Height + 10;
            //_ctrInfoPanel.GL_User = GL_User;
            _ctrSPLAdd._frmTmp = this;
            _ctrSPLAdd.Dock = DockStyle.Fill;
            _ctrSPLAdd.Parent = this.panAusgabe;
            _ctrSPLAdd.Show();
            _ctrSPLAdd.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrSPLAdd</summary>
        ///<remarks></remarks>
        private void OpenCtrRetoureEingang()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrRetoureEingang.Width + 10;
            this.Height = _ctrRetoureEingang.Height + 10;
            _ctrRetoureEingang._frmTmp = this;
            _ctrRetoureEingang.Dock = DockStyle.Fill;
            _ctrRetoureEingang.Parent = this.panAusgabe;
            _ctrRetoureEingang.Show();
            _ctrRetoureEingang.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrSPLAdd</summary>
        ///<remarks></remarks>
        private void OpenCtBSInfo4905()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrBSInfo4905.Width + 10;
            this.Height = _ctrBSInfo4905.Height + 10;
            _ctrBSInfo4905._frmTmp = this;
            _ctrBSInfo4905.Dock = DockStyle.Fill;
            _ctrBSInfo4905.Parent = this.panAusgabe;
            _ctrBSInfo4905.Show();
            _ctrBSInfo4905.BringToFront();
        }
        ///<summary>frmTmp / OpenCtrSPLAdd</summary>
        ///<remarks></remarks>
        private void OpenCtrVDA4984Details()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrVDA4984Details.Width + 10;
            this.Height = _ctrVDA4984Details.Height + 10;
            _ctrVDA4984Details._frmTmp = this;
            _ctrVDA4984Details.Dock = DockStyle.Fill;
            _ctrVDA4984Details.Parent = this.panAusgabe;
            _ctrVDA4984Details.Show();
            _ctrVDA4984Details.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OpenCtrASNArtFieldAssignSelectToCopy()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrASNArtFieldAssignSelectToCopy.Width + 10;
            this.Height = _ctrASNArtFieldAssignSelectToCopy.Height + 10;
            _ctrASNArtFieldAssignSelectToCopy._frmTmp = this;
            _ctrASNArtFieldAssignSelectToCopy.Dock = DockStyle.Fill;
            _ctrASNArtFieldAssignSelectToCopy.Parent = this.panAusgabe;
            _ctrASNArtFieldAssignSelectToCopy.Show();
            _ctrASNArtFieldAssignSelectToCopy.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OpenCtrASNActionSelectToCopy()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrASNActionSelectToCopy.Width + 50;
            this.Height = _ctrASNActionSelectToCopy.Height + 20;
            _ctrASNActionSelectToCopy._frmTmp = this;
            _ctrASNActionSelectToCopy.Dock = DockStyle.Fill;
            _ctrASNActionSelectToCopy.Parent = this.panAusgabe;
            _ctrASNActionSelectToCopy.Show();
            _ctrASNActionSelectToCopy.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OpenCtrJobSelectToCopy()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrJobSelectToCopy.Width + 10;
            this.Height = _ctrJobSelectToCopy.Height + 10;
            _ctrJobSelectToCopy._frmTmp = this;
            _ctrJobSelectToCopy.Dock = DockStyle.Fill;
            _ctrJobSelectToCopy.Parent = this.panAusgabe;
            _ctrJobSelectToCopy.Show();
            _ctrJobSelectToCopy.BringToFront();
        }

        private void OpenCtrVDAClientWorkspaceValueSelectToCopy()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrVDAClientWorkspaceValueSelectToCopy.Width + 10;
            this.Height = _ctrVDAClientWorkspaceValueSelectToCopy.Height + 10;
            _ctrVDAClientWorkspaceValueSelectToCopy._frmTmp = this;
            _ctrVDAClientWorkspaceValueSelectToCopy.Dock = DockStyle.Fill;
            _ctrVDAClientWorkspaceValueSelectToCopy.Parent = this.panAusgabe;
            _ctrVDAClientWorkspaceValueSelectToCopy.Show();
            _ctrVDAClientWorkspaceValueSelectToCopy.BringToFront();
        }

        private void OpenCtrInventoryAdd()
        {
            this.afCLTmp.Visible = false;
            this.afTSTmp.Visible = false;
            this.Text = string.Empty;
            this.Width = _ctrInventoryAdd.Width + 10;
            this.Height = _ctrInventoryAdd.Height + 10;
            _ctrInventoryAdd._frmTmp = this;
            _ctrInventoryAdd.Dock = DockStyle.Fill;
            _ctrInventoryAdd.Parent = this.panAusgabe;
            _ctrInventoryAdd.Show();
            _ctrInventoryAdd.BringToFront();
        }
    }
}