namespace Sped4
{
    partial class ctrSchaeden
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrSchaeden));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.scSchaden = new System.Windows.Forms.SplitContainer();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboArt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAutoSPL = new System.Windows.Forms.CheckBox();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.ttbListe = new System.Windows.Forms.ToolStripSplitButton();
            this.alleSchädenMängelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miActive = new System.Windows.Forms.ToolStripMenuItem();
            this.miPassive = new System.Windows.Forms.ToolStripMenuItem();
            this.miSchaden = new System.Windows.Forms.ToolStripMenuItem();
            this.miMangel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.cbActiv = new System.Windows.Forms.CheckBox();
            this.tbBeschreibung = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBezeichnung = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.tsmSchadenSelect = new Sped4.Controls.AFToolStrip();
            this.tsbtnSchadenSelect = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSchadenSelectClose = new System.Windows.Forms.ToolStripButton();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.scSchaden)).BeginInit();
            this.scSchaden.Panel1.SuspendLayout();
            this.scSchaden.Panel2.SuspendLayout();
            this.scSchaden.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.tsmSchadenSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // scSchaden
            // 
            this.scSchaden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSchaden.Location = new System.Drawing.Point(0, 34);
            this.scSchaden.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scSchaden.Name = "scSchaden";
            // 
            // scSchaden.Panel1
            // 
            this.scSchaden.Panel1.BackColor = System.Drawing.Color.White;
            this.scSchaden.Panel1.Controls.Add(this.tbCode);
            this.scSchaden.Panel1.Controls.Add(this.label4);
            this.scSchaden.Panel1.Controls.Add(this.comboArt);
            this.scSchaden.Panel1.Controls.Add(this.label1);
            this.scSchaden.Panel1.Controls.Add(this.cbAutoSPL);
            this.scSchaden.Panel1.Controls.Add(this.afToolStrip1);
            this.scSchaden.Panel1.Controls.Add(this.cbActiv);
            this.scSchaden.Panel1.Controls.Add(this.tbBeschreibung);
            this.scSchaden.Panel1.Controls.Add(this.label2);
            this.scSchaden.Panel1.Controls.Add(this.tbBezeichnung);
            this.scSchaden.Panel1.Controls.Add(this.label3);
            // 
            // scSchaden.Panel2
            // 
            this.scSchaden.Panel2.BackColor = System.Drawing.Color.White;
            this.scSchaden.Panel2.Controls.Add(this.dgv);
            this.scSchaden.Panel2.Controls.Add(this.tsmSchadenSelect);
            this.scSchaden.Size = new System.Drawing.Size(1056, 567);
            this.scSchaden.SplitterDistance = 491;
            this.scSchaden.SplitterWidth = 5;
            this.scSchaden.TabIndex = 10;
            // 
            // tbCode
            // 
            this.tbCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCode.Location = new System.Drawing.Point(125, 279);
            this.tbCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(330, 22);
            this.tbCode.TabIndex = 160;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(20, 282);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 164;
            this.label4.Text = "Code:";
            // 
            // comboArt
            // 
            this.comboArt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboArt.FormattingEnabled = true;
            this.comboArt.Location = new System.Drawing.Point(125, 246);
            this.comboArt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboArt.Name = "comboArt";
            this.comboArt.Size = new System.Drawing.Size(329, 24);
            this.comboArt.TabIndex = 159;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(20, 254);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 16);
            this.label1.TabIndex = 162;
            this.label1.Text = "Art:";
            // 
            // cbAutoSPL
            // 
            this.cbAutoSPL.AutoSize = true;
            this.cbAutoSPL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAutoSPL.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbAutoSPL.Location = new System.Drawing.Point(125, 345);
            this.cbAutoSPL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAutoSPL.Name = "cbAutoSPL";
            this.cbAutoSPL.Size = new System.Drawing.Size(156, 20);
            this.cbAutoSPL.TabIndex = 162;
            this.cbAutoSPL.Text = "auto. UB in Sperrlager";
            this.cbAutoSPL.UseVisualStyleBackColor = true;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAdd,
            this.tsbtnSave,
            this.tsbtnRefresh,
            this.tsbtnClear,
            this.ttbListe,
            this.tsbtnDelete,
            this.tsbtnClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(491, 27);
            this.afToolStrip1.TabIndex = 160;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnAdd
            // 
            this.tsbtnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAdd.Image = global::Sped4.Properties.Resources.add;
            this.tsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAdd.Name = "tsbtnAdd";
            this.tsbtnAdd.Size = new System.Drawing.Size(29, 24);
            this.tsbtnAdd.Text = "neuen Schaden anlegen";
            this.tsbtnAdd.Click += new System.EventHandler(this.tsbtnAdd_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(29, 24);
            this.tsbtnSave.Text = "Schaden speichern";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(29, 24);
            this.tsbtnRefresh.Text = "aktualisieren";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClear.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(29, 24);
            this.tsbtnClear.Text = "Eingabefelder leeren";
            this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
            // 
            // ttbListe
            // 
            this.ttbListe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ttbListe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alleSchädenMängelToolStripMenuItem,
            this.miActive,
            this.miPassive,
            this.miSchaden,
            this.miMangel});
            this.ttbListe.Image = ((System.Drawing.Image)(resources.GetObject("ttbListe.Image")));
            this.ttbListe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttbListe.Name = "ttbListe";
            this.ttbListe.Size = new System.Drawing.Size(39, 24);
            this.ttbListe.Text = "Listen";
            // 
            // alleSchädenMängelToolStripMenuItem
            // 
            this.alleSchädenMängelToolStripMenuItem.Name = "alleSchädenMängelToolStripMenuItem";
            this.alleSchädenMängelToolStripMenuItem.Size = new System.Drawing.Size(264, 26);
            this.alleSchädenMängelToolStripMenuItem.Text = "alle Schäden / Mängel";
            this.alleSchädenMängelToolStripMenuItem.Click += new System.EventHandler(this.alleSchädenMängelToolStripMenuItem_Click);
            // 
            // miActive
            // 
            this.miActive.Name = "miActive";
            this.miActive.Size = new System.Drawing.Size(264, 26);
            this.miActive.Text = "aktive Schäden / Mängel";
            this.miActive.Click += new System.EventHandler(this.miActive_Click);
            // 
            // miPassive
            // 
            this.miPassive.Name = "miPassive";
            this.miPassive.Size = new System.Drawing.Size(264, 26);
            this.miPassive.Text = "passive Schäden / Mängel";
            this.miPassive.Click += new System.EventHandler(this.miPassive_Click);
            // 
            // miSchaden
            // 
            this.miSchaden.Name = "miSchaden";
            this.miSchaden.Size = new System.Drawing.Size(264, 26);
            this.miSchaden.Text = "nur Schäden";
            this.miSchaden.Click += new System.EventHandler(this.miSchaden_Click);
            // 
            // miMangel
            // 
            this.miMangel.Name = "miMangel";
            this.miMangel.Size = new System.Drawing.Size(264, 26);
            this.miMangel.Text = "nur Mängel";
            this.miMangel.Click += new System.EventHandler(this.miMangel_Click);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(29, 24);
            this.tsbtnDelete.Text = "Schäden löschen";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(29, 24);
            this.tsbtnClose.Text = "Formular schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // cbActiv
            // 
            this.cbActiv.AutoSize = true;
            this.cbActiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbActiv.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbActiv.Location = new System.Drawing.Point(125, 316);
            this.cbActiv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbActiv.Name = "cbActiv";
            this.cbActiv.Size = new System.Drawing.Size(53, 20);
            this.cbActiv.TabIndex = 161;
            this.cbActiv.Text = "aktiv";
            this.cbActiv.UseVisualStyleBackColor = true;
            // 
            // tbBeschreibung
            // 
            this.tbBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBeschreibung.Location = new System.Drawing.Point(125, 103);
            this.tbBeschreibung.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbBeschreibung.Multiline = true;
            this.tbBeschreibung.Name = "tbBeschreibung";
            this.tbBeschreibung.Size = new System.Drawing.Size(330, 132);
            this.tbBeschreibung.TabIndex = 158;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(17, 106);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 154;
            this.label2.Text = "Beschreibung:";
            // 
            // tbBezeichnung
            // 
            this.tbBezeichnung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBezeichnung.Location = new System.Drawing.Point(125, 62);
            this.tbBezeichnung.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbBezeichnung.Name = "tbBezeichnung";
            this.tbBezeichnung.Size = new System.Drawing.Size(330, 22);
            this.tbBezeichnung.TabIndex = 157;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(20, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 155;
            this.label3.Text = "Bezeichnung:";
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 27);
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
            this.dgv.Size = new System.Drawing.Size(560, 540);
            this.dgv.TabIndex = 26;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            // 
            // tsmSchadenSelect
            // 
            this.tsmSchadenSelect.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsmSchadenSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSchadenSelect,
            this.tsbtnSchadenSelectClose});
            this.tsmSchadenSelect.Location = new System.Drawing.Point(0, 0);
            this.tsmSchadenSelect.myColorFrom = System.Drawing.Color.Azure;
            this.tsmSchadenSelect.myColorTo = System.Drawing.Color.Blue;
            this.tsmSchadenSelect.myUnderlineColor = System.Drawing.Color.White;
            this.tsmSchadenSelect.myUnderlined = true;
            this.tsmSchadenSelect.Name = "tsmSchadenSelect";
            this.tsmSchadenSelect.Size = new System.Drawing.Size(560, 27);
            this.tsmSchadenSelect.TabIndex = 161;
            this.tsmSchadenSelect.Text = "afToolStrip2";
            // 
            // tsbtnSchadenSelect
            // 
            this.tsbtnSchadenSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSchadenSelect.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.tsbtnSchadenSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSchadenSelect.Name = "tsbtnSchadenSelect";
            this.tsbtnSchadenSelect.Size = new System.Drawing.Size(29, 24);
            this.tsbtnSchadenSelect.Text = "gewählte Schäden übernehmen";
            this.tsbtnSchadenSelect.Click += new System.EventHandler(this.tsbtnSchadenSelect_Click);
            // 
            // tsbtnSchadenSelectClose
            // 
            this.tsbtnSchadenSelectClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSchadenSelectClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnSchadenSelectClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSchadenSelectClose.Name = "tsbtnSchadenSelectClose";
            this.tsbtnSchadenSelectClose.Size = new System.Drawing.Size(29, 24);
            this.tsbtnSchadenSelectClose.Text = "schliessen";
            this.tsbtnSchadenSelectClose.Click += new System.EventHandler(this.tsbtnSchadenSelectClose_Click);
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
            this.afColorLabel1.myText = "Schäden";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(1056, 34);
            this.afColorLabel1.TabIndex = 9;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrSchaeden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scSchaden);
            this.Controls.Add(this.afColorLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrSchaeden";
            this.Size = new System.Drawing.Size(1056, 601);
            this.Load += new System.EventHandler(this.ctrSchaeden_Load);
            this.scSchaden.Panel1.ResumeLayout(false);
            this.scSchaden.Panel1.PerformLayout();
            this.scSchaden.Panel2.ResumeLayout(false);
            this.scSchaden.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scSchaden)).EndInit();
            this.scSchaden.ResumeLayout(false);
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tsmSchadenSelect.ResumeLayout(false);
            this.tsmSchadenSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        public System.Windows.Forms.TextBox tbBeschreibung;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbBezeichnung;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbActiv;
        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        public System.Windows.Forms.SplitContainer scSchaden;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        public Telerik.WinControls.UI.RadGridView dgv;
        private Controls.AFToolStrip tsmSchadenSelect;
        private System.Windows.Forms.ToolStripButton tsbtnSchadenSelect;
        private System.Windows.Forms.ToolStripButton tsbtnSchadenSelectClose;
        private System.Windows.Forms.ToolStripButton tsbtnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbAutoSPL;
        private System.Windows.Forms.ComboBox comboArt;
        public System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripSplitButton ttbListe;
        private System.Windows.Forms.ToolStripMenuItem miActive;
        private System.Windows.Forms.ToolStripMenuItem miPassive;
        private System.Windows.Forms.ToolStripMenuItem miSchaden;
        private System.Windows.Forms.ToolStripMenuItem miMangel;
        private System.Windows.Forms.ToolStripMenuItem alleSchädenMängelToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;

    }
}
