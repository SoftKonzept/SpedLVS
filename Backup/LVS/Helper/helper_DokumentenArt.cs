using System;
using System.Collections.Generic;

namespace LVS.Helper
{
    public class helper_DokumentenArt
    {
        ///<summary>Globals / DicLagerDocumentArt</summary>
        ///<remarks>Alle Verwendeten Dokumente in der Lagerei. Die ID wird als Verknüpfung in der Datenbank eingetragen.
        ///         Neue Dokumentenarte bitte nur anügen.
        ///         [04.04.2014]</remarks>
        public static Dictionary<Int32, string> DicLagerDocumentArt()
        {
            //letzte ID =22 
            Dictionary<Int32, string> dicLagerDocumentArt = new Dictionary<Int32, string>()
            {
                {0,  "<Dokumentenart wählen>"},
                {20, "Adressliste"},
                {13, "Ausgangsliste"},
                {5,  "AusgangLfs"},
                {2,  "Auslagerungsschein"},
                {4,  "Auslagerungsanzeige"},
                {10, "Bescheinigung"},
                {17, "Bestand"},
                {16, "CMRFrachtbrief"},
                {1,  "Einlagerungsschein"},
                {3,  "Einlagerungsanzeige"},
                {14, "Eingangsliste"},
                {19, "Inventur"},
                {18, "Journal"},
                {15, "KVOFrachtbrief"},
                {6,  "Kunden-Ausgangslieferschein"},
                {7,  "Lagerrechnung"},
                {11, "Lagerschein-§475-BGB"},
                {12, "Manuellerechnung"},
                {21, "ManuelleGutschrift"},
                {9,  "RGAnhang"},
                {22, "RGBuch"},
                {8,  "Speditionsrechnung"}
            };
            return dicLagerDocumentArt;
        }


    }
}
