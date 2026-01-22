namespace LVS
{
    /// <summary>
    ///             2   => gewünschter Liefertermin
    ///             10  => verlangtes Versanddatum
    ///             36  => Verfallsdatum
    ///             51  => Datum EFZ / EIngangsfortschrittszahl
    ///             52  => Datum Nullstellung Fortschrittzahl
    ///             63  => spätestest Lieferdatum
    ///             64  => frührstes Lieferdatum
    ///             94  => Produktionsdatum
    ///             113 => Dokumentenzeit
    ///             132 => Ankunfstdatum
    ///             137 => Dokumentendatum
    ///             171 => Referenzdatum
    /// </summary>
    public enum enumEdi4984SegmQualifier_DTM
    {
        gewünschterLiefertermin_2 = 2,
        verlangtesVersanddatum_10 = 10,
        Verfalldatum_36 = 36,
        DatumEFZ_51 = 51,
        DatumResetNull_52 = 52,
        spaetestesLieferadatum_63 = 63,
        fruehestesLieferdatum_64 = 64,
        Produktionsdatum_94 = 94,
        Dokumentenzeit_113 = 113,
        Ankunftsdatum_132 = 132,
        Dokumentendatum_137 = 137,
        Referenzdatum_171 = 171
    }
}
