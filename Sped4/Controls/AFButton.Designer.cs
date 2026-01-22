namespace Sped4.Controls
{
    partial class AFButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Picture
            // 
            this.Picture.BackColor = System.Drawing.Color.White;
            this.Picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Picture.Location = new System.Drawing.Point(3, 13);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(178, 84);
            this.Picture.TabIndex = 0;
            this.Picture.TabStop = false;
            this.Picture.Click += new System.EventHandler(this.Picture_Click);
            this.Picture.Paint += new System.Windows.Forms.PaintEventHandler(this.Picture_Paint);
            this.Picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseDown);
            this.Picture.MouseEnter += new System.EventHandler(this.Picture_MouseEnter);
            this.Picture.MouseLeave += new System.EventHandler(this.Picture_MouseLeave);
            this.Picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Picture_MouseUp);
            // 
            // AFButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.Picture);
            this.Name = "AFButton";
            this.Size = new System.Drawing.Size(200, 100);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AFButton_Paint);
            this.Resize += new System.EventHandler(this.AFButton_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Picture;
    }
}
