namespace Sped4
{
    partial class ctrFaktArtikelList
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.afToolStrip4 = new Sped4.Controls.AFToolStrip();
            this.tsbtnExportExcel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCloseFakt = new System.Windows.Forms.ToolStripButton();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.afToolStrip4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.SuspendLayout();
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
            this.afColorLabel1.myText = "Fakturierung: Artikelliste";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(1165, 34);
            this.afColorLabel1.TabIndex = 4;
            this.afColorLabel1.Text = "afColorLabel1";
            // 
            // afToolStrip4
            // 
            this.afToolStrip4.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.afToolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnExportExcel,
            this.tsbtnCloseFakt});
            this.afToolStrip4.Location = new System.Drawing.Point(0, 34);
            this.afToolStrip4.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip4.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip4.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip4.myUnderlined = true;
            this.afToolStrip4.Name = "afToolStrip4";
            this.afToolStrip4.Size = new System.Drawing.Size(1165, 27);
            this.afToolStrip4.TabIndex = 10;
            this.afToolStrip4.Text = "afToolStrip4";
            // 
            // tsbtnExportExcel
            // 
            this.tsbtnExportExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnExportExcel.Image = global::Sped4.Properties.Resources.Excel;
            this.tsbtnExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExportExcel.Name = "tsbtnExportExcel";
            this.tsbtnExportExcel.Size = new System.Drawing.Size(29, 24);
            this.tsbtnExportExcel.Text = "Export zu Excel";
            this.tsbtnExportExcel.Click += new System.EventHandler(this.tsbtnExcel_Click);
            // 
            // tsbtnCloseFakt
            // 
            this.tsbtnCloseFakt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCloseFakt.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnCloseFakt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCloseFakt.Name = "tsbtnCloseFakt";
            this.tsbtnCloseFakt.Size = new System.Drawing.Size(29, 24);
            this.tsbtnCloseFakt.Text = "Fakturierung schliessen";
            this.tsbtnCloseFakt.Click += new System.EventHandler(this.tsbtnCloseFakt_Click);
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.Color.White;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgv.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv.Location = new System.Drawing.Point(0, 61);
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
            this.dgv.ShowHeaderCellButtons = true;
            this.dgv.Size = new System.Drawing.Size(1165, 558);
            this.dgv.TabIndex = 23;
            this.dgv.ThemeName = "ControlDefault";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // ctrFaktArtikelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.afToolStrip4);
            this.Controls.Add(this.afColorLabel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrFaktArtikelList";
            this.Size = new System.Drawing.Size(1165, 619);
            this.afToolStrip4.ResumeLayout(false);
            this.afToolStrip4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Controls.AFToolStrip afToolStrip4;
        private System.Windows.Forms.ToolStripButton tsbtnExportExcel;
        private System.Windows.Forms.ToolStripButton tsbtnCloseFakt;
        public Telerik.WinControls.UI.RadGridView dgv;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
