using LVS.Constants;
using LVS.Models;
using System;

namespace LVS.ZUGFeRD
{
    public class InvoiceToZUGFeRD_Head
    {
        internal Invoices Invoice { get; set; }


        public InvoiceToZUGFeRD_Head(Invoices myInvoice)
        {
            Invoice = myInvoice.Copy();
        }

        public string Name
        {
            get
            {
                string str = string.Empty;
                if (Invoice != null)
                {
                    if (Invoice.IsInvoice)
                    {
                        if (Invoice.IsCancelation)
                        {
                            //str = "RECHNUNGSSTORNO";
                            str = constValue_Invoice.const_RechnungsArt_RGKorrektur;
                        }
                        else
                        {
                            str = constValue_Invoice.const_RechnungsArt_RG;  //"RECHNUNG";
                        }
                    }
                    else
                    {
                        if (Invoice.IsCancelation)
                        {
                            //str = "RECHNUNGSSTORNO";
                            str = constValue_Invoice.const_RechnungsArt_GSKorrektur;
                        }
                        else
                        {
                            str = constValue_Invoice.const_RechnungsArt_GS;  //"RECHNUNG";
                        }
                    }
                    //if (Invoice.IsCancelation)
                    //{
                    //    str = "RECHNUNGSSTORNO";
                    //}
                    //else 
                    //{
                    //    str = "RECHNUNG";
                    //}
                }
                return str;
            }
        }

        public DateTime InvoiceDate
        {
            get
            {
                DateTime dtTmp = new DateTime(1900, 1, 1);
                if (Invoice != null)
                {
                    dtTmp = Invoice.Datum;
                }
                return dtTmp;
            }
        }

        public DateTime OrderDate
        {
            get
            {
                DateTime dtTmp = new DateTime(1900, 1, 1);
                if (Invoice != null)
                {
                    dtTmp = Invoice.Datum;
                }
                return dtTmp;
            }
        }
        public string OrderNo
        {
            get
            {
                string str = string.Empty;
                if (Invoice != null)
                {
                    str = Invoice.InvoiceNo.ToString();
                }
                return str;
            }
        }

        public string InvoiceNo
        {
            get
            {
                string str = string.Empty;
                if (Invoice != null)
                {
                    str = Invoice.InvoiceNo.ToString();
                }
                return str;
            }
        }
        public string PaymentReference
        {
            get
            {
                string str = string.Empty;
                if (Invoice != null)
                {
                    //if (Invoice.IsCancelation)
                    //{
                    //    str = "RECHNUNGSSTORNO " + Invoice.InvoiceNo.ToString() + " vom " + Invoice.Datum.ToString("dd.MM.yyyy");                        
                    //}
                    //else
                    //{
                    //    str = "Rechnung " + Invoice.InvoiceNo.ToString() + " vom " + Invoice.Datum.ToString("dd.MM.yyyy");
                    //}

                    str = this.Name + " " + Invoice.InvoiceNo.ToString() + " vom " + Invoice.Datum.ToString("dd.MM.yyyy");
                }
                return str;
            }
        }
        public string ReferenceOrderNo
        {
            get
            {
                string str = string.Empty;
                if (Invoice != null)
                {

                    str = "Auftrag " + Invoice.InvoiceNo.ToString();
                }
                return str;
            }
        }
        public string InvoiceNote_BillingPeriode
        {
            get
            {
                string str = string.Empty;
                if (Invoice != null)
                {
                    str = "Abrechnungszeitraum von " + Invoice.BillingPeriodStart.ToString("dd.MM.yyyy") + " bis " + Invoice.BillingPeriodEnd.ToString("dd.MM.yyyy");
                }
                return str;
            }
        }
        public string InvoiceNote_PaymentTerms
        {
            get
            {
                string str = null;
                if (Invoice != null)
                {
                    if (!string.IsNullOrWhiteSpace(Invoice.InfoText.TrimEnd()))
                    {
                        str = Invoice.InfoText.TrimEnd();
                    }
                    else
                    {
                        str = $"Zahlbar bis {Invoice.DueDate:dd.MM.yyyy} ohne Abzug.";
                    }
                }
                return str.TrimEnd();
            }
        }
        public int InvoiceNote_PaymentTermsDays
        {
            get
            {
                int iTmp = 10;
                if (Invoice != null)
                {
                    //Datediff
                }
                return iTmp;
            }
        }

    }
}
