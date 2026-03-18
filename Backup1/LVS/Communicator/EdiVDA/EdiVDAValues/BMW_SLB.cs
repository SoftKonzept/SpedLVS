namespace LVS.Communicator.EdiVDA.EdiVDAValues
{
    public class BMW_SLB
    {
        /// <summary>
        ///             spezielle Kundenfunktion
        ///             ASN.Typ = AVE 
        ///             > SLB = LAusgang.AusgangID (8-stellig)
        ///             ASN.Typ = UBE/UBL
        ///             > SLB = LEingang.LfsNr (ohne A) kommt aus UB da wird die A+LAusgangID verwendet
        /// </summary>
        public const string const_BMW_SLB = "#BMW_SLB#";

        public static string Execute(clsASNTyp myAsnTyp, clsLagerdaten myLager)
        {
            string strTmp = string.Empty;

            switch (myAsnTyp.Typ)
            {
                case clsASNTyp.const_string_ASNTyp_EML:
                case clsASNTyp.const_string_ASNTyp_EME:
                case clsASNTyp.const_string_ASNTyp_BML:
                case clsASNTyp.const_string_ASNTyp_BME:
                case clsASNTyp.const_string_ASNTyp_RLL:
                case clsASNTyp.const_string_ASNTyp_RLE:
                    break;

                case clsASNTyp.const_string_ASNTyp_UBE:
                case clsASNTyp.const_string_ASNTyp_UBL:
                    if (myLager.Eingang is clsLEingang)
                    {
                        strTmp = myLager.Eingang.LEingangLfsNr.Replace("A", "");
                    }
                    break;

                case clsASNTyp.const_string_ASNTyp_AML:
                case clsASNTyp.const_string_ASNTyp_AME:
                case clsASNTyp.const_string_ASNTyp_AVL:
                case clsASNTyp.const_string_ASNTyp_AVE:
                    if (myLager.Ausgang is clsLAusgang)
                    {
                        strTmp = myLager.Ausgang.LAusgangID.ToString();
                    }
                    break;

            }
            if (!strTmp.Equals(string.Empty))
            {
                strTmp = ediHelper_FormatString.FillValueToLength(true, "0", strTmp, 8, true);
            }

            return strTmp;
        }
    }
}
