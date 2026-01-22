using LVS.ASN.Defaults;
using System;

namespace LVS.Communicator.EdiVDA
{
    public class ediHelper_EdiEDIFACT_ASN_CheckAsnArt
    {
        public static bool CheckAsnArt(string mySegmentToCheck, string myASNFileTyp)
        {
            bool isAsnArtKorrekt = false;

            if (string.IsNullOrEmpty(mySegmentToCheck) || string.IsNullOrEmpty(myASNFileTyp))
                return false;

            if (!Enum.TryParse<enumASNFileTyp>(myASNFileTyp, out var asnFileTyp))
                return false;

            switch (asnFileTyp)
            {
                case enumASNFileTyp.EDIFACT_ASN_D07A:
                    isAsnArtKorrekt = mySegmentToCheck.Contains(Default_DESADV_D07A.const_UNH_S009);
                    //isAsnArtKorrekt = mySegmentToCheck.Contains(ediHelper_EdiEDIFACT_ASN_D07A_CheckProcessableASN.const_UNH_S009);
                    break;
                case enumASNFileTyp.EDIFACT_ASN_D96A:
                    isAsnArtKorrekt = mySegmentToCheck.Contains(Default_DESADV_D96A.const_UNH_S009);
                    //isAsnArtKorrekt = mySegmentToCheck.Contains(ediHelper_EdiEDIFACT_ASN_D96A_CheckProcessableASN.const_UNH_S009);
                    break;
                case enumASNFileTyp.EDIFACT_ASN_D97A:
                    isAsnArtKorrekt = mySegmentToCheck.Contains(Default_DESADV_D97A.const_UNH_S009);
                    //isAsnArtKorrekt = mySegmentToCheck.Contains(ediHelper_EdiEDIFACT_ASN_D97A_CheckProcessableASN.const_UNH_S009);
                    break;

                case enumASNFileTyp.EDIFACT_DELFOR_D97A:

                    break;

                case enumASNFileTyp.EDIFACT_INVRPT_D96A:

                    break;

                case enumASNFileTyp.EDIFACT_Qality_D96A:

                    break;


                // Weitere Fälle können hier ergänzt werden
                default:
                    return false;
            }
            return isAsnArtKorrekt;
        }
    }
}
