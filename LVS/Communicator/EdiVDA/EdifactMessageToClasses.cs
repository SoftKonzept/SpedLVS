using Common.Models;
using LVS.ASN.EDIFACT;
using LVS.ASN.EDIFACT.Defaults;
using LVS.ASN.GlobalValues;
using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class EdifactMessageToClasses
    {
        public DateTime CreatedMessageDate { get; set; } = new DateTime(1900, 1, 1);

        public Asn asn { get; set; }
        public List<string> listEdiSegments_Head = new List<string>();
        public List<string> listEdiSegments_Article = new List<string>();
        public EdiClientWorkspaceValueViewData ediClintWorkspaceValueVD = new EdiClientWorkspaceValueViewData();
        public EdiClientWorkspaceValue ediClientWorkspaceValue = new EdiClientWorkspaceValue();
        public Dictionary<Eingaenge, List<string>> dictEingang = new Dictionary<Eingaenge, List<string>>();
        public Dictionary<Articles, List<string>> dictArticle = new Dictionary<Articles, List<string>>();
        internal Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssignment = new Dictionary<string, clsASNArtFieldAssignment>();
        internal Dictionary<string, clsASNArtFieldAssignment> DictASNArtFieldAssCopyFieldValue = new Dictionary<string, clsASNArtFieldAssignment>();
        internal clsASNArtFieldAssignment asign = new clsASNArtFieldAssignment();
        public AddressViewData adrVD = new AddressViewData();
        public int BenutzerId { get; set; } = 0;
        public EingangViewData eingangViewData { get; set; }
        public Eingaenge eingang { get; set; }
        public Articles article { get; set; }
        public List<Articles> ListArticles { get; set; }

        internal UnitViewData unitViewData { get; set; }
        internal Dictionary<string, AddressReferences> DictAddressReferencesSender { get; set; }
        internal Dictionary<string, AddressReferences> DictAddressReferencesReceiver { get; set; }
        public string ErrorLog { get; set; }

        public EdifactMessageToClasses(Asn myAsn, int myUserId)
        {
            this.ErrorLog = string.Empty;
            BenutzerId = myUserId;
            asn = myAsn;
            if (
                (asn is Asn) &&
                (asn.Id > 0) &&
                (asn.EdiMessageValue.Length > 0)
            )
            {
                unitViewData = new UnitViewData();

                listEdiSegments_Head = new List<string>();
                listEdiSegments_Article = new List<string>();
                dictArticle = new Dictionary<Articles, List<string>>();

                ediClintWorkspaceValueVD = new EdiClientWorkspaceValueViewData();
                adrVD = new AddressViewData();

                asign = new clsASNArtFieldAssignment();

                // -- aufteilen der Segemente Article und Eingang
                GetHeadAndArtikelEdiMessageValueLists();

                eingang = new Eingaenge();
                eingang.ArbeitsbereichId = asn.WorkspaceId;
                eingang.MandantenId = asn.MandantenId;
                eingang.ASN = asn.Id;
                eingang.Eingangsdatum = DateTime.Now;

                if (SetAssignmentToEingang(listEdiSegments_Head))
                {
                    //--- check auf Global Lieferscheinnummer
                    var globalLfs = GlobalFieldVal_FilterByGlobalFieldVal.GetMatchingAssignments(DictASNArtFieldAssignment, GlobalFieldVal_DeliveryNote.const_GlobalVar_DeliveryNote);
                    if ((globalLfs != null) && (globalLfs.Count > 0))
                    {
                        string LfsEdiValueStr = asn.ListEdiMessageValue.FirstOrDefault(value => value.StartsWith(globalLfs[0].ASNField));
                        eingang.LfsNr = GetEdiValue(LfsEdiValueStr, string.Empty);
                    }

                    eingangViewData = new EingangViewData(eingang, BenutzerId);
                    string str = eingangViewData.Eingang.Lieferant;
                    eingangViewData.ListArticleInEingang = new List<Articles>();

                    if (dictArticle.Count > 0)
                    {
                        foreach (var item in dictArticle)
                        {
                            var list = item.Value as List<string>;
                            var art = item.Key as Articles;
                            article = new Articles();
                            article = art.Copy();
                            SetAssignmentToArticle(list);

                            //--- CombiValue setzen
                            clsASNTableCombiValue ASNTableCombiVal = new clsASNTableCombiValue();
                            Dictionary<string, clsASNTableCombiValue> DictASNTableCombiValue = ASNTableCombiVal.GetArtikelFieldAssignment(eingangViewData.Eingang.Auftraggeber, eingangViewData.Eingang.Empfaenger);
                            if (DictASNTableCombiValue.Count > 0)
                            {
                                LVS.ViewData.ArticleViewData artVD = new ArticleViewData(article, BenutzerId);
                                foreach (KeyValuePair<string, clsASNTableCombiValue> pair in DictASNTableCombiValue)
                                {
                                    artVD.CombinateValue(pair.Key.ToString(), (clsASNTableCombiValue)pair.Value);
                                    article = artVD.Artikel.Copy();
                                }
                            }

                            //---- Copy to Field



                            eingangViewData.ListArticleInEingang.Add(article);
                        }
                    }
                }
                //else
                //{
                //    //clsMail EMail = new clsMail();
                //    //EMail.InitClass(this._GL_User, this.sys);
                //    //EMail.Subject = this.sys.Client.MatchCode + DateTime.Now.ToShortDateString() + "- Error clsDocScan: Error beim Insert";

                //    //EMail.Message = this.;
                //    //EMail.SendError();
                //}
            }
        }

        private void GetHeadAndArtikelEdiMessageValueLists()
        {
            //bool IsHeadItem = true;
            //int iCountArt = 0;          
            bool bSwitchToTmpList = false;

            //----------------------------------------------- Unterteilung in die CPS Segmente
            bool IsHeadItem = true;
            int iCountArt = 0;
            Dictionary<int, List<string>> DictSegmentCPS = new Dictionary<int, List<string>>();
            List<string> tmpEdiSegmentArt = new List<string>();
            foreach (string str in asn.ListEdiMessageValue)
            {
                if (str.Substring(0, 3) == CPS.Name)
                {
                    IsHeadItem = false;
                }
                if (!IsHeadItem)
                {
                    if (str.Substring(0, 3) == CPS.Name)
                    {
                        IsHeadItem = false;
                        if (iCountArt > 0)
                        {
                            DictSegmentCPS.Add(iCountArt, new List<string>(tmpEdiSegmentArt.ToList()));
                            iCountArt = iCountArt * 10;
                        }
                        iCountArt++;
                        tmpEdiSegmentArt.Clear();
                        tmpEdiSegmentArt.Add(str);
                    }
                    else if (str.Substring(0, 3) == UNT.Name)
                    {
                        DictSegmentCPS.Add(iCountArt, new List<string>(tmpEdiSegmentArt.ToList()));
                    }
                    else
                    {
                        tmpEdiSegmentArt.Add(str);
                    }
                }
                else
                {
                    listEdiSegments_Head.Add(str);
                }
            }

            //---------------------------------------------------------- Bearbeitung der CPS Dictionary
            tmpEdiSegmentArt = new List<string>();
            dictArticle = new Dictionary<Articles, List<string>>();
            iCountArt = 0;
            foreach (KeyValuePair<int, List<string>> item in DictSegmentCPS)
            {
                if (item.Value.Where(x => x.StartsWith(PAC.Name)).ToList().Count > 1)
                {
                    //--- Ermitteln der zusätzlichen Artikeldaten ab LIN Segment
                    bSwitchToTmpList = false;
                    tmpEdiSegmentArt = new List<string>();
                    foreach (string str in item.Value)
                    {
                        if (str.Substring(0, 3) == LIN.Name)
                        {
                            bSwitchToTmpList = true;
                        }
                        if (bSwitchToTmpList)
                        {
                            tmpEdiSegmentArt.Add(str);
                        }
                    }
                    //--- zusätzlichen Aritkelzusatzdaten den einzelnen Aritkeln zuweisen
                    IsHeadItem = false;
                    listEdiSegments_Article = new List<string>();
                    int CountLoop = 0;
                    foreach (string str in item.Value)
                    {
                        if (str.Substring(0, 3) == PAC.Name)
                        {
                            if (CountLoop > 0)
                            {
                                iCountArt++;
                                Articles a = new Articles();
                                a.Position = iCountArt.ToString();
                                if (listEdiSegments_Article.Where(x => x.StartsWith(LIN.Name)).ToList().Count == 0)
                                {
                                    listEdiSegments_Article.AddRange(new List<string>(tmpEdiSegmentArt));
                                }
                                dictArticle.Add(a, listEdiSegments_Article);
                                listEdiSegments_Article = new List<string>();
                            }
                            CountLoop++;
                            listEdiSegments_Article.Add(str);
                        }
                        else
                        {
                            listEdiSegments_Article.Add(str);
                        }
                    }
                    //----- den letzten Durchlauf hinzufügen
                    if (listEdiSegments_Article.Count > 0)
                    {
                        iCountArt++;
                        Articles a = new Articles();
                        a.Position = iCountArt.ToString();
                        dictArticle.Add(a, new List<string>(listEdiSegments_Article));
                    }
                }
                else
                {
                    iCountArt++;
                    Articles a = new Articles();
                    a.Position = iCountArt.ToString();
                    dictArticle.Add(a, new List<string>(item.Value.ToList()));
                }
            }
        }


        private AddressReferences GetAdrReferenceFromEdiValue(Dictionary<string, AddressReferences> myDict, List<string> myListEdiMessageValue)
        {
            AddressReferences adrRef = new AddressReferences();

            //--- Verweis Sender/Empfänger
            if (myDict.Count > 0)
            {
                string strEdiValueVerweis = string.Empty;
                foreach (var item in myDict)
                {
                    string strEdiRef1 = string.Empty;
                    string strEdiValueRef1 = string.Empty;
                    if ((item.Value.ReferencePart1 != null) && (!item.Value.ReferencePart1.Equals(string.Empty)))
                    {
                        strEdiRef1 = myListEdiMessageValue.FirstOrDefault(x => x.ToString().StartsWith(item.Value.ReferencePart1));
                        strEdiValueRef1 = GetEdiValue(strEdiRef1, string.Empty);
                    }

                    string strEdiRef2 = string.Empty;
                    string strEdiValueRef2 = string.Empty;
                    if ((item.Value.ReferencePart2 != null) && (!item.Value.ReferencePart2.Equals(string.Empty)))
                    {
                        strEdiRef2 = myListEdiMessageValue.FirstOrDefault(x => x.ToString().StartsWith(item.Value.ReferencePart2));
                        strEdiValueRef2 = GetEdiValue(strEdiRef2, string.Empty);
                    }

                    string strEdiRef3 = string.Empty;
                    string strEdiValueRef3 = string.Empty;
                    if ((item.Value.ReferencePart3 != null) && (!item.Value.ReferencePart3.Equals(string.Empty)))
                    {
                        strEdiRef3 = myListEdiMessageValue.FirstOrDefault(x => x.ToString().StartsWith(item.Value.ReferencePart3));
                        strEdiValueRef3 = GetEdiValue(strEdiRef3, string.Empty);
                    }

                    strEdiValueVerweis = strEdiValueRef1 + "#" + strEdiValueRef2 + "#" + strEdiValueRef3;

                    if (myDict.ContainsKey(strEdiValueVerweis))
                    {
                        if (myDict.TryGetValue(strEdiValueVerweis, out adrRef))
                        {
                            if ((adrRef is AddressReferences) && (adrRef.Id > 0))
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return adrRef;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myListEdiMessageValue"></param>
        /// <returns></returns>
        private bool SetAssignmentToEingang(List<string> myListEdiMessageValue)
        {
            bool bReturn = false;

            string strLieferantenNr = string.Empty;
            string strEdiValueReceiver = string.Empty;
            string strReceiverNr = string.Empty;

            string strVerweisTeil1 = string.Empty;
            string strVerweisTeil2 = string.Empty;
            string strVerweisTeil3 = string.Empty;

            if (asn.ASNFileTyp.Equals(constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A))
            {
                AddressReferences adrRefSender = new AddressReferences();
                DictAddressReferencesSender = AddressReferenceViewData.FillDictAdrVerweisSender(asn.MandantenId, asn.WorkspaceId, BenutzerId, asn.ASNFileTyp);
                //--- Verweis Sender
                if (DictAddressReferencesSender.Count > 0)
                {
                    // --- direkte Zuweisung Sender
                    adrRefSender = GetAdrReferenceFromEdiValue(DictAddressReferencesSender, myListEdiMessageValue);
                    if ((adrRefSender is AddressReferences) && (adrRefSender.Id > 0))
                    {
                        eingang.Lieferant = adrRefSender.SupplierNo;
                        eingang.Auftraggeber = adrRefSender.SenderAdrId;
                        eingang.AuftraggeberString = adrRefSender.SenderAddress.ViewIdString;
                        eingang.Versender = adrRefSender.SenderAdrId;
                        eingang.VersenderString = adrRefSender.SenderAddress.ViewIdString; ;

                        //--- direkte Zuweisung Empfänger
                        DictAddressReferencesReceiver = AddressReferenceViewData.FillDictAdrVerweisReceiver(asn.MandantenId, asn.WorkspaceId, BenutzerId, asn.ASNFileTyp);
                        AddressReferences adrRefReceiver = new AddressReferences();
                        adrRefReceiver = GetAdrReferenceFromEdiValue(DictAddressReferencesReceiver, myListEdiMessageValue);
                        if ((adrRefReceiver is AddressReferences) && (adrRefReceiver.Id > 0))
                        {
                            eingang.Empfaenger = adrRefReceiver.VerweisAdrId;
                            eingang.EmpfaengerString = adrRefReceiver.ReceiverAddress.ViewIdString;
                        }
                    }
                }
            }
            else
            {
                string strEdiValueSender = string.Empty;
                strEdiValueSender = myListEdiMessageValue.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CZ));
                strLieferantenNr = GetEdiValue(strEdiValueSender, string.Empty);
                strEdiValueReceiver = myListEdiMessageValue.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CN));
                strReceiverNr = GetEdiValue(strEdiValueReceiver, string.Empty);
                //strLieferantenNr = strLieferantenNr.TrimStart('0');

                //-- Adressen
                strVerweisTeil1 = myListEdiMessageValue.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CZ));
                NAD nadTeil1 = new NAD(strVerweisTeil1, asn.ASNFileTyp);
                strVerweisTeil2 = myListEdiMessageValue.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CN));
                NAD nadTeil2 = new NAD(strVerweisTeil2, asn.ASNFileTyp);
                strVerweisTeil3 = myListEdiMessageValue.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_FW));
                NAD nadTeil3 = new NAD(strVerweisTeil3, asn.ASNFileTyp);

                Dictionary<string, clsADRVerweis> dicVerweis = clsADRVerweis.FillDictAdrVerweis(0, 0, 1, asn.ASNFileTyp);
                //--Verweis Sender
                clsADRVerweis VersenderVerweis = new clsADRVerweis();
                string strVerweisGlobalSender = nadTeil1.C082.f_3039_PartyId + "#0#0";
                strEdiValueSender = nadTeil1.C082.f_3039_PartyId + "#" + nadTeil2.C082.f_3039_PartyId + "#" + nadTeil3.C082.f_3039_PartyId;

                //--- direkte Zuweisung Sender
                if (dicVerweis.ContainsKey(strEdiValueSender))
                {
                    //-- direkte zuweisung liegt vor
                    dicVerweis.TryGetValue(strEdiValueSender, out VersenderVerweis);
                }
                else
                {
                    dicVerweis.TryGetValue(strVerweisGlobalSender, out VersenderVerweis);
                }
                if ((VersenderVerweis is clsADRVerweis) && (VersenderVerweis.ID > 0))
                {
                    adrVD = new AddressViewData((int)VersenderVerweis.SenderAdrID, BenutzerId);
                    if ((adrVD.Address is Addresses) && (adrVD.Address.Id > 0))
                    {
                        eingang.Lieferant = VersenderVerweis.LieferantenVerweis;
                        eingang.Auftraggeber = adrVD.Address.Id;
                        eingang.AuftraggeberString = adrVD.Address.ViewId;
                        eingang.Versender = adrVD.Address.Id;
                        eingang.VersenderString = adrVD.Address.ViewId;
                    }
                }

                //-- Verweis Receiver
                clsADRVerweis ReceiverVerweis = new clsADRVerweis();
                strEdiValueReceiver = nadTeil2.C082.f_3039_PartyId + "#" + nadTeil1.C082.f_3039_PartyId + "#" + nadTeil3.C082.f_3039_PartyId;
                if (dicVerweis.ContainsKey(strEdiValueReceiver))
                {
                    //-- direkte zuweisung liegt vor
                    dicVerweis.TryGetValue(strEdiValueReceiver, out ReceiverVerweis);
                }
                else
                {
                    ReceiverVerweis = VersenderVerweis;
                }
                if ((ReceiverVerweis is clsADRVerweis) && (ReceiverVerweis.ID > 0))
                {
                    adrVD = new AddressViewData((int)ReceiverVerweis.VerweisAdrID, BenutzerId);
                    if ((adrVD.Address is Addresses) && (adrVD.Address.Id > 0))
                    {
                        eingang.Empfaenger = adrVD.Address.Id;
                        eingang.EmpfaengerString = adrVD.Address.ViewId;
                    }
                }
            }

            asign = new clsASNArtFieldAssignment();
            DictASNArtFieldAssignment = asign.GetArtikelFieldAssignment(eingang.Auftraggeber, eingang.Empfaenger, eingang.ArbeitsbereichId);
            DictASNArtFieldAssCopyFieldValue = asign.GetArtikelFieldAssignmentCopyFields(eingang.Auftraggeber, eingang.Empfaenger, eingang.ArbeitsbereichId);

            if (
                    (eingang.Auftraggeber > 0) 
                    && (eingang.Empfaenger > 0) 
                    //&& (DictASNArtFieldAssCopyFieldValue.Count > 0)
               )
            {
                bReturn = true;
                foreach (var pair in DictASNArtFieldAssCopyFieldValue)
                {
                    string ediValue = myListEdiMessageValue.FirstOrDefault(x => x.StartsWith(pair.Key.ToString()));
                    if ((ediValue != null) && (ediValue.Length > 0))
                    {
                        string value = GetEdiValue(ediValue, string.Empty);
                        switch (pair.Value.ArtField)
                        {
                            case clsEdiVDAValueAlias.const_EA_Datum:
                                //DateTime dt = new DateTime(1900, 1, 1);
                                //DateTime.TryParse(value, out dt);
                                //eingang.Eingangsdatum = dt;
                                break;
                            case clsEdiVDAValueAlias.const_EA_KFZ:
                                eingang.WaggonNr = string.Empty;
                                eingang.KFZ = string.Empty;
                                eingang.IsShip = false;
                                eingang.IsWaggon = false;
                                eingang.IsShip = false;
                                if (value.Length > 0)
                                {
                                    if (value.Length > 10)
                                    {
                                        eingang.IsWaggon = true;
                                        eingang.WaggonNr = value;
                                    }
                                    else
                                    {
                                        eingang.IsWaggon = false;
                                        eingang.KFZ = value;
                                    }
                                }
                                break;
                            case clsEdiVDAValueAlias.const_EA_LfsNr:
                                eingang.LfsNr = value;
                                break;

                            case clsEdiVDAValueAlias.const_EA_ExTransportRef:
                                eingang.ExTransportRef = value;
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
            else
            {
                bReturn = false;
                string strError = "ACHTUNG !" + Environment.NewLine;
                strError += "Keine Verarbeitung möglich!" + eingang.Auftraggeber + Environment.NewLine;
                strError += "ASN: " + eingang.ASN + Environment.NewLine;
                strError += "  -> Auftraggeber ID = " + eingang.Auftraggeber + Environment.NewLine;
                strError += "     |-> LieferantenNr = " + strLieferantenNr + Environment.NewLine;
                strError += "  -> Empfänger ID = " + eingang.Auftraggeber + Environment.NewLine;
                strError += "     |-> EmpfängerNr = " + strReceiverNr + Environment.NewLine;
                strError += Environment.NewLine;
                strError += "ASN Message (Kopfteil):" + Environment.NewLine;
                foreach (string s in myListEdiMessageValue)
                {
                    strError += s + Environment.NewLine;
                }

                this.ErrorLog = strError;
            }
            return bReturn;
        }

        private bool SetAssignmentToArticle(List<string> myListEdiMessageValue)
        {
            bool bReturn = false;
            decimal decTmp = 0;

            if (
                    (article is Articles) &&
                    (article.Position.Length > 0) &&
                    (DictASNArtFieldAssCopyFieldValue.Count > 0)
               )
            {
                bReturn = true;
                foreach (var pair in DictASNArtFieldAssCopyFieldValue)
                {
                    string ediValue = string.Empty;
                    if (pair.Value.ASNField.Equals(clsEdiVDAValueAlias.const_Artikel_SELF))
                    {
                        ediValue = pair.Value.ASNField;
                    }
                    else
                    {
                        ediValue = myListEdiMessageValue.FirstOrDefault(x => x.StartsWith(pair.Key.ToString()));
                    }

                    if ((ediValue != null) && (ediValue.Length > 0))
                    {
                        string value = string.Empty;
                        if (pair.Value.IsDefValue)
                        {
                            value = pair.Value.DefValue;
                        }
                        else
                        {
                            //--ermittelt die Value aus dem Edifact Segment
                            value = GetEdiValue(ediValue, pair.Value.SubASNField);
                        }

                        if (
                                ((value.Length > 0) && (pair.Value.FormatFunction.Length > 0)) ||
                                (pair.Value.ASNField.Equals(clsEdiVDAValueAlias.const_Artikel_SELF))
                           )
                        {
                            clsASNFormatFunctions asnFormat = new clsASNFormatFunctions();
                            string strValue = string.Empty;
                            if (pair.Value.ASNField.Equals(clsEdiVDAValueAlias.const_Artikel_SELF))
                            {
                                ArticleViewData artVD = new ArticleViewData(article, BenutzerId);
                                strValue = artVD.GetValueByProperty(pair.Value.ArtField);
                                value = asnFormat.CustomFormatValue(pair.Value.FormatFunction, strValue, article);
                            }
                            else
                            {
                                value = asnFormat.CustomFormatValue(pair.Value.FormatFunction, value, article);
                            }
                            article = asnFormat.article.Copy();
                        }

                        if (!pair.Value.ASNField.Equals(clsEdiVDAValueAlias.const_Artikel_SELF))
                        {
                            switch (pair.Value.ArtField)
                            {
                                case clsEdiVDAValueAlias.const_Artikel_Anzahl:
                                    int iTmp = 1;
                                    int.TryParse(value, out iTmp);
                                    switch (iTmp)
                                    {
                                        case 1:
                                            article.Anzahl = 1;
                                            break;
                                        default:
                                            article.Anzahl = iTmp;
                                            break;
                                    }
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_BestellNr:
                                    article.Bestellnummer = value.TrimStart('0');
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_Brutto:
                                    decTmp = 0;
                                    decimal.TryParse(value, out decTmp);
                                    article.Brutto = decTmp;
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_Breite:
                                    decTmp = 0;
                                    decimal.TryParse(value, out decTmp);
                                    article.Breite = decTmp;
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_Charge:
                                    article.Charge = value.TrimStart('0');
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_Dicke:
                                    decTmp = 0;
                                    decimal.TryParse(value, out decTmp);
                                    article.Dicke = decTmp;
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_Einheit:
                                    Units unit = new Units();
                                    switch (value.ToUpper())
                                    {
                                        case QTY_C186_6411_DefaultsValue.const_MeasureUnitQualifier_KGM:
                                            unit = unitViewData.ListUnits.FirstOrDefault(x => x.Bezeichnung.ToUpper().Equals("KG")
                                                                                               || x.Bezeichnung.Equals("KGM")
                                                                                               );
                                            if ((unit is Units) && (unit.Id > 0))
                                            {
                                                article.Einheit = unit.Bezeichnung;
                                            }
                                            else
                                            {
                                                article.Einheit = "KG";
                                            }
                                            break;
                                        case QTY_C186_6411_DefaultsValue.const_MeasureUnitQualifier_PCI:
                                            unit = unitViewData.ListUnits.FirstOrDefault(x => x.Bezeichnung.ToUpper().Equals("STÜCK")
                                                                                               || x.Bezeichnung.Equals("STUECK")
                                                                                               || x.Bezeichnung.Equals("STK")
                                                                                               || x.Bezeichnung.Equals("STK.")
                                                                                               );
                                            if ((unit is Units) && (unit.Id > 0))
                                            {
                                                article.Einheit = unit.Bezeichnung;
                                            }
                                            else
                                            {
                                                article.Einheit = "STK.";
                                            }
                                            break;
                                        default:
                                            //article.Einheit = "KG";
                                            break;
                                    }
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_exAuftrag:
                                    article.exAuftrag = value.TrimStart('0');
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_exAuftragPos:
                                    article.exAuftragPos = value.TrimStart('0');
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_exBezeichnung:
                                    article.exBezeichnung = value;
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_exMaterialNr:
                                    article.exMaterialnummer = value.TrimStart('0');
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_Glühdatum:
                                    DateTime dt = new DateTime(1900, 1, 1);
                                    DateTime.TryParse(value, out dt);
                                    article.GlowDate = dt;
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_Laenge:
                                    decTmp = 0;
                                    decimal.TryParse(value, out decTmp);
                                    article.Laenge = decTmp;
                                    break;
                                case clsEdiVDAValueAlias.const_Artikel_Netto:
                                    decTmp = 0;
                                    decimal.TryParse(value, out decTmp);
                                    article.Netto = decTmp;
                                    break;

                                case clsEdiVDAValueAlias.const_Artikel_Produktionsnummer:
                                    article.Produktionsnummer = value; // value.TrimStart('0'); ;
                                    break;

                                case clsEdiVDAValueAlias.const_Artikel_Werksnummer:
                                    article.Werksnummer = value; // value.TrimStart('0');
                                    if (eingang is Eingaenge)
                                    {
                                        int iGArtId = GoodstypeViewData.GetGutByADRAndVerweis(BenutzerId, eingang.Auftraggeber, article.Werksnummer, eingang.ArbeitsbereichId);
                                        GoodstypeViewData gVD = new GoodstypeViewData(iGArtId, 1, false);
                                        if ((gVD.Gut is Goodstypes) && (iGArtId > 0) && (gVD.Gut.Id == iGArtId))
                                        {
                                            article = gVD.SetGoodtypeValueToArticle(article).Copy();
                                            //LVS.clsSystem Sys = new LVS.clsSystem();
                                            //Sys.Client.clsLagerdaten_Customized_ASNArtikel_Bestellnummer(ref myArt, myArt.GArt.BestellNr);
                                        }
                                    }
                                    break;

                                case clsEdiVDAValueAlias.const_Artikel_SELF:

                                    if ((value.Length > 0) && (pair.Value.FormatFunction.Length > 0))
                                    {
                                        clsASNFormatFunctions asnFormat = new clsASNFormatFunctions();
                                        value = asnFormat.CustomFormatValue(pair.Value.FormatFunction, value, article);
                                        article = asnFormat.article.Copy();
                                    }

                                    break;

                                default:
                                    break;
                            }
                        }

                        if ((!pair.Value.CopyToField.Equals(string.Empty)) && (pair.Value.CopyToField.Length > 0))
                        {
                            //--- AsnArtFieldAssignment Copy to Field
                            ArticleViewData articleViewData = new ArticleViewData(article, BenutzerId);
                            articleViewData.CopyPropertyValueToProperty(pair.Value.ArtField, pair.Value.CopyToField);
                            article = articleViewData.Artikel.Copy();
                        }
                    }
                }
            }
            return bReturn;
        }
        private string GetEdiValue(string str, string mySubAsnField)
        {
            string stringReturn = string.Empty;
            if ((str != null) && (str.Length > 0))
            {

                switch (str.Substring(0, 3))
                {
                    case ALI.Name:
                        break;

                    case BGM.Name:
                        BGM b = new BGM(str, asn.ASNFileTyp);
                        switch (b.C002.f_1001_MessageTyp)
                        {
                            case BGM.const_DocumentMessageNameCode_C002_1001_351_DespatchAdvice:
                                break;
                        }
                        break;

                    case DTM.Name:
                        DTM d = new DTM(str);
                        switch (d.C507.f_2005_DateQualifier)
                        {
                            case DTM.const_DateQualifier_C507_2005_4_OrdertDate:
                            case DTM.const_DateQualifier_C507_2005_11_ShipmentDate:
                                switch (asn.ASNFileTyp)
                                {
                                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                                        stringReturn = d.C507.f_2380_Date.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case DTM.const_DateQualifier_C507_2005_94_ProductionDate:
                                switch (asn.ASNFileTyp)
                                {
                                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                                        stringReturn = d.C507.f_2380_Date.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case DTM.const_DateQualifier_C507_2005_132_ArrivalDate:
                                switch (asn.ASNFileTyp)
                                {
                                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                                        stringReturn = d.C507.f_2380_Date.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case DTM.const_DateQualifier_C507_2005_137_MessageDate:
                                switch (asn.ASNFileTyp)
                                {
                                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                    case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                                        stringReturn = d.C507.f_2380_Date.ToString();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;

                    case CPS.Name:
                        break;

                    case EQD.Name:
                        break;

                    case GIR.Name:
                        GIR g = new GIR(str, asn.ASNFileTyp);
                        switch (asn.ASNFileTyp)
                        {
                            case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                            case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                                stringReturn = g.C206.f_7402_CoilNumber;
                                break;
                            default:
                                break;
                        }
                        break;

                    case LIN.Name:
                        LIN l = new LIN(str, asn.ASNFileTyp);
                        if ((l.C212 is LIN_C212) && (l.C212.f_7140_ArticleReference.Length > 0))
                        {
                            switch (asn.ASNFileTyp)
                            {
                                case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                                    stringReturn = l.C212.f_7140_ArticleReference;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                    case LOC.Name:
                        LOC loc = new LOC(str, asn.ASNFileTyp);
                        if ((loc.C517 is LOC_C517) && (loc.C517.f_3225_LocationIdentifier.Length > 0))
                        {
                            switch (asn.ASNFileTyp)
                            {
                                case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                                    stringReturn = loc.C517.f_3225_LocationIdentifier;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                    case MEA.Name:
                        MEA m = new MEA(str, asn.ASNFileTyp);
                        switch (m.f_6311_MeasurementQualifier)
                        {
                            //--AAY > Gewicht 
                            case MEA.const_MeasurementQualifier_6311_AAY_PackageMeasurement:
                                switch (m.C502.f_6313_MeasurementCode)
                                {
                                    //--AAG > Brutto
                                    case MEA.const_MeasuredCoded_C502_6313_AAG_PackageGross:
                                        stringReturn = m.C174.f_6314_MeasurementValue.ToString();
                                        break;
                                    //--AAL > Netto
                                    case MEA.const_MeasuredCoded_C502_6313_AAL_PackageNet:
                                        stringReturn = m.C174.f_6314_MeasurementValue.ToString();
                                        break;
                                    //--PD > Breite
                                    case MEA.const_MeasuredCoded_C502_6313_WD_Width:
                                        stringReturn = m.C174.f_6314_MeasurementValue.ToString();
                                        //switch (m.C174.f_6411_MeasureUnitQualifier)
                                        //{
                                        //    case MEA.const_MeasuredUnitQualifier_C174_6411_MMT_Millimeter:
                                        //        int iTmp = 0;
                                        //        int.TryParse(stringReturn, out iTmp);
                                        //        if (iTmp > 0)
                                        //        {                                                   
                                        //            stringReturn = (iTmp/1000).ToString();
                                        //        }
                                        //        break;
                                        //}
                                        break;
                                    //--PD > Dicke
                                    case MEA.const_MeasuredCoded_C502_6313_TH_Thickness:
                                        stringReturn = m.C174.f_6314_MeasurementValue.ToString();
                                        //switch (m.C174.f_6411_MeasureUnitQualifier)
                                        //{
                                        //    case MEA.const_MeasuredUnitQualifier_C174_6411_MMT_Millimeter:
                                        //        int iTmp = 0;
                                        //        int.TryParse(stringReturn, out iTmp);
                                        //        if (iTmp > 0)
                                        //        {
                                        //            stringReturn = (iTmp / 1000).ToString();
                                        //        }
                                        //        break;
                                        //}
                                        break;
                                    //--PD > Laänge
                                    case MEA.const_MeasuredCoded_C502_6313_LN_Length:
                                        stringReturn = m.C174.f_6314_MeasurementValue.ToString();
                                        //switch (m.C174.f_6411_MeasureUnitQualifier)
                                        //{
                                        //    case MEA.const_MeasuredUnitQualifier_C174_6411_MMT_Millimeter:
                                        //        int iTmp = 0;
                                        //        int.TryParse(stringReturn, out iTmp);
                                        //        if (iTmp > 0)
                                        //        {
                                        //            stringReturn = (iTmp / 1000).ToString();
                                        //        }
                                        //        break;
                                        //}
                                        break;
                                }
                                break;
                            //-- Abmessungen
                            case MEA.const_MeasurementQualifier_6311_PD_Dimensions:
                                switch (m.C502.f_6313_MeasurementCode)
                                {
                                    //--PD > Breite
                                    case MEA.const_MeasuredCoded_C502_6313_WD_Width:
                                        stringReturn = m.C174.f_6314_MeasurementValue.ToString();
                                        break;
                                    //--PD > Dicke
                                    case MEA.const_MeasuredCoded_C502_6313_TH_Thickness:
                                        stringReturn = m.C174.f_6314_MeasurementValue.ToString();
                                        break;
                                    //--PD > Laänge
                                    case MEA.const_MeasuredCoded_C502_6313_LN_Length:
                                        stringReturn = m.C174.f_6314_MeasurementValue.ToString();
                                        break;
                                }
                                break;
                        }
                        break;

                    case NAD.Name:
                        NAD n = new NAD(str, asn.ASNFileTyp);
                        switch (n.f_3035_PartyQualifier)
                        {
                            //--- Spediteur / Transporteur
                            case NAD.const_PartyQualifier_3035_CA_Carrier:
                            //break;
                            // -- Empfänger
                            case NAD.const_PartyQualifier_3035_CN_Consignee:
                            case NAD.const_PartyQualifier_3035_SF_ShipFrom:
                            case NAD.const_PartyQualifier_3035_ST_ShipTo:
                                if ((n.C082.f_3039_PartyId != null) && (n.C082.f_3039_PartyId.Length > 0))
                                {
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                        case constValue_AsnArt.const_Art_EDIFACT_DESADV_D07A:
                                            stringReturn = n.C082.f_3039_PartyId;
                                            break;
                                        default:

                                            break;
                                    }
                                }
                                break;
                            // -- Auftraggeber
                            case NAD.const_PartyQualifier_3035_CZ_Consignor:
                                if ((n.C082.f_3039_PartyId != null) && (n.C082.f_3039_PartyId.Length > 0))
                                {
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                        case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A:
                                            stringReturn = n.C082.f_3039_PartyId;
                                            break;
                                        default:

                                            break;
                                    }
                                }
                                break;
                            //-- Anlieferaddresse
                            case NAD.const_PartyQualifier_3035_DP_Delivery:
                                break;
                            //-- Verkäufer
                            case NAD.const_PartyQualifier_3035_SE_Seller:
                                //-- + Lieferantennummer ermitteln
                                break;
                        }
                        break;

                    case PAC.Name:
                        PAC p = new PAC(str, asn.ASNFileTyp);
                        switch (asn.ASNFileTyp)
                        {
                            case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                            case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A:
                                stringReturn = p.f_7224_NumberOfPackage.ToString();
                                break;
                            default:
                                break;
                        }
                        break;

                    case PCI.Name:
                        PCI pci = new PCI(str, asn.ASNFileTyp);
                        stringReturn = pci.C210.f_7102_PackageNumber.ToString();
                        break;

                    case PIA.Name:
                        PIA pia = new PIA(str, asn.ASNFileTyp);
                        stringReturn = pia.C212.f_7140_SupplierArticleReference.ToString();
                        break;

                    case QTY.Name:
                        QTY q = new QTY(str, asn.ASNFileTyp);
                        switch (mySubAsnField)
                        {
                            case constValue_Edifact.const_Edifact_Sub_QTY_C186_f6060:
                                stringReturn = q.C186.f_6060_Quantity.ToString();
                                break;
                            case constValue_Edifact.const_Edifact_Sub_QTY_C186_f6063:
                                stringReturn = q.C186.f_6063_QuantityQualifier.ToString();
                                break;
                            case constValue_Edifact.const_Edifact_Sub_QTY_C186_f6411:
                                stringReturn = q.C186.f_6411_UnitQualifier;
                                break;
                            default:
                                switch (q.C186.f_6063_QuantityQualifier)
                                {
                                    case QTY.const_QuantityQualifier_C186_6063_12_DispatchQuantity:
                                    case QTY.const_QuantityQualifier_C186_6063_47_InvoicedQuantity:
                                    case QTY.const_QuantityQualifier_C186_6063_52_QuantityPerPack:
                                        stringReturn = q.C186.f_6411_UnitQualifier;
                                        break;
                                }
                                break;
                        }
                        break;

                    case RFF.Name:
                        RFF r = new RFF(str, asn.ASNFileTyp);
                        if (r.C506 is RFF_C506)
                        {
                            switch (r.C506.f_1153_RefQualifier)
                            {
                                //-- Lieferschein
                                case RFF.const_ReferenzQualifier_C506_1153_AAS_TransportDocumentNumber:
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                        case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A:
                                            stringReturn = r.C506.f_1154_RefNumber;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case RFF.const_ReferenzQualifier_C506_1153_AAT_MasterLableNumber:
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                        case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A:
                                            stringReturn = r.C506.f_1154_RefNumber;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case RFF.const_ReferenzQualifier_C506_1153_ADE_AcountNumber:
                                    break;
                                case RFF.const_ReferenzQualifier_C506_1153_ON_OrderNumber:
                                    stringReturn = r.C506.f_1154_RefNumber;
                                    break;
                                case RFF.const_ReferenzQualifier_C506_1153_VN_SalesOrderNumber:
                                    break;
                                default:
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                        case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A:
                                            stringReturn = r.C506.f_1154_RefNumber;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                            }
                        }
                        break;

                    case TDT.Name:
                        TDT t = new TDT(str, asn.ASNFileTyp);
                        switch (asn.ASNFileTyp)
                        {
                            case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                            case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A:
                                if (
                                    (t.C222 != null) &&
                                    (t.C222.f_8212_VehicleId != null) &&
                                    (t.C222.f_8212_VehicleId.Length > 0)
                                  )
                                {
                                    stringReturn = t.C222.f_8212_VehicleId;
                                }
                                else
                                {
                                    string s = string.Empty;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case TOD.Name:
                        break;


                    case UNB.Name:
                        UNB unb = new UNB(str);

                        if (unb.S002 is UNB_S002)
                        {
                            string sender = unb.S002.f_0004_SenderIdentification;
                            stringReturn = sender;
                        }
                        if (unb.S003 is UNB_S003)
                        {
                            string receiver = unb.S003.f_0010_ReceiverIdentification;
                            stringReturn = receiver;
                        }
                        if (unb.S004 is UNB_S004)
                        {
                            DateTime dtDate = unb.S004.f_0017_CreationDateTransmittion;
                            DateTime dtTime = unb.S004.f_0019_CreationTimeTransmittion;

                            dtDate = dtDate.AddHours(dtTime.Hour);
                            dtDate = dtDate.AddMinutes(dtTime.Minute);
                            CreatedMessageDate = dtDate;
                            //stringReturn = dtDate.ToString("dd.mm.yyyy HH:mm");
                        }
                        break;
                    case UNH.Name:
                        //UNH u = new UNH(str, asn.ASNFileTyp);
                        break;
                }
            }
            return stringReturn;
        }


        private void SetEdiValue(List<string> myListEdiMessageValue)
        {
            //ListArticles = new List<Articles>();
            //int iArticleCount = 0;
            foreach (string str in myListEdiMessageValue)
            {
                switch (str.Substring(0, 3))
                {
                    case ALI.Name:
                        //BGM b = new BGM(str, myAsn.ASNFileTyp);
                        break;

                    case BGM.Name:
                        BGM b = new BGM(str, asn.ASNFileTyp);
                        switch (b.C002.f_1001_MessageTyp)
                        {
                            case BGM.const_DocumentMessageNameCode_C002_1001_351_DespatchAdvice:
                                break;
                        }
                        break;

                    case DTM.Name:
                        DTM d = new DTM(str);
                        switch (d.C507.f_2005_DateQualifier)
                        {
                            case DTM.const_DateQualifier_C507_2005_4_OrdertDate:
                                break;
                            case DTM.const_DateQualifier_C507_2005_11_ShipmentDate:
                                break;
                            case DTM.const_DateQualifier_C507_2005_94_ProductionDate:
                                switch (asn.ASNFileTyp)
                                {
                                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                        if (article is Articles)
                                        {
                                            article.GlowDate = d.C507.f_2380_Date;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case DTM.const_DateQualifier_C507_2005_132_ArrivalDate:
                                switch (asn.ASNFileTyp)
                                {
                                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                        if (article is Articles)
                                        {
                                            eingang.Eingangsdatum = d.C507.f_2380_Date;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case DTM.const_DateQualifier_C507_2005_137_MessageDate:
                                switch (asn.ASNFileTyp)
                                {
                                    case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                        break;

                    case CPS.Name:
                        break;

                    case EQD.Name:
                        break;

                    case GIR.Name:
                        GIR g = new GIR(str, asn.ASNFileTyp);
                        switch (asn.ASNFileTyp)
                        {
                            case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                if (article is Articles)
                                {
                                    article.Produktionsnummer = g.C206.f_7402_CoilNumber;
                                }
                                break;
                            default:
                                break;
                        }
                        break;


                    case LIN.Name:
                        LIN l = new LIN(str, asn.ASNFileTyp);
                        if ((l.C212 is LIN_C212) && (l.C212.f_7140_ArticleReference.Length > 0))
                        {
                            switch (asn.ASNFileTyp)
                            {
                                case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                    if (article is Articles)
                                    {
                                        article.Werksnummer = l.C212.f_7140_ArticleReference;

                                        if (eingang is Eingaenge)
                                        {
                                            int iGArtId = GoodstypeViewData.GetGutByADRAndVerweis(1, eingang.Auftraggeber, article.Werksnummer, eingang.ArbeitsbereichId);
                                            GoodstypeViewData gVD = new GoodstypeViewData(iGArtId, 1, false);
                                            if ((gVD.Gut is Goodstypes) && (iGArtId > 0) && (gVD.Gut.Id == iGArtId))
                                            {
                                                article = gVD.SetGoodtypeValueToArticle(article).Copy();
                                                //this.Sys.Client.clsLagerdaten_Customized_ASNArtikel_Bestellnummer(ref myArt, myArt.GArt.BestellNr);
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;

                    case LOC.Name:
                        break;

                    case MEA.Name:
                        MEA m = new MEA(str, asn.ASNFileTyp);
                        if (article is Articles)
                        {
                            switch (m.f_6311_MeasurementQualifier)
                            {
                                //--AAY > Gewicht 
                                case MEA.const_MeasurementQualifier_6311_AAY_PackageMeasurement:
                                    switch (m.C502.f_6313_MeasurementCode)
                                    {
                                        //--AAG > Brutto
                                        case MEA.const_MeasuredCoded_C502_6313_AAG_PackageGross:
                                            if (article is Articles)
                                            {
                                                article.Brutto = m.C174.f_6314_MeasurementValue;
                                            }
                                            break;
                                        //--AAL > Netto
                                        case MEA.const_MeasuredCoded_C502_6313_AAL_PackageNet:
                                            if (article is Articles)
                                            {
                                                article.Netto = m.C174.f_6314_MeasurementValue;
                                            }
                                            break;
                                    }
                                    break;
                                //-- Abmessungen
                                case MEA.const_MeasurementQualifier_6311_PD_Dimensions:
                                    //switch (m.C502.f_6313_MeasurementCode)
                                    //{
                                    //    //--PD > Breite
                                    //    case MEA.const_MeasuredCoded_C502_6313_WD_Width:
                                    //        if (article is Articles)
                                    //        {
                                    //            article.Breite = m.C174.f_6314_MeasurementValue;
                                    //        }
                                    //        break;
                                    //    //--PD > Dicke
                                    //    case MEA.const_MeasuredCoded_C502_6313_TH_Thickness:
                                    //        if (article is Articles)
                                    //        {
                                    //            article.Dicke = m.C174.f_6314_MeasurementValue;
                                    //        }
                                    //        break;
                                    //    //--PD > Laänge
                                    //    case MEA.const_MeasuredCoded_C502_6313_LN_Length:
                                    //        if (article is Articles)
                                    //        {
                                    //            article.Laenge = m.C174.f_6314_MeasurementValue;
                                    //        }
                                    //        break;
                                    //}
                                    break;
                            }

                            if (article is Articles)
                            {
                                article.Einheit = m.C174.f_6411_MeasureUnitQualifier;
                            }
                        }
                        break;

                    case NAD.Name:
                        NAD n = new NAD(str, asn.ASNFileTyp);
                        switch (n.f_3035_PartyQualifier)
                        {
                            //--- Spediteur / Transporteur
                            case NAD.const_PartyQualifier_3035_CA_Carrier:
                                break;
                            // -- Empfänger
                            case NAD.const_PartyQualifier_3035_CN_Consignee:
                                if ((n.C082.f_3039_PartyId != null) && (n.C082.f_3039_PartyId.Length > 0))
                                {
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                            ediClientWorkspaceValue = new EdiClientWorkspaceValue();
                                            if (ediClintWorkspaceValueVD.ListEdiAdrWorkspaceAssignments.Count > 0)
                                            {
                                                ediClientWorkspaceValue = ediClintWorkspaceValueVD.ListEdiAdrWorkspaceAssignments.FirstOrDefault(x => x.WorkspaceId == (int)asn.WorkspaceId && x.Property == NAD.Name + "+" + NAD.const_PartyQualifier_3035_CN_Consignee);
                                            }
                                            if (
                                                    (ediClientWorkspaceValue.Id > 0) &&
                                                    (n.C082.f_3039_PartyId.Equals(ediClientWorkspaceValue.Value))
                                               )
                                            {
                                                if (eingang is Eingaenge)
                                                {
                                                    eingang.EmpfaengerString = ediClientWorkspaceValue.Address.ViewId;
                                                    eingang.Empfaenger = ediClientWorkspaceValue.AdrId;
                                                }
                                            }
                                            break;
                                        default:

                                            break;
                                    }
                                }
                                break;
                            // -- Auftraggeber
                            case NAD.const_PartyQualifier_3035_CZ_Consignor:
                                if ((n.C082.f_3039_PartyId != null) && (n.C082.f_3039_PartyId.Length > 0))
                                {
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                            ediClientWorkspaceValue = new EdiClientWorkspaceValue();
                                            if (ediClintWorkspaceValueVD.ListEdiAdrWorkspaceAssignments.Count > 0)
                                            {
                                                ediClientWorkspaceValue = ediClintWorkspaceValueVD.ListEdiAdrWorkspaceAssignments.FirstOrDefault(x => x.WorkspaceId == (int)asn.WorkspaceId && x.Property == NAD.Name + "+" + NAD.const_PartyQualifier_3035_CZ_Consignor);
                                            }
                                            if (
                                                    (ediClientWorkspaceValue.Id > 0) &&
                                                    (n.C082.f_3039_PartyId.Equals(ediClientWorkspaceValue.Value))
                                               )
                                            {
                                                if (eingang is Eingaenge)
                                                {
                                                    eingang.Lieferant = n.C082.f_3039_PartyId;
                                                    eingang.Auftraggeber = ediClientWorkspaceValue.AdrId;
                                                    eingang.AuftraggeberString = ediClientWorkspaceValue.Address.ViewId;

                                                    eingang.Versender = ediClientWorkspaceValue.AdrId;
                                                    eingang.VersenderString = ediClientWorkspaceValue.Address.ViewId;
                                                }
                                            }
                                            break;
                                        default:

                                            break;
                                    }
                                }
                                break;
                            //-- Anlieferaddresse
                            case NAD.const_PartyQualifier_3035_DP_Delivery:
                                break;
                            //-- Verkäufer
                            case NAD.const_PartyQualifier_3035_SE_Seller:
                                //-- + Lieferantennummer ermitteln
                                break;
                        }
                        break;

                    case PAC.Name:
                        PAC p = new PAC(str, asn.ASNFileTyp);
                        if (article is Articles)
                        {
                            article.Anzahl = p.f_7224_NumberOfPackage;
                        }
                        break;


                    case PCI.Name:
                        PCI pci = new PCI(str, asn.ASNFileTyp);

                        break;

                    case PIA.Name:
                        //PIA pia = new PIA(str, asn.ASNFileTyp);
                        break;

                    case QTY.Name:
                        QTY q = new QTY(str, asn.ASNFileTyp);
                        switch (q.C186.f_6063_QuantityQualifier)
                        {
                            case QTY.const_QuantityQualifier_C186_6063_12_DispatchQuantity:
                                break;
                            case QTY.const_QuantityQualifier_C186_6063_47_InvoicedQuantity:
                                break;
                            case QTY.const_QuantityQualifier_C186_6063_52_QuantityPerPack:
                                break;
                        }
                        break;

                    case RFF.Name:
                        RFF r = new RFF(str, asn.ASNFileTyp);
                        if (r.C506 is RFF_C506)
                        {
                            switch (r.C506.f_1153_RefQualifier)
                            {
                                //-- Lieferschein
                                case RFF.const_ReferenzQualifier_C506_1153_AAS_TransportDocumentNumber:
                                    if (eingang is Eingaenge)
                                    {
                                        eingang.LfsNr = r.C506.f_1154_RefNumber;
                                    }
                                    break;
                                case RFF.const_ReferenzQualifier_C506_1153_AAT_MasterLableNumber:
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_INVRPT_D96A:
                                            if (article is Articles)
                                            {
                                                article.Werksnummer = r.C506.f_1154_RefNumber;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case RFF.const_ReferenzQualifier_C506_1153_ADE_AcountNumber:
                                    break;
                                case RFF.const_ReferenzQualifier_C506_1153_ON_OrderNumber:
                                    article.Bestellnummer = r.C506.f_1154_RefNumber;
                                    break;
                                case RFF.const_ReferenzQualifier_C506_1153_VN_SalesOrderNumber:
                                    break;
                                default:
                                    switch (asn.ASNFileTyp)
                                    {
                                        case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                            if (article is Articles)
                                            {
                                                article.exBezeichnung = r.C506.f_1154_RefNumber;
                                            }
                                            break;
                                        default:

                                            break;
                                    }
                                    break;
                            }
                        }
                        break;

                    case TDT.Name:
                        TDT t = new TDT(str, asn.ASNFileTyp);
                        switch (asn.ASNFileTyp)
                        {
                            case constValue_AsnArt.const_Art_EDIFACT_ASN_D96A:
                                if (
                                    (t.C222.f_8212_VehicleId != null) &&
                                    (t.C222.f_8212_VehicleId.Length > 0)
                                  )
                                {
                                    if (eingang is Eingaenge)
                                    {
                                        if (t.C222.f_8212_VehicleId.Length > 10)
                                        {
                                            eingang.IsWaggon = true;
                                            eingang.IsShip = false;
                                            eingang.WaggonNr = t.C222.f_8212_VehicleId;
                                        }
                                        else
                                        {
                                            eingang.IsWaggon = false;
                                            eingang.IsShip = false;
                                            eingang.KFZ = t.C222.f_8212_VehicleId;
                                        }
                                    }
                                }
                                break;
                            default:

                                break;
                        }
                        break;

                    case TOD.Name:
                        break;


                    case UNB.Name:
                        UNB unb = new UNB(str);

                        if (unb.S002 is UNB_S002)
                        {
                            string sender = unb.S002.f_0004_SenderIdentification;
                            //if ((sender.Length>0) && (eingang is Eingaenge))
                            //{
                            //    if (ediClintWorkspaceValueVD.ListEdiAdrWorkspaceAssignments.Count > 0)
                            //    {
                            //        var tmpEdi = ediClintWorkspaceValueVD.ListEdiAdrWorkspaceAssignments.FirstOrDefault(x => x.WorkspaceId == (int)asn.WorkspaceId && x.Property == UNB_S002_0004_DefaultsValue);
                            //    }
                            //    eingang.AuftraggeberString = r.C506.f_1154_RefNumber;
                            //}
                        }
                        if (unb.S003 is UNB_S003)
                        {
                            string receiver = unb.S003.f_0010_ReceiverIdentification;
                            //if ((receiver.Length > 0) && (eingang is Eingaenge))
                            //{
                            //    eingang.EmpfaengerString = r.C506.f_1154_RefNumber;
                            //}
                        }
                        break;
                    case UNH.Name:
                        //UNH u = new UNH(str, asn.ASNFileTyp);
                        break;
                }
            }
        }
    }
}
