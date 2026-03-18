namespace Sped4
{
    partial class ctrFormList
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnCtrActiv = new Sped4.Controls.AFButton_Form();
            this.btnCtrPassiv = new Sped4.Controls.AFButton_Form();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(551, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCtrActiv
            // 
            this.btnCtrActiv.Abgerundet = false;
            this.btnCtrActiv.Active = false;
            this.btnCtrActiv.BackColor = System.Drawing.Color.White;
            this.btnCtrActiv.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCtrActiv.IsActiv = false;
            this.btnCtrActiv.Location = new System.Drawing.Point(200, 0);
            this.btnCtrActiv.myBorderSize = 1;
            this.btnCtrActiv.myColorActivate = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCtrActiv.myColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCtrActiv.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCtrActiv.MyForm = null;
            this.btnCtrActiv.MyFormObject = null;
            this.btnCtrActiv.myText = "AFButton";
            this.btnCtrActiv.Name = "btnCtrActiv";
            this.btnCtrActiv.Pic = null;
            this.btnCtrActiv.Size = new System.Drawing.Size(200, 21);
            this.btnCtrActiv.TabIndex = 2;
            this.btnCtrActiv.MouseClick += new Sped4.Controls.AFButton.MouseEventHandler(this.btnCtrActiv_MouseClick);
            // 
            // btnCtrPassiv
            // 
            this.btnCtrPassiv.Abgerundet = false;
            this.btnCtrPassiv.Active = false;
            this.btnCtrPassiv.BackColor = System.Drawing.Color.White;
            this.btnCtrPassiv.DataBindings.Add(new System.Windows.Forms.Binding("myColorBase", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnCtrPassiv.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCtrPassiv.IsActiv = false;
            this.btnCtrPassiv.Location = new System.Drawing.Point(0, 0);
            this.btnCtrPassiv.myBorderSize = 1;
            this.btnCtrPassiv.myColorActivate = System.Drawing.Color.RosyBrown;
            this.btnCtrPassiv.myColorBase = global::Sped4.Properties.Settings.Default.BaseColor;
            this.btnCtrPassiv.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCtrPassiv.MyForm = null;
            this.btnCtrPassiv.MyFormObject = null;
            this.btnCtrPassiv.myText = "Auftrag: 123456";
            this.btnCtrPassiv.Name = "btnCtrPassiv";
            this.btnCtrPassiv.Pic = null;
            this.btnCtrPassiv.Size = new System.Drawing.Size(200, 21);
            this.btnCtrPassiv.TabIndex = 1;
            this.btnCtrPassiv.MouseClick += new Sped4.Controls.AFButton.MouseEventHandler(this.btnCtrPassiv_MouseClick);
            // 
            // ctrFormList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCtrActiv);
            this.Controls.Add(this.btnCtrPassiv);
            this.Name = "ctrFormList";
            this.Size = new System.Drawing.Size(626, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private Sped4.Controls.AFButton_Form btnCtrPassiv;
        private Sped4.Controls.AFButton_Form btnCtrActiv;
    }
}
