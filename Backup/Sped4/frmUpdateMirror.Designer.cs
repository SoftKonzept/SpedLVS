namespace Sped4
{
    partial class frmUpdateMirror
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
            this.btnStartUpdate = new System.Windows.Forms.Button();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.tbInfoTyp = new System.Windows.Forms.TextBox();
            this.tbInfoDaten = new System.Windows.Forms.TextBox();
            this.btnAbbruch = new System.Windows.Forms.Button();
            this.panelMessages = new System.Windows.Forms.Panel();
            this.tbMessages = new System.Windows.Forms.TextBox();
            this.panelInfo.SuspendLayout();
            this.panelMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartUpdate
            // 
            this.btnStartUpdate.Image = global::Sped4.Properties.Resources.check;
            this.btnStartUpdate.Location = new System.Drawing.Point(324, 12);
            this.btnStartUpdate.Name = "btnStartUpdate";
            this.btnStartUpdate.Size = new System.Drawing.Size(55, 35);
            this.btnStartUpdate.TabIndex = 7;
            this.btnStartUpdate.Tag = "Update starten";
            this.btnStartUpdate.UseVisualStyleBackColor = true;
            this.btnStartUpdate.Click += new System.EventHandler(this.btnStartUpdate_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.tbInfoDaten);
            this.panelInfo.Controls.Add(this.tbInfoTyp);
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(318, 128);
            this.panelInfo.TabIndex = 8;
            // 
            // tbInfoTyp
            // 
            this.tbInfoTyp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbInfoTyp.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbInfoTyp.Location = new System.Drawing.Point(0, 0);
            this.tbInfoTyp.Multiline = true;
            this.tbInfoTyp.Name = "tbInfoTyp";
            this.tbInfoTyp.Size = new System.Drawing.Size(318, 57);
            this.tbInfoTyp.TabIndex = 9;
            this.tbInfoTyp.Text = "TypInfo";
            // 
            // tbInfoDaten
            // 
            this.tbInfoDaten.BackColor = System.Drawing.Color.White;
            this.tbInfoDaten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInfoDaten.Location = new System.Drawing.Point(0, 57);
            this.tbInfoDaten.Multiline = true;
            this.tbInfoDaten.Name = "tbInfoDaten";
            this.tbInfoDaten.ReadOnly = true;
            this.tbInfoDaten.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbInfoDaten.Size = new System.Drawing.Size(318, 71);
            this.tbInfoDaten.TabIndex = 10;
            this.tbInfoDaten.Text = "Test Text";
            // 
            // btnAbbruch
            // 
            this.btnAbbruch.Image = global::Sped4.Properties.Resources.delete_16;
            this.btnAbbruch.Location = new System.Drawing.Point(324, 53);
            this.btnAbbruch.Name = "btnAbbruch";
            this.btnAbbruch.Size = new System.Drawing.Size(55, 35);
            this.btnAbbruch.TabIndex = 8;
            this.btnAbbruch.UseVisualStyleBackColor = true;
            this.btnAbbruch.Click += new System.EventHandler(this.btnAbbruch_Click);
            // 
            // panelMessages
            // 
            this.panelMessages.Controls.Add(this.tbMessages);
            this.panelMessages.Location = new System.Drawing.Point(0, 134);
            this.panelMessages.Name = "panelMessages";
            this.panelMessages.Size = new System.Drawing.Size(387, 343);
            this.panelMessages.TabIndex = 9;
            // 
            // tbMessages
            // 
            this.tbMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMessages.Location = new System.Drawing.Point(0, 0);
            this.tbMessages.Multiline = true;
            this.tbMessages.Name = "tbMessages";
            this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessages.Size = new System.Drawing.Size(387, 343);
            this.tbMessages.TabIndex = 0;
            // 
            // frmUpdateMirror
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 477);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.btnAbbruch);
            this.Controls.Add(this.btnStartUpdate);
            this.Controls.Add(this.panelMessages);
            this.Name = "frmUpdateMirror";
            this.Text = "Updatemonitor";
            this.Load += new System.EventHandler(this.frmUpdateMirror_Load);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.panelMessages.ResumeLayout(false);
            this.panelMessages.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartUpdate;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.TextBox tbInfoTyp;
        private System.Windows.Forms.Button btnAbbruch;
        private System.Windows.Forms.Panel panelMessages;
        private System.Windows.Forms.TextBox tbMessages;
        private System.Windows.Forms.TextBox tbInfoDaten;
    }
}