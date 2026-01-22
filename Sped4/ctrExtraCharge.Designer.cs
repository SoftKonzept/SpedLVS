namespace Sped4
{
    partial class ctrExtraCharge
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
            this.scExtraCharge = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.dgvExtraCharge = new Telerik.WinControls.UI.RadGridView();
            this.mmPanelExtraChargeList = new Sped4.Controls.AFMinMaxPanel();
            this.tsmExtraChargeList = new Sped4.Controls.AFToolStrip();
            this.tsbtnOpenEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbtnListNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnListChange = new System.Windows.Forms.ToolStripButton();
            this.tsbListExcelExport = new System.Windows.Forms.ToolStripButton();
            this.tsbtnListRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnListDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.mmPanelExtraChargeEdit = new Sped4.Controls.AFMinMaxPanel();
            this.panExtraChargeEdit = new System.Windows.Forms.Panel();
            this.cbIsGlobal = new System.Windows.Forms.CheckBox();
            this.tbExtraChargeKontoNr = new System.Windows.Forms.TextBox();
            this.cbExtraChargeKontoText = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lKontoNr = new System.Windows.Forms.Label();
            this.cbEinheit = new System.Windows.Forms.ComboBox();
            this.tbExtraChargePreis = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbExtraChargeRGText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbExtraChargeBeschreibung = new System.Windows.Forms.TextBox();
            this.lBeschreibung = new System.Windows.Forms.Label();
            this.tbExtraChargeBezeichnung = new System.Windows.Forms.TextBox();
            this.lNachname = new System.Windows.Forms.Label();
            this.tsmExtraChargeEdit = new Sped4.Controls.AFToolStrip();
            this.tsbtnExtraChargeAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExtraChargeSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExtraChargeDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbnExtraChargeEditClose = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.miniToolStrip = new Sped4.Controls.AFToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.scExtraCharge)).BeginInit();
            this.scExtraCharge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtraCharge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtraCharge.MasterTemplate)).BeginInit();
            this.mmPanelExtraChargeList.SuspendLayout();
            this.tsmExtraChargeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.mmPanelExtraChargeEdit.SuspendLayout();
            this.panExtraChargeEdit.SuspendLayout();
            this.tsmExtraChargeEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // scExtraCharge
            // 
            this.scExtraCharge.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.scExtraCharge.Controls.Add(this.splitPanel1);
            this.scExtraCharge.Controls.Add(this.splitPanel2);
            this.scExtraCharge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scExtraCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scExtraCharge.Location = new System.Drawing.Point(0, 34);
            this.scExtraCharge.Name = "scExtraCharge";
            // 
            // 
            // 
            this.scExtraCharge.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 28, 200, 200);
            this.scExtraCharge.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.scExtraCharge.Size = new System.Drawing.Size(1219, 717);
            this.scExtraCharge.SplitterWidth = 10;
            this.scExtraCharge.TabIndex = 0;
            this.scExtraCharge.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel1.Controls.Add(this.dgvExtraCharge);
            this.splitPanel1.Controls.Add(this.mmPanelExtraChargeList);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(604, 717);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-59, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // dgvExtraCharge
            // 
            this.dgvExtraCharge.BackColor = System.Drawing.Color.White;
            this.dgvExtraCharge.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvExtraCharge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtraCharge.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvExtraCharge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvExtraCharge.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvExtraCharge.Location = new System.Drawing.Point(0, 98);
            this.dgvExtraCharge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgvExtraCharge.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvExtraCharge.MasterTemplate.EnableFiltering = true;
            this.dgvExtraCharge.MasterTemplate.ShowFilteringRow = false;
            this.dgvExtraCharge.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvExtraCharge.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvExtraCharge.Name = "dgvExtraCharge";
            this.dgvExtraCharge.ReadOnly = true;
            this.dgvExtraCharge.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvExtraCharge.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 62, 240, 150);
            this.dgvExtraCharge.ShowHeaderCellButtons = true;
            this.dgvExtraCharge.Size = new System.Drawing.Size(604, 619);
            this.dgvExtraCharge.TabIndex = 26;
            this.dgvExtraCharge.ThemeName = "ControlDefault";
            this.dgvExtraCharge.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvExtraCharge_MouseClick);
            this.dgvExtraCharge.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvExtraCharge_MouseDoubleClick);
            // 
            // mmPanelExtraChargeList
            // 
            this.mmPanelExtraChargeList.BackColor = System.Drawing.Color.White;
            this.mmPanelExtraChargeList.Controls.Add(this.tsmExtraChargeList);
            this.mmPanelExtraChargeList.Dock = System.Windows.Forms.DockStyle.Top;
            this.mmPanelExtraChargeList.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.mmPanelExtraChargeList.Location = new System.Drawing.Point(0, 0);
            this.mmPanelExtraChargeList.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mmPanelExtraChargeList.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.mmPanelExtraChargeList.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmPanelExtraChargeList.myImage = null;
            this.mmPanelExtraChargeList.myText = "Optionen";
            this.mmPanelExtraChargeList.Name = "mmPanelExtraChargeList";
            this.mmPanelExtraChargeList.Size = new System.Drawing.Size(604, 98);
            this.mmPanelExtraChargeList.TabIndex = 3;
            this.mmPanelExtraChargeList.Text = "afMinMaxPanel1";
            // 
            // tsmExtraChargeList
            // 
            this.tsmExtraChargeList.Dock = System.Windows.Forms.DockStyle.None;
            this.tsmExtraChargeList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsmExtraChargeList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOpenEdit,
            this.tsbtnListNew,
            this.tsbtnListChange,
            this.tsbListExcelExport,
            this.tsbtnListRefresh,
            this.tsbtnListDelete,
            this.tsbClose});
            this.tsmExtraChargeList.Location = new System.Drawing.Point(5, 44);
            this.tsmExtraChargeList.myColorFrom = System.Drawing.Color.Azure;
            this.tsmExtraChargeList.myColorTo = System.Drawing.Color.Blue;
            this.tsmExtraChargeList.myUnderlineColor = System.Drawing.Color.White;
            this.tsmExtraChargeList.myUnderlined = true;
            this.tsmExtraChargeList.Name = "tsmExtraChargeList";
            this.tsmExtraChargeList.Size = new System.Drawing.Size(216, 27);
            this.tsmExtraChargeList.TabIndex = 8;
            this.tsmExtraChargeList.Text = "afToolStrip1";
            // 
            // tsbtnOpenEdit
            // 
            this.tsbtnOpenEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpenEdit.Image = global::Sped4.Properties.Resources.layout;
            this.tsbtnOpenEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpenEdit.Name = "tsbtnOpenEdit";
            this.tsbtnOpenEdit.Size = new System.Drawing.Size(29, 24);
            this.tsbtnOpenEdit.Text = "Nebenkostenerfassung ein-/ausblenden";
            this.tsbtnOpenEdit.Click += new System.EventHandler(this.tsbtnOpenEdit_Click);
            // 
            // tsbtnListNew
            // 
            this.tsbtnListNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnListNew.Image = global::Sped4.Properties.Resources.form_green_add;
            this.tsbtnListNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnListNew.Name = "tsbtnListNew";
            this.tsbtnListNew.Size = new System.Drawing.Size(29, 24);
            this.tsbtnListNew.Text = "Neue Nebenkosten anlegen";
            this.tsbtnListNew.Click += new System.EventHandler(this.tsbtnListNew_Click);
            // 
            // tsbtnListChange
            // 
            this.tsbtnListChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnListChange.Image = global::Sped4.Properties.Resources.form_green_edit;
            this.tsbtnListChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnListChange.Name = "tsbtnListChange";
            this.tsbtnListChange.Size = new System.Drawing.Size(29, 24);
            this.tsbtnListChange.Text = "Nebenkosten bearbeiten";
            this.tsbtnListChange.Click += new System.EventHandler(this.tsbtnListChange_Click);
            // 
            // tsbListExcelExport
            // 
            this.tsbListExcelExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbListExcelExport.Image = global::Sped4.Properties.Resources.Excel;
            this.tsbListExcelExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbListExcelExport.Name = "tsbListExcelExport";
            this.tsbListExcelExport.Size = new System.Drawing.Size(29, 24);
            this.tsbListExcelExport.Text = "Excel Export";
            this.tsbListExcelExport.Click += new System.EventHandler(this.tsbListExcelExport_Click);
            // 
            // tsbtnListRefresh
            // 
            this.tsbtnListRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnListRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnListRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnListRefresh.Name = "tsbtnListRefresh";
            this.tsbtnListRefresh.Size = new System.Drawing.Size(29, 24);
            this.tsbtnListRefresh.Text = "aktualisieren";
            this.tsbtnListRefresh.Click += new System.EventHandler(this.tsbtnListRefresh_Click);
            // 
            // tsbtnListDelete
            // 
            this.tsbtnListDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnListDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnListDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnListDelete.Name = "tsbtnListDelete";
            this.tsbtnListDelete.Size = new System.Drawing.Size(29, 24);
            this.tsbtnListDelete.Text = "Nebenkosten löschen";
            this.tsbtnListDelete.Click += new System.EventHandler(this.tsbtnListDelete_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(29, 24);
            this.tsbClose.Text = "schliessen";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.Color.White;
            this.splitPanel2.Controls.Add(this.mmPanelExtraChargeEdit);
            this.splitPanel2.Location = new System.Drawing.Point(614, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.ControlBounds = new System.Drawing.Rectangle(459, 0, 200, 200);
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel2.Size = new System.Drawing.Size(605, 717);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(59, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // mmPanelExtraChargeEdit
            // 
            this.mmPanelExtraChargeEdit.BackColor = System.Drawing.Color.White;
            this.mmPanelExtraChargeEdit.Controls.Add(this.panExtraChargeEdit);
            this.mmPanelExtraChargeEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mmPanelExtraChargeEdit.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.mmPanelExtraChargeEdit.Location = new System.Drawing.Point(0, 0);
            this.mmPanelExtraChargeEdit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.mmPanelExtraChargeEdit.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.mmPanelExtraChargeEdit.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmPanelExtraChargeEdit.myImage = null;
            this.mmPanelExtraChargeEdit.myText = "Nebenkosten anlegen / bearbeiten";
            this.mmPanelExtraChargeEdit.Name = "mmPanelExtraChargeEdit";
            this.mmPanelExtraChargeEdit.Size = new System.Drawing.Size(605, 717);
            this.mmPanelExtraChargeEdit.TabIndex = 4;
            this.mmPanelExtraChargeEdit.Text = "afMinMaxPanel1";
            // 
            // panExtraChargeEdit
            // 
            this.panExtraChargeEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panExtraChargeEdit.Controls.Add(this.cbIsGlobal);
            this.panExtraChargeEdit.Controls.Add(this.tbExtraChargeKontoNr);
            this.panExtraChargeEdit.Controls.Add(this.cbExtraChargeKontoText);
            this.panExtraChargeEdit.Controls.Add(this.label4);
            this.panExtraChargeEdit.Controls.Add(this.lKontoNr);
            this.panExtraChargeEdit.Controls.Add(this.cbEinheit);
            this.panExtraChargeEdit.Controls.Add(this.tbExtraChargePreis);
            this.panExtraChargeEdit.Controls.Add(this.label3);
            this.panExtraChargeEdit.Controls.Add(this.label2);
            this.panExtraChargeEdit.Controls.Add(this.tbExtraChargeRGText);
            this.panExtraChargeEdit.Controls.Add(this.label1);
            this.panExtraChargeEdit.Controls.Add(this.tbExtraChargeBeschreibung);
            this.panExtraChargeEdit.Controls.Add(this.lBeschreibung);
            this.panExtraChargeEdit.Controls.Add(this.tbExtraChargeBezeichnung);
            this.panExtraChargeEdit.Controls.Add(this.lNachname);
            this.panExtraChargeEdit.Controls.Add(this.tsmExtraChargeEdit);
            this.panExtraChargeEdit.Location = new System.Drawing.Point(5, 38);
            this.panExtraChargeEdit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panExtraChargeEdit.Name = "panExtraChargeEdit";
            this.panExtraChargeEdit.Size = new System.Drawing.Size(595, 675);
            this.panExtraChargeEdit.TabIndex = 2;
            // 
            // cbIsGlobal
            // 
            this.cbIsGlobal.AutoSize = true;
            this.cbIsGlobal.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbIsGlobal.Location = new System.Drawing.Point(172, 615);
            this.cbIsGlobal.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbIsGlobal.Name = "cbIsGlobal";
            this.cbIsGlobal.Size = new System.Drawing.Size(71, 21);
            this.cbIsGlobal.TabIndex = 65;
            this.cbIsGlobal.Text = "Global";
            this.cbIsGlobal.UseVisualStyleBackColor = true;
            // 
            // tbExtraChargeKontoNr
            // 
            this.tbExtraChargeKontoNr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtraChargeKontoNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExtraChargeKontoNr.Enabled = false;
            this.tbExtraChargeKontoNr.Location = new System.Drawing.Point(172, 556);
            this.tbExtraChargeKontoNr.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbExtraChargeKontoNr.Name = "tbExtraChargeKontoNr";
            this.tbExtraChargeKontoNr.Size = new System.Drawing.Size(385, 23);
            this.tbExtraChargeKontoNr.TabIndex = 7;
            // 
            // cbExtraChargeKontoText
            // 
            this.cbExtraChargeKontoText.AllowDrop = true;
            this.cbExtraChargeKontoText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExtraChargeKontoText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExtraChargeKontoText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbExtraChargeKontoText.FormattingEnabled = true;
            this.cbExtraChargeKontoText.Location = new System.Drawing.Point(172, 514);
            this.cbExtraChargeKontoText.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbExtraChargeKontoText.Name = "cbExtraChargeKontoText";
            this.cbExtraChargeKontoText.Size = new System.Drawing.Size(384, 24);
            this.cbExtraChargeKontoText.TabIndex = 6;
            this.cbExtraChargeKontoText.SelectedIndexChanged += new System.EventHandler(this.cbExtraChargeKontoText_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(19, 518);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 64;
            this.label4.Text = "Konto Text:";
            // 
            // lKontoNr
            // 
            this.lKontoNr.AutoSize = true;
            this.lKontoNr.ForeColor = System.Drawing.Color.DarkBlue;
            this.lKontoNr.Location = new System.Drawing.Point(19, 560);
            this.lKontoNr.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lKontoNr.Name = "lKontoNr";
            this.lKontoNr.Size = new System.Drawing.Size(73, 17);
            this.lKontoNr.TabIndex = 63;
            this.lKontoNr.Text = "Konto-Nr.:";
            // 
            // cbEinheit
            // 
            this.cbEinheit.AllowDrop = true;
            this.cbEinheit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEinheit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEinheit.FormattingEnabled = true;
            this.cbEinheit.Location = new System.Drawing.Point(175, 436);
            this.cbEinheit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbEinheit.Name = "cbEinheit";
            this.cbEinheit.Size = new System.Drawing.Size(486, 24);
            this.cbEinheit.TabIndex = 4;
            // 
            // tbExtraChargePreis
            // 
            this.tbExtraChargePreis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtraChargePreis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExtraChargePreis.Enabled = false;
            this.tbExtraChargePreis.Location = new System.Drawing.Point(172, 474);
            this.tbExtraChargePreis.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbExtraChargePreis.Name = "tbExtraChargePreis";
            this.tbExtraChargePreis.Size = new System.Drawing.Size(385, 23);
            this.tbExtraChargePreis.TabIndex = 5;
            this.tbExtraChargePreis.TextChanged += new System.EventHandler(this.tbExtraChargePreis_TextChanged);
            this.tbExtraChargePreis.Validated += new System.EventHandler(this.tbExtraChargePreis_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(19, 476);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 59;
            this.label3.Text = "Preis:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(19, 439);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 57;
            this.label2.Text = "Einheit:";
            // 
            // tbExtraChargeRGText
            // 
            this.tbExtraChargeRGText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtraChargeRGText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExtraChargeRGText.Enabled = false;
            this.tbExtraChargeRGText.Location = new System.Drawing.Point(172, 400);
            this.tbExtraChargeRGText.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbExtraChargeRGText.Name = "tbExtraChargeRGText";
            this.tbExtraChargeRGText.Size = new System.Drawing.Size(385, 23);
            this.tbExtraChargeRGText.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(19, 402);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 55;
            this.label1.Text = "RG-Text:";
            // 
            // tbExtraChargeBeschreibung
            // 
            this.tbExtraChargeBeschreibung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtraChargeBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExtraChargeBeschreibung.Enabled = false;
            this.tbExtraChargeBeschreibung.Location = new System.Drawing.Point(172, 98);
            this.tbExtraChargeBeschreibung.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbExtraChargeBeschreibung.Multiline = true;
            this.tbExtraChargeBeschreibung.Name = "tbExtraChargeBeschreibung";
            this.tbExtraChargeBeschreibung.Size = new System.Drawing.Size(385, 296);
            this.tbExtraChargeBeschreibung.TabIndex = 2;
            // 
            // lBeschreibung
            // 
            this.lBeschreibung.AutoSize = true;
            this.lBeschreibung.ForeColor = System.Drawing.Color.DarkBlue;
            this.lBeschreibung.Location = new System.Drawing.Point(19, 100);
            this.lBeschreibung.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lBeschreibung.Name = "lBeschreibung";
            this.lBeschreibung.Size = new System.Drawing.Size(99, 17);
            this.lBeschreibung.TabIndex = 53;
            this.lBeschreibung.Text = "Beschreibung:";
            // 
            // tbExtraChargeBezeichnung
            // 
            this.tbExtraChargeBezeichnung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExtraChargeBezeichnung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExtraChargeBezeichnung.Enabled = false;
            this.tbExtraChargeBezeichnung.Location = new System.Drawing.Point(172, 60);
            this.tbExtraChargeBezeichnung.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbExtraChargeBezeichnung.Name = "tbExtraChargeBezeichnung";
            this.tbExtraChargeBezeichnung.Size = new System.Drawing.Size(385, 23);
            this.tbExtraChargeBezeichnung.TabIndex = 1;
            // 
            // lNachname
            // 
            this.lNachname.AutoSize = true;
            this.lNachname.ForeColor = System.Drawing.Color.DarkBlue;
            this.lNachname.Location = new System.Drawing.Point(19, 65);
            this.lNachname.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lNachname.Name = "lNachname";
            this.lNachname.Size = new System.Drawing.Size(94, 17);
            this.lNachname.TabIndex = 42;
            this.lNachname.Text = "Bezeichnung:";
            // 
            // tsmExtraChargeEdit
            // 
            this.tsmExtraChargeEdit.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsmExtraChargeEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnExtraChargeAdd,
            this.tsbtnExtraChargeSave,
            this.tsbtnExtraChargeDelete,
            this.tsbnExtraChargeEditClose});
            this.tsmExtraChargeEdit.Location = new System.Drawing.Point(0, 0);
            this.tsmExtraChargeEdit.myColorFrom = System.Drawing.Color.Azure;
            this.tsmExtraChargeEdit.myColorTo = System.Drawing.Color.Blue;
            this.tsmExtraChargeEdit.myUnderlineColor = System.Drawing.Color.White;
            this.tsmExtraChargeEdit.myUnderlined = true;
            this.tsmExtraChargeEdit.Name = "tsmExtraChargeEdit";
            this.tsmExtraChargeEdit.Size = new System.Drawing.Size(595, 27);
            this.tsmExtraChargeEdit.TabIndex = 9;
            this.tsmExtraChargeEdit.Text = "afToolStrip1";
            // 
            // tsbtnExtraChargeAdd
            // 
            this.tsbtnExtraChargeAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExtraChargeAdd.Image = global::Sped4.Properties.Resources.add;
            this.tsbtnExtraChargeAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExtraChargeAdd.Name = "tsbtnExtraChargeAdd";
            this.tsbtnExtraChargeAdd.Size = new System.Drawing.Size(29, 24);
            this.tsbtnExtraChargeAdd.Text = "Neue Nebenkosten anlegen";
            this.tsbtnExtraChargeAdd.Click += new System.EventHandler(this.tsbtnExtraChargeAdd_Click);
            // 
            // tsbtnExtraChargeSave
            // 
            this.tsbtnExtraChargeSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExtraChargeSave.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnExtraChargeSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExtraChargeSave.Name = "tsbtnExtraChargeSave";
            this.tsbtnExtraChargeSave.Size = new System.Drawing.Size(29, 24);
            this.tsbtnExtraChargeSave.Text = "Nebenkosten speichern";
            this.tsbtnExtraChargeSave.Click += new System.EventHandler(this.tsbtnExtraChargeSave_Click);
            // 
            // tsbtnExtraChargeDelete
            // 
            this.tsbtnExtraChargeDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExtraChargeDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnExtraChargeDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExtraChargeDelete.Name = "tsbtnExtraChargeDelete";
            this.tsbtnExtraChargeDelete.Size = new System.Drawing.Size(29, 24);
            this.tsbtnExtraChargeDelete.Text = "Nebenkosten löschen";
            this.tsbtnExtraChargeDelete.Click += new System.EventHandler(this.tsbtnExtraChargeDelete_Click);
            // 
            // tsbnExtraChargeEditClose
            // 
            this.tsbnExtraChargeEditClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbnExtraChargeEditClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbnExtraChargeEditClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbnExtraChargeEditClose.Name = "tsbnExtraChargeEditClose";
            this.tsbnExtraChargeEditClose.Size = new System.Drawing.Size(29, 24);
            this.tsbnExtraChargeEditClose.Text = "schliessen";
            this.tsbnExtraChargeEditClose.Click += new System.EventHandler(this.tsbnExtraChargeEditClose_Click);
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
            this.afColorLabel1.myText = "Nebenkosten";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(1219, 34);
            this.afColorLabel1.TabIndex = 10;
            this.afColorLabel1.Text = "afColorLabel2";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.miniToolStrip.Location = new System.Drawing.Point(1, 25);
            this.miniToolStrip.myColorFrom = System.Drawing.Color.Azure;
            this.miniToolStrip.myColorTo = System.Drawing.Color.Blue;
            this.miniToolStrip.myUnderlineColor = System.Drawing.Color.White;
            this.miniToolStrip.myUnderlined = true;
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(274, 25);
            this.miniToolStrip.TabIndex = 8;
            // 
            // ctrExtraCharge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scExtraCharge);
            this.Controls.Add(this.afColorLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrExtraCharge";
            this.Size = new System.Drawing.Size(1219, 751);
            this.Load += new System.EventHandler(this.ctrExtraCharge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scExtraCharge)).EndInit();
            this.scExtraCharge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtraCharge.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtraCharge)).EndInit();
            this.mmPanelExtraChargeList.ResumeLayout(false);
            this.mmPanelExtraChargeList.PerformLayout();
            this.tsmExtraChargeList.ResumeLayout(false);
            this.tsmExtraChargeList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.mmPanelExtraChargeEdit.ResumeLayout(false);
            this.mmPanelExtraChargeEdit.PerformLayout();
            this.panExtraChargeEdit.ResumeLayout(false);
            this.panExtraChargeEdit.PerformLayout();
            this.tsmExtraChargeEdit.ResumeLayout(false);
            this.tsmExtraChargeEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer scExtraCharge;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Controls.AFColorLabel afColorLabel1;
        private Controls.AFMinMaxPanel mmPanelExtraChargeEdit;
        private System.Windows.Forms.Panel panExtraChargeEdit;
        private Controls.AFToolStrip tsmExtraChargeEdit;
        private System.Windows.Forms.ToolStripButton tsbtnExtraChargeAdd;
        private System.Windows.Forms.ToolStripButton tsbtnExtraChargeSave;
        private System.Windows.Forms.ToolStripButton tsbtnExtraChargeDelete;
        private System.Windows.Forms.ToolStripButton tsbnExtraChargeEditClose;
        private System.Windows.Forms.TextBox tbExtraChargeBeschreibung;
        private System.Windows.Forms.Label lBeschreibung;
        private System.Windows.Forms.TextBox tbExtraChargeBezeichnung;
        private System.Windows.Forms.Label lNachname;
        private Controls.AFMinMaxPanel mmPanelExtraChargeList;
        private Controls.AFToolStrip tsmExtraChargeList;
        private System.Windows.Forms.ToolStripButton tsbtnOpenEdit;
        private System.Windows.Forms.ToolStripButton tsbtnListNew;
        private System.Windows.Forms.ToolStripButton tsbtnListChange;
        private System.Windows.Forms.ToolStripButton tsbListExcelExport;
        private System.Windows.Forms.ToolStripButton tsbtnListRefresh;
        private System.Windows.Forms.ToolStripButton tsbtnListDelete;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private Controls.AFToolStrip miniToolStrip;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox tbExtraChargePreis;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbExtraChargeRGText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbEinheit;
        private System.Windows.Forms.ComboBox cbExtraChargeKontoText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lKontoNr;
        private Telerik.WinControls.UI.RadGridView dgvExtraCharge;
        private System.Windows.Forms.TextBox tbExtraChargeKontoNr;
        private System.Windows.Forms.CheckBox cbIsGlobal;
    }
}
