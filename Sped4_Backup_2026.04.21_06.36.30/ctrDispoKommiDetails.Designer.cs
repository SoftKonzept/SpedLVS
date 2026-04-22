namespace Sped4
{
  partial class ctrDispoKommiDetails
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbAlteInfos = new System.Windows.Forms.TextBox();
            this.gbFahrerKontakt = new System.Windows.Forms.GroupBox();
            this.tbNeueInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbKontaktZeit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbAuftragdaten = new System.Windows.Forms.GroupBox();
            this.dgvInfo = new Sped4.Controls.AFGrid();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsBtnSpeichern = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.gbFahrerKontakt.SuspendLayout();
            this.gbAuftragdaten.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.tbAlteInfos);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(4, 423);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 195);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "bestehende Kontaktinformationen";
            // 
            // tbAlteInfos
            // 
            this.tbAlteInfos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAlteInfos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAlteInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAlteInfos.Location = new System.Drawing.Point(3, 16);
            this.tbAlteInfos.Multiline = true;
            this.tbAlteInfos.Name = "tbAlteInfos";
            this.tbAlteInfos.ReadOnly = true;
            this.tbAlteInfos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbAlteInfos.Size = new System.Drawing.Size(294, 176);
            this.tbAlteInfos.TabIndex = 56;
            // 
            // gbFahrerKontakt
            // 
            this.gbFahrerKontakt.BackColor = System.Drawing.Color.White;
            this.gbFahrerKontakt.Controls.Add(this.tbNeueInfo);
            this.gbFahrerKontakt.Controls.Add(this.label3);
            this.gbFahrerKontakt.Controls.Add(this.tbKontaktZeit);
            this.gbFahrerKontakt.Controls.Add(this.label2);
            this.gbFahrerKontakt.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbFahrerKontakt.Location = new System.Drawing.Point(4, 223);
            this.gbFahrerKontakt.Name = "gbFahrerKontakt";
            this.gbFahrerKontakt.Size = new System.Drawing.Size(297, 193);
            this.gbFahrerKontakt.TabIndex = 66;
            this.gbFahrerKontakt.TabStop = false;
            this.gbFahrerKontakt.Text = "Fahrerkontakt";
            // 
            // tbNeueInfo
            // 
            this.tbNeueInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNeueInfo.Location = new System.Drawing.Point(6, 65);
            this.tbNeueInfo.Multiline = true;
            this.tbNeueInfo.Name = "tbNeueInfo";
            this.tbNeueInfo.Size = new System.Drawing.Size(278, 120);
            this.tbNeueInfo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(15, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "neue Info:";
            // 
            // tbKontaktZeit
            // 
            this.tbKontaktZeit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKontaktZeit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbKontaktZeit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKontaktZeit.Location = new System.Drawing.Point(84, 21);
            this.tbKontaktZeit.Name = "tbKontaktZeit";
            this.tbKontaktZeit.ReadOnly = true;
            this.tbKontaktZeit.Size = new System.Drawing.Size(194, 20);
            this.tbKontaktZeit.TabIndex = 52;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(15, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Kontaktzeit:";
            // 
            // gbAuftragdaten
            // 
            this.gbAuftragdaten.BackColor = System.Drawing.Color.White;
            this.gbAuftragdaten.Controls.Add(this.dgvInfo);
            this.gbAuftragdaten.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbAuftragdaten.Location = new System.Drawing.Point(3, 32);
            this.gbAuftragdaten.Name = "gbAuftragdaten";
            this.gbAuftragdaten.Size = new System.Drawing.Size(298, 186);
            this.gbAuftragdaten.TabIndex = 65;
            this.gbAuftragdaten.TabStop = false;
            this.gbAuftragdaten.Text = "Auftragsdaten";
            // 
            // dgvInfo
            // 
            this.dgvInfo.AllowUserToAddRows = false;
            this.dgvInfo.AllowUserToDeleteRows = false;
            this.dgvInfo.AllowUserToResizeRows = false;
            this.dgvInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvInfo.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.dgvInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfo.ColumnHeadersVisible = false;
            this.dgvInfo.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInfo.Location = new System.Drawing.Point(3, 16);
            this.dgvInfo.MultiSelect = false;
            this.dgvInfo.Name = "dgvInfo";
            this.dgvInfo.ReadOnly = true;
            this.dgvInfo.RowHeadersVisible = false;
            this.dgvInfo.RowTemplate.Height = 55;
            this.dgvInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInfo.ShowEditingIcon = false;
            this.dgvInfo.ShowRowErrors = false;
            this.dgvInfo.Size = new System.Drawing.Size(292, 167);
            this.dgvInfo.TabIndex = 3;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnSpeichern,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(314, 25);
            this.afToolStrip1.TabIndex = 144;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsBtnSpeichern
            // 
            this.tsBtnSpeichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSpeichern.Image = global::Sped4.Properties.Resources.check;
            this.tsBtnSpeichern.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSpeichern.Name = "tsBtnSpeichern";
            this.tsBtnSpeichern.Size = new System.Drawing.Size(23, 22);
            this.tsBtnSpeichern.Text = "Kontaktinfo speichern";
            this.tsBtnSpeichern.Click += new System.EventHandler(this.tsBtnSpeichern_Click);
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
            // ctrDispoKommiDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.afToolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbFahrerKontakt);
            this.Controls.Add(this.gbAuftragdaten);
            this.Name = "ctrDispoKommiDetails";
            this.Size = new System.Drawing.Size(314, 678);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbFahrerKontakt.ResumeLayout(false);
            this.gbFahrerKontakt.PerformLayout();
            this.gbAuftragdaten.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox tbAlteInfos;
    private System.Windows.Forms.GroupBox gbFahrerKontakt;
    private System.Windows.Forms.TextBox tbNeueInfo;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox tbKontaktZeit;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox gbAuftragdaten;
    private Sped4.Controls.AFGrid dgvInfo;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsBtnSpeichern;
    private System.Windows.Forms.ToolStripButton tsbClose;
  }
}
