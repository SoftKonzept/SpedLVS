namespace Sped4.Controls.AdminCockpit
{
    partial class ctrCronJobs
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
            this.scMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.dgvCronJob = new Telerik.WinControls.UI.RadGridView();
            this.menuCronJobList = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.panCronJobEdit = new System.Windows.Forms.Panel();
            this.tbAuftraggeber = new System.Windows.Forms.TextBox();
            this.btnSearchA = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudAdrIdDirect = new System.Windows.Forms.NumericUpDown();
            this.comboPeriode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpActionBis = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtpActionVon = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dtpActionDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.tbBeschreibung = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboAction = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lCronJobId = new System.Windows.Forms.Label();
            this.tbCronJobId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cbAktiv = new System.Windows.Forms.CheckBox();
            this.menuCronJobEdit = new Sped4.Controls.AFToolStrip();
            this.tsbtnCronJobNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCronJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCronJob.MasterTemplate)).BeginInit();
            this.menuCronJobList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.panCronJobEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpActionBis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpActionVon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpActionDate)).BeginInit();
            this.menuCronJobEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Controls.Add(this.splitPanel1);
            this.scMain.Controls.Add(this.splitPanel2);
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Size = new System.Drawing.Size(662, 547);
            this.scMain.SplitterWidth = 8;
            this.scMain.TabIndex = 0;
            this.scMain.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.dgvCronJob);
            this.splitPanel1.Controls.Add(this.menuCronJobList);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            this.splitPanel1.Size = new System.Drawing.Size(324, 547);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // dgvCronJob
            // 
            this.dgvCronJob.BackColor = System.Drawing.Color.White;
            this.dgvCronJob.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvCronJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCronJob.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvCronJob.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvCronJob.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvCronJob.Location = new System.Drawing.Point(0, 25);
            // 
            // 
            // 
            this.dgvCronJob.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvCronJob.MasterTemplate.EnableFiltering = true;
            this.dgvCronJob.MasterTemplate.ShowFilteringRow = false;
            this.dgvCronJob.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvCronJob.MasterTemplate.ViewDefinition = tableViewDefinition3;
            this.dgvCronJob.Name = "dgvCronJob";
            this.dgvCronJob.ReadOnly = true;
            this.dgvCronJob.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvCronJob.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 240, 150);
            this.dgvCronJob.Size = new System.Drawing.Size(324, 522);
            this.dgvCronJob.TabIndex = 28;
            this.dgvCronJob.ThemeName = "ControlDefault";
            this.dgvCronJob.SelectionChanged += new System.EventHandler(this.dgvCronJob_SelectionChanged);
            this.dgvCronJob.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvCronJob_MouseClick);
            this.dgvCronJob.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvCronJob_MouseDoubleClick);
            // 
            // menuCronJobList
            // 
            this.menuCronJobList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh});
            this.menuCronJobList.Location = new System.Drawing.Point(0, 0);
            this.menuCronJobList.myColorFrom = System.Drawing.Color.Azure;
            this.menuCronJobList.myColorTo = System.Drawing.Color.Blue;
            this.menuCronJobList.myUnderlineColor = System.Drawing.Color.White;
            this.menuCronJobList.myUnderlined = true;
            this.menuCronJobList.Name = "menuCronJobList";
            this.menuCronJobList.Size = new System.Drawing.Size(324, 25);
            this.menuCronJobList.TabIndex = 10;
            this.menuCronJobList.Text = "afToolStrip1";
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
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.panCronJobEdit);
            this.splitPanel2.Controls.Add(this.menuCronJobEdit);
            this.splitPanel2.Location = new System.Drawing.Point(332, 0);
            this.splitPanel2.Name = "splitPanel2";
            this.splitPanel2.Size = new System.Drawing.Size(330, 547);
            this.splitPanel2.SizeInfo.MaximumSize = new System.Drawing.Size(335, 0);
            this.splitPanel2.SizeInfo.MinimumSize = new System.Drawing.Size(330, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // panCronJobEdit
            // 
            this.panCronJobEdit.BackColor = System.Drawing.Color.White;
            this.panCronJobEdit.Controls.Add(this.tbAuftraggeber);
            this.panCronJobEdit.Controls.Add(this.btnSearchA);
            this.panCronJobEdit.Controls.Add(this.label1);
            this.panCronJobEdit.Controls.Add(this.nudAdrIdDirect);
            this.panCronJobEdit.Controls.Add(this.comboPeriode);
            this.panCronJobEdit.Controls.Add(this.label7);
            this.panCronJobEdit.Controls.Add(this.label6);
            this.panCronJobEdit.Controls.Add(this.label5);
            this.panCronJobEdit.Controls.Add(this.dtpActionBis);
            this.panCronJobEdit.Controls.Add(this.dtpActionVon);
            this.panCronJobEdit.Controls.Add(this.dtpActionDate);
            this.panCronJobEdit.Controls.Add(this.tbBeschreibung);
            this.panCronJobEdit.Controls.Add(this.label4);
            this.panCronJobEdit.Controls.Add(this.comboAction);
            this.panCronJobEdit.Controls.Add(this.label3);
            this.panCronJobEdit.Controls.Add(this.lCronJobId);
            this.panCronJobEdit.Controls.Add(this.tbCronJobId);
            this.panCronJobEdit.Controls.Add(this.label2);
            this.panCronJobEdit.Controls.Add(this.label26);
            this.panCronJobEdit.Controls.Add(this.cbAktiv);
            this.panCronJobEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panCronJobEdit.Location = new System.Drawing.Point(0, 31);
            this.panCronJobEdit.Name = "panCronJobEdit";
            this.panCronJobEdit.Size = new System.Drawing.Size(330, 516);
            this.panCronJobEdit.TabIndex = 85;
            // 
            // tbAuftraggeber
            // 
            this.tbAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuftraggeber.Enabled = false;
            this.tbAuftraggeber.Location = new System.Drawing.Point(19, 76);
            this.tbAuftraggeber.Name = "tbAuftraggeber";
            this.tbAuftraggeber.ReadOnly = true;
            this.tbAuftraggeber.Size = new System.Drawing.Size(297, 20);
            this.tbAuftraggeber.TabIndex = 240;
            // 
            // btnSearchA
            // 
            this.btnSearchA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchA.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchA.Location = new System.Drawing.Point(191, 44);
            this.btnSearchA.Name = "btnSearchA";
            this.btnSearchA.Size = new System.Drawing.Size(125, 26);
            this.btnSearchA.TabIndex = 239;
            this.btnSearchA.TabStop = false;
            this.btnSearchA.Text = "[Auftraggeber]";
            this.btnSearchA.UseVisualStyleBackColor = true;
            this.btnSearchA.Click += new System.EventHandler(this.btnSearchA_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(16, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 238;
            this.label1.Text = "Adr-Id:";
            // 
            // nudAdrIdDirect
            // 
            this.nudAdrIdDirect.Location = new System.Drawing.Point(112, 49);
            this.nudAdrIdDirect.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudAdrIdDirect.Name = "nudAdrIdDirect";
            this.nudAdrIdDirect.Size = new System.Drawing.Size(64, 20);
            this.nudAdrIdDirect.TabIndex = 237;
            this.nudAdrIdDirect.Leave += new System.EventHandler(this.nudAdrIdDirect_Leave);
            // 
            // comboPeriode
            // 
            this.comboPeriode.FormattingEnabled = true;
            this.comboPeriode.Location = new System.Drawing.Point(112, 450);
            this.comboPeriode.Name = "comboPeriode";
            this.comboPeriode.Size = new System.Drawing.Size(204, 21);
            this.comboPeriode.TabIndex = 236;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(16, 374);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 235;
            this.label7.Text = "Aktions-Datum:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(16, 400);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 234;
            this.label6.Text = "von:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(16, 426);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 233;
            this.label5.Text = "bis:";
            // 
            // dtpActionBis
            // 
            this.dtpActionBis.CalendarSize = new System.Drawing.Size(290, 320);
            this.dtpActionBis.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpActionBis.Location = new System.Drawing.Point(112, 424);
            this.dtpActionBis.Name = "dtpActionBis";
            this.dtpActionBis.Size = new System.Drawing.Size(204, 20);
            this.dtpActionBis.TabIndex = 232;
            this.dtpActionBis.TabStop = false;
            this.dtpActionBis.Text = "16.10.2017 12:46";
            this.dtpActionBis.ThemeName = "ControlDefault";
            this.dtpActionBis.Value = new System.DateTime(2017, 10, 16, 12, 46, 12, 156);
            // 
            // dtpActionVon
            // 
            this.dtpActionVon.CalendarSize = new System.Drawing.Size(290, 320);
            this.dtpActionVon.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpActionVon.Location = new System.Drawing.Point(112, 398);
            this.dtpActionVon.Name = "dtpActionVon";
            this.dtpActionVon.Size = new System.Drawing.Size(204, 20);
            this.dtpActionVon.TabIndex = 231;
            this.dtpActionVon.TabStop = false;
            this.dtpActionVon.Text = "16.10.2017 12:46";
            this.dtpActionVon.ThemeName = "ControlDefault";
            this.dtpActionVon.Value = new System.DateTime(2017, 10, 16, 12, 46, 12, 156);
            // 
            // dtpActionDate
            // 
            this.dtpActionDate.CalendarSize = new System.Drawing.Size(290, 320);
            this.dtpActionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpActionDate.Location = new System.Drawing.Point(112, 372);
            this.dtpActionDate.Name = "dtpActionDate";
            this.dtpActionDate.Size = new System.Drawing.Size(204, 20);
            this.dtpActionDate.TabIndex = 230;
            this.dtpActionDate.TabStop = false;
            this.dtpActionDate.Text = "16.10.2017 12:46";
            this.dtpActionDate.ThemeName = "ControlDefault";
            this.dtpActionDate.Value = new System.DateTime(2017, 10, 16, 12, 46, 12, 156);
            // 
            // tbBeschreibung
            // 
            this.tbBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBeschreibung.Location = new System.Drawing.Point(112, 177);
            this.tbBeschreibung.Multiline = true;
            this.tbBeschreibung.Name = "tbBeschreibung";
            this.tbBeschreibung.Size = new System.Drawing.Size(204, 172);
            this.tbBeschreibung.TabIndex = 229;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(16, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 228;
            this.label4.Text = "Beschreibung:";
            // 
            // comboAction
            // 
            this.comboAction.FormattingEnabled = true;
            this.comboAction.Location = new System.Drawing.Point(112, 140);
            this.comboAction.Name = "comboAction";
            this.comboAction.Size = new System.Drawing.Size(204, 21);
            this.comboAction.TabIndex = 227;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(16, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 226;
            this.label3.Text = "Aktion:";
            // 
            // lCronJobId
            // 
            this.lCronJobId.AutoSize = true;
            this.lCronJobId.ForeColor = System.Drawing.Color.DarkBlue;
            this.lCronJobId.Location = new System.Drawing.Point(16, 20);
            this.lCronJobId.Name = "lCronJobId";
            this.lCronJobId.Size = new System.Drawing.Size(67, 13);
            this.lCronJobId.TabIndex = 225;
            this.lCronJobId.Text = "CronJob ID:";
            // 
            // tbCronJobId
            // 
            this.tbCronJobId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCronJobId.Location = new System.Drawing.Point(112, 18);
            this.tbCronJobId.Name = "tbCronJobId";
            this.tbCronJobId.Size = new System.Drawing.Size(204, 20);
            this.tbCronJobId.TabIndex = 224;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(16, 456);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 221;
            this.label2.Text = "Periode:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.DarkBlue;
            this.label26.Location = new System.Drawing.Point(16, 106);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(42, 13);
            this.label26.TabIndex = 219;
            this.label26.Text = "Status:";
            // 
            // cbAktiv
            // 
            this.cbAktiv.AutoSize = true;
            this.cbAktiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAktiv.Location = new System.Drawing.Point(269, 104);
            this.cbAktiv.Name = "cbAktiv";
            this.cbAktiv.Size = new System.Drawing.Size(47, 17);
            this.cbAktiv.TabIndex = 218;
            this.cbAktiv.Text = "aktiv";
            this.cbAktiv.UseVisualStyleBackColor = true;
            // 
            // menuCronJobEdit
            // 
            this.menuCronJobEdit.AutoSize = false;
            this.menuCronJobEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnCronJobNew,
            this.tsbtnSave,
            this.tsbtnDelete});
            this.menuCronJobEdit.Location = new System.Drawing.Point(0, 0);
            this.menuCronJobEdit.myColorFrom = System.Drawing.Color.Azure;
            this.menuCronJobEdit.myColorTo = System.Drawing.Color.Blue;
            this.menuCronJobEdit.myUnderlineColor = System.Drawing.Color.White;
            this.menuCronJobEdit.myUnderlined = true;
            this.menuCronJobEdit.Name = "menuCronJobEdit";
            this.menuCronJobEdit.Size = new System.Drawing.Size(330, 31);
            this.menuCronJobEdit.TabIndex = 84;
            this.menuCronJobEdit.Text = "afToolStrip1";
            // 
            // tsbtnCronJobNew
            // 
            this.tsbtnCronJobNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCronJobNew.Image = global::Sped4.Properties.Resources.add_24x24;
            this.tsbtnCronJobNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCronJobNew.Name = "tsbtnCronJobNew";
            this.tsbtnCronJobNew.Size = new System.Drawing.Size(23, 28);
            this.tsbtnCronJobNew.Text = "Neue Cronjob anlegen";
            this.tsbtnCronJobNew.Click += new System.EventHandler(this.tsbtnCronJobNew_Click);
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
            // ctrCronJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Name = "ctrCronJobs";
            this.Size = new System.Drawing.Size(662, 547);
            this.Load += new System.EventHandler(this.ctrCronJobs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCronJob.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCronJob)).EndInit();
            this.menuCronJobList.ResumeLayout(false);
            this.menuCronJobList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.panCronJobEdit.ResumeLayout(false);
            this.panCronJobEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpActionBis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpActionVon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpActionDate)).EndInit();
            this.menuCronJobEdit.ResumeLayout(false);
            this.menuCronJobEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer scMain;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private AFToolStrip menuCronJobList;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private AFToolStrip menuCronJobEdit;
        private System.Windows.Forms.ToolStripButton tsbtnCronJobNew;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private Telerik.WinControls.UI.RadGridView dgvCronJob;
        private System.Windows.Forms.Panel panCronJobEdit;
        private System.Windows.Forms.ComboBox comboPeriode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private Telerik.WinControls.UI.RadDateTimePicker dtpActionBis;
        private Telerik.WinControls.UI.RadDateTimePicker dtpActionVon;
        private Telerik.WinControls.UI.RadDateTimePicker dtpActionDate;
        private System.Windows.Forms.TextBox tbBeschreibung;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboAction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lCronJobId;
        private System.Windows.Forms.TextBox tbCronJobId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox cbAktiv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAdrIdDirect;
        private System.Windows.Forms.Button btnSearchA;
        private System.Windows.Forms.TextBox tbAuftraggeber;
    }
}
