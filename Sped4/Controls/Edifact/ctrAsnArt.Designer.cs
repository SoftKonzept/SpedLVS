namespace Sped4.Controls.Edifact
{
    partial class ctrAsnArt
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrAsnArt));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.panAdd = new System.Windows.Forms.Panel();
            this.tbTyp = new System.Windows.Forms.TextBox();
            this.tbBezeichnung = new System.Windows.Forms.TextBox();
            this.tbBeschreibung = new System.Windows.Forms.TextBox();
            this.tbId = new System.Windows.Forms.TextBox();
            this.lTyp = new System.Windows.Forms.Label();
            this.lDescription = new System.Windows.Forms.Label();
            this.lBeschreibung = new System.Windows.Forms.Label();
            this.lId = new System.Windows.Forms.Label();
            this.menuAdd = new Sped4.Controls.AFToolStrip();
            this.tsbtnNewAsnArt = new System.Windows.Forms.ToolStripButton();
            this.tsbtnEdifactSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnEdifactDelete = new System.Windows.Forms.ToolStripButton();
            this.menuASNMain = new Sped4.Controls.AFToolStrip();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.scAsnArtEditUpdate = new Telerik.WinControls.UI.RadSplitContainer();
            this.panEdit = new Telerik.WinControls.UI.SplitPanel();
            this.panUpdate = new Telerik.WinControls.UI.SplitPanel();
            this.btnAsnArtUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.panAdd.SuspendLayout();
            this.menuAdd.SuspendLayout();
            this.menuASNMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scAsnArtEditUpdate)).BeginInit();
            this.scAsnArtEditUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panEdit)).BeginInit();
            this.panEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panUpdate)).BeginInit();
            this.panUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 25);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.dgv);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scAsnArtEditUpdate);
            this.scMain.Size = new System.Drawing.Size(778, 429);
            this.scMain.SplitterDistance = 405;
            this.scMain.TabIndex = 11;
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgv.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgv.MasterTemplate.EnableFiltering = true;
            this.dgv.MasterTemplate.ShowFilteringRow = false;
            this.dgv.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.dgv.ShowHeaderCellButtons = true;
            this.dgv.Size = new System.Drawing.Size(405, 429);
            this.dgv.TabIndex = 28;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            this.dgv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDoubleClick);
            // 
            // panAdd
            // 
            this.panAdd.BackColor = System.Drawing.Color.White;
            this.panAdd.Controls.Add(this.tbTyp);
            this.panAdd.Controls.Add(this.tbBezeichnung);
            this.panAdd.Controls.Add(this.tbBeschreibung);
            this.panAdd.Controls.Add(this.tbId);
            this.panAdd.Controls.Add(this.lTyp);
            this.panAdd.Controls.Add(this.lDescription);
            this.panAdd.Controls.Add(this.lBeschreibung);
            this.panAdd.Controls.Add(this.lId);
            this.panAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panAdd.Location = new System.Drawing.Point(0, 31);
            this.panAdd.Name = "panAdd";
            this.panAdd.Size = new System.Drawing.Size(369, 339);
            this.panAdd.TabIndex = 85;
            // 
            // tbTyp
            // 
            this.tbTyp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTyp.Location = new System.Drawing.Point(152, 52);
            this.tbTyp.Name = "tbTyp";
            this.tbTyp.Size = new System.Drawing.Size(178, 20);
            this.tbTyp.TabIndex = 205;
            // 
            // tbBezeichnung
            // 
            this.tbBezeichnung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBezeichnung.Location = new System.Drawing.Point(152, 81);
            this.tbBezeichnung.Name = "tbBezeichnung";
            this.tbBezeichnung.Size = new System.Drawing.Size(178, 20);
            this.tbBezeichnung.TabIndex = 204;
            // 
            // tbBeschreibung
            // 
            this.tbBeschreibung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBeschreibung.Location = new System.Drawing.Point(152, 112);
            this.tbBeschreibung.Multiline = true;
            this.tbBeschreibung.Name = "tbBeschreibung";
            this.tbBeschreibung.Size = new System.Drawing.Size(178, 171);
            this.tbBeschreibung.TabIndex = 203;
            // 
            // tbId
            // 
            this.tbId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbId.Enabled = false;
            this.tbId.Location = new System.Drawing.Point(152, 27);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(178, 20);
            this.tbId.TabIndex = 202;
            // 
            // lTyp
            // 
            this.lTyp.AutoSize = true;
            this.lTyp.ForeColor = System.Drawing.Color.DarkBlue;
            this.lTyp.Location = new System.Drawing.Point(32, 54);
            this.lTyp.Name = "lTyp";
            this.lTyp.Size = new System.Drawing.Size(24, 13);
            this.lTyp.TabIndex = 201;
            this.lTyp.Text = "Typ";
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.lDescription.ForeColor = System.Drawing.Color.DarkBlue;
            this.lDescription.Location = new System.Drawing.Point(32, 83);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(76, 13);
            this.lDescription.TabIndex = 200;
            this.lDescription.Text = "Bezeichnung:";
            // 
            // lBeschreibung
            // 
            this.lBeschreibung.AutoSize = true;
            this.lBeschreibung.ForeColor = System.Drawing.Color.DarkBlue;
            this.lBeschreibung.Location = new System.Drawing.Point(32, 114);
            this.lBeschreibung.Name = "lBeschreibung";
            this.lBeschreibung.Size = new System.Drawing.Size(80, 13);
            this.lBeschreibung.TabIndex = 199;
            this.lBeschreibung.Text = "Beschreibung:";
            // 
            // lId
            // 
            this.lId.AutoSize = true;
            this.lId.ForeColor = System.Drawing.Color.DarkBlue;
            this.lId.Location = new System.Drawing.Point(32, 29);
            this.lId.Name = "lId";
            this.lId.Size = new System.Drawing.Size(20, 13);
            this.lId.TabIndex = 196;
            this.lId.Text = "Id:";
            // 
            // menuAdd
            // 
            this.menuAdd.AutoSize = false;
            this.menuAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNewAsnArt,
            this.tsbtnEdifactSave,
            this.tsbtnEdifactDelete});
            this.menuAdd.Location = new System.Drawing.Point(0, 0);
            this.menuAdd.myColorFrom = System.Drawing.Color.Azure;
            this.menuAdd.myColorTo = System.Drawing.Color.Blue;
            this.menuAdd.myUnderlineColor = System.Drawing.Color.White;
            this.menuAdd.myUnderlined = true;
            this.menuAdd.Name = "menuAdd";
            this.menuAdd.Size = new System.Drawing.Size(369, 31);
            this.menuAdd.TabIndex = 84;
            this.menuAdd.Text = "afToolStrip1";
            // 
            // tsbtnNewAsnArt
            // 
            this.tsbtnNewAsnArt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNewAsnArt.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNewAsnArt.Image")));
            this.tsbtnNewAsnArt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNewAsnArt.Name = "tsbtnNewAsnArt";
            this.tsbtnNewAsnArt.Size = new System.Drawing.Size(23, 28);
            this.tsbtnNewAsnArt.Text = "Neuer AsnArt Eintrag";
            this.tsbtnNewAsnArt.Click += new System.EventHandler(this.tsbtnNewAsnArt_Click);
            // 
            // tsbtnEdifactSave
            // 
            this.tsbtnEdifactSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnEdifactSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnEdifactSave.Image")));
            this.tsbtnEdifactSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEdifactSave.Name = "tsbtnEdifactSave";
            this.tsbtnEdifactSave.Size = new System.Drawing.Size(23, 28);
            this.tsbtnEdifactSave.Text = "Verweis speichern";
            this.tsbtnEdifactSave.Click += new System.EventHandler(this.tsbtnEdifactSave_Click);
            // 
            // tsbtnEdifactDelete
            // 
            this.tsbtnEdifactDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnEdifactDelete.Image = global::Sped4.Properties.Resources.garbage_delete;
            this.tsbtnEdifactDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEdifactDelete.Name = "tsbtnEdifactDelete";
            this.tsbtnEdifactDelete.Size = new System.Drawing.Size(23, 28);
            this.tsbtnEdifactDelete.Text = "schliessen";
            // 
            // menuASNMain
            // 
            this.menuASNMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefresh});
            this.menuASNMain.Location = new System.Drawing.Point(0, 0);
            this.menuASNMain.myColorFrom = System.Drawing.Color.Azure;
            this.menuASNMain.myColorTo = System.Drawing.Color.Blue;
            this.menuASNMain.myUnderlineColor = System.Drawing.Color.White;
            this.menuASNMain.myUnderlined = true;
            this.menuASNMain.Name = "menuASNMain";
            this.menuASNMain.Size = new System.Drawing.Size(778, 25);
            this.menuASNMain.TabIndex = 10;
            this.menuASNMain.Text = "afToolStrip1";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefresh.Image")));
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefresh.Text = "aktualisieren";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // scAsnArtEditUpdate
            // 
            this.scAsnArtEditUpdate.BackColor = System.Drawing.Color.White;
            this.scAsnArtEditUpdate.Controls.Add(this.panEdit);
            this.scAsnArtEditUpdate.Controls.Add(this.panUpdate);
            this.scAsnArtEditUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scAsnArtEditUpdate.Location = new System.Drawing.Point(0, 0);
            this.scAsnArtEditUpdate.Name = "scAsnArtEditUpdate";
            this.scAsnArtEditUpdate.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.scAsnArtEditUpdate.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scAsnArtEditUpdate.Size = new System.Drawing.Size(369, 429);
            this.scAsnArtEditUpdate.TabIndex = 86;
            this.scAsnArtEditUpdate.TabStop = false;
            // 
            // panEdit
            // 
            this.panEdit.Controls.Add(this.panAdd);
            this.panEdit.Controls.Add(this.menuAdd);
            this.panEdit.Location = new System.Drawing.Point(0, 0);
            this.panEdit.Name = "panEdit";
            // 
            // 
            // 
            this.panEdit.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panEdit.Size = new System.Drawing.Size(369, 370);
            this.panEdit.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.3705882F);
            this.panEdit.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 127);
            this.panEdit.TabIndex = 0;
            this.panEdit.TabStop = false;
            this.panEdit.Text = "Edit";
            this.panEdit.Click += new System.EventHandler(this.splitPanel1_Click);
            // 
            // panUpdate
            // 
            this.panUpdate.Controls.Add(this.btnAsnArtUpdate);
            this.panUpdate.Location = new System.Drawing.Point(0, 374);
            this.panUpdate.Name = "panUpdate";
            // 
            // 
            // 
            this.panUpdate.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.panUpdate.Size = new System.Drawing.Size(369, 55);
            this.panUpdate.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.3705882F);
            this.panUpdate.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -127);
            this.panUpdate.TabIndex = 1;
            this.panUpdate.TabStop = false;
            this.panUpdate.Text = "Update";
            // 
            // btnAsnArtUpdate
            // 
            this.btnAsnArtUpdate.Location = new System.Drawing.Point(69, 13);
            this.btnAsnArtUpdate.Name = "btnAsnArtUpdate";
            this.btnAsnArtUpdate.Size = new System.Drawing.Size(212, 33);
            this.btnAsnArtUpdate.TabIndex = 0;
            this.btnAsnArtUpdate.Text = "AsnArt Tabelle Update";
            this.btnAsnArtUpdate.UseVisualStyleBackColor = true;
            this.btnAsnArtUpdate.Click += new System.EventHandler(this.btnAsnArtUpdate_Click);
            // 
            // ctrAsnArt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.menuASNMain);
            this.Name = "ctrAsnArt";
            this.Size = new System.Drawing.Size(778, 454);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panAdd.ResumeLayout(false);
            this.panAdd.PerformLayout();
            this.menuAdd.ResumeLayout(false);
            this.menuAdd.PerformLayout();
            this.menuASNMain.ResumeLayout(false);
            this.menuASNMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scAsnArtEditUpdate)).EndInit();
            this.scAsnArtEditUpdate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panEdit)).EndInit();
            this.panEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panUpdate)).EndInit();
            this.panUpdate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AFToolStrip menuASNMain;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.SplitContainer scMain;
        private Telerik.WinControls.UI.RadGridView dgv;
        private AFToolStrip menuAdd;
        private System.Windows.Forms.ToolStripButton tsbtnNewAsnArt;
        private System.Windows.Forms.ToolStripButton tsbtnEdifactSave;
        private System.Windows.Forms.ToolStripButton tsbtnEdifactDelete;
        private System.Windows.Forms.Panel panAdd;
        private System.Windows.Forms.Label lTyp;
        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.Label lBeschreibung;
        private System.Windows.Forms.Label lId;
        private System.Windows.Forms.TextBox tbTyp;
        private System.Windows.Forms.TextBox tbBezeichnung;
        private System.Windows.Forms.TextBox tbBeschreibung;
        private System.Windows.Forms.TextBox tbId;
        private Telerik.WinControls.UI.RadSplitContainer scAsnArtEditUpdate;
        private Telerik.WinControls.UI.SplitPanel panEdit;
        private Telerik.WinControls.UI.SplitPanel panUpdate;
        private System.Windows.Forms.Button btnAsnArtUpdate;
    }
}
