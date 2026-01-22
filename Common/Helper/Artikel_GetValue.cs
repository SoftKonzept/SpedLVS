using Common.Constants;
using Common.Models;

namespace Common.Helper
{
    public class Artikel_GetValue
    {
        public static string GetArtValueByField(Articles myArt, string strArtField)
        {
            string strReturn = string.Empty;
            switch (strArtField)
            {
                case constValue_Article.ArtikelField_Anzahl:
                    strReturn = myArt.Anzahl.ToString();
                    break;
                case constValue_Article.ArtikelField_LVSID:
                    strReturn = myArt.LVS_ID.ToString();
                    break;
                case constValue_Article.ArtikelField_Dicke:
                    strReturn = myArt.Dicke.ToString();
                    break;
                case constValue_Article.ArtikelField_Breite:
                    strReturn = myArt.Breite.ToString();
                    break;
                case constValue_Article.ArtikelField_Länge:
                    strReturn = myArt.Laenge.ToString();
                    break;
                case constValue_Article.ArtikelField_Höhe:
                    strReturn = myArt.Hoehe.ToString();
                    break;
                case constValue_Article.ArtikelField_Netto:
                    strReturn = myArt.Netto.ToString();
                    break;
                case constValue_Article.ArtikelField_Brutto:
                    strReturn = myArt.Brutto.ToString();
                    break;
                case constValue_Article.ArtikelField_Einheit:
                    if (myArt.Einheit != null)
                    {
                        strReturn = myArt.Einheit.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_Produktionsnummer:
                    if (myArt.Produktionsnummer != null)
                    {
                        strReturn = myArt.Produktionsnummer.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_Werksnummer:
                    if (myArt.Werksnummer != null)
                    {
                        strReturn = myArt.Werksnummer.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_Charge:
                    if (myArt.Charge != null)
                    {
                        strReturn = myArt.Charge.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_Bestellnummer:
                    if (myArt.Bestellnummer != null)
                    {
                        strReturn = myArt.Bestellnummer.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_exBezeichnung:
                    if (myArt.exBezeichnung != null)
                    {
                        strReturn = myArt.exBezeichnung.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_exMaterialnummer:
                    if (myArt.exMaterialnummer != null)
                    {
                        strReturn = myArt.exMaterialnummer.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_Gut:
                    if (myArt.Gut.Bezeichnung != null)
                    {
                        strReturn = myArt.Gut.Bezeichnung.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_Güte:
                    //strReturn = this.GArt..ToString();
                    break;
                case constValue_Article.ArtikelField_Position:
                    if (myArt.Position != null)
                    {
                        strReturn = myArt.Position.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_exAuftrag:
                    if (myArt.exAuftrag != null)
                    {
                        strReturn = myArt.exAuftrag.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_exAuftragPos:
                    if (myArt.exAuftragPos != null)
                    {
                        strReturn = myArt.exAuftragPos.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_ArtikelIDRef:
                    if (myArt.ArtIDRef != null)
                    {
                        strReturn = myArt.ArtIDRef.ToString();
                    }
                    break;
                case constValue_Article.ArtikelField_GlowDate:
                    if (myArt.GlowDate != null)
                    {
                        strReturn = myArt.GlowDate.ToString("dd.MM.yyyy");
                    }
                    break;

                case constValue_Article.ArtikelField_TARef:
                    if (myArt.TARef != null)
                    {
                        strReturn = myArt.TARef;
                    }
                    break;
                default:
                    break;
            }
            return strReturn;
        }

    }
}
