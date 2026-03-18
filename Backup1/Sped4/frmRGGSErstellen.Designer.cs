namespace Sped4
{
  partial class frmRGGSErstellen
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
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
         System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
         this.gbDocAdressat = new System.Windows.Forms.GroupBox();
         this.tbAuftraggeber = new System.Windows.Forms.TextBox();
         this.btnSearchA = new System.Windows.Forms.Button();
         this.cbDocArt = new System.Windows.Forms.CheckBox();
         this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
         this.tsbtnSavePrint = new System.Windows.Forms.ToolStripButton();
         this.tsbClose = new System.Windows.Forms.ToolStripButton();
         this.gbDocText = new System.Windows.Forms.GroupBox();
         this.dgv = new System.Windows.Forms.DataGridView();
         this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
         this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.Betrag = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
         this.tsbNeu = new System.Windows.Forms.ToolStripButton();
         this.label1 = new System.Windows.Forms.Label();
         this.gbRGAnzeige = new System.Windows.Forms.GroupBox();
         this.nudRGMwSt = new System.Windows.Forms.NumericUpDown();
         this.label9 = new System.Windows.Forms.Label();
         this.tbRGBrutto = new System.Windows.Forms.TextBox();
         this.tbRGMwStBetrag = new System.Windows.Forms.TextBox();
         this.tbRGNetto = new System.Windows.Forms.TextBox();
         this.label8 = new System.Windows.Forms.Label();
         this.lBrutto = new System.Windows.Forms.Label();
         this.lNetto = new System.Windows.Forms.Label();
         this.gbResourcen = new System.Windows.Forms.GroupBox();
         this.tbSU = new System.Windows.Forms.TextBox();
         this.btnSU = new System.Windows.Forms.Button();
         this.comboAuflieger = new System.Windows.Forms.ComboBox();
         this.comboZM = new System.Windows.Forms.ComboBox();
         this.lAuflieger = new System.Windows.Forms.Label();
         this.lZM = new System.Windows.Forms.Label();
         this.cbResourcen = new System.Windows.Forms.CheckBox();
         this.gbAuftrag = new System.Windows.Forms.GroupBox();
         this.tbAuftragsPos = new System.Windows.Forms.TextBox();
         this.tbAuftrag = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.cbAuftrag = new System.Windows.Forms.CheckBox();
         this.label4 = new System.Windows.Forms.Label();
         this.nudDruckanzahl = new System.Windows.Forms.NumericUpDown();
         this.gbDocAdressat.SuspendLayout();
         this.afToolStrip1.SuspendLayout();
         this.gbDocText.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
         this.afToolStrip2.SuspendLayout();
         this.gbRGAnzeige.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.nudRGMwSt)).BeginInit();
         this.gbResourcen.SuspendLayout();
         this.gbAuftrag.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.nudDruckanzahl)).BeginInit();
         this.SuspendLayout();
         // 
         // gbDocAdressat
         // 
         this.gbDocAdressat.Controls.Add(this.tbAuftraggeber);
         this.gbDocAdressat.Controls.Add(this.btnSearchA);
         this.gbDocAdressat.ForeColor = System.Drawing.Color.DarkBlue;
         this.gbDocAdressat.Location = new System.Drawing.Point(12, 52);
         this.gbDocAdressat.Name = "gbDocAdressat";
         this.gbDocAdressat.Size = new System.Drawing.Size(752, 70);
         this.gbDocAdressat.TabIndex = 0;
         this.gbDocAdressat.TabStop = false;
         this.gbDocAdressat.Text = "Rechnungsempfänger";
         // 
         // tbAuftraggeber
         // 
         this.tbAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
         this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbAuftraggeber.Location = new System.Drawing.Point(133, 32);
         this.tbAuftraggeber.Name = "tbAuftraggeber";
         this.tbAuftraggeber.ReadOnly = true;
         this.tbAuftraggeber.Size = new System.Drawing.Size(601, 20);
         this.tbAuftraggeber.TabIndex = 6;
         // 
         // btnSearchA
         // 
         this.btnSearchA.Location = new System.Drawing.Point(19, 29);
         this.btnSearchA.Name = "btnSearchA";
         this.btnSearchA.Size = new System.Drawing.Size(91, 22);
         this.btnSearchA.TabIndex = 5;
         this.btnSearchA.Text = "Adressat";
         this.btnSearchA.UseVisualStyleBackColor = true;
         this.btnSearchA.Click += new System.EventHandler(this.btnSearchA_Click);
         // 
         // cbDocArt
         // 
         this.cbDocArt.AutoSize = true;
         this.cbDocArt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.cbDocArt.ForeColor = System.Drawing.Color.DarkBlue;
         this.cbDocArt.Location = new System.Drawing.Point(12, 29);
         this.cbDocArt.Name = "cbDocArt";
         this.cbDocArt.Size = new System.Drawing.Size(110, 17);
         this.cbDocArt.TabIndex = 1;
         this.cbDocArt.Text = "Gutschrift erstellen";
         this.cbDocArt.UseVisualStyleBackColor = true;
         this.cbDocArt.CheckedChanged += new System.EventHandler(this.cbDocArt_CheckedChanged);
         // 
         // afToolStrip1
         // 
         this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSavePrint,
            this.tsbClose});
         this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
         this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
         this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
         this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
         this.afToolStrip1.myUnderlined = true;
         this.afToolStrip1.Name = "afToolStrip1";
         this.afToolStrip1.Size = new System.Drawing.Size(770, 25);
         this.afToolStrip1.TabIndex = 126;
         this.afToolStrip1.Text = "afToolStrip1";
         // 
         // tsbtnSavePrint
         // 
         this.tsbtnSavePrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbtnSavePrint.Image = global::Sped4.Properties.Resources.check;
         this.tsbtnSavePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbtnSavePrint.Name = "tsbtnSavePrint";
         this.tsbtnSavePrint.Size = new System.Drawing.Size(23, 22);
         this.tsbtnSavePrint.Text = "Rechnung / Gutschrift speichern und drucken";
         this.tsbtnSavePrint.Click += new System.EventHandler(this.tsbtnSavePrint_Click);
         // 
         // tsbClose
         // 
         this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
         this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbClose.Name = "tsbClose";
         this.tsbClose.Size = new System.Drawing.Size(23, 22);
         this.tsbClose.Text = "schliessen";
         this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
         // 
         // gbDocText
         // 
         this.gbDocText.Controls.Add(this.dgv);
         this.gbDocText.Controls.Add(this.afToolStrip2);
         this.gbDocText.ForeColor = System.Drawing.Color.DarkBlue;
         this.gbDocText.Location = new System.Drawing.Point(12, 137);
         this.gbDocText.Name = "gbDocText";
         this.gbDocText.Size = new System.Drawing.Size(752, 164);
         this.gbDocText.TabIndex = 127;
         this.gbDocText.TabStop = false;
         this.gbDocText.Text = "Rechnungstext";
         // 
         // dgv
         // 
         this.dgv.AllowUserToAddRows = false;
         this.dgv.AllowUserToDeleteRows = false;
         this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Delete,
            this.Text,
            this.Betrag});
         this.dgv.Dock = System.Windows.Forms.DockStyle.Top;
         this.dgv.Location = new System.Drawing.Point(3, 41);
         this.dgv.Name = "dgv";
         dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
         dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
         dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
         dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
         dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
         this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
         this.dgv.RowHeadersVisible = false;
         this.dgv.Size = new System.Drawing.Size(746, 117);
         this.dgv.TabIndex = 6;
         this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
         this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
         this.dgv.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellValueChanged);
         // 
         // Delete
         // 
         this.Delete.HeaderText = "";
         this.Delete.Name = "Delete";
         this.Delete.Width = 20;
         // 
         // Text
         // 
         this.Text.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
         dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
         this.Text.DefaultCellStyle = dataGridViewCellStyle1;
         this.Text.HeaderText = "Text";
         this.Text.Name = "Text";
         // 
         // Betrag
         // 
         dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
         dataGridViewCellStyle2.Format = "C2";
         dataGridViewCellStyle2.NullValue = "0,00";
         this.Betrag.DefaultCellStyle = dataGridViewCellStyle2;
         this.Betrag.HeaderText = "Betrag €";
         this.Betrag.Name = "Betrag";
         // 
         // afToolStrip2
         // 
         this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNeu});
         this.afToolStrip2.Location = new System.Drawing.Point(3, 16);
         this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
         this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
         this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
         this.afToolStrip2.myUnderlined = true;
         this.afToolStrip2.Name = "afToolStrip2";
         this.afToolStrip2.Size = new System.Drawing.Size(746, 25);
         this.afToolStrip2.TabIndex = 12;
         this.afToolStrip2.Text = "afToolStrip2";
         // 
         // tsbNeu
         // 
         this.tsbNeu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbNeu.Image = global::Sped4.Properties.Resources.add;
         this.tsbNeu.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbNeu.Name = "tsbNeu";
         this.tsbNeu.Size = new System.Drawing.Size(23, 22);
         this.tsbNeu.Text = "Neuer Artikel";
         this.tsbNeu.Click += new System.EventHandler(this.tsbNeu_Click);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Dock = System.Windows.Forms.DockStyle.Right;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.ForeColor = System.Drawing.Color.Brown;
         this.label1.Location = new System.Drawing.Point(660, 25);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(110, 20);
         this.label1.TabIndex = 129;
         this.label1.Text = "RECHNUNG";
         // 
         // gbRGAnzeige
         // 
         this.gbRGAnzeige.Controls.Add(this.nudRGMwSt);
         this.gbRGAnzeige.Controls.Add(this.label9);
         this.gbRGAnzeige.Controls.Add(this.tbRGBrutto);
         this.gbRGAnzeige.Controls.Add(this.tbRGMwStBetrag);
         this.gbRGAnzeige.Controls.Add(this.tbRGNetto);
         this.gbRGAnzeige.Controls.Add(this.label8);
         this.gbRGAnzeige.Controls.Add(this.lBrutto);
         this.gbRGAnzeige.Controls.Add(this.lNetto);
         this.gbRGAnzeige.ForeColor = System.Drawing.Color.DarkBlue;
         this.gbRGAnzeige.Location = new System.Drawing.Point(549, 307);
         this.gbRGAnzeige.Name = "gbRGAnzeige";
         this.gbRGAnzeige.Size = new System.Drawing.Size(212, 124);
         this.gbRGAnzeige.TabIndex = 130;
         this.gbRGAnzeige.TabStop = false;
         this.gbRGAnzeige.Text = "Anzeige Rechnungsbeträge";
         // 
         // nudRGMwSt
         // 
         this.nudRGMwSt.Location = new System.Drawing.Point(119, 50);
         this.nudRGMwSt.Name = "nudRGMwSt";
         this.nudRGMwSt.Size = new System.Drawing.Size(43, 20);
         this.nudRGMwSt.TabIndex = 8;
         this.nudRGMwSt.ValueChanged += new System.EventHandler(this.nudRGMwSt_ValueChanged);
         // 
         // label9
         // 
         this.label9.AutoSize = true;
         this.label9.Location = new System.Drawing.Point(16, 51);
         this.label9.Name = "label9";
         this.label9.Size = new System.Drawing.Size(69, 13);
         this.label9.TabIndex = 6;
         this.label9.Text = "MwSt-Satz %";
         // 
         // tbRGBrutto
         // 
         this.tbRGBrutto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbRGBrutto.Location = new System.Drawing.Point(119, 97);
         this.tbRGBrutto.Name = "tbRGBrutto";
         this.tbRGBrutto.ReadOnly = true;
         this.tbRGBrutto.Size = new System.Drawing.Size(88, 20);
         this.tbRGBrutto.TabIndex = 5;
         this.tbRGBrutto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // tbRGMwStBetrag
         // 
         this.tbRGMwStBetrag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbRGMwStBetrag.Location = new System.Drawing.Point(119, 77);
         this.tbRGMwStBetrag.Name = "tbRGMwStBetrag";
         this.tbRGMwStBetrag.ReadOnly = true;
         this.tbRGMwStBetrag.Size = new System.Drawing.Size(88, 20);
         this.tbRGMwStBetrag.TabIndex = 4;
         this.tbRGMwStBetrag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // tbRGNetto
         // 
         this.tbRGNetto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbRGNetto.Location = new System.Drawing.Point(119, 23);
         this.tbRGNetto.Name = "tbRGNetto";
         this.tbRGNetto.ReadOnly = true;
         this.tbRGNetto.Size = new System.Drawing.Size(88, 20);
         this.tbRGNetto.TabIndex = 3;
         this.tbRGNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // label8
         // 
         this.label8.AutoSize = true;
         this.label8.Location = new System.Drawing.Point(16, 77);
         this.label8.Name = "label8";
         this.label8.Size = new System.Drawing.Size(46, 13);
         this.label8.TabIndex = 2;
         this.label8.Text = "MwSt. €";
         // 
         // lBrutto
         // 
         this.lBrutto.AutoSize = true;
         this.lBrutto.Location = new System.Drawing.Point(16, 99);
         this.lBrutto.Name = "lBrutto";
         this.lBrutto.Size = new System.Drawing.Size(97, 13);
         this.lBrutto.TabIndex = 1;
         this.lBrutto.Text = "Rechnung Brutto €";
         // 
         // lNetto
         // 
         this.lNetto.AutoSize = true;
         this.lNetto.Location = new System.Drawing.Point(16, 26);
         this.lNetto.Name = "lNetto";
         this.lNetto.Size = new System.Drawing.Size(95, 13);
         this.lNetto.TabIndex = 0;
         this.lNetto.Text = "Rechnung Netto €";
         // 
         // gbResourcen
         // 
         this.gbResourcen.Controls.Add(this.tbSU);
         this.gbResourcen.Controls.Add(this.btnSU);
         this.gbResourcen.Controls.Add(this.comboAuflieger);
         this.gbResourcen.Controls.Add(this.comboZM);
         this.gbResourcen.Controls.Add(this.lAuflieger);
         this.gbResourcen.Controls.Add(this.lZM);
         this.gbResourcen.ForeColor = System.Drawing.Color.DarkBlue;
         this.gbResourcen.Location = new System.Drawing.Point(208, 310);
         this.gbResourcen.Name = "gbResourcen";
         this.gbResourcen.Size = new System.Drawing.Size(325, 120);
         this.gbResourcen.TabIndex = 131;
         this.gbResourcen.TabStop = false;
         this.gbResourcen.Text = "Ressourcenzuweisung";
         // 
         // tbSU
         // 
         this.tbSU.BackColor = System.Drawing.Color.WhiteSmoke;
         this.tbSU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbSU.Location = new System.Drawing.Point(119, 85);
         this.tbSU.Name = "tbSU";
         this.tbSU.ReadOnly = true;
         this.tbSU.Size = new System.Drawing.Size(200, 20);
         this.tbSU.TabIndex = 7;
         // 
         // btnSU
         // 
         this.btnSU.Location = new System.Drawing.Point(6, 85);
         this.btnSU.Name = "btnSU";
         this.btnSU.Size = new System.Drawing.Size(101, 22);
         this.btnSU.TabIndex = 6;
         this.btnSU.Text = "Subunternehmer";
         this.btnSU.UseVisualStyleBackColor = true;
         this.btnSU.Click += new System.EventHandler(this.btnSU_Click);
         // 
         // comboAuflieger
         // 
         this.comboAuflieger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.comboAuflieger.FormattingEnabled = true;
         this.comboAuflieger.Location = new System.Drawing.Point(119, 49);
         this.comboAuflieger.Name = "comboAuflieger";
         this.comboAuflieger.Size = new System.Drawing.Size(200, 21);
         this.comboAuflieger.TabIndex = 4;
         this.comboAuflieger.SelectedIndexChanged += new System.EventHandler(this.comboAuflieger_SelectedIndexChanged);
         // 
         // comboZM
         // 
         this.comboZM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.comboZM.FormattingEnabled = true;
         this.comboZM.Location = new System.Drawing.Point(119, 21);
         this.comboZM.Name = "comboZM";
         this.comboZM.Size = new System.Drawing.Size(200, 21);
         this.comboZM.TabIndex = 3;
         this.comboZM.SelectedIndexChanged += new System.EventHandler(this.comboZM_SelectedIndexChanged);
         // 
         // lAuflieger
         // 
         this.lAuflieger.AutoSize = true;
         this.lAuflieger.Location = new System.Drawing.Point(9, 52);
         this.lAuflieger.Name = "lAuflieger";
         this.lAuflieger.Size = new System.Drawing.Size(51, 13);
         this.lAuflieger.TabIndex = 1;
         this.lAuflieger.Text = "Auflieger:";
         // 
         // lZM
         // 
         this.lZM.AutoSize = true;
         this.lZM.Location = new System.Drawing.Point(9, 24);
         this.lZM.Name = "lZM";
         this.lZM.Size = new System.Drawing.Size(74, 13);
         this.lZM.TabIndex = 0;
         this.lZM.Text = "Zugmaschine:";
         // 
         // cbResourcen
         // 
         this.cbResourcen.AutoSize = true;
         this.cbResourcen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.cbResourcen.ForeColor = System.Drawing.Color.DarkBlue;
         this.cbResourcen.Location = new System.Drawing.Point(138, 29);
         this.cbResourcen.Name = "cbResourcen";
         this.cbResourcen.Size = new System.Drawing.Size(127, 17);
         this.cbResourcen.TabIndex = 132;
         this.cbResourcen.Text = "Ressourcen zuweisen";
         this.cbResourcen.UseVisualStyleBackColor = true;
         this.cbResourcen.CheckedChanged += new System.EventHandler(this.cbResourcen_CheckedChanged);
         // 
         // gbAuftrag
         // 
         this.gbAuftrag.Controls.Add(this.tbAuftragsPos);
         this.gbAuftrag.Controls.Add(this.tbAuftrag);
         this.gbAuftrag.Controls.Add(this.label3);
         this.gbAuftrag.Controls.Add(this.label2);
         this.gbAuftrag.ForeColor = System.Drawing.Color.DarkBlue;
         this.gbAuftrag.Location = new System.Drawing.Point(20, 310);
         this.gbAuftrag.Name = "gbAuftrag";
         this.gbAuftrag.Size = new System.Drawing.Size(182, 120);
         this.gbAuftrag.TabIndex = 133;
         this.gbAuftrag.TabStop = false;
         this.gbAuftrag.Text = "Auftragszuweisung";
         this.gbAuftrag.Leave += new System.EventHandler(this.gbAuftrag_Leave);
         // 
         // tbAuftragsPos
         // 
         this.tbAuftragsPos.BackColor = System.Drawing.Color.White;
         this.tbAuftragsPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbAuftragsPos.Location = new System.Drawing.Point(6, 94);
         this.tbAuftragsPos.Name = "tbAuftragsPos";
         this.tbAuftragsPos.Size = new System.Drawing.Size(170, 20);
         this.tbAuftragsPos.TabIndex = 9;
         this.tbAuftragsPos.Validated += new System.EventHandler(this.tbAuftragsPos_Validated);
         // 
         // tbAuftrag
         // 
         this.tbAuftrag.BackColor = System.Drawing.Color.White;
         this.tbAuftrag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbAuftrag.Location = new System.Drawing.Point(6, 46);
         this.tbAuftrag.Name = "tbAuftrag";
         this.tbAuftrag.Size = new System.Drawing.Size(170, 20);
         this.tbAuftrag.TabIndex = 8;
         this.tbAuftrag.Validated += new System.EventHandler(this.tbAuftrag_Validated);
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(8, 29);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(81, 13);
         this.label3.TabIndex = 2;
         this.label3.Text = "Auftragnummer:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(8, 74);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(124, 13);
         this.label2.TabIndex = 1;
         this.label2.Text = "Auftragspositionsnummer";
         // 
         // cbAuftrag
         // 
         this.cbAuftrag.AutoSize = true;
         this.cbAuftrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.cbAuftrag.ForeColor = System.Drawing.Color.DarkBlue;
         this.cbAuftrag.Location = new System.Drawing.Point(281, 29);
         this.cbAuftrag.Name = "cbAuftrag";
         this.cbAuftrag.Size = new System.Drawing.Size(104, 17);
         this.cbAuftrag.TabIndex = 134;
         this.cbAuftrag.Text = "Auftrag zuweisen";
         this.cbAuftrag.UseVisualStyleBackColor = true;
         this.cbAuftrag.CheckedChanged += new System.EventHandler(this.cbAuftrag_CheckedChanged);
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.ForeColor = System.Drawing.Color.DarkBlue;
         this.label4.Location = new System.Drawing.Point(419, 33);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(70, 13);
         this.label4.TabIndex = 135;
         this.label4.Text = "Druckanzahl:";
         // 
         // nudDruckanzahl
         // 
         this.nudDruckanzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.nudDruckanzahl.Location = new System.Drawing.Point(495, 31);
         this.nudDruckanzahl.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
         this.nudDruckanzahl.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.nudDruckanzahl.Name = "nudDruckanzahl";
         this.nudDruckanzahl.Size = new System.Drawing.Size(38, 20);
         this.nudDruckanzahl.TabIndex = 136;
         this.nudDruckanzahl.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
         // 
         // frmRGGSErstellen
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(770, 436);
         this.Controls.Add(this.nudDruckanzahl);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.cbAuftrag);
         this.Controls.Add(this.gbAuftrag);
         this.Controls.Add(this.cbResourcen);
         this.Controls.Add(this.gbResourcen);
         this.Controls.Add(this.gbRGAnzeige);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.gbDocText);
         this.Controls.Add(this.afToolStrip1);
         this.Controls.Add(this.cbDocArt);
         this.Controls.Add(this.gbDocAdressat);
         this.Name = "frmRGGSErstellen";
         this.Load += new System.EventHandler(this.frmRGGSErstellen_Load);
         this.gbDocAdressat.ResumeLayout(false);
         this.gbDocAdressat.PerformLayout();
         this.afToolStrip1.ResumeLayout(false);
         this.afToolStrip1.PerformLayout();
         this.gbDocText.ResumeLayout(false);
         this.gbDocText.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
         this.afToolStrip2.ResumeLayout(false);
         this.afToolStrip2.PerformLayout();
         this.gbRGAnzeige.ResumeLayout(false);
         this.gbRGAnzeige.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.nudRGMwSt)).EndInit();
         this.gbResourcen.ResumeLayout(false);
         this.gbResourcen.PerformLayout();
         this.gbAuftrag.ResumeLayout(false);
         this.gbAuftrag.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.nudDruckanzahl)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gbDocAdressat;
    private System.Windows.Forms.CheckBox cbDocArt;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbtnSavePrint;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.Button btnSearchA;
    private System.Windows.Forms.TextBox tbAuftraggeber;
    private System.Windows.Forms.GroupBox gbDocText;
    private System.Windows.Forms.DataGridView dgv;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox gbRGAnzeige;
    private System.Windows.Forms.NumericUpDown nudRGMwSt;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox tbRGBrutto;
    private System.Windows.Forms.TextBox tbRGMwStBetrag;
    private System.Windows.Forms.TextBox tbRGNetto;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label lBrutto;
    private System.Windows.Forms.Label lNetto;
    private System.Windows.Forms.GroupBox gbResourcen;
    private System.Windows.Forms.ComboBox comboAuflieger;
    private System.Windows.Forms.ComboBox comboZM;
    private System.Windows.Forms.Label lAuflieger;
    private System.Windows.Forms.Label lZM;
    private System.Windows.Forms.TextBox tbSU;
    private System.Windows.Forms.Button btnSU;
    private Sped4.Controls.AFToolStrip afToolStrip2;
    private System.Windows.Forms.ToolStripButton tsbNeu;
    private System.Windows.Forms.DataGridViewImageColumn Delete;
    private System.Windows.Forms.DataGridViewTextBoxColumn Text;
    private System.Windows.Forms.DataGridViewTextBoxColumn Betrag;
    private System.Windows.Forms.CheckBox cbResourcen;
    private System.Windows.Forms.GroupBox gbAuftrag;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox cbAuftrag;
    private System.Windows.Forms.TextBox tbAuftragsPos;
    private System.Windows.Forms.TextBox tbAuftrag;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown nudDruckanzahl;
  }
}