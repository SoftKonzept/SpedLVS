namespace Sped4.Controls.ASNCenter
{
    partial class ctrJobSelectToCopy
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition4 = new Telerik.WinControls.UI.TableViewDefinition();
            this.rMenu_Job = new Telerik.WinControls.UI.RadMenu();
            this.btnRefresh = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnTakeOver = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnClose = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.rMenu_Job)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // rMenu_Job
            // 
            this.rMenu_Job.AllItemsEqualHeight = true;
            this.rMenu_Job.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnRefresh,
            this.btnTakeOver,
            this.btnClose});
            this.rMenu_Job.Location = new System.Drawing.Point(0, 0);
            this.rMenu_Job.Name = "rMenu_Job";
            this.rMenu_Job.Size = new System.Drawing.Size(803, 42);
            this.rMenu_Job.TabIndex = 15;
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
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 42);
            // 
            // 
            // 
            this.dgv.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgv.MasterTemplate.EnableFiltering = true;
            this.dgv.MasterTemplate.ShowFilteringRow = false;
            this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition4;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 42, 240, 150);
            this.dgv.ShowHeaderCellButtons = true;
            this.dgv.Size = new System.Drawing.Size(803, 254);
            this.dgv.TabIndex = 35;
            this.dgv.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellClick);
            // 
            // ctrJobSelectToCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.rMenu_Job);
            this.Name = "ctrJobSelectToCopy";
            this.Size = new System.Drawing.Size(803, 296);
            this.Load += new System.EventHandler(this.ctrJobSelectToCopy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rMenu_Job)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadMenu rMenu_Job;
        private Telerik.WinControls.UI.RadMenuButtonItem btnRefresh;
        private Telerik.WinControls.UI.RadMenuButtonItem btnTakeOver;
        private Telerik.WinControls.UI.RadMenuButtonItem btnClose;
        private Telerik.WinControls.UI.RadGridView dgv;
    }
}
