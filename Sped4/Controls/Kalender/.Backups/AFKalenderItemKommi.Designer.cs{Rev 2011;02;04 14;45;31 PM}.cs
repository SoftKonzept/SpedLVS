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
          this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
          this.miDetails = new System.Windows.Forms.ToolStripMenuItem();
          this.miUpdateEntladeZeit = new System.Windows.Forms.ToolStripMenuItem();
          this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
          this.dokumenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.miAbholschein = new System.Windows.Forms.ToolStripMenuItem();
          this.miLieferschein = new System.Windows.Forms.ToolStripMenuItem();
          this.contextMenuStrip1.SuspendLayout();
          this.SuspendLayout();
          // 
          // miDelete
          // 
          this.miDelete.Name = "miDelete";
          this.miDelete.Size = new System.Drawing.Size(216, 22);
          this.miDelete.Text = "zurück in Auftragsliste";
          this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
          // 
          // miDetails
          // 
          this.miDetails.Name = "miDetails";
          this.miDetails.Size = new System.Drawing.Size(216, 22);
          this.miDetails.Text = "Auftrag anzeigen";
          this.miDetails.Click += new System.EventHandler(this.miDetails_Click);
          // 
          // miUpdateEntladeZeit
          // 
          this.miUpdateEntladeZeit.Name = "miUpdateEntladeZeit";
          this.miUpdateEntladeZeit.Size = new System.Drawing.Size(216, 22);
          this.miUpdateEntladeZeit.Text = "Entladezeit / Status ändern";
          this.miUpdateEntladeZeit.Click += new System.EventHandler(this.miUpdateEntladeZeit_Click);
          // 
          // contextMenuStrip1
          // 
          this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDelete,
            this.miDetails,
            this.miUpdateEntladeZeit,
            this.dokumenteToolStripMenuItem});
          this.contextMenuStrip1.Name = "contextMenuStrip1";
          this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
          this.contextMenuStrip1.Size = new System.Drawing.Size(217, 114);
          // 
          // dokumenteToolStripMenuItem
          // 
          this.dokumenteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbholschein,
            this.miLieferschein});
          this.dokumenteToolStripMenuItem.Name = "dokumenteToolStripMenuItem";
          this.dokumenteToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
          this.dokumenteToolStripMenuItem.Text = "Dokumente";
          // 
          // miAbholschein
          // 
          this.miAbholschein.Name = "miAbholschein";
          this.miAbholschein.Size = new System.Drawing.Size(152, 22);
          this.miAbholschein.Text = "Abholschein";
          this.miAbholschein.Click += new System.EventHandler(this.miAbholschein_Click);
          // 
          // miLieferschein
          // 
          this.miLieferschein.Name = "miLieferschein";
          this.miLieferschein.Size = new System.Drawing.Size(152, 22);
          this.miLieferschein.Text = "Lieferscheine";
          this.miLieferschein.Click += new System.EventHandler(this.miLieferschein_Click);
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
          this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AFKalenderItemKommi_MouseDoubleClick);
          this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AFKalenderItemKommi_MouseClick);
          this.MouseHover += new System.EventHandler(this.AFKalenderItemKommi_MouseHover);
          this.contextMenuStrip1.ResumeLayout(false);
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripMenuItem miDetails;
        private System.Windows.Forms.ToolStripMenuItem miUpdateEntladeZeit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dokumenteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miAbholschein;
        private System.Windows.Forms.ToolStripMenuItem miLieferschein;

    }
}
