using LVS;
using System;

namespace Sped4
{
    public partial class frmAuftragslisteZeitraum : Sped4.frmTEMPLATE
    {
        public ctrAufträge ctrAuftrag;
        public ctrFaktSpedition ctrFakturierung;
        //public ctrLager ctrLager;
        public ctrSUList ctrSUListe;


        public frmAuftragslisteZeitraum()
        {
            InitializeComponent();
        }
        //
        //
        private void frmAuftragslisteZeitraum_Load(object sender, EventArgs e)
        {
            if (ctrSUListe != null)
            {
                dtpVon.Value = DateTime.Today.AddDays(-8);
                dtpBis.Value = DateTime.Today;
            }
            else
            {
                dtpVon.Value = DateTime.Today.AddDays(-1);
                dtpBis.Value = DateTime.Today.AddDays(8);
            }
            btn1.Text = "suchen";
        }
        //
        //
        //
        private void btnAbbruch_Click(object sender, EventArgs e)
        {
            if (ctrSUListe != null)
            {
                if (ctrAuftrag != null)
                {
                    ctrAuftrag.CloseSUList();
                }
            }
            else
            {

                if (ctrAuftrag != null)
                {
                    ctrAuftrag.AbbruchFrmAuftragslisteZeitraum();
                }
                if (ctrFakturierung != null)
                {

                }

            }
            this.Close();
        }
        //
        //---------- Übernahme Zeitraum - --------------------
        //
        private void btn1_Click(object sender, EventArgs e)
        {
            if (ctrSUListe != null)
            {
                if (dtpVon.Value.Date < DateTime.Now.Date)
                {
                    //ctrSUListe.Show();
                    ctrSUListe.SetSearchTimeDistance(dtpVon.Value.Date, dtpBis.Value.Date);
                    ctrSUListe.InitCtrSUList();
                    this.Close();
                }
                else
                {
                    clsMessages.Auftragsliste_SULIste_ZeitraumAuswahlFalsch();
                }
            }
            else
            {
                if ((dtpVon.Value.Date < dtpBis.Value.Date) | (dtpVon.Value.Date == dtpBis.Value.Date))
                {
                    if (ctrAuftrag != null)
                    {
                        ctrAuftrag.SetSearchTimeDistance(dtpVon.Value.Date, dtpBis.Value.Date);
                        ctrAuftrag.InitDGV();
                    }
                    if (ctrFakturierung != null)
                    {

                    }

                    this.Close();
                }
                else
                {
                    clsMessages.Auftragsliste_ZeitraumAuswahlFalsch();
                }
            }
        }
    }
}
