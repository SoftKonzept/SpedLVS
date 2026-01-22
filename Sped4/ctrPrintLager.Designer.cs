namespace Sped4
{
    partial class ctrPrintLager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrPrintLager));
            this.gbDocDaten = new System.Windows.Forms.GroupBox();
            this.tbSearchEnt = new System.Windows.Forms.TextBox();
            this.btnEntladestelle = new System.Windows.Forms.Button();
            this.tbEntladestelle = new System.Windows.Forms.TextBox();
            this.tbSearchA = new System.Windows.Forms.TextBox();
            this.tbAuftraggeber = new System.Windows.Forms.TextBox();
            this.btnAuftraggeber = new System.Windows.Forms.Button();
            this.tbSearchSped = new System.Windows.Forms.TextBox();
            this.tbnSped = new System.Windows.Forms.Button();
            this.tbSpedition = new System.Windows.Forms.TextBox();
            this.dtp_PrintDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearchV = new System.Windows.Forms.TextBox();
            this.tbSearchE = new System.Windows.Forms.TextBox();
            this.btnEmfaenger = new System.Windows.Forms.Button();
            this.tbVersender = new System.Windows.Forms.TextBox();
            this.tbDocName = new System.Windows.Forms.TextBox();
            this.tbKFZ = new System.Windows.Forms.TextBox();
            this.btnVersender = new System.Windows.Forms.Button();
            this.lKennzeichen = new System.Windows.Forms.Label();
            this.tbEmpfaenger = new System.Windows.Forms.TextBox();
            this.gbDruckArt = new System.Windows.Forms.GroupBox();
            this.nudPrintCount = new System.Windows.Forms.NumericUpDown();
            this.clbPrintDocs = new System.Windows.Forms.CheckedListBox();
            this.cbAusgangAnzeige = new System.Windows.Forms.CheckBox();
            this.cbLEingangDokument = new System.Windows.Forms.CheckBox();
            this.cbNeutraleLAusgangDocs = new System.Windows.Forms.CheckBox();
            this.cbLAusgangAllDocs = new System.Windows.Forms.CheckBox();
            this.cbLAusgangDocument = new System.Windows.Forms.CheckBox();
            this.cbLAusgangsLieferschein = new System.Windows.Forms.CheckBox();
            this.cbEingangsLieferschein = new System.Windows.Forms.CheckBox();
            this.cbEinangDocKomplett = new System.Windows.Forms.CheckBox();
            this.cbArtikelLabel = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbNeutralADR = new System.Windows.Forms.GroupBox();
            this.btnNeutrDelete = new System.Windows.Forms.Button();
            this.tbSearchNeutrAuftraggeber = new System.Windows.Forms.TextBox();
            this.tbNeutralerAuftraggeber = new System.Windows.Forms.TextBox();
            this.btnNeutrAuftraggeber = new System.Windows.Forms.Button();
            this.tbSearchNeutrEmpfaenger = new System.Windows.Forms.TextBox();
            this.tbNeutralerEmpfaenger = new System.Windows.Forms.TextBox();
            this.btnNeutralerEmp = new System.Windows.Forms.Button();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDirectPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.lPrintAnzahl = new System.Windows.Forms.Label();
            this.gbDocDaten.SuspendLayout();
            this.gbDruckArt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrintCount)).BeginInit();
            this.gbNeutralADR.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDocDaten
            // 
            this.gbDocDaten.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDocDaten.Controls.Add(this.tbSearchEnt);
            this.gbDocDaten.Controls.Add(this.btnEntladestelle);
            this.gbDocDaten.Controls.Add(this.tbEntladestelle);
            this.gbDocDaten.Controls.Add(this.tbSearchA);
            this.gbDocDaten.Controls.Add(this.tbAuftraggeber);
            this.gbDocDaten.Controls.Add(this.btnAuftraggeber);
            this.gbDocDaten.Controls.Add(this.tbSearchSped);
            this.gbDocDaten.Controls.Add(this.tbnSped);
            this.gbDocDaten.Controls.Add(this.tbSpedition);
            this.gbDocDaten.Controls.Add(this.dtp_PrintDate);
            this.gbDocDaten.Controls.Add(this.label6);
            this.gbDocDaten.Controls.Add(this.label1);
            this.gbDocDaten.Controls.Add(this.tbSearchV);
            this.gbDocDaten.Controls.Add(this.tbSearchE);
            this.gbDocDaten.Controls.Add(this.btnEmfaenger);
            this.gbDocDaten.Controls.Add(this.tbVersender);
            this.gbDocDaten.Controls.Add(this.tbDocName);
            this.gbDocDaten.Controls.Add(this.tbKFZ);
            this.gbDocDaten.Controls.Add(this.btnVersender);
            this.gbDocDaten.Controls.Add(this.lKennzeichen);
            this.gbDocDaten.Controls.Add(this.tbEmpfaenger);
            this.gbDocDaten.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbDocDaten.Location = new System.Drawing.Point(6, 411);
            this.gbDocDaten.Name = "gbDocDaten";
            this.gbDocDaten.Size = new System.Drawing.Size(509, 221);
            this.gbDocDaten.TabIndex = 132;
            this.gbDocDaten.TabStop = false;
            this.gbDocDaten.Text = "Daten im gedruckten Dokument";
            this.gbDocDaten.Visible = false;
            // 
            // tbSearchEnt
            // 
            this.tbSearchEnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchEnt.Location = new System.Drawing.Point(118, 98);
            this.tbSearchEnt.Name = "tbSearchEnt";
            this.tbSearchEnt.Size = new System.Drawing.Size(104, 20);
            this.tbSearchEnt.TabIndex = 140;
            // 
            // btnEntladestelle
            // 
            this.btnEntladestelle.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnEntladestelle.Location = new System.Drawing.Point(17, 98);
            this.btnEntladestelle.Name = "btnEntladestelle";
            this.btnEntladestelle.Size = new System.Drawing.Size(96, 22);
            this.btnEntladestelle.TabIndex = 139;
            this.btnEntladestelle.Text = "Entladestelle";
            this.btnEntladestelle.UseVisualStyleBackColor = true;
            // 
            // tbEntladestelle
            // 
            this.tbEntladestelle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEntladestelle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEntladestelle.Enabled = false;
            this.tbEntladestelle.Location = new System.Drawing.Point(229, 98);
            this.tbEntladestelle.Name = "tbEntladestelle";
            this.tbEntladestelle.ReadOnly = true;
            this.tbEntladestelle.Size = new System.Drawing.Size(248, 20);
            this.tbEntladestelle.TabIndex = 141;
            // 
            // tbSearchA
            // 
            this.tbSearchA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchA.Location = new System.Drawing.Point(118, 24);
            this.tbSearchA.Name = "tbSearchA";
            this.tbSearchA.Size = new System.Drawing.Size(104, 20);
            this.tbSearchA.TabIndex = 137;
            // 
            // tbAuftraggeber
            // 
            this.tbAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuftraggeber.Enabled = false;
            this.tbAuftraggeber.Location = new System.Drawing.Point(228, 24);
            this.tbAuftraggeber.Name = "tbAuftraggeber";
            this.tbAuftraggeber.ReadOnly = true;
            this.tbAuftraggeber.Size = new System.Drawing.Size(248, 20);
            this.tbAuftraggeber.TabIndex = 138;
            // 
            // btnAuftraggeber
            // 
            this.btnAuftraggeber.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnAuftraggeber.Location = new System.Drawing.Point(16, 24);
            this.btnAuftraggeber.Name = "btnAuftraggeber";
            this.btnAuftraggeber.Size = new System.Drawing.Size(96, 22);
            this.btnAuftraggeber.TabIndex = 136;
            this.btnAuftraggeber.Text = "Auftraggeber";
            this.btnAuftraggeber.UseVisualStyleBackColor = true;
            // 
            // tbSearchSped
            // 
            this.tbSearchSped.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchSped.Location = new System.Drawing.Point(118, 123);
            this.tbSearchSped.Name = "tbSearchSped";
            this.tbSearchSped.Size = new System.Drawing.Size(104, 20);
            this.tbSearchSped.TabIndex = 134;
            // 
            // tbnSped
            // 
            this.tbnSped.ForeColor = System.Drawing.Color.DarkBlue;
            this.tbnSped.Location = new System.Drawing.Point(16, 123);
            this.tbnSped.Name = "tbnSped";
            this.tbnSped.Size = new System.Drawing.Size(96, 22);
            this.tbnSped.TabIndex = 133;
            this.tbnSped.Text = "Spedition";
            this.tbnSped.UseVisualStyleBackColor = true;
            // 
            // tbSpedition
            // 
            this.tbSpedition.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbSpedition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSpedition.Enabled = false;
            this.tbSpedition.Location = new System.Drawing.Point(228, 123);
            this.tbSpedition.Name = "tbSpedition";
            this.tbSpedition.ReadOnly = true;
            this.tbSpedition.Size = new System.Drawing.Size(248, 20);
            this.tbSpedition.TabIndex = 135;
            // 
            // dtp_PrintDate
            // 
            this.dtp_PrintDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp_PrintDate.Location = new System.Drawing.Point(117, 206);
            this.dtp_PrintDate.Name = "dtp_PrintDate";
            this.dtp_PrintDate.Size = new System.Drawing.Size(198, 20);
            this.dtp_PrintDate.TabIndex = 132;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(16, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 131;
            this.label6.Text = "Druckdatum:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(16, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Dokumentenname:";
            // 
            // tbSearchV
            // 
            this.tbSearchV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchV.Location = new System.Drawing.Point(118, 48);
            this.tbSearchV.Name = "tbSearchV";
            this.tbSearchV.Size = new System.Drawing.Size(104, 20);
            this.tbSearchV.TabIndex = 124;
            // 
            // tbSearchE
            // 
            this.tbSearchE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchE.Location = new System.Drawing.Point(118, 73);
            this.tbSearchE.Name = "tbSearchE";
            this.tbSearchE.Size = new System.Drawing.Size(104, 20);
            this.tbSearchE.TabIndex = 126;
            // 
            // btnEmfaenger
            // 
            this.btnEmfaenger.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnEmfaenger.Location = new System.Drawing.Point(16, 73);
            this.btnEmfaenger.Name = "btnEmfaenger";
            this.btnEmfaenger.Size = new System.Drawing.Size(96, 22);
            this.btnEmfaenger.TabIndex = 125;
            this.btnEmfaenger.Text = "Empfänger";
            this.btnEmfaenger.UseVisualStyleBackColor = true;
            // 
            // tbVersender
            // 
            this.tbVersender.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbVersender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVersender.Enabled = false;
            this.tbVersender.Location = new System.Drawing.Point(228, 48);
            this.tbVersender.Name = "tbVersender";
            this.tbVersender.ReadOnly = true;
            this.tbVersender.Size = new System.Drawing.Size(248, 20);
            this.tbVersender.TabIndex = 127;
            // 
            // tbDocName
            // 
            this.tbDocName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDocName.Location = new System.Drawing.Point(117, 179);
            this.tbDocName.Name = "tbDocName";
            this.tbDocName.Size = new System.Drawing.Size(181, 20);
            this.tbDocName.TabIndex = 10;
            // 
            // tbKFZ
            // 
            this.tbKFZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKFZ.Location = new System.Drawing.Point(118, 155);
            this.tbKFZ.Name = "tbKFZ";
            this.tbKFZ.Size = new System.Drawing.Size(180, 20);
            this.tbKFZ.TabIndex = 13;
            // 
            // btnVersender
            // 
            this.btnVersender.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnVersender.Location = new System.Drawing.Point(16, 48);
            this.btnVersender.Name = "btnVersender";
            this.btnVersender.Size = new System.Drawing.Size(96, 22);
            this.btnVersender.TabIndex = 123;
            this.btnVersender.Text = "Versender";
            this.btnVersender.UseVisualStyleBackColor = true;
            // 
            // lKennzeichen
            // 
            this.lKennzeichen.AutoSize = true;
            this.lKennzeichen.ForeColor = System.Drawing.Color.DarkBlue;
            this.lKennzeichen.Location = new System.Drawing.Point(15, 157);
            this.lKennzeichen.Name = "lKennzeichen";
            this.lKennzeichen.Size = new System.Drawing.Size(95, 13);
            this.lKennzeichen.TabIndex = 22;
            this.lKennzeichen.Text = "KFZ-Kennzeichen:";
            // 
            // tbEmpfaenger
            // 
            this.tbEmpfaenger.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEmpfaenger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbEmpfaenger.Enabled = false;
            this.tbEmpfaenger.Location = new System.Drawing.Point(228, 73);
            this.tbEmpfaenger.Name = "tbEmpfaenger";
            this.tbEmpfaenger.ReadOnly = true;
            this.tbEmpfaenger.Size = new System.Drawing.Size(248, 20);
            this.tbEmpfaenger.TabIndex = 128;
            // 
            // gbDruckArt
            // 
            this.gbDruckArt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDruckArt.Controls.Add(this.clbPrintDocs);
            this.gbDruckArt.Controls.Add(this.cbAusgangAnzeige);
            this.gbDruckArt.Controls.Add(this.cbLEingangDokument);
            this.gbDruckArt.Controls.Add(this.cbNeutraleLAusgangDocs);
            this.gbDruckArt.Controls.Add(this.cbLAusgangAllDocs);
            this.gbDruckArt.Controls.Add(this.cbLAusgangDocument);
            this.gbDruckArt.Controls.Add(this.cbLAusgangsLieferschein);
            this.gbDruckArt.Controls.Add(this.cbEingangsLieferschein);
            this.gbDruckArt.Controls.Add(this.cbEinangDocKomplett);
            this.gbDruckArt.Controls.Add(this.cbArtikelLabel);
            this.gbDruckArt.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbDruckArt.Location = new System.Drawing.Point(6, 196);
            this.gbDruckArt.Name = "gbDruckArt";
            this.gbDruckArt.Size = new System.Drawing.Size(506, 209);
            this.gbDruckArt.TabIndex = 142;
            this.gbDruckArt.TabStop = false;
            this.gbDruckArt.Text = "Dokumentenart";
            // 
            // nudPrintCount
            // 
            this.nudPrintCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPrintCount.Location = new System.Drawing.Point(102, 164);
            this.nudPrintCount.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPrintCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrintCount.Name = "nudPrintCount";
            this.nudPrintCount.Size = new System.Drawing.Size(57, 20);
            this.nudPrintCount.TabIndex = 9;
            this.nudPrintCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPrintCount.ValueChanged += new System.EventHandler(this.nudPrintCount_ValueChanged);
            // 
            // clbPrintDocs
            // 
            this.clbPrintDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbPrintDocs.FormattingEnabled = true;
            this.clbPrintDocs.Location = new System.Drawing.Point(255, 19);
            this.clbPrintDocs.Name = "clbPrintDocs";
            this.clbPrintDocs.Size = new System.Drawing.Size(245, 167);
            this.clbPrintDocs.TabIndex = 27;
            this.clbPrintDocs.MouseHover += new System.EventHandler(this.clbPrintDocs_MouseHover);
            this.clbPrintDocs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.clbPrintDocs_MouseMove);
            // 
            // cbAusgangAnzeige
            // 
            this.cbAusgangAnzeige.AutoSize = true;
            this.cbAusgangAnzeige.Enabled = false;
            this.cbAusgangAnzeige.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAusgangAnzeige.Location = new System.Drawing.Point(23, 104);
            this.cbAusgangAnzeige.Name = "cbAusgangAnzeige";
            this.cbAusgangAnzeige.Size = new System.Drawing.Size(159, 17);
            this.cbAusgangAnzeige.TabIndex = 26;
            this.cbAusgangAnzeige.Text = "Ausgangs - Anzeige drucken";
            this.cbAusgangAnzeige.UseVisualStyleBackColor = true;
            this.cbAusgangAnzeige.CheckedChanged += new System.EventHandler(this.cbAusgangAnzeige_CheckedChanged);
            // 
            // cbLEingangDokument
            // 
            this.cbLEingangDokument.AutoSize = true;
            this.cbLEingangDokument.Enabled = false;
            this.cbLEingangDokument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLEingangDokument.Location = new System.Drawing.Point(23, 34);
            this.cbLEingangDokument.Name = "cbLEingangDokument";
            this.cbLEingangDokument.Size = new System.Drawing.Size(162, 17);
            this.cbLEingangDokument.TabIndex = 25;
            this.cbLEingangDokument.Text = "Eingang - Dokument drucken";
            this.cbLEingangDokument.UseVisualStyleBackColor = true;
            this.cbLEingangDokument.CheckedChanged += new System.EventHandler(this.cbLEingangDokument_CheckedChanged);
            // 
            // cbNeutraleLAusgangDocs
            // 
            this.cbNeutraleLAusgangDocs.AutoSize = true;
            this.cbNeutraleLAusgangDocs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbNeutraleLAusgangDocs.Location = new System.Drawing.Point(23, 154);
            this.cbNeutraleLAusgangDocs.Name = "cbNeutraleLAusgangDocs";
            this.cbNeutraleLAusgangDocs.Size = new System.Drawing.Size(217, 17);
            this.cbNeutraleLAusgangDocs.TabIndex = 24;
            this.cbNeutraleLAusgangDocs.Text = "neutrale Ausgangs - Dokumente erstellen";
            this.cbNeutraleLAusgangDocs.UseVisualStyleBackColor = true;
            this.cbNeutraleLAusgangDocs.CheckedChanged += new System.EventHandler(this.cbNeutraleLAusgangDocs_CheckedChanged);
            // 
            // cbLAusgangAllDocs
            // 
            this.cbLAusgangAllDocs.AutoSize = true;
            this.cbLAusgangAllDocs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLAusgangAllDocs.Location = new System.Drawing.Point(23, 137);
            this.cbLAusgangAllDocs.Name = "cbLAusgangAllDocs";
            this.cbLAusgangAllDocs.Size = new System.Drawing.Size(196, 17);
            this.cbLAusgangAllDocs.TabIndex = 23;
            this.cbLAusgangAllDocs.Text = "Alle Ausgangs - Dokumente erstellen";
            this.cbLAusgangAllDocs.UseVisualStyleBackColor = true;
            this.cbLAusgangAllDocs.CheckedChanged += new System.EventHandler(this.cbLAusgangAllDocs_CheckedChanged);
            // 
            // cbLAusgangDocument
            // 
            this.cbLAusgangDocument.AutoSize = true;
            this.cbLAusgangDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLAusgangDocument.Location = new System.Drawing.Point(23, 89);
            this.cbLAusgangDocument.Name = "cbLAusgangDocument";
            this.cbLAusgangDocument.Size = new System.Drawing.Size(165, 17);
            this.cbLAusgangDocument.TabIndex = 22;
            this.cbLAusgangDocument.Text = "Ausgang - Dokument drucken";
            this.cbLAusgangDocument.UseVisualStyleBackColor = true;
            this.cbLAusgangDocument.CheckedChanged += new System.EventHandler(this.cbLAusgangDocument_CheckedChanged);
            // 
            // cbLAusgangsLieferschein
            // 
            this.cbLAusgangsLieferschein.AutoSize = true;
            this.cbLAusgangsLieferschein.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLAusgangsLieferschein.Location = new System.Drawing.Point(23, 120);
            this.cbLAusgangsLieferschein.Name = "cbLAusgangsLieferschein";
            this.cbLAusgangsLieferschein.Size = new System.Drawing.Size(178, 17);
            this.cbLAusgangsLieferschein.TabIndex = 21;
            this.cbLAusgangsLieferschein.Text = "Ausgangs - Lieferschein drucken";
            this.cbLAusgangsLieferschein.UseVisualStyleBackColor = true;
            this.cbLAusgangsLieferschein.CheckedChanged += new System.EventHandler(this.cbLAusgangsLieferschein_CheckedChanged);
            // 
            // cbEingangsLieferschein
            // 
            this.cbEingangsLieferschein.AutoSize = true;
            this.cbEingangsLieferschein.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEingangsLieferschein.Location = new System.Drawing.Point(23, 50);
            this.cbEingangsLieferschein.Name = "cbEingangsLieferschein";
            this.cbEingangsLieferschein.Size = new System.Drawing.Size(170, 17);
            this.cbEingangsLieferschein.TabIndex = 2;
            this.cbEingangsLieferschein.Text = "Eingang - Lfs/Anzeige drucken";
            this.cbEingangsLieferschein.UseVisualStyleBackColor = true;
            this.cbEingangsLieferschein.CheckedChanged += new System.EventHandler(this.cbEingangsLieferschein_CheckedChanged);
            // 
            // cbEinangDocKomplett
            // 
            this.cbEinangDocKomplett.AutoSize = true;
            this.cbEinangDocKomplett.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEinangDocKomplett.Location = new System.Drawing.Point(23, 67);
            this.cbEinangDocKomplett.Name = "cbEinangDocKomplett";
            this.cbEinangDocKomplett.Size = new System.Drawing.Size(193, 17);
            this.cbEinangDocKomplett.TabIndex = 3;
            this.cbEinangDocKomplett.Text = "Alle Eingangs - Dokumente drucken";
            this.cbEinangDocKomplett.UseVisualStyleBackColor = true;
            this.cbEinangDocKomplett.CheckedChanged += new System.EventHandler(this.cbEinangDocKomplett_CheckedChanged);
            // 
            // cbArtikelLabel
            // 
            this.cbArtikelLabel.AutoSize = true;
            this.cbArtikelLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbArtikelLabel.Location = new System.Drawing.Point(23, 19);
            this.cbArtikelLabel.Name = "cbArtikelLabel";
            this.cbArtikelLabel.Size = new System.Drawing.Size(116, 17);
            this.cbArtikelLabel.TabIndex = 1;
            this.cbArtikelLabel.Text = "Artikellabel drucken";
            this.cbArtikelLabel.UseVisualStyleBackColor = true;
            this.cbArtikelLabel.CheckedChanged += new System.EventHandler(this.cbArtikelLabel_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(76, 369);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Druckanzahl";
            // 
            // gbNeutralADR
            // 
            this.gbNeutralADR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNeutralADR.Controls.Add(this.btnNeutrDelete);
            this.gbNeutralADR.Controls.Add(this.tbSearchNeutrAuftraggeber);
            this.gbNeutralADR.Controls.Add(this.tbNeutralerAuftraggeber);
            this.gbNeutralADR.Controls.Add(this.btnNeutrAuftraggeber);
            this.gbNeutralADR.Controls.Add(this.tbSearchNeutrEmpfaenger);
            this.gbNeutralADR.Controls.Add(this.tbNeutralerEmpfaenger);
            this.gbNeutralADR.Controls.Add(this.btnNeutralerEmp);
            this.gbNeutralADR.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbNeutralADR.Location = new System.Drawing.Point(6, 28);
            this.gbNeutralADR.Name = "gbNeutralADR";
            this.gbNeutralADR.Size = new System.Drawing.Size(506, 125);
            this.gbNeutralADR.TabIndex = 144;
            this.gbNeutralADR.TabStop = false;
            this.gbNeutralADR.Text = "neutrale Adressen für Ausgangsdokumente";
            // 
            // btnNeutrDelete
            // 
            this.btnNeutrDelete.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnNeutrDelete.Location = new System.Drawing.Point(342, 15);
            this.btnNeutrDelete.Name = "btnNeutrDelete";
            this.btnNeutrDelete.Size = new System.Drawing.Size(152, 22);
            this.btnNeutrDelete.TabIndex = 151;
            this.btnNeutrDelete.Text = "neutrale Adressen löschen";
            this.btnNeutrDelete.UseVisualStyleBackColor = true;
            this.btnNeutrDelete.Click += new System.EventHandler(this.btnNeutrDelete_Click);
            // 
            // tbSearchNeutrAuftraggeber
            // 
            this.tbSearchNeutrAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchNeutrAuftraggeber.Location = new System.Drawing.Point(115, 43);
            this.tbSearchNeutrAuftraggeber.Name = "tbSearchNeutrAuftraggeber";
            this.tbSearchNeutrAuftraggeber.Size = new System.Drawing.Size(104, 20);
            this.tbSearchNeutrAuftraggeber.TabIndex = 149;
            // 
            // tbNeutralerAuftraggeber
            // 
            this.tbNeutralerAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbNeutralerAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNeutralerAuftraggeber.Enabled = false;
            this.tbNeutralerAuftraggeber.Location = new System.Drawing.Point(225, 43);
            this.tbNeutralerAuftraggeber.Name = "tbNeutralerAuftraggeber";
            this.tbNeutralerAuftraggeber.ReadOnly = true;
            this.tbNeutralerAuftraggeber.Size = new System.Drawing.Size(269, 20);
            this.tbNeutralerAuftraggeber.TabIndex = 150;
            // 
            // btnNeutrAuftraggeber
            // 
            this.btnNeutrAuftraggeber.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnNeutrAuftraggeber.Location = new System.Drawing.Point(13, 37);
            this.btnNeutrAuftraggeber.Name = "btnNeutrAuftraggeber";
            this.btnNeutrAuftraggeber.Size = new System.Drawing.Size(96, 35);
            this.btnNeutrAuftraggeber.TabIndex = 148;
            this.btnNeutrAuftraggeber.Text = "Neutraler Auftraggeber";
            this.btnNeutrAuftraggeber.UseVisualStyleBackColor = true;
            this.btnNeutrAuftraggeber.Click += new System.EventHandler(this.btnNeutrAuftraggeber_Click);
            // 
            // tbSearchNeutrEmpfaenger
            // 
            this.tbSearchNeutrEmpfaenger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchNeutrEmpfaenger.Location = new System.Drawing.Point(115, 85);
            this.tbSearchNeutrEmpfaenger.Name = "tbSearchNeutrEmpfaenger";
            this.tbSearchNeutrEmpfaenger.Size = new System.Drawing.Size(104, 20);
            this.tbSearchNeutrEmpfaenger.TabIndex = 146;
            // 
            // tbNeutralerEmpfaenger
            // 
            this.tbNeutralerEmpfaenger.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbNeutralerEmpfaenger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNeutralerEmpfaenger.Enabled = false;
            this.tbNeutralerEmpfaenger.Location = new System.Drawing.Point(225, 85);
            this.tbNeutralerEmpfaenger.Name = "tbNeutralerEmpfaenger";
            this.tbNeutralerEmpfaenger.ReadOnly = true;
            this.tbNeutralerEmpfaenger.Size = new System.Drawing.Size(269, 20);
            this.tbNeutralerEmpfaenger.TabIndex = 147;
            // 
            // btnNeutralerEmp
            // 
            this.btnNeutralerEmp.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnNeutralerEmp.Location = new System.Drawing.Point(13, 78);
            this.btnNeutralerEmp.Name = "btnNeutralerEmp";
            this.btnNeutralerEmp.Size = new System.Drawing.Size(96, 35);
            this.btnNeutralerEmp.TabIndex = 145;
            this.btnNeutralerEmp.Text = "Neutraler Empfänger";
            this.btnNeutralerEmp.UseVisualStyleBackColor = true;
            this.btnNeutralerEmp.Click += new System.EventHandler(this.btnNeutralerEmp_Click);
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPrint,
            this.tsbtnDirectPrint,
            this.tsbtnClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(535, 25);
            this.afToolStrip1.TabIndex = 9;
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
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Fenster schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // lPrintAnzahl
            // 
            this.lPrintAnzahl.AutoSize = true;
            this.lPrintAnzahl.Location = new System.Drawing.Point(16, 166);
            this.lPrintAnzahl.Name = "lPrintAnzahl";
            this.lPrintAnzahl.Size = new System.Drawing.Size(70, 13);
            this.lPrintAnzahl.TabIndex = 145;
            this.lPrintAnzahl.Text = "Druckanzahl:";
            // 
            // ctrPrintLager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lPrintAnzahl);
            this.Controls.Add(this.nudPrintCount);
            this.Controls.Add(this.gbNeutralADR);
            this.Controls.Add(this.gbDruckArt);
            this.Controls.Add(this.gbDocDaten);
            this.Controls.Add(this.afToolStrip1);
            this.Controls.Add(this.label5);
            this.Name = "ctrPrintLager";
            this.Size = new System.Drawing.Size(535, 635);
            this.Load += new System.EventHandler(this.ctrPrintLager_Load);
            this.gbDocDaten.ResumeLayout(false);
            this.gbDocDaten.PerformLayout();
            this.gbDruckArt.ResumeLayout(false);
            this.gbDruckArt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrintCount)).EndInit();
            this.gbNeutralADR.ResumeLayout(false);
            this.gbNeutralADR.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsbtnDirectPrint;
        private System.Windows.Forms.GroupBox gbDocDaten;
        public System.Windows.Forms.TextBox tbSearchA;
        private System.Windows.Forms.TextBox tbAuftraggeber;
        private System.Windows.Forms.Button btnAuftraggeber;
        public System.Windows.Forms.TextBox tbSearchSped;
        private System.Windows.Forms.Button tbnSped;
        private System.Windows.Forms.TextBox tbSpedition;
        private System.Windows.Forms.DateTimePicker dtp_PrintDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tbSearchV;
        public System.Windows.Forms.TextBox tbSearchE;
        private System.Windows.Forms.Button btnEmfaenger;
        private System.Windows.Forms.TextBox tbVersender;
        private System.Windows.Forms.TextBox tbDocName;
        private System.Windows.Forms.TextBox tbKFZ;
        private System.Windows.Forms.Button btnVersender;
        private System.Windows.Forms.Label lKennzeichen;
        private System.Windows.Forms.TextBox tbEmpfaenger;
        public System.Windows.Forms.TextBox tbSearchEnt;
        private System.Windows.Forms.Button btnEntladestelle;
        private System.Windows.Forms.TextBox tbEntladestelle;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.GroupBox gbDruckArt;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox cbEingangsLieferschein;
        private System.Windows.Forms.CheckBox cbEinangDocKomplett;
        private System.Windows.Forms.CheckBox cbArtikelLabel;
        public System.Windows.Forms.CheckBox cbLAusgangsLieferschein;
        public System.Windows.Forms.CheckBox cbLAusgangAllDocs;
        public System.Windows.Forms.CheckBox cbLAusgangDocument;
        public System.Windows.Forms.CheckBox cbNeutraleLAusgangDocs;
        private System.Windows.Forms.GroupBox gbNeutralADR;
        private System.Windows.Forms.Button btnNeutrDelete;
        public System.Windows.Forms.TextBox tbSearchNeutrAuftraggeber;
        private System.Windows.Forms.TextBox tbNeutralerAuftraggeber;
        private System.Windows.Forms.Button btnNeutrAuftraggeber;
        public System.Windows.Forms.TextBox tbSearchNeutrEmpfaenger;
        private System.Windows.Forms.TextBox tbNeutralerEmpfaenger;
        private System.Windows.Forms.Button btnNeutralerEmp;
        public System.Windows.Forms.CheckBox cbLEingangDokument;
        public System.Windows.Forms.NumericUpDown nudPrintCount;
        public System.Windows.Forms.CheckBox cbAusgangAnzeige;
        private System.Windows.Forms.CheckedListBox clbPrintDocs;
        private System.Windows.Forms.Label lPrintAnzahl;
    }
}
