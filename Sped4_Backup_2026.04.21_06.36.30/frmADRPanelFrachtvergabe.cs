using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmADRPanelFrachtvergabe : Sped4.frmTEMPLATE
    {
        public ctrAuftragDetails ctrAuftragDetails;
        public frmADRPanelFrachtvergabe(ctrAuftragDetails myCtrAuftragDetails)
        {
            InitializeComponent();
            ctrAuftragDetails = myCtrAuftragDetails;
        }
        //
        //----------- LOAD Form  -------------------------
        //
        private void frmADRPanelFrachtvergabe_Load(object sender, EventArgs e)
        {
            OpenCtrADRListe();
        }
        //
        //
        //
        private void OpenCtrADRListe()
        {
            ctrADR_List ctrADRList = new ctrADR_List();
            ctrADRList.ADRListeKomplett = false;
            ctrADRList.SetAFColorLabelMyText("Subunternehmeradressliste");
            ctrADRList.Dock = DockStyle.Fill;
            ctrADRList.Name = "TempADR";
            ctrADRList.Parent = this;
            //ctrADRList.initList(ctrADRList.ADRListeKomplett);
            ctrADRList.initList();
            ctrADRList.Show();
            BringToFront();
            ctrADRList.getADRTakeOver += new ctrADR_List.ADRTakeOverEventHandler(ctrAuftragDetails.SetSubunternehmer);
            ctrADRList.closeFrmADRPanelAuftragserfassung += new ctrADR_List.frmADRPanelAuftragserfassungCloseEventHandler(ClosePanelFrachtvergabe);
            ctrADRList.SetADRSucheAktiv();
        }
        //
        //-------- Close -----------------
        //
        private void ClosePanelFrachtvergabe()
        {
            this.Close();
        }
    }
}
