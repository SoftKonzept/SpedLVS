namespace Sped4
{
    partial class ctrTableOfAccount
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
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.scTableOfAccountContainer = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.dgvTableOfAccounts = new Telerik.WinControls.UI.RadGridView();
            this.mmPanelTableOfAccounts = new Sped4.Controls.AFMinMaxPanel();
            this.tsmExtraChargeList = new Sped4.Controls.AFToolStrip();
            this.tsbtnLayoutOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTableOfAccoutAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTableOfAccountChange = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTableOfAccountExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTableOfAccountRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTableOfAccountDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnTableOfAccountClose = new System.Windows.Forms.ToolStripButton();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.mmPanelExtraChargeEdit = new Sped4.Controls.AFMinMaxPanel();
            this.panTableOfAccountEdit = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.nudEditKontoNr = new System.Windows.Forms.NumericUpDown();
            this.dtpEditBis = new System.Windows.Forms.DateTimePicker();
            this.cbEditKontoText = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEditBeschreibung = new System.Windows.Forms.TextBox();
            this.lBeschreibung = new System.Windows.Forms.Label();
            this.lKontoNr = new System.Windows.Forms.Label();
            this.tsmExtraChargeEdit = new Sped4.Controls.AFToolStrip();
            this.tsbtnEditAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnEditSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnEditDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbnEditClose = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.scTableOfAccountContainer)).BeginInit();
            this.scTableOfAccountContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableOfAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableOfAccounts.MasterTemplate)).BeginInit();
            this.mmPanelTableOfAccounts.SuspendLayout();
            this.tsmExtraChargeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.mmPanelExtraChargeEdit.SuspendLayout();
            this.panTableOfAccountEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditKontoNr)).BeginInit();
            this.tsmExtraChargeEdit.SuspendLayout();
            this.SuspendLayout();
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
            this.afColorLabel1.myText = "Fibu - Kontenplan";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(911, 28);
            this.afColorLabel1.TabIndex = 11;
            this.afColorLabel1.Text = "afColorLabel2";
            // 
            // scTableOfAccountContainer
            // 
            this.scTableOfAccountContainer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.scTableOfAccountContainer.Controls.Add(this.splitPanel1);
            this.scTableOfAccountContainer.Controls.Add(this.splitPanel2);
            this.scTableOfAccountContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTableOfAccountContainer.Location = new System.Drawing.Point(0, 28);
            this.scTableOfAccountContainer.Name = "scTableOfAccountContainer";
            // 
            // 
            // 
            this.scTableOfAccountContainer.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 28, 200, 200);
            this.scTableOfAccountContainer.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scTableOfAccountContainer.Size = new System.Drawing.Size(911, 489);
            this.scTableOfAccountContainer.SplitterWidth = 4;
            this.scTableOfAccountContainer.TabIndex = 12;
            this.scTableOfAccountContainer.TabStop = false;
            this.scTableOfAccountContainer.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel1.Controls.Add(this.dgvTableOfAccounts);
            this.splitPanel1.Controls.Add(this.mmPanelTableOfAccounts);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(454, 489);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // dgvTableOfAccounts
            // 
            this.dgvTableOfAccounts.BackColor = System.Drawing.Color.White;
            this.dgvTableOfAccounts.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvTableOfAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableOfAccounts.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvTableOfAccounts.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvTableOfAccounts.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvTableOfAccounts.Location = new System.Drawing.Point(0, 58);
            // 
            // dgvTableOfAccounts
            // 
            this.dgvTableOfAccounts.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvTableOfAccounts.MasterTemplate.EnableFiltering = true;
            this.dgvTableOfAccounts.MasterTemplate.ShowFilteringRow = false;
            this.dgvTableOfAccounts.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvTableOfAccounts.Name = "dgvTableOfAccounts";
            this.dgvTableOfAccounts.ReadOnly = true;
            this.dgvTableOfAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvTableOfAccounts.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 58, 240, 150);
            this.dgvTableOfAccounts.ShowHeaderCellButtons = true;
            this.dgvTableOfAccounts.Size = new System.Drawing.Size(454, 431);
            this.dgvTableOfAccounts.TabIndex = 27;
            this.dgvTableOfAccounts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvTableOfAccounts_MouseClick);
            this.dgvTableOfAccounts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvTableOfAccounts_MouseDoubleClick);
            // 
            // mmPanelTableOfAccounts
            // 
            this.mmPanelTableOfAccounts.BackColor = System.Drawing.Color.White;
            this.mmPanelTableOfAccounts.Controls.Add(this.tsmExtraChargeList);
            this.mmPanelTableOfAccounts.Dock = System.Windows.Forms.DockStyle.Top;
            this.mmPanelTableOfAccounts.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.mmPanelTableOfAccounts.Location = new System.Drawing.Point(0, 0);
            this.mmPanelTableOfAccounts.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.mmPanelTableOfAccounts.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmPanelTableOfAccounts.myImage = null;
            this.mmPanelTableOfAccounts.myText = "Optionen";
            this.mmPanelTableOfAccounts.Name = "mmPanelTableOfAccounts";
            this.mmPanelTableOfAccounts.Size = new System.Drawing.Size(454, 58);
            this.mmPanelTableOfAccounts.TabIndex = 4;
            this.mmPanelTableOfAccounts.Text = "afMinMaxPanel1";
            // 
            // tsmExtraChargeList
            // 
            this.tsmExtraChargeList.Dock = System.Windows.Forms.DockStyle.None;
            this.tsmExtraChargeList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnLayoutOpen,
            this.tsbtnTableOfAccoutAdd,
            this.tsbtnTableOfAccountChange,
            this.tsbtnTableOfAccountExcel,
            this.tsbtnTableOfAccountRefresh,
            this.tsbtnTableOfAccountDelete,
            this.tsbtnTableOfAccountClose});
            this.tsmExtraChargeList.Location = new System.Drawing.Point(3, 28);
            this.tsmExtraChargeList.myColorFrom = System.Drawing.Color.Azure;
            this.tsmExtraChargeList.myColorTo = System.Drawing.Color.Blue;
            this.tsmExtraChargeList.myUnderlineColor = System.Drawing.Color.White;
            this.tsmExtraChargeList.myUnderlined = true;
            this.tsmExtraChargeList.Name = "tsmExtraChargeList";
            this.tsmExtraChargeList.Size = new System.Drawing.Size(173, 25);
            this.tsmExtraChargeList.TabIndex = 8;
            this.tsmExtraChargeList.Text = "afToolStrip1";
            // 
            // tsbtnLayoutOpen
            // 
            this.tsbtnLayoutOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnLayoutOpen.Image = global::Sped4.Properties.Resources.layout_left;
            this.tsbtnLayoutOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLayoutOpen.Name = "tsbtnLayoutOpen";
            this.tsbtnLayoutOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbtnLayoutOpen.Text = "Sonderkostenerfassung ein-/ausblenden";
            this.tsbtnLayoutOpen.Click += new System.EventHandler(this.tsbtnLayoutOpen_Click);
            // 
            // tsbtnTableOfAccoutAdd
            // 
            this.tsbtnTableOfAccoutAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTableOfAccoutAdd.Image = global::Sped4.Properties.Resources.form_green_add;
            this.tsbtnTableOfAccoutAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTableOfAccoutAdd.Name = "tsbtnTableOfAccoutAdd";
            this.tsbtnTableOfAccoutAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTableOfAccoutAdd.Text = "Neues Konto erstellen";
            this.tsbtnTableOfAccoutAdd.Click += new System.EventHandler(this.tsbtnTableOfAccoutAdd_Click);
            // 
            // tsbtnTableOfAccountChange
            // 
            this.tsbtnTableOfAccountChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTableOfAccountChange.Image = global::Sped4.Properties.Resources.form_green_edit;
            this.tsbtnTableOfAccountChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTableOfAccountChange.Name = "tsbtnTableOfAccountChange";
            this.tsbtnTableOfAccountChange.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTableOfAccountChange.Text = "Konto ändern";
            this.tsbtnTableOfAccountChange.Click += new System.EventHandler(this.tsbtnTableOfAccountChange_Click);
            // 
            // tsbtnTableOfAccountExcel
            // 
            this.tsbtnTableOfAccountExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTableOfAccountExcel.Image = global::Sped4.Properties.Resources.Excel;
            this.tsbtnTableOfAccountExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTableOfAccountExcel.Name = "tsbtnTableOfAccountExcel";
            this.tsbtnTableOfAccountExcel.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTableOfAccountExcel.Text = "Excel Export";
            this.tsbtnTableOfAccountExcel.Click += new System.EventHandler(this.tsbtnTableOfAccountExcel_Click);
            // 
            // tsbtnTableOfAccountRefresh
            // 
            this.tsbtnTableOfAccountRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTableOfAccountRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnTableOfAccountRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTableOfAccountRefresh.Name = "tsbtnTableOfAccountRefresh";
            this.tsbtnTableOfAccountRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTableOfAccountRefresh.Text = "aktualisieren";
            this.tsbtnTableOfAccountRefresh.Click += new System.EventHandler(this.tsbtnTableOfAccountRefresh_Click);
            // 
            // tsbtnTableOfAccountDelete
            // 
            this.tsbtnTableOfAccountDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTableOfAccountDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnTableOfAccountDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTableOfAccountDelete.Name = "tsbtnTableOfAccountDelete";
            this.tsbtnTableOfAccountDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTableOfAccountDelete.Text = "Konto löschen";
            this.tsbtnTableOfAccountDelete.Click += new System.EventHandler(this.tsbtnTableOfAccountDelete_Click);
            // 
            // tsbtnTableOfAccountClose
            // 
            this.tsbtnTableOfAccountClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTableOfAccountClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnTableOfAccountClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTableOfAccountClose.Name = "tsbtnTableOfAccountClose";
            this.tsbtnTableOfAccountClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTableOfAccountClose.Text = "schliessen";
            this.tsbtnTableOfAccountClose.Click += new System.EventHandler(this.tsbtnTableOfAccountClose_Click);
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.Color.White;
            this.splitPanel2.Controls.Add(this.mmPanelExtraChargeEdit);
            this.splitPanel2.Location = new System.Drawing.Point(458, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.ControlBounds = new System.Drawing.Rectangle(458, 0, 200, 200);
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(453, 489);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // mmPanelExtraChargeEdit
            // 
            this.mmPanelExtraChargeEdit.BackColor = System.Drawing.Color.White;
            this.mmPanelExtraChargeEdit.Controls.Add(this.panTableOfAccountEdit);
            this.mmPanelExtraChargeEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.mmPanelExtraChargeEdit.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.mmPanelExtraChargeEdit.Location = new System.Drawing.Point(0, 0);
            this.mmPanelExtraChargeEdit.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.mmPanelExtraChargeEdit.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmPanelExtraChargeEdit.myImage = null;
            this.mmPanelExtraChargeEdit.myText = "Sonderkosten anlegen / bearbeiten";
            this.mmPanelExtraChargeEdit.Name = "mmPanelExtraChargeEdit";
            this.mmPanelExtraChargeEdit.Size = new System.Drawing.Size(453, 407);
            this.mmPanelExtraChargeEdit.TabIndex = 5;
            this.mmPanelExtraChargeEdit.Text = "afMinMaxPanel1";
            // 
            // panTableOfAccountEdit
            // 
            this.panTableOfAccountEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panTableOfAccountEdit.Controls.Add(this.label3);
            this.panTableOfAccountEdit.Controls.Add(this.nudEditKontoNr);
            this.panTableOfAccountEdit.Controls.Add(this.dtpEditBis);
            this.panTableOfAccountEdit.Controls.Add(this.cbEditKontoText);
            this.panTableOfAccountEdit.Controls.Add(this.label2);
            this.panTableOfAccountEdit.Controls.Add(this.label1);
            this.panTableOfAccountEdit.Controls.Add(this.tbEditBeschreibung);
            this.panTableOfAccountEdit.Controls.Add(this.lBeschreibung);
            this.panTableOfAccountEdit.Controls.Add(this.lKontoNr);
            this.panTableOfAccountEdit.Controls.Add(this.tsmExtraChargeEdit);
            this.panTableOfAccountEdit.Location = new System.Drawing.Point(5, 37);
            this.panTableOfAccountEdit.Name = "panTableOfAccountEdit";
            this.panTableOfAccountEdit.Size = new System.Drawing.Size(445, 356);
            this.panTableOfAccountEdit.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(12, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Verfällt nicht:";
            // 
            // nudEditKontoNr
            // 
            this.nudEditKontoNr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudEditKontoNr.Location = new System.Drawing.Point(110, 43);
            this.nudEditKontoNr.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudEditKontoNr.Name = "nudEditKontoNr";
            this.nudEditKontoNr.Size = new System.Drawing.Size(311, 20);
            this.nudEditKontoNr.TabIndex = 62;
            this.nudEditKontoNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpEditBis
            // 
            this.dtpEditBis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEditBis.Location = new System.Drawing.Point(110, 288);
            this.dtpEditBis.Name = "dtpEditBis";
            this.dtpEditBis.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpEditBis.Size = new System.Drawing.Size(311, 20);
            this.dtpEditBis.TabIndex = 61;
            // 
            // cbEditKontoText
            // 
            this.cbEditKontoText.AllowDrop = true;
            this.cbEditKontoText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEditKontoText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditKontoText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEditKontoText.FormattingEnabled = true;
            this.cbEditKontoText.Location = new System.Drawing.Point(110, 66);
            this.cbEditKontoText.Name = "cbEditKontoText";
            this.cbEditKontoText.Size = new System.Drawing.Size(311, 21);
            this.cbEditKontoText.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(16, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "Konto Text:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(12, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "Gültig Bis:";
            // 
            // tbEditBeschreibung
            // 
            this.tbEditBeschreibung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEditBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEditBeschreibung.Enabled = false;
            this.tbEditBeschreibung.Location = new System.Drawing.Point(110, 92);
            this.tbEditBeschreibung.Multiline = true;
            this.tbEditBeschreibung.Name = "tbEditBeschreibung";
            this.tbEditBeschreibung.Size = new System.Drawing.Size(311, 190);
            this.tbEditBeschreibung.TabIndex = 2;
            // 
            // lBeschreibung
            // 
            this.lBeschreibung.AutoSize = true;
            this.lBeschreibung.ForeColor = System.Drawing.Color.DarkBlue;
            this.lBeschreibung.Location = new System.Drawing.Point(16, 94);
            this.lBeschreibung.Name = "lBeschreibung";
            this.lBeschreibung.Size = new System.Drawing.Size(81, 13);
            this.lBeschreibung.TabIndex = 53;
            this.lBeschreibung.Text = "Beschreibung:";
            // 
            // lKontoNr
            // 
            this.lKontoNr.AutoSize = true;
            this.lKontoNr.ForeColor = System.Drawing.Color.DarkBlue;
            this.lKontoNr.Location = new System.Drawing.Point(16, 45);
            this.lKontoNr.Name = "lKontoNr";
            this.lKontoNr.Size = new System.Drawing.Size(60, 13);
            this.lKontoNr.TabIndex = 42;
            this.lKontoNr.Text = "Konto-Nr.:";
            // 
            // tsmExtraChargeEdit
            // 
            this.tsmExtraChargeEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnEditAdd,
            this.tsbtnEditSave,
            this.tsbtnEditDelete,
            this.tsbnEditClose});
            this.tsmExtraChargeEdit.Location = new System.Drawing.Point(0, 0);
            this.tsmExtraChargeEdit.myColorFrom = System.Drawing.Color.Azure;
            this.tsmExtraChargeEdit.myColorTo = System.Drawing.Color.Blue;
            this.tsmExtraChargeEdit.myUnderlineColor = System.Drawing.Color.White;
            this.tsmExtraChargeEdit.myUnderlined = true;
            this.tsmExtraChargeEdit.Name = "tsmExtraChargeEdit";
            this.tsmExtraChargeEdit.Size = new System.Drawing.Size(445, 25);
            this.tsmExtraChargeEdit.TabIndex = 9;
            this.tsmExtraChargeEdit.Text = "afToolStrip1";
            // 
            // tsbtnEditAdd
            // 
            this.tsbtnEditAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnEditAdd.Image = global::Sped4.Properties.Resources.add;
            this.tsbtnEditAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEditAdd.Name = "tsbtnEditAdd";
            this.tsbtnEditAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbtnEditAdd.Text = "Adressefassung ein-/ausblenden";
            this.tsbtnEditAdd.Click += new System.EventHandler(this.tsbtnEditAdd_Click);
            // 
            // tsbtnEditSave
            // 
            this.tsbtnEditSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnEditSave.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnEditSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEditSave.Name = "tsbtnEditSave";
            this.tsbtnEditSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnEditSave.Text = "Neue Adresse";
            this.tsbtnEditSave.Click += new System.EventHandler(this.tsbtnEditSave_Click);
            // 
            // tsbtnEditDelete
            // 
            this.tsbtnEditDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnEditDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnEditDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEditDelete.Name = "tsbtnEditDelete";
            this.tsbtnEditDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnEditDelete.Text = "Adresse löschen";
            this.tsbtnEditDelete.Click += new System.EventHandler(this.tsbtnEditDelete_Click);
            // 
            // tsbnEditClose
            // 
            this.tsbnEditClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbnEditClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbnEditClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbnEditClose.Name = "tsbnEditClose";
            this.tsbnEditClose.Size = new System.Drawing.Size(23, 22);
            this.tsbnEditClose.Text = "schliessen";
            this.tsbnEditClose.Click += new System.EventHandler(this.tsbnEditClose_Click);
            // 
            // ctrTableOfAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scTableOfAccountContainer);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrTableOfAccount";
            this.Size = new System.Drawing.Size(911, 517);
            this.Load += new System.EventHandler(this.ctrTableOfAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scTableOfAccountContainer)).EndInit();
            this.scTableOfAccountContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableOfAccounts.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableOfAccounts)).EndInit();
            this.mmPanelTableOfAccounts.ResumeLayout(false);
            this.mmPanelTableOfAccounts.PerformLayout();
            this.tsmExtraChargeList.ResumeLayout(false);
            this.tsmExtraChargeList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.mmPanelExtraChargeEdit.ResumeLayout(false);
            this.mmPanelExtraChargeEdit.PerformLayout();
            this.panTableOfAccountEdit.ResumeLayout(false);
            this.panTableOfAccountEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEditKontoNr)).EndInit();
            this.tsmExtraChargeEdit.ResumeLayout(false);
            this.tsmExtraChargeEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Telerik.WinControls.UI.RadSplitContainer scTableOfAccountContainer;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Controls.AFMinMaxPanel mmPanelTableOfAccounts;
        private Controls.AFToolStrip tsmExtraChargeList;
        private System.Windows.Forms.ToolStripButton tsbtnLayoutOpen;
        private System.Windows.Forms.ToolStripButton tsbtnTableOfAccoutAdd;
        private System.Windows.Forms.ToolStripButton tsbtnTableOfAccountChange;
        private System.Windows.Forms.ToolStripButton tsbtnTableOfAccountExcel;
        private System.Windows.Forms.ToolStripButton tsbtnTableOfAccountRefresh;
        private System.Windows.Forms.ToolStripButton tsbtnTableOfAccountDelete;
        private System.Windows.Forms.ToolStripButton tsbtnTableOfAccountClose;
        private Controls.AFMinMaxPanel mmPanelExtraChargeEdit;
        private System.Windows.Forms.Panel panTableOfAccountEdit;
        private System.Windows.Forms.ComboBox cbEditKontoText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbEditBeschreibung;
        private System.Windows.Forms.Label lBeschreibung;
        private System.Windows.Forms.Label lKontoNr;
        private Controls.AFToolStrip tsmExtraChargeEdit;
        private System.Windows.Forms.ToolStripButton tsbtnEditAdd;
        private System.Windows.Forms.ToolStripButton tsbtnEditSave;
        private System.Windows.Forms.ToolStripButton tsbtnEditDelete;
        private System.Windows.Forms.ToolStripButton tsbnEditClose;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.DateTimePicker dtpEditBis;
        private System.Windows.Forms.NumericUpDown nudEditKontoNr;
        private Telerik.WinControls.UI.RadGridView dgvTableOfAccounts;
        private System.Windows.Forms.Label label3;
    }
}
