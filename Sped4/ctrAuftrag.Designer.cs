namespace Sped4
{
    partial class ctrAufträge
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrAufträge));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.miUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.miDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.mitSplitting = new System.Windows.Forms.ToolStripMenuItem();
            this.miAPAuf = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddImage = new System.Windows.Forms.ToolStripMenuItem();
            this.miToKommiInPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStatusListe = new System.Windows.Forms.ToolStripMenuItem();
            this.miStatChange = new System.Windows.Forms.ToolStripMenuItem();
            this.miFrachtvergabe = new System.Windows.Forms.ToolStripMenuItem();
            this.miDocs = new System.Windows.Forms.ToolStripMenuItem();
            this.miCloseCtr = new System.Windows.Forms.ToolStripMenuItem();
            this.workerLoadOrderList = new System.ComponentModel.BackgroundWorker();
            this.afMinMaxPanel1 = new Sped4.Controls.AFMinMaxPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbNeu = new System.Windows.Forms.ToolStripButton();
            this.tsbChange = new System.Windows.Forms.ToolStripButton();
            this.tsbDetails = new System.Windows.Forms.ToolStripButton();
            this.tsbSplitt = new System.Windows.Forms.ToolStripButton();
            this.tsbtnStatChange = new System.Windows.Forms.ToolStripButton();
            this.tsbScan = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSub = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbListen = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmaktWeek = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.miDispoOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.miDoneOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.miVergabe = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStornoAuftrZeitraum = new System.Windows.Forms.ToolStripMenuItem();
            this.miAllOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnLegende = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAnpassen = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPLZ_ELS = new System.Windows.Forms.TextBox();
            this.tbPLZ_BLS = new System.Windows.Forms.TextBox();
            this.cbEntlPLZ = new System.Windows.Forms.CheckBox();
            this.cbBelPLZ = new System.Windows.Forms.CheckBox();
            this.pbFilter = new System.Windows.Forms.PictureBox();
            this.tbSearchAuftrag = new System.Windows.Forms.TextBox();
            this.cbAuftrag = new System.Windows.Forms.CheckBox();
            this.ckbRelation = new System.Windows.Forms.CheckBox();
            this.cbRelation = new System.Windows.Forms.ComboBox();
            this.tbSearchA = new System.Windows.Forms.TextBox();
            this.cbSearchAuftraggeber = new System.Windows.Forms.CheckBox();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.pbChangeFontSize = new System.Windows.Forms.PictureBox();
            this.label = new System.Windows.Forms.Label();
            this.cbSkin = new System.Windows.Forms.ComboBox();
            this.Schriftgröße = new System.Windows.Forms.Label();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.contextMenuStrip1.SuspendLayout();
            this.afMinMaxPanel1.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilter)).BeginInit();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbChangeFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAdd,
            this.miUpdate,
            this.miDetails,
            this.mitSplitting,
            this.miAPAuf,
            this.miDelete,
            this.miAddImage,
            this.miToKommiInPlan,
            this.tsmiStatusListe,
            this.miStatChange,
            this.miFrachtvergabe,
            this.miDocs,
            this.miCloseCtr});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(306, 316);
            // 
            // miAdd
            // 
            this.miAdd.Name = "miAdd";
            this.miAdd.Size = new System.Drawing.Size(305, 24);
            this.miAdd.Text = "Neuen Auftrag anlegen";
            this.miAdd.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // miUpdate
            // 
            this.miUpdate.Name = "miUpdate";
            this.miUpdate.Size = new System.Drawing.Size(305, 24);
            this.miUpdate.Text = "Ändern von Auftragsdaten";
            this.miUpdate.Click += new System.EventHandler(this.miUpdate_Click);
            // 
            // miDetails
            // 
            this.miDetails.Name = "miDetails";
            this.miDetails.Size = new System.Drawing.Size(305, 24);
            this.miDetails.Text = "Details zum Auftrag";
            this.miDetails.Click += new System.EventHandler(this.miDetails_Click);
            // 
            // mitSplitting
            // 
            this.mitSplitting.Name = "mitSplitting";
            this.mitSplitting.Size = new System.Drawing.Size(305, 24);
            this.mitSplitting.Text = "Teilen des Auftrags";
            this.mitSplitting.Click += new System.EventHandler(this.mitSplitting_Click);
            // 
            // miAPAuf
            // 
            this.miAPAuf.Name = "miAPAuf";
            this.miAPAuf.Size = new System.Drawing.Size(305, 24);
            this.miAPAuf.Text = "Auftragposition auflösen";
            this.miAPAuf.Click += new System.EventHandler(this.miAPAuf_Click);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(305, 24);
            this.miDelete.Text = "Auftrag stornieren";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // miAddImage
            // 
            this.miAddImage.Name = "miAddImage";
            this.miAddImage.Size = new System.Drawing.Size(305, 24);
            this.miAddImage.Text = "Auftrag hinterlegen";
            this.miAddImage.Click += new System.EventHandler(this.miAddImage_Click);
            // 
            // miToKommiInPlan
            // 
            this.miToKommiInPlan.Name = "miToKommiInPlan";
            this.miToKommiInPlan.Size = new System.Drawing.Size(305, 24);
            this.miToKommiInPlan.Text = "zum Auftrag";
            this.miToKommiInPlan.Click += new System.EventHandler(this.miToKommiInPlan_Click);
            // 
            // tsmiStatusListe
            // 
            this.tsmiStatusListe.Name = "tsmiStatusListe";
            this.tsmiStatusListe.Size = new System.Drawing.Size(305, 24);
            this.tsmiStatusListe.Text = "Statusübersicht Auftrag";
            this.tsmiStatusListe.Click += new System.EventHandler(this.tsmiStatusListe_Click);
            // 
            // miStatChange
            // 
            this.miStatChange.Name = "miStatChange";
            this.miStatChange.Size = new System.Drawing.Size(305, 24);
            this.miStatChange.Text = "Status ändern";
            this.miStatChange.Click += new System.EventHandler(this.miStatChange_Click);
            // 
            // miFrachtvergabe
            // 
            this.miFrachtvergabe.Name = "miFrachtvergabe";
            this.miFrachtvergabe.Size = new System.Drawing.Size(305, 24);
            this.miFrachtvergabe.Text = "Frachtvergabe an Subunternehmer";
            this.miFrachtvergabe.Click += new System.EventHandler(this.miFrachtvergabe_Click);
            // 
            // miDocs
            // 
            this.miDocs.Name = "miDocs";
            this.miDocs.Size = new System.Drawing.Size(305, 24);
            this.miDocs.Text = "Dokumente erstellen";
            this.miDocs.Click += new System.EventHandler(this.miDocs_Click);
            // 
            // miCloseCtr
            // 
            this.miCloseCtr.Name = "miCloseCtr";
            this.miCloseCtr.Size = new System.Drawing.Size(305, 24);
            this.miCloseCtr.Text = "Liste schliessen";
            this.miCloseCtr.Click += new System.EventHandler(this.miCloseCtr_Click);
            // 
            // workerLoadOrderList
            // 
            this.workerLoadOrderList.WorkerReportsProgress = true;
            this.workerLoadOrderList.WorkerSupportsCancellation = true;
            // 
            // afMinMaxPanel1
            // 
            this.afMinMaxPanel1.Controls.Add(this.textBox1);
            this.afMinMaxPanel1.Controls.Add(this.afToolStrip1);
            this.afMinMaxPanel1.Controls.Add(this.groupBox1);
            this.afMinMaxPanel1.Controls.Add(this.gbOptions);
            this.afMinMaxPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afMinMaxPanel1.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.afMinMaxPanel1.Location = new System.Drawing.Point(0, 34);
            this.afMinMaxPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.afMinMaxPanel1.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.afMinMaxPanel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.afMinMaxPanel1.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.afMinMaxPanel1.myText = "Optionen";
            this.afMinMaxPanel1.Name = "afMinMaxPanel1";
            this.afMinMaxPanel1.Size = new System.Drawing.Size(1079, 241);
            this.afMinMaxPanel1.TabIndex = 0;
            this.afMinMaxPanel1.Text = "afMinMaxPanel1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(544, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 12;
            this.textBox1.Visible = false;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNeu,
            this.tsbChange,
            this.tsbDetails,
            this.tsbSplitt,
            this.tsbtnStatChange,
            this.tsbScan,
            this.tsbtnSub,
            this.tsbDelete,
            this.tsbRefresh,
            this.tsbListen,
            this.tsbtnLegende,
            this.tsbtnAnpassen,
            this.tsbClose});
            this.afToolStrip1.Location = new System.Drawing.Point(4, 31);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(371, 27);
            this.afToolStrip1.TabIndex = 7;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbNeu
            // 
            this.tsbNeu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNeu.Image = global::Sped4.Properties.Resources.form_green_add;
            this.tsbNeu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNeu.Name = "tsbNeu";
            this.tsbNeu.Size = new System.Drawing.Size(29, 24);
            this.tsbNeu.Text = "Neuer Auftrag";
            this.tsbNeu.Click += new System.EventHandler(this.miAdd_Click);
            // 
            // tsbChange
            // 
            this.tsbChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChange.Image = global::Sped4.Properties.Resources.form_green_edit;
            this.tsbChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChange.Name = "tsbChange";
            this.tsbChange.Size = new System.Drawing.Size(29, 24);
            this.tsbChange.Text = "Auftrag ändern";
            this.tsbChange.Click += new System.EventHandler(this.miUpdate_Click);
            // 
            // tsbDetails
            // 
            this.tsbDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDetails.Image = global::Sped4.Properties.Resources.form_green;
            this.tsbDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDetails.Name = "tsbDetails";
            this.tsbDetails.Size = new System.Drawing.Size(29, 24);
            this.tsbDetails.Text = "Auftrag anzeigen";
            this.tsbDetails.Visible = false;
            this.tsbDetails.Click += new System.EventHandler(this.miDetails_Click);
            // 
            // tsbSplitt
            // 
            this.tsbSplitt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSplitt.Image = global::Sped4.Properties.Resources.cubes;
            this.tsbSplitt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSplitt.Name = "tsbSplitt";
            this.tsbSplitt.Size = new System.Drawing.Size(29, 24);
            this.tsbSplitt.Text = "Auftrag teilen";
            this.tsbSplitt.Click += new System.EventHandler(this.mitSplitting_Click);
            // 
            // tsbtnStatChange
            // 
            this.tsbtnStatChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnStatChange.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnStatChange.Image")));
            this.tsbtnStatChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStatChange.Name = "tsbtnStatChange";
            this.tsbtnStatChange.Size = new System.Drawing.Size(29, 24);
            this.tsbtnStatChange.Text = "Status ändern";
            this.tsbtnStatChange.Click += new System.EventHandler(this.tsbtnStatChange_Click);
            // 
            // tsbScan
            // 
            this.tsbScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScan.Image = ((System.Drawing.Image)(resources.GetObject("tsbScan.Image")));
            this.tsbScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScan.Name = "tsbScan";
            this.tsbScan.Size = new System.Drawing.Size(29, 24);
            this.tsbScan.Text = "Auftrag hinterlegen";
            this.tsbScan.Click += new System.EventHandler(this.miAddImage_Click);
            // 
            // tsbtnSub
            // 
            this.tsbtnSub.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSub.Enabled = false;
            this.tsbtnSub.Image = global::Sped4.Properties.Resources.truck_green16;
            this.tsbtnSub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSub.Name = "tsbtnSub";
            this.tsbtnSub.Size = new System.Drawing.Size(29, 24);
            this.tsbtnSub.Text = "Auftragsliste Subunternehmer";
            this.tsbtnSub.Click += new System.EventHandler(this.tsbtnSub_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(29, 24);
            this.tsbDelete.Text = "Auftrag stornieren";
            this.tsbDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::Sped4.Properties.Resources.refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(29, 24);
            this.tsbRefresh.Text = "aktualisieren";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbListen
            // 
            this.tsbListen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbListen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmaktWeek,
            this.miOpenOrder,
            this.miDispoOrder,
            this.miDoneOrder,
            this.miVergabe,
            this.tsmiStornoAuftrZeitraum,
            this.miAllOrder});
            this.tsbListen.Image = ((System.Drawing.Image)(resources.GetObject("tsbListen.Image")));
            this.tsbListen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbListen.Name = "tsbListen";
            this.tsbListen.Size = new System.Drawing.Size(39, 24);
            this.tsbListen.Text = "Auftragslisten";
            // 
            // tsmaktWeek
            // 
            this.tsmaktWeek.Name = "tsmaktWeek";
            this.tsmaktWeek.Size = new System.Drawing.Size(346, 26);
            this.tsmaktWeek.Text = "aktuelle Übersicht";
            this.tsmaktWeek.Click += new System.EventHandler(this.tsmaktWeek_Click);
            // 
            // miOpenOrder
            // 
            this.miOpenOrder.Name = "miOpenOrder";
            this.miOpenOrder.Size = new System.Drawing.Size(346, 26);
            this.miOpenOrder.Text = "offene Aufträge nach Zeitraum";
            this.miOpenOrder.Click += new System.EventHandler(this.miOpenOrder_Click);
            // 
            // miDispoOrder
            // 
            this.miDispoOrder.Name = "miDispoOrder";
            this.miDispoOrder.Size = new System.Drawing.Size(346, 26);
            this.miDispoOrder.Text = "disponierte Aufträge nach Zeitraum";
            this.miDispoOrder.Click += new System.EventHandler(this.miDispoOrder_Click);
            // 
            // miDoneOrder
            // 
            this.miDoneOrder.Name = "miDoneOrder";
            this.miDoneOrder.Size = new System.Drawing.Size(346, 26);
            this.miDoneOrder.Text = "durchgeführte Aufträge nach Zeitraum";
            this.miDoneOrder.Click += new System.EventHandler(this.miDoneOrder_Click);
            // 
            // miVergabe
            // 
            this.miVergabe.Name = "miVergabe";
            this.miVergabe.Size = new System.Drawing.Size(346, 26);
            this.miVergabe.Text = "Aufträge an Subunternehmer";
            this.miVergabe.Visible = false;
            this.miVergabe.Click += new System.EventHandler(this.miVergabe_Click);
            // 
            // tsmiStornoAuftrZeitraum
            // 
            this.tsmiStornoAuftrZeitraum.Name = "tsmiStornoAuftrZeitraum";
            this.tsmiStornoAuftrZeitraum.Size = new System.Drawing.Size(346, 26);
            this.tsmiStornoAuftrZeitraum.Text = "stornierte Aufträge nach Zeitraum";
            this.tsmiStornoAuftrZeitraum.Click += new System.EventHandler(this.tsmiStronoAuftrZeitraum_Click);
            // 
            // miAllOrder
            // 
            this.miAllOrder.Name = "miAllOrder";
            this.miAllOrder.Size = new System.Drawing.Size(346, 26);
            this.miAllOrder.Text = "alle Aufträge";
            this.miAllOrder.Click += new System.EventHandler(this.miAllOrder_Click);
            // 
            // tsbtnLegende
            // 
            this.tsbtnLegende.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnLegende.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnLegende.Image")));
            this.tsbtnLegende.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLegende.Name = "tsbtnLegende";
            this.tsbtnLegende.Size = new System.Drawing.Size(29, 24);
            this.tsbtnLegende.Text = "Statuslegende anzeigen";
            this.tsbtnLegende.Click += new System.EventHandler(this.tsbtnLegende_Click);
            // 
            // tsbtnAnpassen
            // 
            this.tsbtnAnpassen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAnpassen.Image = global::Sped4.Properties.Resources.PrintSetupHS;
            this.tsbtnAnpassen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAnpassen.Name = "tsbtnAnpassen";
            this.tsbtnAnpassen.Size = new System.Drawing.Size(29, 24);
            this.tsbtnAnpassen.Text = "Fensterbreite optimieren";
            this.tsbtnAnpassen.Click += new System.EventHandler(this.tsbtnAnpassen_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(29, 24);
            this.tsbClose.Text = "schliessen";
            this.tsbClose.Click += new System.EventHandler(this.miCloseCtr_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbPLZ_ELS);
            this.groupBox1.Controls.Add(this.tbPLZ_BLS);
            this.groupBox1.Controls.Add(this.cbEntlPLZ);
            this.groupBox1.Controls.Add(this.cbBelPLZ);
            this.groupBox1.Controls.Add(this.pbFilter);
            this.groupBox1.Controls.Add(this.tbSearchAuftrag);
            this.groupBox1.Controls.Add(this.cbAuftrag);
            this.groupBox1.Controls.Add(this.ckbRelation);
            this.groupBox1.Controls.Add(this.cbRelation);
            this.groupBox1.Controls.Add(this.tbSearchA);
            this.groupBox1.Controls.Add(this.cbSearchAuftraggeber);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(4, 65);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(484, 170);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // tbPLZ_ELS
            // 
            this.tbPLZ_ELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPLZ_ELS.Enabled = false;
            this.tbPLZ_ELS.Location = new System.Drawing.Point(171, 122);
            this.tbPLZ_ELS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbPLZ_ELS.Name = "tbPLZ_ELS";
            this.tbPLZ_ELS.Size = new System.Drawing.Size(250, 22);
            this.tbPLZ_ELS.TabIndex = 140;
            // 
            // tbPLZ_BLS
            // 
            this.tbPLZ_BLS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPLZ_BLS.Enabled = false;
            this.tbPLZ_BLS.Location = new System.Drawing.Point(171, 98);
            this.tbPLZ_BLS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbPLZ_BLS.Name = "tbPLZ_BLS";
            this.tbPLZ_BLS.Size = new System.Drawing.Size(250, 22);
            this.tbPLZ_BLS.TabIndex = 139;
            // 
            // cbEntlPLZ
            // 
            this.cbEntlPLZ.AutoSize = true;
            this.cbEntlPLZ.Location = new System.Drawing.Point(11, 123);
            this.cbEntlPLZ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbEntlPLZ.Name = "cbEntlPLZ";
            this.cbEntlPLZ.Size = new System.Drawing.Size(84, 20);
            this.cbEntlPLZ.TabIndex = 138;
            this.cbEntlPLZ.Text = "PLZ ELS:";
            this.cbEntlPLZ.UseVisualStyleBackColor = true;
            this.cbEntlPLZ.CheckedChanged += new System.EventHandler(this.cbEntlPLZ_CheckedChanged);
            // 
            // cbBelPLZ
            // 
            this.cbBelPLZ.AutoSize = true;
            this.cbBelPLZ.Location = new System.Drawing.Point(11, 98);
            this.cbBelPLZ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbBelPLZ.Name = "cbBelPLZ";
            this.cbBelPLZ.Size = new System.Drawing.Size(84, 20);
            this.cbBelPLZ.TabIndex = 137;
            this.cbBelPLZ.Text = "PLZ BLS:";
            this.cbBelPLZ.UseVisualStyleBackColor = true;
            this.cbBelPLZ.CheckedChanged += new System.EventHandler(this.cbBelPLZ_CheckedChanged);
            // 
            // pbFilter
            // 
            this.pbFilter.Image = global::Sped4.Properties.Resources.magnifying_glass;
            this.pbFilter.Location = new System.Drawing.Point(443, 17);
            this.pbFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbFilter.Name = "pbFilter";
            this.pbFilter.Size = new System.Drawing.Size(24, 24);
            this.pbFilter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFilter.TabIndex = 136;
            this.pbFilter.TabStop = false;
            this.pbFilter.Click += new System.EventHandler(this.pbFilter_Click);
            // 
            // tbSearchAuftrag
            // 
            this.tbSearchAuftrag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchAuftrag.Enabled = false;
            this.tbSearchAuftrag.Location = new System.Drawing.Point(171, 75);
            this.tbSearchAuftrag.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSearchAuftrag.Name = "tbSearchAuftrag";
            this.tbSearchAuftrag.Size = new System.Drawing.Size(250, 22);
            this.tbSearchAuftrag.TabIndex = 11;
            // 
            // cbAuftrag
            // 
            this.cbAuftrag.AutoSize = true;
            this.cbAuftrag.Location = new System.Drawing.Point(11, 75);
            this.cbAuftrag.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbAuftrag.Name = "cbAuftrag";
            this.cbAuftrag.Size = new System.Drawing.Size(126, 20);
            this.cbAuftrag.TabIndex = 10;
            this.cbAuftrag.Text = "Auftragsnummer";
            this.cbAuftrag.UseVisualStyleBackColor = true;
            this.cbAuftrag.CheckedChanged += new System.EventHandler(this.cbAuftrag_CheckedChanged);
            // 
            // ckbRelation
            // 
            this.ckbRelation.AutoSize = true;
            this.ckbRelation.Location = new System.Drawing.Point(11, 26);
            this.ckbRelation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbRelation.Name = "ckbRelation";
            this.ckbRelation.Size = new System.Drawing.Size(79, 20);
            this.ckbRelation.TabIndex = 3;
            this.ckbRelation.Text = "Relation";
            this.ckbRelation.UseVisualStyleBackColor = true;
            this.ckbRelation.CheckedChanged += new System.EventHandler(this.ckbRelation_CheckedChanged);
            // 
            // cbRelation
            // 
            this.cbRelation.AllowDrop = true;
            this.cbRelation.Enabled = false;
            this.cbRelation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRelation.FormattingEnabled = true;
            this.cbRelation.Location = new System.Drawing.Point(171, 21);
            this.cbRelation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbRelation.Name = "cbRelation";
            this.cbRelation.Size = new System.Drawing.Size(249, 24);
            this.cbRelation.TabIndex = 4;
            // 
            // tbSearchA
            // 
            this.tbSearchA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchA.Enabled = false;
            this.tbSearchA.Location = new System.Drawing.Point(171, 53);
            this.tbSearchA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbSearchA.Name = "tbSearchA";
            this.tbSearchA.Size = new System.Drawing.Size(250, 22);
            this.tbSearchA.TabIndex = 6;
            // 
            // cbSearchAuftraggeber
            // 
            this.cbSearchAuftraggeber.AutoSize = true;
            this.cbSearchAuftraggeber.Location = new System.Drawing.Point(11, 52);
            this.cbSearchAuftraggeber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSearchAuftraggeber.Name = "cbSearchAuftraggeber";
            this.cbSearchAuftraggeber.Size = new System.Drawing.Size(107, 20);
            this.cbSearchAuftraggeber.TabIndex = 5;
            this.cbSearchAuftraggeber.Text = "Auftraggeber";
            this.cbSearchAuftraggeber.UseVisualStyleBackColor = true;
            this.cbSearchAuftraggeber.CheckedChanged += new System.EventHandler(this.cbSearchAuftraggeber_CheckedChanged);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.pbChangeFontSize);
            this.gbOptions.Controls.Add(this.label);
            this.gbOptions.Controls.Add(this.cbSkin);
            this.gbOptions.Controls.Add(this.Schriftgröße);
            this.gbOptions.Controls.Add(this.nudFontSize);
            this.gbOptions.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbOptions.Location = new System.Drawing.Point(496, 65);
            this.gbOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbOptions.Size = new System.Drawing.Size(181, 169);
            this.gbOptions.TabIndex = 11;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Einstellungen";
            // 
            // pbChangeFontSize
            // 
            this.pbChangeFontSize.Image = global::Sped4.Properties.Resources.PortraitHS;
            this.pbChangeFontSize.Location = new System.Drawing.Point(104, 43);
            this.pbChangeFontSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbChangeFontSize.Name = "pbChangeFontSize";
            this.pbChangeFontSize.Size = new System.Drawing.Size(32, 27);
            this.pbChangeFontSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbChangeFontSize.TabIndex = 137;
            this.pbChangeFontSize.TabStop = false;
            this.pbChangeFontSize.Tag = "Schriftgröße ändern";
            this.pbChangeFontSize.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.ForeColor = System.Drawing.Color.DarkBlue;
            this.label.Location = new System.Drawing.Point(8, 80);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(47, 16);
            this.label.TabIndex = 6;
            this.label.Text = "Layout";
            // 
            // cbSkin
            // 
            this.cbSkin.AllowDrop = true;
            this.cbSkin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSkin.FormattingEnabled = true;
            this.cbSkin.Location = new System.Drawing.Point(8, 108);
            this.cbSkin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSkin.Name = "cbSkin";
            this.cbSkin.Size = new System.Drawing.Size(164, 24);
            this.cbSkin.TabIndex = 5;
            this.cbSkin.SelectedIndexChanged += new System.EventHandler(this.cbSkin_SelectedIndexChanged);
            // 
            // Schriftgröße
            // 
            this.Schriftgröße.AutoSize = true;
            this.Schriftgröße.ForeColor = System.Drawing.Color.DarkBlue;
            this.Schriftgröße.Location = new System.Drawing.Point(11, 26);
            this.Schriftgröße.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Schriftgröße.Name = "Schriftgröße";
            this.Schriftgröße.Size = new System.Drawing.Size(79, 16);
            this.Schriftgröße.TabIndex = 1;
            this.Schriftgröße.Text = "Schriftgröße";
            // 
            // nudFontSize
            // 
            this.nudFontSize.DecimalPlaces = 2;
            this.nudFontSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFontSize.Location = new System.Drawing.Point(13, 47);
            this.nudFontSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudFontSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudFontSize.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(83, 22);
            this.nudFontSize.TabIndex = 0;
            this.nudFontSize.Value = new decimal(new int[] {
            825,
            0,
            0,
            131072});
            this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
            // 
            // afColorLabel1
            // 
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afColorLabel1.Location = new System.Drawing.Point(0, 0);
            this.afColorLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.afColorLabel1.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afColorLabel1.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afColorLabel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afColorLabel1.myText = "";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(1079, 34);
            this.afColorLabel1.TabIndex = 2;
            this.afColorLabel1.Text = "afColorLabel1";
            // 
            // dgv
            // 
            this.dgv.AutoScroll = true;
            this.dgv.AutoSizeRows = true;
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 275);
            this.dgv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgv.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgv.MasterTemplate.EnableFiltering = true;
            this.dgv.MasterTemplate.ShowFilteringRow = false;
            this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgv.ShowGroupPanel = false;
            this.dgv.ShowHeaderCellButtons = true;
            this.dgv.Size = new System.Drawing.Size(1079, 344);
            this.dgv.TabIndex = 26;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.dgv_RowFormatting);
            this.dgv.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgv.RowMouseMove += new Telerik.WinControls.UI.RowMouseMoveEventHandler(this.dgv_RowMouseMove);
            this.dgv.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.ToolTipTextNeeded += new Telerik.WinControls.ToolTipTextNeededEventHandler(this.dgv_ToolTipTextNeeded);
            this.dgv.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_DragDrop);
            this.dgv.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgv_DragEnter);
            this.dgv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDoubleClick);
            this.dgv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDown);
            // 
            // ctrAufträge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.afMinMaxPanel1);
            this.Controls.Add(this.afColorLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrAufträge";
            this.Size = new System.Drawing.Size(1079, 619);
            this.Load += new System.EventHandler(this.ctrAufträge_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.afMinMaxPanel1.ResumeLayout(false);
            this.afMinMaxPanel1.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFilter)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbChangeFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sped4.Controls.AFMinMaxPanel afMinMaxPanel1;
        private Sped4.Controls.AFColorLabel afColorLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miAdd;
        private System.Windows.Forms.ToolStripMenuItem miUpdate;
        private System.Windows.Forms.ToolStripMenuItem miDetails;
        private System.Windows.Forms.ToolStripMenuItem miCloseCtr;
        private System.Windows.Forms.ToolStripMenuItem mitSplitting;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.CheckBox ckbRelation;
        private System.Windows.Forms.ComboBox cbRelation;
        private System.Windows.Forms.CheckBox cbSearchAuftraggeber;
        private System.Windows.Forms.TextBox tbSearchA;
        private Sped4.Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNeu;
        private System.Windows.Forms.ToolStripButton tsbChange;
        private System.Windows.Forms.ToolStripButton tsbDetails;
        private System.Windows.Forms.ToolStripButton tsbSplitt;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripMenuItem miAddImage;
        private System.Windows.Forms.ToolStripButton tsbScan;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripSplitButton tsbListen;
        private System.Windows.Forms.ToolStripMenuItem miAPAuf;
        private System.Windows.Forms.ToolStripMenuItem miOpenOrder;
        private System.Windows.Forms.ToolStripMenuItem miDispoOrder;
        private System.Windows.Forms.ToolStripMenuItem miDoneOrder;
        private System.Windows.Forms.ToolStripMenuItem miToKommiInPlan;
        private System.Windows.Forms.ToolStripMenuItem tsmaktWeek;
        private System.Windows.Forms.ToolStripMenuItem tsmiStatusListe;
        private System.Windows.Forms.TextBox tbSearchAuftrag;
        private System.Windows.Forms.CheckBox cbAuftrag;
        private System.Windows.Forms.ToolStripMenuItem miStatChange;
        private System.Windows.Forms.ToolStripMenuItem miFrachtvergabe;
        private System.Windows.Forms.ToolStripMenuItem miVergabe;
        private System.Windows.Forms.ToolStripMenuItem miDocs;
        private System.Windows.Forms.PictureBox pbFilter;
        private System.Windows.Forms.ToolStripButton tsbtnAnpassen;
        private System.Windows.Forms.ToolStripButton tsbtnLegende;
        private System.Windows.Forms.ToolStripMenuItem tsmiStornoAuftrZeitraum;
        private System.Windows.Forms.ToolStripButton tsbtnSub;
        private System.Windows.Forms.ToolStripButton tsbtnStatChange;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.Label Schriftgröße;
        private System.Windows.Forms.TextBox tbPLZ_ELS;
        private System.Windows.Forms.TextBox tbPLZ_BLS;
        private System.Windows.Forms.CheckBox cbEntlPLZ;
        private System.Windows.Forms.CheckBox cbBelPLZ;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox cbSkin;
        private System.Windows.Forms.PictureBox pbChangeFontSize;
        public System.ComponentModel.BackgroundWorker workerLoadOrderList;
        private System.Windows.Forms.ToolStripMenuItem miAllOrder;
        private System.Windows.Forms.TextBox textBox1;
        public Telerik.WinControls.UI.RadGridView dgv;
    }
}
