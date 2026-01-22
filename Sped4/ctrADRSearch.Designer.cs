namespace Sped4
{
    partial class ctrADRSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrADRSearch));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.minMaxADRSearch = new Sped4.Controls.AFMinMaxPanel();
            this.tbAdrInfo = new System.Windows.Forms.TextBox();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.miListeKomplett = new System.Windows.Forms.ToolStripMenuItem();
            this.adresslisteaktivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adresslistepassivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miListeKunde = new System.Windows.Forms.ToolStripMenuItem();
            this.miListeVersender = new System.Windows.Forms.ToolStripMenuItem();
            this.miListeBelade = new System.Windows.Forms.ToolStripMenuItem();
            this.miListeEntlade = new System.Windows.Forms.ToolStripMenuItem();
            this.miListePost = new System.Windows.Forms.ToolStripMenuItem();
            this.miListeRechnung = new System.Windows.Forms.ToolStripMenuItem();
            this.miListeSpedition = new System.Windows.Forms.ToolStripMenuItem();
            this.miListeDiverse = new System.Windows.Forms.ToolStripMenuItem();
            this.miClose = new System.Windows.Forms.ToolStripButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lSSuchbegriff = new System.Windows.Forms.Label();
            this.cbSearchArt = new System.Windows.Forms.CheckBox();
            this.grdADRList = new Telerik.WinControls.UI.RadGridView();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.minMaxADRSearch.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdADRList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdADRList.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // minMaxADRSearch
            // 
            this.minMaxADRSearch.BackColor = System.Drawing.Color.White;
            this.minMaxADRSearch.Controls.Add(this.tbAdrInfo);
            this.minMaxADRSearch.Controls.Add(this.afToolStrip1);
            this.minMaxADRSearch.Controls.Add(this.txtSearch);
            this.minMaxADRSearch.Controls.Add(this.lSSuchbegriff);
            this.minMaxADRSearch.Controls.Add(this.cbSearchArt);
            this.minMaxADRSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.minMaxADRSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.minMaxADRSearch.Location = new System.Drawing.Point(0, 34);
            this.minMaxADRSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.minMaxADRSearch.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.minMaxADRSearch.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxADRSearch.myImage = null;
            this.minMaxADRSearch.myText = "Optionen";
            this.minMaxADRSearch.Name = "minMaxADRSearch";
            this.minMaxADRSearch.Size = new System.Drawing.Size(603, 194);
            this.minMaxADRSearch.TabIndex = 10;
            this.minMaxADRSearch.Text = "afMinMaxPanel1";
            // 
            // tbAdrInfo
            // 
            this.tbAdrInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAdrInfo.Location = new System.Drawing.Point(4, 106);
            this.tbAdrInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbAdrInfo.Multiline = true;
            this.tbAdrInfo.Name = "tbAdrInfo";
            this.tbAdrInfo.Size = new System.Drawing.Size(588, 79);
            this.tbAdrInfo.TabIndex = 9;
            this.tbAdrInfo.Text = "Name\r\nStrasse\r\nPLZ - Ort\r\nLand";
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh,
            this.toolStripSplitButton1,
            this.miClose});
            this.afToolStrip1.Location = new System.Drawing.Point(4, 34);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(110, 27);
            this.afToolStrip1.TabIndex = 8;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefresh.Image")));
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(29, 24);
            this.tsbtnRefresh.Text = "aktualisieren";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miListeKomplett,
            this.adresslisteaktivToolStripMenuItem,
            this.adresslistepassivToolStripMenuItem,
            this.miListeKunde,
            this.miListeVersender,
            this.miListeBelade,
            this.miListeEntlade,
            this.miListePost,
            this.miListeRechnung,
            this.miListeSpedition,
            this.miListeDiverse});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(39, 24);
            this.toolStripSplitButton1.Text = "Listen";
            // 
            // miListeKomplett
            // 
            this.miListeKomplett.Name = "miListeKomplett";
            this.miListeKomplett.Size = new System.Drawing.Size(237, 26);
            this.miListeKomplett.Text = "Adressliste komplett";
            this.miListeKomplett.Click += new System.EventHandler(this.miListeKomplett_Click);
            // 
            // adresslisteaktivToolStripMenuItem
            // 
            this.adresslisteaktivToolStripMenuItem.Name = "adresslisteaktivToolStripMenuItem";
            this.adresslisteaktivToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.adresslisteaktivToolStripMenuItem.Text = "Adressliste [aktiv]";
            this.adresslisteaktivToolStripMenuItem.Click += new System.EventHandler(this.adresslisteaktivToolStripMenuItem_Click);
            // 
            // adresslistepassivToolStripMenuItem
            // 
            this.adresslistepassivToolStripMenuItem.Name = "adresslistepassivToolStripMenuItem";
            this.adresslistepassivToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.adresslistepassivToolStripMenuItem.Text = "Adressliste [passiv]";
            this.adresslistepassivToolStripMenuItem.Click += new System.EventHandler(this.adresslistepassivToolStripMenuItem_Click);
            // 
            // miListeKunde
            // 
            this.miListeKunde.Name = "miListeKunde";
            this.miListeKunde.Size = new System.Drawing.Size(237, 26);
            this.miListeKunde.Text = "Kunde / Auftraggeber";
            // 
            // miListeVersender
            // 
            this.miListeVersender.Name = "miListeVersender";
            this.miListeVersender.Size = new System.Drawing.Size(237, 26);
            this.miListeVersender.Text = "Versender / Lieferant";
            // 
            // miListeBelade
            // 
            this.miListeBelade.Name = "miListeBelade";
            this.miListeBelade.Size = new System.Drawing.Size(237, 26);
            this.miListeBelade.Text = "Beladeadresse";
            // 
            // miListeEntlade
            // 
            this.miListeEntlade.Name = "miListeEntlade";
            this.miListeEntlade.Size = new System.Drawing.Size(237, 26);
            this.miListeEntlade.Text = "Entladeadresse";
            // 
            // miListePost
            // 
            this.miListePost.Name = "miListePost";
            this.miListePost.Size = new System.Drawing.Size(237, 26);
            this.miListePost.Text = "Postadresse";
            // 
            // miListeRechnung
            // 
            this.miListeRechnung.Name = "miListeRechnung";
            this.miListeRechnung.Size = new System.Drawing.Size(237, 26);
            this.miListeRechnung.Text = "Rechnungsadresse";
            // 
            // miListeSpedition
            // 
            this.miListeSpedition.Name = "miListeSpedition";
            this.miListeSpedition.Size = new System.Drawing.Size(237, 26);
            this.miListeSpedition.Text = "Spedition";
            // 
            // miListeDiverse
            // 
            this.miListeDiverse.Name = "miListeDiverse";
            this.miListeDiverse.Size = new System.Drawing.Size(237, 26);
            this.miListeDiverse.Text = "Diverse";
            // 
            // miClose
            // 
            this.miClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.miClose.Image = ((System.Drawing.Image)(resources.GetObject("miClose.Image")));
            this.miClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miClose.Name = "miClose";
            this.miClose.Size = new System.Drawing.Size(29, 24);
            this.miClose.Text = "schliessen";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(289, 74);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(303, 22);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lSSuchbegriff
            // 
            this.lSSuchbegriff.AutoSize = true;
            this.lSSuchbegriff.Location = new System.Drawing.Point(200, 78);
            this.lSSuchbegriff.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lSSuchbegriff.Name = "lSSuchbegriff";
            this.lSSuchbegriff.Size = new System.Drawing.Size(74, 16);
            this.lSSuchbegriff.TabIndex = 2;
            this.lSSuchbegriff.Text = "Suchbegriff";
            // 
            // cbSearchArt
            // 
            this.cbSearchArt.AutoSize = true;
            this.cbSearchArt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSearchArt.Location = new System.Drawing.Point(8, 76);
            this.cbSearchArt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSearchArt.Name = "cbSearchArt";
            this.cbSearchArt.Size = new System.Drawing.Size(165, 20);
            this.cbSearchArt.TabIndex = 4;
            this.cbSearchArt.Text = "Volltextsuche aktivieren";
            this.cbSearchArt.UseVisualStyleBackColor = true;
            this.cbSearchArt.CheckedChanged += new System.EventHandler(this.cbSearchArt_CheckedChanged);
            // 
            // grdADRList
            // 
            this.grdADRList.BackColor = System.Drawing.Color.White;
            this.grdADRList.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdADRList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdADRList.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grdADRList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdADRList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdADRList.Location = new System.Drawing.Point(0, 228);
            this.grdADRList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.grdADRList.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdADRList.MasterTemplate.EnableFiltering = true;
            this.grdADRList.MasterTemplate.ShowFilteringRow = false;
            this.grdADRList.MasterTemplate.ShowHeaderCellButtons = true;
            this.grdADRList.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdADRList.Name = "grdADRList";
            this.grdADRList.ReadOnly = true;
            this.grdADRList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.grdADRList.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 186, 240, 150);
            this.grdADRList.ShowHeaderCellButtons = true;
            this.grdADRList.Size = new System.Drawing.Size(603, 416);
            this.grdADRList.TabIndex = 26;
            this.grdADRList.ThemeName = "ControlDefault";
            this.grdADRList.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdADRList_CellClick);
            this.grdADRList.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdADRList_CellDoubleClick);
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
            this.afColorLabel1.myText = "Adressliste";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(603, 34);
            this.afColorLabel1.TabIndex = 27;
            this.afColorLabel1.Text = "afColorLabel2";
            // 
            // ctrADRSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdADRList);
            this.Controls.Add(this.minMaxADRSearch);
            this.Controls.Add(this.afColorLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrADRSearch";
            this.Size = new System.Drawing.Size(603, 644);
            this.Load += new System.EventHandler(this.ctrADRSearch_Load);
            this.minMaxADRSearch.ResumeLayout(false);
            this.minMaxADRSearch.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdADRList.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdADRList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFMinMaxPanel minMaxADRSearch;
        public System.Windows.Forms.TextBox tbAdrInfo;
        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem miListeKomplett;
        private System.Windows.Forms.ToolStripMenuItem adresslisteaktivToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adresslistepassivToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miListeKunde;
        private System.Windows.Forms.ToolStripMenuItem miListeVersender;
        private System.Windows.Forms.ToolStripMenuItem miListeBelade;
        private System.Windows.Forms.ToolStripMenuItem miListeEntlade;
        private System.Windows.Forms.ToolStripMenuItem miListePost;
        private System.Windows.Forms.ToolStripMenuItem miListeRechnung;
        private System.Windows.Forms.ToolStripMenuItem miListeSpedition;
        private System.Windows.Forms.ToolStripMenuItem miListeDiverse;
        private System.Windows.Forms.ToolStripButton miClose;
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lSSuchbegriff;
        private System.Windows.Forms.CheckBox cbSearchArt;
        private Telerik.WinControls.UI.RadGridView grdADRList;
        public Controls.AFColorLabel afColorLabel1;
    }
}
