namespace Sped4
{
  partial class frmAbrechnungslisteZeitraum
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
        this.dtpBis = new System.Windows.Forms.DateTimePicker();
        this.dtpVon = new System.Windows.Forms.DateTimePicker();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.btnAbbruch = new System.Windows.Forms.Button();
        this.btn1 = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // dtpBis
        // 
        this.dtpBis.Location = new System.Drawing.Point(50, 116);
        this.dtpBis.Name = "dtpBis";
        this.dtpBis.Size = new System.Drawing.Size(200, 20);
        this.dtpBis.TabIndex = 0;
        // 
        // dtpVon
        // 
        this.dtpVon.Location = new System.Drawing.Point(50, 65);
        this.dtpVon.Name = "dtpVon";
        this.dtpVon.Size = new System.Drawing.Size(200, 20);
        this.dtpVon.TabIndex = 1;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.ForeColor = System.Drawing.Color.DarkBlue;
        this.label1.Location = new System.Drawing.Point(12, 29);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(238, 13);
        this.label1.TabIndex = 2;
        this.label1.Text = "Bitte wählen Sie den gewünschten Zeitraum aus:";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.DarkBlue;
        this.label2.Location = new System.Drawing.Point(12, 69);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(28, 13);
        this.label2.TabIndex = 3;
        this.label2.Text = "von:";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.ForeColor = System.Drawing.Color.DarkBlue;
        this.label3.Location = new System.Drawing.Point(17, 120);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(23, 13);
        this.label3.TabIndex = 4;
        this.label3.Text = "bis:";
        // 
        // btnAbbruch
        // 
        this.btnAbbruch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btnAbbruch.Image = global::Sped4.Properties.Resources.delete_16;
        this.btnAbbruch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnAbbruch.Location = new System.Drawing.Point(139, 160);
        this.btnAbbruch.Name = "btnAbbruch";
        this.btnAbbruch.Size = new System.Drawing.Size(83, 25);
        this.btnAbbruch.TabIndex = 22;
        this.btnAbbruch.Text = "     &Abbruch";
        this.btnAbbruch.UseVisualStyleBackColor = true;
        this.btnAbbruch.Click += new System.EventHandler(this.btnAbbruch_Click);
        // 
        // btn1
        // 
        this.btn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.btn1.Image = global::Sped4.Properties.Resources.check;
        this.btn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btn1.Location = new System.Drawing.Point(50, 160);
        this.btn1.Name = "btn1";
        this.btn1.Size = new System.Drawing.Size(83, 25);
        this.btn1.TabIndex = 21;
        this.btn1.Text = "     &suchen";
        this.btn1.UseVisualStyleBackColor = true;
        this.btn1.Click += new System.EventHandler(this.btn1_Click);
        // 
        // frmAuftragslisteZeitraum
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.ClientSize = new System.Drawing.Size(266, 206);
        this.Controls.Add(this.btnAbbruch);
        this.Controls.Add(this.btn1);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.dtpVon);
        this.Controls.Add(this.dtpBis);
        this.Name = "frmAuftragslisteZeitraum";
        this.Text = "Auswahl Zeitraum";
        this.Load += new System.EventHandler(this.frmAbrechnungslisteZeitraum_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DateTimePicker dtpBis;
    private System.Windows.Forms.DateTimePicker dtpVon;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnAbbruch;
    private System.Windows.Forms.Button btn1;
  }
}
