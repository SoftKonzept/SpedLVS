using Sped4.Classes;
using System;
using System.Drawing;

namespace Sped4
{
    public partial class frmSettingsView : Sped4.frmTEMPLATE
    {
        internal ctrMenu _ctrMenu;


        public frmSettingsView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Sped4.Properties.Settings.Default.BackColor = colorDialog1.Color;
            this._ctrMenu._frmMain.RefreshColor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Sped4.Properties.Settings.Default.BaseColor = colorDialog1.Color;
            this._ctrMenu._frmMain.RefreshColor();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Sped4.Properties.Settings.Default.BaseColor2 = colorDialog1.Color;
            this._ctrMenu._frmMain.RefreshColor();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Sped4.Properties.Settings.Default.EffectColor = colorDialog1.Color;
            this._ctrMenu._frmMain.RefreshColor();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Sped4.Properties.Settings.Default.EffectColor2 = colorDialog1.Color;
            this._ctrMenu._frmMain.RefreshColor();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            Sped4.Properties.Settings.Default.BackColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Sped4.Properties.Settings.Default.BaseColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Sped4.Properties.Settings.Default.BaseColor2 = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Sped4.Properties.Settings.Default.EffectColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            Sped4.Properties.Settings.Default.EffectColor2 = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

            this._ctrMenu._frmMain.RefreshColor();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Sped4.Properties.Settings.Default.BackColor = Globals.DefaultBackColor;
            //Sped4.Properties.Settings.Default.BaseColor = Globals.DefaultBaseColor;
            //Sped4.Properties.Settings.Default.BaseColor2 = Globals.DefaultBaseColor2;
            //Sped4.Properties.Settings.Default.EffectColor = Globals.DefaultEffectColor;
            //Sped4.Properties.Settings.Default.EffectColor2 = Globals.DefaultEffectColor2;

            helper_PropertySettings.SetBackColor_Default(this._ctrMenu._frmMain);
            this._ctrMenu._frmMain.RefreshColor();
        }


    }
}
