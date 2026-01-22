namespace Sped4
{
    partial class ctrFilterSelect
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.splittFilterSelect = new System.Windows.Forms.SplitContainer();
            this.dgvFilterDaten = new Telerik.WinControls.UI.RadGridView();
            this.menuFilerDaten = new Sped4.Controls.AFToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.dgvFilter = new Telerik.WinControls.UI.RadGridView();
            this.menuFilter = new Sped4.Controls.AFToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splittFilterSelect)).BeginInit();
            this.splittFilterSelect.Panel1.SuspendLayout();
            this.splittFilterSelect.Panel2.SuspendLayout();
            this.splittFilterSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterDaten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterDaten.MasterTemplate)).BeginInit();
            this.menuFilerDaten.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter.MasterTemplate)).BeginInit();
            this.menuFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // splittFilterSelect
            // 
            this.splittFilterSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splittFilterSelect.Location = new System.Drawing.Point(0, 0);
            this.splittFilterSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splittFilterSelect.Name = "splittFilterSelect";
            this.splittFilterSelect.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splittFilterSelect.Panel1
            // 
            this.splittFilterSelect.Panel1.Controls.Add(this.dgvFilterDaten);
            this.splittFilterSelect.Panel1.Controls.Add(this.menuFilerDaten);
            // 
            // splittFilterSelect.Panel2
            // 
            this.splittFilterSelect.Panel2.Controls.Add(this.dgvFilter);
            this.splittFilterSelect.Panel2.Controls.Add(this.menuFilter);
            this.splittFilterSelect.Size = new System.Drawing.Size(879, 599);
            this.splittFilterSelect.SplitterDistance = 296;
            this.splittFilterSelect.SplitterWidth = 5;
            this.splittFilterSelect.TabIndex = 0;
            // 
            // dgvFilterDaten
            // 
            this.dgvFilterDaten.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFilterDaten.BackColor = System.Drawing.Color.White;
            this.dgvFilterDaten.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvFilterDaten.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvFilterDaten.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvFilterDaten.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvFilterDaten.Location = new System.Drawing.Point(0, 31);
            this.dgvFilterDaten.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgvFilterDaten.MasterTemplate.AllowAddNewRow = false;
            this.dgvFilterDaten.MasterTemplate.AllowDeleteRow = false;
            this.dgvFilterDaten.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvFilterDaten.MasterTemplate.EnableGrouping = false;
            this.dgvFilterDaten.MasterTemplate.ShowFilteringRow = false;
            this.dgvFilterDaten.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvFilterDaten.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvFilterDaten.Name = "dgvFilterDaten";
            this.dgvFilterDaten.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvFilterDaten.ShowHeaderCellButtons = true;
            this.dgvFilterDaten.Size = new System.Drawing.Size(824, 269);
            this.dgvFilterDaten.TabIndex = 26;
            this.dgvFilterDaten.ThemeName = "ControlDefault";
            // 
            // menuFilerDaten
            // 
            this.menuFilerDaten.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuFilerDaten.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
            this.menuFilerDaten.Location = new System.Drawing.Point(0, 0);
            this.menuFilerDaten.myColorFrom = System.Drawing.Color.Azure;
            this.menuFilerDaten.myColorTo = System.Drawing.Color.Blue;
            this.menuFilerDaten.myUnderlineColor = System.Drawing.Color.White;
            this.menuFilerDaten.myUnderlined = true;
            this.menuFilerDaten.Name = "menuFilerDaten";
            this.menuFilerDaten.Size = new System.Drawing.Size(879, 25);
            this.menuFilerDaten.TabIndex = 25;
            this.menuFilerDaten.Text = "afToolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(171, 22);
            this.toolStripLabel2.Text = "Eingabe der Filterdaten";
            // 
            // dgvFilter
            // 
            this.dgvFilter.BackColor = System.Drawing.Color.White;
            this.dgvFilter.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFilter.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvFilter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvFilter.Location = new System.Drawing.Point(0, 25);
            this.dgvFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgvFilter.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvFilter.MasterTemplate.EnableFiltering = true;
            this.dgvFilter.MasterTemplate.ShowFilteringRow = false;
            this.dgvFilter.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvFilter.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgvFilter.Name = "dgvFilter";
            this.dgvFilter.ReadOnly = true;
            this.dgvFilter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvFilter.ShowHeaderCellButtons = true;
            this.dgvFilter.Size = new System.Drawing.Size(879, 273);
            this.dgvFilter.TabIndex = 26;
            this.dgvFilter.ThemeName = "ControlDefault";
            this.dgvFilter.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvFilter_MouseDoubleClick);
            // 
            // menuFilter
            // 
            this.menuFilter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.menuFilter.Location = new System.Drawing.Point(0, 0);
            this.menuFilter.myColorFrom = System.Drawing.Color.Azure;
            this.menuFilter.myColorTo = System.Drawing.Color.Blue;
            this.menuFilter.myUnderlineColor = System.Drawing.Color.White;
            this.menuFilter.myUnderlined = true;
            this.menuFilter.Name = "menuFilter";
            this.menuFilter.Size = new System.Drawing.Size(879, 25);
            this.menuFilter.TabIndex = 25;
            this.menuFilter.Text = "afToolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(126, 22);
            this.toolStripLabel1.Text = "verfügbare Filter";
            // 
            // ctrFilterSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splittFilterSelect);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrFilterSelect";
            this.Size = new System.Drawing.Size(879, 599);
            this.splittFilterSelect.Panel1.ResumeLayout(false);
            this.splittFilterSelect.Panel1.PerformLayout();
            this.splittFilterSelect.Panel2.ResumeLayout(false);
            this.splittFilterSelect.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splittFilterSelect)).EndInit();
            this.splittFilterSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterDaten.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterDaten)).EndInit();
            this.menuFilerDaten.ResumeLayout(false);
            this.menuFilerDaten.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            this.menuFilter.ResumeLayout(false);
            this.menuFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splittFilterSelect;
        private Controls.AFToolStrip menuFilerDaten;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private Controls.AFToolStrip menuFilter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public Telerik.WinControls.UI.RadGridView dgvFilter;
        public Telerik.WinControls.UI.RadGridView dgvFilterDaten;
    }
}
