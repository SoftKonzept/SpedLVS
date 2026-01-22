using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class INV_4503_4503_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public INV_4503_4503_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = string.Empty;
            //string strTmp = myEdiSegmentField.Code;
            //Value = strTmp.Replace("NAD#", "");

            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
                    ValueArt = Default_VDAClientOut.const_Const;
                    Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    ValueArt = clsEdiVDAValueAlias.const_cFunction_EdifactINVRPT_INV_4503_4503_BalanceMethod;
                    Value = string.Empty;
                    break;
            }
        }
    }
}
