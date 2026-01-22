using CustomizedUpdates.ArcelorUmstellung2024;
using CustomizedUpdates.MainSystem;
using LVS;
using System;
using System.Windows.Forms;


namespace CustomizedUpdates
{
    public partial class frmMain : Form
    {
        public Globals._GL_SYSTEM GLSystem = new Globals._GL_SYSTEM();
        public Globals._GL_USER GLUser = new Globals._GL_USER();
        public SystemMain systemMain = new SystemMain();
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            systemMain = new SystemMain();
            // initialisieren der DB Daten
            systemMain.InitSystem(ref this.GLSystem);
            OpenCtrAMChange2024();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OpenCtrAMChange2024()
        {
            ctrAMChange2024 _ctrAmChange2024 = new ctrAMChange2024();
            _ctrAmChange2024.Parent = this.pageBmwArcelor;
            _ctrAmChange2024.Dock = DockStyle.Fill;
            _ctrAmChange2024.InitCtr();
            _ctrAmChange2024.Show();
            _ctrAmChange2024.BringToFront();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabMain.SelectedIndex)
            {
                case 0:
                    OpenCtrAMChange2024();
                    break;
                case 1:
                    break;

            }
        }

        private void tsmClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
