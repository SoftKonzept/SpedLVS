namespace Sped4.Controls.ToDo
{
    partial class ctrAnonymousDatabase
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
            this.panAnonymDatabase = new System.Windows.Forms.Panel();
            this.tbLogInfo = new System.Windows.Forms.TextBox();
            this.btnDoAnonymousDatabase = new System.Windows.Forms.Button();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuCronJobList = new Sped4.Controls.AFToolStrip();
            this.tsbtnClear = new System.Windows.Forms.ToolStripButton();
            this.panAnonymDatabase.SuspendLayout();
            this.menuCronJobList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panAnonymDatabase
            // 
            this.panAnonymDatabase.Controls.Add(this.tbLogInfo);
            this.panAnonymDatabase.Controls.Add(this.btnDoAnonymousDatabase);
            this.panAnonymDatabase.Controls.Add(this.tbDatabase);
            this.panAnonymDatabase.Controls.Add(this.tbServer);
            this.panAnonymDatabase.Controls.Add(this.label5);
            this.panAnonymDatabase.Controls.Add(this.label4);
            this.panAnonymDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panAnonymDatabase.Location = new System.Drawing.Point(0, 25);
            this.panAnonymDatabase.Name = "panAnonymDatabase";
            this.panAnonymDatabase.Size = new System.Drawing.Size(677, 475);
            this.panAnonymDatabase.TabIndex = 1;
            // 
            // tbLogInfo
            // 
            this.tbLogInfo.Location = new System.Drawing.Point(34, 128);
            this.tbLogInfo.Multiline = true;
            this.tbLogInfo.Name = "tbLogInfo";
            this.tbLogInfo.Size = new System.Drawing.Size(397, 329);
            this.tbLogInfo.TabIndex = 6;
            // 
            // btnDoAnonymousDatabase
            // 
            this.btnDoAnonymousDatabase.Location = new System.Drawing.Point(34, 87);
            this.btnDoAnonymousDatabase.Name = "btnDoAnonymousDatabase";
            this.btnDoAnonymousDatabase.Size = new System.Drawing.Size(297, 35);
            this.btnDoAnonymousDatabase.TabIndex = 5;
            this.btnDoAnonymousDatabase.Text = "Datenbanken anonymisieren";
            this.btnDoAnonymousDatabase.UseVisualStyleBackColor = true;
            this.btnDoAnonymousDatabase.Click += new System.EventHandler(this.btnDoAnonymousDatabase_Click);
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(137, 57);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(194, 20);
            this.tbDatabase.TabIndex = 4;
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(137, 33);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(194, 20);
            this.tbServer.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(38, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Datenbank:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(38, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Server:";
            // 
            // menuCronJobList
            // 
            this.menuCronJobList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnClear});
            this.menuCronJobList.Location = new System.Drawing.Point(0, 0);
            this.menuCronJobList.myColorFrom = System.Drawing.Color.Azure;
            this.menuCronJobList.myColorTo = System.Drawing.Color.Blue;
            this.menuCronJobList.myUnderlineColor = System.Drawing.Color.White;
            this.menuCronJobList.myUnderlined = true;
            this.menuCronJobList.Name = "menuCronJobList";
            this.menuCronJobList.Size = new System.Drawing.Size(677, 25);
            this.menuCronJobList.TabIndex = 11;
            this.menuCronJobList.Text = "afToolStrip1";
            // 
            // tsbtnClear
            // 
            this.tsbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClear.Image = global::Sped4.Properties.Resources.selection_replace_32x32;
            this.tsbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnClear.Name = "tsbtnClear";
            this.tsbtnClear.Size = new System.Drawing.Size(23, 22);
            this.tsbtnClear.Text = "aktualisieren";
            this.tsbtnClear.Click += new System.EventHandler(this.tsbtnClear_Click);
            // 
            // ctrAnonymousDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panAnonymDatabase);
            this.Controls.Add(this.menuCronJobList);
            this.Name = "ctrAnonymousDatabase";
            this.Size = new System.Drawing.Size(677, 500);
            this.Load += new System.EventHandler(this.ctrAnonymousDatabase_Load);
            this.panAnonymDatabase.ResumeLayout(false);
            this.panAnonymDatabase.PerformLayout();
            this.menuCronJobList.ResumeLayout(false);
            this.menuCronJobList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panAnonymDatabase;
        private AFToolStrip menuCronJobList;
        private System.Windows.Forms.ToolStripButton tsbtnClear;
        private System.Windows.Forms.TextBox tbLogInfo;
        private System.Windows.Forms.Button btnDoAnonymousDatabase;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}
