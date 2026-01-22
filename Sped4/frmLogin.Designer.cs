namespace Sped4
{
  partial class frmLogin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("deutsch", 0);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLoginName = new System.Windows.Forms.TextBox();
            this.tbPasswort = new System.Windows.Forms.TextBox();
            this.lbArbeitsbereich = new System.Windows.Forms.ListBox();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.btnLogin = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lAusArbeitsbereich = new System.Windows.Forms.Label();
            this.lAusSprache = new System.Windows.Forms.Label();
            this.lArbeitsbereich = new System.Windows.Forms.Label();
            this.lSprache = new System.Windows.Forms.Label();
            this.lvSprache = new System.Windows.Forms.ListView();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.scLogin = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.afToolStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scLogin)).BeginInit();
            this.scLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.Location = new System.Drawing.Point(46, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label2.Location = new System.Drawing.Point(46, 372);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Passwort";
            // 
            // tbLoginName
            // 
            this.tbLoginName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLoginName.Location = new System.Drawing.Point(102, 337);
            this.tbLoginName.Name = "tbLoginName";
            this.tbLoginName.Size = new System.Drawing.Size(228, 20);
            this.tbLoginName.TabIndex = 1;
            this.tbLoginName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLoginName_KeyDown);
            // 
            // tbPasswort
            // 
            this.tbPasswort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPasswort.Location = new System.Drawing.Point(102, 370);
            this.tbPasswort.Name = "tbPasswort";
            this.tbPasswort.Size = new System.Drawing.Size(228, 20);
            this.tbPasswort.TabIndex = 2;
            this.tbPasswort.UseSystemPasswordChar = true;
            this.tbPasswort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPasswort_KeyDown);
            this.tbPasswort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPasswort_KeyPress);
            // 
            // lbArbeitsbereich
            // 
            this.lbArbeitsbereich.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbArbeitsbereich.FormattingEnabled = true;
            this.lbArbeitsbereich.Location = new System.Drawing.Point(133, 86);
            this.lbArbeitsbereich.Name = "lbArbeitsbereich";
            this.lbArbeitsbereich.Size = new System.Drawing.Size(197, 119);
            this.lbArbeitsbereich.TabIndex = 5;
            this.lbArbeitsbereich.SelectedIndexChanged += new System.EventHandler(this.lbArbeitsbereich_SelectedIndexChanged);
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "germany.PNG");
            this.il.Images.SetKeyName(1, "GreatBrit.PNG");
            // 
            // afToolStrip2
            // 
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLogin,
            this.btnClose});
            this.afToolStrip2.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            this.afToolStrip2.Size = new System.Drawing.Size(367, 25);
            this.afToolStrip2.TabIndex = 7;
            // 
            // btnLogin
            // 
            this.btnLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLogin.Image = global::Sped4.Properties.Resources.check;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(23, 22);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = global::Sped4.Properties.Resources.delete;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(46, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Sprache:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(130, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Arbeitsbereiche:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lAusArbeitsbereich);
            this.groupBox1.Controls.Add(this.lAusSprache);
            this.groupBox1.Controls.Add(this.lArbeitsbereich);
            this.groupBox1.Controls.Add(this.lSprache);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(49, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auswahl:";
            // 
            // lAusArbeitsbereich
            // 
            this.lAusArbeitsbereich.AutoSize = true;
            this.lAusArbeitsbereich.ForeColor = System.Drawing.Color.DimGray;
            this.lAusArbeitsbereich.Location = new System.Drawing.Point(95, 56);
            this.lAusArbeitsbereich.Name = "lAusArbeitsbereich";
            this.lAusArbeitsbereich.Size = new System.Drawing.Size(26, 13);
            this.lAusArbeitsbereich.TabIndex = 3;
            this.lAusArbeitsbereich.Text = "leer";
            // 
            // lAusSprache
            // 
            this.lAusSprache.AutoSize = true;
            this.lAusSprache.ForeColor = System.Drawing.Color.DimGray;
            this.lAusSprache.Location = new System.Drawing.Point(95, 30);
            this.lAusSprache.Name = "lAusSprache";
            this.lAusSprache.Size = new System.Drawing.Size(48, 13);
            this.lAusSprache.TabIndex = 2;
            this.lAusSprache.Text = "deutsch";
            // 
            // lArbeitsbereich
            // 
            this.lArbeitsbereich.AutoSize = true;
            this.lArbeitsbereich.Location = new System.Drawing.Point(12, 56);
            this.lArbeitsbereich.Name = "lArbeitsbereich";
            this.lArbeitsbereich.Size = new System.Drawing.Size(84, 13);
            this.lArbeitsbereich.TabIndex = 1;
            this.lArbeitsbereich.Text = "Arbeitsbereich:";
            // 
            // lSprache
            // 
            this.lSprache.AutoSize = true;
            this.lSprache.Location = new System.Drawing.Point(12, 30);
            this.lSprache.Name = "lSprache";
            this.lSprache.Size = new System.Drawing.Size(51, 13);
            this.lSprache.TabIndex = 0;
            this.lSprache.Text = "Sprache:";
            // 
            // lvSprache
            // 
            this.lvSprache.Enabled = false;
            this.lvSprache.HideSelection = false;
            this.lvSprache.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvSprache.Location = new System.Drawing.Point(47, 86);
            this.lvSprache.MultiSelect = false;
            this.lvSprache.Name = "lvSprache";
            this.lvSprache.Size = new System.Drawing.Size(80, 119);
            this.lvSprache.SmallImageList = this.il;
            this.lvSprache.TabIndex = 12;
            this.lvSprache.UseCompatibleStateImageBehavior = false;
            this.lvSprache.View = System.Windows.Forms.View.List;
            this.lvSprache.SelectedIndexChanged += new System.EventHandler(this.lvSprache_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            // 
            // scLogin
            // 
            this.scLogin.Controls.Add(this.splitPanel1);
            this.scLogin.Controls.Add(this.splitPanel2);
            this.scLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLogin.Location = new System.Drawing.Point(0, 0);
            this.scLogin.Name = "scLogin";
            this.scLogin.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.scLogin.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.scLogin.Size = new System.Drawing.Size(367, 579);
            this.scLogin.TabIndex = 15;
            this.scLogin.TabStop = false;
            this.scLogin.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.tbInfo);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(367, 117);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.2965218F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -116);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // tbInfo
            // 
            this.tbInfo.BackColor = System.Drawing.Color.White;
            this.tbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInfo.Location = new System.Drawing.Point(0, 0);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.ReadOnly = true;
            this.tbInfo.Size = new System.Drawing.Size(367, 117);
            this.tbInfo.TabIndex = 14;
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.afToolStrip2);
            this.splitPanel2.Controls.Add(this.label4);
            this.splitPanel2.Controls.Add(this.label1);
            this.splitPanel2.Controls.Add(this.lvSprache);
            this.splitPanel2.Controls.Add(this.label2);
            this.splitPanel2.Controls.Add(this.groupBox1);
            this.splitPanel2.Controls.Add(this.tbLoginName);
            this.splitPanel2.Controls.Add(this.tbPasswort);
            this.splitPanel2.Controls.Add(this.label3);
            this.splitPanel2.Controls.Add(this.lbArbeitsbereich);
            this.splitPanel2.Location = new System.Drawing.Point(0, 121);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel2.Size = new System.Drawing.Size(367, 458);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.2965218F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 116);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 579);
            this.Controls.Add(this.scLogin);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sped4 Zugangskontrolle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogin_KeyDown);
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scLogin)).EndInit();
            this.scLogin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            this.splitPanel2.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbLoginName;
    private System.Windows.Forms.TextBox tbPasswort;
    private System.Windows.Forms.ListBox lbArbeitsbereich;
    private System.Windows.Forms.ImageList il;
    private Controls.AFToolStrip afToolStrip2;
    private System.Windows.Forms.ToolStripButton btnLogin;
    private System.Windows.Forms.ToolStripButton btnClose;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label lAusArbeitsbereich;
    private System.Windows.Forms.Label lAusSprache;
    private System.Windows.Forms.Label lArbeitsbereich;
    private System.Windows.Forms.Label lSprache;
    private System.Windows.Forms.ListView lvSprache;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button3;
    private Telerik.WinControls.UI.RadSplitContainer scLogin;
    private Telerik.WinControls.UI.SplitPanel splitPanel1;
    private Telerik.WinControls.UI.SplitPanel splitPanel2;
    public System.Windows.Forms.TextBox tbInfo;
    }
}