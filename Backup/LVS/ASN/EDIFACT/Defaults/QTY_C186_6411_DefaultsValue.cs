using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class QTY_C186_6411_DefaultsValue
    {
        public const string const_MeasureUnitQualifier_KGM = "KGM";
        public const string const_MeasureUnitQualifier_PCI = "PCI";
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public QTY_C186_6411_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = "KGM";
            //string strTmp = myEdiSegmentField.Code;
            //Value = strTmp.Replace(QTY.Name+"#", "");

            //switch (myAsnArt.Typ)
            //{
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
            //        switch (myEdiSegmentField.Code)
            //        {
            //            case "QTY#12":
            //            case "QTY#52":
            //                ValueArt = clsEdiVDAValueAlias.const_ArtFunc_BruttoTO;
            //                Value = clsEdiVDAValueAlias.const_VDA_Value_Empty;
            //                break;
            //        }
            //        break;
            //    case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
            //        switch (myEdiSegmentField.Code)
            //        {                        
            //            case "QTY#52":
            //            case "QTY#156":
            //                ValueArt = clsEdiVDAValueAlias.const_ArtFunc_BruttoTO;
            //                Value = clsEdiVDAValueAlias.const_VDA_Value_Empty;
            //                break;
            //        }
            //        break;
            //}
        }
    }
}
