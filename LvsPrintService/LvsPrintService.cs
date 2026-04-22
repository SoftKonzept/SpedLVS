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

        // neu: Flag, ob EventLog sicher verwendet werden kann
        private bool eventLogEnabled = false;

        public LvsPrintService()
        {
            InitializeComponent();
            eventLogLvsPrintService = new System.Diagnostics.EventLog();


            //--- neu test

            const string source = "LvsPrintServiceSource";
            const string logName = "LvsPrintServiceLog";

            try
            {
                bool isAdmin = false;
                try
                {
                    var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    var principal = new System.Security.Principal.WindowsPrincipal(identity);
                    isAdmin = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
                }
                catch
                {
                    // Falls die Rechteprüfung fehlschlägt, gehen wir vorsichtig vor.
                    isAdmin = false;
                }

                if (isAdmin)
                {
                    // Nur als Administrator versuchen, Source zu prüfen/erstellen
                    if (!System.Diagnostics.EventLog.SourceExists(source))
                    {
                        System.Diagnostics.EventLog.CreateEventSource(source, logName);
                    }
                    eventLogLvsPrintService.Source = source;
                    eventLogLvsPrintService.Log = logName;
                    eventLogEnabled = true;
                }
                else
                {
                    // Kein Admin: sicheren Fallback aktivieren (kein EventLog-Zugriff)
                    eventLogEnabled = false;
                    // Optional: statt EventLog fallback Logging (Datei/Trace) implementieren
                }
            }
            catch (System.Security.SecurityException secEx)
            {
                // Kein Zugriff auf EventLogs (z. B. Security-Log) -> fallback
                eventLogEnabled = false;
                System.Diagnostics.Trace.TraceWarning($"EventLog nicht verfügbar: {secEx.Message}");
            }
            catch (Exception ex)
            {
                // Sonstige Fehler ebenfalls nicht zum Absturz bringen
                eventLogEnabled = false;
                System.Diagnostics.Trace.TraceError($"Fehler beim Initialisieren des EventLog: {ex.Message}");
            }

            //--- alt
            //try
            //{
            //    if (!System.Diagnostics.EventLog.SourceExists("LvsPrintServiceSource"))
            //    {
            //        System.Diagnostics.EventLog.CreateEventSource(
            //            "LvsPrintServiceSource", "LvsPrintServiceLog");
            //    }
            //    eventLogLvsPrintService.Source = "LvsPrintServiceSource";
            //    eventLogLvsPrintService.Log = "LvsPrintServiceLog";
            //}
            //catch (Exception ex)
            //{
            //    string s = ex.Message;
            //}
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
            //eventLogLvsPrintService.WriteEntry("In OnStop.");
            
            //-- neu
            if (eventLogEnabled)
            {
                eventLogLvsPrintService.WriteEntry("In OnStop.");
            }
            else
            {
                System.Diagnostics.Trace.TraceInformation("In OnStop. (EventLog disabled)");
            }
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            // TODO: Insert monitoring activities here.
            //eventLogLvsPrintService.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            
            //--- neu
            if (eventLogEnabled)
            {
                eventLogLvsPrintService.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            }
            else
            {
                System.Diagnostics.Trace.TraceInformation("Monitoring the System (EventLog disabled)");
            }


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
