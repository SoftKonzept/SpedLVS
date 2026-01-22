using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class NAD_3207_3207_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public NAD_3207_3207_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = string.Empty;
            //string strTmp = myEdiSegmentField.Code;
            //Value = strTmp.Replace("NAD#", "");

            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
                    ValueArt = Default_VDAClientOut.const_Const;
                    Value = clsEdiVDAValueAlias.const_VDA_Value_NotUsed;
                    break;
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    switch (myEdiSegmentField.Code)
                    {
                        //--- BY = Buyer
                        case "NAD#BY":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "Buyer Country Code";
                            break;
                        //--- CN = Consignee
                        case "NAD#CN":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "Consignee Country Code";
                            break;
                        //---- Delivery Party
                        case "NAD#DP":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "Delivery Party Country Code";
                            break;
                        // --- GM = Inventory controller
                        case "NAD#GM":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "Inventory controller Country Code";
                            break;
                        //---- SE = Seller
                        case "NAD#SE":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "Seller Country Code";
                            break;
                        // ---  WH = Warehouse
                        case "NAD#WH":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "Warehouse Country Code";
                            break;
                    }
                    break;
            }
        }
    }
}
