using Common.Models;
using LVS;
using LVS.InitValueLvsPrinterService;
using LVS.ViewData;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;


namespace LvsPrintService
{
    public partial class LvsPrintService : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        internal bool OnElapsedProcess { get; set; } = true;
        public LVS.Globals._GL_SYSTEM GLSystem = new LVS.Globals._GL_SYSTEM();
        public LVS.Globals._GL_USER GLUser = new LVS.Globals._GL_USER();
        internal LVS.clsSystem system = new clsSystem();
        private int eventId { get; set; } = 1;

        public LvsPrintService()
        {
            InitializeComponent();
            eventLogLvsPrintService = new System.Diagnostics.EventLog();
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists("LvsPrintServiceSource"))
                {
                    System.Diagnostics.EventLog.CreateEventSource(
                        "LvsPrintServiceSource", "LvsPrintServiceLog");
                }
                eventLogLvsPrintService.Source = "LvsPrintServiceSource";
                eventLogLvsPrintService.Log = "LvsPrintServiceLog";
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        public void onDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            GLSystem = new LVS.Globals._GL_SYSTEM();
            system = new clsSystem();
            system.InitSystem(ref GLSystem, 0);


            // stest Printer name
            //string s = InitValue.InitValue_PrintServicePrinter_Default.DefaultPrinter();
            //List<string> l = PrinterSettings_Printer.GetPrinter();

            // -- Check DB Verbindung 
            clsSQLARCHIVE sqlArchiv = new clsSQLARCHIVE();
            clsSQLcon sqlLvs = new clsSQLcon();

            if (
                    (sqlArchiv.init()) &&
                    (sqlLvs.init())
               )
            {
                UsersViewData uVD = new UsersViewData(GLUser, 1);
                GLUser = uVD._GL_User;

#if DEBUG
                GetAndProcessPrintQueue();


#else
                // Set up a timer that triggers every minute.
                timer = new Timer();
                //timer.Interval = 30000; // 30 seconds
                int iInterval = LVS.InitValueLvsPrinterService.InitValue_Settings.TimerElapsedDuration();
                timer.Interval = iInterval;
                timer.Elapsed += new ElapsedEventHandler(this.OnElapsedTime);
                timer.Enabled = true;
                OnElapsedProcess = false;
                timer.Start();
#endif
            }
        }

        protected override void OnStop()
        {
            eventLogLvsPrintService.WriteEntry("In OnStop.");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            // TODO: Insert monitoring activities here.
            eventLogLvsPrintService.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            GetAndProcessPrintQueue();
        }

        private void GetAndProcessPrintQueue()
        {
            OnElapsedProcess = true;
            try
            {
                // get print orders
                PrintQueueViewData pVD = new PrintQueueViewData();
                pVD.GetPrintQueueList();

                foreach (PrintQueues p in pVD.ListPrintQueue)
                {
                    Printing(p);
                    Task.Delay(1000).Wait();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                OnElapsedProcess = false;
            }
            string str = string.Empty;
        }

        private void Printing(PrintQueues myPrintQueue)
        {
            LVS.Print.TelerikPrint p = new LVS.Print.TelerikPrint();
            p.InitClass(GLUser, GLSystem, system, myPrintQueue);
        }


    }
}
