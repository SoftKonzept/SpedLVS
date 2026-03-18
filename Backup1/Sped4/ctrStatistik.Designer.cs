namespace Sped4
{
    partial class ctrStatistik
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
            Telerik.WinControls.UI.Docking.AutoHideGroup autoHideGroup1 = new Telerik.WinControls.UI.Docking.AutoHideGroup();
            Telerik.WinControls.UI.CartesianArea cartesianArea1 = new Telerik.WinControls.UI.CartesianArea();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.dockWindowPlaceholder1 = new Telerik.WinControls.UI.Docking.DockWindowPlaceholder();
            this.dockStatOutput = new Telerik.WinControls.UI.Docking.RadDock();
            this.windowChart = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.cvStatChart = new Telerik.WinControls.UI.RadChartView();
            this.documentContainer1 = new Telerik.WinControls.UI.Docking.DocumentContainer();
            this.documentTabStrip1 = new Telerik.WinControls.UI.Docking.DocumentTabStrip();
            this.windowGrid = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.toolTabStrip1 = new Telerik.WinControls.UI.Docking.ToolTabStrip();
            this.splittCtr = new System.Windows.Forms.SplitContainer();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnPrintChart = new System.Windows.Forms.ToolStripButton();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.cbSPLExcl = new System.Windows.Forms.CheckBox();
            this.cbSchadenExcl = new System.Windows.Forms.CheckBox();
            this.cbRLExcl = new System.Windows.Forms.CheckBox();
            this.btnSearchA = new System.Windows.Forms.Button();
            this.tbAuftraggeber = new System.Windows.Forms.TextBox();
            this.tbSearchA = new System.Windows.Forms.TextBox();
            this.comboChart = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboStatistikArt = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboListe = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFilterActivate = new System.Windows.Forms.CheckBox();
            this.lZeitraumVon = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpBis = new System.Windows.Forms.DateTimePicker();
            this.dtpVon = new System.Windows.Forms.DateTimePicker();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dockStatOutput)).BeginInit();
            this.dockStatOutput.SuspendLayout();
            this.windowChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cvStatChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).BeginInit();
            this.documentContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).BeginInit();
            this.documentTabStrip1.SuspendLayout();
            this.windowGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip1)).BeginInit();
            this.toolTabStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splittCtr)).BeginInit();
            this.splittCtr.Panel1.SuspendLayout();
            this.splittCtr.SuspendLayout();
            this.afToolStrip2.SuspendLayout();
            this.afMinMaxPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockWindowPlaceholder1
            // 
            this.dockWindowPlaceholder1.DockWindowName = "toolWindow2";
            this.dockWindowPlaceholder1.DockWindowText = "toolWindow2";
            this.dockWindowPlaceholder1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dockWindowPlaceholder1.Location = new System.Drawing.Point(0, 0);
            this.dockWindowPlaceholder1.Name = "dockWindowPlaceholder1";
            this.dockWindowPlaceholder1.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.dockWindowPlaceholder1.Size = new System.Drawing.Size(200, 200);
            this.dockWindowPlaceholder1.Text = "dockWindowPlaceholder1";
            // 
            // dockStatOutput
            // 
            this.dockStatOutput.ActiveWindow = this.windowChart;
            this.dockStatOutput.BackColor = System.Drawing.Color.White;
            this.dockStatOutput.CausesValidation = false;
            this.dockStatOutput.Controls.Add(this.documentContainer1);
            this.dockStatOutput.Controls.Add(this.toolTabStrip1);
            this.dockStatOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockStatOutput.IsCleanUpTarget = true;
            this.dockStatOutput.Location = new System.Drawing.Point(0, 263);
            this.dockStatOutput.MainDocumentContainer = this.documentContainer1;
            this.dockStatOutput.Name = "dockStatOutput";
            this.dockStatOutput.Padding = new System.Windows.Forms.Padding(0);
            // 
            // 
            // 
            this.dockStatOutput.RootElement.MinSize = new System.Drawing.Size(0, 0);
            autoHideGroup1.Windows.Add(this.dockWindowPlaceholder1);
            this.dockStatOutput.SerializableAutoHideContainer.RightAutoHideGroups.Add(autoHideGroup1);
            this.dockStatOutput.Size = new System.Drawing.Size(1005, 291);
            this.dockStatOutput.SplitterWidth = 8;
            this.dockStatOutput.TabIndex = 12;
            this.dockStatOutput.TabStop = false;
            // 
            // windowChart
            // 
            this.windowChart.Caption = null;
            this.windowChart.Controls.Add(this.cvStatChart);
            this.windowChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowChart.Location = new System.Drawing.Point(4, 34);
            this.windowChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.windowChart.Name = "windowChart";
            this.windowChart.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.windowChart.Size = new System.Drawing.Size(367, 253);
            this.windowChart.Text = "Ausgabe als Diagramm";
            // 
            // cvStatChart
            // 
            this.cvStatChart.AreaDesign = cartesianArea1;
            this.cvStatChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cvStatChart.Location = new System.Drawing.Point(0, 0);
            this.cvStatChart.Name = "cvStatChart";
            this.cvStatChart.ShowGrid = false;
            this.cvStatChart.Size = new System.Drawing.Size(367, 253);
            this.cvStatChart.TabIndex = 0;
            this.cvStatChart.ThemeName = "ControlDefault";
            // 
            // documentContainer1
            // 
            this.documentContainer1.BackColor = System.Drawing.Color.Transparent;
            this.documentContainer1.CausesValidation = false;
            this.documentContainer1.Controls.Add(this.documentTabStrip1);
            this.documentContainer1.Name = "documentContainer1";
            this.documentContainer1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // 
            // 
            this.documentContainer1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.documentContainer1.SizeInfo.AbsoluteSize = new System.Drawing.Size(448, 200);
            this.documentContainer1.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Fill;
            this.documentContainer1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-175, 0);
            this.documentContainer1.SplitterWidth = 8;
            // 
            // documentTabStrip1
            // 
            this.documentTabStrip1.BackColor = System.Drawing.Color.Transparent;
            this.documentTabStrip1.CausesValidation = false;
            this.documentTabStrip1.Controls.Add(this.windowGrid);
            this.documentTabStrip1.Location = new System.Drawing.Point(0, 0);
            this.documentTabStrip1.Name = "documentTabStrip1";
            // 
            // 
            // 
            this.documentTabStrip1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.documentTabStrip1.SelectedIndex = 0;
            this.documentTabStrip1.Size = new System.Drawing.Size(622, 291);
            this.documentTabStrip1.TabIndex = 0;
            this.documentTabStrip1.TabStop = false;
            // 
            // windowGrid
            // 
            this.windowGrid.BackColor = System.Drawing.Color.White;
            this.windowGrid.Caption = null;
            this.windowGrid.Controls.Add(this.dgv);
            this.windowGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowGrid.Location = new System.Drawing.Point(5, 35);
            this.windowGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.windowGrid.Name = "windowGrid";
            this.windowGrid.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.windowGrid.Size = new System.Drawing.Size(610, 249);
            this.windowGrid.Text = "Ausgabe als Tabelle";
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 0);
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
            this.dgv.ShowHeaderCellButtons = true;
            this.dgv.Size = new System.Drawing.Size(610, 249);
            this.dgv.TabIndex = 25;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgv_CellFormatting);
            // 
            // toolTabStrip1
            // 
            this.toolTabStrip1.Controls.Add(this.windowChart);
            this.toolTabStrip1.Location = new System.Drawing.Point(630, 0);
            this.toolTabStrip1.Name = "toolTabStrip1";
            // 
            // 
            // 
            this.toolTabStrip1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.toolTabStrip1.SelectedIndex = 0;
            this.toolTabStrip1.Size = new System.Drawing.Size(375, 291);
            this.toolTabStrip1.SizeInfo.AbsoluteSize = new System.Drawing.Size(375, 200);
            this.toolTabStrip1.SizeInfo.SplitterCorrection = new System.Drawing.Size(175, 0);
            this.toolTabStrip1.TabIndex = 1;
            this.toolTabStrip1.TabStop = false;
            // 
            // splittCtr
            // 
            this.splittCtr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splittCtr.Location = new System.Drawing.Point(0, 34);
            this.splittCtr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splittCtr.Name = "splittCtr";
            // 
            // splittCtr.Panel1
            // 
            this.splittCtr.Panel1.BackColor = System.Drawing.Color.White;
            this.splittCtr.Panel1.Controls.Add(this.dockStatOutput);
            this.splittCtr.Panel1.Controls.Add(this.afToolStrip2);
            this.splittCtr.Panel1.Controls.Add(this.afMinMaxPanel1);
            // 
            // splittCtr.Panel2
            // 
            this.splittCtr.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splittCtr.Size = new System.Drawing.Size(1575, 554);
            this.splittCtr.SplitterDistance = 1005;
            this.splittCtr.SplitterWidth = 5;
            this.splittCtr.TabIndex = 2;
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSearch,
            this.tsbtnClear,
            this.tsbtnPrint,
            this.tsbtnExcel,
            this.tsbtnClose,
            this.toolStripSeparator1,
            this.tsbtnPrintChart});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 236);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(1005, 27);
            this.afToolStrip2.TabIndex = 24;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnSearch
            // 
            this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSearch.Image = global::Sped4.Properties.Resources.selection_view_32x32;
            this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearch.Name = "tsbtnSearch";
            this.tsbtnSearch.Size = new System.Drawing.Size(29, 24);
            this.tsbtnSearch.Text = "Bestandsdaten laden";
            this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClear.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(29, 24);
            this.tsbtnClear.Text = "alle Vorgaben zurücksetzen";
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrint.Image = global::Sped4.Properties.Resources.Printer1;
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(29, 24);
            this.tsbtnPrint.Text = "Bestand drucken";
            this.tsbtnPrint.Visible = false;
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // tsbtnExcel
            // 
            this.tsbtnExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExcel.Image = global::Sped4.Properties.Resources.Excel;
            this.tsbtnExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExcel.Name = "tsbtnExcel";
            this.tsbtnExcel.Size = new System.Drawing.Size(29, 24);
            this.tsbtnExcel.Text = "Export zu Excel";
            this.tsbtnExcel.Click += new System.EventHandler(this.tsbtnExcel_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(29, 24);
            this.tsbtnClose.Text = "Suche schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbtnPrintChart
            // 
            this.tsbtnPrintChart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrintChart.Image = global::Sped4.Properties.Resources.printer2_24x24;
            this.tsbtnPrintChart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrintChart.Name = "tsbtnPrintChart";
            this.tsbtnPrintChart.Size = new System.Drawing.Size(29, 24);
            this.tsbtnPrintChart.Text = "Diagramm drucken";
            this.tsbtnPrintChart.Click += new System.EventHandler(this.tsbtnPrintChart_Click);
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.BackColor = System.Drawing.Color.White;
            this.afMinMaxPanel1.Controls.Add(this.cbSPLExcl);
            this.afMinMaxPanel1.Controls.Add(this.cbSchadenExcl);
            this.afMinMaxPanel1.Controls.Add(this.cbRLExcl);
            this.afMinMaxPanel1.Controls.Add(this.btnSearchA);
            this.afMinMaxPanel1.Controls.Add(this.tbAuftraggeber);
            this.afMinMaxPanel1.Controls.Add(this.tbSearchA);
            this.afMinMaxPanel1.Controls.Add(this.comboChart);
            this.afMinMaxPanel1.Controls.Add(this.label4);
            this.afMinMaxPanel1.Controls.Add(this.comboStatistikArt);
            this.afMinMaxPanel1.Controls.Add(this.label3);
            this.afMinMaxPanel1.Controls.Add(this.comboListe);
            this.afMinMaxPanel1.Controls.Add(this.label2);
            this.afMinMaxPanel1.Controls.Add(this.cbFilterActivate);
            this.afMinMaxPanel1.Controls.Add(this.lZeitraumVon);
            this.afMinMaxPanel1.Controls.Add(this.label1);
            this.afMinMaxPanel1.Controls.Add(this.dtpBis);
            this.afMinMaxPanel1.Controls.Add(this.dtpVon);
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 0);
            this.afMinMaxPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Optionen";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(1005, 236);
            this.afMinMaxPanel1.TabIndex = 25;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            this.afMinMaxPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.afMinMaxPanel1_Paint);
            // 
            // cbSPLExcl
            // 
            this.cbSPLExcl.AutoSize = true;
            this.cbSPLExcl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSPLExcl.Location = new System.Drawing.Point(723, 95);
            this.cbSPLExcl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSPLExcl.Name = "cbSPLExcl";
            this.cbSPLExcl.Size = new System.Drawing.Size(162, 20);
            this.cbSPLExcl.TabIndex = 175;
            this.cbSPLExcl.Text = "Endbestand: ohne SPL";
            this.cbSPLExcl.UseVisualStyleBackColor = true;
            // 
            // cbSchadenExcl
            // 
            this.cbSchadenExcl.AutoSize = true;
            this.cbSchadenExcl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSchadenExcl.Location = new System.Drawing.Point(723, 66);
            this.cbSchadenExcl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSchadenExcl.Name = "cbSchadenExcl";
            this.cbSchadenExcl.Size = new System.Drawing.Size(168, 20);
            this.cbSchadenExcl.TabIndex = 174;
            this.cbSchadenExcl.Text = "Eingang: ohne Schäden";
            this.cbSchadenExcl.UseVisualStyleBackColor = true;
            // 
            // cbRLExcl
            // 
            this.cbRLExcl.AutoSize = true;
            this.cbRLExcl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRLExcl.Location = new System.Drawing.Point(723, 38);
            this.cbRLExcl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbRLExcl.Name = "cbRLExcl";
            this.cbRLExcl.Size = new System.Drawing.Size(211, 20);
            this.cbRLExcl.TabIndex = 173;
            this.cbRLExcl.Text = "Eingang: ohne Rücklieferungen";
            this.cbRLExcl.UseVisualStyleBackColor = true;
            // 
            // btnSearchA
            // 
            this.btnSearchA.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchA.Location = new System.Drawing.Point(33, 66);
            this.btnSearchA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearchA.Name = "btnSearchA";
            this.btnSearchA.Size = new System.Drawing.Size(113, 27);
            this.btnSearchA.TabIndex = 160;
            this.btnSearchA.Text = "Adresse";
            this.btnSearchA.UseVisualStyleBackColor = true;
            this.btnSearchA.Click += new System.EventHandler(this.btnSearchA_Click);
            // 
            // tbAuftraggeber
            // 
            this.tbAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuftraggeber.Enabled = false;
            this.tbAuftraggeber.Location = new System.Drawing.Point(292, 66);
            this.tbAuftraggeber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbAuftraggeber.Name = "tbAuftraggeber";
            this.tbAuftraggeber.ReadOnly = true;
            this.tbAuftraggeber.Size = new System.Drawing.Size(358, 22);
            this.tbAuftraggeber.TabIndex = 162;
            // 
            // tbSearchA
            // 
            this.tbSearchA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchA.Location = new System.Drawing.Point(155, 66);
            this.tbSearchA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSearchA.Name = "tbSearchA";
            this.tbSearchA.Size = new System.Drawing.Size(129, 22);
            this.tbSearchA.TabIndex = 161;
            this.tbSearchA.TextChanged += new System.EventHandler(this.tbSearchA_TextChanged);
            // 
            // comboChart
            // 
            this.comboChart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboChart.FormattingEnabled = true;
            this.comboChart.Items.AddRange(new object[] {
            "Balkendiagramm",
            "Kurvendiagramm"});
            this.comboChart.Location = new System.Drawing.Point(155, 100);
            this.comboChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboChart.Name = "comboChart";
            this.comboChart.Size = new System.Drawing.Size(495, 24);
            this.comboChart.TabIndex = 62;
            this.comboChart.SelectedIndexChanged += new System.EventHandler(this.comboChart_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(29, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 61;
            this.label4.Text = "Chart:";
            // 
            // comboStatistikArt
            // 
            this.comboStatistikArt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboStatistikArt.FormattingEnabled = true;
            this.comboStatistikArt.Location = new System.Drawing.Point(155, 33);
            this.comboStatistikArt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboStatistikArt.Name = "comboStatistikArt";
            this.comboStatistikArt.Size = new System.Drawing.Size(495, 24);
            this.comboStatistikArt.TabIndex = 60;
            this.comboStatistikArt.SelectedIndexChanged += new System.EventHandler(this.comboStatistikArt_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(29, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 59;
            this.label3.Text = "Statistik: ";
            // 
            // comboListe
            // 
            this.comboListe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboListe.FormattingEnabled = true;
            this.comboListe.Location = new System.Drawing.Point(452, 203);
            this.comboListe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboListe.Name = "comboListe";
            this.comboListe.Size = new System.Drawing.Size(244, 24);
            this.comboListe.TabIndex = 58;
            this.comboListe.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(348, 207);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 57;
            this.label2.Text = "Listenansicht:";
            this.label2.Visible = false;
            // 
            // cbFilterActivate
            // 
            this.cbFilterActivate.AutoSize = true;
            this.cbFilterActivate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbFilterActivate.Location = new System.Drawing.Point(41, 212);
            this.cbFilterActivate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbFilterActivate.Name = "cbFilterActivate";
            this.cbFilterActivate.Size = new System.Drawing.Size(116, 20);
            this.cbFilterActivate.TabIndex = 56;
            this.cbFilterActivate.Text = "Filter aktivieren";
            this.cbFilterActivate.UseVisualStyleBackColor = true;
            this.cbFilterActivate.Visible = false;
            this.cbFilterActivate.CheckedChanged += new System.EventHandler(this.cbFilterActivate_CheckedChanged);
            // 
            // lZeitraumVon
            // 
            this.lZeitraumVon.AutoSize = true;
            this.lZeitraumVon.ForeColor = System.Drawing.Color.DarkBlue;
            this.lZeitraumVon.Location = new System.Drawing.Point(29, 142);
            this.lZeitraumVon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lZeitraumVon.Name = "lZeitraumVon";
            this.lZeitraumVon.Size = new System.Drawing.Size(87, 16);
            this.lZeitraumVon.TabIndex = 53;
            this.lZeitraumVon.Text = "Zeitraum von:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(29, 170);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 16);
            this.label1.TabIndex = 54;
            this.label1.Text = "Zeitraum bis:";
            // 
            // dtpBis
            // 
            this.dtpBis.Location = new System.Drawing.Point(155, 164);
            this.dtpBis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpBis.Name = "dtpBis";
            this.dtpBis.Size = new System.Drawing.Size(495, 22);
            this.dtpBis.TabIndex = 55;
            // 
            // dtpVon
            // 
            this.dtpVon.Location = new System.Drawing.Point(155, 135);
            this.dtpVon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpVon.Name = "dtpVon";
            this.dtpVon.Size = new System.Drawing.Size(495, 22);
            this.dtpVon.TabIndex = 52;
            this.dtpVon.ValueChanged += new System.EventHandler(this.dtpVon_ValueChanged);
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
            this.afColorLabel1.myText = "Statistik";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(1575, 34);
            this.afColorLabel1.TabIndex = 10;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrStatistik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splittCtr);
            this.Controls.Add(this.afColorLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrStatistik";
            this.Size = new System.Drawing.Size(1575, 588);
            ((System.ComponentModel.ISupportInitialize)(this.dockStatOutput)).EndInit();
            this.dockStatOutput.ResumeLayout(false);
            this.windowChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cvStatChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).EndInit();
            this.documentContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).EndInit();
            this.documentTabStrip1.ResumeLayout(false);
            this.windowGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip1)).EndInit();
            this.toolTabStrip1.ResumeLayout(false);
            this.splittCtr.Panel1.ResumeLayout(false);
            this.splittCtr.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splittCtr)).EndInit();
            this.splittCtr.ResumeLayout(false);
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.afMinMaxPanel1.ResumeLayout(false);
            this.afMinMaxPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Telerik.WinControls.UI.Docking.RadDock dockStatOutput;
        private Telerik.WinControls.UI.Docking.ToolWindow windowGrid;
        private Telerik.WinControls.UI.Docking.DocumentContainer documentContainer1;
        private Telerik.WinControls.UI.Docking.DocumentTabStrip documentTabStrip1;
        private Telerik.WinControls.UI.Docking.ToolTabStrip toolTabStrip1;
        private Telerik.WinControls.UI.Docking.ToolWindow windowChart;
        private Telerik.WinControls.UI.Docking.DockWindowPlaceholder dockWindowPlaceholder1;
        public Telerik.WinControls.UI.RadGridView dgv;
        private Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnSearch;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsbtnExcel;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Controls.AFMinMaxPanel afMinMaxPanel1;
        private System.Windows.Forms.SplitContainer splittCtr;
        private System.Windows.Forms.CheckBox cbFilterActivate;
        private System.Windows.Forms.Label lZeitraumVon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpBis;
        private System.Windows.Forms.DateTimePicker dtpVon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboListe;
        private System.Windows.Forms.ComboBox comboStatistikArt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboChart;
        private System.Windows.Forms.Label label4;
        private Telerik.WinControls.UI.RadChartView cvStatChart;
        private System.Windows.Forms.Button btnSearchA;
        private System.Windows.Forms.TextBox tbAuftraggeber;
        private System.Windows.Forms.TextBox tbSearchA;
        private System.Windows.Forms.ToolStripButton tsbtnPrintChart;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox cbSchadenExcl;
        private System.Windows.Forms.CheckBox cbRLExcl;
        private System.Windows.Forms.CheckBox cbSPLExcl;
    }
}
