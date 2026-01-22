namespace Sped4.Controls.ASNCenter
{
    partial class ctrAdrVerweis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrAdrVerweis));
            this.scAdrVerweise = new Telerik.WinControls.UI.RadSplitContainer();
            this.panAdrVerweiseList = new Telerik.WinControls.UI.SplitPanel();
            this.dgvAdrVerweis = new Telerik.WinControls.UI.RadGridView();
            this.panAdrVerweiseEdit = new Telerik.WinControls.UI.SplitPanel();
            this.tbRefPart3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbRefPart2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbRefPart1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboMandant = new System.Windows.Forms.ComboBox();
            this.nudAdrIdDirect = new System.Windows.Forms.NumericUpDown();
            this.tbSupplierNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbASNSenderVerweis = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbUserS713F13 = new System.Windows.Forms.CheckBox();
            this.cbUser712F04 = new System.Windows.Forms.CheckBox();
            this.btnSearchA = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbVerweisADRLong = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.tbADRVerweisBemerkung = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.comboVerweisArt = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cbASNFileTyp = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cbAktiv = new System.Windows.Forms.CheckBox();
            this.tbASNLieferantenNummer = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.tbASNVerweis = new System.Windows.Forms.TextBox();
            this.cbArbeitsbereich = new System.Windows.Forms.ComboBox();
            this.tbVerweisADR = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbId = new System.Windows.Forms.TextBox();
            this.menuASNMain = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefreshAdrVerweis = new System.Windows.Forms.ToolStripButton();
            this.menuVerweisEdit = new Sped4.Controls.AFToolStrip();
            this.tsbtnASNVerweisNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnASNSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnASNVerweisDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSettingExport = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSettingImport = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.scAdrVerweise)).BeginInit();
            this.scAdrVerweise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panAdrVerweiseList)).BeginInit();
            this.panAdrVerweiseList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdrVerweis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdrVerweis.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panAdrVerweiseEdit)).BeginInit();
            this.panAdrVerweiseEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).BeginInit();
            this.menuASNMain.SuspendLayout();
            this.menuVerweisEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // scAdrVerweise
            // 
            this.scAdrVerweise.Controls.Add(this.panAdrVerweiseList);
            this.scAdrVerweise.Controls.Add(this.panAdrVerweiseEdit);
            this.scAdrVerweise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scAdrVerweise.Location = new System.Drawing.Point(0, 0);
            this.scAdrVerweise.Name = "scAdrVerweise";
            this.scAdrVerweise.Size = new System.Drawing.Size(849, 700);
            this.scAdrVerweise.SplitterWidth = 8;
            this.scAdrVerweise.TabIndex = 1;
            this.scAdrVerweise.TabStop = false;
            // 
            // panAdrVerweiseList
            // 
            this.panAdrVerweiseList.Controls.Add(this.dgvAdrVerweis);
            this.panAdrVerweiseList.Controls.Add(this.menuASNMain);
            this.panAdrVerweiseList.Location = new System.Drawing.Point(0, 0);
            this.panAdrVerweiseList.Name = "panAdrVerweiseList";
            this.panAdrVerweiseList.Size = new System.Drawing.Size(425, 700);
            this.panAdrVerweiseList.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.005603969F, 0F);
            this.panAdrVerweiseList.SizeInfo.SplitterCorrection = new System.Drawing.Size(38, 0);
            this.panAdrVerweiseList.TabIndex = 0;
            this.panAdrVerweiseList.TabStop = false;
            this.panAdrVerweiseList.Text = "splitPanel1";
            // 
            // dgvAdrVerweis
            // 
            this.dgvAdrVerweis.BackColor = System.Drawing.Color.White;
            this.dgvAdrVerweis.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvAdrVerweis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdrVerweis.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvAdrVerweis.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvAdrVerweis.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvAdrVerweis.Location = new System.Drawing.Point(0, 25);
            // 
            // 
            // 
            this.dgvAdrVerweis.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvAdrVerweis.MasterTemplate.EnableFiltering = true;
            this.dgvAdrVerweis.MasterTemplate.ShowFilteringRow = false;
            this.dgvAdrVerweis.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvAdrVerweis.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvAdrVerweis.Name = "dgvAdrVerweis";
            this.dgvAdrVerweis.ReadOnly = true;
            this.dgvAdrVerweis.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgvAdrVerweis.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 425, 675);
            this.dgvAdrVerweis.Size = new System.Drawing.Size(425, 675);
            this.dgvAdrVerweis.TabIndex = 27;
            this.dgvAdrVerweis.ThemeName = "ControlDefault";
            this.dgvAdrVerweis.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvAdrVerweis_MouseClick);
            this.dgvAdrVerweis.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvAdrVerweis_MouseDoubleClick);
            // 
            // panAdrVerweiseEdit
            // 
            this.panAdrVerweiseEdit.AutoScroll = true;
            this.panAdrVerweiseEdit.BackColor = System.Drawing.Color.White;
            this.panAdrVerweiseEdit.Controls.Add(this.tbId);
            this.panAdrVerweiseEdit.Controls.Add(this.label13);
            this.panAdrVerweiseEdit.Controls.Add(this.tbRefPart3);
            this.panAdrVerweiseEdit.Controls.Add(this.label10);
            this.panAdrVerweiseEdit.Controls.Add(this.tbRefPart2);
            this.panAdrVerweiseEdit.Controls.Add(this.label11);
            this.panAdrVerweiseEdit.Controls.Add(this.tbRefPart1);
            this.panAdrVerweiseEdit.Controls.Add(this.label12);
            this.panAdrVerweiseEdit.Controls.Add(this.label9);
            this.panAdrVerweiseEdit.Controls.Add(this.label8);
            this.panAdrVerweiseEdit.Controls.Add(this.label7);
            this.panAdrVerweiseEdit.Controls.Add(this.label6);
            this.panAdrVerweiseEdit.Controls.Add(this.label5);
            this.panAdrVerweiseEdit.Controls.Add(this.comboMandant);
            this.panAdrVerweiseEdit.Controls.Add(this.nudAdrIdDirect);
            this.panAdrVerweiseEdit.Controls.Add(this.tbSupplierNo);
            this.panAdrVerweiseEdit.Controls.Add(this.label4);
            this.panAdrVerweiseEdit.Controls.Add(this.tbASNSenderVerweis);
            this.panAdrVerweiseEdit.Controls.Add(this.label3);
            this.panAdrVerweiseEdit.Controls.Add(this.cbUserS713F13);
            this.panAdrVerweiseEdit.Controls.Add(this.cbUser712F04);
            this.panAdrVerweiseEdit.Controls.Add(this.btnSearchA);
            this.panAdrVerweiseEdit.Controls.Add(this.label2);
            this.panAdrVerweiseEdit.Controls.Add(this.label1);
            this.panAdrVerweiseEdit.Controls.Add(this.tbVerweisADRLong);
            this.panAdrVerweiseEdit.Controls.Add(this.label36);
            this.panAdrVerweiseEdit.Controls.Add(this.tbADRVerweisBemerkung);
            this.panAdrVerweiseEdit.Controls.Add(this.label45);
            this.panAdrVerweiseEdit.Controls.Add(this.comboVerweisArt);
            this.panAdrVerweiseEdit.Controls.Add(this.label24);
            this.panAdrVerweiseEdit.Controls.Add(this.cbASNFileTyp);
            this.panAdrVerweiseEdit.Controls.Add(this.label41);
            this.panAdrVerweiseEdit.Controls.Add(this.label26);
            this.panAdrVerweiseEdit.Controls.Add(this.cbAktiv);
            this.panAdrVerweiseEdit.Controls.Add(this.tbASNLieferantenNummer);
            this.panAdrVerweiseEdit.Controls.Add(this.label39);
            this.panAdrVerweiseEdit.Controls.Add(this.label34);
            this.panAdrVerweiseEdit.Controls.Add(this.tbASNVerweis);
            this.panAdrVerweiseEdit.Controls.Add(this.cbArbeitsbereich);
            this.panAdrVerweiseEdit.Controls.Add(this.tbVerweisADR);
            this.panAdrVerweiseEdit.Controls.Add(this.label37);
            this.panAdrVerweiseEdit.Controls.Add(this.menuVerweisEdit);
            this.panAdrVerweiseEdit.Location = new System.Drawing.Point(433, 0);
            this.panAdrVerweiseEdit.Name = "panAdrVerweiseEdit";
            this.panAdrVerweiseEdit.Size = new System.Drawing.Size(416, 700);
            this.panAdrVerweiseEdit.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.005603999F, 0F);
            this.panAdrVerweiseEdit.SizeInfo.SplitterCorrection = new System.Drawing.Size(-38, 0);
            this.panAdrVerweiseEdit.TabIndex = 1;
            this.panAdrVerweiseEdit.TabStop = false;
            this.panAdrVerweiseEdit.Text = "splitPanel2";
            // 
            // tbRefPart3
            // 
            this.tbRefPart3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRefPart3.Enabled = false;
            this.tbRefPart3.Location = new System.Drawing.Point(117, 536);
            this.tbRefPart3.Name = "tbRefPart3";
            this.tbRefPart3.Size = new System.Drawing.Size(148, 22);
            this.tbRefPart3.TabIndex = 233;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.DarkBlue;
            this.label10.Location = new System.Drawing.Point(19, 538);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 232;
            this.label10.Text = "Ref-Part3:";
            // 
            // tbRefPart2
            // 
            this.tbRefPart2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRefPart2.Enabled = false;
            this.tbRefPart2.Location = new System.Drawing.Point(117, 514);
            this.tbRefPart2.Name = "tbRefPart2";
            this.tbRefPart2.Size = new System.Drawing.Size(148, 22);
            this.tbRefPart2.TabIndex = 231;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.DarkBlue;
            this.label11.Location = new System.Drawing.Point(19, 516);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 230;
            this.label11.Text = "Ref-Part 2:";
            // 
            // tbRefPart1
            // 
            this.tbRefPart1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRefPart1.Enabled = false;
            this.tbRefPart1.Location = new System.Drawing.Point(117, 492);
            this.tbRefPart1.Name = "tbRefPart1";
            this.tbRefPart1.Size = new System.Drawing.Size(148, 22);
            this.tbRefPart1.TabIndex = 229;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.DarkBlue;
            this.label12.Location = new System.Drawing.Point(19, 494);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 228;
            this.label12.Text = "Ref-Part 1:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Location = new System.Drawing.Point(20, 652);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(383, 13);
            this.label9.TabIndex = 227;
            this.label9.Text = "Verweis Empfänger : \'ReferenzPart2\'+#+\'ReferenzPart1\'+#\'ReferenzPart3\'";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.DarkBlue;
            this.label8.Location = new System.Drawing.Point(20, 600);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(363, 13);
            this.label8.TabIndex = 226;
            this.label8.Text = "Verweis Sender : \'ReferenzPart1\'+#+\'ReferenzPart2\'+#\'ReferenzPart3\'";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(20, 637);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(308, 13);
            this.label7.TabIndex = 225;
            this.label7.Text = "Verweis Empfänger : \'NAD+CN\'+#+\'NAD+CZ\'+#\'NAD+FW\'";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(20, 584);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(288, 13);
            this.label6.TabIndex = 224;
            this.label6.Text = "Verweis Sender : \'NAD+CZ\'+#+\'NAD+CN\'+#\'NAD+FW\'";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(19, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 222;
            this.label5.Text = "Mandant:";
            // 
            // comboMandant
            // 
            this.comboMandant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboMandant.FormattingEnabled = true;
            this.comboMandant.Items.AddRange(new object[] {
            "IN",
            "OUT"});
            this.comboMandant.Location = new System.Drawing.Point(118, 143);
            this.comboMandant.Name = "comboMandant";
            this.comboMandant.Size = new System.Drawing.Size(244, 21);
            this.comboMandant.TabIndex = 223;
            // 
            // nudAdrIdDirect
            // 
            this.nudAdrIdDirect.Location = new System.Drawing.Point(242, 59);
            this.nudAdrIdDirect.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudAdrIdDirect.Name = "nudAdrIdDirect";
            this.nudAdrIdDirect.Size = new System.Drawing.Size(88, 22);
            this.nudAdrIdDirect.TabIndex = 221;
            this.nudAdrIdDirect.Leave += new System.EventHandler(this.nudAdrIdDirect_Leave);
            // 
            // tbSupplierNo
            // 
            this.tbSupplierNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSupplierNo.Enabled = false;
            this.tbSupplierNo.Location = new System.Drawing.Point(117, 296);
            this.tbSupplierNo.Name = "tbSupplierNo";
            this.tbSupplierNo.Size = new System.Drawing.Size(148, 22);
            this.tbSupplierNo.TabIndex = 220;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(19, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 219;
            this.label4.Text = "SupplierNo:";
            // 
            // tbASNSenderVerweis
            // 
            this.tbASNSenderVerweis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbASNSenderVerweis.Enabled = false;
            this.tbASNSenderVerweis.Location = new System.Drawing.Point(117, 273);
            this.tbASNSenderVerweis.Name = "tbASNSenderVerweis";
            this.tbASNSenderVerweis.Size = new System.Drawing.Size(148, 22);
            this.tbASNSenderVerweis.TabIndex = 218;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(19, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 217;
            this.label3.Text = "Sender-Verweis:";
            // 
            // cbUserS713F13
            // 
            this.cbUserS713F13.AutoSize = true;
            this.cbUserS713F13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbUserS713F13.Location = new System.Drawing.Point(117, 356);
            this.cbUserS713F13.Name = "cbUserS713F13";
            this.cbUserS713F13.Size = new System.Drawing.Size(99, 17);
            this.cbUserS713F13.TabIndex = 216;
            this.cbUserS713F13.Text = "UseSatz713F13";
            this.cbUserS713F13.UseVisualStyleBackColor = true;
            // 
            // cbUser712F04
            // 
            this.cbUser712F04.AutoSize = true;
            this.cbUser712F04.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbUser712F04.Location = new System.Drawing.Point(117, 339);
            this.cbUser712F04.Name = "cbUser712F04";
            this.cbUser712F04.Size = new System.Drawing.Size(99, 17);
            this.cbUser712F04.TabIndex = 215;
            this.cbUser712F04.Text = "UseSatz712F04";
            this.cbUser712F04.UseVisualStyleBackColor = true;
            // 
            // btnSearchA
            // 
            this.btnSearchA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchA.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchA.Location = new System.Drawing.Point(20, 87);
            this.btnSearchA.Name = "btnSearchA";
            this.btnSearchA.Size = new System.Drawing.Size(80, 29);
            this.btnSearchA.TabIndex = 214;
            this.btnSearchA.TabStop = false;
            this.btnSearchA.Text = "[ADR]";
            this.btnSearchA.UseVisualStyleBackColor = true;
            this.btnSearchA.Click += new System.EventHandler(this.btnSearchA_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(20, 622);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(323, 13);
            this.label2.TabIndex = 213;
            this.label2.Text = "Verweis Empfänger : Satz711F03+#+Satz711F04+#Satz713F13";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(19, 569);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 13);
            this.label1.TabIndex = 212;
            this.label1.Text = "Verweis Sender : Satz711F04+#+Satz711F03+#Satz712F04";
            // 
            // tbVerweisADRLong
            // 
            this.tbVerweisADRLong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVerweisADRLong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVerweisADRLong.Enabled = false;
            this.tbVerweisADRLong.Location = new System.Drawing.Point(117, 87);
            this.tbVerweisADRLong.Name = "tbVerweisADRLong";
            this.tbVerweisADRLong.ReadOnly = true;
            this.tbVerweisADRLong.Size = new System.Drawing.Size(264, 22);
            this.tbVerweisADRLong.TabIndex = 211;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.ForeColor = System.Drawing.Color.DarkBlue;
            this.label36.Location = new System.Drawing.Point(19, 63);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(88, 13);
            this.label36.TabIndex = 210;
            this.label36.Text = "Verweisadresse:";
            // 
            // tbADRVerweisBemerkung
            // 
            this.tbADRVerweisBemerkung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbADRVerweisBemerkung.Enabled = false;
            this.tbADRVerweisBemerkung.Location = new System.Drawing.Point(117, 378);
            this.tbADRVerweisBemerkung.Multiline = true;
            this.tbADRVerweisBemerkung.Name = "tbADRVerweisBemerkung";
            this.tbADRVerweisBemerkung.Size = new System.Drawing.Size(245, 108);
            this.tbADRVerweisBemerkung.TabIndex = 209;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.ForeColor = System.Drawing.Color.DarkBlue;
            this.label45.Location = new System.Drawing.Point(19, 380);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(69, 13);
            this.label45.TabIndex = 208;
            this.label45.Text = "Bemerkung:";
            // 
            // comboVerweisArt
            // 
            this.comboVerweisArt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboVerweisArt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboVerweisArt.FormattingEnabled = true;
            this.comboVerweisArt.Location = new System.Drawing.Point(118, 197);
            this.comboVerweisArt.Name = "comboVerweisArt";
            this.comboVerweisArt.Size = new System.Drawing.Size(244, 21);
            this.comboVerweisArt.TabIndex = 207;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.DarkBlue;
            this.label24.Location = new System.Drawing.Point(19, 200);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(63, 13);
            this.label24.TabIndex = 206;
            this.label24.Text = "Verweisart:";
            // 
            // cbASNFileTyp
            // 
            this.cbASNFileTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbASNFileTyp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbASNFileTyp.FormattingEnabled = true;
            this.cbASNFileTyp.Location = new System.Drawing.Point(117, 170);
            this.cbASNFileTyp.Name = "cbASNFileTyp";
            this.cbASNFileTyp.Size = new System.Drawing.Size(244, 21);
            this.cbASNFileTyp.TabIndex = 205;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.ForeColor = System.Drawing.Color.DarkBlue;
            this.label41.Location = new System.Drawing.Point(19, 173);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(82, 13);
            this.label41.TabIndex = 204;
            this.label41.Text = "Meldungs-Typ:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.DarkBlue;
            this.label26.Location = new System.Drawing.Point(19, 323);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(39, 13);
            this.label26.TabIndex = 203;
            this.label26.Text = "Status";
            // 
            // cbAktiv
            // 
            this.cbAktiv.AutoSize = true;
            this.cbAktiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAktiv.Location = new System.Drawing.Point(117, 322);
            this.cbAktiv.Name = "cbAktiv";
            this.cbAktiv.Size = new System.Drawing.Size(47, 17);
            this.cbAktiv.TabIndex = 202;
            this.cbAktiv.Text = "aktiv";
            this.cbAktiv.UseVisualStyleBackColor = true;
            // 
            // tbASNLieferantenNummer
            // 
            this.tbASNLieferantenNummer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbASNLieferantenNummer.Enabled = false;
            this.tbASNLieferantenNummer.Location = new System.Drawing.Point(117, 250);
            this.tbASNLieferantenNummer.Name = "tbASNLieferantenNummer";
            this.tbASNLieferantenNummer.Size = new System.Drawing.Size(148, 22);
            this.tbASNLieferantenNummer.TabIndex = 200;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.ForeColor = System.Drawing.Color.DarkBlue;
            this.label39.Location = new System.Drawing.Point(19, 252);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(77, 13);
            this.label39.TabIndex = 199;
            this.label39.Text = "LieferantenNr";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.DarkBlue;
            this.label34.Location = new System.Drawing.Point(19, 122);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(81, 13);
            this.label34.TabIndex = 195;
            this.label34.Text = "Arbeitsbereich";
            // 
            // tbASNVerweis
            // 
            this.tbASNVerweis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbASNVerweis.Enabled = false;
            this.tbASNVerweis.Location = new System.Drawing.Point(117, 228);
            this.tbASNVerweis.Name = "tbASNVerweis";
            this.tbASNVerweis.Size = new System.Drawing.Size(148, 22);
            this.tbASNVerweis.TabIndex = 198;
            // 
            // cbArbeitsbereich
            // 
            this.cbArbeitsbereich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArbeitsbereich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbArbeitsbereich.FormattingEnabled = true;
            this.cbArbeitsbereich.Location = new System.Drawing.Point(117, 119);
            this.cbArbeitsbereich.Name = "cbArbeitsbereich";
            this.cbArbeitsbereich.Size = new System.Drawing.Size(244, 21);
            this.cbArbeitsbereich.TabIndex = 196;
            // 
            // tbVerweisADR
            // 
            this.tbVerweisADR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVerweisADR.Enabled = false;
            this.tbVerweisADR.Location = new System.Drawing.Point(117, 59);
            this.tbVerweisADR.Name = "tbVerweisADR";
            this.tbVerweisADR.Size = new System.Drawing.Size(109, 22);
            this.tbVerweisADR.TabIndex = 193;
            this.tbVerweisADR.TextChanged += new System.EventHandler(this.tbVerweisADR_TextChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.ForeColor = System.Drawing.Color.DarkBlue;
            this.label37.Location = new System.Drawing.Point(19, 230);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(49, 13);
            this.label37.TabIndex = 197;
            this.label37.Text = "Verweis:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.DarkBlue;
            this.label13.Location = new System.Drawing.Point(17, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 234;
            this.label13.Text = "Id:";
            // 
            // tbId
            // 
            this.tbId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbId.Enabled = false;
            this.tbId.Location = new System.Drawing.Point(117, 32);
            this.tbId.Name = "tbId";
            this.tbId.ReadOnly = true;
            this.tbId.Size = new System.Drawing.Size(109, 22);
            this.tbId.TabIndex = 235;
            this.tbId.Text = "0";
            // 
            // menuASNMain
            // 
            this.menuASNMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefreshAdrVerweis});
            this.menuASNMain.Location = new System.Drawing.Point(0, 0);
            this.menuASNMain.myColorFrom = System.Drawing.Color.Azure;
            this.menuASNMain.myColorTo = System.Drawing.Color.Blue;
            this.menuASNMain.myUnderlineColor = System.Drawing.Color.White;
            this.menuASNMain.myUnderlined = true;
            this.menuASNMain.Name = "menuASNMain";
            this.menuASNMain.Size = new System.Drawing.Size(425, 25);
            this.menuASNMain.TabIndex = 9;
            this.menuASNMain.Text = "afToolStrip1";
            // 
            // tsbtnRefreshAdrVerweis
            // 
            this.tsbtnRefreshAdrVerweis.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefreshAdrVerweis.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefreshAdrVerweis.Image")));
            this.tsbtnRefreshAdrVerweis.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefreshAdrVerweis.Name = "tsbtnRefreshAdrVerweis";
            this.tsbtnRefreshAdrVerweis.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefreshAdrVerweis.Text = "aktualisieren";
            this.tsbtnRefreshAdrVerweis.Click += new System.EventHandler(this.tsbtnRefreshAdrVerweis_Click);
            // 
            // menuVerweisEdit
            // 
            this.menuVerweisEdit.AutoSize = false;
            this.menuVerweisEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnASNVerweisNew,
            this.tsbtnASNSave,
            this.tsbtnASNVerweisDelete,
            this.tsbtnSettingExport,
            this.tsbtnSettingImport});
            this.menuVerweisEdit.Location = new System.Drawing.Point(0, 0);
            this.menuVerweisEdit.myColorFrom = System.Drawing.Color.Azure;
            this.menuVerweisEdit.myColorTo = System.Drawing.Color.Blue;
            this.menuVerweisEdit.myUnderlineColor = System.Drawing.Color.White;
            this.menuVerweisEdit.myUnderlined = true;
            this.menuVerweisEdit.Name = "menuVerweisEdit";
            this.menuVerweisEdit.Size = new System.Drawing.Size(416, 31);
            this.menuVerweisEdit.TabIndex = 83;
            this.menuVerweisEdit.Text = "afToolStrip1";
            // 
            // tsbtnASNVerweisNew
            // 
            this.tsbtnASNVerweisNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnASNVerweisNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnASNVerweisNew.Image")));
            this.tsbtnASNVerweisNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnASNVerweisNew.Name = "tsbtnASNVerweisNew";
            this.tsbtnASNVerweisNew.Size = new System.Drawing.Size(23, 28);
            this.tsbtnASNVerweisNew.Text = "Neuen";
            this.tsbtnASNVerweisNew.Click += new System.EventHandler(this.tsbtnASNVerweisNew_Click);
            // 
            // tsbtnASNSave
            // 
            this.tsbtnASNSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnASNSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnASNSave.Image")));
            this.tsbtnASNSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnASNSave.Name = "tsbtnASNSave";
            this.tsbtnASNSave.Size = new System.Drawing.Size(23, 28);
            this.tsbtnASNSave.Text = "Verweis speichern";
            this.tsbtnASNSave.Click += new System.EventHandler(this.tsbtnASNSave_Click);
            // 
            // tsbtnASNVerweisDelete
            // 
            this.tsbtnASNVerweisDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnASNVerweisDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnASNVerweisDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnASNVerweisDelete.Name = "tsbtnASNVerweisDelete";
            this.tsbtnASNVerweisDelete.Size = new System.Drawing.Size(23, 28);
            this.tsbtnASNVerweisDelete.Text = "schliessen";
            this.tsbtnASNVerweisDelete.Click += new System.EventHandler(this.tsbtnASNVerweisDelete_Click);
            // 
            // tsbtnSettingExport
            // 
            this.tsbtnSettingExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSettingExport.Image = global::Sped4.Properties.Resources.box_out_24x24;
            this.tsbtnSettingExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSettingExport.Name = "tsbtnSettingExport";
            this.tsbtnSettingExport.Size = new System.Drawing.Size(23, 28);
            this.tsbtnSettingExport.ToolTipText = "Export Einstellungen";
            this.tsbtnSettingExport.Click += new System.EventHandler(this.tsbtnSettingExport_Click);
            // 
            // tsbtnSettingImport
            // 
            this.tsbtnSettingImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSettingImport.Image = global::Sped4.Properties.Resources.box_into_24x24;
            this.tsbtnSettingImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSettingImport.Name = "tsbtnSettingImport";
            this.tsbtnSettingImport.Size = new System.Drawing.Size(23, 28);
            this.tsbtnSettingImport.ToolTipText = "Einstellungen importieren";
            this.tsbtnSettingImport.Click += new System.EventHandler(this.tsbtnSettingImport_Click);
            // 
            // ctrAdrVerweis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scAdrVerweise);
            this.Name = "ctrAdrVerweis";
            this.Size = new System.Drawing.Size(849, 700);
            this.Load += new System.EventHandler(this.ctrAdrVerweis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scAdrVerweise)).EndInit();
            this.scAdrVerweise.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panAdrVerweiseList)).EndInit();
            this.panAdrVerweiseList.ResumeLayout(false);
            this.panAdrVerweiseList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdrVerweis.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdrVerweis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panAdrVerweiseEdit)).EndInit();
            this.panAdrVerweiseEdit.ResumeLayout(false);
            this.panAdrVerweiseEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrIdDirect)).EndInit();
            this.menuASNMain.ResumeLayout(false);
            this.menuASNMain.PerformLayout();
            this.menuVerweisEdit.ResumeLayout(false);
            this.menuVerweisEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer scAdrVerweise;
        private Telerik.WinControls.UI.SplitPanel panAdrVerweiseList;
        private Telerik.WinControls.UI.RadGridView dgvAdrVerweis;
        private AFToolStrip menuASNMain;
        private System.Windows.Forms.ToolStripButton tsbtnRefreshAdrVerweis;
        private Telerik.WinControls.UI.SplitPanel panAdrVerweiseEdit;
        private System.Windows.Forms.TextBox tbADRVerweisBemerkung;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox comboVerweisArt;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cbASNFileTyp;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox cbAktiv;
        private System.Windows.Forms.TextBox tbASNLieferantenNummer;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox tbASNVerweis;
        private System.Windows.Forms.ComboBox cbArbeitsbereich;
        private System.Windows.Forms.TextBox tbVerweisADR;
        private System.Windows.Forms.Label label37;
        private AFToolStrip menuVerweisEdit;
        private System.Windows.Forms.ToolStripButton tsbtnASNVerweisNew;
        private System.Windows.Forms.ToolStripButton tsbtnASNSave;
        private System.Windows.Forms.ToolStripButton tsbtnASNVerweisDelete;
        private System.Windows.Forms.TextBox tbVerweisADRLong;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchA;
        private System.Windows.Forms.CheckBox cbUserS713F13;
        private System.Windows.Forms.CheckBox cbUser712F04;
        private System.Windows.Forms.TextBox tbASNSenderVerweis;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSupplierNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudAdrIdDirect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboMandant;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRefPart3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbRefPart2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbRefPart1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripButton tsbtnSettingExport;
        private System.Windows.Forms.ToolStripButton tsbtnSettingImport;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbId;
    }
}
