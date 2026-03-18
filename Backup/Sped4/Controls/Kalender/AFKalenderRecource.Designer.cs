namespace Sped4.Controls.Kalender
{
    partial class AFKalenderRecource
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
          this.SuspendLayout();
          // 
          // AFKalenderRecource
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.BackColor = System.Drawing.Color.Transparent;
          this.ColorTo = System.Drawing.Color.Blue;
          this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.myBorderSize = 1;
          this.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.Name = "AFKalenderRecource";
          this.Size = new System.Drawing.Size(70, 25);
          this.Load += new System.EventHandler(this.AFKalenderRecource_Load);
          this.Paint += new System.Windows.Forms.PaintEventHandler(this.AFKalenderRecource_Paint);
          this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AFKalenderRecource_MouseDown);
          this.ResumeLayout(false);

        }

        #endregion
    }
}
