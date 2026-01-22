namespace Sped4
{
    partial class ctrPostCenter
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
            this.scPostCenterContainerMain = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.panActionSelect = new System.Windows.Forms.Panel();
            this.tbAuftraggeber = new System.Windows.Forms.TextBox();
            this.tbSearchA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            this.btnPrintAllAnzeigen = new Telerik.WinControls.UI.RadButton();
            this.btnPrintAusgangAnzeige = new Telerik.WinControls.UI.RadButton();
            this.btnPrintEingangAnzeige = new Telerik.WinControls.UI.RadButton();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpPrintDate = new System.Windows.Forms.DateTimePicker();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.panLog = new System.Windows.Forms.Panel();
            this.tbLog = new Telerik.WinControls.UI.RadTextBox();
            this.afColorLabel1 = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.scPostCenterContainerMain)).BeginInit();
            this.scPostCenterContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            this.panActionSelect.SuspendLayout();
            this.afToolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAllAnzeigen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAusgangAnzeige)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintEingangAnzeige)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.panLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbLog)).BeginInit();
            this.SuspendLayout();
            // 
            // scPostCenterContainerMain
            // 
            this.scPostCenterContainerMain.Controls.Add(this.splitPanel1);
            this.scPostCenterContainerMain.Controls.Add(this.splitPanel2);
            this.scPostCenterContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scPostCenterContainerMain.Location = new System.Drawing.Point(0, 28);
            this.scPostCenterContainerMain.Name = "scPostCenterContainerMain";
            this.scPostCenterContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.scPostCenterContainerMain.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scPostCenterContainerMain.Size = new System.Drawing.Size(653, 567);
            this.scPostCenterContainerMain.SplitterWidth = 4;
            this.scPostCenterContainerMain.TabIndex = 11;
            this.scPostCenterContainerMain.TabStop = false;
            this.scPostCenterContainerMain.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.panActionSelect);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(653, 215);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.1175373F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -77);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // panActionSelect
            // 
            this.panActionSelect.BackColor = System.Drawing.Color.White;
            this.panActionSelect.Controls.Add(this.tbAuftraggeber);
            this.panActionSelect.Controls.Add(this.tbSearchA);
            this.panActionSelect.Controls.Add(this.label1);
            this.panActionSelect.Controls.Add(this.afToolStrip2);
            this.panActionSelect.Controls.Add(this.btnPrintAllAnzeigen);
            this.panActionSelect.Controls.Add(this.btnPrintAusgangAnzeige);
            this.panActionSelect.Controls.Add(this.btnPrintEingangAnzeige);
            this.panActionSelect.Controls.Add(this.label4);
            this.panActionSelect.Controls.Add(this.dtpPrintDate);
            this.panActionSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panActionSelect.Location = new System.Drawing.Point(0, 0);
            this.panActionSelect.Name = "panActionSelect";
            this.panActionSelect.Size = new System.Drawing.Size(653, 215);
            this.panActionSelect.TabIndex = 0;
            // 
            // tbAuftraggeber
            // 
            this.tbAuftraggeber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAuftraggeber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAuftraggeber.Enabled = false;
            this.tbAuftraggeber.Location = new System.Drawing.Point(368, 67);
            this.tbAuftraggeber.Name = "tbAuftraggeber";
            this.tbAuftraggeber.ReadOnly = true;
            this.tbAuftraggeber.Size = new System.Drawing.Size(255, 20);
            this.tbAuftraggeber.TabIndex = 122;
            // 
            // tbSearchA
            // 
            this.tbSearchA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSearchA.Location = new System.Drawing.Point(448, 41);
            this.tbSearchA.Name = "tbSearchA";
            this.tbSearchA.Size = new System.Drawing.Size(86, 20);
            this.tbSearchA.TabIndex = 121;
            this.tbSearchA.TextChanged += new System.EventHandler(this.tbSearchA_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(365, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "Kunde:";
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClose});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(653, 25);
            this.afToolStrip2.TabIndex = 55;
            this.afToolStrip2.Text = "afToolStrip2";
            // 
            // tsbtnClose
            // 
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClose.Text = "Post Center schliessen";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // btnPrintAllAnzeigen
            // 
            this.btnPrintAllAnzeigen.Location = new System.Drawing.Point(25, 133);
            this.btnPrintAllAnzeigen.Name = "btnPrintAllAnzeigen";
            this.btnPrintAllAnzeigen.Size = new System.Drawing.Size(311, 24);
            this.btnPrintAllAnzeigen.TabIndex = 54;
            this.btnPrintAllAnzeigen.Text = "Eingang- / Ausgangsanzeigen";
            this.btnPrintAllAnzeigen.Click += new System.EventHandler(this.btnPrintAllAnzeigen_Click);
            // 
            // btnPrintAusgangAnzeige
            // 
            this.btnPrintAusgangAnzeige.Location = new System.Drawing.Point(25, 103);
            this.btnPrintAusgangAnzeige.Name = "btnPrintAusgangAnzeige";
            this.btnPrintAusgangAnzeige.Size = new System.Drawing.Size(311, 24);
            this.btnPrintAusgangAnzeige.TabIndex = 53;
            this.btnPrintAusgangAnzeige.Text = "Ausgangsanzeige";
            this.btnPrintAusgangAnzeige.Click += new System.EventHandler(this.btnPrintAusgangAnzeige_Click);
            // 
            // btnPrintEingangAnzeige
            // 
            this.btnPrintEingangAnzeige.Location = new System.Drawing.Point(25, 73);
            this.btnPrintEingangAnzeige.Name = "btnPrintEingangAnzeige";
            this.btnPrintEingangAnzeige.Size = new System.Drawing.Size(311, 24);
            this.btnPrintEingangAnzeige.TabIndex = 52;
            this.btnPrintEingangAnzeige.Text = "Eingangsanzeige";
            this.btnPrintEingangAnzeige.Click += new System.EventHandler(this.btnPrintEingangAnzeige_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(22, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Datum:";
            // 
            // dtpPrintDate
            // 
            this.dtpPrintDate.Location = new System.Drawing.Point(105, 37);
            this.dtpPrintDate.Name = "dtpPrintDate";
            this.dtpPrintDate.Size = new System.Drawing.Size(231, 20);
            this.dtpPrintDate.TabIndex = 50;
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.panLog);
            this.splitPanel2.Location = new System.Drawing.Point(0, 219);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(653, 348);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.1175373F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 77);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // panLog
            // 
            this.panLog.AutoScroll = true;
            this.panLog.BackColor = System.Drawing.Color.Gainsboro;
            this.panLog.Controls.Add(this.tbLog);
            this.panLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLog.Location = new System.Drawing.Point(0, 0);
            this.panLog.Name = "panLog";
            this.panLog.Size = new System.Drawing.Size(653, 348);
            this.panLog.TabIndex = 0;
            // 
            // tbLog
            // 
            this.tbLog.AutoScroll = true;
            this.tbLog.AutoSize = false;
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(0, 0);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(653, 348);
            this.tbLog.TabIndex = 0;
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
            this.afColorLabel1.myText = "Post Center";
            this.afColorLabel1.myUnderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.afColorLabel1.myUnderlined = true;
            this.afColorLabel1.Name = "afColorLabel1";
            this.afColorLabel1.Size = new System.Drawing.Size(653, 28);
            this.afColorLabel1.TabIndex = 10;
            this.afColorLabel1.Text = "afColorLabel2";
            // 
            // ctrPostCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scPostCenterContainerMain);
            this.Controls.Add(this.afColorLabel1);
            this.Name = "ctrPostCenter";
            this.Size = new System.Drawing.Size(653, 595);
            ((System.ComponentModel.ISupportInitialize)(this.scPostCenterContainerMain)).EndInit();
            this.scPostCenterContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.panActionSelect.ResumeLayout(false);
            this.panActionSelect.PerformLayout();
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAllAnzeigen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAusgangAnzeige)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintEingangAnzeige)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.panLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFColorLabel afColorLabel1;
        private Telerik.WinControls.UI.RadSplitContainer scPostCenterContainerMain;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private System.Windows.Forms.Panel panActionSelect;
        private System.Windows.Forms.Panel panLog;
        private Telerik.WinControls.UI.RadButton btnPrintAllAnzeigen;
        private Telerik.WinControls.UI.RadButton btnPrintAusgangAnzeige;
        private Telerik.WinControls.UI.RadButton btnPrintEingangAnzeige;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpPrintDate;
        private Telerik.WinControls.UI.RadTextBox tbLog;
        private Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAuftraggeber;
        private System.Windows.Forms.TextBox tbSearchA;
    }
}
