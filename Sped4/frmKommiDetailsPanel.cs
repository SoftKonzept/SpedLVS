using Sped4.Controls;
using System;
using System.Windows.Forms;


namespace Sped4
{
    public partial class frmKommiDetailsPanel : Sped4.frmTEMPLATE
    {
        //**Container für Doppelclick / Dispoplan auf die einzelnen Kommissionen 
        frmDispoKalender Kalender;
        frmAuftrag_Splitting AuftragSplit;
        ctrAufträge AuftragCtr;
        public ctrSUList ctrSUListe;
        AFKalenderItemKommi Kommi;

        public frmAuftragView _av;


        public frmKommiDetailsPanel(frmDispoKalender _Kalender, AFKalenderItemKommi _Kommi)
        {
            InitializeComponent();
            Kalender = _Kalender;
            AuftragCtr = _Kalender.AuftragCtr;
            Kommi = _Kommi;
        }
        //        
        //
        private void frmMDIDetails_Load(object sender, EventArgs e)
        {
            //Show in Panel 1
            ctrDispoKommiDetails det = new ctrDispoKommiDetails(Kommi.Kommission.ID, Kalender, this);
            det.Parent = splitContainer1.Panel1;
            det.Dock = DockStyle.Fill;
            det.Show();
            //det.BringToFront();

            LoadAuftragView();
        }
        //
        //
        public void LoadAuftragView()
        {
            //------ AuftragsView wird geladen

            // frmAuftragView av = new frmAuftragView(Kommi.Kommission.AuftragID, Kommi.Kommission.AuftragPos, AuftragCtr, AuftragSplit, ctrSUListe);

            // frmAuftragView av = new frmAuftragView(Kommi.Kommission.AuftragPos_ID);
            frmAuftragView av = new frmAuftragView();
            av.GL_User = Kalender.GL_User;
            //av._AuftragPosTableID = Kommi.Kommission.AuftragPos_ID;
            //av._AuftragNr = Kommi.Kommission.AuftragID;
            //av._AuftragPosNr = Kommi.Kommission.AuftragPos;
            av._ctrAuftrag = AuftragCtr;
            av._AuftragSplit = AuftragSplit;
            av._ctrSUListe = ctrSUListe;
            av.FormBorderStyle = FormBorderStyle.None;
            av.SplitContainerCollaped(true);
            av.TopLevel = false;
            av.Dock = DockStyle.Fill;
            av.Parent = splitContainer1.Panel2;
            av.KommiDetailPanel = this;
            av.Show();
            av.ResizeAuftragView += new frmAuftragView.AuftragViewResizeEventHandler(ResizeByFrmAuftragView);
            //av.CloseKommiDetailsPanel += new frmAuftragView.KommiDetailPanelCloseEventHandler(CloseFrmKommiDetailsPanel);
            _av = av;
        }
        //
        //
        private void ResizeByFrmAuftragView()
        {
            //Baustelle austomtischer Resize
            Int32 widthGesamt = 100;
            Int32 leftWidth = 100;
            Int32 rWidth = 100;
            Int32 fixLeftWidth = this.splitContainer1.SplitterDistance; //Feste Größe soll für Panel 1 beibehalten werden


            if (_av.scMainPage.Panel1Collapsed)
            {
                //leftWidth = 598; // _av.splitContainer1.SplitterDistance;
                widthGesamt = 740; // _av.splitContainer1.Size.Width - leftWidth;
                this.Width = fixLeftWidth + widthGesamt + 4 + 10;

            }
            else
            {
                leftWidth = 598; // _av.splitContainer1.SplitterDistance;
                rWidth = 740;// _av.splitContainer1.Width;
                widthGesamt = leftWidth + 4 + rWidth;
                this.Width = fixLeftWidth + 4 + widthGesamt + 10;
            }


            this.splitContainer1.SplitterDistance = fixLeftWidth + 4;
            this.Refresh();
        }
        //
        //
        //
        public void CloseFrmKommiDetailsPanel()
        {
            if (Kommi != null)
            {
                Kommi.RefreshKommiDaten();
            }
            this.Close();
        }

    }
}
