namespace Sped4
{
    partial class ctrPrint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrPrint));
            this.panPrint = new System.Windows.Forms.Panel();
            this.gbDocDaten = new System.Windows.Forms.GroupBox();
            this.dtp_PrintDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearchV = new System.Windows.Forms.TextBox();
            this.tbAuflieger = new System.Windows.Forms.TextBox();
            this.tbSearchE = new System.Windows.Forms.TextBox();
            this.tbZM = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEmfaenger = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbVersender = new System.Windows.Forms.TextBox();
            this.tbDocName = new System.Windows.Forms.TextBox();
            this.tbFahrer = new System.Windows.Forms.TextBox();
            this.btnVersender = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEmpfaenger = new System.Windows.Forms.TextBox();
            this.dgv = new Sped4.Controls.AFGrid();
            this.gbDruckArt = new System.Windows.Forms.GroupBox();
            this.cbNotizPrint = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudPMA = new System.Windows.Forms.NumericUpDown();
            this.tbNeutrDocName = new System.Windows.Forms.TextBox();
            this.cbOwnDoc = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudPMLfs = new System.Windows.Forms.NumericUpDown();
            this.cbNeutrLfsAbholschein = new System.Windows.Forms.CheckBox();
            this.cbLfsAbholschein = new System.Windows.Forms.CheckBox();
            this.cbNeutrLfSchein = new System.Windows.Forms.CheckBox();
            this.cbAbholschein = new System.Windows.Forms.CheckBox();
            this.cbFremdLfs = new System.Windows.Forms.CheckBox();
            this.cbLfSchein = new System.Windows.Forms.CheckBox();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDirectPrint = new System.Windows.Forms.ToolStripButton();
            this.panPrint.SuspendLayout();
            this.gbDocDaten.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.gbDruckArt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPMLfs)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panPrint
            // 
            this.panPrint.AutoScroll = true;
            this.panPrint.BackColor = System.Drawing.Color.White;
            this.panPrint.Controls.Add(this.gbDocDaten);
            this.panPrint.Controls.Add(this.dgv);
            this.panPrint.Controls.Add(this.gbDruckArt);
            this.panPrint.Controls.Add(this.afToolStrip1);
            this.panPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPrint.Location = new System.Drawing.Point(0, 0);
            this.panPrint.Name = "panPrint";
            this.panPrint.Size = new System.Drawing.Size(505, 873);
            this.panPrint.TabIndex = 0;
            // 
            // gbDocDaten
            // 
            this.gbDocDaten.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDocDaten.Controls.Add(this.dtp_PrintDate);
            this.gbDocDaten.Controls.Add(this.label6);
            this.gbDocDaten.Controls.Add(this.label1);
            this.gbDocDaten.Controls.Add(this.tbSearchV);
            this.gbDocDaten.Controls.Add(this.tbAuflieger);
            this.gbDocDaten.Controls.Add(this.tbSearchE);
            this.gbDocDaten.Controls.Add(this.tbZM);
            this.gbDocDaten.Controls.Add(this.label3);
            this.gbDocDaten.Controls.Add(this.btnEmfaenger);
            this.gbDocDaten.Controls.Add(this.label4);
            this.gbDocDaten.Controls.Add(this.tbVersender);
            this.gbDocDaten.Controls.Add(this.tbDocName);
            this.gbDocDaten.Controls.Add(this.tbFahrer);
            this.gbDocDaten.Controls.Add(this.btnVersender);
            this.gbDocDaten.Controls.Add(this.label2);
            this.gbDocDaten.Controls.Add(this.tbEmpfaenger);
            this.gbDocDaten.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbDocDaten.Location = new System.Drawing.Point(11, 37);
            this.gbDocDaten.Name = "gbDocDaten";
            this.gbDocDaten.Size = new System.Drawing.Size(485, 221);
            this.gbDocDaten.TabIndex = 131;
            this.gbDocDaten.TabStop = false;
            this.gbDocDaten.Text = "Daten im gedruckten Dokument";
            // 
            // dtp_PrintDate
            // 
            this.dtp_PrintDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_PrintDate.Location = new System.Drawing.Point(111, 119);
            this.dtp_PrintDate.Name = "dtp_PrintDate";
            this.dtp_PrintDate.Size = new System.Drawing.Size(192, 20);
            this.dtp_PrintDate.TabIndex = 132;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(13, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 131;
            this.label6.Text = "Druckdatum:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(13, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Dokumentenname:";
            // 
            // tbSearchV
            // 
            this.tbSearchV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchV.Location = new System.Drawing.Point(118, 28);
            this.tbSearchV.Name = "tbSearchV";
            this.tbSearchV.Size = new System.Drawing.Size(104, 20);
            this.tbSearchV.TabIndex = 124;
            this.tbSearchV.TextChanged += new System.EventHandler(this.tbSearchV_TextChanged);
            this.tbSearchV.Validated += new System.EventHandler(this.tbSearchV_Validated);
            // 
            // tbAuflieger
            // 
            this.tbAuflieger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuflieger.Location = new System.Drawing.Point(111, 164);
            this.tbAuflieger.Name = "tbAuflieger";
            this.tbAuflieger.Size = new System.Drawing.Size(192, 20);
            this.tbAuflieger.TabIndex = 12;
            // 
            // tbSearchE
            // 
            this.tbSearchE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchE.Location = new System.Drawing.Point(118, 56);
            this.tbSearchE.Name = "tbSearchE";
            this.tbSearchE.Size = new System.Drawing.Size(104, 20);
            this.tbSearchE.TabIndex = 126;
            this.tbSearchE.TextChanged += new System.EventHandler(this.tbSearchE_TextChanged);
            this.tbSearchE.Validated += new System.EventHandler(this.tbSearchE_Validated);
            // 
            // tbZM
            // 
            this.tbZM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbZM.Location = new System.Drawing.Point(111, 141);
            this.tbZM.Name = "tbZM";
            this.tbZM.Size = new System.Drawing.Size(192, 20);
            this.tbZM.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(13, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Auflieger:";
            // 
            // btnEmfaenger
            // 
            this.btnEmfaenger.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnEmfaenger.Location = new System.Drawing.Point(16, 56);
            this.btnEmfaenger.Name = "btnEmfaenger";
            this.btnEmfaenger.Size = new System.Drawing.Size(96, 22);
            this.btnEmfaenger.TabIndex = 125;
            this.btnEmfaenger.Text = "Empfänger";
            this.btnEmfaenger.UseVisualStyleBackColor = true;
            this.btnEmfaenger.Click += new System.EventHandler(this.btnEmfaenger_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(13, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Zugmaschine:";
            // 
            // tbVersender
            // 
            this.tbVersender.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbVersender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVersender.Enabled = false;
            this.tbVersender.Location = new System.Drawing.Point(228, 28);
            this.tbVersender.Name = "tbVersender";
            this.tbVersender.ReadOnly = true;
            this.tbVersender.Size = new System.Drawing.Size(248, 20);
            this.tbVersender.TabIndex = 127;
            // 
            // tbDocName
            // 
            this.tbDocName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDocName.Location = new System.Drawing.Point(111, 91);
            this.tbDocName.Name = "tbDocName";
            this.tbDocName.Size = new System.Drawing.Size(192, 20);
            this.tbDocName.TabIndex = 10;
            // 
            // tbFahrer
            // 
            this.tbFahrer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFahrer.Location = new System.Drawing.Point(111, 189);
            this.tbFahrer.Name = "tbFahrer";
            this.tbFahrer.Size = new System.Drawing.Size(192, 20);
            this.tbFahrer.TabIndex = 13;
            // 
            // btnVersender
            // 
            this.btnVersender.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnVersender.Location = new System.Drawing.Point(16, 28);
            this.btnVersender.Name = "btnVersender";
            this.btnVersender.Size = new System.Drawing.Size(96, 22);
            this.btnVersender.TabIndex = 123;
            this.btnVersender.Text = "Versender";
            this.btnVersender.UseVisualStyleBackColor = true;
            this.btnVersender.Click += new System.EventHandler(this.btnVersender_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(13, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Faher:";
            // 
            // tbEmpfaenger
            // 
            this.tbEmpfaenger.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEmpfaenger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEmpfaenger.Enabled = false;
            this.tbEmpfaenger.Location = new System.Drawing.Point(228, 56);
            this.tbEmpfaenger.Name = "tbEmpfaenger";
            this.tbEmpfaenger.ReadOnly = true;
            this.tbEmpfaenger.Size = new System.Drawing.Size(248, 20);
            this.tbEmpfaenger.TabIndex = 128;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgv.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Location = new System.Drawing.Point(15, 503);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 55;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowEditingIcon = false;
            this.dgv.ShowRowErrors = false;
            this.dgv.Size = new System.Drawing.Size(481, 356);
            this.dgv.TabIndex = 23;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // gbDruckArt
            // 
            this.gbDruckArt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDruckArt.Controls.Add(this.cbNotizPrint);
            this.gbDruckArt.Controls.Add(this.label7);
            this.gbDruckArt.Controls.Add(this.nudPMA);
            this.gbDruckArt.Controls.Add(this.tbNeutrDocName);
            this.gbDruckArt.Controls.Add(this.cbOwnDoc);
            this.gbDruckArt.Controls.Add(this.label5);
            this.gbDruckArt.Controls.Add(this.nudPMLfs);
            this.gbDruckArt.Controls.Add(this.cbNeutrLfsAbholschein);
            this.gbDruckArt.Controls.Add(this.cbLfsAbholschein);
            this.gbDruckArt.Controls.Add(this.cbNeutrLfSchein);
            this.gbDruckArt.Controls.Add(this.cbAbholschein);
            this.gbDruckArt.Controls.Add(this.cbFremdLfs);
            this.gbDruckArt.Controls.Add(this.cbLfSchein);
            this.gbDruckArt.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbDruckArt.Location = new System.Drawing.Point(11, 264);
            this.gbDruckArt.Name = "gbDruckArt";
            this.gbDruckArt.Size = new System.Drawing.Size(485, 233);
            this.gbDruckArt.TabIndex = 9;
            this.gbDruckArt.TabStop = false;
            this.gbDruckArt.Text = "Dokumentenart";
            // 
            // cbNotizPrint
            // 
            this.cbNotizPrint.AutoSize = true;
            this.cbNotizPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNotizPrint.Location = new System.Drawing.Point(287, 34);
            this.cbNotizPrint.Name = "cbNotizPrint";
            this.cbNotizPrint.Size = new System.Drawing.Size(89, 17);
            this.cbNotizPrint.TabIndex = 23;
            this.cbNotizPrint.Text = "Notiz drucken";
            this.cbNotizPrint.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(344, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Druckanzahl Abholschein";
            // 
            // nudPMA
            // 
            this.nudPMA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPMA.Location = new System.Drawing.Point(287, 83);
            this.nudPMA.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPMA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPMA.Name = "nudPMA";
            this.nudPMA.Size = new System.Drawing.Size(41, 20);
            this.nudPMA.TabIndex = 21;
            this.nudPMA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tbNeutrDocName
            // 
            this.tbNeutrDocName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNeutrDocName.Location = new System.Drawing.Point(46, 195);
            this.tbNeutrDocName.Name = "tbNeutrDocName";
            this.tbNeutrDocName.Size = new System.Drawing.Size(159, 20);
            this.tbNeutrDocName.TabIndex = 8;
            this.tbNeutrDocName.Tag = "Bitte Dokumentenname angeben";
            this.tbNeutrDocName.TextChanged += new System.EventHandler(this.tbNeutrDocName_TextChanged);
            // 
            // cbOwnDoc
            // 
            this.cbOwnDoc.AutoSize = true;
            this.cbOwnDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbOwnDoc.Location = new System.Drawing.Point(25, 172);
            this.cbOwnDoc.Name = "cbOwnDoc";
            this.cbOwnDoc.Size = new System.Drawing.Size(154, 17);
            this.cbOwnDoc.TabIndex = 7;
            this.cbOwnDoc.Text = "eigenes Dokument erstellen";
            this.cbOwnDoc.UseVisualStyleBackColor = true;
            this.cbOwnDoc.CheckedChanged += new System.EventHandler(this.cbOwnDoc_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(344, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Druckanzahl Lieferschein";
            // 
            // nudPMLfs
            // 
            this.nudPMLfs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPMLfs.Location = new System.Drawing.Point(287, 57);
            this.nudPMLfs.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPMLfs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPMLfs.Name = "nudPMLfs";
            this.nudPMLfs.Size = new System.Drawing.Size(41, 20);
            this.nudPMLfs.TabIndex = 9;
            this.nudPMLfs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbNeutrLfsAbholschein
            // 
            this.cbNeutrLfsAbholschein.AutoSize = true;
            this.cbNeutrLfsAbholschein.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNeutrLfsAbholschein.Location = new System.Drawing.Point(25, 126);
            this.cbNeutrLfsAbholschein.Name = "cbNeutrLfsAbholschein";
            this.cbNeutrLfsAbholschein.Size = new System.Drawing.Size(232, 17);
            this.cbNeutrLfsAbholschein.TabIndex = 5;
            this.cbNeutrLfsAbholschein.Text = "neutr. Lieferschein + Abholschein (Standard)";
            this.cbNeutrLfsAbholschein.UseVisualStyleBackColor = true;
            this.cbNeutrLfsAbholschein.CheckedChanged += new System.EventHandler(this.cbNeutrLfsAbholschein_CheckedChanged);
            // 
            // cbLfsAbholschein
            // 
            this.cbLfsAbholschein.AutoSize = true;
            this.cbLfsAbholschein.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLfsAbholschein.Location = new System.Drawing.Point(25, 103);
            this.cbLfsAbholschein.Name = "cbLfsAbholschein";
            this.cbLfsAbholschein.Size = new System.Drawing.Size(202, 17);
            this.cbLfsAbholschein.TabIndex = 4;
            this.cbLfsAbholschein.Text = "Lieferschein + Abholschein (Standard)";
            this.cbLfsAbholschein.UseVisualStyleBackColor = true;
            this.cbLfsAbholschein.CheckedChanged += new System.EventHandler(this.cbLfsAbholschein_CheckedChanged);
            // 
            // cbNeutrLfSchein
            // 
            this.cbNeutrLfSchein.AutoSize = true;
            this.cbNeutrLfSchein.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNeutrLfSchein.Location = new System.Drawing.Point(25, 57);
            this.cbNeutrLfSchein.Name = "cbNeutrLfSchein";
            this.cbNeutrLfSchein.Size = new System.Drawing.Size(124, 17);
            this.cbNeutrLfSchein.TabIndex = 2;
            this.cbNeutrLfSchein.Text = "neutraler Lieferschein";
            this.cbNeutrLfSchein.UseVisualStyleBackColor = true;
            this.cbNeutrLfSchein.CheckedChanged += new System.EventHandler(this.cbNeutrLfSchein_CheckedChanged);
            // 
            // cbAbholschein
            // 
            this.cbAbholschein.AutoSize = true;
            this.cbAbholschein.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAbholschein.Location = new System.Drawing.Point(25, 80);
            this.cbAbholschein.Name = "cbAbholschein";
            this.cbAbholschein.Size = new System.Drawing.Size(81, 17);
            this.cbAbholschein.TabIndex = 3;
            this.cbAbholschein.Text = "Abholschein";
            this.cbAbholschein.UseVisualStyleBackColor = true;
            this.cbAbholschein.CheckedChanged += new System.EventHandler(this.cbAbholschein_CheckedChanged);
            // 
            // cbFremdLfs
            // 
            this.cbFremdLfs.AutoSize = true;
            this.cbFremdLfs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFremdLfs.Location = new System.Drawing.Point(25, 149);
            this.cbFremdLfs.Name = "cbFremdLfs";
            this.cbFremdLfs.Size = new System.Drawing.Size(133, 17);
            this.cbFremdLfs.TabIndex = 6;
            this.cbFremdLfs.Text = "Lieferschein Holzrichter";
            this.cbFremdLfs.UseVisualStyleBackColor = true;
            this.cbFremdLfs.CheckedChanged += new System.EventHandler(this.cbAnmeldeschein_CheckedChanged);
            // 
            // cbLfSchein
            // 
            this.cbLfSchein.AutoSize = true;
            this.cbLfSchein.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLfSchein.Location = new System.Drawing.Point(25, 34);
            this.cbLfSchein.Name = "cbLfSchein";
            this.cbLfSchein.Size = new System.Drawing.Size(80, 17);
            this.cbLfSchein.TabIndex = 1;
            this.cbLfSchein.Text = "Lieferschein";
            this.cbLfSchein.UseVisualStyleBackColor = true;
            this.cbLfSchein.CheckedChanged += new System.EventHandler(this.cbLfSchein_CheckedChanged);
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPrint,
            this.tsbtnDirectPrint});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(505, 25);
            this.afToolStrip1.TabIndex = 8;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrint.Image")));
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPrint.Text = "Druckvorschau erstellen ";
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // tsbtnDirectPrint
            // 
            this.tsbtnDirectPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDirectPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDirectPrint.Image")));
            this.tsbtnDirectPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDirectPrint.Name = "tsbtnDirectPrint";
            this.tsbtnDirectPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDirectPrint.Text = "Dokument drucken";
            this.tsbtnDirectPrint.Click += new System.EventHandler(this.tsbtnDirectPrint_Click);
            // 
            // ctrPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panPrint);
            this.Name = "ctrPrint";
            this.Size = new System.Drawing.Size(505, 873);
            this.Load += new System.EventHandler(this.ctrPrint_Load);
            this.panPrint.ResumeLayout(false);
            this.panPrint.PerformLayout();
            this.gbDocDaten.ResumeLayout(false);
            this.gbDocDaten.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.gbDruckArt.ResumeLayout(false);
            this.gbDruckArt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPMLfs)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panPrint;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.GroupBox gbDruckArt;
        private System.Windows.Forms.CheckBox cbAbholschein;
        private System.Windows.Forms.CheckBox cbFremdLfs;
        private System.Windows.Forms.CheckBox cbLfSchein;
        private System.Windows.Forms.TextBox tbDocName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbZM;
        private System.Windows.Forms.TextBox tbAuflieger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFahrer;
        private Sped4.Controls.AFGrid dgv;
        private System.Windows.Forms.ToolStripButton tsbtnDirectPrint;
        private System.Windows.Forms.CheckBox cbLfsAbholschein;
        private System.Windows.Forms.CheckBox cbNeutrLfsAbholschein;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudPMLfs;
        private System.Windows.Forms.TextBox tbNeutrDocName;
        private System.Windows.Forms.CheckBox cbOwnDoc;
        private System.Windows.Forms.TextBox tbVersender;
        private System.Windows.Forms.TextBox tbEmpfaenger;
        private System.Windows.Forms.Button btnVersender;
        private System.Windows.Forms.Button btnEmfaenger;
        private System.Windows.Forms.GroupBox gbDocDaten;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtp_PrintDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudPMA;
        public System.Windows.Forms.CheckBox cbNeutrLfSchein;
        private System.Windows.Forms.CheckBox cbNotizPrint;
        public System.Windows.Forms.TextBox tbSearchE;
        public System.Windows.Forms.TextBox tbSearchV;
    }
}
