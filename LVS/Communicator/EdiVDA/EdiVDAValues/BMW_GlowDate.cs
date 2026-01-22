namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_GlowDate
    {
        public const string const_BMW_GlowDate = "#BMW_GlowDate#";

        /// <summary>
        ///             CHeck und AUsgabe Glühdatum
        /// </summary>
        /// <param name="myAsnTyp"></param>
        /// <param name="myLager"></param>
        /// <returns></returns>

        public static string Execute(clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            if (myLager.Artikel is clsArtikel)
            {
                //if (!myLager.Artikel.GlowDate.Equals(Globals.DefaultDateTimeMinValue))
                //{

                //}
                //strTmp = myLager.Artikel.GlowDate.ToString("yyyyMMdd");

                strTmp = myLager.Artikel.GlowDate.ToString("dd.MM.yyyy");

            }
            return strTmp;
        }
    }
}
