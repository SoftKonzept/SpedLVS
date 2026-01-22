using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class LIN_C212_7140_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public LIN_C212_7140_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = string.Empty;
            //string strTmp = myEdiSegmentField.Code;
            //Value = strTmp.Replace("NAD#", "");

            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
                    ValueArt = clsEdiVDAValueAlias.const_Artikel_Werksnummer;
                    Value = string.Empty;
                    break;
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    ValueArt = clsEdiVDAValueAlias.const_Artikel_Werksnummer;
                    Value = string.Empty;
                    break;
            }
        }
    }
}
