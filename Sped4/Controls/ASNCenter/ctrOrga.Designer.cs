namespace Sped4.Controls.ASNCenter
{
    partial class ctrOrga
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrOrga));
            this.scOrgaMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.panOrgaList = new Telerik.WinControls.UI.SplitPanel();
            this.dgvOrga = new Telerik.WinControls.UI.RadGridView();
            this.menuASNMain = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefreshAdrVerweis = new System.Windows.Forms.ToolStripButton();
            this.panOrgaEdit = new Telerik.WinControls.UI.SplitPanel();
            this.lOrgaId = new System.Windows.Forms.Label();
            this.tbOrgaId = new System.Windows.Forms.TextBox();
            this.nudSendeIdNew = new System.Windows.Forms.NumericUpDown();
            this.nudSendeIdOld = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cbAktiv = new System.Windows.Forms.CheckBox();
            this.menuVerweisEdit = new Sped4.Controls.AFToolStrip();
            this.tsbtnOrgaNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOrgaSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOrgaDelete = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scOrgaMain)).BeginInit();
            this.scOrgaMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panOrgaList)).BeginInit();
            this.panOrgaList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrga)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrga.MasterTemplate)).BeginInit();
            this.menuASNMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panOrgaEdit)).BeginInit();
            this.panOrgaEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSendeIdNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSendeIdOld)).BeginInit();
            this.menuVerweisEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // scOrgaMain
            // 
            this.scOrgaMain.Controls.Add(this.panOrgaList);
            this.scOrgaMain.Controls.Add(this.panOrgaEdit);
            this.scOrgaMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scOrgaMain.Location = new System.Drawing.Point(0, 0);
            this.scOrgaMain.Name = "scOrgaMain";
            // 
            // 
            // 
            this.scOrgaMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scOrgaMain.Size = new System.Drawing.Size(754, 432);
            this.scOrgaMain.SplitterWidth = 8;
            this.scOrgaMain.TabIndex = 2;
            this.scOrgaMain.TabStop = false;
            // 
            // panOrgaList
            // 
            this.panOrgaList.Controls.Add(this.dgvOrga);
            this.panOrgaList.Controls.Add(this.menuASNMain);
            this.panOrgaList.Location = new System.Drawing.Point(0, 0);
            this.panOrgaList.Name = "panOrgaList";
            // 
            // 
            // 
            this.panOrgaList.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panOrgaList.Size = new System.Drawing.Size(377, 432);
            this.panOrgaList.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.005603969F, 0F);
            this.panOrgaList.SizeInfo.SplitterCorrection = new System.Drawing.Size(38, 0);
            this.panOrgaList.TabIndex = 0;
            this.panOrgaList.TabStop = false;
            this.panOrgaList.Text = "splitPanel1";
            // 
            // dgvOrga
            // 
            this.dgvOrga.BackColor = System.Drawing.Color.White;
            this.dgvOrga.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvOrga.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrga.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvOrga.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvOrga.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvOrga.Location = new System.Drawing.Point(0, 25);
            // 
            // 
            // 
            this.dgvOrga.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvOrga.MasterTemplate.EnableFiltering = true;
            this.dgvOrga.MasterTemplate.ShowFilteringRow = false;
            this.dgvOrga.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvOrga.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvOrga.Name = "dgvOrga";
            this.dgvOrga.ReadOnly = true;
            this.dgvOrga.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvOrga.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 240, 150);
            this.dgvOrga.ShowHeaderCellButtons = true;
            this.dgvOrga.Size = new System.Drawing.Size(377, 407);
            this.dgvOrga.TabIndex = 27;
            this.dgvOrga.ThemeName = "ControlDefault";
            this.dgvOrga.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvOrga_MouseClick);
            this.dgvOrga.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvOrga_MouseDoubleClick);
            // 
            // menuASNMain
            // 
            this.menuASNMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefreshAdrVerweis});
            this.menuASNMain.Location = new System.Drawing.Point(0, 0);
            this.menuASNMain.myColorFrom = System.Drawing.Color.Azure;
            this.menuASNMain.myColorTo = System.Drawing.Color.Blue;
            this.menuASNMain.myUnderlineColor = System.Drawing.Color.White;
            this.menuASNMain.myUnderlined = true;
            this.menuASNMain.Name = "menuASNMain";
            this.menuASNMain.Size = new System.Drawing.Size(377, 25);
            this.menuASNMain.TabIndex = 9;
            this.menuASNMain.Text = "afToolStrip1";
            // 
            // tsbtnRefreshAdrVerweis
            // 
            this.tsbtnRefreshAdrVerweis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefreshAdrVerweis.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefreshAdrVerweis.Image")));
            this.tsbtnRefreshAdrVerweis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefreshAdrVerweis.Name = "tsbtnRefreshAdrVerweis";
            this.tsbtnRefreshAdrVerweis.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefreshAdrVerweis.Text = "aktualisieren";
            this.tsbtnRefreshAdrVerweis.Click += new System.EventHandler(this.tsbtnRefreshAdrVerweis_Click);
            // 
            // panOrgaEdit
            // 
            this.panOrgaEdit.AutoScroll = true;
            this.panOrgaEdit.BackColor = System.Drawing.Color.White;
            this.panOrgaEdit.Controls.Add(this.lOrgaId);
            this.panOrgaEdit.Controls.Add(this.tbOrgaId);
            this.panOrgaEdit.Controls.Add(this.nudSendeIdNew);
            this.panOrgaEdit.Controls.Add(this.nudSendeIdOld);
            this.panOrgaEdit.Controls.Add(this.label2);
            this.panOrgaEdit.Controls.Add(this.label1);
            this.panOrgaEdit.Controls.Add(this.label26);
            this.panOrgaEdit.Controls.Add(this.cbAktiv);
            this.panOrgaEdit.Controls.Add(this.menuVerweisEdit);
            this.panOrgaEdit.Location = new System.Drawing.Point(385, 0);
            this.panOrgaEdit.Name = "panOrgaEdit";
            // 
            // 
            // 
            this.panOrgaEdit.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panOrgaEdit.Size = new System.Drawing.Size(369, 432);
            this.panOrgaEdit.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.005603999F, 0F);
            this.panOrgaEdit.SizeInfo.SplitterCorrection = new System.Drawing.Size(-38, 0);
            this.panOrgaEdit.TabIndex = 1;
            this.panOrgaEdit.TabStop = false;
            this.panOrgaEdit.Text = "splitPanel2";
            // 
            // lOrgaId
            // 
            this.lOrgaId.AutoSize = true;
            this.lOrgaId.ForeColor = System.Drawing.Color.DarkBlue;
            this.lOrgaId.Location = new System.Drawing.Point(22, 51);
            this.lOrgaId.Name = "lOrgaId";
            this.lOrgaId.Size = new System.Drawing.Size(49, 13);
            this.lOrgaId.TabIndex = 217;
            this.lOrgaId.Text = "Orga Id:";
            // 
            // tbOrgaId
            // 
            this.tbOrgaId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOrgaId.Location = new System.Drawing.Point(120, 47);
            this.tbOrgaId.Name = "tbOrgaId";
            this.tbOrgaId.Size = new System.Drawing.Size(109, 20);
            this.tbOrgaId.TabIndex = 216;
            // 
            // nudSendeIdNew
            // 
            this.nudSendeIdNew.Location = new System.Drawing.Point(121, 127);
            this.nudSendeIdNew.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSendeIdNew.Name = "nudSendeIdNew";
            this.nudSendeIdNew.Size = new System.Drawing.Size(120, 20);
            this.nudSendeIdNew.TabIndex = 215;
            // 
            // nudSendeIdOld
            // 
            this.nudSendeIdOld.Location = new System.Drawing.Point(121, 93);
            this.nudSendeIdOld.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSendeIdOld.Name = "nudSendeIdOld";
            this.nudSendeIdOld.Size = new System.Drawing.Size(120, 20);
            this.nudSendeIdOld.TabIndex = 214;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(23, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 213;
            this.label2.Text = "Send-Id Old:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(23, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 212;
            this.label1.Text = "Send-Id New:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.DarkBlue;
            this.label26.Location = new System.Drawing.Point(23, 71);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(39, 13);
            this.label26.TabIndex = 203;
            this.label26.Text = "Status";
            // 
            // cbAktiv
            // 
            this.cbAktiv.AutoSize = true;
            this.cbAktiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAktiv.Location = new System.Drawing.Point(121, 70);
            this.cbAktiv.Name = "cbAktiv";
            this.cbAktiv.Size = new System.Drawing.Size(47, 17);
            this.cbAktiv.TabIndex = 202;
            this.cbAktiv.Text = "aktiv";
            this.cbAktiv.UseVisualStyleBackColor = true;
            // 
            // menuVerweisEdit
            // 
            this.menuVerweisEdit.AutoSize = false;
            this.menuVerweisEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOrgaNew,
            this.tsbtnOrgaSave,
            this.tsbtnOrgaDelete});
            this.menuVerweisEdit.Location = new System.Drawing.Point(0, 0);
            this.menuVerweisEdit.myColorFrom = System.Drawing.Color.Azure;
            this.menuVerweisEdit.myColorTo = System.Drawing.Color.Blue;
            this.menuVerweisEdit.myUnderlineColor = System.Drawing.Color.White;
            this.menuVerweisEdit.myUnderlined = true;
            this.menuVerweisEdit.Name = "menuVerweisEdit";
            this.menuVerweisEdit.Size = new System.Drawing.Size(369, 31);
            this.menuVerweisEdit.TabIndex = 83;
            this.menuVerweisEdit.Text = "afToolStrip1";
            // 
            // tsbtnOrgaNew
            // 
            this.tsbtnOrgaNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOrgaNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOrgaNew.Image")));
            this.tsbtnOrgaNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOrgaNew.Name = "tsbtnOrgaNew";
            this.tsbtnOrgaNew.Size = new System.Drawing.Size(23, 28);
            this.tsbtnOrgaNew.Text = "Neuen";
            this.tsbtnOrgaNew.Click += new System.EventHandler(this.tsbtnOrgaNew_Click);
            // 
            // tsbtnOrgaSave
            // 
            this.tsbtnOrgaSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOrgaSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOrgaSave.Image")));
            this.tsbtnOrgaSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOrgaSave.Name = "tsbtnOrgaSave";
            this.tsbtnOrgaSave.Size = new System.Drawing.Size(23, 28);
            this.tsbtnOrgaSave.Text = "Verweis speichern";
            this.tsbtnOrgaSave.Click += new System.EventHandler(this.tsbtnOrgaSave_Click);
            // 
            // tsbtnOrgaDelete
            // 
            this.tsbtnOrgaDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOrgaDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnOrgaDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOrgaDelete.Name = "tsbtnOrgaDelete";
            this.tsbtnOrgaDelete.Size = new System.Drawing.Size(23, 28);
            this.tsbtnOrgaDelete.Text = "schliessen";
            this.tsbtnOrgaDelete.Click += new System.EventHandler(this.tsbtnOrgaDelete_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // ctrOrga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scOrgaMain);
            this.Name = "ctrOrga";
            this.Size = new System.Drawing.Size(754, 432);
            this.Load += new System.EventHandler(this.ctrOrga_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scOrgaMain)).EndInit();
            this.scOrgaMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panOrgaList)).EndInit();
            this.panOrgaList.ResumeLayout(false);
            this.panOrgaList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrga.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrga)).EndInit();
            this.menuASNMain.ResumeLayout(false);
            this.menuASNMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panOrgaEdit)).EndInit();
            this.panOrgaEdit.ResumeLayout(false);
            this.panOrgaEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSendeIdNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSendeIdOld)).EndInit();
            this.menuVerweisEdit.ResumeLayout(false);
            this.menuVerweisEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer scOrgaMain;
        private Telerik.WinControls.UI.SplitPanel panOrgaList;
        private Telerik.WinControls.UI.RadGridView dgvOrga;
        private AFToolStrip menuASNMain;
        private System.Windows.Forms.ToolStripButton tsbtnRefreshAdrVerweis;
        private Telerik.WinControls.UI.SplitPanel panOrgaEdit;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox cbAktiv;
        private AFToolStrip menuVerweisEdit;
        private System.Windows.Forms.ToolStripButton tsbtnOrgaNew;
        private System.Windows.Forms.ToolStripButton tsbtnOrgaSave;
        private System.Windows.Forms.ToolStripButton tsbtnOrgaDelete;
        private System.Windows.Forms.Label lOrgaId;
        private System.Windows.Forms.TextBox tbOrgaId;
        private System.Windows.Forms.NumericUpDown nudSendeIdNew;
        private System.Windows.Forms.NumericUpDown nudSendeIdOld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    }
}
