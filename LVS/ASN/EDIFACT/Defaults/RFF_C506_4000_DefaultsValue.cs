using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class RFF_C506_4000_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public RFF_C506_4000_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = string.Empty;

            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
                    switch (myEdiSegmentField.Code)
                    {
                        case "RFF#AEV":
                        case "RFF#BT":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                            break;
                        case "RFF#ON":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                            break;
                    }
                    break;
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    switch (myEdiSegmentField.Code)
                    {
                        case "RFF#ON":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                            break;
                    }
                    break;
            }
        }
    }
}
