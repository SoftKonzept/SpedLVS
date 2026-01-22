using System.Collections.Generic;

namespace Sped4.Settings
{
    public class DictSettings
    {
        /// <summary>
        ///             Status des Artikels (Honselmann)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> DicStatus()
        {
            Dictionary<string, string> dicStatus = new Dictionary<string, string>
            {
                { "10", "Eingänge im System erstellt" },
                { "20", "Ware zur Entladung bereitgestellt (LKW, Waggon)" },
                { "30", "Eingang abgeschlossen" },
                { "40", "Alle Eingangsdokumente erstellt" },
                { "50", "Lagerausgang erstellt" },
                { "60", "Ausgangsschein /Dokument erstellt" },
                { "70", "Ausgangabgeschlossen / Ausgangslieferschein erstellt" }
            };
            return dicStatus;
        }


    }
}
