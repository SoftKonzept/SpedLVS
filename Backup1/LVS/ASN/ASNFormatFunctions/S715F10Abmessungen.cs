namespace LVS.ASN.ASNFormatFunctions
{
    public class S715F10Abmessungen
    {
        /// <summary>
        ///             Set715F10Abmessungen
        ///             Satz VDA 715 Feld 10
        ///             Länge 12 Zeichen
        ///             0-3 => Länge
        ///             4-7 => Breite
        ///             8-11=> Dicke
        ///             in mm
        /// </summary>

        public const string const_S715F10Abmessungen = "#715F10Abmessungen#";
        public const string strExtention = ",00";
        ///<summary>clsASNFromatFunctions / func_Diff1000</summary>
        ///<remarks></remarks>>
        public static clsArtikel Execute(string myValue, clsArtikel myArtikel)
        {
            clsArtikel myArt = myArtikel.Copy();
            string strL = "0";
            string strB = "0";
            string strD = "0";
            decimal decTmp = 0.00m;

            if (myValue.Length > 1)
            {
                if (myValue.Length >= 4)
                {
                    strL = myValue.Substring(0, 4) + strExtention;
                    if (myValue.Length >= 8)
                    {
                        strB = myValue.Substring(4, 4);
                        if (myValue.Length >= 12)
                        {
                            strD = myValue.Substring(8, 4) + strExtention;
                        }
                        else
                        {
                            strD = myValue.Substring(8, myValue.Length) + strExtention;
                        }
                    }
                    else
                    {
                        strB = myValue.Substring(4, myValue.Length) + strExtention;
                    }
                }
                else
                {
                    strL = myValue.Substring(0, myValue.Length) + strExtention;
                }
            }

            decTmp = 0.00m;
            decimal.TryParse(strL, out decTmp);
            myArt.Laenge = decTmp;

            decTmp = 0.00m;
            decimal.TryParse(strB, out decTmp);
            myArt.Breite = decTmp;
            if (myArt.Breite > 0)
            {
                myArt.Breite = myArt.Breite / 10;
            }

            decTmp = 0.00m;
            decimal.TryParse(strD, out decTmp);
            myArt.Dicke = decTmp;
            if (myArt.Dicke > 0)
            {
                myArt.Dicke = myArt.Dicke / 1000;
            }
            return myArt;
        }
    }
}
