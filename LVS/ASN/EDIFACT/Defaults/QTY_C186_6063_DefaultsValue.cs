using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class QTY_C186_6063_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public QTY_C186_6063_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = string.Empty;
            string strTmp = myEdiSegmentField.Code;
            Value = strTmp.Replace(QTY.Name + "#", "");

            //switch (myAsnArt.Typ)
            //{
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
            //        ValueArt = Default_VDAClientOut.const_Const;
            //        Value = "1";
            //        break;
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
            //        ValueArt = Default_VDAClientOut.const_Const;
            //        Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
            //        break;
            //}
        }
    }
}
