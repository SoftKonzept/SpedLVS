namespace Sped4
{
  partial class frmDispoKommiChange
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
            this.tbEntladezeitAlt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbBeladezeitAlt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nud_NBeZeitMin = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpNewBeladezeit = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nud_NBeZeitStd = new System.Windows.Forms.NumericUpDown();
            this.nud_NEntZeitMin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpNewEntladezeit = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nud_NEntZeitStd = new System.Windows.Forms.NumericUpDown();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NBeZeitMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NBeZeitStd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NEntZeitMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NEntZeitStd)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "aktuelle Startzeit:";
            // 
            // tbEntladezeitAlt
            // 
            this.tbEntladezeitAlt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEntladezeitAlt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEntladezeitAlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEntladezeitAlt.Location = new System.Drawing.Point(111, 54);
            this.tbEntladezeitAlt.Name = "tbEntladezeitAlt";
            this.tbEntladezeitAlt.ReadOnly = true;
            this.tbEntladezeitAlt.Size = new System.Drawing.Size(322, 20);
            this.tbEntladezeitAlt.TabIndex = 51;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbBeladezeitAlt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nud_NBeZeitMin);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpNewBeladezeit);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbEntladezeitAlt);
            this.groupBox1.Controls.Add(this.nud_NBeZeitStd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nud_NEntZeitMin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpNewEntladezeit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.nud_NEntZeitStd);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(9, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 147);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Start- / Endzeiten ändern";
            // 
            // tbBeladezeitAlt
            // 
            this.tbBeladezeitAlt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBeladezeitAlt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbBeladezeitAlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBeladezeitAlt.Location = new System.Drawing.Point(111, 29);
            this.tbBeladezeitAlt.Name = "tbBeladezeitAlt";
            this.tbBeladezeitAlt.ReadOnly = true;
            this.tbBeladezeitAlt.Size = new System.Drawing.Size(322, 20);
            this.tbBeladezeitAlt.TabIndex = 165;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(5, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 164;
            this.label2.Text = "aktuelle Endzeit:";
            // 
            // nud_NBeZeitMin
            // 
            this.nud_NBeZeitMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nud_NBeZeitMin.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nud_NBeZeitMin.Location = new System.Drawing.Point(382, 84);
            this.nud_NBeZeitMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nud_NBeZeitMin.Name = "nud_NBeZeitMin";
            this.nud_NBeZeitMin.Size = new System.Drawing.Size(45, 20);
            this.nud_NBeZeitMin.TabIndex = 163;
            this.nud_NBeZeitMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nud_NBeZeitMin.ValueChanged += new System.EventHandler(this.nud_NBeZeitMin_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(433, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 162;
            this.label7.Text = "Uhr";
            // 
            // dtpNewBeladezeit
            // 
            this.dtpNewBeladezeit.Location = new System.Drawing.Point(113, 84);
            this.dtpNewBeladezeit.Name = "dtpNewBeladezeit";
            this.dtpNewBeladezeit.Size = new System.Drawing.Size(200, 20);
            this.dtpNewBeladezeit.TabIndex = 161;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(372, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 160;
            this.label8.Text = ":";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 159;
            this.label9.Text = "neue Startzeit:";
            // 
            // nud_NBeZeitStd
            // 
            this.nud_NBeZeitStd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nud_NBeZeitStd.Location = new System.Drawing.Point(322, 84);
            this.nud_NBeZeitStd.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nud_NBeZeitStd.Name = "nud_NBeZeitStd";
            this.nud_NBeZeitStd.Size = new System.Drawing.Size(45, 20);
            this.nud_NBeZeitStd.TabIndex = 158;
            this.nud_NBeZeitStd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nud_NEntZeitMin
            // 
            this.nud_NEntZeitMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nud_NEntZeitMin.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nud_NEntZeitMin.Location = new System.Drawing.Point(382, 110);
            this.nud_NEntZeitMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nud_NEntZeitMin.Name = "nud_NEntZeitMin";
            this.nud_NEntZeitMin.Size = new System.Drawing.Size(45, 20);
            this.nud_NEntZeitMin.TabIndex = 157;
            this.nud_NEntZeitMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nud_NEntZeitMin.ValueChanged += new System.EventHandler(this.nud_NEntZeitMin_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(433, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 156;
            this.label5.Text = "Uhr";
            // 
            // dtpNewEntladezeit
            // 
            this.dtpNewEntladezeit.Location = new System.Drawing.Point(113, 110);
            this.dtpNewEntladezeit.Name = "dtpNewEntladezeit";
            this.dtpNewEntladezeit.Size = new System.Drawing.Size(200, 20);
            this.dtpNewEntladezeit.TabIndex = 155;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(372, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 154;
            this.label4.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 153;
            this.label6.Text = "neue Endzeit:";
            // 
            // nud_NEntZeitStd
            // 
            this.nud_NEntZeitStd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nud_NEntZeitStd.Location = new System.Drawing.Point(322, 110);
            this.nud_NEntZeitStd.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nud_NEntZeitStd.Name = "nud_NEntZeitStd";
            this.nud_NEntZeitStd.Size = new System.Drawing.Size(45, 20);
            this.nud_NEntZeitStd.TabIndex = 152;
            this.nud_NEntZeitStd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.afToolStrip1.Size = new System.Drawing.Size(525, 25);
            this.afToolStrip1.TabIndex = 145;
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
            // frmDispoKommiChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(525, 196);
            this.Controls.Add(this.afToolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDispoKommiChange";
            this.Text = "Tour Start- und Endzeit  ändern";
            this.Load += new System.EventHandler(this.frmDispoKommiChange_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NBeZeitMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NBeZeitStd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NEntZeitMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_NEntZeitStd)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbEntladezeitAlt;
    private System.Windows.Forms.GroupBox groupBox1;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.TextBox tbBeladezeitAlt;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown nud_NBeZeitMin;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.DateTimePicker dtpNewBeladezeit;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.NumericUpDown nud_NBeZeitStd;
    private System.Windows.Forms.NumericUpDown nud_NEntZeitMin;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.DateTimePicker dtpNewEntladezeit;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown nud_NEntZeitStd;
  }
}
