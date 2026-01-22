namespace Common.ApiUri
{
    public class apiServerUri
    {
        //------------------------------------------------------------------------------------------------
        //
        //          Hier werden alle Daten für den Zugriff auf den Webservice hinterlegt
        //          
        //
        //------------------------------------------------------------------------------------------------



        public static string GetApiServerUri()
        {
            string ServerUrl = string.Empty;
            //

#if DEBUG
            //--  home über WAVE LINK
            //--  localhost Laptop WS-MR-W11
            //--  für Amulator wie Pixel 5 API 33...
            //ServerUrl = "https://192.168.1.55/LvsMobileAPI";


            //--- WLAN IP  // über USB 
            //-- Scanner Anschluss über USB / WLAN hotSpot
            ServerUrl = "https://192.168.137.1/LvsMobileAPI";

            //---local ohne Internet Verbindung
            //ServerUrl = "https://172.21.224.1:443";

            //--- Büro
            //ServerUrl = "https://192.168.21.111:443";
            //ServerUrl ="https://192.168.21.101:443";
#else

            //---- SZG 
            ///  IP: 77.235.173.244 -> lvscallszg.de
            ///  IP INTERN https://192.168.61.3:444/
            //ServerUrl ="https://192.168.61.3:444";
            ///        


            //--- WLAN IP  // über USB 
            //-- Scanner Anschluss über USB / WLAN
            // ServerUrl = "https://192.168.137.1:443";
            //ServerUrl = "https://192.168.137.1/LvsMobileAPI";

            ServerUrl = "https://lvsszg.de/LvsMobileAPI";


#endif

            return ServerUrl;
        }

    }
}
