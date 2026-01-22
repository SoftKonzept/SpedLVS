namespace Sped4
{
    partial class ctrASNAction
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.scASNAction = new Telerik.WinControls.UI.RadSplitContainer();
            this.panASNAction_List = new Telerik.WinControls.UI.SplitPanel();
            this.dgvASNAction = new Telerik.WinControls.UI.RadGridView();
            this.rMenuList = new Telerik.WinControls.UI.RadMenu();
            this.btnAsnActionRefresh = new Telerik.WinControls.UI.RadMenuItem();
            this.btnAddASNActionRange = new Telerik.WinControls.UI.RadMenuItem();
            this.panASNAction_Edit = new Telerik.WinControls.UI.SplitPanel();
            this.btnSetGlobalAction = new System.Windows.Forms.Button();
            this.cbUseOldPropertyValue = new Telerik.WinControls.UI.RadCheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudOrderID = new System.Windows.Forms.NumericUpDown();
            this.nudAdrIdDirect = new System.Windows.Forms.NumericUpDown();
            this.cbVirtFile = new Telerik.WinControls.UI.RadCheckBox();
            this.cbAktiv = new Telerik.WinControls.UI.RadCheckBox();
            this.tbBemerkung = new System.Windows.Forms.TextBox();
            this.cbMandant = new System.Windows.Forms.ComboBox();
            this.tbSearchE = new System.Windows.Forms.TextBox();
            this.cbASNAction = new System.Windows.Forms.ComboBox();
            this.tbEmpfaenger = new System.Windows.Forms.TextBox();
            this.cbASNTyp = new System.Windows.Forms.ComboBox();
            this.btnSearchE = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbArbeitsbereich = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rmASNActionEdit = new Telerik.WinControls.UI.RadMenu();
            this.btnAsnActionAdd = new Telerik.WinControls.UI.RadMenuItem();
            this.btnAsnActionSave = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnAsnActionDelete = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnASNActionCopy = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnASNActionDefaultAddRange = new Telerik.WinControls.UI.RadMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scASNAction)).BeginInit();
            this.scASNAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panASNAction_List)).BeginInit();
            this.panASNAction_List.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvASNAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvASNAction.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rMenuList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panASNAction_Edit)).BeginInit();
            this.panASNAction_Edit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbUseOldPropertyValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrderID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbVirtFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAktiv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmASNActionEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(124, 240);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(106, 20);
            this.textBox1.TabIndex = 216;
            // 
            // scASNAction
            // 
            this.scASNAction.Controls.Add(this.panASNAction_List);
            this.scASNAction.Controls.Add(this.panASNAction_Edit);
            this.scASNAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scASNAction.Location = new System.Drawing.Point(0, 0);
            this.scASNAction.Name = "scASNAction";
            // 
            // 
            // 
            this.scASNAction.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scASNAction.Size = new System.Drawing.Size(675, 455);
            this.scASNAction.SplitterWidth = 8;
            this.scASNAction.TabIndex = 1;
            this.scASNAction.TabStop = false;
            // 
            // panASNAction_List
            // 
            this.panASNAction_List.Controls.Add(this.dgvASNAction);
            this.panASNAction_List.Controls.Add(this.rMenuList);
            this.panASNAction_List.Location = new System.Drawing.Point(0, 0);
            this.panASNAction_List.Name = "panASNAction_List";
            // 
            // 
            // 
            this.panASNAction_List.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panASNAction_List.Size = new System.Drawing.Size(227, 455);
            this.panASNAction_List.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.05846423F, 0F);
            this.panASNAction_List.SizeInfo.SplitterCorrection = new System.Drawing.Size(-2, 0);
            this.panASNAction_List.TabIndex = 0;
            this.panASNAction_List.TabStop = false;
            this.panASNAction_List.Text = "splitPanel1";
            // 
            // dgvASNAction
            // 
            this.dgvASNAction.BackColor = System.Drawing.Color.White;
            this.dgvASNAction.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvASNAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvASNAction.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvASNAction.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvASNAction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvASNAction.Location = new System.Drawing.Point(0, 36);
            // 
            // 
            // 
            this.dgvASNAction.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvASNAction.MasterTemplate.EnableFiltering = true;
            this.dgvASNAction.MasterTemplate.ShowFilteringRow = false;
            this.dgvASNAction.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvASNAction.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvASNAction.Name = "dgvASNAction";
            this.dgvASNAction.ReadOnly = true;
            this.dgvASNAction.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvASNAction.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 36, 240, 150);
            this.dgvASNAction.ShowHeaderCellButtons = true;
            this.dgvASNAction.Size = new System.Drawing.Size(227, 419);
            this.dgvASNAction.TabIndex = 29;
            this.dgvASNAction.ThemeName = "ControlDefault";
            this.dgvASNAction.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvASNAction_MouseClick);
            this.dgvASNAction.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvASNAction_MouseDoubleClick);
            // 
            // rMenuList
            // 
            this.rMenuList.AllItemsEqualHeight = true;
            this.rMenuList.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnAsnActionRefresh,
            this.btnAddASNActionRange});
            this.rMenuList.Location = new System.Drawing.Point(0, 0);
            this.rMenuList.Name = "rMenuList";
            this.rMenuList.Size = new System.Drawing.Size(227, 36);
            this.rMenuList.TabIndex = 3;
            this.rMenuList.ThemeName = "ControlDefault";
            // 
            // btnAsnActionRefresh
            // 
            this.btnAsnActionRefresh.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnAsnActionRefresh.HintText = "";
            this.btnAsnActionRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.btnAsnActionRefresh.Name = "btnAsnActionRefresh";
            this.btnAsnActionRefresh.Text = "";
            this.btnAsnActionRefresh.ToolTipText = "Neue Datensätze hinzufügen";
            this.btnAsnActionRefresh.UseCompatibleTextRendering = false;
            this.btnAsnActionRefresh.Click += new System.EventHandler(this.btnAsnActionRefresh_Click);
            // 
            // btnAddASNActionRange
            // 
            this.btnAddASNActionRange.Image = global::Sped4.Properties.Resources.chart_dot;
            this.btnAddASNActionRange.Name = "btnAddASNActionRange";
            this.btnAddASNActionRange.Text = "";
            this.btnAddASNActionRange.ToolTipText = "selektierte AktionRange hinzufügen";
            this.btnAddASNActionRange.Click += new System.EventHandler(this.btnAddASNActionRange_Click);
            // 
            // panASNAction_Edit
            // 
            this.panASNAction_Edit.BackColor = System.Drawing.Color.White;
            this.panASNAction_Edit.Controls.Add(this.btnSetGlobalAction);
            this.panASNAction_Edit.Controls.Add(this.cbUseOldPropertyValue);
            this.panASNAction_Edit.Controls.Add(this.label5);
            this.panASNAction_Edit.Controls.Add(this.nudOrderID);
            this.panASNAction_Edit.Controls.Add(this.nudAdrIdDirect);
            this.panASNAction_Edit.Controls.Add(this.cbVirtFile);
            this.panASNAction_Edit.Controls.Add(this.cbAktiv);
            this.panASNAction_Edit.Controls.Add(this.tbBemerkung);
            this.panASNAction_Edit.Controls.Add(this.cbMandant);
            this.panASNAction_Edit.Controls.Add(this.tbSearchE);
            this.panASNAction_Edit.Controls.Add(this.cbASNAction);
            this.panASNAction_Edit.Controls.Add(this.tbEmpfaenger);
            this.panASNAction_Edit.Controls.Add(this.cbASNTyp);
            this.panASNAction_Edit.Controls.Add(this.btnSearchE);
            this.panASNAction_Edit.Controls.Add(this.label4);
            this.panASNAction_Edit.Controls.Add(this.cbArbeitsbereich);
            this.panASNAction_Edit.Controls.Add(this.label3);
            this.panASNAction_Edit.Controls.Add(this.label35);
            this.panASNAction_Edit.Controls.Add(this.label2);
            this.panASNAction_Edit.Controls.Add(this.label1);
            this.panASNAction_Edit.Controls.Add(this.rmASNActionEdit);
            this.panASNAction_Edit.Location = new System.Drawing.Point(235, 0);
            this.panASNAction_Edit.Name = "panASNAction_Edit";
            // 
            // 
            // 
            this.panASNAction_Edit.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panASNAction_Edit.Size = new System.Drawing.Size(440, 455);
            this.panASNAction_Edit.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.05846423F, 0F);
            this.panASNAction_Edit.SizeInfo.MaximumSize = new System.Drawing.Size(440, 0);
            this.panASNAction_Edit.SizeInfo.MinimumSize = new System.Drawing.Size(440, 0);
            this.panASNAction_Edit.SizeInfo.SplitterCorrection = new System.Drawing.Size(2, 0);
            this.panASNAction_Edit.TabIndex = 1;
            this.panASNAction_Edit.TabStop = false;
            this.panASNAction_Edit.Text = "panASNAction_Edit";
            // 
            // btnSetGlobalAction
            // 
            this.btnSetGlobalAction.Location = new System.Drawing.Point(135, 44);
            this.btnSetGlobalAction.Name = "btnSetGlobalAction";
            this.btnSetGlobalAction.Size = new System.Drawing.Size(106, 23);
            this.btnSetGlobalAction.TabIndex = 243;
            this.btnSetGlobalAction.Text = "Global Action";
            this.btnSetGlobalAction.UseVisualStyleBackColor = true;
            this.btnSetGlobalAction.Click += new System.EventHandler(this.btnSetGlobalAction_Click);
            // 
            // cbUseOldPropertyValue
            // 
            this.cbUseOldPropertyValue.Location = new System.Drawing.Point(135, 285);
            this.cbUseOldPropertyValue.Name = "cbUseOldPropertyValue";
            this.cbUseOldPropertyValue.Size = new System.Drawing.Size(170, 18);
            this.cbUseOldPropertyValue.TabIndex = 242;
            this.cbUseOldPropertyValue.Text = "verwende alte Property Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(17, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 241;
            this.label5.Text = "Order ID:";
            // 
            // nudOrderID
            // 
            this.nudOrderID.Location = new System.Drawing.Point(181, 248);
            this.nudOrderID.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudOrderID.Name = "nudOrderID";
            this.nudOrderID.Size = new System.Drawing.Size(109, 20);
            this.nudOrderID.TabIndex = 240;
            // 
            // nudAdrIdDirect
            // 
            this.nudAdrIdDirect.Location = new System.Drawing.Point(20, 44);
            this.nudAdrIdDirect.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudAdrIdDirect.Name = "nudAdrIdDirect";
            this.nudAdrIdDirect.Size = new System.Drawing.Size(109, 20);
            this.nudAdrIdDirect.TabIndex = 239;
            this.nudAdrIdDirect.Leave += new System.EventHandler(this.nudAdrIdDirect_Leave);
            // 
            // cbVirtFile
            // 
            this.cbVirtFile.Location = new System.Drawing.Point(274, 74);
            this.cbVirtFile.Name = "cbVirtFile";
            this.cbVirtFile.Size = new System.Drawing.Size(138, 18);
            this.cbVirtFile.TabIndex = 238;
            this.cbVirtFile.Text = "virtuelles Files erstellen";
            // 
            // cbAktiv
            // 
            this.cbAktiv.Location = new System.Drawing.Point(274, 54);
            this.cbAktiv.Name = "cbAktiv";
            this.cbAktiv.Size = new System.Drawing.Size(47, 18);
            this.cbAktiv.TabIndex = 237;
            this.cbAktiv.Text = "aktiv";
            // 
            // tbBemerkung
            // 
            this.tbBemerkung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBemerkung.Location = new System.Drawing.Point(19, 329);
            this.tbBemerkung.Multiline = true;
            this.tbBemerkung.Name = "tbBemerkung";
            this.tbBemerkung.Size = new System.Drawing.Size(390, 108);
            this.tbBemerkung.TabIndex = 236;
            // 
            // cbMandant
            // 
            this.cbMandant.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbMandant.FormattingEnabled = true;
            this.cbMandant.ItemHeight = 13;
            this.cbMandant.Location = new System.Drawing.Point(135, 164);
            this.cbMandant.Name = "cbMandant";
            this.cbMandant.Size = new System.Drawing.Size(155, 21);
            this.cbMandant.TabIndex = 235;
            // 
            // tbSearchE
            // 
            this.tbSearchE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchE.Location = new System.Drawing.Point(135, 73);
            this.tbSearchE.Name = "tbSearchE";
            this.tbSearchE.Size = new System.Drawing.Size(106, 20);
            this.tbSearchE.TabIndex = 225;
            // 
            // cbASNAction
            // 
            this.cbASNAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbASNAction.FormattingEnabled = true;
            this.cbASNAction.ItemHeight = 13;
            this.cbASNAction.Location = new System.Drawing.Point(135, 195);
            this.cbASNAction.Name = "cbASNAction";
            this.cbASNAction.Size = new System.Drawing.Size(155, 21);
            this.cbASNAction.TabIndex = 234;
            // 
            // tbEmpfaenger
            // 
            this.tbEmpfaenger.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEmpfaenger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEmpfaenger.Enabled = false;
            this.tbEmpfaenger.Location = new System.Drawing.Point(19, 98);
            this.tbEmpfaenger.Name = "tbEmpfaenger";
            this.tbEmpfaenger.ReadOnly = true;
            this.tbEmpfaenger.Size = new System.Drawing.Size(390, 20);
            this.tbEmpfaenger.TabIndex = 226;
            // 
            // cbASNTyp
            // 
            this.cbASNTyp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbASNTyp.FormattingEnabled = true;
            this.cbASNTyp.ItemHeight = 13;
            this.cbASNTyp.Location = new System.Drawing.Point(135, 221);
            this.cbASNTyp.Name = "cbASNTyp";
            this.cbASNTyp.Size = new System.Drawing.Size(155, 21);
            this.cbASNTyp.TabIndex = 233;
            // 
            // btnSearchE
            // 
            this.btnSearchE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchE.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchE.Location = new System.Drawing.Point(19, 70);
            this.btnSearchE.Name = "btnSearchE";
            this.btnSearchE.Size = new System.Drawing.Size(110, 22);
            this.btnSearchE.TabIndex = 224;
            this.btnSearchE.TabStop = false;
            this.btnSearchE.Text = "[Empfaenger]";
            this.btnSearchE.UseVisualStyleBackColor = true;
            this.btnSearchE.Click += new System.EventHandler(this.btnSearchE_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(16, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 232;
            this.label4.Text = "Mandant:";
            // 
            // cbArbeitsbereich
            // 
            this.cbArbeitsbereich.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbArbeitsbereich.FormattingEnabled = true;
            this.cbArbeitsbereich.ItemHeight = 13;
            this.cbArbeitsbereich.Location = new System.Drawing.Point(135, 135);
            this.cbArbeitsbereich.Name = "cbArbeitsbereich";
            this.cbArbeitsbereich.Size = new System.Drawing.Size(155, 21);
            this.cbArbeitsbereich.TabIndex = 227;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(16, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 231;
            this.label3.Text = "ASN Aktion";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.DarkBlue;
            this.label35.Location = new System.Drawing.Point(16, 138);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(84, 13);
            this.label35.TabIndex = 228;
            this.label35.Text = "Arbeitsbereich:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(16, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 230;
            this.label2.Text = "ASN Typ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(17, 313);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 229;
            this.label1.Text = "Bemerkung";
            // 
            // rmASNActionEdit
            // 
            this.rmASNActionEdit.AllItemsEqualHeight = true;
            this.rmASNActionEdit.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnAsnActionAdd,
            this.btnAsnActionSave,
            this.btnAsnActionDelete,
            this.btnASNActionCopy,
            this.btnASNActionDefaultAddRange});
            this.rmASNActionEdit.Location = new System.Drawing.Point(0, 0);
            this.rmASNActionEdit.Name = "rmASNActionEdit";
            this.rmASNActionEdit.Size = new System.Drawing.Size(440, 34);
            this.rmASNActionEdit.TabIndex = 2;
            this.rmASNActionEdit.ThemeName = "ControlDefault";
            // 
            // btnAsnActionAdd
            // 
            this.btnAsnActionAdd.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnAsnActionAdd.HintText = "";
            this.btnAsnActionAdd.Image = global::Sped4.Properties.Resources.add_24x24;
            this.btnAsnActionAdd.Name = "btnAsnActionAdd";
            this.btnAsnActionAdd.Text = "";
            this.btnAsnActionAdd.ToolTipText = "Neue Datensätze hinzufügen";
            this.btnAsnActionAdd.UseCompatibleTextRendering = false;
            this.btnAsnActionAdd.Click += new System.EventHandler(this.btnAsnActionAdd_Click);
            // 
            // btnAsnActionSave
            // 
            // 
            // 
            // 
            this.btnAsnActionSave.ButtonElement.ToolTipText = "Datensatz speichern";
            this.btnAsnActionSave.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnAsnActionSave.Image = global::Sped4.Properties.Resources.check_24x24;
            this.btnAsnActionSave.Name = "btnAsnActionSave";
            this.btnAsnActionSave.Text = "";
            this.btnAsnActionSave.ToolTipText = "Datensatz speichern";
            this.btnAsnActionSave.UseCompatibleTextRendering = false;
            this.btnAsnActionSave.Click += new System.EventHandler(this.btnAsnActionSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAsnActionSave.GetChildAt(2))).ToolTipText = "Datensatz speichern";
            // 
            // btnAsnActionDelete
            // 
            // 
            // 
            // 
            this.btnAsnActionDelete.ButtonElement.ToolTipText = "Datensatz löschen";
            this.btnAsnActionDelete.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnAsnActionDelete.Image = global::Sped4.Properties.Resources.garbage_delete_24x24;
            this.btnAsnActionDelete.Name = "btnAsnActionDelete";
            this.btnAsnActionDelete.Text = "";
            this.btnAsnActionDelete.ToolTipText = "Datensatz löschen";
            this.btnAsnActionDelete.UseCompatibleTextRendering = false;
            this.btnAsnActionDelete.Click += new System.EventHandler(this.btnAsnActionDelete_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAsnActionDelete.GetChildAt(2))).ToolTipText = "Datensatz löschen";
            // 
            // btnASNActionCopy
            // 
            this.btnASNActionCopy.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.btnASNActionCopy.Image = global::Sped4.Properties.Resources.copyDoc_24x24;
            this.btnASNActionCopy.Name = "btnASNActionCopy";
            this.btnASNActionCopy.StretchVertically = false;
            this.btnASNActionCopy.Text = "";
            this.btnASNActionCopy.Click += new System.EventHandler(this.btnASNActionCopy_Click);
            // 
            // btnASNActionDefaultAddRange
            // 
            this.btnASNActionDefaultAddRange.Name = "btnASNActionDefaultAddRange";
            this.btnASNActionDefaultAddRange.Text = "Def. Aktions";
            this.btnASNActionDefaultAddRange.Click += new System.EventHandler(this.btnASNActionDefaultAddRange_Click);
            // 
            // ctrASNAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scASNAction);
            this.Name = "ctrASNAction";
            this.Size = new System.Drawing.Size(675, 455);
            this.Load += new System.EventHandler(this.ctrASNAction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scASNAction)).EndInit();
            this.scASNAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panASNAction_List)).EndInit();
            this.panASNAction_List.ResumeLayout(false);
            this.panASNAction_List.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvASNAction.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvASNAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rMenuList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panASNAction_Edit)).EndInit();
            this.panASNAction_Edit.ResumeLayout(false);
            this.panASNAction_Edit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbUseOldPropertyValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOrderID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbVirtFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbAktiv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmASNActionEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private Telerik.WinControls.UI.RadSplitContainer scASNAction;
        private Telerik.WinControls.UI.SplitPanel panASNAction_List;
        private Telerik.WinControls.UI.SplitPanel panASNAction_Edit;
        private Telerik.WinControls.UI.RadGridView dgvASNAction;
        private Telerik.WinControls.UI.RadMenuItem btnAsnActionRefresh;
        private Telerik.WinControls.UI.RadCheckBox cbVirtFile;
        private Telerik.WinControls.UI.RadCheckBox cbAktiv;
        private System.Windows.Forms.TextBox tbBemerkung;
        private System.Windows.Forms.ComboBox cbMandant;
        private System.Windows.Forms.TextBox tbSearchE;
        private System.Windows.Forms.ComboBox cbASNAction;
        private System.Windows.Forms.TextBox tbEmpfaenger;
        private System.Windows.Forms.ComboBox cbASNTyp;
        private System.Windows.Forms.Button btnSearchE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbArbeitsbereich;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadMenu rmASNActionEdit;
        private Telerik.WinControls.UI.RadMenuItem btnAsnActionAdd;
        private Telerik.WinControls.UI.RadMenuButtonItem btnAsnActionSave;
        private Telerik.WinControls.UI.RadMenuButtonItem btnAsnActionDelete;
        private Telerik.WinControls.UI.RadMenuButtonItem btnASNActionCopy;
        private Telerik.WinControls.UI.RadMenuItem btnASNActionDefaultAddRange;
        private Telerik.WinControls.UI.RadMenu rMenuList;
        private Telerik.WinControls.UI.RadMenuItem btnAddASNActionRange;
        private System.Windows.Forms.NumericUpDown nudAdrIdDirect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudOrderID;
        private Telerik.WinControls.UI.RadCheckBox cbUseOldPropertyValue;
        private System.Windows.Forms.Button btnSetGlobalAction;
    }
}
