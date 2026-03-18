using LVS.Constants;

namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class LiefNrDeleteSlash
    {
        public const string const_LiefNrDeleteSlash = "#LiefNrDeleteSlash#";

        /// <summary>
        ///             VDA4913 = 9 Stellen
        ///             VDA4987 = 10 Stellen
        /// </summary>
        /// <param name="myLager"></param>
        /// <param name="myASNArt"></param>
        /// <returns></returns>
        public static string Execute(clsLagerdaten myLager, clsASNArt myASNArt)
        {
            string strTmp = string.Empty;

            int iLength = 9;
            if (myASNArt.Typ.Equals(constValue_AsnArt.const_Art_EdifactVDA4987))
            {
                iLength = 10;
                if (myLager.Artikel.Eingang is clsLEingang)
                {
                    strTmp = myLager.Artikel.Eingang.Lieferant.Trim();
                    strTmp = strTmp.Replace("/", "");
                }
                else
                {
                    if (myLager.Artikel.Ausgang is clsLAusgang)
                    {
                        strTmp = myLager.Artikel.Ausgang.Lieferant.Trim();
                        strTmp = strTmp.Replace("/", "");
                    }
                }
            }
            else
            {
                iLength = 9;
                if (myLager.Eingang is clsLEingang)
                {
                    if (myLager.Eingang.Lieferant != null)
                    {
                        strTmp = myLager.Eingang.Lieferant.Trim();
                        strTmp = strTmp.Replace("/", "");
                    }
                }
                else
                {
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        strTmp = myLager.Ausgang.Lieferant.Trim();
                        strTmp = strTmp.Replace("/", "");
                    }
                }
            }

            while (strTmp.Length < iLength)
            {
                strTmp = "0" + strTmp;
            }
            return strTmp;
        }
    }
}
