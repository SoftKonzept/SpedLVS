namespace Sped4
{
  partial class frmKommiDetailsPanel
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
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.splitContainer1.SuspendLayout();
        this.SuspendLayout();
        // 
        // splitContainer1
        // 
        this.splitContainer1.BackColor = global::Sped4.Properties.Settings.Default.EffectColor;
        this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer1.Location = new System.Drawing.Point(0, 0);
        this.splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.AutoScroll = true;
        this.splitContainer1.Size = new System.Drawing.Size(1057, 873);
        this.splitContainer1.SplitterDistance = 315;
        this.splitContainer1.TabIndex = 0;
        // 
        // frmDetailsPanel
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.ClientSize = new System.Drawing.Size(1057, 873);
        this.Controls.Add(this.splitContainer1);
        this.Name = "frmDetailsPanel";
        this.Text = "Detailübersicht";
        this.Load += new System.EventHandler(this.frmMDIDetails_Load);
        this.splitContainer1.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
  }
}
