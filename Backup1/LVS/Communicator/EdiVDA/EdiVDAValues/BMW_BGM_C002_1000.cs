namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_BGM_C002_1000
    {
        public const string const_BMW_BGM_C002_1000 = "#BMW_BGM_C002_1000#";

        /// <summary>
        ///             Kombination 48 + LVSNR 6stellig
        /// 
        /// </summary>
        /// <param name="myAsnTyp"></param>
        /// <param name="myLager"></param>
        /// <returns></returns>

        public static string Execute(clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            strTmp = "48" + myLager.Artikel.LVS_ID.ToString("000000");
            return strTmp;
        }
    }
}
