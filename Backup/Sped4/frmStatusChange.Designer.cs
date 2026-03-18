namespace Sped4
{
  partial class frmStatusChange
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.dtp_NewDate = new System.Windows.Forms.DateTimePicker();
      this.cbBezahlt = new System.Windows.Forms.CheckBox();
      this.cbBerechnet = new System.Windows.Forms.CheckBox();
      this.cbDisponiert = new System.Windows.Forms.CheckBox();
      this.lAuftragPos = new System.Windows.Forms.Label();
      this.lAuftrag = new System.Windows.Forms.Label();
      this.cbDoksUnvoll = new System.Windows.Forms.CheckBox();
      this.cbFreigabeBerechnung = new System.Windows.Forms.CheckBox();
      this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
      this.tsbtnSavePrint = new System.Windows.Forms.ToolStripButton();
      this.tsbClose = new System.Windows.Forms.ToolStripButton();
      this.groupBox1.SuspendLayout();
      this.afToolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.dtp_NewDate);
      this.groupBox1.Controls.Add(this.cbBezahlt);
      this.groupBox1.Controls.Add(this.cbBerechnet);
      this.groupBox1.Controls.Add(this.cbDisponiert);
      this.groupBox1.Controls.Add(this.lAuftragPos);
      this.groupBox1.Controls.Add(this.lAuftrag);
      this.groupBox1.Controls.Add(this.cbDoksUnvoll);
      this.groupBox1.Controls.Add(this.cbFreigabeBerechnung);
      this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
      this.groupBox1.Location = new System.Drawing.Point(12, 28);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(422, 253);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Status ändern ";
      // 
      // dtp_NewDate
      // 
      this.dtp_NewDate.Location = new System.Drawing.Point(53, 212);
      this.dtp_NewDate.Name = "dtp_NewDate";
      this.dtp_NewDate.Size = new System.Drawing.Size(200, 20);
      this.dtp_NewDate.TabIndex = 57;
      // 
      // cbBezahlt
      // 
      this.cbBezahlt.AutoSize = true;
      this.cbBezahlt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbBezahlt.Location = new System.Drawing.Point(37, 189);
      this.cbBezahlt.Name = "cbBezahlt";
      this.cbBezahlt.Size = new System.Drawing.Size(262, 17);
      this.cbBezahlt.TabIndex = 6;
      this.cbBezahlt.Text = "Rechnung wurde bezahlt - Auftrag abgeschlossen!";
      this.cbBezahlt.UseVisualStyleBackColor = true;
      this.cbBezahlt.CheckedChanged += new System.EventHandler(this.cbBezahlt_CheckedChanged);
      // 
      // cbBerechnet
      // 
      this.cbBerechnet.AutoSize = true;
      this.cbBerechnet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbBerechnet.Location = new System.Drawing.Point(37, 156);
      this.cbBerechnet.Name = "cbBerechnet";
      this.cbBerechnet.Size = new System.Drawing.Size(195, 17);
      this.cbBerechnet.TabIndex = 5;
      this.cbBerechnet.Text = "Auftrag wurde an Kunden berechnet";
      this.cbBerechnet.UseVisualStyleBackColor = true;
      this.cbBerechnet.CheckedChanged += new System.EventHandler(this.cbBerechnet_CheckedChanged);
      // 
      // cbDisponiert
      // 
      this.cbDisponiert.AutoSize = true;
      this.cbDisponiert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbDisponiert.Location = new System.Drawing.Point(37, 69);
      this.cbDisponiert.Name = "cbDisponiert";
      this.cbDisponiert.Size = new System.Drawing.Size(12, 11);
      this.cbDisponiert.TabIndex = 4;
      this.cbDisponiert.UseVisualStyleBackColor = true;
      this.cbDisponiert.CheckedChanged += new System.EventHandler(this.cbDisponiert_CheckedChanged);
      // 
      // lAuftragPos
      // 
      this.lAuftragPos.AutoSize = true;
      this.lAuftragPos.Location = new System.Drawing.Point(14, 43);
      this.lAuftragPos.Name = "lAuftragPos";
      this.lAuftragPos.Size = new System.Drawing.Size(35, 13);
      this.lAuftragPos.TabIndex = 3;
      this.lAuftragPos.Text = "label1";
      // 
      // lAuftrag
      // 
      this.lAuftrag.AutoSize = true;
      this.lAuftrag.Location = new System.Drawing.Point(14, 26);
      this.lAuftrag.Name = "lAuftrag";
      this.lAuftrag.Size = new System.Drawing.Size(35, 13);
      this.lAuftrag.TabIndex = 2;
      this.lAuftrag.Text = "label1";
      // 
      // cbDoksUnvoll
      // 
      this.cbDoksUnvoll.AutoSize = true;
      this.cbDoksUnvoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbDoksUnvoll.Location = new System.Drawing.Point(37, 95);
      this.cbDoksUnvoll.Name = "cbDoksUnvoll";
      this.cbDoksUnvoll.Size = new System.Drawing.Size(12, 11);
      this.cbDoksUnvoll.TabIndex = 1;
      this.cbDoksUnvoll.UseVisualStyleBackColor = true;
      this.cbDoksUnvoll.CheckedChanged += new System.EventHandler(this.cbDoksUnvoll_CheckedChanged);
      // 
      // cbFreigabeBerechnung
      // 
      this.cbFreigabeBerechnung.AutoSize = true;
      this.cbFreigabeBerechnung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.cbFreigabeBerechnung.Location = new System.Drawing.Point(37, 125);
      this.cbFreigabeBerechnung.Name = "cbFreigabeBerechnung";
      this.cbFreigabeBerechnung.Size = new System.Drawing.Size(12, 11);
      this.cbFreigabeBerechnung.TabIndex = 0;
      this.cbFreigabeBerechnung.UseVisualStyleBackColor = true;
      this.cbFreigabeBerechnung.CheckedChanged += new System.EventHandler(this.cbFreigabeBerechnung_CheckedChanged);
      // 
      // afToolStrip1
      // 
      this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSavePrint,
            this.tsbClose});
      this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
      this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
      this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
      this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
      this.afToolStrip1.myUnderlined = true;
      this.afToolStrip1.Name = "afToolStrip1";
      this.afToolStrip1.Size = new System.Drawing.Size(446, 25);
      this.afToolStrip1.TabIndex = 126;
      this.afToolStrip1.Text = "afToolStrip1";
      // 
      // tsbtnSavePrint
      // 
      this.tsbtnSavePrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbtnSavePrint.Image = global::Sped4.Properties.Resources.check;
      this.tsbtnSavePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbtnSavePrint.Name = "tsbtnSavePrint";
      this.tsbtnSavePrint.Size = new System.Drawing.Size(23, 22);
      this.tsbtnSavePrint.Text = "Rechnung / Gutschrift speichern und drucken";
      this.tsbtnSavePrint.Click += new System.EventHandler(this.tsbtnSavePrint_Click);
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
      // frmStatusChange
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(446, 292);
      this.Controls.Add(this.afToolStrip1);
      this.Controls.Add(this.groupBox1);
      this.Name = "frmStatusChange";
      this.Text = "Aktualisierung Status";
      this.Load += new System.EventHandler(this.frmStatusChange_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.afToolStrip1.ResumeLayout(false);
      this.afToolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.CheckBox cbDoksUnvoll;
    private System.Windows.Forms.CheckBox cbFreigabeBerechnung;
    private System.Windows.Forms.Label lAuftragPos;
    private System.Windows.Forms.Label lAuftrag;
    private System.Windows.Forms.CheckBox cbDisponiert;
    private System.Windows.Forms.CheckBox cbBerechnet;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbtnSavePrint;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.CheckBox cbBezahlt;
    private System.Windows.Forms.DateTimePicker dtp_NewDate;
  }
}
