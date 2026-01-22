namespace Sped4
{
    partial class frmArchiveView
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.scMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.panArchivList = new Telerik.WinControls.UI.RadPanel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabDocumentList = new System.Windows.Forms.TabPage();
            this.dgvDocuments = new Telerik.WinControls.UI.RadGridView();
            this.tabImageList = new System.Windows.Forms.TabPage();
            this.dgvImages = new Telerik.WinControls.UI.RadGridView();
            this.menuArchivList = new Telerik.WinControls.UI.RadMenu();
            this.miBtnRefresh = new Telerik.WinControls.UI.RadMenuItem();
            this.miBtnSearch = new Telerik.WinControls.UI.RadMenuItem();
            this.colpanSearchOption = new Telerik.WinControls.UI.RadCollapsiblePanel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboSearchDataField = new System.Windows.Forms.ComboBox();
            this.nudSearchValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.clbVehicle = new System.Windows.Forms.CheckedListBox();
            this.nudSearchId = new System.Windows.Forms.NumericUpDown();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.tabFilesAndImages = new System.Windows.Forms.TabControl();
            this.tabSelectedDocument = new System.Windows.Forms.TabPage();
            this.pdfViewer = new Telerik.WinControls.UI.RadPdfViewer();
            this.radPdfViewerNavigator1 = new Telerik.WinControls.UI.RadPdfViewerNavigator();
            this.tabSelectedImage = new System.Windows.Forms.TabPage();
            this.imageEditor = new Telerik.WinControls.UI.RadImageEditor();
            this.tsArtikeldatenMenu = new Sped4.Controls.AFToolStrip();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panArchivList)).BeginInit();
            this.panArchivList.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabDocumentList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments.MasterTemplate)).BeginInit();
            this.tabImageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuArchivList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colpanSearchOption)).BeginInit();
            this.colpanSearchOption.PanelContainer.SuspendLayout();
            this.colpanSearchOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearchValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearchId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.tabFilesAndImages.SuspendLayout();
            this.tabSelectedDocument.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPdfViewerNavigator1)).BeginInit();
            this.tabSelectedImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageEditor)).BeginInit();
            this.tsArtikeldatenMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.BackColor = System.Drawing.Color.Silver;
            this.scMain.Controls.Add(this.splitPanel1);
            this.scMain.Controls.Add(this.splitPanel2);
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // 
            // 
            this.scMain.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.scMain.Size = new System.Drawing.Size(1609, 707);
            this.scMain.SplitterWidth = 6;
            this.scMain.TabIndex = 0;
            this.scMain.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.White;
            this.splitPanel1.Controls.Add(this.panArchivList);
            this.splitPanel1.Controls.Add(this.colpanSearchOption);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(565, 707);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.1475359F, 0F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-174, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "panViewData";
            // 
            // panArchivList
            // 
            this.panArchivList.BackColor = System.Drawing.Color.White;
            this.panArchivList.Controls.Add(this.tabMain);
            this.panArchivList.Controls.Add(this.menuArchivList);
            this.panArchivList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panArchivList.Location = new System.Drawing.Point(0, 124);
            this.panArchivList.Margin = new System.Windows.Forms.Padding(4);
            this.panArchivList.Name = "panArchivList";
            this.panArchivList.Size = new System.Drawing.Size(565, 583);
            this.panArchivList.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabDocumentList);
            this.tabMain.Controls.Add(this.tabImageList);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 36);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(565, 547);
            this.tabMain.TabIndex = 27;
            this.tabMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabMain_Selected);
            // 
            // tabDocumentList
            // 
            this.tabDocumentList.Controls.Add(this.dgvDocuments);
            this.tabDocumentList.Location = new System.Drawing.Point(4, 22);
            this.tabDocumentList.Name = "tabDocumentList";
            this.tabDocumentList.Padding = new System.Windows.Forms.Padding(3);
            this.tabDocumentList.Size = new System.Drawing.Size(557, 521);
            this.tabDocumentList.TabIndex = 0;
            this.tabDocumentList.Text = "Dokumente";
            this.tabDocumentList.UseVisualStyleBackColor = true;
            // 
            // dgvDocuments
            // 
            this.dgvDocuments.BackColor = System.Drawing.Color.White;
            this.dgvDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocuments.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvDocuments.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvDocuments.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvDocuments.Location = new System.Drawing.Point(3, 3);
            this.dgvDocuments.Margin = new System.Windows.Forms.Padding(8);
            // 
            // 
            // 
            this.dgvDocuments.MasterTemplate.AllowAddNewRow = false;
            this.dgvDocuments.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn1.HeaderText = "";
            gridViewTextBoxColumn1.Name = "col1";
            this.dgvDocuments.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1});
            this.dgvDocuments.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvDocuments.MasterTemplate.EnableFiltering = true;
            this.dgvDocuments.MasterTemplate.ShowFilteringRow = false;
            this.dgvDocuments.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvDocuments.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvDocuments.Name = "dgvDocuments";
            this.dgvDocuments.ReadOnly = true;
            this.dgvDocuments.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvDocuments.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 3, 240, 150);
            this.dgvDocuments.ShowHeaderCellButtons = true;
            this.dgvDocuments.Size = new System.Drawing.Size(551, 515);
            this.dgvDocuments.TabIndex = 26;
            this.dgvDocuments.ThemeName = "ControlDefault";
            this.dgvDocuments.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgvDocuments.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellClick);
            // 
            // tabImageList
            // 
            this.tabImageList.Controls.Add(this.dgvImages);
            this.tabImageList.Location = new System.Drawing.Point(4, 22);
            this.tabImageList.Name = "tabImageList";
            this.tabImageList.Padding = new System.Windows.Forms.Padding(3);
            this.tabImageList.Size = new System.Drawing.Size(630, 493);
            this.tabImageList.TabIndex = 1;
            this.tabImageList.Text = "Bilder";
            this.tabImageList.UseVisualStyleBackColor = true;
            // 
            // dgvImages
            // 
            this.dgvImages.BackColor = System.Drawing.Color.White;
            this.dgvImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImages.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvImages.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvImages.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvImages.Location = new System.Drawing.Point(3, 3);
            this.dgvImages.Margin = new System.Windows.Forms.Padding(8);
            // 
            // 
            // 
            this.dgvImages.MasterTemplate.AllowAddNewRow = false;
            this.dgvImages.MasterTemplate.AllowColumnReorder = false;
            this.dgvImages.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvImages.MasterTemplate.EnableFiltering = true;
            this.dgvImages.MasterTemplate.ShowFilteringRow = false;
            this.dgvImages.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvImages.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgvImages.Name = "dgvImages";
            this.dgvImages.ReadOnly = true;
            this.dgvImages.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvImages.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 3, 240, 150);
            this.dgvImages.ShowHeaderCellButtons = true;
            this.dgvImages.Size = new System.Drawing.Size(624, 487);
            this.dgvImages.TabIndex = 27;
            this.dgvImages.ThemeName = "ControlDefault";
            this.dgvImages.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvImages_CellClick);
            // 
            // menuArchivList
            // 
            this.menuArchivList.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.miBtnRefresh,
            this.miBtnSearch});
            this.menuArchivList.Location = new System.Drawing.Point(0, 0);
            this.menuArchivList.Margin = new System.Windows.Forms.Padding(4);
            this.menuArchivList.Name = "menuArchivList";
            this.menuArchivList.Size = new System.Drawing.Size(565, 36);
            this.menuArchivList.TabIndex = 0;
            this.menuArchivList.ThemeName = "ControlDefault";
            // 
            // miBtnRefresh
            // 
            this.miBtnRefresh.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.miBtnRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.miBtnRefresh.Name = "miBtnRefresh";
            this.miBtnRefresh.Text = "radMenuItem1";
            this.miBtnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.miBtnRefresh.Click += new System.EventHandler(this.miBtnRefresh_Click);
            // 
            // miBtnSearch
            // 
            this.miBtnSearch.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.miBtnSearch.Image = global::Sped4.Properties.Resources.selection_view_32x32;
            this.miBtnSearch.Name = "miBtnSearch";
            this.miBtnSearch.Text = "radMenuItem3";
            this.miBtnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.miBtnSearch.Click += new System.EventHandler(this.miBtnSearch_Click);
            // 
            // colpanSearchOption
            // 
            this.colpanSearchOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.colpanSearchOption.HeaderText = "Such - Einstellungen";
            this.colpanSearchOption.Location = new System.Drawing.Point(0, 0);
            this.colpanSearchOption.Margin = new System.Windows.Forms.Padding(6);
            this.colpanSearchOption.Name = "colpanSearchOption";
            this.colpanSearchOption.OwnerBoundsCache = new System.Drawing.Rectangle(0, 0, 494, 210);
            this.colpanSearchOption.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            // 
            // colpanSearchOption.PanelContainer
            // 
            this.colpanSearchOption.PanelContainer.BackColor = System.Drawing.Color.White;
            this.colpanSearchOption.PanelContainer.Controls.Add(this.label2);
            this.colpanSearchOption.PanelContainer.Controls.Add(this.comboSearchDataField);
            this.colpanSearchOption.PanelContainer.Controls.Add(this.nudSearchValue);
            this.colpanSearchOption.PanelContainer.Controls.Add(this.label1);
            this.colpanSearchOption.PanelContainer.Controls.Add(this.clbVehicle);
            this.colpanSearchOption.PanelContainer.Controls.Add(this.nudSearchId);
            this.colpanSearchOption.PanelContainer.Margin = new System.Windows.Forms.Padding(564);
            this.colpanSearchOption.PanelContainer.Size = new System.Drawing.Size(546, 82);
            this.colpanSearchOption.Size = new System.Drawing.Size(565, 124);
            this.colpanSearchOption.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(158, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Such-Datenfeld:";
            // 
            // comboSearchDataField
            // 
            this.comboSearchDataField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboSearchDataField.FormattingEnabled = true;
            this.comboSearchDataField.Items.AddRange(new object[] {
            "NotSet",
            "LvsID",
            "EingangID",
            "AusgangID",
            "RgID"});
            this.comboSearchDataField.Location = new System.Drawing.Point(152, 29);
            this.comboSearchDataField.Name = "comboSearchDataField";
            this.comboSearchDataField.Size = new System.Drawing.Size(173, 21);
            this.comboSearchDataField.TabIndex = 7;
            this.comboSearchDataField.SelectedIndexChanged += new System.EventHandler(this.comboSearchDataField_SelectedIndexChanged);
            // 
            // nudSearchValue
            // 
            this.nudSearchValue.Location = new System.Drawing.Point(26, 29);
            this.nudSearchValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudSearchValue.Name = "nudSearchValue";
            this.nudSearchValue.Size = new System.Drawing.Size(112, 20);
            this.nudSearchValue.TabIndex = 5;
            this.nudSearchValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Such-Value:";
            // 
            // clbVehicle
            // 
            this.clbVehicle.FormattingEnabled = true;
            this.clbVehicle.Items.AddRange(new object[] {
            "NotSet",
            "LKW",
            "Waggon"});
            this.clbVehicle.Location = new System.Drawing.Point(1312, 98);
            this.clbVehicle.Margin = new System.Windows.Forms.Padding(19);
            this.clbVehicle.Name = "clbVehicle";
            this.clbVehicle.Size = new System.Drawing.Size(926, 480);
            this.clbVehicle.TabIndex = 3;
            // 
            // nudSearchId
            // 
            this.nudSearchId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudSearchId.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.nudSearchId.Location = new System.Drawing.Point(152, 669);
            this.nudSearchId.Margin = new System.Windows.Forms.Padding(19);
            this.nudSearchId.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudSearchId.Name = "nudSearchId";
            this.nudSearchId.Size = new System.Drawing.Size(1025, 20);
            this.nudSearchId.TabIndex = 1;
            this.nudSearchId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.Color.White;
            this.splitPanel2.Controls.Add(this.tabFilesAndImages);
            this.splitPanel2.Location = new System.Drawing.Point(571, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel2.Size = new System.Drawing.Size(1038, 707);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.1475359F, 0F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(174, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // tabFilesAndImages
            // 
            this.tabFilesAndImages.Controls.Add(this.tabSelectedDocument);
            this.tabFilesAndImages.Controls.Add(this.tabSelectedImage);
            this.tabFilesAndImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFilesAndImages.Location = new System.Drawing.Point(0, 0);
            this.tabFilesAndImages.Name = "tabFilesAndImages";
            this.tabFilesAndImages.SelectedIndex = 0;
            this.tabFilesAndImages.Size = new System.Drawing.Size(1038, 707);
            this.tabFilesAndImages.TabIndex = 2;
            // 
            // tabSelectedDocument
            // 
            this.tabSelectedDocument.Controls.Add(this.pdfViewer);
            this.tabSelectedDocument.Controls.Add(this.radPdfViewerNavigator1);
            this.tabSelectedDocument.Location = new System.Drawing.Point(4, 22);
            this.tabSelectedDocument.Name = "tabSelectedDocument";
            this.tabSelectedDocument.Padding = new System.Windows.Forms.Padding(3);
            this.tabSelectedDocument.Size = new System.Drawing.Size(1030, 681);
            this.tabSelectedDocument.TabIndex = 0;
            this.tabSelectedDocument.Text = "Dokumente";
            this.tabSelectedDocument.UseVisualStyleBackColor = true;
            // 
            // pdfViewer
            // 
            this.pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer.Location = new System.Drawing.Point(3, 43);
            this.pdfViewer.Margin = new System.Windows.Forms.Padding(4);
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.ScaleFactor = 1.953125F;
            this.pdfViewer.Size = new System.Drawing.Size(1024, 635);
            this.pdfViewer.TabIndex = 0;
            this.pdfViewer.ThemeName = "ControlDefault";
            this.pdfViewer.ThumbnailsScaleFactor = 0.15F;
            // 
            // radPdfViewerNavigator1
            // 
            this.radPdfViewerNavigator1.AssociatedViewer = this.pdfViewer;
            this.radPdfViewerNavigator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPdfViewerNavigator1.Location = new System.Drawing.Point(3, 3);
            this.radPdfViewerNavigator1.Name = "radPdfViewerNavigator1";
            this.radPdfViewerNavigator1.Size = new System.Drawing.Size(1024, 40);
            this.radPdfViewerNavigator1.TabIndex = 1;
            this.radPdfViewerNavigator1.ThemeName = "Fluent";
            // 
            // tabSelectedImage
            // 
            this.tabSelectedImage.Controls.Add(this.imageEditor);
            this.tabSelectedImage.Location = new System.Drawing.Point(4, 22);
            this.tabSelectedImage.Name = "tabSelectedImage";
            this.tabSelectedImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabSelectedImage.Size = new System.Drawing.Size(957, 617);
            this.tabSelectedImage.TabIndex = 1;
            this.tabSelectedImage.Text = "Bilder";
            this.tabSelectedImage.UseVisualStyleBackColor = true;
            // 
            // imageEditor
            // 
            this.imageEditor.AllowDrop = true;
            this.imageEditor.BackColor = System.Drawing.Color.White;
            this.imageEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageEditor.Location = new System.Drawing.Point(3, 3);
            this.imageEditor.Name = "imageEditor";
            this.imageEditor.Size = new System.Drawing.Size(951, 611);
            this.imageEditor.TabIndex = 0;
            this.imageEditor.Text = "radImageEditor1";
            this.imageEditor.ThemeName = "ControlDefault";
            // 
            // tsArtikeldatenMenu
            // 
            this.tsArtikeldatenMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsArtikeldatenMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClose,
            this.toolStripSeparator2});
            this.tsArtikeldatenMenu.Location = new System.Drawing.Point(0, 0);
            this.tsArtikeldatenMenu.myColorFrom = System.Drawing.Color.Azure;
            this.tsArtikeldatenMenu.myColorTo = System.Drawing.Color.Blue;
            this.tsArtikeldatenMenu.myUnderlineColor = System.Drawing.Color.White;
            this.tsArtikeldatenMenu.myUnderlined = true;
            this.tsArtikeldatenMenu.Name = "tsArtikeldatenMenu";
            this.tsArtikeldatenMenu.Size = new System.Drawing.Size(1609, 27);
            this.tsArtikeldatenMenu.TabIndex = 137;
            this.tsArtikeldatenMenu.Text = "afToolStrip3";
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(24, 24);
            this.tsbtnClose.Text = "Artikelliste ausblenden!";
            this.tsbtnClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // frmArchiveView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1609, 707);
            this.Controls.Add(this.tsArtikeldatenMenu);
            this.Controls.Add(this.scMain);
            this.Name = "frmArchiveView";
            this.Text = "Dokumenten - Archiv";
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panArchivList)).EndInit();
            this.panArchivList.ResumeLayout(false);
            this.panArchivList.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabDocumentList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocuments)).EndInit();
            this.tabImageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuArchivList)).EndInit();
            this.colpanSearchOption.PanelContainer.ResumeLayout(false);
            this.colpanSearchOption.PanelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colpanSearchOption)).EndInit();
            this.colpanSearchOption.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSearchValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSearchId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.tabFilesAndImages.ResumeLayout(false);
            this.tabSelectedDocument.ResumeLayout(false);
            this.tabSelectedDocument.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPdfViewerNavigator1)).EndInit();
            this.tabSelectedImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageEditor)).EndInit();
            this.tsArtikeldatenMenu.ResumeLayout(false);
            this.tsArtikeldatenMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer scMain;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadCollapsiblePanel colpanSearchOption;
        private System.Windows.Forms.NumericUpDown nudSearchId;
        private System.Windows.Forms.CheckedListBox clbVehicle;
        private Telerik.WinControls.UI.RadPanel panArchivList;
        private Telerik.WinControls.UI.RadMenuItem miBtnRefresh;
        private Telerik.WinControls.UI.RadMenu menuArchivList;
        private Telerik.WinControls.UI.RadMenuItem miBtnSearch;
        private Telerik.WinControls.UI.RadPdfViewer pdfViewer;
        private System.Windows.Forms.NumericUpDown nudSearchValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboSearchDataField;
        private System.Windows.Forms.Label label2;
        public Telerik.WinControls.UI.RadGridView dgvDocuments;
        private Telerik.WinControls.UI.RadPdfViewerNavigator radPdfViewerNavigator1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabDocumentList;
        private System.Windows.Forms.TabPage tabImageList;
        public Telerik.WinControls.UI.RadGridView dgvImages;
        private System.Windows.Forms.TabControl tabFilesAndImages;
        private System.Windows.Forms.TabPage tabSelectedDocument;
        private System.Windows.Forms.TabPage tabSelectedImage;
        private Telerik.WinControls.UI.RadImageEditor imageEditor;
        private Controls.AFToolStrip tsArtikeldatenMenu;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
