namespace Sped4
{
    partial class frmSchaden
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchaden));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.afMinMaxTarif = new Sped4.Controls.AFMinMaxPanel();
            this.cbSchadenAktiv = new System.Windows.Forms.CheckBox();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnSchadenNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSchadenSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSchadenDelete = new System.Windows.Forms.ToolStripButton();
            this.tbSchadensbezeichnung = new System.Windows.Forms.TextBox();
            this.tbBeschreibung = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.grd = new Sped4.Controls.AFGrid();
            this.afToolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.afMinMaxTarif.SuspendLayout();
            this.afToolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            this.SuspendLayout();
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(772, 25);
            this.afToolStrip1.TabIndex = 42;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnClose.Image")));
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Fenster schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.afMinMaxTarif);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grd);
            this.splitContainer1.Size = new System.Drawing.Size(772, 376);
            this.splitContainer1.SplitterDistance = 362;
            this.splitContainer1.TabIndex = 43;
            // 
            // afMinMaxTarif
            // 
            this.afMinMaxTarif.BackColor = System.Drawing.Color.White;
            this.afMinMaxTarif.Controls.Add(this.cbSchadenAktiv);
            this.afMinMaxTarif.Controls.Add(this.afToolStrip2);
            this.afMinMaxTarif.Controls.Add(this.tbSchadensbezeichnung);
            this.afMinMaxTarif.Controls.Add(this.tbBeschreibung);
            this.afMinMaxTarif.Controls.Add(this.label1);
            this.afMinMaxTarif.Controls.Add(this.label9);
            this.afMinMaxTarif.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxTarif.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxTarif.Location = new System.Drawing.Point(0, 0);
            this.afMinMaxTarif.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxTarif.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxTarif.myImage = ((System.Drawing.Image)(resources.GetObject("afMinMaxTarif.myImage")));
            this.afMinMaxTarif.myText = "Schaden anlegen";
            this.afMinMaxTarif.Name = "afMinMaxTarif";
            this.afMinMaxTarif.Size = new System.Drawing.Size(362, 225);
            this.afMinMaxTarif.TabIndex = 3;
            this.afMinMaxTarif.Text = "afMinMaxPanel1";
            // 
            // cbSchadenAktiv
            // 
            this.cbSchadenAktiv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSchadenAktiv.AutoSize = true;
            this.cbSchadenAktiv.Checked = true;
            this.cbSchadenAktiv.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSchadenAktiv.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cbSchadenAktiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSchadenAktiv.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbSchadenAktiv.Location = new System.Drawing.Point(297, 47);
            this.cbSchadenAktiv.Name = "cbSchadenAktiv";
            this.cbSchadenAktiv.Size = new System.Drawing.Size(46, 17);
            this.cbSchadenAktiv.TabIndex = 24;
            this.cbSchadenAktiv.Text = "aktiv";
            this.cbSchadenAktiv.UseVisualStyleBackColor = true;
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSchadenNew,
            this.tsbtnSchadenSave,
            this.tsbtnSchadenDelete});
            this.afToolStrip2.Location = new System.Drawing.Point(3, 28);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(81, 25);
            this.afToolStrip2.TabIndex = 8;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnSchadenNew
            // 
            this.tsbtnSchadenNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSchadenNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSchadenNew.Image")));
            this.tsbtnSchadenNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSchadenNew.Name = "tsbtnSchadenNew";
            this.tsbtnSchadenNew.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSchadenNew.Text = "Neuen Schaden anlegen";
            this.tsbtnSchadenNew.Click += new System.EventHandler(this.tsbtnSchadenNew_Click);
            // 
            // tsbtnSchadenSave
            // 
            this.tsbtnSchadenSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSchadenSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSchadenSave.Image")));
            this.tsbtnSchadenSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSchadenSave.Name = "tsbtnSchadenSave";
            this.tsbtnSchadenSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSchadenSave.Text = "Schadensdaten speichern";
            this.tsbtnSchadenSave.Click += new System.EventHandler(this.tsbtnSchadenSave_Click);
            // 
            // tsbtnSchadenDelete
            // 
            this.tsbtnSchadenDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSchadenDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSchadenDelete.Image")));
            this.tsbtnSchadenDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSchadenDelete.Name = "tsbtnSchadenDelete";
            this.tsbtnSchadenDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSchadenDelete.Text = "Schaden löschen";
            // 
            // tbSchadensbezeichnung
            // 
            this.tbSchadensbezeichnung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSchadensbezeichnung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSchadensbezeichnung.Location = new System.Drawing.Point(82, 70);
            this.tbSchadensbezeichnung.Name = "tbSchadensbezeichnung";
            this.tbSchadensbezeichnung.Size = new System.Drawing.Size(262, 20);
            this.tbSchadensbezeichnung.TabIndex = 10;
            // 
            // tbBeschreibung
            // 
            this.tbBeschreibung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBeschreibung.Location = new System.Drawing.Point(82, 99);
            this.tbBeschreibung.Multiline = true;
            this.tbBeschreibung.Name = "tbBeschreibung";
            this.tbBeschreibung.Size = new System.Drawing.Size(262, 114);
            this.tbBeschreibung.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(6, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schaden:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Location = new System.Drawing.Point(6, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Beschreibung:";
            // 
            // grd
            // 
            this.grd.AllowUserToAddRows = false;
            this.grd.AllowUserToDeleteRows = false;
            this.grd.AllowUserToResizeRows = false;
            this.grd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grd.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.grd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grd.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd.DefaultCellStyle = dataGridViewCellStyle1;
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.Location = new System.Drawing.Point(0, 0);
            this.grd.MultiSelect = false;
            this.grd.Name = "grd";
            this.grd.ReadOnly = true;
            this.grd.RowHeadersVisible = false;
            this.grd.RowTemplate.Height = 55;
            this.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grd.ShowEditingIcon = false;
            this.grd.ShowRowErrors = false;
            this.grd.Size = new System.Drawing.Size(406, 376);
            this.grd.TabIndex = 5;
            this.grd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_CellClick);
            // 
            // frmSchaden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 401);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.afToolStrip1);
            this.Name = "frmSchaden";
            this.Text = "Schadensliste";
            this.Load += new System.EventHandler(this.frmSchaden_Load);
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.afMinMaxTarif.ResumeLayout(false);
            this.afMinMaxTarif.PerformLayout();
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.AFMinMaxPanel afMinMaxTarif;
        private System.Windows.Forms.CheckBox cbSchadenAktiv;
        private Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnSchadenNew;
        private System.Windows.Forms.ToolStripButton tsbtnSchadenSave;
        private System.Windows.Forms.ToolStripButton tsbtnSchadenDelete;
        private System.Windows.Forms.TextBox tbSchadensbezeichnung;
        private System.Windows.Forms.TextBox tbBeschreibung;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private Controls.AFGrid grd;
    }
}