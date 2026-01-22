namespace Sped4
{
  partial class frmStatusInfoAuftrag
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lGesamtgewicht = new System.Windows.Forms.Label();
      this.lAuftragID = new System.Windows.Forms.Label();
      this.dgv = new Sped4.Controls.AFGrid();
      this.col1 = new System.Windows.Forms.DataGridViewImageColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(44, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Auftrag:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 27);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(83, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Gesamtgewicht:";
      // 
      // lGesamtgewicht
      // 
      this.lGesamtgewicht.AutoSize = true;
      this.lGesamtgewicht.Location = new System.Drawing.Point(115, 28);
      this.lGesamtgewicht.Name = "lGesamtgewicht";
      this.lGesamtgewicht.Size = new System.Drawing.Size(35, 13);
      this.lGesamtgewicht.TabIndex = 4;
      this.lGesamtgewicht.Text = "label2";
      // 
      // lAuftragID
      // 
      this.lAuftragID.AutoSize = true;
      this.lAuftragID.Location = new System.Drawing.Point(115, 9);
      this.lAuftragID.Name = "lAuftragID";
      this.lAuftragID.Size = new System.Drawing.Size(35, 13);
      this.lAuftragID.TabIndex = 3;
      this.lAuftragID.Text = "label2";
      // 
      // dgv
      // 
      this.dgv.AllowDrop = true;
      this.dgv.AllowUserToAddRows = false;
      this.dgv.AllowUserToDeleteRows = false;
      this.dgv.AllowUserToResizeRows = false;
      this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
      this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
      this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1,
            this.Column1,
            this.Column2,
            this.Column3});
      this.dgv.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      dataGridViewCellStyle8.ForeColor = System.Drawing.Color.DarkBlue;
      dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv.DefaultCellStyle = dataGridViewCellStyle8;
      this.dgv.Location = new System.Drawing.Point(0, 48);
      this.dgv.MultiSelect = false;
      this.dgv.Name = "dgv";
      this.dgv.ReadOnly = true;
      this.dgv.RowHeadersVisible = false;
      this.dgv.RowTemplate.Height = 55;
      this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv.ShowEditingIcon = false;
      this.dgv.ShowRowErrors = false;
      this.dgv.Size = new System.Drawing.Size(279, 415);
      this.dgv.TabIndex = 2;
      this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
      // 
      // col1
      // 
      this.col1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.col1.HeaderText = "Info";
      this.col1.Name = "col1";
      this.col1.ReadOnly = true;
      this.col1.Width = 31;
      // 
      // Column1
      // 
      this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.Column1.DefaultCellStyle = dataGridViewCellStyle5;
      this.Column1.HeaderText = "Auftrag";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.Width = 66;
      // 
      // Column2
      // 
      this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.Column2.DefaultCellStyle = dataGridViewCellStyle6;
      this.Column2.HeaderText = "Position";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.Width = 69;
      // 
      // Column3
      // 
      this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
      this.Column3.DefaultCellStyle = dataGridViewCellStyle7;
      this.Column3.HeaderText = "Positionsgewicht";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.Width = 111;
      // 
      // frmStatusInfoAuftrag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(277, 462);
      this.Controls.Add(this.dgv);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lGesamtgewicht);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lAuftragID);
      this.ForeColor = System.Drawing.Color.DarkBlue;
      this.Name = "frmStatusInfoAuftrag";
      this.Text = "Statusübersicht je Auftrag";
      ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Sped4.Controls.AFGrid dgv;
    private System.Windows.Forms.DataGridViewImageColumn col1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lAuftragID;
    private System.Windows.Forms.Label lGesamtgewicht;
    private System.Windows.Forms.Label label2;
  }
}