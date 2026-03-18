using LVS.ASN.EDIFACT.Initialisierung;
using LVS.Models;
using LVS.ViewData;
using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.Defaults
{
    public class Default_DELFOR_DESADV
    {
        internal WorkspaceViewData wsVD = new WorkspaceViewData();
        internal AddressViewData adrVD = new AddressViewData();
        internal AsnArtViewData asnVD = new AsnArtViewData();
        internal Globals._GL_USER GL_USER = new Globals._GL_USER();
        public Default_DELFOR_DESADV(int myDestAdrID,
                                    int myAsnArtId,
                                    int myWorkspaceId,
                                    Globals._GL_USER myGLUser)
        {
            bool bReturn = false;
            GL_USER = myGLUser;

            wsVD = new WorkspaceViewData(myWorkspaceId);
            adrVD = new AddressViewData(myDestAdrID, (int)GL_USER.User_ID);
            asnVD = new AsnArtViewData(myAsnArtId, (int)GL_USER.User_ID, false);


            List<EdiSegmentViewData> listEdiSegment = new List<EdiSegmentViewData>();

            InitUNB iUNB = new InitUNB(asnVD.AsnArt, UNB.Name);
            iUNB.ediSegmentVD.EdiSegment.SortId = 1;
            listEdiSegment.Add(iUNB.ediSegmentVD);

            InitUNH iUNH = new InitUNH(asnVD.AsnArt, UNH.Name);
            iUNH.ediSegmentVD.EdiSegment.SortId = 2;
            listEdiSegment.Add(iUNH.ediSegmentVD);

            InitBGM iBGM = new InitBGM(asnVD.AsnArt, "BGM");
            iBGM.ediSegmentVD.EdiSegment.SortId = 3;
            listEdiSegment.Add(iBGM.ediSegmentVD);

            InitDTM iDTM = new InitDTM(asnVD.AsnArt, "137", "102", "DTM#137");
            iDTM.ediSegmentVD.EdiSegment.SortId = 4;
            iDTM.ediSegmentVD.EdiSegment.Code = "DTM#137";
            var list = Default_DELFOR_DESADV.SetValueToEdiSegmentPropertyInList(iDTM.ediSegmentVD.EdiSegment.ListEdiSegmentElements.ToList(), "Code", iDTM.ediSegmentVD.EdiSegment.Code);
            iDTM.ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<EdiSegmentElements>(list);
            listEdiSegment.Add(iDTM.ediSegmentVD);


            InitNAD iNAD = new InitNAD(asnVD.AsnArt, "SU", "2135760", "NAD#SU");
            iNAD.ediSegmentVD.EdiSegment.SortId = 5;
            iNAD.ediSegmentVD.EdiSegment.Code = "NAD#SU";
            listEdiSegment.Add(iNAD.ediSegmentVD);

            iNAD = new InitNAD(asnVD.AsnArt, "SF", "2135760", "NAD#SF");
            iNAD.ediSegmentVD.EdiSegment.SortId = 6;
            iNAD.ediSegmentVD.EdiSegment.Code = "NAD#SF";
            listEdiSegment.Add(iNAD.ediSegmentVD);

            InitGIS iGIS = new InitGIS(asnVD.AsnArt, GIS.Name);
            iGIS.ediSegmentVD.EdiSegment.SortId = 7;
            listEdiSegment.Add(iGIS.ediSegmentVD);

            iNAD = new InitNAD(asnVD.AsnArt, "ST", "2135760", "NAD#ST");
            iNAD.ediSegmentVD.EdiSegment.SortId = 8;
            iNAD.ediSegmentVD.EdiSegment.Code = "NAD#ST";
            listEdiSegment.Add(iNAD.ediSegmentVD);

            InitLIN iLIN = new InitLIN(asnVD.AsnArt, "LIN");
            iLIN.ediSegmentVD.EdiSegment.SortId = 9;
            listEdiSegment.Add(iLIN.ediSegmentVD);

            InitIMD iIMD = new InitIMD(asnVD.AsnArt, "E", "IMD#E");
            iIMD.ediSegmentVD.EdiSegment.SortId = 10;
            iIMD.ediSegmentVD.EdiSegment.Code = "IMD#E";
            listEdiSegment.Add(iIMD.ediSegmentVD);

            InitRFF iRFF = new InitRFF(asnVD.AsnArt, "ON", "RFF#ON");
            iRFF.ediSegmentVD.EdiSegment.SortId = 11;
            iRFF.ediSegmentVD.EdiSegment.Description = "REFERENCE Order number";
            iRFF.ediSegmentVD.EdiSegment.Code = "RFF#ON";
            listEdiSegment.Add(iRFF.ediSegmentVD);

            iRFF = new InitRFF(asnVD.AsnArt, "AAN", "RFF#AAN");
            iRFF.ediSegmentVD.EdiSegment.SortId = 12;
            iRFF.ediSegmentVD.EdiSegment.Description = "REFERENCE Delivery schedule number";
            iRFF.ediSegmentVD.EdiSegment.Code = "RFF#AAN";
            listEdiSegment.Add(iRFF.ediSegmentVD);

            InitQTY iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#70");
            iQTY.ediSegmentVD.EdiSegment.SortId = 13;
            iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY Cumulative quantity received";
            iQTY.ediSegmentVD.EdiSegment.Code = "QTY#70";
            listEdiSegment.Add(iQTY.ediSegmentVD);

            iDTM = new InitDTM(asnVD.AsnArt, "51", "102", "DTM#51");
            iDTM.ediSegmentVD.EdiSegment.SortId = 14;
            iDTM.ediSegmentVD.EdiSegment.Description = "DATE Cumulative quantity start date";
            iDTM.ediSegmentVD.EdiSegment.Code = "DTM#51";
            listEdiSegment.Add(iDTM.ediSegmentVD);

            iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#48");
            iQTY.ediSegmentVD.EdiSegment.SortId = 14;
            iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY Received quantity";
            iQTY.ediSegmentVD.EdiSegment.Code = "QTY#48";
            listEdiSegment.Add(iQTY.ediSegmentVD);

            iRFF = new InitRFF(asnVD.AsnArt, "SI", "RFF#SI");
            iRFF.ediSegmentVD.EdiSegment.SortId = 15;
            iRFF.ediSegmentVD.EdiSegment.Description = "SID (Shipper's identifying number for shipment)";
            iRFF.ediSegmentVD.EdiSegment.Code = "RFF#SI";
            listEdiSegment.Add(iRFF.ediSegmentVD);

            iDTM = new InitDTM(asnVD.AsnArt, "50", "102", "DTM#50");
            iDTM.ediSegmentVD.EdiSegment.SortId = 16;
            iDTM.ediSegmentVD.EdiSegment.Code = "DTM#50";
            listEdiSegment.Add(iDTM.ediSegmentVD);

            InitSSC iSSC = new InitSSC(asnVD.AsnArt, "SSC#10");
            iSSC.ediSegmentVD.EdiSegment.SortId = 17;
            iSSC.ediSegmentVD.EdiSegment.Code = "SSC#10";
            listEdiSegment.Add(iSSC.ediSegmentVD);

            iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#1");
            iQTY.ediSegmentVD.EdiSegment.SortId = 18;
            iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY Discrete quantity";
            iQTY.ediSegmentVD.EdiSegment.Code = "QTY#1";
            listEdiSegment.Add(iQTY.ediSegmentVD);

            iDTM = new InitDTM(asnVD.AsnArt, "10", "102", "DTM#10");
            iDTM.ediSegmentVD.EdiSegment.SortId = 19;
            iDTM.ediSegmentVD.EdiSegment.Code = "DTM#10";
            listEdiSegment.Add(iDTM.ediSegmentVD);

            iSSC = new InitSSC(asnVD.AsnArt, "SSC#4");
            iSSC.ediSegmentVD.EdiSegment.SortId = 20;
            iQTY.ediSegmentVD.EdiSegment.Description = "SCHEDULING CONDITIONS Planning/forecast";
            iSSC.ediSegmentVD.EdiSegment.Code = "SSC#4";
            listEdiSegment.Add(iSSC.ediSegmentVD);

            iQTY = new InitQTY(asnVD.AsnArt, QTY.Name + "#1");
            iQTY.ediSegmentVD.EdiSegment.SortId = 21;
            iQTY.ediSegmentVD.EdiSegment.Description = "QUANTITY Discrete quantity";
            iQTY.ediSegmentVD.EdiSegment.Code = "QTY#1";
            listEdiSegment.Add(iQTY.ediSegmentVD);

            iDTM = new InitDTM(asnVD.AsnArt, "10", "102", "DTM#10");
            iDTM.ediSegmentVD.EdiSegment.SortId = 19;
            iDTM.ediSegmentVD.EdiSegment.Code = "DTM#10";
            listEdiSegment.Add(iDTM.ediSegmentVD);

            InitUNT iUNT = new InitUNT(asnVD.AsnArt, UNT.Name);
            iUNT.ediSegmentVD.EdiSegment.SortId = 20;
            iUNT.ediSegmentVD.EdiSegment.Code = "UNT";
            listEdiSegment.Add(iUNT.ediSegmentVD);

            InitUNZ iUNZ = new InitUNZ(asnVD.AsnArt, UNZ.Name);
            iUNZ.ediSegmentVD.EdiSegment.SortId = 21;
            iUNZ.ediSegmentVD.EdiSegment.Code = "UNZ";
            listEdiSegment.Add(iUNZ.ediSegmentVD);

            //return bReturn;
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
