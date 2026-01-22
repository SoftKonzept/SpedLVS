namespace Sped4.Controls.ASNCenter
{
    partial class ctrEdifactImport
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
            this.lSqlServerName = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.panInputSql = new System.Windows.Forms.Panel();
            this.comboPass = new System.Windows.Forms.ComboBox();
            this.comboUser = new System.Windows.Forms.ComboBox();
            this.btnCheckConnection = new System.Windows.Forms.Button();
            this.comboDatabase = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lUser = new System.Windows.Forms.Label();
            this.comboSqlServerLists = new System.Windows.Forms.ComboBox();
            this.lDbName = new System.Windows.Forms.Label();
            this.scEdiElemts = new System.Windows.Forms.SplitContainer();
            this.panEdiElements = new System.Windows.Forms.Panel();
            this.dgvAsnArt = new Telerik.WinControls.UI.RadGridView();
            this.panElementsFields = new System.Windows.Forms.Panel();
            this.dgvEdiSegmentElementField = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.panInputSql.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scEdiElemts)).BeginInit();
            this.scEdiElemts.Panel1.SuspendLayout();
            this.scEdiElemts.Panel2.SuspendLayout();
            this.scEdiElemts.SuspendLayout();
            this.panEdiElements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsnArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsnArt.MasterTemplate)).BeginInit();
            this.panElementsFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdiSegmentElementField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdiSegmentElementField.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // lSqlServerName
            // 
            this.lSqlServerName.AutoSize = true;
            this.lSqlServerName.Location = new System.Drawing.Point(23, 16);
            this.lSqlServerName.Name = "lSqlServerName";
            this.lSqlServerName.Size = new System.Drawing.Size(96, 13);
            this.lSqlServerName.TabIndex = 0;
            this.lSqlServerName.Text = "SQL Server Name:";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(475, 95);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(227, 25);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.panInputSql);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scEdiElemts);
            this.scMain.Size = new System.Drawing.Size(812, 534);
            this.scMain.SplitterDistance = 142;
            this.scMain.TabIndex = 6;
            // 
            // panInputSql
            // 
            this.panInputSql.BackColor = System.Drawing.Color.White;
            this.panInputSql.Controls.Add(this.comboPass);
            this.panInputSql.Controls.Add(this.comboUser);
            this.panInputSql.Controls.Add(this.btnCheckConnection);
            this.panInputSql.Controls.Add(this.comboDatabase);
            this.panInputSql.Controls.Add(this.label1);
            this.panInputSql.Controls.Add(this.lUser);
            this.panInputSql.Controls.Add(this.comboSqlServerLists);
            this.panInputSql.Controls.Add(this.lDbName);
            this.panInputSql.Controls.Add(this.lSqlServerName);
            this.panInputSql.Controls.Add(this.btnImport);
            this.panInputSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panInputSql.Location = new System.Drawing.Point(0, 0);
            this.panInputSql.Name = "panInputSql";
            this.panInputSql.Size = new System.Drawing.Size(812, 142);
            this.panInputSql.TabIndex = 0;
            // 
            // comboPass
            // 
            this.comboPass.FormattingEnabled = true;
            this.comboPass.Items.AddRange(new object[] {
            "Admin.24",
            "Lvs@comtec.24",
            "masterkey",
            "mMuFKTdWsxn3rmqs"});
            this.comboPass.Location = new System.Drawing.Point(125, 67);
            this.comboPass.Name = "comboPass";
            this.comboPass.Size = new System.Drawing.Size(279, 21);
            this.comboPass.TabIndex = 16;
            // 
            // comboUser
            // 
            this.comboUser.FormattingEnabled = true;
            this.comboUser.Items.AddRange(new object[] {
            "LvsUser",
            "sa"});
            this.comboUser.Location = new System.Drawing.Point(125, 40);
            this.comboUser.Name = "comboUser";
            this.comboUser.Size = new System.Drawing.Size(279, 21);
            this.comboUser.TabIndex = 15;
            // 
            // btnCheckConnection
            // 
            this.btnCheckConnection.Location = new System.Drawing.Point(475, 40);
            this.btnCheckConnection.Name = "btnCheckConnection";
            this.btnCheckConnection.Size = new System.Drawing.Size(227, 26);
            this.btnCheckConnection.TabIndex = 14;
            this.btnCheckConnection.Text = "Check Verbindung Source Connection";
            this.btnCheckConnection.UseVisualStyleBackColor = true;
            this.btnCheckConnection.Click += new System.EventHandler(this.btnCheckConnection_Click);
            // 
            // comboDatabase
            // 
            this.comboDatabase.FormattingEnabled = true;
            this.comboDatabase.Items.AddRange(new object[] {
            "BMW_DESADV",
            "SZG_COM",
            "SZG_COM_BMW"});
            this.comboDatabase.Location = new System.Drawing.Point(125, 95);
            this.comboDatabase.Name = "comboDatabase";
            this.comboDatabase.Size = new System.Drawing.Size(279, 21);
            this.comboDatabase.TabIndex = 13;
            this.comboDatabase.SelectedIndexChanged += new System.EventHandler(this.comboDatabase_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "DB - Passwort:";
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(23, 44);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(56, 13);
            this.lUser.TabIndex = 9;
            this.lUser.Text = "DB - User:";
            // 
            // comboSqlServerLists
            // 
            this.comboSqlServerLists.FormattingEnabled = true;
            this.comboSqlServerLists.Location = new System.Drawing.Point(125, 13);
            this.comboSqlServerLists.Name = "comboSqlServerLists";
            this.comboSqlServerLists.Size = new System.Drawing.Size(279, 21);
            this.comboSqlServerLists.TabIndex = 8;
            this.comboSqlServerLists.SelectedIndexChanged += new System.EventHandler(this.comboSqlServerLists_SelectedIndexChanged);
            // 
            // lDbName
            // 
            this.lDbName.AutoSize = true;
            this.lDbName.Location = new System.Drawing.Point(23, 98);
            this.lDbName.Name = "lDbName";
            this.lDbName.Size = new System.Drawing.Size(63, 13);
            this.lDbName.TabIndex = 6;
            this.lDbName.Text = "Datenbank:";
            // 
            // scEdiElemts
            // 
            this.scEdiElemts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scEdiElemts.Location = new System.Drawing.Point(0, 0);
            this.scEdiElemts.Name = "scEdiElemts";
            // 
            // scEdiElemts.Panel1
            // 
            this.scEdiElemts.Panel1.Controls.Add(this.panEdiElements);
            // 
            // scEdiElemts.Panel2
            // 
            this.scEdiElemts.Panel2.Controls.Add(this.panElementsFields);
            this.scEdiElemts.Size = new System.Drawing.Size(812, 388);
            this.scEdiElemts.SplitterDistance = 428;
            this.scEdiElemts.TabIndex = 0;
            // 
            // panEdiElements
            // 
            this.panEdiElements.BackColor = System.Drawing.Color.White;
            this.panEdiElements.Controls.Add(this.dgvAsnArt);
            this.panEdiElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panEdiElements.Location = new System.Drawing.Point(0, 0);
            this.panEdiElements.Name = "panEdiElements";
            this.panEdiElements.Size = new System.Drawing.Size(428, 388);
            this.panEdiElements.TabIndex = 0;
            // 
            // dgvAsnArt
            // 
            this.dgvAsnArt.BackColor = System.Drawing.Color.White;
            this.dgvAsnArt.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvAsnArt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAsnArt.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvAsnArt.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvAsnArt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvAsnArt.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgvAsnArt.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvAsnArt.MasterTemplate.EnableFiltering = true;
            this.dgvAsnArt.MasterTemplate.ShowFilteringRow = false;
            this.dgvAsnArt.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvAsnArt.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvAsnArt.Name = "dgvAsnArt";
            this.dgvAsnArt.ReadOnly = true;
            this.dgvAsnArt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvAsnArt.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.dgvAsnArt.ShowHeaderCellButtons = true;
            this.dgvAsnArt.Size = new System.Drawing.Size(428, 388);
            this.dgvAsnArt.TabIndex = 29;
            this.dgvAsnArt.ThemeName = "ControlDefault";
            this.dgvAsnArt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvAsnArt_MouseClick);
            this.dgvAsnArt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvAsnArt_MouseDoubleClick);
            // 
            // panElementsFields
            // 
            this.panElementsFields.BackColor = System.Drawing.Color.White;
            this.panElementsFields.Controls.Add(this.dgvEdiSegmentElementField);
            this.panElementsFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panElementsFields.Location = new System.Drawing.Point(0, 0);
            this.panElementsFields.Name = "panElementsFields";
            this.panElementsFields.Size = new System.Drawing.Size(380, 388);
            this.panElementsFields.TabIndex = 0;
            // 
            // dgvEdiSegmentElementField
            // 
            this.dgvEdiSegmentElementField.BackColor = System.Drawing.Color.White;
            this.dgvEdiSegmentElementField.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvEdiSegmentElementField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEdiSegmentElementField.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvEdiSegmentElementField.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvEdiSegmentElementField.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvEdiSegmentElementField.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgvEdiSegmentElementField.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvEdiSegmentElementField.MasterTemplate.EnableFiltering = true;
            this.dgvEdiSegmentElementField.MasterTemplate.ShowFilteringRow = false;
            this.dgvEdiSegmentElementField.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvEdiSegmentElementField.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgvEdiSegmentElementField.Name = "dgvEdiSegmentElementField";
            this.dgvEdiSegmentElementField.ReadOnly = true;
            this.dgvEdiSegmentElementField.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvEdiSegmentElementField.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.dgvEdiSegmentElementField.ShowHeaderCellButtons = true;
            this.dgvEdiSegmentElementField.Size = new System.Drawing.Size(380, 388);
            this.dgvEdiSegmentElementField.TabIndex = 30;
            this.dgvEdiSegmentElementField.ThemeName = "ControlDefault";
            // 
            // ctrEdifactImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Name = "ctrEdifactImport";
            this.Size = new System.Drawing.Size(812, 534);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.panInputSql.ResumeLayout(false);
            this.panInputSql.PerformLayout();
            this.scEdiElemts.Panel1.ResumeLayout(false);
            this.scEdiElemts.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scEdiElemts)).EndInit();
            this.scEdiElemts.ResumeLayout(false);
            this.panEdiElements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsnArt.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsnArt)).EndInit();
            this.panElementsFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdiSegmentElementField.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdiSegmentElementField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lSqlServerName;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.SplitContainer scEdiElemts;
        private System.Windows.Forms.Panel panInputSql;
        private System.Windows.Forms.Panel panEdiElements;
        private System.Windows.Forms.Panel panElementsFields;
        private System.Windows.Forms.Label lDbName;
        private System.Windows.Forms.ComboBox comboSqlServerLists;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.ComboBox comboDatabase;
        private System.Windows.Forms.Button btnCheckConnection;
        private System.Windows.Forms.ComboBox comboPass;
        private System.Windows.Forms.ComboBox comboUser;
        private Telerik.WinControls.UI.RadGridView dgvAsnArt;
        private Telerik.WinControls.UI.RadGridView dgvEdiSegmentElementField;
    }
}
