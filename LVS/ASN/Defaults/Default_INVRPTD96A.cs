using LVS.ASN.EDIFACT.Initialisierung;
using LVS.Models;
using LVS.ViewData;
using System.Collections.Generic;

namespace LVS.ASN.Defaults
{
    public class Default_INVRPTD96A
    {
        public const string const_UNH_S009 = "DESADV:D:96A:UN:";

        List<EdiSegments> listEdiSegment = new List<EdiSegments>();

        internal WorkspaceViewData wsVD = new WorkspaceViewData();
        internal AddressViewData adrVD = new AddressViewData();
        internal AsnArtViewData asnVD = new AsnArtViewData();
        internal Globals._GL_USER GL_USER = new Globals._GL_USER();
        public Default_INVRPTD96A(int myDestAdrID,
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

                    InitDTM iDTM = new InitDTM(asnVD.AsnArt, "182", "203", "DTM#182");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 5;
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    InitNAD iNAD = new InitNAD(asnVD.AsnArt, "GM", "", "NAD#GM");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 6;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    iNAD = new InitNAD(asnVD.AsnArt, "WH", "", "NAD#WH");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 7;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    InitLIN iLIN = new InitLIN(asnVD.AsnArt, LIN.Name);
                    iLIN.ediSegmentVD.EdiSegment.SortId = 8;
                    listEdiSegment.Add(iLIN.ediSegmentVD.EdiSegment);

                    InitPIA iPia = new InitPIA(asnVD.AsnArt, PIA.Name);
                    iPia.ediSegmentVD.EdiSegment.SortId = 9;
                    listEdiSegment.Add(iPia.ediSegmentVD.EdiSegment);

                    InitIMD iImd = new InitIMD(asnVD.AsnArt, "", IMD.Name);
                    iImd.ediSegmentVD.EdiSegment.SortId = 10;
                    listEdiSegment.Add(iImd.ediSegmentVD.EdiSegment);

                    InitRFF iRFF = new InitRFF(asnVD.AsnArt, "ON", "RFF#ON");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 11;
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    InitQTY iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#156");
                    iQTY.ediSegmentVD.EdiSegment.SortId = 12;
                    iQTY.ediSegmentVD.EdiSegment.Description = "Inventory Movement Quantity";
                    listEdiSegment.Add(iQTY.ediSegmentVD.EdiSegment);

                    InitINV iInv = new InitINV(asnVD.AsnArt, INV.Name);
                    iInv.ediSegmentVD.EdiSegment.SortId = 13;
                    listEdiSegment.Add(iInv.ediSegmentVD.EdiSegment);

                    iDTM = new InitDTM(asnVD.AsnArt, "179", "203", "DTM#179");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 14;
                    iDTM.ediSegmentVD.EdiSegment.Description = "Movement Booking Datetime";
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    iNAD = new InitNAD(asnVD.AsnArt, "BY", "1111", NAD.Name + "#BY");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 15;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    iNAD = new InitNAD(asnVD.AsnArt, "SE", "1111", NAD.Name + "#SE");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 16;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    iNAD = new InitNAD(asnVD.AsnArt, "CN", "1111", NAD.Name + "#CN");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 17;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    //--- zusatz bei AML
                    iNAD = new InitNAD(asnVD.AsnArt, "DP", "1111", NAD.Name + "#DP");
                    iNAD.ediSegmentVD.EdiSegment.SortId = 18;
                    listEdiSegment.Add(iNAD.ediSegmentVD.EdiSegment);

                    iRFF = new InitRFF(asnVD.AsnArt, "AAK", RFF.Name + "#AAK");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 19;
                    iRFF.ediSegmentVD.EdiSegment.Description = "Despatch advice number Document Number";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    //--- zusatz bei AML
                    iRFF = new InitRFF(asnVD.AsnArt, "AFV", RFF.Name + "#AFV");
                    iRFF.ediSegmentVD.EdiSegment.SortId = 20;
                    iRFF.ediSegmentVD.EdiSegment.Description = "Consumption Stock Movement Reference Document Number";
                    listEdiSegment.Add(iRFF.ediSegmentVD.EdiSegment);

                    //--- zusatz bei AML
                    iDTM = new InitDTM(asnVD.AsnArt, "102", "203", DTM.Name + "#102");
                    iDTM.ediSegmentVD.EdiSegment.SortId = 21;
                    iDTM.ediSegmentVD.EdiSegment.Description = "Movement Consumption Date";
                    listEdiSegment.Add(iDTM.ediSegmentVD.EdiSegment);

                    InitCPS iCPS = new InitCPS(asnVD.AsnArt, CPS.Name);
                    iCPS.ediSegmentVD.EdiSegment.SortId = 22;
                    listEdiSegment.Add(iCPS.ediSegmentVD.EdiSegment);

                    InitPAC iPAC = new InitPAC(asnVD.AsnArt, PAC.Name);
                    iPAC.ediSegmentVD.EdiSegment.SortId = 23;
                    listEdiSegment.Add(iPAC.ediSegmentVD.EdiSegment);

                    iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#52");
                    iQTY.ediSegmentVD.EdiSegment.SortId = 24;
                    iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY per pack";
                    listEdiSegment.Add(iQTY.ediSegmentVD.EdiSegment);

                    InitPCI iPCI = new InitPCI(asnVD.AsnArt, PCI.Name);
                    iPCI.ediSegmentVD.EdiSegment.SortId = 25;
                    listEdiSegment.Add(iPCI.ediSegmentVD.EdiSegment);

                    InitGIN iGin = new InitGIN(asnVD.AsnArt, GIN.Name + "#ML");
                    iGin.ediSegmentVD.EdiSegment.SortId = 26;
                    listEdiSegment.Add(iGin.ediSegmentVD.EdiSegment);

                    InitUNT iUNT = new InitUNT(asnVD.AsnArt, UNT.Name);
                    iUNT.ediSegmentVD.EdiSegment.SortId = 15;
                    //iUNT.ediSegmentVD.EdiSegment.Code = "UNT";
                    listEdiSegment.Add(iUNT.ediSegmentVD.EdiSegment);

                    InitUNZ iUNZ = new InitUNZ(asnVD.AsnArt, UNZ.Name);
                    iUNZ.ediSegmentVD.EdiSegment.SortId = 16;
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
