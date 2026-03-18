namespace Sped4
{
  partial class frmAnmeldeschein
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
      this.tbText = new System.Windows.Forms.TextBox();
      this.tbAuflieger = new System.Windows.Forms.TextBox();
      this.tbZM = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.tbDocName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
      this.tsBtnSpeichern = new System.Windows.Forms.ToolStripButton();
      this.tsbClose = new System.Windows.Forms.ToolStripButton();
      this.groupBox1.SuspendLayout();
      this.afToolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tbText
      // 
      this.tbText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbText.Location = new System.Drawing.Point(111, 103);
      this.tbText.Multiline = true;
      this.tbText.Name = "tbText";
      this.tbText.Size = new System.Drawing.Size(217, 186);
      this.tbText.TabIndex = 4;
      // 
      // tbAuflieger
      // 
      this.tbAuflieger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbAuflieger.Location = new System.Drawing.Point(111, 77);
      this.tbAuflieger.Name = "tbAuflieger";
      this.tbAuflieger.Size = new System.Drawing.Size(217, 20);
      this.tbAuflieger.TabIndex = 3;
      // 
      // tbZM
      // 
      this.tbZM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbZM.Location = new System.Drawing.Point(111, 51);
      this.tbZM.Name = "tbZM";
      this.tbZM.Size = new System.Drawing.Size(215, 20);
      this.tbZM.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.ForeColor = System.Drawing.Color.DarkBlue;
      this.label2.Location = new System.Drawing.Point(13, 105);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(31, 13);
      this.label2.TabIndex = 12;
      this.label2.Text = "Text:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.ForeColor = System.Drawing.Color.DarkBlue;
      this.label3.Location = new System.Drawing.Point(13, 79);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(51, 13);
      this.label3.TabIndex = 13;
      this.label3.Text = "Auflieger:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.ForeColor = System.Drawing.Color.DarkBlue;
      this.label4.Location = new System.Drawing.Point(13, 53);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(74, 13);
      this.label4.TabIndex = 14;
      this.label4.Text = "Zugmaschine:";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.tbDocName);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.tbText);
      this.groupBox1.Controls.Add(this.tbZM);
      this.groupBox1.Controls.Add(this.tbAuflieger);
      this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
      this.groupBox1.Location = new System.Drawing.Point(12, 37);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(332, 305);
      this.groupBox1.TabIndex = 15;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Daten";
      // 
      // tbDocName
      // 
      this.tbDocName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbDocName.Location = new System.Drawing.Point(111, 25);
      this.tbDocName.Name = "tbDocName";
      this.tbDocName.Size = new System.Drawing.Size(215, 20);
      this.tbDocName.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.DarkBlue;
      this.label1.Location = new System.Drawing.Point(13, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(97, 13);
      this.label1.TabIndex = 15;
      this.label1.Text = "Dokumentenname:";
      // 
      // afToolStrip1
      // 
      this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnSpeichern,
            this.tsbClose});
      this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
      this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
      this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
      this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
      this.afToolStrip1.myUnderlined = true;
      this.afToolStrip1.Name = "afToolStrip1";
      this.afToolStrip1.Size = new System.Drawing.Size(356, 25);
      this.afToolStrip1.TabIndex = 127;
      this.afToolStrip1.Text = "afToolStrip1";
      // 
      // tsBtnSpeichern
      // 
      this.tsBtnSpeichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsBtnSpeichern.Image = global::Sped4.Properties.Resources.check;
      this.tsBtnSpeichern.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsBtnSpeichern.Name = "tsBtnSpeichern";
      this.tsBtnSpeichern.Size = new System.Drawing.Size(23, 22);
      this.tsBtnSpeichern.Text = "Fracht speichern";
      this.tsBtnSpeichern.Click += new System.EventHandler(this.tsBtnSpeichern_Click);
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
      // frmAnmeldeschein
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(356, 357);
      this.Controls.Add(this.afToolStrip1);
      this.Controls.Add(this.groupBox1);
      this.Name = "frmAnmeldeschein";
      this.Text = "Dateneingabe Avisierung";
      this.Load += new System.EventHandler(this.frmAnmeldeschein_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.afToolStrip1.ResumeLayout(false);
      this.afToolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbText;
    private System.Windows.Forms.TextBox tbAuflieger;
    private System.Windows.Forms.TextBox tbZM;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox1;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsBtnSpeichern;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.TextBox tbDocName;
    private System.Windows.Forms.Label label1;
  }
}