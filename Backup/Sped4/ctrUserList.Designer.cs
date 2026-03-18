namespace Sped4
{
    partial class ctrUserList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scListen = new System.Windows.Forms.SplitContainer();
            this.scNewList = new System.Windows.Forms.SplitContainer();
            this.dgvNewList = new Sped4.Controls.AFGrid();
            this.dgvTable = new Sped4.Controls.AFGrid();
            this.afToolStrip4 = new Sped4.Controls.AFToolStrip();
            this.tsbtnAllToList = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSelectedToList = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelSelectedFromList = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelAllFromList = new System.Windows.Forms.ToolStripButton();
            this.dgvUserList = new Sped4.Controls.AFGrid();
            this.tsArtikelGrid = new Sped4.Controls.AFToolStrip();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnNewList = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnIntegrate = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSeparate = new System.Windows.Forms.ToolStripButton();
            this.gbListePref = new System.Windows.Forms.GroupBox();
            this.cbListe = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.cbPrivate = new System.Windows.Forms.CheckBox();
            this.lAktion = new System.Windows.Forms.Label();
            this.tbBezeichnung = new System.Windows.Forms.TextBox();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.scListen)).BeginInit();
            this.scListen.Panel1.SuspendLayout();
            this.scListen.Panel2.SuspendLayout();
            this.scListen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scNewList)).BeginInit();
            this.scNewList.Panel1.SuspendLayout();
            this.scNewList.Panel2.SuspendLayout();
            this.scNewList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.afToolStrip4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).BeginInit();
            this.afMinMaxPanel1.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.gbListePref.SuspendLayout();
            this.SuspendLayout();
            // 
            // scListen
            // 
            this.scListen.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.scListen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scListen.Location = new System.Drawing.Point(0, 212);
            this.scListen.Name = "scListen";
            // 
            // scListen.Panel1
            // 
            this.scListen.Panel1.BackColor = System.Drawing.Color.White;
            this.scListen.Panel1.Controls.Add(this.scNewList);
            // 
            // scListen.Panel2
            // 
            this.scListen.Panel2.BackColor = System.Drawing.Color.White;
            this.scListen.Panel2.Controls.Add(this.dgvUserList);
            this.scListen.Size = new System.Drawing.Size(864, 340);
            this.scListen.SplitterDistance = 585;
            this.scListen.TabIndex = 0;
            // 
            // scNewList
            // 
            this.scNewList.BackColor = System.Drawing.Color.DimGray;
            this.scNewList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scNewList.Location = new System.Drawing.Point(0, 0);
            this.scNewList.Name = "scNewList";
            // 
            // scNewList.Panel1
            // 
            this.scNewList.Panel1.Controls.Add(this.dgvNewList);
            // 
            // scNewList.Panel2
            // 
            this.scNewList.Panel2.Controls.Add(this.dgvTable);
            this.scNewList.Panel2.Controls.Add(this.afToolStrip4);
            this.scNewList.Size = new System.Drawing.Size(585, 340);
            this.scNewList.SplitterDistance = 314;
            this.scNewList.TabIndex = 0;
            // 
            // dgvNewList
            // 
            this.dgvNewList.AllowUserToAddRows = false;
            this.dgvNewList.AllowUserToDeleteRows = false;
            this.dgvNewList.AllowUserToResizeRows = false;
            this.dgvNewList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNewList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvNewList.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.dgvNewList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNewList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvNewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNewList.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNewList.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNewList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNewList.Enabled = false;
            this.dgvNewList.Location = new System.Drawing.Point(0, 0);
            this.dgvNewList.MultiSelect = false;
            this.dgvNewList.Name = "dgvNewList";
            this.dgvNewList.ReadOnly = true;
            this.dgvNewList.RowHeadersVisible = false;
            this.dgvNewList.RowTemplate.Height = 55;
            this.dgvNewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNewList.ShowEditingIcon = false;
            this.dgvNewList.ShowRowErrors = false;
            this.dgvNewList.Size = new System.Drawing.Size(314, 340);
            this.dgvNewList.TabIndex = 139;
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AllowUserToDeleteRows = false;
            this.dgvTable.AllowUserToResizeRows = false;
            this.dgvTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvTable.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.dgvTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTable.Enabled = false;
            this.dgvTable.Location = new System.Drawing.Point(24, 0);
            this.dgvTable.MultiSelect = false;
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowHeadersVisible = false;
            this.dgvTable.RowTemplate.Height = 55;
            this.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTable.ShowEditingIcon = false;
            this.dgvTable.ShowRowErrors = false;
            this.dgvTable.Size = new System.Drawing.Size(243, 340);
            this.dgvTable.TabIndex = 138;
            // 
            // afToolStrip4
            // 
            this.afToolStrip4.Dock = System.Windows.Forms.DockStyle.Left;
            this.afToolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAllToList,
            this.tsbtnSelectedToList,
            this.tsbtnDelSelectedFromList,
            this.tsbtnDelAllFromList});
            this.afToolStrip4.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip4.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip4.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip4.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip4.myUnderlined = true;
            this.afToolStrip4.Name = "afToolStrip4";
            this.afToolStrip4.Size = new System.Drawing.Size(24, 340);
            this.afToolStrip4.TabIndex = 140;
            this.afToolStrip4.Text = "afToolStrip4";
            // 
            // tsbtnAllToList
            // 
            this.tsbtnAllToList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAllToList.Image = global::Sped4.Properties.Resources.navigate_left2;
            this.tsbtnAllToList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAllToList.Name = "tsbtnAllToList";
            this.tsbtnAllToList.Size = new System.Drawing.Size(21, 20);
            this.tsbtnAllToList.Text = "Lager schliessen";
            this.tsbtnAllToList.Click += new System.EventHandler(this.tsbtnAllToList_Click);
            // 
            // tsbtnSelectedToList
            // 
            this.tsbtnSelectedToList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSelectedToList.Image = global::Sped4.Properties.Resources.navigate_left;
            this.tsbtnSelectedToList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSelectedToList.Name = "tsbtnSelectedToList";
            this.tsbtnSelectedToList.Size = new System.Drawing.Size(21, 20);
            this.tsbtnSelectedToList.Text = "toolStripButton1";
            this.tsbtnSelectedToList.Click += new System.EventHandler(this.tsbtnSelectedArtToAusgang_Click);
            // 
            // tsbtnDelSelectedFromList
            // 
            this.tsbtnDelSelectedFromList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelSelectedFromList.Image = global::Sped4.Properties.Resources.navigate_right;
            this.tsbtnDelSelectedFromList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelSelectedFromList.Name = "tsbtnDelSelectedFromList";
            this.tsbtnDelSelectedFromList.Size = new System.Drawing.Size(21, 20);
            this.tsbtnDelSelectedFromList.Text = "toolStripButton2";
            this.tsbtnDelSelectedFromList.Click += new System.EventHandler(this.tsbtnDelSelectedFromList_Click);
            // 
            // tsbtnDelAllFromList
            // 
            this.tsbtnDelAllFromList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelAllFromList.Image = global::Sped4.Properties.Resources.navigate_right2;
            this.tsbtnDelAllFromList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelAllFromList.Name = "tsbtnDelAllFromList";
            this.tsbtnDelAllFromList.Size = new System.Drawing.Size(21, 20);
            this.tsbtnDelAllFromList.Text = "toolStripButton3";
            this.tsbtnDelAllFromList.Click += new System.EventHandler(this.tsbtnDelAllFromList_Click);
            // 
            // dgvUserList
            // 
            this.dgvUserList.AllowUserToAddRows = false;
            this.dgvUserList.AllowUserToDeleteRows = false;
            this.dgvUserList.AllowUserToResizeRows = false;
            this.dgvUserList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUserList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvUserList.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.dgvUserList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUserList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserList.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUserList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserList.Location = new System.Drawing.Point(0, 0);
            this.dgvUserList.MultiSelect = false;
            this.dgvUserList.Name = "dgvUserList";
            this.dgvUserList.RowHeadersVisible = false;
            this.dgvUserList.RowTemplate.Height = 55;
            this.dgvUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserList.ShowEditingIcon = false;
            this.dgvUserList.ShowRowErrors = false;
            this.dgvUserList.Size = new System.Drawing.Size(275, 340);
            this.dgvUserList.TabIndex = 139;
            this.dgvUserList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserList_CellDoubleClick);
            // 
            // tsArtikelGrid
            // 
            this.tsArtikelGrid.Location = new System.Drawing.Point(0, 187);
            this.tsArtikelGrid.myColorFrom = System.Drawing.Color.Azure;
            this.tsArtikelGrid.myColorTo = System.Drawing.Color.Blue;
            this.tsArtikelGrid.myUnderlineColor = System.Drawing.Color.White;
            this.tsArtikelGrid.myUnderlined = true;
            this.tsArtikelGrid.Name = "tsArtikelGrid";
            this.tsArtikelGrid.Size = new System.Drawing.Size(864, 25);
            this.tsArtikelGrid.TabIndex = 137;
            this.tsArtikelGrid.Text = "afToolStrip3";
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.BackColor = System.Drawing.Color.White;
            this.afMinMaxPanel1.Controls.Add(this.afToolStrip1);
            this.afMinMaxPanel1.Controls.Add(this.gbListePref);
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 28);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Eingangsdaten";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(864, 159);
            this.afMinMaxPanel1.TabIndex = 134;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNewList,
            this.tsbtnSave,
            this.tsbtnDelete,
            this.tsbtnClose,
            this.toolStripSeparator1,
            this.tsbtnIntegrate,
            this.tsbtnSeparate});
            this.afToolStrip1.Location = new System.Drawing.Point(3, 28);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(156, 25);
            this.afToolStrip1.TabIndex = 8;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnNewList
            // 
            this.tsbtnNewList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNewList.Image = global::Sped4.Properties.Resources.note_add_24x24;
            this.tsbtnNewList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNewList.Name = "tsbtnNewList";
            this.tsbtnNewList.Size = new System.Drawing.Size(23, 22);
            this.tsbtnNewList.Text = "Neue Userliste anlegen";
            this.tsbtnNewList.Click += new System.EventHandler(this.tsbtnNewList_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDelete.Text = "Userliste löschen";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Lager schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnIntegrate
            // 
            this.tsbtnIntegrate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnIntegrate.Image = global::Sped4.Properties.Resources.window_split_hor;
            this.tsbtnIntegrate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnIntegrate.Name = "tsbtnIntegrate";
            this.tsbtnIntegrate.Size = new System.Drawing.Size(23, 22);
            this.tsbtnIntegrate.Text = "Lagereingang im Hauptfenster ingetegrieren";
            // 
            // tsbtnSeparate
            // 
            this.tsbtnSeparate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSeparate.Image = global::Sped4.Properties.Resources.windows;
            this.tsbtnSeparate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSeparate.Name = "tsbtnSeparate";
            this.tsbtnSeparate.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSeparate.Text = "Lagereingang in eigenem Fenster anzeigen";
            // 
            // gbListePref
            // 
            this.gbListePref.Controls.Add(this.cbListe);
            this.gbListePref.Controls.Add(this.label31);
            this.gbListePref.Controls.Add(this.cbPrivate);
            this.gbListePref.Controls.Add(this.lAktion);
            this.gbListePref.Controls.Add(this.tbBezeichnung);
            this.gbListePref.Enabled = false;
            this.gbListePref.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbListePref.Location = new System.Drawing.Point(9, 58);
            this.gbListePref.Name = "gbListePref";
            this.gbListePref.Size = new System.Drawing.Size(567, 86);
            this.gbListePref.TabIndex = 134;
            this.gbListePref.TabStop = false;
            this.gbListePref.Text = "Lagereingangsdaten";
            // 
            // cbListe
            // 
            this.cbListe.AllowDrop = true;
            this.cbListe.Enabled = false;
            this.cbListe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbListe.FormattingEnabled = true;
            this.cbListe.Items.AddRange(new object[] {
            "Bestandsliste",
            "Journal"});
            this.cbListe.Location = new System.Drawing.Point(149, 46);
            this.cbListe.Name = "cbListe";
            this.cbListe.Size = new System.Drawing.Size(188, 21);
            this.cbListe.TabIndex = 145;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.DarkBlue;
            this.label31.Location = new System.Drawing.Point(17, 26);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 13);
            this.label31.TabIndex = 144;
            this.label31.Text = "Bezeichnung:";
            // 
            // cbPrivate
            // 
            this.cbPrivate.AutoSize = true;
            this.cbPrivate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cbPrivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPrivate.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbPrivate.Location = new System.Drawing.Point(348, 24);
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.Size = new System.Drawing.Size(49, 17);
            this.cbPrivate.TabIndex = 140;
            this.cbPrivate.Text = "privat";
            this.cbPrivate.UseVisualStyleBackColor = true;
            // 
            // lAktion
            // 
            this.lAktion.AutoSize = true;
            this.lAktion.ForeColor = System.Drawing.Color.DarkBlue;
            this.lAktion.Location = new System.Drawing.Point(17, 49);
            this.lAktion.Name = "lAktion";
            this.lAktion.Size = new System.Drawing.Size(47, 13);
            this.lAktion.TabIndex = 143;
            this.lAktion.Text = "für Liste:";
            // 
            // tbBezeichnung
            // 
            this.tbBezeichnung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBezeichnung.Location = new System.Drawing.Point(149, 24);
            this.tbBezeichnung.Name = "tbBezeichnung";
            this.tbBezeichnung.Size = new System.Drawing.Size(188, 20);
            this.tbBezeichnung.TabIndex = 2;
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
            this.afColorLabel1.myText = "User - Listen";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(864, 28);
            this.afColorLabel1.TabIndex = 5;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scListen);
            this.Controls.Add(this.tsArtikelGrid);
            this.Controls.Add(this.afMinMaxPanel1);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrUserList";
            this.Size = new System.Drawing.Size(864, 552);
            this.Load += new System.EventHandler(this.ctrUserList_Load);
            this.scListen.Panel1.ResumeLayout(false);
            this.scListen.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scListen)).EndInit();
            this.scListen.ResumeLayout(false);
            this.scNewList.Panel1.ResumeLayout(false);
            this.scNewList.Panel2.ResumeLayout(false);
            this.scNewList.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scNewList)).EndInit();
            this.scNewList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.afToolStrip4.ResumeLayout(false);
            this.afToolStrip4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).EndInit();
            this.afMinMaxPanel1.ResumeLayout(false);
            this.afMinMaxPanel1.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.gbListePref.ResumeLayout(false);
            this.gbListePref.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Controls.AFMinMaxPanel afMinMaxPanel1;
        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnNewList;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnIntegrate;
        private System.Windows.Forms.ToolStripButton tsbtnSeparate;
        private System.Windows.Forms.GroupBox gbListePref;
        private System.Windows.Forms.TextBox tbBezeichnung;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lAktion;
        private Controls.AFToolStrip tsArtikelGrid;
        private System.Windows.Forms.SplitContainer scListen;
        private System.Windows.Forms.CheckBox cbPrivate;
        private System.Windows.Forms.SplitContainer scNewList;
        private Controls.AFGrid dgvTable;
        private System.Windows.Forms.ComboBox cbListe;
        private Controls.AFGrid dgvUserList;
        private Controls.AFGrid dgvNewList;
        private Controls.AFToolStrip afToolStrip4;
        private System.Windows.Forms.ToolStripButton tsbtnAllToList;
        private System.Windows.Forms.ToolStripButton tsbtnSelectedToList;
        private System.Windows.Forms.ToolStripButton tsbtnDelSelectedFromList;
        private System.Windows.Forms.ToolStripButton tsbtnDelAllFromList;
    }
}
