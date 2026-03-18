using LVS.ViewData;
using s2industries.ZUGFeRD;

namespace LVS.ZUGFeRD
{
    public class ZUGFeRD_ReferenceDocument
    {
        internal InvoiceViewData invoiceViewData { get; set; }
        public AdditionalReferencedDocument ReferencedDocument { get; set; }
        public ZUGFeRD_ReferenceDocument(int myOriginalInvoiceId)
        {
            invoiceViewData = new InvoiceViewData(myOriginalInvoiceId, 1);
            if (invoiceViewData != null)
            {
                if ((invoiceViewData.Invoice != null) && (invoiceViewData.Invoice.Id == myOriginalInvoiceId))
                {
                    ReferencedDocument = new AdditionalReferencedDocument();
                    ReferencedDocument.ID = invoiceViewData.Invoice.InvoiceNo.ToString();
                    //ReferencedDocument.IssueDateTime = invoiceViewData.Invoice.Datum;
                    if (invoiceViewData.Invoice.IsInvoice)
                    {
                        ReferencedDocument.TypeCode = AdditionalReferencedDocumentTypeCode.InvoiceDataSheet;
                        ReferencedDocument.ReferenceTypeCode = ReferenceTypeCodes.IV;
                    }
                    else
                    {
                        ReferencedDocument.TypeCode = AdditionalReferencedDocumentTypeCode.InvoiceDataSheet;
                        ReferencedDocument.ReferenceTypeCode = ReferenceTypeCodes.CD;
                    }
                }
            }
        }
    }
}
