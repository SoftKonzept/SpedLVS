namespace Import
{
    partial class frmMainImport
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panMain = new System.Windows.Forms.Panel();
            this.lCalcDate = new System.Windows.Forms.Label();
            this.dtpCalcDateToKeep = new System.Windows.Forms.DateTimePicker();
            this.cbGutImportIsUsedOnly = new System.Windows.Forms.CheckBox();
            this.cbCreateNewDatabase = new System.Windows.Forms.CheckBox();
            this.comboArbeitsbereich = new System.Windows.Forms.ComboBox();
            this.tbDestDB = new Telerik.WinControls.UI.RadTextBox();
            this.tbDestServer = new Telerik.WinControls.UI.RadTextBox();
            this.tbSourceServer = new Telerik.WinControls.UI.RadTextBox();
            this.tbSourceDB = new Telerik.WinControls.UI.RadTextBox();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.btnStartImport = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnClose = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnASNConnection = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnLM = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.scMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDestDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDestServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSourceServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSourceDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DB - Source:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "DB - Destination:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Arbeitsbereich:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Server - Destination:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Server - Source:";
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.White;
            this.panMain.Controls.Add(this.radButton1);
            this.panMain.Controls.Add(this.lCalcDate);
            this.panMain.Controls.Add(this.dtpCalcDateToKeep);
            this.panMain.Controls.Add(this.cbGutImportIsUsedOnly);
            this.panMain.Controls.Add(this.cbCreateNewDatabase);
            this.panMain.Controls.Add(this.comboArbeitsbereich);
            this.panMain.Controls.Add(this.tbDestDB);
            this.panMain.Controls.Add(this.tbDestServer);
            this.panMain.Controls.Add(this.tbSourceServer);
            this.panMain.Controls.Add(this.label4);
            this.panMain.Controls.Add(this.label2);
            this.panMain.Controls.Add(this.label6);
            this.panMain.Controls.Add(this.tbSourceDB);
            this.panMain.Controls.Add(this.label5);
            this.panMain.Controls.Add(this.label1);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(826, 188);
            this.panMain.TabIndex = 6;
            // 
            // lCalcDate
            // 
            this.lCalcDate.AutoSize = true;
            this.lCalcDate.Location = new System.Drawing.Point(573, 61);
            this.lCalcDate.Name = "lCalcDate";
            this.lCalcDate.Size = new System.Drawing.Size(230, 13);
            this.lCalcDate.TabIndex = 11;
            this.lCalcDate.Text = "zu berücksichtigendes Abrechnungsdatum:";
            // 
            // dtpCalcDateToKeep
            // 
            this.dtpCalcDateToKeep.Location = new System.Drawing.Point(604, 81);
            this.dtpCalcDateToKeep.Name = "dtpCalcDateToKeep";
            this.dtpCalcDateToKeep.Size = new System.Drawing.Size(199, 20);
            this.dtpCalcDateToKeep.TabIndex = 10;
            // 
            // cbGutImportIsUsedOnly
            // 
            this.cbGutImportIsUsedOnly.AutoSize = true;
            this.cbGutImportIsUsedOnly.Location = new System.Drawing.Point(573, 41);
            this.cbGutImportIsUsedOnly.Name = "cbGutImportIsUsedOnly";
            this.cbGutImportIsUsedOnly.Size = new System.Drawing.Size(230, 17);
            this.cbGutImportIsUsedOnly.TabIndex = 8;
            this.cbGutImportIsUsedOnly.Text = "Nur verwendete Güterarten importieren";
            this.cbGutImportIsUsedOnly.UseVisualStyleBackColor = true;
            // 
            // cbCreateNewDatabase
            // 
            this.cbCreateNewDatabase.AutoSize = true;
            this.cbCreateNewDatabase.Location = new System.Drawing.Point(573, 15);
            this.cbCreateNewDatabase.Name = "cbCreateNewDatabase";
            this.cbCreateNewDatabase.Size = new System.Drawing.Size(172, 17);
            this.cbCreateNewDatabase.TabIndex = 7;
            this.cbCreateNewDatabase.Text = "Import in \"NEUE\" Datenbank";
            this.cbCreateNewDatabase.UseVisualStyleBackColor = true;
            // 
            // comboArbeitsbereich
            // 
            this.comboArbeitsbereich.FormattingEnabled = true;
            this.comboArbeitsbereich.Location = new System.Drawing.Point(152, 140);
            this.comboArbeitsbereich.Name = "comboArbeitsbereich";
            this.comboArbeitsbereich.Size = new System.Drawing.Size(227, 21);
            this.comboArbeitsbereich.TabIndex = 6;
            // 
            // tbDestDB
            // 
            this.tbDestDB.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbDestDB.Location = new System.Drawing.Point(152, 104);
            this.tbDestDB.Name = "tbDestDB";
            // 
            // 
            // 
            this.tbDestDB.RootElement.ControlBounds = new System.Drawing.Rectangle(152, 104, 100, 20);
            this.tbDestDB.RootElement.StretchVertically = true;
            this.tbDestDB.Size = new System.Drawing.Size(406, 20);
            this.tbDestDB.TabIndex = 2;
            // 
            // tbDestServer
            // 
            this.tbDestServer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbDestServer.Location = new System.Drawing.Point(152, 79);
            this.tbDestServer.Name = "tbDestServer";
            // 
            // 
            // 
            this.tbDestServer.RootElement.ControlBounds = new System.Drawing.Rectangle(152, 79, 100, 20);
            this.tbDestServer.RootElement.StretchVertically = true;
            this.tbDestServer.Size = new System.Drawing.Size(406, 20);
            this.tbDestServer.TabIndex = 3;
            // 
            // tbSourceServer
            // 
            this.tbSourceServer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbSourceServer.Location = new System.Drawing.Point(152, 14);
            this.tbSourceServer.Name = "tbSourceServer";
            // 
            // 
            // 
            this.tbSourceServer.RootElement.ControlBounds = new System.Drawing.Rectangle(152, 14, 100, 20);
            this.tbSourceServer.RootElement.StretchVertically = true;
            this.tbSourceServer.Size = new System.Drawing.Size(406, 20);
            this.tbSourceServer.TabIndex = 3;
            // 
            // tbSourceDB
            // 
            this.tbSourceDB.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbSourceDB.Location = new System.Drawing.Point(152, 39);
            this.tbSourceDB.Name = "tbSourceDB";
            // 
            // 
            // 
            this.tbSourceDB.RootElement.ControlBounds = new System.Drawing.Rectangle(152, 39, 100, 20);
            this.tbSourceDB.RootElement.StretchVertically = true;
            this.tbSourceDB.Size = new System.Drawing.Size(406, 20);
            this.tbSourceDB.TabIndex = 3;
            // 
            // radMenu1
            // 
            this.radMenu1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnStartImport,
            this.btnClose,
            this.btnASNConnection,
            this.btnLM});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            // 
            // 
            // 
            this.radMenu1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 24);
            this.radMenu1.Size = new System.Drawing.Size(826, 23);
            this.radMenu1.TabIndex = 7;
            // 
            // btnStartImport
            // 
            // 
            // 
            // 
            this.btnStartImport.ButtonElement.UseCompatibleTextRendering = false;
            this.btnStartImport.Name = "btnStartImport";
            this.btnStartImport.Text = "Start Import";
            this.btnStartImport.Click += new System.EventHandler(this.btnStartImport_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnStartImport.GetChildAt(2))).UseCompatibleTextRendering = false;
            // 
            // btnClose
            // 
            // 
            // 
            // 
            this.btnClose.ButtonElement.UseCompatibleTextRendering = false;
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "schliessen";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(2))).UseCompatibleTextRendering = false;
            // 
            // btnASNConnection
            // 
            // 
            // 
            // 
            this.btnASNConnection.ButtonElement.UseCompatibleTextRendering = false;
            this.btnASNConnection.Name = "btnASNConnection";
            this.btnASNConnection.Text = "ASN";
            this.btnASNConnection.Click += new System.EventHandler(this.btnASNConnection_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnASNConnection.GetChildAt(2))).UseCompatibleTextRendering = false;
            // 
            // btnLM
            // 
            // 
            // 
            // 
            this.btnLM.ButtonElement.UseCompatibleTextRendering = false;
            this.btnLM.Name = "btnLM";
            this.btnLM.Text = "fehlende Lagermeldungen";
            this.btnLM.Click += new System.EventHandler(this.btnLM_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnLM.GetChildAt(2))).UseCompatibleTextRendering = false;
            // 
            // scMain
            // 
            this.scMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.scMain.Controls.Add(this.splitPanel1);
            this.scMain.Controls.Add(this.splitPanel2);
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 23);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.scMain.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 26, 200, 200);
            this.scMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scMain.Size = new System.Drawing.Size(826, 512);
            this.scMain.TabIndex = 8;
            this.scMain.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel1.Controls.Add(this.panMain);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(826, 188);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.129703F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -122);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel2.Controls.Add(this.lbLog);
            this.splitPanel2.Location = new System.Drawing.Point(0, 192);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 191, 200, 200);
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(826, 320);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.129703F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 122);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(0, 0);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(826, 320);
            this.lbLog.TabIndex = 0;
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(645, 137);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(110, 24);
            this.radButton1.TabIndex = 9;
            this.radButton1.Text = "radButton1";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // frmMainImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 535);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.radMenu1);
            this.Name = "frmMainImport";
            this.Text = "Import LVS-Daten für Sped4";
            this.Load += new System.EventHandler(this.frmMainImport_Load);
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDestDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDestServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSourceServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSourceDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panMain;
        private Telerik.WinControls.UI.RadTextBox tbDestDB;
        private Telerik.WinControls.UI.RadTextBox tbDestServer;
        private Telerik.WinControls.UI.RadTextBox tbSourceServer;
        private Telerik.WinControls.UI.RadTextBox tbSourceDB;
        private Telerik.WinControls.UI.RadMenu radMenu1;
        private Telerik.WinControls.UI.RadMenuButtonItem btnStartImport;
        private Telerik.WinControls.UI.RadMenuButtonItem btnClose;
        private System.Windows.Forms.ComboBox comboArbeitsbereich;
        private Telerik.WinControls.UI.RadSplitContainer scMain;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        public System.Windows.Forms.ListBox lbLog;
        private Telerik.WinControls.UI.RadMenuButtonItem btnASNConnection;
        private Telerik.WinControls.UI.RadMenuButtonItem btnLM;
        private System.Windows.Forms.CheckBox cbCreateNewDatabase;
        private System.Windows.Forms.CheckBox cbGutImportIsUsedOnly;
        private System.Windows.Forms.Label lCalcDate;
        private System.Windows.Forms.DateTimePicker dtpCalcDateToKeep;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}

