using Common.Models;
using LVS.Helper;
using LVS.ViewData;
using System;

namespace LVS.ZUGFeRD
{
    public class ZUGFeRD_IsAvailable
    {
        public Mandanten Mandant = new Mandanten();
        internal MandantenViewData MandantenVD { get; set; }

        public Addresses AdrInvoiceReceiver = new Addresses();
        internal AddressViewData AdrVD { get; set; }
        public string LogCheck = string.Empty;
        public string LogError = string.Empty;

        internal Globals._GL_SYSTEM GLSystem;

        public bool IsZUGFeRDAvailable { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myMandant"></param>
        /// <param name="myAdr"></param>
        public ZUGFeRD_IsAvailable(Mandanten myMandant, Addresses myAdr)
        {
            if (
                 (myMandant is Mandanten) &&
                 (myMandant.Id > 0) &&
                 (myAdr is Addresses) &&
                 (myAdr.Id > 0)
                )
            {
                Mandant = myMandant;
                AdrInvoiceReceiver = myAdr;
                Check();
            }
            CheckInitValues();
            Check();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLSystem"></param>
        /// <param name="myRGReceiverId"></param>
        /// <param name="myUserId"></param>
        public ZUGFeRD_IsAvailable(Globals._GL_SYSTEM myGLSystem, int myRGReceiverId, int myUserId)
        {
            GLSystem = myGLSystem;
            if (GLSystem.sys_MandantenID > 0)
            {
                MandantenVD = new MandantenViewData((int)GLSystem.sys_MandantenID);
                Mandant = MandantenVD.Mandant.Copy();
            }
            if (myRGReceiverId > 0)
            {
                AdrVD = new AddressViewData(myRGReceiverId, myUserId);
                AdrInvoiceReceiver = AdrVD.Address.Copy();
            }
            CheckInitValues();
            Check();
        }

        public ZUGFeRD_IsAvailable(int myInvoiceId)
        {
            if (myInvoiceId > 0)
            {
                InvoiceViewData invVD = new InvoiceViewData(myInvoiceId, 1);
                if (invVD.Invoice.ClientId > 0)
                {
                    MandantenVD = new MandantenViewData(invVD.Invoice.ClientId);
                    Mandant = MandantenVD.Mandant.Copy();
                }
                if (invVD.Invoice.Receiver > 0)
                {
                    AdrVD = new AddressViewData(invVD.Invoice.Receiver, 1);
                    AdrInvoiceReceiver = AdrVD.Address.Copy();
                }
            }
            CheckInitValues();
            Check();
        }

        private void CheckInitValues()
        {
            LogError = string.Empty;
            //------------------ e Rechnung Eingabefelder ------------------------
            // Mandant Client
            if ((Mandant is null) || (Mandant.Id == 0))
            {
                LogError += "Mandant ist null!" + Environment.NewLine;
            }
            //-- Empfänger
            if ((AdrInvoiceReceiver is null) || (AdrInvoiceReceiver.Id == 0))
            {
                LogError += "Rechnungsempfänger ist null!" + Environment.NewLine;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Check()
        {
            bool bOK = true;
            string strMes = string.Empty;

            //------------------ e Rechnung Eingabefelder ------------------------
            // UST
            if ((Mandant is Mandanten) && (Mandant.VatId.Length == 0))
            {
                strMes = strMes + "Datenfeld Mandant USId ist leer!" + Environment.NewLine;
                bOK = false;
            }
            else
            {
                Helper_VAT_Validation vat = new Helper_VAT_Validation(Mandant.VatId);
                bOK = vat.ValidationOK;
                if (!bOK)
                {
                    strMes = strMes + "Die Mandant UStId: " + Mandant.VatId + " entspricht nicht den Vorgaben eienr UStId!" + Environment.NewLine;
                }
            }
            ////-- Bank
            //if ((Mandant is Mandanten) && (Mandant.Bank.Length == 0))
            //{
            //    strMes = strMes + "Datenfeld Mandant Bankname ist leer!" + Environment.NewLine;
            //    bOK = false;
            //}
            //-- ´BLZ
            if ((Mandant is Mandanten) && (Mandant.Blz.Length == 0))
            {
                strMes = strMes + "Datenfeld Mandant BLZ ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- ´BIC
            if ((Mandant is Mandanten) && (Mandant.Bic.Length == 0))
            {
                strMes = strMes + "Datenfeld Mandant BIC ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- ´Konto
            if ((Mandant is Mandanten) && (Mandant.Konto.Length == 0))
            {
                strMes = strMes + "Datenfeld Mandant Konto ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- ´IBAN
            if (((Mandant is Mandanten) && Mandant.Iban.Length == 0))
            {
                strMes = strMes + "Datenfeld Mandant IBAN ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //------------------------- Kontaktdaten ------------------------
            //-- Ansprechpartner
            if ((Mandant is Mandanten) && (Mandant.Contact.Length == 0))
            {
                strMes = strMes + "Datenfeld Mandant Ansprechpartner ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- Telefon
            if ((Mandant is Mandanten) && (Mandant.Phone.Length == 0))
            {
                strMes = strMes + "Datenfeld Mandant Telefon ist leer!" + Environment.NewLine;
                bOK = false;
            }
            //-- E-Mail
            if ((Mandant is Mandanten) && (Mandant.Mail.Length == 0))
            {
                strMes = strMes + "Datenfeld Mandant E-Mail ist leer!" + Environment.NewLine;
                bOK = false;
            }

            //-- USTId Invoice Receiver
            if ((AdrInvoiceReceiver is Addresses) && (AdrInvoiceReceiver.CustomerData.UstId.Length == 0))
            {
                strMes = strMes + "Datenfeld Adresse|Kundendaten UST-ID des Rechnungsempfänger ist leer!" + Environment.NewLine;
                bOK = false;
            }
            else
            {
                Helper_VAT_Validation vat = new Helper_VAT_Validation(AdrInvoiceReceiver.CustomerData.UstId);
                bOK = vat.ValidationOK;
                if (!bOK)
                {
                    strMes = strMes + "Die Mandant UStId: " + AdrInvoiceReceiver.CustomerData.UstId + " entspricht nicht den Vorgaben eienr UStId!" + Environment.NewLine;
                }
            }
            //-- Mailadresse Invoice Receiver
            if(!helper_EmailValidator.IsValidEmail(AdrInvoiceReceiver.CustomerData.Mailaddress))
            {
                strMes = strMes + "Die E-Mailadresse des Rechnungsempfängers ist ungültig!" + Environment.NewLine;
                bOK = false;
            }

            if (!bOK)
            {
                string strHead = "Leider kann keine e-Rechnung erstellt werden,";
                strHead += "es liegen folgende Fehler vor:" + Environment.NewLine + Environment.NewLine;

                string strErrorMessage = LogError + Environment.NewLine + strMes;
                //strErrorMessage += strMes;

                LogCheck = strHead;
                LogCheck += strErrorMessage;
                //clsMessages.Allgemein_EingabeDatenFehlerhaft(strMes);
            }
            IsZUGFeRDAvailable = bOK;
        }
    }
}
