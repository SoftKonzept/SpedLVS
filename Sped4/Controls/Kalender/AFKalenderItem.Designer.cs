namespace Sped4.Controls
{
    partial class AFKalenderItem
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
            this.SuspendLayout();
            // 
            // AFKalenderItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "AFKalenderItem";
            this.Size = new System.Drawing.Size(282, 56);
            this.Load += new System.EventHandler(this.AFKalenderKommi_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AFKalenderKommi_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AFKalenderKommi_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AFKalenderKommi_MouseDown);
            this.Resize += new System.EventHandler(this.AFKalenderKommi_Resize);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AFKalenderKommi_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
