namespace Sped4
{
   partial class ctrComMailingLists
   {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Verwendete Ressourcen bereinigen.
      /// </summary>
      /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Vom Komponenten-Designer generierter Code

      /// <summary> 
      /// Erforderliche Methode für die Designerunterstützung. 
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent()
      {
            this.lbMailingLists = new System.Windows.Forms.ListBox();
            this.lbMailAdressen = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSearchA = new System.Windows.Forms.Button();
            this.tbSearchA = new System.Windows.Forms.TextBox();
            this.tbAuftraggeber = new System.Windows.Forms.TextBox();
            this.lbCombiAdr = new System.Windows.Forms.ListBox();
            this.afToolStrip4 = new Sped4.Controls.AFToolStrip();
            this.tsbtnAllToAusgang = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelArtFromAAusgang = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelAllFromAAusgang = new System.Windows.Forms.ToolStripButton();
            this.afToolStrip3 = new Sped4.Controls.AFToolStrip();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.afToolStrip4.SuspendLayout();
            this.afToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMailingLists
            // 
            this.lbMailingLists.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbMailingLists.FormattingEnabled = true;
            this.lbMailingLists.Location = new System.Drawing.Point(0, 25);
            this.lbMailingLists.Name = "lbMailingLists";
            this.lbMailingLists.Size = new System.Drawing.Size(587, 134);
            this.lbMailingLists.TabIndex = 22;
            this.lbMailingLists.SelectedValueChanged += new System.EventHandler(this.lbMailingLists_SelectedValueChanged);
            // 
            // lbMailAdressen
            // 
            this.lbMailAdressen.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbMailAdressen.FormattingEnabled = true;
            this.lbMailAdressen.Location = new System.Drawing.Point(0, 159);
            this.lbMailAdressen.Name = "lbMailAdressen";
            this.lbMailAdressen.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbMailAdressen.Size = new System.Drawing.Size(587, 134);
            this.lbMailAdressen.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.afToolStrip4);
            this.groupBox1.Controls.Add(this.lbCombiAdr);
            this.groupBox1.Controls.Add(this.afToolStrip3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 293);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 289);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bestände";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnSearchA);
            this.groupBox2.Controls.Add(this.tbSearchA);
            this.groupBox2.Controls.Add(this.tbAuftraggeber);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(176, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 245);
            this.groupBox2.TabIndex = 164;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Adresse";
            // 
            // btnSearchA
            // 
            this.btnSearchA.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchA.Location = new System.Drawing.Point(6, 28);
            this.btnSearchA.Name = "btnSearchA";
            this.btnSearchA.Size = new System.Drawing.Size(85, 22);
            this.btnSearchA.TabIndex = 160;
            this.btnSearchA.Text = "Adresse";
            this.btnSearchA.UseVisualStyleBackColor = true;
            // 
            // tbSearchA
            // 
            this.tbSearchA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchA.Location = new System.Drawing.Point(97, 28);
            this.tbSearchA.Name = "tbSearchA";
            this.tbSearchA.Size = new System.Drawing.Size(97, 20);
            this.tbSearchA.TabIndex = 161;
            this.tbSearchA.TextChanged += new System.EventHandler(this.tbSearchA_TextChanged);
            // 
            // tbAuftraggeber
            // 
            this.tbAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuftraggeber.Enabled = false;
            this.tbAuftraggeber.Location = new System.Drawing.Point(6, 56);
            this.tbAuftraggeber.Name = "tbAuftraggeber";
            this.tbAuftraggeber.ReadOnly = true;
            this.tbAuftraggeber.Size = new System.Drawing.Size(269, 20);
            this.tbAuftraggeber.TabIndex = 162;
            // 
            // lbCombiAdr
            // 
            this.lbCombiAdr.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbCombiAdr.FormattingEnabled = true;
            this.lbCombiAdr.Location = new System.Drawing.Point(3, 41);
            this.lbCombiAdr.Name = "lbCombiAdr";
            this.lbCombiAdr.Size = new System.Drawing.Size(149, 245);
            this.lbCombiAdr.TabIndex = 0;
            // 
            // afToolStrip4
            // 
            this.afToolStrip4.Dock = System.Windows.Forms.DockStyle.Left;
            this.afToolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAllToAusgang,
            this.tsbtnDelArtFromAAusgang,
            this.tsbtnDelAllFromAAusgang});
            this.afToolStrip4.Location = new System.Drawing.Point(152, 41);
            this.afToolStrip4.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip4.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip4.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip4.myUnderlined = true;
            this.afToolStrip4.Name = "afToolStrip4";
            this.afToolStrip4.Size = new System.Drawing.Size(24, 245);
            this.afToolStrip4.TabIndex = 163;
            this.afToolStrip4.Text = "afToolStrip4";
            // 
            // tsbtnAllToAusgang
            // 
            this.tsbtnAllToAusgang.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAllToAusgang.Image = global::Sped4.Properties.Resources.navigate_left2;
            this.tsbtnAllToAusgang.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAllToAusgang.Name = "tsbtnAllToAusgang";
            this.tsbtnAllToAusgang.Size = new System.Drawing.Size(21, 20);
            this.tsbtnAllToAusgang.Text = "Gewählte Adresse der Liste hinzufügen";
            this.tsbtnAllToAusgang.Click += new System.EventHandler(this.tsbtnAllToAusgang_Click);
            // 
            // tsbtnDelArtFromAAusgang
            // 
            this.tsbtnDelArtFromAAusgang.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelArtFromAAusgang.Image = global::Sped4.Properties.Resources.navigate_right;
            this.tsbtnDelArtFromAAusgang.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelArtFromAAusgang.Name = "tsbtnDelArtFromAAusgang";
            this.tsbtnDelArtFromAAusgang.Size = new System.Drawing.Size(21, 20);
            this.tsbtnDelArtFromAAusgang.Text = "markierten Bestand aus der Liste enfernen";
            this.tsbtnDelArtFromAAusgang.Click += new System.EventHandler(this.tsbtnDelArtFromAAusgang_Click);
            // 
            // tsbtnDelAllFromAAusgang
            // 
            this.tsbtnDelAllFromAAusgang.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelAllFromAAusgang.Image = global::Sped4.Properties.Resources.navigate_right2;
            this.tsbtnDelAllFromAAusgang.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelAllFromAAusgang.Name = "tsbtnDelAllFromAAusgang";
            this.tsbtnDelAllFromAAusgang.Size = new System.Drawing.Size(21, 20);
            this.tsbtnDelAllFromAAusgang.Text = "alle Bestände aus der Liste entfernen";
            this.tsbtnDelAllFromAAusgang.Visible = false;
            this.tsbtnDelAllFromAAusgang.Click += new System.EventHandler(this.tsbtnDelAllFromAAusgang_Click);
            // 
            // afToolStrip3
            // 
            this.afToolStrip3.Location = new System.Drawing.Point(3, 16);
            this.afToolStrip3.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip3.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip3.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip3.myUnderlined = true;
            this.afToolStrip3.Name = "afToolStrip3";
            this.afToolStrip3.Size = new System.Drawing.Size(581, 25);
            this.afToolStrip3.TabIndex = 1;
            this.afToolStrip3.Text = "afToolStrip3";
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSave,
            this.tsbtnClose});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(587, 25);
            this.afToolStrip2.TabIndex = 21;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSave.Text = "Speichern";
            this.tsbtnSave.Visible = false;
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "schliessen";
            // 
            // ctrComMailingLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbMailAdressen);
            this.Controls.Add(this.lbMailingLists);
            this.Controls.Add(this.afToolStrip2);
            this.Name = "ctrComMailingLists";
            this.Size = new System.Drawing.Size(587, 582);
            this.Load += new System.EventHandler(this.ctrComMailingLists_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.afToolStrip4.ResumeLayout(false);
            this.afToolStrip4.PerformLayout();
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion
      private Controls.AFToolStrip afToolStrip2;
      private System.Windows.Forms.ToolStripButton tsbtnSave;
      private System.Windows.Forms.ToolStripButton tsbtnClose;
      private System.Windows.Forms.ListBox lbMailingLists;
      private System.Windows.Forms.ListBox lbMailAdressen;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.ListBox lbCombiAdr;
      private Controls.AFToolStrip afToolStrip3;
      private System.Windows.Forms.Button btnSearchA;
      private System.Windows.Forms.TextBox tbAuftraggeber;
      private System.Windows.Forms.TextBox tbSearchA;
      private System.Windows.Forms.GroupBox groupBox2;
      private Controls.AFToolStrip afToolStrip4;
      private System.Windows.Forms.ToolStripButton tsbtnAllToAusgang;
      private System.Windows.Forms.ToolStripButton tsbtnDelArtFromAAusgang;
      private System.Windows.Forms.ToolStripButton tsbtnDelAllFromAAusgang;
   }
}
