using Common.Models;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class UNB_S002_0004_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public UNB_S002_0004_DefaultsValue(Addresses myAdr, AsnArt myAsnArt)
        {
            ValueArt = clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_UNB_S002_0004_Sender;
            Value = string.Empty; ;

            //switch (myAsnArt.Typ)
            //{
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
            //        ValueArt = Default_VDAClientOut.const_Const;
            //        Value = "3";
            //        break;
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
            //        ValueArt = Default_VDAClientOut.const_Const;
            //        Value = "1";
            //        break;

            //}

        }
    }
}
