using Common.Models;
using LVS.ASN.Defaults;
using LVS.Constants;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{
    public class NAD_C082_3039_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public NAD_C082_3039_DefaultsValue(Addresses myAdr, AsnArt myAsnArt, EdiSegmentElementFields myEdiSegmentField)
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
                            ValueArt = clsEdiVDAValueAlias.const_Lieferantennummer;
                            Value = string.Empty;
                            break;
                        case "NAD#ST":
                            ValueArt = clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_ST_Recipient;
                            Value = string.Empty;
                            break;
                    }
                    break;
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    switch (myEdiSegmentField.Code)
                    {
                        //--- BY = Buyer
                        case "NAD#BY":
                            ValueArt = clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_BY_Buyer;
                            Value = string.Empty;
                            break;
                        //--- CN = Consignee
                        case "NAD#CN":
                            ValueArt = clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_CN_Consignee;
                            Value = string.Empty;
                            break;
                        case "NAD#DP":
                            ValueArt = clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_DP_DeliveryPart;
                            Value = string.Empty;
                            break;
                        // --- GM = Inventory controller
                        case "NAD#GM":
                            ValueArt = clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_GM_InventoryController;
                            Value = string.Empty;
                            break;
                        //---- SE = Seller
                        case "NAD#SE":
                            ValueArt = clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SE_Seller;
                            Value = string.Empty;
                            break;
                        // ---  WH = Warehouse
                        case "NAD#WH":
                            ValueArt = clsEdiVDAValueAlias.const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse;
                            Value = string.Empty;
                            break;
                    }
                    break;
            }
        }
    }
}
