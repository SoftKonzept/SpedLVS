namespace Sped4
{
  partial class frmFibuExport
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
      this.gbZeitraum = new System.Windows.Forms.GroupBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.nudJahr = new System.Windows.Forms.NumericUpDown();
      this.cbMonat = new System.Windows.Forms.ComboBox();
      this.cbZeitraum = new System.Windows.Forms.CheckBox();
      this.gbVorgang = new System.Windows.Forms.GroupBox();
      this.cbAll = new System.Windows.Forms.CheckBox();
      this.cbFVGS = new System.Windows.Forms.CheckBox();
      this.cbGSSU = new System.Windows.Forms.CheckBox();
      this.cbGS = new System.Windows.Forms.CheckBox();
      this.cbRG = new System.Windows.Forms.CheckBox();
      this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
      this.tsBtnGet = new System.Windows.Forms.ToolStripButton();
      this.tsbClose = new System.Windows.Forms.ToolStripButton();
      this.panelDGV = new System.Windows.Forms.Panel();
      this.dgv = new Sped4.Controls.AFGrid();
      this.Typ = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Beleg = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Adressat = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Betrag = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Konto = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Übergabe = new System.Windows.Forms.DataGridViewImageColumn();
      this.gbZeitraum.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudJahr)).BeginInit();
      this.gbVorgang.SuspendLayout();
      this.afToolStrip1.SuspendLayout();
      this.panelDGV.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
      this.SuspendLayout();
      // 
      // gbZeitraum
      // 
      this.gbZeitraum.Controls.Add(this.label2);
      this.gbZeitraum.Controls.Add(this.label1);
      this.gbZeitraum.Controls.Add(this.nudJahr);
      this.gbZeitraum.Controls.Add(this.cbMonat);
      this.gbZeitraum.ForeColor = System.Drawing.Color.DarkBlue;
      this.gbZeitraum.Location = new System.Drawing.Point(12, 61);
      this.gbZeitraum.Name = "gbZeitraum";
      this.gbZeitraum.Size = new System.Drawing.Size(188, 77);
      this.gbZeitraum.TabIndex = 0;
      this.gbZeitraum.TabStop = false;
      this.gbZeitraum.Text = "Zeitraum";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(11, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(27, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Jahr";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(37, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Monat";
      // 
      // nudJahr
      // 
      this.nudJahr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nudJahr.Location = new System.Drawing.Point(119, 46);
      this.nudJahr.Maximum = new decimal(new int[] {
            3999,
            0,
            0,
            0});
      this.nudJahr.Minimum = new decimal(new int[] {
            2010,
            0,
            0,
            0});
      this.nudJahr.Name = "nudJahr";
      this.nudJahr.Size = new System.Drawing.Size(55, 20);
      this.nudJahr.TabIndex = 4;
      this.nudJahr.Value = new decimal(new int[] {
            2010,
            0,
            0,
            0});
      // 
      // cbMonat
      // 
      this.cbMonat.AllowDrop = true;
      this.cbMonat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbMonat.FormattingEnabled = true;
      this.cbMonat.Location = new System.Drawing.Point(54, 19);
      this.cbMonat.Name = "cbMonat";
      this.cbMonat.Size = new System.Drawing.Size(120, 21);
      this.cbMonat.TabIndex = 3;
      // 
      // cbZeitraum
      // 
      this.cbZeitraum.AutoSize = true;
      this.cbZeitraum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbZeitraum.ForeColor = System.Drawing.Color.DarkBlue;
      this.cbZeitraum.Location = new System.Drawing.Point(12, 37);
      this.cbZeitraum.Name = "cbZeitraum";
      this.cbZeitraum.Size = new System.Drawing.Size(87, 17);
      this.cbZeitraum.TabIndex = 4;
      this.cbZeitraum.Text = "kein Zeitraum";
      this.cbZeitraum.UseVisualStyleBackColor = true;
      this.cbZeitraum.CheckedChanged += new System.EventHandler(this.cbZeitraum_CheckedChanged);
      // 
      // gbVorgang
      // 
      this.gbVorgang.Controls.Add(this.cbAll);
      this.gbVorgang.Controls.Add(this.cbFVGS);
      this.gbVorgang.Controls.Add(this.cbGSSU);
      this.gbVorgang.Controls.Add(this.cbGS);
      this.gbVorgang.Controls.Add(this.cbRG);
      this.gbVorgang.ForeColor = System.Drawing.Color.DarkBlue;
      this.gbVorgang.Location = new System.Drawing.Point(206, 28);
      this.gbVorgang.Name = "gbVorgang";
      this.gbVorgang.Size = new System.Drawing.Size(154, 136);
      this.gbVorgang.TabIndex = 5;
      this.gbVorgang.TabStop = false;
      this.gbVorgang.Text = "Vorgänge";
      // 
      // cbAll
      // 
      this.cbAll.AutoSize = true;
      this.cbAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbAll.ForeColor = System.Drawing.Color.DarkBlue;
      this.cbAll.Location = new System.Drawing.Point(6, 111);
      this.cbAll.Name = "cbAll";
      this.cbAll.Size = new System.Drawing.Size(45, 17);
      this.cbAll.TabIndex = 9;
      this.cbAll.Text = "Alles";
      this.cbAll.UseVisualStyleBackColor = true;
      this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
      // 
      // cbFVGS
      // 
      this.cbFVGS.AutoSize = true;
      this.cbFVGS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbFVGS.ForeColor = System.Drawing.Color.DarkBlue;
      this.cbFVGS.Location = new System.Drawing.Point(6, 88);
      this.cbFVGS.Name = "cbFVGS";
      this.cbFVGS.Size = new System.Drawing.Size(102, 17);
      this.cbFVGS.TabIndex = 8;
      this.cbFVGS.Text = "FV - Gutschriften";
      this.cbFVGS.UseVisualStyleBackColor = true;
      this.cbFVGS.CheckedChanged += new System.EventHandler(this.cbFVGS_CheckedChanged);
      // 
      // cbGSSU
      // 
      this.cbGSSU.AutoSize = true;
      this.cbGSSU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbGSSU.ForeColor = System.Drawing.Color.DarkBlue;
      this.cbGSSU.Location = new System.Drawing.Point(6, 65);
      this.cbGSSU.Name = "cbGSSU";
      this.cbGSSU.Size = new System.Drawing.Size(139, 17);
      this.cbGSSU.TabIndex = 7;
      this.cbGSSU.Text = "Unternehmergutschriften";
      this.cbGSSU.UseVisualStyleBackColor = true;
      this.cbGSSU.CheckedChanged += new System.EventHandler(this.cbGSSU_CheckedChanged);
      // 
      // cbGS
      // 
      this.cbGS.AutoSize = true;
      this.cbGS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbGS.ForeColor = System.Drawing.Color.DarkBlue;
      this.cbGS.Location = new System.Drawing.Point(6, 42);
      this.cbGS.Name = "cbGS";
      this.cbGS.Size = new System.Drawing.Size(80, 17);
      this.cbGS.TabIndex = 6;
      this.cbGS.Text = "Gutschriften";
      this.cbGS.UseVisualStyleBackColor = true;
      this.cbGS.CheckedChanged += new System.EventHandler(this.cbGS_CheckedChanged);
      // 
      // cbRG
      // 
      this.cbRG.AutoSize = true;
      this.cbRG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbRG.ForeColor = System.Drawing.Color.DarkBlue;
      this.cbRG.Location = new System.Drawing.Point(6, 19);
      this.cbRG.Name = "cbRG";
      this.cbRG.Size = new System.Drawing.Size(85, 17);
      this.cbRG.TabIndex = 5;
      this.cbRG.Text = "Rechnungen";
      this.cbRG.UseVisualStyleBackColor = true;
      this.cbRG.CheckedChanged += new System.EventHandler(this.cbRG_CheckedChanged);
      // 
      // afToolStrip1
      // 
      this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnGet,
            this.tsbClose});
      this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
      this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
      this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
      this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
      this.afToolStrip1.myUnderlined = true;
      this.afToolStrip1.Name = "afToolStrip1";
      this.afToolStrip1.Size = new System.Drawing.Size(476, 25);
      this.afToolStrip1.TabIndex = 127;
      this.afToolStrip1.Text = "afToolStrip1";
      // 
      // tsBtnGet
      // 
      this.tsBtnGet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsBtnGet.Image = global::Sped4.Properties.Resources.magnifying_glass;
      this.tsBtnGet.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsBtnGet.Name = "tsBtnGet";
      this.tsBtnGet.Size = new System.Drawing.Size(23, 22);
      this.tsBtnGet.Text = "Daten suchen";
      this.tsBtnGet.Click += new System.EventHandler(this.tsBtnGet_Click);
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
      // panelDGV
      // 
      this.panelDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panelDGV.Controls.Add(this.dgv);
      this.panelDGV.Location = new System.Drawing.Point(0, 170);
      this.panelDGV.Name = "panelDGV";
      this.panelDGV.Size = new System.Drawing.Size(476, 299);
      this.panelDGV.TabIndex = 128;
      // 
      // dgv
      // 
      this.dgv.AllowDrop = true;
      this.dgv.AllowUserToAddRows = false;
      this.dgv.AllowUserToDeleteRows = false;
      this.dgv.AllowUserToResizeRows = false;
      this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
      this.dgv.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
      this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Typ,
            this.Beleg,
            this.Adressat,
            this.Datum,
            this.Betrag,
            this.Konto,
            this.Übergabe});
      this.dgv.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
      dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv.DefaultCellStyle = dataGridViewCellStyle20;
      this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgv.Location = new System.Drawing.Point(0, 0);
      this.dgv.MultiSelect = false;
      this.dgv.Name = "dgv";
      this.dgv.ReadOnly = true;
      this.dgv.RowHeadersVisible = false;
      this.dgv.RowTemplate.Height = 55;
      this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv.ShowEditingIcon = false;
      this.dgv.ShowRowErrors = false;
      this.dgv.Size = new System.Drawing.Size(476, 299);
      this.dgv.TabIndex = 6;
      this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
      // 
      // Typ
      // 
      this.Typ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle16.Format = "N0";
      dataGridViewCellStyle16.NullValue = null;
      dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.Typ.DefaultCellStyle = dataGridViewCellStyle16;
      this.Typ.HeaderText = "Typ";
      this.Typ.Name = "Typ";
      this.Typ.ReadOnly = true;
      this.Typ.Width = 50;
      // 
      // Beleg
      // 
      this.Beleg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Beleg.HeaderText = "Beleg";
      this.Beleg.Name = "Beleg";
      this.Beleg.ReadOnly = true;
      this.Beleg.Width = 59;
      // 
      // Adressat
      // 
      this.Adressat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.Adressat.HeaderText = "Adressat";
      this.Adressat.Name = "Adressat";
      this.Adressat.ReadOnly = true;
      this.Adressat.Width = 73;
      // 
      // Datum
      // 
      this.Datum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
      this.Datum.DefaultCellStyle = dataGridViewCellStyle17;
      this.Datum.HeaderText = "Datum";
      this.Datum.Name = "Datum";
      this.Datum.ReadOnly = true;
      this.Datum.Width = 63;
      // 
      // Betrag
      // 
      this.Betrag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
      dataGridViewCellStyle18.Format = "C2";
      dataGridViewCellStyle18.NullValue = null;
      this.Betrag.DefaultCellStyle = dataGridViewCellStyle18;
      this.Betrag.HeaderText = "Betrag";
      this.Betrag.Name = "Betrag";
      this.Betrag.ReadOnly = true;
      this.Betrag.Width = 63;
      // 
      // Konto
      // 
      this.Konto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
      this.Konto.DefaultCellStyle = dataGridViewCellStyle19;
      this.Konto.HeaderText = "Konto";
      this.Konto.Name = "Konto";
      this.Konto.ReadOnly = true;
      this.Konto.Width = 60;
      // 
      // Übergabe
      // 
      this.Übergabe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Übergabe.HeaderText = "Übergabe";
      this.Übergabe.Name = "Übergabe";
      this.Übergabe.ReadOnly = true;
      this.Übergabe.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.Übergabe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.Übergabe.Width = 79;
      // 
      // frmFibuExport
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(476, 469);
      this.Controls.Add(this.panelDGV);
      this.Controls.Add(this.afToolStrip1);
      this.Controls.Add(this.gbVorgang);
      this.Controls.Add(this.cbZeitraum);
      this.Controls.Add(this.gbZeitraum);
      this.Name = "frmFibuExport";
      this.Text = "Export an FIBU";
      this.Load += new System.EventHandler(this.frmFibuExport_Load);
      this.gbZeitraum.ResumeLayout(false);
      this.gbZeitraum.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudJahr)).EndInit();
      this.gbVorgang.ResumeLayout(false);
      this.gbVorgang.PerformLayout();
      this.afToolStrip1.ResumeLayout(false);
      this.afToolStrip1.PerformLayout();
      this.panelDGV.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox gbZeitraum;
    private System.Windows.Forms.ComboBox cbMonat;
    private System.Windows.Forms.NumericUpDown nudJahr;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox cbZeitraum;
    private System.Windows.Forms.GroupBox gbVorgang;
    private System.Windows.Forms.CheckBox cbFVGS;
    private System.Windows.Forms.CheckBox cbGSSU;
    private System.Windows.Forms.CheckBox cbGS;
    private System.Windows.Forms.CheckBox cbRG;
    private System.Windows.Forms.CheckBox cbAll;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsBtnGet;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.Panel panelDGV;
    private Sped4.Controls.AFGrid dgv;
    private System.Windows.Forms.DataGridViewTextBoxColumn Typ;
    private System.Windows.Forms.DataGridViewTextBoxColumn Beleg;
    private System.Windows.Forms.DataGridViewTextBoxColumn Adressat;
    private System.Windows.Forms.DataGridViewTextBoxColumn Datum;
    private System.Windows.Forms.DataGridViewTextBoxColumn Betrag;
    private System.Windows.Forms.DataGridViewTextBoxColumn Konto;
    private System.Windows.Forms.DataGridViewImageColumn Übergabe;
  }
}