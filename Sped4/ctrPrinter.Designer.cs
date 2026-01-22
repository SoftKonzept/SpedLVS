namespace Sped4
{
   partial class ctrPrinter
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
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            this.tsbtnImportSetting = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.afToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 53);
            // 
            // 
            // 
            this.dgv.MasterTemplate.AllowAddNewRow = false;
            this.dgv.MasterTemplate.AllowColumnChooser = false;
            this.dgv.MasterTemplate.AllowColumnReorder = false;
            this.dgv.MasterTemplate.AllowColumnResize = false;
            this.dgv.MasterTemplate.AllowDeleteRow = false;
            this.dgv.MasterTemplate.AllowDragToGroup = false;
            this.dgv.MasterTemplate.AllowRowReorder = true;
            this.dgv.MasterTemplate.AllowRowResize = false;
            this.dgv.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgv.MasterTemplate.EnableGrouping = false;
            this.dgv.MasterTemplate.EnableSorting = false;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(773, 504);
            this.dgv.TabIndex = 0;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.radGridView1_CellBeginEdit);
            this.dgv.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellEditorInitialized);
            this.dgv.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellEndEdit);
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSave,
            this.tsbtnImportSetting,
            this.tsbtnClose,
            this.toolStripSeparator2});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 28);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(773, 25);
            this.afToolStrip2.TabIndex = 22;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSave.Text = "Druckerzuordnung speichern";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Suche schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator2.Visible = false;
            // 
            // afColorLabel1
            // 
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.afColorLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.afColorLabel1.Location = new System.Drawing.Point(0, 0);
            this.afColorLabel1.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.afColorLabel1.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.afColorLabel1.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.afColorLabel1.myText = "Druckerzuweisung";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(773, 28);
            this.afColorLabel1.TabIndex = 21;
            this.afColorLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tsbtnImportSetting
            // 
            this.tsbtnImportSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnImportSetting.Image = global::Sped4.Properties.Resources.documents_exchange_24x24;
            this.tsbtnImportSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnImportSetting.Name = "tsbtnImportSetting";
            this.tsbtnImportSetting.Size = new System.Drawing.Size(23, 22);
            this.tsbtnImportSetting.Text = "Import Printerlist";
            this.tsbtnImportSetting.Click += new System.EventHandler(this.tsbtnImportSetting_Click);
            // 
            // ctrPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.afToolStrip2);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrPrinter";
            this.Size = new System.Drawing.Size(773, 557);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private Telerik.WinControls.UI.RadGridView dgv;
      private Controls.AFToolStrip afToolStrip2;
      private System.Windows.Forms.ToolStripButton tsbtnSave;
      private System.Windows.Forms.ToolStripButton tsbtnClose;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private Controls.AFColorLabel afColorLabel1;
        private System.Windows.Forms.ToolStripButton tsbtnImportSetting;
    }
}
