using System.Windows.Forms;


namespace Sped4
{
    public partial class frmADRPanelFakturierung : Sped4.frmTEMPLATE
    {
        public decimal TakeOverADR_ID = 0;
        ctrADR_List ctrADRList = new ctrADR_List();

        // Container für Adressschnelleingabe in Auftragserfassung
        frmAuftrag_Fast auftragErfassung;
        frmFakturierung fakturierung;
        frmRGGSErstellen RGGSErstellen;
        public frmADRPanelFakturierung()
        {
            InitializeComponent();
        }
        //
        //
        public void SetFakturierungFrm(frmFakturierung _fakturierung)
        {
            fakturierung = _fakturierung;
        }
        public void SetRGGSFrm(frmRGGSErstellen _RGGSErstellen)
        {
            RGGSErstellen = _RGGSErstellen;
        }
        //
        //
        private void OpenCtrADRListe()
        {
            ctrADR_List _ctrADRList = new ctrADR_List();
            ctrADRList = _ctrADRList;
            ctrADRList.Dock = DockStyle.Fill;
            ctrADRList.Name = "TempADR";
            ctrADRList.Parent = panel1;

            if (fakturierung != null)
            {
                FromFrmFakturierung();
            }
            if (RGGSErstellen != null)
            {
                FromFrmRGGSErstellen();
            }
            //ctrADRList.initList(ctrADRList.ADRListeKomplett);
            ctrADRList.initList();
            ctrADRList.Show();
            BringToFront();
            ctrADRList.getADRTakeOver += new ctrADR_List.ADRTakeOverEventHandler(SetSearchADR_ID);
            ctrADRList.closeFrmADRPanelFakturierung += new ctrADR_List.frmADRPanelFakturierungCloseEventHandler(CloseFrmADRPanelFakturierung);
            //ctrADRList.closeFrmADRPanelAuftragserfassung += new ctrADR_List.frmADRPanelAuftragserfassungCloseEventHandler(CloseFrmADRPanelAuftragserfassung);
            ctrADRList.SetADRSucheAktiv();

        }
        //
        //
        private void FromFrmRGGSErstellen()
        {
            if (RGGSErstellen.SearchButton == 1)
            {
                ctrADRList.ADRListeKomplett = true;
                ctrADRList.SetAFColorLabelMyText("Kundenadressliste");
            }
        }
        //
        private void FromFrmFakturierung()
        {
            if (fakturierung.SearchButton == 1)//KUndenadresse
            {
                ctrADRList.ADRListeKomplett = false;
                ctrADRList.SetAFColorLabelMyText("Kundenadressliste");
            }
            else
            {
                ctrADRList.ADRListeKomplett = true;
                if (fakturierung.SearchButton == 2)//Versandadresse
                {
                    ctrADRList.SetAFColorLabelMyText("Versandadressliste");
                }
                if (fakturierung.SearchButton == 3)//Empfangsadresse
                {
                    ctrADRList.SetAFColorLabelMyText("Empfangsadressliste");
                }
                if (fakturierung.SearchButton == 4)//Empfangsadresse
                {
                    ctrADRList.SetAFColorLabelMyText("Liste Subunternehmer");
                }
            }
        }
        //
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            OpenCtrADRListe();
        }
        //
        //------ übergibt die ADR ID aus der Suche ------
        //
        private void SetSearchADR_ID(decimal ADR_ID)
        {
            TakeOverADR_ID = ADR_ID;
            if (fakturierung != null)
            {
                fakturierung.SetADRRecAfterADRSearch(TakeOverADR_ID);
            }
            if (RGGSErstellen != null)
            {
                RGGSErstellen.SetADRRecAfterADRSearch(TakeOverADR_ID);
            }
        }
        //
        //------close form --
        //
        private void CloseFrmADRPanelFakturierung()
        {
            this.Close();
        }
    }
}
