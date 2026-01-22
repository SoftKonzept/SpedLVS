namespace Sped4
{
  partial class frmKDFrachtKonditionen
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
        this.tsbNeu = new System.Windows.Forms.ToolStripButton();
        this.tsbSpeichern = new System.Windows.Forms.ToolStripButton();
        this.tsbClose = new System.Windows.Forms.ToolStripButton();
        this.dgv = new Sped4.Controls.AFGrid();
        this.afToolStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
        this.SuspendLayout();
        // 
        // afToolStrip1
        // 
        this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNeu,
            this.tsbSpeichern,
            this.tsbClose});
        this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
        this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
        this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
        this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
        this.afToolStrip1.myUnderlined = true;
        this.afToolStrip1.Name = "afToolStrip1";
        this.afToolStrip1.Size = new System.Drawing.Size(253, 25);
        this.afToolStrip1.TabIndex = 12;
        this.afToolStrip1.Text = "afToolStrip1";
        // 
        // tsbNeu
        // 
        this.tsbNeu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbNeu.Image = global::Sped4.Properties.Resources.add;
        this.tsbNeu.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbNeu.Name = "tsbNeu";
        this.tsbNeu.Size = new System.Drawing.Size(23, 22);
        this.tsbNeu.Text = "Neue Konditionen";
        this.tsbNeu.Click += new System.EventHandler(this.tsbNeu_Click);
        // 
        // tsbSpeichern
        // 
        this.tsbSpeichern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbSpeichern.Image = global::Sped4.Properties.Resources.check;
        this.tsbSpeichern.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbSpeichern.Name = "tsbSpeichern";
        this.tsbSpeichern.Size = new System.Drawing.Size(23, 22);
        this.tsbSpeichern.Text = "Frachtkonditionen speichern";
        this.tsbSpeichern.Click += new System.EventHandler(this.tsbSpeichern_Click);
        // 
        // tsbClose
        // 
        this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsbClose.Image = global::Sped4.Properties.Resources.delete_16;
        this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.tsbClose.Name = "tsbClose";
        this.tsbClose.Size = new System.Drawing.Size(23, 22);
        this.tsbClose.Text = "schliessen";
        this.tsbClose.Click += new System.EventHandler(this.toolStripButton1_Click);
        // 
        // dgv
        // 
        this.dgv.AllowUserToAddRows = false;
        dataGridViewCellStyle1.NullValue = null;
        this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.dgv.BackgroundColor = System.Drawing.Color.White;
        this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgv.Location = new System.Drawing.Point(0, 28);
        this.dgv.Name = "dgv";
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
        this.dgv.RowHeadersVisible = false;
        this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
        this.dgv.Size = new System.Drawing.Size(253, 427);
        this.dgv.TabIndex = 13;
        this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
        this.dgv.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_CellValidating);
        this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
        // 
        // frmKDFrachtKonditionen
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.ClientSize = new System.Drawing.Size(253, 453);
        this.Controls.Add(this.dgv);
        this.Controls.Add(this.afToolStrip1);
        this.Name = "frmKDFrachtKonditionen";
        this.Text = "Eingabe Frachtkonditionen";
        this.Load += new System.EventHandler(this.frmKDFrachtKonditionen_Load);
        this.afToolStrip1.ResumeLayout(false);
        this.afToolStrip1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbNeu;
    private Sped4.Controls.AFGrid dgv;
    private System.Windows.Forms.ToolStripButton tsbClose;
    private System.Windows.Forms.ToolStripButton tsbSpeichern;
  }
}
