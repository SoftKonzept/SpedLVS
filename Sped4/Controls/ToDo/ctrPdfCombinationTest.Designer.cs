namespace Sped4.Controls.ToDo
{
    partial class ctrPdfCombinationTest
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
            this.btnPdfCombination = new System.Windows.Forms.Button();
            this.panTestMain = new System.Windows.Forms.Panel();
            this.btnXmlPathSearch = new System.Windows.Forms.Button();
            this.tbXmlPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchAttachemntFileName = new System.Windows.Forms.Button();
            this.btnSearchEInvoiceFilePath = new System.Windows.Forms.Button();
            this.tbPathAttachment = new System.Windows.Forms.TextBox();
            this.tbPathInvoice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panTestMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPdfCombination
            // 
            this.btnPdfCombination.Location = new System.Drawing.Point(34, 125);
            this.btnPdfCombination.Name = "btnPdfCombination";
            this.btnPdfCombination.Size = new System.Drawing.Size(149, 23);
            this.btnPdfCombination.TabIndex = 0;
            this.btnPdfCombination.Text = "Pdf Kombination";
            this.btnPdfCombination.UseVisualStyleBackColor = true;
            this.btnPdfCombination.Click += new System.EventHandler(this.btnPdfCombination_Click);
            // 
            // panTestMain
            // 
            this.panTestMain.BackColor = System.Drawing.Color.White;
            this.panTestMain.Controls.Add(this.btnXmlPathSearch);
            this.panTestMain.Controls.Add(this.tbXmlPath);
            this.panTestMain.Controls.Add(this.label3);
            this.panTestMain.Controls.Add(this.btnSearchAttachemntFileName);
            this.panTestMain.Controls.Add(this.btnSearchEInvoiceFilePath);
            this.panTestMain.Controls.Add(this.tbPathAttachment);
            this.panTestMain.Controls.Add(this.tbPathInvoice);
            this.panTestMain.Controls.Add(this.label2);
            this.panTestMain.Controls.Add(this.label1);
            this.panTestMain.Controls.Add(this.btnPdfCombination);
            this.panTestMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panTestMain.Location = new System.Drawing.Point(0, 0);
            this.panTestMain.Name = "panTestMain";
            this.panTestMain.Size = new System.Drawing.Size(561, 388);
            this.panTestMain.TabIndex = 1;
            // 
            // btnXmlPathSearch
            // 
            this.btnXmlPathSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnXmlPathSearch.Image = global::Sped4.Properties.Resources.magnifying_glass;
            this.btnXmlPathSearch.Location = new System.Drawing.Point(502, 76);
            this.btnXmlPathSearch.Name = "btnXmlPathSearch";
            this.btnXmlPathSearch.Size = new System.Drawing.Size(28, 21);
            this.btnXmlPathSearch.TabIndex = 9;
            this.btnXmlPathSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnXmlPathSearch.UseVisualStyleBackColor = true;
            this.btnXmlPathSearch.Click += new System.EventHandler(this.btnXmlPathSearch_Click);
            // 
            // tbXmlPath
            // 
            this.tbXmlPath.Location = new System.Drawing.Point(123, 77);
            this.tbXmlPath.Name = "tbXmlPath";
            this.tbXmlPath.Size = new System.Drawing.Size(373, 20);
            this.tbXmlPath.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pfad xml:";
            // 
            // btnSearchAttachemntFileName
            // 
            this.btnSearchAttachemntFileName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchAttachemntFileName.Image = global::Sped4.Properties.Resources.magnifying_glass;
            this.btnSearchAttachemntFileName.Location = new System.Drawing.Point(502, 50);
            this.btnSearchAttachemntFileName.Name = "btnSearchAttachemntFileName";
            this.btnSearchAttachemntFileName.Size = new System.Drawing.Size(28, 21);
            this.btnSearchAttachemntFileName.TabIndex = 6;
            this.btnSearchAttachemntFileName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearchAttachemntFileName.UseVisualStyleBackColor = true;
            this.btnSearchAttachemntFileName.Click += new System.EventHandler(this.btnSearchAttachemntFileName_Click);
            // 
            // btnSearchEInvoiceFilePath
            // 
            this.btnSearchEInvoiceFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchEInvoiceFilePath.Image = global::Sped4.Properties.Resources.magnifying_glass;
            this.btnSearchEInvoiceFilePath.Location = new System.Drawing.Point(502, 22);
            this.btnSearchEInvoiceFilePath.Name = "btnSearchEInvoiceFilePath";
            this.btnSearchEInvoiceFilePath.Size = new System.Drawing.Size(28, 21);
            this.btnSearchEInvoiceFilePath.TabIndex = 5;
            this.btnSearchEInvoiceFilePath.UseVisualStyleBackColor = true;
            this.btnSearchEInvoiceFilePath.Click += new System.EventHandler(this.btnSearchEInvoiceFilePath_Click);
            // 
            // tbPathAttachment
            // 
            this.tbPathAttachment.Location = new System.Drawing.Point(123, 51);
            this.tbPathAttachment.Name = "tbPathAttachment";
            this.tbPathAttachment.Size = new System.Drawing.Size(373, 20);
            this.tbPathAttachment.TabIndex = 4;
            // 
            // tbPathInvoice
            // 
            this.tbPathInvoice.Location = new System.Drawing.Point(123, 22);
            this.tbPathInvoice.Name = "tbPathInvoice";
            this.tbPathInvoice.Size = new System.Drawing.Size(373, 20);
            this.tbPathInvoice.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pfad RG-Anhang:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pfad eRG:";
            // 
            // ctrTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panTestMain);
            this.Name = "ctrTest";
            this.Size = new System.Drawing.Size(561, 388);
            this.Load += new System.EventHandler(this.ctrTest_Load);
            this.panTestMain.ResumeLayout(false);
            this.panTestMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPdfCombination;
        private System.Windows.Forms.Panel panTestMain;
        private System.Windows.Forms.TextBox tbPathAttachment;
        private System.Windows.Forms.TextBox tbPathInvoice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchAttachemntFileName;
        private System.Windows.Forms.Button btnSearchEInvoiceFilePath;
        private System.Windows.Forms.Button btnXmlPathSearch;
        private System.Windows.Forms.TextBox tbXmlPath;
        private System.Windows.Forms.Label label3;
    }
}
