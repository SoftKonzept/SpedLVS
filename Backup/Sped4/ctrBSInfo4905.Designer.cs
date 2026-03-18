namespace Sped4
{
    partial class ctrBSInfo4905
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrBSInfo4905));
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.BSWorker = new System.ComponentModel.BackgroundWorker();
            this.mMain = new Sped4.Controls.AFToolStrip();
            this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMail = new System.Windows.Forms.ToolStripButton();
            this.tsbtnIntegrateCtr = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCtrInWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.tsbar4905 = new System.Windows.Forms.ToolStripProgressBar();
            this.tslVDA4905Info = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnReSizeGrid = new System.Windows.Forms.ToolStripButton();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.cbOnlyVDA4984 = new System.Windows.Forms.CheckBox();
            this.cbActivGut = new System.Windows.Forms.CheckBox();
            this.cbInclSPL = new System.Windows.Forms.CheckBox();
            this.cbChecked = new System.Windows.Forms.CheckBox();
            this.cbRuckstand = new System.Windows.Forms.CheckBox();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.mMain.SuspendLayout();
            this.afMinMaxPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AutoScroll = true;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EnableGestures = false;
            this.dgv.EnableTheming = false;
            this.dgv.Location = new System.Drawing.Point(0, 113);
            this.dgv.Margin = new System.Windows.Forms.Padding(5);
            // 
            // 
            // 
            this.dgv.MasterTemplate.EnableFiltering = true;
            this.dgv.MasterTemplate.EnableGrouping = false;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.ShowGroupPanel = false;
            this.dgv.Size = new System.Drawing.Size(879, 377);
            this.dgv.TabIndex = 25;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgv.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.dgv_ContextMenuOpening);
            // 
            // BSWorker
            // 
            this.BSWorker.WorkerSupportsCancellation = true;
            this.BSWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BSWorker_DoWork);
            // 
            // mMain
            // 
            this.mMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSearch,
            this.toolStripSeparator1,
            this.tsbtnPrint,
            this.tsbtnExcel,
            this.tsbtnMail,
            this.tsbtnIntegrateCtr,
            this.tsbtnCtrInWindow,
            this.tsbtnClose,
            this.tsbar4905,
            this.tslVDA4905Info,
            this.toolStripSeparator2,
            this.tsbtnReSizeGrid});
            this.mMain.Location = new System.Drawing.Point(0, 86);
            this.mMain.myColorFrom = System.Drawing.Color.Azure;
            this.mMain.myColorTo = System.Drawing.Color.Blue;
            this.mMain.myUnderlineColor = System.Drawing.Color.White;
            this.mMain.myUnderlined = true;
            this.mMain.Name = "mMain";
            this.mMain.Size = new System.Drawing.Size(879, 27);
            this.mMain.TabIndex = 24;
            // 
            // tsbtnSearch
            // 
            this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSearch.Image = global::Sped4.Properties.Resources.selection_view_32x32;
            this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearch.Name = "tsbtnSearch";
            this.tsbtnSearch.Size = new System.Drawing.Size(24, 24);
            this.tsbtnSearch.Text = "Bestandsinformationen ermitteln";
            this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrint.Image")));
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(24, 24);
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
            this.tsbtnExcel.Size = new System.Drawing.Size(24, 24);
            this.tsbtnExcel.Text = "Export zu Excel";
            this.tsbtnExcel.Click += new System.EventHandler(this.tsbtnExcel_Click);
            // 
            // tsbtnMail
            // 
            this.tsbtnMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnMail.Image = global::Sped4.Properties.Resources.mail_forward_24x24;
            this.tsbtnMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMail.Name = "tsbtnMail";
            this.tsbtnMail.Size = new System.Drawing.Size(24, 24);
            this.tsbtnMail.Text = "Bestand per Mail versenden";
            this.tsbtnMail.Visible = false;
            // 
            // tsbtnIntegrateCtr
            // 
            this.tsbtnIntegrateCtr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnIntegrateCtr.Image = global::Sped4.Properties.Resources.window_split_hor;
            this.tsbtnIntegrateCtr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnIntegrateCtr.Name = "tsbtnIntegrateCtr";
            this.tsbtnIntegrateCtr.Size = new System.Drawing.Size(24, 24);
            this.tsbtnIntegrateCtr.Text = "Fenster zurück ins Hauptfenster integrieren";
            this.tsbtnIntegrateCtr.Visible = false;
            this.tsbtnIntegrateCtr.Click += new System.EventHandler(this.tsbtnIntegrateCtr_Click);
            // 
            // tsbtnCtrInWindow
            // 
            this.tsbtnCtrInWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCtrInWindow.Image = global::Sped4.Properties.Resources.windows;
            this.tsbtnCtrInWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCtrInWindow.Name = "tsbtnCtrInWindow";
            this.tsbtnCtrInWindow.Size = new System.Drawing.Size(24, 24);
            this.tsbtnCtrInWindow.Text = "separates Fenster öffnen";
            this.tsbtnCtrInWindow.Visible = false;
            this.tsbtnCtrInWindow.Click += new System.EventHandler(this.tsbtnCtrInWindow_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClose.Image")));
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(24, 24);
            this.tsbtnClose.Text = "Suche schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // tsbar4905
            // 
            this.tsbar4905.Maximum = 10;
            this.tsbar4905.Name = "tsbar4905";
            this.tsbar4905.Size = new System.Drawing.Size(200, 24);
            this.tsbar4905.Step = 1;
            // 
            // tslVDA4905Info
            // 
            this.tslVDA4905Info.Name = "tslVDA4905Info";
            this.tslVDA4905Info.Size = new System.Drawing.Size(28, 24);
            this.tslVDA4905Info.Text = "Info";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbtnReSizeGrid
            // 
            this.tsbtnReSizeGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnReSizeGrid.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnReSizeGrid.Image")));
            this.tsbtnReSizeGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnReSizeGrid.Name = "tsbtnReSizeGrid";
            this.tsbtnReSizeGrid.Size = new System.Drawing.Size(24, 24);
            this.tsbtnReSizeGrid.Text = "toolStripButton1";
            this.tsbtnReSizeGrid.Visible = false;
            this.tsbtnReSizeGrid.Click += new System.EventHandler(this.tsbtnReSizeGrid_Click);
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.BackColor = System.Drawing.Color.White;
            this.afMinMaxPanel1.Controls.Add(this.cbOnlyVDA4984);
            this.afMinMaxPanel1.Controls.Add(this.cbActivGut);
            this.afMinMaxPanel1.Controls.Add(this.cbInclSPL);
            this.afMinMaxPanel1.Controls.Add(this.cbChecked);
            this.afMinMaxPanel1.Controls.Add(this.cbRuckstand);
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 28);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Optionen";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(879, 58);
            this.afMinMaxPanel1.TabIndex = 12;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            // 
            // cbOnlyVDA4984
            // 
            this.cbOnlyVDA4984.AutoSize = true;
            this.cbOnlyVDA4984.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbOnlyVDA4984.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbOnlyVDA4984.Location = new System.Drawing.Point(479, 28);
            this.cbOnlyVDA4984.Name = "cbOnlyVDA4984";
            this.cbOnlyVDA4984.Size = new System.Drawing.Size(88, 17);
            this.cbOnlyVDA4984.TabIndex = 8;
            this.cbOnlyVDA4984.Text = "nur VDA4905";
            this.cbOnlyVDA4984.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cbOnlyVDA4984.UseVisualStyleBackColor = true;
            this.cbOnlyVDA4984.Visible = false;
            this.cbOnlyVDA4984.CheckedChanged += new System.EventHandler(this.cbOnlyVDA4905_CheckedChanged);
            // 
            // cbActivGut
            // 
            this.cbActivGut.AutoSize = true;
            this.cbActivGut.Checked = true;
            this.cbActivGut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActivGut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbActivGut.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbActivGut.Location = new System.Drawing.Point(349, 28);
            this.cbActivGut.Name = "cbActivGut";
            this.cbActivGut.Size = new System.Drawing.Size(124, 17);
            this.cbActivGut.TabIndex = 7;
            this.cbActivGut.Text = "nur aktive Güterarten";
            this.cbActivGut.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cbActivGut.UseVisualStyleBackColor = true;
            // 
            // cbInclSPL
            // 
            this.cbInclSPL.AutoSize = true;
            this.cbInclSPL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbInclSPL.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbInclSPL.Location = new System.Drawing.Point(249, 28);
            this.cbInclSPL.Name = "cbInclSPL";
            this.cbInclSPL.Size = new System.Drawing.Size(94, 17);
            this.cbInclSPL.TabIndex = 6;
            this.cbInclSPL.Text = "incl. Sperrlager";
            this.cbInclSPL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cbInclSPL.UseVisualStyleBackColor = true;
            // 
            // cbChecked
            // 
            this.cbChecked.AutoSize = true;
            this.cbChecked.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbChecked.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbChecked.Location = new System.Drawing.Point(184, 28);
            this.cbChecked.Name = "cbChecked";
            this.cbChecked.Size = new System.Drawing.Size(59, 17);
            this.cbChecked.TabIndex = 5;
            this.cbChecked.Text = "Geprüft";
            this.cbChecked.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cbChecked.UseVisualStyleBackColor = true;
            // 
            // cbRuckstand
            // 
            this.cbRuckstand.AutoSize = true;
            this.cbRuckstand.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbRuckstand.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbRuckstand.Location = new System.Drawing.Point(40, 28);
            this.cbRuckstand.Name = "cbRuckstand";
            this.cbRuckstand.Size = new System.Drawing.Size(138, 17);
            this.cbRuckstand.TabIndex = 2;
            this.cbRuckstand.Text = "Rückstand einschließen";
            this.cbRuckstand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cbRuckstand.UseVisualStyleBackColor = true;
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
            this.afColorLabel1.myText = "Bestandsinformationen ";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(879, 28);
            this.afColorLabel1.TabIndex = 11;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrBSInfo4905
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.mMain);
            this.Controls.Add(this.afMinMaxPanel1);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrBSInfo4905";
            this.Size = new System.Drawing.Size(879, 490);
            this.Load += new System.EventHandler(this.ctrBSInfo4905_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.mMain.ResumeLayout(false);
            this.mMain.PerformLayout();
            this.afMinMaxPanel1.ResumeLayout(false);
            this.afMinMaxPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Controls.AFColorLabel afColorLabel1;
        private Controls.AFMinMaxPanel afMinMaxPanel1;
        private Controls.AFToolStrip mMain;
        private System.Windows.Forms.ToolStripButton tsbtnSearch;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsbtnExcel;
        private System.Windows.Forms.ToolStripButton tsbtnMail;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Telerik.WinControls.UI.RadGridView dgv;
        private System.Windows.Forms.CheckBox cbRuckstand;
        private System.Windows.Forms.CheckBox cbChecked;
        private System.Windows.Forms.CheckBox cbInclSPL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripProgressBar tsbar4905;
        private System.Windows.Forms.ToolStripLabel tslVDA4905Info;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnIntegrateCtr;
        private System.Windows.Forms.ToolStripButton tsbtnCtrInWindow;
        private System.Windows.Forms.CheckBox cbActivGut;
        private System.ComponentModel.BackgroundWorker BSWorker;
        private System.Windows.Forms.ToolStripButton tsbtnReSizeGrid;
        private System.Windows.Forms.CheckBox cbOnlyVDA4984;
    }
}
