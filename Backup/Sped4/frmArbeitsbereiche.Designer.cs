namespace Sped4
{
    partial class frmArbeitsbereiche
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArbeitsbereiche));
            this.splitContainerAB = new System.Windows.Forms.SplitContainer();
            this.afToolStrip2 = new Sped4.Controls.AFToolStrip();
            this.tsbtnShowHideAbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnNew = new System.Windows.Forms.ToolStripButton();
            this.tsbSpeichern = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClearFrm = new System.Windows.Forms.ToolStripButton();
            this.tsbtnClose = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAB)).BeginInit();
            this.splitContainerAB.SuspendLayout();
            this.afToolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerAB
            // 
            resources.ApplyResources(this.splitContainerAB, "splitContainerAB");
            this.splitContainerAB.Name = "splitContainerAB";
            // 
            // splitContainerAB.Panel1
            // 
            resources.ApplyResources(this.splitContainerAB.Panel1, "splitContainerAB.Panel1");
            // 
            // splitContainerAB.Panel2
            // 
            resources.ApplyResources(this.splitContainerAB.Panel2, "splitContainerAB.Panel2");
            this.splitContainerAB.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerAB_SplitterMoved);
            // 
            // afToolStrip2
            // 
            resources.ApplyResources(this.afToolStrip2, "afToolStrip2");
            this.afToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnShowHideAbAdd,
            this.tsbtnNew,
            this.tsbSpeichern,
            this.tsbtnClearFrm,
            this.tsbtnClose});
            this.afToolStrip2.myColorFrom = System.Drawing.Color.Azure;
            this.afToolStrip2.myColorTo = System.Drawing.Color.Blue;
            this.afToolStrip2.myUnderlineColor = System.Drawing.Color.White;
            this.afToolStrip2.myUnderlined = true;
            this.afToolStrip2.Name = "afToolStrip2";
            // 
            // tsbtnShowHideAbAdd
            // 
            resources.ApplyResources(this.tsbtnShowHideAbAdd, "tsbtnShowHideAbAdd");
            this.tsbtnShowHideAbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnShowHideAbAdd.Image = global::Sped4.Properties.Resources.layout;
            this.tsbtnShowHideAbAdd.Name = "tsbtnShowHideAbAdd";
            this.tsbtnShowHideAbAdd.Click += new System.EventHandler(this.tsbtnShowHideAbAdd_Click);
            // 
            // tsbtnNew
            // 
            resources.ApplyResources(this.tsbtnNew, "tsbtnNew");
            this.tsbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNew.Image = global::Sped4.Properties.Resources.add;
            this.tsbtnNew.Name = "tsbtnNew";
            this.tsbtnNew.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbSpeichern
            // 
            resources.ApplyResources(this.tsbSpeichern, "tsbSpeichern");
            this.tsbSpeichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSpeichern.Image = global::Sped4.Properties.Resources.check;
            this.tsbSpeichern.Name = "tsbSpeichern";
            this.tsbSpeichern.Click += new System.EventHandler(this.tsbSpeichern_Click);
            // 
            // tsbtnClearFrm
            // 
            resources.ApplyResources(this.tsbtnClearFrm, "tsbtnClearFrm");
            this.tsbtnClearFrm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClearFrm.Image = global::Sped4.Properties.Resources.form_green_delete;
            this.tsbtnClearFrm.Name = "tsbtnClearFrm";
            this.tsbtnClearFrm.Click += new System.EventHandler(this.tsbtnClearFrm_Click);
            // 
            // tsbtnClose
            // 
            resources.ApplyResources(this.tsbtnClose, "tsbtnClose");
            this.tsbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnClose.Image = global::Sped4.Properties.Resources.delete;
            this.tsbtnClose.Name = "tsbtnClose";
            this.tsbtnClose.Click += new System.EventHandler(this.tsbtnClose_Click);
            // 
            // frmArbeitsbereiche
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerAB);
            this.Controls.Add(this.afToolStrip2);
            this.Name = "frmArbeitsbereiche";
            this.Load += new System.EventHandler(this.frmArbeitsbereiche_Load);
            this.ResizeEnd += new System.EventHandler(this.frmArbeitsbereiche_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerAB)).EndInit();
            this.splitContainerAB.ResumeLayout(false);
            this.afToolStrip2.ResumeLayout(false);
            this.afToolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.AFToolStrip afToolStrip2;
        private System.Windows.Forms.ToolStripButton tsbSpeichern;
        private System.Windows.Forms.ToolStripButton tsbtnShowHideAbAdd;
        private System.Windows.Forms.ToolStripButton tsbtnClose;
        private System.Windows.Forms.SplitContainer splitContainerAB;
        private System.Windows.Forms.ToolStripButton tsbtnClearFrm;
        private System.Windows.Forms.ToolStripButton tsbtnNew;

    }
}