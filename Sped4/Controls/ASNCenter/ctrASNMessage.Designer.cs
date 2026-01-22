namespace Sped4
{
    partial class ctrASNMessage
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sc_ASNMessageTestMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.panASNMessagesTest = new Telerik.WinControls.UI.SplitPanel();
            this.sc_ASNMessageTest = new Telerik.WinControls.UI.RadSplitContainer();
            this.panTestEdit = new Telerik.WinControls.UI.SplitPanel();
            this.cbMesToReceiver = new System.Windows.Forms.CheckBox();
            this.cbMesToSupporter = new System.Windows.Forms.CheckBox();
            this.tbQueueDescription = new System.Windows.Forms.TextBox();
            this.btnStartCreateMessage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudArtikelId = new System.Windows.Forms.NumericUpDown();
            this.cbArbeitsbereich = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbASNAction = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.panTestView = new Telerik.WinControls.UI.SplitPanel();
            this.dgvActionView = new Telerik.WinControls.UI.RadGridView();
            this.rmASNActionEdit = new Telerik.WinControls.UI.RadMenu();
            this.btnAsnActionAdd = new Telerik.WinControls.UI.RadMenuItem();
            this.btnAsnActionSave = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnAsnActionDelete = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.panQueueView = new Telerik.WinControls.UI.SplitPanel();
            this.dgvQueue = new Telerik.WinControls.UI.RadGridView();
            this.menuQueue = new Telerik.WinControls.UI.RadMenu();
            this.RefreshQueue = new Telerik.WinControls.UI.RadMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.sc_ASNMessageTestMain)).BeginInit();
            this.sc_ASNMessageTestMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panASNMessagesTest)).BeginInit();
            this.panASNMessagesTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sc_ASNMessageTest)).BeginInit();
            this.sc_ASNMessageTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panTestEdit)).BeginInit();
            this.panTestEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArtikelId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panTestView)).BeginInit();
            this.panTestView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActionView.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmASNActionEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panQueueView)).BeginInit();
            this.panQueueView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuQueue)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // sc_ASNMessageTestMain
            // 
            this.sc_ASNMessageTestMain.Controls.Add(this.panASNMessagesTest);
            this.sc_ASNMessageTestMain.Controls.Add(this.panQueueView);
            this.sc_ASNMessageTestMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_ASNMessageTestMain.Location = new System.Drawing.Point(0, 0);
            this.sc_ASNMessageTestMain.Name = "sc_ASNMessageTestMain";
            this.sc_ASNMessageTestMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.sc_ASNMessageTestMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.sc_ASNMessageTestMain.Size = new System.Drawing.Size(703, 495);
            this.sc_ASNMessageTestMain.SplitterWidth = 8;
            this.sc_ASNMessageTestMain.TabIndex = 0;
            this.sc_ASNMessageTestMain.TabStop = false;
            // 
            // panASNMessagesTest
            // 
            this.panASNMessagesTest.Controls.Add(this.sc_ASNMessageTest);
            this.panASNMessagesTest.Controls.Add(this.rmASNActionEdit);
            this.panASNMessagesTest.Location = new System.Drawing.Point(0, 0);
            this.panASNMessagesTest.Name = "panASNMessagesTest";
            // 
            // 
            // 
            this.panASNMessagesTest.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panASNMessagesTest.Size = new System.Drawing.Size(703, 249);
            this.panASNMessagesTest.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.01120163F);
            this.panASNMessagesTest.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 15);
            this.panASNMessagesTest.TabIndex = 0;
            this.panASNMessagesTest.TabStop = false;
            this.panASNMessagesTest.Text = "splitPanel1";
            // 
            // sc_ASNMessageTest
            // 
            this.sc_ASNMessageTest.Controls.Add(this.panTestEdit);
            this.sc_ASNMessageTest.Controls.Add(this.panTestView);
            this.sc_ASNMessageTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc_ASNMessageTest.Location = new System.Drawing.Point(0, 34);
            this.sc_ASNMessageTest.Name = "sc_ASNMessageTest";
            // 
            // 
            // 
            this.sc_ASNMessageTest.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.sc_ASNMessageTest.Size = new System.Drawing.Size(703, 215);
            this.sc_ASNMessageTest.SplitterWidth = 8;
            this.sc_ASNMessageTest.TabIndex = 0;
            this.sc_ASNMessageTest.TabStop = false;
            // 
            // panTestEdit
            // 
            this.panTestEdit.BackColor = System.Drawing.Color.White;
            this.panTestEdit.Controls.Add(this.cbMesToReceiver);
            this.panTestEdit.Controls.Add(this.cbMesToSupporter);
            this.panTestEdit.Controls.Add(this.tbQueueDescription);
            this.panTestEdit.Controls.Add(this.btnStartCreateMessage);
            this.panTestEdit.Controls.Add(this.label2);
            this.panTestEdit.Controls.Add(this.label5);
            this.panTestEdit.Controls.Add(this.nudArtikelId);
            this.panTestEdit.Controls.Add(this.cbArbeitsbereich);
            this.panTestEdit.Controls.Add(this.label1);
            this.panTestEdit.Controls.Add(this.cbASNAction);
            this.panTestEdit.Controls.Add(this.label35);
            this.panTestEdit.Location = new System.Drawing.Point(0, 0);
            this.panTestEdit.Name = "panTestEdit";
            // 
            // 
            // 
            this.panTestEdit.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panTestEdit.Size = new System.Drawing.Size(348, 215);
            this.panTestEdit.TabIndex = 0;
            this.panTestEdit.TabStop = false;
            this.panTestEdit.Text = "splitPanel1";
            // 
            // cbMesToReceiver
            // 
            this.cbMesToReceiver.AutoSize = true;
            this.cbMesToReceiver.Location = new System.Drawing.Point(27, 30);
            this.cbMesToReceiver.Name = "cbMesToReceiver";
            this.cbMesToReceiver.Size = new System.Drawing.Size(146, 17);
            this.cbMesToReceiver.TabIndex = 247;
            this.cbMesToReceiver.Text = "Message an Empfänger";
            this.cbMesToReceiver.UseVisualStyleBackColor = true;
            // 
            // cbMesToSupporter
            // 
            this.cbMesToSupporter.AutoSize = true;
            this.cbMesToSupporter.Location = new System.Drawing.Point(27, 7);
            this.cbMesToSupporter.Name = "cbMesToSupporter";
            this.cbMesToSupporter.Size = new System.Drawing.Size(135, 17);
            this.cbMesToSupporter.TabIndex = 246;
            this.cbMesToSupporter.Text = "Message an Lieferant";
            this.cbMesToSupporter.UseVisualStyleBackColor = true;
            // 
            // tbQueueDescription
            // 
            this.tbQueueDescription.Location = new System.Drawing.Point(27, 152);
            this.tbQueueDescription.Multiline = true;
            this.tbQueueDescription.Name = "tbQueueDescription";
            this.tbQueueDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbQueueDescription.Size = new System.Drawing.Size(302, 52);
            this.tbQueueDescription.TabIndex = 245;
            this.tbQueueDescription.Text = "created by ASNMessageTestCtr";
            // 
            // btnStartCreateMessage
            // 
            this.btnStartCreateMessage.Location = new System.Drawing.Point(235, 6);
            this.btnStartCreateMessage.Name = "btnStartCreateMessage";
            this.btnStartCreateMessage.Size = new System.Drawing.Size(94, 36);
            this.btnStartCreateMessage.TabIndex = 4;
            this.btnStartCreateMessage.Text = "Meldung erzeugen";
            this.btnStartCreateMessage.UseVisualStyleBackColor = true;
            this.btnStartCreateMessage.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(24, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 244;
            this.label2.Text = "Beschreibung:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(26, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 243;
            this.label5.Text = "Artikel-ID:";
            // 
            // nudArtikelId
            // 
            this.nudArtikelId.Location = new System.Drawing.Point(145, 111);
            this.nudArtikelId.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudArtikelId.Name = "nudArtikelId";
            this.nudArtikelId.Size = new System.Drawing.Size(154, 20);
            this.nudArtikelId.TabIndex = 3;
            // 
            // cbArbeitsbereich
            // 
            this.cbArbeitsbereich.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbArbeitsbereich.FormattingEnabled = true;
            this.cbArbeitsbereich.ItemHeight = 13;
            this.cbArbeitsbereich.Location = new System.Drawing.Point(145, 48);
            this.cbArbeitsbereich.Name = "cbArbeitsbereich";
            this.cbArbeitsbereich.Size = new System.Drawing.Size(155, 21);
            this.cbArbeitsbereich.TabIndex = 1;
            this.cbArbeitsbereich.SelectedIndexChanged += new System.EventHandler(this.cbArbeitsbereich_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(26, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 233;
            this.label1.Text = "Arbeitsbereich:";
            // 
            // cbASNAction
            // 
            this.cbASNAction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbASNAction.FormattingEnabled = true;
            this.cbASNAction.ItemHeight = 13;
            this.cbASNAction.Location = new System.Drawing.Point(145, 81);
            this.cbASNAction.Name = "cbASNAction";
            this.cbASNAction.Size = new System.Drawing.Size(155, 21);
            this.cbASNAction.TabIndex = 2;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ForeColor = System.Drawing.Color.DarkBlue;
            this.label35.Location = new System.Drawing.Point(26, 84);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(68, 13);
            this.label35.TabIndex = 230;
            this.label35.Text = "ASN-Action:";
            // 
            // panTestView
            // 
            this.panTestView.BackColor = System.Drawing.Color.White;
            this.panTestView.Controls.Add(this.dgvActionView);
            this.panTestView.Location = new System.Drawing.Point(356, 0);
            this.panTestView.Name = "panTestView";
            // 
            // 
            // 
            this.panTestView.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panTestView.Size = new System.Drawing.Size(347, 215);
            this.panTestView.TabIndex = 1;
            this.panTestView.TabStop = false;
            this.panTestView.Text = "splitPanel2";
            // 
            // dgvActionView
            // 
            this.dgvActionView.BackColor = System.Drawing.Color.White;
            this.dgvActionView.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvActionView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActionView.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvActionView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvActionView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvActionView.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgvActionView.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvActionView.MasterTemplate.EnableFiltering = true;
            this.dgvActionView.MasterTemplate.ShowFilteringRow = false;
            this.dgvActionView.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvActionView.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvActionView.Name = "dgvActionView";
            this.dgvActionView.ReadOnly = true;
            this.dgvActionView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvActionView.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.dgvActionView.ShowHeaderCellButtons = true;
            this.dgvActionView.Size = new System.Drawing.Size(347, 215);
            this.dgvActionView.TabIndex = 31;
            this.dgvActionView.ThemeName = "ControlDefault";
            // 
            // rmASNActionEdit
            // 
            this.rmASNActionEdit.AllItemsEqualHeight = true;
            this.rmASNActionEdit.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnAsnActionAdd,
            this.btnAsnActionSave,
            this.btnAsnActionDelete});
            this.rmASNActionEdit.Location = new System.Drawing.Point(0, 0);
            this.rmASNActionEdit.Name = "rmASNActionEdit";
            this.rmASNActionEdit.Size = new System.Drawing.Size(703, 34);
            this.rmASNActionEdit.TabIndex = 3;
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
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAsnActionDelete.GetChildAt(2))).ToolTipText = "Datensatz löschen";
            // 
            // panQueueView
            // 
            this.panQueueView.Controls.Add(this.dgvQueue);
            this.panQueueView.Controls.Add(this.menuQueue);
            this.panQueueView.Location = new System.Drawing.Point(0, 257);
            this.panQueueView.Name = "panQueueView";
            // 
            // 
            // 
            this.panQueueView.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panQueueView.Size = new System.Drawing.Size(703, 238);
            this.panQueueView.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.01120163F);
            this.panQueueView.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -15);
            this.panQueueView.TabIndex = 1;
            this.panQueueView.TabStop = false;
            this.panQueueView.Text = "splitPanel2";
            // 
            // dgvQueue
            // 
            this.dgvQueue.BackColor = System.Drawing.Color.White;
            this.dgvQueue.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueue.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvQueue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvQueue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvQueue.Location = new System.Drawing.Point(0, 36);
            // 
            // 
            // 
            this.dgvQueue.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvQueue.MasterTemplate.EnableFiltering = true;
            this.dgvQueue.MasterTemplate.ShowFilteringRow = false;
            this.dgvQueue.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvQueue.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.ReadOnly = true;
            this.dgvQueue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvQueue.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 36, 240, 150);
            this.dgvQueue.ShowHeaderCellButtons = true;
            this.dgvQueue.Size = new System.Drawing.Size(703, 202);
            this.dgvQueue.TabIndex = 30;
            this.dgvQueue.ThemeName = "ControlDefault";
            // 
            // menuQueue
            // 
            this.menuQueue.AllItemsEqualHeight = true;
            this.menuQueue.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.RefreshQueue});
            this.menuQueue.Location = new System.Drawing.Point(0, 0);
            this.menuQueue.Name = "menuQueue";
            this.menuQueue.Size = new System.Drawing.Size(703, 36);
            this.menuQueue.TabIndex = 31;
            this.menuQueue.ThemeName = "ControlDefault";
            // 
            // RefreshQueue
            // 
            this.RefreshQueue.DisplayStyle = Telerik.WinControls.DisplayStyle.Image;
            this.RefreshQueue.HintText = "";
            this.RefreshQueue.Image = global::Sped4.Properties.Resources.refresh;
            this.RefreshQueue.Name = "RefreshQueue";
            this.RefreshQueue.Text = "";
            this.RefreshQueue.ToolTipText = "Neue Datensätze hinzufügen";
            this.RefreshQueue.UseCompatibleTextRendering = false;
            this.RefreshQueue.Click += new System.EventHandler(this.RefreshQueue_Click);
            // 
            // ctrASNMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sc_ASNMessageTestMain);
            this.Name = "ctrASNMessage";
            this.Size = new System.Drawing.Size(703, 495);
            this.Load += new System.EventHandler(this.ctrASNMessageTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sc_ASNMessageTestMain)).EndInit();
            this.sc_ASNMessageTestMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panASNMessagesTest)).EndInit();
            this.panASNMessagesTest.ResumeLayout(false);
            this.panASNMessagesTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sc_ASNMessageTest)).EndInit();
            this.sc_ASNMessageTest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panTestEdit)).EndInit();
            this.panTestEdit.ResumeLayout(false);
            this.panTestEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArtikelId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panTestView)).EndInit();
            this.panTestView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActionView.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmASNActionEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panQueueView)).EndInit();
            this.panQueueView.ResumeLayout(false);
            this.panQueueView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuQueue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private Telerik.WinControls.UI.SplitPanel splitPanel3;
        private Telerik.WinControls.UI.SplitPanel splitPanel4;
        private Telerik.WinControls.UI.RadSplitContainer sc_ASNMessageTestMain;
        private Telerik.WinControls.UI.SplitPanel panASNMessagesTest;
        private Telerik.WinControls.UI.SplitPanel panQueueView;
        private Telerik.WinControls.UI.RadGridView dgvQueue;
        private Telerik.WinControls.UI.RadMenu menuQueue;
        private Telerik.WinControls.UI.RadMenuItem RefreshQueue;
        private Telerik.WinControls.UI.RadSplitContainer sc_ASNMessageTest;
        private Telerik.WinControls.UI.SplitPanel panTestEdit;
        private Telerik.WinControls.UI.SplitPanel panTestView;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem2;
        private Telerik.WinControls.UI.RadMenuItem btnAsnActionAdd;
        private Telerik.WinControls.UI.RadMenuButtonItem btnAsnActionSave;
        private Telerik.WinControls.UI.RadMenuButtonItem btnAsnActionDelete;
        private System.Windows.Forms.Button btnStartCreateMessage;
        private System.Windows.Forms.ComboBox cbASNAction;
        private System.Windows.Forms.Label label35;
        private Telerik.WinControls.UI.RadGridView dgvActionView;
        private Telerik.WinControls.UI.RadMenu rmASNActionEdit;
        private System.Windows.Forms.ComboBox cbArbeitsbereich;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudArtikelId;
        private System.Windows.Forms.TextBox tbQueueDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbMesToReceiver;
        private System.Windows.Forms.CheckBox cbMesToSupporter;
    }
}
