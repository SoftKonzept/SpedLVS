using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class frmMenge : Sped4.frmTEMPLATE
    {
        public frmMenge()
        {
            InitializeComponent();
        }

        public decimal Menge
        {
            get { return Convert.ToDecimal(TextBox1.Text); }
            set { TextBox1.Text = value.ToString(); }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    Menge = Convert.ToDecimal(TextBox1.Text);
                    this.Close();
                }
            }
            catch
            {
            }
        }


    }
}
