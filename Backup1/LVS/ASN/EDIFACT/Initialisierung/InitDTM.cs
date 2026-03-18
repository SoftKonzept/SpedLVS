using LVS.Communicator.EdiVDA;
using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitDTM
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitDTM(AsnArt myAsnArt, string myDTM_C507_2005, string myDTM_C507_2379, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = DTM.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "R";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "Despatch advice date";
            ediSegmentVD.EdiSegment.Created = DateTime.Now;
            ediSegmentVD.EdiSegment.tmpId = 0;
            ediSegmentVD.EdiSegment.Storable = false;
            ediSegmentVD.EdiSegment.Code = myCode;
            ediSegmentVD.EdiSegment.SortId = 1;
            ediSegmentVD.EdiSegment.IsActive = true;
            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    switch (ediSegmentVD.EdiSegment.Code)
                    {
                        //case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_NAD_DP:
                        //case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_RFF_AFV:
                        case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_DTM_102:
                            ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement;
                            break;
                    }
                    break;
                default:
                    ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = string.Empty;
                    break;
            }
            ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<Models.EdiSegmentElements>();

            EdiSegmentElements ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = DTM.Name;
            ese.Description = "Despatch advice date";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + DTM.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            EdiSegmentElementFields esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = DTM.Name;
            esef.Name = "Despatch advice date";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "const";
            esef.constValue = "DTM";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = myCode;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + DTM.Name;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);

            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = DTM_C507.Kennung;
            ese.Description = "Datum/Uhrzeit/Zeitspanne";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + DTM_C507.Kennung;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = DTM_C507.Kennung_2005;
            esef.Name = "Datums- oder Uhrzeits- oder Zeitspannenfunktion, Qualifier";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "Document issue date time";
            esef.constValue = myDTM_C507_2005;
            esef.Position = 2;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + DTM_C507.Kennung_2005;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = DTM_C507.Kennung_2380;
            esef.Name = "Datum oder Uhrzeit oder Zeitspanne Wert";
            esef.Status = "R";
            esef.Format = "a35";
            esef.Description = "Übertragungsdatum 711F07  YYMMDD";
            esef.constValue = myDTM_C507_2379;
            esef.Position = 3;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + DTM_C507.Kennung_2380;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = DTM_C507.Kennung_2379;
            esef.Name = "Datums- oder Uhrzeits- oder Zeitspannenfunktion, Qualifier";
            esef.Status = "O";
            esef.Format = "a3";
            esef.Description = "102 - CCYYMMDD 203 - CCYYMMDDHHMM";
            esef.constValue = "102";
            esef.Position = 4;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + DTM_C507.Kennung_2379;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);

        }
    }

}
