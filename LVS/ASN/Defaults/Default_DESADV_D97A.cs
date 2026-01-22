using LVS.ASN.EDIFACT.Initialisierung;
using LVS.Models;
using LVS.ViewData;
using System.Collections.Generic;

namespace LVS.ASN.Defaults
{
    public class Default_DESADV_D97A
    {
        public const string const_UNH_S009 = "DESADV:D:97A:UN:";

        List<EdiSegments> listEdiSegment = new List<EdiSegments>();

        internal WorkspaceViewData wsVD = new WorkspaceViewData();
        internal AddressViewData adrVD = new AddressViewData();
        internal AsnArtViewData asnVD = new AsnArtViewData();
        internal Globals._GL_USER GL_USER = new Globals._GL_USER();
        public Default_DESADV_D97A(int myDestAdrID,
                                   int myAsnArtId,
                                   int myWorkspaceId,
                                   Globals._GL_USER myGLUser)
        {
            bool bReturn = false;
            GL_USER = myGLUser;
            bool bCreateStruckture = true;
            bool bDeleteExistingStruckture = true;

            if (myAsnArtId >= 15)
            {
                //--- Check ob bereits das Mapping hitnerlegt ist und löschen
                EdiSegmentViewData esVD = new EdiSegmentViewData(myAsnArtId, (int)GL_USER.User_ID);
                esVD.FillListAndDictEdiSegment();

                if (esVD.ListEdiSegments.Count > 0)
                {
                    string strError = "Zur gewählten ASNArt sind bereits eine EDI Strucktur hinterlegt. Möchten Sie Sie die bestehende Strucktur ersetzen?";
                    bDeleteExistingStruckture = clsMessages.Allgemein_SelectionInfoTextShow(strError);

                    if (bDeleteExistingStruckture)
                    {
                        foreach (EdiSegments es in esVD.ListEdiSegments)
                        {
                            esVD = new EdiSegmentViewData(es);
                            foreach (EdiSegmentElements ese in es.ListEdiSegmentElements)
                            {
                                EdiSegmentElementViewData eseVD = new EdiSegmentElementViewData(ese);

                                foreach (EdiSegmentElementFields esef in ese.ListEdiSegmentElementFields)
                                {
                                    EdiSegmentElementFieldViewData esefVD = new EdiSegmentElementFieldViewData(esef);

                                    VDAClientValueViewData cvVD = new VDAClientValueViewData();
                                    cvVD.DeleteByEdiSegementElementFieldId((int)esef.Id);
                                    //cvVD.DeleteByEdiSegementElementFieldId((int)esef.AsnArtId);

                                    esefVD.Delete();
                                }
                                eseVD.Delete();
                            }
                            esVD.Delete();
                        }
                    }
                    else
                    {
                        bCreateStruckture = false;
                    }
                }
                else
                {
                    bCreateStruckture = true;
                }

                if (bCreateStruckture)
                {
                    wsVD = new WorkspaceViewData(myWorkspaceId);
                    adrVD = new AddressViewData(myDestAdrID, (int)GL_USER.User_ID);
                    asnVD = new AsnArtViewData(myAsnArtId, (int)GL_USER.User_ID, false);
                    listEdiSegment = new List<EdiSegments>();

                    InitUNA iUNA = new InitUNA(asnVD.AsnArt);
                    iUNA.ediSegmentVD.EdiSegment.SortId = 1;
                    //iUNA.ediSegmentVD.EdiSegment.AsnArtId = myAsnArtId;
                    listEdiSegment.Add(iUNA.ediSegmentVD.EdiSegment);

                    InitUNB iUNB = new InitUNB(asnVD.AsnArt, UNB.Name);
                    iUNB.ediSegmentVD.EdiSegment.SortId = 2;
                    //iUNB.ediSegmentVD.EdiSegment.AsnArtId = myAsnArtId;
                    listEdiSegment.Add(iUNB.ediSegmentVD.EdiSegment);

                    InitUNH iUNH = new InitUNH(asnVD.AsnArt, UNH.Name);
                    iUNH.ediSegmentVD.EdiSegment.SortId = 3;
                    //iUNH.ediSegmentVD.EdiSegment.AsnArtId = myAsnArtId;
                    listEdiSegment.Add(iUNH.ediSegmentVD.EdiSegment);

                    InitBGM iBGM = new InitBGM(asnVD.AsnArt, BGM.Name);
                    iBGM.ediSegmentVD.EdiSegment.SortId = 4;
                    //iBGM.ediSegmentVD.EdiSegment.AsnArtId = myAsnArtId;
                    listEdiSegment.Add(iBGM.ediSegmentVD.EdiSegment);

                    InitDTM iDTM = new InitDTM(asnVD.AsnArt, "11", "203", "DTM#11");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 5;
                    //iDTM.ediSegmentVD.EdiSegment.AsnArtId = myAsnArtId;
                    //iDTM.ediSegmentVD.EdiSegment.Code = "DTM#11";
                    //var list = Default_DELFOR_DESADV.SetValueToEdiSegmentPropertyInList(iDTM.ediSegmentVD.EdiSegment.ListEdiSegmentElements.ToList(), "Code", iDTM.ediSegmentVD.EdiSegment.Code);
                    //iDTM.ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<EdiSegmentElements>(list);
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    iDTM = new InitDTM(asnVD.AsnArt, "137", "203", "DTM#137");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 6;
                    //iDTM.ediSegmentVD.EdiSegment.AsnArtId = myAsnArtId;
                    //iDTM.ediSegmentVD.EdiSegment.Code = "DTM#137";
                    //list = Default_DELFOR_DESADV.SetValueToEdiSegmentPropertyInList(iDTM.ediSegmentVD.EdiSegment.ListEdiSegmentElements.ToList(), "Code", iDTM.ediSegmentVD.EdiSegment.Code);
                    //iDTM.ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<EdiSegmentElements>(list);
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    InitRFF iRFF = new InitRFF(asnVD.AsnArt, "AEV", "RFF#AEV");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 7;
                    iRFF.ediSegmentVD.EdiSegment.Description = "REFERENCE Order number";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    InitNAD iNAD = new InitNAD(asnVD.AsnArt, "ST", "0018", "NAD#ST");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 8;
                    //iNAD.ediSegmentVD.EdiSegment.Code = "NAD#ST";
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    iNAD = new InitNAD(asnVD.AsnArt, "SU", "2135760", "NAD#SU");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 9;
                    iNAD.ediSegmentVD.EdiSegment.Code = "NAD#SU";
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    InitEQD iEQD = new InitEQD(asnVD.AsnArt, EQD.Name);
                    iEQD.ediSegmentVD.EdiSegment.SortId = 10;
                    listEdiSegment.Add(iEQD.ediSegmentVD.EdiSegment);

                    //--- Pro Artikel auf Lieferschein
                    InitCPS iCPS = new InitCPS(asnVD.AsnArt, CPS.Name);
                    iCPS.ediSegmentVD.EdiSegment.SortId = 11;
                    //iCPS.ediSegmentVD.EdiSegment.Code = CPS.Name;
                    listEdiSegment.Add(iCPS.ediSegmentVD.EdiSegment);

                    InitPAC iPAC = new InitPAC(asnVD.AsnArt, PAC.Name);
                    iPAC.ediSegmentVD.EdiSegment.SortId = 12;
                    //iPAC.ediSegmentVD.EdiSegment.Code = PAC.Name;
                    listEdiSegment.Add(iPAC.ediSegmentVD.EdiSegment);

                    InitQTY iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#52");
                    iQTY.ediSegmentVD.EdiSegment.SortId = 13;
                    iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY";
                    //iQTY.ediSegmentVD.EdiSegment.Code = QTY.Name + "#52";
                    listEdiSegment.Add(iQTY.ediSegmentVD.EdiSegment);

                    InitPCI iPCI = new InitPCI(asnVD.AsnArt, PCI.Name);
                    iPCI.ediSegmentVD.EdiSegment.SortId = 14;
                    listEdiSegment.Add(iPCI.ediSegmentVD.EdiSegment);

                    InitGIR iGIR = new InitGIR(asnVD.AsnArt, GIR.Name);
                    iGIR.ediSegmentVD.EdiSegment.SortId = 15;
                    listEdiSegment.Add(iGIR.ediSegmentVD.EdiSegment);

                    InitLIN iLIN = new InitLIN(asnVD.AsnArt, LIN.Name);
                    iLIN.ediSegmentVD.EdiSegment.SortId = 16;
                    listEdiSegment.Add(iLIN.ediSegmentVD.EdiSegment);

                    iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#12");
                    iQTY.ediSegmentVD.EdiSegment.SortId = 17;
                    iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY";
                    //iQTY.ediSegmentVD.EdiSegment.Code = QTY.Name + "#12";
                    listEdiSegment.Add(iQTY.ediSegmentVD.EdiSegment);

                    iRFF = new InitRFF(asnVD.AsnArt, "ON", "RFF#ON");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 18;
                    iRFF.ediSegmentVD.EdiSegment.Description = "REFERENCE Order number";
                    //iRFF.ediSegmentVD.EdiSegment.Code = "RFF#ON";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    //-- mr neu hinzu für Benteler 
                    iRFF = new InitRFF(asnVD.AsnArt, "BT", "RFF#BT");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 19;
                    iRFF.ediSegmentVD.EdiSegment.Description = "REFERENCE Charge";
                    //iRFF.ediSegmentVD.EdiSegment.Code = "RFF#ON";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);


                    InitUNT iUNT = new InitUNT(asnVD.AsnArt, UNT.Name);
                    iUNT.ediSegmentVD.EdiSegment.SortId = 20;
                    //iUNT.ediSegmentVD.EdiSegment.Code = "UNT";
                    listEdiSegment.Add(iUNT.ediSegmentVD.EdiSegment);

                    InitUNZ iUNZ = new InitUNZ(asnVD.AsnArt, UNZ.Name);
                    iUNZ.ediSegmentVD.EdiSegment.SortId = 21;
                    //iUNZ.ediSegmentVD.EdiSegment.Code = "UNZ";
                    listEdiSegment.Add(iUNZ.ediSegmentVD.EdiSegment);

                    int iEsSort = 0;
                    int iEsefSort = 0;
                    //Insert / Create Struckture
                    foreach (EdiSegments es in listEdiSegment)
                    {
                        iEsSort++;
                        es.SortId = iEsSort;
                        esVD = new EdiSegmentViewData();
                        esVD = new EdiSegmentViewData(es);
                        esVD.Add();
                        if (esVD.EdiSegment.Id > 0)
                        {
                            int iEseSort = 0;
                            foreach (EdiSegmentElements ese in esVD.EdiSegment.ListEdiSegmentElements)
                            {
                                ese.EdiSegmentId = (int)esVD.EdiSegment.Id;
                                iEseSort++;
                                ese.SortId = iEseSort;
                                EdiSegmentElementViewData eseVD = new EdiSegmentElementViewData(ese);
                                eseVD.Add();

                                foreach (EdiSegmentElementFields esef in eseVD.EdiSegmentElement.ListEdiSegmentElementFields)
                                {
                                    iEsefSort++;
                                    esef.SortId = iEsefSort;
                                    esef.EdiSemgentElementId = (int)eseVD.EdiSegmentElement.Id;
                                    esef.EdiSegmentId = (int)esVD.EdiSegment.Id;
                                    esef.AsnArtId = (int)asnVD.AsnArt.Id;
                                    EdiSegmentElementFieldViewData esefVD = new EdiSegmentElementFieldViewData(esef);
                                    esefVD.Add();

                                    Default_VDAClientOut defVDAClientOut = new Default_VDAClientOut(esef, adrVD.Address, asnVD.AsnArt);

                                    VDAClientValueViewData vdaClientVD = new VDAClientValueViewData();
                                    vdaClientVD.VdaClientValue = defVDAClientOut.VDAClientValues.Copy();
                                    vdaClientVD.Add();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                string strError = "Die gewählte ASNArt ist noch nicht als Default hinterlegt!";
                clsMessages.Allgemein_ERRORTextShow(strError);
            }
        }

        public static List<EdiSegmentElements> SetValueToEdiSegmentPropertyInList(List<EdiSegmentElements> myList, string myProperty, string myValue)
        {
            foreach (EdiSegmentElements ese in myList)
            {
                switch (myProperty)
                {
                    case "Code":
                        ese.Code = myValue;
                        break;
                }
                foreach (EdiSegmentElementFields esef in ese.ListEdiSegmentElementFields)
                {
                    switch (myProperty)
                    {
                        case "Code":
                            esef.Code = myValue;
                            break;
                    }
                }
            }
            return myList;
        }
    }
}
