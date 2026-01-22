namespace Sped4
{
    partial class ctrJournal
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
            this.rDgv = new Telerik.WinControls.UI.RadGridView();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.scAttachmentProzessContainer = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.menuSubFilter = new Sped4.Controls.AFToolStrip();
            this.tsbtnGroupCollapse = new System.Windows.Forms.ToolStripButton();
            this.tsbtnGroupExpand = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tscbGroup = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tscbSort = new System.Windows.Forms.ToolStripComboBox();
            this.menuMain = new Sped4.Controls.AFToolStrip();
            this.tsbtnSearchShow = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbtnShowAll = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMail = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbSearch = new System.Windows.Forms.ToolStripComboBox();
            this.tslSearchText = new System.Windows.Forms.ToolStripLabel();
            this.tstbSearchArtikel = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbcViews = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.comboTarif = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboAuftraggeber = new System.Windows.Forms.ComboBox();
            this.gbAuswahl = new System.Windows.Forms.GroupBox();
            this.cbSchadenExcl = new System.Windows.Forms.CheckBox();
            this.cbRLExcl = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCalDateDiff = new System.Windows.Forms.Button();
            this.nudDays = new System.Windows.Forms.NumericUpDown();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbJournalart = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpBis = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpVon = new System.Windows.Forms.DateTimePicker();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.rDgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDgv.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scAttachmentProzessContainer)).BeginInit();
            this.scAttachmentProzessContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.menuSubFilter.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.afMinMaxPanel1.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.gbAuswahl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).BeginInit();
            this.SuspendLayout();
            // 
            // rDgv
            // 
            this.rDgv.BackColor = System.Drawing.Color.White;
            this.rDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rDgv.EnableCustomFiltering = true;
            this.rDgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.rDgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rDgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rDgv.Location = new System.Drawing.Point(0, 54);
            this.rDgv.Margin = new System.Windows.Forms.Padding(8);
            // 
            // 
            // 
            this.rDgv.MasterTemplate.EnableAlternatingRowColor = true;
            this.rDgv.MasterTemplate.EnableCustomFiltering = true;
            this.rDgv.MasterTemplate.EnableFiltering = true;
            this.rDgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.rDgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rDgv.Name = "rDgv";
            this.rDgv.ReadOnly = true;
            this.rDgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.rDgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 54, 240, 150);
            this.rDgv.ShowHeaderCellButtons = true;
            this.rDgv.Size = new System.Drawing.Size(990, 383);
            this.rDgv.TabIndex = 22;
            this.rDgv.ThemeName = "ControlDefault";
            this.rDgv.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.rDgv_CellFormatting);
            this.rDgv.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.rDgv_ContextMenuOpening);
            this.rDgv.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.rDgv_ToolTipTextNeeded);
            // 
            // scAttachmentProzessContainer
            // 
            this.scAttachmentProzessContainer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.scAttachmentProzessContainer.Controls.Add(this.splitPanel1);
            this.scAttachmentProzessContainer.Controls.Add(this.splitPanel2);
            this.scAttachmentProzessContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scAttachmentProzessContainer.Location = new System.Drawing.Point(0, 167);
            this.scAttachmentProzessContainer.Name = "scAttachmentProzessContainer";
            // 
            // 
            // 
            this.scAttachmentProzessContainer.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 167, 200, 200);
            this.scAttachmentProzessContainer.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.scAttachmentProzessContainer.Size = new System.Drawing.Size(990, 437);
            this.scAttachmentProzessContainer.SplitterWidth = 10;
            this.scAttachmentProzessContainer.TabIndex = 34;
            this.scAttachmentProzessContainer.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel1.Collapsed = true;
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(467, 542);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.1450304F, 0F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-143, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            this.splitPanel1.Visible = false;
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel2.Controls.Add(this.rDgv);
            this.splitPanel2.Controls.Add(this.menuSubFilter);
            this.splitPanel2.Controls.Add(this.menuMain);
            this.splitPanel2.Location = new System.Drawing.Point(0, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel2.Size = new System.Drawing.Size(990, 437);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.1450304F, 0F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(143, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // menuSubFilter
            // 
            this.menuSubFilter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuSubFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnGroupCollapse,
            this.tsbtnGroupExpand,
            this.toolStripLabel4,
            this.tscbGroup,
            this.toolStripSeparator6,
            this.toolStripLabel5,
            this.tscbSort});
            this.menuSubFilter.Location = new System.Drawing.Point(0, 27);
            this.menuSubFilter.myColorFrom = System.Drawing.Color.Azure;
            this.menuSubFilter.myColorTo = System.Drawing.Color.Blue;
            this.menuSubFilter.myUnderlineColor = System.Drawing.Color.White;
            this.menuSubFilter.myUnderlined = true;
            this.menuSubFilter.Name = "menuSubFilter";
            this.menuSubFilter.Size = new System.Drawing.Size(990, 27);
            this.menuSubFilter.TabIndex = 23;
            this.menuSubFilter.Text = "afToolStrip2";
            // 
            // tsbtnGroupCollapse
            // 
            this.tsbtnGroupCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGroupCollapse.Image = global::Sped4.Properties.Resources.scroll_close_16x16;
            this.tsbtnGroupCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGroupCollapse.Name = "tsbtnGroupCollapse";
            this.tsbtnGroupCollapse.Size = new System.Drawing.Size(24, 24);
            this.tsbtnGroupCollapse.Text = "alle Gruppierungen schliessen";
            this.tsbtnGroupCollapse.Click += new System.EventHandler(this.TsbtnGroupCollapse_Click);
            // 
            // tsbtnGroupExpand
            // 
            this.tsbtnGroupExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGroupExpand.Image = global::Sped4.Properties.Resources.scroll_open_16x16;
            this.tsbtnGroupExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGroupExpand.Name = "tsbtnGroupExpand";
            this.tsbtnGroupExpand.Size = new System.Drawing.Size(24, 24);
            this.tsbtnGroupExpand.Text = "alle Gruppierungen öffnen";
            this.tsbtnGroupExpand.Click += new System.EventHandler(this.TsbtnGroupExpand_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(77, 24);
            this.toolStripLabel4.Text = "Gruppierung:";
            // 
            // tscbGroup
            // 
            this.tscbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbGroup.Items.AddRange(new object[] {
            "-Gruppierung wählen-",
            "Eingang|TransportReferenz"});
            this.tscbGroup.Name = "tscbGroup";
            this.tscbGroup.Size = new System.Drawing.Size(180, 27);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(65, 24);
            this.toolStripLabel5.Text = "Sortierung:";
            // 
            // tscbSort
            // 
            this.tscbSort.Items.AddRange(new object[] {
            "-Sortierung wählen-",
            "Abmessung [Dicke|Breite|Länge]"});
            this.tscbSort.Name = "tscbSort";
            this.tscbSort.Size = new System.Drawing.Size(200, 27);
            // 
            // menuMain
            // 
            this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSearchShow,
            this.tsbtnSearch,
            this.tsbtnShowAll,
            this.tsbtnExcel,
            this.tsbtnPrint,
            this.tsbtnMail,
            this.tsbtnClose,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.tscbSearch,
            this.tslSearchText,
            this.tstbSearchArtikel,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tsbcViews,
            this.toolStripSeparator3});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.myColorFrom = System.Drawing.Color.Azure;
            this.menuMain.myColorTo = System.Drawing.Color.Blue;
            this.menuMain.myUnderlineColor = System.Drawing.Color.White;
            this.menuMain.myUnderlined = true;
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(990, 27);
            this.menuMain.TabIndex = 21;
            this.menuMain.Text = "afToolStrip2";
            // 
            // tsbtnSearchShow
            // 
            this.tsbtnSearchShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSearchShow.Image = global::Sped4.Properties.Resources.layout_left;
            this.tsbtnSearchShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearchShow.Name = "tsbtnSearchShow";
            this.tsbtnSearchShow.Size = new System.Drawing.Size(24, 24);
            this.tsbtnSearchShow.Text = "Sucheingabe öffnen";
            this.tsbtnSearchShow.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbtnSearch
            // 
            this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSearch.Image = global::Sped4.Properties.Resources.selection_view_32x32;
            this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearch.Name = "tsbtnSearch";
            this.tsbtnSearch.Size = new System.Drawing.Size(24, 24);
            this.tsbtnSearch.Text = "Journaldaten laden";
            this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
            // 
            // tsbtnShowAll
            // 
            this.tsbtnShowAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnShowAll.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.tsbtnShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnShowAll.Name = "tsbtnShowAll";
            this.tsbtnShowAll.Size = new System.Drawing.Size(24, 24);
            this.tsbtnShowAll.Text = "alle Filter löschen - Standard Journaldaten laden";
            this.tsbtnShowAll.Click += new System.EventHandler(this.tsbtnShowAll_Click);
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
            // tsbtnPrint
            // 
            this.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrint.Image = global::Sped4.Properties.Resources.Printer1;
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(24, 24);
            this.tsbtnPrint.Text = "Journal drucken";
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // tsbtnMail
            // 
            this.tsbtnMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnMail.Image = global::Sped4.Properties.Resources.mail_forward_24x24;
            this.tsbtnMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMail.Name = "tsbtnMail";
            this.tsbtnMail.Size = new System.Drawing.Size(24, 24);
            this.tsbtnMail.Text = "Journal als Mail versenden";
            this.tsbtnMail.Click += new System.EventHandler(this.tsbtnMail_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(24, 24);
            this.tsbtnClose.Text = "Suche schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 24);
            this.toolStripLabel2.Text = "Spalte:";
            this.toolStripLabel2.Visible = false;
            // 
            // tscbSearch
            // 
            this.tscbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbSearch.DropDownWidth = 200;
            this.tscbSearch.Name = "tscbSearch";
            this.tscbSearch.Size = new System.Drawing.Size(200, 27);
            this.tscbSearch.Visible = false;
            // 
            // tslSearchText
            // 
            this.tslSearchText.Name = "tslSearchText";
            this.tslSearchText.Size = new System.Drawing.Size(71, 24);
            this.tslSearchText.Text = "Suchbegriff:";
            this.tslSearchText.Visible = false;
            // 
            // tstbSearchArtikel
            // 
            this.tstbSearchArtikel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstbSearchArtikel.Name = "tstbSearchArtikel";
            this.tstbSearchArtikel.Size = new System.Drawing.Size(200, 27);
            this.tstbSearchArtikel.Visible = false;
            this.tstbSearchArtikel.TextChanged += new System.EventHandler(this.tstbSearchArtikel_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(50, 24);
            this.toolStripLabel1.Text = "Ansicht:";
            // 
            // tsbcViews
            // 
            this.tsbcViews.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbcViews.Name = "tsbcViews";
            this.tsbcViews.Size = new System.Drawing.Size(150, 27);
            this.tsbcViews.SelectedIndexChanged += new System.EventHandler(this.tsbcViews_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.Controls.Add(this.gbFilter);
            this.afMinMaxPanel1.Controls.Add(this.gbAuswahl);
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 28);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Optionen";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(990, 139);
            this.afMinMaxPanel1.TabIndex = 8;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            // 
            // gbFilter
            // 
            this.gbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbFilter.Controls.Add(this.comboTarif);
            this.gbFilter.Controls.Add(this.label7);
            this.gbFilter.Controls.Add(this.label2);
            this.gbFilter.Controls.Add(this.comboAuftraggeber);
            this.gbFilter.Enabled = false;
            this.gbFilter.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbFilter.Location = new System.Drawing.Point(606, 22);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(364, 105);
            this.gbFilter.TabIndex = 158;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter-Einstellungen";
            // 
            // comboTarif
            // 
            this.comboTarif.AllowDrop = true;
            this.comboTarif.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboTarif.FormattingEnabled = true;
            this.comboTarif.ItemHeight = 13;
            this.comboTarif.Location = new System.Drawing.Point(87, 53);
            this.comboTarif.Name = "comboTarif";
            this.comboTarif.Size = new System.Drawing.Size(261, 21);
            this.comboTarif.TabIndex = 167;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(10, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 166;
            this.label7.Text = "Tarif:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(9, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 165;
            this.label2.Text = "Auftraggeber:";
            // 
            // comboAuftraggeber
            // 
            this.comboAuftraggeber.AllowDrop = true;
            this.comboAuftraggeber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboAuftraggeber.FormattingEnabled = true;
            this.comboAuftraggeber.ItemHeight = 13;
            this.comboAuftraggeber.Location = new System.Drawing.Point(87, 27);
            this.comboAuftraggeber.Name = "comboAuftraggeber";
            this.comboAuftraggeber.Size = new System.Drawing.Size(261, 21);
            this.comboAuftraggeber.TabIndex = 164;
            this.comboAuftraggeber.SelectedIndexChanged += new System.EventHandler(this.comboAuftraggeber_SelectedIndexChanged);
            // 
            // gbAuswahl
            // 
            this.gbAuswahl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbAuswahl.Controls.Add(this.cbSchadenExcl);
            this.gbAuswahl.Controls.Add(this.cbRLExcl);
            this.gbAuswahl.Controls.Add(this.label3);
            this.gbAuswahl.Controls.Add(this.btnCalDateDiff);
            this.gbAuswahl.Controls.Add(this.nudDays);
            this.gbAuswahl.Controls.Add(this.cbFilter);
            this.gbAuswahl.Controls.Add(this.label6);
            this.gbAuswahl.Controls.Add(this.cbJournalart);
            this.gbAuswahl.Controls.Add(this.label4);
            this.gbAuswahl.Controls.Add(this.dtpBis);
            this.gbAuswahl.Controls.Add(this.label1);
            this.gbAuswahl.Controls.Add(this.dtpVon);
            this.gbAuswahl.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbAuswahl.Location = new System.Drawing.Point(33, 22);
            this.gbAuswahl.Name = "gbAuswahl";
            this.gbAuswahl.Size = new System.Drawing.Size(567, 105);
            this.gbAuswahl.TabIndex = 157;
            this.gbAuswahl.TabStop = false;
            this.gbAuswahl.Text = "Auswahl Journaldaten";
            // 
            // cbSchadenExcl
            // 
            this.cbSchadenExcl.AutoSize = true;
            this.cbSchadenExcl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSchadenExcl.Location = new System.Drawing.Point(106, 76);
            this.cbSchadenExcl.Name = "cbSchadenExcl";
            this.cbSchadenExcl.Size = new System.Drawing.Size(138, 17);
            this.cbSchadenExcl.TabIndex = 172;
            this.cbSchadenExcl.Text = "Eingang: ohne Schäden";
            this.cbSchadenExcl.UseVisualStyleBackColor = true;
            // 
            // cbRLExcl
            // 
            this.cbRLExcl.AutoSize = true;
            this.cbRLExcl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRLExcl.Location = new System.Drawing.Point(106, 57);
            this.cbRLExcl.Name = "cbRLExcl";
            this.cbRLExcl.Size = new System.Drawing.Size(173, 17);
            this.cbRLExcl.TabIndex = 171;
            this.cbRLExcl.Text = "Eingang: ohne Rücklieferungen";
            this.cbRLExcl.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(521, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 170;
            this.label3.Text = "Tage";
            // 
            // btnCalDateDiff
            // 
            this.btnCalDateDiff.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCalDateDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalDateDiff.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnCalDateDiff.Location = new System.Drawing.Point(293, 71);
            this.btnCalDateDiff.Name = "btnCalDateDiff";
            this.btnCalDateDiff.Size = new System.Drawing.Size(119, 22);
            this.btnCalDateDiff.TabIndex = 169;
            this.btnCalDateDiff.TabStop = false;
            this.btnCalDateDiff.Tag = "1";
            this.btnCalDateDiff.Text = "[Zeitraum berechnen]";
            this.btnCalDateDiff.UseVisualStyleBackColor = true;
            this.btnCalDateDiff.Click += new System.EventHandler(this.btnCalDateDiff_Click);
            // 
            // nudDays
            // 
            this.nudDays.Location = new System.Drawing.Point(429, 73);
            this.nudDays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDays.Name = "nudDays";
            this.nudDays.Size = new System.Drawing.Size(86, 20);
            this.nudDays.TabIndex = 168;
            this.nudDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbFilter
            // 
            this.cbFilter.AutoSize = true;
            this.cbFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFilter.Location = new System.Drawing.Point(12, 57);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(94, 17);
            this.cbFilter.TabIndex = 166;
            this.cbFilter.Text = "Filter aktivieren";
            this.cbFilter.UseVisualStyleBackColor = true;
            this.cbFilter.CheckedChanged += new System.EventHandler(this.cbFilter_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(9, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 165;
            this.label6.Text = "Bestandsart:";
            // 
            // cbJournalart
            // 
            this.cbJournalart.AllowDrop = true;
            this.cbJournalart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbJournalart.FormattingEnabled = true;
            this.cbJournalart.ItemHeight = 13;
            this.cbJournalart.Location = new System.Drawing.Point(87, 27);
            this.cbJournalart.Name = "cbJournalart";
            this.cbJournalart.Size = new System.Drawing.Size(185, 21);
            this.cbJournalart.TabIndex = 164;
            this.cbJournalart.SelectedIndexChanged += new System.EventHandler(this.cbJournalart_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(290, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Zeitraum von:";
            // 
            // dtpBis
            // 
            this.dtpBis.Location = new System.Drawing.Point(368, 47);
            this.dtpBis.Name = "dtpBis";
            this.dtpBis.Size = new System.Drawing.Size(185, 20);
            this.dtpBis.TabIndex = 51;
            this.dtpBis.ValueChanged += new System.EventHandler(this.dtpBis_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(290, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Zeitraum bis:";
            // 
            // dtpVon
            // 
            this.dtpVon.Location = new System.Drawing.Point(368, 24);
            this.dtpVon.Name = "dtpVon";
            this.dtpVon.Size = new System.Drawing.Size(185, 20);
            this.dtpVon.TabIndex = 12;
            this.dtpVon.ValueChanged += new System.EventHandler(this.dtpVon_ValueChanged);
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
            this.afColorLabel1.myText = "Journal";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(990, 28);
            this.afColorLabel1.TabIndex = 7;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.scAttachmentProzessContainer);
            this.Controls.Add(this.afMinMaxPanel1);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrJournal";
            this.Size = new System.Drawing.Size(990, 604);
            this.Load += new System.EventHandler(this.ctrJournal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rDgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scAttachmentProzessContainer)).EndInit();
            this.scAttachmentProzessContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitPanel2.PerformLayout();
            this.menuSubFilter.ResumeLayout(false);
            this.menuSubFilter.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.afMinMaxPanel1.ResumeLayout(false);
            this.afMinMaxPanel1.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.gbAuswahl.ResumeLayout(false);
            this.gbAuswahl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDays)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFMinMaxPanel afMinMaxPanel1;
        private Controls.AFToolStrip menuMain;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsbtnShowAll;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DateTimePicker dtpVon;
        private System.Windows.Forms.DateTimePicker dtpBis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbAuswahl;
        private System.Windows.Forms.ToolStripButton tsbtnSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbJournalart;
        private System.Windows.Forms.ToolStripButton tsbtnExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboAuftraggeber;
        private System.Windows.Forms.CheckBox cbFilter;
        private System.Windows.Forms.ComboBox comboTarif;
        private System.Windows.Forms.Label label7;
        internal Controls.AFColorLabel afColorLabel1;
        private System.Windows.Forms.ToolStripButton tsbtnMail;
        private Telerik.WinControls.UI.RadSplitContainer scAttachmentProzessContainer;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private System.Windows.Forms.ToolStripButton tsbtnSearchShow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tsbcViews;
        public Telerik.WinControls.UI.RadGridView rDgv;
        private System.Windows.Forms.ToolStripComboBox tscbSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel tslSearchText;
        private System.Windows.Forms.ToolStripTextBox tstbSearchArtikel;
        private System.Windows.Forms.NumericUpDown nudDays;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCalDateDiff;
        private System.Windows.Forms.CheckBox cbSchadenExcl;
        private System.Windows.Forms.CheckBox cbRLExcl;
        private Controls.AFToolStrip menuSubFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox tscbGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox tscbSort;
        private System.Windows.Forms.ToolStripButton tsbtnGroupCollapse;
        private System.Windows.Forms.ToolStripButton tsbtnGroupExpand;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    }
}
