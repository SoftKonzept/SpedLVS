namespace Sped4.Controls.ToDo
{
    partial class ctrCleanAsnTables
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
            this.panMainClean = new System.Windows.Forms.Panel();
            this.bar = new Telerik.WinControls.UI.RadProgressBar();
            this.lvLogs = new Telerik.WinControls.UI.RadListView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCleanAsnTable = new System.Windows.Forms.Button();
            this.panMainClean.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // panMainClean
            // 
            this.panMainClean.BackColor = System.Drawing.Color.White;
            this.panMainClean.Controls.Add(this.bar);
            this.panMainClean.Controls.Add(this.lvLogs);
            this.panMainClean.Controls.Add(this.label1);
            this.panMainClean.Controls.Add(this.btnCleanAsnTable);
            this.panMainClean.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMainClean.Location = new System.Drawing.Point(0, 0);
            this.panMainClean.Name = "panMainClean";
            this.panMainClean.Size = new System.Drawing.Size(535, 382);
            this.panMainClean.TabIndex = 0;
            // 
            // bar
            // 
            this.bar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar.Location = new System.Drawing.Point(0, 358);
            this.bar.Maximum = 10;
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(535, 24);
            this.bar.TabIndex = 4;
            this.bar.Text = "Statusbar";
            this.bar.ThemeName = "Fluent";
            // 
            // lvLogs
            // 
            this.lvLogs.AllowArbitraryItemHeight = true;
            this.lvLogs.AllowArbitraryItemWidth = true;
            this.lvLogs.AutoScroll = true;
            this.lvLogs.Location = new System.Drawing.Point(16, 66);
            this.lvLogs.Name = "lvLogs";
            // 
            // 
            // 
            this.lvLogs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.lvLogs.Size = new System.Drawing.Size(508, 286);
            this.lvLogs.TabIndex = 3;
            this.lvLogs.ThemeName = "ControlDefault";
            this.lvLogs.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(this.lvLogs_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Log:";
            // 
            // btnCleanAsnTable
            // 
            this.btnCleanAsnTable.Location = new System.Drawing.Point(16, 12);
            this.btnCleanAsnTable.Name = "btnCleanAsnTable";
            this.btnCleanAsnTable.Size = new System.Drawing.Size(213, 23);
            this.btnCleanAsnTable.TabIndex = 0;
            this.btnCleanAsnTable.Text = "Clean ASN";
            this.btnCleanAsnTable.UseVisualStyleBackColor = true;
            this.btnCleanAsnTable.Click += new System.EventHandler(this.btnCleanAsnTable_Click);
            // 
            // ctrCleanAsnTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panMainClean);
            this.Name = "ctrCleanAsnTables";
            this.Size = new System.Drawing.Size(535, 382);
            this.Load += new System.EventHandler(this.ctrCleanAsnTables_Load);
            this.panMainClean.ResumeLayout(false);
            this.panMainClean.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panMainClean;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCleanAsnTable;
        private Telerik.WinControls.UI.RadListView lvLogs;
        private Telerik.WinControls.UI.RadProgressBar bar;
    }
}
