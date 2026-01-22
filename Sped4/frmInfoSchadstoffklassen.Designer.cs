namespace Sped4
{
  partial class frmInfoSchadstoffklassen
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
      this.afToolStrip1 = new Sped4.Controls.AFToolStrip();
      this.tsbclose = new System.Windows.Forms.ToolStripButton();
      this.panel1 = new System.Windows.Forms.Panel();
      this.dgv = new Sped4.Controls.AFGrid();
      this.Farbe = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.afToolStrip1.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
      this.SuspendLayout();
      // 
      // afToolStrip1
      // 
      this.afToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbclose});
      this.afToolStrip1.Location = new System.Drawing.Point(0, 0);
      this.afToolStrip1.myColorFrom = System.Drawing.Color.Azure;
      this.afToolStrip1.myColorTo = System.Drawing.Color.Blue;
      this.afToolStrip1.myUnderlineColor = System.Drawing.Color.White;
      this.afToolStrip1.myUnderlined = true;
      this.afToolStrip1.Name = "afToolStrip1";
      this.afToolStrip1.Size = new System.Drawing.Size(210, 25);
      this.afToolStrip1.TabIndex = 10;
      this.afToolStrip1.Text = "afToolStrip1";
      this.afToolStrip1.UseWaitCursor = true;
      // 
      // tsbclose
      // 
      this.tsbclose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbclose.Image = global::Sped4.Properties.Resources.delete;
      this.tsbclose.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbclose.Name = "tsbclose";
      this.tsbclose.Size = new System.Drawing.Size(23, 22);
      this.tsbclose.Text = "Info schliessen";
      this.tsbclose.Click += new System.EventHandler(this.tsbSpeichern_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.dgv);
      this.panel1.Location = new System.Drawing.Point(0, 25);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(210, 136);
      this.panel1.TabIndex = 11;
      this.panel1.UseWaitCursor = true;
      // 
      // dgv
      // 
      this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Farbe});
      this.dgv.Location = new System.Drawing.Point(0, 0);
      this.dgv.MultiSelect = false;
      this.dgv.Name = "dgv";
      this.dgv.ReadOnly = true;
      this.dgv.RowHeadersVisible = false;
      this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv.Size = new System.Drawing.Size(210, 107);
      this.dgv.TabIndex = 0;
      this.dgv.UseWaitCursor = true;
      this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
      // 
      // Farbe
      // 
      this.Farbe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
      this.Farbe.DefaultCellStyle = dataGridViewCellStyle1;
      this.Farbe.HeaderText = "Farbe";
      this.Farbe.Name = "Farbe";
      this.Farbe.ReadOnly = true;
      // 
      // frmInfoSchadstoffklassen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(210, 131);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.afToolStrip1);
      this.Name = "frmInfoSchadstoffklassen";
      this.Text = "Info Schadstoffklassen";
      this.UseWaitCursor = true;
      this.Load += new System.EventHandler(this.frmInfoSchadstoffklassen_Load);
      this.afToolStrip1.ResumeLayout(false);
      this.afToolStrip1.PerformLayout();
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Sped4.Controls.AFToolStrip afToolStrip1;
    private System.Windows.Forms.ToolStripButton tsbclose;
    private System.Windows.Forms.Panel panel1;
    private Sped4.Controls.AFGrid dgv;
    private System.Windows.Forms.DataGridViewTextBoxColumn Farbe;
  }
}