namespace Sped4
{
    partial class ctrSearch
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
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.scSearchCtr = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnShowSearchCtr = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTabkeOver = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTabkeOverAusgang = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCreateAusgang = new System.Windows.Forms.ToolStripButton();
            this.tsbtnShowAll = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tscbSearch = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbSearchArtikel = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbView = new System.Windows.Forms.ToolStripComboBox();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSearchCtr)).BeginInit();
            this.scSearchCtr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.afToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 28);
            this.dgv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgv.MasterTemplate.AllowAddNewRow = false;
            this.dgv.MasterTemplate.AllowEditRow = false;
            this.dgv.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgv.MasterTemplate.EnableGrouping = false;
            this.dgv.MasterTemplate.ShowFilteringRow = false;
            this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 240, 150);
            this.dgv.ShowGroupPanel = false;
            this.dgv.ShowHeaderCellButtons = true;
            this.dgv.Size = new System.Drawing.Size(1043, 564);
            this.dgv.TabIndex = 23;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.dgv_RowFormatting);
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            this.dgv.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.dgv_ToolTipTextNeeded);
            // 
            // scSearchCtr
            // 
            this.scSearchCtr.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.scSearchCtr.Controls.Add(this.splitPanel1);
            this.scSearchCtr.Controls.Add(this.splitPanel2);
            this.scSearchCtr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSearchCtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scSearchCtr.Location = new System.Drawing.Point(0, 34);
            this.scSearchCtr.Name = "scSearchCtr";
            // 
            // 
            // 
            this.scSearchCtr.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 28, 200, 200);
            this.scSearchCtr.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.scSearchCtr.Size = new System.Drawing.Size(1547, 592);
            this.scSearchCtr.SplitterWidth = 5;
            this.scSearchCtr.TabIndex = 24;
            this.scSearchCtr.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(499, 592);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.1764706F, 0F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-131, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            this.splitPanel1.ThemeName = "ControlDefault";
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel2.Controls.Add(this.dgv);
            this.splitPanel2.Controls.Add(this.afToolStrip2);
            this.splitPanel2.Location = new System.Drawing.Point(504, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.ControlBounds = new System.Drawing.Rectangle(378, 0, 200, 200);
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel2.Size = new System.Drawing.Size(1043, 592);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.1764706F, 0F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(131, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnShowSearchCtr,
            this.tsbtnTabkeOver,
            this.tsbtnTabkeOverAusgang,
            this.tsbtnCreateAusgang,
            this.tsbtnShowAll,
            this.tsbtnClose,
            this.toolStripSeparator2,
            this.tscbSearch,
            this.toolStripLabel1,
            this.tstbSearchArtikel,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.tscbView});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(1043, 28);
            this.afToolStrip2.TabIndex = 20;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnShowSearchCtr
            // 
            this.tsbtnShowSearchCtr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnShowSearchCtr.Image = global::Sped4.Properties.Resources.layout;
            this.tsbtnShowSearchCtr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnShowSearchCtr.Name = "tsbtnShowSearchCtr";
            this.tsbtnShowSearchCtr.Size = new System.Drawing.Size(29, 25);
            this.tsbtnShowSearchCtr.Text = "Sucheingabe öffnen";
            this.tsbtnShowSearchCtr.Click += new System.EventHandler(this.tsbtnShowSearchCtr_Click);
            // 
            // tsbtnTabkeOver
            // 
            this.tsbtnTabkeOver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTabkeOver.Image = global::Sped4.Properties.Resources.box_into_24x24;
            this.tsbtnTabkeOver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTabkeOver.Name = "tsbtnTabkeOver";
            this.tsbtnTabkeOver.Size = new System.Drawing.Size(29, 25);
            this.tsbtnTabkeOver.Text = "ausgewählten Artikel im Eingang anzeigen";
            this.tsbtnTabkeOver.Click += new System.EventHandler(this.tsbtnTabkeOver_Click);
            // 
            // tsbtnTabkeOverAusgang
            // 
            this.tsbtnTabkeOverAusgang.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTabkeOverAusgang.Image = global::Sped4.Properties.Resources.box_out_24x24;
            this.tsbtnTabkeOverAusgang.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTabkeOverAusgang.Name = "tsbtnTabkeOverAusgang";
            this.tsbtnTabkeOverAusgang.Size = new System.Drawing.Size(29, 25);
            this.tsbtnTabkeOverAusgang.Text = "ausgewählten Artikel übernehmen (Ausgang)";
            this.tsbtnTabkeOverAusgang.ToolTipText = "ausgewählten Artikel in Ausgang anzeigen";
            this.tsbtnTabkeOverAusgang.Click += new System.EventHandler(this.tsbtnTabkeOverAusgang_Click);
            // 
            // tsbtnCreateAusgang
            // 
            this.tsbtnCreateAusgang.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCreateAusgang.Image = global::Sped4.Properties.Resources.preferences1;
            this.tsbtnCreateAusgang.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCreateAusgang.Name = "tsbtnCreateAusgang";
            this.tsbtnCreateAusgang.Size = new System.Drawing.Size(29, 25);
            this.tsbtnCreateAusgang.Text = "Ausgang erstellen und abschließen";
            this.tsbtnCreateAusgang.Click += new System.EventHandler(this.tsbtnCreateAusgang_Click);
            // 
            // tsbtnShowAll
            // 
            this.tsbtnShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnShowAll.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.tsbtnShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnShowAll.Name = "tsbtnShowAll";
            this.tsbtnShowAll.Size = new System.Drawing.Size(29, 25);
            this.tsbtnShowAll.Text = "Filter löschen - alle Artikel anzeigen";
            this.tsbtnShowAll.Click += new System.EventHandler(this.tsbtnShowAll_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(29, 25);
            this.tsbtnClose.Text = "Suche schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // tscbSearch
            // 
            this.tscbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tscbSearch.Name = "tscbSearch";
            this.tscbSearch.Size = new System.Drawing.Size(279, 28);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(88, 25);
            this.toolStripLabel1.Text = "Suchbegriff:";
            // 
            // tstbSearchArtikel
            // 
            this.tstbSearchArtikel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstbSearchArtikel.Name = "tstbSearchArtikel";
            this.tstbSearchArtikel.Size = new System.Drawing.Size(232, 28);
            this.tstbSearchArtikel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tstbSearchArtikel_KeyPress);
            this.tstbSearchArtikel.TextChanged += new System.EventHandler(this.tstbSearchArtikel_TextChanged);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Sped4.Properties.Resources.selection_view_32x32;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 25);
            this.toolStripButton1.Text = "Suche starten";
            this.toolStripButton1.ToolTipText = "Suche starten";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(57, 25);
            this.toolStripLabel2.Text = "Ansicht";
            // 
            // tscbView
            // 
            this.tscbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tscbView.Name = "tscbView";
            this.tscbView.Size = new System.Drawing.Size(121, 28);
            this.tscbView.SelectedIndexChanged += new System.EventHandler(this.tscbView_SelectedIndexChanged);
            // 
            // afColorLabel1
            // 
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afColorLabel1.Location = new System.Drawing.Point(0, 0);
            this.afColorLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.afColorLabel1.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afColorLabel1.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afColorLabel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afColorLabel1.myText = "Suche ...";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(1547, 34);
            this.afColorLabel1.TabIndex = 6;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scSearchCtr);
            this.Controls.Add(this.afColorLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrSearch";
            this.Size = new System.Drawing.Size(1547, 626);
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSearchCtr)).EndInit();
            this.scSearchCtr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitPanel2.PerformLayout();
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnShowAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox tscbSearch;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.ToolStripButton tsbtnTabkeOver;
        private Telerik.WinControls.UI.RadSplitContainer scSearchCtr;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private System.Windows.Forms.ToolStripButton tsbtnShowSearchCtr;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscbView;
        private Telerik.WinControls.UI.RadGridView dgv;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripTextBox tstbSearchArtikel;
        private System.Windows.Forms.ToolStripButton tsbtnTabkeOverAusgang;
        private System.Windows.Forms.ToolStripButton tsbtnCreateAusgang;
    }
}
