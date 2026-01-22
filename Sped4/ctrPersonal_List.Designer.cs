namespace Sped4
{
    partial class ctrPersonal_List
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrPersonal_List));
            this.panPersonal = new System.Windows.Forms.Panel();
            this.grdList = new Sped4.Controls.AFGrid();
            this.minMaxPersonalSearch = new Sped4.Controls.AFMinMaxPanel();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbNeu = new System.Windows.Forms.ToolStripButton();
            this.tsbChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tstbAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tstbAktuell = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAnpassen = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.lSSuchbegriff = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miNeu = new System.Windows.Forms.ToolStripMenuItem();
            this.miUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.miExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miListeClose = new System.Windows.Forms.ToolStripMenuItem();
            this.panPersonal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.minMaxPersonalSearch.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPersonal
            // 
            this.panPersonal.BackColor = System.Drawing.Color.White;
            this.panPersonal.Controls.Add(this.grdList);
            this.panPersonal.Controls.Add(this.minMaxPersonalSearch);
            this.panPersonal.Controls.Add(this.afColorLabel1);
            this.panPersonal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPersonal.Location = new System.Drawing.Point(0, 0);
            this.panPersonal.Name = "panPersonal";
            this.panPersonal.Size = new System.Drawing.Size(407, 508);
            this.panPersonal.TabIndex = 0;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToResizeRows = false;
            this.grdList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grdList.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.grdList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 128);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowHeadersVisible = false;
            this.grdList.RowTemplate.Height = 55;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.ShowEditingIcon = false;
            this.grdList.ShowRowErrors = false;
            this.grdList.Size = new System.Drawing.Size(407, 380);
            this.grdList.TabIndex = 8;
            this.grdList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.grdList_MouseClick);
            this.grdList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdList_MouseDoubleClick);
            // 
            // minMaxPersonalSearch
            // 
            this.minMaxPersonalSearch.BackColor = System.Drawing.Color.White;
            this.minMaxPersonalSearch.Controls.Add(this.afToolStrip1);
            this.minMaxPersonalSearch.Controls.Add(this.lSSuchbegriff);
            this.minMaxPersonalSearch.Controls.Add(this.txtSearch);
            this.minMaxPersonalSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.minMaxPersonalSearch.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.minMaxPersonalSearch.Location = new System.Drawing.Point(0, 28);
            this.minMaxPersonalSearch.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.minMaxPersonalSearch.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxPersonalSearch.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.minMaxPersonalSearch.myText = "Optionen";
            this.minMaxPersonalSearch.Name = "minMaxPersonalSearch";
            this.minMaxPersonalSearch.Size = new System.Drawing.Size(407, 100);
            this.minMaxPersonalSearch.TabIndex = 7;
            this.minMaxPersonalSearch.Text = "afMinMaxPanel1";
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNeu,
            this.tsbChange,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripDropDownButton1,
            this.miDelete,
            this.tsbtnAnpassen,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(3, 24);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(233, 25);
            this.afToolStrip1.TabIndex = 8;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbNeu
            // 
            this.tsbNeu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNeu.Image = global::Sped4.Properties.Resources.form_green_add;
            this.tsbNeu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNeu.Name = "tsbNeu";
            this.tsbNeu.Size = new System.Drawing.Size(23, 22);
            this.tsbNeu.Text = "Neues Personal";
            this.tsbNeu.Click += new System.EventHandler(this.miNeu_Click);
            // 
            // tsbChange
            // 
            this.tsbChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChange.Image = global::Sped4.Properties.Resources.form_green_edit;
            this.tsbChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChange.Name = "tsbChange";
            this.tsbChange.Size = new System.Drawing.Size(23, 22);
            this.tsbChange.Text = "Personaldaten ändern";
            this.tsbChange.Click += new System.EventHandler(this.miUpdate_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Excel Export";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.miExcel_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Sped4.Properties.Resources.refresh;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "aktualisieren";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbAll,
            this.tstbAktuell});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "Listen";
            // 
            // tstbAll
            // 
            this.tstbAll.Name = "tstbAll";
            this.tstbAll.Size = new System.Drawing.Size(191, 22);
            this.tstbAll.Text = "Personalliste komplett";
            this.tstbAll.Click += new System.EventHandler(this.tstbAll_Click);
            // 
            // tstbAktuell
            // 
            this.tstbAktuell.Name = "tstbAktuell";
            this.tstbAktuell.Size = new System.Drawing.Size(191, 22);
            this.tstbAktuell.Text = "Personalliste aktuell";
            this.tstbAktuell.Click += new System.EventHandler(this.tstbAktuell_Click);
            // 
            // miDelete
            // 
            this.miDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.miDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.miDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(23, 22);
            this.miDelete.Text = "Personaldatensatz löschen";
            this.miDelete.Click += new System.EventHandler(this.toolStripButton3_Click);
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
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 20);
            this.tsbClose.Text = "schliessen";
            this.tsbClose.Click += new System.EventHandler(this.miListeClose_Click);
            // 
            // lSSuchbegriff
            // 
            this.lSSuchbegriff.AutoSize = true;
            this.lSSuchbegriff.Location = new System.Drawing.Point(30, 64);
            this.lSSuchbegriff.Name = "lSSuchbegriff";
            this.lSSuchbegriff.Size = new System.Drawing.Size(61, 13);
            this.lSSuchbegriff.TabIndex = 2;
            this.lSSuchbegriff.Text = "Suchbegriff";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(110, 61);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(255, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
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
            this.afColorLabel1.myText = "Personalliste [aktuell]";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(407, 28);
            this.afColorLabel1.TabIndex = 6;
            this.afColorLabel1.Text = "afColorLabel1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNeu,
            this.miUpdate,
            this.miExcel,
            this.miListeClose});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 92);
            // 
            // miNeu
            // 
            this.miNeu.Name = "miNeu";
            this.miNeu.Size = new System.Drawing.Size(189, 22);
            this.miNeu.Text = "Personal hinzufügen";
            this.miNeu.Click += new System.EventHandler(this.miNeu_Click);
            // 
            // miUpdate
            // 
            this.miUpdate.Name = "miUpdate";
            this.miUpdate.Size = new System.Drawing.Size(189, 22);
            this.miUpdate.Text = "Personaldaten ändern";
            this.miUpdate.Click += new System.EventHandler(this.miUpdate_Click_1);
            // 
            // miExcel
            // 
            this.miExcel.Name = "miExcel";
            this.miExcel.Size = new System.Drawing.Size(189, 22);
            this.miExcel.Text = "Excel Export";
            this.miExcel.Click += new System.EventHandler(this.miExcel_Click);
            // 
            // miListeClose
            // 
            this.miListeClose.Name = "miListeClose";
            this.miListeClose.Size = new System.Drawing.Size(189, 22);
            this.miListeClose.Text = "Liste schliessen";
            this.miListeClose.Click += new System.EventHandler(this.miListeClose_Click);
            // 
            // ctrPersonal_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panPersonal);
            this.Name = "ctrPersonal_List";
            this.Size = new System.Drawing.Size(407, 508);
            this.panPersonal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.minMaxPersonalSearch.ResumeLayout(false);
            this.minMaxPersonalSearch.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPersonal;
        private Sped4.Controls.AFColorLabel afColorLabel1;
        private Sped4.Controls.AFMinMaxPanel minMaxPersonalSearch;
        private Sped4.Controls.AFGrid grdList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miUpdate;
        private System.Windows.Forms.ToolStripMenuItem miNeu;
        private System.Windows.Forms.ToolStripMenuItem miListeClose;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lSSuchbegriff;
        private System.Windows.Forms.ToolStripMenuItem miExcel;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNeu;
        private System.Windows.Forms.ToolStripButton tsbChange;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem tstbAll;
        private System.Windows.Forms.ToolStripMenuItem tstbAktuell;
        private System.Windows.Forms.ToolStripButton miDelete;
        private System.Windows.Forms.ToolStripButton tsbtnAnpassen;
    }
}
