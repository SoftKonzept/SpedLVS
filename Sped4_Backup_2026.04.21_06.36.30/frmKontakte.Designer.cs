namespace Sped4
{
    partial class frmKontakte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKontakte));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbKontaktdaten = new System.Windows.Forms.GroupBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lSSuchbegriff = new System.Windows.Forms.Label();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.lInfo = new System.Windows.Forms.Label();
            this.tbAP = new System.Windows.Forms.TextBox();
            this.lAbteilung = new System.Windows.Forms.Label();
            this.lAnsprechpartner = new System.Windows.Forms.Label();
            this.lTelefon = new System.Windows.Forms.Label();
            this.tbAbt = new System.Windows.Forms.TextBox();
            this.tbTel = new System.Windows.Forms.TextBox();
            this.lFax = new System.Windows.Forms.Label();
            this.lMail = new System.Windows.Forms.Label();
            this.tbFax = new System.Windows.Forms.TextBox();
            this.tbMail = new System.Windows.Forms.TextBox();
            this.btnAbbruch = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.lKontaktname = new System.Windows.Forms.Label();
            this.tbKontaktADR = new System.Windows.Forms.TextBox();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExcelExport = new System.Windows.Forms.ToolStripButton();
            this.grdKontakte = new Sped4.Controls.AFGrid();
            this.tbTextSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbKontaktdaten.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKontakte)).BeginInit();
            this.SuspendLayout();
            // 
            // gbKontaktdaten
            // 
            this.gbKontaktdaten.Controls.Add(this.txtSearch);
            this.gbKontaktdaten.Controls.Add(this.lSSuchbegriff);
            this.gbKontaktdaten.Controls.Add(this.tbInfo);
            this.gbKontaktdaten.Controls.Add(this.lInfo);
            this.gbKontaktdaten.Controls.Add(this.tbAP);
            this.gbKontaktdaten.Controls.Add(this.lAbteilung);
            this.gbKontaktdaten.Controls.Add(this.lAnsprechpartner);
            this.gbKontaktdaten.Controls.Add(this.lTelefon);
            this.gbKontaktdaten.Controls.Add(this.tbAbt);
            this.gbKontaktdaten.Controls.Add(this.tbTel);
            this.gbKontaktdaten.Controls.Add(this.lFax);
            this.gbKontaktdaten.Controls.Add(this.lMail);
            this.gbKontaktdaten.Controls.Add(this.tbFax);
            this.gbKontaktdaten.Controls.Add(this.tbMail);
            this.gbKontaktdaten.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbKontaktdaten.Location = new System.Drawing.Point(12, 220);
            this.gbKontaktdaten.Name = "gbKontaktdaten";
            this.gbKontaktdaten.Size = new System.Drawing.Size(577, 246);
            this.gbKontaktdaten.TabIndex = 31;
            this.gbKontaktdaten.TabStop = false;
            this.gbKontaktdaten.Text = "Kontaktdaten";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(126, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(382, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lSSuchbegriff
            // 
            this.lSSuchbegriff.AutoSize = true;
            this.lSSuchbegriff.Location = new System.Drawing.Point(28, 22);
            this.lSSuchbegriff.Name = "lSSuchbegriff";
            this.lSSuchbegriff.Size = new System.Drawing.Size(61, 13);
            this.lSSuchbegriff.TabIndex = 25;
            this.lSSuchbegriff.Text = "Suchbegriff";
            // 
            // tbInfo
            // 
            this.tbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbInfo.Location = new System.Drawing.Point(126, 178);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(382, 60);
            this.tbInfo.TabIndex = 7;
            // 
            // lInfo
            // 
            this.lInfo.AutoSize = true;
            this.lInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lInfo.Location = new System.Drawing.Point(28, 181);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(25, 13);
            this.lInfo.TabIndex = 23;
            this.lInfo.Text = "Info";
            // 
            // tbAP
            // 
            this.tbAP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAP.Location = new System.Drawing.Point(126, 88);
            this.tbAP.Name = "tbAP";
            this.tbAP.Size = new System.Drawing.Size(382, 20);
            this.tbAP.TabIndex = 3;
            this.tbAP.Visible = false;
            // 
            // lAbteilung
            // 
            this.lAbteilung.AutoSize = true;
            this.lAbteilung.ForeColor = System.Drawing.Color.DarkBlue;
            this.lAbteilung.Location = new System.Drawing.Point(28, 69);
            this.lAbteilung.Name = "lAbteilung";
            this.lAbteilung.Size = new System.Drawing.Size(51, 13);
            this.lAbteilung.TabIndex = 0;
            this.lAbteilung.Text = "Abteilung";
            // 
            // lAnsprechpartner
            // 
            this.lAnsprechpartner.AutoSize = true;
            this.lAnsprechpartner.ForeColor = System.Drawing.Color.DarkBlue;
            this.lAnsprechpartner.Location = new System.Drawing.Point(28, 91);
            this.lAnsprechpartner.Name = "lAnsprechpartner";
            this.lAnsprechpartner.Size = new System.Drawing.Size(85, 13);
            this.lAnsprechpartner.TabIndex = 1;
            this.lAnsprechpartner.Text = "Ansprechpartner";
            // 
            // lTelefon
            // 
            this.lTelefon.AutoSize = true;
            this.lTelefon.BackColor = System.Drawing.Color.White;
            this.lTelefon.ForeColor = System.Drawing.Color.DarkBlue;
            this.lTelefon.Location = new System.Drawing.Point(28, 113);
            this.lTelefon.Name = "lTelefon";
            this.lTelefon.Size = new System.Drawing.Size(43, 13);
            this.lTelefon.TabIndex = 2;
            this.lTelefon.Text = "Telefon";
            // 
            // tbAbt
            // 
            this.tbAbt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAbt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAbt.Location = new System.Drawing.Point(126, 66);
            this.tbAbt.Name = "tbAbt";
            this.tbAbt.Size = new System.Drawing.Size(382, 20);
            this.tbAbt.TabIndex = 2;
            this.tbAbt.Visible = false;
            // 
            // tbTel
            // 
            this.tbTel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTel.Location = new System.Drawing.Point(126, 110);
            this.tbTel.Name = "tbTel";
            this.tbTel.Size = new System.Drawing.Size(382, 20);
            this.tbTel.TabIndex = 4;
            this.tbTel.Visible = false;
            // 
            // lFax
            // 
            this.lFax.AutoSize = true;
            this.lFax.ForeColor = System.Drawing.Color.DarkBlue;
            this.lFax.Location = new System.Drawing.Point(28, 135);
            this.lFax.Name = "lFax";
            this.lFax.Size = new System.Drawing.Size(24, 13);
            this.lFax.TabIndex = 6;
            this.lFax.Text = "Fax";
            // 
            // lMail
            // 
            this.lMail.AutoSize = true;
            this.lMail.ForeColor = System.Drawing.Color.DarkBlue;
            this.lMail.Location = new System.Drawing.Point(28, 159);
            this.lMail.Name = "lMail";
            this.lMail.Size = new System.Drawing.Size(36, 13);
            this.lMail.TabIndex = 8;
            this.lMail.Text = "E-Mail";
            // 
            // tbFax
            // 
            this.tbFax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFax.Location = new System.Drawing.Point(126, 132);
            this.tbFax.Name = "tbFax";
            this.tbFax.Size = new System.Drawing.Size(382, 20);
            this.tbFax.TabIndex = 5;
            this.tbFax.Visible = false;
            // 
            // tbMail
            // 
            this.tbMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMail.Location = new System.Drawing.Point(126, 156);
            this.tbMail.Name = "tbMail";
            this.tbMail.Size = new System.Drawing.Size(382, 20);
            this.tbMail.TabIndex = 6;
            this.tbMail.Visible = false;
            // 
            // btnAbbruch
            // 
            this.btnAbbruch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbbruch.Image = global::Sped4.Properties.Resources.delete_16;
            this.btnAbbruch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbbruch.Location = new System.Drawing.Point(307, 472);
            this.btnAbbruch.Name = "btnAbbruch";
            this.btnAbbruch.Size = new System.Drawing.Size(83, 25);
            this.btnAbbruch.TabIndex = 9;
            this.btnAbbruch.Text = "     &Abbruch";
            this.btnAbbruch.UseVisualStyleBackColor = true;
            this.btnAbbruch.Click += new System.EventHandler(this.btnAbbruch_Click);
            // 
            // btn1
            // 
            this.btn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn1.Image = global::Sped4.Properties.Resources.check;
            this.btn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn1.Location = new System.Drawing.Point(218, 472);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(83, 25);
            this.btn1.TabIndex = 8;
            this.btn1.Text = "     &Speichern";
            this.btn1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // lKontaktname
            // 
            this.lKontaktname.AutoSize = true;
            this.lKontaktname.ForeColor = System.Drawing.Color.DarkBlue;
            this.lKontaktname.Location = new System.Drawing.Point(15, 9);
            this.lKontaktname.Name = "lKontaktname";
            this.lKontaktname.Size = new System.Drawing.Size(76, 13);
            this.lKontaktname.TabIndex = 32;
            this.lKontaktname.Text = "ADR-Kontakte";
            // 
            // tbKontaktADR
            // 
            this.tbKontaktADR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKontaktADR.Enabled = false;
            this.tbKontaktADR.Location = new System.Drawing.Point(107, 6);
            this.tbKontaktADR.Multiline = true;
            this.tbKontaktADR.Name = "tbKontaktADR";
            this.tbKontaktADR.Size = new System.Drawing.Size(194, 51);
            this.tbKontaktADR.TabIndex = 34;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDelete,
            this.tsbtnExcelExport});
            this.afToolStrip1.Location = new System.Drawing.Point(307, 9);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(89, 25);
            this.afToolStrip1.TabIndex = 35;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbDelete.Text = "ausgewählten Kontakt löschen";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbtnExcelExport
            // 
            this.tsbtnExcelExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExcelExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExcelExport.Image")));
            this.tsbtnExcelExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExcelExport.Name = "tsbtnExcelExport";
            this.tsbtnExcelExport.Size = new System.Drawing.Size(23, 22);
            this.tsbtnExcelExport.Text = "Excel Export";
            this.tsbtnExcelExport.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // grdKontakte
            // 
            this.grdKontakte.AllowUserToAddRows = false;
            this.grdKontakte.AllowUserToDeleteRows = false;
            this.grdKontakte.AllowUserToResizeRows = false;
            this.grdKontakte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdKontakte.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grdKontakte.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.grdKontakte.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grdKontakte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdKontakte.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdKontakte.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdKontakte.Location = new System.Drawing.Point(15, 63);
            this.grdKontakte.MultiSelect = false;
            this.grdKontakte.Name = "grdKontakte";
            this.grdKontakte.ReadOnly = true;
            this.grdKontakte.RowHeadersVisible = false;
            this.grdKontakte.RowTemplate.Height = 55;
            this.grdKontakte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdKontakte.ShowEditingIcon = false;
            this.grdKontakte.ShowRowErrors = false;
            this.grdKontakte.Size = new System.Drawing.Size(574, 151);
            this.grdKontakte.TabIndex = 33;
            this.grdKontakte.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdKontakte_MouseDoubleClick);
            this.grdKontakte.SelectionChanged += new System.EventHandler(this.grdKontakte_SelectionChanged);
            // 
            // tbTextSearch
            // 
            this.tbTextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTextSearch.Location = new System.Drawing.Point(431, 37);
            this.tbTextSearch.Name = "tbTextSearch";
            this.tbTextSearch.Size = new System.Drawing.Size(158, 20);
            this.tbTextSearch.TabIndex = 36;
            this.tbTextSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Volltextsuche";
            // 
            // frmKontakte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(606, 509);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTextSearch);
            this.Controls.Add(this.afToolStrip1);
            this.Controls.Add(this.tbKontaktADR);
            this.Controls.Add(this.grdKontakte);
            this.Controls.Add(this.lKontaktname);
            this.Controls.Add(this.gbKontaktdaten);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btnAbbruch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmKontakte";
            this.Text = "Kontakteingabe";
            this.gbKontaktdaten.ResumeLayout(false);
            this.gbKontaktdaten.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKontakte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbKontaktdaten;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.Button btnAbbruch;
        private System.Windows.Forms.TextBox tbAP;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Label lAbteilung;
        private System.Windows.Forms.Label lAnsprechpartner;
        private System.Windows.Forms.Label lTelefon;
        private System.Windows.Forms.TextBox tbAbt;
        private System.Windows.Forms.TextBox tbTel;
        private System.Windows.Forms.Label lFax;
        private System.Windows.Forms.Label lMail;
        private System.Windows.Forms.TextBox tbFax;
        private System.Windows.Forms.TextBox tbMail;
        private System.Windows.Forms.Label lKontaktname;
        private Sped4.Controls.AFGrid grdKontakte;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lSSuchbegriff;
        private System.Windows.Forms.TextBox tbKontaktADR;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbtnExcelExport;
        private System.Windows.Forms.TextBox tbTextSearch;
        private System.Windows.Forms.Label label1;
    }
}