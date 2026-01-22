using System;

namespace LVS
{
    public class helper_InvoiceFileName
    {

        public const string const_InvoiceFileName_inclRGAnhang = "_inclRGAnhang";
        public const string const_InvoiceAttachment_FileName = "RGAnhangNr";
        public static string GetInvoiceFileNameWithDatePrefix(int myInvoiceNo)
        {
            string strReturn = string.Empty;
            strReturn += DateTime.Now.ToString("yyyyMMddHHmmss") + "_RGNr" + myInvoiceNo + ".pdf";
            return strReturn;
        }
        public static string GetInvoiceAttachmentFileNameWithDatePrefix(int myInvoiceNo)
        {
            string strReturn = string.Empty;
            strReturn += const_InvoiceAttachment_FileName + myInvoiceNo + ".pdf";
            //strReturn += DateTime.Now.ToString("yyyyMMddHHmmss") + const_InvoiceAttachment_FileName + myInvoiceNo + ".pdf";
            return strReturn;
        }

        public static string GetInvoiceAndAttachmentFileNameWithDatePrefix(int myInvoiceNo)
        {
            string strReturn = string.Empty;
            //strReturn += DateTime.Now.ToString("yyyyMMddHHmmss") + "_RGNr" + myInvoiceNo + "_inclAnhang.pdf";
            strReturn += DateTime.Now.ToString("yyyyMMddHHmmss") + "_RGNr" + myInvoiceNo + helper_InvoiceFileName.const_InvoiceFileName_inclRGAnhang + ".pdf";
            return strReturn;
        }

        public static string GetXInvoiceFileName(int myInvoiceNo, string myPdfFileName)
        {
            string strReturn = string.Empty;
            if (myPdfFileName.EndsWith(helper_InvoiceFileName.const_InvoiceFileName_inclRGAnhang))
            {
                strReturn = helper_InvoiceFileName.GetXInvoiceAndAttachmentFileName(myInvoiceNo);
            }
            else
            {
                strReturn = helper_InvoiceFileName.GetXInvoiceFileName(myInvoiceNo);
            }
            //strReturn += "eRGNr" + myInvoiceNo + ".pdf";
            return strReturn;
        }
        public static string GetXInvoiceFileName(int myInvoiceNo)
        {
            string strReturn = string.Empty;
            strReturn += "eRGNr" + myInvoiceNo + ".pdf";
            return strReturn;
        }
        public static string GetXInvoiceAndAttachmentFileName(int myInvoiceNo)
        {
            string strReturn = string.Empty;
            //strReturn += "eRGNr" + myInvoiceNo + "_inclAnhang.pdf";
            strReturn += "eRGNr" + myInvoiceNo + helper_InvoiceFileName.const_InvoiceFileName_inclRGAnhang + ".pdf";
            return strReturn;
        }


    }
}
