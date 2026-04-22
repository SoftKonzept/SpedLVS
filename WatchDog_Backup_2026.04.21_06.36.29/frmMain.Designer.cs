namespace WatchDog
{
    partial class frmMain
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.mbtnExit = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.mbtnStart = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.mbtnStop = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.tabViewGrid = new System.Windows.Forms.TabControl();
            this.tabPage_WatchDog = new System.Windows.Forms.TabPage();
            this.scWDInfo = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.tbWDInfo = new System.Windows.Forms.TextBox();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.pbWatchDogMain = new System.Windows.Forms.PictureBox();
            this.tabPage_LogSYS = new System.Windows.Forms.TabPage();
            this.dgvLogSYS = new Telerik.WinControls.UI.RadGridView();
            this.tWatchDog = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            this.tabViewGrid.SuspendLayout();
            this.tabPage_WatchDog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scWDInfo)).BeginInit();
            this.scWDInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatchDogMain)).BeginInit();
            this.tabPage_LogSYS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogSYS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogSYS.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // mbtnExit
            // 
            // 
            // 
            // 
            this.mbtnExit.ButtonElement.ToolTipText = "Exit WatchDog";
            this.mbtnExit.Image = global::WatchDog.Properties.Resources.exit_48x48;
            this.mbtnExit.Name = "mbtnExit";
            this.mbtnExit.Text = "EXIT";
            this.mbtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mbtnExit.ToolTipText = "Exit WatchDog";
            this.mbtnExit.UseCompatibleTextRendering = false;
            this.mbtnExit.Click += new System.EventHandler(this.MbtnExit_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.mbtnExit.GetChildAt(2))).ToolTipText = "Exit WatchDog";
            // 
            // mbtnStart
            // 
            this.mbtnStart.Image = global::WatchDog.Properties.Resources.media_play_blue_48x48;
            this.mbtnStart.Name = "mbtnStart";
            this.mbtnStart.Text = "START";
            this.mbtnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mbtnStart.UseCompatibleTextRendering = false;
            this.mbtnStart.Click += new System.EventHandler(this.MbtnStart_Click);
            // 
            // mbtnStop
            // 
            this.mbtnStop.Image = global::WatchDog.Properties.Resources.media_stop_blue_48x48;
            this.mbtnStop.Name = "mbtnStop";
            this.mbtnStop.Text = "STOP";
            this.mbtnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.mbtnStop.UseCompatibleTextRendering = false;
            this.mbtnStop.Click += new System.EventHandler(this.MbtnStop_Click);
            // 
            // radMenu1
            // 
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.mbtnExit,
            this.mbtnStart,
            this.mbtnStop,
            this.mbtnExit,
            this.mbtnStart,
            this.mbtnStop});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(769, 71);
            this.radMenu1.TabIndex = 0;
            // 
            // tabViewGrid
            // 
            this.tabViewGrid.Controls.Add(this.tabPage_WatchDog);
            this.tabViewGrid.Controls.Add(this.tabPage_LogSYS);
            this.tabViewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabViewGrid.Location = new System.Drawing.Point(0, 71);
            this.tabViewGrid.Name = "tabViewGrid";
            this.tabViewGrid.SelectedIndex = 0;
            this.tabViewGrid.Size = new System.Drawing.Size(769, 379);
            this.tabViewGrid.TabIndex = 8;
            // 
            // tabPage_WatchDog
            // 
            this.tabPage_WatchDog.Controls.Add(this.scWDInfo);
            this.tabPage_WatchDog.Location = new System.Drawing.Point(4, 22);
            this.tabPage_WatchDog.Name = "tabPage_WatchDog";
            this.tabPage_WatchDog.Size = new System.Drawing.Size(761, 353);
            this.tabPage_WatchDog.TabIndex = 5;
            this.tabPage_WatchDog.Text = "Watchdog";
            this.tabPage_WatchDog.UseVisualStyleBackColor = true;
            // 
            // scWDInfo
            // 
            this.scWDInfo.Controls.Add(this.splitPanel1);
            this.scWDInfo.Controls.Add(this.splitPanel2);
            this.scWDInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scWDInfo.Location = new System.Drawing.Point(0, 0);
            this.scWDInfo.Name = "scWDInfo";
            // 
            // 
            // 
            this.scWDInfo.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.scWDInfo.Size = new System.Drawing.Size(761, 353);
            this.scWDInfo.TabIndex = 1;
            this.scWDInfo.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.White;
            this.splitPanel1.Controls.Add(this.tbWDInfo);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(246, 353);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.1752627F, 0F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-184, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // tbWDInfo
            // 
            this.tbWDInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbWDInfo.Location = new System.Drawing.Point(0, 0);
            this.tbWDInfo.Multiline = true;
            this.tbWDInfo.Name = "tbWDInfo";
            this.tbWDInfo.Size = new System.Drawing.Size(246, 353);
            this.tbWDInfo.TabIndex = 0;
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.Color.White;
            this.splitPanel2.Controls.Add(this.pbWatchDogMain);
            this.splitPanel2.Location = new System.Drawing.Point(250, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(511, 353);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.1752626F, 0F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(184, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // pbWatchDogMain
            // 
            this.pbWatchDogMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbWatchDogMain.Image = global::WatchDog.Properties.Resources.dog_256x256;
            this.pbWatchDogMain.Location = new System.Drawing.Point(0, 0);
            this.pbWatchDogMain.Name = "pbWatchDogMain";
            this.pbWatchDogMain.Size = new System.Drawing.Size(511, 353);
            this.pbWatchDogMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbWatchDogMain.TabIndex = 0;
            this.pbWatchDogMain.TabStop = false;
            // 
            // tabPage_LogSYS
            // 
            this.tabPage_LogSYS.Controls.Add(this.dgvLogSYS);
            this.tabPage_LogSYS.Location = new System.Drawing.Point(4, 22);
            this.tabPage_LogSYS.Name = "tabPage_LogSYS";
            this.tabPage_LogSYS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_LogSYS.Size = new System.Drawing.Size(761, 353);
            this.tabPage_LogSYS.TabIndex = 3;
            this.tabPage_LogSYS.Text = "Log - System";
            this.tabPage_LogSYS.UseVisualStyleBackColor = true;
            // 
            // dgvLogSYS
            // 
            this.dgvLogSYS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogSYS.Location = new System.Drawing.Point(3, 3);
            // 
            // 
            // 
            this.dgvLogSYS.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvLogSYS.Name = "dgvLogSYS";
            this.dgvLogSYS.Size = new System.Drawing.Size(755, 347);
            this.dgvLogSYS.TabIndex = 0;
            // 
            // tWatchDog
            // 
            this.tWatchDog.Interval = 10000;
            this.tWatchDog.Tick += new System.EventHandler(this.TWatchDog_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 450);
            this.Controls.Add(this.tabViewGrid);
            this.Controls.Add(this.radMenu1);
            this.Name = "frmMain";
            this.Text = "WatchDog WD - Communicator 2015 @ Comtec Nöker GmbH";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            this.tabViewGrid.ResumeLayout(false);
            this.tabPage_WatchDog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scWDInfo)).EndInit();
            this.scWDInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbWatchDogMain)).EndInit();
            this.tabPage_LogSYS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogSYS.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogSYS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadMenuButtonItem mbtnExit;
        private Telerik.WinControls.UI.RadMenuButtonItem mbtnStart;
        private Telerik.WinControls.UI.RadMenuButtonItem mbtnStop;
        private Telerik.WinControls.UI.RadMenu radMenu1;
        private System.Windows.Forms.TabControl tabViewGrid;
        private System.Windows.Forms.TabPage tabPage_WatchDog;
        private Telerik.WinControls.UI.RadSplitContainer scWDInfo;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private System.Windows.Forms.TextBox tbWDInfo;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private System.Windows.Forms.PictureBox pbWatchDogMain;
        private System.Windows.Forms.TabPage tabPage_LogSYS;
        private System.Windows.Forms.Timer tWatchDog;
        private Telerik.WinControls.UI.RadGridView dgvLogSYS;
    }
}

