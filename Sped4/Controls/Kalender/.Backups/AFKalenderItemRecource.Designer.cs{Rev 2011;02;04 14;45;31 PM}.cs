namespace Sped4.Controls
{
    partial class AFKalenderItemRecource
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeDateTo = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDelete,
            this.miChangeDateTo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(209, 48);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(208, 22);
            this.miDelete.Text = "Recource entfernen";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miChangeDateTo
            // 
            this.miChangeDateTo.Name = "miChangeDateTo";
            this.miChangeDateTo.Size = new System.Drawing.Size(208, 22);
            this.miChangeDateTo.Text = "Recourcenendzeit ändern";
            this.miChangeDateTo.Click += new System.EventHandler(this.miChangeDateTo_Click);
            // 
            // AFKalenderItemRecource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.myBorderSize = 1;
            this.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AFKalenderItemRecource";
            this.Size = new System.Drawing.Size(207, 39);
            this.Load += new System.EventHandler(this.AFKalenderItemRecource_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AFKalenderItemRecource_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AFKalenderItemRecource_MouseDoubleClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AFKalenderItemRecource_MouseClick);
            this.MouseHover += new System.EventHandler(this.AFKalenderItemRecource_MouseHover);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripMenuItem miChangeDateTo;
    }
}
