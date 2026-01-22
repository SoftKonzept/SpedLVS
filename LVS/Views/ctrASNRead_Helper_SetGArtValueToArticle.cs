using Common.Models;
using LVS.ViewData;
using System;
using System.Runtime.Serialization;

namespace LVS.Views
{
    [Serializable]
    [DataContract]
    public class ctrASNRead_Helper_SetGArtValueToArticle
    {
        public Articles Article { get; set; }
        public ctrASNRead_Helper_SetGArtValueToArticle(Articles myArt, LVS.clsSystem mySystem)
        {
            Article = myArt;

            if (Article.GArtID > 0)
            {
                if (Article.Gut is null)
                {
                    GoodstypeViewData gtVD = new GoodstypeViewData(Article.GArtID, 1, true);
                    Article.Gut = gtVD.Gut;
                }

                if ((Article.Gut is Goodstypes) && (!Article.Gut.Werksnummer.Equals(string.Empty)))
                {
                    Article.Werksnummer = Article.Gut.Werksnummer;
                }
                if ((Article.Gut is Goodstypes) && (!Article.Gut.Einheit.Equals(string.Empty)))
                {
                    Article.Einheit = Article.Gut.Einheit;
                }
                if ((Article.Gut is Goodstypes) && (Article.Dicke == 0M))
                {
                    Article.Dicke = Article.Gut.Dicke;
                }
                if ((Article.Gut is Goodstypes) && (Article.Breite == 0M))
                {
                    Article.Breite = Article.Gut.Breite;
                }
                if ((Article.Gut is Goodstypes) && (Article.Laenge == 0M))
                {
                    Article.Laenge = Article.Gut.Laenge;
                }
                if ((Article.Gut is Goodstypes) && (Article.Hoehe == 0M))
                {
                    Article.Hoehe = Article.Gut.Hoehe;
                }
                //if (myArt.Bestellnummer.Equals(string.Empty))
                //{
                //    myArt.Bestellnummer = myArt.GArt.BestellNr;
                //}
                //--- Bestellnummer
                if ((Article.Netto == 0) && ((Article.Gut is Goodstypes) && (Article.Gut.Netto > 0)))
                {
                    Article.Netto = Article.Gut.Netto;
                }
                if ((Article.Brutto == 0) && ((Article.Gut is Goodstypes) && (Article.Gut.Brutto > 0)))
                {
                    Article.Brutto = Article.Gut.Brutto;
                }

                if ((Article.Gut is Goodstypes) && (!Article.Gut.BestellNr.Equals(string.Empty)))
                {
                    mySystem.Client.clsLagerdaten_Customized_ASNArtikel_Bestellnummer(ref myArt, myArt.Gut.BestellNr);
                }

                //-- IsMulde
                if (
                        (Article.Gut is Goodstypes) &&
                        ((Article.Gut.ArtikelArt.IndexOf("COIL") > -1) || (Article.Gut.ArtikelArt.IndexOf("Coil") > -1))
                    )
                {
                    Article.IsMulde = true;
                }
                //-- IsStackable
                if (Article.Gut is Goodstypes)
                {
                    Article.IsStackable = Article.Gut.IsStackable;
                }


            }

        }
    }
}
