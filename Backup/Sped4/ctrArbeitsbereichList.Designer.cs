namespace Sped4
{
    partial class ctrArbeitsbereichList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.panABList = new System.Windows.Forms.Panel();
            this.dgv = new Telerik.WinControls.UI.RadGridView();
            this.panABList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // panABList
            // 
            this.panABList.BackColor = System.Drawing.Color.Gainsboro;
            this.panABList.Controls.Add(this.dgv);
            this.panABList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panABList.Location = new System.Drawing.Point(0, 0);
            this.panABList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panABList.Name = "panABList";
            this.panABList.Size = new System.Drawing.Size(585, 521);
            this.panABList.TabIndex = 4;
            // 
            // dgv
            // 
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // 
            // 
            this.dgv.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(585, 521);
            this.dgv.TabIndex = 0;
            this.dgv.ThemeName = "ControlDefault";
            this.dgv.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // ctrArbeitsbereichList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panABList);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ctrArbeitsbereichList";
            this.Size = new System.Drawing.Size(585, 521);
            this.Load += new System.EventHandler(this.ctrArbeitsbereichList_Load);
            this.panABList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panABList;
        public Telerik.WinControls.UI.RadGridView dgv;
    }
}
