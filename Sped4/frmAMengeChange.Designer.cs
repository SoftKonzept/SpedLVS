namespace Sped4
{
  partial class frmAMengeChange
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbSpeichern = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.tbNettoAlt = new System.Windows.Forms.TextBox();
            this.tbAnzahlAlt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbBruttoAlt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lEinheit = new System.Windows.Forms.Label();
            this.gbGeaMengen = new System.Windows.Forms.GroupBox();
            this.nudAnzahlNeu = new System.Windows.Forms.NumericUpDown();
            this.nudBruttoNeu = new System.Windows.Forms.NumericUpDown();
            this.nudNettoNeu = new System.Windows.Forms.NumericUpDown();
            this.cbBrutto = new System.Windows.Forms.CheckBox();
            this.cbNetto = new System.Windows.Forms.CheckBox();
            this.cbAnzahl = new System.Windows.Forms.CheckBox();
            this.tbarArtChange = new Telerik.WinControls.UI.RadTrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panArtikel = new System.Windows.Forms.Panel();
            this.tbLaenge = new System.Windows.Forms.TextBox();
            this.tbBreite = new System.Windows.Forms.TextBox();
            this.tbProduktionsnummer = new System.Windows.Forms.TextBox();
            this.tbDicke = new System.Windows.Forms.TextBox();
            this.tbWerksnummer = new System.Windows.Forms.TextBox();
            this.tbArtikelID = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.afToolStrip1.SuspendLayout();
            this.gbGeaMengen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnzahlNeu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBruttoNeu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNettoNeu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarArtChange)).BeginInit();
            this.panArtikel.SuspendLayout();
            this.SuspendLayout();
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSpeichern,
            this.tsbtnClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(466, 25);
            this.afToolStrip1.TabIndex = 10;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbSpeichern
            // 
            this.tsbSpeichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSpeichern.Image = global::Sped4.Properties.Resources.check;
            this.tsbSpeichern.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSpeichern.Name = "tsbSpeichern";
            this.tsbSpeichern.Size = new System.Drawing.Size(23, 22);
            this.tsbSpeichern.Text = "Speichern der Userdaten";
            this.tsbSpeichern.Click += new System.EventHandler(this.tsbSpeichern_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Userverwaltung schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // tbNettoAlt
            // 
            this.tbNettoAlt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbNettoAlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNettoAlt.Location = new System.Drawing.Point(138, 52);
            this.tbNettoAlt.Name = "tbNettoAlt";
            this.tbNettoAlt.ReadOnly = true;
            this.tbNettoAlt.Size = new System.Drawing.Size(81, 20);
            this.tbNettoAlt.TabIndex = 81;
            this.tbNettoAlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbAnzahlAlt
            // 
            this.tbAnzahlAlt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAnzahlAlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAnzahlAlt.Location = new System.Drawing.Point(138, 27);
            this.tbAnzahlAlt.Name = "tbAnzahlAlt";
            this.tbAnzahlAlt.ReadOnly = true;
            this.tbAnzahlAlt.Size = new System.Drawing.Size(81, 20);
            this.tbAnzahlAlt.TabIndex = 80;
            this.tbAnzahlAlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(225, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "[kg]";
            // 
            // tbBruttoAlt
            // 
            this.tbBruttoAlt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbBruttoAlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBruttoAlt.Location = new System.Drawing.Point(138, 85);
            this.tbBruttoAlt.Name = "tbBruttoAlt";
            this.tbBruttoAlt.ReadOnly = true;
            this.tbBruttoAlt.Size = new System.Drawing.Size(81, 20);
            this.tbBruttoAlt.TabIndex = 82;
            this.tbBruttoAlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "[kg]";
            // 
            // lEinheit
            // 
            this.lEinheit.AutoSize = true;
            this.lEinheit.Location = new System.Drawing.Point(224, 31);
            this.lEinheit.Name = "lEinheit";
            this.lEinheit.Size = new System.Drawing.Size(32, 13);
            this.lEinheit.TabIndex = 14;
            this.lEinheit.Text = "[Stk.]";
            // 
            // gbGeaMengen
            // 
            this.gbGeaMengen.Controls.Add(this.nudAnzahlNeu);
            this.gbGeaMengen.Controls.Add(this.nudBruttoNeu);
            this.gbGeaMengen.Controls.Add(this.nudNettoNeu);
            this.gbGeaMengen.Controls.Add(this.cbBrutto);
            this.gbGeaMengen.Controls.Add(this.cbNetto);
            this.gbGeaMengen.Controls.Add(this.cbAnzahl);
            this.gbGeaMengen.Controls.Add(this.tbarArtChange);
            this.gbGeaMengen.Controls.Add(this.label8);
            this.gbGeaMengen.Controls.Add(this.label10);
            this.gbGeaMengen.Controls.Add(this.tbBruttoAlt);
            this.gbGeaMengen.Controls.Add(this.label2);
            this.gbGeaMengen.Controls.Add(this.label4);
            this.gbGeaMengen.Controls.Add(this.tbAnzahlAlt);
            this.gbGeaMengen.Controls.Add(this.lEinheit);
            this.gbGeaMengen.Controls.Add(this.tbNettoAlt);
            this.gbGeaMengen.Controls.Add(this.label6);
            this.gbGeaMengen.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbGeaMengen.Location = new System.Drawing.Point(12, 188);
            this.gbGeaMengen.Name = "gbGeaMengen";
            this.gbGeaMengen.Size = new System.Drawing.Size(439, 195);
            this.gbGeaMengen.TabIndex = 14;
            this.gbGeaMengen.TabStop = false;
            this.gbGeaMengen.Text = "editierbare Artikeldaten";
            // 
            // nudAnzahlNeu
            // 
            this.nudAnzahlNeu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudAnzahlNeu.Location = new System.Drawing.Point(255, 29);
            this.nudAnzahlNeu.Name = "nudAnzahlNeu";
            this.nudAnzahlNeu.Size = new System.Drawing.Size(108, 20);
            this.nudAnzahlNeu.TabIndex = 89;
            this.nudAnzahlNeu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudBruttoNeu
            // 
            this.nudBruttoNeu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudBruttoNeu.DecimalPlaces = 2;
            this.nudBruttoNeu.Location = new System.Drawing.Point(255, 86);
            this.nudBruttoNeu.Name = "nudBruttoNeu";
            this.nudBruttoNeu.Size = new System.Drawing.Size(108, 20);
            this.nudBruttoNeu.TabIndex = 88;
            this.nudBruttoNeu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nudNettoNeu
            // 
            this.nudNettoNeu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudNettoNeu.DecimalPlaces = 2;
            this.nudNettoNeu.Location = new System.Drawing.Point(255, 53);
            this.nudNettoNeu.Name = "nudNettoNeu";
            this.nudNettoNeu.Size = new System.Drawing.Size(108, 20);
            this.nudNettoNeu.TabIndex = 87;
            this.nudNettoNeu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudNettoNeu.ValueChanged += new System.EventHandler(this.nudNettoNeu_ValueChanged);
            // 
            // cbBrutto
            // 
            this.cbBrutto.AutoSize = true;
            this.cbBrutto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBrutto.Location = new System.Drawing.Point(29, 86);
            this.cbBrutto.Name = "cbBrutto";
            this.cbBrutto.Size = new System.Drawing.Size(91, 17);
            this.cbBrutto.TabIndex = 86;
            this.cbBrutto.Text = "Bruttogewicht:";
            this.cbBrutto.UseVisualStyleBackColor = true;
            this.cbBrutto.CheckedChanged += new System.EventHandler(this.cbBrutto_CheckedChanged);
            // 
            // cbNetto
            // 
            this.cbNetto.AutoSize = true;
            this.cbNetto.Enabled = false;
            this.cbNetto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNetto.Location = new System.Drawing.Point(29, 51);
            this.cbNetto.Name = "cbNetto";
            this.cbNetto.Size = new System.Drawing.Size(89, 17);
            this.cbNetto.TabIndex = 85;
            this.cbNetto.Text = "Nettogewicht:";
            this.cbNetto.UseVisualStyleBackColor = true;
            this.cbNetto.CheckedChanged += new System.EventHandler(this.cbNetto_CheckedChanged);
            // 
            // cbAnzahl
            // 
            this.cbAnzahl.AutoSize = true;
            this.cbAnzahl.Enabled = false;
            this.cbAnzahl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAnzahl.Location = new System.Drawing.Point(29, 28);
            this.cbAnzahl.Name = "cbAnzahl";
            this.cbAnzahl.Size = new System.Drawing.Size(55, 17);
            this.cbAnzahl.TabIndex = 84;
            this.cbAnzahl.Text = "Anzahl";
            this.cbAnzahl.UseVisualStyleBackColor = true;
            this.cbAnzahl.CheckedChanged += new System.EventHandler(this.cbAnzahl_CheckedChanged);
            // 
            // tbarArtChange
            // 
            this.tbarArtChange.LabelStyle = Telerik.WinControls.UI.TrackBarLabelStyle.TopLeft;
            this.tbarArtChange.LargeTickFrequency = 10;
            this.tbarArtChange.Location = new System.Drawing.Point(20, 126);
            this.tbarArtChange.Maximum = 100F;
            this.tbarArtChange.Name = "tbarArtChange";
            this.tbarArtChange.Size = new System.Drawing.Size(393, 55);
            this.tbarArtChange.TabIndex = 83;
            this.tbarArtChange.Text = "radTrackBar1";
            this.tbarArtChange.ValueChanged += new System.EventHandler(this.tbarArtChange_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(365, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "[kg]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(365, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "[kg]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(365, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "[Stk.]";
            // 
            // panArtikel
            // 
            this.panArtikel.Controls.Add(this.tbLaenge);
            this.panArtikel.Controls.Add(this.tbBreite);
            this.panArtikel.Controls.Add(this.tbProduktionsnummer);
            this.panArtikel.Controls.Add(this.tbDicke);
            this.panArtikel.Controls.Add(this.tbWerksnummer);
            this.panArtikel.Controls.Add(this.tbArtikelID);
            this.panArtikel.Controls.Add(this.label17);
            this.panArtikel.Controls.Add(this.label16);
            this.panArtikel.Controls.Add(this.label15);
            this.panArtikel.Controls.Add(this.label14);
            this.panArtikel.Controls.Add(this.label13);
            this.panArtikel.Controls.Add(this.label12);
            this.panArtikel.Location = new System.Drawing.Point(12, 28);
            this.panArtikel.Name = "panArtikel";
            this.panArtikel.Size = new System.Drawing.Size(439, 154);
            this.panArtikel.TabIndex = 16;
            // 
            // tbLaenge
            // 
            this.tbLaenge.AcceptsTab = true;
            this.tbLaenge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLaenge.Location = new System.Drawing.Point(124, 125);
            this.tbLaenge.Name = "tbLaenge";
            this.tbLaenge.ReadOnly = true;
            this.tbLaenge.Size = new System.Drawing.Size(131, 20);
            this.tbLaenge.TabIndex = 25;
            this.tbLaenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbBreite
            // 
            this.tbBreite.AcceptsTab = true;
            this.tbBreite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBreite.Location = new System.Drawing.Point(124, 102);
            this.tbBreite.Name = "tbBreite";
            this.tbBreite.ReadOnly = true;
            this.tbBreite.Size = new System.Drawing.Size(131, 20);
            this.tbBreite.TabIndex = 24;
            this.tbBreite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbProduktionsnummer
            // 
            this.tbProduktionsnummer.AcceptsTab = true;
            this.tbProduktionsnummer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbProduktionsnummer.Location = new System.Drawing.Point(124, 55);
            this.tbProduktionsnummer.Name = "tbProduktionsnummer";
            this.tbProduktionsnummer.ReadOnly = true;
            this.tbProduktionsnummer.Size = new System.Drawing.Size(131, 20);
            this.tbProduktionsnummer.TabIndex = 23;
            this.tbProduktionsnummer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbDicke
            // 
            this.tbDicke.AcceptsTab = true;
            this.tbDicke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDicke.Location = new System.Drawing.Point(124, 78);
            this.tbDicke.Name = "tbDicke";
            this.tbDicke.ReadOnly = true;
            this.tbDicke.Size = new System.Drawing.Size(131, 20);
            this.tbDicke.TabIndex = 22;
            this.tbDicke.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbWerksnummer
            // 
            this.tbWerksnummer.AcceptsTab = true;
            this.tbWerksnummer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWerksnummer.Location = new System.Drawing.Point(124, 33);
            this.tbWerksnummer.Name = "tbWerksnummer";
            this.tbWerksnummer.ReadOnly = true;
            this.tbWerksnummer.Size = new System.Drawing.Size(131, 20);
            this.tbWerksnummer.TabIndex = 21;
            this.tbWerksnummer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbArtikelID
            // 
            this.tbArtikelID.AcceptsTab = true;
            this.tbArtikelID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbArtikelID.Location = new System.Drawing.Point(124, 10);
            this.tbArtikelID.Name = "tbArtikelID";
            this.tbArtikelID.ReadOnly = true;
            this.tbArtikelID.Size = new System.Drawing.Size(131, 20);
            this.tbArtikelID.TabIndex = 20;
            this.tbArtikelID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.DarkBlue;
            this.label17.Location = new System.Drawing.Point(17, 104);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "Breite";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.DarkBlue;
            this.label16.Location = new System.Drawing.Point(17, 127);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "Länge";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.DarkBlue;
            this.label15.Location = new System.Drawing.Point(17, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 17;
            this.label15.Text = "Dicke";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.DarkBlue;
            this.label14.Location = new System.Drawing.Point(17, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Produktionsnummer:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.DarkBlue;
            this.label13.Location = new System.Drawing.Point(17, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Werksnummer:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.DarkBlue;
            this.label12.Location = new System.Drawing.Point(17, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Artikel ID:";
            // 
            // frmAMengeChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 393);
            this.Controls.Add(this.panArtikel);
            this.Controls.Add(this.gbGeaMengen);
            this.Controls.Add(this.afToolStrip1);
            this.Name = "frmAMengeChange";
            this.Text = "Auslagerungsmengen ändern";
            this.Load += new System.EventHandler(this.frmAMengeChange_Load);
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.gbGeaMengen.ResumeLayout(false);
            this.gbGeaMengen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnzahlNeu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBruttoNeu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNettoNeu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarArtChange)).EndInit();
            this.panArtikel.ResumeLayout(false);
            this.panArtikel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbSpeichern;
    private System.Windows.Forms.ToolStripButton tsbtnClose;
    private System.Windows.Forms.TextBox tbNettoAlt;
    private System.Windows.Forms.TextBox tbAnzahlAlt;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lEinheit;
    private System.Windows.Forms.GroupBox gbGeaMengen;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox tbBruttoAlt;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.CheckBox cbBrutto;
    private System.Windows.Forms.CheckBox cbNetto;
    private System.Windows.Forms.CheckBox cbAnzahl;
    private Telerik.WinControls.UI.RadTrackBar tbarArtChange;
    private System.Windows.Forms.Panel panArtikel;
    private System.Windows.Forms.TextBox tbLaenge;
    private System.Windows.Forms.TextBox tbBreite;
    private System.Windows.Forms.TextBox tbProduktionsnummer;
    private System.Windows.Forms.TextBox tbDicke;
    private System.Windows.Forms.TextBox tbWerksnummer;
    private System.Windows.Forms.TextBox tbArtikelID;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.NumericUpDown nudAnzahlNeu;
    private System.Windows.Forms.NumericUpDown nudBruttoNeu;
    private System.Windows.Forms.NumericUpDown nudNettoNeu;
  }
}