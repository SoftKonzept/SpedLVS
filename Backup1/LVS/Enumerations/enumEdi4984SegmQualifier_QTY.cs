namespace LVS
{
    /// <summary>
    ///             11  -> Teilmenge
    ///             12  -> ausgelieferte Menge
    ///             52  -> Menge pro Packung
    ///             70  -> Eingangsfortschrittszahl / kumulativ empfangene Menge
    ///             72  -> komulierte Menge bis Ende des Vorjahres
    ///             83  -> Rückstand
    ///             84  -> dringend zu liefernde Menge
    ///             113 -> zu liefernde Menge
    ///             171 -> max. Stapelbarkeit
    ///             189 -> Paketanzahl je Ladungseinheit
    /// </summary>

    public enum enumEdi4984SegmQualifier_QTY
    {
        Teilmenge_11 = 11,
        ausgelieferteMenge_12 = 12,
        MengeProPackung_52 = 52,
        Eingangsfortschrittszahl_70 = 70,
        Rückstand_83 = 83,
        dringendZuLieferndeMenge_84 = 84,
        zuLieferndeMenge_113 = 113,
        maxStapelbarkeit_171 = 171,
        PaketAnzhalLadungseinheit_189 = 189
    }
}
