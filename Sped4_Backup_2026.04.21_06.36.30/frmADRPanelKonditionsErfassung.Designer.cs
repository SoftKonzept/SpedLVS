namespace Sped4
{
  partial class frmADRPanelKonditionsErfassung
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
        this.tbKDADR = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.btnPkm = new System.Windows.Forms.Button();
        this.cbGNT200 = new System.Windows.Forms.CheckBox();
        this.btnEP = new System.Windows.Forms.Button();
        this.btnGewicht = new System.Windows.Forms.Button();
        this.cbGFT = new System.Windows.Forms.CheckBox();
        this.cbGNTalt = new System.Windows.Forms.CheckBox();
        this.cbGNT = new System.Windows.Forms.CheckBox();
        this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
        this.tsbSpeichern = new System.Windows.Forms.ToolStripButton();
        this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
        this.groupBox1.SuspendLayout();
        this.afToolStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // tbKDADR
        // 
        this.tbKDADR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.tbKDADR.Enabled = false;
        this.tbKDADR.Location = new System.Drawing.Point(70, 42);
        this.tbKDADR.Multiline = true;
        this.tbKDADR.Name = "tbKDADR";
        this.tbKDADR.Size = new System.Drawing.Size(194, 70);
        this.tbKDADR.TabIndex = 35;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.DarkBlue;
        this.label1.Location = new System.Drawing.Point(13, 42);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(38, 13);
        this.label1.TabIndex = 36;
        this.label1.Text = "Kunde";
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.btnPkm);
        this.groupBox1.Controls.Add(this.cbGNT200);
        this.groupBox1.Controls.Add(this.btnEP);
        this.groupBox1.Controls.Add(this.btnGewicht);
        this.groupBox1.Controls.Add(this.cbGFT);
        this.groupBox1.Controls.Add(this.cbGNTalt);
        this.groupBox1.Controls.Add(this.cbGNT);
        this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
        this.groupBox1.Location = new System.Drawing.Point(70, 129);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(193, 214);
        this.groupBox1.TabIndex = 40;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Arten Frachtkonditionen";
        // 
        // btnPkm
        // 
        this.btnPkm.Location = new System.Drawing.Point(45, 122);
        this.btnPkm.Name = "btnPkm";
        this.btnPkm.Size = new System.Drawing.Size(84, 23);
        this.btnPkm.TabIndex = 6;
        this.btnPkm.Text = "€/km";
        this.btnPkm.UseVisualStyleBackColor = true;
        this.btnPkm.Click += new System.EventHandler(this.btnPkm_Click);
        // 
        // cbGNT200
        // 
        this.cbGNT200.AutoSize = true;
        this.cbGNT200.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.cbGNT200.Location = new System.Drawing.Point(45, 76);
        this.cbGNT200.Name = "cbGNT200";
        this.cbGNT200.Size = new System.Drawing.Size(100, 17);
        this.cbGNT200.TabIndex = 5;
        this.cbGNT200.Text = "GNT bis 200 km";
        this.cbGNT200.UseVisualStyleBackColor = true;
        this.cbGNT200.CheckedChanged += new System.EventHandler(this.cbGNT200_CheckedChanged);
        // 
        // btnEP
        // 
        this.btnEP.Location = new System.Drawing.Point(45, 180);
        this.btnEP.Name = "btnEP";
        this.btnEP.Size = new System.Drawing.Size(84, 21);
        this.btnEP.TabIndex = 4;
        this.btnEP.Text = "€/EP und km";
        this.btnEP.UseVisualStyleBackColor = true;
        this.btnEP.Click += new System.EventHandler(this.btnEP_Click);
        // 
        // btnGewicht
        // 
        this.btnGewicht.Location = new System.Drawing.Point(45, 151);
        this.btnGewicht.Name = "btnGewicht";
        this.btnGewicht.Size = new System.Drawing.Size(84, 23);
        this.btnGewicht.TabIndex = 3;
        this.btnGewicht.Text = "€/to und km";
        this.btnGewicht.UseVisualStyleBackColor = true;
        this.btnGewicht.Click += new System.EventHandler(this.btnGewicht_Click);
        // 
        // cbGFT
        // 
        this.cbGFT.AutoSize = true;
        this.cbGFT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.cbGFT.Location = new System.Drawing.Point(45, 99);
        this.cbGFT.Name = "cbGFT";
        this.cbGFT.Size = new System.Drawing.Size(44, 17);
        this.cbGFT.TabIndex = 2;
        this.cbGFT.Text = "GFT";
        this.cbGFT.UseVisualStyleBackColor = true;
        this.cbGFT.CheckedChanged += new System.EventHandler(this.cbGFT_CheckedChanged);
        // 
        // cbGNTalt
        // 
        this.cbGNTalt.AutoSize = true;
        this.cbGNTalt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.cbGNTalt.Location = new System.Drawing.Point(45, 53);
        this.cbGNTalt.Name = "cbGNTalt";
        this.cbGNTalt.Size = new System.Drawing.Size(60, 17);
        this.cbGNTalt.TabIndex = 1;
        this.cbGNTalt.Text = "GNT alt";
        this.cbGNTalt.UseVisualStyleBackColor = true;
        this.cbGNTalt.CheckedChanged += new System.EventHandler(this.cbGNTalt_CheckedChanged);
        // 
        // cbGNT
        // 
        this.cbGNT.AutoSize = true;
        this.cbGNT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.cbGNT.Location = new System.Drawing.Point(45, 30);
        this.cbGNT.Name = "cbGNT";
        this.cbGNT.Size = new System.Drawing.Size(46, 17);
        this.cbGNT.TabIndex = 0;
        this.cbGNT.Text = "GNT";
        this.cbGNT.UseVisualStyleBackColor = true;
        this.cbGNT.CheckedChanged += new System.EventHandler(this.cbGNT_CheckedChanged);
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
        this.afToolStrip1.Size = new System.Drawing.Size(309, 25);
        this.afToolStrip1.TabIndex = 41;
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
        // frmADRPanelKonditionsErfassung
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.ClientSize = new System.Drawing.Size(309, 381);
        this.Controls.Add(this.afToolStrip1);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.tbKDADR);
        this.Name = "frmADRPanelKonditionsErfassung";
        this.Text = "Übersicht Erfassung Frachtkonditionen";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.afToolStrip1.ResumeLayout(false);
        this.afToolStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbKDADR;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnGewicht;
    private System.Windows.Forms.CheckBox cbGFT;
    private System.Windows.Forms.CheckBox cbGNTalt;
    private System.Windows.Forms.CheckBox cbGNT;
    private System.Windows.Forms.Button btnEP;
    private System.Windows.Forms.CheckBox cbGNT200;
    private System.Windows.Forms.Button btnPkm;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbSpeichern;
    private System.Windows.Forms.ToolStripButton tsbtnClose;
  }
}
