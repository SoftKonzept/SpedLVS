using LVS;
using System;
using System.Windows.Forms;

namespace Sped4
{
    public partial class ctrPrintLagerOrtLabel : UserControl
    {
        public Globals._GL_USER GL_User;
        public ctrMenu _ctrMenu;
        private string _DocFileNameAndPath;
        internal clsReportDocSetting RepDocSettings;
        public string Path { get; set; }             // Value / Wert zum Key in INIdoc
        public string ReportFileName { get; set; }
        public string DocFileNameAndPath
        {
            get
            {
                _DocFileNameAndPath = this.Path;
                _DocFileNameAndPath = _DocFileNameAndPath + this.ReportFileName;
                return _DocFileNameAndPath;
            }
        }
        public ctrPrintLagerOrtLabel()
        {
            InitializeComponent();
            this.checkBoxLeft.Checked = true;
        }

        private void tsbtnDirectPrint_Click(object sender, EventArgs e)
        {
            this._ctrMenu._frmMain.system.ReportDocSetting.InitClass(this.GL_User, this._ctrMenu._frmMain.GL_System, this._ctrMenu._frmMain.system, 0, this._ctrMenu._frmMain.system.AbBereich.ID);
            if (this.checkBoxLeft.Checked)
            {
                RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.LOLabelLinks.ToString());
            }
            else if (this.checkBoxUpLeft.Checked)
            {
                RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.LOLabelLinksOben.ToString());
            }
            else if (this.checkBoxRight.Checked)
            {
                RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.LOLabelRechts.ToString());
            }
            else if (this.checkBoxDownRight.Checked)
            {
                RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.LOLabelRechtsUnten.ToString());
            }
            else if (this.checkBoxBothLeftDownRightUp.Checked)
            {
                RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.LOLabelBeide.ToString());
            }
            else if (this.checkBoxBothLeftUpRightDown.Checked)
            {
                RepDocSettings = this._ctrMenu._frmMain.system.ReportDocSetting.GetClassByDocKey(enumIniDocKey.LOLabelBeideLinksObenRechtsUnten.ToString());
            }
            else
            {
                return;
            }
            if (RepDocSettings != null)
            {
                Path = RepDocSettings.Path;
                ReportFileName = RepDocSettings.ReportFileName;
            }
            this._ctrMenu.OpenFrmReporView(this, false);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
