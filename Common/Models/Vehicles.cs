using System;

namespace Common.Models
{
    public class Vehicles
    {
        public int Id { get; set; }
        public string KFZ { get; set; }
        public string Fabrikat { get; set; }
        public string Bezeichnung { get; set; }
        public Int32 KIntern { get; set; }
        public string FGNr { get; set; }
        public DateTime Tuev { get; set; }
        public DateTime SP { get; set; }
        public DateTime BJ { get; set; }
        public DateTime seit { get; set; }
        public DateTime Abmeldung { get; set; }
        public Int32 Laufleistung { get; set; }
        public char ZM { get; set; }
        public char Anhaenger { get; set; }
        public char Plane { get; set; }
        public char Sattel { get; set; }
        public char Coil { get; set; }
        public Int32 Leergewicht { get; set; }
        public Int32 zlGG { get; set; }
        public decimal Innenhoehe { get; set; }
        public Int32 Stellplaetze { get; set; }
        public string Besonderheit { get; set; }
        public decimal Laenge { get; set; }
        public string AbgasNorm { get; set; }
        public Int32 Achsen { get; set; }
        //public string Besitzer { get; set; }
        public decimal MandantenID { get; set; }


        public string VehicleStringShort
        {
            get
            {
                string strReturn = KFZ + Environment.NewLine;
                strReturn += Fabrikat;
                return strReturn;
            }
        }

        public Vehicles Copy()
        {
            return (Vehicles)this.MemberwiseClone();
        }
    }
}
