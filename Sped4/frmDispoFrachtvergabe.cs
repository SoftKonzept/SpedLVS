using Common.Enumerations;
using LVS;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmDispoFrachtvergabe : Sped4.frmTEMPLATE
    {
        public Globals._GL_USER GL_User;
        public decimal _AuftragID;
        public decimal _AuftragPos;
        public decimal _ArtikelTableID;
        public decimal _AuftragPosTableID;
        public decimal _MandantenID;
        public ctrAuftragDetails ad;
        ctrAufträge ctrAuftrag;
        clsDruck druck = new clsDruck(); //??????

        public frmDispoFrachtvergabe(ctrAufträge _ctrAuftrag)
        {
            InitializeComponent();
            ctrAuftrag = _ctrAuftrag;
        }
        //
        private void frmDispoFrachtvergabe_Load(object sender, EventArgs e)
        {
            _MandantenID = clsMandanten.GetMandantenIDFromAuftragByAuftragPosTableID(_AuftragPosTableID, GL_User.User_ID);
            //  GetArtikelDaten();
            druck.AuftragID = _AuftragID;
            druck.AuftragPos = _AuftragPos;

            ctrAuftragDetails _ad = new ctrAuftragDetails();
            ad = _ad;
            ad._AuftragID = _AuftragID;
            ad._AuftragPos = _AuftragPos;
            ad._AuftragPosTableID = _AuftragPosTableID;
            ad.GL_User = GL_User;
            ad.Dock = DockStyle.Fill;
            ad.Parent = this;
            ad.Show();
            ad.BringToFront();
        }
        //
        private void GetArtikelDaten()
        {
            //Baustellle kann weg wenn getesetet wird eigentlich nicht mehr gebraucht
            //Auftragnummer und AuftragPosNummer ermitteln
            DataTable dt = clsArtikel.GetAllArtikeldateDispoByID(this.GL_User, _ArtikelTableID);
            if (dt.Rows.Count > 0)
            {
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //hier können die Artikeldaten übernommen werden
                    _AuftragID = (decimal)dt.Rows[i]["ArtikelID"];
                    _AuftragPos = (decimal)dt.Rows[i]["AuftragPos"];
                    _MandantenID = (decimal)dt.Rows[i]["Mandanten_ID"];
                }
            }
        }
        //
        private void tsbADR_Click(object sender, EventArgs e)
        {
            ad.OpenFrmADRPanelFrachtvergabe();
        }

        //
        //-------- close Form ----------------
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        //------ Transportauftrag speichern, scannen, drucken --------
        //
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            if (ad.SUisIn)
            {
                if (ad.CheckValue())
                {
                    ad.AssignValue();
                    tsbSpeichern.Visible = false;
                    OpenReportView();
                    this.Close();
                    ctrAuftrag.InitDGV();
                    if (ctrAuftrag.ctrSUListe != null)
                    {
                        ctrAuftrag.ctrSUListe.InitCtrSUList();
                    }
                }
                else
                {
                    clsMessages.DateCheck_DateToBeInPastLiefertermin();

                }
            }
            else
            {
                clsMessages.Frachtvergabe_SUfehlt();
            }
        }
        //
        //---------- ReportView wird aufgerufen ---------------------
        //
        private void OpenReportView()
        {
            //Panel für ADR CTR öffnen
            if (Functions.frm_IsFormTypeAlreadyOpen(typeof(frmReportViewer)) != null)
            {
                Functions.frm_FormTypeClose(typeof(frmReportViewer));
            }
            frmReportViewer reportview = new frmReportViewer(ad.dsTransportauftrag, enumDokumentenArt.FrachtauftragAnSU.ToString());
            reportview.GL_User = GL_User;
            reportview._AuftragPosTableID = _AuftragPosTableID;
            reportview._AuftragID = this._AuftragID;
            reportview._MandantenID = _MandantenID;
            reportview.StartPosition = FormStartPosition.CenterParent;
            reportview.Show();
            reportview.BringToFront();
        }




    }
}
