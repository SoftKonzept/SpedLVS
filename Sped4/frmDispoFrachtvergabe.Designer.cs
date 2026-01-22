namespace Sped4
{
  partial class frmDispoFrachtvergabe
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDispoFrachtvergabe));
      this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
      this.tsbADR = new System.Windows.Forms.ToolStripButton();
      this.tsbSpeichern = new System.Windows.Forms.ToolStripButton();
      this.tsbClose = new System.Windows.Forms.ToolStripButton();
      this.printDocument1 = new System.Drawing.Printing.PrintDocument();
      this.ppdialog = new System.Windows.Forms.PrintPreviewDialog();
      this.afToolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // afToolStrip1
      // 
      this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbADR,
            this.tsbSpeichern,
            this.tsbClose});
      this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
      this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
      this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
      this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
      this.afToolStrip1.myUnderlined = true;
      this.afToolStrip1.Name = "afToolStrip1";
      this.afToolStrip1.Size = new System.Drawing.Size(552, 25);
      this.afToolStrip1.TabIndex = 8;
      this.afToolStrip1.Text = "afToolStrip1";
      // 
      // tsbADR
      // 
      this.tsbADR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbADR.Image = global::Sped4.Properties.Resources.address_book2;
      this.tsbADR.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbADR.Name = "tsbADR";
      this.tsbADR.Size = new System.Drawing.Size(23, 22);
      this.tsbADR.Text = "Subunternehmer hinzufügen";
      this.tsbADR.Click += new System.EventHandler(this.tsbADR_Click);
      // 
      // tsbSpeichern
      // 
      this.tsbSpeichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbSpeichern.Image = global::Sped4.Properties.Resources.check;
      this.tsbSpeichern.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbSpeichern.Name = "tsbSpeichern";
      this.tsbSpeichern.Size = new System.Drawing.Size(23, 22);
      this.tsbSpeichern.Text = "Tranportauftrag speichern und drucken";
      this.tsbSpeichern.Click += new System.EventHandler(this.tsbSpeichern_Click);
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
      // ppdialog
      // 
      this.ppdialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
      this.ppdialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
      this.ppdialog.ClientSize = new System.Drawing.Size(400, 300);
      this.ppdialog.Enabled = true;
      this.ppdialog.Icon = ((System.Drawing.Icon)(resources.GetObject("ppdialog.Icon")));
      this.ppdialog.Name = "ppdialog";
      this.ppdialog.Visible = false;
      // 
      // frmDispoFrachtvergabe
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(552, 748);
      this.Controls.Add(this.afToolStrip1);
      this.Name = "frmDispoFrachtvergabe";
      this.Text = "Frachtvergabe an Subunternehmer";
      this.Load += new System.EventHandler(this.frmDispoFrachtvergabe_Load);
      this.afToolStrip1.ResumeLayout(false);
      this.afToolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbADR;
    private System.Windows.Forms.ToolStripButton tsbSpeichern;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Drawing.Printing.PrintDocument printDocument1;
    private System.Windows.Forms.PrintPreviewDialog ppdialog;

  }
}
