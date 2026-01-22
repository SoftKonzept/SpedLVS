using Common.Models;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class UNB_0020_0020_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public UNB_0020_0020_DefaultsValue(Addresses myAdr, AsnArt myAsnArt)
        {
            ValueArt = clsEdiVDAValueAlias.const_SystemId_Queue;
            Value = string.Empty;

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
