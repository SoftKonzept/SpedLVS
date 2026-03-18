namespace Sped4
{
    partial class frmFahrzeuge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFahrzeuge));
            this.gbFahrDaten = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIntIDSelect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lInternIDAuflieger = new System.Windows.Forms.Label();
            this.btnInterneNr = new System.Windows.Forms.Button();
            this.pbINr = new System.Windows.Forms.PictureBox();
            this.tbKIntern = new System.Windows.Forms.TextBox();
            this.lInternIDZM = new System.Windows.Forms.Label();
            this.pbTruck = new System.Windows.Forms.PictureBox();
            this.pbTrailer = new System.Windows.Forms.PictureBox();
            this.chbTrailer = new System.Windows.Forms.CheckBox();
            this.chbZM = new System.Windows.Forms.CheckBox();
            this.tbBezeichnung = new System.Windows.Forms.TextBox();
            this.lFabrikat = new System.Windows.Forms.Label();
            this.lBeizeichnung = new System.Windows.Forms.Label();
            this.lFGNr = new System.Windows.Forms.Label();
            this.tbFabrikat = new System.Windows.Forms.TextBox();
            this.tbFGNr = new System.Windows.Forms.TextBox();
            this.tbKFZ = new System.Windows.Forms.TextBox();
            this.lKFZ = new System.Windows.Forms.Label();
            this.lSeit = new System.Windows.Forms.Label();
            this.lStellplaetze = new System.Windows.Forms.Label();
            this.lLaufleistung = new System.Windows.Forms.Label();
            this.tbLaufleistung = new System.Windows.Forms.TextBox();
            this.tbStellplaetze = new System.Windows.Forms.TextBox();
            this.lBJ = new System.Windows.Forms.Label();
            this.lSP = new System.Windows.Forms.Label();
            this.lTuev = new System.Windows.Forms.Label();
            this.lBesonerheit = new System.Windows.Forms.Label();
            this.tbBesonderheit = new System.Windows.Forms.TextBox();
            this.lInnenh = new System.Windows.Forms.Label();
            this.tbInnenhoehe = new System.Windows.Forms.TextBox();
            this.dtpTuev = new System.Windows.Forms.DateTimePicker();
            this.dtpSP = new System.Windows.Forms.DateTimePicker();
            this.dtpBJ = new System.Windows.Forms.DateTimePicker();
            this.dtpSeit = new System.Windows.Forms.DateTimePicker();
            this.chbCoil = new System.Windows.Forms.CheckBox();
            this.chbPlane = new System.Windows.Forms.CheckBox();
            this.chbSattel = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lLeergewicht = new System.Windows.Forms.Label();
            this.gbFahr2 = new System.Windows.Forms.GroupBox();
            this.cbBesitzer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbZM = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboAbgas = new System.Windows.Forms.ComboBox();
            this.gbAuflieger = new System.Windows.Forms.GroupBox();
            this.nudAchsen = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLaengeA = new System.Windows.Forms.TextBox();
            this.lLaenge = new System.Windows.Forms.Label();
            this.dtpAbmeldung = new System.Windows.Forms.DateTimePicker();
            this.cbAbmeldung = new System.Windows.Forms.CheckBox();
            this.tbLeergewicht = new System.Windows.Forms.TextBox();
            this.tbZlGw = new System.Windows.Forms.TextBox();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnSavePrint = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.gbFahrDaten.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbINr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTruck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrailer)).BeginInit();
            this.gbFahr2.SuspendLayout();
            this.gbZM.SuspendLayout();
            this.gbAuflieger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAchsen)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFahrDaten
            // 
            this.gbFahrDaten.Controls.Add(this.groupBox1);
            this.gbFahrDaten.Controls.Add(this.pbTruck);
            this.gbFahrDaten.Controls.Add(this.pbTrailer);
            this.gbFahrDaten.Controls.Add(this.chbTrailer);
            this.gbFahrDaten.Controls.Add(this.chbZM);
            this.gbFahrDaten.Controls.Add(this.tbBezeichnung);
            this.gbFahrDaten.Controls.Add(this.lFabrikat);
            this.gbFahrDaten.Controls.Add(this.lBeizeichnung);
            this.gbFahrDaten.Controls.Add(this.lFGNr);
            this.gbFahrDaten.Controls.Add(this.tbFabrikat);
            this.gbFahrDaten.Controls.Add(this.tbFGNr);
            this.gbFahrDaten.Controls.Add(this.tbKFZ);
            this.gbFahrDaten.Controls.Add(this.lKFZ);
            this.gbFahrDaten.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbFahrDaten.Location = new System.Drawing.Point(12, 39);
            this.gbFahrDaten.Name = "gbFahrDaten";
            this.gbFahrDaten.Size = new System.Drawing.Size(550, 266);
            this.gbFahrDaten.TabIndex = 29;
            this.gbFahrDaten.TabStop = false;
            this.gbFahrDaten.Text = "Fahrzeugdaten";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnIntIDSelect);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lInternIDAuflieger);
            this.groupBox1.Controls.Add(this.btnInterneNr);
            this.groupBox1.Controls.Add(this.pbINr);
            this.groupBox1.Controls.Add(this.tbKIntern);
            this.groupBox1.Controls.Add(this.lInternIDZM);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(24, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 124);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Interne ID";
            // 
            // btnIntIDSelect
            // 
            this.btnIntIDSelect.Enabled = false;
            this.btnIntIDSelect.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnIntIDSelect.Location = new System.Drawing.Point(7, 61);
            this.btnIntIDSelect.Name = "btnIntIDSelect";
            this.btnIntIDSelect.Size = new System.Drawing.Size(90, 57);
            this.btnIntIDSelect.TabIndex = 33;
            this.btnIntIDSelect.Text = "gewählte ID dem Fahrzeug zuweisen";
            this.btnIntIDSelect.UseVisualStyleBackColor = true;
            this.btnIntIDSelect.Click += new System.EventHandler(this.btnIntIDSelect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "[nächste ID]";
            // 
            // lInternIDAuflieger
            // 
            this.lInternIDAuflieger.AutoSize = true;
            this.lInternIDAuflieger.Location = new System.Drawing.Point(103, 77);
            this.lInternIDAuflieger.Name = "lInternIDAuflieger";
            this.lInternIDAuflieger.Size = new System.Drawing.Size(22, 13);
            this.lInternIDAuflieger.TabIndex = 31;
            this.lInternIDAuflieger.Text = "[nr]";
            // 
            // btnInterneNr
            // 
            this.btnInterneNr.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnInterneNr.Location = new System.Drawing.Point(7, 19);
            this.btnInterneNr.Name = "btnInterneNr";
            this.btnInterneNr.Size = new System.Drawing.Size(90, 36);
            this.btnInterneNr.TabIndex = 29;
            this.btnInterneNr.Text = "Check Interne ID";
            this.btnInterneNr.UseVisualStyleBackColor = true;
            this.btnInterneNr.Click += new System.EventHandler(this.btnInterneNr_Click);
            // 
            // pbINr
            // 
            this.pbINr.Image = ((System.Drawing.Image)(resources.GetObject("pbINr.Image")));
            this.pbINr.Location = new System.Drawing.Point(264, 9);
            this.pbINr.Name = "pbINr";
            this.pbINr.Size = new System.Drawing.Size(26, 30);
            this.pbINr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbINr.TabIndex = 30;
            this.pbINr.TabStop = false;
            // 
            // tbKIntern
            // 
            this.tbKIntern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKIntern.Location = new System.Drawing.Point(174, 19);
            this.tbKIntern.Name = "tbKIntern";
            this.tbKIntern.Size = new System.Drawing.Size(84, 20);
            this.tbKIntern.TabIndex = 27;
            // 
            // lInternIDZM
            // 
            this.lInternIDZM.AutoSize = true;
            this.lInternIDZM.Location = new System.Drawing.Point(103, 51);
            this.lInternIDZM.Name = "lInternIDZM";
            this.lInternIDZM.Size = new System.Drawing.Size(22, 13);
            this.lInternIDZM.TabIndex = 28;
            this.lInternIDZM.Text = "[nr]";
            // 
            // pbTruck
            // 
            this.pbTruck.Image = global::Sped4.Properties.Resources.truck_green;
            this.pbTruck.Location = new System.Drawing.Point(359, 31);
            this.pbTruck.Name = "pbTruck";
            this.pbTruck.Size = new System.Drawing.Size(25, 24);
            this.pbTruck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbTruck.TabIndex = 25;
            this.pbTruck.TabStop = false;
            // 
            // pbTrailer
            // 
            this.pbTrailer.Image = global::Sped4.Properties.Resources.package;
            this.pbTrailer.Location = new System.Drawing.Point(359, 61);
            this.pbTrailer.Name = "pbTrailer";
            this.pbTrailer.Size = new System.Drawing.Size(26, 26);
            this.pbTrailer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbTrailer.TabIndex = 24;
            this.pbTrailer.TabStop = false;
            // 
            // chbTrailer
            // 
            this.chbTrailer.AutoSize = true;
            this.chbTrailer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbTrailer.ForeColor = System.Drawing.Color.DarkBlue;
            this.chbTrailer.Location = new System.Drawing.Point(391, 61);
            this.chbTrailer.Name = "chbTrailer";
            this.chbTrailer.Size = new System.Drawing.Size(69, 17);
            this.chbTrailer.TabIndex = 3;
            this.chbTrailer.Text = "Anhänger";
            this.chbTrailer.UseVisualStyleBackColor = true;
            this.chbTrailer.CheckedChanged += new System.EventHandler(this.chbTrailer_CheckedChanged);
            // 
            // chbZM
            // 
            this.chbZM.AutoSize = true;
            this.chbZM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbZM.ForeColor = System.Drawing.Color.DarkBlue;
            this.chbZM.Location = new System.Drawing.Point(391, 31);
            this.chbZM.Name = "chbZM";
            this.chbZM.Size = new System.Drawing.Size(157, 17);
            this.chbZM.TabIndex = 2;
            this.chbZM.Text = "Zugmaschine / Motorwagen";
            this.chbZM.UseVisualStyleBackColor = true;
            this.chbZM.CheckedChanged += new System.EventHandler(this.chbZM_CheckedChanged);
            // 
            // tbBezeichnung
            // 
            this.tbBezeichnung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBezeichnung.Location = new System.Drawing.Point(128, 205);
            this.tbBezeichnung.Name = "tbBezeichnung";
            this.tbBezeichnung.Size = new System.Drawing.Size(357, 20);
            this.tbBezeichnung.TabIndex = 5;
            // 
            // lFabrikat
            // 
            this.lFabrikat.AutoSize = true;
            this.lFabrikat.ForeColor = System.Drawing.Color.DarkBlue;
            this.lFabrikat.Location = new System.Drawing.Point(19, 185);
            this.lFabrikat.Name = "lFabrikat";
            this.lFabrikat.Size = new System.Drawing.Size(45, 13);
            this.lFabrikat.TabIndex = 0;
            this.lFabrikat.Text = "Fabrikat";
            // 
            // lBeizeichnung
            // 
            this.lBeizeichnung.AutoSize = true;
            this.lBeizeichnung.ForeColor = System.Drawing.Color.DarkBlue;
            this.lBeizeichnung.Location = new System.Drawing.Point(19, 208);
            this.lBeizeichnung.Name = "lBeizeichnung";
            this.lBeizeichnung.Size = new System.Drawing.Size(69, 13);
            this.lBeizeichnung.TabIndex = 1;
            this.lBeizeichnung.Text = "Bezeichnung";
            // 
            // lFGNr
            // 
            this.lFGNr.AutoSize = true;
            this.lFGNr.BackColor = System.Drawing.Color.White;
            this.lFGNr.ForeColor = System.Drawing.Color.DarkBlue;
            this.lFGNr.Location = new System.Drawing.Point(19, 234);
            this.lFGNr.Name = "lFGNr";
            this.lFGNr.Size = new System.Drawing.Size(95, 13);
            this.lFGNr.TabIndex = 2;
            this.lFGNr.Text = "Fahrgestellnummer";
            // 
            // tbFabrikat
            // 
            this.tbFabrikat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFabrikat.Location = new System.Drawing.Point(128, 182);
            this.tbFabrikat.Name = "tbFabrikat";
            this.tbFabrikat.Size = new System.Drawing.Size(357, 20);
            this.tbFabrikat.TabIndex = 4;
            // 
            // tbFGNr
            // 
            this.tbFGNr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFGNr.Location = new System.Drawing.Point(128, 231);
            this.tbFGNr.Name = "tbFGNr";
            this.tbFGNr.Size = new System.Drawing.Size(357, 20);
            this.tbFGNr.TabIndex = 6;
            // 
            // tbKFZ
            // 
            this.tbKFZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKFZ.Location = new System.Drawing.Point(130, 26);
            this.tbKFZ.Name = "tbKFZ";
            this.tbKFZ.Size = new System.Drawing.Size(219, 20);
            this.tbKFZ.TabIndex = 1;
            // 
            // lKFZ
            // 
            this.lKFZ.AutoSize = true;
            this.lKFZ.ForeColor = System.Drawing.Color.DarkBlue;
            this.lKFZ.Location = new System.Drawing.Point(21, 29);
            this.lKFZ.Name = "lKFZ";
            this.lKFZ.Size = new System.Drawing.Size(92, 13);
            this.lKFZ.TabIndex = 21;
            this.lKFZ.Text = "KFZ-Kennzeichen";
            // 
            // lSeit
            // 
            this.lSeit.AutoSize = true;
            this.lSeit.ForeColor = System.Drawing.Color.DarkBlue;
            this.lSeit.Location = new System.Drawing.Point(19, 223);
            this.lSeit.Name = "lSeit";
            this.lSeit.Size = new System.Drawing.Size(80, 13);
            this.lSeit.TabIndex = 16;
            this.lSeit.Text = "Inbetriebnahme";
            // 
            // lStellplaetze
            // 
            this.lStellplaetze.AutoSize = true;
            this.lStellplaetze.ForeColor = System.Drawing.Color.DarkBlue;
            this.lStellplaetze.Location = new System.Drawing.Point(91, 122);
            this.lStellplaetze.Name = "lStellplaetze";
            this.lStellplaetze.Size = new System.Drawing.Size(55, 13);
            this.lStellplaetze.TabIndex = 8;
            this.lStellplaetze.Text = "Stellplätze";
            // 
            // lLaufleistung
            // 
            this.lLaufleistung.AutoSize = true;
            this.lLaufleistung.ForeColor = System.Drawing.Color.DarkBlue;
            this.lLaufleistung.Location = new System.Drawing.Point(320, 272);
            this.lLaufleistung.Name = "lLaufleistung";
            this.lLaufleistung.Size = new System.Drawing.Size(64, 13);
            this.lLaufleistung.TabIndex = 6;
            this.lLaufleistung.Text = "Laufleistung";
            // 
            // tbLaufleistung
            // 
            this.tbLaufleistung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLaufleistung.Location = new System.Drawing.Point(401, 270);
            this.tbLaufleistung.Name = "tbLaufleistung";
            this.tbLaufleistung.Size = new System.Drawing.Size(108, 20);
            this.tbLaufleistung.TabIndex = 22;
            // 
            // tbStellplaetze
            // 
            this.tbStellplaetze.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbStellplaetze.Location = new System.Drawing.Point(162, 119);
            this.tbStellplaetze.Name = "tbStellplaetze";
            this.tbStellplaetze.Size = new System.Drawing.Size(108, 20);
            this.tbStellplaetze.TabIndex = 19;
            // 
            // lBJ
            // 
            this.lBJ.AutoSize = true;
            this.lBJ.ForeColor = System.Drawing.Color.DarkBlue;
            this.lBJ.Location = new System.Drawing.Point(19, 172);
            this.lBJ.Name = "lBJ";
            this.lBJ.Size = new System.Drawing.Size(43, 13);
            this.lBJ.TabIndex = 9;
            this.lBJ.Text = "Baujahr";
            // 
            // lSP
            // 
            this.lSP.AutoSize = true;
            this.lSP.ForeColor = System.Drawing.Color.DarkBlue;
            this.lSP.Location = new System.Drawing.Point(19, 131);
            this.lSP.Name = "lSP";
            this.lSP.Size = new System.Drawing.Size(21, 13);
            this.lSP.TabIndex = 17;
            this.lSP.Text = "SP";
            // 
            // lTuev
            // 
            this.lTuev.AutoSize = true;
            this.lTuev.ForeColor = System.Drawing.Color.DarkBlue;
            this.lTuev.Location = new System.Drawing.Point(19, 93);
            this.lTuev.Name = "lTuev";
            this.lTuev.Size = new System.Drawing.Size(29, 13);
            this.lTuev.TabIndex = 23;
            this.lTuev.Text = "TÜV";
            // 
            // lBesonerheit
            // 
            this.lBesonerheit.AutoSize = true;
            this.lBesonerheit.ForeColor = System.Drawing.Color.DarkBlue;
            this.lBesonerheit.Location = new System.Drawing.Point(19, 331);
            this.lBesonerheit.Name = "lBesonerheit";
            this.lBesonerheit.Size = new System.Drawing.Size(69, 13);
            this.lBesonerheit.TabIndex = 7;
            this.lBesonerheit.Text = "Besonderheit";
            // 
            // tbBesonderheit
            // 
            this.tbBesonderheit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBesonderheit.Location = new System.Drawing.Point(18, 350);
            this.tbBesonderheit.Multiline = true;
            this.tbBesonderheit.Name = "tbBesonderheit";
            this.tbBesonderheit.Size = new System.Drawing.Size(497, 76);
            this.tbBesonderheit.TabIndex = 23;
            // 
            // lInnenh
            // 
            this.lInnenh.AutoSize = true;
            this.lInnenh.ForeColor = System.Drawing.Color.DarkBlue;
            this.lInnenh.Location = new System.Drawing.Point(64, 98);
            this.lInnenh.Name = "lInnenh";
            this.lInnenh.Size = new System.Drawing.Size(81, 13);
            this.lInnenh.TabIndex = 26;
            this.lInnenh.Text = "Innenhoehe [m]";
            // 
            // tbInnenhoehe
            // 
            this.tbInnenhoehe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbInnenhoehe.Location = new System.Drawing.Point(162, 96);
            this.tbInnenhoehe.Name = "tbInnenhoehe";
            this.tbInnenhoehe.Size = new System.Drawing.Size(108, 20);
            this.tbInnenhoehe.TabIndex = 18;
            // 
            // dtpTuev
            // 
            this.dtpTuev.Location = new System.Drawing.Point(24, 108);
            this.dtpTuev.Name = "dtpTuev";
            this.dtpTuev.Size = new System.Drawing.Size(198, 20);
            this.dtpTuev.TabIndex = 7;
            // 
            // dtpSP
            // 
            this.dtpSP.Location = new System.Drawing.Point(22, 149);
            this.dtpSP.Name = "dtpSP";
            this.dtpSP.Size = new System.Drawing.Size(198, 20);
            this.dtpSP.TabIndex = 8;
            // 
            // dtpBJ
            // 
            this.dtpBJ.Location = new System.Drawing.Point(22, 187);
            this.dtpBJ.Name = "dtpBJ";
            this.dtpBJ.Size = new System.Drawing.Size(198, 20);
            this.dtpBJ.TabIndex = 9;
            // 
            // dtpSeit
            // 
            this.dtpSeit.Location = new System.Drawing.Point(22, 240);
            this.dtpSeit.Name = "dtpSeit";
            this.dtpSeit.Size = new System.Drawing.Size(198, 20);
            this.dtpSeit.TabIndex = 10;
            // 
            // chbCoil
            // 
            this.chbCoil.AutoSize = true;
            this.chbCoil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbCoil.Location = new System.Drawing.Point(20, 77);
            this.chbCoil.Name = "chbCoil";
            this.chbCoil.Size = new System.Drawing.Size(68, 17);
            this.chbCoil.TabIndex = 17;
            this.chbCoil.Text = "Coilmulde";
            this.chbCoil.UseVisualStyleBackColor = true;
            // 
            // chbPlane
            // 
            this.chbPlane.AutoSize = true;
            this.chbPlane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbPlane.Location = new System.Drawing.Point(20, 58);
            this.chbPlane.Name = "chbPlane";
            this.chbPlane.Size = new System.Drawing.Size(50, 17);
            this.chbPlane.TabIndex = 16;
            this.chbPlane.Text = "Plane";
            this.chbPlane.UseVisualStyleBackColor = true;
            this.chbPlane.CheckedChanged += new System.EventHandler(this.chbPlane_CheckedChanged);
            // 
            // chbSattel
            // 
            this.chbSattel.AutoSize = true;
            this.chbSattel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbSattel.Location = new System.Drawing.Point(21, 19);
            this.chbSattel.Name = "chbSattel";
            this.chbSattel.Size = new System.Drawing.Size(90, 17);
            this.chbSattel.TabIndex = 14;
            this.chbSattel.Text = "Sattelauflieger";
            this.chbSattel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(284, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "zul. Gesamtgewicht";
            // 
            // lLeergewicht
            // 
            this.lLeergewicht.AutoSize = true;
            this.lLeergewicht.ForeColor = System.Drawing.Color.DarkBlue;
            this.lLeergewicht.Location = new System.Drawing.Point(318, 247);
            this.lLeergewicht.Name = "lLeergewicht";
            this.lLeergewicht.Size = new System.Drawing.Size(65, 13);
            this.lLeergewicht.TabIndex = 31;
            this.lLeergewicht.Text = "Leergewicht";
            // 
            // gbFahr2
            // 
            this.gbFahr2.Controls.Add(this.cbBesitzer);
            this.gbFahr2.Controls.Add(this.label4);
            this.gbFahr2.Controls.Add(this.gbZM);
            this.gbFahr2.Controls.Add(this.gbAuflieger);
            this.gbFahr2.Controls.Add(this.dtpAbmeldung);
            this.gbFahr2.Controls.Add(this.cbAbmeldung);
            this.gbFahr2.Controls.Add(this.tbLeergewicht);
            this.gbFahr2.Controls.Add(this.tbZlGw);
            this.gbFahr2.Controls.Add(this.lLeergewicht);
            this.gbFahr2.Controls.Add(this.label1);
            this.gbFahr2.Controls.Add(this.dtpSeit);
            this.gbFahr2.Controls.Add(this.dtpBJ);
            this.gbFahr2.Controls.Add(this.dtpSP);
            this.gbFahr2.Controls.Add(this.dtpTuev);
            this.gbFahr2.Controls.Add(this.tbBesonderheit);
            this.gbFahr2.Controls.Add(this.lBesonerheit);
            this.gbFahr2.Controls.Add(this.lTuev);
            this.gbFahr2.Controls.Add(this.lSP);
            this.gbFahr2.Controls.Add(this.lBJ);
            this.gbFahr2.Controls.Add(this.tbLaufleistung);
            this.gbFahr2.Controls.Add(this.lLaufleistung);
            this.gbFahr2.Controls.Add(this.lSeit);
            this.gbFahr2.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbFahr2.Location = new System.Drawing.Point(12, 311);
            this.gbFahr2.Name = "gbFahr2";
            this.gbFahr2.Size = new System.Drawing.Size(550, 437);
            this.gbFahr2.TabIndex = 30;
            this.gbFahr2.TabStop = false;
            this.gbFahr2.Text = "technische Daten";
            // 
            // cbBesitzer
            // 
            this.cbBesitzer.AllowDrop = true;
            this.cbBesitzer.Enabled = false;
            this.cbBesitzer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBesitzer.FormattingEnabled = true;
            this.cbBesitzer.Location = new System.Drawing.Point(389, 305);
            this.cbBesitzer.Name = "cbBesitzer";
            this.cbBesitzer.Size = new System.Drawing.Size(127, 21);
            this.cbBesitzer.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(300, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Besitzer / Firma:";
            // 
            // gbZM
            // 
            this.gbZM.Controls.Add(this.label2);
            this.gbZM.Controls.Add(this.comboAbgas);
            this.gbZM.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbZM.Location = new System.Drawing.Point(17, 20);
            this.gbZM.Name = "gbZM";
            this.gbZM.Size = new System.Drawing.Size(210, 65);
            this.gbZM.TabIndex = 32;
            this.gbZM.TabStop = false;
            this.gbZM.Text = "Info Zugmaschine / Motorwagen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(13, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Abgasnorm";
            // 
            // comboAbgas
            // 
            this.comboAbgas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboAbgas.FormattingEnabled = true;
            this.comboAbgas.Location = new System.Drawing.Point(79, 25);
            this.comboAbgas.Name = "comboAbgas";
            this.comboAbgas.Size = new System.Drawing.Size(121, 21);
            this.comboAbgas.TabIndex = 0;
            // 
            // gbAuflieger
            // 
            this.gbAuflieger.Controls.Add(this.nudAchsen);
            this.gbAuflieger.Controls.Add(this.label3);
            this.gbAuflieger.Controls.Add(this.tbLaengeA);
            this.gbAuflieger.Controls.Add(this.lLaenge);
            this.gbAuflieger.Controls.Add(this.chbSattel);
            this.gbAuflieger.Controls.Add(this.chbCoil);
            this.gbAuflieger.Controls.Add(this.chbPlane);
            this.gbAuflieger.Controls.Add(this.tbInnenhoehe);
            this.gbAuflieger.Controls.Add(this.lInnenh);
            this.gbAuflieger.Controls.Add(this.lStellplaetze);
            this.gbAuflieger.Controls.Add(this.tbStellplaetze);
            this.gbAuflieger.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbAuflieger.Location = new System.Drawing.Point(238, 20);
            this.gbAuflieger.Name = "gbAuflieger";
            this.gbAuflieger.Size = new System.Drawing.Size(277, 178);
            this.gbAuflieger.TabIndex = 13;
            this.gbAuflieger.TabStop = false;
            this.gbAuflieger.Text = "Art / Aufbau";
            // 
            // nudAchsen
            // 
            this.nudAchsen.Location = new System.Drawing.Point(225, 150);
            this.nudAchsen.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudAchsen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAchsen.Name = "nudAchsen";
            this.nudAchsen.Size = new System.Drawing.Size(45, 20);
            this.nudAchsen.TabIndex = 32;
            this.nudAchsen.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(80, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Achsenanzahl";
            // 
            // tbLaengeA
            // 
            this.tbLaengeA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLaengeA.Location = new System.Drawing.Point(162, 37);
            this.tbLaengeA.Name = "tbLaengeA";
            this.tbLaengeA.Size = new System.Drawing.Size(108, 20);
            this.tbLaengeA.TabIndex = 15;
            // 
            // lLaenge
            // 
            this.lLaenge.AutoSize = true;
            this.lLaenge.Location = new System.Drawing.Point(91, 39);
            this.lLaenge.Name = "lLaenge";
            this.lLaenge.Size = new System.Drawing.Size(54, 13);
            this.lLaenge.TabIndex = 30;
            this.lLaenge.Text = "Länge [m]";
            // 
            // dtpAbmeldung
            // 
            this.dtpAbmeldung.Enabled = false;
            this.dtpAbmeldung.Location = new System.Drawing.Point(23, 289);
            this.dtpAbmeldung.Name = "dtpAbmeldung";
            this.dtpAbmeldung.Size = new System.Drawing.Size(199, 20);
            this.dtpAbmeldung.TabIndex = 12;
            // 
            // cbAbmeldung
            // 
            this.cbAbmeldung.AutoSize = true;
            this.cbAbmeldung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAbmeldung.ForeColor = System.Drawing.Color.DarkBlue;
            this.cbAbmeldung.Location = new System.Drawing.Point(22, 267);
            this.cbAbmeldung.Name = "cbAbmeldung";
            this.cbAbmeldung.Size = new System.Drawing.Size(125, 17);
            this.cbAbmeldung.TabIndex = 11;
            this.cbAbmeldung.Text = "Fahrzeug abgemeldet";
            this.cbAbmeldung.UseVisualStyleBackColor = true;
            this.cbAbmeldung.CheckedChanged += new System.EventHandler(this.cbAbmeldung_CheckedChanged);
            // 
            // tbLeergewicht
            // 
            this.tbLeergewicht.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLeergewicht.Location = new System.Drawing.Point(401, 245);
            this.tbLeergewicht.Name = "tbLeergewicht";
            this.tbLeergewicht.Size = new System.Drawing.Size(108, 20);
            this.tbLeergewicht.TabIndex = 21;
            // 
            // tbZlGw
            // 
            this.tbZlGw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbZlGw.Location = new System.Drawing.Point(401, 221);
            this.tbZlGw.Name = "tbZlGw";
            this.tbZlGw.Size = new System.Drawing.Size(108, 20);
            this.tbZlGw.TabIndex = 20;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSavePrint,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(575, 25);
            this.afToolStrip1.TabIndex = 126;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnSavePrint
            // 
            this.tsbtnSavePrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSavePrint.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnSavePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSavePrint.Name = "tsbtnSavePrint";
            this.tsbtnSavePrint.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSavePrint.Text = "Daten speichern";
            this.tsbtnSavePrint.Click += new System.EventHandler(this.tsbtnSavePrint_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(23, 22);
            this.tsbClose.Text = "Fenster schliessen";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // frmFahrzeuge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(575, 760);
            this.Controls.Add(this.afToolStrip1);
            this.Controls.Add(this.gbFahr2);
            this.Controls.Add(this.gbFahrDaten);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmFahrzeuge";
            this.Text = "Eingabe Fahrzeugdaten";
            this.Load += new System.EventHandler(this.frmFahrzeuge_Load);
            this.gbFahrDaten.ResumeLayout(false);
            this.gbFahrDaten.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbINr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTruck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTrailer)).EndInit();
            this.gbFahr2.ResumeLayout(false);
            this.gbFahr2.PerformLayout();
            this.gbZM.ResumeLayout(false);
            this.gbZM.PerformLayout();
            this.gbAuflieger.ResumeLayout(false);
            this.gbAuflieger.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAchsen)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFahrDaten;
        private System.Windows.Forms.TextBox tbBezeichnung;
        private System.Windows.Forms.Label lFabrikat;
        private System.Windows.Forms.Label lBeizeichnung;
        private System.Windows.Forms.Label lFGNr;
        private System.Windows.Forms.TextBox tbFabrikat;
        private System.Windows.Forms.TextBox tbFGNr;
        private System.Windows.Forms.TextBox tbKFZ;
        private System.Windows.Forms.Label lKFZ;
        private System.Windows.Forms.CheckBox chbTrailer;
        private System.Windows.Forms.CheckBox chbZM;
        private System.Windows.Forms.Label lSeit;
        private System.Windows.Forms.Label lStellplaetze;
        private System.Windows.Forms.Label lLaufleistung;
        private System.Windows.Forms.TextBox tbLaufleistung;
        private System.Windows.Forms.TextBox tbStellplaetze;
        private System.Windows.Forms.Label lBJ;
        private System.Windows.Forms.Label lSP;
        private System.Windows.Forms.Label lTuev;
        private System.Windows.Forms.Label lBesonerheit;
        private System.Windows.Forms.TextBox tbBesonderheit;
        private System.Windows.Forms.Label lInnenh;
        private System.Windows.Forms.TextBox tbInnenhoehe;
        private System.Windows.Forms.DateTimePicker dtpTuev;
        private System.Windows.Forms.DateTimePicker dtpSP;
        private System.Windows.Forms.DateTimePicker dtpBJ;
        private System.Windows.Forms.DateTimePicker dtpSeit;
        private System.Windows.Forms.CheckBox chbCoil;
        private System.Windows.Forms.CheckBox chbPlane;
        private System.Windows.Forms.CheckBox chbSattel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lLeergewicht;
        private System.Windows.Forms.GroupBox gbFahr2;
        private System.Windows.Forms.TextBox tbLeergewicht;
        private System.Windows.Forms.TextBox tbZlGw;
        private System.Windows.Forms.DateTimePicker dtpAbmeldung;
        private System.Windows.Forms.CheckBox cbAbmeldung;
        private System.Windows.Forms.PictureBox pbTruck;
        private System.Windows.Forms.PictureBox pbTrailer;
        private System.Windows.Forms.GroupBox gbAuflieger;
        private System.Windows.Forms.TextBox tbLaengeA;
        private System.Windows.Forms.Label lLaenge;
        private System.Windows.Forms.TextBox tbKIntern;
        private System.Windows.Forms.GroupBox gbZM;
        private System.Windows.Forms.ComboBox comboAbgas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudAchsen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lInternIDZM;
        private System.Windows.Forms.Button btnInterneNr;
        private System.Windows.Forms.PictureBox pbINr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbBesitzer;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnSavePrint;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lInternIDAuflieger;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnIntIDSelect;
    }
}
