namespace Sped4.Controls.ASNCenter
{
    partial class ctrASNMain
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
            this.scASNMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.panAuftraggeberADR = new Telerik.WinControls.UI.SplitPanel();
            this.nudAdrIdDirect = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.comboASNArt = new System.Windows.Forms.ComboBox();
            this.rmReportSettingEdit = new Telerik.WinControls.UI.RadMenu();
            this.btnSearch = new Telerik.WinControls.UI.RadMenuItem();
            this.btnSave = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnDelete = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            this.btnCloseMain = new Telerik.WinControls.UI.RadMenuItem();
            this.tbAuftraggeber = new System.Windows.Forms.TextBox();
            this.tbSearchA = new System.Windows.Forms.TextBox();
            this.btnSearchA = new System.Windows.Forms.Button();
            this.panEditdata = new Telerik.WinControls.UI.SplitPanel();
            this.pageViewASNSetting = new Telerik.WinControls.UI.RadPageView();
            this.tabPageASNSetting_AdrVerweise = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_Orga = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_ASNAction = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_Jobs = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_VDAClientOut = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_EDIFACTClientOut = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_VDAClientWorkspaceValue = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_ASNArtFieldAssignment = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_ASNMessagesTest = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_tabPageImportEdifact = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabPageASNSetting_VDAClientOutUpdate = new Telerik.WinControls.UI.RadPageViewPage();
            this.miniToolStrip = new Sped4.Controls.AFToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.scASNMain)).BeginInit();
            this.scASNMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panAuftraggeberADR)).BeginInit();
            this.panAuftraggeberADR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmReportSettingEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panEditdata)).BeginInit();
            this.panEditdata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageViewASNSetting)).BeginInit();
            this.pageViewASNSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // scASNMain
            // 
            this.scASNMain.Controls.Add(this.panAuftraggeberADR);
            this.scASNMain.Controls.Add(this.panEditdata);
            this.scASNMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scASNMain.Location = new System.Drawing.Point(0, 0);
            this.scASNMain.Name = "scASNMain";
            this.scASNMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.scASNMain.Size = new System.Drawing.Size(1097, 805);
            this.scASNMain.SplitterWidth = 8;
            this.scASNMain.TabIndex = 3;
            this.scASNMain.TabStop = false;
            // 
            // panAuftraggeberADR
            // 
            this.panAuftraggeberADR.BackColor = System.Drawing.Color.White;
            this.panAuftraggeberADR.Controls.Add(this.nudAdrIdDirect);
            this.panAuftraggeberADR.Controls.Add(this.label1);
            this.panAuftraggeberADR.Controls.Add(this.comboASNArt);
            this.panAuftraggeberADR.Controls.Add(this.rmReportSettingEdit);
            this.panAuftraggeberADR.Controls.Add(this.tbAuftraggeber);
            this.panAuftraggeberADR.Controls.Add(this.tbSearchA);
            this.panAuftraggeberADR.Controls.Add(this.btnSearchA);
            this.panAuftraggeberADR.Location = new System.Drawing.Point(0, 0);
            this.panAuftraggeberADR.Name = "panAuftraggeberADR";
            this.panAuftraggeberADR.Size = new System.Drawing.Size(1097, 100);
            this.panAuftraggeberADR.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.375156F);
            this.panAuftraggeberADR.SizeInfo.MaximumSize = new System.Drawing.Size(0, 100);
            this.panAuftraggeberADR.SizeInfo.MinimumSize = new System.Drawing.Size(0, 98);
            this.panAuftraggeberADR.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -217);
            this.panAuftraggeberADR.TabIndex = 0;
            this.panAuftraggeberADR.TabStop = false;
            this.panAuftraggeberADR.Text = "Auftraggeberdaten";
            // 
            // nudAdrIdDirect
            // 
            this.nudAdrIdDirect.Location = new System.Drawing.Point(19, 40);
            this.nudAdrIdDirect.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudAdrIdDirect.Name = "nudAdrIdDirect";
            this.nudAdrIdDirect.Size = new System.Drawing.Size(110, 20);
            this.nudAdrIdDirect.TabIndex = 126;
            this.nudAdrIdDirect.ValueChanged += new System.EventHandler(this.nudAdrIdDirect_ValueChanged);
            this.nudAdrIdDirect.Leave += new System.EventHandler(this.nudAdrIdDirect_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(780, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 125;
            this.label1.Text = "ASN-Art:";
            // 
            // comboASNArt
            // 
            this.comboASNArt.FormattingEnabled = true;
            this.comboASNArt.Location = new System.Drawing.Point(836, 52);
            this.comboASNArt.Name = "comboASNArt";
            this.comboASNArt.Size = new System.Drawing.Size(167, 21);
            this.comboASNArt.TabIndex = 124;
            this.comboASNArt.SelectedValueChanged += new System.EventHandler(this.comboASNArt_SelectedValueChanged);
            // 
            // rmReportSettingEdit
            // 
            this.rmReportSettingEdit.AllItemsEqualHeight = true;
            this.rmReportSettingEdit.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnSearch,
            this.btnSave,
            this.btnDelete,
            this.btnCloseMain});
            this.rmReportSettingEdit.Location = new System.Drawing.Point(0, 0);
            this.rmReportSettingEdit.Name = "rmReportSettingEdit";
            this.rmReportSettingEdit.Size = new System.Drawing.Size(1097, 34);
            this.rmReportSettingEdit.TabIndex = 123;
            this.rmReportSettingEdit.ThemeName = "ControlDefault";
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnSearch.HintText = "";
            this.btnSearch.Image = global::Sped4.Properties.Resources.magnifying_glass;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Text = "";
            this.btnSearch.ToolTipText = "Start Suche.....";
            this.btnSearch.UseCompatibleTextRendering = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSave
            // 
            // 
            // 
            // 
            this.btnSave.ButtonElement.ToolTipText = "Datensatz speichern";
            this.btnSave.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnSave.Image = global::Sped4.Properties.Resources.check_24x24;
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "";
            this.btnSave.ToolTipText = "Datensatz speichern";
            this.btnSave.UseCompatibleTextRendering = false;
            this.btnSave.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(2))).ToolTipText = "Datensatz speichern";
            // 
            // btnDelete
            // 
            // 
            // 
            // 
            this.btnDelete.ButtonElement.ToolTipText = "Datensatz löschen";
            this.btnDelete.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnDelete.Image = global::Sped4.Properties.Resources.garbage_delete_24x24;
            this.btnDelete.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItem1});
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "";
            this.btnDelete.ToolTipText = "Datensatz löschen";
            this.btnDelete.UseCompatibleTextRendering = false;
            this.btnDelete.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDelete.GetChildAt(2))).ToolTipText = "Datensatz löschen";
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = "radMenuItem1";
            // 
            // btnCloseMain
            // 
            this.btnCloseMain.Image = global::Sped4.Properties.Resources.delete_16;
            this.btnCloseMain.Name = "btnCloseMain";
            this.btnCloseMain.Text = "";
            this.btnCloseMain.Click += new System.EventHandler(this.btnCloseMain_Click);
            // 
            // tbAuftraggeber
            // 
            this.tbAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuftraggeber.Enabled = false;
            this.tbAuftraggeber.Location = new System.Drawing.Point(135, 68);
            this.tbAuftraggeber.Name = "tbAuftraggeber";
            this.tbAuftraggeber.ReadOnly = true;
            this.tbAuftraggeber.Size = new System.Drawing.Size(462, 20);
            this.tbAuftraggeber.TabIndex = 122;
            // 
            // tbSearchA
            // 
            this.tbSearchA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchA.Location = new System.Drawing.Point(135, 40);
            this.tbSearchA.Name = "tbSearchA";
            this.tbSearchA.Size = new System.Drawing.Size(328, 20);
            this.tbSearchA.TabIndex = 121;
            this.tbSearchA.TextChanged += new System.EventHandler(this.tbSearchA_TextChanged);
            // 
            // btnSearchA
            // 
            this.btnSearchA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchA.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchA.Location = new System.Drawing.Point(19, 62);
            this.btnSearchA.Name = "btnSearchA";
            this.btnSearchA.Size = new System.Drawing.Size(110, 29);
            this.btnSearchA.TabIndex = 2;
            this.btnSearchA.TabStop = false;
            this.btnSearchA.Text = "[Auftraggeber]";
            this.btnSearchA.UseVisualStyleBackColor = true;
            this.btnSearchA.Click += new System.EventHandler(this.btnSearchA_Click);
            // 
            // panEditdata
            // 
            this.panEditdata.Controls.Add(this.pageViewASNSetting);
            this.panEditdata.Location = new System.Drawing.Point(0, 108);
            this.panEditdata.Name = "panEditdata";
            this.panEditdata.Size = new System.Drawing.Size(1097, 697);
            this.panEditdata.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.375156F);
            this.panEditdata.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 217);
            this.panEditdata.TabIndex = 1;
            this.panEditdata.TabStop = false;
            this.panEditdata.Text = "splitPanel2";
            // 
            // pageViewASNSetting
            // 
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_AdrVerweise);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_Orga);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_ASNAction);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_Jobs);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_VDAClientOut);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_EDIFACTClientOut);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_VDAClientWorkspaceValue);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_ASNArtFieldAssignment);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_ASNMessagesTest);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_tabPageImportEdifact);
            this.pageViewASNSetting.Controls.Add(this.tabPageASNSetting_VDAClientOutUpdate);
            this.pageViewASNSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageViewASNSetting.Location = new System.Drawing.Point(0, 0);
            this.pageViewASNSetting.Name = "pageViewASNSetting";
            this.pageViewASNSetting.SelectedPage = this.tabPageASNSetting_AdrVerweise;
            this.pageViewASNSetting.Size = new System.Drawing.Size(1097, 697);
            this.pageViewASNSetting.TabIndex = 1;
            this.pageViewASNSetting.ThemeName = "ControlDefault";
            this.pageViewASNSetting.SelectedPageChanged += new System.EventHandler(this.pageViewASNSetting_SelectedPageChanged);
            // 
            // tabPageASNSetting_AdrVerweise
            // 
            this.tabPageASNSetting_AdrVerweise.ItemSize = new System.Drawing.SizeF(83F, 28F);
            this.tabPageASNSetting_AdrVerweise.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_AdrVerweise.Name = "tabPageASNSetting_AdrVerweise";
            this.tabPageASNSetting_AdrVerweise.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_AdrVerweise.Text = "Adr-Verweise";
            // 
            // tabPageASNSetting_Orga
            // 
            this.tabPageASNSetting_Orga.ItemSize = new System.Drawing.SizeF(41F, 28F);
            this.tabPageASNSetting_Orga.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_Orga.Name = "tabPageASNSetting_Orga";
            this.tabPageASNSetting_Orga.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_Orga.Text = "Orga";
            // 
            // tabPageASNSetting_ASNAction
            // 
            this.tabPageASNSetting_ASNAction.ItemSize = new System.Drawing.SizeF(92F, 28F);
            this.tabPageASNSetting_ASNAction.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_ASNAction.Name = "tabPageASNSetting_ASNAction";
            this.tabPageASNSetting_ASNAction.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_ASNAction.Text = "ASNAction Edit";
            // 
            // tabPageASNSetting_Jobs
            // 
            this.tabPageASNSetting_Jobs.ItemSize = new System.Drawing.SizeF(38F, 28F);
            this.tabPageASNSetting_Jobs.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_Jobs.Name = "tabPageASNSetting_Jobs";
            this.tabPageASNSetting_Jobs.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_Jobs.Text = "Jobs";
            // 
            // tabPageASNSetting_VDAClientOut
            // 
            this.tabPageASNSetting_VDAClientOut.ItemSize = new System.Drawing.SizeF(89F, 28F);
            this.tabPageASNSetting_VDAClientOut.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_VDAClientOut.Name = "tabPageASNSetting_VDAClientOut";
            this.tabPageASNSetting_VDAClientOut.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_VDAClientOut.Text = "VDAClientOUT";
            // 
            // tabPageASNSetting_EDIFACTClientOut
            // 
            this.tabPageASNSetting_EDIFACTClientOut.ItemSize = new System.Drawing.SizeF(109F, 28F);
            this.tabPageASNSetting_EDIFACTClientOut.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_EDIFACTClientOut.Name = "tabPageASNSetting_EDIFACTClientOut";
            this.tabPageASNSetting_EDIFACTClientOut.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_EDIFACTClientOut.Text = "EDIFACT ClientOut";
            // 
            // tabPageASNSetting_VDAClientWorkspaceValue
            // 
            this.tabPageASNSetting_VDAClientWorkspaceValue.ItemSize = new System.Drawing.SizeF(128F, 28F);
            this.tabPageASNSetting_VDAClientWorkspaceValue.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_VDAClientWorkspaceValue.Name = "tabPageASNSetting_VDAClientWorkspaceValue";
            this.tabPageASNSetting_VDAClientWorkspaceValue.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_VDAClientWorkspaceValue.Text = "VDA Workspace Value";
            // 
            // tabPageASNSetting_ASNArtFieldAssignment
            // 
            this.tabPageASNSetting_ASNArtFieldAssignment.ItemSize = new System.Drawing.SizeF(135F, 28F);
            this.tabPageASNSetting_ASNArtFieldAssignment.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_ASNArtFieldAssignment.Name = "tabPageASNSetting_ASNArtFieldAssignment";
            this.tabPageASNSetting_ASNArtFieldAssignment.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_ASNArtFieldAssignment.Text = "ASNArtFieldAssignment";
            // 
            // tabPageASNSetting_ASNMessagesTest
            // 
            this.tabPageASNSetting_ASNMessagesTest.ItemSize = new System.Drawing.SizeF(87F, 28F);
            this.tabPageASNSetting_ASNMessagesTest.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_ASNMessagesTest.Name = "tabPageASNSetting_ASNMessagesTest";
            this.tabPageASNSetting_ASNMessagesTest.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_ASNMessagesTest.Text = "ASNMessages";
            // 
            // tabPageASNSetting_tabPageImportEdifact
            // 
            this.tabPageASNSetting_tabPageImportEdifact.ItemSize = new System.Drawing.SizeF(144F, 28F);
            this.tabPageASNSetting_tabPageImportEdifact.Location = new System.Drawing.Point(10, 37);
            this.tabPageASNSetting_tabPageImportEdifact.Name = "tabPageASNSetting_tabPageImportEdifact";
            this.tabPageASNSetting_tabPageImportEdifact.Size = new System.Drawing.Size(1076, 649);
            this.tabPageASNSetting_tabPageImportEdifact.Text = "Import EDIFACT Meldung";
            // 
            // tabPageASNSetting_VDAClientOutUpdate
            // 
            this.tabPageASNSetting_VDAClientOutUpdate.ItemSize = new System.Drawing.SizeF(123F, 28F);
            this.tabPageASNSetting_VDAClientOutUpdate.Location = new System.Drawing.Point(0, 0);
            this.tabPageASNSetting_VDAClientOutUpdate.Name = "tabPageASNSetting_VDAClientOutUpdate";
            this.tabPageASNSetting_VDAClientOutUpdate.Size = new System.Drawing.Size(200, 100);
            this.tabPageASNSetting_VDAClientOutUpdate.Text = "VDAClientOutUpdate";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Location = new System.Drawing.Point(78, 6);
            this.miniToolStrip.myColorFrom = System.Drawing.Color.Azure;
            this.miniToolStrip.myColorTo = System.Drawing.Color.Blue;
            this.miniToolStrip.myUnderlineColor = System.Drawing.Color.White;
            this.miniToolStrip.myUnderlined = true;
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(81, 31);
            this.miniToolStrip.TabIndex = 83;
            // 
            // ctrASNMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scASNMain);
            this.Name = "ctrASNMain";
            this.Size = new System.Drawing.Size(1097, 805);
            this.Load += new System.EventHandler(this.ctrASNMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scASNMain)).EndInit();
            this.scASNMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panAuftraggeberADR)).EndInit();
            this.panAuftraggeberADR.ResumeLayout(false);
            this.panAuftraggeberADR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmReportSettingEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panEditdata)).EndInit();
            this.panEditdata.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pageViewASNSetting)).EndInit();
            this.pageViewASNSetting.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer scASNMain;
        private Telerik.WinControls.UI.SplitPanel panAuftraggeberADR;
        private Telerik.WinControls.UI.SplitPanel panEditdata;
        private Telerik.WinControls.UI.RadPageView pageViewASNSetting;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_VDAClientOut;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_ASNAction;
        private System.Windows.Forms.Button btnSearchA;
        private System.Windows.Forms.TextBox tbAuftraggeber;
        private System.Windows.Forms.TextBox tbSearchA;
        private Telerik.WinControls.UI.RadMenu rmReportSettingEdit;
        private Telerik.WinControls.UI.RadMenuItem btnSearch;
        private Telerik.WinControls.UI.RadMenuButtonItem btnSave;
        private Telerik.WinControls.UI.RadMenuButtonItem btnDelete;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_AdrVerweise;
        private AFToolStrip miniToolStrip;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_Orga;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_Jobs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboASNArt;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_ASNArtFieldAssignment;
        private Telerik.WinControls.UI.RadMenuItem btnCloseMain;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_VDAClientWorkspaceValue;
        private System.Windows.Forms.NumericUpDown nudAdrIdDirect;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_ASNMessagesTest;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_EDIFACTClientOut;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_tabPageImportEdifact;
        private Telerik.WinControls.UI.RadPageViewPage tabPageASNSetting_VDAClientOutUpdate;
    }
}
