namespace Sped4
{
    partial class ctrComLog
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
            this.tbComLog = new Telerik.WinControls.UI.RadTextBox();
            this.minMaxOption = new Sped4.Controls.AFMinMaxPanel();
            this.Calender = new Telerik.WinControls.UI.RadCalendar();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tslLogIN = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslLogOUT = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tbComLog)).BeginInit();
            this.minMaxOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Calender)).BeginInit();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbComLog
            // 
            this.tbComLog.AutoSize = false;
            this.tbComLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbComLog.Location = new System.Drawing.Point(0, 185);
            this.tbComLog.Multiline = true;
            this.tbComLog.Name = "tbComLog";
            this.tbComLog.Size = new System.Drawing.Size(921, 156);
            this.tbComLog.TabIndex = 9;
            // 
            // minMaxOption
            // 
            this.minMaxOption.BackColor = System.Drawing.Color.White;
            this.minMaxOption.Controls.Add(this.Calender);
            this.minMaxOption.Controls.Add(this.afToolStrip1);
            this.minMaxOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.minMaxOption.ExpandedCallapsed = Sped4.Controls.AFMinMaxPanel.EStatus.Expanded;
            this.minMaxOption.Location = new System.Drawing.Point(0, 28);
            this.minMaxOption.myFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.minMaxOption.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minMaxOption.myImage = global::Sped4.Properties.Resources.gears_preferences;
            this.minMaxOption.myText = "Optionen";
            this.minMaxOption.Name = "minMaxOption";
            this.minMaxOption.Size = new System.Drawing.Size(921, 157);
            this.minMaxOption.TabIndex = 8;
            this.minMaxOption.Text = "afMinMaxPanel1";
            // 
            // Calender
            // 
            this.Calender.Columns = 21;
            this.Calender.Location = new System.Drawing.Point(3, 56);
            this.Calender.MonthLayout = Telerik.WinControls.UI.MonthLayout.Layout_21rows_x_2columns;
            this.Calender.Name = "Calender";
            this.Calender.Rows = 2;
            this.Calender.Size = new System.Drawing.Size(915, 95);
            this.Calender.TabIndex = 9;
            this.Calender.Text = "radCalendar1";
            this.Calender.SelectionChanged += new System.EventHandler(this.Calender_SelectionChanged);
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tslLogIN,
            this.toolStripSeparator1,
            this.tslLogOUT,
            this.toolStripSeparator2,
            this.tsbtnClose});
            this.afToolStrip1.Location = new System.Drawing.Point(3, 28);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(257, 25);
            this.afToolStrip1.TabIndex = 8;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Sped4.Properties.Resources.refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tslLogIN
            // 
            this.tslLogIN.Name = "tslLogIN";
            this.tslLogIN.Size = new System.Drawing.Size(72, 22);
            this.tslLogIN.Text = "ASN/DFÜ IN";
            this.tslLogIN.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslLogOUT
            // 
            this.tslLogOUT.Name = "tslLogOUT";
            this.tslLogOUT.Size = new System.Drawing.Size(84, 22);
            this.tslLogOUT.Text = "ASN/DFÜ OUT";
            this.tslLogOUT.Click += new System.EventHandler(this.tslLogOUT_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Logbuch schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
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
            this.afColorLabel1.myText = "Logbuch";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(921, 28);
            this.afColorLabel1.TabIndex = 7;
            // 
            // ctrComLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbComLog);
            this.Controls.Add(this.minMaxOption);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrComLog";
            this.Size = new System.Drawing.Size(921, 341);
            this.Load += new System.EventHandler(this.ctrComLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbComLog)).EndInit();
            this.minMaxOption.ResumeLayout(false);
            this.minMaxOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Calender)).EndInit();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Controls.AFMinMaxPanel minMaxOption;
        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.ToolStripLabel tslLogIN;
        private System.Windows.Forms.ToolStripLabel tslLogOUT;
        private Telerik.WinControls.UI.RadTextBox tbComLog;
        private Telerik.WinControls.UI.RadCalendar Calender;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
