namespace Sped4
{
  partial class ctrRelationen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrRelationen));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miCloseCtr = new System.Windows.Forms.ToolStripMenuItem();
            this.afGrid1 = new Sped4.Controls.AFGrid();
            this.minMaxOption = new Sped4.Controls.AFMinMaxPanel();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbNeu = new System.Windows.Forms.ToolStripButton();
            this.tsbChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.lSSuchbegriff = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.afGrid1)).BeginInit();
            this.minMaxOption.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAdd,
            this.miUpdate,
            this.miExportExcel,
            this.miDelete,
            this.miCloseCtr});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 114);
            // 
            // miAdd
            // 
            this.miAdd.Name = "miAdd";
            this.miAdd.Size = new System.Drawing.Size(161, 22);
            this.miAdd.Text = "Neue Relation";
            this.miAdd.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // miUpdate
            // 
            this.miUpdate.Name = "miUpdate";
            this.miUpdate.Size = new System.Drawing.Size(161, 22);
            this.miUpdate.Text = "Relation ändern";
            this.miUpdate.Click += new System.EventHandler(this.miUpdate_Click);
            // 
            // miExportExcel
            // 
            this.miExportExcel.Name = "miExportExcel";
            this.miExportExcel.Size = new System.Drawing.Size(161, 22);
            this.miExportExcel.Text = "Excel Export";
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(161, 22);
            this.miDelete.Text = "Relation löschen";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miCloseCtr
            // 
            this.miCloseCtr.Name = "miCloseCtr";
            this.miCloseCtr.Size = new System.Drawing.Size(161, 22);
            this.miCloseCtr.Text = "Liste schliessen";
            this.miCloseCtr.Click += new System.EventHandler(this.miCloseCtr_Click);
            // 
            // afGrid1
            // 
            this.afGrid1.AllowUserToAddRows = false;
            this.afGrid1.AllowUserToDeleteRows = false;
            this.afGrid1.AllowUserToResizeRows = false;
            this.afGrid1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.afGrid1.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.afGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.afGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.afGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.afGrid1.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.afGrid1.DefaultCellStyle = dataGridViewCellStyle1;
            this.afGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.afGrid1.Location = new System.Drawing.Point(0, 87);
            this.afGrid1.MultiSelect = false;
            this.afGrid1.Name = "afGrid1";
            this.afGrid1.ReadOnly = true;
            this.afGrid1.RowHeadersVisible = false;
            this.afGrid1.RowTemplate.Height = 55;
            this.afGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.afGrid1.ShowEditingIcon = false;
            this.afGrid1.ShowRowErrors = false;
            this.afGrid1.Size = new System.Drawing.Size(395, 422);
            this.afGrid1.TabIndex = 7;
            this.afGrid1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.afGrid1_MouseClick);
            this.afGrid1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.afGrid1_MouseDoubleClick);
            // 
            // minMaxOption
            // 
            this.minMaxOption.BackColor = System.Drawing.Color.White;
            this.minMaxOption.Controls.Add(this.afToolStrip1);
            this.minMaxOption.Controls.Add(this.lSSuchbegriff);
            this.minMaxOption.Controls.Add(this.txtSearch);
            this.minMaxOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.minMaxOption.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.minMaxOption.Location = new System.Drawing.Point(0, 28);
            this.minMaxOption.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.minMaxOption.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxOption.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.minMaxOption.myText = "Optionen";
            this.minMaxOption.Name = "minMaxOption";
            this.minMaxOption.Size = new System.Drawing.Size(395, 59);
            this.minMaxOption.TabIndex = 6;
            this.minMaxOption.Text = "afMinMaxPanel1";
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNeu,
            this.tsbChange,
            this.toolStripButton3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(3, 28);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(181, 25);
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
            this.tsbNeu.Text = "Neue Güterart";
            this.tsbNeu.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // tsbChange
            // 
            this.tsbChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChange.Image = global::Sped4.Properties.Resources.form_green_edit;
            this.tsbChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChange.Name = "tsbChange";
            this.tsbChange.Size = new System.Drawing.Size(23, 22);
            this.tsbChange.Text = "Güterart ändern";
            this.tsbChange.Click += new System.EventHandler(this.miUpdate_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Sped4.Properties.Resources.refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "aktualisieren";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Relation löschen";
            this.toolStripButton2.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "schliessen";
            this.tsbClose.Click += new System.EventHandler(this.miCloseCtr_Click);
            // 
            // lSSuchbegriff
            // 
            this.lSSuchbegriff.AutoSize = true;
            this.lSSuchbegriff.Location = new System.Drawing.Point(33, 63);
            this.lSSuchbegriff.Name = "lSSuchbegriff";
            this.lSSuchbegriff.Size = new System.Drawing.Size(61, 13);
            this.lSSuchbegriff.TabIndex = 2;
            this.lSSuchbegriff.Text = "Suchbegriff";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(110, 60);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(245, 20);
            this.txtSearch.TabIndex = 3;
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
            this.afColorLabel1.myText = "Relationen";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(395, 28);
            this.afColorLabel1.TabIndex = 5;
            // 
            // ctrRelationen
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.afGrid1);
            this.Controls.Add(this.minMaxOption);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrRelationen";
            this.Size = new System.Drawing.Size(395, 509);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.afGrid1)).EndInit();
            this.minMaxOption.ResumeLayout(false);
            this.minMaxOption.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private Sped4.Controls.AFColorLabel afColorLabel1;
    private Sped4.Controls.AFMinMaxPanel minMaxOption;
    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbNeu;
    private System.Windows.Forms.ToolStripButton tsbChange;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.Label lSSuchbegriff;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem miAdd;
    private System.Windows.Forms.ToolStripMenuItem miUpdate;
    private System.Windows.Forms.ToolStripMenuItem miExportExcel;
    private System.Windows.Forms.ToolStripMenuItem miCloseCtr;
    private Sped4.Controls.AFGrid afGrid1;
    private System.Windows.Forms.ToolStripButton toolStripButton1;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripMenuItem miDelete;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
  }
}
