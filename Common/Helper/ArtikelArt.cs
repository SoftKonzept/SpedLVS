using System.Collections.Generic;

namespace Common.Helper
{
    public class ArtikelArt
    {


        public const string ArticleArt_Bleche = "Bleche";
        public const string ArticleArt_Coils = "Coils";
        public const string ArticleArt_EUROPaletten = "EURO-Paletten";
        public const string ArticleArt_Paletten = "Paletten";
        public const string ArticleArt_Platinen = "Platinen";
        public const string ArticleArt_Rohre = "Rohre";
        public const string ArticleArt_Stabstahl = "Stabstahl";

        public static List<string> ListArticleArt()
        {
            List<string> ListArtikelArt = new List<string>();
            ListArtikelArt.Add(ArtikelArt.ArticleArt_Bleche);
            ListArtikelArt.Add(ArtikelArt.ArticleArt_Coils);
            ListArtikelArt.Add(ArtikelArt.ArticleArt_EUROPaletten);
            ListArtikelArt.Add(ArtikelArt.ArticleArt_Paletten);
            ListArtikelArt.Add(ArtikelArt.ArticleArt_Platinen);
            ListArtikelArt.Add(ArtikelArt.ArticleArt_Rohre);
            ListArtikelArt.Add(ArtikelArt.ArticleArt_Stabstahl);
            return ListArtikelArt;
        }

    }
}
