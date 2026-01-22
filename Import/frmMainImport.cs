using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LVS;
using Import;

namespace Import
{
    public partial class frmMainImport : Form
    {
        public Globals._GL_SYSTEM GL_System = new Globals._GL_SYSTEM();
        internal clsSystemImport SystemImp;



        internal clsUser ImpUser;
        public frmMainImport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMainImport_Load(object sender, EventArgs e)
        {
            SystemImp = new clsSystemImport(this.GL_System);

            tbSourceServer.Text = SystemImp.con_Server;
            tbSourceDB.Text = "LvsSZG_F16";

            tbDestServer.Text = SystemImp.con_Server;
            tbDestDB.Text = SystemImp.con_Database;

            comboArbeitsbereich.DataSource = clsArbeitsbereiche.GetArbeitsbereichList(this.SystemImp._GL_User.User_ID);
            comboArbeitsbereich.DisplayMember = "Arbeitsbereich";
            comboArbeitsbereich.ValueMember = "ID";
            comboArbeitsbereich.SelectedIndex = 4;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartImport_Click(object sender, EventArgs e)
        {
            this.SystemImp.con_DatabaseImp = tbSourceDB.Text.Trim();
            this.SystemImp.con_ServerImp = tbSourceServer.Text.Trim();
            this.SystemImp.con_PassDBImp = this.SystemImp.GLSystem.con_PassDB;
            this.SystemImp.con_UserImp = this.SystemImp.GLSystem.con_UserDB;

            decimal decTmp = 0;
            decimal.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out decTmp);
            if (decTmp > 0)
            {
                this.SystemImp.AbBereich = new clsArbeitsbereiche();
                this.SystemImp.AbBereich.InitCls(this.SystemImp.GLUser, decTmp);
                this.SystemImp.GLSystem.sys_ArbeitsbereichID = this.SystemImp.AbBereich.ID;

                this.SystemImp.GLSystem.sys_ArbeitsbereichID = this.SystemImp.AbBereich.ID;
                this.SystemImp.GLSystem.sys_MandantenID  = this.SystemImp.AbBereich.MandantenID;
                this.SystemImp.GLSystem.sys_Arbeitsbereich_ASNTransfer = this.SystemImp.AbBereich.ASNTransfer;

                this.SystemImp.Import_CreateNewDestDB = cbCreateNewDatabase.Checked;
                this.SystemImp.Import_GutOnlyIsUsed = cbGutImportIsUsedOnly.Checked;
                this.SystemImp.CalcDateToKeep = this.dtpCalcDateToKeep.Value;

                if (clsMessages.DoProzess())
                {

                    clsLvsImport import = new clsLvsImport(this.SystemImp);
                    import.SetProzessInforamtionHandler += Import_SetProzessInforamtionHandler;
                    import.Start();

                    //---- Log in Datei schreiben
                    string strLogPath = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_Log.txt";
                    File.WriteAllLines(strLogPath, import.ListLog);
                    if (File.Exists(strLogPath))
                    {
                        Process.Start(@"notepad.exe", strLogPath);
                    }
                }
            }
        }
        /// <summary>
        ///                 aktuallisiert die INfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Import_SetProzessInforamtionHandler(object sender, List<string> e)
        {
            this.lbLog.DataSource = null;
            this.lbLog.DataSource = e;
            //throw new NotImplementedException();
        }
        /// <summary>
        ///             Import form schliessen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnASNConnection_Click(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            decimal.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out decTmp);
            if (decTmp > 0)
            {
                this.SystemImp.AbBereich = new clsArbeitsbereiche();
                this.SystemImp.AbBereich.InitCls(this.SystemImp.GLUser, decTmp);
                this.SystemImp.GLSystem.sys_ArbeitsbereichID = this.SystemImp.AbBereich.ID;

                this.SystemImp.GLSystem.sys_ArbeitsbereichID = this.SystemImp.AbBereich.ID;
                this.SystemImp.GLSystem.sys_MandantenID = this.SystemImp.AbBereich.MandantenID;
                this.SystemImp.GLSystem.sys_Arbeitsbereich_ASNTransfer = this.SystemImp.AbBereich.ASNTransfer;

                //if (this.SystemImp.CheckConnectionLVSOld)
                //{
                    if ((this.SystemImp.CheckConnectionSped) && (this.SystemImp.CheckConnectionCom))
                    {
                        impASNAction action = new impASNAction(this.SystemImp);
                        action.SetProzessInforamtionHandler += Import_SetProzessInforamtionHandler;
                        action.DoImport();
                    }
                //}
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLM_Click(object sender, EventArgs e)
        {
            decimal decTmp = 0;
            decimal.TryParse(comboArbeitsbereich.SelectedValue.ToString(), out decTmp);
            if (decTmp > 0)
            {
                this.SystemImp.AbBereich = new clsArbeitsbereiche();
                this.SystemImp.AbBereich.InitCls(this.SystemImp.GLUser, decTmp);
                this.SystemImp.GLSystem.sys_ArbeitsbereichID = this.SystemImp.AbBereich.ID;

                this.SystemImp.GLSystem.sys_ArbeitsbereichID = this.SystemImp.AbBereich.ID;
                this.SystemImp.GLSystem.sys_MandantenID = this.SystemImp.AbBereich.MandantenID;
                this.SystemImp.GLSystem.sys_Arbeitsbereich_ASNTransfer = this.SystemImp.AbBereich.ASNTransfer;

                if ((this.SystemImp.CheckConnectionSped) && (this.SystemImp.CheckConnectionCom))
                {
                    impArtikel art = new impArtikel(this.SystemImp);
                    art.SetProzessInforamtionHandler += Import_SetProzessInforamtionHandler;
                    art.CreateMissingLM();


                }

            }

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //frmADRSelection AdrSelection = new frmADRSelection();
            //AdrSelection.Show();
            //AdrSelection.BringToFront();
            //AdrSelection.Focus();
        }
    }
}
