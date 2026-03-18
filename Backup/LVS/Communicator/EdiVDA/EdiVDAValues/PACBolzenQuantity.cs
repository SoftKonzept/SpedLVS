namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class PACBolzenQuantity
    {
        public const string const_PACNumberBolzen = "#PACBolzenQuantity#";

        public static string Execute(clsArtikel myArtikel)
        {
            string strTmp = string.Empty;
            if ((myArtikel is clsArtikel) && (myArtikel.GArt is clsGut))
            {
                strTmp = myArtikel.GArt.MEAbsteckBolzen.ToString();
            }
            return strTmp;
        }
    }
}
