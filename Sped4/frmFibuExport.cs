using LVS;
using Sped4.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmFibuExport : frmTEMPLATE
    {
        public DataTable dt = new DataTable();
        public string Exportliste = string.Empty;

        public frmFibuExport()
        {
            InitializeComponent();
        }
        //
        //
        //
        private void frmFibuExport_Load(object sender, EventArgs e)
        {
            //DataSource für Monatsauswahl
            cbMonat.DataSource = Functions.GetMonatsnamen();

            //Jahr setzen
            nudJahr.Value = Convert.ToDecimal(DateTime.Today.Year.ToString());

            //Auswahl Vorgänge
            cbAll.Checked = true;

            //kein Zeitraum als Start
            cbZeitraum.Checked = true;
        }
        //
        //-------------- Zeitraum deaktivieren ---------------
        //
        private void cbZeitraum_CheckedChanged(object sender, EventArgs e)
        {
            if (cbZeitraum.Checked == true)
            {
                gbZeitraum.Enabled = false;
            }
            else
            {
                gbZeitraum.Enabled = true;
            }
        }
        //
        //-------- Close Form ------------
        //
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        //---------- Enable Column --------------
        //
        private void DGVEnableColumns()
        {
            if (dgv.Rows.Count > 0)
            {
                dgv.Columns["Rechnungs_ID"].Visible = false;
                dgv.Columns["Frachten_ID"].Visible = false;
                dgv.Columns["BelegNummer"].Visible = false;
                dgv.Columns["Belegdatum"].Visible = false;
                dgv.Columns["GS_ID"].Visible = false;
                dgv.Columns["GS"].Visible = false;
                dgv.Columns["GS_SU"].Visible = false;
                dgv.Columns["FVGS"].Visible = false;
                dgv.Columns["KontoNummer"].Visible = false;
                dgv.Columns["Empfaenger"].Visible = false;
                dgv.Columns["NettoBetrag"].Visible = false;
                dgv.Columns["MwStSatz"].Visible = false;
                dgv.Columns["MwStBetrag"].Visible = false;
                dgv.Columns["BruttoBetrag"].Visible = false;
                dgv.Columns["RGGSArt"].Visible = false;
                dgv.Columns["gesendet"].Visible = false;
            }
        }
        //
        //------------------------------------------------------
        //
        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv.Rows.Count > 0)
            {
                clsFibuExport xFibu = new clsFibuExport();

                if (dgv.Columns["Rechnungs_ID"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Rechnungs_ID"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["Rechnungs_ID"].Value != null)
                        {
                            xFibu.Rechnungen_ID = (decimal)dgv.Rows[e.RowIndex].Cells["Rechnungs_ID"].Value;
                        }
                    }
                }
                if (dgv.Columns["Frachten_ID"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Frachten_ID"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["Frachten_ID"].Value != null)
                        {
                            xFibu.Frachten_ID = (decimal)dgv.Rows[e.RowIndex].Cells["Frachten_ID"].Value;
                        }
                    }
                }
                if (dgv.Columns["BelegNummer"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["BelegNummer"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["BelegNummer"].Value != null)
                        {
                            xFibu.BelegNummer = (decimal)dgv.Rows[e.RowIndex].Cells["BelegNummer"].Value;
                        }
                    }
                }
                if (dgv.Columns["GS_ID"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["GS_ID"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["GS_ID"].Value != null)
                        {
                            xFibu.BelegNummer = (decimal)dgv.Rows[e.RowIndex].Cells["GS_ID"].Value;
                        }
                    }
                }
                if (dgv.Columns["KontoNummer"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["KontoNummer"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["KontoNummer"].Value != null)
                        {
                            xFibu.KontoNummer = (Int32)dgv.Rows[e.RowIndex].Cells["KontoNummer"].Value;
                        }
                    }
                }
                if (dgv.Columns["Empfaenger"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Empfaenger"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["Empfaenger"].Value != null)
                        {
                            xFibu.Empfaenger = (string)dgv.Rows[e.RowIndex].Cells["Empfaenger"].Value;
                        }
                    }
                }
                if (dgv.Columns["NettoBetrag"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["NettoBetrag"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["NettoBetrag"].Value != null)
                        {
                            xFibu.NettoBetrag = (decimal)dgv.Rows[e.RowIndex].Cells["NettoBetrag"].Value;
                        }
                    }
                }
                if (dgv.Columns["MwStSatz"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["MwStSatz"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["MwStSatz"].Value != null)
                        {
                            xFibu.MwStSatz = (decimal)dgv.Rows[e.RowIndex].Cells["MwStSatz"].Value;
                        }
                    }
                }
                if (dgv.Columns["MwStBetrag"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["MwStBetrag"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["MwStBetrag"].Value != null)
                        {
                            xFibu.NettoBetrag = (decimal)dgv.Rows[e.RowIndex].Cells["MwStBetrag"].Value;
                        }
                    }
                }
                if (dgv.Columns["BruttoBetrag"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["BruttoBetrag"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["BruttoBetrag"].Value != null)
                        {
                            xFibu.BruttoBetrag = (decimal)dgv.Rows[e.RowIndex].Cells["BruttoBetrag"].Value;
                        }
                    }
                }
                if (dgv.Columns["RGGSArt"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["RGGSArt"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["RGGSArt"].Value != null)
                        {
                            xFibu.RGGSArt = (Int32)dgv.Rows[e.RowIndex].Cells["RGGSArt"].Value;
                        }
                    }
                }
                if (dgv.Columns["Belegdatum"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["Belegdatum"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["Belegdatum"].Value != null)
                        {
                            xFibu.Belegdatum = (DateTime)dgv.Rows[e.RowIndex].Cells["Belegdatum"].Value;
                        }
                    }
                }
                if (dgv.Columns["gesendet"] != null)
                {
                    if ((!object.ReferenceEquals(dgv.Rows[e.RowIndex].Cells["gesendet"].Value, DBNull.Value)))
                    {
                        if (dgv.Rows[e.RowIndex].Cells["gesendet"].Value != null)
                        {
                            xFibu.gesendet = (bool)dgv.Rows[e.RowIndex].Cells["gesendet"].Value;
                        }
                    }
                }

                /***************************************************************************************/

                //Typ Art
                if (e.ColumnIndex == 0)
                {
                    string Typ = string.Empty;
                    switch (xFibu.RGGSArt)
                    {
                        case 1:
                            Typ = "RG";
                            break;
                        case 2:
                            Typ = "GS";
                            break;
                        case 3:
                            Typ = "FVGS";
                            break;
                        case 4:
                            Typ = "GSSU";
                            break;
                    }
                    e.Value = Typ;
                }
                //BelegNummer
                if (e.ColumnIndex == 1)
                {
                    e.Value = xFibu.BelegNummer;
                }
                //Adressat / Empfänger
                if (e.ColumnIndex == 2)
                {
                    e.Value = xFibu.Empfaenger;
                }
                //Datum
                if (e.ColumnIndex == 3)
                {
                    e.Value = xFibu.Belegdatum.ToShortDateString(); ;
                }
                //BruttoBetrag
                if (e.ColumnIndex == 4)
                {
                    e.Value = Functions.FormatDecimal(xFibu.BruttoBetrag);
                }
                //Konto
                if (e.ColumnIndex == 5)
                {
                    e.Value = xFibu.KontoNummer;
                }
                //Übergabe
                if (e.ColumnIndex == 6)
                {
                    if (xFibu.gesendet)
                    {

                        //e.Value = Sped4.Properties.Resources.done;
                    }
                    else
                    {
                        //e.Value = Sped4.Properties.Resources.Culture;
                    }
                }

            }


        }
        //
        //------------ Daten laden aus DB ---------------
        //
        private void tsBtnGet_Click(object sender, EventArgs e)
        {
            clsFibuExport ex = new clsFibuExport();
            //DB Abfrage
            dt.Clear();
            dt = ex.GetDatenExportToFibu(Exportliste);

            dgv.DataSource = dt;
            DGVEnableColumns();

        }
        //
        //----------------- Checkbox ALL --------------------------
        //
        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked == true)
            {
                Exportliste = enumFibuExportListe.All.ToString();
                cbRG.Checked = true;
                cbGS.Checked = true;
                cbGSSU.Checked = true;
                cbFVGS.Checked = true;
            }
            else
            {
                cbRG.Checked = false;
                cbGS.Checked = false;
                cbGSSU.Checked = false;
                cbFVGS.Checked = false;
            }
        }
        //----------- CheckBox Rechnungen ------------------------
        private void cbRG_CheckedChanged(object sender, EventArgs e)
        {
            //Rechnungen
            if (cbRG.Checked == true)
            {
                if (cbAll.Checked == false)
                {
                    Exportliste = enumFibuExportListe.RG.ToString();
                    cbGS.Checked = false;
                    cbGSSU.Checked = false;
                    cbFVGS.Checked = false;
                }
            }
        }
        //------------ Checkbox Gutschriften ------------------
        private void cbGS_CheckedChanged(object sender, EventArgs e)
        {
            //Gutschriften
            if (cbGS.Checked == true)
            {
                if (cbAll.Checked == false)
                {
                    Exportliste = enumFibuExportListe.GS.ToString();
                    cbRG.Checked = false;
                    cbGSSU.Checked = false;
                    cbFVGS.Checked = false;
                }
            }

        }
        //------------ Checkbox Unternehmergutschrift -------------------
        private void cbGSSU_CheckedChanged(object sender, EventArgs e)
        {
            //Unternehmergutschriften
            if (cbGSSU.Checked == true)
            {
                if (cbAll.Checked == false)
                {
                    Exportliste = enumFibuExportListe.GSSU.ToString();
                    cbRG.Checked = false;
                    cbGS.Checked = false;
                    cbFVGS.Checked = false;
                }
            }

        }
        //------------- Checkbox FVGS -----------------------
        private void cbFVGS_CheckedChanged(object sender, EventArgs e)
        {
            //FVGS
            if (cbFVGS.Checked == true)
            {
                if (cbAll.Checked == false)
                {
                    Exportliste = enumFibuExportListe.FVGS.ToString();
                    cbRG.Checked = false;
                    cbGS.Checked = false;
                    cbGSSU.Checked = false;
                }
            }
        }
    }
}
