using LVS;
using LVS.Dokumente;
using System;

namespace Sped4
{
    public partial class frmLiestViewer : Sped4.frmTEMPLATE
    {
        public decimal iADR;
        bool Kunde;
        public Globals._GL_USER GL_User;
        docList_Avisierung List_AV;

        public frmLiestViewer(Boolean _Kunde, decimal iID)
        {
            InitializeComponent();
            Kunde = _Kunde;
            iADR = iID;
        }
        //
        //
        //
        private void frmLiestViewer_Load(object sender, EventArgs e)
        {
            IniListView();

        }
        //
        public void IniListView()
        {
            List_AV = new docList_Avisierung();
            List_AV.GL_User = GL_User;
            this.Text = "Avisierungsliste";
            List_AV.InitListe(Kunde, iADR);
            this.repView.Report = List_AV;


            //Functions.AddLogbuch(BenutzerID, Globals.enumLogbuchAktion.Druck.ToString(), Beschreibung);
            this.repView.RefreshReport();
        }
    }
}
