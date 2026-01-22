using LVS.Constants;

namespace LVS.Communicator.EdiVDA
{
    public class EdiSegmentCheck_BMW
    {
        /// <summary>
        ///             Die Artikelart macht eine Unterscheidung bei den Lagermeldungen.
        ///             Bei den Artikeldaten werden für Platinen kein Gewicht übermittelt sondern die Anzahl,
        ///             bei Coils wird Netto und Brutto übermittelt.
        ///             Dadurch müssen die entsprechenden Segemente deaktiviert werden, je nach Artikelart.
        /// </summary>
        public const string const_EdiSegmentCheck_BMW = "#EdiSegmentCheck_BMW#";


        //------------- Segment to deactivate ---------------------
        public const string const_SegToDeactivate_Code_MEA_PD_AAL = "MEA#PD#AAL";
        public const string const_SegToDeactivate_Code_MEA_SV = "MEA#SV";
        public const string const_SegToDeactivate_Code_RFF_ACE = "RFF#ACE";

        public static bool Check(clsEdiVDACreate myEdiCreate)
        {
            bool result = true;
            if (myEdiCreate.ediSegment.Code != null)
            {
                switch (myEdiCreate.ediSegment.Code)
                {
                    case EdiSegmentCheck_BMW.const_SegToDeactivate_Code_MEA_PD_AAL:
                        if (myEdiCreate.Lager.Artikel.GArt.ArtikelArt.ToUpper().Equals(clsGut.const_GArtArt_Platinen.ToUpper()))
                        {
                            switch (myEdiCreate.ASN.ASNArt.Typ)
                            {
                                case constValue_AsnArt.const_Art_DESADV_BMW_4a:
                                case constValue_AsnArt.const_Art_DESADV_BMW_4b:
                                case constValue_AsnArt.const_Art_DESADV_BMW_4b_RL:
                                case constValue_AsnArt.const_Art_DESADV_BMW_4b_ST:
                                case constValue_AsnArt.const_Art_DESADV_BMW_6:
                                case constValue_AsnArt.const_Art_DESADV_BMW_6_UB:
                                    result = false;
                                    break;
                            }
                        }
                        break;
                    case EdiSegmentCheck_BMW.const_SegToDeactivate_Code_MEA_SV:
                        switch (myEdiCreate.ASN.ASNArt.Typ)
                        {
                            //case clsASNArt.const_Art_DESADV_BMW_4a:
                            //case clsASNArt.const_Art_DESADV_BMW_4b:
                            //case clsASNArt.const_Art_DESADV_BMW_4b_RL:
                            //case clsASNArt.const_Art_DESADV_BMW_4b_ST:
                            //case clsASNArt.const_Art_DESADV_BMW_6:
                            case constValue_AsnArt.const_Art_DESADV_BMW_6_UB:
                                result = false;
                                break;
                        }
                        break;
                    case EdiSegmentCheck_BMW.const_SegToDeactivate_Code_RFF_ACE:
                        switch (myEdiCreate.ASN.ASNArt.Typ)
                        {
                            //case clsASNArt.const_Art_DESADV_BMW_4a:
                            //case clsASNArt.const_Art_DESADV_BMW_4b:
                            //case clsASNArt.const_Art_DESADV_BMW_4b_RL:
                            //case clsASNArt.const_Art_DESADV_BMW_4b_ST:
                            //case clsASNArt.const_Art_DESADV_BMW_6:
                            case constValue_AsnArt.const_Art_DESADV_BMW_6_UB:
                                result = false;
                                break;
                        }
                        break;

                }
            }
            //switch (myEdiCreate.ASN.ASNFileTyp)
            //{
            //    case clsASNArt.const_Art_DESADV_BMW_4a:
            //    case clsASNArt.const_Art_DESADV_BMW_4b:

            //        break;
            //    case clsASNArt.const_Art_DESADV_BMW_4b_RL:
            //    case clsASNArt.const_Art_DESADV_BMW_4b_ST:
            //    case clsASNArt.const_Art_DESADV_BMW_6:
            //    case clsASNArt.const_Art_DESADV_BMW_6_UB:
            //        if (myEdiCreate.ediSegment.Code.Equals(EdiSegmentCheck_BMW.const_SegToDeactivate_Code_MEA_PD_AAL))
            //        {
            //            if (myEdiCreate.Lager.Artikel.GArt.ArtikelArt.ToUpper().Equals(clsGut.const_GArtArt_Platinen.ToUpper()))
            //            {
            //                result = false;
            //            }
            //        }
            //        break;
            //}
            return result;
        }
    }
}
