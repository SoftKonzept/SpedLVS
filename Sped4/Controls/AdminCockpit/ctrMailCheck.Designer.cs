namespace Sped4.Controls.AdminCockpit
{
    partial class ctrMailCheck
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
            this.gbSMTP = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPass = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.MaskedTextBox();
            this.cbSmtpAuth = new System.Windows.Forms.CheckBox();
            this.cbSSLTLS = new System.Windows.Forms.CheckBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMailMessage = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbBetreff = new System.Windows.Forms.MaskedTextBox();
            this.tbMailTo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbMailFrom = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSmtpTest = new System.Windows.Forms.Button();
            this.btnMailSend = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.btnSKData = new System.Windows.Forms.Button();
            this.btnMaildaten = new System.Windows.Forms.Button();
            this.btnCredentialCreate = new System.Windows.Forms.Button();
            this.btnCredentialsImport = new System.Windows.Forms.Button();
            this.gbSMTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.gbMailMessage.SuspendLayout();
            this.gbLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSMTP
            // 
            this.gbSMTP.Controls.Add(this.label4);
            this.gbSMTP.Controls.Add(this.tbPass);
            this.gbSMTP.Controls.Add(this.label3);
            this.gbSMTP.Controls.Add(this.tbUser);
            this.gbSMTP.Controls.Add(this.cbSmtpAuth);
            this.gbSMTP.Controls.Add(this.cbSSLTLS);
            this.gbSMTP.Controls.Add(this.nudPort);
            this.gbSMTP.Controls.Add(this.label2);
            this.gbSMTP.Controls.Add(this.tbServer);
            this.gbSMTP.Controls.Add(this.label1);
            this.gbSMTP.Location = new System.Drawing.Point(12, 13);
            this.gbSMTP.Name = "gbSMTP";
            this.gbSMTP.Size = new System.Drawing.Size(370, 198);
            this.gbSMTP.TabIndex = 0;
            this.gbSMTP.TabStop = false;
            this.gbSMTP.Text = "SMTP Verbindung";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Passwort:";
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(80, 158);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(284, 20);
            this.tbPass.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Benutzer:";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(80, 135);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(284, 20);
            this.tbUser.TabIndex = 2;
            // 
            // cbSmtpAuth
            // 
            this.cbSmtpAuth.AutoSize = true;
            this.cbSmtpAuth.Location = new System.Drawing.Point(21, 92);
            this.cbSmtpAuth.Name = "cbSmtpAuth";
            this.cbSmtpAuth.Size = new System.Drawing.Size(143, 17);
            this.cbSmtpAuth.TabIndex = 6;
            this.cbSmtpAuth.Text = "SMTP - Authentifizierung";
            this.cbSmtpAuth.UseVisualStyleBackColor = true;
            // 
            // cbSSLTLS
            // 
            this.cbSSLTLS.AutoSize = true;
            this.cbSSLTLS.Location = new System.Drawing.Point(194, 59);
            this.cbSSLTLS.Name = "cbSSLTLS";
            this.cbSSLTLS.Size = new System.Drawing.Size(77, 17);
            this.cbSSLTLS.TabIndex = 5;
            this.cbSSLTLS.Text = "SSL / TLS";
            this.cbSSLTLS.UseVisualStyleBackColor = true;
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(80, 56);
            this.nudPort.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(64, 20);
            this.nudPort.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(80, 26);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(284, 20);
            this.tbServer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // gbMailMessage
            // 
            this.gbMailMessage.Controls.Add(this.label11);
            this.gbMailMessage.Controls.Add(this.tbMessage);
            this.gbMailMessage.Controls.Add(this.label10);
            this.gbMailMessage.Controls.Add(this.tbBetreff);
            this.gbMailMessage.Controls.Add(this.tbMailTo);
            this.gbMailMessage.Controls.Add(this.label9);
            this.gbMailMessage.Controls.Add(this.tbMailFrom);
            this.gbMailMessage.Controls.Add(this.label8);
            this.gbMailMessage.Location = new System.Drawing.Point(398, 19);
            this.gbMailMessage.Name = "gbMailMessage";
            this.gbMailMessage.Size = new System.Drawing.Size(370, 192);
            this.gbMailMessage.TabIndex = 10;
            this.gbMailMessage.TabStop = false;
            this.gbMailMessage.Text = "E-Mail Nachricht";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Text";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(80, 104);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(250, 74);
            this.tbMessage.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Betreff:";
            // 
            // tbBetreff
            // 
            this.tbBetreff.Location = new System.Drawing.Point(80, 78);
            this.tbBetreff.Name = "tbBetreff";
            this.tbBetreff.Size = new System.Drawing.Size(247, 20);
            this.tbBetreff.TabIndex = 12;
            // 
            // tbMailTo
            // 
            this.tbMailTo.Location = new System.Drawing.Point(80, 52);
            this.tbMailTo.Name = "tbMailTo";
            this.tbMailTo.Size = new System.Drawing.Size(247, 20);
            this.tbMailTo.TabIndex = 11;
            this.tbMailTo.Text = "info@softkonzept.com";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "bis:";
            // 
            // tbMailFrom
            // 
            this.tbMailFrom.Location = new System.Drawing.Point(80, 26);
            this.tbMailFrom.Name = "tbMailFrom";
            this.tbMailFrom.Size = new System.Drawing.Size(247, 20);
            this.tbMailFrom.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "von:";
            // 
            // btnSmtpTest
            // 
            this.btnSmtpTest.Location = new System.Drawing.Point(12, 217);
            this.btnSmtpTest.Name = "btnSmtpTest";
            this.btnSmtpTest.Size = new System.Drawing.Size(144, 34);
            this.btnSmtpTest.TabIndex = 11;
            this.btnSmtpTest.Text = "SMTP Verbindung testen";
            this.btnSmtpTest.UseVisualStyleBackColor = true;
            this.btnSmtpTest.Click += new System.EventHandler(this.btnSmtpTest_Click);
            // 
            // btnMailSend
            // 
            this.btnMailSend.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnMailSend.ForeColor = System.Drawing.Color.White;
            this.btnMailSend.Location = new System.Drawing.Point(162, 217);
            this.btnMailSend.Name = "btnMailSend";
            this.btnMailSend.Size = new System.Drawing.Size(131, 34);
            this.btnMailSend.TabIndex = 12;
            this.btnMailSend.Text = "Testmail senden";
            this.btnMailSend.UseVisualStyleBackColor = false;
            this.btnMailSend.Click += new System.EventHandler(this.btnMailSend_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(299, 217);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(108, 34);
            this.btnLog.TabIndex = 13;
            this.btnLog.Text = "Log leeren";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.statusStrip);
            this.gbLog.Controls.Add(this.rtbLog);
            this.gbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLog.Location = new System.Drawing.Point(0, 0);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(731, 200);
            this.gbLog.TabIndex = 14;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Protokoll / Fehlerdetails";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(3, 175);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(725, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(3, 19);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(722, 153);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // scMain
            // 
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.btnCredentialsImport);
            this.scMain.Panel1.Controls.Add(this.btnCredentialCreate);
            this.scMain.Panel1.Controls.Add(this.btnSKData);
            this.scMain.Panel1.Controls.Add(this.btnMaildaten);
            this.scMain.Panel1.Controls.Add(this.gbSMTP);
            this.scMain.Panel1.Controls.Add(this.btnLog);
            this.scMain.Panel1.Controls.Add(this.gbMailMessage);
            this.scMain.Panel1.Controls.Add(this.btnMailSend);
            this.scMain.Panel1.Controls.Add(this.btnSmtpTest);
            this.scMain.Panel1MinSize = 280;
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.AutoScroll = true;
            this.scMain.Panel2.Controls.Add(this.gbLog);
            this.scMain.Panel2MinSize = 200;
            this.scMain.Size = new System.Drawing.Size(731, 484);
            this.scMain.SplitterDistance = 280;
            this.scMain.TabIndex = 15;
            // 
            // btnSKData
            // 
            this.btnSKData.BackColor = System.Drawing.Color.Wheat;
            this.btnSKData.Location = new System.Drawing.Point(184, 252);
            this.btnSKData.Name = "btnSKData";
            this.btnSKData.Size = new System.Drawing.Size(166, 23);
            this.btnSKData.TabIndex = 15;
            this.btnSKData.Text = "SK Maildaten setzen";
            this.btnSKData.UseVisualStyleBackColor = false;
            this.btnSKData.Click += new System.EventHandler(this.btnSKData_Click);
            // 
            // btnMaildaten
            // 
            this.btnMaildaten.BackColor = System.Drawing.Color.Wheat;
            this.btnMaildaten.Location = new System.Drawing.Point(12, 252);
            this.btnMaildaten.Name = "btnMaildaten";
            this.btnMaildaten.Size = new System.Drawing.Size(166, 23);
            this.btnMaildaten.TabIndex = 14;
            this.btnMaildaten.Text = "Standart Maildaten setzen";
            this.btnMaildaten.UseVisualStyleBackColor = false;
            this.btnMaildaten.Click += new System.EventHandler(this.btnMaildaten_Click);
            // 
            // btnCredentialCreate
            // 
            this.btnCredentialCreate.BackColor = System.Drawing.Color.Wheat;
            this.btnCredentialCreate.Location = new System.Drawing.Point(451, 217);
            this.btnCredentialCreate.Name = "btnCredentialCreate";
            this.btnCredentialCreate.Size = new System.Drawing.Size(134, 52);
            this.btnCredentialCreate.TabIndex = 16;
            this.btnCredentialCreate.Text = "Mail Credential erstellen";
            this.btnCredentialCreate.UseVisualStyleBackColor = false;
            this.btnCredentialCreate.Click += new System.EventHandler(this.btnCredentialCreate_Click);
            // 
            // btnCredentialsImport
            // 
            this.btnCredentialsImport.BackColor = System.Drawing.Color.Wheat;
            this.btnCredentialsImport.Location = new System.Drawing.Point(591, 217);
            this.btnCredentialsImport.Name = "btnCredentialsImport";
            this.btnCredentialsImport.Size = new System.Drawing.Size(134, 52);
            this.btnCredentialsImport.TabIndex = 17;
            this.btnCredentialsImport.Text = "Credentials importieren";
            this.btnCredentialsImport.UseVisualStyleBackColor = false;
            this.btnCredentialsImport.Click += new System.EventHandler(this.btnCredentialsImport_Click);
            // 
            // ctrMailCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Name = "ctrMailCheck";
            this.Size = new System.Drawing.Size(781, 532);
            this.Load += new System.EventHandler(this.MailCheck_Load);
            this.gbSMTP.ResumeLayout(false);
            this.gbSMTP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.gbMailMessage.ResumeLayout(false);
            this.gbMailMessage.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSMTP;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox tbUser;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSSLTLS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox tbPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbSmtpAuth;
        private System.Windows.Forms.GroupBox gbMailMessage;
        private System.Windows.Forms.TextBox tbMailFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MaskedTextBox tbBetreff;
        private System.Windows.Forms.TextBox tbMailTo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSmtpTest;
        private System.Windows.Forms.Button btnMailSend;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Button btnMaildaten;
        private System.Windows.Forms.Button btnSKData;
        private System.Windows.Forms.Button btnCredentialCreate;
        private System.Windows.Forms.Button btnCredentialsImport;
    }
}
