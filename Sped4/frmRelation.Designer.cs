namespace Sped4
{
  partial class frmRelation
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
        this.components = new System.ComponentModel.Container();
        this.tbRelation = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.miUpdate = new System.Windows.Forms.ToolStripMenuItem();
        this.miExportExcel = new System.Windows.Forms.ToolStripMenuItem();
        this.miCloseCtr = new System.Windows.Forms.ToolStripMenuItem();
        this.tsArtikeldatenMenu = new Sped4.Controls.AFToolStrip();
        this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
        this.tsbtnFrmClose = new System.Windows.Forms.ToolStripButton();
        this.contextMenuStrip1.SuspendLayout();
        this.tsArtikeldatenMenu.SuspendLayout();
        this.SuspendLayout();
        // 
        // tbRelation
        // 
        this.tbRelation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.tbRelation.Location = new System.Drawing.Point(101, 48);
        this.tbRelation.Name = "tbRelation";
        this.tbRelation.Size = new System.Drawing.Size(157, 20);
        this.tbRelation.TabIndex = 1;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.ForeColor = System.Drawing.Color.DarkBlue;
        this.label2.Location = new System.Drawing.Point(26, 50);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(46, 13);
        this.label2.TabIndex = 66;
        this.label2.Text = "Relation";
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUpdate,
            this.miExportExcel,
            this.miCloseCtr});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
        this.contextMenuStrip1.Size = new System.Drawing.Size(68, 70);
        // 
        // miUpdate
        // 
        this.miUpdate.Name = "miUpdate";
        this.miUpdate.Size = new System.Drawing.Size(67, 22);
        // 
        // miExportExcel
        // 
        this.miExportExcel.Name = "miExportExcel";
        this.miExportExcel.Size = new System.Drawing.Size(67, 22);
        // 
        // miCloseCtr
        // 
        this.miCloseCtr.Name = "miCloseCtr";
        this.miCloseCtr.Size = new System.Drawing.Size(67, 22);
        // 
        // tsArtikeldatenMenu
        // 
        this.tsArtikeldatenMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSave,
            this.tsbtnFrmClose});
        this.tsArtikeldatenMenu.Location = new System.Drawing.Point(0, 0);
        this.tsArtikeldatenMenu.myColorFrom = System.Drawing.Color.Azure;
        this.tsArtikeldatenMenu.myColorTo = System.Drawing.Color.Blue;
        this.tsArtikeldatenMenu.myUnderlineColor = System.Drawing.Color.White;
        this.tsArtikeldatenMenu.myUnderlined = true;
        this.tsArtikeldatenMenu.Name = "tsArtikeldatenMenu";
        this.tsArtikeldatenMenu.Size = new System.Drawing.Size(303, 25);
        this.tsArtikeldatenMenu.TabIndex = 171;
        this.tsArtikeldatenMenu.Text = "afToolStrip3";
        // 
        // tsbtnSave
        // 
        this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbtnSave.Image = global::Sped4.Properties.Resources.check;
        this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbtnSave.Name = "tsbtnSave";
        this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
        this.tsbtnSave.Text = "toolStripButton1";
        this.tsbtnSave.Click += new System.EventHandler(this.toolStripButton1_Click);
        // 
        // tsbtnFrmClose
        // 
        this.tsbtnFrmClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbtnFrmClose.Image = global::Sped4.Properties.Resources.delete;
        this.tsbtnFrmClose.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbtnFrmClose.Name = "tsbtnFrmClose";
        this.tsbtnFrmClose.Size = new System.Drawing.Size(23, 22);
        this.tsbtnFrmClose.Text = "schliessen";
        this.tsbtnFrmClose.Click += new System.EventHandler(this.tsbtnFrmClose_Click);
        // 
        // frmRelation
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.ClientSize = new System.Drawing.Size(303, 92);
        this.Controls.Add(this.tsArtikeldatenMenu);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.tbRelation);
        this.Name = "frmRelation";
        this.Text = "Relation";
        this.contextMenuStrip1.ResumeLayout(false);
        this.tsArtikeldatenMenu.ResumeLayout(false);
        this.tsArtikeldatenMenu.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbRelation;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem miUpdate;
    private System.Windows.Forms.ToolStripMenuItem miExportExcel;
    private System.Windows.Forms.ToolStripMenuItem miCloseCtr;
    private Sped4.Controls.AFToolStrip tsArtikeldatenMenu;
    private System.Windows.Forms.ToolStripButton tsbtnFrmClose;
    private System.Windows.Forms.ToolStripButton tsbtnSave;
  }
}
