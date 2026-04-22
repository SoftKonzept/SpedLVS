namespace Sped4
{
    partial class ctrInventoryAdd
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
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.afCLInventory = new Sped4.Controls.AFColorLabel();
            this.dtpInvnetoryFrom = new System.Windows.Forms.DateTimePicker();
            this.comboInventoryArt = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tsmMain = new Sped4.Controls.AFToolStrip();
            this.tsbtnCreateInventory = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCloseCtr = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.tsmArtikel = new Sped4.Controls.AFToolStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tsmMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // afCLInventory
            // 
            this.afCLInventory.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afCLInventory.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afCLInventory.Dock = System.Windows.Forms.DockStyle.Top;
            this.afCLInventory.Location = new System.Drawing.Point(0, 0);
            this.afCLInventory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.afCLInventory.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afCLInventory.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afCLInventory.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afCLInventory.myText = "Inventuren - Anlegen";
            this.afCLInventory.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afCLInventory.myUnderlined = true;
            this.afCLInventory.Name = "afCLInventory";
            this.afCLInventory.Size = new System.Drawing.Size(952, 43);
            this.afCLInventory.TabIndex = 6;
            this.afCLInventory.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dtpInvnetoryFrom
            // 
            this.dtpInvnetoryFrom.Location = new System.Drawing.Point(116, 102);
            this.dtpInvnetoryFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtpInvnetoryFrom.Name = "dtpInvnetoryFrom";
            this.dtpInvnetoryFrom.Size = new System.Drawing.Size(250, 22);
            this.dtpInvnetoryFrom.TabIndex = 172;
            // 
            // comboInventoryArt
            // 
            this.comboInventoryArt.AllowDrop = true;
            this.comboInventoryArt.Enabled = false;
            this.comboInventoryArt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboInventoryArt.FormattingEnabled = true;
            this.comboInventoryArt.ItemHeight = 16;
            this.comboInventoryArt.Location = new System.Drawing.Point(116, 65);
            this.comboInventoryArt.Margin = new System.Windows.Forms.Padding(4);
            this.comboInventoryArt.Name = "comboInventoryArt";
            this.comboInventoryArt.Size = new System.Drawing.Size(250, 24);
            this.comboInventoryArt.TabIndex = 171;
            this.comboInventoryArt.SelectedIndexChanged += new System.EventHandler(this.comboInventoryArt_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(13, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Art:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(13, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Beschreibung:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(13, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "vom:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // tsmMain
            // 
            this.tsmMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsmMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnCreateInventory,
            this.tsbtnCloseCtr,
            this.toolStripSeparator1});
            this.tsmMain.Location = new System.Drawing.Point(0, 0);
            this.tsmMain.myColorFrom = System.Drawing.Color.Azure;
            this.tsmMain.myColorTo = System.Drawing.Color.Blue;
            this.tsmMain.myUnderlineColor = System.Drawing.Color.White;
            this.tsmMain.myUnderlined = true;
            this.tsmMain.Name = "tsmMain";
            this.tsmMain.Size = new System.Drawing.Size(403, 27);
            this.tsmMain.TabIndex = 144;
            this.tsmMain.Text = "afToolStrip3";
            // 
            // tsbtnCreateInventory
            // 
            this.tsbtnCreateInventory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCreateInventory.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnCreateInventory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCreateInventory.Name = "tsbtnCreateInventory";
            this.tsbtnCreateInventory.Size = new System.Drawing.Size(29, 24);
            this.tsbtnCreateInventory.Text = "Inventur erstellen";
            this.tsbtnCreateInventory.Click += new System.EventHandler(this.tsbtnCreateInventory_Click);
            // 
            // tsbtnCloseCtr
            // 
            this.tsbtnCloseCtr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCloseCtr.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnCloseCtr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCloseCtr.Name = "tsbtnCloseCtr";
            this.tsbtnCloseCtr.Size = new System.Drawing.Size(29, 24);
            this.tsbtnCloseCtr.Text = "Invneturerstellung schließen";
            this.tsbtnCloseCtr.Click += new System.EventHandler(this.tsbtnCloseCtr_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.Location = new System.Drawing.Point(0, 25);
            this.dgv.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            // 
            // 
            // 
            this.dgv.MasterTemplate.AllowAddNewRow = false;
            this.dgv.MasterTemplate.AllowEditRow = false;
            this.dgv.MasterTemplate.EnableGrouping = false;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 25, 240, 150);
            this.dgv.Size = new System.Drawing.Size(545, 612);
            this.dgv.TabIndex = 144;
            this.dgv.ThemeName = "ControlDefault";
            // 
            // tsmArtikel
            // 
            this.tsmArtikel.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsmArtikel.Location = new System.Drawing.Point(0, 0);
            this.tsmArtikel.myColorFrom = System.Drawing.Color.Azure;
            this.tsmArtikel.myColorTo = System.Drawing.Color.Blue;
            this.tsmArtikel.myUnderlineColor = System.Drawing.Color.White;
            this.tsmArtikel.myUnderlined = true;
            this.tsmArtikel.Name = "tsmArtikel";
            this.tsmArtikel.Size = new System.Drawing.Size(545, 25);
            this.tsmArtikel.TabIndex = 143;
            this.tsmArtikel.Text = "afToolStrip3";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.IsSplitterFixed = true;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 43);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panel1);
            this.splitContainerMain.Panel1.Controls.Add(this.tsmMain);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.dgv);
            this.splitContainerMain.Panel2.Controls.Add(this.tsmArtikel);
            this.splitContainerMain.Size = new System.Drawing.Size(952, 637);
            this.splitContainerMain.SplitterDistance = 403;
            this.splitContainerMain.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tbDescription);
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpInvnetoryFrom);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboInventoryArt);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 610);
            this.panel1.TabIndex = 145;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(16, 178);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(350, 413);
            this.tbDescription.TabIndex = 174;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(116, 32);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(250, 22);
            this.tbName.TabIndex = 173;
            // 
            // ctrInventoryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.afCLInventory);
            this.Name = "ctrInventoryAdd";
            this.Size = new System.Drawing.Size(952, 680);
            this.Load += new System.EventHandler(this.ctrInventoryAdd_Load);
            this.tsmMain.ResumeLayout(false);
            this.tsmMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFColorLabel afCLInventory;
        private Controls.AFToolStrip tsmArtikel;
        private Controls.AFToolStrip tsmMain;
        private System.Windows.Forms.ToolStripButton tsbtnCreateInventory;
        private System.Windows.Forms.ToolStripButton tsbtnCloseCtr;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboInventoryArt;
        private System.Windows.Forms.DateTimePicker dtpInvnetoryFrom;
        public Telerik.WinControls.UI.RadGridView dgv;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbDescription;
    }
}
