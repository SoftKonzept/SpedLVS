namespace LVS
{
    public enum enumLagerAktionen
    {
        EingangErstellt,
        EingangChanged,
        EingangChecked,
        EingangReset,
        AusgangErstellt,
        AusgangChanged,
        AusgangChecked,
        ArtikelAdd_Eingang,
        ArtikelAdd_Ausgang,
        ArtikelAdd_Artikel,
        ArtikelChange,
        ArtikelUmbuchung,
        ArtikelRL,
        ArtikelStorno,
        ArtikelDelete,
        ArtikelChecked,
        ArtikelSchadenAdd,
        ArtikelSchadenDel,
        SperrlagerIN,
        SperrlagerOUT,
        ArtikelSonderkosteAdd,
        ArtikelSonderkostenDel,
        ArtikelSonderkostenChange,
        //LSL, //Lieferschein vom Lieferant
        //EML, //an Empfänger
        //EME,  //an Lieferant
        //BME,
        //BML,
        //AML, // an Empfänger
        //AME, // an Lieferant
        //AVL, //Avis an Empfänger
        //AVE, // an Lieferant
        //STL, // Storno an Empfänger
        //STE, //an Lieferant
        //AbE, //Abruf Empfänger
        //AbL, //Abruf Lieferant
        PrintEingangDoc,
        PrintEingangAnzeige,
        PrintEingangLfs,
        PrintAusgangDoc,
        PrintAusgangAnzeige,
        PrintAusganLfs,
        PrintEingangPerDay,
        PrintAusgangPerDay,
        ImageToArtikel,
        StornoKorrekturVerfahren,
        AbrufCreate,
        AbrufChange,
        AbrufDelete,
        ArtikelCustomerProcessExceptionExist,
    }
}
