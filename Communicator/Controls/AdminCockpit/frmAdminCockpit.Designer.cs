namespace Communicator.Controls.AdminCockpit
{
    partial class frmAdminCockpit
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
            this.viewPageTest = new Telerik.WinControls.UI.RadPageView();
            this.pageViewTest = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabMainTest = new System.Windows.Forms.TabControl();
            this.pageFtpTest = new System.Windows.Forms.TabPage();
            this.tbftpTextLog = new Telerik.WinControls.UI.RadTextBox();
            this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
            this.tbftpPass = new Telerik.WinControls.UI.RadTextBox();
            this.tbftpUser = new Telerik.WinControls.UI.RadTextBox();
            this.tbHost = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lPassword = new System.Windows.Forms.Label();
            this.lUser = new System.Windows.Forms.Label();
            this.lHost = new System.Windows.Forms.Label();
            this.menuTestFtp = new Telerik.WinControls.UI.RadMenu();
            this.mbtnTestStart = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pageViewEdi = new Telerik.WinControls.UI.RadPageViewPage();
            this.tabEdiProzesses = new System.Windows.Forms.TabControl();
            this.tabPageEdiZQMQalityXml = new System.Windows.Forms.TabPage();
            this.gbEdiZQMQualityXml = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEdiZQMQalityXmlFrom = new System.Windows.Forms.DateTimePicker();
            this.btnReReadZQMQalityXml = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            ((System.ComponentModel.ISupportInitialize)(this.viewPageTest)).BeginInit();
            this.viewPageTest.SuspendLayout();
            this.pageViewTest.SuspendLayout();
            this.tabMainTest.SuspendLayout();
            this.pageFtpTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbftpTextLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbftpPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbftpUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuTestFtp)).BeginInit();
            this.pageViewEdi.SuspendLayout();
            this.tabEdiProzesses.SuspendLayout();
            this.tabPageEdiZQMQalityXml.SuspendLayout();
            this.gbEdiZQMQualityXml.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // viewPageTest
            // 
            this.viewPageTest.Controls.Add(this.pageViewTest);
            this.viewPageTest.Controls.Add(this.pageViewEdi);
            this.viewPageTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPageTest.Location = new System.Drawing.Point(0, 0);
            this.viewPageTest.Name = "viewPageTest";
            this.viewPageTest.SelectedPage = this.pageViewTest;
            this.viewPageTest.Size = new System.Drawing.Size(842, 491);
            this.viewPageTest.TabIndex = 0;
            this.viewPageTest.ThemeName = "ControlDefault";
            this.viewPageTest.ViewMode = Telerik.WinControls.UI.PageViewMode.Backstage;
            ((Telerik.WinControls.UI.RadPageViewBackstageElement)(this.viewPageTest.GetChildAt(0))).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Left;
            ((Telerik.WinControls.UI.StripViewItemContainer)(this.viewPageTest.GetChildAt(0).GetChildAt(0))).MinSize = new System.Drawing.Size(125, 0);
            // 
            // pageViewTest
            // 
            this.pageViewTest.Controls.Add(this.tabMainTest);
            this.pageViewTest.ItemSize = new System.Drawing.SizeF(106F, 45F);
            this.pageViewTest.Location = new System.Drawing.Point(136, 4);
            this.pageViewTest.Name = "pageViewTest";
            this.pageViewTest.Size = new System.Drawing.Size(702, 483);
            this.pageViewTest.Text = "Test - Center";
            // 
            // tabMainTest
            // 
            this.tabMainTest.Controls.Add(this.pageFtpTest);
            this.tabMainTest.Controls.Add(this.tabPage2);
            this.tabMainTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMainTest.Location = new System.Drawing.Point(0, 0);
            this.tabMainTest.Name = "tabMainTest";
            this.tabMainTest.SelectedIndex = 0;
            this.tabMainTest.Size = new System.Drawing.Size(702, 483);
            this.tabMainTest.TabIndex = 0;
            // 
            // pageFtpTest
            // 
            this.pageFtpTest.Controls.Add(this.tbftpTextLog);
            this.pageFtpTest.Controls.Add(this.radTextBox1);
            this.pageFtpTest.Controls.Add(this.tbftpPass);
            this.pageFtpTest.Controls.Add(this.tbftpUser);
            this.pageFtpTest.Controls.Add(this.tbHost);
            this.pageFtpTest.Controls.Add(this.label1);
            this.pageFtpTest.Controls.Add(this.lPassword);
            this.pageFtpTest.Controls.Add(this.lUser);
            this.pageFtpTest.Controls.Add(this.lHost);
            this.pageFtpTest.Controls.Add(this.menuTestFtp);
            this.pageFtpTest.Location = new System.Drawing.Point(4, 22);
            this.pageFtpTest.Name = "pageFtpTest";
            this.pageFtpTest.Padding = new System.Windows.Forms.Padding(3);
            this.pageFtpTest.Size = new System.Drawing.Size(694, 457);
            this.pageFtpTest.TabIndex = 0;
            this.pageFtpTest.Text = "ftp Connection";
            this.pageFtpTest.UseVisualStyleBackColor = true;
            // 
            // tbftpTextLog
            // 
            this.tbftpTextLog.Location = new System.Drawing.Point(291, 50);
            this.tbftpTextLog.Multiline = true;
            this.tbftpTextLog.Name = "tbftpTextLog";
            // 
            // 
            // 
            this.tbftpTextLog.RootElement.StretchVertically = true;
            this.tbftpTextLog.Size = new System.Drawing.Size(371, 393);
            this.tbftpTextLog.TabIndex = 9;
            // 
            // radTextBox1
            // 
            this.radTextBox1.Location = new System.Drawing.Point(122, 124);
            this.radTextBox1.Name = "radTextBox1";
            this.radTextBox1.Size = new System.Drawing.Size(143, 20);
            this.radTextBox1.TabIndex = 8;
            // 
            // tbftpPass
            // 
            this.tbftpPass.Location = new System.Drawing.Point(122, 98);
            this.tbftpPass.Name = "tbftpPass";
            this.tbftpPass.Size = new System.Drawing.Size(143, 20);
            this.tbftpPass.TabIndex = 7;
            // 
            // tbftpUser
            // 
            this.tbftpUser.Location = new System.Drawing.Point(122, 74);
            this.tbftpUser.Name = "tbftpUser";
            this.tbftpUser.Size = new System.Drawing.Size(143, 20);
            this.tbftpUser.TabIndex = 6;
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(122, 50);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(143, 20);
            this.tbHost.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ftp-Directory:";
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(25, 100);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(51, 13);
            this.lPassword.TabIndex = 3;
            this.lPassword.Text = "ftp-Pass:";
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(25, 76);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(52, 13);
            this.lUser.TabIndex = 2;
            this.lUser.Text = "ftp-User:";
            // 
            // lHost
            // 
            this.lHost.AutoSize = true;
            this.lHost.Location = new System.Drawing.Point(25, 52);
            this.lHost.Name = "lHost";
            this.lHost.Size = new System.Drawing.Size(60, 13);
            this.lHost.TabIndex = 1;
            this.lHost.Text = "ftp-Server:";
            // 
            // menuTestFtp
            // 
            this.menuTestFtp.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.mbtnTestStart});
            this.menuTestFtp.Location = new System.Drawing.Point(3, 3);
            this.menuTestFtp.Name = "menuTestFtp";
            this.menuTestFtp.Size = new System.Drawing.Size(688, 24);
            this.menuTestFtp.TabIndex = 0;
            // 
            // mbtnTestStart
            // 
            // 
            // 
            // 
            this.mbtnTestStart.ButtonElement.ShowBorder = false;
            this.mbtnTestStart.ButtonElement.ToolTipText = "Check ftp Connection";
            this.mbtnTestStart.CanFocus = true;
            this.mbtnTestStart.ClickMode = Telerik.WinControls.ClickMode.Press;
            this.mbtnTestStart.Name = "mbtnTestStart";
            this.mbtnTestStart.StretchHorizontally = false;
            this.mbtnTestStart.StretchVertically = false;
            this.mbtnTestStart.Text = "Start";
            this.mbtnTestStart.ToolTipText = "Check ftp Connection";
            ((Telerik.WinControls.UI.RadButtonElement)(this.mbtnTestStart.GetChildAt(2))).ToolTipText = "Check ftp Connection";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(694, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pageViewEdi
            // 
            this.pageViewEdi.Controls.Add(this.tabEdiProzesses);
            this.pageViewEdi.ItemSize = new System.Drawing.SizeF(106F, 45F);
            this.pageViewEdi.Location = new System.Drawing.Point(136, 4);
            this.pageViewEdi.Name = "pageViewEdi";
            this.pageViewEdi.Size = new System.Drawing.Size(702, 483);
            this.pageViewEdi.Text = "Edi Verarbeitung";
            // 
            // tabEdiProzesses
            // 
            this.tabEdiProzesses.Controls.Add(this.tabPageEdiZQMQalityXml);
            this.tabEdiProzesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEdiProzesses.Location = new System.Drawing.Point(0, 0);
            this.tabEdiProzesses.Name = "tabEdiProzesses";
            this.tabEdiProzesses.SelectedIndex = 0;
            this.tabEdiProzesses.Size = new System.Drawing.Size(702, 483);
            this.tabEdiProzesses.TabIndex = 1;
            // 
            // tabPageEdiZQMQalityXml
            // 
            this.tabPageEdiZQMQalityXml.Controls.Add(this.gbEdiZQMQualityXml);
            this.tabPageEdiZQMQalityXml.Controls.Add(this.tbLog);
            this.tabPageEdiZQMQalityXml.Location = new System.Drawing.Point(4, 22);
            this.tabPageEdiZQMQalityXml.Name = "tabPageEdiZQMQalityXml";
            this.tabPageEdiZQMQalityXml.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEdiZQMQalityXml.Size = new System.Drawing.Size(694, 457);
            this.tabPageEdiZQMQalityXml.TabIndex = 1;
            this.tabPageEdiZQMQalityXml.Text = "EdiZQMQalityXml";
            this.tabPageEdiZQMQalityXml.UseVisualStyleBackColor = true;
            // 
            // gbEdiZQMQualityXml
            // 
            this.gbEdiZQMQualityXml.Controls.Add(this.label2);
            this.gbEdiZQMQualityXml.Controls.Add(this.dtpEdiZQMQalityXmlFrom);
            this.gbEdiZQMQualityXml.Controls.Add(this.btnReReadZQMQalityXml);
            this.gbEdiZQMQualityXml.Location = new System.Drawing.Point(17, 23);
            this.gbEdiZQMQualityXml.Name = "gbEdiZQMQualityXml";
            this.gbEdiZQMQualityXml.Size = new System.Drawing.Size(272, 92);
            this.gbEdiZQMQualityXml.TabIndex = 2;
            this.gbEdiZQMQualityXml.TabStop = false;
            this.gbEdiZQMQualityXml.Text = "ReRead ZQMQalityXml";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ab Datum:";
            // 
            // dtpEdiZQMQalityXmlFrom
            // 
            this.dtpEdiZQMQalityXmlFrom.Location = new System.Drawing.Point(78, 61);
            this.dtpEdiZQMQalityXmlFrom.Name = "dtpEdiZQMQalityXmlFrom";
            this.dtpEdiZQMQalityXmlFrom.Size = new System.Drawing.Size(188, 20);
            this.dtpEdiZQMQalityXmlFrom.TabIndex = 1;
            // 
            // btnReReadZQMQalityXml
            // 
            this.btnReReadZQMQalityXml.Location = new System.Drawing.Point(6, 21);
            this.btnReReadZQMQalityXml.Name = "btnReReadZQMQalityXml";
            this.btnReReadZQMQalityXml.Size = new System.Drawing.Size(260, 33);
            this.btnReReadZQMQalityXml.TabIndex = 0;
            this.btnReReadZQMQalityXml.Text = "ReRead ZQMQalityXml";
            this.btnReReadZQMQalityXml.UseVisualStyleBackColor = true;
            this.btnReReadZQMQalityXml.Click += new System.EventHandler(this.btnReReadZQMQalityXml_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(295, 23);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(393, 428);
            this.tbLog.TabIndex = 1;
            // 
            // frmAdminCockpit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 491);
            this.Controls.Add(this.viewPageTest);
            this.Name = "frmAdminCockpit";
            this.Text = "Admin-Cockpit";
            ((System.ComponentModel.ISupportInitialize)(this.viewPageTest)).EndInit();
            this.viewPageTest.ResumeLayout(false);
            this.pageViewTest.ResumeLayout(false);
            this.tabMainTest.ResumeLayout(false);
            this.pageFtpTest.ResumeLayout(false);
            this.pageFtpTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbftpTextLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbftpPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbftpUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuTestFtp)).EndInit();
            this.pageViewEdi.ResumeLayout(false);
            this.tabEdiProzesses.ResumeLayout(false);
            this.tabPageEdiZQMQalityXml.ResumeLayout(false);
            this.tabPageEdiZQMQalityXml.PerformLayout();
            this.gbEdiZQMQualityXml.ResumeLayout(false);
            this.gbEdiZQMQualityXml.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView viewPageTest;
        private Telerik.WinControls.UI.RadPageViewPage pageViewTest;
        private System.Windows.Forms.TabControl tabMainTest;
        private System.Windows.Forms.TabPage pageFtpTest;
        private System.Windows.Forms.TabPage tabPage2;
        private Telerik.WinControls.UI.RadPageViewPage pageViewEdi;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        private Telerik.WinControls.UI.RadMenu menuTestFtp;
        private Telerik.WinControls.UI.RadMenuButtonItem mbtnTestStart;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.Label lHost;
        private Telerik.WinControls.UI.RadTextBox tbftpTextLog;
        private Telerik.WinControls.UI.RadTextBox radTextBox1;
        private Telerik.WinControls.UI.RadTextBox tbftpPass;
        private Telerik.WinControls.UI.RadTextBox tbftpUser;
        private Telerik.WinControls.UI.RadTextBox tbHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TabControl tabEdiProzesses;
        private System.Windows.Forms.TabPage tabPageEdiZQMQalityXml;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button btnReReadZQMQalityXml;
        private System.Windows.Forms.GroupBox gbEdiZQMQualityXml;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEdiZQMQalityXmlFrom;
    }
}
