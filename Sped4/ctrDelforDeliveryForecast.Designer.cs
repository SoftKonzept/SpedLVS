namespace Sped4
{
    partial class ctrDelforDeliveryForecast
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
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn5 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewImageColumn gridViewImageColumn3 = new Telerik.WinControls.UI.GridViewImageColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition8 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn6 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition9 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition7 = new Telerik.WinControls.UI.TableViewDefinition();
            this.scMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitDelforCall = new Telerik.WinControls.UI.SplitPanel();
            this.tabDelfor = new System.Windows.Forms.TabControl();
            this.tabPage_Forecast = new System.Windows.Forms.TabPage();
            this.dgvDelfor = new Telerik.WinControls.UI.RadGridView();
            this.tabPage_Delivery = new System.Windows.Forms.TabPage();
            this.dgvDelivery = new Telerik.WinControls.UI.RadGridView();
            this.MenueDelfor = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAllCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAllUncheck = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tslOrderByEDate = new System.Windows.Forms.ToolStripLabel();
            this.splitBestand = new Telerik.WinControls.UI.SplitPanel();
            this.dgvArticle = new Telerik.WinControls.UI.RadGridView();
            this.MenuBestand = new Sped4.Controls.AFToolStrip();
            this.tsbtnArticleRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnArticleSetSelection = new System.Windows.Forms.ToolStripButton();
            this.tsbtnArticleSetUnSelection = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCreateCall = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.lStoreOutQuantity = new System.Windows.Forms.Label();
            this.lForecastQuantity = new System.Windows.Forms.Label();
            this.comboWerksnummer = new System.Windows.Forms.ComboBox();
            this.switch_QuantitySelectionMode = new Telerik.WinControls.UI.RadToggleSwitch();
            this.lMengenAuswahl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStoreOutQuantity = new Telerik.WinControls.UI.RadTextBox();
            this.tbForecastQuantity = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.switchSort = new Telerik.WinControls.UI.RadToggleSwitch();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDelforCall)).BeginInit();
            this.splitDelforCall.SuspendLayout();
            this.tabDelfor.SuspendLayout();
            this.tabPage_Forecast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelfor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelfor.MasterTemplate)).BeginInit();
            this.tabPage_Delivery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelivery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelivery.MasterTemplate)).BeginInit();
            this.MenueDelfor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBestand)).BeginInit();
            this.splitBestand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticle.MasterTemplate)).BeginInit();
            this.MenuBestand.SuspendLayout();
            this.afMinMaxPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.switch_QuantitySelectionMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStoreOutQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbForecastQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchSort)).BeginInit();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Controls.Add(this.splitDelforCall);
            this.scMain.Controls.Add(this.splitBestand);
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 141);
            this.scMain.Name = "scMain";
            // 
            // 
            // 
            this.scMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scMain.Size = new System.Drawing.Size(1004, 365);
            this.scMain.TabIndex = 13;
            this.scMain.TabStop = false;
            this.scMain.ThemeName = "ControlDefault";
            // 
            // splitDelforCall
            // 
            this.splitDelforCall.Controls.Add(this.tabDelfor);
            this.splitDelforCall.Controls.Add(this.MenueDelfor);
            this.splitDelforCall.Location = new System.Drawing.Point(0, 0);
            this.splitDelforCall.Name = "splitDelforCall";
            // 
            // 
            // 
            this.splitDelforCall.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitDelforCall.Size = new System.Drawing.Size(462, 365);
            this.splitDelforCall.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.03799999F, 0F);
            this.splitDelforCall.SizeInfo.SplitterCorrection = new System.Drawing.Size(-38, 0);
            this.splitDelforCall.TabIndex = 0;
            this.splitDelforCall.TabStop = false;
            this.splitDelforCall.ThemeName = "ControlDefault";
            // 
            // tabDelfor
            // 
            this.tabDelfor.Controls.Add(this.tabPage_Forecast);
            this.tabDelfor.Controls.Add(this.tabPage_Delivery);
            this.tabDelfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDelfor.Location = new System.Drawing.Point(0, 27);
            this.tabDelfor.Name = "tabDelfor";
            this.tabDelfor.SelectedIndex = 0;
            this.tabDelfor.Size = new System.Drawing.Size(462, 338);
            this.tabDelfor.TabIndex = 27;
            this.tabDelfor.SelectedIndexChanged += new System.EventHandler(this.tabDelfor_SelectedIndexChanged);
            // 
            // tabPage_Forecast
            // 
            this.tabPage_Forecast.Controls.Add(this.dgvDelfor);
            this.tabPage_Forecast.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Forecast.Name = "tabPage_Forecast";
            this.tabPage_Forecast.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Forecast.Size = new System.Drawing.Size(454, 312);
            this.tabPage_Forecast.TabIndex = 0;
            this.tabPage_Forecast.Text = "Planung";
            this.tabPage_Forecast.UseVisualStyleBackColor = true;
            // 
            // dgvDelfor
            // 
            this.dgvDelfor.BackColor = System.Drawing.Color.White;
            this.dgvDelfor.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvDelfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDelfor.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvDelfor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvDelfor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvDelfor.Location = new System.Drawing.Point(3, 3);
            this.dgvDelfor.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            gridViewCheckBoxColumn5.HeaderText = "Select";
            gridViewCheckBoxColumn5.Name = "Select";
            gridViewImageColumn3.HeaderText = "";
            gridViewImageColumn3.IsPinned = true;
            gridViewImageColumn3.Name = "Deaktivate";
            gridViewImageColumn3.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            this.dgvDelfor.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn5,
            gridViewImageColumn3});
            this.dgvDelfor.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvDelfor.MasterTemplate.EnableFiltering = true;
            this.dgvDelfor.MasterTemplate.ShowFilteringRow = false;
            this.dgvDelfor.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvDelfor.MasterTemplate.ViewDefinition = tableViewDefinition8;
            this.dgvDelfor.Name = "dgvDelfor";
            this.dgvDelfor.ReadOnly = true;
            this.dgvDelfor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvDelfor.ShowHeaderCellButtons = true;
            this.dgvDelfor.Size = new System.Drawing.Size(448, 306);
            this.dgvDelfor.TabIndex = 26;
            this.dgvDelfor.ThemeName = "ControlDefault";
            this.dgvDelfor.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgvDelfor_CellFormatting);
            this.dgvDelfor.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvDelfor_CellClick);
            this.dgvDelfor.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.dgvDelfor_ToolTipTextNeeded);
            // 
            // tabPage_Delivery
            // 
            this.tabPage_Delivery.Controls.Add(this.dgvDelivery);
            this.tabPage_Delivery.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Delivery.Name = "tabPage_Delivery";
            this.tabPage_Delivery.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Delivery.Size = new System.Drawing.Size(454, 312);
            this.tabPage_Delivery.TabIndex = 1;
            this.tabPage_Delivery.Text = "Erledigte ";
            this.tabPage_Delivery.UseVisualStyleBackColor = true;
            // 
            // dgvDelivery
            // 
            this.dgvDelivery.BackColor = System.Drawing.Color.White;
            this.dgvDelivery.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvDelivery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDelivery.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvDelivery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvDelivery.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvDelivery.Location = new System.Drawing.Point(3, 3);
            this.dgvDelivery.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            gridViewCheckBoxColumn6.HeaderText = "Select";
            gridViewCheckBoxColumn6.Name = "Select";
            this.dgvDelivery.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn6});
            this.dgvDelivery.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvDelivery.MasterTemplate.EnableFiltering = true;
            this.dgvDelivery.MasterTemplate.ShowFilteringRow = false;
            this.dgvDelivery.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvDelivery.MasterTemplate.ViewDefinition = tableViewDefinition9;
            this.dgvDelivery.Name = "dgvDelivery";
            this.dgvDelivery.ReadOnly = true;
            this.dgvDelivery.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvDelivery.ShowHeaderCellButtons = true;
            this.dgvDelivery.Size = new System.Drawing.Size(448, 306);
            this.dgvDelivery.TabIndex = 27;
            this.dgvDelivery.ThemeName = "ControlDefault";
            this.dgvDelivery.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvDelivery_CellClick);
            // 
            // MenueDelfor
            // 
            this.MenueDelfor.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenueDelfor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh,
            this.tsbtnAllCheck,
            this.tsbtnAllUncheck,
            this.tsbtnClose,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.tslOrderByEDate});
            this.MenueDelfor.Location = new System.Drawing.Point(0, 0);
            this.MenueDelfor.myColorFrom = System.Drawing.Color.Azure;
            this.MenueDelfor.myColorTo = System.Drawing.Color.Blue;
            this.MenueDelfor.myUnderlineColor = System.Drawing.Color.White;
            this.MenueDelfor.myUnderlined = true;
            this.MenueDelfor.Name = "MenueDelfor";
            this.MenueDelfor.Size = new System.Drawing.Size(462, 27);
            this.MenueDelfor.TabIndex = 25;
            this.MenueDelfor.Text = "afToolStrip2";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(24, 24);
            this.tsbtnRefresh.Text = "Liste aktualisieren...";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // tsbtnAllCheck
            // 
            this.tsbtnAllCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAllCheck.Image = global::Sped4.Properties.Resources.checkbox_32x32;
            this.tsbtnAllCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAllCheck.Name = "tsbtnAllCheck";
            this.tsbtnAllCheck.Size = new System.Drawing.Size(24, 24);
            this.tsbtnAllCheck.Text = "alle Datensätze auswählen";
            this.tsbtnAllCheck.Visible = false;
            this.tsbtnAllCheck.Click += new System.EventHandler(this.tsbtnAllCheck_Click);
            // 
            // tsbtnAllUncheck
            // 
            this.tsbtnAllUncheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAllUncheck.Image = global::Sped4.Properties.Resources.checkbox_unchecked_32x32;
            this.tsbtnAllUncheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAllUncheck.Name = "tsbtnAllUncheck";
            this.tsbtnAllUncheck.Size = new System.Drawing.Size(24, 24);
            this.tsbtnAllUncheck.Text = "Auswahl aufheben";
            this.tsbtnAllUncheck.Visible = false;
            this.tsbtnAllUncheck.Click += new System.EventHandler(this.tsbtnAllUncheck_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(24, 24);
            this.tsbtnClose.Text = "schliessen";
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
            this.toolStripLabel2.Size = new System.Drawing.Size(80, 24);
            this.toolStripLabel2.Text = "Lieferplanung";
            // 
            // tslOrderByEDate
            // 
            this.tslOrderByEDate.Name = "tslOrderByEDate";
            this.tslOrderByEDate.Size = new System.Drawing.Size(0, 24);
            // 
            // splitBestand
            // 
            this.splitBestand.Controls.Add(this.dgvArticle);
            this.splitBestand.Controls.Add(this.MenuBestand);
            this.splitBestand.Location = new System.Drawing.Point(466, 0);
            this.splitBestand.Name = "splitBestand";
            // 
            // 
            // 
            this.splitBestand.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitBestand.Size = new System.Drawing.Size(538, 365);
            this.splitBestand.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.03799999F, 0F);
            this.splitBestand.SizeInfo.SplitterCorrection = new System.Drawing.Size(38, 0);
            this.splitBestand.TabIndex = 1;
            this.splitBestand.TabStop = false;
            this.splitBestand.ThemeName = "ControlDefault";
            // 
            // dgvArticle
            // 
            this.dgvArticle.BackColor = System.Drawing.Color.White;
            this.dgvArticle.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvArticle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvArticle.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvArticle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvArticle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvArticle.Location = new System.Drawing.Point(0, 27);
            this.dgvArticle.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            this.dgvArticle.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvArticle.MasterTemplate.EnableFiltering = true;
            this.dgvArticle.MasterTemplate.ShowFilteringRow = false;
            this.dgvArticle.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvArticle.MasterTemplate.ViewDefinition = tableViewDefinition7;
            this.dgvArticle.Name = "dgvArticle";
            this.dgvArticle.ReadOnly = true;
            this.dgvArticle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvArticle.ShowHeaderCellButtons = true;
            this.dgvArticle.Size = new System.Drawing.Size(538, 338);
            this.dgvArticle.TabIndex = 27;
            this.dgvArticle.ThemeName = "ControlDefault";
            this.dgvArticle.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvArticle_CellClick);
            // 
            // MenuBestand
            // 
            this.MenuBestand.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuBestand.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnArticleRefresh,
            this.tsbtnArticleSetSelection,
            this.tsbtnArticleSetUnSelection,
            this.tsbtnCreateCall,
            this.toolStripSeparator1,
            this.toolStripLabel1});
            this.MenuBestand.Location = new System.Drawing.Point(0, 0);
            this.MenuBestand.myColorFrom = System.Drawing.Color.Azure;
            this.MenuBestand.myColorTo = System.Drawing.Color.Blue;
            this.MenuBestand.myUnderlineColor = System.Drawing.Color.White;
            this.MenuBestand.myUnderlined = true;
            this.MenuBestand.Name = "MenuBestand";
            this.MenuBestand.Size = new System.Drawing.Size(538, 27);
            this.MenuBestand.TabIndex = 26;
            this.MenuBestand.Text = "afToolStrip2";
            // 
            // tsbtnArticleRefresh
            // 
            this.tsbtnArticleRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnArticleRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnArticleRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnArticleRefresh.Name = "tsbtnArticleRefresh";
            this.tsbtnArticleRefresh.Size = new System.Drawing.Size(24, 24);
            this.tsbtnArticleRefresh.Text = "Liste aktualisieren...";
            this.tsbtnArticleRefresh.Click += new System.EventHandler(this.tsbtnArticleRefresh_Click);
            // 
            // tsbtnArticleSetSelection
            // 
            this.tsbtnArticleSetSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnArticleSetSelection.Image = global::Sped4.Properties.Resources.checkbox_32x32;
            this.tsbtnArticleSetSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnArticleSetSelection.Name = "tsbtnArticleSetSelection";
            this.tsbtnArticleSetSelection.Size = new System.Drawing.Size(24, 24);
            this.tsbtnArticleSetSelection.Text = "benötigte Datensätze auswählen";
            this.tsbtnArticleSetSelection.Click += new System.EventHandler(this.tsbtnArticleSetSelection_Click);
            // 
            // tsbtnArticleSetUnSelection
            // 
            this.tsbtnArticleSetUnSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnArticleSetUnSelection.Image = global::Sped4.Properties.Resources.checkbox_unchecked_32x32;
            this.tsbtnArticleSetUnSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnArticleSetUnSelection.Name = "tsbtnArticleSetUnSelection";
            this.tsbtnArticleSetUnSelection.Size = new System.Drawing.Size(24, 24);
            this.tsbtnArticleSetUnSelection.Text = "Auswahl aufheben";
            this.tsbtnArticleSetUnSelection.Click += new System.EventHandler(this.tsbtnArticleSetUnSelection_Click);
            // 
            // tsbtnCreateCall
            // 
            this.tsbtnCreateCall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCreateCall.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.tsbtnCreateCall.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCreateCall.Name = "tsbtnCreateCall";
            this.tsbtnCreateCall.Size = new System.Drawing.Size(24, 24);
            this.tsbtnCreateCall.Text = "Abruf erstellen ";
            this.tsbtnCreateCall.Click += new System.EventHandler(this.tsbtnCreateCall_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(49, 24);
            this.toolStripLabel1.Text = "Bestand";
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.BackColor = System.Drawing.Color.White;
            this.afMinMaxPanel1.Controls.Add(this.lStoreOutQuantity);
            this.afMinMaxPanel1.Controls.Add(this.lForecastQuantity);
            this.afMinMaxPanel1.Controls.Add(this.comboWerksnummer);
            this.afMinMaxPanel1.Controls.Add(this.switch_QuantitySelectionMode);
            this.afMinMaxPanel1.Controls.Add(this.lMengenAuswahl);
            this.afMinMaxPanel1.Controls.Add(this.label4);
            this.afMinMaxPanel1.Controls.Add(this.label3);
            this.afMinMaxPanel1.Controls.Add(this.label2);
            this.afMinMaxPanel1.Controls.Add(this.tbStoreOutQuantity);
            this.afMinMaxPanel1.Controls.Add(this.tbForecastQuantity);
            this.afMinMaxPanel1.Controls.Add(this.label1);
            this.afMinMaxPanel1.Controls.Add(this.switchSort);
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 28);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Optionen";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(1004, 113);
            this.afMinMaxPanel1.TabIndex = 12;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            // 
            // lStoreOutQuantity
            // 
            this.lStoreOutQuantity.AutoSize = true;
            this.lStoreOutQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lStoreOutQuantity.Location = new System.Drawing.Point(611, 61);
            this.lStoreOutQuantity.Name = "lStoreOutQuantity";
            this.lStoreOutQuantity.Size = new System.Drawing.Size(24, 15);
            this.lStoreOutQuantity.TabIndex = 15;
            this.lStoreOutQuantity.Text = "KG";
            // 
            // lForecastQuantity
            // 
            this.lForecastQuantity.AutoSize = true;
            this.lForecastQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lForecastQuantity.Location = new System.Drawing.Point(611, 33);
            this.lForecastQuantity.Name = "lForecastQuantity";
            this.lForecastQuantity.Size = new System.Drawing.Size(24, 15);
            this.lForecastQuantity.TabIndex = 14;
            this.lForecastQuantity.Text = "KG";
            // 
            // comboWerksnummer
            // 
            this.comboWerksnummer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboWerksnummer.FormattingEnabled = true;
            this.comboWerksnummer.Location = new System.Drawing.Point(124, 61);
            this.comboWerksnummer.Name = "comboWerksnummer";
            this.comboWerksnummer.Size = new System.Drawing.Size(139, 21);
            this.comboWerksnummer.TabIndex = 13;
            this.comboWerksnummer.SelectedIndexChanged += new System.EventHandler(this.comboWerksnummer_SelectedIndexChanged);
            // 
            // switch_QuantitySelectionMode
            // 
            this.switch_QuantitySelectionMode.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.switch_QuantitySelectionMode.Location = new System.Drawing.Point(124, 32);
            this.switch_QuantitySelectionMode.Name = "switch_QuantitySelectionMode";
            this.switch_QuantitySelectionMode.OffText = "Anzahl";
            this.switch_QuantitySelectionMode.OnText = "KG";
            this.switch_QuantitySelectionMode.Size = new System.Drawing.Size(139, 23);
            this.switch_QuantitySelectionMode.TabIndex = 3;
            this.switch_QuantitySelectionMode.ThemeName = "ControlDefault";
            this.switch_QuantitySelectionMode.ValueChanged += new System.EventHandler(this.switch_QuantitySelectionMode_ValueChanged);
            ((Telerik.WinControls.UI.RadToggleSwitchElement)(this.switch_QuantitySelectionMode.GetChildAt(0))).ThumbOffset = 119;
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switch_QuantitySelectionMode.GetChildAt(0).GetChildAt(1))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(110)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switch_QuantitySelectionMode.GetChildAt(0).GetChildAt(1))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(174)))), ((int)(((byte)(64)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switch_QuantitySelectionMode.GetChildAt(0).GetChildAt(1))).Text = "Anzahl";
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switch_QuantitySelectionMode.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(163)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switch_QuantitySelectionMode.GetChildAt(0).GetChildAt(1))).Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            // 
            // lMengenAuswahl
            // 
            this.lMengenAuswahl.AutoSize = true;
            this.lMengenAuswahl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lMengenAuswahl.Location = new System.Drawing.Point(25, 33);
            this.lMengenAuswahl.Name = "lMengenAuswahl";
            this.lMengenAuswahl.Size = new System.Drawing.Size(48, 15);
            this.lMengenAuswahl.TabIndex = 10;
            this.lMengenAuswahl.Text = "Einheit:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Werksnummer:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(329, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Auslagerung:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(329, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "geplante Liefermenge:";
            // 
            // tbStoreOutQuantity
            // 
            this.tbStoreOutQuantity.Location = new System.Drawing.Point(466, 59);
            this.tbStoreOutQuantity.Name = "tbStoreOutQuantity";
            this.tbStoreOutQuantity.Size = new System.Drawing.Size(139, 20);
            this.tbStoreOutQuantity.TabIndex = 5;
            this.tbStoreOutQuantity.ThemeName = "ControlDefault";
            // 
            // tbForecastQuantity
            // 
            this.tbForecastQuantity.Location = new System.Drawing.Point(466, 33);
            this.tbForecastQuantity.Name = "tbForecastQuantity";
            this.tbForecastQuantity.Size = new System.Drawing.Size(139, 20);
            this.tbForecastQuantity.TabIndex = 4;
            this.tbForecastQuantity.ThemeName = "ControlDefault";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(661, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sortierung:";
            // 
            // switchSort
            // 
            this.switchSort.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.switchSort.Location = new System.Drawing.Point(734, 30);
            this.switchSort.Name = "switchSort";
            this.switchSort.OffText = "Glühdatum";
            this.switchSort.OnText = "Eingangsdatum";
            this.switchSort.Size = new System.Drawing.Size(139, 23);
            this.switchSort.TabIndex = 2;
            this.switchSort.ThemeName = "ControlDefault";
            this.switchSort.ValueChanged += new System.EventHandler(this.switchSort_ValueChanged);
            ((Telerik.WinControls.UI.RadToggleSwitchElement)(this.switchSort.GetChildAt(0))).ThumbOffset = 119;
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switchSort.GetChildAt(0).GetChildAt(1))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(110)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switchSort.GetChildAt(0).GetChildAt(1))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(174)))), ((int)(((byte)(64)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switchSort.GetChildAt(0).GetChildAt(1))).Text = "Glühdatum";
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switchSort.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(163)))));
            ((Telerik.WinControls.UI.ToggleSwitchPartElement)(this.switchSort.GetChildAt(0).GetChildAt(1))).Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
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
            this.afColorLabel1.myText = "DELFOR Lieferplanung";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(1004, 28);
            this.afColorLabel1.TabIndex = 11;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrDelforDeliveryForecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.afMinMaxPanel1);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrDelforDeliveryForecast";
            this.Size = new System.Drawing.Size(1004, 506);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDelforCall)).EndInit();
            this.splitDelforCall.ResumeLayout(false);
            this.splitDelforCall.PerformLayout();
            this.tabDelfor.ResumeLayout(false);
            this.tabPage_Forecast.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelfor.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelfor)).EndInit();
            this.tabPage_Delivery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelivery.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelivery)).EndInit();
            this.MenueDelfor.ResumeLayout(false);
            this.MenueDelfor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBestand)).EndInit();
            this.splitBestand.ResumeLayout(false);
            this.splitBestand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticle.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticle)).EndInit();
            this.MenuBestand.ResumeLayout(false);
            this.MenuBestand.PerformLayout();
            this.afMinMaxPanel1.ResumeLayout(false);
            this.afMinMaxPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.switch_QuantitySelectionMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStoreOutQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbForecastQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.switchSort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal Controls.AFColorLabel afColorLabel1;
        private Controls.AFMinMaxPanel afMinMaxPanel1;
        private Telerik.WinControls.UI.RadSplitContainer scMain;
        private Telerik.WinControls.UI.SplitPanel splitDelforCall;
        private Telerik.WinControls.UI.SplitPanel splitBestand;
        private Controls.AFToolStrip MenueDelfor;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ToolStripButton tsbtnAllCheck;
        private System.Windows.Forms.ToolStripButton tsbtnAllUncheck;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private Controls.AFToolStrip MenuBestand;
        private System.Windows.Forms.ToolStripButton tsbtnArticleRefresh;
        private System.Windows.Forms.ToolStripButton tsbtnArticleSetSelection;
        private System.Windows.Forms.ToolStripButton tsbtnArticleSetUnSelection;
        private System.Windows.Forms.ToolStripButton tsbtnCreateCall;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public Telerik.WinControls.UI.RadGridView dgvDelfor;
        public Telerik.WinControls.UI.RadGridView dgvArticle;
        private System.Windows.Forms.ToolStripLabel tslOrderByEDate;
        private Telerik.WinControls.UI.RadToggleSwitch switchSort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox tbStoreOutQuantity;
        private Telerik.WinControls.UI.RadTextBox tbForecastQuantity;
        private System.Windows.Forms.Label label4;
        private Telerik.WinControls.UI.RadToggleSwitch switch_QuantitySelectionMode;
        private System.Windows.Forms.Label lMengenAuswahl;
        private System.Windows.Forms.ComboBox comboWerksnummer;
        private System.Windows.Forms.Label lStoreOutQuantity;
        private System.Windows.Forms.Label lForecastQuantity;
        private System.Windows.Forms.TabControl tabDelfor;
        private System.Windows.Forms.TabPage tabPage_Forecast;
        private System.Windows.Forms.TabPage tabPage_Delivery;
        public Telerik.WinControls.UI.RadGridView dgvDelivery;
    }
}
