using LVS.Constants;
using LVS.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class ediHelper_EdiEDIFACT_ASN_Check
    {        
        public ediHelper_EdiEDIFACT_ASN_Check(clsJobs myJob, List<string> myAsn)
        {
            //--- Check ASN Art
            string strDesAdvCheck = string.Empty;
            strDesAdvCheck = myAsn.FirstOrDefault(x => x.Contains(UNH.Name + "+"));
            if ((strDesAdvCheck != null) && (strDesAdvCheck.Length > 0) && (strDesAdvCheck.StartsWith(UNH.Name)))
            {
                string strToCheck = string.Empty;
                UNH uNH = new UNH(strDesAdvCheck, myJob.ASNFileTyp);
                if (uNH != null)
                {
                    if (uNH.S009 != null)
                    {
                        strToCheck += uNH.S009.f_0065_MessageTypIdentifier + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                        strToCheck += uNH.S009.f_0052_MessageTypVersion + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                        strToCheck += uNH.S009.f_0054_MessageRelease + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                        strToCheck += uNH.S009.f_0051_ControllingAgency + constValue_Edifact.const_Edifact_UNA1_GruppendatenelementTrennzeichen;
                    }
                }
                //IsAsnArtKorrekt = (strToCheck.Contains(ediHelper_EdiEDIFACT_ASN_D07A_CheckProcessableASN.const_UNH_S009));
                IsAsnArtKorrekt = ediHelper_EdiEDIFACT_ASN_CheckAsnArt.CheckAsnArt(strToCheck, myJob.ASNFileTyp);
            }
            InitAdrVerweis(myJob, myAsn);
        }

        private bool _IsAsnArtKorrekt = false;
        public bool IsAsnArtKorrekt 
        {
            
            get { return _IsAsnArtKorrekt; }
            set { _IsAsnArtKorrekt = value; }
        }

        //--- ADR Verweis existiert
        private bool _ExistsAdrVerweis = false;
        public bool ExistsAdrVerweis
        {

            get { return _ExistsAdrVerweis; }
            set { _ExistsAdrVerweis = value; }
        }

        private Dictionary<string, NAD> _DictNad = new Dictionary<string, NAD>();
        public Dictionary<string, NAD> DictNad
        {

            get { return _DictNad; }
            set { _DictNad = value; }
        }
        private Dictionary<string, clsADRVerweis> _DicSenderVerweis = new Dictionary<string, clsADRVerweis>();
        public Dictionary<string, clsADRVerweis> DicSenderVerweis
        {

            get { return _DicSenderVerweis; }
            set { _DicSenderVerweis = value; }
        }

        //--- Adr Verweis 
        private string _AdrVerweis = string.Empty;
        public string AdrVerweis
        {
            get 
            {
                _AdrVerweis = string.Empty;
                if (DictNad != null && DictNad.Count > 0)
                {
                    foreach (var nad in DictNad.Values)
                    {
                        _AdrVerweis += nad.C082.f_3039_PartyId + "#";
                    }
                    _AdrVerweis = _AdrVerweis.TrimEnd('#'); 
                }
                return _AdrVerweis; 
            }
            set { _AdrVerweis = value; }
        }

        //--- Adr Verweis Global 
        private string _AdrVerweisGlobal = string.Empty;
        public string AdrVerweisGlobal
        {
            get
            {
                _AdrVerweisGlobal = string.Empty;
                if (DictNad != null && DictNad.Count > 0)
                {
                    foreach (var nad in DictNad.Values)
                    {
                        _AdrVerweisGlobal += nad.C082.f_3039_PartyId + "#0#0";
                        break;
                    }
                }
                return _AdrVerweisGlobal;
            }
            set { _AdrVerweisGlobal = value; }
        }

        public clsADRVerweis AdrVerweisASN { get; set; } = new clsADRVerweis(); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myJob"></param>
        /// <param name="myAsn"></param>
        private void InitAdrVerweis(clsJobs myJob, List<string> myAsn)
        {
            List<string> listAdrSegmente = new List<string>();
            DicSenderVerweis = clsADRVerweis.FillDictAdrVerweis(0, 0, 1, myJob.ASNFileTyp);
            //ExistsAdrVerweis = DicSenderVerweis.ContainsKey(AdrVerweis);
            //if (!ExistsAdrVerweis)
            //{
            //    //-- dann CHeck Sender global
            //    ExistsAdrVerweis = DicSenderVerweis.ContainsKey(AdrVerweisGlobal);
            //}

            //--- Check welche AdrVerweis Variante wird verwendet
            //--- Standardmäßig NAD+CZ+#+NAD+CN+#+NAD+FW für den Sender
             
            string strAdrVerweis = string.Empty;

            if (Enum.TryParse<enumASNFileTyp>(myJob.ASNFileTyp, out var asnFileTyp))
            {
                switch (asnFileTyp)
                {
                    case enumASNFileTyp.EDIFACT_ASN_D96A:
                    case enumASNFileTyp.EDIFACT_ASN_D97A:
                        listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CZ)));
                        listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CN)));
                        listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_FW)));
                        break;

                    case enumASNFileTyp.EDIFACT_ASN_D07A:
                    case enumASNFileTyp.EDIFACT_DESADV_D07A:
                        listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_UNB_UNOC_3)));
                        listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_SF)));
                        listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_LOC_11)));
                        break;

                    //case enumASNFileTyp.EDIFACT_ASN_D96A:
                    //    break;
                    case enumASNFileTyp.EDIFACT_DELFOR_D97A:
                        break;
                    case enumASNFileTyp.EDIFACT_INVRPT_D96A:
                        break;
                    case enumASNFileTyp.EDIFACT_Qality_D96A:
                        break;
                    // Weitere Fälle können hier ergänzt werden
                    default:
                        break;
                }

                if (listAdrSegmente.Count >= 3)
                {
                    DictNad = new Dictionary<string, NAD>();

                    //-- Dictionary mit den NAD Segmenten füllen    
                    foreach (var nadSegment in listAdrSegmente)
                    {
                        if (!string.IsNullOrEmpty(nadSegment))
                        {
                            NAD nad = new NAD(nadSegment, myJob.ASNFileTyp);
                            if (nad != null)
                            {
                                DictNad[nadSegment] = nad;
                            }
                        }
                    }

                }
                //DicSenderVerweis = clsADRVerweis.FillDictAdrVerweis(0, 0, 1, myJob.ASNFileTyp);
                ExistsAdrVerweis = DicSenderVerweis.ContainsKey(AdrVerweis);
                //if (!ExistsAdrVerweis)
                //{
                //    //-- dann CHeck Sender global
                //    ExistsAdrVerweis = DicSenderVerweis.ContainsKey(AdrVerweisGlobal);
                //}
            }

            //--- Ein einzelner Verweis liegt vor = eine Adresse
            if (ExistsAdrVerweis)
            {
                if (!string.IsNullOrEmpty(AdrVerweis) && DicSenderVerweis != null)
                {
                    // Sichere Extraktion mit TryGetValue
                    if (DicSenderVerweis.TryGetValue(AdrVerweis, out var adrVerweis))
                    {
                        AdrVerweisASN = adrVerweis;
                    }
                }
            }
            else
            {
                //-- dann CHeck Sender global
                ExistsAdrVerweis = DicSenderVerweis.ContainsKey(AdrVerweisGlobal);
                if (ExistsAdrVerweis)
                {
                    if (!string.IsNullOrEmpty(AdrVerweisGlobal) && DicSenderVerweis != null)
                    {
                        // Sichere Extraktion mit TryGetValue
                        if (DicSenderVerweis.TryGetValue(AdrVerweisGlobal, out var adrVerweis))
                        {
                            AdrVerweisASN = adrVerweis;
                        }
                    }
                }
            }

            //--- Check AdrVerweis nach AdrId in ClsJob
            AddressReferenceViewData adrVD = new AddressReferenceViewData((int)myJob.AdrVerweisID, 1, true);
            if(adrVD.ListAddressReferences.Count>0)
            {
                //var listReferenceParts = new List<string>();
                ////-- Check Reference Part 1-3                
                //if ((!string.IsNullOrWhiteSpace(adrVD.adrReference.ReferencePart1)) || (!string.IsNullOrWhiteSpace(adrVD.adrReference.ReferencePart1)))
                //{
                //    listReferenceParts.Add(adrVD.adrReference.ReferencePart1);
                //}
                //if ((!string.IsNullOrWhiteSpace(adrVD.adrReference.ReferencePart2)) || (!string.IsNullOrWhiteSpace(adrVD.adrReference.ReferencePart2)))
                //{
                //    listReferenceParts.Add(adrVD.adrReference.ReferencePart2);
                //}
                //if ((!string.IsNullOrWhiteSpace(adrVD.adrReference.ReferencePart3)) || (!string.IsNullOrWhiteSpace(adrVD.adrReference.ReferencePart3)))
                //{
                //    listReferenceParts.Add(adrVD.adrReference.ReferencePart3);
                //}
                ////-- dann Check in ASN Segmenten
                //foreach (var part in listReferenceParts)
                //{
                //    if (!string.IsNullOrWhiteSpace(part))
                //    {
                //        listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().Contains(part)));
                //    }
                //}   


                //if (listAdrSegmente.Count >= 3)
                //{
                //    DictNad = new Dictionary<string, NAD>();
                //    //-- Dictionary mit den NAD Segmenten füllen    
                //    foreach (var nadSegment in listAdrSegmente)
                //    {
                //        if (!string.IsNullOrEmpty(nadSegment))
                //        {
                //            NAD nad = new NAD(nadSegment, myJob.ASNFileTyp);
                //            if (nad != null)
                //            {
                //                DictNad[nadSegment] = nad;
                //            }
                //        }
                //    }
                //}
                //ExistsAdrVerweis = DicSenderVerweis.ContainsKey(AdrVerweis);
            }
            else
            {
                //if (string.IsNullOrEmpty(mySegmentToCheck) || string.IsNullOrEmpty(myJob.ASNFileTyp))
                //    return false;
                //if (Enum.TryParse<enumASNFileTyp>(myJob.ASNFileTyp, out var asnFileTyp))
                //{
                //    switch (asnFileTyp)
                //    {
                //        case enumASNFileTyp.EDIFACT_ASN_D96A:
                //        case enumASNFileTyp.EDIFACT_ASN_D97A:
                //            listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CZ)));
                //            listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_CN)));
                //            listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_FW)));
                //            break;

                //        case enumASNFileTyp.EDIFACT_ASN_D07A:
                //        case enumASNFileTyp.EDIFACT_DESADV_D07A:
                //            listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_UNB_UNOC_3)));
                //            listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_NAD_SF)));
                //            listAdrSegmente.Add(myAsn.FirstOrDefault(x => x.ToString().StartsWith(constValue_Edifact.const_Edifact_LOC_11)));
                //            break;

                //        //case enumASNFileTyp.EDIFACT_ASN_D96A:
                //        //    break;
                //        case enumASNFileTyp.EDIFACT_DELFOR_D97A:
                //            break;
                //        case enumASNFileTyp.EDIFACT_INVRPT_D96A:
                //            break;
                //        case enumASNFileTyp.EDIFACT_Qality_D96A:
                //            break;
                //        // Weitere Fälle können hier ergänzt werden
                //        default:
                //            break;
                //    }

                //    if (listAdrSegmente.Count >= 3)
                //    {
                //        DictNad = new Dictionary<string, NAD>();

                //        //-- Dictionary mit den NAD Segmenten füllen    
                //        foreach (var nadSegment in listAdrSegmente)
                //        {
                //            if (!string.IsNullOrEmpty(nadSegment))
                //            {
                //                NAD nad = new NAD(nadSegment, myJob.ASNFileTyp);
                //                if (nad != null)
                //                {
                //                    DictNad[nadSegment] = nad;
                //                }
                //            }
                //        }

                //    }
                //    DicSenderVerweis = clsADRVerweis.FillDictAdrVerweis(0, 0, 1, myJob.ASNFileTyp);
                //    ExistsAdrVerweis = DicSenderVerweis.ContainsKey(AdrVerweis);
                //    if (!ExistsAdrVerweis)
                //    {
                //        //-- dann CHeck Sender global
                //        ExistsAdrVerweis = DicSenderVerweis.ContainsKey(AdrVerweisGlobal);
                //    }

                //}
            }
        }
    }
}
