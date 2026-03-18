namespace Sped4
{
  partial class frmTarifauswahl
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
      this.btnGNT = new System.Windows.Forms.Button();
      this.btnGNTalt = new System.Windows.Forms.Button();
      this.btnGFT = new System.Windows.Forms.Button();
      this.btnKDspez = new System.Windows.Forms.Button();
      this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
      this.tsbClose = new System.Windows.Forms.ToolStripButton();
      this.afToolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnGNT
      // 
      this.btnGNT.Location = new System.Drawing.Point(23, 41);
      this.btnGNT.Name = "btnGNT";
      this.btnGNT.Size = new System.Drawing.Size(104, 23);
      this.btnGNT.TabIndex = 0;
      this.btnGNT.Text = "GNT";
      this.btnGNT.UseVisualStyleBackColor = true;
      // 
      // btnGNTalt
      // 
      this.btnGNTalt.Location = new System.Drawing.Point(23, 70);
      this.btnGNTalt.Name = "btnGNTalt";
      this.btnGNTalt.Size = new System.Drawing.Size(104, 23);
      this.btnGNTalt.TabIndex = 1;
      this.btnGNTalt.Text = "GNTalt";
      this.btnGNTalt.UseVisualStyleBackColor = true;
      // 
      // btnGFT
      // 
      this.btnGFT.Location = new System.Drawing.Point(23, 127);
      this.btnGFT.Name = "btnGFT";
      this.btnGFT.Size = new System.Drawing.Size(104, 23);
      this.btnGFT.TabIndex = 2;
      this.btnGFT.Text = "GFT";
      this.btnGFT.UseVisualStyleBackColor = true;
      // 
      // btnKDspez
      // 
      this.btnKDspez.Location = new System.Drawing.Point(23, 179);
      this.btnKDspez.Name = "btnKDspez";
      this.btnKDspez.Size = new System.Drawing.Size(104, 23);
      this.btnKDspez.TabIndex = 3;
      this.btnKDspez.Text = "Kundentarif";
      this.btnKDspez.UseVisualStyleBackColor = true;
      // 
      // afToolStrip1
      // 
      this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose});
      this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
      this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
      this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
      this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
      this.afToolStrip1.myUnderlined = true;
      this.afToolStrip1.Name = "afToolStrip1";
      this.afToolStrip1.Size = new System.Drawing.Size(155, 25);
      this.afToolStrip1.TabIndex = 126;
      this.afToolStrip1.Text = "afToolStrip1";
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
      // frmTarifauswahl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(155, 218);
      this.Controls.Add(this.afToolStrip1);
      this.Controls.Add(this.btnKDspez);
      this.Controls.Add(this.btnGFT);
      this.Controls.Add(this.btnGNTalt);
      this.Controls.Add(this.btnGNT);
      this.Name = "frmTarifauswahl";
      this.Text = "Tarifauswahl";
      this.afToolStrip1.ResumeLayout(false);
      this.afToolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnGNT;
    private System.Windows.Forms.Button btnGNTalt;
    private System.Windows.Forms.Button btnGFT;
    private System.Windows.Forms.Button btnKDspez;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbClose;
  }
}