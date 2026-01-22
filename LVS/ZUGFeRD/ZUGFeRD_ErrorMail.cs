using LVS.ViewData;
using System;

namespace LVS.ZUGFeRD
{
    public class ZUGFeRD_ErrorMail
    {
        public ZUGFeRD_ErrorMail(InvoiceViewData myInvoiceVD, string myAttachmentPath, Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            clsMail ErrorMail = new clsMail();
            ErrorMail.InitClass(myGLUser, mySystem);
            ErrorMail.Subject = "TelerikPrint_DirectPrintToPDF - Error Mail E-Rechnung";

            string strMes = "Info zur Rechnung: " + Environment.NewLine;
            strMes += "RG:           " + myInvoiceVD.Invoice.InvoiceNo + "[" + myInvoiceVD.Invoice.Id + "]" + Environment.NewLine;
            strMes += "Datum:        " + myInvoiceVD.Invoice.Datum.ToString("dd.MM.yyyy") + Environment.NewLine;
            strMes += "Empfänger Id: " + myInvoiceVD.Invoice.Receiver + Environment.NewLine;
            strMes += "PDF- Pfad:    " + myAttachmentPath + " [RG als PDF erstellt]" + Environment.NewLine;
            strMes += Environment.NewLine;
            strMes += "InvoiceViewData.ZugferdCheck: " + Environment.NewLine;

            if (myInvoiceVD.ZugferdCheck.LogCheck.Length > 0)
            {
                strMes += "LogCheck: " + Environment.NewLine;
                strMes += myInvoiceVD.ZugferdCheck.LogCheck + Environment.NewLine;
            }
            ErrorMail.Message = strMes;
            if (helper_IOFile.CheckFile(myAttachmentPath))
            {
                ErrorMail.ListAttachment.Add(myAttachmentPath);
            }
            ErrorMail.SendError();

            clsMessages.Allgemein_InfoTextShow(strMes);
        }
    }
}
