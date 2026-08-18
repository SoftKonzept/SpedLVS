namespace Sped4.Controls.ToDo
{
    partial class ctrUserlistMailCredentials
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
            this.btnCreateCredentials = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tbProcessStatus = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnCreateCredentials
            // 
            this.btnCreateCredentials.Location = new System.Drawing.Point(16, 14);
            this.btnCreateCredentials.Name = "btnCreateCredentials";
            this.btnCreateCredentials.Size = new System.Drawing.Size(262, 23);
            this.btnCreateCredentials.TabIndex = 0;
            this.btnCreateCredentials.Text = "button1";
            this.btnCreateCredentials.UseVisualStyleBackColor = true;
            this.btnCreateCredentials.Click += new System.EventHandler(this.btnCreateCredentials_Click_1);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(0, 43);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(554, 340);
            this.tbLog.TabIndex = 1;
            // 
            // tbProcessStatus
            // 
            this.tbProcessStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbProcessStatus.Location = new System.Drawing.Point(0, 389);
            this.tbProcessStatus.Name = "tbProcessStatus";
            this.tbProcessStatus.Size = new System.Drawing.Size(557, 23);
            this.tbProcessStatus.TabIndex = 2;
            // 
            // ctrUserlistMailCredentials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbProcessStatus);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnCreateCredentials);
            this.Name = "ctrUserlistMailCredentials";
            this.Size = new System.Drawing.Size(557, 412);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateCredentials;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.ProgressBar tbProcessStatus;
    }
}
