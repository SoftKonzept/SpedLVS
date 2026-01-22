namespace Sped4
{
  partial class ctrLogbuch
  {
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new Sped4.Controls.AFGrid();
            this.minMaxOption = new Sped4.Controls.AFMinMaxPanel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.pbFilter = new System.Windows.Forms.PictureBox();
            this.dtpDatumBis = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.cbText = new System.Windows.Forms.CheckBox();
            this.dtpDatumAb = new System.Windows.Forms.DateTimePicker();
            this.cbDatum = new System.Windows.Forms.CheckBox();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.minMaxOption.SuspendLayout();
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilter)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgv.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 238);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 55;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowEditingIcon = false;
            this.dgv.ShowRowErrors = false;
            this.dgv.Size = new System.Drawing.Size(880, 421);
            this.dgv.TabIndex = 8;
            // 
            // minMaxOption
            // 
            this.minMaxOption.BackColor = System.Drawing.Color.White;
            this.minMaxOption.Controls.Add(this.gbFilter);
            this.minMaxOption.Controls.Add(this.afToolStrip1);
            this.minMaxOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.minMaxOption.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.minMaxOption.Location = new System.Drawing.Point(0, 28);
            this.minMaxOption.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.minMaxOption.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxOption.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.minMaxOption.myText = "Optionen";
            this.minMaxOption.Name = "minMaxOption";
            this.minMaxOption.Size = new System.Drawing.Size(880, 210);
            this.minMaxOption.TabIndex = 7;
            this.minMaxOption.Text = "afMinMaxPanel1";
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.pbFilter);
            this.gbFilter.Controls.Add(this.dtpDatumBis);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Controls.Add(this.tbSearch);
            this.gbFilter.Controls.Add(this.cbText);
            this.gbFilter.Controls.Add(this.dtpDatumAb);
            this.gbFilter.Controls.Add(this.cbDatum);
            this.gbFilter.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbFilter.Location = new System.Drawing.Point(18, 66);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(378, 124);
            this.gbFilter.TabIndex = 9;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // pbFilter
            // 
            this.pbFilter.Image = global::Sped4.Properties.Resources.magnifying_glass;
            this.pbFilter.Location = new System.Drawing.Point(336, 19);
            this.pbFilter.Name = "pbFilter";
            this.pbFilter.Size = new System.Drawing.Size(24, 24);
            this.pbFilter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFilter.TabIndex = 137;
            this.pbFilter.TabStop = false;
            this.pbFilter.Tag = "Suchen...";
            this.pbFilter.Click += new System.EventHandler(this.pbFilter_Click);
            // 
            // dtpDatumBis
            // 
            this.dtpDatumBis.Location = new System.Drawing.Point(121, 44);
            this.dtpDatumBis.Name = "dtpDatumBis";
            this.dtpDatumBis.Size = new System.Drawing.Size(200, 20);
            this.dtpDatumBis.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "bis zum..";
            // 
            // tbSearch
            // 
            this.tbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearch.Enabled = false;
            this.tbSearch.Location = new System.Drawing.Point(121, 78);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(200, 20);
            this.tbSearch.TabIndex = 7;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // cbText
            // 
            this.cbText.AutoSize = true;
            this.cbText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbText.Location = new System.Drawing.Point(14, 77);
            this.cbText.Name = "cbText";
            this.cbText.Size = new System.Drawing.Size(90, 17);
            this.cbText.TabIndex = 2;
            this.cbText.Text = "Suche nach...";
            this.cbText.UseVisualStyleBackColor = true;
            this.cbText.CheckedChanged += new System.EventHandler(this.cbText_CheckedChanged);
            // 
            // dtpDatumAb
            // 
            this.dtpDatumAb.Location = new System.Drawing.Point(121, 17);
            this.dtpDatumAb.Name = "dtpDatumAb";
            this.dtpDatumAb.Size = new System.Drawing.Size(200, 20);
            this.dtpDatumAb.TabIndex = 1;
            // 
            // cbDatum
            // 
            this.cbDatum.AutoSize = true;
            this.cbDatum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDatum.Location = new System.Drawing.Point(14, 19);
            this.cbDatum.Name = "cbDatum";
            this.cbDatum.Size = new System.Drawing.Size(101, 17);
            this.cbDatum.TabIndex = 0;
            this.cbDatum.Text = "Datum: ab dem..";
            this.cbDatum.UseVisualStyleBackColor = true;
            this.cbDatum.CheckedChanged += new System.EventHandler(this.cbDatum_CheckedChanged);
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbtnClose});
            this.afToolStrip1.Location = new System.Drawing.Point(3, 28);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(58, 25);
            this.afToolStrip1.TabIndex = 8;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Sped4.Properties.Resources.refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Logbuch schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // afColorLabel1
            // 
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afColorLabel1.Location = new System.Drawing.Point(0, 0);
            this.afColorLabel1.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afColorLabel1.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afColorLabel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afColorLabel1.myText = "Logbuch";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(880, 28);
            this.afColorLabel1.TabIndex = 6;
            // 
            // ctrLogbuch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.minMaxOption);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrLogbuch";
            this.Size = new System.Drawing.Size(880, 659);
            this.Load += new System.EventHandler(this.ctrLogbuch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.minMaxOption.ResumeLayout(false);
            this.minMaxOption.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilter)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Sped4.Controls.AFColorLabel afColorLabel1;
    private Sped4.Controls.AFMinMaxPanel minMaxOption;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private Sped4.Controls.AFGrid dgv;
    private System.Windows.Forms.GroupBox gbFilter;
    private System.Windows.Forms.CheckBox cbDatum;
    private System.Windows.Forms.DateTimePicker dtpDatumAb;
    private System.Windows.Forms.CheckBox cbText;
    private System.Windows.Forms.TextBox tbSearch;
    private System.Windows.Forms.ToolStripButton tsbtnClose;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.DateTimePicker dtpDatumBis;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pbFilter;
  }
}
