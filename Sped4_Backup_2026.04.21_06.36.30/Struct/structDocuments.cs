using System;

namespace Sped4.Struct
{

    public struct structDocuments
    {
        public decimal DocScanTableID;
        public decimal AuftragID;
        public decimal LEingangID;
        public decimal LAusgangID;
        public decimal AuftragsNr;
        public decimal AuftragPosNr;
        public decimal ArtikelTableID;
        public decimal AuftragPosTableID;
        public decimal AuftragTableID;

        public Int32 PicNumber;
        //public decimal AuftragScanID;
        public decimal OrderPosRecID;
        public Int32 x_Pos;
        public Int32 y_Pos;
        public bool init;

        public string Pfad;
        public string ScanFilename;
        public string Beschreibung;
    }
}
