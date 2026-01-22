using LVS;
using System;

namespace Sped4
{
    public partial class frmAbrechnungslisteZeitraum : Sped4.frmTEMPLATE
    {
        //ctrAufträge ctrAuftrag;
        ctrFaktSpedition ctrFakturierung;

        public frmAbrechnungslisteZeitraum(ctrFaktSpedition _ctrFakturierung)
        {
            InitializeComponent();
            ctrFakturierung = _ctrFakturierung;
        }

        private void frmAbrechnungslisteZeitraum_Load(object sender, EventArgs e)
        {
            btn1.Text = "suchen";
            //Startdatum
            if (DateTime.Today.Month == 1)
            {
                dtpVon.Value = Convert.ToDateTime("01.12." + (DateTime.Today.Year - 1));
            }
            else
            {
                dtpVon.Value = Convert.ToDateTime("01." + (DateTime.Today.Month - 1) + "." + DateTime.Today.Year);
            }
            //Enddatum
            dtpBis.Value = DateTime.Today;
        }

        private void btnAbbruch_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if ((dtpVon.Value.Date < dtpBis.Value.Date) | (dtpVon.Value.Date == dtpBis.Value.Date))
            {
                ctrFakturierung.SearchDateVon = dtpVon.Value;
                ctrFakturierung.SearchDateBis = dtpBis.Value;
                this.Close();
            }
            else
            {
                clsMessages.Auftragsliste_ZeitraumAuswahlFalsch();
            }
        }
    }
}
