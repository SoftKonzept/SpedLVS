using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class CPS_7164_7164_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public CPS_7164_7164_DefaultsValue(Addresses myAdr, AsnArt myAsnArt)
        {
            ValueArt = clsEdiVDAValueAlias.const_EA_ArtPosEoA;
            Value = string.Empty;

            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    ValueArt = Default_VDAClientOut.const_Const;
                    Value = "1";
                    break;

            }
        }
    }
}
