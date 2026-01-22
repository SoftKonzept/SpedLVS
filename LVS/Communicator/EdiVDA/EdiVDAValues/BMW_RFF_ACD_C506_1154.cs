namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_RFF_ACD_C506_1154
    {
        public const string const_BMW_RFF_ACD_C506_1154 = "#BMW_RFF_ACD_C506_1154#";

        /// <summary>
        ///             1979031000225386
        ///             Kombination Lieerantennummer + LVSNR 
        ///             
        ///             Lieferantennummer:  19790310 - 8 stellig
        ///             LVSNR:              00225386 - 8 stellig
        /// 
        /// </summary>
        /// <param name="myAsnTyp"></param>
        /// <param name="myLager"></param>
        /// <returns></returns>

        public static string Execute(clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            string strLieferantennummer = BMW_UNB_S002_0004.const_BMW_SenderIdentification.ToString();
            int iLen8 = 8;
            while (iLen8 - strLieferantennummer.Length > 0)
            {
                strLieferantennummer = "0" + strLieferantennummer;
            }
            string strLvs = myLager.Artikel.LVS_ID.ToString("00000000");

            strTmp = strLieferantennummer + strLvs;


            return strTmp;
        }
    }
}
