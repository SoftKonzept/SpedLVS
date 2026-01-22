namespace Sped4
{
    partial class ctrCalcSped
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.afToolStrip3 = new Sped4.Controls.AFToolStrip();
            this.tsbtnRGNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRGSave = new System.Windows.Forms.ToolStripButton();
            this.gbAuftragDaten = new System.Windows.Forms.GroupBox();
            this.tbADREntladestelle = new System.Windows.Forms.TextBox();
            this.tbMCEntladestelle = new System.Windows.Forms.TextBox();
            this.btnEntladestelle = new System.Windows.Forms.Button();
            this.btnEmpfänger = new System.Windows.Forms.Button();
            this.tbADREmpfänger = new System.Windows.Forms.TextBox();
            this.tbMCEmpfänger = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tbKm = new System.Windows.Forms.TextBox();
            this.lGewicht = new System.Windows.Forms.Label();
            this.tbADRAuftraggeber = new System.Windows.Forms.TextBox();
            this.tbMCAuftraggeber = new System.Windows.Forms.TextBox();
            this.btnAuftraggeber = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.panCalPo = new System.Windows.Forms.Panel();
            this.dgvAArtikel = new Sped4.Controls.AFGrid();
            this.Pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Netto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afToolStrip3.SuspendLayout();
            this.gbAuftragDaten.SuspendLayout();
            this.panCalPo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAArtikel)).BeginInit();
            this.SuspendLayout();
            // 
            // afToolStrip3
            // 
            this.afToolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRGNew,
            this.tsbtnRGSave});
            this.afToolStrip3.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip3.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip3.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip3.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip3.myUnderlined = true;
            this.afToolStrip3.Name = "afToolStrip3";
            this.afToolStrip3.Size = new System.Drawing.Size(581, 25);
            this.afToolStrip3.TabIndex = 166;
            this.afToolStrip3.Text = "afToolStrip3";
            // 
            // tsbtnRGNew
            // 
            this.tsbtnRGNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRGNew.Image = global::Sped4.Properties.Resources.add;
            this.tsbtnRGNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRGNew.Name = "tsbtnRGNew";
            this.tsbtnRGNew.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRGNew.Text = "toolStripButton1";
            // 
            // tsbtnRGSave
            // 
            this.tsbtnRGSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRGSave.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnRGSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRGSave.Name = "tsbtnRGSave";
            this.tsbtnRGSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRGSave.Text = "toolStripButton1";
            // 
            // gbAuftragDaten
            // 
            this.gbAuftragDaten.Controls.Add(this.tbADREntladestelle);
            this.gbAuftragDaten.Controls.Add(this.tbMCEntladestelle);
            this.gbAuftragDaten.Controls.Add(this.btnEntladestelle);
            this.gbAuftragDaten.Controls.Add(this.btnEmpfänger);
            this.gbAuftragDaten.Controls.Add(this.tbADREmpfänger);
            this.gbAuftragDaten.Controls.Add(this.tbMCEmpfänger);
            this.gbAuftragDaten.Controls.Add(this.textBox5);
            this.gbAuftragDaten.Controls.Add(this.tbKm);
            this.gbAuftragDaten.Controls.Add(this.lGewicht);
            this.gbAuftragDaten.Controls.Add(this.tbADRAuftraggeber);
            this.gbAuftragDaten.Controls.Add(this.tbMCAuftraggeber);
            this.gbAuftragDaten.Controls.Add(this.btnAuftraggeber);
            this.gbAuftragDaten.Controls.Add(this.label7);
            this.gbAuftragDaten.Controls.Add(this.label8);
            this.gbAuftragDaten.Controls.Add(this.label9);
            this.gbAuftragDaten.Controls.Add(this.textBox7);
            this.gbAuftragDaten.Controls.Add(this.textBox8);
            this.gbAuftragDaten.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAuftragDaten.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbAuftragDaten.Location = new System.Drawing.Point(0, 25);
            this.gbAuftragDaten.Name = "gbAuftragDaten";
            this.gbAuftragDaten.Size = new System.Drawing.Size(581, 169);
            this.gbAuftragDaten.TabIndex = 165;
            this.gbAuftragDaten.TabStop = false;
            this.gbAuftragDaten.Text = "Auftragsdaten";
            // 
            // tbADREntladestelle
            // 
            this.tbADREntladestelle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbADREntladestelle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbADREntladestelle.Enabled = false;
            this.tbADREntladestelle.Location = new System.Drawing.Point(208, 136);
            this.tbADREntladestelle.Name = "tbADREntladestelle";
            this.tbADREntladestelle.ReadOnly = true;
            this.tbADREntladestelle.Size = new System.Drawing.Size(354, 20);
            this.tbADREntladestelle.TabIndex = 175;
            // 
            // tbMCEntladestelle
            // 
            this.tbMCEntladestelle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMCEntladestelle.Location = new System.Drawing.Point(102, 136);
            this.tbMCEntladestelle.Name = "tbMCEntladestelle";
            this.tbMCEntladestelle.Size = new System.Drawing.Size(100, 20);
            this.tbMCEntladestelle.TabIndex = 173;
            // 
            // btnEntladestelle
            // 
            this.btnEntladestelle.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnEntladestelle.Location = new System.Drawing.Point(8, 133);
            this.btnEntladestelle.Name = "btnEntladestelle";
            this.btnEntladestelle.Size = new System.Drawing.Size(88, 22);
            this.btnEntladestelle.TabIndex = 172;
            this.btnEntladestelle.Text = "Entladestelle";
            this.btnEntladestelle.UseVisualStyleBackColor = true;
            // 
            // btnEmpfänger
            // 
            this.btnEmpfänger.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnEmpfänger.Location = new System.Drawing.Point(8, 108);
            this.btnEmpfänger.Name = "btnEmpfänger";
            this.btnEmpfänger.Size = new System.Drawing.Size(88, 22);
            this.btnEmpfänger.TabIndex = 170;
            this.btnEmpfänger.Text = "Empfänger";
            this.btnEmpfänger.UseVisualStyleBackColor = true;
            // 
            // tbADREmpfänger
            // 
            this.tbADREmpfänger.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbADREmpfänger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbADREmpfänger.Enabled = false;
            this.tbADREmpfänger.Location = new System.Drawing.Point(208, 111);
            this.tbADREmpfänger.Name = "tbADREmpfänger";
            this.tbADREmpfänger.ReadOnly = true;
            this.tbADREmpfänger.Size = new System.Drawing.Size(354, 20);
            this.tbADREmpfänger.TabIndex = 174;
            // 
            // tbMCEmpfänger
            // 
            this.tbMCEmpfänger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMCEmpfänger.Location = new System.Drawing.Point(102, 111);
            this.tbMCEmpfänger.Name = "tbMCEmpfänger";
            this.tbMCEmpfänger.Size = new System.Drawing.Size(100, 20);
            this.tbMCEmpfänger.TabIndex = 171;
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(347, 51);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(115, 20);
            this.textBox5.TabIndex = 169;
            // 
            // tbKm
            // 
            this.tbKm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKm.Enabled = false;
            this.tbKm.Location = new System.Drawing.Point(347, 25);
            this.tbKm.Name = "tbKm";
            this.tbKm.Size = new System.Drawing.Size(115, 20);
            this.tbKm.TabIndex = 168;
            // 
            // lGewicht
            // 
            this.lGewicht.AutoSize = true;
            this.lGewicht.ForeColor = System.Drawing.Color.DarkBlue;
            this.lGewicht.Location = new System.Drawing.Point(259, 53);
            this.lGewicht.Name = "lGewicht";
            this.lGewicht.Size = new System.Drawing.Size(49, 13);
            this.lGewicht.TabIndex = 164;
            this.lGewicht.Text = "Gewicht:";
            // 
            // tbADRAuftraggeber
            // 
            this.tbADRAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbADRAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbADRAuftraggeber.Enabled = false;
            this.tbADRAuftraggeber.Location = new System.Drawing.Point(208, 86);
            this.tbADRAuftraggeber.Name = "tbADRAuftraggeber";
            this.tbADRAuftraggeber.ReadOnly = true;
            this.tbADRAuftraggeber.Size = new System.Drawing.Size(354, 20);
            this.tbADRAuftraggeber.TabIndex = 158;
            // 
            // tbMCAuftraggeber
            // 
            this.tbMCAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMCAuftraggeber.Location = new System.Drawing.Point(102, 86);
            this.tbMCAuftraggeber.Name = "tbMCAuftraggeber";
            this.tbMCAuftraggeber.Size = new System.Drawing.Size(100, 20);
            this.tbMCAuftraggeber.TabIndex = 157;
            // 
            // btnAuftraggeber
            // 
            this.btnAuftraggeber.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAuftraggeber.Location = new System.Drawing.Point(8, 85);
            this.btnAuftraggeber.Name = "btnAuftraggeber";
            this.btnAuftraggeber.Size = new System.Drawing.Size(88, 22);
            this.btnAuftraggeber.TabIndex = 156;
            this.btnAuftraggeber.Text = "Auftraggeber";
            this.btnAuftraggeber.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(259, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 163;
            this.label7.Text = "km";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.DarkBlue;
            this.label8.Location = new System.Drawing.Point(10, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 160;
            this.label8.Text = "Auftrag / Artikel ID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Location = new System.Drawing.Point(10, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 162;
            this.label9.Text = "Datum:";
            // 
            // textBox7
            // 
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(127, 25);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(115, 20);
            this.textBox7.TabIndex = 159;
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Enabled = false;
            this.textBox8.Location = new System.Drawing.Point(127, 49);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(115, 20);
            this.textBox8.TabIndex = 161;
            // 
            // panCalPo
            // 
            this.panCalPo.Controls.Add(this.dgvAArtikel);
            this.panCalPo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panCalPo.Location = new System.Drawing.Point(0, 194);
            this.panCalPo.Name = "panCalPo";
            this.panCalPo.Size = new System.Drawing.Size(581, 384);
            this.panCalPo.TabIndex = 167;
            // 
            // dgvAArtikel
            // 
            this.dgvAArtikel.AllowDrop = true;
            this.dgvAArtikel.AllowUserToAddRows = false;
            this.dgvAArtikel.AllowUserToDeleteRows = false;
            this.dgvAArtikel.AllowUserToOrderColumns = true;
            this.dgvAArtikel.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.dgvAArtikel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAArtikel.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAArtikel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAArtikel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pos,
            this.Text,
            this.Netto});
            this.dgvAArtikel.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAArtikel.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAArtikel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAArtikel.Location = new System.Drawing.Point(0, 0);
            this.dgvAArtikel.MultiSelect = false;
            this.dgvAArtikel.Name = "dgvAArtikel";
            this.dgvAArtikel.ReadOnly = true;
            this.dgvAArtikel.RowHeadersVisible = false;
            this.dgvAArtikel.RowTemplate.Height = 82;
            this.dgvAArtikel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAArtikel.ShowEditingIcon = false;
            this.dgvAArtikel.ShowRowErrors = false;
            this.dgvAArtikel.Size = new System.Drawing.Size(581, 384);
            this.dgvAArtikel.TabIndex = 22;
            // 
            // Pos
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.Pos.DefaultCellStyle = dataGridViewCellStyle1;
            this.Pos.HeaderText = "Pos";
            this.Pos.MinimumWidth = 50;
            this.Pos.Name = "Pos";
            this.Pos.ReadOnly = true;
            this.Pos.Width = 60;
            // 
            // Text
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.NullValue = null;
            this.Text.DefaultCellStyle = dataGridViewCellStyle2;
            this.Text.HeaderText = "Text";
            this.Text.MinimumWidth = 400;
            this.Text.Name = "Text";
            this.Text.ReadOnly = true;
            this.Text.Width = 450;
            // 
            // Netto
            // 
            this.Netto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = "0";
            this.Netto.DefaultCellStyle = dataGridViewCellStyle3;
            this.Netto.HeaderText = "Netto";
            this.Netto.MinimumWidth = 60;
            this.Netto.Name = "Netto";
            this.Netto.ReadOnly = true;
            // 
            // ctrCalcSped
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panCalPo);
            this.Controls.Add(this.gbAuftragDaten);
            this.Controls.Add(this.afToolStrip3);
            this.Name = "ctrCalcSped";
            this.Size = new System.Drawing.Size(581, 578);
            this.afToolStrip3.ResumeLayout(false);
            this.afToolStrip3.PerformLayout();
            this.gbAuftragDaten.ResumeLayout(false);
            this.gbAuftragDaten.PerformLayout();
            this.panCalPo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAArtikel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbAuftragDaten;
        private System.Windows.Forms.TextBox tbADREntladestelle;
        private System.Windows.Forms.TextBox tbMCEntladestelle;
        private System.Windows.Forms.Button btnEntladestelle;
        private System.Windows.Forms.Button btnEmpfänger;
        private System.Windows.Forms.TextBox tbADREmpfänger;
        private System.Windows.Forms.TextBox tbMCEmpfänger;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox tbKm;
        private System.Windows.Forms.Label lGewicht;
        private System.Windows.Forms.TextBox tbADRAuftraggeber;
        private System.Windows.Forms.TextBox tbMCAuftraggeber;
        private System.Windows.Forms.Button btnAuftraggeber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private Controls.AFToolStrip afToolStrip3;
        private System.Windows.Forms.ToolStripButton tsbtnRGNew;
        private System.Windows.Forms.ToolStripButton tsbtnRGSave;
        private System.Windows.Forms.Panel panCalPo;
        public Controls.AFGrid dgvAArtikel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Text;
        private System.Windows.Forms.DataGridViewTextBoxColumn Netto;
    }
}
