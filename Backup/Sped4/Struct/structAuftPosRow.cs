using System.Windows.Forms;

namespace Sped4.Struct
{
    public struct structAuftPosRow
    {
        public decimal AuftragPosTableID;     // Auftragnummer
        public decimal AuftragID;
        public decimal AuftragPos; // Pos des Auftrages
        public decimal AuftragPosID; //ID Table AuftragPos
        public decimal ArtikelID; //ID Table Artikel
        public decimal B_ID;
        public decimal E_ID;
        //public decimal Menge;
        public decimal Gewicht;
        public int RowIndex;
        //public ctrAufträge Receiverctr;
        public UserControl Receiverctr;
        public decimal TourID;
        public bool disponierbar;

    }
}
