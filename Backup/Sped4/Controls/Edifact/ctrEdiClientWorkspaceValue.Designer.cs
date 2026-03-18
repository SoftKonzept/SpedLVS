namespace Sped4.Controls.Edifact
{
    partial class ctrEdiClientWorkspaceValue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrEdiClientWorkspaceValue));
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.scMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitDgv = new Telerik.WinControls.UI.SplitPanel();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.menuDgv = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.splitEdit = new Telerik.WinControls.UI.SplitPanel();
            this.panAdd = new System.Windows.Forms.Panel();
            this.tbCreated = new System.Windows.Forms.TextBox();
            this.comboDirection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboAsnArt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboArbeitsbereich = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbAdrMatchCode = new System.Windows.Forms.TextBox();
            this.tbAdrShort = new System.Windows.Forms.TextBox();
            this.nudAdrDirect = new System.Windows.Forms.NumericUpDown();
            this.btnSearchAdr = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbProperty = new System.Windows.Forms.TextBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.lProperty = new System.Windows.Forms.Label();
            this.lValue = new System.Windows.Forms.Label();
            this.lCreated = new System.Windows.Forms.Label();
            this.lId = new System.Windows.Forms.Label();
            this.menuAdd = new Sped4.Controls.AFToolStrip();
            this.tsbtnNewAsnArt = new System.Windows.Forms.ToolStripButton();
            this.tsbtnEdifactSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnEdifactDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCopy = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDgv)).BeginInit();
            this.splitDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.menuDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitEdit)).BeginInit();
            this.splitEdit.SuspendLayout();
            this.panAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrDirect)).BeginInit();
            this.menuAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // afColorLabel1
            // 
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afColorLabel1.Location = new System.Drawing.Point(0, 0);
            this.afColorLabel1.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afColorLabel1.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afColorLabel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afColorLabel1.myText = "EdiAdrWorkspaceAssignment";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(732, 28);
            this.afColorLabel1.TabIndex = 12;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scMain
            // 
            this.scMain.Controls.Add(this.splitDgv);
            this.scMain.Controls.Add(this.splitEdit);
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 28);
            this.scMain.Name = "scMain";
            this.scMain.Size = new System.Drawing.Size(732, 490);
            this.scMain.TabIndex = 13;
            this.scMain.TabStop = false;
            this.scMain.ThemeName = "ControlDefault";
            // 
            // splitDgv
            // 
            this.splitDgv.Controls.Add(this.dgv);
            this.splitDgv.Controls.Add(this.menuDgv);
            this.splitDgv.Location = new System.Drawing.Point(0, 0);
            this.splitDgv.Name = "splitDgv";
            this.splitDgv.Size = new System.Drawing.Size(443, 490);
            this.splitDgv.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.1085165F, 0F);
            this.splitDgv.SizeInfo.SplitterCorrection = new System.Drawing.Size(79, 0);
            this.splitDgv.TabIndex = 0;
            this.splitDgv.TabStop = false;
            this.splitDgv.ThemeName = "ControlDefault";
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 25);
            // 
            // 
            // 
            this.dgv.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgv.MasterTemplate.EnableFiltering = true;
            this.dgv.MasterTemplate.ShowFilteringRow = false;
            this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 240, 150);
            this.dgv.Size = new System.Drawing.Size(443, 465);
            this.dgv.TabIndex = 29;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // menuDgv
            // 
            this.menuDgv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh});
            this.menuDgv.Location = new System.Drawing.Point(0, 0);
            this.menuDgv.myColorFrom = System.Drawing.Color.Azure;
            this.menuDgv.myColorTo = System.Drawing.Color.Blue;
            this.menuDgv.myUnderlineColor = System.Drawing.Color.White;
            this.menuDgv.myUnderlined = true;
            this.menuDgv.Name = "menuDgv";
            this.menuDgv.Size = new System.Drawing.Size(443, 25);
            this.menuDgv.TabIndex = 11;
            this.menuDgv.Text = "afToolStrip1";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefresh.Image")));
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefresh.Text = "aktualisieren";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // splitEdit
            // 
            this.splitEdit.Controls.Add(this.panAdd);
            this.splitEdit.Controls.Add(this.menuAdd);
            this.splitEdit.Location = new System.Drawing.Point(447, 0);
            this.splitEdit.Name = "splitEdit";
            this.splitEdit.Size = new System.Drawing.Size(285, 490);
            this.splitEdit.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.1085165F, 0F);
            this.splitEdit.SizeInfo.SplitterCorrection = new System.Drawing.Size(-79, 0);
            this.splitEdit.TabIndex = 1;
            this.splitEdit.TabStop = false;
            this.splitEdit.ThemeName = "ControlDefault";
            // 
            // panAdd
            // 
            this.panAdd.BackColor = System.Drawing.Color.White;
            this.panAdd.Controls.Add(this.tbCreated);
            this.panAdd.Controls.Add(this.comboDirection);
            this.panAdd.Controls.Add(this.label3);
            this.panAdd.Controls.Add(this.comboAsnArt);
            this.panAdd.Controls.Add(this.label2);
            this.panAdd.Controls.Add(this.comboArbeitsbereich);
            this.panAdd.Controls.Add(this.label10);
            this.panAdd.Controls.Add(this.tbAdrMatchCode);
            this.panAdd.Controls.Add(this.tbAdrShort);
            this.panAdd.Controls.Add(this.nudAdrDirect);
            this.panAdd.Controls.Add(this.btnSearchAdr);
            this.panAdd.Controls.Add(this.label1);
            this.panAdd.Controls.Add(this.tbProperty);
            this.panAdd.Controls.Add(this.tbValue);
            this.panAdd.Controls.Add(this.lProperty);
            this.panAdd.Controls.Add(this.lValue);
            this.panAdd.Controls.Add(this.lCreated);
            this.panAdd.Controls.Add(this.lId);
            this.panAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panAdd.Location = new System.Drawing.Point(0, 31);
            this.panAdd.Name = "panAdd";
            this.panAdd.Size = new System.Drawing.Size(285, 459);
            this.panAdd.TabIndex = 86;
            // 
            // tbCreated
            // 
            this.tbCreated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbCreated.Location = new System.Drawing.Point(88, 269);
            this.tbCreated.Name = "tbCreated";
            this.tbCreated.Size = new System.Drawing.Size(179, 20);
            this.tbCreated.TabIndex = 268;
            // 
            // comboDirection
            // 
            this.comboDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboDirection.FormattingEnabled = true;
            this.comboDirection.Items.AddRange(new object[] {
            "IN",
            "OUT"});
            this.comboDirection.Location = new System.Drawing.Point(88, 239);
            this.comboDirection.Name = "comboDirection";
            this.comboDirection.Size = new System.Drawing.Size(178, 21);
            this.comboDirection.TabIndex = 267;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(13, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 266;
            this.label3.Text = "Direction:";
            // 
            // comboAsnArt
            // 
            this.comboAsnArt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboAsnArt.FormattingEnabled = true;
            this.comboAsnArt.Items.AddRange(new object[] {
            "IN",
            "OUT"});
            this.comboAsnArt.Location = new System.Drawing.Point(103, 153);
            this.comboAsnArt.Name = "comboAsnArt";
            this.comboAsnArt.Size = new System.Drawing.Size(162, 21);
            this.comboAsnArt.TabIndex = 265;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(13, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 264;
            this.label2.Text = "AsnArt:";
            // 
            // comboArbeitsbereich
            // 
            this.comboArbeitsbereich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboArbeitsbereich.FormattingEnabled = true;
            this.comboArbeitsbereich.Items.AddRange(new object[] {
            "IN",
            "OUT"});
            this.comboArbeitsbereich.Location = new System.Drawing.Point(103, 127);
            this.comboArbeitsbereich.Name = "comboArbeitsbereich";
            this.comboArbeitsbereich.Size = new System.Drawing.Size(162, 21);
            this.comboArbeitsbereich.TabIndex = 263;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.DarkBlue;
            this.label10.Location = new System.Drawing.Point(13, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 262;
            this.label10.Text = "Arbeitsbereich:";
            // 
            // tbAdrMatchCode
            // 
            this.tbAdrMatchCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAdrMatchCode.Location = new System.Drawing.Point(14, 71);
            this.tbAdrMatchCode.Name = "tbAdrMatchCode";
            this.tbAdrMatchCode.Size = new System.Drawing.Size(251, 20);
            this.tbAdrMatchCode.TabIndex = 260;
            // 
            // tbAdrShort
            // 
            this.tbAdrShort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAdrShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAdrShort.Enabled = false;
            this.tbAdrShort.Location = new System.Drawing.Point(13, 97);
            this.tbAdrShort.Name = "tbAdrShort";
            this.tbAdrShort.ReadOnly = true;
            this.tbAdrShort.Size = new System.Drawing.Size(253, 20);
            this.tbAdrShort.TabIndex = 261;
            // 
            // nudAdrDirect
            // 
            this.nudAdrDirect.Location = new System.Drawing.Point(126, 43);
            this.nudAdrDirect.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudAdrDirect.Name = "nudAdrDirect";
            this.nudAdrDirect.Size = new System.Drawing.Size(104, 20);
            this.nudAdrDirect.TabIndex = 259;
            this.nudAdrDirect.Leave += new System.EventHandler(this.nudAdrDirect_Leave);
            // 
            // btnSearchAdr
            // 
            this.btnSearchAdr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchAdr.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchAdr.Location = new System.Drawing.Point(14, 43);
            this.btnSearchAdr.Name = "btnSearchAdr";
            this.btnSearchAdr.Size = new System.Drawing.Size(83, 22);
            this.btnSearchAdr.TabIndex = 252;
            this.btnSearchAdr.TabStop = false;
            this.btnSearchAdr.Text = "[Address]";
            this.btnSearchAdr.UseVisualStyleBackColor = true;
            this.btnSearchAdr.Click += new System.EventHandler(this.btnSearchAdr_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 16);
            this.label1.MinimumSize = new System.Drawing.Size(30, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 206;
            this.label1.Text = "0";
            // 
            // tbProperty
            // 
            this.tbProperty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbProperty.Location = new System.Drawing.Point(88, 183);
            this.tbProperty.Name = "tbProperty";
            this.tbProperty.Size = new System.Drawing.Size(178, 20);
            this.tbProperty.TabIndex = 205;
            // 
            // tbValue
            // 
            this.tbValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValue.Location = new System.Drawing.Point(87, 209);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(179, 20);
            this.tbValue.TabIndex = 204;
            // 
            // lProperty
            // 
            this.lProperty.AutoSize = true;
            this.lProperty.ForeColor = System.Drawing.Color.DarkBlue;
            this.lProperty.Location = new System.Drawing.Point(13, 187);
            this.lProperty.Name = "lProperty";
            this.lProperty.Size = new System.Drawing.Size(50, 13);
            this.lProperty.TabIndex = 201;
            this.lProperty.Text = "Property";
            // 
            // lValue
            // 
            this.lValue.AutoSize = true;
            this.lValue.ForeColor = System.Drawing.Color.DarkBlue;
            this.lValue.Location = new System.Drawing.Point(13, 218);
            this.lValue.Name = "lValue";
            this.lValue.Size = new System.Drawing.Size(38, 13);
            this.lValue.TabIndex = 200;
            this.lValue.Text = "Value:";
            // 
            // lCreated
            // 
            this.lCreated.AutoSize = true;
            this.lCreated.ForeColor = System.Drawing.Color.DarkBlue;
            this.lCreated.Location = new System.Drawing.Point(13, 273);
            this.lCreated.Name = "lCreated";
            this.lCreated.Size = new System.Drawing.Size(45, 13);
            this.lCreated.TabIndex = 199;
            this.lCreated.Text = "Erstellt:";
            // 
            // lId
            // 
            this.lId.AutoSize = true;
            this.lId.ForeColor = System.Drawing.Color.DarkBlue;
            this.lId.Location = new System.Drawing.Point(11, 16);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(20, 13);
            this.lId.TabIndex = 196;
            this.lId.Text = "Id:";
            // 
            // menuAdd
            // 
            this.menuAdd.AutoSize = false;
            this.menuAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNewAsnArt,
            this.tsbtnEdifactSave,
            this.tsbtnEdifactDelete,
            this.tsbtnCopy});
            this.menuAdd.Location = new System.Drawing.Point(0, 0);
            this.menuAdd.myColorFrom = System.Drawing.Color.Azure;
            this.menuAdd.myColorTo = System.Drawing.Color.Blue;
            this.menuAdd.myUnderlineColor = System.Drawing.Color.White;
            this.menuAdd.myUnderlined = true;
            this.menuAdd.Name = "menuAdd";
            this.menuAdd.Size = new System.Drawing.Size(285, 31);
            this.menuAdd.TabIndex = 85;
            this.menuAdd.Text = "afToolStrip1";
            // 
            // tsbtnNewAsnArt
            // 
            this.tsbtnNewAsnArt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNewAsnArt.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNewAsnArt.Image")));
            this.tsbtnNewAsnArt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNewAsnArt.Name = "tsbtnNewAsnArt";
            this.tsbtnNewAsnArt.Size = new System.Drawing.Size(23, 28);
            this.tsbtnNewAsnArt.Text = "Neuer AsnArt Eintrag";
            this.tsbtnNewAsnArt.Click += new System.EventHandler(this.tsbtnNewAsnArt_Click);
            // 
            // tsbtnEdifactSave
            // 
            this.tsbtnEdifactSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnEdifactSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnEdifactSave.Image")));
            this.tsbtnEdifactSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEdifactSave.Name = "tsbtnEdifactSave";
            this.tsbtnEdifactSave.Size = new System.Drawing.Size(23, 28);
            this.tsbtnEdifactSave.Text = "Verweis speichern";
            this.tsbtnEdifactSave.Click += new System.EventHandler(this.tsbtnEdifactSave_Click);
            // 
            // tsbtnEdifactDelete
            // 
            this.tsbtnEdifactDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnEdifactDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnEdifactDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEdifactDelete.Name = "tsbtnEdifactDelete";
            this.tsbtnEdifactDelete.Size = new System.Drawing.Size(23, 28);
            this.tsbtnEdifactDelete.Text = "schliessen";
            this.tsbtnEdifactDelete.Click += new System.EventHandler(this.tsbtnEdifactDelete_Click);
            // 
            // tsbtnCopy
            // 
            this.tsbtnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCopy.Image = global::Sped4.Properties.Resources.copy_32;
            this.tsbtnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCopy.Name = "tsbtnCopy";
            this.tsbtnCopy.Size = new System.Drawing.Size(23, 28);
            this.tsbtnCopy.Text = "Kopiere Datensatz";
            this.tsbtnCopy.Click += new System.EventHandler(this.tsbtnCopy_Click);
            // 
            // ctrEdiClientWorkspaceValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrEdiClientWorkspaceValue";
            this.Size = new System.Drawing.Size(732, 518);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDgv)).EndInit();
            this.splitDgv.ResumeLayout(false);
            this.splitDgv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.menuDgv.ResumeLayout(false);
            this.menuDgv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitEdit)).EndInit();
            this.splitEdit.ResumeLayout(false);
            this.panAdd.ResumeLayout(false);
            this.panAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrDirect)).EndInit();
            this.menuAdd.ResumeLayout(false);
            this.menuAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal AFColorLabel afColorLabel1;
        private Telerik.WinControls.UI.RadSplitContainer scMain;
        private Telerik.WinControls.UI.SplitPanel splitDgv;
        private Telerik.WinControls.UI.SplitPanel splitEdit;
        private AFToolStrip menuDgv;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private AFToolStrip menuAdd;
        private System.Windows.Forms.ToolStripButton tsbtnNewAsnArt;
        private System.Windows.Forms.ToolStripButton tsbtnEdifactSave;
        private System.Windows.Forms.ToolStripButton tsbtnEdifactDelete;
        private Telerik.WinControls.UI.RadGridView dgv;
        private System.Windows.Forms.Panel panAdd;
        private System.Windows.Forms.TextBox tbProperty;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label lProperty;
        private System.Windows.Forms.Label lValue;
        private System.Windows.Forms.Label lCreated;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchAdr;
        private System.Windows.Forms.NumericUpDown nudAdrDirect;
        private System.Windows.Forms.TextBox tbAdrMatchCode;
        private System.Windows.Forms.TextBox tbAdrShort;
        private System.Windows.Forms.ComboBox comboAsnArt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboArbeitsbereich;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboDirection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCreated;
        private System.Windows.Forms.ToolStripButton tsbtnCopy;
    }
}
