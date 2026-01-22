using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class NAD_3035_3035_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public NAD_3035_3035_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            string strTmp = myEdiSegmentField.Code;
            Value = strTmp.Replace("NAD#", "");

            //switch (myAsnArt.Typ)
            //{
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
            //        switch (myEdiSegmentField.Code)
            //        {
            //            case "RFF#AEV":
            //                ValueArt = Default_VDAClientOut.const_Const;
            //                Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
            //                break;
            //            case "RFF#ON":
            //                ValueArt = Default_VDAClientOut.const_Const;
            //                Value = "10";
            //                break;
            //        }
            //        break;
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
            //        switch (myEdiSegmentField.Code)
            //        {
            //            case "NAD#GM":
            //                ValueArt = Default_VDAClientOut.const_Const;
            //                Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
            //                break;
            //        }
            //        break;
            //}
        }
    }
}
