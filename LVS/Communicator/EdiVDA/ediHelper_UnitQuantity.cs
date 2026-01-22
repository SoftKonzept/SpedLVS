using System;

namespace LVS
{
    public class ediHelper_UnitQuantity
    {
        public static enumEdifactUnit ConvertEinheitToUnit(clsArtikel myArtikel)
        {
            enumEdifactUnit tmpEdiQuantity = enumEdifactUnit.C62;

            if (myArtikel is clsArtikel)
            {
                switch (myArtikel.Einheit)
                {
                    case "kg":
                    case "KG":
                    case "Kg":
                    case "Kilogramm":
                        tmpEdiQuantity = enumEdifactUnit.KGM;
                        break;

                    case "Stk":
                    case "Stk.":
                    case "Stück":
                    case "Stueck":
                    case "ST":
                        tmpEdiQuantity = enumEdifactUnit.C62;
                        break;

                }
            }
            return tmpEdiQuantity;
        }

        /// <summary>
        ///            auf Basis der Güterart
        /// </summary>
        /// <param name="myArtikel"></param>
        /// <returns></returns>
        public static enumEdifactUnit GetUnitForEdi(clsArtikel myArtikel)
        {
            enumEdifactUnit tmpEdiQuantity = enumEdifactUnit.C62;

            if (myArtikel is clsArtikel)
            {
                if ((myArtikel.GArtID > 0) && (myArtikel.GArt is clsGut))
                {
                    if (myArtikel.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        tmpEdiQuantity = enumEdifactUnit.KGM;
                    }
                    else
                    {
                        tmpEdiQuantity = enumEdifactUnit.C62;
                    }
                }
            }
            return tmpEdiQuantity;
        }
        /// <summary>
        ///             auf Basis der Güterart
        /// </summary>
        /// <param name="myArtikel"></param>
        /// <returns></returns>
        public static int GetQuantityForEdi_Brutto(clsArtikel myArtikel)
        {
            int iReturn = 0;

            if (myArtikel is clsArtikel)
            {
                if ((myArtikel.GArtID > 0) && (myArtikel.GArt is clsGut))
                {
                    if (myArtikel.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        string strTmp = ((int)myArtikel.Brutto).ToString();
                        int.TryParse(strTmp, out iReturn);
                    }
                    else
                    {
                        iReturn = myArtikel.Anzahl;
                    }
                }
            }
            return iReturn;
        }
        /// <summary>
        ///                 auf Basis der Güterart
        /// </summary>
        /// <param name="myArtikel"></param>
        /// <returns></returns>
        public static int GetQuantityForEdi_Netto(clsArtikel myArtikel)
        {
            int iReturn = 0;

            if (myArtikel is clsArtikel)
            {
                if ((myArtikel.GArtID > 0) && (myArtikel.GArt is clsGut))
                {
                    if (myArtikel.GArt.ArtikelArt.IndexOf("Coil", StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        string strTmp = ((int)myArtikel.Netto).ToString();
                        int.TryParse(strTmp, out iReturn);
                    }
                    else
                    {
                        iReturn = myArtikel.Anzahl;
                    }
                }
            }
            return iReturn;
        }
    }
}
