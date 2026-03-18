namespace Sped4
{
  partial class frmDispoRecourceChange
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbRecourcenentzeitAlt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudEMin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cbNewDateTo = new System.Windows.Forms.CheckBox();
            this.tbMaxREZ = new System.Windows.Forms.TextBox();
            this.dtp_NewEDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudEHour = new System.Windows.Forms.NumericUpDown();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tbMaxRSZ = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudSMin = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbRessourcenStartAlt = new System.Windows.Forms.TextBox();
            this.dtpNewSDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudSHour = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEHour)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSHour)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "aktuelle Endzeit:";
            // 
            // tbRecourcenentzeitAlt
            // 
            this.tbRecourcenentzeitAlt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRecourcenentzeitAlt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRecourcenentzeitAlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRecourcenentzeitAlt.Location = new System.Drawing.Point(194, 26);
            this.tbRecourcenentzeitAlt.Name = "tbRecourcenentzeitAlt";
            this.tbRecourcenentzeitAlt.ReadOnly = true;
            this.tbRecourcenentzeitAlt.Size = new System.Drawing.Size(215, 20);
            this.tbRecourcenentzeitAlt.TabIndex = 51;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudEMin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbNewDateTo);
            this.groupBox1.Controls.Add(this.tbMaxREZ);
            this.groupBox1.Controls.Add(this.dtp_NewEDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbRecourcenentzeitAlt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudEHour);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(12, 180);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 152);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ressourcen-Endzeit";
            // 
            // nudEMin
            // 
            this.nudEMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudEMin.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudEMin.Location = new System.Drawing.Point(367, 115);
            this.nudEMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudEMin.Name = "nudEMin";
            this.nudEMin.Size = new System.Drawing.Size(45, 20);
            this.nudEMin.TabIndex = 151;
            this.nudEMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudEMin.ValueChanged += new System.EventHandler(this.nudEMin_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(418, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Uhr";
            // 
            // cbNewDateTo
            // 
            this.cbNewDateTo.AutoSize = true;
            this.cbNewDateTo.Location = new System.Drawing.Point(20, 89);
            this.cbNewDateTo.Name = "cbNewDateTo";
            this.cbNewDateTo.Size = new System.Drawing.Size(140, 17);
            this.cbNewDateTo.TabIndex = 57;
            this.cbNewDateTo.Text = "Neues Datum unendlich";
            this.cbNewDateTo.UseVisualStyleBackColor = true;
            this.cbNewDateTo.CheckedChanged += new System.EventHandler(this.cbNewDateTo_CheckedChanged);
            // 
            // tbMaxREZ
            // 
            this.tbMaxREZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMaxREZ.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbMaxREZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMaxREZ.Location = new System.Drawing.Point(194, 52);
            this.tbMaxREZ.Name = "tbMaxREZ";
            this.tbMaxREZ.ReadOnly = true;
            this.tbMaxREZ.Size = new System.Drawing.Size(215, 20);
            this.tbMaxREZ.TabIndex = 56;
            // 
            // dtp_NewEDate
            // 
            this.dtp_NewEDate.Location = new System.Drawing.Point(98, 115);
            this.dtp_NewEDate.Name = "dtp_NewEDate";
            this.dtp_NewEDate.Size = new System.Drawing.Size(200, 20);
            this.dtp_NewEDate.TabIndex = 56;
            this.dtp_NewEDate.ValueChanged += new System.EventHandler(this.dtp_NewEDate_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(17, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "max. Endzeit:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(357, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 54;
            this.label2.Text = "neue Endzeit:";
            // 
            // nudEHour
            // 
            this.nudEHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudEHour.Location = new System.Drawing.Point(307, 115);
            this.nudEHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudEHour.Name = "nudEHour";
            this.nudEHour.Size = new System.Drawing.Size(45, 20);
            this.nudEHour.TabIndex = 1;
            this.nudEHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudEHour.ValueChanged += new System.EventHandler(this.nudEHour_ValueChanged);
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(478, 25);
            this.afToolStrip1.TabIndex = 144;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Sped4.Properties.Resources.check;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "     &Speichern";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            // tbMaxRSZ
            // 
            this.tbMaxRSZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMaxRSZ.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbMaxRSZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMaxRSZ.Location = new System.Drawing.Point(194, 47);
            this.tbMaxRSZ.Name = "tbMaxRSZ";
            this.tbMaxRSZ.ReadOnly = true;
            this.tbMaxRSZ.Size = new System.Drawing.Size(215, 20);
            this.tbMaxRSZ.TabIndex = 149;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(17, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 148;
            this.label6.Text = "max. Startzeit:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudSMin);
            this.groupBox2.Controls.Add(this.tbMaxRSZ);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbRessourcenStartAlt);
            this.groupBox2.Controls.Add(this.dtpNewSDate);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.nudSHour);
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(12, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(454, 121);
            this.groupBox2.TabIndex = 147;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ressourcen-Startzeit";
            // 
            // nudSMin
            // 
            this.nudSMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudSMin.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudSMin.Location = new System.Drawing.Point(364, 86);
            this.nudSMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudSMin.Name = "nudSMin";
            this.nudSMin.Size = new System.Drawing.Size(45, 20);
            this.nudSMin.TabIndex = 150;
            this.nudSMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSMin.ValueChanged += new System.EventHandler(this.nudSMin_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.DarkBlue;
            this.label11.Location = new System.Drawing.Point(17, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 146;
            this.label11.Text = "neue Startzeit:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(418, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Uhr";
            // 
            // tbRessourcenStartAlt
            // 
            this.tbRessourcenStartAlt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRessourcenStartAlt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRessourcenStartAlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRessourcenStartAlt.Location = new System.Drawing.Point(194, 24);
            this.tbRessourcenStartAlt.Name = "tbRessourcenStartAlt";
            this.tbRessourcenStartAlt.ReadOnly = true;
            this.tbRessourcenStartAlt.Size = new System.Drawing.Size(215, 20);
            this.tbRessourcenStartAlt.TabIndex = 146;
            // 
            // dtpNewSDate
            // 
            this.dtpNewSDate.Location = new System.Drawing.Point(98, 86);
            this.dtpNewSDate.Name = "dtpNewSDate";
            this.dtpNewSDate.Size = new System.Drawing.Size(200, 20);
            this.dtpNewSDate.TabIndex = 56;
            this.dtpNewSDate.ValueChanged += new System.EventHandler(this.dtpNewSDate_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.DarkBlue;
            this.label10.Location = new System.Drawing.Point(17, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 145;
            this.label10.Text = "aktuelle Startzeit:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(355, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 55;
            this.label8.Text = ":";
            // 
            // nudSHour
            // 
            this.nudSHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudSHour.Location = new System.Drawing.Point(307, 86);
            this.nudSHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudSHour.Name = "nudSHour";
            this.nudSHour.Size = new System.Drawing.Size(45, 20);
            this.nudSHour.TabIndex = 1;
            this.nudSHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSHour.ValueChanged += new System.EventHandler(this.nudSHour_ValueChanged);
            // 
            // frmDispoRecourceChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(478, 340);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.afToolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDispoRecourceChange";
            this.Text = "Ressourcenzeiten ändern";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEHour)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSHour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbRecourcenentzeitAlt;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown nudEHour;
    private System.Windows.Forms.DateTimePicker dtp_NewEDate;
    private System.Windows.Forms.CheckBox cbNewDateTo;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox tbMaxREZ;
    private System.Windows.Forms.Label label5;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.TextBox tbMaxRSZ;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox tbRessourcenStartAlt;
    private System.Windows.Forms.DateTimePicker dtpNewSDate;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.NumericUpDown nudSHour;
    private System.Windows.Forms.NumericUpDown nudSMin;
    private System.Windows.Forms.NumericUpDown nudEMin;
  }
}
