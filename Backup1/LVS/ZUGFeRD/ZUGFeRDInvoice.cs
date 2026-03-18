using LVS.Models;
using LVS.ViewData;
using s2industries.ZUGFeRD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;



namespace LVS.ZUGFeRD
{
    /// <summary>
    ///             Verwendete NUGET Pakete
    ///             ZUGFeRD-csharp
    /// </summary>
    public class ZUGFeRDInvoice
    {
        internal const string const_BT_24_SpecificationIdentifier = "urn:cen.eu:en16931:2017#compliant#urn:xoev-de:kosit:standard:xrechnung_3.0";
        public string AttachmentPath { get; set; } = string.Empty;
        internal string PdfSourcePath { get; set; } = string.Empty;
        internal string PdfSourceFilename { get; set; } = string.Empty;
        internal Invoices Invoice { get; set; }
        internal AddressViewData adrViewData { get; set; }
        internal InvoiceViewData InvoiceVD { get; set; }
        public List<string> LogMessages { get; set; } = new List<string>();
        internal InvoiceDescriptor desc { get; set; }

        int iCol0Width = 40;
        int iCol1Width = 120;
        public ZUGFeRDInvoice(string mySourcePath, InvoiceViewData myInvoiceViewData)
        {
            LogMessages = new List<string>();
            LogMessages.Add("-" + Environment.NewLine);
            LogMessages.Add("---> Aufruf ZUGFeRDInvoice");
            LogMessages.Add("     |- Z 36 - ZUGFeRDInvoice(string mySourcePath, InvoiceViewData myInvoiceViewData)");
            LogMessages.Add("     |-> Paremter:");
            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "mySourcePath", mySourcePath.ToString()));
            if ((myInvoiceViewData is InvoiceViewData) && (myInvoiceViewData.Invoice is Invoices))
            {
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "InvoiceId:".PadRight(iCol0Width), myInvoiceViewData.Invoice.Id));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Invoice Nr:".PadRight(iCol0Width), myInvoiceViewData.Invoice.InvoiceNo));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Receiver:".PadRight(iCol0Width), myInvoiceViewData.Invoice.AdrReceiver.AddressStringShort));
            }
            else
            {
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "myInvoiceViewData".PadRight(iCol0Width), "Null"));
            }

            InvoiceVD = myInvoiceViewData;
            string SourceFilePathPdf = mySourcePath;
            PdfSourceFilename = System.IO.Path.GetFileName(mySourcePath);
            PdfSourcePath = System.IO.Path.GetDirectoryName(mySourcePath); //System.IO.Path.GetFullPath(mySourcePath);         

            try
            {
                desc = new InvoiceDescriptor();
                

                LogMessages.Add("-" + Environment.NewLine);
                LogMessages.Add("---> Aufruf CreateInvoiceHeadXml()");
                LogMessages.Add("     |- Z 63 - CreateInvoiceHeadXml()");
                CreateInvoiceHeadXml();

                /// --- Erstellen der ZUGFeRD XML Datei
                string strXmlFileName = PdfSourceFilename.Replace(".pdf", "");
                string strXmlFilePath = PdfSourcePath + "\\" + strXmlFileName + ".xml";

                ////Exportieren der XML Datei
                ////desc.Save(strXmlFilePath, ZUGFeRDVersion.Version23, Profile.Extended);
               //desc.Save(strXmlFilePath, ZUGFeRDVersion.Version23, Profile.Comfort);
                //desc.Save(strXmlFilePath, ZUGFeRDVersion.Version23, Profile.XRechnung, ZUGFeRDFormats.CII);
                desc.Save(strXmlFilePath, ZUGFeRDVersion.Version23, Profile.Extended, ZUGFeRDFormats.CII);


                string strTmpeFileName = helper_StringManipulation.DeleteDatePrefix(strXmlFileName);

                string newEmbeddedPdfFileName = helper_InvoiceFileName.GetXInvoiceFileName(myInvoiceViewData.Invoice.InvoiceNo, strTmpeFileName);
                string ePdfOutputPath = PdfSourcePath;
                string ePdfFilePath = Path.Combine(ePdfOutputPath, newEmbeddedPdfFileName);

                //=======================================================================  Telerik
                //TelerikReporting_CreateEmbeddedPdfFile telerikReporting_CreateEmbeddedPdfFile = new TelerikReporting_CreateEmbeddedPdfFile(SourceFilePathPdf, strXmlFilePath, ePdfFilePath);
                try
                {
                    LogMessages.Add("-" + Environment.NewLine);
                    LogMessages.Add("---> Aufruf TelerikReporting_CreateEmbeddedPdfFile");
                    LogMessages.Add("     |- Z 86 - TelerikReporting_CreateEmbeddedPdfFile telerikReporting_CreateEmbeddedPdfFile = new TelerikReporting_CreateEmbeddedPdfFile(SourceFilePathPdf, strXmlFilePath, ePdfFilePath)");
                    LogMessages.Add("     |-> Paremter:");
                    LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "sourceFilePathPdf", SourceFilePathPdf));
                    LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "xmlFilePath", strXmlFilePath));
                    LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "embeddedFilePath", ePdfFilePath));

                    TelerikReporting_CreateEmbeddedPdfFile telerikReporting_CreateEmbeddedPdfFile = new TelerikReporting_CreateEmbeddedPdfFile(SourceFilePathPdf, strXmlFilePath, ePdfFilePath);
                    LogMessages.AddRange(telerikReporting_CreateEmbeddedPdfFile.LogMessages);

                    LogMessages.Add("TelerikReporting_CreateEmbeddedPdfFile - LOG:");
                    LogMessages.AddRange(telerikReporting_CreateEmbeddedPdfFile.LogMessages);

                    LogMessages.Add("-----------------------------------");
                }
                catch (Exception ex)
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
                    clsMail ErrorMail = new clsMail();
                    ErrorMail.InitClass(new Globals._GL_USER(), null);
                    ErrorMail.Subject = "ZUGFeRDInvoice | TelerikReporting_CreateEmbeddedPdfFile - Error Mail E-Rechnung";

                    string strMes = "Exception bei Aufruf TelerikReporting_CreateEmbeddedPdfFile [ZUGFeRDInvoice > Zeile 78]" + Environment.NewLine;

                    strMes += Environment.NewLine + Environment.NewLine;
                    strMes += "-----------------------------------" + Environment.NewLine;
                    strMes += "ZUGFeRDInvoice";
                    strMes += "Paremter:";
                    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "mySourcePath:".PadRight(iCol0Width), mySourcePath);

                    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "ePdfFilePath:".PadRight(iCol0Width), ePdfFilePath);
                    if ((myInvoiceViewData is InvoiceViewData) && (myInvoiceViewData.Invoice is Invoices))
                    {
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "InvoiceId:".PadRight(iCol0Width), myInvoiceViewData.Invoice.Id);
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Invoice Nr:".PadRight(iCol0Width), myInvoiceViewData.Invoice.InvoiceNo);
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "Receiver:".PadRight(iCol0Width), myInvoiceViewData.Invoice.AdrReceiver.AddressStringShort);
                    }
                    else
                    {
                        strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "myInvoiceViewData".PadRight(iCol0Width), "Null");
                    }
                    strMes += "-----------------------------------" + Environment.NewLine;
                    strMes += "TelerikReporting_CreateEmbeddedPdfFile";
                    strMes += "Paremter:";
                    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "SourceFilePathPdf:".PadRight(iCol0Width), SourceFilePathPdf);
                    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "strXmlFilePath:".PadRight(iCol0Width), strXmlFilePath);
                    strMes += string.Format("{0,-" + iCol0Width + "}: {1,-" + iCol1Width + "}", "ePdfFilePath:".PadRight(iCol0Width), ePdfFilePath);

                    strMes += ">>>" + Environment.NewLine;
                    strMes += ">>> ex.Message:" + Environment.NewLine;
                    strMes += ex.Message;
                    strMes += ">>> ex.InnerException:" + Environment.NewLine;
                    strMes += ex.InnerException.ToString();


                    strMes += ">>> Log aus TelerikReporting_CreateEmbeddedPdfFile" + Environment.NewLine;
                    foreach (string logLine in LogMessages)
                    {
                        strMes += logLine + Environment.NewLine;
                    }
                    ErrorMail.Message = strMes;
                    ErrorMail.SendError();
                }

                AttachmentPath = ePdfFilePath;
                string str = string.Empty;
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
        }


        private void CreateInvoiceHeadXml()
        {
            //LogMessages = new List<string>();
            LogMessages.Add("-" + Environment.NewLine);
            LogMessages.Add("---> Start CreateInvoiceHeadXml");

            try
            {
                //LogMessages.Add("-----------------------------------" + Environment.NewLine);
                LogMessages.Add("InvoiceToZUGFeRD_Head - Z 163");
                LogMessages.Add("---> Übergang: InvoiceToZUGFeRD_Head xmlHead = new InvoiceToZUGFeRD_Head(InvoiceVD.Invoice)");
                LogMessages.Add("Paremter");
                if ((InvoiceVD is InvoiceViewData) && (InvoiceVD.Invoice is Invoices))
                {
                    LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Invoice.InvoiceNo", InvoiceVD.Invoice.InvoiceNo));
                    LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Datum", InvoiceVD.Invoice.Datum.ToString("dd.MM.yyyy")));
                    LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Empfänger Id", InvoiceVD.Invoice.Receiver));
                }
                else
                {
                    LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "InvoiceViewData", "IsNull"));
                    LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Invoice", "IsNull"));
                }


                LogMessages.Add("CreateInvoiceHeadXml-Paremter");


                InvoiceToZUGFeRD_Head xmlHead = new InvoiceToZUGFeRD_Head(InvoiceVD.Invoice);
                //-- Rechnung / Storno
                desc.Name = xmlHead.Name;
                desc.IsTest = false;
                //desc.OrderDate = null; // DateTime.Now.AddDays(-2);  Version Version20
                //desc.OrderDate = xmlHead.OrderDate;                                                   -> fehler Zeile 0262
                desc.OrderNo = xmlHead.OrderNo;
                desc.PaymentReference = xmlHead.PaymentReference;
                desc.ReferenceOrderNo = xmlHead.ReferenceOrderNo;

                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.Nameo", desc.Name));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.OrderNo", desc.OrderNo));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.PaymentReference", desc.PaymentReference));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.ReferenceOrderNo", desc.ReferenceOrderNo));

                ///---------------------------------------------------- Rechnungstext 
                ///                ///  SubjectCodes
                ///  - Unknown
                ///  - AAI > generelle Info
                ///  - AAJ > zusätzliche Konditionen
                ///  - ABN > Buchhaltungsinformationen
                ///  - AAK > Preiskonditionen
                ///  - ACB > zusätzliche Angaben
                ///  und weitere
                //desc.Notes = new List<s2industries.ZUGFeRD.Note>();
                s2industries.ZUGFeRD.Note note = new s2industries.ZUGFeRD.Note(xmlHead.InvoiceNote_BillingPeriode, SubjectCodes.ACB, null);
                desc.Notes.Add(note);

                ///--------------------------------------- BT-1 Invoice number
                desc.InvoiceNo = xmlHead.InvoiceNo;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.InvoiceNo", desc.InvoiceNo));
                ///--------------------------------------- BT-2 Invoice issue date 
                desc.InvoiceDate = xmlHead.InvoiceDate;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.InvoiceDate", desc.InvoiceDate.ToString()));
                //---------------------------------------- BT-3 Invoice type code
                /// 326(Partial invoice)
                /// 380(Commercial invoice) = Rechnung 
                /// 384(Corrected invoice) = Korrigierte Rechnung	Neue Rechnung mit korrigierten Werten
                /// 389(Self - billed invoice)
                /// 381(Credit note)       = Gutschrif / Rechnungskorrektur 
                /// 457                    = Storno (Storno einer Belastung	Spezialfall, z. B. bei internen Buchungen)
                /// 875(Partial construction invoice)
                /// 876(Partial final construction invoice)
                /// 877(Final construction invoice

                if (InvoiceVD.Invoice.IsInvoice)
                {
                    if (InvoiceVD.Invoice.IsCancelation)
                    {
                        // 381(Credit note)  
                        desc.Type = InvoiceType.CreditNote;
                        ZUGFeRD_ReferenceDocument tmpRefDoc = new ZUGFeRD_ReferenceDocument(InvoiceVD.Invoice.StornoId);
                        desc.AdditionalReferencedDocuments.Add(tmpRefDoc.ReferencedDocument);
                    }
                    else
                    {
                        //     Commercial invoice (380) is an Invoice This is the main invoice type Handelsrechnung
                        desc.Type = InvoiceType.Invoice;
                    }
                }
                else
                {
                    //     Credit note (381) is a Credit Note This is the main credit note type Gutschriftanzeige
                    desc.Type = InvoiceType.CreditNote;
                    if (InvoiceVD.Invoice.IsCancelation)
                    {
                        ZUGFeRD_ReferenceDocument tmpRefDoc = new ZUGFeRD_ReferenceDocument(InvoiceVD.Invoice.StornoId);
                        desc.AdditionalReferencedDocuments.Add(tmpRefDoc.ReferencedDocument);
                    }
                }
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.Type", desc.Type));

                //----------------------------------------- BT-5  Invoice currency code
                desc.Currency = CurrencyCodes.EUR;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.Currency", desc.Currency));

                //----------------------------------------- BT-6  VAT accounting currency code 
                /// Der Gesamtbetrag der Mehrwertsteuer, ausgedrückt in der im Land des Verkäufers 
                /// akzeptierten oder vorgeschriebenen Rechnungswährung. Ist in Kombination mit 
                /// dem MwSt.-Gesamtbetrag in Rechnungswährung(BT - 111) zu verwenden, 
                /// wenn der MwSt.-Buchführungswährungscode vom Rechnungswährungscode abweicht.
                /// In normalen Fakturierungsszenarien lassen Sie diese Eigenschaft leer!

                //desc.TaxCurrency = CurrencyCodes.EUR;

                //----------------------------------------- BT-7  Value added tax point date 
                /// aktuell nicht verwendet

                //----------------------------------------- BT-8  Value added tax point date code
                /// 3(Invoice document issue date time)
                /// 35(Delivery date / time, actual)
                /// 432(Paid to date)
                /// aktuell nicht verwendet

                //----------------------------------------- BT-9  Payment due date 
                /// Übergabe in den Zahlungsbedingungen siehe BT-20

                //----------------------------------------- BT-10  Buyer reference 
                /// aktuell nicht verwendet

                //----------------------------------------- BT-11 Project reference 

                //----------------------------------------- BT-12 Contract reference
                //ContractReferencedDocument cRD = new ContractReferencedDocument();
                //cRD.IssueDateTime = DateTime.Now.AddDays(-2);    //- Bestell oder Lieferdatum
                //cRD.ID = "101";                          // ID oder Vertragsnummer               
                //desc.ContractReferencedDocument = cRD;
                //desc.ContractReferencedDocument = new ContractReferencedDocument()
                //{
                //    ID="Vertrag 1234",
                //    IssueDateTime = DateTime.Now.AddDays(-2)
                //};

                /// aktuell nicht verwendet
                //----------------------------------------- BT-13 Purchase order reference
                //----------------------------------------- BT-14 Sales order reference
                //----------------------------------------- BT-15 Receiving advice reference 
                //----------------------------------------- BT-16 Despatch advice reference
                //----------------------------------------- BT-17 Tender or lot reference 
                //----------------------------------------- BT-18 Invoiced object identifier
                //----------------------------------------- BT-19 Buyer accounting reference 

                //----------------------------------------- BT-20 Payment terms
                /// incl. Übergabe Zahlungsziel (Due Date)

                var paymentTerms = new PaymentTerms();

               // // Fälligkeitsdatum (BT-9) – hast du ja bereits:  
               // paymentTerms.DueDate = InvoiceVD.Invoice.DueDate;

               // // Beschreibung (BT-20):  
               //// string termsText = $"Zahlbar bis {myDueDate:dd.MM.yyyy} ohne Abzug per Überweisung.";

               // if (!string.IsNullOrWhiteSpace(xmlHead.InvoiceNote_PaymentTerms))
               // {
               //     paymentTerms.Description = xmlHead.InvoiceNote_PaymentTerms;   // BT-20 wird nur gesetzt, wenn nicht leer  
               // }

               // //desc.PaymentTerms = paymentTerms;
               // desc.AddTradePaymentTerms()

                desc.AddTradePaymentTerms(xmlHead.InvoiceNote_PaymentTerms, InvoiceVD.Invoice.DueDate);

                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "xmlHead.InvoiceNote_PaymentTerms", xmlHead.InvoiceNote_PaymentTerms));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "InvoiceVD.Invoice.DueDate", InvoiceVD.Invoice.DueDate));

                //----------------------------------------- BT-21 -> BG-1 INVOICE NOTE
                //----------------------------------------- BT-22 -> BG-1 INVOICE NOTE
                //----------------------------------------- BT-23 Business process type -> BG-2 PROCESS CONTROL 
                desc.BusinessProcess = "urn:fdc:peppol.eu:2017:poacc:billing:01:1.0";
            
                //----------------------------------------- BT-25 Preceding Invoice reference -> BG-3 PRECEDING INVOICE REFERENCE
                //----------------------------------------- BT-26 Preceding Invoice issue date -> BG-3 PRECEDING INVOICE REFERENCE
                ///-------------------------------------------------------------------------------------------------------------------- BG-4 SELLER 
                Party Seller = ZUGFeRD.ZUGFeRD_Party.GetPartyItem(InvoiceVD.mandantenVD.Mandant.Address, InvoiceVD.mandantenVD.Mandant.Mail);
                desc.Seller = Seller;
                //desc.SetSeller(Seller.Name, Seller.Postcode, Seller.City, Seller.Street, (CountryCodes)Seller.Country);
                desc.SetSeller(Seller.Name, Seller.Postcode, Seller.City, Seller.Street, (CountryCodes)Seller.Country, null, Seller.GlobalID, null, null, Seller.CountrySubdivisionName, Seller.AddressLine3);

                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "Seller.Name", Seller.Name));

                ////------------------------------------------------------------------------------------------------------------------ BT-34 Seller electronic address -> BG-4 SELLER 
                desc.SetSellerElectronicAddress(InvoiceVD.mandantenVD.Mandant.Mail, ElectronicAddressSchemeIdentifiers.EM);

                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "InvoiceVD.mandantenVD.Mandant.Mail", InvoiceVD.mandantenVD.Mandant.Mail));
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "ElectronicAddressSchemeIdentifiers.EM", ElectronicAddressSchemeIdentifiers.EM));

                //--------------------------------------------------------------------------------------------------------------------- BT-41 Seller contact point -> BG-4 SELLER 
                string sContactName = InvoiceVD.mandantenVD.Mandant.Contact;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "sContactName", InvoiceVD.mandantenVD.Mandant.Contact));

                //----------------------------------------- BT-42 Seller contact telephone number  -> BG-4 SELLER 
                string sContractPhone = InvoiceVD.mandantenVD.Mandant.Phone;
                //----------------------------------------- BT-43 Seller contact email address -> BG-4 SELLER 
                string sContractMail = InvoiceVD.mandantenVD.Mandant.Mail;

                //---- die Datenfelder Name, Telefon und Mail müssen gefüllt sein
                //---- es dürfen keine leeren xml Felder erstellt werden
                string sContactOrgUnit = InvoiceVD.mandantenVD.Mandant.Organisation;  // Departmentname - Abteilungsname
                if (
                        (!string.IsNullOrEmpty(sContactName)) ||
                        (!string.IsNullOrEmpty(sContractPhone)) ||
                        (!string.IsNullOrEmpty(sContractMail))
                   )
                {
                    desc.SetSellerContact(sContactName, sContactOrgUnit, sContractMail, sContractPhone, "");
                }
                /// aktuell nicht verwendet
                //----------------------------------------- BT-62 Seller tax representative name --------------------------------------- BG-4 SELLER
                //----------------------------------------- BT-63 Seller tax representative VAT identifier 
                //----------------------------------------- BT-31 Seller VAT identifier -> USTID
                desc.AddSellerTaxRegistration(InvoiceVD.mandantenVD.Mandant.VatId.Replace(" ", ""), TaxRegistrationSchemeID.VA);
                //----------------------------------------- BT-32 Seller tax registration identifier -> Steuernummer
                desc.AddSellerTaxRegistration(InvoiceVD.mandantenVD.Mandant.TaxNumber, TaxRegistrationSchemeID.FC);
                //----------------------------------------- BT-33 Seller additional legal information 
                desc.ActualDeliveryDate = InvoiceVD.Invoice.Datum; 
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.ActualDeliveryDate", desc.ActualDeliveryDate));

                ///--------------------------------------------------------------------------------------------------------------------- Buyer 
                ///---- Prüfung Adressen RG Empfänger in Datenfeld Addresses.Adr_RG -> Adresse in Rechnung ist Adr_RG
                int iAdrIdBuyer = InvoiceVD.Invoice.Receiver;
                if (iAdrIdBuyer != InvoiceVD.Invoice.AdrReceiver.AdrId_RG)
                {
                    iAdrIdBuyer = InvoiceVD.Invoice.AdrReceiver.AdrId_RG;
                }
                AddressViewData adrVD = new AddressViewData(iAdrIdBuyer, 1);
                Party Buyer = ZUGFeRD.ZUGFeRD_Party.GetPartyItem(adrVD.Address, adrVD.Address.CustomerData.Mailaddress);
                desc.Buyer = Buyer;
                desc.SetBuyer(Buyer.Name, Buyer.Postcode, Buyer.City, Buyer.Street, (CountryCodes)Buyer.Country, null, Buyer.GlobalID, string.Empty, null, Buyer.CountrySubdivisionName, Buyer.AddressLine3);
                
                LogMessages.Add(string.Format("{0,0} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.Buyer", desc.Buyer));

                ////------------------------------------------------------------------------------------------------------------------ BT-49 Buyer electronic address
                if (!string.IsNullOrEmpty(adrVD.Address.CustomerData.Mailaddress))
                {
                    desc.SetBuyerElectronicAddress(adrVD.Address.CustomerData.Mailaddress, ElectronicAddressSchemeIdentifiers.EM);
                }
                //-------------------------------------------------------------------------- BT-56 Buyer contact point
                string sContactNameB = null;
                if(!string.IsNullOrEmpty(sContactName))
                {
                    sContactNameB = adrVD.Address.CustomerData.Contact;
                }                
                //-------------------------------------------------------------------------- BT-57 Buyer contact telephone number 
                string sContractPhoneB = adrVD.Address.CustomerData.Phone;
                if (!string.IsNullOrEmpty(sContractPhoneB))
                {
                    sContractPhoneB = adrVD.Address.CustomerData.Phone;
                }
                string sContactOrgUnitB = adrVD.Address.CustomerData.Organisation;  // Departmentname - Abteilungsname
                if (!string.IsNullOrEmpty(sContactOrgUnitB))
                {
                    sContactOrgUnitB = adrVD.Address.CustomerData.Organisation;
                }
                //-------------------------------------------------------------------------- BT-58 Buyer contact email address 
                string sContractMailB = adrVD.Address.CustomerData.Mailaddress;
                desc.SetSellerContact(sContactName, sContactOrgUnit, sContractMail, sContractPhone, "");              
               
                //----------------------------------------- BT-62 Buyer tax representative name
                //----------------------------------------- BT-63 Buyer tax representative VAT identifier
                //----------------------------------------- BG-18  PAYMENT CARD INFORMATION
                //----------------------------------------- BT-32 Buyer tax registration identifier
                //----------------------------------------- BT-33 Buyer additional legal information

                //----------------------------------------- BT-48 Buyer VAT identifier -> USTID
                desc.AddBuyerTaxRegistration(adrVD.Address.CustomerData.UstId.Replace(" ",""), TaxRegistrationSchemeID.VA);

                ////--------------------------------------- BT-49 Buyer electronic address
                //ElectronicAddress eaBuyer = new ElectronicAddress();
                //eaBuyer.Address = adrVD.Address.CustomerData.Mailaddress;
                //eaBuyer.ElectronicAddressSchemeID = ElectronicAddressSchemeIdentifiers.EM;  // E-Mail                
                //desc.SellerElectronicAddress = eaBuyer;

                ////----------------------------------------- BT-56 Buyer contact point
                //string sContactNameB = adrVD.Address.CustomerData.Contact;
                ////----------------------------------------- BT-57 Buyer contact telephone number 
                //string sContractPhoneB = adrVD.Address.CustomerData.Phone;
                ////----------------------------------------- BT-58 Buyer contact email address 
                //string sContractMailB = adrVD.Address.CustomerData.Mailaddress;
                //string sContactOrgUnitB = adrVD.Address.CustomerData.Organisation;  // Departmentname - Abteilungsname
                //desc.SetBuyerContact(sContactNameB, sContactOrgUnitB, sContractMailB, sContractPhoneB, "");

                //----------------------------------------- BT-62 Buyer tax representative name
                //----------------------------------------- BT-63 Buyer tax representative VAT identifier
                //----------------------------------------- BG-18  PAYMENT CARD INFORMATION

                //-------------------------------------------------------------------------------- BT-81 Payment means type code
                PaymentMeans pm = new PaymentMeans()
                {
                    //----------------------------------------- BT-81 Payment means type code
                    /// PaymentMeansTypeCodes
                    /// - Unknown
                    /// - NotDefined               
                    /// außerhalb des SEPA-Raumes  Code 30 (Credit transfer (non-SEPA))
                    /// innerhalb des SEPA-Raumes  Code 58 „SEPACreditTransfer“ 
                    TypeCode = PaymentMeansTypeCodes.SEPACreditTransfer,
                    //----------------------------------------- BT-82 Payment means text 
                    Information = "Überweisung",
                    // return Payment means text
                    //----------------------------------------- BT-83 Remittance information
                    //- Verwendungszweck PaymentRefrenz
                    //----------------------------------------- BG-17 CREDIT TRANSFER
                    //- Bankkonto für Zahlung
                    //----------------------------------------- BG-18 PAYMENT CARD INFORMATION
                    FinancialCard = null,
                    //----------------------------------------- BG-19 DIRECT DEBIT 
                    //-- Infos für Lastschrift
                    SEPACreditorIdentifier = null,
                    SEPAMandateReference = null,
                };
                desc.PaymentMeans = pm;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.PaymentMeans", desc.PaymentMeans));

                //--- Bankverbindung RG - Ersteller
                BankAccount bankAccount = new BankAccount()
                {
                    ID = string.Empty, // InvoiceVD.mandantenVD.Mandant.Konto,
                    Name = InvoiceVD.mandantenVD.Mandant.Address.Name1,
                    Bankleitzahl = InvoiceVD.mandantenVD.Mandant.Blz.Replace(" ",""),
                    IBAN = InvoiceVD.mandantenVD.Mandant.Iban.Replace(" ", ""),
                    BIC = InvoiceVD.mandantenVD.Mandant.Bic.Replace(" ", "")
                };
                //--------------------------------------------------------------------------------------- BT-90 DIRECT DEBIT, dann hinzufügen
                desc.CreditorBankAccounts.Add(bankAccount);

                decimal decLineTotalAmount = 0;
                decimal decTaxBasisAmount = 0;
                decimal decTaxTotalAmount = 0;
                decimal decGrandTotalAmount = 0;

                if (InvoiceVD.Invoice.IsInvoice)
                {
                    if (InvoiceVD.Invoice.IsCancelation)
                    {
                        decLineTotalAmount = InvoiceVD.Invoice.NetAmount * (-1);
                        decTaxBasisAmount = InvoiceVD.Invoice.NetAmount * (-1);
                        decTaxTotalAmount = InvoiceVD.Invoice.VatRate * (-1);
                        decGrandTotalAmount = InvoiceVD.Invoice.GrossAmount * (-1);
                    }
                    else
                    {
                        decLineTotalAmount = InvoiceVD.Invoice.NetAmount;
                        decTaxBasisAmount = InvoiceVD.Invoice.NetAmount;
                        decTaxTotalAmount = InvoiceVD.Invoice.VatRate;
                        decGrandTotalAmount = InvoiceVD.Invoice.GrossAmount;
                    }
                }
                else
                {
                    decLineTotalAmount = InvoiceVD.Invoice.NetAmount * (-1);
                    decTaxBasisAmount = InvoiceVD.Invoice.NetAmount * (-1);
                    decTaxTotalAmount = InvoiceVD.Invoice.VatRate * (-1);
                    decGrandTotalAmount = InvoiceVD.Invoice.GrossAmount * (-1);
                }

                ///-----------------------------------------------------------------------------  DOCUMENT TOTALS
                //----------------------------------------- BG-106 Sum of Invoice line net amount > Netto Gesamtbetrag der Positionen
                desc.LineTotalAmount = decLineTotalAmount; // InvoiceVD.Invoice.NetAmount;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.LineTotalAmount", desc.LineTotalAmount));

                ////----------------------------------------- BG-107 Sum of allowances on document level > Gesamtbetrag der Abschläge
                desc.AllowanceTotalAmount = 0.00M;
                ////----------------------------------------- BG-108 Sum of charges on document level > Gesamtbetrag der Zuschläge
                desc.ChargeTotalAmount = 0.0M;
                ////----------------------------------------- BG-109 Invoice total amount without VAT > Netto Rechnungsbetrag Basisbetrag der Steuerberechnung
                desc.TaxBasisAmount = decTaxBasisAmount; // InvoiceVD.Invoice.NetAmount;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.TaxBasisAmount", desc.TaxBasisAmount));

                ////----------------------------------------- BG-110 Invoice total VAT amount > Steuergesamtbetrag
                desc.TaxTotalAmount = decTaxTotalAmount; // InvoiceVD.Invoice.VatRate;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.TaxTotalAmount", desc.TaxTotalAmount));

                ////----------------------------------------- BG-111 Invoice total VAT amount in accounting currency  > Steuergesamtbetrag
                desc.TaxTotalAmount = decTaxTotalAmount; // InvoiceVD.Invoice.VatRate;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.TaxTotalAmount", desc.TaxTotalAmount));

                ////----------------------------------------- BG-112 Invoice total amount with VAT / Gesamt Brutto
                desc.GrandTotalAmount = decGrandTotalAmount; // InvoiceVD.Invoice.GrossAmount;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.GrandTotalAmount", desc.GrandTotalAmount));

                ////----------------------------------------- BG-113 Paid amount / Anzahlungsbetrag
                desc.TotalPrepaidAmount = 0.0M;
                ////----------------------------------------- BG-114 Rounding amount / RoundingAmount / Rundungsbetrag, profile COMFORT and EXTENDED 
                desc.RoundingAmount = 0.0M;
                ////----------------------------------------- BG-115 Amount due for payment / Zahlbetrag / offener Betrag
                desc.DuePayableAmount = decGrandTotalAmount; // InvoiceVD.Invoice.GrossAmount;
                LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "desc.DuePayableAmount", desc.DuePayableAmount));

                //------------------------------------------- BT- 116 VAT category taxable amount
                decimal decTaxBasisAmountAll = (decimal)desc.LineTotalAmount;
                //------------------------------------------- BT- 118 VAT category code 
                TaxCategoryCodes taxCategory = TaxCategoryCodes.S;
                //------------------------------------------- BT- 119 VAT category rate 
                decimal decVatPercent = InvoiceVD.Invoice.Vat;
                ////------------------------------------------ BG-111 Invoice total VAT amount in accounting currency  > Steuergesamtbetrag
                decimal decTaxAmount = (decimal)desc.TaxTotalAmount;

                if (InvoiceVD.Invoice.Vat == 0M)
                {
                    //posTaxType = TaxTypes.FRE;
                    //----------------------------------------- BT- 118 VAT category code 
                    //// - Z = Steuerfrei
                    //// - K = umsatzsteuerfrei für innergemeinschaftliche Leistungen
                    //// - S = Standard
                    taxCategory = TaxCategoryCodes.Z;
                }
                desc.AddApplicableTradeTax(decTaxBasisAmountAll, decVatPercent, decTaxAmount, TaxTypes.VAT, taxCategory, desc.AllowanceTotalAmount, null, null);


                LogMessages.Add("-" + Environment.NewLine);
                LogMessages.Add("------------------------------");
                LogMessages.Add("Paremter - RGPositonen");
                ///======================================================== RG Positionen
                if (InvoiceVD.InvoiceItemVD.ListInvoiceItems.Count > 0)
                {
                    string posLineId = "001";
                    string posName = "Pos.Name";
                    string posDescription = "Positionsdetails";
                    QuantityCodes posUnitCode = QuantityCodes.C62;
                    decimal? posUnitQuantity = null;
                    decimal? posGrossUnitPrice = null;
                    decimal? posNetUnitPrice = null;
                    decimal posBilledQuantity = 0m;
                    decimal? posLineTotalAmount = null;
                    TaxTypes posTaxType = TaxTypes.VAT;
                    TaxCategoryCodes posCategoryCode = TaxCategoryCodes.S;
                    decimal posTaxPercent = 0m;
                    string posComment = null;
                    GlobalID id = null;
                    string sellerAssignedID = "";
                    string buyerAssignedID = "";
                    string posDeliveryNoteID = "";
                    DateTime? posDeliveryNoteDate = null;
                    string posBuyerOrderID = "";
                    DateTime? posBuyerOrderDate = null;
                    DateTime? posBillingPeriodStart = null;
                    DateTime? posBillingPeriodEnd = null;

                    foreach (InvoiceItems item in InvoiceVD.InvoiceItemVD.ListInvoiceItems)
                    {
                        //posLineId = item.Position.ToString().PadLeft(3, '0');
                        //posName = item.RGText;
                        //posDescription = item.InvoiceItemText;
                        //posUnitCode = ZUGFeRD_QuantityCode.ConvertToZUGFeRD_QuantityCode(item);
                        //////---------------------------------------------------------------------------------------------BasisQuantity(BT - 149)
                        //posUnitQuantity = item.Qunatity;
                        //posUnitQuantity = item.PricePerUnitFactor;
                        //posBilledQuantity = item.Qunatity;

                        ////--Unterscheidung GS und Rechnungskorrektur
                        ///// normale GS = Menge positiv und Preis positiv
                        ///// Rechnungskorrektur = Menge negativ und Preis positiv
                        //////---------------------------------------------------------------------------------------------BilledQuantity(BT - 129)
                        //decimal decCalcQuantity = 1;

                        //if (InvoiceVD.Invoice.IsInvoice)
                        //{
                        //    if (InvoiceVD.Invoice.IsCancelation)
                        //    {
                        //        //posBilledQuantity = item.Qunatity * (-1);
                        //        posBilledQuantity = item.PricePerUnitFactor * (-1);
                        //        decCalcQuantity = ((decimal)posBilledQuantity / (decimal)posUnitQuantity);
                        //    }
                        //    else
                        //    {
                        //        //posBilledQuantity = item.Qunatity;
                        //        posBilledQuantity = item.PricePerUnitFactor;
                        //        decCalcQuantity = posBilledQuantity;
                        //    }
                        //}
                        //else
                        //{
                        //    //posBilledQuantity = item.Qunatity * (-1);
                        //    posBilledQuantity = item.PricePerUnitFactor * (-1);
                        //    decCalcQuantity = ((decimal)posBilledQuantity / (decimal)posUnitQuantity);
                        //}

                        ////----------------------------------------------------------------------------------------------NetAmount/Nettopreis (BT-146) 
                        //posNetUnitPrice = item.UnitPrice;
                        ////----------------------------------------------------------------------------------------------LineTotalAmount (BT-131)
                        ////posLineTotalAmount = posNetUnitPrice * (posBilledQuantity / posUnitQuantity); // item.NetAmount   ;   //--- UnitPrice*(BilledQuantity/BasisQuantity)
                        //posLineTotalAmount = posNetUnitPrice * posBilledQuantity;
                        //posTaxPercent = InvoiceVD.Invoice.Vat;

                        //decimal decPosVatRate = decimal.Round(((decimal)posLineTotalAmount * (posTaxPercent / 100)), 2); // decimal.Round((item.NetAmount * (InvoiceVD.Invoice.Vat/100)),2);
                        //                                                                                                 //----------------------------------------------------------------------------------------------Brutto-Listenpreis (BT-148)
                        //posGrossUnitPrice = posLineTotalAmount + decPosVatRate; // item.NetAmount + decPosVatRate;

                        //posBillingPeriodStart = InvoiceVD.Invoice.BillingPeriodStart;
                        //posBillingPeriodEnd = InvoiceVD.Invoice.BillingPeriodEnd;

                        //posTaxType = TaxTypes.VAT;
                        ////----------------------------------------- BT- 118 VAT category code 
                        ////// - Z = Steuerfrei
                        ////// - K = umsatzsteuerfrei für innergemeinschaftliche Leistungen
                        ////// - S = Standard
                        //posCategoryCode = TaxCategoryCodes.S;

                        //if (InvoiceVD.Invoice.VatRate == 0M)
                        //{
                        //    //posTaxType = TaxTypes.FRE;
                        //    //----------------------------------------- BT- 118 VAT category code 
                        //    //// - Z = Steuerfrei
                        //    //// - K = umsatzsteuerfrei für innergemeinschaftliche Leistungen
                        //    //// - S = Standard
                        //    //posCategoryCode = TaxCategoryCodes.Z;
                        //    posCategoryCode = TaxCategoryCodes.Z;
                        //}


                        //desc.AddTradeLineItem(posName                           //Name
                        //                        , (decimal)posNetUnitPrice      //netUnitPrice
                        //                        , QuantityCodes.C62             //unitCode
                        //                        , posDescription                //description
                        //                        , posUnitQuantity               //unitQuantity
                        //                        , posGrossUnitPrice             //grossUnitPrice
                        //                        , posBilledQuantity             // billedQuantity
                        //                        , (decimal)posLineTotalAmount   // lineTotalAmount
                        //                        , (TaxTypes)posTaxType          // taxType
                        //                        , posCategoryCode               // categoryCode
                        //                        , posTaxPercent                 // taxPercent
                        //                        , posComment                    // comment
                        //                        , id                            // id
                        //                        , sellerAssignedID              // sellerAssignedID
                        //                        , buyerAssignedID               // buyerAssignedID
                        //                        , posDeliveryNoteID             // deliveryNoteID
                        //                        , posDeliveryNoteDate           // deliveryNoteDate
                        //                        , string.Empty                  // buyerOrderLineID
                        //                        , posBuyerOrderID               // buyerOrderID
                        //                        , posBuyerOrderDate             // buyerOrderDate 
                        //                        , posBillingPeriodStart
                        //                        , posBillingPeriodEnd
                        //                        );

                        try
                        {
                            ZUGFeRD_TradeLineItem tradeItem = new ZUGFeRD_TradeLineItem(item);
                            LogMessages.Add("-" + Environment.NewLine);
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posLineId ", tradeItem.posLineId));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "UnitCode ", QuantityCodes.C62));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posDescription ", tradeItem.LineItem.Description));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posUnitQuantity ", tradeItem.LineItem.UnitQuantity));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posNetUnitPrice ", tradeItem.LineItem.GrossUnitPrice));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posBilledQuantity", tradeItem.LineItem.BilledQuantity));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posLineTotalAmount ", tradeItem.LineItem.LineTotalAmount));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posTaxType ", tradeItem.LineItem.TaxType));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posTaxCategoryCode ", tradeItem.LineItem.TaxCategoryCode));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posTaxPercent ", tradeItem.LineItem.TaxPercent));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "GlobalID   ", tradeItem.LineItem.GlobalID));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "sellerAssignedID ", tradeItem.LineItem.SellerAssignedID));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posBuyerAssignedID ", tradeItem.LineItem.BuyerAssignedID));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posBillingPeriodStart ", tradeItem.LineItem.BillingPeriodStart));
                            LogMessages.Add(string.Format("{0,5} {1,-" + iCol0Width + "} :{2,-" + iCol1Width + "}", ">", "posBillingPeriodEnd ", tradeItem.LineItem.BillingPeriodEnd));



                            string s = string.Empty;
                            desc.AddTradeLineItem
                                (
                                tradeItem.posLineId,
                                (decimal)tradeItem.LineItem.NetUnitPrice,
                                QuantityCodes.C62,  // tradeItem.LineItem.UnitCode,
                                tradeItem.LineItem.Description,
                                tradeItem.LineItem.UnitQuantity,
                                tradeItem.LineItem.GrossUnitPrice,
                                tradeItem.LineItem.BilledQuantity,
                                tradeItem.LineItem.LineTotalAmount,
                                tradeItem.LineItem.TaxType,
                                tradeItem.LineItem.TaxCategoryCode,
                                tradeItem.LineItem.TaxPercent,
                                null,
                                tradeItem.LineItem.GlobalID,
                                tradeItem.LineItem.SellerAssignedID,
                                tradeItem.LineItem.BuyerAssignedID,
                                string.Empty, //tradeItem.LineItem.DeliveryNoteID,
                                null,         //tradeItem.LineItem.DeliveryNoteDate,
                                string.Empty, //tradeItem.LineItem.BuyerOrderLineID,
                                string.Empty, //tradeItem.LineItem.BuyerOrderID,
                                null,          //tradeItem.LineItem.BuyerOrderDate,
                                tradeItem.LineItem.BillingPeriodStart,
                                tradeItem.LineItem.BillingPeriodEnd
                                );

                            var lineItem = desc.TradeLineItems[0];
                            string str = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            LogMessages.Add(">>>" + Environment.NewLine);
                            LogMessages.Add(">>> -- ZUGFeRD_TradeLineItem" + Environment.NewLine);
                            LogMessages.Add(">>> ex.Message:\"");
                            LogMessages.Add(ex.Message);
                            LogMessages.Add(">>> ex.InnerException:");
                            LogMessages.Add(ex.InnerException.ToString());
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogMessages.Add(">>>" + Environment.NewLine);
                LogMessages.Add(">>> ex.Message:\"");
                LogMessages.Add(ex.Message);
                LogMessages.Add(">>> ex.InnerException:");
                LogMessages.Add(ex.InnerException.ToString());
            }
        }

    }
}
