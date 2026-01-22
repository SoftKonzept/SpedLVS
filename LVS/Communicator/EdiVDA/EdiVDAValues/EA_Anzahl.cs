namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class EA_Anzahl
    {
        /// <summary>
        /// </summary>
        public const string const_EA_Anzahl = "EA.Anzahl";
        //internal const string const_Ausgang = "A";
        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            int iSum = 0;
            object objSum = 0;
            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                case clsASNTyp.const_string_ASNTyp_TSE:
                case clsASNTyp.const_string_ASNTyp_TSL:
                case clsASNTyp.const_string_ASNTyp_STE:
                case clsASNTyp.const_string_ASNTyp_STL:
                    if (myLager.Eingang is clsLEingang)
                    {
                        objSum = myLager.Eingang.dtArtInLEingang.Compute("SUM(Anzahl)", string.Empty);
                        int.TryParse(objSum.ToString(), out iSum);
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        objSum = myLager.Ausgang.dtArtInLAusgang.Compute("SUM(Anzahl)", string.Empty);
                        int.TryParse(objSum.ToString(), out iSum);
                    }
                    break;
            }
            return iSum.ToString();
        }
    }
}
