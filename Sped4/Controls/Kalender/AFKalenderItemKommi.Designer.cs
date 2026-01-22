namespace Sped4.Controls
{
    partial class AFKalenderItemKommi
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
            this.components = new System.ComponentModel.Container();
            this.miDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dokumenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // miDetails
            // 
            this.miDetails.Name = "miDetails";
            this.miDetails.Size = new System.Drawing.Size(183, 22);
            this.miDetails.Text = "Auftrag anzeigen";
            this.miDetails.Click += new System.EventHandler(this.miDetails_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDetails,
            this.dokumenteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 70);
            // 
            // dokumenteToolStripMenuItem
            // 
            this.dokumenteToolStripMenuItem.Name = "dokumenteToolStripMenuItem";
            this.dokumenteToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.dokumenteToolStripMenuItem.Text = "Dokumente erstellen";
            this.dokumenteToolStripMenuItem.Click += new System.EventHandler(this.dokumenteToolStripMenuItem_Click);
            // 
            // AFKalenderItemKommi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.myBorderSize = 1;
            this.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AFKalenderItemKommi";
            this.Size = new System.Drawing.Size(203, 110);
            this.Load += new System.EventHandler(this.AFKalenderItemKommi_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AFKalenderItemKommi_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AFKalenderItemKommi_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AFKalenderItemKommi_MouseDoubleClick);
            this.MouseHover += new System.EventHandler(this.AFKalenderItemKommi_MouseHover);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem miDetails;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dokumenteToolStripMenuItem;

    }
}
