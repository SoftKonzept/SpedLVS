using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsArtikelVita
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtImport"></param>
        public static void AddVitaEingang(impArtikel myArtImport)
        {
            clsArtikelVita.AddEinlagerungAuto(myArtImport.SysImport.GLUser.User_ID
                                                  , myArtImport.Eingang.LEingangTableID
                                                  , myArtImport.Eingang.LEingangID
                                                  );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtImport"></param>
        public static void AddVitaArtikel(impArtikel myArtImport)
        {
            string strSql = clsArtikelVita.AddArtikelLEingangAuto(myArtImport.SysImport.GLUser
                                                                  , myArtImport.Artikel.ID
                                                                  , myArtImport.Eingang.LEingangID
                                                                  );
            clsSQLcon.ExecuteSQLWithTRANSACTION(strSql, "ArtikelVita", myArtImport.SysImport.GLUser.User_ID);
        }
    }
}
