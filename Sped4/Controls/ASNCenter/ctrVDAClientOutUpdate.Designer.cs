namespace Sped4.Controls.ASNCenter
{
    partial class ctrVDAClientOutUpdate
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.scMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.btnSqlExecute = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSqlStatement = new Telerik.WinControls.UI.RadTextBox();
            this.comboPass = new System.Windows.Forms.ComboBox();
            this.comboUser = new System.Windows.Forms.ComboBox();
            this.comboDatabase = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lUser = new System.Windows.Forms.Label();
            this.comboSqlServerLists = new System.Windows.Forms.ComboBox();
            this.lDbName = new System.Windows.Forms.Label();
            this.lSqlServerName = new System.Windows.Forms.Label();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.dgvSqlResult = new Telerik.WinControls.UI.RadGridView();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSqlStatement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSqlResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSqlResult.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Controls.Add(this.splitPanel1);
            this.scMain.Controls.Add(this.splitPanel2);
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.scMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scMain.Size = new System.Drawing.Size(708, 511);
            this.scMain.SplitterWidth = 8;
            this.scMain.TabIndex = 0;
            this.scMain.TabStop = false;
            this.scMain.ThemeName = "Fluent";
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.label3);
            this.splitPanel1.Controls.Add(this.btnSqlExecute);
            this.splitPanel1.Controls.Add(this.btnImport);
            this.splitPanel1.Controls.Add(this.label2);
            this.splitPanel1.Controls.Add(this.tbSqlStatement);
            this.splitPanel1.Controls.Add(this.comboPass);
            this.splitPanel1.Controls.Add(this.comboUser);
            this.splitPanel1.Controls.Add(this.comboDatabase);
            this.splitPanel1.Controls.Add(this.label1);
            this.splitPanel1.Controls.Add(this.lUser);
            this.splitPanel1.Controls.Add(this.comboSqlServerLists);
            this.splitPanel1.Controls.Add(this.lDbName);
            this.splitPanel1.Controls.Add(this.lSqlServerName);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(708, 271);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.0387674F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 19);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            this.splitPanel1.ThemeName = "Fluent";
            // 
            // btnSqlExecute
            // 
            this.btnSqlExecute.Location = new System.Drawing.Point(429, 142);
            this.btnSqlExecute.Name = "btnSqlExecute";
            this.btnSqlExecute.Size = new System.Drawing.Size(155, 25);
            this.btnSqlExecute.TabIndex = 28;
            this.btnSqlExecute.Text = "SQL - Abfrage ausführen";
            this.btnSqlExecute.UseVisualStyleBackColor = true;
            this.btnSqlExecute.Click += new System.EventHandler(this.btnSqlExecute_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(429, 16);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(155, 25);
            this.btnImport.TabIndex = 27;
            this.btnImport.Text = "Update";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "SQL-Statement:";
            // 
            // tbSqlStatement
            // 
            this.tbSqlStatement.Location = new System.Drawing.Point(20, 142);
            this.tbSqlStatement.Multiline = true;
            this.tbSqlStatement.Name = "tbSqlStatement";
            // 
            // 
            // 
            this.tbSqlStatement.RootElement.StretchVertically = true;
            this.tbSqlStatement.Size = new System.Drawing.Size(378, 111);
            this.tbSqlStatement.TabIndex = 25;
            this.tbSqlStatement.Text = "SELECT * FROM VDAClientOUT WHERE";
            this.tbSqlStatement.ThemeName = "Fluent";
            // 
            // comboPass
            // 
            this.comboPass.FormattingEnabled = true;
            this.comboPass.Items.AddRange(new object[] {
            "Admin.24",
            "Lvs@comtec.24",
            "masterkey",
            "mMuFKTdWsxn3rmqs"});
            this.comboPass.Location = new System.Drawing.Point(119, 70);
            this.comboPass.Name = "comboPass";
            this.comboPass.Size = new System.Drawing.Size(279, 21);
            this.comboPass.TabIndex = 24;
            // 
            // comboUser
            // 
            this.comboUser.FormattingEnabled = true;
            this.comboUser.Items.AddRange(new object[] {
            "LvsUser",
            "sa"});
            this.comboUser.Location = new System.Drawing.Point(119, 43);
            this.comboUser.Name = "comboUser";
            this.comboUser.Size = new System.Drawing.Size(279, 21);
            this.comboUser.TabIndex = 23;
            // 
            // comboDatabase
            // 
            this.comboDatabase.FormattingEnabled = true;
            this.comboDatabase.Items.AddRange(new object[] {
            "BMW_DESADV",
            "SZG_COM",
            "SZG_COM_Import",
            "SZG_COM_VW"});
            this.comboDatabase.Location = new System.Drawing.Point(119, 98);
            this.comboDatabase.Name = "comboDatabase";
            this.comboDatabase.Size = new System.Drawing.Size(279, 21);
            this.comboDatabase.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "DB - Passwort:";
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(17, 47);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(57, 13);
            this.lUser.TabIndex = 20;
            this.lUser.Text = "DB - User:";
            // 
            // comboSqlServerLists
            // 
            this.comboSqlServerLists.FormattingEnabled = true;
            this.comboSqlServerLists.Location = new System.Drawing.Point(119, 16);
            this.comboSqlServerLists.Name = "comboSqlServerLists";
            this.comboSqlServerLists.Size = new System.Drawing.Size(279, 21);
            this.comboSqlServerLists.TabIndex = 19;
            // 
            // lDbName
            // 
            this.lDbName.AutoSize = true;
            this.lDbName.Location = new System.Drawing.Point(17, 101);
            this.lDbName.Name = "lDbName";
            this.lDbName.Size = new System.Drawing.Size(67, 13);
            this.lDbName.TabIndex = 18;
            this.lDbName.Text = "Datenbank:";
            // 
            // lSqlServerName
            // 
            this.lSqlServerName.AutoSize = true;
            this.lSqlServerName.Location = new System.Drawing.Point(17, 19);
            this.lSqlServerName.Name = "lSqlServerName";
            this.lSqlServerName.Size = new System.Drawing.Size(95, 13);
            this.lSqlServerName.TabIndex = 17;
            this.lSqlServerName.Text = "SQL Server Name:";
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.dgvSqlResult);
            this.splitPanel2.Location = new System.Drawing.Point(0, 279);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(708, 232);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.0387674F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -19);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            this.splitPanel2.ThemeName = "Fluent";
            // 
            // dgvSqlResult
            // 
            this.dgvSqlResult.BackColor = System.Drawing.Color.White;
            this.dgvSqlResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvSqlResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSqlResult.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvSqlResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvSqlResult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvSqlResult.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgvSqlResult.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvSqlResult.MasterTemplate.EnableFiltering = true;
            this.dgvSqlResult.MasterTemplate.ShowFilteringRow = false;
            this.dgvSqlResult.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvSqlResult.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgvSqlResult.Name = "dgvSqlResult";
            this.dgvSqlResult.ReadOnly = true;
            this.dgvSqlResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvSqlResult.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.dgvSqlResult.ShowHeaderCellButtons = true;
            this.dgvSqlResult.Size = new System.Drawing.Size(708, 232);
            this.dgvSqlResult.TabIndex = 30;
            this.dgvSqlResult.ThemeName = "ControlDefault";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(426, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Update aktuell nur für Tabelle VDAClientOUT ! ";
            // 
            // ctrVDAClientOutUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Name = "ctrVDAClientOutUpdate";
            this.Size = new System.Drawing.Size(708, 511);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSqlStatement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSqlResult.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSqlResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer scMain;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadTextBox tbSqlStatement;
        private System.Windows.Forms.ComboBox comboPass;
        private System.Windows.Forms.ComboBox comboUser;
        private System.Windows.Forms.ComboBox comboDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.ComboBox comboSqlServerLists;
        private System.Windows.Forms.Label lDbName;
        private System.Windows.Forms.Label lSqlServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSqlExecute;
        private System.Windows.Forms.Button btnImport;
        private Telerik.WinControls.UI.RadGridView dgvSqlResult;
        private System.Windows.Forms.Label label3;
    }
}
