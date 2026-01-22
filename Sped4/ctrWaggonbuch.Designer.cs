namespace Sped4
{
   partial class ctrWaggonbuch
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrWaggonbuch));
         this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
         this.dgv = new Telerik.WinControls.UI.RadGridView();
         this.panBestandlist = new System.Windows.Forms.Panel();
         this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
         this.tsbtnSearchShow = new System.Windows.Forms.ToolStripButton();
         this.tsbtnSearch = new System.Windows.Forms.ToolStripButton();
         this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
         this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.tscbSearch = new System.Windows.Forms.ToolStripComboBox();
         this.tslSearchText = new System.Windows.Forms.ToolStripLabel();
         this.tstbSearchArtikel = new System.Windows.Forms.ToolStripTextBox();
         this.tsbtnStartSearch = new System.Windows.Forms.ToolStripButton();
         this.scBestandList = new Telerik.WinControls.UI.RadSplitContainer();
         this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
         this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
         this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.label6 = new System.Windows.Forms.Label();
         this.cbBestandsart = new System.Windows.Forms.ComboBox();
         this.btnSearchA = new System.Windows.Forms.Button();
         this.tbBrutto = new System.Windows.Forms.TextBox();
         this.lZeitraumVon = new System.Windows.Forms.Label();
         this.tbNetto = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.tbAnzahl = new System.Windows.Forms.TextBox();
         this.dtpBis = new System.Windows.Forms.DateTimePicker();
         this.label3 = new System.Windows.Forms.Label();
         this.dtpVon = new System.Windows.Forms.DateTimePicker();
         this.label5 = new System.Windows.Forms.Label();
         this.tbAuftraggeber = new System.Windows.Forms.TextBox();
         this.tbSearchA = new System.Windows.Forms.TextBox();
         this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
         ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
         this.panBestandlist.SuspendLayout();
         this.afToolStrip2.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.scBestandList)).BeginInit();
         this.scBestandList.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
         this.splitPanel2.SuspendLayout();
         this.afMinMaxPanel1.SuspendLayout();
         this.groupBox1.SuspendLayout();
         this.SuspendLayout();
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
         // dgv
         // 
         this.dgv.MasterTemplate.AutoExpandGroups = true;
         this.dgv.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
         this.dgv.MasterTemplate.EnableAlternatingRowColor = true;
         this.dgv.MasterTemplate.EnableFiltering = true;
         this.dgv.MasterTemplate.ShowFilteringRow = false;
         this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
         this.dgv.Name = "dgv";
         this.dgv.ReadOnly = true;
         this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
         // 
         // 
         // 
         this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 240, 150);
         this.dgv.ShowHeaderCellButtons = true;
         this.dgv.Size = new System.Drawing.Size(879, 318);
         this.dgv.TabIndex = 24;
         this.dgv.Text = "radGridView1";
         this.dgv.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.dgv_ContextMenuOpening);
         this.dgv.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.dgv_ToolTipTextNeeded);
         // 
         // panBestandlist
         // 
         this.panBestandlist.Controls.Add(this.dgv);
         this.panBestandlist.Controls.Add(this.afToolStrip2);
         this.panBestandlist.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panBestandlist.Location = new System.Drawing.Point(0, 0);
         this.panBestandlist.Name = "panBestandlist";
         this.panBestandlist.Size = new System.Drawing.Size(879, 343);
         this.panBestandlist.TabIndex = 25;
         // 
         // afToolStrip2
         // 
         this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSearchShow,
            this.tsbtnSearch,
            this.tsbtnClear,
            this.tsbtnClose,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.tscbSearch,
            this.tslSearchText,
            this.tstbSearchArtikel,
            this.tsbtnStartSearch});
         this.afToolStrip2.Location = new System.Drawing.Point(0, 0);
         this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
         this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
         this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
         this.afToolStrip2.myUnderlined = true;
         this.afToolStrip2.Name = "afToolStrip2";
         this.afToolStrip2.Size = new System.Drawing.Size(879, 25);
         this.afToolStrip2.TabIndex = 23;
         this.afToolStrip2.Text = "afToolStrip2";
         // 
         // tsbtnSearchShow
         // 
         this.tsbtnSearchShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbtnSearchShow.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearchShow.Image")));
         this.tsbtnSearchShow.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbtnSearchShow.Name = "tsbtnSearchShow";
         this.tsbtnSearchShow.Size = new System.Drawing.Size(23, 22);
         this.tsbtnSearchShow.Text = "Sucheingabe öffnen";
         this.tsbtnSearchShow.Visible = false;
         this.tsbtnSearchShow.Click += new System.EventHandler(this.tsbtnSearchShow_Click);
         // 
         // tsbtnSearch
         // 
         this.tsbtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearch.Image")));
         this.tsbtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbtnSearch.Name = "tsbtnSearch";
         this.tsbtnSearch.Size = new System.Drawing.Size(23, 22);
         this.tsbtnSearch.Text = "Bestandsdaten laden";
         this.tsbtnSearch.Click += new System.EventHandler(this.tsbtnSearch_Click);
         // 
         // tsbtnClear
         // 
         this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClear.Image")));
         this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbtnClear.Name = "tsbtnClear";
         this.tsbtnClear.Size = new System.Drawing.Size(23, 22);
         this.tsbtnClear.Text = "alle Vorgaben zurücksetzen";
         this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
         // 
         // tsbtnClose
         // 
         this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClose.Image")));
         this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbtnClose.Name = "tsbtnClose";
         this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
         this.tsbtnClose.Text = "Suche schliessen";
         this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
         this.toolStripSeparator2.Visible = false;
         // 
         // tscbSearch
         // 
         this.tscbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.tscbSearch.Name = "tscbSearch";
         this.tscbSearch.Size = new System.Drawing.Size(100, 25);
         this.tscbSearch.Visible = false;
         this.tscbSearch.SelectedIndexChanged += new System.EventHandler(this.tscbSearch_SelectedIndexChanged);
         // 
         // tslSearchText
         // 
         this.tslSearchText.Name = "tslSearchText";
         this.tslSearchText.Size = new System.Drawing.Size(71, 22);
         this.tslSearchText.Text = "Suchbegriff:";
         this.tslSearchText.Visible = false;
         // 
         // tstbSearchArtikel
         // 
         this.tstbSearchArtikel.Name = "tstbSearchArtikel";
         this.tstbSearchArtikel.Size = new System.Drawing.Size(100, 25);
         this.tstbSearchArtikel.Visible = false;
         this.tstbSearchArtikel.TextChanged += new System.EventHandler(this.tstbSearchArtikel_TextChanged);
         // 
         // tsbtnStartSearch
         // 
         this.tsbtnStartSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbtnStartSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnStartSearch.Image")));
         this.tsbtnStartSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbtnStartSearch.Name = "tsbtnStartSearch";
         this.tsbtnStartSearch.Size = new System.Drawing.Size(23, 22);
         this.tsbtnStartSearch.Text = "toolStripButton1";
         this.tsbtnStartSearch.Visible = false;
         this.tsbtnStartSearch.Click += new System.EventHandler(this.tsbtnStartSearch_Click);
         // 
         // scBestandList
         // 
         this.scBestandList.BackColor = System.Drawing.SystemColors.ControlLightLight;
         this.scBestandList.Controls.Add(this.splitPanel1);
         this.scBestandList.Controls.Add(this.splitPanel2);
         this.scBestandList.Dock = System.Windows.Forms.DockStyle.Fill;
         this.scBestandList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.scBestandList.Location = new System.Drawing.Point(0, 202);
         this.scBestandList.Name = "scBestandList";
         // 
         // 
         // 
         this.scBestandList.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 202, 200, 200);
         this.scBestandList.RootElement.MinSize = new System.Drawing.Size(25, 25);
         this.scBestandList.Size = new System.Drawing.Size(879, 343);
         this.scBestandList.SplitterWidth = 4;
         this.scBestandList.TabIndex = 34;
         this.scBestandList.TabStop = false;
         this.scBestandList.Text = "radSplitContainer1";
         // 
         // splitPanel1
         // 
         this.splitPanel1.AutoScroll = true;
         this.splitPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
         this.splitPanel1.Collapsed = true;
         this.splitPanel1.Location = new System.Drawing.Point(0, 0);
         this.splitPanel1.Name = "splitPanel1";
         // 
         // 
         // 
         this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
         this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
         this.splitPanel1.Size = new System.Drawing.Size(369, 296);
         this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.07828572F, 0F);
         this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-22, 0);
         this.splitPanel1.TabIndex = 0;
         this.splitPanel1.TabStop = false;
         this.splitPanel1.Text = "splitPanel1";
         this.splitPanel1.Visible = false;
         // 
         // splitPanel2
         // 
         this.splitPanel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
         this.splitPanel2.Controls.Add(this.panBestandlist);
         this.splitPanel2.Location = new System.Drawing.Point(0, 0);
         this.splitPanel2.Name = "splitPanel2";
         // 
         // 
         // 
         this.splitPanel2.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
         this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
         this.splitPanel2.Size = new System.Drawing.Size(879, 343);
         this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.07828569F, 0F);
         this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(22, 0);
         this.splitPanel2.TabIndex = 1;
         this.splitPanel2.TabStop = false;
         this.splitPanel2.Text = "splitPanel2";
         // 
         // afMinMaxPanel1
         // 
         this.afMinMaxPanel1.BackColor = System.Drawing.Color.White;
         this.afMinMaxPanel1.Controls.Add(this.groupBox1);
         this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
         this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 28);
         this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
         this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.afMinMaxPanel1.myImage = ((System.Drawing.Image)(resources.GetObject("afMinMaxPanel1.myImage")));
         this.afMinMaxPanel1.myText = "Optionen";
         this.afMinMaxPanel1.Name = "afMinMaxPanel1";
         this.afMinMaxPanel1.Size = new System.Drawing.Size(879, 174);
         this.afMinMaxPanel1.TabIndex = 10;
         this.afMinMaxPanel1.Text = "afMinMaxPanel1";
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.label6);
         this.groupBox1.Controls.Add(this.cbBestandsart);
         this.groupBox1.Controls.Add(this.btnSearchA);
         this.groupBox1.Controls.Add(this.tbBrutto);
         this.groupBox1.Controls.Add(this.lZeitraumVon);
         this.groupBox1.Controls.Add(this.tbNetto);
         this.groupBox1.Controls.Add(this.label1);
         this.groupBox1.Controls.Add(this.label2);
         this.groupBox1.Controls.Add(this.tbAnzahl);
         this.groupBox1.Controls.Add(this.dtpBis);
         this.groupBox1.Controls.Add(this.label3);
         this.groupBox1.Controls.Add(this.dtpVon);
         this.groupBox1.Controls.Add(this.label5);
         this.groupBox1.Controls.Add(this.tbAuftraggeber);
         this.groupBox1.Controls.Add(this.tbSearchA);
         this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
         this.groupBox1.Location = new System.Drawing.Point(29, 25);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(669, 134);
         this.groupBox1.TabIndex = 10;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Filter";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.ForeColor = System.Drawing.Color.DarkBlue;
         this.label6.Location = new System.Drawing.Point(15, 23);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(45, 13);
         this.label6.TabIndex = 163;
         this.label6.Text = "Ansicht:";
         this.label6.Click += new System.EventHandler(this.label6_Click);
         // 
         // cbBestandsart
         // 
         this.cbBestandsart.AllowDrop = true;
         this.cbBestandsart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.cbBestandsart.FormattingEnabled = true;
         this.cbBestandsart.ItemHeight = 13;
         this.cbBestandsart.Location = new System.Drawing.Point(106, 20);
         this.cbBestandsart.Name = "cbBestandsart";
         this.cbBestandsart.Size = new System.Drawing.Size(372, 21);
         this.cbBestandsart.TabIndex = 162;
         this.cbBestandsart.SelectedIndexChanged += new System.EventHandler(this.cbBestandsart_SelectedIndexChanged);
         // 
         // btnSearchA
         // 
         this.btnSearchA.ForeColor = System.Drawing.Color.DarkBlue;
         this.btnSearchA.Location = new System.Drawing.Point(18, 48);
         this.btnSearchA.Name = "btnSearchA";
         this.btnSearchA.Size = new System.Drawing.Size(85, 22);
         this.btnSearchA.TabIndex = 157;
         this.btnSearchA.Text = "Adresse";
         this.btnSearchA.UseVisualStyleBackColor = true;
         this.btnSearchA.Click += new System.EventHandler(this.btnSearchA_Click);
         // 
         // tbBrutto
         // 
         this.tbBrutto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbBrutto.Location = new System.Drawing.Point(560, 67);
         this.tbBrutto.Name = "tbBrutto";
         this.tbBrutto.ReadOnly = true;
         this.tbBrutto.Size = new System.Drawing.Size(88, 20);
         this.tbBrutto.TabIndex = 153;
         this.tbBrutto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.tbBrutto.Visible = false;
         // 
         // lZeitraumVon
         // 
         this.lZeitraumVon.AutoSize = true;
         this.lZeitraumVon.ForeColor = System.Drawing.Color.DarkBlue;
         this.lZeitraumVon.Location = new System.Drawing.Point(15, 83);
         this.lZeitraumVon.Name = "lZeitraumVon";
         this.lZeitraumVon.Size = new System.Drawing.Size(72, 13);
         this.lZeitraumVon.TabIndex = 49;
         this.lZeitraumVon.Text = "Zeitraum von:";
         // 
         // tbNetto
         // 
         this.tbNetto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbNetto.Location = new System.Drawing.Point(560, 44);
         this.tbNetto.Name = "tbNetto";
         this.tbNetto.ReadOnly = true;
         this.tbNetto.Size = new System.Drawing.Size(88, 20);
         this.tbNetto.TabIndex = 152;
         this.tbNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         this.tbNetto.Visible = false;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.ForeColor = System.Drawing.Color.DarkBlue;
         this.label1.Location = new System.Drawing.Point(15, 106);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(67, 13);
         this.label1.TabIndex = 50;
         this.label1.Text = "Zeitraum bis:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.ForeColor = System.Drawing.Color.DarkBlue;
         this.label2.Location = new System.Drawing.Point(498, 23);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(42, 13);
         this.label2.TabIndex = 52;
         this.label2.Text = "Anzahl:";
         // 
         // tbAnzahl
         // 
         this.tbAnzahl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbAnzahl.Location = new System.Drawing.Point(560, 21);
         this.tbAnzahl.Name = "tbAnzahl";
         this.tbAnzahl.ReadOnly = true;
         this.tbAnzahl.Size = new System.Drawing.Size(88, 20);
         this.tbAnzahl.TabIndex = 151;
         this.tbAnzahl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
         // 
         // dtpBis
         // 
         this.dtpBis.Location = new System.Drawing.Point(109, 101);
         this.dtpBis.Name = "dtpBis";
         this.dtpBis.Size = new System.Drawing.Size(184, 20);
         this.dtpBis.TabIndex = 51;
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.ForeColor = System.Drawing.Color.DarkBlue;
         this.label3.Location = new System.Drawing.Point(498, 46);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(40, 13);
         this.label3.TabIndex = 53;
         this.label3.Text = "Private";
         this.label3.Visible = false;
         // 
         // dtpVon
         // 
         this.dtpVon.Location = new System.Drawing.Point(109, 78);
         this.dtpVon.Name = "dtpVon";
         this.dtpVon.Size = new System.Drawing.Size(184, 20);
         this.dtpVon.TabIndex = 12;
         this.dtpVon.ValueChanged += new System.EventHandler(this.dtpVon_ValueChanged);
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.ForeColor = System.Drawing.Color.DarkBlue;
         this.label5.Location = new System.Drawing.Point(496, 69);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(22, 13);
         this.label5.TabIndex = 54;
         this.label5.Text = "DB";
         this.label5.Visible = false;
         // 
         // tbAuftraggeber
         // 
         this.tbAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
         this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbAuftraggeber.Enabled = false;
         this.tbAuftraggeber.Location = new System.Drawing.Point(212, 51);
         this.tbAuftraggeber.Name = "tbAuftraggeber";
         this.tbAuftraggeber.ReadOnly = true;
         this.tbAuftraggeber.Size = new System.Drawing.Size(266, 20);
         this.tbAuftraggeber.TabIndex = 159;
         // 
         // tbSearchA
         // 
         this.tbSearchA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbSearchA.Location = new System.Drawing.Point(109, 50);
         this.tbSearchA.Name = "tbSearchA";
         this.tbSearchA.Size = new System.Drawing.Size(97, 20);
         this.tbSearchA.TabIndex = 158;
         this.tbSearchA.TextChanged += new System.EventHandler(this.tbSearchA_TextChanged);
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
         this.afColorLabel1.myText = "Waggonbuch";
         this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
         this.afColorLabel1.myUnderlined = true;
         this.afColorLabel1.Name = "afColorLabel1";
         this.afColorLabel1.Size = new System.Drawing.Size(879, 28);
         this.afColorLabel1.TabIndex = 9;
         this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
         // 
         // ctrWaggonbuch
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.scBestandList);
         this.Controls.Add(this.afMinMaxPanel1);
         this.Controls.Add(this.afColorLabel1);
         this.Name = "ctrWaggonbuch";
         this.Size = new System.Drawing.Size(879, 545);
         this.Load += new System.EventHandler(this.ctrBestand_Load);
         ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
         this.panBestandlist.ResumeLayout(false);
         this.panBestandlist.PerformLayout();
         this.afToolStrip2.ResumeLayout(false);
         this.afToolStrip2.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.scBestandList)).EndInit();
         this.scBestandList.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
         this.splitPanel2.ResumeLayout(false);
         this.afMinMaxPanel1.ResumeLayout(false);
         this.afMinMaxPanel1.PerformLayout();
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private Telerik.WinControls.UI.RadGridView radGridView1;
      private Controls.AFMinMaxPanel afMinMaxPanel1;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.ComboBox cbBestandsart;
      private System.Windows.Forms.Button btnSearchA;
      public System.Windows.Forms.TextBox tbBrutto;
      private System.Windows.Forms.Label lZeitraumVon;
      public System.Windows.Forms.TextBox tbNetto;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      public System.Windows.Forms.TextBox tbAnzahl;
      private System.Windows.Forms.DateTimePicker dtpBis;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.DateTimePicker dtpVon;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.TextBox tbAuftraggeber;
      private System.Windows.Forms.TextBox tbSearchA;
      private Controls.AFToolStrip afToolStrip2;
      private System.Windows.Forms.ToolStripButton tsbtnSearch;
      private System.Windows.Forms.ToolStripButton tsbtnClear;
      private System.Windows.Forms.ToolStripButton tsbtnClose;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStripComboBox tscbSearch;
      private System.Windows.Forms.ToolStripLabel tslSearchText;
      private System.Windows.Forms.ToolStripTextBox tstbSearchArtikel;
      private System.Windows.Forms.SaveFileDialog saveFileDialog;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      internal Controls.AFColorLabel afColorLabel1;
      private System.Windows.Forms.Panel panBestandlist;
      private Telerik.WinControls.UI.RadSplitContainer scBestandList;
      private Telerik.WinControls.UI.SplitPanel splitPanel1;
      private Telerik.WinControls.UI.SplitPanel splitPanel2;
      private System.Windows.Forms.ToolStripButton tsbtnSearchShow;
      public Telerik.WinControls.UI.RadGridView dgv;
      private System.Windows.Forms.ToolStripButton tsbtnStartSearch;
   }
}
