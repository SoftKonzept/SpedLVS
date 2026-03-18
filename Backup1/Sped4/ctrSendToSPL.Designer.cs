namespace Sped4
{
   partial class ctrSendToSPL
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
         this.scPostCenterContainerMain = new Telerik.WinControls.UI.RadSplitContainer();
         this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
         this.panActionSelect = new System.Windows.Forms.Panel();
         this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
         this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
         this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
         ((System.ComponentModel.ISupportInitialize)(this.scPostCenterContainerMain)).BeginInit();
         this.scPostCenterContainerMain.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
         this.splitPanel1.SuspendLayout();
         this.panActionSelect.SuspendLayout();
         this.afToolStrip2.SuspendLayout();
         this.SuspendLayout();
         // 
         // scPostCenterContainerMain
         // 
         this.scPostCenterContainerMain.Controls.Add(this.splitPanel1);
         this.scPostCenterContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
         this.scPostCenterContainerMain.Location = new System.Drawing.Point(0, 28);
         this.scPostCenterContainerMain.Name = "scPostCenterContainerMain";
         this.scPostCenterContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // 
         // 
         this.scPostCenterContainerMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
         this.scPostCenterContainerMain.Size = new System.Drawing.Size(653, 567);
         this.scPostCenterContainerMain.SplitterWidth = 4;
         this.scPostCenterContainerMain.TabIndex = 11;
         this.scPostCenterContainerMain.TabStop = false;
         this.scPostCenterContainerMain.Text = "radSplitContainer1";
         // 
         // splitPanel1
         // 
         this.splitPanel1.Controls.Add(this.panActionSelect);
         this.splitPanel1.Location = new System.Drawing.Point(0, 0);
         this.splitPanel1.Name = "splitPanel1";
         // 
         // 
         // 
         this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
         this.splitPanel1.Size = new System.Drawing.Size(653, 567);
         this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.1175373F);
         this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -77);
         this.splitPanel1.TabIndex = 0;
         this.splitPanel1.TabStop = false;
         this.splitPanel1.Text = "splitPanel1";
         // 
         // panActionSelect
         // 
         this.panActionSelect.BackColor = System.Drawing.Color.White;
         this.panActionSelect.Controls.Add(this.afToolStrip2);
         this.panActionSelect.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panActionSelect.Location = new System.Drawing.Point(0, 0);
         this.panActionSelect.Name = "panActionSelect";
         this.panActionSelect.Size = new System.Drawing.Size(653, 567);
         this.panActionSelect.TabIndex = 0;
         // 
         // afToolStrip2
         // 
         this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClose});
         this.afToolStrip2.Location = new System.Drawing.Point(0, 0);
         this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
         this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
         this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
         this.afToolStrip2.myUnderlined = true;
         this.afToolStrip2.Name = "afToolStrip2";
         this.afToolStrip2.Size = new System.Drawing.Size(653, 25);
         this.afToolStrip2.TabIndex = 55;
         this.afToolStrip2.Text = "afToolStrip2";
         // 
         // afColorLabel1
         // 
         this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
         this.afColorLabel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.afColorLabel1.Location = new System.Drawing.Point(0, 0);
         this.afColorLabel1.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
         this.afColorLabel1.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
         this.afColorLabel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
         this.afColorLabel1.myText = "Artikel ins Sperrlager verschieben";
         this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
         this.afColorLabel1.myUnderlined = true;
         this.afColorLabel1.Name = "afColorLabel1";
         this.afColorLabel1.Size = new System.Drawing.Size(653, 28);
         this.afColorLabel1.TabIndex = 10;
         this.afColorLabel1.Text = "afColorLabel2";
         // 
         // ctrSendToSPL
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this.scPostCenterContainerMain);
         this.Controls.Add(this.afColorLabel1);
         this.Name = "ctrSendToSPL";
         this.Size = new System.Drawing.Size(653, 595);
         ((System.ComponentModel.ISupportInitialize)(this.scPostCenterContainerMain)).EndInit();
         this.scPostCenterContainerMain.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
         this.splitPanel1.ResumeLayout(false);
         this.panActionSelect.ResumeLayout(false);
         this.panActionSelect.PerformLayout();
         this.afToolStrip2.ResumeLayout(false);
         this.afToolStrip2.PerformLayout();
         this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Telerik.WinControls.UI.RadSplitContainer scPostCenterContainerMain;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private System.Windows.Forms.Panel panActionSelect;
        private Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
    }
}
