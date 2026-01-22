using System.Collections.Generic;

namespace LVS
{
    public class clsAufgabe
    {

        public decimal ArtikelID
        {
            get { return artikel.ID; }
            set
            {
                artikel = new clsArtikel();
                artikel.ID = value;
                artikel.GetArtikeldatenByTableID();
            }
        }
        public clsArtikel artikel;
        public List<clsExtraCharge> aufgaben = new List<clsExtraCharge>();

        public void addAufgabe(clsExtraCharge ExtraCharge)
        {
            aufgaben.Add(ExtraCharge);
        }

        public int Count { get { return aufgaben.Count; } }

        public bool contains(clsExtraCharge ExtraCharge)
        {

            foreach (clsExtraCharge ex in aufgaben)
            {
                if (ex.ID == ExtraCharge.ID)
                    return true;
            }
            return false;
        }


        public void Remove(clsExtraCharge ExtraCharge)
        {
            foreach (clsExtraCharge ex in aufgaben)
            {
                if (ex.ID == ExtraCharge.ID)
                    aufgaben.Remove(ex);
            }
        }
    }
}
