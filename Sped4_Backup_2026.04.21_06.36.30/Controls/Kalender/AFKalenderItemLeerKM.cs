using LVS;
using Sped4.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sped4.Controls
{
    public partial class AFKalenderItemLeerKM : Sped4.Controls.AFKalenderItem
    {
        public const string const_ctrName = "KalenderItemLeerKM";
        public new Globals._GL_USER GL_User;
        public delegate void ThreadCtrInvokeEventHandler();
        internal clsLKM LKM = new clsLKM();
        //frmDispoKalender Kalender;

        frmDispo Kalender;
        public ctrMenu ctrMenu;

        public delegate void ctrAuftragRefreshEventHandler();
        public event ctrAuftragRefreshEventHandler ctrAuftragRefresh;

        //public AFKalenderItemLeerKM(frmDispoKalender _Kalender)
        public AFKalenderItemLeerKM(frmDispo _Kalender)
        {
            InitializeComponent();
            Kalender = _Kalender;
            this.Name = AFKalenderItemLeerKM.const_ctrName;
        }
        ///<summary>AFKalenderItemLeerKM / AFKalenderItemLeerKM_Load</summary>
        ///<remarks>.</remarks>
        private void AFKalenderItemLeerKM_Load(object sender, EventArgs e)
        {
            this.ColorFrom = Color.Red;
            this.ColorTo = Color.GhostWhite;
            this.Height = 15;
        }
        ///<summary>AFKalenderItemLeerKM / AFKalenderItemLeerKM_Load</summary>
        ///<remarks>.</remarks>
        private void AFKalenderItemLeerKM_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AFKalenderItemLeerKM_MouseClick(object sender, MouseEventArgs e)
        {
            ToolTip info = new ToolTip();
            info.SetToolTip(this, this.LKM.MouseOverInfo);
        }



    }
}

