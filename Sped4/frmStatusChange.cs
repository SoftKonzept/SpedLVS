using LVS;
using LVS.Dokumente;
using System;
using System.Data;
namespace Sped4
{
    public partial class frmStatusChange : Sped4.frmTEMPLATE
    {
        internal clsAuftrag Auftrag;
        public Globals._GL_USER GL_User;
        clsAuftragsstatus ast = new clsAuftragsstatus();
        public ctrFaktSpedition ctrFakturierung;
        public ctrAufträge ctrAuftrag;
        public ctrSUList ctrSUListe;
        internal decimal RGGS;
        public DataTable dt = new DataTable();
        public bool CallFromFakturierung = false;
        public delegate void RefreshAuftragListEventHandler();
        public event RefreshAuftragListEventHandler RefreshAuftragList;


        ///<summary>frmStatusChange / InitClass</summary>
        ///<remarks></remarks>
        public frmStatusChange()
        {
            InitializeComponent();
        }
        ///<summary>frmStatusChange / frmStatusChange_Load</summary>
        ///<remarks></remarks>
        private void frmStatusChange_Load(object sender, EventArgs e)
        {
            InitFrm();
            SetAllCheckboxToFalse();
            //if (CallFromFakturierung)
            //{ 
            //  //über RGNr
            //  Status = (Int32)dt.Rows[0]["Status"];
            //}

            SetCheckBoxCheckedByStatus();
        }
        ///<summary>frmStatusChange / SetCheckBoxCheckedByStatus</summary>
        ///<remarks></remarks>
        private void SetCheckBoxCheckedByStatus()
        {
            switch (this.Auftrag.AuftragPos.Status)
            {
                case 4:
                    cbDisponiert.Checked = true;

                    cbBezahlt.Enabled = false;
                    cbBerechnet.Enabled = false;
                    cbFreigabeBerechnung.Enabled = false;
                    break;

                case 5:
                    cbDoksUnvoll.Checked = true;

                    cbBerechnet.Enabled = false;
                    cbBezahlt.Enabled = false;
                    break;

                case 6:
                    cbFreigabeBerechnung.Checked = true;

                    cbBezahlt.Enabled = false;
                    break;

                case 7:
                    cbBerechnet.Checked = true;
                    dtp_NewDate.Enabled = true;
                    cbDisponiert.Enabled = false;
                    cbDoksUnvoll.Enabled = false;
                    cbFreigabeBerechnung.Enabled = false;
                    break;

                case 8:
                    cbBezahlt.Checked = true;
                    dtp_NewDate.Enabled = true;
                    cbDisponiert.Enabled = false;
                    cbDoksUnvoll.Enabled = false;
                    cbFreigabeBerechnung.Enabled = false;
                    break;

            }
        }
        ///<summary>frmStatusChange / SetAllCheckboxToFalse</summary>
        ///<remarks></remarks>
        private void SetAllCheckboxToFalse()
        {
            cbDisponiert.Checked = false;
            cbDoksUnvoll.Checked = false;
            cbFreigabeBerechnung.Checked = false;
            cbBerechnet.Checked = false;
            cbBezahlt.Checked = false;
            dtp_NewDate.Enabled = false;
        }
        ///<summary>frmStatusChange / InitFrm</summary>
        ///<remarks></remarks>
        private void InitFrm()
        {
            if (CallFromFakturierung)
            {
                cbDisponiert.Text = "Auftrag wurde disponiert und ist in Bearbeitung";
                cbDoksUnvoll.Text = "Auftrag wurde druchgeführt - Dokumente/Ablieferbelege unvollständig";
                cbFreigabeBerechnung.Text = "Dokumente/Ablieferbelege vollständig\r\n Freigabe zur Berechnung!";

                lAuftrag.Text = "Rechnung : " + RGGS.ToString();
                lAuftragPos.Visible = false;
            }
            else
            {
                cbDisponiert.Text = "Auftrag wurde disponiert und ist in Bearbeitung";
                cbDoksUnvoll.Text = "Auftrag wurde druchgeführt - Dokumente/Ablieferbelege unvollständig";
                cbFreigabeBerechnung.Text = "Dokumente/Ablieferbelege vollständig\r\n Freigabe zur Berechnung!";

                lAuftrag.Text = "Auftrag: " + Auftrag.ANr.ToString();
                lAuftragPos.Text = " Auftragsposition: " + Auftrag.AuftragPos.AuftragPos.ToString();
            }
        }
        ///<summary>frmStatusChange / tsbtnSavePrint_Click</summary>
        ///<remarks></remarks>
        private void tsbtnSavePrint_Click(object sender, EventArgs e)
        {
            if (CallFromFakturierung)
            {
                //update db Rechnungen
                clsRechnungen rg = new clsRechnungen();
                rg.Rechnungsnummer = RGGS;
                rg.UpdateRGBezahlt();

                //Update db Auftrag Pos
                for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    clsAuftragsstatus ast = new clsAuftragsstatus();
                    ast.BenutzerID = GL_User.User_ID;
                    ast.AP_ID = (decimal)dt.Rows[i]["ID"];
                    ast.SetStatusBezahlt();
                }
            }
            else
            {
                //Wenn alle notwendingen Dokumente vorliegen muss der Transport manuell auf 
                //die Freigabe zur Berechnung erteilt werden
                if (cbDisponiert.Checked == true)
                {
                    this.Auftrag.AuftragPos.Status = 4;
                }
                if (cbDoksUnvoll.Checked == true)
                {
                    this.Auftrag.AuftragPos.Status = 5;
                }
                if (cbFreigabeBerechnung.Checked == true)
                {
                    this.Auftrag.AuftragPos.Status = 6;
                }
                if (cbBerechnet.Checked == true)
                {
                    this.Auftrag.AuftragPos.Status = 7;
                }
                if (cbBezahlt.Checked == true)
                {
                    this.Auftrag.AuftragPos.Status = 8;
                }

                //ast.BenutzerID = GL_User.User_ID;
                //ast.Auftrag_ID = Auftrag;
                //ast.AuftragPos = AuftragPos;
                //ast.Status = Status;
                //UpdateStatus();
                this.Auftrag.AuftragPos.Update();
            }
            if (ctrFakturierung != null)
            {

            }
            if (ctrAuftrag != null)
            {
                ctrAuftrag.InitDGV();
            }
            if (ctrSUListe != null)
            {
                ctrSUListe.InitCtrSUList();
            }
            this.Close();
        }
        ///<summary>frmStatusChange / tsbtnSavePrint_Click</summary>
        ///<remarks></remarks>
        private void UpdateStatus()
        {
            switch (this.Auftrag.AuftragPos.Status)
            {
                case 4:
                    ast.SetStatusDisposition();
                    break;

                case 5:
                    ast.SetStatusDone();
                    break;

                case 6:
                    ast.SetStatusForBerechnung();
                    break;

                case 7:
                    ast.SetStatusBerechnet();
                    break;

                case 8:
                    ast.SetStatusBezahlt();
                    break;

                default:
                    break;
            }
        }

        //
        //------------- Form close -----------------
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        /****************************************************************************
         *                              Comboboxen 
         * 
         * **************************************************************************/
        //
        //
        private void cbDoksUnvoll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDoksUnvoll.Checked == true)
            {
                cbDisponiert.Checked = false;
                cbFreigabeBerechnung.Checked = false;
                cbBerechnet.Checked = false;
                cbBezahlt.Checked = false;
            }
            else
            {
                //cbDoksUnvoll.Checked = true;
            }
        }
        //
        //
        private void cbBerechnet_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBerechnet.Checked == true)
            {
                cbDisponiert.Checked = false;
                cbDoksUnvoll.Checked = false;
                cbFreigabeBerechnung.Checked = false;
                cbBezahlt.Checked = false;
            }
            else
            {
                //cbBerechnet.Checked = false;
            }
        }
        //
        //
        private void cbBezahlt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBezahlt.Checked == true)
            {
                cbDisponiert.Checked = false;
                cbDoksUnvoll.Checked = false;
                cbFreigabeBerechnung.Checked = false;
                cbBerechnet.Checked = false;
            }
            else
            {
                //cbBezahlt.Checked = false;
            }
        }
        //
        //
        private void cbFreigabeBerechnung_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFreigabeBerechnung.Checked == true)
            {
                cbDisponiert.Checked = false;
                cbDoksUnvoll.Checked = false;
                cbBerechnet.Checked = false;
                cbBezahlt.Checked = false;
            }
            else
            {
                //cbFreigabeBerechnung.Checked = false;
            }
        }
        //
        //
        //
        private void cbDisponiert_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDisponiert.Checked == true)
            {
                cbDoksUnvoll.Checked = false;
                cbFreigabeBerechnung.Checked = false;
                cbBerechnet.Checked = false;
                cbBezahlt.Checked = false;
            }
            else
            {
                cbDisponiert.Checked = false;
            }
        }

    }
}
