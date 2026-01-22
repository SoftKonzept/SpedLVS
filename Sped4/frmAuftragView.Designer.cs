namespace Sped4
{
  partial class frmAuftragView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuftragView));
            Telerik.WinControls.UI.GridViewImageColumn gridViewImageColumn1 = new Telerik.WinControls.UI.GridViewImageColumn();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnArtikelDetails = new System.Windows.Forms.ToolStripButton();
            this.tsbImagePlus = new System.Windows.Forms.ToolStripButton();
            this.tsbImageMinus = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRotate = new System.Windows.Forms.ToolStripButton();
            this.tsbtnScan = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAusgabe = new System.Windows.Forms.ToolStripButton();
            this.tsbtnScanDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miDocs = new System.Windows.Forms.ToolStripMenuItem();
            this.miDocAusgabe = new System.Windows.Forms.ToolStripMenuItem();
            this.scanLöschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scMainPage = new System.Windows.Forms.SplitContainer();
            this.scDocs = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.dgvImg = new Telerik.WinControls.UI.RadGridView();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.pdfViewer = new Telerik.WinControls.UI.RadPdfViewer();
            this.radPdfViewerNavigator1 = new Telerik.WinControls.UI.RadPdfViewerNavigator();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.afToolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMainPage)).BeginInit();
            this.scMainPage.Panel2.SuspendLayout();
            this.scMainPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDocs)).BeginInit();
            this.scDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImg.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPdfViewerNavigator1)).BeginInit();
            this.SuspendLayout();
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnArtikelDetails,
            this.tsbImagePlus,
            this.tsbImageMinus,
            this.tsbtnRotate,
            this.tsbtnScan,
            this.tsbtnUpdate,
            this.tsbtnAusgabe,
            this.tsbtnScanDelete,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(1562, 25);
            this.afToolStrip1.TabIndex = 143;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnArtikelDetails
            // 
            this.tsbtnArtikelDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnArtikelDetails.Image = global::Sped4.Properties.Resources.layout_left;
            this.tsbtnArtikelDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnArtikelDetails.Name = "tsbtnArtikelDetails";
            this.tsbtnArtikelDetails.Size = new System.Drawing.Size(23, 22);
            this.tsbtnArtikelDetails.Text = "Artikeldetails einblenden";
            this.tsbtnArtikelDetails.Click += new System.EventHandler(this.tsbtnArtikelDetails_Click);
            // 
            // tsbImagePlus
            // 
            this.tsbImagePlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImagePlus.Image = ((System.Drawing.Image)(resources.GetObject("tsbImagePlus.Image")));
            this.tsbImagePlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImagePlus.Name = "tsbImagePlus";
            this.tsbImagePlus.Size = new System.Drawing.Size(23, 22);
            this.tsbImagePlus.Text = "Dokument vergrößern";
            this.tsbImagePlus.Visible = false;
            this.tsbImagePlus.Click += new System.EventHandler(this.tsbImagePlus_Click);
            // 
            // tsbImageMinus
            // 
            this.tsbImageMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImageMinus.Image = ((System.Drawing.Image)(resources.GetObject("tsbImageMinus.Image")));
            this.tsbImageMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImageMinus.Name = "tsbImageMinus";
            this.tsbImageMinus.Size = new System.Drawing.Size(23, 22);
            this.tsbImageMinus.Text = "Dokument verkleinern";
            this.tsbImageMinus.Visible = false;
            this.tsbImageMinus.Click += new System.EventHandler(this.tsbImageMinus_Click);
            // 
            // tsbtnRotate
            // 
            this.tsbtnRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRotate.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRotate.Image")));
            this.tsbtnRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRotate.Name = "tsbtnRotate";
            this.tsbtnRotate.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRotate.Text = "Dokument um 90° drehen";
            this.tsbtnRotate.Visible = false;
            this.tsbtnRotate.Click += new System.EventHandler(this.tsbtnRotate_Click);
            // 
            // tsbtnScan
            // 
            this.tsbtnScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnScan.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnScan.Image")));
            this.tsbtnScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnScan.Name = "tsbtnScan";
            this.tsbtnScan.Size = new System.Drawing.Size(23, 22);
            this.tsbtnScan.Text = "Dokument hinterlegen / scannen";
            this.tsbtnScan.Click += new System.EventHandler(this.tsbtnScan_Click);
            // 
            // tsbtnUpdate
            // 
            this.tsbtnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnUpdate.Image = global::Sped4.Properties.Resources.FloppyDisk;
            this.tsbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUpdate.Name = "tsbtnUpdate";
            this.tsbtnUpdate.Size = new System.Drawing.Size(23, 22);
            this.tsbtnUpdate.Text = "Dokument Update";
            this.tsbtnUpdate.Visible = false;
            this.tsbtnUpdate.Click += new System.EventHandler(this.tsbtnUpdate_Click);
            // 
            // tsbtnAusgabe
            // 
            this.tsbtnAusgabe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAusgabe.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAusgabe.Image")));
            this.tsbtnAusgabe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAusgabe.Name = "tsbtnAusgabe";
            this.tsbtnAusgabe.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAusgabe.Text = "Dokument ausgeben";
            this.tsbtnAusgabe.Visible = false;
            this.tsbtnAusgabe.Click += new System.EventHandler(this.tsbtnAusgabe_Click);
            // 
            // tsbtnScanDelete
            // 
            this.tsbtnScanDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnScanDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnScanDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnScanDelete.Name = "tsbtnScanDelete";
            this.tsbtnScanDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnScanDelete.Text = "aktuelles Dokument löschen";
            this.tsbtnScanDelete.Click += new System.EventHandler(this.tsbtnScanDelete_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "schliessen";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDocs,
            this.scanLöschenToolStripMenuItem,
            this.speichernToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(144, 70);
            // 
            // miDocs
            // 
            this.miDocs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDocAusgabe});
            this.miDocs.Name = "miDocs";
            this.miDocs.Size = new System.Drawing.Size(143, 22);
            this.miDocs.Text = "Dokument";
            // 
            // miDocAusgabe
            // 
            this.miDocAusgabe.Name = "miDocAusgabe";
            this.miDocAusgabe.Size = new System.Drawing.Size(120, 22);
            this.miDocAusgabe.Text = "Ausgabe";
            this.miDocAusgabe.Click += new System.EventHandler(this.miDocAusgabe_Click);
            // 
            // scanLöschenToolStripMenuItem
            // 
            this.scanLöschenToolStripMenuItem.Name = "scanLöschenToolStripMenuItem";
            this.scanLöschenToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.scanLöschenToolStripMenuItem.Text = "Scan löschen";
            this.scanLöschenToolStripMenuItem.Click += new System.EventHandler(this.scanLöschenToolStripMenuItem_Click);
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.speichernToolStripMenuItem.Text = "speichern";
            // 
            // scMainPage
            // 
            this.scMainPage.BackColor = System.Drawing.Color.LightSteelBlue;
            this.scMainPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMainPage.Location = new System.Drawing.Point(0, 25);
            this.scMainPage.Name = "scMainPage";
            // 
            // scMainPage.Panel1
            // 
            this.scMainPage.Panel1.AutoScroll = true;
            this.scMainPage.Panel1.AutoScrollMinSize = new System.Drawing.Size(18, 970);
            this.scMainPage.Panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.scMainPage.Panel1MinSize = 590;
            // 
            // scMainPage.Panel2
            // 
            this.scMainPage.Panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.scMainPage.Panel2.Controls.Add(this.scDocs);
            this.scMainPage.Panel2.Controls.Add(this.vScrollBar1);
            this.scMainPage.Panel2.Controls.Add(this.hScrollBar1);
            this.scMainPage.Size = new System.Drawing.Size(1562, 849);
            this.scMainPage.SplitterDistance = 590;
            this.scMainPage.TabIndex = 149;
            // 
            // scDocs
            // 
            this.scDocs.Controls.Add(this.splitPanel1);
            this.scDocs.Controls.Add(this.splitPanel2);
            this.scDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDocs.Location = new System.Drawing.Point(0, 0);
            this.scDocs.Name = "scDocs";
            // 
            // 
            // 
            this.scDocs.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scDocs.Size = new System.Drawing.Size(953, 834);
            this.scDocs.SplitterWidth = 4;
            this.scDocs.TabIndex = 151;
            this.scDocs.TabStop = false;
            this.scDocs.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.dgvImg);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(123, 834);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.370041F, 0F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-271, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // dgvImg
            // 
            this.dgvImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImg.Location = new System.Drawing.Point(0, 0);
            // 
            // dgvImg
            // 
            gridViewImageColumn1.HeaderText = "ImageIcon";
            gridViewImageColumn1.ImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            gridViewImageColumn1.Name = "ImageIcon";
            gridViewImageColumn1.Width = 85;
            this.dgvImg.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewImageColumn1});
            this.dgvImg.MasterTemplate.ShowFilteringRow = false;
            this.dgvImg.MasterTemplate.ShowRowHeaderColumn = false;
            this.dgvImg.Name = "dgvImg";
            this.dgvImg.ShowGroupPanel = false;
            this.dgvImg.Size = new System.Drawing.Size(123, 834);
            this.dgvImg.TabIndex = 0;
            this.dgvImg.Text = "radGridView1";
            this.dgvImg.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.dgvImg_RowFormatting);
            this.dgvImg.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgvImg_CellFormatting);
            this.dgvImg.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvImg_CellClick);
            this.dgvImg.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvImg_CellDoubleClick);
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.pdfViewer);
            this.splitPanel2.Controls.Add(this.radPdfViewerNavigator1);
            this.splitPanel2.Location = new System.Drawing.Point(127, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(826, 834);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.370041F, 0F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(271, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // pdfViewer
            // 
            this.pdfViewer.AutoScroll = true;
            this.pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer.Location = new System.Drawing.Point(0, 38);
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.Size = new System.Drawing.Size(826, 796);
            this.pdfViewer.TabIndex = 150;
            this.pdfViewer.Text = "radPdfViewer1";
            this.pdfViewer.ThumbnailsScaleFactor = 0.15F;
            // 
            // radPdfViewerNavigator1
            // 
            this.radPdfViewerNavigator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPdfViewerNavigator1.Location = new System.Drawing.Point(0, 0);
            this.radPdfViewerNavigator1.Name = "radPdfViewerNavigator1";
            this.radPdfViewerNavigator1.Size = new System.Drawing.Size(826, 38);
            this.radPdfViewerNavigator1.TabIndex = 149;
            this.radPdfViewerNavigator1.Text = "radPdfViewerNavigator1";
            this.radPdfViewerNavigator1.Visible = false;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(953, 0);
            this.vScrollBar1.MinimumSize = new System.Drawing.Size(15, 400);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(15, 834);
            this.vScrollBar1.TabIndex = 148;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 834);
            this.hScrollBar1.MinimumSize = new System.Drawing.Size(500, 15);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(968, 15);
            this.hScrollBar1.TabIndex = 147;
            // 
            // frmAuftragView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1562, 874);
            this.Controls.Add(this.scMainPage);
            this.Controls.Add(this.afToolStrip1);
            this.Name = "frmAuftragView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.frmAuftragView_Load);
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.scMainPage.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMainPage)).EndInit();
            this.scMainPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDocs)).EndInit();
            this.scDocs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImg.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPdfViewerNavigator1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbAuftragView;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.ToolStripButton tsbImagePlus;
    private System.Windows.Forms.ToolStripButton tsbImageMinus;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem miDocs;
    private System.Windows.Forms.ToolStripMenuItem miDocAusgabe;
    private System.Windows.Forms.ToolStripMenuItem scanLöschenToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton tsbtnRotate;
    private System.Windows.Forms.ToolStripButton tsbtnUpdate;
    private System.Windows.Forms.ToolStripButton tsbtnAusgabe;
    private System.Windows.Forms.ToolStripButton tsbtnScanDelete;
    public System.Windows.Forms.ToolStripButton tsbtnArtikelDetails;
    public System.Windows.Forms.SplitContainer scMainPage;
    private System.Windows.Forms.HScrollBar hScrollBar1;
    private System.Windows.Forms.VScrollBar vScrollBar1;
    private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton tsbtnScan;
    private Telerik.WinControls.UI.RadPdfViewer pdfViewer;
    private Telerik.WinControls.UI.RadPdfViewerNavigator radPdfViewerNavigator1;
    private Telerik.WinControls.UI.RadSplitContainer scDocs;
    private Telerik.WinControls.UI.SplitPanel splitPanel1;
    private Telerik.WinControls.UI.SplitPanel splitPanel2;
    private Telerik.WinControls.UI.RadGridView dgvImg;
  }
}
