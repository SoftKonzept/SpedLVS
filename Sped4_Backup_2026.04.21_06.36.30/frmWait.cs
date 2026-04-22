using LVS;
using System;
using System.Threading;
using System.Windows.Forms;


namespace Sped4
{
    public partial class frmWait : frmTEMPLATE
    {
        public frmMAIN _frmMain;
        public delegate void ThreadCtrInvokeEventHandler();

        public frmWait()
        {
            InitializeComponent();
        }
        ///<summary>frmWait / frmWait_Load</summary>
        ///<remarks>Info Sped4 start wird angezeigt.</remarks>
        private void frmWait_Load(object sender, EventArgs e)
        {
            this.Refresh();
            this.Show();
            this.BringToFront();
            Thread.Sleep(1000);

            if (this._frmMain != null)
            {
                AddInfo("Sped 4 System wird initialisiert!");
                this._frmMain.system = new clsSystem(Application.StartupPath);
                //this._frmMain.system.InitDBConnection(ref this._frmMain.GL_System);

                Int32 iTry = 1;
                while (
                        (!this._frmMain.system.InitDBConnection(ref this._frmMain.GL_System, ref this._frmMain.system.listLogToFileSystem)) &&
                        (iTry <= 5)
                      )
                {
                    Thread.Sleep(1000);
                    AddInfo(iTry.ToString() + ". Versuch eine Datenbankverbindung hergestellt ist fehlgeschlagen.");
                    iTry++;
                    if (iTry == 6)
                    {
                        AddInfo("Keine Datenbankverbidnung möglich");
                        Thread.Sleep(500);
                        CloseFrm();
                        Application.Exit();
                    }
                }

                AddInfo("Datenbankverbindung wurde hergestellt!");
                AddInfo("Sped 4 gestartet!");
                Thread.Sleep(3000);
                //Login Form wird ertellt
                this._frmMain.OpenFrmLogin();
            }
            else
            {
                //tbInfo.Text = string.Empty;
                AddInfo("Bitte warten...");
                AddInfo("Aktion wird durchgeführt...");
                this.Focus();
            }
            Thread.Sleep(2000);
        }
        ///<summary>frmWait / AddInfo</summary>
        ///<remarks>Informationen werden dem Infofeld mit Datum und Uhrzeit hinzugefügt.</remarks>
        ///<param name="myInfo">anzuzeigende Info</param>
        public void AddInfo(string myInfo)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            AddInfo(myInfo);
                                                                        }
                                                                    )
                                 );
                return;
            }
            //if (tbInfo.Text == string.Empty)
            // {
            //     tbInfo.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + myInfo;
            // }
            // else
            // {
            //     tbInfo.Text = tbInfo.Text +
            //                   Environment.NewLine +
            //                  DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + myInfo;

            // }
        }
        ///<summary>frmWait / CloseFrm</summary>
        ///<remarks></remarks>
        ///<param name="myInfo"></param>
        public void CloseFrm()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(
                                    new ThreadCtrInvokeEventHandler(
                                                                        delegate ()
                                                                        {
                                                                            this.CloseFrm();
                                                                        }
                                                                    )
                                 );
                return;
            }
            this.Close();
        }

    }
}
