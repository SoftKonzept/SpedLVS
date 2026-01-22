namespace Sped4
{
    partial class frmTestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestForm));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.mstripOne = new Sped4.Controls.AFToolStrip();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tslTest = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnPrintPDF = new System.Windows.Forms.ToolStripButton();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.tsbtnPrintTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.mstripOne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mstripOne
            // 
            this.mstripOne.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPrint,
            this.tsBtnClose,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.tslTest,
            this.toolStripSeparator1,
            this.tsbtnPrintPDF});
            this.mstripOne.Location = new System.Drawing.Point(2, 2);
            this.mstripOne.myColorFrom = System.Drawing.Color.Azure;
            this.mstripOne.myColorTo = System.Drawing.Color.Blue;
            this.mstripOne.myUnderlineColor = System.Drawing.Color.White;
            this.mstripOne.myUnderlined = true;
            this.mstripOne.Name = "mstripOne";
            this.mstripOne.Size = new System.Drawing.Size(480, 25);
            this.mstripOne.TabIndex = 142;
            this.mstripOne.Text = "afToolStrip1";
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrint.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPrint.Text = "Liste drucken";
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // tsBtnClose
            // 
            this.tsBtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnClose.Name = "tsBtnClose";
            this.tsBtnClose.Size = new System.Drawing.Size(23, 22);
            this.tsBtnClose.Text = "Druck abbrechen";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "BestFit..";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Autosize...";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // tslTest
            // 
            this.tslTest.Name = "tslTest";
            this.tslTest.Size = new System.Drawing.Size(86, 22);
            this.tslTest.Text = "toolStripLabel1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnPrintPDF
            // 
            this.tsbtnPrintPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrintPDF.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPrintPDF.Image")));
            this.tsbtnPrintPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrintPDF.Name = "tsbtnPrintPDF";
            this.tsbtnPrintPDF.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPrintPDF.Text = "Test Print PDF";
            this.tsbtnPrintPDF.DoubleClick += new System.EventHandler(this.tsbtnPrintPDF_DoubleClick);
            // 
            // dgv
            // 
            this.dgv.AutoScroll = true;
            this.dgv.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.Location = new System.Drawing.Point(2, 27);
            // 
            // 
            // 
            this.dgv.MasterTemplate.AllowAddNewRow = false;
            this.dgv.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgv.MasterTemplate.ShowGroupedColumns = true;
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            // 
            // 
            // 
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 28, 240, 150);
            this.dgv.Size = new System.Drawing.Size(480, 379);
            this.dgv.TabIndex = 144;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(492, 434);
            this.tabControl1.TabIndex = 145;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv);
            this.tabPage1.Controls.Add(this.mstripOne);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(484, 408);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.afToolStrip1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(484, 408);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPrintTest,
            this.toolStripButton4});
            this.afToolStrip1.Location = new System.Drawing.Point(2, 2);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(480, 25);
            this.afToolStrip1.TabIndex = 143;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // tsbtnPrintTest
            // 
            this.tsbtnPrintTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrintTest.Image = global::Sped4.Properties.Resources.check;
            this.tsbtnPrintTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrintTest.Name = "tsbtnPrintTest";
            this.tsbtnPrintTest.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPrintTest.Text = "Print Test";
            this.tsbtnPrintTest.Click += new System.EventHandler(this.tsbtnPrintTest_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::Sped4.Properties.Resources.application_enterprise_32x32;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Print";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // frmTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 434);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmTestForm";
            this.Text = "frmTestForm";
            this.Load += new System.EventHandler(this.frmTestForm_Load);
            this.mstripOne.ResumeLayout(false);
            this.mstripOne.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AFToolStrip mstripOne;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsBtnClose;
        public Telerik.WinControls.UI.RadGridView dgv;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripLabel tslTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnPrintPDF;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnPrintTest;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
    }
}