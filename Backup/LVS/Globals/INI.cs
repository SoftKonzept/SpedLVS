namespace LVS
{
    public static class GlobalINI
    {
        //public static clsINI.clsINI INI = new clsINI.clsINI();
        //public static clsINI.clsINI INI = new clsINI.clsINI(Application.StartupPath + "\\Config.ini");
        public static clsINI.clsINI GetINI(string path)
        {
            clsINI.clsINI retINI = new clsINI.clsINI(path);
            return retINI;
        }

        public static clsINI.clsINI GetINI()
        {
            //string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string StartupPath = GlobalINI.GetStartUpPath(); //System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            clsINI.clsINI retINI = new clsINI.clsINI(StartupPath + "\\Config.ini");
            return retINI;
        }

        public static string GetStartUpPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}
