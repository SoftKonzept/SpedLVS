using System;
using System.IO;

namespace LVS.InitValue
{
    public class DefaultPath_Lvs
    {
        public const string DefaultPathLVS = @"C:\LVS\";
        public static string DefaultPath()
        {
            string defaultPathLVS = DefaultPath_Lvs.DefaultPathLVS;
            if (string.IsNullOrWhiteSpace(defaultPathLVS))
            {
                defaultPathLVS = @"C:\LVS\";
            }
            // Prüfen, ob der Ordner existiert, ansonsten erstellen
            if (!Directory.Exists(defaultPathLVS))
            {
                try
                {
                    Directory.CreateDirectory(defaultPathLVS);
                }
                catch (Exception ex)
                {
                    //throw new InvalidOperationException($"Fehler beim Erstellen des Ordners: {defaultPathLVS}", ex);
                }
            }
            return defaultPathLVS;
        }
    }
}
