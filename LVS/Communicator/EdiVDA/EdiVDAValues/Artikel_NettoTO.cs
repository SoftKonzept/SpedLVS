namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Artikel_NettoTO
    {
        public const string const_Artikel_NettoTO = "#Artikel_NettoTO#";

        public static string Execute(clsLagerdaten myLager)
        {
            string strTmp = string.Empty;
            int iTmp = 0;
            int.TryParse(myLager.Artikel.Netto.ToString(), out iTmp);
            iTmp = (int)myLager.Artikel.Netto;
            strTmp = iTmp.ToString();
            return strTmp;
        }
    }
}
