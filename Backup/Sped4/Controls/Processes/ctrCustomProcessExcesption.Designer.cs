namespace Sped4.Controls.Processes
{
    partial class ctrCustomProcessExcesption
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panDgvCustomProcess = new System.Windows.Forms.Panel();
            this.dgvCustomProcesses = new Telerik.WinControls.UI.RadGridView();
            this.scEdit = new System.Windows.Forms.SplitContainer();
            this.panCustomProzessView = new System.Windows.Forms.Panel();
            this.tbAdrId = new System.Windows.Forms.TextBox();
            this.btnSearchGoodsTypes = new System.Windows.Forms.Button();
            this.lWorkspaces = new System.Windows.Forms.Label();
            this.tbWorkspaceList = new System.Windows.Forms.TextBox();
            this.tbVerweisADRLong = new System.Windows.Forms.TextBox();
            this.lId = new System.Windows.Forms.Label();
            this.tbId = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cbAktiv = new System.Windows.Forms.CheckBox();
            this.dgvExceptions = new Telerik.WinControls.UI.RadGridView();
            this.menuCustomerProcesses = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.menuCustomerProcessView = new Sped4.Controls.AFToolStrip();
            this.tsbtnAddProcess = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.menuCustomerProcessExceptions = new Sped4.Controls.AFToolStrip();
            this.tsbtnCustomerProcessException = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDeleteException = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panDgvCustomProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomProcesses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomProcesses.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scEdit)).BeginInit();
            this.scEdit.Panel1.SuspendLayout();
            this.scEdit.Panel2.SuspendLayout();
            this.scEdit.SuspendLayout();
            this.panCustomProzessView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExceptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExceptions.MasterTemplate)).BeginInit();
            this.menuCustomerProcesses.SuspendLayout();
            this.menuCustomerProcessView.SuspendLayout();
            this.menuCustomerProcessExceptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleName = "scMain";
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.panDgvCustomProcess);
            this.splitContainer1.Panel1.Controls.Add(this.menuCustomerProcesses);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scEdit);
            this.splitContainer1.Size = new System.Drawing.Size(782, 438);
            this.splitContainer1.SplitterDistance = 365;
            this.splitContainer1.TabIndex = 0;
            // 
            // panDgvCustomProcess
            // 
            this.panDgvCustomProcess.BackColor = System.Drawing.Color.White;
            this.panDgvCustomProcess.Controls.Add(this.dgvCustomProcesses);
            this.panDgvCustomProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panDgvCustomProcess.Location = new System.Drawing.Point(0, 25);
            this.panDgvCustomProcess.Name = "panDgvCustomProcess";
            this.panDgvCustomProcess.Size = new System.Drawing.Size(365, 413);
            this.panDgvCustomProcess.TabIndex = 0;
            // 
            // dgvCustomProcesses
            // 
            this.dgvCustomProcesses.BackColor = System.Drawing.Color.White;
            this.dgvCustomProcesses.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvCustomProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomProcesses.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvCustomProcesses.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvCustomProcesses.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvCustomProcesses.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgvCustomProcesses.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvCustomProcesses.MasterTemplate.EnableFiltering = true;
            this.dgvCustomProcesses.MasterTemplate.ShowFilteringRow = false;
            this.dgvCustomProcesses.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvCustomProcesses.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvCustomProcesses.Name = "dgvCustomProcesses";
            this.dgvCustomProcesses.ReadOnly = true;
            this.dgvCustomProcesses.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvCustomProcesses.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 365, 413);
            this.dgvCustomProcesses.Size = new System.Drawing.Size(365, 413);
            this.dgvCustomProcesses.TabIndex = 30;
            this.dgvCustomProcesses.ThemeName = "ControlDefault";
            this.dgvCustomProcesses.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvCustomProcesses_MouseClick);
            this.dgvCustomProcesses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvCustomProcesses_MouseDoubleClick);
            // 
            // scEdit
            // 
            this.scEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scEdit.Location = new System.Drawing.Point(0, 0);
            this.scEdit.Name = "scEdit";
            this.scEdit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scEdit.Panel1
            // 
            this.scEdit.Panel1.Controls.Add(this.panCustomProzessView);
            this.scEdit.Panel1.Controls.Add(this.menuCustomerProcessView);
            // 
            // scEdit.Panel2
            // 
            this.scEdit.Panel2.Controls.Add(this.dgvExceptions);
            this.scEdit.Panel2.Controls.Add(this.menuCustomerProcessExceptions);
            this.scEdit.Size = new System.Drawing.Size(413, 438);
            this.scEdit.SplitterDistance = 202;
            this.scEdit.TabIndex = 1;
            // 
            // panCustomProzessView
            // 
            this.panCustomProzessView.BackColor = System.Drawing.Color.White;
            this.panCustomProzessView.Controls.Add(this.tbAdrId);
            this.panCustomProzessView.Controls.Add(this.btnSearchGoodsTypes);
            this.panCustomProzessView.Controls.Add(this.lWorkspaces);
            this.panCustomProzessView.Controls.Add(this.tbWorkspaceList);
            this.panCustomProzessView.Controls.Add(this.tbVerweisADRLong);
            this.panCustomProzessView.Controls.Add(this.lId);
            this.panCustomProzessView.Controls.Add(this.tbId);
            this.panCustomProzessView.Controls.Add(this.label26);
            this.panCustomProzessView.Controls.Add(this.cbAktiv);
            this.panCustomProzessView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panCustomProzessView.Location = new System.Drawing.Point(0, 31);
            this.panCustomProzessView.Name = "panCustomProzessView";
            this.panCustomProzessView.Size = new System.Drawing.Size(413, 171);
            this.panCustomProzessView.TabIndex = 0;
            // 
            // tbAdrId
            // 
            this.tbAdrId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAdrId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAdrId.Enabled = false;
            this.tbAdrId.Location = new System.Drawing.Point(109, 40);
            this.tbAdrId.Name = "tbAdrId";
            this.tbAdrId.ReadOnly = true;
            this.tbAdrId.Size = new System.Drawing.Size(55, 20);
            this.tbAdrId.TabIndex = 271;
            // 
            // btnSearchGoodsTypes
            // 
            this.btnSearchGoodsTypes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchGoodsTypes.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchGoodsTypes.Location = new System.Drawing.Point(20, 107);
            this.btnSearchGoodsTypes.Name = "btnSearchGoodsTypes";
            this.btnSearchGoodsTypes.Size = new System.Drawing.Size(80, 29);
            this.btnSearchGoodsTypes.TabIndex = 270;
            this.btnSearchGoodsTypes.TabStop = false;
            this.btnSearchGoodsTypes.Text = "[Güterart]";
            this.btnSearchGoodsTypes.UseVisualStyleBackColor = true;
            this.btnSearchGoodsTypes.Click += new System.EventHandler(this.btnSearchGoodsTypes_Click);
            // 
            // lWorkspaces
            // 
            this.lWorkspaces.AutoSize = true;
            this.lWorkspaces.ForeColor = System.Drawing.Color.DarkBlue;
            this.lWorkspaces.Location = new System.Drawing.Point(13, 70);
            this.lWorkspaces.Name = "lWorkspaces";
            this.lWorkspaces.Size = new System.Drawing.Size(83, 13);
            this.lWorkspaces.TabIndex = 269;
            this.lWorkspaces.Text = "Arbeitsbereiche:";
            // 
            // tbWorkspaceList
            // 
            this.tbWorkspaceList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWorkspaceList.Location = new System.Drawing.Point(109, 68);
            this.tbWorkspaceList.Name = "tbWorkspaceList";
            this.tbWorkspaceList.ReadOnly = true;
            this.tbWorkspaceList.Size = new System.Drawing.Size(291, 20);
            this.tbWorkspaceList.TabIndex = 268;
            // 
            // tbVerweisADRLong
            // 
            this.tbVerweisADRLong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVerweisADRLong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVerweisADRLong.Enabled = false;
            this.tbVerweisADRLong.Location = new System.Drawing.Point(172, 40);
            this.tbVerweisADRLong.Name = "tbVerweisADRLong";
            this.tbVerweisADRLong.ReadOnly = true;
            this.tbVerweisADRLong.Size = new System.Drawing.Size(228, 20);
            this.tbVerweisADRLong.TabIndex = 267;
            // 
            // lId
            // 
            this.lId.AutoSize = true;
            this.lId.ForeColor = System.Drawing.Color.DarkBlue;
            this.lId.Location = new System.Drawing.Point(13, 14);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(21, 13);
            this.lId.TabIndex = 245;
            this.lId.Text = "ID:";
            // 
            // tbId
            // 
            this.tbId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbId.Location = new System.Drawing.Point(109, 12);
            this.tbId.Name = "tbId";
            this.tbId.ReadOnly = true;
            this.tbId.Size = new System.Drawing.Size(204, 20);
            this.tbId.TabIndex = 244;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.DarkBlue;
            this.label26.Location = new System.Drawing.Point(13, 42);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(87, 13);
            this.label26.TabIndex = 243;
            this.label26.Text = "Kunden-Prozess:";
            // 
            // cbAktiv
            // 
            this.cbAktiv.AutoSize = true;
            this.cbAktiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAktiv.Location = new System.Drawing.Point(354, 12);
            this.cbAktiv.Name = "cbAktiv";
            this.cbAktiv.Size = new System.Drawing.Size(46, 17);
            this.cbAktiv.TabIndex = 242;
            this.cbAktiv.Text = "aktiv";
            this.cbAktiv.UseVisualStyleBackColor = true;
            // 
            // dgvExceptions
            // 
            this.dgvExceptions.BackColor = System.Drawing.Color.White;
            this.dgvExceptions.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvExceptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExceptions.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvExceptions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvExceptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvExceptions.Location = new System.Drawing.Point(0, 25);
            // 
            // 
            // 
            this.dgvExceptions.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvExceptions.MasterTemplate.EnableFiltering = true;
            this.dgvExceptions.MasterTemplate.ShowFilteringRow = false;
            this.dgvExceptions.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvExceptions.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgvExceptions.Name = "dgvExceptions";
            this.dgvExceptions.ReadOnly = true;
            this.dgvExceptions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvExceptions.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 413, 207);
            this.dgvExceptions.Size = new System.Drawing.Size(413, 207);
            this.dgvExceptions.TabIndex = 31;
            this.dgvExceptions.ThemeName = "ControlDefault";
            this.dgvExceptions.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvExceptions_MouseClick);
            this.dgvExceptions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvExceptions_MouseDoubleClick);
            // 
            // menuCustomerProcesses
            // 
            this.menuCustomerProcesses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh});
            this.menuCustomerProcesses.Location = new System.Drawing.Point(0, 0);
            this.menuCustomerProcesses.myColorFrom = System.Drawing.Color.Azure;
            this.menuCustomerProcesses.myColorTo = System.Drawing.Color.Blue;
            this.menuCustomerProcesses.myUnderlineColor = System.Drawing.Color.White;
            this.menuCustomerProcesses.myUnderlined = true;
            this.menuCustomerProcesses.Name = "menuCustomerProcesses";
            this.menuCustomerProcesses.Size = new System.Drawing.Size(365, 25);
            this.menuCustomerProcesses.TabIndex = 12;
            this.menuCustomerProcesses.Text = "afToolStrip1";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefresh.Text = "aktualisieren";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // menuCustomerProcessView
            // 
            this.menuCustomerProcessView.AutoSize = false;
            this.menuCustomerProcessView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddProcess,
            this.tsbtnSave,
            this.tsbtnDelete});
            this.menuCustomerProcessView.Location = new System.Drawing.Point(0, 0);
            this.menuCustomerProcessView.myColorFrom = System.Drawing.Color.Azure;
            this.menuCustomerProcessView.myColorTo = System.Drawing.Color.Blue;
            this.menuCustomerProcessView.myUnderlineColor = System.Drawing.Color.White;
            this.menuCustomerProcessView.myUnderlined = true;
            this.menuCustomerProcessView.Name = "menuCustomerProcessView";
            this.menuCustomerProcessView.Size = new System.Drawing.Size(413, 31);
            this.menuCustomerProcessView.TabIndex = 86;
            this.menuCustomerProcessView.Text = "afToolStrip1";
            // 
            // tsbtnAddProcess
            // 
            this.tsbtnAddProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddProcess.Image = global::Sped4.Properties.Resources.add_24x24;
            this.tsbtnAddProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddProcess.Name = "tsbtnAddProcess";
            this.tsbtnAddProcess.Size = new System.Drawing.Size(23, 28);
            this.tsbtnAddProcess.Text = "Neuen Prozess anlegen";
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::Sped4.Properties.Resources.check_24x24;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 28);
            this.tsbtnSave.Text = "Daten speichern";
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(23, 28);
            this.tsbtnDelete.Text = "CronJob löschen";
            // 
            // menuCustomerProcessExceptions
            // 
            this.menuCustomerProcessExceptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnCustomerProcessException,
            this.tsbtnDeleteException});
            this.menuCustomerProcessExceptions.Location = new System.Drawing.Point(0, 0);
            this.menuCustomerProcessExceptions.myColorFrom = System.Drawing.Color.Azure;
            this.menuCustomerProcessExceptions.myColorTo = System.Drawing.Color.Blue;
            this.menuCustomerProcessExceptions.myUnderlineColor = System.Drawing.Color.White;
            this.menuCustomerProcessExceptions.myUnderlined = true;
            this.menuCustomerProcessExceptions.Name = "menuCustomerProcessExceptions";
            this.menuCustomerProcessExceptions.Size = new System.Drawing.Size(413, 25);
            this.menuCustomerProcessExceptions.TabIndex = 13;
            this.menuCustomerProcessExceptions.Text = "afToolStrip1";
            // 
            // tsbtnCustomerProcessException
            // 
            this.tsbtnCustomerProcessException.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCustomerProcessException.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnCustomerProcessException.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCustomerProcessException.Name = "tsbtnCustomerProcessException";
            this.tsbtnCustomerProcessException.Size = new System.Drawing.Size(23, 22);
            this.tsbtnCustomerProcessException.Text = "aktualisieren";
            this.tsbtnCustomerProcessException.Click += new System.EventHandler(this.tsbtnCustomerProcessException_Click);
            // 
            // tsbtnDeleteException
            // 
            this.tsbtnDeleteException.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDeleteException.Image = global::Sped4.Properties.Resources.garbage_delete_24x24;
            this.tsbtnDeleteException.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeleteException.Name = "tsbtnDeleteException";
            this.tsbtnDeleteException.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDeleteException.Text = "tsbtnDelete";
            this.tsbtnDeleteException.Click += new System.EventHandler(this.tsbtnDeleteException_Click);
            // 
            // ctrCustomProcessExcesption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ctrCustomProcessExcesption";
            this.Size = new System.Drawing.Size(782, 438);
            this.Load += new System.EventHandler(this.ctrCustomProcessExcesption_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panDgvCustomProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomProcesses.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomProcesses)).EndInit();
            this.scEdit.Panel1.ResumeLayout(false);
            this.scEdit.Panel2.ResumeLayout(false);
            this.scEdit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scEdit)).EndInit();
            this.scEdit.ResumeLayout(false);
            this.panCustomProzessView.ResumeLayout(false);
            this.panCustomProzessView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExceptions.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExceptions)).EndInit();
            this.menuCustomerProcesses.ResumeLayout(false);
            this.menuCustomerProcesses.PerformLayout();
            this.menuCustomerProcessView.ResumeLayout(false);
            this.menuCustomerProcessView.PerformLayout();
            this.menuCustomerProcessExceptions.ResumeLayout(false);
            this.menuCustomerProcessExceptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panDgvCustomProcess;
        private AFToolStrip menuCustomerProcesses;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private Telerik.WinControls.UI.RadGridView dgvCustomProcesses;
        private System.Windows.Forms.SplitContainer scEdit;
        private System.Windows.Forms.Panel panCustomProzessView;
        private AFToolStrip menuCustomerProcessView;
        private System.Windows.Forms.ToolStripButton tsbtnAddProcess;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private AFToolStrip menuCustomerProcessExceptions;
        private System.Windows.Forms.ToolStripButton tsbtnCustomerProcessException;
        private Telerik.WinControls.UI.RadGridView dgvExceptions;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox cbAktiv;
        private System.Windows.Forms.TextBox tbVerweisADRLong;
        private System.Windows.Forms.Label lWorkspaces;
        private System.Windows.Forms.TextBox tbWorkspaceList;
        private System.Windows.Forms.Button btnSearchGoodsTypes;
        private System.Windows.Forms.ToolStripButton tsbtnDeleteException;
        private System.Windows.Forms.TextBox tbAdrId;
    }
}
