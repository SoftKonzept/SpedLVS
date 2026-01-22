using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{

    public class PAC_PAC_PAC_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public PAC_PAC_PAC_DefaultsValue(Addresses myAdr, AsnArt myAsnArt)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = PAC.Name;

            //switch (myAsnArt.Typ)
            //{
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
            //        ValueArt = Default_VDAClientOut.const_Const;
            //        Value = "351";
            //        break;
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
            //        ValueArt = Default_VDAClientOut.const_Const;
            //        Value = "20";
            //        break;
            //}

        }


    }
}
