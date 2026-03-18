namespace Sped4
{
    partial class frmPrintRepViewer
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
            this.rViewer = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rViewer
            // 
            this.rViewer.AccessibilityKeyMap = null;
            this.rViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rViewer.Location = new System.Drawing.Point(0, 0);
            this.rViewer.Name = "rViewer";
            this.rViewer.Size = new System.Drawing.Size(629, 504);
            this.rViewer.TabIndex = 0;
            // 
            // frmPrintRepViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 504);
            this.Controls.Add(this.rViewer);
            this.Name = "frmPrintRepViewer";
            this.Text = "frmPrintRepViewer";
            this.ResumeLayout(false);

        }

        #endregion

        internal Telerik.ReportViewer.WinForms.ReportViewer rViewer;
    }
}