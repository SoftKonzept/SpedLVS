using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class BGM_C106_1004_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public BGM_C106_1004_DefaultsValue(Addresses myAdr, AsnArt myAsnArt)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = string.Empty;

            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
                    ValueArt = clsEdiVDAValueAlias.const_EA_LfsNr;
                    Value = string.Empty;
                    break;
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    ValueArt = clsEdiVDAValueAlias.const_Artikel_LVSNr;
                    Value = string.Empty;
                    break;
            }

        }
    }
}
