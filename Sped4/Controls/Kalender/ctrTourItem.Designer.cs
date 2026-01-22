namespace Sped4.Controls
{
    partial class ctrTourItem
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.miUpdateEntladeZeit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDelete,
            this.miDetails,
            this.miUpdateEntladeZeit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(250, 92);

            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(249, 22);
            this.miDelete.Text = "Tour zurück in Auftragsliste";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miDetails
            // 
            this.miDetails.Name = "miDetails";
            this.miDetails.Size = new System.Drawing.Size(249, 22);
            this.miDetails.Text = "Tourdetails anzeigen";
            this.miDetails.Click += new System.EventHandler(this.miDetails_Click);
            // 
            // miUpdateEntladeZeit
            // 
            this.miUpdateEntladeZeit.Name = "miUpdateEntladeZeit";
            this.miUpdateEntladeZeit.Size = new System.Drawing.Size(249, 22);
            this.miUpdateEntladeZeit.Text = "Tour Start- und Endzeiten ändern";
            this.miUpdateEntladeZeit.Click += new System.EventHandler(this.miUpdateEntladeZeit_Click);
            // 
            // AFKalenderItemTour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.myBorderSize = 1;
            this.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AFKalenderItemTour";
            this.Size = new System.Drawing.Size(336, 150);
            this.Load += new System.EventHandler(this.AFKalenderItemTour_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AFKalenderItemTour_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AFKalenderItemTour_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AFKalenderItemTour_MouseDoubleClick);
            this.MouseHover += new System.EventHandler(this.AFKalenderItemTour_MouseHover);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripMenuItem miDetails;
        private System.Windows.Forms.ToolStripMenuItem miUpdateEntladeZeit;
    }
}
