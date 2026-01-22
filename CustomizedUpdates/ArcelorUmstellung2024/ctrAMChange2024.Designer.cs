namespace CustomizedUpdates.ArcelorUmstellung2024
{
    partial class ctrAMChange2024
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
            this.panAMMain = new System.Windows.Forms.Panel();
            this.tbInfo1 = new System.Windows.Forms.TextBox();
            this.btnUpdateLiefNr = new System.Windows.Forms.Button();
            this.tbLiefNrNEU = new System.Windows.Forms.TextBox();
            this.tbLiefNrALT = new System.Windows.Forms.TextBox();
            this.lLiefNrNEU = new System.Windows.Forms.Label();
            this.lLiefAlt = new System.Windows.Forms.Label();
            this.btnUpdateGueterart = new System.Windows.Forms.Button();
            this.btnUpdateArticleData = new System.Windows.Forms.Button();
            this.panAMMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panAMMain
            // 
            this.panAMMain.BackColor = System.Drawing.Color.White;
            this.panAMMain.Controls.Add(this.btnUpdateArticleData);
            this.panAMMain.Controls.Add(this.btnUpdateGueterart);
            this.panAMMain.Controls.Add(this.tbInfo1);
            this.panAMMain.Controls.Add(this.btnUpdateLiefNr);
            this.panAMMain.Controls.Add(this.tbLiefNrNEU);
            this.panAMMain.Controls.Add(this.tbLiefNrALT);
            this.panAMMain.Controls.Add(this.lLiefNrNEU);
            this.panAMMain.Controls.Add(this.lLiefAlt);
            this.panAMMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panAMMain.Location = new System.Drawing.Point(0, 0);
            this.panAMMain.Name = "panAMMain";
            this.panAMMain.Size = new System.Drawing.Size(737, 479);
            this.panAMMain.TabIndex = 0;
            // 
            // tbInfo1
            // 
            this.tbInfo1.Location = new System.Drawing.Point(295, 22);
            this.tbInfo1.Multiline = true;
            this.tbInfo1.Name = "tbInfo1";
            this.tbInfo1.Size = new System.Drawing.Size(426, 441);
            this.tbInfo1.TabIndex = 15;
            // 
            // btnUpdateLiefNr
            // 
            this.btnUpdateLiefNr.Location = new System.Drawing.Point(24, 232);
            this.btnUpdateLiefNr.Name = "btnUpdateLiefNr";
            this.btnUpdateLiefNr.Size = new System.Drawing.Size(257, 58);
            this.btnUpdateLiefNr.TabIndex = 14;
            this.btnUpdateLiefNr.Text = "3. Update Lieferantennummer in den Eingängen";
            this.btnUpdateLiefNr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateLiefNr.UseVisualStyleBackColor = true;
            this.btnUpdateLiefNr.Click += new System.EventHandler(this.btnUpdateLiefNr_Click);
            // 
            // tbLiefNrNEU
            // 
            this.tbLiefNrNEU.Location = new System.Drawing.Point(167, 47);
            this.tbLiefNrNEU.Name = "tbLiefNrNEU";
            this.tbLiefNrNEU.ReadOnly = true;
            this.tbLiefNrNEU.Size = new System.Drawing.Size(114, 20);
            this.tbLiefNrNEU.TabIndex = 13;
            // 
            // tbLiefNrALT
            // 
            this.tbLiefNrALT.Location = new System.Drawing.Point(167, 22);
            this.tbLiefNrALT.Name = "tbLiefNrALT";
            this.tbLiefNrALT.ReadOnly = true;
            this.tbLiefNrALT.Size = new System.Drawing.Size(114, 20);
            this.tbLiefNrALT.TabIndex = 12;
            // 
            // lLiefNrNEU
            // 
            this.lLiefNrNEU.AutoSize = true;
            this.lLiefNrNEU.Location = new System.Drawing.Point(21, 54);
            this.lLiefNrNEU.Name = "lLiefNrNEU";
            this.lLiefNrNEU.Size = new System.Drawing.Size(126, 13);
            this.lLiefNrNEU.TabIndex = 11;
            this.lLiefNrNEU.Text = "Lieferantennummer NEU:";
            // 
            // lLiefAlt
            // 
            this.lLiefAlt.AutoSize = true;
            this.lLiefAlt.Location = new System.Drawing.Point(21, 25);
            this.lLiefAlt.Name = "lLiefAlt";
            this.lLiefAlt.Size = new System.Drawing.Size(123, 13);
            this.lLiefAlt.TabIndex = 10;
            this.lLiefAlt.Text = "Lieferantennummer ALT:";
            // 
            // btnUpdateGueterart
            // 
            this.btnUpdateGueterart.Location = new System.Drawing.Point(24, 168);
            this.btnUpdateGueterart.Name = "btnUpdateGueterart";
            this.btnUpdateGueterart.Size = new System.Drawing.Size(257, 58);
            this.btnUpdateGueterart.TabIndex = 16;
            this.btnUpdateGueterart.Text = "2. Update Güterarten (Bestellnummer)";
            this.btnUpdateGueterart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateGueterart.UseVisualStyleBackColor = true;
            this.btnUpdateGueterart.Click += new System.EventHandler(this.btnUpdateGueterart_Click);
            // 
            // btnUpdateArticleData
            // 
            this.btnUpdateArticleData.Location = new System.Drawing.Point(24, 104);
            this.btnUpdateArticleData.Name = "btnUpdateArticleData";
            this.btnUpdateArticleData.Size = new System.Drawing.Size(257, 58);
            this.btnUpdateArticleData.TabIndex = 17;
            this.btnUpdateArticleData.Text = "1. Update BestellNr. in Artikeln";
            this.btnUpdateArticleData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdateArticleData.UseVisualStyleBackColor = true;
            this.btnUpdateArticleData.Click += new System.EventHandler(this.btnUpdateArticleData_Click);
            // 
            // ctrAMChange2024
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panAMMain);
            this.Name = "ctrAMChange2024";
            this.Size = new System.Drawing.Size(737, 479);
            this.panAMMain.ResumeLayout(false);
            this.panAMMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panAMMain;
        private System.Windows.Forms.TextBox tbLiefNrNEU;
        private System.Windows.Forms.TextBox tbLiefNrALT;
        private System.Windows.Forms.Label lLiefNrNEU;
        private System.Windows.Forms.Label lLiefAlt;
        private System.Windows.Forms.Button btnUpdateLiefNr;
        private System.Windows.Forms.TextBox tbInfo1;
        private System.Windows.Forms.Button btnUpdateGueterart;
        private System.Windows.Forms.Button btnUpdateArticleData;
    }
}
