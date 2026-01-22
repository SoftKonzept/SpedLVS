namespace Sped4
{
    partial class ctrRetoure
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.scMainRetoure = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.cbRetourAnzahl = new Telerik.WinControls.UI.RadCheckBox();
            this.lLieferant = new Telerik.WinControls.UI.RadLabel();
            this.tsmMain = new Sped4.Controls.AFToolStrip();
            this.tsbtnCreateRetoure = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCloseCtr = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tstbLVSSearch = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnLVSNrSearch = new System.Windows.Forms.ToolStripButton();
            this.tbAEmpf = new Telerik.WinControls.UI.RadTextBox();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.tsmArtikel = new Sped4.Controls.AFToolStrip();
            this.afCLRetoure = new Sped4.Controls.AFColorLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scMainRetoure)).BeginInit();
            this.scMainRetoure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbRetourAnzahl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lLieferant)).BeginInit();
            this.tsmMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAEmpf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // scMainRetoure
            // 
            this.scMainRetoure.Controls.Add(this.splitPanel1);
            this.scMainRetoure.Controls.Add(this.splitPanel2);
            this.scMainRetoure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMainRetoure.Location = new System.Drawing.Point(0, 34);
            this.scMainRetoure.Name = "scMainRetoure";
            this.scMainRetoure.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.scMainRetoure.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.scMainRetoure.Size = new System.Drawing.Size(549, 616);
            this.scMainRetoure.SplitterWidth = 10;
            this.scMainRetoure.TabIndex = 0;
            this.scMainRetoure.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.White;
            this.splitPanel1.Controls.Add(this.cbRetourAnzahl);
            this.splitPanel1.Controls.Add(this.lLieferant);
            this.splitPanel1.Controls.Add(this.tsmMain);
            this.splitPanel1.Controls.Add(this.tbAEmpf);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(549, 186);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.1935484F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -81);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // cbRetourAnzahl
            // 
            this.cbRetourAnzahl.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbRetourAnzahl.Location = new System.Drawing.Point(160, 150);
            this.cbRetourAnzahl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbRetourAnzahl.Name = "cbRetourAnzahl";
            this.cbRetourAnzahl.Size = new System.Drawing.Size(263, 23);
            this.cbRetourAnzahl.TabIndex = 145;
            this.cbRetourAnzahl.Text = "Einbuchung mehrerer Retoure-Artikel";
            // 
            // lLieferant
            // 
            this.lLieferant.Location = new System.Drawing.Point(16, 48);
            this.lLieferant.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lLieferant.Name = "lLieferant";
            this.lLieferant.Size = new System.Drawing.Size(133, 22);
            this.lLieferant.TabIndex = 144;
            this.lLieferant.Text = "Ausgang / Lieferant:";
            // 
            // tsmMain
            // 
            this.tsmMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsmMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnCreateRetoure,
            this.tsbtnCloseCtr,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tstbLVSSearch,
            this.tsbtnLVSNrSearch});
            this.tsmMain.Location = new System.Drawing.Point(0, 0);
            this.tsmMain.myColorFrom = System.Drawing.Color.Azure;
            this.tsmMain.myColorTo = System.Drawing.Color.Blue;
            this.tsmMain.myUnderlineColor = System.Drawing.Color.White;
            this.tsmMain.myUnderlined = true;
            this.tsmMain.Name = "tsmMain";
            this.tsmMain.Size = new System.Drawing.Size(549, 27);
            this.tsmMain.TabIndex = 143;
            this.tsmMain.Text = "afToolStrip3";
            // 
            // tsbtnCreateRetoure
            // 
            this.tsbtnCreateRetoure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCreateRetoure.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnCreateRetoure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCreateRetoure.Name = "tsbtnCreateRetoure";
            this.tsbtnCreateRetoure.Size = new System.Drawing.Size(29, 24);
            this.tsbtnCreateRetoure.Text = "Retoureneinlagerung durchführen";
            this.tsbtnCreateRetoure.Click += new System.EventHandler(this.tsbtnCreateRetoure_Click);
            // 
            // tsbtnCloseCtr
            // 
            this.tsbtnCloseCtr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCloseCtr.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnCloseCtr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCloseCtr.Name = "tsbtnCloseCtr";
            this.tsbtnCloseCtr.Size = new System.Drawing.Size(29, 24);
            this.tsbtnCloseCtr.Text = "toolStripButton2";
            this.tsbtnCloseCtr.Click += new System.EventHandler(this.tsbtnCloseCtr_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(60, 24);
            this.toolStripLabel1.Text = "LVS-Nr.:";
            // 
            // tstbLVSSearch
            // 
            this.tstbLVSSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstbLVSSearch.Name = "tstbLVSSearch";
            this.tstbLVSSearch.Size = new System.Drawing.Size(204, 27);
            // 
            // tsbtnLVSNrSearch
            // 
            this.tsbtnLVSNrSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnLVSNrSearch.Image = global::Sped4.Properties.Resources.selection_view_32x32;
            this.tsbtnLVSNrSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLVSNrSearch.Name = "tsbtnLVSNrSearch";
            this.tsbtnLVSNrSearch.Size = new System.Drawing.Size(29, 24);
            this.tsbtnLVSNrSearch.Text = "toolStripButton1";
            this.tsbtnLVSNrSearch.Click += new System.EventHandler(this.tsbtnLVSNrSearch_Click);
            // 
            // tbAEmpf
            // 
            this.tbAEmpf.AutoSize = false;
            this.tbAEmpf.Location = new System.Drawing.Point(160, 45);
            this.tbAEmpf.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbAEmpf.Multiline = true;
            this.tbAEmpf.Name = "tbAEmpf";
            this.tbAEmpf.ReadOnly = true;
            this.tbAEmpf.Size = new System.Drawing.Size(339, 98);
            this.tbAEmpf.TabIndex = 0;
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.Color.White;
            this.splitPanel2.Controls.Add(this.dgv);
            this.splitPanel2.Controls.Add(this.tsmArtikel);
            this.splitPanel2.Location = new System.Drawing.Point(0, 196);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel2.Size = new System.Drawing.Size(549, 420);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.1935484F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 81);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.Location = new System.Drawing.Point(0, 25);
            this.dgv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgv.MasterTemplate.AllowAddNewRow = false;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 240, 150);
            this.dgv.Size = new System.Drawing.Size(549, 395);
            this.dgv.TabIndex = 141;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellClick);
            // 
            // tsmArtikel
            // 
            this.tsmArtikel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsmArtikel.Location = new System.Drawing.Point(0, 0);
            this.tsmArtikel.myColorFrom = System.Drawing.Color.Azure;
            this.tsmArtikel.myColorTo = System.Drawing.Color.Blue;
            this.tsmArtikel.myUnderlineColor = System.Drawing.Color.White;
            this.tsmArtikel.myUnderlined = true;
            this.tsmArtikel.Name = "tsmArtikel";
            this.tsmArtikel.Size = new System.Drawing.Size(549, 25);
            this.tsmArtikel.TabIndex = 142;
            this.tsmArtikel.Text = "afToolStrip3";
            // 
            // afCLRetoure
            // 
            this.afCLRetoure.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afCLRetoure.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afCLRetoure.Dock = System.Windows.Forms.DockStyle.Top;
            this.afCLRetoure.Location = new System.Drawing.Point(0, 0);
            this.afCLRetoure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.afCLRetoure.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afCLRetoure.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afCLRetoure.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afCLRetoure.myText = "Retouren-Eingang";
            this.afCLRetoure.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afCLRetoure.myUnderlined = true;
            this.afCLRetoure.Name = "afCLRetoure";
            this.afCLRetoure.Size = new System.Drawing.Size(549, 34);
            this.afCLRetoure.TabIndex = 5;
            this.afCLRetoure.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ctrRetoure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMainRetoure);
            this.Controls.Add(this.afCLRetoure);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ctrRetoure";
            this.Size = new System.Drawing.Size(549, 650);
            this.Load += new System.EventHandler(this.ctrRetoure_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scMainRetoure)).EndInit();
            this.scMainRetoure.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbRetourAnzahl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lLieferant)).EndInit();
            this.tsmMain.ResumeLayout(false);
            this.tsmMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAEmpf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer scMainRetoure;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadGridView dgv;
        private Controls.AFToolStrip tsmMain;
        private Telerik.WinControls.UI.RadTextBox tbAEmpf;
        private Controls.AFToolStrip tsmArtikel;
        private System.Windows.Forms.ToolStripButton tsbtnCreateRetoure;
        private System.Windows.Forms.ToolStripButton tsbtnCloseCtr;
        private Controls.AFColorLabel afCLRetoure;
        private Telerik.WinControls.UI.RadLabel lLieferant;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tstbLVSSearch;
        private System.Windows.Forms.ToolStripButton tsbtnLVSNrSearch;
        private Telerik.WinControls.UI.RadCheckBox cbRetourAnzahl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
