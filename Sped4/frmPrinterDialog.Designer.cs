namespace Sped4
{
    partial class frmPrinterDialog
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
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbPrinter = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbView = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cbFormat = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnClose = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            this.afToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.Location = new System.Drawing.Point(0, 0);
            // 
            // dgv
            // 
            this.dgv.MasterTemplate.AllowAddNewRow = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            // 
            // 
            // 
            this.dgv.RootElement.AccessibleDescription = null;
            this.dgv.RootElement.AccessibleName = null;
            this.dgv.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.dgv.Size = new System.Drawing.Size(717, 362);
            this.dgv.TabIndex = 140;
            this.dgv.Text = "radGridView1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(717, 362);
            this.panel2.TabIndex = 7;
            // 
            // afToolStrip1
            // 
            this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cbPrinter,
            this.toolStripLabel2,
            this.cbView,
            this.toolStripLabel3,
            this.cbFormat,
            this.tsbtnPrint,
            this.tsBtnClose});
            this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip1.myUnderlined = true;
            this.afToolStrip1.Name = "afToolStrip1";
            this.afToolStrip1.Size = new System.Drawing.Size(717, 25);
            this.afToolStrip1.TabIndex = 8;
            this.afToolStrip1.Text = "afToolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 22);
            this.toolStripLabel1.Text = "Drucker";
            // 
            // cbPrinter
            // 
            this.cbPrinter.Name = "cbPrinter";
            this.cbPrinter.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel2.Text = "Ansicht";
            // 
            // cbView
            // 
            this.cbView.Name = "cbView";
            this.cbView.Size = new System.Drawing.Size(121, 25);
            this.cbView.SelectedIndexChanged += new System.EventHandler(this.cbView_SelectedIndexChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel3.Text = "Format";
            // 
            // cbFormat
            // 
            this.cbFormat.Items.AddRange(new object[] {
            "Querformat",
            "Hochformat"});
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrint.Image = global::Sped4.Properties.Resources.printer2_24x24;
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
            this.tsBtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // frmPrinterDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 362);
            this.Controls.Add(this.afToolStrip1);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "frmPrinterDialog";
            this.Text = "frmPrinterDialog";
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel2.ResumeLayout(false);
            this.afToolStrip1.ResumeLayout(false);
            this.afToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView dgv;
        private System.Windows.Forms.Panel panel2;
        private Controls.AFToolStrip afToolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbPrinter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbView;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsBtnClose;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cbFormat;
    }
}