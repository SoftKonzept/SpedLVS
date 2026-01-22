namespace WatchDogService
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main()
        {
            //bool isDebug = false;
            //if (args != null && args.Length > 0)
            //{
            //    isDebug = args[0].ToLower().Contains("debug");
            //}

            //if (isDebug)
            //{
            //    WDService service = new WDService(isDebug);
            //    service.onDebug();
            //}
            //else
            //{
            //    ServiceBase[] ServicesToRun;
            //    ServicesToRun = new ServiceBase[]
            //    {
            //    new WDService(isDebug)
            //    };
            //    ServiceBase.Run(ServicesToRun);
            //}

            //Thread.Sleep(1000);

#if DEBUG
            WDService service = new WDService(true);
            service.onDebug();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WDService(false)
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
