using LVS.Constants;

namespace LVS.Communicator.EdiVDA
{
    public class EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement
    {
        /// <summary>
        ///             INVRPTD96A
        ///             
        ///             Message StoreIn folgende Segement müssen deaktiviert werden.
        ///             
        ///             NAD+DP
        ///             RFF+AFV
        ///             DTM+171
        /// 
        /// </summary>
        public const string const_EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement = "#EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement#";


        //------------- Segment to deactivate ---------------------
        public const string const_SegToDeactivate_Code_NAD_DP = "NAD#DP";
        public const string const_SegToDeactivate_Code_RFF_AFV = "RFF#AFV";
        public const string const_SegToDeactivate_Code_DTM_102 = "DTM#102";

        public static bool Check(clsEdiVDACreate myEdiCreate)
        {
            bool result = true;
            if (myEdiCreate.ediSegment.Code != null)
            {
                switch (myEdiCreate.ediSegment.Code)
                {
                    case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_NAD_DP:
                    case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_RFF_AFV:
                    case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_DTM_102:
                        switch (myEdiCreate.ASN.ASNArt.Typ)
                        {
                            case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                                //Wenn EM dann deaktivieren
                                switch (myEdiCreate.ASN.ASNTyp.TypID)
                                {
                                    case clsASNTyp.const_ASNTyp_EME:
                                    case clsASNTyp.const_ASNTyp_EML:
                                        result = false;
                                        break;

                                }
                                break;
                        }
                        break;
                }
            }
            return result;
        }
    }
}
