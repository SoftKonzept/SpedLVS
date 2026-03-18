namespace Sped4
{
    partial class frmPersonal
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
          this.gbPersonal = new System.Windows.Forms.GroupBox();
          this.cbAnrede = new System.Windows.Forms.ComboBox();
          this.label1 = new System.Windows.Forms.Label();
          this.cbBis = new System.Windows.Forms.CheckBox();
          this.cbBeruf = new System.Windows.Forms.ComboBox();
          this.cbAbteilung = new System.Windows.Forms.ComboBox();
          this.lMail = new System.Windows.Forms.Label();
          this.tbMail = new System.Windows.Forms.TextBox();
          this.tbTel = new System.Windows.Forms.TextBox();
          this.lTel = new System.Windows.Forms.Label();
          this.pictureBox1 = new System.Windows.Forms.PictureBox();
          this.lNotiz = new System.Windows.Forms.Label();
          this.tbVorname = new System.Windows.Forms.TextBox();
          this.lAbteilung = new System.Windows.Forms.Label();
          this.tbNotiz = new System.Windows.Forms.TextBox();
          this.lName = new System.Windows.Forms.Label();
          this.lVorname = new System.Windows.Forms.Label();
          this.lBeruf = new System.Windows.Forms.Label();
          this.tbName = new System.Windows.Forms.TextBox();
          this.lStr = new System.Windows.Forms.Label();
          this.lOrt = new System.Windows.Forms.Label();
          this.lPLZ = new System.Windows.Forms.Label();
          this.tbStr = new System.Windows.Forms.TextBox();
          this.tbPLZ = new System.Windows.Forms.TextBox();
          this.tbOrt = new System.Windows.Forms.TextBox();
          this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
          this.tsbSpeichern = new System.Windows.Forms.ToolStripButton();
          this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
          this.gbPersonal.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
          this.afToolStrip1.SuspendLayout();
          this.SuspendLayout();
          // 
          // gbPersonal
          // 
          this.gbPersonal.Controls.Add(this.cbAnrede);
          this.gbPersonal.Controls.Add(this.label1);
          this.gbPersonal.Controls.Add(this.cbBis);
          this.gbPersonal.Controls.Add(this.cbBeruf);
          this.gbPersonal.Controls.Add(this.cbAbteilung);
          this.gbPersonal.Controls.Add(this.lMail);
          this.gbPersonal.Controls.Add(this.tbMail);
          this.gbPersonal.Controls.Add(this.tbTel);
          this.gbPersonal.Controls.Add(this.lTel);
          this.gbPersonal.Controls.Add(this.pictureBox1);
          this.gbPersonal.Controls.Add(this.lNotiz);
          this.gbPersonal.Controls.Add(this.tbVorname);
          this.gbPersonal.Controls.Add(this.lAbteilung);
          this.gbPersonal.Controls.Add(this.tbNotiz);
          this.gbPersonal.Controls.Add(this.lName);
          this.gbPersonal.Controls.Add(this.lVorname);
          this.gbPersonal.Controls.Add(this.lBeruf);
          this.gbPersonal.Controls.Add(this.tbName);
          this.gbPersonal.Controls.Add(this.lStr);
          this.gbPersonal.Controls.Add(this.lOrt);
          this.gbPersonal.Controls.Add(this.lPLZ);
          this.gbPersonal.Controls.Add(this.tbStr);
          this.gbPersonal.Controls.Add(this.tbPLZ);
          this.gbPersonal.Controls.Add(this.tbOrt);
          this.gbPersonal.ForeColor = System.Drawing.Color.DarkBlue;
          this.gbPersonal.Location = new System.Drawing.Point(22, 28);
          this.gbPersonal.Name = "gbPersonal";
          this.gbPersonal.Size = new System.Drawing.Size(556, 492);
          this.gbPersonal.TabIndex = 29;
          this.gbPersonal.TabStop = false;
          this.gbPersonal.Text = "Personaldaten";
          // 
          // cbAnrede
          // 
          this.cbAnrede.AllowDrop = true;
          this.cbAnrede.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.cbAnrede.FormattingEnabled = true;
          this.cbAnrede.Location = new System.Drawing.Point(86, 28);
          this.cbAnrede.Name = "cbAnrede";
          this.cbAnrede.Size = new System.Drawing.Size(256, 21);
          this.cbAnrede.TabIndex = 35;
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.ForeColor = System.Drawing.Color.DarkBlue;
          this.label1.Location = new System.Drawing.Point(27, 31);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(41, 13);
          this.label1.TabIndex = 34;
          this.label1.Text = "Anrede";
          // 
          // cbBis
          // 
          this.cbBis.AutoSize = true;
          this.cbBis.Location = new System.Drawing.Point(86, 331);
          this.cbBis.Name = "cbBis";
          this.cbBis.Size = new System.Drawing.Size(145, 17);
          this.cbBis.TabIndex = 11;
          this.cbBis.Text = "Arbeitsverhältnis beendet";
          this.cbBis.UseVisualStyleBackColor = true;
          this.cbBis.CheckedChanged += new System.EventHandler(this.cbBis_CheckedChanged);
          // 
          // cbBeruf
          // 
          this.cbBeruf.AllowDrop = true;
          this.cbBeruf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.cbBeruf.FormattingEnabled = true;
          this.cbBeruf.Location = new System.Drawing.Point(86, 291);
          this.cbBeruf.Name = "cbBeruf";
          this.cbBeruf.Size = new System.Drawing.Size(440, 21);
          this.cbBeruf.TabIndex = 9;
          // 
          // cbAbteilung
          // 
          this.cbAbteilung.AllowDrop = true;
          this.cbAbteilung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.cbAbteilung.FormattingEnabled = true;
          this.cbAbteilung.Location = new System.Drawing.Point(86, 264);
          this.cbAbteilung.Name = "cbAbteilung";
          this.cbAbteilung.Size = new System.Drawing.Size(440, 21);
          this.cbAbteilung.TabIndex = 8;
          // 
          // lMail
          // 
          this.lMail.AutoSize = true;
          this.lMail.ForeColor = System.Drawing.Color.DarkBlue;
          this.lMail.Location = new System.Drawing.Point(27, 220);
          this.lMail.Name = "lMail";
          this.lMail.Size = new System.Drawing.Size(36, 13);
          this.lMail.TabIndex = 29;
          this.lMail.Text = "E-Mail";
          // 
          // tbMail
          // 
          this.tbMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.tbMail.Location = new System.Drawing.Point(86, 218);
          this.tbMail.Name = "tbMail";
          this.tbMail.Size = new System.Drawing.Size(440, 20);
          this.tbMail.TabIndex = 7;
          // 
          // tbTel
          // 
          this.tbTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.tbTel.Location = new System.Drawing.Point(86, 193);
          this.tbTel.Name = "tbTel";
          this.tbTel.Size = new System.Drawing.Size(440, 20);
          this.tbTel.TabIndex = 6;
          // 
          // lTel
          // 
          this.lTel.AutoSize = true;
          this.lTel.ForeColor = System.Drawing.Color.DarkBlue;
          this.lTel.Location = new System.Drawing.Point(27, 195);
          this.lTel.Name = "lTel";
          this.lTel.Size = new System.Drawing.Size(43, 13);
          this.lTel.TabIndex = 26;
          this.lTel.Text = "Telefon";
          // 
          // pictureBox1
          // 
          this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
          this.pictureBox1.Image = global::Sped4.Properties.Resources.schoolboy;
          this.pictureBox1.Location = new System.Drawing.Point(383, 31);
          this.pictureBox1.Name = "pictureBox1";
          this.pictureBox1.Size = new System.Drawing.Size(116, 143);
          this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
          this.pictureBox1.TabIndex = 25;
          this.pictureBox1.TabStop = false;
          this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
          // 
          // lNotiz
          // 
          this.lNotiz.AutoSize = true;
          this.lNotiz.BackColor = System.Drawing.Color.White;
          this.lNotiz.ForeColor = System.Drawing.Color.DarkBlue;
          this.lNotiz.Location = new System.Drawing.Point(27, 399);
          this.lNotiz.Name = "lNotiz";
          this.lNotiz.Size = new System.Drawing.Size(31, 13);
          this.lNotiz.TabIndex = 2;
          this.lNotiz.Text = "Notiz";
          // 
          // tbVorname
          // 
          this.tbVorname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.tbVorname.Location = new System.Drawing.Point(86, 77);
          this.tbVorname.Name = "tbVorname";
          this.tbVorname.Size = new System.Drawing.Size(256, 20);
          this.tbVorname.TabIndex = 2;
          // 
          // lAbteilung
          // 
          this.lAbteilung.AutoSize = true;
          this.lAbteilung.ForeColor = System.Drawing.Color.DarkBlue;
          this.lAbteilung.Location = new System.Drawing.Point(27, 268);
          this.lAbteilung.Name = "lAbteilung";
          this.lAbteilung.Size = new System.Drawing.Size(51, 13);
          this.lAbteilung.TabIndex = 21;
          this.lAbteilung.Text = "Abteilung";
          // 
          // tbNotiz
          // 
          this.tbNotiz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.tbNotiz.Location = new System.Drawing.Point(86, 396);
          this.tbNotiz.Multiline = true;
          this.tbNotiz.Name = "tbNotiz";
          this.tbNotiz.Size = new System.Drawing.Size(440, 85);
          this.tbNotiz.TabIndex = 12;
          // 
          // lName
          // 
          this.lName.AutoSize = true;
          this.lName.ForeColor = System.Drawing.Color.DarkBlue;
          this.lName.Location = new System.Drawing.Point(27, 57);
          this.lName.Name = "lName";
          this.lName.Size = new System.Drawing.Size(35, 13);
          this.lName.TabIndex = 0;
          this.lName.Text = "Name";
          // 
          // lVorname
          // 
          this.lVorname.AutoSize = true;
          this.lVorname.ForeColor = System.Drawing.Color.DarkBlue;
          this.lVorname.Location = new System.Drawing.Point(27, 80);
          this.lVorname.Name = "lVorname";
          this.lVorname.Size = new System.Drawing.Size(49, 13);
          this.lVorname.TabIndex = 1;
          this.lVorname.Text = "Vorname";
          // 
          // lBeruf
          // 
          this.lBeruf.AutoSize = true;
          this.lBeruf.ForeColor = System.Drawing.Color.DarkBlue;
          this.lBeruf.Location = new System.Drawing.Point(27, 294);
          this.lBeruf.Name = "lBeruf";
          this.lBeruf.Size = new System.Drawing.Size(32, 13);
          this.lBeruf.TabIndex = 23;
          this.lBeruf.Text = "Beruf";
          // 
          // tbName
          // 
          this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.tbName.Location = new System.Drawing.Point(86, 54);
          this.tbName.Name = "tbName";
          this.tbName.Size = new System.Drawing.Size(256, 20);
          this.tbName.TabIndex = 1;
          // 
          // lStr
          // 
          this.lStr.AutoSize = true;
          this.lStr.ForeColor = System.Drawing.Color.DarkBlue;
          this.lStr.Location = new System.Drawing.Point(27, 105);
          this.lStr.Name = "lStr";
          this.lStr.Size = new System.Drawing.Size(38, 13);
          this.lStr.TabIndex = 6;
          this.lStr.Text = "Straße";
          // 
          // lOrt
          // 
          this.lOrt.AutoSize = true;
          this.lOrt.ForeColor = System.Drawing.Color.DarkBlue;
          this.lOrt.Location = new System.Drawing.Point(27, 151);
          this.lOrt.Name = "lOrt";
          this.lOrt.Size = new System.Drawing.Size(21, 13);
          this.lOrt.TabIndex = 7;
          this.lOrt.Text = "Ort";
          // 
          // lPLZ
          // 
          this.lPLZ.AutoSize = true;
          this.lPLZ.ForeColor = System.Drawing.Color.DarkBlue;
          this.lPLZ.Location = new System.Drawing.Point(27, 129);
          this.lPLZ.Name = "lPLZ";
          this.lPLZ.Size = new System.Drawing.Size(27, 13);
          this.lPLZ.TabIndex = 8;
          this.lPLZ.Text = "PLZ";
          // 
          // tbStr
          // 
          this.tbStr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.tbStr.Location = new System.Drawing.Point(86, 102);
          this.tbStr.Name = "tbStr";
          this.tbStr.Size = new System.Drawing.Size(256, 20);
          this.tbStr.TabIndex = 3;
          // 
          // tbPLZ
          // 
          this.tbPLZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.tbPLZ.Location = new System.Drawing.Point(86, 126);
          this.tbPLZ.Name = "tbPLZ";
          this.tbPLZ.Size = new System.Drawing.Size(100, 20);
          this.tbPLZ.TabIndex = 4;
          // 
          // tbOrt
          // 
          this.tbOrt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
          this.tbOrt.Location = new System.Drawing.Point(86, 148);
          this.tbOrt.Name = "tbOrt";
          this.tbOrt.Size = new System.Drawing.Size(256, 20);
          this.tbOrt.TabIndex = 5;
          // 
          // afToolStrip1
          // 
          this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSpeichern,
            this.tsbtnClose});
          this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
          this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
          this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
          this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
          this.afToolStrip1.myUnderlined = true;
          this.afToolStrip1.Name = "afToolStrip1";
          this.afToolStrip1.Size = new System.Drawing.Size(600, 25);
          this.afToolStrip1.TabIndex = 30;
          this.afToolStrip1.Text = "afToolStrip1";
          // 
          // tsbSpeichern
          // 
          this.tsbSpeichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.tsbSpeichern.Image = global::Sped4.Properties.Resources.check;
          this.tsbSpeichern.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.tsbSpeichern.Name = "tsbSpeichern";
          this.tsbSpeichern.Size = new System.Drawing.Size(23, 22);
          this.tsbSpeichern.Text = "speichern ";
          this.tsbSpeichern.Click += new System.EventHandler(this.tsbSpeichern_Click);
          // 
          // tsbtnClose
          // 
          this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
          this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.tsbtnClose.Name = "tsbtnClose";
          this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
          this.tsbtnClose.Text = "schliessen";
          this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
          // 
          // frmPersonal
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.ClientSize = new System.Drawing.Size(600, 527);
          this.Controls.Add(this.afToolStrip1);
          this.Controls.Add(this.gbPersonal);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.Name = "frmPersonal";
          this.Text = "Eingabe Personaldaten";
          this.Load += new System.EventHandler(this.frmPersonal_Load);
          this.gbPersonal.ResumeLayout(false);
          this.gbPersonal.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
          this.afToolStrip1.ResumeLayout(false);
          this.afToolStrip1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPersonal;
        private System.Windows.Forms.TextBox tbVorname;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Label lVorname;
        private System.Windows.Forms.Label lNotiz;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lBeruf;
        private System.Windows.Forms.Label lStr;
        private System.Windows.Forms.Label lAbteilung;
        private System.Windows.Forms.Label lOrt;
        private System.Windows.Forms.Label lPLZ;
        private System.Windows.Forms.TextBox tbStr;
        private System.Windows.Forms.TextBox tbPLZ;
        private System.Windows.Forms.TextBox tbNotiz;
        private System.Windows.Forms.TextBox tbOrt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lMail;
        private System.Windows.Forms.TextBox tbMail;
        private System.Windows.Forms.TextBox tbTel;
        private System.Windows.Forms.Label lTel;
        private System.Windows.Forms.ComboBox cbBeruf;
        private System.Windows.Forms.ComboBox cbAbteilung;
        private System.Windows.Forms.CheckBox cbBis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAnrede;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSpeichern;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
    }
}
