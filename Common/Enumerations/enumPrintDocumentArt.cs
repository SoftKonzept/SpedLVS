namespace Common.Enumerations
{

    /// <summary>
    ///             siehe public static Dictionary<Int32, string> DicLagerDocumentArt()
    ///             Globals Zeile 579
    /// </summary>
    public enum enumPrintDocumentArt
    {
        NotSet = 0,

        // Eingang
        Einlagerungsschein = 1,
        Einlagerungsanzeige = 3,
        Eingangsliste = 14,

        // Ausgang
        Auslagerungsschein = 2,
        Auslagerungsanzeige = 4,
        AusgangLfs = 5,
        KundenAusgangslieferschein = 6,
        Ausgangsliste = 13,
        KVOFrachtbrief = 15,
        CMRFrachtbrief = 16,

        // Artikel
        SPLDoc = 41,
        LabelAll = 42,
        //Eingangsliste = 43,
        SchadenLabel = 44,
        SchadenDoc = 45,

        // Rechnungen
        Lagerrechnung = 7,
        Speditionsrechnung = 8,
        RGAnhang = 9,
        Manuellerechnung = 12,
        ManuelleGutschrift = 21,
        RGBuch = 22,

        Bescheinigung = 10,
        Lagerschein = 11,

        Bestand = 17,
        Journal = 18,
        Inventur = 19,
        Adressliste = 20
    }
}
