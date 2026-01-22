using LVS;
using System;
using System.Windows.Forms;


namespace Sped4
{
    public partial class ctrInfoPanel : UserControl
    {
        public clsComTecCheck comCheck;
        public frmTmp _frmTmp;

        public ctrInfoPanel()
        {
            InitializeComponent();
        }
        ///<summary>ctrInfoPanel / InitCtr</summary>
        ///<remarks></remarks>
        public void InitCtr()
        {

        }
        ///<summary>ctrInfoPanel / AddInfoText</summary>
        ///<remarks>Udate Infotext</remarks>
        public void AddInfoText(string strAddTxt)
        {
            string strTxt = this.tbInfoTxt.Text;
            strAddTxt = Functions.FormatShortDateTime(DateTime.Now) + strAddTxt;
            strTxt = Environment.NewLine + this.tbInfoTxt.Text;
            this.tbInfoTxt.Text = strAddTxt + strTxt;
        }
        ///<summary>ctrInfoPanel / ClearInfoTxt</summary>
        ///<remarks>Udate Infotext</remarks>
        public void ClearInfoTxt()
        {
            this.tbInfoTxt.Text = string.Empty;
        }
        ///<summary>ctrInfoPanel / ClearInfoTxt</summary>
        ///<remarks>Udate Infotext</remarks>
        public void InitProcessBar(Int32 myMaximum, Int32 myMinimum)
        {
            this.pbElement.Minimum = myMinimum;
            this.pbElement.Maximum = myMaximum;
            this.pbElement.Value1 = 0;
        }
        ///<summary>ctrInfoPanel / DoProgressBar</summary>
        ///<remarks>Fortschritt Progressbar</remarks>
        public void DoProgressBar()
        {
            if (this.pbElement.Value1 < this.pbElement.Maximum)
            {
                this.pbElement.Value1++;
            }
        }
        ///<summary>ctrInfoPanel / ClearProgressBarLabel</summary>
        ///<remarks></remarks>
        public void ClearProgressBarLabel()
        {
            this.barText.Text = string.Empty;
        }
        ///<summary>ctrInfoPanel / SetProgressBarText</summary>
        ///<remarks></remarks>
        public void SetProgressBarText(string strTxt)
        {
            this.barText.Text = strTxt;
        }
    }
}
