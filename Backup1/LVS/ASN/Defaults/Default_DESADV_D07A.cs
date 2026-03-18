using LVS.ASN.EDIFACT.Initialisierung;
using LVS.Models;
using LVS.ViewData;
using System.Collections.Generic;

namespace LVS.ASN.Defaults
{
    /// <summary>
    ///             DESADV:D:07A für Mendritzki in Plettenberg
    /// </summary>
    public class Default_DESADV_D07A
    {
        public const string const_UNH_S009 = "DESADV:D:07A:UN:";

        List<EdiSegments> listEdiSegment = new List<EdiSegments>();

        internal WorkspaceViewData wsVD = new WorkspaceViewData();
        internal AddressViewData adrVD = new AddressViewData();
        internal AsnArtViewData asnVD = new AsnArtViewData();
        internal Globals._GL_USER GL_USER = new Globals._GL_USER();
        public Default_DESADV_D07A(int myDestAdrID,
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
                    listEdiSegment.Add(iUNA.ediSegmentVD.EdiSegment);

                    InitUNB iUNB = new InitUNB(asnVD.AsnArt, UNB.Name);
                    iUNB.ediSegmentVD.EdiSegment.SortId = 2;
                    listEdiSegment.Add(iUNB.ediSegmentVD.EdiSegment);

                    InitUNH iUNH = new InitUNH(asnVD.AsnArt, UNH.Name);
                    iUNH.ediSegmentVD.EdiSegment.SortId = 3;
                    listEdiSegment.Add(iUNH.ediSegmentVD.EdiSegment);

                    InitBGM iBGM = new InitBGM(asnVD.AsnArt, BGM.Name);
                    iBGM.ediSegmentVD.EdiSegment.SortId = 4;
                    listEdiSegment.Add(iBGM.ediSegmentVD.EdiSegment);

                    InitDTM iDTM = new InitDTM(asnVD.AsnArt, "351", "203", "DTM#351");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 5;
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    iDTM = new InitDTM(asnVD.AsnArt, "137", "203", "DTM#137");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 6;
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    iDTM = new InitDTM(asnVD.AsnArt, "132", "102", "DTM#132");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 6;
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    InitMEA iMEA = new InitMEA(asnVD.AsnArt, "AAX", "AAD", "MEA#AAX#AAD");
                    iMEA.ediSegmentVD.EdiSegment.SortId = 7;
                    listEdiSegment.Add(iMEA.ediSegmentVD.EdiSegment);

                    iMEA = new InitMEA(asnVD.AsnArt, "AAX", "AAL", "MEA#AAX#AAL");
                    iMEA.ediSegmentVD.EdiSegment.SortId = 8;
                    listEdiSegment.Add(iMEA.ediSegmentVD.EdiSegment);

                    InitRFF iRFF = new InitRFF(asnVD.AsnArt, "AAS", "RFF#AAS");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 9;
                    iRFF.ediSegmentVD.EdiSegment.Description = "REFERENCE Order number";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    iRFF = new InitRFF(asnVD.AsnArt, "PK", "RFF#PK");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 10;
                    iRFF.ediSegmentVD.EdiSegment.Description = "";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    InitNAD iNAD = new InitNAD(asnVD.AsnArt, "SF", "000215217", "NAD#SF");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 11;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    iNAD = new InitNAD(asnVD.AsnArt, "SF", "0201", "NAD#ST");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 12;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    InitLOC iLOC = new InitLOC(asnVD.AsnArt, "LOC#11");
                    iLOC.ediSegmentVD.EdiSegment.SortId = 13;
                    listEdiSegment.Add(iLOC.ediSegmentVD.EdiSegment);

                    iNAD = new InitNAD(asnVD.AsnArt, "SE", "000215217", "NAD#SE");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 14;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    iNAD = new InitNAD(asnVD.AsnArt, "BY", "0201", "NAD#BY");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 15;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    iLOC = new InitLOC(asnVD.AsnArt, "LOC#11#GLAUCHAU");
                    iLOC.ediSegmentVD.EdiSegment.SortId = 16;
                    listEdiSegment.Add(iLOC.ediSegmentVD.EdiSegment);


                    //--- Pro Artikel auf Lieferschein
                    InitCPS iCPS = new InitCPS(asnVD.AsnArt, CPS.Name);
                    iCPS.ediSegmentVD.EdiSegment.SortId = 14;
                    listEdiSegment.Add(iCPS.ediSegmentVD.EdiSegment);

                    InitPAC iPAC = new InitPAC(asnVD.AsnArt, PAC.Name);
                    iPAC.ediSegmentVD.EdiSegment.SortId = 15;
                    listEdiSegment.Add(iPAC.ediSegmentVD.EdiSegment);

                    InitQTY iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#52");
                    iQTY.ediSegmentVD.EdiSegment.SortId = 16;
                    iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY";
                    listEdiSegment.Add(iQTY.ediSegmentVD.EdiSegment);

                    InitPCI iPCI = new InitPCI(asnVD.AsnArt, PCI.Name + "#17");
                    iPCI.ediSegmentVD.EdiSegment.SortId = 17;
                    listEdiSegment.Add(iPCI.ediSegmentVD.EdiSegment);

                    InitGIN iGIN = new InitGIN(asnVD.AsnArt, GIN.Name + "#ML");
                    iGIN.ediSegmentVD.EdiSegment.SortId = 18;
                    listEdiSegment.Add(iGIN.ediSegmentVD.EdiSegment);

                    InitLIN iLIN = new InitLIN(asnVD.AsnArt, LIN.Name);
                    iLIN.ediSegmentVD.EdiSegment.SortId = 19;
                    listEdiSegment.Add(iLIN.ediSegmentVD.EdiSegment);

                    InitPIA iPIA = new InitPIA(asnVD.AsnArt, PIA.Name + "#1");
                    iPIA.ediSegmentVD.EdiSegment.SortId = 20;
                    listEdiSegment.Add(iPIA.ediSegmentVD.EdiSegment);

                    InitIMD iIMD = new InitIMD(asnVD.AsnArt, string.Empty, IMD.Name);
                    iIMD.ediSegmentVD.EdiSegment.SortId = 21;
                    listEdiSegment.Add(iIMD.ediSegmentVD.EdiSegment);

                    iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#12");
                    iQTY.ediSegmentVD.EdiSegment.SortId = 22;
                    iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY";
                    listEdiSegment.Add(iQTY.ediSegmentVD.EdiSegment);

                    InitALI iALI = new InitALI(asnVD.AsnArt, ALI.Name);
                    iALI.ediSegmentVD.EdiSegment.SortId = 23;
                    listEdiSegment.Add(iALI.ediSegmentVD.EdiSegment);

                    iDTM = new InitDTM(asnVD.AsnArt, "171", "102", "DTM#171");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 24;
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    iRFF = new InitRFF(asnVD.AsnArt, "AAU", "RFF#AAU");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 25;
                    iRFF.ediSegmentVD.EdiSegment.Description = "Despatch Advice Number";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    iRFF = new InitRFF(asnVD.AsnArt, "ON", "RFF#ON");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 26;
                    iRFF.ediSegmentVD.EdiSegment.Description = "REFERENCE Order number";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    InitUNT iUNT = new InitUNT(asnVD.AsnArt, UNT.Name);
                    iUNT.ediSegmentVD.EdiSegment.SortId = 27;
                    listEdiSegment.Add(iUNT.ediSegmentVD.EdiSegment);

                    InitUNZ iUNZ = new InitUNZ(asnVD.AsnArt, UNZ.Name);
                    iUNZ.ediSegmentVD.EdiSegment.SortId = 28;
                    listEdiSegment.Add(iUNZ.ediSegmentVD.EdiSegment);

                    ///============================================================================
                    ///
                    ///  --- Verarbeitung Mendritzki nur eingehende Meldungen
                    ///

                    int iEsSort = 0;
                    int iEsefSort = 0;
                    //Insert / Create Struckture
                    foreach (EdiSegments es in listEdiSegment)
                    {
                        iEsSort++;
                        es.SortId = iEsSort;
                        esVD = new EdiSegmentViewData();
                        esVD = new EdiSegmentViewData(es);

                        if (esVD.EdiSegment.Name.Equals("MEA"))
                        {
                            string str = string.Empty;
                        }
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
