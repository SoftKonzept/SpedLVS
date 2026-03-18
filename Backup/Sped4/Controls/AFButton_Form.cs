using System;
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

        private Object _MyFormObject;

        public Object MyFormObject
        {
            get
            {
                return _MyFormObject;
            }
            set
            {
                _MyFormObject = value;
            }
        }

        private bool _IsActiv;

        public bool IsActiv
        {
            get
            {
                return _IsActiv;
            }
            set
            {
                _IsActiv = value;
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
