using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class DTM_C507_2380_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public DTM_C507_2380_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            string strTmp = myEdiSegmentField.Code;
            Value = string.Empty;

            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
                    switch (myEdiSegmentField.Code)
                    {
                        case "DTM#11":
                            ValueArt = clsEdiVDAValueAlias.const_EA_Datum;
                            Value = string.Empty;
                            break;
                        case "DTM#137":
                            ValueArt = clsEdiVDAValueAlias.const_EA_Datum;
                            Value = string.Empty;
                            break;
                    }
                    break;
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    switch (myEdiSegmentField.Code)
                    {
                        case "DTM#102":
                            ValueArt = clsEdiVDAValueAlias.const_EA_Datum;
                            Value = string.Empty;
                            break;
                        case "DTM#171":
                            ValueArt = clsEdiVDAValueAlias.const_EA_Datum;
                            Value = string.Empty;
                            break;
                        case "DTM#179":
                            ValueArt = clsEdiVDAValueAlias.const_EA_Datum;
                            Value = string.Empty;
                            break;
                        case "DTM#182":
                            ValueArt = clsEdiVDAValueAlias.const_EA_Datum;
                            Value = string.Empty;
                            break;
                    }
                    break;
            }

        }
    }
}
