namespace Sped4
{
    partial class frmDistance
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.panelDGV = new System.Windows.Forms.Panel();
            this.dgv = new Sped4.Controls.AFGrid();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.cbSObj = new System.Windows.Forms.ComboBox();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.cbNachLand = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbVonLand = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbKM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbVonOrt = new System.Windows.Forms.TextBox();
            this.tbNachPLZ = new System.Windows.Forms.TextBox();
            this.tbNachOrt = new System.Windows.Forms.TextBox();
            this.tbVonPLZ = new System.Windows.Forms.TextBox();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRoute = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.afToolStrip1.SuspendLayout();
            this.panelDGV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.afMinMaxPanel1.SuspendLayout();
            this.panelDetails.SuspendLayout();
            this.afToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton5});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(423, 25);
            this.afToolStrip1.TabIndex = 9;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Sped4.Properties.Resources.check;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "ausgewählte Entfernung übernehmen!";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click_1);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::Sped4.Properties.Resources.delete;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "DistanceClose";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // panelDGV
            // 
            this.panelDGV.Controls.Add(this.dgv);
            this.panelDGV.Controls.Add(this.afMinMaxPanel1);
            this.panelDGV.Controls.Add(this.afToolStrip1);
            this.panelDGV.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDGV.Location = new System.Drawing.Point(0, 0);
            this.panelDGV.Name = "panelDGV";
            this.panelDGV.Size = new System.Drawing.Size(423, 520);
            this.panelDGV.TabIndex = 10;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgv.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 135);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 55;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowEditingIcon = false;
            this.dgv.ShowRowErrors = false;
            this.dgv.Size = new System.Drawing.Size(423, 385);
            this.dgv.TabIndex = 134;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.BackColor = System.Drawing.Color.White;
            this.afMinMaxPanel1.Controls.Add(this.tbSearch);
            this.afMinMaxPanel1.Controls.Add(this.cbSObj);
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 25);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Optionen";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(423, 110);
            this.afMinMaxPanel1.TabIndex = 133;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearch.Location = new System.Drawing.Point(45, 59);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(338, 20);
            this.tbSearch.TabIndex = 10;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // cbSObj
            // 
            this.cbSObj.AllowDrop = true;
            this.cbSObj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSObj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSObj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSObj.FormattingEnabled = true;
            this.cbSObj.Location = new System.Drawing.Point(45, 32);
            this.cbSObj.Name = "cbSObj";
            this.cbSObj.Size = new System.Drawing.Size(338, 21);
            this.cbSObj.TabIndex = 9;
            this.cbSObj.SelectedIndexChanged += new System.EventHandler(this.cbSObj_SelectedIndexChanged);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.cbNachLand);
            this.panelDetails.Controls.Add(this.label7);
            this.panelDetails.Controls.Add(this.cbVonLand);
            this.panelDetails.Controls.Add(this.label6);
            this.panelDetails.Controls.Add(this.label5);
            this.panelDetails.Controls.Add(this.tbKM);
            this.panelDetails.Controls.Add(this.label4);
            this.panelDetails.Controls.Add(this.label3);
            this.panelDetails.Controls.Add(this.label2);
            this.panelDetails.Controls.Add(this.label1);
            this.panelDetails.Controls.Add(this.tbVonOrt);
            this.panelDetails.Controls.Add(this.tbNachPLZ);
            this.panelDetails.Controls.Add(this.tbNachOrt);
            this.panelDetails.Controls.Add(this.tbVonPLZ);
            this.panelDetails.Controls.Add(this.afToolStrip2);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Location = new System.Drawing.Point(423, 0);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(409, 520);
            this.panelDetails.TabIndex = 11;
            // 
            // cbNachLand
            // 
            this.cbNachLand.AllowDrop = true;
            this.cbNachLand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNachLand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNachLand.FormattingEnabled = true;
            this.cbNachLand.Location = new System.Drawing.Point(93, 195);
            this.cbNachLand.Name = "cbNachLand";
            this.cbNachLand.Size = new System.Drawing.Size(175, 21);
            this.cbNachLand.TabIndex = 116;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(23, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 115;
            this.label7.Text = "nach Land";
            // 
            // cbVonLand
            // 
            this.cbVonLand.AllowDrop = true;
            this.cbVonLand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVonLand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVonLand.FormattingEnabled = true;
            this.cbVonLand.Location = new System.Drawing.Point(93, 89);
            this.cbVonLand.Name = "cbVonLand";
            this.cbVonLand.Size = new System.Drawing.Size(175, 21);
            this.cbVonLand.TabIndex = 114;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(23, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 113;
            this.label6.Text = "von Land:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(23, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 112;
            this.label5.Text = "km:";
            // 
            // tbKM
            // 
            this.tbKM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKM.Location = new System.Drawing.Point(91, 250);
            this.tbKM.Name = "tbKM";
            this.tbKM.Size = new System.Drawing.Size(177, 20);
            this.tbKM.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(23, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 110;
            this.label4.Text = "von PLZ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(23, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 109;
            this.label3.Text = "von Ort:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(23, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 108;
            this.label2.Text = "nach PLZ:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(23, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 107;
            this.label1.Text = "nach Ort:";
            // 
            // tbVonOrt
            // 
            this.tbVonOrt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVonOrt.Location = new System.Drawing.Point(91, 64);
            this.tbVonOrt.Name = "tbVonOrt";
            this.tbVonOrt.Size = new System.Drawing.Size(177, 20);
            this.tbVonOrt.TabIndex = 2;
            // 
            // tbNachPLZ
            // 
            this.tbNachPLZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNachPLZ.Location = new System.Drawing.Point(91, 143);
            this.tbNachPLZ.Name = "tbNachPLZ";
            this.tbNachPLZ.Size = new System.Drawing.Size(177, 20);
            this.tbNachPLZ.TabIndex = 3;
            // 
            // tbNachOrt
            // 
            this.tbNachOrt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNachOrt.Location = new System.Drawing.Point(91, 169);
            this.tbNachOrt.Name = "tbNachOrt";
            this.tbNachOrt.Size = new System.Drawing.Size(177, 20);
            this.tbNachOrt.TabIndex = 4;
            // 
            // tbVonPLZ
            // 
            this.tbVonPLZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVonPLZ.Location = new System.Drawing.Point(91, 40);
            this.tbVonPLZ.Name = "tbVonPLZ";
            this.tbVonPLZ.Size = new System.Drawing.Size(177, 20);
            this.tbVonPLZ.TabIndex = 1;
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNew,
            this.tsbtnSave,
            this.tsbtnDelete,
            this.tsbtnRoute});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(409, 25);
            this.afToolStrip2.TabIndex = 10;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnNew
            // 
            this.tsbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNew.Image = global::Sped4.Properties.Resources.add;
            this.tsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNew.Name = "tsbtnNew";
            this.tsbtnNew.Size = new System.Drawing.Size(23, 22);
            this.tsbtnNew.Text = "Neue Entfernung hinzufügen";
            this.tsbtnNew.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSave.Text = "Entfernung speichern";
            this.tsbtnSave.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDelete.Text = "Entfernung löschen";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // tsbtnRoute
            // 
            this.tsbtnRoute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRoute.Image = global::Sped4.Properties.Resources.chart_dot;
            this.tsbtnRoute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRoute.Name = "tsbtnRoute";
            this.tsbtnRoute.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRoute.Text = "km - Entfernung ermitteln.";
            this.tsbtnRoute.Click += new System.EventHandler(this.tsbtnRoute_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            // 
            // frmDistance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 520);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.panelDGV);
            this.Name = "frmDistance";
            this.Text = "Entfernungscenter - Hinzufügen / Löschen von Entfernungen";
            this.Load += new System.EventHandler(this.frmDistance_Load);
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.panelDGV.ResumeLayout(false);
            this.panelDGV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.afMinMaxPanel1.ResumeLayout(false);
            this.afMinMaxPanel1.PerformLayout();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.Panel panelDGV;
        private Sped4.Controls.AFMinMaxPanel afMinMaxPanel1;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.ComboBox cbSObj;
        private System.Windows.Forms.TextBox tbSearch;
        private Sped4.Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.TextBox tbVonOrt;
        private System.Windows.Forms.TextBox tbNachPLZ;
        private System.Windows.Forms.TextBox tbNachOrt;
        private System.Windows.Forms.TextBox tbVonPLZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbtnNew;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbKM;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ComboBox cbNachLand;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbVonLand;
        private System.Windows.Forms.Label label6;
        private Controls.AFGrid dgv;
        private System.Windows.Forms.ToolStripButton tsbtnRoute;
    }
}