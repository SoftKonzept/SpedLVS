using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class RFF_C506_1154_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public RFF_C506_1154_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = string.Empty;
            //string strTmp = myEdiSegmentField.Code;
            //Value = strTmp.Replace(DTM.Name+"#", "");

            switch (myEdiSegmentField.Code)
            {
                case "RFF#AAK":
                    ValueArt = clsEdiVDAValueAlias.const_EA_EingangId;
                    Value = string.Empty;
                    break;
                case "RFF#AFV":
                    ValueArt = clsEdiVDAValueAlias.const_EA_AusgangId;
                    Value = string.Empty;
                    break;
                case "RFF#AEV":
                    ValueArt = clsEdiVDAValueAlias.const_VDA_Value_Blanks;
                    Value = string.Empty;
                    break;
                //-- Order Number - Bestellnummer
                case "RFF#ON":
                    ValueArt = clsEdiVDAValueAlias.const_Artikel_BestellNr;
                    Value = string.Empty;
                    break;
                case "RFF#BT":
                    ValueArt = clsEdiVDAValueAlias.const_Artikel_Charge;
                    Value = string.Empty;
                    break;
            }

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
