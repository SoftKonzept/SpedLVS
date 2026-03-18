namespace Sped4.Controls.ASNCenter
{
    partial class ctrASNArtFieldAssignSelectToCopy
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
            this.rMenu_AsnArtFieldAssign = new Telerik.WinControls.UI.RadMenu();
            this.btnRefresh = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnTakeOver = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnClose = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.dgvASNArtFieldAssignmentSelect = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.rMenu_AsnArtFieldAssign)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvASNArtFieldAssignmentSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvASNArtFieldAssignmentSelect.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // rMenu_AsnArtFieldAssign
            // 
            this.rMenu_AsnArtFieldAssign.AllItemsEqualHeight = true;
            this.rMenu_AsnArtFieldAssign.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnRefresh,
            this.btnTakeOver,
            this.btnClose});
            this.rMenu_AsnArtFieldAssign.Location = new System.Drawing.Point(0, 0);
            this.rMenu_AsnArtFieldAssign.Name = "rMenu_AsnArtFieldAssign";
            this.rMenu_AsnArtFieldAssign.Size = new System.Drawing.Size(602, 42);
            this.rMenu_AsnArtFieldAssign.TabIndex = 14;
            this.rMenu_AsnArtFieldAssign.ThemeName = "ControlDefault";
            // 
            // btnRefresh
            // 
            // 
            // 
            // 
            this.btnRefresh.ButtonElement.ToolTipText = "Refresh";
            this.btnRefresh.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnRefresh.Image = global::Sped4.Properties.Resources.refresh_24x24;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "";
            this.btnRefresh.ToolTipText = "Refresh";
            this.btnRefresh.UseCompatibleTextRendering = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRefresh.GetChildAt(2))).ToolTipText = "Refresh";
            // 
            // btnTakeOver
            // 
            this.btnTakeOver.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.FitToAvailableSize;
            // 
            // 
            // 
            this.btnTakeOver.ButtonElement.ToolTipText = "selektierte Datensätze übernehmen";
            this.btnTakeOver.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnTakeOver.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.btnTakeOver.Name = "btnTakeOver";
            this.btnTakeOver.Text = "";
            this.btnTakeOver.ToolTipText = "selektierte Datensätze übernehmen";
            this.btnTakeOver.UseCompatibleTextRendering = false;
            this.btnTakeOver.Click += new System.EventHandler(this.btnTakeOver_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnTakeOver.GetChildAt(2))).ToolTipText = "selektierte Datensätze übernehmen";
            // 
            // btnClose
            // 
            // 
            // 
            // 
            this.btnClose.ButtonElement.ToolTipText = "schließen";
            this.btnClose.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnClose.Image = global::Sped4.Properties.Resources.delete_16;
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "";
            this.btnClose.ToolTipText = "schließen";
            this.btnClose.UseCompatibleTextRendering = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(2))).ToolTipText = "schließen";
            // 
            // dgvASNArtFieldAssignmentSelect
            // 
            this.dgvASNArtFieldAssignmentSelect.BackColor = System.Drawing.Color.White;
            this.dgvASNArtFieldAssignmentSelect.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvASNArtFieldAssignmentSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvASNArtFieldAssignmentSelect.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvASNArtFieldAssignmentSelect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvASNArtFieldAssignmentSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvASNArtFieldAssignmentSelect.Location = new System.Drawing.Point(0, 42);
            // 
            // 
            // 
            this.dgvASNArtFieldAssignmentSelect.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvASNArtFieldAssignmentSelect.MasterTemplate.EnableFiltering = true;
            this.dgvASNArtFieldAssignmentSelect.MasterTemplate.ShowFilteringRow = false;
            this.dgvASNArtFieldAssignmentSelect.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvASNArtFieldAssignmentSelect.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvASNArtFieldAssignmentSelect.Name = "dgvASNArtFieldAssignmentSelect";
            this.dgvASNArtFieldAssignmentSelect.ReadOnly = true;
            this.dgvASNArtFieldAssignmentSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvASNArtFieldAssignmentSelect.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 42, 240, 150);
            this.dgvASNArtFieldAssignmentSelect.ShowHeaderCellButtons = true;
            this.dgvASNArtFieldAssignmentSelect.Size = new System.Drawing.Size(602, 399);
            this.dgvASNArtFieldAssignmentSelect.TabIndex = 34;
            this.dgvASNArtFieldAssignmentSelect.ThemeName = "ControlDefault";
            this.dgvASNArtFieldAssignmentSelect.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvASNArtFieldAssignmentSelect_CellClick);
            // 
            // ctrASNArtFieldAssignSelectToCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvASNArtFieldAssignmentSelect);
            this.Controls.Add(this.rMenu_AsnArtFieldAssign);
            this.Name = "ctrASNArtFieldAssignSelectToCopy";
            this.Size = new System.Drawing.Size(602, 441);
            this.Load += new System.EventHandler(this.ctrASNArtFieldAssignSelectToCopy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rMenu_AsnArtFieldAssign)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvASNArtFieldAssignmentSelect.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvASNArtFieldAssignmentSelect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadMenu rMenu_AsnArtFieldAssign;
        private Telerik.WinControls.UI.RadMenuButtonItem btnRefresh;
        private Telerik.WinControls.UI.RadMenuButtonItem btnTakeOver;
        private Telerik.WinControls.UI.RadMenuButtonItem btnClose;
        private Telerik.WinControls.UI.RadGridView dgvASNArtFieldAssignmentSelect;
    }
}
