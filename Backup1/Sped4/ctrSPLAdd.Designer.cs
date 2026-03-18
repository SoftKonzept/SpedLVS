namespace Sped4
{
    partial class ctrSPLAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrSPLAdd));
            this.afCLEingang = new Sped4.Controls.AFColorLabel();
            this.tsArtikeldatenMenu = new Sped4.Controls.AFToolStrip();
            this.tsbtnClearCtr = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPrintSPLLabel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPrintSPLDoc = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMail = new System.Windows.Forms.ToolStripButton();
            this.tbSperrgrund = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbSperrdatum = new System.Windows.Forms.TextBox();
            this.scDaten = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.tbVermerk = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudDefWindungen = new System.Windows.Forms.NumericUpDown();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.dgvSchaden = new Telerik.WinControls.UI.RadGridView();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tsArtikeldatenMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDaten)).BeginInit();
            this.scDaten.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefWindungen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchaden)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchaden.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // afCLEingang
            // 
            this.afCLEingang.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afCLEingang.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afCLEingang.Dock = System.Windows.Forms.DockStyle.Top;
            this.afCLEingang.Location = new System.Drawing.Point(0, 0);
            this.afCLEingang.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afCLEingang.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afCLEingang.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afCLEingang.myText = "Sperrlagerdaten für SPL Dokumente";
            this.afCLEingang.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afCLEingang.myUnderlined = true;
            this.afCLEingang.Name = "afCLEingang";
            this.afCLEingang.Size = new System.Drawing.Size(632, 28);
            this.afCLEingang.TabIndex = 5;
            this.afCLEingang.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tsArtikeldatenMenu
            // 
            this.tsArtikeldatenMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClearCtr,
            this.tsbtnPrintSPLLabel,
            this.tsbtnPrintSPLDoc,
            this.tsbtnClose,
            this.tsbtnMail});
            this.tsArtikeldatenMenu.Location = new System.Drawing.Point(0, 28);
            this.tsArtikeldatenMenu.myColorFrom = System.Drawing.Color.Azure;
            this.tsArtikeldatenMenu.myColorTo = System.Drawing.Color.Blue;
            this.tsArtikeldatenMenu.myUnderlineColor = System.Drawing.Color.White;
            this.tsArtikeldatenMenu.myUnderlined = true;
            this.tsArtikeldatenMenu.Name = "tsArtikeldatenMenu";
            this.tsArtikeldatenMenu.Size = new System.Drawing.Size(632, 25);
            this.tsArtikeldatenMenu.TabIndex = 137;
            this.tsArtikeldatenMenu.Text = "afToolStrip3";
            // 
            // tsbtnClearCtr
            // 
            this.tsbtnClearCtr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClearCtr.Image = global::Sped4.Properties.Resources.code_delete_24x24;
            this.tsbtnClearCtr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClearCtr.Name = "tsbtnClearCtr";
            this.tsbtnClearCtr.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClearCtr.Text = "Eingabefelder leeren";
            this.tsbtnClearCtr.Click += new System.EventHandler(this.tsbtnClearCtr_Click);
            // 
            // tsbtnPrintSPLLabel
            // 
            this.tsbtnPrintSPLLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrintSPLLabel.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrintSPLLabel.Image")));
            this.tsbtnPrintSPLLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrintSPLLabel.Name = "tsbtnPrintSPLLabel";
            this.tsbtnPrintSPLLabel.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPrintSPLLabel.Tag = "PrintSPL";
            this.tsbtnPrintSPLLabel.Text = "Sperrlagerkarte drucken";
            this.tsbtnPrintSPLLabel.Click += new System.EventHandler(this.tsbtnPrintSPL_Click);
            // 
            // tsbtnPrintSPLDoc
            // 
            this.tsbtnPrintSPLDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrintSPLDoc.Image = global::Sped4.Properties.Resources.printer2_24x24;
            this.tsbtnPrintSPLDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrintSPLDoc.Name = "tsbtnPrintSPLDoc";
            this.tsbtnPrintSPLDoc.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPrintSPLDoc.Tag = "SPLDocPrint";
            this.tsbtnPrintSPLDoc.Text = "Sperrlagerdokument / Sperrlagermeldung drucken";
            this.tsbtnPrintSPLDoc.Click += new System.EventHandler(this.tsbtnSPLDocPrint_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClose.Image")));
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Fenster schließen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // tsbtnMail
            // 
            this.tsbtnMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnMail.Image = global::Sped4.Properties.Resources.mail_forward_24x24;
            this.tsbtnMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMail.Name = "tsbtnMail";
            this.tsbtnMail.Size = new System.Drawing.Size(23, 22);
            this.tsbtnMail.Text = "SPL-Meldung per Mail versenden";
            this.tsbtnMail.Click += new System.EventHandler(this.tsbtnMail_Click);
            // 
            // tbSperrgrund
            // 
            this.tbSperrgrund.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSperrgrund.Location = new System.Drawing.Point(18, 94);
            this.tbSperrgrund.Multiline = true;
            this.tbSperrgrund.Name = "tbSperrgrund";
            this.tbSperrgrund.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSperrgrund.Size = new System.Drawing.Size(274, 192);
            this.tbSperrgrund.TabIndex = 100;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.DarkBlue;
            this.label34.Location = new System.Drawing.Point(16, 78);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(69, 13);
            this.label34.TabIndex = 190;
            this.label34.Text = "Sperrgrund:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.DarkBlue;
            this.label20.Location = new System.Drawing.Point(16, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(134, 13);
            this.label20.TabIndex = 187;
            this.label20.Text = "Anzahl betr. Windungen";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.DarkBlue;
            this.label13.Location = new System.Drawing.Point(16, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 186;
            this.label13.Text = "Sperrdatum:";
            // 
            // tbSperrdatum
            // 
            this.tbSperrdatum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSperrdatum.Location = new System.Drawing.Point(92, 17);
            this.tbSperrdatum.Name = "tbSperrdatum";
            this.tbSperrdatum.Size = new System.Drawing.Size(202, 20);
            this.tbSperrdatum.TabIndex = 101;
            // 
            // scDaten
            // 
            this.scDaten.Controls.Add(this.splitPanel1);
            this.scDaten.Controls.Add(this.splitPanel2);
            this.scDaten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDaten.Location = new System.Drawing.Point(0, 53);
            this.scDaten.Name = "scDaten";
            // 
            // 
            // 
            this.scDaten.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scDaten.Size = new System.Drawing.Size(632, 513);
            this.scDaten.SplitterWidth = 4;
            this.scDaten.TabIndex = 139;
            this.scDaten.TabStop = false;
            this.scDaten.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.AutoScroll = true;
            this.splitPanel1.BackColor = System.Drawing.Color.White;
            this.splitPanel1.Controls.Add(this.tbVermerk);
            this.splitPanel1.Controls.Add(this.label1);
            this.splitPanel1.Controls.Add(this.nudDefWindungen);
            this.splitPanel1.Controls.Add(this.tbSperrgrund);
            this.splitPanel1.Controls.Add(this.label34);
            this.splitPanel1.Controls.Add(this.tbSperrdatum);
            this.splitPanel1.Controls.Add(this.label20);
            this.splitPanel1.Controls.Add(this.label13);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(301, 513);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.02070063F, 0F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-21, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // tbVermerk
            // 
            this.tbVermerk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVermerk.Location = new System.Drawing.Point(18, 305);
            this.tbVermerk.Multiline = true;
            this.tbVermerk.Name = "tbVermerk";
            this.tbVermerk.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbVermerk.Size = new System.Drawing.Size(273, 175);
            this.tbVermerk.TabIndex = 192;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(16, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 191;
            this.label1.Text = "Vermerk:";
            // 
            // nudDefWindungen
            // 
            this.nudDefWindungen.Location = new System.Drawing.Point(194, 49);
            this.nudDefWindungen.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDefWindungen.Name = "nudDefWindungen";
            this.nudDefWindungen.Size = new System.Drawing.Size(89, 20);
            this.nudDefWindungen.TabIndex = 102;
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.Color.White;
            this.splitPanel2.Controls.Add(this.dgvSchaden);
            this.splitPanel2.Location = new System.Drawing.Point(305, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(327, 513);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.02070063F, 0F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(21, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // dgvSchaden
            // 
            this.dgvSchaden.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvSchaden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchaden.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSchaden.Location = new System.Drawing.Point(0, 0);
            // 
            // dgvSchaden
            // 
            this.dgvSchaden.MasterTemplate.AllowAddNewRow = false;
            this.dgvSchaden.MasterTemplate.ShowFilteringRow = false;
            this.dgvSchaden.Name = "dgvSchaden";
            this.dgvSchaden.ReadOnly = true;
            // 
            // 
            // 
            this.dgvSchaden.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.dgvSchaden.ShowGroupPanel = false;
            this.dgvSchaden.Size = new System.Drawing.Size(327, 513);
            this.dgvSchaden.TabIndex = 141;
            this.dgvSchaden.Text = "radGridView1";
            this.dgvSchaden.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvSchaden_CellClick);
            // 
            // ctrSPLAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scDaten);
            this.Controls.Add(this.tsArtikeldatenMenu);
            this.Controls.Add(this.afCLEingang);
            this.Name = "ctrSPLAdd";
            this.Size = new System.Drawing.Size(632, 566);
            this.Load += new System.EventHandler(this.ctrSPLAdd_Load);
            this.tsArtikeldatenMenu.ResumeLayout(false);
            this.tsArtikeldatenMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDaten)).EndInit();
            this.scDaten.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefWindungen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchaden.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchaden)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFColorLabel afCLEingang;
        private Controls.AFToolStrip tsArtikeldatenMenu;
        private System.Windows.Forms.ToolStripButton tsbtnClearCtr;
        private System.Windows.Forms.TextBox tbSperrgrund;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbSperrdatum;
        private Telerik.WinControls.UI.RadSplitContainer scDaten;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private System.Windows.Forms.NumericUpDown nudDefWindungen;
        private Telerik.WinControls.UI.RadGridView dgvSchaden;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.TextBox tbVermerk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbtnMail;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        public System.Windows.Forms.ToolStripButton tsbtnPrintSPLLabel;
        public System.Windows.Forms.ToolStripButton tsbtnPrintSPLDoc;
    }
}
