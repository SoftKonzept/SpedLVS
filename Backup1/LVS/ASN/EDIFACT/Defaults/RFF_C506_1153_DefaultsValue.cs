using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class RFF_C506_1153_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public RFF_C506_1153_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;

            string strTmp = myEdiSegmentField.Code;
            Value = strTmp.Replace(RFF.Name + "#", "");

            //switch (myAsnArt.Typ)
            //{
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
            //        //ValueArt = Default_VDAClientOut.const_Const;
            //        //Value = "BT";

            //        //switch (myEdiSegmentField.Code)
            //        //{ 

            //        //}

            //        break;
            //    //case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
            //    //    ValueArt = Default_VDAClientOut.const_Const;
            //    //    Value = "20";
            //    //    break;
            //}

        }
    }
}
