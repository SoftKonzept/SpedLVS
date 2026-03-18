namespace Sped4
{
    partial class ctrFahrzeug_List
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrFahrzeug_List));
            this.panFahrzeuge = new System.Windows.Forms.Panel();
            this.grdList = new Sped4.Controls.AFGrid();
            this.minMaxFahrzeugSearch = new Sped4.Controls.AFMinMaxPanel();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbNeu = new System.Windows.Forms.ToolStripButton();
            this.tsbChange = new System.Windows.Forms.ToolStripButton();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.fahrzeuglisteAktuellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fahrzeuglisteKomplettToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fahrzeuglisteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAnpassen = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lSSuchbegriff = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cbBesitzer = new System.Windows.Forms.ComboBox();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miNeu = new System.Windows.Forms.ToolStripMenuItem();
            this.miUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miListClose = new System.Windows.Forms.ToolStripMenuItem();
            this.panFahrzeuge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.minMaxFahrzeugSearch.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panFahrzeuge
            // 
            this.panFahrzeuge.BackColor = System.Drawing.Color.White;
            this.panFahrzeuge.Controls.Add(this.grdList);
            this.panFahrzeuge.Controls.Add(this.minMaxFahrzeugSearch);
            this.panFahrzeuge.Controls.Add(this.afColorLabel1);
            this.panFahrzeuge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFahrzeuge.Location = new System.Drawing.Point(0, 0);
            this.panFahrzeuge.Name = "panFahrzeuge";
            this.panFahrzeuge.Size = new System.Drawing.Size(324, 619);
            this.panFahrzeuge.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToResizeRows = false;
            this.grdList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.grdList.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.grdList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 155);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grdList.RowTemplate.Height = 55;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.ShowEditingIcon = false;
            this.grdList.ShowRowErrors = false;
            this.grdList.Size = new System.Drawing.Size(324, 464);
            this.grdList.TabIndex = 7;
            this.grdList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdList_CellFormatting);
            this.grdList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grdADRList_MouseClick);
            this.grdList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdList_MouseDoubleClick);
            // 
            // minMaxFahrzeugSearch
            // 
            this.minMaxFahrzeugSearch.BackColor = System.Drawing.Color.Transparent;
            this.minMaxFahrzeugSearch.Controls.Add(this.afToolStrip1);
            this.minMaxFahrzeugSearch.Controls.Add(this.label1);
            this.minMaxFahrzeugSearch.Controls.Add(this.lSSuchbegriff);
            this.minMaxFahrzeugSearch.Controls.Add(this.txtSearch);
            this.minMaxFahrzeugSearch.Controls.Add(this.cbBesitzer);
            this.minMaxFahrzeugSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.minMaxFahrzeugSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.minMaxFahrzeugSearch.Location = new System.Drawing.Point(0, 28);
            this.minMaxFahrzeugSearch.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.minMaxFahrzeugSearch.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxFahrzeugSearch.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.minMaxFahrzeugSearch.myText = "Optionen";
            this.minMaxFahrzeugSearch.Name = "minMaxFahrzeugSearch";
            this.minMaxFahrzeugSearch.Size = new System.Drawing.Size(324, 127);
            this.minMaxFahrzeugSearch.TabIndex = 6;
            this.minMaxFahrzeugSearch.Text = "afMinMaxPanel1";
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNeu,
            this.tsbChange,
            this.tsbExcel,
            this.toolStripButton1,
            this.toolStripDropDownButton1,
            this.toolStripButton2,
            this.tsbtnAnpassen,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(3, 24);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(202, 25);
            this.afToolStrip1.TabIndex = 9;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbNeu
            // 
            this.tsbNeu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNeu.Image = global::Sped4.Properties.Resources.form_green_add;
            this.tsbNeu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNeu.Name = "tsbNeu";
            this.tsbNeu.Size = new System.Drawing.Size(23, 22);
            this.tsbNeu.Text = "Neues Fahrzeug";
            this.tsbNeu.Click += new System.EventHandler(this.miNeu_Click);
            // 
            // tsbChange
            // 
            this.tsbChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChange.Image = global::Sped4.Properties.Resources.form_green_edit;
            this.tsbChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChange.Name = "tsbChange";
            this.tsbChange.Size = new System.Drawing.Size(23, 22);
            this.tsbChange.Text = "Fahrzeugdaten ändern";
            this.tsbChange.Click += new System.EventHandler(this.miUpdate_Click);
            // 
            // tsbExcel
            // 
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.Image = ((System.Drawing.Image)(resources.GetObject("tsbExcel.Image")));
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 22);
            this.tsbExcel.Text = "Excel Export";
            this.tsbExcel.Click += new System.EventHandler(this.miExportExcel_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Sped4.Properties.Resources.refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "aktualisieren";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fahrzeuglisteAktuellToolStripMenuItem,
            this.fahrzeuglisteKomplettToolStripMenuItem,
            this.fahrzeuglisteToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "Listen";
            // 
            // fahrzeuglisteAktuellToolStripMenuItem
            // 
            this.fahrzeuglisteAktuellToolStripMenuItem.Name = "fahrzeuglisteAktuellToolStripMenuItem";
            this.fahrzeuglisteAktuellToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.fahrzeuglisteAktuellToolStripMenuItem.Text = "Fahrzeugliste - aktuell - sortiert nach interner ID";
            this.fahrzeuglisteAktuellToolStripMenuItem.Click += new System.EventHandler(this.fahrzeuglisteAktuellToolStripMenuItem_Click);
            // 
            // fahrzeuglisteKomplettToolStripMenuItem
            // 
            this.fahrzeuglisteKomplettToolStripMenuItem.Name = "fahrzeuglisteKomplettToolStripMenuItem";
            this.fahrzeuglisteKomplettToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.fahrzeuglisteKomplettToolStripMenuItem.Text = "Fahrzeugliste - aktuell - sortiert nach KFZ";
            this.fahrzeuglisteKomplettToolStripMenuItem.Click += new System.EventHandler(this.fahrzeuglisteKomplettToolStripMenuItem_Click);
            // 
            // fahrzeuglisteToolStripMenuItem
            // 
            this.fahrzeuglisteToolStripMenuItem.Name = "fahrzeuglisteToolStripMenuItem";
            this.fahrzeuglisteToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.fahrzeuglisteToolStripMenuItem.Text = "Fahrzeugliste -  alle - soriert nach KFZ";
            this.fahrzeuglisteToolStripMenuItem.Click += new System.EventHandler(this.fahrzeuglisteToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Fahrzeug löschen";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // tsbtnAnpassen
            // 
            this.tsbtnAnpassen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAnpassen.Image = global::Sped4.Properties.Resources.PrintSetupHS;
            this.tsbtnAnpassen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAnpassen.Name = "tsbtnAnpassen";
            this.tsbtnAnpassen.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAnpassen.Text = "Fensterbreite optimieren";
            this.tsbtnAnpassen.Click += new System.EventHandler(this.tsbtnAnpassen_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "schliessen";
            this.tsbClose.Click += new System.EventHandler(this.miListClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Besitzer";
            // 
            // lSSuchbegriff
            // 
            this.lSSuchbegriff.AutoSize = true;
            this.lSSuchbegriff.Location = new System.Drawing.Point(16, 102);
            this.lSSuchbegriff.Name = "lSSuchbegriff";
            this.lSSuchbegriff.Size = new System.Drawing.Size(61, 13);
            this.lSSuchbegriff.TabIndex = 2;
            this.lSSuchbegriff.Text = "Suchbegriff";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(99, 99);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(182, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cbBesitzer
            // 
            this.cbBesitzer.AllowDrop = true;
            this.cbBesitzer.Enabled = false;
            this.cbBesitzer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBesitzer.FormattingEnabled = true;
            this.cbBesitzer.Location = new System.Drawing.Point(99, 60);
            this.cbBesitzer.Name = "cbBesitzer";
            this.cbBesitzer.Size = new System.Drawing.Size(146, 21);
            this.cbBesitzer.TabIndex = 35;
            this.cbBesitzer.SelectedIndexChanged += new System.EventHandler(this.cbBesitzer_SelectedIndexChanged);
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
            this.afColorLabel1.myText = "Fahrzeugliste [aktuell]";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(324, 28);
            this.afColorLabel1.TabIndex = 5;
            this.afColorLabel1.Text = "afColorLabel1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNeu,
            this.miUpdate,
            this.miExportExcel,
            this.miListClose});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(193, 92);
            // 
            // miNeu
            // 
            this.miNeu.Name = "miNeu";
            this.miNeu.Size = new System.Drawing.Size(192, 22);
            this.miNeu.Text = "Fahrzeug hinzufügen";
            this.miNeu.Click += new System.EventHandler(this.miNeu_Click);
            // 
            // miUpdate
            // 
            this.miUpdate.Name = "miUpdate";
            this.miUpdate.Size = new System.Drawing.Size(192, 22);
            this.miUpdate.Text = "Fahrzeugdaten ändern";
            this.miUpdate.Click += new System.EventHandler(this.miUpdate_Click);
            // 
            // miExportExcel
            // 
            this.miExportExcel.Name = "miExportExcel";
            this.miExportExcel.Size = new System.Drawing.Size(192, 22);
            this.miExportExcel.Text = "Export nach Excel";
            this.miExportExcel.Click += new System.EventHandler(this.miExportExcel_Click);
            // 
            // miListClose
            // 
            this.miListClose.Name = "miListClose";
            this.miListClose.Size = new System.Drawing.Size(192, 22);
            this.miListClose.Text = "Liste schliessen";
            this.miListClose.Click += new System.EventHandler(this.miListClose_Click);
            // 
            // ctrFahrzeug_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panFahrzeuge);
            this.Name = "ctrFahrzeug_List";
            this.Size = new System.Drawing.Size(324, 619);
            this.Load += new System.EventHandler(this.ctrFahrzeug_List_Load);
            this.panFahrzeuge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.minMaxFahrzeugSearch.ResumeLayout(false);
            this.minMaxFahrzeugSearch.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panFahrzeuge;
        private Sped4.Controls.AFColorLabel afColorLabel1;
        private Sped4.Controls.AFMinMaxPanel minMaxFahrzeugSearch;
        private Sped4.Controls.AFGrid grdList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miUpdate;
        private System.Windows.Forms.ToolStripMenuItem miNeu;
        private System.Windows.Forms.ToolStripMenuItem miListClose;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lSSuchbegriff;
        private System.Windows.Forms.ToolStripMenuItem miExportExcel;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNeu;
        private System.Windows.Forms.ToolStripButton tsbChange;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton tsbExcel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem fahrzeuglisteAktuellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fahrzeuglisteKomplettToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ComboBox cbBesitzer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem fahrzeuglisteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbtnAnpassen;
       
    }
}
