using System;
using System.IO;

namespace LVS.InitValue
{
    public class DefaultPath_LvsExport
    {
        public static string DefaultPath()
        {
            string defaultPath = DefaultPath_Lvs.DefaultPath();
            defaultPath = Path.Combine(DefaultPath_Lvs.DefaultPath(), "Export");
            // Prüfen, ob der Ordner existiert, ansonsten erstellen
            if (!Directory.Exists(defaultPath))
            {
                try
                {
                    Directory.CreateDirectory(defaultPath);
                }
                catch (Exception ex)
                {
                    //throw new InvalidOperationException($"Fehler beim Erstellen des Ordners: {defaultPathLVS}", ex);
                }
            }
            return defaultPath;
        }
    }
}
