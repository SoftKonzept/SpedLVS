namespace Import
{
    partial class frmADRSelection
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnAdd = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.btnCloseFrm = new Telerik.WinControls.UI.RadMenuButtonItem();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.tbADR_Ort = new System.Windows.Forms.TextBox();
            this.tbADR_PLZ = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbADR_F1 = new System.Windows.Forms.TextBox();
            this.tbADR_F2 = new System.Windows.Forms.TextBox();
            this.tbADR_STRA = new System.Windows.Forms.TextBox();
            this.tbADR_SUCHB = new System.Windows.Forms.TextBox();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.dgvSelection = new Telerik.WinControls.UI.RadGridView();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelection.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Text = "Übernehmen";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCloseFrm
            // 
            this.btnCloseFrm.Name = "btnCloseFrm";
            this.btnCloseFrm.Text = "close";
            this.btnCloseFrm.Click += new System.EventHandler(this.btnCloseFrm_Click);
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 26);
            this.radSplitContainer1.Name = "radSplitContainer1";
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainer1.Size = new System.Drawing.Size(800, 424);
            this.radSplitContainer1.TabIndex = 1;
            this.radSplitContainer1.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.White;
            this.splitPanel1.Controls.Add(this.label7);
            this.splitPanel1.Controls.Add(this.tbADR_Ort);
            this.splitPanel1.Controls.Add(this.tbADR_PLZ);
            this.splitPanel1.Controls.Add(this.label6);
            this.splitPanel1.Controls.Add(this.label5);
            this.splitPanel1.Controls.Add(this.label4);
            this.splitPanel1.Controls.Add(this.label2);
            this.splitPanel1.Controls.Add(this.label1);
            this.splitPanel1.Controls.Add(this.tbADR_F1);
            this.splitPanel1.Controls.Add(this.tbADR_F2);
            this.splitPanel1.Controls.Add(this.tbADR_STRA);
            this.splitPanel1.Controls.Add(this.tbADR_SUCHB);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(398, 424);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Ort";
            // 
            // tbADR_Ort
            // 
            this.tbADR_Ort.Location = new System.Drawing.Point(106, 177);
            this.tbADR_Ort.Name = "tbADR_Ort";
            this.tbADR_Ort.Size = new System.Drawing.Size(275, 20);
            this.tbADR_Ort.TabIndex = 13;
            // 
            // tbADR_PLZ
            // 
            this.tbADR_PLZ.Location = new System.Drawing.Point(106, 151);
            this.tbADR_PLZ.Name = "tbADR_PLZ";
            this.tbADR_PLZ.Size = new System.Drawing.Size(275, 20);
            this.tbADR_PLZ.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Name1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Name2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Straße";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "PLZ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "ViewID";
            // 
            // tbADR_F1
            // 
            this.tbADR_F1.Location = new System.Drawing.Point(106, 73);
            this.tbADR_F1.Name = "tbADR_F1";
            this.tbADR_F1.Size = new System.Drawing.Size(275, 20);
            this.tbADR_F1.TabIndex = 4;
            // 
            // tbADR_F2
            // 
            this.tbADR_F2.Location = new System.Drawing.Point(106, 99);
            this.tbADR_F2.Name = "tbADR_F2";
            this.tbADR_F2.Size = new System.Drawing.Size(275, 20);
            this.tbADR_F2.TabIndex = 3;
            // 
            // tbADR_STRA
            // 
            this.tbADR_STRA.Location = new System.Drawing.Point(106, 125);
            this.tbADR_STRA.Name = "tbADR_STRA";
            this.tbADR_STRA.Size = new System.Drawing.Size(275, 20);
            this.tbADR_STRA.TabIndex = 2;
            // 
            // tbADR_SUCHB
            // 
            this.tbADR_SUCHB.Location = new System.Drawing.Point(106, 47);
            this.tbADR_SUCHB.Name = "tbADR_SUCHB";
            this.tbADR_SUCHB.Size = new System.Drawing.Size(275, 20);
            this.tbADR_SUCHB.TabIndex = 0;
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.dgvSelection);
            this.splitPanel2.Location = new System.Drawing.Point(402, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(398, 424);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // dgvSelection
            // 
            this.dgvSelection.AutoSizeRows = true;
            this.dgvSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelection.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgvSelection.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.dgvSelection.Name = "dgvSelection";
            this.dgvSelection.Size = new System.Drawing.Size(398, 424);
            this.dgvSelection.TabIndex = 0;
            this.dgvSelection.CreateRow += new Telerik.WinControls.UI.GridViewCreateRowEventHandler(this.dgvSelection_CreateRow);
            this.dgvSelection.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvSelection_CellClick);
            // 
            // frmADRSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            // 
            // radMenu1
            // 
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnAdd,
            this.btnCloseFrm});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(800, 26);
            this.radMenu1.TabIndex = 0;
            this.Controls.Add(this.radSplitContainer1);
            this.Controls.Add(this.radMenu1);
            this.Name = "frmADRSelection";
            this.Text = "Auswahl Adresse";
            this.Load += new System.EventHandler(this.frmADRSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelection.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadMenuButtonItem btnAdd;
        private Telerik.WinControls.UI.RadMenuButtonItem btnCloseFrm;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbADR_Ort;
        private System.Windows.Forms.TextBox tbADR_PLZ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbADR_F1;
        private System.Windows.Forms.TextBox tbADR_F2;
        private System.Windows.Forms.TextBox tbADR_STRA;
        private System.Windows.Forms.TextBox tbADR_SUCHB;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadGridView dgvSelection;
        private Telerik.WinControls.UI.RadMenu radMenu1;
    }
}