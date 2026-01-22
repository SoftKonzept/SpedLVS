namespace Sped4
{
  partial class frmGueterArten
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
            this.components = new System.ComponentModel.Container();
            this.tbViewID = new System.Windows.Forms.TextBox();
            this.tbBezeichung = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miCloseCtr = new System.Windows.Forms.ToolStripMenuItem();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbSpeichern = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.lHoehe = new System.Windows.Forms.Label();
            this.lLaenge = new System.Windows.Forms.Label();
            this.lBreite = new System.Windows.Forms.Label();
            this.lDicke = new System.Windows.Forms.Label();
            this.nudMassAnzahl = new System.Windows.Forms.NumericUpDown();
            this.lMassAnzahl = new System.Windows.Forms.Label();
            this.tbDicke = new System.Windows.Forms.TextBox();
            this.tbLaenge = new System.Windows.Forms.TextBox();
            this.tbHoehe = new System.Windows.Forms.TextBox();
            this.tbBreite = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMassAnzahl)).BeginInit();
            this.SuspendLayout();
            // 
            // tbViewID
            // 
            this.tbViewID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbViewID.Location = new System.Drawing.Point(102, 63);
            this.tbViewID.Name = "tbViewID";
            this.tbViewID.Size = new System.Drawing.Size(189, 20);
            this.tbViewID.TabIndex = 1;
            this.tbViewID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbViewID.Validated += new System.EventHandler(this.tbViewID_Validated);
            // 
            // tbBezeichung
            // 
            this.tbBezeichung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBezeichung.Location = new System.Drawing.Point(102, 94);
            this.tbBezeichung.Name = "tbBezeichung";
            this.tbBezeichung.Size = new System.Drawing.Size(189, 20);
            this.tbBezeichung.TabIndex = 2;
            this.tbBezeichung.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbBezeichung.Validated += new System.EventHandler(this.tbBezeichung_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Suchbegriff";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(12, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "Bezeichnung";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUpdate,
            this.miExportExcel,
            this.miCloseCtr});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(68, 70);
            // 
            // miUpdate
            // 
            this.miUpdate.Name = "miUpdate";
            this.miUpdate.Size = new System.Drawing.Size(67, 22);
            // 
            // miExportExcel
            // 
            this.miExportExcel.Name = "miExportExcel";
            this.miExportExcel.Size = new System.Drawing.Size(67, 22);
            // 
            // miCloseCtr
            // 
            this.miCloseCtr.Name = "miCloseCtr";
            this.miCloseCtr.Size = new System.Drawing.Size(67, 22);
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
            this.afToolStrip1.Size = new System.Drawing.Size(303, 25);
            this.afToolStrip1.TabIndex = 67;
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
            // lHoehe
            // 
            this.lHoehe.AutoSize = true;
            this.lHoehe.ForeColor = System.Drawing.Color.DarkBlue;
            this.lHoehe.Location = new System.Drawing.Point(12, 201);
            this.lHoehe.Name = "lHoehe";
            this.lHoehe.Size = new System.Drawing.Size(33, 13);
            this.lHoehe.TabIndex = 68;
            this.lHoehe.Text = "Höhe";
            // 
            // lLaenge
            // 
            this.lLaenge.AutoSize = true;
            this.lLaenge.ForeColor = System.Drawing.Color.DarkBlue;
            this.lLaenge.Location = new System.Drawing.Point(12, 175);
            this.lLaenge.Name = "lLaenge";
            this.lLaenge.Size = new System.Drawing.Size(37, 13);
            this.lLaenge.TabIndex = 69;
            this.lLaenge.Text = "Länge";
            // 
            // lBreite
            // 
            this.lBreite.AutoSize = true;
            this.lBreite.ForeColor = System.Drawing.Color.DarkBlue;
            this.lBreite.Location = new System.Drawing.Point(12, 122);
            this.lBreite.Name = "lBreite";
            this.lBreite.Size = new System.Drawing.Size(34, 13);
            this.lBreite.TabIndex = 70;
            this.lBreite.Text = "Breite";
            // 
            // lDicke
            // 
            this.lDicke.AutoSize = true;
            this.lDicke.ForeColor = System.Drawing.Color.DarkBlue;
            this.lDicke.Location = new System.Drawing.Point(12, 148);
            this.lDicke.Name = "lDicke";
            this.lDicke.Size = new System.Drawing.Size(35, 13);
            this.lDicke.TabIndex = 71;
            this.lDicke.Text = "Dicke";
            // 
            // nudMassAnzahl
            // 
            this.nudMassAnzahl.Location = new System.Drawing.Point(171, 225);
            this.nudMassAnzahl.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudMassAnzahl.Name = "nudMassAnzahl";
            this.nudMassAnzahl.Size = new System.Drawing.Size(120, 20);
            this.nudMassAnzahl.TabIndex = 7;
            this.nudMassAnzahl.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMassAnzahl.ValueChanged += new System.EventHandler(this.nudMassAnzahl_ValueChanged);
            // 
            // lMassAnzahl
            // 
            this.lMassAnzahl.AutoSize = true;
            this.lMassAnzahl.ForeColor = System.Drawing.Color.DarkBlue;
            this.lMassAnzahl.Location = new System.Drawing.Point(12, 227);
            this.lMassAnzahl.Name = "lMassAnzahl";
            this.lMassAnzahl.Size = new System.Drawing.Size(63, 13);
            this.lMassAnzahl.TabIndex = 73;
            this.lMassAnzahl.Text = "Massanzahl";
            // 
            // tbDicke
            // 
            this.tbDicke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDicke.Location = new System.Drawing.Point(171, 146);
            this.tbDicke.Name = "tbDicke";
            this.tbDicke.Size = new System.Drawing.Size(120, 20);
            this.tbDicke.TabIndex = 4;
            this.tbDicke.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDicke.Validated += new System.EventHandler(this.tbDicke_Validated);
            // 
            // tbLaenge
            // 
            this.tbLaenge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLaenge.Location = new System.Drawing.Point(171, 173);
            this.tbLaenge.Name = "tbLaenge";
            this.tbLaenge.Size = new System.Drawing.Size(120, 20);
            this.tbLaenge.TabIndex = 5;
            this.tbLaenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbLaenge.Validated += new System.EventHandler(this.tbLaenge_Validated);
            // 
            // tbHoehe
            // 
            this.tbHoehe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHoehe.Location = new System.Drawing.Point(171, 199);
            this.tbHoehe.Name = "tbHoehe";
            this.tbHoehe.Size = new System.Drawing.Size(120, 20);
            this.tbHoehe.TabIndex = 6;
            this.tbHoehe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbHoehe.Validated += new System.EventHandler(this.tbHoehe_Validated);
            // 
            // tbBreite
            // 
            this.tbBreite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBreite.Location = new System.Drawing.Point(171, 120);
            this.tbBreite.Name = "tbBreite";
            this.tbBreite.Size = new System.Drawing.Size(120, 20);
            this.tbBreite.TabIndex = 3;
            this.tbBreite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbBreite.Validated += new System.EventHandler(this.tbBreite_Validated);
            // 
            // frmGueterArten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(303, 260);
            this.Controls.Add(this.tbBreite);
            this.Controls.Add(this.tbHoehe);
            this.Controls.Add(this.tbLaenge);
            this.Controls.Add(this.lMassAnzahl);
            this.Controls.Add(this.afToolStrip1);
            this.Controls.Add(this.nudMassAnzahl);
            this.Controls.Add(this.tbDicke);
            this.Controls.Add(this.lLaenge);
            this.Controls.Add(this.lBreite);
            this.Controls.Add(this.lDicke);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lHoehe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbViewID);
            this.Controls.Add(this.tbBezeichung);
            this.Name = "frmGueterArten";
            this.Text = "Güterarten";
            this.Load += new System.EventHandler(this.frmGueterArten_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMassAnzahl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbViewID;
    private System.Windows.Forms.TextBox tbBezeichung;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem miUpdate;
    private System.Windows.Forms.ToolStripMenuItem miExportExcel;
    private System.Windows.Forms.ToolStripMenuItem miCloseCtr;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbSpeichern;
    private System.Windows.Forms.ToolStripButton tsbtnClose;
    private System.Windows.Forms.Label lHoehe;
    private System.Windows.Forms.Label lLaenge;
    private System.Windows.Forms.Label lBreite;
    private System.Windows.Forms.Label lDicke;
    private System.Windows.Forms.NumericUpDown nudMassAnzahl;
    private System.Windows.Forms.Label lMassAnzahl;
    private System.Windows.Forms.TextBox tbDicke;
    private System.Windows.Forms.TextBox tbLaenge;
    private System.Windows.Forms.TextBox tbHoehe;
    private System.Windows.Forms.TextBox tbBreite;
  }
}
