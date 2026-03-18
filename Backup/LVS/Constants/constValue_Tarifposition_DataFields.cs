using System.Collections.Generic;

namespace LVS.Constants
{
    public class constValue_Tarifposition_DataFields
    {
        public const string const_Tarifposition_DataField_Artikel_ID = "Artikel.ID";
        public const string const_Tarifposition_DataField_Artikel_Anzahl = "Artikel.Anzahl";
        public const string const_Tarifposition_DataField_Artikel_Brutto = "Artikel.Brutto";
        public const string const_Tarifposition_DataField_Artikel_Netto = "Artikel.Netto";

        public const string const_Tarifposition_DataField_Eingang_Id = "Eingang.ID";
        public const string const_Tarifposition_DataField_Eingang_WaggonNo = "Eingang.WaggonNo";
        //public const string const_Tarifposition_DataField_Eingang_ArtikelAnzahl = "Eingang.ArtikelAnzahl";

        public static List<string> GetDataFields()
        {
            List<string> fields = new List<string>();
            fields.Add(const_Tarifposition_DataField_Artikel_ID);
            fields.Add(const_Tarifposition_DataField_Artikel_Anzahl);
            fields.Add(const_Tarifposition_DataField_Artikel_Netto);
            fields.Add(const_Tarifposition_DataField_Artikel_Brutto);
            fields.Add(const_Tarifposition_DataField_Eingang_Id);
            fields.Add(const_Tarifposition_DataField_Eingang_WaggonNo);
            //fields.Add(const_Tarifposition_DataField_Eingang_ArtikelAnzahl);
            return fields;
        }

    }
}
