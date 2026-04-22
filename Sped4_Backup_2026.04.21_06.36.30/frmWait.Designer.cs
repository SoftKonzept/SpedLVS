namespace Sped4
{
    partial class frmWait
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWait));
            this.pBoxWait = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxWait)).BeginInit();
            this.SuspendLayout();
            // 
            // pBoxWait
            // 
            this.pBoxWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBoxWait.Image = ((System.Drawing.Image)(resources.GetObject("pBoxWait.Image")));
            this.pBoxWait.Location = new System.Drawing.Point(0, 0);
            this.pBoxWait.Name = "pBoxWait";
            this.pBoxWait.Size = new System.Drawing.Size(432, 380);
            this.pBoxWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pBoxWait.TabIndex = 12;
            this.pBoxWait.TabStop = false;
            // 
            // frmWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 380);
            this.Controls.Add(this.pBoxWait);
            this.Name = "frmWait";
            this.Text = "Sped4 ";
            this.Load += new System.EventHandler(this.frmWait_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pBoxWait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pBoxWait;

    }
}