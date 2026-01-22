namespace Sped4
{
    partial class frmDispo
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDispo));
            this.VehicelPanel = new System.Windows.Forms.Panel();
            this.TimePanel = new System.Windows.Forms.Panel();
            this.pbLeftVor = new System.Windows.Forms.PictureBox();
            this.pbLeftZur = new System.Windows.Forms.PictureBox();
            this.pbRightVor = new System.Windows.Forms.PictureBox();
            this.pbRightZur = new System.Windows.Forms.PictureBox();
            this.lblCaption = new Sped4.Controls.AFColorLabel();
            this.MiscPanel = new Sped4.Controls.AFMinMaxPanel();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDispoPlanIntegrieren = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDispoplanFenster = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.DispoKalender = new System.Windows.Forms.MonthCalendar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudRowHeight = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbFahrer = new System.Windows.Forms.CheckBox();
            this.cbFahrerliste = new System.Windows.Forms.CheckBox();
            this.cbAuflieger = new System.Windows.Forms.CheckBox();
            this.cbAufliegerListe = new System.Windows.Forms.CheckBox();
            this.emptySpace = new System.Windows.Forms.Panel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.TimePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftVor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftZur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightVor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightZur)).BeginInit();
            this.MiscPanel.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowHeight)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VehicelPanel
            // 
            this.VehicelPanel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.VehicelPanel.AutoScroll = true;
            this.VehicelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VehicelPanel.Location = new System.Drawing.Point(0, 277);
            this.VehicelPanel.Margin = new System.Windows.Forms.Padding(9);
            this.VehicelPanel.Name = "VehicelPanel";
            this.VehicelPanel.Size = new System.Drawing.Size(842, 197);
            this.VehicelPanel.TabIndex = 10;
            this.VehicelPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.VehicelPanel_DragDrop);
            this.VehicelPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.VehicelPanel_DragEnter);
            // 
            // TimePanel
            // 
            this.TimePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.TimePanel.Controls.Add(this.pbLeftVor);
            this.TimePanel.Controls.Add(this.pbLeftZur);
            this.TimePanel.Controls.Add(this.pbRightVor);
            this.TimePanel.Controls.Add(this.pbRightZur);
            this.TimePanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TimePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TimePanel.Location = new System.Drawing.Point(0, 232);
            this.TimePanel.Name = "TimePanel";
            this.TimePanel.Size = new System.Drawing.Size(842, 45);
            this.TimePanel.TabIndex = 9;
            this.TimePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TimePanel_Paint);
            this.TimePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TimePanel_MouseDown);
            this.TimePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TimePanel_MouseMove);
            // 
            // pbLeftVor
            // 
            this.pbLeftVor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbLeftVor.Image = ((System.Drawing.Image)(resources.GetObject("pbLeftVor.Image")));
            this.pbLeftVor.Location = new System.Drawing.Point(3, 16);
            this.pbLeftVor.Name = "pbLeftVor";
            this.pbLeftVor.Size = new System.Drawing.Size(19, 25);
            this.pbLeftVor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLeftVor.TabIndex = 12;
            this.pbLeftVor.TabStop = false;
            this.pbLeftVor.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pbLeftVor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbLeftVor_MouseDown);
            // 
            // pbLeftZur
            // 
            this.pbLeftZur.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pbLeftZur.Image = ((System.Drawing.Image)(resources.GetObject("pbLeftZur.Image")));
            this.pbLeftZur.Location = new System.Drawing.Point(22, 16);
            this.pbLeftZur.Name = "pbLeftZur";
            this.pbLeftZur.Size = new System.Drawing.Size(20, 25);
            this.pbLeftZur.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLeftZur.TabIndex = 15;
            this.pbLeftZur.TabStop = false;
            this.pbLeftZur.Click += new System.EventHandler(this.pbLeftZur_Click);
            this.pbLeftZur.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbLeftZur_MouseDown);
            // 
            // pbRightVor
            // 
            this.pbRightVor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbRightVor.Image = ((System.Drawing.Image)(resources.GetObject("pbRightVor.Image")));
            this.pbRightVor.Location = new System.Drawing.Point(820, 15);
            this.pbRightVor.Name = "pbRightVor";
            this.pbRightVor.Size = new System.Drawing.Size(20, 25);
            this.pbRightVor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRightVor.TabIndex = 13;
            this.pbRightVor.TabStop = false;
            this.pbRightVor.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pbRightVor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbRightVor_MouseDown);
            // 
            // pbRightZur
            // 
            this.pbRightZur.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pbRightZur.Image = ((System.Drawing.Image)(resources.GetObject("pbRightZur.Image")));
            this.pbRightZur.Location = new System.Drawing.Point(799, 15);
            this.pbRightZur.Name = "pbRightZur";
            this.pbRightZur.Size = new System.Drawing.Size(19, 25);
            this.pbRightZur.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbRightZur.TabIndex = 14;
            this.pbRightZur.TabStop = false;
            this.pbRightZur.Click += new System.EventHandler(this.pbRightZur_Click);
            this.pbRightZur.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbRightZur_MouseDown);
            // 
            // lblCaption
            // 
            this.lblCaption.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblCaption.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCaption.Location = new System.Drawing.Point(0, 0);
            this.lblCaption.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.lblCaption.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.lblCaption.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblCaption.myText = "Test Dispo";
            this.lblCaption.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblCaption.myUnderlined = true;
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(842, 28);
            this.lblCaption.TabIndex = 11;
            this.lblCaption.Text = "afColorLabel1";
            // 
            // MiscPanel
            // 
            this.MiscPanel.Controls.Add(this.afToolStrip1);
            this.MiscPanel.Controls.Add(this.label2);
            this.MiscPanel.Controls.Add(this.DispoKalender);
            this.MiscPanel.Controls.Add(this.groupBox2);
            this.MiscPanel.Controls.Add(this.groupBox1);
            this.MiscPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MiscPanel.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.MiscPanel.Location = new System.Drawing.Point(0, 36);
            this.MiscPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MiscPanel.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.MiscPanel.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiscPanel.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.MiscPanel.myText = "Einstellungen";
            this.MiscPanel.Name = "MiscPanel";
            this.MiscPanel.Size = new System.Drawing.Size(842, 196);
            this.MiscPanel.TabIndex = 12;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbtnDispoPlanIntegrieren,
            this.tsbtnDispoplanFenster,
            this.tsbClose,
            this.toolStripButton1});
            this.afToolStrip1.Location = new System.Drawing.Point(3, 28);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(158, 25);
            this.afToolStrip1.TabIndex = 10;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbtnDispoPlanIntegrieren
            // 
            this.tsbtnDispoPlanIntegrieren.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDispoPlanIntegrieren.Image = global::Sped4.Properties.Resources.window_split_hor;
            this.tsbtnDispoPlanIntegrieren.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDispoPlanIntegrieren.Name = "tsbtnDispoPlanIntegrieren";
            this.tsbtnDispoPlanIntegrieren.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDispoPlanIntegrieren.Text = "Dispoplan in Sped4-Fenster integrieren";
            this.tsbtnDispoPlanIntegrieren.Click += new System.EventHandler(this.tsbtnDispoPlanIntegrieren_Click);
            // 
            // tsbtnDispoplanFenster
            // 
            this.tsbtnDispoplanFenster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDispoplanFenster.Image = global::Sped4.Properties.Resources.windows;
            this.tsbtnDispoplanFenster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDispoplanFenster.Name = "tsbtnDispoplanFenster";
            this.tsbtnDispoplanFenster.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDispoplanFenster.Text = "Dispoplan in eigenem Fenster anzeigen";
            this.tsbtnDispoplanFenster.Click += new System.EventHandler(this.tsbtnDispoplanFenster_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "schliessen";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(541, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Info Schadstoffklassen anzeigen";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // DispoKalender
            // 
            this.DispoKalender.Location = new System.Drawing.Point(334, 28);
            this.DispoKalender.Margin = new System.Windows.Forms.Padding(15);
            this.DispoKalender.Name = "DispoKalender";
            this.DispoKalender.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DispoKalender.ShowWeekNumbers = true;
            this.DispoKalender.TabIndex = 13;
            this.DispoKalender.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.DispoKalender_DateChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbnOK);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nudRowHeight);
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(62, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 58);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Layouteinstellungen";
            this.groupBox2.Visible = false;
            // 
            // tbnOK
            // 
            this.tbnOK.Location = new System.Drawing.Point(169, 23);
            this.tbnOK.Name = "tbnOK";
            this.tbnOK.Size = new System.Drawing.Size(85, 23);
            this.tbnOK.TabIndex = 2;
            this.tbnOK.Text = "übernehmen";
            this.tbnOK.UseVisualStyleBackColor = true;
            this.tbnOK.Click += new System.EventHandler(this.tbnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Höhe Fahrzeugzeile";
            // 
            // nudRowHeight
            // 
            this.nudRowHeight.Location = new System.Drawing.Point(11, 19);
            this.nudRowHeight.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudRowHeight.Minimum = new decimal(new int[] {
            71,
            0,
            0,
            0});
            this.nudRowHeight.Name = "nudRowHeight";
            this.nudRowHeight.Size = new System.Drawing.Size(45, 20);
            this.nudRowHeight.TabIndex = 0;
            this.nudRowHeight.Value = new decimal(new int[] {
            71,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbFahrer);
            this.groupBox1.Controls.Add(this.cbFahrerliste);
            this.groupBox1.Controls.Add(this.cbAuflieger);
            this.groupBox1.Controls.Add(this.cbAufliegerListe);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(62, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 58);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ressourcen";
            // 
            // cbFahrer
            // 
            this.cbFahrer.AutoSize = true;
            this.cbFahrer.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbFahrer.Location = new System.Drawing.Point(6, 19);
            this.cbFahrer.Name = "cbFahrer";
            this.cbFahrer.Size = new System.Drawing.Size(102, 17);
            this.cbFahrer.TabIndex = 6;
            this.cbFahrer.Text = "Fahrer anzeigen";
            this.cbFahrer.UseVisualStyleBackColor = true;
            this.cbFahrer.CheckedChanged += new System.EventHandler(this.cbFahrer_CheckedChanged);
            // 
            // cbFahrerliste
            // 
            this.cbFahrerliste.AutoSize = true;
            this.cbFahrerliste.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbFahrerliste.Location = new System.Drawing.Point(6, 36);
            this.cbFahrerliste.Name = "cbFahrerliste";
            this.cbFahrerliste.Size = new System.Drawing.Size(120, 17);
            this.cbFahrerliste.TabIndex = 9;
            this.cbFahrerliste.Text = "Fahrerliste anzeigen";
            this.cbFahrerliste.UseVisualStyleBackColor = true;
            this.cbFahrerliste.CheckedChanged += new System.EventHandler(this.cbFahrerliste_CheckedChanged);
            // 
            // cbAuflieger
            // 
            this.cbAuflieger.AutoSize = true;
            this.cbAuflieger.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbAuflieger.Location = new System.Drawing.Point(132, 19);
            this.cbAuflieger.Name = "cbAuflieger";
            this.cbAuflieger.Size = new System.Drawing.Size(113, 17);
            this.cbAuflieger.TabIndex = 2;
            this.cbAuflieger.Text = "Auflieger anzeigen";
            this.cbAuflieger.UseVisualStyleBackColor = true;
            this.cbAuflieger.CheckedChanged += new System.EventHandler(this.cbAuflieger_CheckedChanged);
            // 
            // cbAufliegerListe
            // 
            this.cbAufliegerListe.AutoSize = true;
            this.cbAufliegerListe.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbAufliegerListe.Location = new System.Drawing.Point(132, 37);
            this.cbAufliegerListe.Name = "cbAufliegerListe";
            this.cbAufliegerListe.Size = new System.Drawing.Size(131, 17);
            this.cbAufliegerListe.TabIndex = 8;
            this.cbAufliegerListe.Text = "Aufliegerliste anzeigen";
            this.cbAufliegerListe.UseVisualStyleBackColor = true;
            this.cbAufliegerListe.CheckedChanged += new System.EventHandler(this.cbAufliegerListe_CheckedChanged);
            // 
            // emptySpace
            // 
            this.emptySpace.Dock = System.Windows.Forms.DockStyle.Top;
            this.emptySpace.Location = new System.Drawing.Point(0, 28);
            this.emptySpace.Name = "emptySpace";
            this.emptySpace.Size = new System.Drawing.Size(842, 8);
            this.emptySpace.TabIndex = 13;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // frmDispo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(842, 474);
            this.Controls.Add(this.VehicelPanel);
            this.Controls.Add(this.TimePanel);
            this.Controls.Add(this.MiscPanel);
            this.Controls.Add(this.emptySpace);
            this.Controls.Add(this.lblCaption);
            this.Name = "frmDispo";
            this.Text = "Disposition: Kalender";
            this.Load += new System.EventHandler(this.frmDispoKalender_Load);
            this.SizeChanged += new System.EventHandler(this.frmDispoKalender_SizeChanged);
            this.TimePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftVor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftZur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightVor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRightZur)).EndInit();
            this.MiscPanel.ResumeLayout(false);
            this.MiscPanel.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRowHeight)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel VehicelPanel;
        internal System.Windows.Forms.Panel TimePanel;
        private Sped4.Controls.AFColorLabel lblCaption;
        private Sped4.Controls.AFMinMaxPanel MiscPanel;
        private System.Windows.Forms.CheckBox cbAuflieger;
        private System.Windows.Forms.Panel emptySpace;
        private System.Windows.Forms.CheckBox cbFahrer;
        private System.Windows.Forms.CheckBox cbAufliegerListe;
        private System.Windows.Forms.CheckBox cbFahrerliste;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbLeftVor;
        private System.Windows.Forms.PictureBox pbRightVor;
        private System.Windows.Forms.PictureBox pbLeftZur;
        private System.Windows.Forms.PictureBox pbRightZur;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudRowHeight;
        private System.Windows.Forms.Button tbnOK;
        public System.Windows.Forms.MonthCalendar DispoKalender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton tsbtnDispoPlanIntegrieren;
        private System.Windows.Forms.ToolStripButton tsbtnDispoplanFenster;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}
