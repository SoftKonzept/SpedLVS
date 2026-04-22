namespace Sped4
{
    partial class ctrLieferEinteilung
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrLieferEinteilung));
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.scLET = new System.Windows.Forms.SplitContainer();
            this.dgvSelect = new Telerik.WinControls.UI.RadGridView();
            this.dgvLET = new Telerik.WinControls.UI.RadGridView();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnGridSelectShow = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMail = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.dtpVon = new System.Windows.Forms.DateTimePicker();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.scLET)).BeginInit();
            this.scLET.Panel1.SuspendLayout();
            this.scLET.Panel2.SuspendLayout();
            this.scLET.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelect.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLET)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLET.MasterTemplate)).BeginInit();
            this.afToolStrip2.SuspendLayout();
            this.SuspendLayout();
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
            this.afColorLabel1.myText = "Liefereinteilung";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(923, 34);
            this.afColorLabel1.TabIndex = 11;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.BackColor = System.Drawing.Color.White;
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 34);
            this.afMinMaxPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Optionen";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(923, 39);
            this.afMinMaxPanel1.TabIndex = 12;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            // 
            // scLET
            // 
            this.scLET.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLET.Location = new System.Drawing.Point(0, 122);
            this.scLET.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scLET.Name = "scLET";
            // 
            // scLET.Panel1
            // 
            this.scLET.Panel1.Controls.Add(this.dgvSelect);
            // 
            // scLET.Panel2
            // 
            this.scLET.Panel2.Controls.Add(this.dgvLET);
            this.scLET.Size = new System.Drawing.Size(923, 481);
            this.scLET.SplitterDistance = 226;
            this.scLET.SplitterWidth = 5;
            this.scLET.TabIndex = 13;
            // 
            // dgvSelect
            // 
            this.dgvSelect.BackColor = System.Drawing.Color.White;
            this.dgvSelect.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelect.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvSelect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvSelect.Location = new System.Drawing.Point(0, 0);
            this.dgvSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgvSelect.MasterTemplate.AllowAddNewRow = false;
            this.dgvSelect.MasterTemplate.AllowDeleteRow = false;
            this.dgvSelect.MasterTemplate.AllowDragToGroup = false;
            this.dgvSelect.MasterTemplate.AllowEditRow = false;
            this.dgvSelect.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvSelect.MasterTemplate.EnableFiltering = true;
            this.dgvSelect.MasterTemplate.ShowFilteringRow = false;
            this.dgvSelect.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvSelect.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvSelect.Name = "dgvSelect";
            this.dgvSelect.ReadOnly = true;
            this.dgvSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvSelect.ShowGroupPanel = false;
            this.dgvSelect.ShowHeaderCellButtons = true;
            this.dgvSelect.Size = new System.Drawing.Size(226, 481);
            this.dgvSelect.TabIndex = 26;
            this.dgvSelect.ThemeName = "ControlDefault";
            this.dgvSelect.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvSelect_CellClick);
            this.dgvSelect.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvSelect_CellDoubleClick);
            // 
            // dgvLET
            // 
            this.dgvLET.BackColor = System.Drawing.Color.White;
            this.dgvLET.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvLET.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLET.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvLET.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvLET.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvLET.Location = new System.Drawing.Point(0, 0);
            this.dgvLET.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgvLET.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvLET.MasterTemplate.EnableFiltering = true;
            this.dgvLET.MasterTemplate.ShowFilteringRow = false;
            this.dgvLET.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvLET.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgvLET.Name = "dgvLET";
            this.dgvLET.ReadOnly = true;
            this.dgvLET.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvLET.ShowGroupPanel = false;
            this.dgvLET.ShowHeaderCellButtons = true;
            this.dgvLET.Size = new System.Drawing.Size(692, 481);
            this.dgvLET.TabIndex = 27;
            this.dgvLET.ThemeName = "ControlDefault";
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnGridSelectShow,
            this.tsbtnRefresh,
            this.tsbtnPrint,
            this.tsbtnExcel,
            this.tsbtnMail,
            this.tsbtnClose});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 73);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(923, 27);
            this.afToolStrip2.TabIndex = 24;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnGridSelectShow
            // 
            this.tsbtnGridSelectShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGridSelectShow.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnGridSelectShow.Image")));
            this.tsbtnGridSelectShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGridSelectShow.Name = "tsbtnGridSelectShow";
            this.tsbtnGridSelectShow.Size = new System.Drawing.Size(29, 24);
            this.tsbtnGridSelectShow.Text = "Sucheingabe öffnen";
            this.tsbtnGridSelectShow.Click += new System.EventHandler(this.tsbtnGridSelectShow_Click);
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(29, 24);
            this.tsbtnRefresh.Text = "alle Vorgaben zurücksetzen";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrint.Image")));
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(29, 24);
            this.tsbtnPrint.Text = "Bestand drucken";
            this.tsbtnPrint.Visible = false;
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // tsbtnExcel
            // 
            this.tsbtnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExcel.Image = global::Sped4.Properties.Resources.Excel;
            this.tsbtnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExcel.Name = "tsbtnExcel";
            this.tsbtnExcel.Size = new System.Drawing.Size(29, 24);
            this.tsbtnExcel.Text = "Export zu Excel";
            this.tsbtnExcel.Click += new System.EventHandler(this.tsbtnExcel_Click);
            // 
            // tsbtnMail
            // 
            this.tsbtnMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnMail.Image = global::Sped4.Properties.Resources.mail_forward_24x24;
            this.tsbtnMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMail.Name = "tsbtnMail";
            this.tsbtnMail.Size = new System.Drawing.Size(29, 24);
            this.tsbtnMail.Text = "Bestand per Mail versenden";
            this.tsbtnMail.Click += new System.EventHandler(this.tsbtnMail_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClose.Image")));
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(29, 24);
            this.tsbtnClose.Text = "Suche schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // dtpVon
            // 
            this.dtpVon.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpVon.Location = new System.Drawing.Point(0, 100);
            this.dtpVon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpVon.Name = "dtpVon";
            this.dtpVon.Size = new System.Drawing.Size(923, 22);
            this.dtpVon.TabIndex = 25;
            // 
            // ctrLieferEinteilung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scLET);
            this.Controls.Add(this.dtpVon);
            this.Controls.Add(this.afToolStrip2);
            this.Controls.Add(this.afMinMaxPanel1);
            this.Controls.Add(this.afColorLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrLieferEinteilung";
            this.Size = new System.Drawing.Size(923, 603);
            this.Load += new System.EventHandler(this.ctrLieferEinteilung_Load);
            this.scLET.Panel1.ResumeLayout(false);
            this.scLET.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scLET)).EndInit();
            this.scLET.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelect.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLET.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLET)).EndInit();
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Controls.AFColorLabel afColorLabel1;
        private Controls.AFMinMaxPanel afMinMaxPanel1;
        private System.Windows.Forms.SplitContainer scLET;
        public Telerik.WinControls.UI.RadGridView dgvSelect;
        public Telerik.WinControls.UI.RadGridView dgvLET;
        private Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnGridSelectShow;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsbtnExcel;
        private System.Windows.Forms.ToolStripButton tsbtnMail;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.DateTimePicker dtpVon;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
