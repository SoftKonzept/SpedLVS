namespace Sped4
{
    partial class ctrArbeistlisteAufgaben
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
            this.radSplitContainer2 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.dgvArtikel = new Telerik.WinControls.UI.RadGridView();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.panExtraChargeEdit = new System.Windows.Forms.Panel();
            this.tbPreis = new System.Windows.Forms.TextBox();
            this.cbExtraCharge = new System.Windows.Forms.ComboBox();
            this.nudECAssMenge = new System.Windows.Forms.NumericUpDown();
            this.tbECAssRGText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbEinheit = new System.Windows.Forms.ComboBox();
            this.dtpECAssDatum = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnAddAufgabe = new System.Windows.Forms.ToolStripButton();
            this.tsBtnCancel = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer2)).BeginInit();
            this.radSplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtikel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtikel.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.panExtraChargeEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudECAssMenge)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radSplitContainer2
            // 
            this.radSplitContainer2.Controls.Add(this.splitPanel1);
            this.radSplitContainer2.Controls.Add(this.splitPanel2);
            this.radSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer2.Location = new System.Drawing.Point(0, 27);
            this.radSplitContainer2.Name = "radSplitContainer2";
            // 
            // 
            // 
            this.radSplitContainer2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radSplitContainer2.Size = new System.Drawing.Size(871, 576);
            this.radSplitContainer2.SplitterWidth = 5;
            this.radSplitContainer2.TabIndex = 4;
            this.radSplitContainer2.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.dgvArtikel);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(433, 576);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // dgvArtikel
            // 
            this.dgvArtikel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvArtikel.Location = new System.Drawing.Point(0, 0);
            this.dgvArtikel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgvArtikel.MasterTemplate.AllowAddNewRow = false;
            this.dgvArtikel.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvArtikel.Name = "dgvArtikel";
            this.dgvArtikel.ReadOnly = true;
            this.dgvArtikel.Size = new System.Drawing.Size(433, 576);
            this.dgvArtikel.TabIndex = 1;
            this.dgvArtikel.ThemeName = "ControlDefault";
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.panExtraChargeEdit);
            this.splitPanel2.Location = new System.Drawing.Point(438, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel2.Size = new System.Drawing.Size(433, 576);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // panExtraChargeEdit
            // 
            this.panExtraChargeEdit.BackColor = System.Drawing.Color.White;
            this.panExtraChargeEdit.Controls.Add(this.tbPreis);
            this.panExtraChargeEdit.Controls.Add(this.cbExtraCharge);
            this.panExtraChargeEdit.Controls.Add(this.nudECAssMenge);
            this.panExtraChargeEdit.Controls.Add(this.tbECAssRGText);
            this.panExtraChargeEdit.Controls.Add(this.label1);
            this.panExtraChargeEdit.Controls.Add(this.cbEinheit);
            this.panExtraChargeEdit.Controls.Add(this.dtpECAssDatum);
            this.panExtraChargeEdit.Controls.Add(this.label6);
            this.panExtraChargeEdit.Controls.Add(this.label5);
            this.panExtraChargeEdit.Controls.Add(this.label4);
            this.panExtraChargeEdit.Controls.Add(this.label2);
            this.panExtraChargeEdit.Controls.Add(this.label3);
            this.panExtraChargeEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panExtraChargeEdit.Location = new System.Drawing.Point(0, 0);
            this.panExtraChargeEdit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panExtraChargeEdit.Name = "panExtraChargeEdit";
            this.panExtraChargeEdit.Size = new System.Drawing.Size(433, 576);
            this.panExtraChargeEdit.TabIndex = 13;
            // 
            // tbPreis
            // 
            this.tbPreis.Location = new System.Drawing.Point(252, 139);
            this.tbPreis.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbPreis.Name = "tbPreis";
            this.tbPreis.Size = new System.Drawing.Size(219, 22);
            this.tbPreis.TabIndex = 76;
            // 
            // cbExtraCharge
            // 
            this.cbExtraCharge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExtraCharge.FormattingEnabled = true;
            this.cbExtraCharge.Location = new System.Drawing.Point(252, 19);
            this.cbExtraCharge.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbExtraCharge.Name = "cbExtraCharge";
            this.cbExtraCharge.Size = new System.Drawing.Size(144, 24);
            this.cbExtraCharge.TabIndex = 75;
            this.cbExtraCharge.SelectedIndexChanged += new System.EventHandler(this.cbExtraCharge_SelectedIndexChanged);
            // 
            // nudECAssMenge
            // 
            this.nudECAssMenge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudECAssMenge.Location = new System.Drawing.Point(252, 326);
            this.nudECAssMenge.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.nudECAssMenge.Name = "nudECAssMenge";
            this.nudECAssMenge.Size = new System.Drawing.Size(147, 22);
            this.nudECAssMenge.TabIndex = 74;
            this.nudECAssMenge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudECAssMenge.Visible = false;
            // 
            // tbECAssRGText
            // 
            this.tbECAssRGText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbECAssRGText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbECAssRGText.Enabled = false;
            this.tbECAssRGText.Location = new System.Drawing.Point(31, 214);
            this.tbECAssRGText.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tbECAssRGText.Name = "tbECAssRGText";
            this.tbECAssRGText.Size = new System.Drawing.Size(367, 22);
            this.tbECAssRGText.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(28, 188);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 19);
            this.label1.TabIndex = 73;
            this.label1.Text = "Rechnungstext:";
            // 
            // cbEinheit
            // 
            this.cbEinheit.AllowDrop = true;
            this.cbEinheit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEinheit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEinheit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEinheit.FormattingEnabled = true;
            this.cbEinheit.Location = new System.Drawing.Point(252, 98);
            this.cbEinheit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbEinheit.Name = "cbEinheit";
            this.cbEinheit.Size = new System.Drawing.Size(144, 24);
            this.cbEinheit.TabIndex = 3;
            // 
            // dtpECAssDatum
            // 
            this.dtpECAssDatum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpECAssDatum.Location = new System.Drawing.Point(252, 61);
            this.dtpECAssDatum.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dtpECAssDatum.Name = "dtpECAssDatum";
            this.dtpECAssDatum.Size = new System.Drawing.Size(144, 22);
            this.dtpECAssDatum.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(28, 330);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 19);
            this.label6.TabIndex = 72;
            this.label6.Text = "Menge / Anzahl:";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(28, 139);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 19);
            this.label5.TabIndex = 71;
            this.label5.Text = "Preis / Einheit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(28, 101);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 19);
            this.label4.TabIndex = 69;
            this.label4.Text = "Einheit:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(28, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 19);
            this.label2.TabIndex = 67;
            this.label2.Text = "Datum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(28, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 19);
            this.label3.TabIndex = 65;
            this.label3.Text = "Sonderkosten:";
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddAufgabe,
            this.tsBtnCancel});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(871, 27);
            this.afToolStrip1.TabIndex = 2;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnAddAufgabe
            // 
            this.tsbtnAddAufgabe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddAufgabe.Image = global::Sped4.Properties.Resources.add;
            this.tsbtnAddAufgabe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddAufgabe.Name = "tsbtnAddAufgabe";
            this.tsbtnAddAufgabe.Size = new System.Drawing.Size(29, 24);
            this.tsbtnAddAufgabe.Text = "Zuweisen";
            this.tsbtnAddAufgabe.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsBtnCancel
            // 
            this.tsBtnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnCancel.Image = global::Sped4.Properties.Resources.delete;
            this.tsBtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnCancel.Name = "tsBtnCancel";
            this.tsBtnCancel.Size = new System.Drawing.Size(29, 24);
            this.tsBtnCancel.Text = "Abbrechen";
            this.tsBtnCancel.Click += new System.EventHandler(this.tsBtnCancel_Click);
            // 
            // ctrArbeistlisteAufgaben
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSplitContainer2);
            this.Controls.Add(this.afToolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrArbeistlisteAufgaben";
            this.Size = new System.Drawing.Size(871, 603);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer2)).EndInit();
            this.radSplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtikel.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArtikel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.panExtraChargeEdit.ResumeLayout(false);
            this.panExtraChargeEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudECAssMenge)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnAddAufgabe;
        private System.Windows.Forms.ToolStripButton tsBtnCancel;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer2;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private System.Windows.Forms.Panel panExtraChargeEdit;
        private System.Windows.Forms.TextBox tbECAssRGText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbEinheit;
        private System.Windows.Forms.DateTimePicker dtpECAssDatum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbExtraCharge;
        private System.Windows.Forms.NumericUpDown nudECAssMenge;
        private System.Windows.Forms.TextBox tbPreis;
        private Telerik.WinControls.UI.RadGridView dgvArtikel;
    }
}
