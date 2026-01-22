namespace Sped4
{
  partial class frmScanDokArt
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
      this.tbInfo = new System.Windows.Forms.TextBox();
      this.cbDokuArt = new System.Windows.Forms.ComboBox();
      this.btn1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // tbInfo
      // 
      this.tbInfo.BackColor = System.Drawing.Color.White;
      this.tbInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbInfo.Enabled = false;
      this.tbInfo.Location = new System.Drawing.Point(12, 12);
      this.tbInfo.Multiline = true;
      this.tbInfo.Name = "tbInfo";
      this.tbInfo.Size = new System.Drawing.Size(312, 54);
      this.tbInfo.TabIndex = 0;
      // 
      // cbDokuArt
      // 
      this.cbDokuArt.AllowDrop = true;
      this.cbDokuArt.FormattingEnabled = true;
      this.cbDokuArt.Location = new System.Drawing.Point(70, 97);
      this.cbDokuArt.Name = "cbDokuArt";
      this.cbDokuArt.Size = new System.Drawing.Size(188, 21);
      this.cbDokuArt.TabIndex = 1;
      this.cbDokuArt.SelectedValueChanged += new System.EventHandler(this.cbDokuArt_SelectedValueChanged);
      // 
      // btn1
      // 
      this.btn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btn1.Image = global::Sped4.Properties.Resources.check;
      this.btn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btn1.Location = new System.Drawing.Point(126, 128);
      this.btn1.Name = "btn1";
      this.btn1.Size = new System.Drawing.Size(83, 25);
      this.btn1.TabIndex = 2;
      this.btn1.Text = "     &Speichern";
      this.btn1.UseVisualStyleBackColor = true;

      // 
      // frmScanDokArt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(338, 165);
      this.Controls.Add(this.btn1);
      this.Controls.Add(this.cbDokuArt);
      this.Controls.Add(this.tbInfo);
      this.Name = "frmScanDokArt";
      this.Text = "Auswahl Dokumentenart";
      this.Load += new System.EventHandler(this.frmScanDokArt_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbInfo;
    private System.Windows.Forms.ComboBox cbDokuArt;
    private System.Windows.Forms.Button btn1;
  }
}
