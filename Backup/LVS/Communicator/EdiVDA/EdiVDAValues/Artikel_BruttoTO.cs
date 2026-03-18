namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Artikel_BruttoTO
    {
        public const string const_Artikel_BruttoTO = "#Artikel_BruttoTO#";

        public static string Execute(clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            int iTmp = 0;
            //int.TryParse(myLager.Artikel.Brutto.ToString(), out iTmp);
            iTmp = (int)myLager.Artikel.Brutto;
            strTmp = iTmp.ToString();
            return strTmp;
        }
    }
}
