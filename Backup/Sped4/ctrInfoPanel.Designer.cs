namespace Sped4
{
    partial class ctrInfoPanel
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.pbElement = new Telerik.WinControls.UI.RadProgressBarElement();
            this.barText = new Telerik.WinControls.UI.RadLabelElement();
            this.tbInfoTxt = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbInfoTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.pbElement,
            this.barText});
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 248);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Size = new System.Drawing.Size(439, 24);
            this.radStatusStrip1.TabIndex = 0;
            this.radStatusStrip1.Text = "radStatusStrip1";
            // 
            // pbElement
            // 
            this.pbElement.AutoSize = false;
            this.pbElement.Bounds = new System.Drawing.Rectangle(0, 0, 250, 18);
            this.pbElement.DefaultSize = new System.Drawing.Size(300, 20);
            this.pbElement.Name = "pbElement";
            this.pbElement.SeparatorColor1 = System.Drawing.Color.White;
            this.pbElement.SeparatorColor2 = System.Drawing.Color.White;
            this.pbElement.SeparatorColor3 = System.Drawing.Color.White;
            this.pbElement.SeparatorColor4 = System.Drawing.Color.White;
            this.pbElement.SeparatorGradientAngle = 0;
            this.pbElement.SeparatorGradientPercentage1 = 0.4F;
            this.pbElement.SeparatorGradientPercentage2 = 0.6F;
            this.pbElement.SeparatorNumberOfColors = 2;
            this.radStatusStrip1.SetSpring(this.pbElement, false);
            this.pbElement.StepWidth = 14;
            this.pbElement.SweepAngle = 90;
            this.pbElement.Text = "";
            this.pbElement.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // barText
            // 
            this.barText.AccessibleDescription = "barText";
            this.barText.AccessibleName = "barText";
            this.barText.Name = "barText";
            this.radStatusStrip1.SetSpring(this.barText, false);
            this.barText.Text = "barText";
            this.barText.TextWrap = true;
            this.barText.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // tbInfoTxt
            // 
            this.tbInfoTxt.AutoScroll = true;
            this.tbInfoTxt.AutoSize = false;
            this.tbInfoTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInfoTxt.Location = new System.Drawing.Point(0, 0);
            this.tbInfoTxt.Multiline = true;
            this.tbInfoTxt.Name = "tbInfoTxt";
            this.tbInfoTxt.Size = new System.Drawing.Size(439, 248);
            this.tbInfoTxt.TabIndex = 1;
            // 
            // ctrInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbInfoTxt);
            this.Controls.Add(this.radStatusStrip1);
            this.Name = "ctrInfoPanel";
            this.Size = new System.Drawing.Size(439, 272);
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbInfoTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        public Telerik.WinControls.UI.RadProgressBarElement pbElement;
        public Telerik.WinControls.UI.RadLabelElement barText;
        public Telerik.WinControls.UI.RadTextBox tbInfoTxt;
    }
}
