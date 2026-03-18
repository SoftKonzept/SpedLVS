namespace Sped4
{
    partial class ctrSUList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miStatChange = new System.Windows.Forms.ToolStripMenuItem();
            this.miDocs = new System.Windows.Forms.ToolStripMenuItem();
            this.miCloseCtr = new System.Windows.Forms.ToolStripMenuItem();
            this.grdAuftrag = new Sped4.Controls.AFGrid();
            this.minMaxADRSearch = new Sped4.Controls.AFMinMaxPanel();
            this.pbFilter = new System.Windows.Forms.PictureBox();
            this.cbSuAuswahl = new System.Windows.Forms.ComboBox();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnStatChange = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAnpassen = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.cbSearchSU = new System.Windows.Forms.CheckBox();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAuftrag)).BeginInit();
            this.minMaxADRSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilter)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miStatChange,
            this.miDocs,
            this.miCloseCtr});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 92);
            // 
            // miStatChange
            // 
            this.miStatChange.Name = "miStatChange";
            this.miStatChange.Size = new System.Drawing.Size(183, 22);
            this.miStatChange.Text = "Status ändern";
            this.miStatChange.Click += new System.EventHandler(this.ChangeStatus);
            // 
            // miDocs
            // 
            this.miDocs.Name = "miDocs";
            this.miDocs.Size = new System.Drawing.Size(183, 22);
            this.miDocs.Text = "Dokumente erstellen";
            this.miDocs.Click += new System.EventHandler(this.miDocs_Click);
            // 
            // miCloseCtr
            // 
            this.miCloseCtr.Name = "miCloseCtr";
            this.miCloseCtr.Size = new System.Drawing.Size(183, 22);
            this.miCloseCtr.Text = "Liste schliessen";
            this.miCloseCtr.Click += new System.EventHandler(this.miCloseCtr_Click);
            // 
            // grdAuftrag
            // 
            this.grdAuftrag.AllowDrop = true;
            this.grdAuftrag.AllowUserToAddRows = false;
            this.grdAuftrag.AllowUserToDeleteRows = false;
            this.grdAuftrag.AllowUserToResizeRows = false;
            this.grdAuftrag.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grdAuftrag.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.grdAuftrag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdAuftrag.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grdAuftrag.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAuftrag.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAuftrag.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdAuftrag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAuftrag.Location = new System.Drawing.Point(0, 128);
            this.grdAuftrag.MultiSelect = false;
            this.grdAuftrag.Name = "grdAuftrag";
            this.grdAuftrag.ReadOnly = true;
            this.grdAuftrag.RowHeadersVisible = false;
            this.grdAuftrag.RowTemplate.Height = 55;
            this.grdAuftrag.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAuftrag.ShowEditingIcon = false;
            this.grdAuftrag.ShowRowErrors = false;
            this.grdAuftrag.Size = new System.Drawing.Size(407, 377);
            this.grdAuftrag.TabIndex = 7;
            this.grdAuftrag.DoubleClick += new System.EventHandler(this.grdAuftrag_DoubleClick);
            this.grdAuftrag.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdAuftrag_MouseDoubleClick);
            this.grdAuftrag.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdAuftrag_CellFormatting);
            this.grdAuftrag.DragEnter += new System.Windows.Forms.DragEventHandler(this.grdAuftrag_DragEnter);
            this.grdAuftrag.DataSourceChanged += new System.EventHandler(this.grdAuftrag_DataSourceChanged);
            this.grdAuftrag.DragDrop += new System.Windows.Forms.DragEventHandler(this.grdAuftrag_DragDrop);
            // 
            // minMaxADRSearch
            // 
            this.minMaxADRSearch.BackColor = System.Drawing.Color.White;
            this.minMaxADRSearch.Controls.Add(this.pbFilter);
            this.minMaxADRSearch.Controls.Add(this.cbSuAuswahl);
            this.minMaxADRSearch.Controls.Add(this.afToolStrip1);
            this.minMaxADRSearch.Controls.Add(this.cbSearchSU);
            this.minMaxADRSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.minMaxADRSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.minMaxADRSearch.Location = new System.Drawing.Point(0, 28);
            this.minMaxADRSearch.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.minMaxADRSearch.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxADRSearch.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.minMaxADRSearch.myText = "Optionen";
            this.minMaxADRSearch.Name = "minMaxADRSearch";
            this.minMaxADRSearch.Size = new System.Drawing.Size(407, 100);
            this.minMaxADRSearch.TabIndex = 6;
            this.minMaxADRSearch.Text = "afMinMaxPanel1";
            // 
            // pbFilter
            // 
            this.pbFilter.Enabled = false;
            this.pbFilter.Image = global::Sped4.Properties.Resources.magnifying_glass;
            this.pbFilter.Location = new System.Drawing.Point(374, 58);
            this.pbFilter.Name = "pbFilter";
            this.pbFilter.Size = new System.Drawing.Size(24, 24);
            this.pbFilter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFilter.TabIndex = 137;
            this.pbFilter.TabStop = false;
            this.pbFilter.Click += new System.EventHandler(this.pbFilter_Click);
            // 
            // cbSuAuswahl
            // 
            this.cbSuAuswahl.AllowDrop = true;
            this.cbSuAuswahl.Enabled = false;
            this.cbSuAuswahl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSuAuswahl.FormattingEnabled = true;
            this.cbSuAuswahl.Location = new System.Drawing.Point(180, 61);
            this.cbSuAuswahl.Name = "cbSuAuswahl";
            this.cbSuAuswahl.Size = new System.Drawing.Size(188, 21);
            this.cbSuAuswahl.TabIndex = 10;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.toolStripButton1,
            this.tsbtnStatChange,
            this.tsbtnAnpassen,
            this.tsbDelete,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(3, 28);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(150, 25);
            this.afToolStrip1.TabIndex = 8;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::Sped4.Properties.Resources.RefreshDocViewHS;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbRefresh.Text = "Liste aktualisieren";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Sped4.Properties.Resources.calendar_31;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbtnStatChange
            // 
            this.tsbtnStatChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnStatChange.Image = global::Sped4.Properties.Resources.preferences1;
            this.tsbtnStatChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStatChange.Name = "tsbtnStatChange";
            this.tsbtnStatChange.Size = new System.Drawing.Size(23, 22);
            this.tsbtnStatChange.Text = "Status ändern";
            this.tsbtnStatChange.Click += new System.EventHandler(this.tsbtnStatChange_Click);
            // 
            // tsbtnAnpassen
            // 
            this.tsbtnAnpassen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAnpassen.Image = global::Sped4.Properties.Resources.PrintSetupHS;
            this.tsbtnAnpassen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAnpassen.Name = "tsbtnAnpassen";
            this.tsbtnAnpassen.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAnpassen.Text = "Fensterbreite optimieren";
            this.tsbtnAnpassen.Click += new System.EventHandler(this.tsbtnAnpassen_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbDelete.Text = "Auftrag an Subunternehmer stornieren";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
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
            // cbSearchSU
            // 
            this.cbSearchSU.AutoSize = true;
            this.cbSearchSU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSearchSU.Location = new System.Drawing.Point(19, 65);
            this.cbSearchSU.Name = "cbSearchSU";
            this.cbSearchSU.Size = new System.Drawing.Size(155, 17);
            this.cbSearchSU.TabIndex = 4;
            this.cbSearchSU.Text = "Subunternehmer selektieren";
            this.cbSearchSU.UseVisualStyleBackColor = true;
            this.cbSearchSU.CheckedChanged += new System.EventHandler(this.cbSearchSU_CheckedChanged);
            // 
            // afColorLabel1
            // 
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afColorLabel1.Location = new System.Drawing.Point(0, 0);
            this.afColorLabel1.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afColorLabel1.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afColorLabel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afColorLabel1.myText = "Subunternehmer";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(407, 28);
            this.afColorLabel1.TabIndex = 5;
            this.afColorLabel1.Text = "afColorLabel1";
            // 
            // ctrSUList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grdAuftrag);
            this.Controls.Add(this.minMaxADRSearch);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrSUList";
            this.Size = new System.Drawing.Size(407, 505);
            this.Load += new System.EventHandler(this.ctrSUList_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAuftrag)).EndInit();
            this.minMaxADRSearch.ResumeLayout(false);
            this.minMaxADRSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilter)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Sped4.Controls.AFColorLabel afColorLabel1;
        private Sped4.Controls.AFMinMaxPanel minMaxADRSearch;
        private System.Windows.Forms.CheckBox cbSearchSU;
        private System.Windows.Forms.ComboBox cbSuAuswahl;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnAnpassen;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        public Sped4.Controls.AFGrid grdAuftrag;
        private System.Windows.Forms.PictureBox pbFilter;
        private System.Windows.Forms.ToolStripButton tsbtnStatChange;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miStatChange;
        private System.Windows.Forms.ToolStripMenuItem miDocs;
        private System.Windows.Forms.ToolStripMenuItem miCloseCtr;

    }
}
