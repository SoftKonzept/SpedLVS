namespace Sped4
{
    partial class frmAuftragFast
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.multiColumnComboBox1 = new Sped4.MultiColumnComboBox();
            this.multiColumnComboBox2 = new Sped4.MultiColumnComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.multiColumnComboBox3 = new Sped4.MultiColumnComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAuftraggeber = new System.Windows.Forms.TextBox();
            this.tbVersender = new System.Windows.Forms.TextBox();
            this.tbEmpfaenger = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpT_date = new System.Windows.Forms.DateTimePicker();
            this.afGrid1 = new Sped4.Controls.AFGrid();
            this.Artikel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gut = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Dicke = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Breite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Laenge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hoehe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Menge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gewicht = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbTermin = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbANr = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbNotiz = new System.Windows.Forms.TextBox();
            this.tbLadeNr = new System.Windows.Forms.TextBox();
            this.tbT_KW = new System.Windows.Forms.TextBox();
            this.lAuftragsdatum = new System.Windows.Forms.Label();
            this.cbUnvollstaendig = new System.Windows.Forms.CheckBox();
            this.cbVollstaendig = new System.Windows.Forms.CheckBox();
            this.tbAuftragsdatum = new System.Windows.Forms.TextBox();
            this.gbArtVorlagen = new System.Windows.Forms.GroupBox();
            this.grdArtVorlage = new Sped4.Controls.AFGrid();
            this.gbPos = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.afGrid1)).BeginInit();
            this.gbArtVorlagen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdArtVorlage)).BeginInit();
            this.gbPos.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Auftraggeber";
            // 
            // multiColumnComboBox1
            // 
            this.multiColumnComboBox1.AutoComplete = false;
            this.multiColumnComboBox1.AutoDropdown = false;
            this.multiColumnComboBox1.BackColorEven = System.Drawing.Color.White;
            this.multiColumnComboBox1.BackColorOdd = System.Drawing.Color.White;
            this.multiColumnComboBox1.ColumnNames = "";
            this.multiColumnComboBox1.ColumnWidthDefault = 75;
            this.multiColumnComboBox1.ColumnWidths = "";
            this.multiColumnComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.multiColumnComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiColumnComboBox1.FormattingEnabled = true;
            this.multiColumnComboBox1.LinkedColumnIndex = 0;
            this.multiColumnComboBox1.LinkedTextBox = null;
            this.multiColumnComboBox1.Location = new System.Drawing.Point(86, 39);
            this.multiColumnComboBox1.Name = "multiColumnComboBox1";
            this.multiColumnComboBox1.Size = new System.Drawing.Size(143, 21);
            this.multiColumnComboBox1.TabIndex = 1;
            this.multiColumnComboBox1.OpenSearchForm += new System.EventHandler(this.multiColumnComboBox1_OpenSearchForm);
            this.multiColumnComboBox1.SelectedIndexChanged += new System.EventHandler(this.multiColumnComboBox1_SelectedIndexChanged);
            // 
            // multiColumnComboBox2
            // 
            this.multiColumnComboBox2.AutoComplete = false;
            this.multiColumnComboBox2.AutoDropdown = false;
            this.multiColumnComboBox2.BackColorEven = System.Drawing.Color.White;
            this.multiColumnComboBox2.BackColorOdd = System.Drawing.Color.White;
            this.multiColumnComboBox2.ColumnNames = "";
            this.multiColumnComboBox2.ColumnWidthDefault = 75;
            this.multiColumnComboBox2.ColumnWidths = "";
            this.multiColumnComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.multiColumnComboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiColumnComboBox2.FormattingEnabled = true;
            this.multiColumnComboBox2.LinkedColumnIndex = 0;
            this.multiColumnComboBox2.LinkedTextBox = null;
            this.multiColumnComboBox2.Location = new System.Drawing.Point(86, 66);
            this.multiColumnComboBox2.Name = "multiColumnComboBox2";
            this.multiColumnComboBox2.Size = new System.Drawing.Size(143, 21);
            this.multiColumnComboBox2.TabIndex = 2;
            this.multiColumnComboBox2.OpenSearchForm += new System.EventHandler(this.multiColumnComboBox2_OpenSearchForm);
            this.multiColumnComboBox2.SelectedIndexChanged += new System.EventHandler(this.multiColumnComboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Versender";
            // 
            // multiColumnComboBox3
            // 
            this.multiColumnComboBox3.AutoComplete = false;
            this.multiColumnComboBox3.AutoDropdown = false;
            this.multiColumnComboBox3.BackColorEven = System.Drawing.Color.White;
            this.multiColumnComboBox3.BackColorOdd = System.Drawing.Color.White;
            this.multiColumnComboBox3.ColumnNames = "";
            this.multiColumnComboBox3.ColumnWidthDefault = 75;
            this.multiColumnComboBox3.ColumnWidths = "";
            this.multiColumnComboBox3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.multiColumnComboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiColumnComboBox3.FormattingEnabled = true;
            this.multiColumnComboBox3.LinkedColumnIndex = 0;
            this.multiColumnComboBox3.LinkedTextBox = null;
            this.multiColumnComboBox3.Location = new System.Drawing.Point(86, 93);
            this.multiColumnComboBox3.Name = "multiColumnComboBox3";
            this.multiColumnComboBox3.Size = new System.Drawing.Size(143, 21);
            this.multiColumnComboBox3.TabIndex = 3;
            this.multiColumnComboBox3.OpenSearchForm += new System.EventHandler(this.multiColumnComboBox3_OpenSearchForm);
            this.multiColumnComboBox3.SelectedIndexChanged += new System.EventHandler(this.multiColumnComboBox3_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Empfänger";
            // 
            // tbAuftraggeber
            // 
            this.tbAuftraggeber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuftraggeber.Enabled = false;
            this.tbAuftraggeber.Location = new System.Drawing.Point(234, 40);
            this.tbAuftraggeber.Name = "tbAuftraggeber";
            this.tbAuftraggeber.Size = new System.Drawing.Size(387, 20);
            this.tbAuftraggeber.TabIndex = 7;
            // 
            // tbVersender
            // 
            this.tbVersender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVersender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVersender.Enabled = false;
            this.tbVersender.Location = new System.Drawing.Point(234, 67);
            this.tbVersender.Name = "tbVersender";
            this.tbVersender.Size = new System.Drawing.Size(387, 20);
            this.tbVersender.TabIndex = 8;
            // 
            // tbEmpfaenger
            // 
            this.tbEmpfaenger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEmpfaenger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEmpfaenger.Enabled = false;
            this.tbEmpfaenger.Location = new System.Drawing.Point(234, 94);
            this.tbEmpfaenger.Name = "tbEmpfaenger";
            this.tbEmpfaenger.Size = new System.Drawing.Size(387, 20);
            this.tbEmpfaenger.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label4.Location = new System.Drawing.Point(12, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Liefertermin";
            // 
            // dtpT_date
            // 
            this.dtpT_date.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpT_date.Location = new System.Drawing.Point(86, 158);
            this.dtpT_date.Name = "dtpT_date";
            this.dtpT_date.Size = new System.Drawing.Size(225, 20);
            this.dtpT_date.TabIndex = 4;
            // 
            // afGrid1
            // 
            this.afGrid1.AllowDrop = true;
            this.afGrid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.afGrid1.BackgroundColor = System.Drawing.Color.White;
            this.afGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.afGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Artikel,
            this.Gut,
            this.Dicke,
            this.Breite,
            this.Laenge,
            this.Hoehe,
            this.Menge,
            this.ME,
            this.Gewicht});
            this.afGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.afGrid1.Location = new System.Drawing.Point(3, 16);
            this.afGrid1.Name = "afGrid1";
            this.afGrid1.Size = new System.Drawing.Size(601, 180);
            this.afGrid1.TabIndex = 9;
            //this.afGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.afGrid1_MouseDown);
            this.afGrid1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.afGrid1_RowsAdded);
            this.afGrid1.DragEnter += new System.Windows.Forms.DragEventHandler(this.afGrid1_DragEnter);
            this.afGrid1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.afGrid1_EditingControlShowing);
            this.afGrid1.DragDrop += new System.Windows.Forms.DragEventHandler(this.afGrid1_DragDrop);
            // 
            // Artikel
            // 
            dataGridViewCellStyle1.NullValue = "0";
            this.Artikel.DefaultCellStyle = dataGridViewCellStyle1;
            this.Artikel.FillWeight = 104.6683F;
            this.Artikel.HeaderText = "Artikel";
            this.Artikel.Name = "Artikel";
            // 
            // Gut
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "-";
            this.Gut.DefaultCellStyle = dataGridViewCellStyle2;
            this.Gut.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Gut.FillWeight = 185.9256F;
            this.Gut.HeaderText = "Gut";
            this.Gut.Name = "Gut";
            this.Gut.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Gut.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Dicke
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0,00";
            this.Dicke.DefaultCellStyle = dataGridViewCellStyle3;
            this.Dicke.FillWeight = 75.37164F;
            this.Dicke.HeaderText = "Dicke";
            this.Dicke.Name = "Dicke";
            // 
            // Breite
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0,00";
            this.Breite.DefaultCellStyle = dataGridViewCellStyle4;
            this.Breite.FillWeight = 65.89586F;
            this.Breite.HeaderText = "Breite";
            this.Breite.Name = "Breite";
            // 
            // Laenge
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0,00";
            this.Laenge.DefaultCellStyle = dataGridViewCellStyle5;
            this.Laenge.FillWeight = 76.50523F;
            this.Laenge.HeaderText = "Länge";
            this.Laenge.Name = "Laenge";
            // 
            // Hoehe
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0,00";
            this.Hoehe.DefaultCellStyle = dataGridViewCellStyle6;
            this.Hoehe.FillWeight = 73.72441F;
            this.Hoehe.HeaderText = "Höhe";
            this.Hoehe.Name = "Hoehe";
            // 
            // Menge
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = "0";
            this.Menge.DefaultCellStyle = dataGridViewCellStyle7;
            this.Menge.FillWeight = 132.2058F;
            this.Menge.HeaderText = "Menge";
            this.Menge.Name = "Menge";
            // 
            // ME
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = "-";
            this.ME.DefaultCellStyle = dataGridViewCellStyle8;
            this.ME.FillWeight = 87.45399F;
            this.ME.HeaderText = "ME";
            this.ME.Name = "ME";
            // 
            // Gewicht
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = "0,00";
            this.Gewicht.DefaultCellStyle = dataGridViewCellStyle9;
            this.Gewicht.FillWeight = 113.9328F;
            this.Gewicht.HeaderText = "Gewicht";
            this.Gewicht.Name = "Gewicht";
            // 
            // cbTermin
            // 
            this.cbTermin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTermin.AutoSize = true;
            this.cbTermin.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cbTermin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTermin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.cbTermin.Location = new System.Drawing.Point(86, 129);
            this.cbTermin.Name = "cbTermin";
            this.cbTermin.Size = new System.Drawing.Size(115, 17);
            this.cbTermin.TabIndex = 6;
            this.cbTermin.Text = "Termine unbekannt";
            this.cbTermin.UseVisualStyleBackColor = true;
            this.cbTermin.CheckedChanged += new System.EventHandler(this.cbTermin_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Auftragsnr.";
            // 
            // tbANr
            // 
            this.tbANr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbANr.Enabled = false;
            this.tbANr.Location = new System.Drawing.Point(86, 6);
            this.tbANr.Name = "tbANr";
            this.tbANr.Size = new System.Drawing.Size(143, 20);
            this.tbANr.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Image = global::Sped4.Properties.Resources.delete_16;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(305, 643);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 25);
            this.button2.TabIndex = 13;
            this.button2.Text = "     &Abbruch";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Image = global::Sped4.Properties.Resources.check;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(215, 643);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 25);
            this.button1.TabIndex = 12;
            this.button1.Text = "     &Speichern";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label7.Location = new System.Drawing.Point(12, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Ladenummer";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label8.Location = new System.Drawing.Point(322, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Notiz";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label9.Location = new System.Drawing.Point(12, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Termin KW:";
            // 
            // tbNotiz
            // 
            this.tbNotiz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNotiz.Location = new System.Drawing.Point(368, 131);
            this.tbNotiz.Multiline = true;
            this.tbNotiz.Name = "tbNotiz";
            this.tbNotiz.Size = new System.Drawing.Size(253, 101);
            this.tbNotiz.TabIndex = 8;
            // 
            // tbLadeNr
            // 
            this.tbLadeNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLadeNr.Location = new System.Drawing.Point(86, 212);
            this.tbLadeNr.Name = "tbLadeNr";
            this.tbLadeNr.Size = new System.Drawing.Size(225, 20);
            this.tbLadeNr.TabIndex = 7;
            // 
            // tbT_KW
            // 
            this.tbT_KW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbT_KW.Location = new System.Drawing.Point(86, 183);
            this.tbT_KW.Name = "tbT_KW";
            this.tbT_KW.Size = new System.Drawing.Size(225, 20);
            this.tbT_KW.TabIndex = 5;
            // 
            // lAuftragsdatum
            // 
            this.lAuftragsdatum.AutoSize = true;
            this.lAuftragsdatum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.lAuftragsdatum.Location = new System.Drawing.Point(365, 9);
            this.lAuftragsdatum.Name = "lAuftragsdatum";
            this.lAuftragsdatum.Size = new System.Drawing.Size(78, 13);
            this.lAuftragsdatum.TabIndex = 29;
            this.lAuftragsdatum.Text = "Auftragsdatum:";
            // 
            // cbUnvollstaendig
            // 
            this.cbUnvollstaendig.AutoSize = true;
            this.cbUnvollstaendig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbUnvollstaendig.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.cbUnvollstaendig.Location = new System.Drawing.Point(308, 620);
            this.cbUnvollstaendig.Name = "cbUnvollstaendig";
            this.cbUnvollstaendig.Size = new System.Drawing.Size(154, 17);
            this.cbUnvollstaendig.TabIndex = 11;
            this.cbUnvollstaendig.Text = "Auftragsdaten unvollständig";
            this.cbUnvollstaendig.UseVisualStyleBackColor = true;
            this.cbUnvollstaendig.CheckedChanged += new System.EventHandler(this.cbUnvollstaendig_CheckedChanged);
            // 
            // cbVollstaendig
            // 
            this.cbVollstaendig.AutoSize = true;
            this.cbVollstaendig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVollstaendig.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.cbVollstaendig.Location = new System.Drawing.Point(157, 620);
            this.cbVollstaendig.Name = "cbVollstaendig";
            this.cbVollstaendig.Size = new System.Drawing.Size(142, 17);
            this.cbVollstaendig.TabIndex = 10;
            this.cbVollstaendig.Text = "Auftragsdaten vollständig";
            this.cbVollstaendig.UseVisualStyleBackColor = true;
            this.cbVollstaendig.CheckedChanged += new System.EventHandler(this.cbVollstaendig_CheckedChanged);
            // 
            // tbAuftragsdatum
            // 
            this.tbAuftragsdatum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuftragsdatum.Enabled = false;
            this.tbAuftragsdatum.Location = new System.Drawing.Point(472, 6);
            this.tbAuftragsdatum.Name = "tbAuftragsdatum";
            this.tbAuftragsdatum.Size = new System.Drawing.Size(149, 20);
            this.tbAuftragsdatum.TabIndex = 33;
            // 
            // gbArtVorlagen
            // 
            this.gbArtVorlagen.Controls.Add(this.grdArtVorlage);
            this.gbArtVorlagen.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.gbArtVorlagen.Location = new System.Drawing.Point(15, 249);
            this.gbArtVorlagen.Name = "gbArtVorlagen";
            this.gbArtVorlagen.Size = new System.Drawing.Size(606, 143);
            this.gbArtVorlagen.TabIndex = 35;
            this.gbArtVorlagen.TabStop = false;
            this.gbArtVorlagen.Text = "Artikelvorlagen zur Auswahl";
            // 
            // grdArtVorlage
            // 
            this.grdArtVorlage.AllowDrop = true;
            this.grdArtVorlage.AllowUserToAddRows = false;
            this.grdArtVorlage.AllowUserToDeleteRows = false;
            this.grdArtVorlage.AllowUserToResizeRows = false;
            this.grdArtVorlage.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grdArtVorlage.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.grdArtVorlage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdArtVorlage.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grdArtVorlage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdArtVorlage.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdArtVorlage.DefaultCellStyle = dataGridViewCellStyle10;
            this.grdArtVorlage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdArtVorlage.Location = new System.Drawing.Point(3, 16);
            this.grdArtVorlage.MultiSelect = false;
            this.grdArtVorlage.Name = "grdArtVorlage";
            this.grdArtVorlage.ReadOnly = true;
            this.grdArtVorlage.RowHeadersVisible = false;
            this.grdArtVorlage.RowTemplate.Height = 55;
            this.grdArtVorlage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdArtVorlage.ShowEditingIcon = false;
            this.grdArtVorlage.ShowRowErrors = false;
            this.grdArtVorlage.Size = new System.Drawing.Size(600, 124);
            this.grdArtVorlage.TabIndex = 9;
            this.grdArtVorlage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdArtVorlage_MouseDown);
            // 
            // gbPos
            // 
            this.gbPos.Controls.Add(this.afGrid1);
            this.gbPos.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.gbPos.Location = new System.Drawing.Point(15, 415);
            this.gbPos.Name = "gbPos";
            this.gbPos.Size = new System.Drawing.Size(607, 199);
            this.gbPos.TabIndex = 36;
            this.gbPos.TabStop = false;
            this.gbPos.Text = "Eingabe der Artikel";
            // 
            // frmAuftragFast
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(634, 691);
            this.Controls.Add(this.gbPos);
            this.Controls.Add(this.gbArtVorlagen);
            this.Controls.Add(this.cbTermin);
            this.Controls.Add(this.tbAuftragsdatum);
            this.Controls.Add(this.cbVollstaendig);
            this.Controls.Add(this.cbUnvollstaendig);
            this.Controls.Add(this.lAuftragsdatum);
            this.Controls.Add(this.tbT_KW);
            this.Controls.Add(this.tbLadeNr);
            this.Controls.Add(this.tbNotiz);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbANr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpT_date);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbEmpfaenger);
            this.Controls.Add(this.tbVersender);
            this.Controls.Add(this.tbAuftraggeber);
            this.Controls.Add(this.multiColumnComboBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.multiColumnComboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.multiColumnComboBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAuftragFast";
            this.Text = "Auftrag anlegen";
            ((System.ComponentModel.ISupportInitialize)(this.afGrid1)).EndInit();
            this.gbArtVorlagen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdArtVorlage)).EndInit();
            this.gbPos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MultiColumnComboBox multiColumnComboBox1;
        private MultiColumnComboBox multiColumnComboBox2;
        private System.Windows.Forms.Label label2;
        private MultiColumnComboBox multiColumnComboBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAuftraggeber;
        private System.Windows.Forms.TextBox tbVersender;
        private System.Windows.Forms.TextBox tbEmpfaenger;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpT_date;
        private Sped4.Controls.AFGrid afGrid1;
        private System.Windows.Forms.CheckBox cbTermin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbANr;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbNotiz;
        private System.Windows.Forms.TextBox tbLadeNr;
        private System.Windows.Forms.TextBox tbT_KW;
        private System.Windows.Forms.Label lAuftragsdatum;
        private System.Windows.Forms.CheckBox cbUnvollstaendig;
        private System.Windows.Forms.CheckBox cbVollstaendig;
        private System.Windows.Forms.TextBox tbAuftragsdatum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Artikel;
        private System.Windows.Forms.DataGridViewComboBoxColumn Gut;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dicke;
        private System.Windows.Forms.DataGridViewTextBoxColumn Breite;
        private System.Windows.Forms.DataGridViewTextBoxColumn Laenge;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hoehe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Menge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ME;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gewicht;
        private System.Windows.Forms.GroupBox gbArtVorlagen;
        private Sped4.Controls.AFGrid grdArtVorlage;
        private System.Windows.Forms.GroupBox gbPos;

    }
}