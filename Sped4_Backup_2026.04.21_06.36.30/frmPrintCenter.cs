using LVS;
using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmPrintCenter : frmTEMPLATE
    {
        internal clsTour Tour;
        public Globals._GL_USER GL_User;
        public Sped4.Controls.AFKalenderItemKommi _AFKalenderItemKommi;
        //public frmDispoKalender _Kalender;
        public decimal _AuftragID = 0;
        public decimal _AuftragPos = 0;
        public decimal _ArtikelTableID = 0;
        public decimal _AuftragTableID = 0;
        public decimal _AuftragPosTableID = 0;
        public decimal _MandantenID = 0;

        public frmPrintCenter()
        {
            InitializeComponent();
        }
        //
        //
        //
        private void frmPrintCenter_Load(object sender, EventArgs e)
        {

            this.Text = "[Auftrag]-[Pos]: " + _AuftragID + "-" + _AuftragPos;
            if (_AuftragPosTableID != 0)
            {
                OpenAuftragDetailsInPanel();
            }
        }
        //
        //
        private void OpenAuftragDetailsInPanel()
        {
            try
            {
                ctrArtDetails artD = new ctrArtDetails();
                artD.GL_User = GL_User;
                artD.Auftrag = Tour.Auftrag;
                //artD.AuftragPosTableID = _AuftragPosTableID;
                artD.Parent = splitContainer1.Panel1;
                artD.Dock = DockStyle.Fill;


                ctrPrint pri = new ctrPrint(_AuftragPosTableID);
                pri.GL_User = GL_User;
                pri.ctrArtD = artD;
                pri._frmPrintCenter = this;
                pri.Parent = splitContainer1.Panel2;
                pri.Dock = DockStyle.Fill;
                pri.Show();
                pri.BringToFront();


                artD._ctrPrint = pri;
                artD.Show();
                artD.BringToFront();

            }
            catch (Exception ex)
            {
                decimal decUser = -1.00M;
                Functions.AddLogbuch(decUser, "OpenAuftragDetailsInPanel", ex.ToString());
            }
        }
        //
        //--------------- Form Close-------------------------
        //
        private void tsbtnFrmClose_Click(object sender, EventArgs e)
        {
            if (_AFKalenderItemKommi != null)
            {
                this._AFKalenderItemKommi.RefreshKommiDaten();
            }
            this.Close();
        }

    }
}
