namespace Sped4.Controls.Edifact
{
    partial class ctrCreateEdiStruckture
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
            this.btnCreateStruckture = new System.Windows.Forms.Button();
            this.panMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboAsnArt = new System.Windows.Forms.ComboBox();
            this.comboArbeitsbereich = new System.Windows.Forms.ComboBox();
            this.tbAdrMatchCode = new System.Windows.Forms.TextBox();
            this.tbAdrShort = new System.Windows.Forms.TextBox();
            this.nudAdrDirect = new System.Windows.Forms.NumericUpDown();
            this.btnSearchAdr = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCreation = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrDirect)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateStruckture
            // 
            this.btnCreateStruckture.Location = new System.Drawing.Point(83, 227);
            this.btnCreateStruckture.Name = "btnCreateStruckture";
            this.btnCreateStruckture.Size = new System.Drawing.Size(128, 23);
            this.btnCreateStruckture.TabIndex = 0;
            this.btnCreateStruckture.Text = "Create Struckture";
            this.btnCreateStruckture.UseVisualStyleBackColor = true;
            this.btnCreateStruckture.Click += new System.EventHandler(this.btnCreateStruckture_Click);
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.White;
            this.panMain.Controls.Add(this.panel1);
            this.panMain.Controls.Add(this.label10);
            this.panMain.Controls.Add(this.label2);
            this.panMain.Controls.Add(this.btnCreateStruckture);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(308, 256);
            this.panMain.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.comboAsnArt);
            this.panel1.Controls.Add(this.comboArbeitsbereich);
            this.panel1.Controls.Add(this.tbAdrMatchCode);
            this.panel1.Controls.Add(this.tbAdrShort);
            this.panel1.Controls.Add(this.nudAdrDirect);
            this.panel1.Controls.Add(this.btnSearchAdr);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnCreation);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 256);
            this.panel1.TabIndex = 269;
            // 
            // comboAsnArt
            // 
            this.comboAsnArt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboAsnArt.FormattingEnabled = true;
            this.comboAsnArt.Items.AddRange(new object[] {
            "IN",
            "OUT"});
            this.comboAsnArt.Location = new System.Drawing.Point(110, 107);
            this.comboAsnArt.Name = "comboAsnArt";
            this.comboAsnArt.Size = new System.Drawing.Size(162, 21);
            this.comboAsnArt.TabIndex = 274;
            this.comboAsnArt.SelectedIndexChanged += new System.EventHandler(this.comboAsnArt_SelectedIndexChanged);
            // 
            // comboArbeitsbereich
            // 
            this.comboArbeitsbereich.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboArbeitsbereich.FormattingEnabled = true;
            this.comboArbeitsbereich.Items.AddRange(new object[] {
            "IN",
            "OUT"});
            this.comboArbeitsbereich.Location = new System.Drawing.Point(100, 139);
            this.comboArbeitsbereich.Name = "comboArbeitsbereich";
            this.comboArbeitsbereich.Size = new System.Drawing.Size(172, 21);
            this.comboArbeitsbereich.TabIndex = 273;
            this.comboArbeitsbereich.SelectedIndexChanged += new System.EventHandler(this.comboArbeitsbereich_SelectedIndexChanged);
            // 
            // tbAdrMatchCode
            // 
            this.tbAdrMatchCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAdrMatchCode.Location = new System.Drawing.Point(20, 47);
            this.tbAdrMatchCode.Name = "tbAdrMatchCode";
            this.tbAdrMatchCode.Size = new System.Drawing.Size(251, 20);
            this.tbAdrMatchCode.TabIndex = 271;
            // 
            // tbAdrShort
            // 
            this.tbAdrShort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbAdrShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAdrShort.Enabled = false;
            this.tbAdrShort.Location = new System.Drawing.Point(19, 73);
            this.tbAdrShort.Name = "tbAdrShort";
            this.tbAdrShort.ReadOnly = true;
            this.tbAdrShort.Size = new System.Drawing.Size(253, 20);
            this.tbAdrShort.TabIndex = 272;
            // 
            // nudAdrDirect
            // 
            this.nudAdrDirect.Location = new System.Drawing.Point(132, 19);
            this.nudAdrDirect.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudAdrDirect.Name = "nudAdrDirect";
            this.nudAdrDirect.Size = new System.Drawing.Size(104, 20);
            this.nudAdrDirect.TabIndex = 270;
            this.nudAdrDirect.Leave += new System.EventHandler(this.nudAdrDirect_Leave);
            // 
            // btnSearchAdr
            // 
            this.btnSearchAdr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchAdr.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSearchAdr.Location = new System.Drawing.Point(20, 19);
            this.btnSearchAdr.Name = "btnSearchAdr";
            this.btnSearchAdr.Size = new System.Drawing.Size(83, 22);
            this.btnSearchAdr.TabIndex = 269;
            this.btnSearchAdr.TabStop = false;
            this.btnSearchAdr.Text = "[Address]";
            this.btnSearchAdr.UseVisualStyleBackColor = true;
            this.btnSearchAdr.Click += new System.EventHandler(this.btnSearchAdr_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(17, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 267;
            this.label1.Text = "Arbeitsbereich:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(17, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 265;
            this.label3.Text = "AsnArt:";
            // 
            // btnCreation
            // 
            this.btnCreation.Location = new System.Drawing.Point(20, 182);
            this.btnCreation.Name = "btnCreation";
            this.btnCreation.Size = new System.Drawing.Size(261, 39);
            this.btnCreation.TabIndex = 0;
            this.btnCreation.Text = "Create Structure";
            this.btnCreation.UseVisualStyleBackColor = true;
            this.btnCreation.Click += new System.EventHandler(this.btnCreateStruckture_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.DarkBlue;
            this.label10.Location = new System.Drawing.Point(17, 147);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 267;
            this.label10.Text = "Arbeitsbereich:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(17, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 265;
            this.label2.Text = "AsnArt:";
            // 
            // ctrCreateEdiStruckture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panMain);
            this.Name = "ctrCreateEdiStruckture";
            this.Size = new System.Drawing.Size(308, 256);
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdrDirect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateStruckture;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCreation;
        private System.Windows.Forms.TextBox tbAdrMatchCode;
        private System.Windows.Forms.TextBox tbAdrShort;
        private System.Windows.Forms.NumericUpDown nudAdrDirect;
        private System.Windows.Forms.Button btnSearchAdr;
        private System.Windows.Forms.ComboBox comboArbeitsbereich;
        private System.Windows.Forms.ComboBox comboAsnArt;
    }
}
