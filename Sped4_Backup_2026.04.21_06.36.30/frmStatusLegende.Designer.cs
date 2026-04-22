namespace Sped4
{
    partial class frmStatusLegende
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
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatusLegende));
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
          this.dgv = new Sped4.Controls.AFGrid();
          this.Symbol = new System.Windows.Forms.DataGridViewImageColumn();
          this.Farbe = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Beschreibung = new System.Windows.Forms.DataGridViewTextBoxColumn();
          ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
          this.SuspendLayout();
          // 
          // dgv
          // 
          this.dgv.AllowDrop = true;
          this.dgv.AllowUserToAddRows = false;
          this.dgv.AllowUserToDeleteRows = false;
          this.dgv.AllowUserToOrderColumns = true;
          this.dgv.AllowUserToResizeRows = false;
          this.dgv.BackgroundColor = global::Sped4.Properties.Settings.Default.BackColor;
          this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
          this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Symbol,
            this.Farbe,
            this.Beschreibung});
          this.dgv.DataBindings.Add(new System.Windows.Forms.Binding("BackgroundColor", global::Sped4.Properties.Settings.Default, "BackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
          dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
          dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
          dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
          dataGridViewCellStyle4.ForeColor = System.Drawing.Color.DarkBlue;
          dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
          this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
          this.dgv.Location = new System.Drawing.Point(0, 0);
          this.dgv.MultiSelect = false;
          this.dgv.Name = "dgv";
          this.dgv.ReadOnly = true;
          dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
          dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
          this.dgv.RowHeadersVisible = false;
          this.dgv.RowTemplate.Height = 55;
          this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
          this.dgv.ShowEditingIcon = false;
          this.dgv.ShowRowErrors = false;
          this.dgv.Size = new System.Drawing.Size(225, 611);
          this.dgv.TabIndex = 2;
          this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
          // 
          // Symbol
          // 
          this.Symbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
          dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle2.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle2.NullValue")));
          dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
          this.Symbol.DefaultCellStyle = dataGridViewCellStyle2;
          this.Symbol.HeaderText = "Symbol";
          this.Symbol.Name = "Symbol";
          this.Symbol.ReadOnly = true;
          this.Symbol.Width = 47;
          // 
          // Farbe
          // 
          this.Farbe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
          dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
          dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
          this.Farbe.DefaultCellStyle = dataGridViewCellStyle3;
          this.Farbe.HeaderText = "Farbe";
          this.Farbe.Name = "Farbe";
          this.Farbe.ReadOnly = true;
          this.Farbe.Width = 59;
          // 
          // Beschreibung
          // 
          this.Beschreibung.HeaderText = "";
          this.Beschreibung.Name = "Beschreibung";
          this.Beschreibung.ReadOnly = true;
          this.Beschreibung.Width = 30;
          // 
          // frmStatusLegende
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.ClientSize = new System.Drawing.Size(225, 611);
          this.Controls.Add(this.dgv);
          this.Name = "frmStatusLegende";
          this.Text = "Statuslegenden";
          ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

        private Sped4.Controls.AFGrid dgv;
        private System.Windows.Forms.DataGridViewImageColumn Symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Farbe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Beschreibung;


    }
}
