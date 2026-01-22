using LVS;
using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmArbeitsbereiche : Sped4.frmTEMPLATE
    {
        ///<summary>frmArbeitsbereiche</summary>
        ///<remarks>Über dieses Formular können neue Arbeitsberech angelegt, bestehende Arbeitsbereiche geänder oder
        ///         ein Arbeitsbereich deaktiviert werden.
        ///         Die From ist der Container für die beiden CtrArbeitsbereichList udn ctrArbeitsbereichAdd.</remarks>

        public Globals._GL_USER GL_User;
        internal ctrArbeitsbereichAdd _ctrArbeitsbereichAdd;
        internal ctrArbeitsbereichList _ctrArbeitsbereichList;
        internal ctrMenu _ctrMenu;
        internal Int32 _iGrdListWidth;
        internal bool _bUpdate;
        //*******************************************************************************************
        ///<summary>frmMandanten/frmArbeitsbereiche</summary>
        ///<remarks>Initialisierung</remarks>
        public frmArbeitsbereiche()
        {
            InitializeComponent();
        }
        ///<summary>frmMandanten/frmArbeitsbereiche_Load</summary>
        ///<remarks>Initialisierung der beiden Ctr beim Laden der Form.</remarks>
        private void frmArbeitsbereiche_Load(object sender, EventArgs e)
        {
            InitCtrArbeitsbereichAdd();
            InitCtrArbeitsbereichList();
            this.Text = "Arbeitsbereiche";
            InitFrm();
        }
        ///<summary>frmMandanten/InitCtrArbeitsbereichAdd</summary>
        ///<remarks>CTR wird erzeugt und die Form eingebette.</remarks>
        private void InitCtrArbeitsbereichAdd()
        {
            _ctrArbeitsbereichAdd = new ctrArbeitsbereichAdd();
            _ctrArbeitsbereichAdd.Visible = true;  //muss umgekehrt gesetzt weden   
            _ctrArbeitsbereichAdd.GL_User = GL_User;
            if (this._ctrMenu._frmMain != null)
            {
                _ctrArbeitsbereichAdd.Sys = this._ctrMenu._frmMain.system;
            }
            _ctrArbeitsbereichAdd.Parent = this.splitContainerAB.Panel1;
            _ctrArbeitsbereichAdd.Dock = DockStyle.Fill;
            _ctrArbeitsbereichAdd.Show();
            _ctrArbeitsbereichAdd.BringToFront();
        }
        ///<summary>frmMandanten/InitCtrArbeitsbereichList</summary>
        ///<remarks>CTR wird erzeugt und die Form eingebette.</remarks>
        private void InitCtrArbeitsbereichList()
        {
            _ctrArbeitsbereichList = new ctrArbeitsbereichList();
            _ctrArbeitsbereichList.GetArbeitsbereichTakeOver += new ctrArbeitsbereichList.ArbeitsbereichTakeOverEventHandler(_ctrArbeitsbereichList_GetArbeitsbereichTakeOver);
            _ctrArbeitsbereichList.GL_User = GL_User;
            _ctrArbeitsbereichList._frmArbeitsbereiche = this;
            _ctrArbeitsbereichList.Parent = this.splitContainerAB.Panel2;
            _ctrArbeitsbereichList.Dock = DockStyle.Fill;
            _ctrArbeitsbereichList.Show();
            _ctrArbeitsbereichList.BringToFront();
        }
        ///<summary>frmMandanten/InitFrm</summary>
        ///<remarks>Über die jeweiligen Initialisierungsprozeduren der CTR werden die CTRs mit Daten geladen.</remarks>
        public void InitFrm()
        {
            _bUpdate = false;
            _ctrArbeitsbereichAdd.Visible = true;
            _ctrArbeitsbereichList.InitGrd();
            _ctrArbeitsbereichAdd.InitCtrABAdd();
            //Ein- / Ausblenden ctrArbeitsbereichAdd
            HideShowCtrArbeitsbereichAdd();
        }
        ///<summary>frmMandanten/_ctrArbeitsbereichList_GetArbeitsbereichTakeOver</summary>
        ///<remarks>Übergabe der Arbeitsbereichs ID in das Erassungsformular (Update).</remarks>
        void _ctrArbeitsbereichList_GetArbeitsbereichTakeOver(decimal TakeOverID)
        {
            this._ctrArbeitsbereichAdd.SetFrmForUpdate(TakeOverID);
            _ctrArbeitsbereichAdd.Visible = false;
            HideShowCtrArbeitsbereichAdd();
            _bUpdate = true;
        }
        ///<summary>frmMandanten/tsbtnShowHideAbAdd_Click</summary>
        ///<remarks>Ein neuer Arbeitsbereich soll erfasst werden. Dazu wird das Formular (ctrArbeitsbereichAdd) eingeblendet.</remarks>
        private void tsbtnShowHideAbAdd_Click(object sender, EventArgs e)
        {
            //check of Formular eingeblendet
            HideShowCtrArbeitsbereichAdd();
        }
        ///<summary>frmMandanten/HideShowCtrArbeitsbereichAdd</summary>
        ///<remarks>Das Formular wird in der Größe angepasst, wenn das ctrArbeitsbereichAdd ein-/ausgeblendet wird. 
        ///         Hierzu werden die Breiten aller sichtbaren CTR ermittelt und daraus die Gesamtbreite des Formulars
        ///         berechnet.</remarks>
        private void HideShowCtrArbeitsbereichAdd()
        {
            if (_ctrArbeitsbereichAdd.Visible == true)
            {
                _ctrArbeitsbereichAdd.Visible = false;
                this.splitContainerAB.Panel1Collapsed = true;
                tsbtnShowHideAbAdd.Image = Sped4.Properties.Resources.layout_left;
                tsbtnShowHideAbAdd.Text = "Formular für neuen Arbeitsbereich einblenden";
                tsbtnShowHideAbAdd.ToolTipText = "Formular für neuen Arbeitsbereich einblenden";
            }
            else
            {
                _ctrArbeitsbereichAdd.Visible = true;
                this.splitContainerAB.Panel1Collapsed = false;
                tsbtnShowHideAbAdd.Image = Sped4.Properties.Resources.layout;
                tsbtnShowHideAbAdd.Text = "Formular für neuen Arbeitsbereich ausblenden";
                tsbtnShowHideAbAdd.ToolTipText = "Formular für neuen Arbeitsbereich ausblenden";
            }
            SetFrmWidth();
            CheckButtons(_ctrArbeitsbereichAdd.Visible);
        }
        ///<summary>frmMandanten/SetFrmWidth</summary>
        ///<remarks>Berechnung der Gesamtbreite des Formulars.</remarks>
        private void SetFrmWidth()
        {
            if (_ctrArbeitsbereichAdd.Visible == true)
            {
                _iGrdListWidth = _ctrArbeitsbereichList.dgv.Width;  //Functions.dgv_GetWidthShownGrid(ref _ctrArbeitsbereichList.grd);
                this.Width = this.splitContainerAB.SplitterDistance + _iGrdListWidth + 20;
            }
            else
            {
                _iGrdListWidth = _ctrArbeitsbereichList.dgv.Width; //Functions.dgv_GetWidthShownGrid(ref _ctrArbeitsbereichList.grd);
                this.Width = _iGrdListWidth + 20;
            }
        }
        ///<summary>frmMandanten/CheckButtons</summary>
        ///<remarks>Die folgenden Button werden Enable gesetzt:
        ///         - tsbSpeichern >>> Speichern der Daten 
        ///         - tsbClearFrm  >>> manuelles Reseten/Clear der Eingabefelder und Variablen</remarks>
        ///<param name="bEnable">Wird als bool übergeben und setzt entsprechend den Parameter
        ///                      Enable.</param>
        private void CheckButtons(Boolean bEnable)
        {
            tsbSpeichern.Enabled = bEnable;
            tsbtnClearFrm.Enabled = bEnable;
        }
        ///<summary>frmMandanten/tsbtnClose_Click</summary>
        ///<remarks>Schließt das Formular.</remarks>
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmMandanten/tsbSpeichern_Click</summary>
        ///<remarks>Über die folgende Funktionen werden Prüfungen vor der Eintrag / Update der Daten durchgeführt:
        ///         - GL_User.Arbeitsbereich_Access >>> Userberechtigungen
        ///         -_ctrArbeitsbereichAdd.DoCheck() >>> Eingabefelder und Logik 
        ///         - Neueintrag oder Update</remarks>
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            //Check UserBerechtigungen
            if (GL_User.write_Arbeitsbereich)
            {
                //Check EingabeFelder
                if (_ctrArbeitsbereichAdd.DoCheck())
                {
                    //Update oder Neueintrag?
                    if (_bUpdate)
                    {
                        _ctrArbeitsbereichAdd.clsAB.UpdateArbeitsbereich();
                    }
                    else
                    {
                        _ctrArbeitsbereichAdd.clsAB.AddArbreitsbereich();
                        if (_ctrArbeitsbereichAdd.clsAB.ID > 0)
                        {
                            clsPrimeKeys pk = new clsPrimeKeys();
                            pk.Mandanten_ID = _ctrArbeitsbereichAdd.clsAB.MandantenID;
                            pk._GL_User = this._ctrMenu.GL_User;
                            pk.AbBereichID = _ctrArbeitsbereichAdd.clsAB.ID;
                            pk.AddPrimekeysDefault();
                        }
                    }
                    //Form reset
                    InitFrm();
                }
            }
        }
        ///<summary>frmMandanten/tsbtnClearFrm_Click</summary>
        ///<remarks>Reset der CTR ctrArbeitsbereichAdd (Eingabeformular).</remarks>
        private void tsbtnClearFrm_Click(object sender, EventArgs e)
        {
            _ctrArbeitsbereichAdd.ClearFrm();
        }
        ///<summary>frmMandanten/frmArbeitsbereiche_ResizeEnd</summary>
        ///<remarks>Anpassen der Formulargröße beim manuelle Größenänderung.</remarks>
        private void frmArbeitsbereiche_ResizeEnd(object sender, EventArgs e)
        {
            SetFrmWidth();
        }
        ///<summary>frmMandanten/splitContainerAB_SplitterMoved</summary>
        ///<remarks>Anpassen der Formulargröße beim manuelle Verschieben des Splittbalkens.</remarks>
        private void splitContainerAB_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (this.splitContainerAB.SplitterDistance == this.splitContainerAB.Panel1MinSize)
            {
                SetFrmWidth();
            }
            else
            {
                this.splitContainerAB.SplitterDistance = this.splitContainerAB.Panel1MinSize;
            }
        }
        ///<summary>frmMandanten/toolStripButton1_Click</summary>
        ///<remarks></remarks>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _ctrArbeitsbereichAdd.ClearFrm();
            _ctrArbeitsbereichAdd.Visible = true;
            CheckButtons(true);
            this.splitContainerAB.Panel1Collapsed = false;
            tsbtnShowHideAbAdd.Image = Sped4.Properties.Resources.layout;
            tsbtnShowHideAbAdd.Text = "Formular für neuen Arbeitsbereich ausblenden";
            tsbtnShowHideAbAdd.ToolTipText = "Formular für neuen Arbeitsbereich ausblenden";
        }
    }
}
