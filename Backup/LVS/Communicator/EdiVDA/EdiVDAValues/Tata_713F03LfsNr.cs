namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class Tata_713F03LfsNr
    {
        public const string const_Tata_713F03LfsNr = "#Tata_713F03LfsNr#";

        public static string Execute(clsASNTyp myAsnTyp, clsArtikel myArtikel)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                    if (myArtikel.Eingang is clsLEingang)
                    {
                        strTmp = myArtikel.Eingang.LEingangLfsNr;
                    }
                    break;


                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (myArtikel.Ausgang is clsLAusgang)
                    {
                        strTmp = myArtikel.Ausgang.SLB.ToString();
                    }
                    break;

            }
            return strTmp;
        }
    }
}
