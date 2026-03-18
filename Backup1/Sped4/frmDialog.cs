using System.Windows.Forms;

namespace Sped4
{
    public partial class frmDialog : Form
    {
        public ctrGridPrinter ctrGridPrinter;
        public ctrArbeistlisteAufgaben ctrArbeitsListeAufgaben;
        public ctrMenu ctrMenu;
        public ctrArbeitsliste ctrArbeitsliste;
        public frmDialog(object myObj, ctrMenu _ctrMenu, Control myControl = null)
        {


            if (myControl != null)
            {
                if (typeof(ctrArbeitsliste) == myControl.GetType())
                {
                    ctrArbeitsliste = (ctrArbeitsliste)myControl;
                }
            }
            ctrMenu = _ctrMenu;
            Control tmp = (Control)myObj;
            // tmp.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

            this.Height = tmp.Height;
            this.Width = tmp.Width;

            tmp.Dock = DockStyle.Fill;

            this.AutoSize = true;
            this.SuspendLayout();
            if (myObj.GetType() == typeof(ctrGridPrinter))
            {
                ctrGridPrinter = (ctrGridPrinter)tmp;
                ctrGridPrinter.add(this);
                this.Controls.Add(ctrGridPrinter);
            }
            if (myObj.GetType() == typeof(ctrArbeistlisteAufgaben))
            {

                ctrArbeitsListeAufgaben = (ctrArbeistlisteAufgaben)tmp;
                ctrArbeitsListeAufgaben.add(this);
                this.Controls.Add(ctrArbeitsListeAufgaben);
            }


            this.ResumeLayout(false);

        }
    }

}
