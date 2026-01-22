namespace Sped4.Controls.ASNCenter
{
    partial class ctrASNActionSelectToCopy
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
            this.rMenu_VDAClientOutEdit = new Telerik.WinControls.UI.RadMenu();
            this.btnRefresh = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnTakeOver = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnClose = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.scMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.panAdrEmp = new Telerik.WinControls.UI.SplitPanel();
            this.nudAdrIdDirect = new System.Windows.Forms.NumericUpDown();
            this.tbSearchE = new System.Windows.Forms.TextBox();
            this.tbEmpfaenger = new System.Windows.Forms.TextBox();
            this.btnSearchE = new System.Windows.Forms.Button();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            ((System.ComponentModel.ISupportInitialize)(this.rMenu_VDAClientOutEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panAdrEmp)).BeginInit();
            this.panAdrEmp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rMenu_VDAClientOutEdit
            // 
            this.rMenu_VDAClientOutEdit.AllItemsEqualHeight = true;
            this.rMenu_VDAClientOutEdit.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnRefresh,
            this.btnTakeOver,
            this.btnClose});
            this.rMenu_VDAClientOutEdit.Location = new System.Drawing.Point(0, 0);
            this.rMenu_VDAClientOutEdit.Name = "rMenu_VDAClientOutEdit";
            this.rMenu_VDAClientOutEdit.Size = new System.Drawing.Size(644, 42);
            this.rMenu_VDAClientOutEdit.TabIndex = 15;
            this.rMenu_VDAClientOutEdit.ThemeName = "ControlDefault";
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
            this.dgv.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgv.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgv.MasterTemplate.EnableFiltering = true;
            this.dgv.MasterTemplate.ShowFilteringRow = false;
            this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.dgv.ShowHeaderCellButtons = true;
            this.dgv.Size = new System.Drawing.Size(644, 324);
            this.dgv.TabIndex = 35;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellClick);
            // 
            // scMain
            // 
            this.scMain.Controls.Add(this.panAdrEmp);
            this.scMain.Controls.Add(this.splitPanel2);
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 42);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.scMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scMain.Size = new System.Drawing.Size(644, 407);
            this.scMain.SplitterWidth = 8;
            this.scMain.TabIndex = 36;
            this.scMain.TabStop = false;
            // 
            // panAdrEmp
            // 
            this.panAdrEmp.BackColor = System.Drawing.Color.White;
            this.panAdrEmp.Controls.Add(this.nudAdrIdDirect);
            this.panAdrEmp.Controls.Add(this.tbSearchE);
            this.panAdrEmp.Controls.Add(this.tbEmpfaenger);
            this.panAdrEmp.Controls.Add(this.btnSearchE);
            this.panAdrEmp.Location = new System.Drawing.Point(0, 0);
            this.panAdrEmp.Name = "panAdrEmp";
            // 
            // 
            // 
            this.panAdrEmp.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panAdrEmp.Size = new System.Drawing.Size(644, 75);
            this.panAdrEmp.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.3114144F);
            this.panAdrEmp.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -126);
            this.panAdrEmp.TabIndex = 0;
            this.panAdrEmp.TabStop = false;
            // 
            // nudAdrIdDirect
            // 
            this.nudAdrIdDirect.Location = new System.Drawing.Point(118, 20);
            this.nudAdrIdDirect.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudAdrIdDirect.Name = "nudAdrIdDirect";
            this.nudAdrIdDirect.Size = new System.Drawing.Size(89, 20);
            this.nudAdrIdDirect.TabIndex = 230;
            this.nudAdrIdDirect.Leave += new System.EventHandler(this.nudAdrIdDirect_Leave);
            // 
            // tbSearchE
            // 
            this.tbSearchE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchE.Location = new System.Drawing.Point(213, 20);
            this.tbSearchE.Name = "tbSearchE";
            this.tbSearchE.Size = new System.Drawing.Size(200, 20);
            this.tbSearchE.TabIndex = 228;
            // 
            // tbEmpfaenger
            // 
            this.tbEmpfaenger.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEmpfaenger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEmpfaenger.Enabled = false;
            this.tbEmpfaenger.Location = new System.Drawing.Point(23, 45);
            this.tbEmpfaenger.Name = "tbEmpfaenger";
            this.tbEmpfaenger.ReadOnly = true;
            this.tbEmpfaenger.Size = new System.Drawing.Size(390, 20);
            this.tbEmpfaenger.TabIndex = 229;
            // 
            // btnSearchE
            // 
            this.btnSearchE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchE.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchE.Location = new System.Drawing.Point(23, 17);
            this.btnSearchE.Name = "btnSearchE";
            this.btnSearchE.Size = new System.Drawing.Size(89, 22);
            this.btnSearchE.TabIndex = 227;
            this.btnSearchE.TabStop = false;
            this.btnSearchE.Text = "[Empfaenger]";
            this.btnSearchE.UseVisualStyleBackColor = true;
            this.btnSearchE.Click += new System.EventHandler(this.btnSearchE_Click);
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.dgv);
            this.splitPanel2.Location = new System.Drawing.Point(0, 83);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(644, 324);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.3114144F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 126);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // ctrASNActionSelectToCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.rMenu_VDAClientOutEdit);
            this.Name = "ctrASNActionSelectToCopy";
            this.Size = new System.Drawing.Size(644, 449);
            this.Load += new System.EventHandler(this.ctrASNActionSelectToCopy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rMenu_VDAClientOutEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panAdrEmp)).EndInit();
            this.panAdrEmp.ResumeLayout(false);
            this.panAdrEmp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadMenu rMenu_VDAClientOutEdit;
        private Telerik.WinControls.UI.RadMenuButtonItem btnRefresh;
        private Telerik.WinControls.UI.RadMenuButtonItem btnTakeOver;
        private Telerik.WinControls.UI.RadMenuButtonItem btnClose;
        private Telerik.WinControls.UI.RadGridView dgv;
        private Telerik.WinControls.UI.RadSplitContainer scMain;
        private Telerik.WinControls.UI.SplitPanel panAdrEmp;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private System.Windows.Forms.TextBox tbSearchE;
        private System.Windows.Forms.TextBox tbEmpfaenger;
        private System.Windows.Forms.Button btnSearchE;
        private System.Windows.Forms.NumericUpDown nudAdrIdDirect;
    }
}
