namespace Sped4
{
    partial class ctrSperrlager
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.dgvSPL = new Telerik.WinControls.UI.RadGridView();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCheckOut = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRLToSL = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAuslagerung = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tscbSearch = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbSearchArtikel = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnStartSearch = new System.Windows.Forms.ToolStripButton();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.cbReebookInOldEingang = new System.Windows.Forms.CheckBox();
            this.gbSPLFilter = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbAuftraggeber = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpBis = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBrutto = new System.Windows.Forms.TextBox();
            this.dtpVon = new System.Windows.Forms.DateTimePicker();
            this.tbNetto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbAnzahl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPL.MasterTemplate)).BeginInit();
            this.afToolStrip2.SuspendLayout();
            this.afMinMaxPanel1.SuspendLayout();
            this.gbSPLFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSPL
            // 
            this.dgvSPL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSPL.Location = new System.Drawing.Point(0, 233);
            this.dgvSPL.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            this.dgvSPL.MasterTemplate.AllowAddNewRow = false;
            this.dgvSPL.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvSPL.Name = "dgvSPL";
            this.dgvSPL.Size = new System.Drawing.Size(741, 328);
            this.dgvSPL.TabIndex = 24;
            this.dgvSPL.ThemeName = "ControlDefault";
            this.dgvSPL.CreateCell += new Telerik.WinControls.UI.GridViewCreateCellEventHandler(this.dgvSPL_CreateCell);
            this.dgvSPL.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvSPL_CellClick);
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh,
            this.tsbtnCheckOut,
            this.tsbtnSearch,
            this.tsbtnRLToSL,
            this.tsbtnClear,
            this.tsbtnAuslagerung,
            this.tsbtnClose,
            this.toolStripSeparator2,
            this.tscbSearch,
            this.toolStripLabel1,
            this.tstbSearchArtikel,
            this.tsbtnStartSearch});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 206);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(741, 27);
            this.afToolStrip2.TabIndex = 22;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(24, 24);
            this.tsbtnRefresh.Text = "Refresh";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // tsbtnCheckOut
            // 
            this.tsbtnCheckOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCheckOut.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnCheckOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCheckOut.Name = "tsbtnCheckOut";
            this.tsbtnCheckOut.Size = new System.Drawing.Size(24, 24);
            this.tsbtnCheckOut.Text = "markierte Artikel zurück in Bestand buchen";
            this.tsbtnCheckOut.Visible = false;
            this.tsbtnCheckOut.Click += new System.EventHandler(this.tsbtnCheckOut_Click);
            // 
            // tsbtnSearch
            // 
            this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSearch.Image = global::Sped4.Properties.Resources.selection_view_32x32;
            this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearch.Name = "tsbtnSearch";
            this.tsbtnSearch.Size = new System.Drawing.Size(24, 24);
            this.tsbtnSearch.Text = "Bestandsdaten laden";
            this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
            // 
            // tsbtnRLToSL
            // 
            this.tsbtnRLToSL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRLToSL.Image = global::Sped4.Properties.Resources.truck_blue;
            this.tsbtnRLToSL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRLToSL.Name = "tsbtnRLToSL";
            this.tsbtnRLToSL.Size = new System.Drawing.Size(24, 24);
            this.tsbtnRLToSL.Text = "Rücklieferung zum Lieferanten...";
            this.tsbtnRLToSL.Click += new System.EventHandler(this.tsbtnRLToSL_Click);
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClear.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(24, 24);
            this.tsbtnClear.Text = "alle Vorgaben zurücksetzen";
            this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
            // 
            // tsbtnAuslagerung
            // 
            this.tsbtnAuslagerung.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAuslagerung.Image = global::Sped4.Properties.Resources.box_out_16x16;
            this.tsbtnAuslagerung.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAuslagerung.Name = "tsbtnAuslagerung";
            this.tsbtnAuslagerung.Size = new System.Drawing.Size(24, 24);
            this.tsbtnAuslagerung.Text = "ausgewählte Artikel auslagern...";
            this.tsbtnAuslagerung.Click += new System.EventHandler(this.tsbtnAuslagerung_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(24, 24);
            this.tsbtnClose.Text = "Suche schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tscbSearch
            // 
            this.tscbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbSearch.Name = "tscbSearch";
            this.tscbSearch.Size = new System.Drawing.Size(180, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 24);
            this.toolStripLabel1.Text = "Suchbegriff:";
            // 
            // tstbSearchArtikel
            // 
            this.tstbSearchArtikel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstbSearchArtikel.Name = "tstbSearchArtikel";
            this.tstbSearchArtikel.Size = new System.Drawing.Size(114, 27);
            this.tstbSearchArtikel.TextChanged += new System.EventHandler(this.tstbSearchArtikel_TextChanged);
            // 
            // tsbtnStartSearch
            // 
            this.tsbtnStartSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnStartSearch.Image = global::Sped4.Properties.Resources.selection_view_32x32;
            this.tsbtnStartSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStartSearch.Name = "tsbtnStartSearch";
            this.tsbtnStartSearch.Size = new System.Drawing.Size(24, 24);
            this.tsbtnStartSearch.Text = "Suche starten";
            this.tsbtnStartSearch.Click += new System.EventHandler(this.tsbtnStartSearch_Click);
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.Controls.Add(this.cbReebookInOldEingang);
            this.afMinMaxPanel1.Controls.Add(this.gbSPLFilter);
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 28);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Optionen";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(741, 178);
            this.afMinMaxPanel1.TabIndex = 9;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            // 
            // cbReebookInOldEingang
            // 
            this.cbReebookInOldEingang.AutoSize = true;
            this.cbReebookInOldEingang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbReebookInOldEingang.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbReebookInOldEingang.Location = new System.Drawing.Point(29, 31);
            this.cbReebookInOldEingang.Name = "cbReebookInOldEingang";
            this.cbReebookInOldEingang.Size = new System.Drawing.Size(199, 17);
            this.cbReebookInOldEingang.TabIndex = 162;
            this.cbReebookInOldEingang.Text = "Rückbuchung in alten Lagereingang ";
            this.cbReebookInOldEingang.UseVisualStyleBackColor = true;
            this.cbReebookInOldEingang.Visible = false;
            // 
            // gbSPLFilter
            // 
            this.gbSPLFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbSPLFilter.Controls.Add(this.label6);
            this.gbSPLFilter.Controls.Add(this.cbAuftraggeber);
            this.gbSPLFilter.Controls.Add(this.label4);
            this.gbSPLFilter.Controls.Add(this.dtpBis);
            this.gbSPLFilter.Controls.Add(this.label1);
            this.gbSPLFilter.Controls.Add(this.tbBrutto);
            this.gbSPLFilter.Controls.Add(this.dtpVon);
            this.gbSPLFilter.Controls.Add(this.tbNetto);
            this.gbSPLFilter.Controls.Add(this.label2);
            this.gbSPLFilter.Controls.Add(this.label5);
            this.gbSPLFilter.Controls.Add(this.tbAnzahl);
            this.gbSPLFilter.Controls.Add(this.label3);
            this.gbSPLFilter.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbSPLFilter.Location = new System.Drawing.Point(25, 54);
            this.gbSPLFilter.Name = "gbSPLFilter";
            this.gbSPLFilter.Size = new System.Drawing.Size(499, 112);
            this.gbSPLFilter.TabIndex = 157;
            this.gbSPLFilter.TabStop = false;
            this.gbSPLFilter.Text = "Auswahl Bestandsdaten";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(21, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Auftraggeber:";
            // 
            // cbAuftraggeber
            // 
            this.cbAuftraggeber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAuftraggeber.FormattingEnabled = true;
            this.cbAuftraggeber.Location = new System.Drawing.Point(117, 27);
            this.cbAuftraggeber.Name = "cbAuftraggeber";
            this.cbAuftraggeber.Size = new System.Drawing.Size(182, 21);
            this.cbAuftraggeber.TabIndex = 52;
            this.cbAuftraggeber.SelectedIndexChanged += new System.EventHandler(this.cbAuftraggeber_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(21, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Zeitraum von:";
            // 
            // dtpBis
            // 
            this.dtpBis.Location = new System.Drawing.Point(115, 80);
            this.dtpBis.Name = "dtpBis";
            this.dtpBis.Size = new System.Drawing.Size(184, 20);
            this.dtpBis.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(21, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Zeitraum bis:";
            // 
            // tbBrutto
            // 
            this.tbBrutto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBrutto.Location = new System.Drawing.Point(392, 79);
            this.tbBrutto.Name = "tbBrutto";
            this.tbBrutto.ReadOnly = true;
            this.tbBrutto.Size = new System.Drawing.Size(88, 20);
            this.tbBrutto.TabIndex = 153;
            this.tbBrutto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpVon
            // 
            this.dtpVon.Location = new System.Drawing.Point(115, 57);
            this.dtpVon.Name = "dtpVon";
            this.dtpVon.Size = new System.Drawing.Size(184, 20);
            this.dtpVon.TabIndex = 12;
            // 
            // tbNetto
            // 
            this.tbNetto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNetto.Location = new System.Drawing.Point(392, 56);
            this.tbNetto.Name = "tbNetto";
            this.tbNetto.ReadOnly = true;
            this.tbNetto.Size = new System.Drawing.Size(88, 20);
            this.tbNetto.TabIndex = 152;
            this.tbNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(328, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Anzahl:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(328, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Brutto [kg] :";
            // 
            // tbAnzahl
            // 
            this.tbAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAnzahl.Location = new System.Drawing.Point(392, 33);
            this.tbAnzahl.Name = "tbAnzahl";
            this.tbAnzahl.ReadOnly = true;
            this.tbAnzahl.Size = new System.Drawing.Size(88, 20);
            this.tbAnzahl.TabIndex = 151;
            this.tbAnzahl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(328, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Netto [kg] :";
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
            this.afColorLabel1.myText = "Sperrlagerbestand";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(741, 28);
            this.afColorLabel1.TabIndex = 8;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrSperrlager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvSPL);
            this.Controls.Add(this.afToolStrip2);
            this.Controls.Add(this.afMinMaxPanel1);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrSperrlager";
            this.Size = new System.Drawing.Size(741, 561);
            this.Load += new System.EventHandler(this.ctrSperrlager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPL.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSPL)).EndInit();
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.afMinMaxPanel1.ResumeLayout(false);
            this.afMinMaxPanel1.PerformLayout();
            this.gbSPLFilter.ResumeLayout(false);
            this.gbSPLFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Controls.AFMinMaxPanel afMinMaxPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpBis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpVon;
        public System.Windows.Forms.TextBox tbBrutto;
        public System.Windows.Forms.TextBox tbNetto;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbAnzahl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnSearch;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox tscbSearch;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbSearchArtikel;
        private System.Windows.Forms.GroupBox gbSPLFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbAuftraggeber;
        private System.Windows.Forms.ToolStripButton tsbtnCheckOut;
        private System.Windows.Forms.CheckBox cbReebookInOldEingang;
        private System.Windows.Forms.ToolStripButton tsbtnStartSearch;
        private System.Windows.Forms.ToolStripButton tsbtnRLToSL;
        private System.Windows.Forms.ToolStripButton tsbtnAuslagerung;
        private Telerik.WinControls.UI.RadGridView dgvSPL;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
    }
}
