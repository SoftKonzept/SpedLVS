namespace Sped4
{
    partial class ctrMenu
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
            Telerik.WinControls.UI.ListViewDetailColumn listViewDetailColumn2 = new Telerik.WinControls.UI.ListViewDetailColumn("Column 0", "Column 0");
            this.lvMenu = new Telerik.WinControls.UI.RadListView();
            this.panSubMenu = new System.Windows.Forms.Panel();
            this.panDefault = new System.Windows.Forms.Panel();
            this.btnFakturierung = new Sped4.Controls.AFButton();
            this.btnLager = new Sped4.Controls.AFButton();
            this.btnDispo = new Sped4.Controls.AFButton();
            this.btnSpedition = new Sped4.Controls.AFButton();
            this.btnStatistik = new Sped4.Controls.AFButton();
            this.btnStammdaten = new Sped4.Controls.AFButton();
            this.lblCaption = new Sped4.Controls.AFColorLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lvMenu)).BeginInit();
            this.panSubMenu.SuspendLayout();
            this.panDefault.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMenu
            // 
            this.lvMenu.AllowEdit = false;
            this.lvMenu.AutoScroll = true;
            listViewDetailColumn2.HeaderText = "Column 0";
            listViewDetailColumn2.Width = 120F;
            this.lvMenu.Columns.AddRange(new Telerik.WinControls.UI.ListViewDetailColumn[] {
            listViewDetailColumn2});
            this.lvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMenu.GroupItemSize = new System.Drawing.Size(120, 20);
            this.lvMenu.ItemSize = new System.Drawing.Size(120, 30);
            this.lvMenu.ItemSpacing = 5;
            this.lvMenu.Location = new System.Drawing.Point(0, 0);
            this.lvMenu.Name = "lvMenu";
            this.lvMenu.Padding = new System.Windows.Forms.Padding(10);
            // 
            // 
            // 
            this.lvMenu.RootElement.MaxSize = new System.Drawing.Size(0, 0);
            this.lvMenu.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.lvMenu.SelectLastAddedItem = false;
            this.lvMenu.Size = new System.Drawing.Size(210, 425);
            this.lvMenu.TabIndex = 34;
            this.lvMenu.ThemeName = "Fluent";
            this.lvMenu.ItemMouseClick += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListView1_ItemMouseClick);
            // 
            // panSubMenu
            // 
            this.panSubMenu.Controls.Add(this.panDefault);
            this.panSubMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panSubMenu.Location = new System.Drawing.Point(0, 37);
            this.panSubMenu.Name = "panSubMenu";
            this.panSubMenu.Size = new System.Drawing.Size(210, 425);
            this.panSubMenu.TabIndex = 28;
            // 
            // panDefault
            // 
            this.panDefault.Controls.Add(this.lvMenu);
            this.panDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panDefault.Location = new System.Drawing.Point(0, 0);
            this.panDefault.Name = "panDefault";
            this.panDefault.Size = new System.Drawing.Size(210, 425);
            this.panDefault.TabIndex = 28;
            // 
            // btnFakturierung
            // 
            this.btnFakturierung.Abgerundet = false;
            this.btnFakturierung.Active = false;
            this.btnFakturierung.BackColor = System.Drawing.Color.White;
            this.btnFakturierung.DataBindings.Add(new System.Windows.Forms.Binding("myColorBase", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnFakturierung.DataBindings.Add(new System.Windows.Forms.Binding("myColorActivate", global::Sped4.Properties.Settings.Default, "BaseColor2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnFakturierung.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnFakturierung.Location = new System.Drawing.Point(0, 462);
            this.btnFakturierung.myBorderSize = 1;
            this.btnFakturierung.myColorActivate = global::Sped4.Properties.Settings.Default.BaseColor2;
            this.btnFakturierung.myColorBase = global::Sped4.Properties.Settings.Default.BaseColor;
            this.btnFakturierung.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFakturierung.myText = "Fakturierung";
            this.btnFakturierung.Name = "btnFakturierung";
            this.btnFakturierung.Pic = global::Sped4.Properties.Resources.folder2;
            this.btnFakturierung.Size = new System.Drawing.Size(210, 33);
            this.btnFakturierung.TabIndex = 15;
            this.btnFakturierung.Click += new Sped4.Controls.AFButton.ClickEventHandler(this.btnFakturierung_Click);
            // 
            // btnLager
            // 
            this.btnLager.Abgerundet = false;
            this.btnLager.Active = false;
            this.btnLager.BackColor = System.Drawing.Color.White;
            this.btnLager.DataBindings.Add(new System.Windows.Forms.Binding("myColorBase", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnLager.DataBindings.Add(new System.Windows.Forms.Binding("myColorActivate", global::Sped4.Properties.Settings.Default, "BaseColor2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnLager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLager.Location = new System.Drawing.Point(0, 495);
            this.btnLager.myBorderSize = 1;
            this.btnLager.myColorActivate = global::Sped4.Properties.Settings.Default.BaseColor2;
            this.btnLager.myColorBase = global::Sped4.Properties.Settings.Default.BaseColor;
            this.btnLager.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLager.myText = "Lagerverwaltung";
            this.btnLager.Name = "btnLager";
            this.btnLager.Pic = global::Sped4.Properties.Resources.shelf;
            this.btnLager.Size = new System.Drawing.Size(210, 32);
            this.btnLager.TabIndex = 12;
            this.btnLager.Click += new Sped4.Controls.AFButton.ClickEventHandler(this.btnLager_Click);
            // 
            // btnDispo
            // 
            this.btnDispo.Abgerundet = false;
            this.btnDispo.Active = false;
            this.btnDispo.BackColor = System.Drawing.Color.White;
            this.btnDispo.DataBindings.Add(new System.Windows.Forms.Binding("myColorBase", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnDispo.DataBindings.Add(new System.Windows.Forms.Binding("myColorActivate", global::Sped4.Properties.Settings.Default, "BaseColor2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnDispo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDispo.Location = new System.Drawing.Point(0, 527);
            this.btnDispo.myBorderSize = 1;
            this.btnDispo.myColorActivate = global::Sped4.Properties.Settings.Default.BaseColor2;
            this.btnDispo.myColorBase = global::Sped4.Properties.Settings.Default.BaseColor;
            this.btnDispo.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDispo.myText = "Disposition";
            this.btnDispo.Name = "btnDispo";
            this.btnDispo.Pic = global::Sped4.Properties.Resources.cubes;
            this.btnDispo.Size = new System.Drawing.Size(210, 33);
            this.btnDispo.TabIndex = 2;
            this.btnDispo.Click += new Sped4.Controls.AFButton.ClickEventHandler(this.btnDispo_Click);
            // 
            // btnSpedition
            // 
            this.btnSpedition.Abgerundet = false;
            this.btnSpedition.Active = false;
            this.btnSpedition.BackColor = System.Drawing.Color.White;
            this.btnSpedition.DataBindings.Add(new System.Windows.Forms.Binding("myColorBase", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnSpedition.DataBindings.Add(new System.Windows.Forms.Binding("myColorActivate", global::Sped4.Properties.Settings.Default, "BaseColor2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnSpedition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSpedition.Location = new System.Drawing.Point(0, 560);
            this.btnSpedition.myBorderSize = 1;
            this.btnSpedition.myColorActivate = global::Sped4.Properties.Settings.Default.BaseColor2;
            this.btnSpedition.myColorBase = global::Sped4.Properties.Settings.Default.BaseColor;
            this.btnSpedition.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpedition.myText = "Spedition";
            this.btnSpedition.Name = "btnSpedition";
            this.btnSpedition.Pic = global::Sped4.Properties.Resources.money;
            this.btnSpedition.Size = new System.Drawing.Size(210, 36);
            this.btnSpedition.TabIndex = 7;
            this.btnSpedition.Click += new Sped4.Controls.AFButton.ClickEventHandler(this.btnAuftrag_Click);
            // 
            // btnStatistik
            // 
            this.btnStatistik.Abgerundet = false;
            this.btnStatistik.Active = false;
            this.btnStatistik.BackColor = System.Drawing.Color.White;
            this.btnStatistik.DataBindings.Add(new System.Windows.Forms.Binding("myColorActivate", global::Sped4.Properties.Settings.Default, "BaseColor2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnStatistik.DataBindings.Add(new System.Windows.Forms.Binding("myColorBase", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnStatistik.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStatistik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(72)))), ((int)(((byte)(117)))));
            this.btnStatistik.Location = new System.Drawing.Point(0, 596);
            this.btnStatistik.myBorderSize = 1;
            this.btnStatistik.myColorActivate = global::Sped4.Properties.Settings.Default.BaseColor2;
            this.btnStatistik.myColorBase = global::Sped4.Properties.Settings.Default.BaseColor;
            this.btnStatistik.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatistik.myText = "Statistik";
            this.btnStatistik.Name = "btnStatistik";
            this.btnStatistik.Pic = global::Sped4.Properties.Resources.shopping_cart;
            this.btnStatistik.Size = new System.Drawing.Size(210, 37);
            this.btnStatistik.TabIndex = 11;
            this.btnStatistik.Click += new Sped4.Controls.AFButton.ClickEventHandler(this.btnStatistik_Click);
            // 
            // btnStammdaten
            // 
            this.btnStammdaten.Abgerundet = false;
            this.btnStammdaten.Active = false;
            this.btnStammdaten.BackColor = System.Drawing.Color.White;
            this.btnStammdaten.DataBindings.Add(new System.Windows.Forms.Binding("myColorBase", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnStammdaten.DataBindings.Add(new System.Windows.Forms.Binding("myColorActivate", global::Sped4.Properties.Settings.Default, "BaseColor2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.btnStammdaten.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStammdaten.Location = new System.Drawing.Point(0, 633);
            this.btnStammdaten.myBorderSize = 1;
            this.btnStammdaten.myColorActivate = global::Sped4.Properties.Settings.Default.BaseColor2;
            this.btnStammdaten.myColorBase = global::Sped4.Properties.Settings.Default.BaseColor;
            this.btnStammdaten.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStammdaten.myText = "Stammdaten";
            this.btnStammdaten.Name = "btnStammdaten";
            this.btnStammdaten.Pic = global::Sped4.Properties.Resources.form_green;
            this.btnStammdaten.Size = new System.Drawing.Size(210, 32);
            this.btnStammdaten.TabIndex = 13;
            this.btnStammdaten.Click += new Sped4.Controls.AFButton.ClickEventHandler(this.btnStammdaten_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.DataBindings.Add(new System.Windows.Forms.Binding("myColorTo", global::Sped4.Properties.Settings.Default, "BaseColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblCaption.DataBindings.Add(new System.Windows.Forms.Binding("myUnderlineColor", global::Sped4.Properties.Settings.Default, "BaseColor2", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblCaption.DataBindings.Add(new System.Windows.Forms.Binding("myColorFrom", global::Sped4.Properties.Settings.Default, "EffectColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCaption.Location = new System.Drawing.Point(0, 0);
            this.lblCaption.myColorFrom = global::Sped4.Properties.Settings.Default.EffectColor;
            this.lblCaption.myColorTo = global::Sped4.Properties.Settings.Default.BaseColor;
            this.lblCaption.myFontStyle = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.myText = "Stammdaten";
            this.lblCaption.myUnderlineColor = global::Sped4.Properties.Settings.Default.BaseColor2;
            this.lblCaption.myUnderlined = true;
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(210, 37);
            this.lblCaption.TabIndex = 8;
            this.lblCaption.Text = "afColorLabel1";
            // 
            // ctrMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panSubMenu);
            this.Controls.Add(this.btnFakturierung);
            this.Controls.Add(this.btnLager);
            this.Controls.Add(this.btnDispo);
            this.Controls.Add(this.btnSpedition);
            this.Controls.Add(this.btnStatistik);
            this.Controls.Add(this.btnStammdaten);
            this.Controls.Add(this.lblCaption);
            this.Name = "ctrMenu";
            this.Size = new System.Drawing.Size(210, 665);
            ((System.ComponentModel.ISupportInitialize)(this.lvMenu)).EndInit();
            this.panSubMenu.ResumeLayout(false);
            this.panDefault.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sped4.Controls.AFButton btnDispo;
        private Sped4.Controls.AFButton btnSpedition;
        private Sped4.Controls.AFColorLabel lblCaption;
        private Sped4.Controls.AFButton btnStatistik;
        private Sped4.Controls.AFButton btnLager;
        private Sped4.Controls.AFButton btnStammdaten;
        private Sped4.Controls.AFButton btnFakturierung;
        private System.Windows.Forms.Panel panSubMenu;
        private System.Windows.Forms.Panel panDefault;
        private Telerik.WinControls.UI.RadListView lvMenu;
      
    }
}
