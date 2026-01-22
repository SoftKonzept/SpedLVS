using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFButton_Form : Sped4.Controls.AFButton
    {
        public AFButton_Form()
        {
            InitializeComponent();
        }

        private Form _MyForm;

        public Form MyForm
        {
            get
            {
                return _MyForm;
            }
            set
            {
                _MyForm = value;
            }
        }

        private void AFButton_Form_Click()
        {
            if (MyForm != null)
            {
                MyForm.Focus();
            }            
        }

    }
}
