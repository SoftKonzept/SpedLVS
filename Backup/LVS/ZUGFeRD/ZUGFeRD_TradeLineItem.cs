using DocumentFormat.OpenXml.Office.CustomUI;
using LVS.Models;
using LVS.ViewData;
using s2industries.ZUGFeRD;
using System;

namespace LVS.ZUGFeRD
{
    public class ZUGFeRD_TradeLineItem
    {
        public ZUGFeRD_TradeLineItem(InvoiceItems myItem)
        {
            item = myItem.Copy();
            InvoiceVD = new InvoiceViewData(item.InvoiceId, 1);
        }

        public TradeLineItem LineItem
        {
            get
            {
                TradeLineItem tli = new TradeLineItem(posLineId);
                tli.Name = posName;
                tli.UnitCode = QuantityCodes.C62;
                tli.Description = posDescription;
                tli.UnitQuantity = posUnitQuantity;           // BT-149 = 1.0
                tli.GrossUnitPrice = posGrossUnitPrice;       // BT-148
                tli.BilledQuantity = decCalcQuantity;         // BT-129 (bei Gutschrift negativ!)
                tli.LineTotalAmount = posLineTotalAmount;     // BT-131
                tli.TaxType = posTaxType;
                tli.TaxCategoryCode = posCategoryCode;
                tli.TaxPercent = posTaxPercent;
                tli.ActualDeliveryDate = posDeliveryNoteDate;
                tli.SellerAssignedID = sellerAssignedID;
                tli.BuyerAssignedID = buyerAssignedID;
                tli.DeliveryNoteReferencedDocument = null;
                tli.BuyerOrderReferencedDocument = null;
                tli.BillingPeriodStart = posBillingPeriodStart;
                tli.BillingPeriodEnd = posBillingPeriodEnd;
                tli.ChargeFreeQuantity = 0M;
                tli.ChargeFreeUnitCode = null;
                tli.GlobalID = posId;
                tli.NetUnitPrice = posNetUnitPrice;           // BT-146 (immer positiv!)
                tli.PackageUnitCode = null;
                tli.ShipTo = null;
                tli.TaxExemptionReason = null;
                tli.UltimateShipTo = null;

                return tli;
            }
        }

        internal InvoiceViewData InvoiceVD { get; set; }
        public InvoiceItems item { get; set; }

        public string posLineId
        {
            get { return item.Position.ToString().PadLeft(3, '0'); }
        }

        public string posName
        {
            get { return item.RGText; }
        }

        public decimal posQuantity
        {
            get { return item.Qunatity; }
        }

        public string posDescription
        {
            get { return item.InvoiceItemText; }
        }

        public QuantityCodes posUnitCode
        {
            get
            {
                QuantityCodes tmp = ZUGFeRD_QuantityCode.ConvertToZUGFeRD_QuantityCode(item);
                return tmp;
            }
        }

        //---------------------------------------------------------------------------------------------
        // BT-149: BasisQuantity (immer 1.0 für XRechnung)
        //---------------------------------------------------------------------------------------------
        public decimal posUnitQuantity
        {
            get { return 1M; }
        }

        //---------------------------------------------------------------------------------------------
        // BT-129: BilledQuantity
        // *** ÄNDERUNG: Bei Gutschrift NEGATIV, bei Rechnung POSITIV
        //---------------------------------------------------------------------------------------------
        public decimal decCalcQuantity
        {
            get
            {
                decimal qty = item.PricePerUnitFactor;  // z.B. 1.0m

                if (InvoiceVD.Invoice.IsCancelation)
                {
                    return -Math.Abs(qty);  // z.B. -1.0 für Gutschrift
                }
                else
                {
                    return Math.Abs(qty);   // z.B. +1.0 für Rechnung
                }
            }
        }

        //---------------------------------------------------------------------------------------------
        // BT-146: NetUnitPrice (Netto-Einheitspreis)
        // *** ÄNDERUNG: IMMER POSITIV (BR-27)
        //---------------------------------------------------------------------------------------------
        public decimal posNetUnitPrice
        {
            get
            {
                return Math.Abs(item.UnitPrice);
            }
        }

        //---------------------------------------------------------------------------------------------
        // BT-131: LineTotalAmount (Zeilennettobetrag)
        // Formel: BT-146 × (BT-129 / BT-149)
        // Da BT-149 = 1, vereinfacht sich das zu: BT-146 × BT-129
        // Bei Gutschrift: +1085 × (-1) = -1085 ✓
        //---------------------------------------------------------------------------------------------
        public decimal posLineTotalAmount
        {
            get
            {
                return posNetUnitPrice * decCalcQuantity;
            }
        }

        public decimal posTaxPercent
        {
            get { return InvoiceVD.Invoice.Vat; }
        }

        public decimal decPosVatRate
        {
            get
            {
                return decimal.Round(
                    posLineTotalAmount * (posTaxPercent / 100M),
                    2,
                    System.MidpointRounding.AwayFromZero
                );
            }
        }

        //---------------------------------------------------------------------------------------------  
        // BT-148: GrossUnitPrice (Brutto-Einheitspreis VOR Rabatten, OHNE MwSt!)  
        // NICHT mit MwSt multiplizieren!  
        // Falls keine Rabatte/Zuschläge: GrossUnitPrice = NetUnitPrice  
        //---------------------------------------------------------------------------------------------
        public decimal posGrossUnitPrice
        {
            get
            {
                //return posNetUnitPrice * (1 + (posTaxPercent / 100M));
                // Wenn Sie Rabatte/Zuschläge auf Positionsebene haben:  
                // return posNetUnitPrice + SummeRabatte - SummeZuschläge;  

                // Wenn KEINE Rabatte/Zuschläge (wie in Ihrem Fall):  
                return posNetUnitPrice;
            }
        }

        public DateTime posBillingPeriodStart
        {
            get { return InvoiceVD.Invoice.BillingPeriodStart; }
        }

        public DateTime posBillingPeriodEnd
        {
            get { return InvoiceVD.Invoice.BillingPeriodEnd; }
        }

        public TaxTypes posTaxType
        {
            get { return TaxTypes.VAT; }
        }

        //---------------------------------------------------------------------------------------------
        // BT-118: VAT category code
        // Z = Zero rated (0%)
        // S = Standard rate
        //---------------------------------------------------------------------------------------------
        public TaxCategoryCodes posCategoryCode
        {
            get
            {
                if (InvoiceVD.Invoice.VatRate == 0M)
                {
                    return TaxCategoryCodes.Z;
                }
                else
                {
                    return TaxCategoryCodes.S;
                }
            }
        }

        public string sellerAssignedID
        {
            get { return string.Empty; }
        }

        public string buyerAssignedID
        {
            get { return string.Empty; }
        }

        public string posDeliveryNoteID
        {
            get { return string.Empty; }
        }

        public DateTime? posDeliveryNoteDate
        {
            get { return null; }
        }

        public string posBuyerOrderID
        {
            get { return string.Empty; }
        }

        public DateTime? posBuyerOrderDate
        {
            get { return null; }
        }

        public GlobalID posId
        {
            get { return null; }
        }
    }
}