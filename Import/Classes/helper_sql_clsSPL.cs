using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsSPL
    {
        public static void AddToSPL(impArtikel myArtImport)
        {
            clsSPL spl = new clsSPL();
            spl.InitClass(myArtImport.SysImport.GLUser);

            spl.ArtikelID = myArtImport.Artikel.ID;
            spl.Datum = DateTime.Now;
            spl.BKZ = "IN";
            spl.SPLIDIn = 0;
            spl.Sperrgrund = string.Empty;
            spl.Vermerk = string.Empty;

            spl.Add(true);
        }
    }
}
