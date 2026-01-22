namespace Sped4
{
  partial class ctrLager
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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrLager));
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.bestandslisteDesKundenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zumEingangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ausgangBearbeitenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dgv = new Sped4.Controls.AFGrid();
      this.minMaxADRSearch = new Sped4.Controls.AFMinMaxPanel();
      this.gbAuftraggeber = new System.Windows.Forms.GroupBox();
      this.pbFilterKDID = new System.Windows.Forms.PictureBox();
      this.pbSucheDelete = new System.Windows.Forms.PictureBox();
      this.pbFilterName = new System.Windows.Forms.PictureBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbNameSearch = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tbMCSearch = new System.Windows.Forms.TextBox();
      this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
      this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
      this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
      this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
      this.bestandNachKundeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.bestandArtikelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.bestandNachKundeAusgangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.miAusgangArtikel = new System.Windows.Forms.ToolStripMenuItem();
      this.tsbClose = new System.Windows.Forms.ToolStripButton();
      this.gbArtikel = new System.Windows.Forms.GroupBox();
      this.pbSucheArtikelDelete = new System.Windows.Forms.PictureBox();
      this.pbSeachArtikel = new System.Windows.Forms.PictureBox();
      this.lSSuchbegriff = new System.Windows.Forms.Label();
      this.tbArtikelSearch = new System.Windows.Forms.TextBox();
      this.cbFilter = new System.Windows.Forms.ComboBox();
      this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
      this.contextMenuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
      this.minMaxADRSearch.SuspendLayout();
      this.gbAuftraggeber.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbFilterKDID)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbSucheDelete)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbFilterName)).BeginInit();
      this.afToolStrip1.SuspendLayout();
      this.gbArtikel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbSucheArtikelDelete)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbSeachArtikel)).BeginInit();
      this.SuspendLayout();
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bestandslisteDesKundenToolStripMenuItem,
            this.zumEingangToolStripMenuItem,
            this.ausgangBearbeitenToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.contextMenuStrip1.Size = new System.Drawing.Size(183, 70);
      // 
      // bestandslisteDesKundenToolStripMenuItem
      // 
      this.bestandslisteDesKundenToolStripMenuItem.Name = "bestandslisteDesKundenToolStripMenuItem";
      this.bestandslisteDesKundenToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
      this.bestandslisteDesKundenToolStripMenuItem.Text = "Ausgang erstellen";
      this.bestandslisteDesKundenToolStripMenuItem.Click += new System.EventHandler(this.bestandslisteDesKundenToolStripMenuItem_Click);
      // 
      // zumEingangToolStripMenuItem
      // 
      this.zumEingangToolStripMenuItem.Name = "zumEingangToolStripMenuItem";
      this.zumEingangToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
      this.zumEingangToolStripMenuItem.Text = "Eingang bearbeiten";
      this.zumEingangToolStripMenuItem.Click += new System.EventHandler(this.zumEingangToolStripMenuItem_Click);
      // 
      // ausgangBearbeitenToolStripMenuItem
      // 
      this.ausgangBearbeitenToolStripMenuItem.Name = "ausgangBearbeitenToolStripMenuItem";
      this.ausgangBearbeitenToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
      this.ausgangBearbeitenToolStripMenuItem.Text = "Ausgang bearbeiten";
      this.ausgangBearbeitenToolStripMenuItem.Click += new System.EventHandler(this.ausgangBearbeitenToolStripMenuItem_Click);
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
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkBlue;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv.DefaultCellStyle = dataGridViewCellStyle1;
      this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgv.Location = new System.Drawing.Point(0, 177);
      this.dgv.MultiSelect = false;
      this.dgv.Name = "dgv";
      this.dgv.ReadOnly = true;
      this.dgv.RowHeadersVisible = false;
      this.dgv.RowTemplate.Height = 55;
      this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv.ShowEditingIcon = false;
      this.dgv.ShowRowErrors = false;
      this.dgv.Size = new System.Drawing.Size(546, 271);
      this.dgv.TabIndex = 9;
      this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
      this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
      this.dgv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDoubleClick);
      this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
      // 
      // minMaxADRSearch
      // 
      this.minMaxADRSearch.BackColor = System.Drawing.Color.White;
      this.minMaxADRSearch.Controls.Add(this.gbAuftraggeber);
      this.minMaxADRSearch.Controls.Add(this.afToolStrip1);
      this.minMaxADRSearch.Controls.Add(this.gbArtikel);
      this.minMaxADRSearch.Dock = System.Windows.Forms.DockStyle.Top;
      this.minMaxADRSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
      this.minMaxADRSearch.Location = new System.Drawing.Point(0, 28);
      this.minMaxADRSearch.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
      this.minMaxADRSearch.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.minMaxADRSearch.myImage = global::Sped4.Properties.Resources.gears_preferences;
      this.minMaxADRSearch.myText = "Optionen";
      this.minMaxADRSearch.Name = "minMaxADRSearch";
      this.minMaxADRSearch.Size = new System.Drawing.Size(546, 149);
      this.minMaxADRSearch.TabIndex = 6;
      this.minMaxADRSearch.Text = "afMinMaxPanel1";
      // 
      // gbAuftraggeber
      // 
      this.gbAuftraggeber.Controls.Add(this.pbFilterKDID);
      this.gbAuftraggeber.Controls.Add(this.pbSucheDelete);
      this.gbAuftraggeber.Controls.Add(this.pbFilterName);
      this.gbAuftraggeber.Controls.Add(this.label2);
      this.gbAuftraggeber.Controls.Add(this.tbNameSearch);
      this.gbAuftraggeber.Controls.Add(this.label1);
      this.gbAuftraggeber.Controls.Add(this.tbMCSearch);
      this.gbAuftraggeber.Location = new System.Drawing.Point(262, 56);
      this.gbAuftraggeber.Name = "gbAuftraggeber";
      this.gbAuftraggeber.Size = new System.Drawing.Size(271, 87);
      this.gbAuftraggeber.TabIndex = 11;
      this.gbAuftraggeber.TabStop = false;
      this.gbAuftraggeber.Text = "Suche nach Auftraggeber";
      // 
      // pbFilterKDID
      // 
      this.pbFilterKDID.Image = global::Sped4.Properties.Resources.magnifying_glass;
      this.pbFilterKDID.Location = new System.Drawing.Point(219, 19);
      this.pbFilterKDID.Name = "pbFilterKDID";
      this.pbFilterKDID.Size = new System.Drawing.Size(24, 24);
      this.pbFilterKDID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbFilterKDID.TabIndex = 133;
      this.pbFilterKDID.TabStop = false;
      this.pbFilterKDID.Click += new System.EventHandler(this.pictureBox1_Click);
      // 
      // pbSucheDelete
      // 
      this.pbSucheDelete.Image = global::Sped4.Properties.Resources.delete_16;
      this.pbSucheDelete.Location = new System.Drawing.Point(249, 10);
      this.pbSucheDelete.Name = "pbSucheDelete";
      this.pbSucheDelete.Size = new System.Drawing.Size(16, 16);
      this.pbSucheDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbSucheDelete.TabIndex = 132;
      this.pbSucheDelete.TabStop = false;
      this.pbSucheDelete.Tag = "Suche löschen / Beenden";
      this.pbSucheDelete.Click += new System.EventHandler(this.pbSucheDelete_Click);
      // 
      // pbFilterName
      // 
      this.pbFilterName.Image = global::Sped4.Properties.Resources.magnifying_glass;
      this.pbFilterName.Location = new System.Drawing.Point(219, 47);
      this.pbFilterName.Name = "pbFilterName";
      this.pbFilterName.Size = new System.Drawing.Size(24, 24);
      this.pbFilterName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbFilterName.TabIndex = 131;
      this.pbFilterName.TabStop = false;
      this.pbFilterName.Click += new System.EventHandler(this.pbFilterArtikel_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 52);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Name";
      // 
      // tbNameSearch
      // 
      this.tbNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbNameSearch.Location = new System.Drawing.Point(82, 49);
      this.tbNameSearch.Name = "tbNameSearch";
      this.tbNameSearch.Size = new System.Drawing.Size(131, 20);
      this.tbNameSearch.TabIndex = 5;
      this.tbNameSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(61, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Kunden Nr.";
      // 
      // tbMCSearch
      // 
      this.tbMCSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbMCSearch.Location = new System.Drawing.Point(82, 24);
      this.tbMCSearch.Name = "tbMCSearch";
      this.tbMCSearch.Size = new System.Drawing.Size(131, 20);
      this.tbMCSearch.TabIndex = 3;
      this.tbMCSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // afToolStrip1
      // 
      this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClear,
            this.tsbtnSearch,
            this.toolStripDropDownButton1,
            this.tsbClose});
      this.afToolStrip1.Location = new System.Drawing.Point(3, 28);
      this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
      this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
      this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
      this.afToolStrip1.myUnderlined = true;
      this.afToolStrip1.Name = "afToolStrip1";
      this.afToolStrip1.Size = new System.Drawing.Size(108, 25);
      this.afToolStrip1.TabIndex = 8;
      this.afToolStrip1.Text = "afToolStrip1";
      // 
      // tsbtnClear
      // 
      this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbtnClear.Image = global::Sped4.Properties.Resources.form_green_delete;
      this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbtnClear.Name = "tsbtnClear";
      this.tsbtnClear.Size = new System.Drawing.Size(23, 22);
      this.tsbtnClear.Text = "Formular bereinigen";
      this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
      // 
      // tsbtnSearch
      // 
      this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbtnSearch.Image = global::Sped4.Properties.Resources.magnifying_glass;
      this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbtnSearch.Name = "tsbtnSearch";
      this.tsbtnSearch.Size = new System.Drawing.Size(23, 22);
      this.tsbtnSearch.Text = "Artikelsuche aktivieren";
      this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
      // 
      // toolStripDropDownButton1
      // 
      this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bestandNachKundeToolStripMenuItem,
            this.bestandArtikelToolStripMenuItem,
            this.bestandNachKundeAusgangToolStripMenuItem,
            this.miAusgangArtikel});
      this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
      this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
      this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
      this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
      // 
      // bestandNachKundeToolStripMenuItem
      // 
      this.bestandNachKundeToolStripMenuItem.Name = "bestandNachKundeToolStripMenuItem";
      this.bestandNachKundeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.bestandNachKundeToolStripMenuItem.Text = "Bestand nach Kunde";
      this.bestandNachKundeToolStripMenuItem.Click += new System.EventHandler(this.bestandNachKundeToolStripMenuItem_Click);
      // 
      // bestandArtikelToolStripMenuItem
      // 
      this.bestandArtikelToolStripMenuItem.Name = "bestandArtikelToolStripMenuItem";
      this.bestandArtikelToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.bestandArtikelToolStripMenuItem.Text = "Bestand Artikel";
      this.bestandArtikelToolStripMenuItem.Click += new System.EventHandler(this.bestandArtikelToolStripMenuItem_Click);
      // 
      // bestandNachKundeAusgangToolStripMenuItem
      // 
      this.bestandNachKundeAusgangToolStripMenuItem.Name = "bestandNachKundeAusgangToolStripMenuItem";
      this.bestandNachKundeAusgangToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
      this.bestandNachKundeAusgangToolStripMenuItem.Text = "Ausgang nach Kunde";
      this.bestandNachKundeAusgangToolStripMenuItem.Click += new System.EventHandler(this.bestandNachKundeAusgangToolStripMenuItem_Click);
      // 
      // miAusgangArtikel
      // 
      this.miAusgangArtikel.Name = "miAusgangArtikel";
      this.miAusgangArtikel.Size = new System.Drawing.Size(186, 22);
      this.miAusgangArtikel.Text = "Ausgang nach Artikel";
      this.miAusgangArtikel.Click += new System.EventHandler(this.miAusgangArtikel_Click);
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
      // gbArtikel
      // 
      this.gbArtikel.Controls.Add(this.pbSucheArtikelDelete);
      this.gbArtikel.Controls.Add(this.pbSeachArtikel);
      this.gbArtikel.Controls.Add(this.lSSuchbegriff);
      this.gbArtikel.Controls.Add(this.tbArtikelSearch);
      this.gbArtikel.Controls.Add(this.cbFilter);
      this.gbArtikel.Location = new System.Drawing.Point(15, 56);
      this.gbArtikel.Name = "gbArtikel";
      this.gbArtikel.Size = new System.Drawing.Size(241, 88);
      this.gbArtikel.TabIndex = 10;
      this.gbArtikel.TabStop = false;
      this.gbArtikel.Text = "Suche nach Artikel";
      // 
      // pbSucheArtikelDelete
      // 
      this.pbSucheArtikelDelete.Image = global::Sped4.Properties.Resources.delete_16;
      this.pbSucheArtikelDelete.Location = new System.Drawing.Point(219, 10);
      this.pbSucheArtikelDelete.Name = "pbSucheArtikelDelete";
      this.pbSucheArtikelDelete.Size = new System.Drawing.Size(16, 16);
      this.pbSucheArtikelDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbSucheArtikelDelete.TabIndex = 135;
      this.pbSucheArtikelDelete.TabStop = false;
      this.pbSucheArtikelDelete.Tag = "Suche löschen / Beenden";
      this.pbSucheArtikelDelete.Click += new System.EventHandler(this.pbSucheArtikelDelete_Click);
      // 
      // pbSeachArtikel
      // 
      this.pbSeachArtikel.Image = global::Sped4.Properties.Resources.magnifying_glass;
      this.pbSeachArtikel.Location = new System.Drawing.Point(211, 49);
      this.pbSeachArtikel.Name = "pbSeachArtikel";
      this.pbSeachArtikel.Size = new System.Drawing.Size(24, 24);
      this.pbSeachArtikel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pbSeachArtikel.TabIndex = 134;
      this.pbSeachArtikel.TabStop = false;
      this.pbSeachArtikel.Click += new System.EventHandler(this.pbSeachArtikel_Click);
      // 
      // lSSuchbegriff
      // 
      this.lSSuchbegriff.AutoSize = true;
      this.lSSuchbegriff.Location = new System.Drawing.Point(12, 54);
      this.lSSuchbegriff.Name = "lSSuchbegriff";
      this.lSSuchbegriff.Size = new System.Drawing.Size(44, 13);
      this.lSSuchbegriff.TabIndex = 2;
      this.lSSuchbegriff.Text = "Suche..";
      // 
      // tbArtikelSearch
      // 
      this.tbArtikelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbArtikelSearch.Location = new System.Drawing.Point(62, 49);
      this.tbArtikelSearch.Name = "tbArtikelSearch";
      this.tbArtikelSearch.Size = new System.Drawing.Size(143, 20);
      this.tbArtikelSearch.TabIndex = 3;
      this.tbArtikelSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // cbFilter
      // 
      this.cbFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cbFilter.FormattingEnabled = true;
      this.cbFilter.Location = new System.Drawing.Point(15, 19);
      this.cbFilter.Name = "cbFilter";
      this.cbFilter.Size = new System.Drawing.Size(164, 21);
      this.cbFilter.TabIndex = 9;
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
      this.afColorLabel1.myText = "Lager";
      this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
      this.afColorLabel1.myUnderlined = true;
      this.afColorLabel1.Name = "afColorLabel1";
      this.afColorLabel1.Size = new System.Drawing.Size(546, 28);
      this.afColorLabel1.TabIndex = 5;
      this.afColorLabel1.Text = "afColorLabel1";
      // 
      // ctrLager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.dgv);
      this.Controls.Add(this.minMaxADRSearch);
      this.Controls.Add(this.afColorLabel1);
      this.ForeColor = System.Drawing.Color.DarkBlue;
      this.Name = "ctrLager";
      this.Size = new System.Drawing.Size(546, 448);
      this.Load += new System.EventHandler(this.ctrLager_Load);
      this.contextMenuStrip1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
      this.minMaxADRSearch.ResumeLayout(false);
      this.minMaxADRSearch.PerformLayout();
      this.gbAuftraggeber.ResumeLayout(false);
      this.gbAuftraggeber.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbFilterKDID)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbSucheDelete)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbFilterName)).EndInit();
      this.afToolStrip1.ResumeLayout(false);
      this.afToolStrip1.PerformLayout();
      this.gbArtikel.ResumeLayout(false);
      this.gbArtikel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbSucheArtikelDelete)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbSeachArtikel)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Sped4.Controls.AFColorLabel afColorLabel1;
    private Sped4.Controls.AFMinMaxPanel minMaxADRSearch;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.TextBox tbArtikelSearch;
    private System.Windows.Forms.Label lSSuchbegriff;
    private Sped4.Controls.AFGrid dgv;
    private System.Windows.Forms.ComboBox cbFilter;
    private System.Windows.Forms.GroupBox gbAuftraggeber;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbMCSearch;
    private System.Windows.Forms.GroupBox gbArtikel;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem bestandslisteDesKundenToolStripMenuItem;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbNameSearch;
    private System.Windows.Forms.PictureBox pbSucheDelete;
    private System.Windows.Forms.PictureBox pbFilterName;
    private System.Windows.Forms.PictureBox pbFilterKDID;
    private System.Windows.Forms.ToolStripButton tsbtnClear;
    private System.Windows.Forms.ToolStripButton tsbtnSearch;
    private System.Windows.Forms.PictureBox pbSucheArtikelDelete;
    private System.Windows.Forms.PictureBox pbSeachArtikel;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem bestandNachKundeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem bestandArtikelToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zumEingangToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem bestandNachKundeAusgangToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem miAusgangArtikel;
    private System.Windows.Forms.ToolStripMenuItem ausgangBearbeitenToolStripMenuItem;
  }
}
