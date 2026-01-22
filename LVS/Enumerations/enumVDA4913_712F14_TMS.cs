namespace LVS
{
    /// <summary>
    ///             Satz712F14 Transportmittelschlüssel
    ///             01 = KFZ
    ///             02 = Bordero#
    ///             06 = Stückgut#
    ///             07 = Expressgut#
    ///             08 = Waggon
    ///             09 = Postpaket
    ///             10 = Flugnummer
    ///             11 = Schiffsname
    /// </summary>
    public enum enumVDA4913_712F14_TMS
    {
        KFZ = 1,
        Borderonummer = 2,
        Stückgutnummer = 6,
        Expressgutnummer = 7,
        Waggonnummer = 8,
        Postpaket = 9,
        Flugnummer = 10,
        Schiffsname = 11
    }
}
