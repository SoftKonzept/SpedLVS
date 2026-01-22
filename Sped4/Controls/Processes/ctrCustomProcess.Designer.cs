namespace Sped4.Controls.Processes
{
    partial class ctrCustomProcess
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
            Telerik.WinControls.UI.ListViewDataItem listViewDataItem1 = new Telerik.WinControls.UI.ListViewDataItem("ListViewItem 1");
            Telerik.WinControls.UI.ListViewDataItem listViewDataItem2 = new Telerik.WinControls.UI.ListViewDataItem("ListViewItem 2");
            Telerik.WinControls.UI.ListViewDataItem listViewDataItem3 = new Telerik.WinControls.UI.ListViewDataItem("ListViewItem 3");
            this.sc_Main = new Telerik.WinControls.UI.RadSplitContainer();
            this.panList = new Telerik.WinControls.UI.SplitPanel();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.panAdd = new Telerik.WinControls.UI.SplitPanel();
            this.nudAdrIdDirect = new System.Windows.Forms.NumericUpDown();
            this.btnSearchA = new System.Windows.Forms.Button();
            this.tbVerweisADRLong = new System.Windows.Forms.TextBox();
            this.lAdr = new System.Windows.Forms.Label();
            this.tbVerweisADR = new System.Windows.Forms.TextBox();
            this.gbWorkspaceSelection = new System.Windows.Forms.GroupBox();
            this.CheckListWorkspace = new Telerik.WinControls.UI.RadListView();
            this.btnCopyToWorkspace = new System.Windows.Forms.Button();
            this.lWorkspaces = new System.Windows.Forms.Label();
            this.tbWorkspaceList = new System.Windows.Forms.TextBox();
            this.comboProcesses = new System.Windows.Forms.ComboBox();
            this.lProcessname = new System.Windows.Forms.Label();
            this.lId = new System.Windows.Forms.Label();
            this.tbId = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cbAktiv = new System.Windows.Forms.CheckBox();
            this.menuList = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.menuAdd = new Sped4.Controls.AFToolStrip();
            this.tsbtnAddProcess = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.sc_Main)).BeginInit();
            this.sc_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panList)).BeginInit();
            this.panList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panAdd)).BeginInit();
            this.panAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).BeginInit();
            this.gbWorkspaceSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CheckListWorkspace)).BeginInit();
            this.menuList.SuspendLayout();
            this.menuAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // sc_Main
            // 
            this.sc_Main.Controls.Add(this.panList);
            this.sc_Main.Controls.Add(this.panAdd);
            this.sc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_Main.Location = new System.Drawing.Point(0, 0);
            this.sc_Main.Name = "sc_Main";
            this.sc_Main.Size = new System.Drawing.Size(788, 534);
            this.sc_Main.SplitterWidth = 8;
            this.sc_Main.TabIndex = 1;
            this.sc_Main.TabStop = false;
            // 
            // panList
            // 
            this.panList.BackColor = System.Drawing.Color.White;
            this.panList.Controls.Add(this.dgv);
            this.panList.Controls.Add(this.menuList);
            this.panList.Location = new System.Drawing.Point(0, 0);
            this.panList.Name = "panList";
            this.panList.Size = new System.Drawing.Size(436, 534);
            this.panList.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.05867344F, 0F);
            this.panList.SizeInfo.SplitterCorrection = new System.Drawing.Size(46, 0);
            this.panList.TabIndex = 0;
            this.panList.TabStop = false;
            this.panList.Text = "splitPanel1";
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 25);
            // 
            // 
            // 
            this.dgv.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgv.MasterTemplate.EnableFiltering = true;
            this.dgv.MasterTemplate.ShowFilteringRow = false;
            this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 436, 509);
            this.dgv.Size = new System.Drawing.Size(436, 509);
            this.dgv.TabIndex = 29;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            this.dgv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDoubleClick);
            // 
            // panAdd
            // 
            this.panAdd.BackColor = System.Drawing.Color.White;
            this.panAdd.Controls.Add(this.nudAdrIdDirect);
            this.panAdd.Controls.Add(this.btnSearchA);
            this.panAdd.Controls.Add(this.tbVerweisADRLong);
            this.panAdd.Controls.Add(this.lAdr);
            this.panAdd.Controls.Add(this.tbVerweisADR);
            this.panAdd.Controls.Add(this.gbWorkspaceSelection);
            this.panAdd.Controls.Add(this.lWorkspaces);
            this.panAdd.Controls.Add(this.tbWorkspaceList);
            this.panAdd.Controls.Add(this.comboProcesses);
            this.panAdd.Controls.Add(this.lProcessname);
            this.panAdd.Controls.Add(this.lId);
            this.panAdd.Controls.Add(this.tbId);
            this.panAdd.Controls.Add(this.label26);
            this.panAdd.Controls.Add(this.cbAktiv);
            this.panAdd.Controls.Add(this.menuAdd);
            this.panAdd.Location = new System.Drawing.Point(444, 0);
            this.panAdd.Name = "panAdd";
            this.panAdd.Size = new System.Drawing.Size(344, 534);
            this.panAdd.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.05867347F, 0F);
            this.panAdd.SizeInfo.SplitterCorrection = new System.Drawing.Size(-46, 0);
            this.panAdd.TabIndex = 1;
            this.panAdd.TabStop = false;
            this.panAdd.Text = "splitPanel2";
            // 
            // nudAdrIdDirect
            // 
            this.nudAdrIdDirect.Location = new System.Drawing.Point(232, 124);
            this.nudAdrIdDirect.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudAdrIdDirect.Name = "nudAdrIdDirect";
            this.nudAdrIdDirect.Size = new System.Drawing.Size(88, 20);
            this.nudAdrIdDirect.TabIndex = 268;
            this.nudAdrIdDirect.Leave += new System.EventHandler(this.nudAdrIdDirect_Leave);
            // 
            // btnSearchA
            // 
            this.btnSearchA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchA.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchA.Location = new System.Drawing.Point(23, 125);
            this.btnSearchA.Name = "btnSearchA";
            this.btnSearchA.Size = new System.Drawing.Size(80, 29);
            this.btnSearchA.TabIndex = 267;
            this.btnSearchA.TabStop = false;
            this.btnSearchA.Text = "[ADR]";
            this.btnSearchA.UseVisualStyleBackColor = true;
            this.btnSearchA.Click += new System.EventHandler(this.btnSearchA_Click);
            // 
            // tbVerweisADRLong
            // 
            this.tbVerweisADRLong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVerweisADRLong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVerweisADRLong.Enabled = false;
            this.tbVerweisADRLong.Location = new System.Drawing.Point(23, 160);
            this.tbVerweisADRLong.Name = "tbVerweisADRLong";
            this.tbVerweisADRLong.ReadOnly = true;
            this.tbVerweisADRLong.Size = new System.Drawing.Size(249, 20);
            this.tbVerweisADRLong.TabIndex = 266;
            // 
            // lAdr
            // 
            this.lAdr.AutoSize = true;
            this.lAdr.ForeColor = System.Drawing.Color.DarkBlue;
            this.lAdr.Location = new System.Drawing.Point(21, 106);
            this.lAdr.Name = "lAdr";
            this.lAdr.Size = new System.Drawing.Size(57, 13);
            this.lAdr.TabIndex = 265;
            this.lAdr.Text = "Addresse:";
            // 
            // tbVerweisADR
            // 
            this.tbVerweisADR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVerweisADR.Enabled = false;
            this.tbVerweisADR.Location = new System.Drawing.Point(116, 125);
            this.tbVerweisADR.Name = "tbVerweisADR";
            this.tbVerweisADR.Size = new System.Drawing.Size(109, 20);
            this.tbVerweisADR.TabIndex = 264;
            // 
            // gbWorkspaceSelection
            // 
            this.gbWorkspaceSelection.Controls.Add(this.CheckListWorkspace);
            this.gbWorkspaceSelection.Controls.Add(this.btnCopyToWorkspace);
            this.gbWorkspaceSelection.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbWorkspaceSelection.Location = new System.Drawing.Point(23, 246);
            this.gbWorkspaceSelection.Name = "gbWorkspaceSelection";
            this.gbWorkspaceSelection.Size = new System.Drawing.Size(297, 206);
            this.gbWorkspaceSelection.TabIndex = 263;
            this.gbWorkspaceSelection.TabStop = false;
            this.gbWorkspaceSelection.Text = "Arbeitsbereich";
            // 
            // CheckListWorkspace
            // 
            this.CheckListWorkspace.CheckOnClickMode = Telerik.WinControls.UI.CheckOnClickMode.FirstClick;
            listViewDataItem1.Text = "ListViewItem 1";
            listViewDataItem2.Text = "ListViewItem 2";
            listViewDataItem3.Text = "ListViewItem 3";
            this.CheckListWorkspace.Items.AddRange(new Telerik.WinControls.UI.ListViewDataItem[] {
            listViewDataItem1,
            listViewDataItem2,
            listViewDataItem3});
            this.CheckListWorkspace.Location = new System.Drawing.Point(6, 23);
            this.CheckListWorkspace.MultiSelect = true;
            this.CheckListWorkspace.Name = "CheckListWorkspace";
            this.CheckListWorkspace.ShowCheckBoxes = true;
            this.CheckListWorkspace.Size = new System.Drawing.Size(285, 176);
            this.CheckListWorkspace.TabIndex = 265;
            this.CheckListWorkspace.ThemeName = "ControlDefault";
            this.CheckListWorkspace.SelectedItemChanged += new System.EventHandler(this.CheckListWorkspace_SelectedItemChanged);
            this.CheckListWorkspace.ItemCheckedChanged += new Telerik.WinControls.UI.ListViewItemEventHandler(this.CheckListWorkspace_ItemCheckedChanged);
            // 
            // btnCopyToWorkspace
            // 
            this.btnCopyToWorkspace.Location = new System.Drawing.Point(310, 176);
            this.btnCopyToWorkspace.Name = "btnCopyToWorkspace";
            this.btnCopyToWorkspace.Size = new System.Drawing.Size(75, 23);
            this.btnCopyToWorkspace.TabIndex = 263;
            this.btnCopyToWorkspace.Text = "Copy to";
            this.btnCopyToWorkspace.UseVisualStyleBackColor = true;
            // 
            // lWorkspaces
            // 
            this.lWorkspaces.AutoSize = true;
            this.lWorkspaces.ForeColor = System.Drawing.Color.DarkBlue;
            this.lWorkspaces.Location = new System.Drawing.Point(20, 222);
            this.lWorkspaces.Name = "lWorkspaces";
            this.lWorkspaces.Size = new System.Drawing.Size(90, 13);
            this.lWorkspaces.TabIndex = 245;
            this.lWorkspaces.Text = "Arbeitsbereiche:";
            // 
            // tbWorkspaceList
            // 
            this.tbWorkspaceList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWorkspaceList.Location = new System.Drawing.Point(116, 220);
            this.tbWorkspaceList.Name = "tbWorkspaceList";
            this.tbWorkspaceList.ReadOnly = true;
            this.tbWorkspaceList.Size = new System.Drawing.Size(204, 20);
            this.tbWorkspaceList.TabIndex = 244;
            // 
            // comboProcesses
            // 
            this.comboProcesses.FormattingEnabled = true;
            this.comboProcesses.Location = new System.Drawing.Point(116, 193);
            this.comboProcesses.Name = "comboProcesses";
            this.comboProcesses.Size = new System.Drawing.Size(204, 21);
            this.comboProcesses.TabIndex = 243;
            // 
            // lProcessname
            // 
            this.lProcessname.AutoSize = true;
            this.lProcessname.ForeColor = System.Drawing.Color.DarkBlue;
            this.lProcessname.Location = new System.Drawing.Point(20, 196);
            this.lProcessname.Name = "lProcessname";
            this.lProcessname.Size = new System.Drawing.Size(76, 13);
            this.lProcessname.TabIndex = 242;
            this.lProcessname.Text = "Prozessname:";
            // 
            // lId
            // 
            this.lId.AutoSize = true;
            this.lId.ForeColor = System.Drawing.Color.DarkBlue;
            this.lId.Location = new System.Drawing.Point(20, 55);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(21, 13);
            this.lId.TabIndex = 241;
            this.lId.Text = "ID:";
            // 
            // tbId
            // 
            this.tbId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbId.Location = new System.Drawing.Point(116, 53);
            this.tbId.Name = "tbId";
            this.tbId.ReadOnly = true;
            this.tbId.Size = new System.Drawing.Size(204, 20);
            this.tbId.TabIndex = 240;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.DarkBlue;
            this.label26.Location = new System.Drawing.Point(20, 81);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(42, 13);
            this.label26.TabIndex = 238;
            this.label26.Text = "Status:";
            // 
            // cbAktiv
            // 
            this.cbAktiv.AutoSize = true;
            this.cbAktiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAktiv.Location = new System.Drawing.Point(273, 79);
            this.cbAktiv.Name = "cbAktiv";
            this.cbAktiv.Size = new System.Drawing.Size(47, 17);
            this.cbAktiv.TabIndex = 237;
            this.cbAktiv.Text = "aktiv";
            this.cbAktiv.UseVisualStyleBackColor = true;
            // 
            // menuList
            // 
            this.menuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh});
            this.menuList.Location = new System.Drawing.Point(0, 0);
            this.menuList.myColorFrom = System.Drawing.Color.Azure;
            this.menuList.myColorTo = System.Drawing.Color.Blue;
            this.menuList.myUnderlineColor = System.Drawing.Color.White;
            this.menuList.myUnderlined = true;
            this.menuList.Name = "menuList";
            this.menuList.Size = new System.Drawing.Size(436, 25);
            this.menuList.TabIndex = 11;
            this.menuList.Text = "afToolStrip1";
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
            // menuAdd
            // 
            this.menuAdd.AutoSize = false;
            this.menuAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddProcess,
            this.tsbtnSave,
            this.tsbtnDelete});
            this.menuAdd.Location = new System.Drawing.Point(0, 0);
            this.menuAdd.myColorFrom = System.Drawing.Color.Azure;
            this.menuAdd.myColorTo = System.Drawing.Color.Blue;
            this.menuAdd.myUnderlineColor = System.Drawing.Color.White;
            this.menuAdd.myUnderlined = true;
            this.menuAdd.Name = "menuAdd";
            this.menuAdd.Size = new System.Drawing.Size(344, 31);
            this.menuAdd.TabIndex = 85;
            this.menuAdd.Text = "afToolStrip1";
            // 
            // tsbtnAddProcess
            // 
            this.tsbtnAddProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddProcess.Image = global::Sped4.Properties.Resources.add_24x24;
            this.tsbtnAddProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddProcess.Name = "tsbtnAddProcess";
            this.tsbtnAddProcess.Size = new System.Drawing.Size(23, 28);
            this.tsbtnAddProcess.Text = "Neuen Prozess anlegen";
            this.tsbtnAddProcess.Click += new System.EventHandler(this.tsbtnAddProcess_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::Sped4.Properties.Resources.check_24x24;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 28);
            this.tsbtnSave.Text = "Daten speichern";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(23, 28);
            this.tsbtnDelete.Text = "CronJob löschen";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // ctrCustomProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sc_Main);
            this.Name = "ctrCustomProcess";
            this.Size = new System.Drawing.Size(788, 534);
            this.Load += new System.EventHandler(this.ctrCustomProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sc_Main)).EndInit();
            this.sc_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panList)).EndInit();
            this.panList.ResumeLayout(false);
            this.panList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panAdd)).EndInit();
            this.panAdd.ResumeLayout(false);
            this.panAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).EndInit();
            this.gbWorkspaceSelection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CheckListWorkspace)).EndInit();
            this.menuList.ResumeLayout(false);
            this.menuList.PerformLayout();
            this.menuAdd.ResumeLayout(false);
            this.menuAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer sc_Main;
        private Telerik.WinControls.UI.SplitPanel panList;
        private Telerik.WinControls.UI.RadGridView dgv;
        private AFToolStrip menuList;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private Telerik.WinControls.UI.SplitPanel panAdd;
        private System.Windows.Forms.GroupBox gbWorkspaceSelection;
        private Telerik.WinControls.UI.RadListView CheckListWorkspace;
        private System.Windows.Forms.Button btnCopyToWorkspace;
        private System.Windows.Forms.Label lWorkspaces;
        private System.Windows.Forms.TextBox tbWorkspaceList;
        private System.Windows.Forms.ComboBox comboProcesses;
        private System.Windows.Forms.Label lProcessname;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox cbAktiv;
        private AFToolStrip menuAdd;
        private System.Windows.Forms.ToolStripButton tsbtnAddProcess;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.NumericUpDown nudAdrIdDirect;
        private System.Windows.Forms.Button btnSearchA;
        private System.Windows.Forms.TextBox tbVerweisADRLong;
        private System.Windows.Forms.Label lAdr;
        private System.Windows.Forms.TextBox tbVerweisADR;
    }
}
