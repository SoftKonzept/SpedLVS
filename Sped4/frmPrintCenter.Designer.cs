namespace Sped4
{
    partial class frmPrintCenter
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
            this.tsArtikeldatenMenu = new Sped4.Controls.AFToolStrip();
            this.tsbtnFrmClose = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tsArtikeldatenMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsArtikeldatenMenu
            // 
            this.tsArtikeldatenMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnFrmClose});
            this.tsArtikeldatenMenu.Location = new System.Drawing.Point(0, 0);
            this.tsArtikeldatenMenu.myColorFrom = System.Drawing.Color.Azure;
            this.tsArtikeldatenMenu.myColorTo = System.Drawing.Color.Blue;
            this.tsArtikeldatenMenu.myUnderlineColor = System.Drawing.Color.White;
            this.tsArtikeldatenMenu.myUnderlined = true;
            this.tsArtikeldatenMenu.Name = "tsArtikeldatenMenu";
            this.tsArtikeldatenMenu.Size = new System.Drawing.Size(1164, 25);
            this.tsArtikeldatenMenu.TabIndex = 170;
            this.tsArtikeldatenMenu.Text = "afToolStrip3";
            // 
            // tsbtnFrmClose
            // 
            this.tsbtnFrmClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFrmClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnFrmClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFrmClose.Name = "tsbtnFrmClose";
            this.tsbtnFrmClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnFrmClose.Text = "schliessen";
            this.tsbtnFrmClose.Click += new System.EventHandler(this.tsbtnFrmClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.AutoScrollMinSize = new System.Drawing.Size(500, 960);
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.AliceBlue;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.AutoScrollMinSize = new System.Drawing.Size(230, 700);
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.splitContainer1.Size = new System.Drawing.Size(1164, 849);
            this.splitContainer1.SplitterDistance = 590;
            this.splitContainer1.TabIndex = 171;
            // 
            // frmPrintCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1164, 874);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tsArtikeldatenMenu);
            this.Name = "frmPrintCenter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPrintCenter";
            this.Load += new System.EventHandler(this.frmPrintCenter_Load);
            this.tsArtikeldatenMenu.ResumeLayout(false);
            this.tsArtikeldatenMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sped4.Controls.AFToolStrip tsArtikeldatenMenu;
        private System.Windows.Forms.ToolStripButton tsbtnFrmClose;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}