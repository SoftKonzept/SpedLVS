namespace Sped4
{
   partial class ctrComunicatorLog
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrComunicatorLog));
         this.afCLEingang = new Sped4.Controls.AFColorLabel();
         this.radListView1 = new Telerik.WinControls.UI.RadListView();
         this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
         this.tsbtnComplete = new System.Windows.Forms.ToolStripButton();
         this.tsbtnASNRead = new System.Windows.Forms.ToolStripButton();
         this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
         this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
         this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
         this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
         ((System.ComponentModel.ISupportInitialize)(this.radListView1)).BeginInit();
         this.afToolStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // afCLEingang
         // 
         this.afCLEingang.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.afCLEingang.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.afCLEingang.Dock = System.Windows.Forms.DockStyle.Top;
         this.afCLEingang.Location = new System.Drawing.Point(0, 0);
         this.afCLEingang.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
         this.afCLEingang.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
         this.afCLEingang.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
         this.afCLEingang.myText = "Communicator Log";
         this.afCLEingang.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
         this.afCLEingang.myUnderlined = true;
         this.afCLEingang.Name = "afCLEingang";
         this.afCLEingang.Size = new System.Drawing.Size(301, 28);
         this.afCLEingang.TabIndex = 5;
         this.afCLEingang.TextAlign = System.Drawing.ContentAlignment.TopCenter;
         this.afCLEingang.Click += new System.EventHandler(this.afCLEingang_Click);
         // 
         // radListView1
         // 
         this.radListView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.radListView1.Location = new System.Drawing.Point(0, 53);
         this.radListView1.Name = "radListView1";
         this.radListView1.Size = new System.Drawing.Size(301, 687);
         this.radListView1.TabIndex = 6;
         this.radListView1.Text = "radListView1";
         // 
         // afToolStrip1
         // 
         this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnComplete,
            this.tsbtnASNRead,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
         this.afToolStrip1.Location = new System.Drawing.Point(0, 28);
         this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
         this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
         this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
         this.afToolStrip1.myUnderlined = true;
         this.afToolStrip1.Name = "afToolStrip1";
         this.afToolStrip1.Size = new System.Drawing.Size(301, 25);
         this.afToolStrip1.TabIndex = 7;
         this.afToolStrip1.Text = "afToolStrip1";
         // 
         // tsbtnComplete
         // 
         this.tsbtnComplete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbtnComplete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnComplete.Image")));
         this.tsbtnComplete.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbtnComplete.Name = "tsbtnComplete";
         this.tsbtnComplete.Size = new System.Drawing.Size(23, 22);
         this.tsbtnComplete.Text = "toolStripButton1";
         // 
         // tsbtnASNRead
         // 
         this.tsbtnASNRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tsbtnASNRead.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnASNRead.Image")));
         this.tsbtnASNRead.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.tsbtnASNRead.Name = "tsbtnASNRead";
         this.tsbtnASNRead.Size = new System.Drawing.Size(23, 22);
         this.tsbtnASNRead.Text = "toolStripButton2";
         // 
         // toolStripButton3
         // 
         this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
         this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton3.Name = "toolStripButton3";
         this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton3.Text = "toolStripButton3";
         // 
         // toolStripButton4
         // 
         this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
         this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton4.Name = "toolStripButton4";
         this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton4.Text = "toolStripButton4";
         // 
         // toolStripButton5
         // 
         this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
         this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton5.Name = "toolStripButton5";
         this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton5.Text = "toolStripButton5";
         // 
         // toolStripButton6
         // 
         this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
         this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.toolStripButton6.Name = "toolStripButton6";
         this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
         this.toolStripButton6.Text = "toolStripButton6";
         // 
         // ctrComunicatorLog
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.radListView1);
         this.Controls.Add(this.afToolStrip1);
         this.Controls.Add(this.afCLEingang);
         this.Name = "ctrComunicatorLog";
         this.Size = new System.Drawing.Size(301, 740);
         ((System.ComponentModel.ISupportInitialize)(this.radListView1)).EndInit();
         this.afToolStrip1.ResumeLayout(false);
         this.afToolStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private Controls.AFColorLabel afCLEingang;
      private Telerik.WinControls.UI.RadListView radListView1;
      private Controls.AFToolStrip afToolStrip1;
      private System.Windows.Forms.ToolStripButton tsbtnComplete;
      private System.Windows.Forms.ToolStripButton tsbtnASNRead;
      private System.Windows.Forms.ToolStripButton toolStripButton3;
      private System.Windows.Forms.ToolStripButton toolStripButton4;
      private System.Windows.Forms.ToolStripButton toolStripButton5;
      private System.Windows.Forms.ToolStripButton toolStripButton6;

   }
}
