namespace Sped4
{
    partial class frmTmp
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
            this.afTSTmp = new Sped4.Controls.AFToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.afCLTmp = new Sped4.Controls.AFColorLabel();
            this.panAusgabe = new System.Windows.Forms.Panel();
            this.afTSTmp.SuspendLayout();
            this.SuspendLayout();
            // 
            // afTSTmp
            // 
            this.afTSTmp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose});
            this.afTSTmp.Location = new System.Drawing.Point(0, 28);
            this.afTSTmp.myColorFrom = System.Drawing.Color.Azure;
            this.afTSTmp.myColorTo = System.Drawing.Color.Blue;
            this.afTSTmp.myUnderlineColor = System.Drawing.Color.White;
            this.afTSTmp.myUnderlined = true;
            this.afTSTmp.Name = "afTSTmp";
            this.afTSTmp.Size = new System.Drawing.Size(378, 25);
            this.afTSTmp.TabIndex = 144;
            this.afTSTmp.Text = "afToolStrip1";
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
            // afCLTmp
            // 
            this.afCLTmp.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afCLTmp.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afCLTmp.Dock = System.Windows.Forms.DockStyle.Top;
            this.afCLTmp.Location = new System.Drawing.Point(0, 0);
            this.afCLTmp.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afCLTmp.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afCLTmp.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afCLTmp.myText = "";
            this.afCLTmp.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afCLTmp.myUnderlined = true;
            this.afCLTmp.Name = "afCLTmp";
            this.afCLTmp.Size = new System.Drawing.Size(378, 28);
            this.afCLTmp.TabIndex = 145;
            this.afCLTmp.Text = "afColorLabel1";
            // 
            // panAusgabe
            // 
            this.panAusgabe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panAusgabe.Location = new System.Drawing.Point(0, 53);
            this.panAusgabe.Name = "panAusgabe";
            this.panAusgabe.Size = new System.Drawing.Size(378, 275);
            this.panAusgabe.TabIndex = 146;
            // 
            // frmTmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(378, 328);
            this.Controls.Add(this.panAusgabe);
            this.Controls.Add(this.afTSTmp);
            this.Controls.Add(this.afCLTmp);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "frmTmp";
            this.Text = "";
            this.Load += new System.EventHandler(this.frmTmp_Load);
            this.afTSTmp.ResumeLayout(false);
            this.afTSTmp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFToolStrip afTSTmp;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private Controls.AFColorLabel afCLTmp;
        private System.Windows.Forms.Panel panAusgabe;

    }
}