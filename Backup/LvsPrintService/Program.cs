using System.ServiceProcess;

namespace LvsPrintService
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new LvsPrintService()
            //};
            //ServiceBase.Run(ServicesToRun);
#if DEBUG
            LvsPrintService service = new LvsPrintService();
            service.onDebug();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new LvsPrintService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
