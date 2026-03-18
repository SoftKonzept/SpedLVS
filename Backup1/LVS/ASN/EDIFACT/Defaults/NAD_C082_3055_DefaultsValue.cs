using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class NAD_C082_3055_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public NAD_C082_3055_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = string.Empty;
            //string strTmp = myEdiSegmentField.Code;
            //Value = strTmp.Replace("NAD#", "");

            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_ASN_D97A:
                    switch (myEdiSegmentField.Code)
                    {
                        case "NAD#SU":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "92";
                            break;
                        case "NAD#ST":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "10";
                            break;
                    }
                    break;
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    switch (myEdiSegmentField.Code)
                    {
                        //--- BY = Buyer
                        case "NAD#BY":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "92";
                            break;
                        //--- CN = Consignee
                        case "NAD#CN":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "92";
                            break;
                        //---- Delivery Party
                        case "NAD#DP":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "92";
                            break;
                        // --- GM = Inventory controller
                        case "NAD#GM":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "92";
                            break;
                        //---- SE = Seller
                        case "NAD#SE":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "92";
                            break;
                        // ---  WH = Warehouse
                        case "NAD#WH":
                            ValueArt = Default_VDAClientOut.const_Const;
                            Value = "92";
                            break;
                    }
                    break;
            }
        }
    }
}
