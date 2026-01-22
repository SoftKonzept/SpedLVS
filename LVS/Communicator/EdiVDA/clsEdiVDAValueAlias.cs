using LVS.ASN.ASNFormatFunctions;
using LVS.Communicator.EdiVDA.EdiVDAValues;
using System.Collections.Generic;
using System.Data;

namespace LVS
{
    public class clsEdiVDAValueAlias
    {

        //public const string const_EDIFACT_NotUsedEdiSegmentElement = "#NOTUSED#";

        /*************************************************************************
        *                      spezielle Client Function
        * **********************************************************************/
        public const string const_cFunction_VDACustomizedValue = "#VDACustomizedValue#";

        public const string const_cFunction_SIL_716F03 = "#SIL_716F03#";
        public const string const_cFunction_SIL_ProdNrCHeck = "#SIL_SIL_ProdNrCHeck#";
        public const string const_cFunction_SZG_LVSForVW = VW_LVSForVW.const_VW_LVSForVW;   // "#LVSForVW#";
        public const string const_cFunction_SZG_TMN = SZG_TMN.const_SZG_TMN;                // "#SZG_TMN#";
        public const string const_cFunction_SLE_VGS = "#SLE_VGS#";
        public const string const_cFunction_Arcelor_EABmwFormat = Arcelor_EA_BMWFormat.const_Arcelor_EA_BMWFormat;
        public const string const_cFunction_BMW_VGS = BMW_VGS.const_BMW_VGS;
        public const string const_cFunction_BMW_SLB = BMW_SLB.const_BMW_SLB;
        public const string const_cFunction_BMW_EANo = BMW_EANo.const_BMW_EANo;
        public const string const_cFunction_BMW_EANoWithPrefix = BMW_EANoWithPrefix.const_BMW_EANoWithPrefix;
        public const string const_cFunction_BMW_PACNumberBolzen = BMW_PACNumberBolzen.const_BMW_PACNumberBolzen;
        public const string const_cFunction_BMW_Einheit = BMW_Einheit.const_BMW_Einheit;
        public const string const_cFunction_BMW_EDIFACTRecipientId = BMW_UNB_S003_0010.const_BMW_EDIFACTRecipientId;
        public const string const_cFunction_BMW_EDIFACTSenderId = BMW_UNB_S002_0004.const_BMW_EDIFACTSenderId;
        public const string const_cFunction_BMW_GlowDate = BMW_GlowDate.const_BMW_GlowDate;
        public const string const_cFunction_BMW_MATDate = BMW_MATDate.const_BMW_MATDate;
        public const string const_cFunction_BMW_MATTime = BMW_MATTime.const_BMW_MATTime;
        public const string const_cFunction_BMW_Schaden = BMW_Schaden.const_BMW_Schaden;
        public const string const_cFunction_BMW_713F17_KGorSTK = BMW_713F17KGorSTK.const_BMW_713F17_KGorSTK;
        public const string const_cFunction_BMW_714F06Brutto = BMW_714F06Brutto.const_BMW_714F06Brutto;
        public const string const_cFunction_BMW_714F08Netto = BMW_714F08Netto.const_BMW_714F08Netto;
        public const string const_cFunction_BMW_714F09Einheit = BMW_714F09Einheit.const_BMW_714F09Einhei;
        public const string const_cFunction_BMW_RFF_ACD_C506_1154 = BMW_RFF_ACD_C506_1154.const_BMW_RFF_ACD_C506_1154;
        public const string const_cFunction_BMW_BGM_C002_1000 = BMW_BGM_C002_1000.const_BMW_BGM_C002_1000;
        public const string const_cFunction_BMW_BGM_C106_1004_EANr = BMW_BGM_C106_1004_EANr.const_BMW_BGM_C106_1004_EANr;
        public const string const_cFunction_BMW_MEA_C174_6314_ArticleNettoTO = BMW_MEA_C174_6314_ArticleNettoTO.const_BMW_MEA_C174_6314_ArticleNettoTO;
        public const string const_cFunction_BMW_MEA_C174_6314_KGorStkBruttoTO = BMW_MEA_C174_6314_KGorStkBruttoTO.const_BMW_MEA_C174_6314_KGorStkBruttoTO;

        public const string const_cFunction_Hydro_712F03SLB = Hydro_712F03SLB.const_Hydro_712F03SLB;

        public const string const_cFunction_EdifactINVRPT_INV_4501_4501_MessageType = EdifactINVRPT_INV_4501_4501_MessageType.const_EdifactINVRPT_INV_4501_4501_MessageType;
        public const string const_cFunction_EdifactINVRPT_INV_7491_7491_TypeMovement = EdifactINVRPT_INV_7491_7491_TypeMovement.const_EdifactINVRPT_INV_7491_7491_TypeMovement;
        public const string const_cFunction_EdifactINVRPT_INV_4499_4499_MovementReason = EdifactINVRPT_INV_4499_4499_MovementReason.const_EdifactINVRPT_INV_4499_4499_MovementReason;
        public const string const_cFunction_EdifactINVRPT_INV_4503_4503_BalanceMethod = EdifactINVRPT_INV_4503_4503_BalanceMethod.const_EdifactINVRPT_INV_4503_4503_BalanceMethod;

        public const string const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_BY_Buyer = EdiClientWorkspaceValue_NAD_C082_3039_BY_Buyer.const_EdiClientWorkspaceValue_NAD_C082_3039_Buyer;
        public const string const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_CN_Consignee = EdiClientWorkspaceValue_NAD_C082_3039_CN_Consignee.const_EdiClientWorkspaceValue_NAD_C082_3039_Consignee;
        public const string const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_DP_DeliveryPart = EdiClientWorkspaceValue_NAD_C082_3039_DP_DeliveryPart.const_EdiClientWorkspaceValue_NAD_C082_3039_DeliveryPart;
        public const string const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_GM_InventoryController = EdiClientWorkspaceValue_NAD_C082_3039_GM_InventoryController.const_EdiClientWorkspaceValue_NAD_C082_3039_InventoryController;
        public const string const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_ST_Recipient = EdiClientWorkspaceValue_NAD_C082_3039_ST_RecipienNo.const_EdiClientWorkspaceValue_NAD_C082_3039_ST_RecipienNo;
        public const string const_cFunction_EdiAdrWorkspaceAssign_NAD_C082_3039_ST_Recipient = EdiClientWorkspaceValue_NAD_C082_3039_ST_RecipienNo.const_EdiAdrWorkspaceAssign_NAD_C082_3039_ST_RecipienNo;  //-- alt muss aber erhalten bleiben
        public const string const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SE_Seller = EdiClientWorkspaceValue_NAD_C082_3039_SE_Seller.const_EdiClientWorkspaceValue_NAD_C082_3039_Seller;
        public const string const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SU_Supplier = EdiClientWorkspaceValue_NAD_C082_3039_SU_SupplierNo.const_EdiClientWorkspaceValue_NAD_C082_3039_SupplierNo;
        public const string const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse = EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse.const_EdiClientWorkspaceValue_NAD_C082_3039_Warehouse;

        public const string const_cFunction_EdiClientWorkspaceValue_UNB_S002_0004_Sender = EdiClientWorkspaceValue_UNB_S002_0004_Sender.const_EdiClientWorkspaceValue_UNB_S002_0004_Sender;
        public const string const_cFunction_EdiAdrWorkspaceAssign_UNB_S002_0004_Sender = EdiClientWorkspaceValue_UNB_S002_0004_Sender.const_EdiAdrWorkspaceAssign_UNB_S002_0004_Sender; //-- alt muss aber erhalten bleiben
        public const string const_cFunction_EdiClientWorkspaceValue_UNB_S003_0010_Receiver = EdiClientWorkspaceValue_UNB_S003_0010_Receiver.const_EdiClientWorkspaceValue_UNB_S003_0010_Receiver;
        public const string const_cFunction_EdiAdrWorkspaceAssign_UNB_S003_0010_Receiver = EdiClientWorkspaceValue_UNB_S003_0010_Receiver.const_EdiAdrWorkspaceAssign_UNB_S003_0010_Receiver; //-- alt muss aber erhalten bleiben
        public const string const_cFunction_EdiClientWorkspaceValue_UNB_S003_0010_Client = EdiClientWorkspaceValue_UNB_S003_0010_Client.const_EdiClientWorkspaceValue_UNB_S003_0010_Client;
        public const string const_cFunction_EdiClientWorkspaceValue_UNB_S004_0017_DateOfCreation = EdiClientWorkspaceValue_UNB_S004_0017_DateOfCreation.const_EdiAdrWorkspaceAssign_UNB_S004_0017_DateOfCreation;

        public const string const_cFunction_MUBEA_712F03SLB = MUBEA_712F03SLB.const_MUBEA_712F03SLB;
        public const string const_cFunction_MENDRITZKI_Charge = MENDRITZKI_Charge.const_MENDRITZKI_Charge;
        public const string const_cFunction_MENDRITZKI_Produktionsnummer = MENDRITZKI_Produktionsnummer.const_MENDRITZKI_Produktionsnummer;

        public const string const_cFunction_Novelis_F13F03_LfsNo = Novelis_F13F03_LfsNo.const_Novelis_F13F03_LfsNo;


        public const string const_cFunction_SLBWithPrefix = SLBWithPrefix.const_SLBWithPrefix;

        public const string const_cFunction_Tata_713F03LfsNr = Tata_713F03LfsNr.const_Tata_713F03LfsNr;
        public const string const_cFunction_VOEST_EA_SLB = VOEST_EA_SLB.const_EA_SLB;

        public const string const_cFunction_VW_BruttoKGorSt = VW_BruttoKGorST.const_VW_BruttoKGorST; // "#BruttoKGorST#";
        public const string const_cFunction_VW_NettoKGorSt = VW_NettoKGorST.const_VW_NettoKGorST; // "#NettoKGorST#";
        public const string const_cFunction_VW_EinheitKGorSt = VW_EinheitKGorST.const_VW_EinheitKGorST; // "#EinheitKGorSt#";
        public const string const_cFunction_VW_QTY_EinheitPCIorKGM = VW_QTY_EinheitPCIorKGM.const_VW_QTY_EinheitPCIorKGM;
        public const string const_cFunction_VW_QTY_BruttoOrStk = VW_QTY_BruttoOrStk.const_VW_QTY_BruttoOrStk;
        public const string const_cFunction_VW_SupplierNo = VW_SupplierNo.const_VW_SupplierNo;

        public const string const_cFunction_VW_SchadenTopOne = VW_SchadenText.const_VW_SchadenText;  // "#SchadenText#";
        public const string const_cFunction_VW_GIN_ML_C208 = VW_GIN_ML_C208.const_VW_GIN_ML_C208;
        public const string const_cFunction_VW_RFF_ON_C506 = VW_RFF_ON_C506.const_VW_RFF_ON_C506;
        public const string const_cFunction_VW_TMN = VW_TMN.const_SZG_TMN;                // "#SZG_TMN#";


        public const string const_Reciever = "#Receiver#";
        public const string const_RecieverNo = ReceiverNo.const_ReceiverNo; //"#Receiver#";
        public const string const_Sender = Sender.const_Sender;//"#Sender#";
        public const string const_Lieferantennummer = SupplierNo.const_Lieferantennummer; // "#Lieferantennummer#";
        public const string const_SupplierNo = SupplierNo.const_SupplierNo;  //"#SupplierNo#";
        public const string const_SIDOld = "#SIDOld#";
        public const string const_SIDNew = "#SIDNew#";

        public const string const_DUNSNrMS = DUNSNr.const_DUNSNrMS;  // EDI Sender
        public const string const_DUNSNrSF = DUNSNr.const_DUNSNrSF;  // Warenversender / Lieferant
        public const string const_DUNSNrST = DUNSNr.const_DUNSNrST;  // Warenempfänger / Empfänger
        public const string const_DUNSNrFW = DUNSNr.const_DUNSNrFW;  // Spediteur / Transportunternehmen
        public const string const_DUNSNrSE = DUNSNr.const_DUNSNrSE;  // Verkäufer

        public const string const_SystemId_Queue = QueueID.const_SystemId_Queue;


        public const string const_Vorgang = VGS.const_VGS; // "#VGS#";
        public const string const_TransportMittelSchlüssel = TMS.const_TMS; // "#TMS#";    //
        public const string const_TransportMittelNummer = TMN.const_TMN; // "#TMN#";       //Satz712F15
        public const string const_VDA_Value_NOW = "#NOW#";              // DAtum YYmmdd
        public const string const_VDA_Value_TimeNow = "#TIMENOW#";              // DAtum YYmmdd
        public const string const_VDA_Value_NOWTIME = "#NOWTIME#";
        public const string const_VDA_Value_Blanks = "#BLANKS#";
        public const string const_VDA_Value_const = "const";
        public const string const_VDA_Value_Empty = "#Empty#";

        public const string const_VDA_Value_NotUsed = "#NOTUSED#";

        public const string const_EDI_Value_SegementCount = "#SegmentCount#";
        public const string const_EDI_Value_NachrichtenReferenz = "#NachrichtenReferenz#";

        public const string const_TransportMittelNummerTrim = "#TMNTrim#";




        /*******************************************************************************
         *                  function auf Artikeldaten
         * ****************************************************************************/
        public const string const_ArtFunc_PACNumber = PACNumber.const_PACNumber;

        public const string const_ArtFunc_PACBolzenQuantity = PACBolzenQuantity.const_PACNumberBolzen;
        public const string const_ArtFunc_WerksnummerOhneBlank = WerksnummerOhneBlank.const_WerksnummerOhneBlank; // "#WerksnummerOhneBlank#";
        public const string const_ArtFunc_WerksnummerMitFuehrendemBlank = WerksnummerMitFBlank.const_WerksnummerMitFBlank; //"#WerksnummerMitFBlank#";
        public const string const_ArtFunk_WerksnummerFormatVW = VW_WerksnummerFormat.const_VW_WerksnummerFormat; // "#WerksnummerVWFormat#";
        public const string const_ArtFunk_WerksnummerWihtHyphen = WerksnummerWithHyphen.const_WerksnummerWithHyphen; // Werksnummer mit BIndestich vor Index 00
        public const string const_ArtFunk_WerksnummerWihtOutHyphen = WerksnummerWithOutHyphen.const_WerksnummerWithOutHyphen; // Werksnummer mit BIndestich vor Index 00

        public const string const_ArtFunc_BruttoTO = Artikel_BruttoTO.const_Artikel_BruttoTO; //
        public const string const_ArtFunc_NettoTO = Artikel_NettoTO.const_Artikel_NettoTO; //

        public const string const_ArtFunc_ProduktionsnummerFillTo9With0 = ProduktionsnummerFillTo9With0.const_ProduktionsnummerFillTo9With0;  // "#ProduktionsnummerFillTo9With0#";
        public const string const_ArtFunc_Anzahlx1000 = "#Anzahlx1000#";
        public const string const_ArtFunc_LVSNr8Stellig = "#LVSNr8Stellig#";

        public const string const_ArtFunc_GlowDateToEdi_yyyyMMdd = Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyyyMMdd;
        public const string const_ArtFunc_GlowDateToEdi_yyyyMMddOrBlank = Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyyyMMddOrBlank;
        public const string const_ArtFunc_GlowDateToEdi_yyMMdd = Format_GlowDateToEdi.const_Function_GlowDateToEdi_yyMMdd;
        public const string const_ArtFunc_GlowDateToEdi_ddMMyyyy = Format_GlowDateToEdi.const_Function_GlowDateToEdi_ddMMyyyy;
        public const string const_ArtFunc_GlowDateToEdi_ddMMyy = Format_GlowDateToEdi.const_Function_GlowDateToEdi_ddMMyy;

        public static List<string> ListValue_Functions
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_cFunction_VDACustomizedValue
                    ,const_cFunction_Arcelor_EABmwFormat

                    ,const_cFunction_BMW_713F17_KGorSTK
                    ,const_cFunction_BMW_714F06Brutto
                    ,const_cFunction_BMW_714F08Netto
                    ,const_cFunction_BMW_714F09Einheit
                    ,const_cFunction_BMW_EANoWithPrefix
                    ,const_cFunction_BMW_BGM_C002_1000
                    ,const_cFunction_BMW_BGM_C106_1004_EANr
                    ,const_cFunction_BMW_EDIFACTRecipientId
                    ,const_cFunction_BMW_EDIFACTSenderId
                    ,const_cFunction_BMW_Einheit
                    ,const_cFunction_BMW_GlowDate
                    ,const_cFunction_BMW_MATDate
                    ,const_cFunction_BMW_MATTime
                    ,const_cFunction_BMW_MEA_C174_6314_ArticleNettoTO
                    ,const_cFunction_BMW_MEA_C174_6314_KGorStkBruttoTO
                    ,const_cFunction_BMW_PACNumberBolzen
                    ,const_cFunction_BMW_RFF_ACD_C506_1154
                    ,const_cFunction_BMW_Schaden
                    ,const_cFunction_BMW_SLB
                    ,const_cFunction_BMW_VGS

                    ,const_cFunction_EdifactINVRPT_INV_4501_4501_MessageType
                    ,const_cFunction_EdifactINVRPT_INV_4503_4503_BalanceMethod
                    ,const_cFunction_EdifactINVRPT_INV_4499_4499_MovementReason
                    ,const_cFunction_EdifactINVRPT_INV_7491_7491_TypeMovement

                    ,const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_BY_Buyer
                    ,const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_CN_Consignee
                    ,const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_DP_DeliveryPart
                    ,const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_GM_InventoryController
                    ,const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_ST_Recipient
                    ,const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SU_Supplier
                    ,const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_SE_Seller
                    ,const_cFunction_EdiClientWorkspaceValue_UNB_S002_0004_Sender
                    ,const_cFunction_EdiClientWorkspaceValue_UNB_S003_0010_Client
                    ,const_cFunction_EdiClientWorkspaceValue_UNB_S003_0010_Receiver
                    ,const_cFunction_EdiClientWorkspaceValue_UNB_S004_0017_DateOfCreation
                    ,const_cFunction_EdiClientWorkspaceValue_NAD_C082_3039_WH_Warehouse

                    ,const_cFunction_Hydro_712F03SLB
                    ,const_cFunction_MENDRITZKI_Charge
                    ,const_cFunction_MENDRITZKI_Produktionsnummer
                    ,const_cFunction_MUBEA_712F03SLB

                    ,const_cFunction_Novelis_F13F03_LfsNo

                    ,const_cFunction_SIL_716F03
                    ,const_cFunction_SIL_ProdNrCHeck
                    ,const_cFunction_SLE_VGS
                    ,const_cFunction_SZG_TMN
                    ,const_cFunction_SZG_LVSForVW
                    ,const_cFunction_SLBWithPrefix
                    ,const_cFunction_Tata_713F03LfsNr

                    ,const_cFunction_VOEST_EA_SLB

                    ,const_cFunction_VW_BruttoKGorSt
                    ,const_cFunction_VW_EinheitKGorSt
                    ,const_cFunction_VW_GIN_ML_C208
                    ,const_cFunction_VW_RFF_ON_C506
                    ,const_cFunction_VW_NettoKGorSt
                    ,const_cFunction_VW_QTY_BruttoOrStk
                    ,const_cFunction_VW_QTY_EinheitPCIorKGM
                    ,const_cFunction_VW_SupplierNo
                    ,const_cFunction_VW_SchadenTopOne
                    ,const_cFunction_VW_TMN

                    ,const_EDI_Value_SegementCount
                    ,const_EDI_Value_NachrichtenReferenz
                    ,const_Reciever
                    ,const_RecieverNo
                    ,const_Sender
                    ,const_Lieferantennummer
                    ,const_SIDOld
                    ,const_SIDNew
                    ,const_SystemId_Queue
                    ,const_Vorgang
                    ,const_TransportMittelSchlüssel
                    ,const_TransportMittelNummer
                    ,const_TransportMittelNummerTrim
                    ,const_VDA_Value_NOW
                    ,const_VDA_Value_TimeNow
                    ,const_VDA_Value_Blanks
                    ,const_ArtFunk_WerksnummerWihtHyphen
                    ,const_ArtFunk_WerksnummerWihtOutHyphen
                    ,const_ArtFunc_WerksnummerOhneBlank
                    ,const_ArtFunc_WerksnummerMitFuehrendemBlank
                    ,const_ArtFunk_WerksnummerFormatVW
                    ,const_ArtFunc_BruttoTO
                    ,const_ArtFunc_NettoTO


                    ,const_ArtFunc_Anzahlx1000
                    ,const_ArtFunc_GlowDateToEdi_yyyyMMdd
                    ,const_ArtFunc_GlowDateToEdi_yyyyMMddOrBlank
                    ,const_ArtFunc_GlowDateToEdi_yyMMdd
                    ,const_ArtFunc_GlowDateToEdi_ddMMyyyy
                    ,const_ArtFunc_GlowDateToEdi_ddMMyy
                    ,const_ArtFunc_LVSNr8Stellig
                    ,const_ArtFunc_PACBolzenQuantity
                    ,const_ArtFunc_PACNumber
                    ,const_ArtFunc_ProduktionsnummerFillTo9With0

                    ,const_EFunc_LieferantennummerDeleteSlash

                    ,const_VDA_Value_const
                    ,const_VDA_Value_Empty
                };
                tmp.Sort();
                return tmp;
            }
        }


        /*************************************************************************
         *                LEingangsdaten
         * **********************************************************************/
        public const string const_Eingang_ID = "Eingang.ID";
        public const string const_Eingang_EingangID = "Eingang.EingangID";
        public const string const_Eingang_Datum = "Eingang.Datum";
        public const string const_Eingang_LfsNr = "Eingang.LfsNr";
        public const string const_Eingang_KFZ = "Eingang.KFZ";
        public const string const_Eingang_Waggon = "Eingang.Waggon";
        public const string const_Eingang_ExTransportRef = "Eingang.ExTransportRef";
        public const string const_Eingang_ExAuftragRef = "Eingang.ExAuftragRef";
        public const string const_Eingang_Brutto = "Eingang.Brutto";
        public const string const_Eingang_Netto = "Eingang.Netto";
        public const string const_Eingang_Anzahl = "Eingang.Anzahl";


        public static List<string> ListValue_Eingang
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_Eingang_ID
                    ,const_Eingang_EingangID
                    ,const_Eingang_Datum
                    ,const_Eingang_LfsNr
                    ,const_Eingang_KFZ
                    ,const_Eingang_Waggon
                    ,const_Eingang_ExTransportRef
                    ,const_Eingang_ExAuftragRef
                    ,const_Eingang_Brutto
                    ,const_Eingang_Netto
                    ,const_Eingang_Anzahl

                };
                tmp.Sort();
                return tmp;
            }
        }


        public const string const_EFunc_LieferantennummerDeleteSlash = LiefNrDeleteSlash.const_LiefNrDeleteSlash;//"#LiefNrDeleteSlash#";


        /*************************************************************************
         *                LAusgangsdaten
         * **********************************************************************/
        public const string const_Ausgang_ID = "Ausgang.ID";
        public const string const_Ausgang_LAusgangID = "Ausgang.LAusgangID";
        public const string const_Ausgang_SLB = "Ausgang.SLB";


        public static List<string> ListValue_Ausgang
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_Ausgang_ID
                    ,const_Ausgang_LAusgangID
                    ,const_Ausgang_SLB
                };
                tmp.Sort();
                return tmp;
            }
        }

        /*************************************************************************
         *                   EA Daten 
         * **********************************************************************/
        public const string const_EA_ArtPosEoA = EA_ArtPosEoA.const_EA_ArtPosEoA; //"#ArtPosEoA#";
        public const string const_EA_ID = "EA.ID";
        public const string const_EA_EingangId = EA_EingangId.const_EA_EingangId;
        public const string const_EA_AusgangId = EA_AusgangId.const_EA_AusgangId;
        public const string const_EA_EANr = EA_No.const_EA_Nr; //"EA.Nr";
        public const string const_EAFunc_EANrWithPrefix = EA_NoWithPrefix.const_EA_NoWithPrefix;
        public const string const_EA_Datum = EA_Datum.const_EA_Datum; // "EA.Datum";
        public const string const_EA_LfsNr = EA_LfsNr.const_EA_LfsNr; // "EA.LfsNr";
        public const string const_EA_ExTransportRef = "EA.ExTransportRef";
        public const string const_EA_ExAuftragRef = "EA.ExAuftragRef";
        public const string const_EA_Brutto = EA_Brutto.const_EA_Brutto; // "EA.Brutto";
        //public const string const_EA_BruttoTO = EA_Brutto.const_EA_Brutto; // "EA.Brutto";
        public const string const_EA_Netto = EA_Netto.const_EA_Netto; // "EA.Netto";
        public const string const_EA_Anzahl = EA_Anzahl.const_EA_Anzahl; // "EA.Anzahl";
        public const string const_EAFunc_EAAnzahlX1000 = EA_AnzahlX1000.const_EA_AnzahlX1000;
        public const string const_EA_ArtikelCount = EA_ArtikelCount.const_EA_ArtikelCount;
        public const string const_EA_Termin = EA_Termin.const_EA_Termin; // "EA.Termin";
        public const string const_EA_SLB = EA_SLB.const_EA_SLB; // "EA.SLB";
        public const string const_EA_MATDate = EA_MATDate.const_EA_MATDate;// "EA.MATDate";
        public const string const_EA_MATTime = EA_MATTime.const_EA_MATTime; // "EA.MATTime";
        public const string const_EA_Auftraggeber = "EA.Auftraggeber";
        public const string const_EA_AuftraggeberName = "EA.AuftraggeberName";
        public const string const_EA_AuftraggeberStr = "EA.AuftraggeberStr";
        public const string const_EA_AuftraggeberPLZ = "EA.AuftraggeberPLZ";
        public const string const_EA_AuftraggeberOrt = "EA.AuftraggeberOrt";
        public const string const_EA_Empfaenger = "EA.Empfaenger";
        public const string const_EA_EmpfaengerName = "EA.EmpfaengerName";
        public const string const_EA_EmpfaengerStr = "EA.EmpfaengerStr";
        public const string const_EA_EmpfaengerPLZ = "EA.EmpfaengerPLZ";
        public const string const_EA_EmpfaengerOrt = "EA.EmpfaengerOrt";
        public const string const_EA_KFZ = EA_KFZ.const_EA_KFZ;
        public static List<string> ListValue_EA
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_EA_ArtPosEoA
                    ,const_EA_Anzahl
                    ,const_EA_AusgangId
                    ,const_EA_Auftraggeber
                    ,const_EA_AuftraggeberName
                    ,const_EA_AuftraggeberStr
                    ,const_EA_AuftraggeberPLZ
                    ,const_EA_AuftraggeberOrt
                    ,const_EA_ArtikelCount
                    ,const_EA_Brutto
                    ,const_EA_Datum
                    ,const_EA_EANr
                    ,const_EA_EingangId
                    ,const_EA_Empfaenger
                    ,const_EA_EmpfaengerName
                    ,const_EA_EmpfaengerStr
                    ,const_EA_EmpfaengerPLZ
                    ,const_EA_EmpfaengerOrt
                    ,const_EA_ExTransportRef
                    ,const_EA_ExAuftragRef
                    ,const_EA_ID
                    ,const_EA_KFZ
                    ,const_EA_LfsNr
                    ,const_EA_MATDate
                    ,const_EA_MATTime
                    ,const_EA_Netto
                    ,const_EA_SLB
                    ,const_EA_Termin
                    ,const_EAFunc_EANrWithPrefix
                    ,const_EAFunc_EAAnzahlX1000

                };
                tmp.Sort();
                return tmp;
            }
        }


        /**************************************************************************
         *           Artikeldaten
         * ***********************************************************************/
        public const string const_Artikel_SELF = "#Artikel.SELF#";

        public const string const_Artikel_ID = clsArtikel.ArtikelField_ID; // "Artikel.ID";
        public const string const_Artikel_LVSNr = clsArtikel.ArtikelField_LVSID; // "Artikel.LVS_ID";
        public const string const_Artikel_Netto = clsArtikel.ArtikelField_Netto; //"Artikel.Netto";
        public const string const_Artikel_Brutto = clsArtikel.ArtikelField_Brutto; //"Artikel.Brutto";
        public const string const_Artikel_Tara = clsArtikel.ArtikelField_Tara; //"Artikel.Tara";
        public const string const_Artikel_Gut = clsArtikel.ArtikelField_Gut; // "Artikel.Gut";
        public const string const_Artikel_Dicke = clsArtikel.ArtikelField_Dicke; // "Artikel.Dicke";
        public const string const_Artikel_Breite = clsArtikel.ArtikelField_Breite;// "Artikel.Breite";
        public const string const_Artikel_Laenge = clsArtikel.ArtikelField_Länge; // "Artikel.Laenge";
        public const string const_Artikel_Anzahl = clsArtikel.ArtikelField_Anzahl; // "Artikel.Anzahl";
        public const string const_Artikel_Einheit = clsArtikel.ArtikelField_Einheit; // "Artikel.Einheit";
        public const string const_Artikel_Werksnummer = clsArtikel.ArtikelField_Werksnummer; // "Artikel.Werksnummer";
        public const string const_Artikel_Produktionsnummer = clsArtikel.ArtikelField_Produktionsnummer; // "Artikel.Produktionsnummer";
        public const string const_Artikel_Charge = clsArtikel.ArtikelField_Charge; // "Artikel.Charge";
        public const string const_Artikel_BestellNr = clsArtikel.ArtikelField_Bestellnummer; // "Artikel.BestellNr";
        public const string const_Artikel_exMaterialNr = clsArtikel.ArtikelField_exMaterialnummer; // "Artikel.ExMaterialnummer";
        public const string const_Artikel_exAuftrag = clsArtikel.ArtikelField_exAuftrag;
        public const string const_Artikel_exAuftragPos = clsArtikel.ArtikelField_exAuftragPos;
        public const string const_Artikel_exBezeichnung = clsArtikel.ArtikelField_exBezeichnung;
        public const string const_Artikel_Pos = clsArtikel.ArtikelField_Position; // "Artikel.Pos";
        public const string const_Artikel_AbrufRef = clsArtikel.ArtikelField_AbrufRef; // "Artikel.AbrufRef";
        public const string const_Artikel_TARef = clsArtikel.ArtikelField_TARef; // "Artikel.TARef";
        public const string const_Artikel_ArtIDRef = clsArtikel.ArtikelField_ArtikelIDRef; // "Artikel.ArtIDRef";
        public const string const_Artikel_Glühdatum = clsArtikel.ArtikelField_GlowDate; // "Artikel.ArtIDRef";
        public const string const_Artikel_Güte = clsArtikel.ArtikelField_Güte;


        public const string const_Artikel_LVSNrBeforeUB = clsArtikel.ArtikelField_LVSNrBeforeUB; // "LVSNrVorUB";


        public static List<string> ListValue_Artikel
        {
            get
            {
                List<string> tmp = new List<string>()
                {
                    const_Artikel_ID
                    ,const_Artikel_LVSNr
                    ,const_Artikel_LVSNrBeforeUB
                    ,const_Artikel_Netto
                    ,const_Artikel_Brutto
                    ,const_Artikel_Tara
                    ,const_Artikel_Gut
                    ,const_Artikel_Dicke
                    ,const_Artikel_Breite
                    ,const_Artikel_Laenge
                    ,const_Artikel_Anzahl
                    ,const_Artikel_Einheit
                    ,const_Artikel_Werksnummer
                    ,const_Artikel_Produktionsnummer
                    ,const_Artikel_Charge
                    ,const_Artikel_BestellNr
                    ,const_Artikel_exAuftrag
                    ,const_Artikel_exAuftragPos
                    ,const_Artikel_exBezeichnung
                    ,const_Artikel_exMaterialNr
                    ,const_Artikel_Pos
                    ,const_Artikel_AbrufRef
                    ,const_Artikel_TARef
                    ,const_Artikel_ArtIDRef
                    ,const_Artikel_Glühdatum
                    ,const_Artikel_Güte

                };
                tmp.Sort();
                return tmp;
            }
        }
        /*************************************************************************
 *                      allgemeine Function
 * **********************************************************************/


        public static DataTable GetInputSelections()
        {
            DataTable dt = new DataTable("InputSelections");
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Art", typeof(string));

            DataRow row;
            //-- Artikel
            foreach (string itm in clsEdiVDAValueAlias.ListValue_Artikel)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "Artikel";
                dt.Rows.Add(row);
            }
            //-- EA
            foreach (string itm in clsEdiVDAValueAlias.ListValue_EA)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "EA";
                dt.Rows.Add(row);
            }
            //-- Eingang
            foreach (string itm in clsEdiVDAValueAlias.ListValue_Eingang)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "Eingang";
                dt.Rows.Add(row);
            }
            //-- Ausgang
            foreach (string itm in clsEdiVDAValueAlias.ListValue_Ausgang)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "Ausgang";
                dt.Rows.Add(row);
            }
            //-- Functions
            foreach (string itm in clsEdiVDAValueAlias.ListValue_Functions)
            {
                row = dt.NewRow();
                row["Value"] = itm;
                row["Art"] = "Function";
                dt.Rows.Add(row);
            }
            return dt;
        }



    }
}
