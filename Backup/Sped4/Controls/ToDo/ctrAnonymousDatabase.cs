using System.Windows.Forms;

namespace Sped4.Controls.ToDo
{
    public partial class ctrAnonymousDatabase : UserControl
    {
        internal ctrMenu _ctrMenu { get; set; } = null;
        public ctrAnonymousDatabase(ctrMenu myCtrMenu)
        {
            InitializeComponent();
            _ctrMenu = myCtrMenu;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrAnonymousDatabase_Load(object sender, System.EventArgs e)
        {
            tbLogInfo.Text = string.Empty;
            btnDoAnonymousDatabase.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnClear_Click(object sender, System.EventArgs e)
        {
            tbLogInfo.Text = string.Empty;
            btnDoAnonymousDatabase.Enabled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoAnonymousDatabase_Click(object sender, System.EventArgs e)
        {
            DialogResult result = MessageBox.Show("Soll die aktuelle Datenbank anoymisiert werden?", "ACHTUNG", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                AnonymousDatabase anonymousDatabase = new AnonymousDatabase(this._ctrMenu._frmMain.GL_User);
                if (anonymousDatabase.Logs.Count > 0)
                {
                    string strLog = string.Empty;
                    foreach (string log in anonymousDatabase.Logs)
                    {
                        strLog = strLog + log;
                    }
                    tbLogInfo.Text = strLog;
                }
                else
                {
                    tbLogInfo.Text = "Es wurden keine Daten anonymisiert!";
                }
            }
            btnDoAnonymousDatabase.Enabled = false;
        }


    }
}
