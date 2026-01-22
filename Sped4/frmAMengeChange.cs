using LVS;
using System;

namespace Sped4
{
    public partial class frmAMengeChange : frmTEMPLATE
    {
        internal clsArtikel ArtikelAlt;
        internal clsArtikel ArtikelNeu;
        //public ctrAuslagerung ctrAuslagerung;
        public frmAuftrag_Splitting frmAuftragSplitting;
        internal Int32 iRowIndex = 0;
        public bool bo_AuftragsSplitting = false;
        public decimal ArtikelID = 0;
        public Int32 RowIndex;


        /***********************************************************************
        *                Methoden / Procedure
        * ********************************************************************/
        ///<summary>frmAMengeChange/ frmAMengeChange</summary>
        ///<remarks></remarks> 
        public frmAMengeChange()
        {
            InitializeComponent();
        }
        ///<summary>frmAMengeChange/ frmAMengeChange</summary>
        ///<remarks></remarks> 
        private void frmAMengeChange_Load(object sender, EventArgs e)
        {
            //Artikelklasse zuweisen
            if (this.frmAuftragSplitting != null)
            {
                this.ArtikelAlt = this.frmAuftragSplitting.Tour.Auftrag.AuftragPos.Artikel.Copy();
                this.ArtikelNeu = this.ArtikelAlt.Copy();
                ResetDatenForArtikelNeu();
                SetDatenToForm();
                SetDatenValueCtrEnable();
                InitRackBar("Brutto");
            }
        }
        ///<summary>frmAMengeChange/ ResetDatenForArtikelNeu</summary>
        ///<remarks></remarks> 
        private void ResetDatenForArtikelNeu()
        {
            this.ArtikelNeu.ID = 0;
            this.ArtikelNeu.Anzahl = 0;
            this.ArtikelNeu.Netto = 0;
            this.ArtikelNeu.Brutto = 0;
        }
        ///<summary>frmAMengeChange/ tsbtnClose_Click</summary>
        ///<remarks></remarks> 
        private void tsbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///<summary>frmAMengeChange/ InitRackBar</summary>
        ///<remarks></remarks>
        private void InitRackBar(string myElement)
        {
            decimal decQuantityFor1Percent = this.ArtikelAlt.Netto / 100;

            this.tbarArtChange.LabelStyle = Telerik.WinControls.UI.TrackBarLabelStyle.TopLeft;
            this.tbarArtChange.ShowButtons = true;
            this.tbarArtChange.TrackBarMode = Telerik.WinControls.UI.TrackBarRangeMode.SingleThumb;

            switch (myElement)
            {
                case "Anzahl":
                    //this.tbarArtChange.Minimum = 0;
                    //this.tbarArtChange.Maximum = this.ArtikelAlt.Anzahl;
                    //this.tbarArtChange.Value = 0;
                    //this.nudAnzahlNeu.Minimum = (decimal)this.tbarArtChange.Minimum;
                    //this.nudAnzahlNeu.Maximum = (decimal)this.tbarArtChange.Maximum;
                    break;
                case "Netto":
                    //this.tbarArtChange.Minimum = 0;
                    //this.tbarArtChange.Maximum = 100;
                    //this.tbarArtChange.Value = 0;


                    //nudNettoNeu.Minimum = 0;
                    //nudNettoNeu.Maximum = this.ArtikelAlt.Brutto;
                    //nudNettoNeu.Value = 0;


                    //if (nudNettoNeu.Value >= nudBruttoNeu.Value)
                    //{
                    //    nudBruttoNeu.Minimum = this.nudNettoNeu.Value;
                    //    nudBruttoNeu.Maximum = this.ArtikelAlt.Brutto;
                    //    nudBruttoNeu.Value = this.nudNettoNeu.Value;
                    //}
                    //else
                    //{
                    //    nudBruttoNeu.Minimum = this.nudNettoNeu.Value; ;
                    //    nudBruttoNeu.Maximum = this.ArtikelAlt.Brutto;
                    //    nudBruttoNeu.Value = this.nudNettoNeu.Value; ;
                    //}
                    break;

                case "Brutto":
                    this.tbarArtChange.Minimum = 0;
                    this.tbarArtChange.Maximum = 100;
                    this.tbarArtChange.Value = 0;

                    this.nudBruttoNeu.Minimum = 0;
                    this.nudBruttoNeu.Maximum = this.ArtikelAlt.Brutto;
                    this.nudBruttoNeu.Value = 0;

                    this.nudNettoNeu.Minimum = 0;
                    this.nudNettoNeu.Maximum = this.ArtikelAlt.Brutto;
                    this.nudNettoNeu.Value = 0;

                    //if (this.nudNettoNeu.Value >= this.nudBruttoNeu.Value)
                    //{
                    //    this.tbarArtChange.Minimum = (float)(nudNettoNeu.Value / decQuantityFor1Percent);
                    //    this.tbarArtChange.Value = (float)(nudNettoNeu.Value / decQuantityFor1Percent);
                    //}
                    //else
                    //{
                    //    this.tbarArtChange.Value = 0;
                    //}
                    break;

            }
        }
        ///<summary>frmAMengeChange/ tbarArtChange_ValueChanged</summary>
        ///<remarks></remarks> 
        private void tbarArtChange_ValueChanged(object sender, EventArgs e)
        {
            decimal decQuantityFor1Percent = 0;
            //if (cbAnzahl.Checked)
            //{
            //    nudAnzahlNeu.Value = (decimal)tbarArtChange.Value;
            //}
            //else if (cbNetto.Checked)
            //{
            //    decQuantityFor1Percent = this.ArtikelAlt.Netto / 100;
            //    this.nudNettoNeu.Value = ((decimal)tbarArtChange.Value * decQuantityFor1Percent);
            //    //check ob Brutto noch kleiner
            //    if (nudNettoNeu.Value >= nudBruttoNeu.Value)
            //    {
            //        this.nudBruttoNeu.Value = this.nudNettoNeu.Value;
            //        this.nudBruttoNeu.Minimum = this.nudNettoNeu.Value;
            //    }
            //}
            //else if (cbBrutto.Checked)
            //{
            decQuantityFor1Percent = this.ArtikelAlt.Brutto / 100;
            //if ((decimal)tbarArtChange.Minimum < (nudNettoNeu.Value / decQuantityFor1Percent))
            //{
            //    this.tbarArtChange.Value = (float)(nudNettoNeu.Value / decQuantityFor1Percent);
            //    this.tbarArtChange.Minimum = (float)(nudNettoNeu.Value / decQuantityFor1Percent);
            //}
            //else
            //{
            decimal decTmp = ((decimal)tbarArtChange.Value * decQuantityFor1Percent);
            if (decTmp < (decimal)this.tbarArtChange.Minimum)
            {
                decTmp = (decimal)this.tbarArtChange.Minimum;
            }
            this.nudBruttoNeu.Value = decTmp;
            this.nudNettoNeu.Value = this.nudBruttoNeu.Value;
            //}
            //}
        }
        ///<summary>frmAMengeChange/ cbAnzahl_CheckedChanged</summary>
        ///<remarks></remarks> 
        private void cbAnzahl_CheckedChanged(object sender, EventArgs e)
        {
            //cbAnzahl.Checked = (!cbAnzahl.Checked);
            //cbBrutto.Checked = false;
            //cbNetto.Checked = false;
            //if (cbAnzahl.Checked)
            //{
            //    InitRackBar("Anzahl");
            //    SetDatenValueCtrEnable();
            //}
        }
        ///<summary>frmAMengeChange/ cbNetto_CheckedChanged</summary>
        ///<remarks></remarks> 
        private void cbNetto_CheckedChanged(object sender, EventArgs e)
        {
            ////cbNetto.Checked = (!cbNetto.Checked);
            //cbBrutto.Checked = false;
            //cbAnzahl.Checked = false;
            //if (cbNetto.Checked)
            //{
            //    InitRackBar("Netto");
            //    SetDatenValueCtrEnable();
            //}
        }
        ///<summary>frmAMengeChange/ cbBrutto_CheckedChanged</summary>
        ///<remarks></remarks> 
        private void cbBrutto_CheckedChanged(object sender, EventArgs e)
        {
            //cbBrutto.Checked = (!cbBrutto.Checked);
            cbAnzahl.Checked = false;
            cbNetto.Checked = false;
            if (cbBrutto.Checked)
            {
                InitRackBar("Brutto");
                SetDatenValueCtrEnable();
            }
        }
        ///<summary>frmAMengeChange/ SetDatenToForm</summary>
        ///<remarks></remarks> 
        private void SetDatenValueCtrEnable()
        {
            //nudAnzahlNeu.Enabled = cbAnzahl.Checked;
            //nudNettoNeu.Enabled = cbNetto.Checked;
            //nudBruttoNeu.Enabled = cbBrutto.Checked;
        }
        ///<summary>frmAMengeChange/ SetDatenToForm</summary>
        ///<remarks></remarks> 
        private void SetDatenToForm()
        {
            if (this.ArtikelAlt != null)
            {
                //nicht editierbare Artikeldaten
                tbArtikelID.Text = ArtikelAlt.ID.ToString();
                tbWerksnummer.Text = ArtikelAlt.Werksnummer;
                tbProduktionsnummer.Text = ArtikelAlt.Produktionsnummer;
                tbDicke.Text = Functions.FormatDecimal(ArtikelAlt.Dicke);
                tbBreite.Text = Functions.FormatDecimal(ArtikelAlt.Breite);
                tbLaenge.Text = Functions.FormatDecimal(ArtikelAlt.Laenge);

                //editierbare Werte
                //tbAnzahlAlt.Text = ArtikelAlt.Anzahl.ToString();
                //tbNettoAlt.Text = Functions.FormatDecimal(ArtikelAlt.Netto);
                tbBruttoAlt.Text = Functions.FormatDecimal(ArtikelAlt.Brutto);

                //neuen Werte auf null
                //nudAnzahlNeu.Value = (decimal)ArtikelNeu.Anzahl;
                //nudNettoNeu.Value = ArtikelNeu.Netto;
                nudBruttoNeu.Value = ArtikelNeu.Brutto;

                //cbAnzahl.Checked = false;
                //cbNetto.Checked = false;
                cbBrutto.Checked = false;
            }
        }
        ///<summary>frmAMengeChange/ tsbSpeichern_Click</summary>
        ///<remarks></remarks> 
        private void tsbSpeichern_Click(object sender, EventArgs e)
        {
            //Artikel alt -> Gewichte und Anzahl ermitteln
            //Anzahl
            //if (ArtikelAlt.Anzahl > 0)
            //{
            //    this.ArtikelAlt.Anzahl = this.ArtikelAlt.Anzahl - (Int32)nudAnzahlNeu.Value;
            //}
            ////Netto
            //if (ArtikelAlt.Netto > 0)
            //{
            //    this.ArtikelAlt.Netto = this.ArtikelAlt.Netto - nudNettoNeu.Value;
            //}
            //Brutto
            if (ArtikelAlt.Brutto > 0)
            {
                this.ArtikelAlt.Brutto = this.ArtikelAlt.Brutto - nudBruttoNeu.Value;
                this.ArtikelAlt.Netto = this.ArtikelAlt.Netto - nudNettoNeu.Value;
                this.ArtikelAlt.Anzahl = this.ArtikelAlt.Anzahl - (Int32)nudAnzahlNeu.Value;
            }
            //gemGewicht
            this.ArtikelAlt.gemGewicht = this.ArtikelAlt.Brutto;

            //Artikel neu -> Gewichte und Anzahl ermitteln
            this.ArtikelNeu.Anzahl = (Int32)nudAnzahlNeu.Value;
            this.ArtikelNeu.Netto = nudBruttoNeu.Value; ;
            this.ArtikelNeu.Brutto = nudBruttoNeu.Value;
            this.ArtikelNeu.gemGewicht = this.ArtikelNeu.Brutto;
            this.ArtikelNeu.interneInfo = "Artikelsplitt aus ID:" + this.ArtikelAlt.ID.ToString();

            //Artikelsplitt durchführen
            this.ArtikelAlt.DoArtikelSplitt(ref this.ArtikelNeu);
            //Artikelliste in Auftragsplitting neu laden
            this.frmAuftragSplitting.InitDGVArtikel();
            //Frm schliessen
            this.Close();
        }
        ///<summary>frmAMengeChange/ nudNettoNeu_ValueChanged</summary>
        ///<remarks></remarks> 
        private void nudNettoNeu_ValueChanged(object sender, EventArgs e)
        {
            if (nudNettoNeu.Value > nudBruttoNeu.Value)
            {
                nudNettoNeu.Value = nudBruttoNeu.Value;
            }
        }





    }
}
