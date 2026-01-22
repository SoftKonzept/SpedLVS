using LVS;
using LVS.Communicator.CronJob;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Sped4.Controls.ToDo
{
    public partial class ctrCleanAsnTables : UserControl
    {
        internal ctrMenu _ctrMenu { get; set; } = null;
        //internal List<string> Logs { get; set; } = new List<string>();

        public BindingList<string> Logs { get; set; } = new BindingList<string>();
        internal BackgroundWorker workerBar;
        internal BackgroundWorker workerList;

        public ctrCleanAsnTables(ctrMenu myMenu)
        {
            InitializeComponent();
            _ctrMenu = myMenu;
        }

        private void ctrCleanAsnTables_Load(object sender, EventArgs e)
        {
            Logs.Clear();
            lvLogs.AllowArbitraryItemHeight = true;
            lvLogs.AllowArbitraryItemWidth = true;
            lvLogs.DataSource = Logs;

            bar.Minimum = 0;
            bar.Maximum = 100;

            workerBar = new BackgroundWorker();
            workerBar.WorkerReportsProgress = true;

            workerList = new BackgroundWorker();

        }


        private async void btnCleanAsnTable_Click(object sender, EventArgs e)
        {
            Logs.Clear();
            if (clsMessages.Allgemein_SelectionInfoTextShow("Sollen die ASN Daten bereinigt werden?"))
            {
                bar.Value1 = 0;

                string mes = DateTime.Now.ToString("dd.MM.yyyy") + " - Starte Bereinigung der ASN Daten...";
                AddLogEntry(mes);

                string strMessageLog = string.Empty;
                CronJob_CleanUpEdiMessages clean = new CronJob_CleanUpEdiMessages();
                clean.ProgressMaxValue += max =>
                {
                    // UI-Update sicher im UI-Thread
                    Invoke(new Action(() => bar.Maximum = max));
                };

                workerBar.DoWork += (s, args) =>
                {
                    clean.StartCleaning(this.workerBar);
                };

                workerBar.ProgressChanged += (s, args) =>
                {
                    bar.Value1 = args.ProgressPercentage;
                    bar.Text = args.ProgressPercentage.ToString() + " von " + bar.Maximum.ToString();

                    if (args.UserState is string logMessage)
                    {
                        //AddLogToLogs(logMessage);
                        AddLogEntry(logMessage);
                    }
                };

                workerBar.RunWorkerCompleted += (s, args) =>
                {
                    MessageBox.Show("Fertig!");
                };

                if (!workerBar.IsBusy)
                {
                    workerBar.RunWorkerAsync();
                    //workerList.RunWorkerAsync();
                }
            }
        }


        private void AddLogEntry(string message)
        {
            if (lvLogs.InvokeRequired)
            {
                lvLogs.Invoke(new Action(() => Logs.Add(message))); // Thread-sicherer Zugriff
            }
            else
            {
                Logs.Add(message);
            }
            //Logs.Add(message);
            //lvLogs.Items.Add(message);
        }

        private void lvLogs_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            string str = e.CellElement.Text;
        }
    }
}
