using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.Defaults
{
    public class Default_VDAClientOut_SetArticleItem
    {
        public VDAClientValues VDAClientValues { get; set; } = new VDAClientValues();
        public EdiSegmentElementFields EdiSegmentElementFields { get; set; } = new EdiSegmentElementFields();
        public Default_VDAClientOut_SetArticleItem(VDAClientValues myVdaClientValue, EdiSegmentElementFields myEsef, AsnArt myAsnArt)
        {
            VDAClientValues = myVdaClientValue.Copy();
            EdiSegmentElementFields = myEsef.Copy();

            switch (myAsnArt.Bezeichnung)
            {
                /// -- Benteler ASN
                case constValue_AsnArt.const_Art_EDIFACT_ASN_D97A:
                    switch (EdiSegmentElementFields.Kennung.Substring(0, 3))
                    {
                        case "CPS":
                        case "PAC":
                        case "QTY":
                        case "PCI":
                        case "GIR":
                        case "LIN":
                            VDAClientValues.IsArtSatz = true;
                            break;
                        case "RFF":
                            VDAClientValues.IsArtSatz = EdiSegmentElementFields.Code.Equals("RFF#ON");
                            break;
                        default:
                            VDAClientValues.IsArtSatz = false;
                            break;
                    }
                    break;
            }
        }
    }
}
